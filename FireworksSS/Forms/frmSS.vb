'--------------------------------------------------------------------------------------------------
' FireworksSS: frmSS.vb
'    © 2026 Remus Rigo
'       v1.0.20261104
'--------------------------------------------------------------------------------------------------

Imports FireworksSS.API

Public Class frmSS

   Public PreviewHandle As IntPtr = IntPtr.Zero   ' set by Main for /p
   Private _isPreview As Boolean = False
   Private _scale As Single = 1.0F                ' < 1 in preview so bursts fit the small window

   Private mouseStart As Point? = Nothing         ' Nothing until the first MouseMove

   Private ReadOnly _rockets As New List(Of Rocket)
   Private ReadOnly _particles As New List(Of Particle)
   Private ReadOnly _rnd As New Random()
   Private WithEvents _timer As New Timer() With {.Interval = 16}   ' ~60 FPS

   '-----------------------------------------------------------------------------------------------
   ' Explode: Create particles from a rocket
   Private Sub Explode(r As Rocket)
      ' BurstSize is the average; vary it ±30%, and use about a third in the small preview
      Dim avg As Double = cfg.BurstSize * If(_isPreview, 0.35, 1.0)
      Dim count As Integer = _rnd.Next(Math.Max(1, CInt(avg * 0.7)), CInt(avg * 1.3) + 1)
      Dim maxSpeed As Double = (3 + _rnd.NextDouble() * 3) * _scale

      ' Occasionally use a second colour for a two-tone burst
      Dim colour2 As Color = Color.FromArgb(_rnd.Next(120, 256), _rnd.Next(120, 256), _rnd.Next(120, 256))
      Dim twoTone As Boolean = _rnd.NextDouble() < 0.3

      ' ParticleLife (ms) is the average lifetime; Life goes 1 -> 0, so decay = 1 / ticks
      Dim baseDecay As Double = _timer.Interval / CDbl(cfg.ParticleLife)

      For i As Integer = 1 To count
         Dim angle As Double = _rnd.NextDouble() * 2 * Math.PI
         ' Square root gives an evenly filled circle instead of a dense centre
         Dim speed As Double = Math.Sqrt(_rnd.NextDouble()) * maxSpeed
         Dim c As Color = If(twoTone AndAlso i Mod 2 = 0, colour2, r.RocketColor)
         Dim decay As Single = CSng(baseDecay * (0.6 + _rnd.NextDouble() * 0.8))   ' ±40% variation

         _particles.Add(New Particle(r.X, r.Y, CSng(Math.Cos(angle) * speed), CSng(Math.Sin(angle) * speed), c, decay, _scale))
      Next
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' _timer: Tick
   Private Sub _timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
      ' The preview host (Screen Saver Settings) was closed or switched to another screensaver
      If _isPreview AndAlso Not IsWindow(PreviewHandle) Then
         Me.Close()
         Return
      End If

      ' Randomly launch a new rocket: LaunchRate per second -> chance per tick
      If _rnd.NextDouble() < cfg.LaunchRate * _timer.Interval / 1000.0 Then
         Dim r As Rocket
         If _isPreview Then
            r = New Rocket(Me.ClientSize.Width, Me.ClientSize.Height, _rnd)
         Else
            ' Full screen spans the whole virtual screen, which can include areas no monitor shows
            ' (monitors of different heights or offset vertically). Launch inside one real monitor.
            Dim screens As Screen() = Screen.AllScreens
            Dim area As Rectangle = Me.RectangleToClient(screens(_rnd.Next(screens.Length)).Bounds)
            r = New Rocket(area.Width, area.Height, _rnd)
            r.X += area.Left    ' Rocket works in 0..width / 0..height, move it onto that monitor
            r.Y += area.Top
         End If
         r.VX *= _scale
         r.Scale = _scale
         _rockets.Add(r)
      End If

      For i As Integer = _rockets.Count - 1 To 0 Step -1
         _rockets(i).Update()
         If _rockets(i).Exploded Then
            Explode(_rockets(i))
            _rockets.RemoveAt(i)
         End If
      Next

      For i As Integer = _particles.Count - 1 To 0 Step -1
         _particles(i).Update()
         If _particles(i).IsDead Then _particles.RemoveAt(i)
      Next

      Me.Invalidate()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: Load
   Private Sub frmSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      ' Common settings. ShowInTaskbar recreates the window handle,
      ' so it must be set BEFORE SetParent in preview mode.
      With Me
         .AutoScaleMode = AutoScaleMode.None
         .FormBorderStyle = FormBorderStyle.None
         .StartPosition = FormStartPosition.Manual
         .ShowInTaskbar = False
         .BackColor = Color.Black
         .DoubleBuffered = True
         .KeyPreview = True
      End With

      If PreviewHandle <> IntPtr.Zero Then
         ' Preview: become a child of the little monitor in Screen Saver Settings
         _isPreview = True

         Dim rc As RECT
         GetClientRect(PreviewHandle, rc)

         SetParent(Me.Handle, PreviewHandle)
         Dim style As Integer = GetWindowLong(Me.Handle, GWL_STYLE)
         SetWindowLong(Me.Handle, GWL_STYLE, (style And Not WS_POPUP) Or WS_CHILD)

         Me.Bounds = New Rectangle(0, 0, rc.Right - rc.Left, rc.Bottom - rc.Top)

         ' Keep the same proportions as full screen
         _scale = CSng(Me.ClientSize.Height / Screen.PrimaryScreen.Bounds.Height)
      Else
         ' Full screen: cover all monitors
         Me.Bounds = SystemInformation.VirtualScreen ' set bounds after StartPosition, so it doesn't get overridden
#If Not DEBUG Then
         Me.TopMost = True
#End If
         Cursor.Hide()
      End If

      _timer.Start()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: FormClosed
   Private Sub frmSS_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
      _timer.Stop()
      If Not _isPreview Then Cursor.Show()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: DpiChanged
   ' The full-screen window spans all monitors. With mixed scaling (e.g. 100% + 150%) Windows can
   ' decide it moved to another monitor and WinForms would rescale/move it, leaving gaps.
   ' Cancel that and keep covering the whole virtual screen.
   Private Sub frmSS_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles MyBase.DpiChanged
      If _isPreview Then Return
      e.Cancel = True
      Me.Bounds = SystemInformation.VirtualScreen
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: Paint
   Private Sub frmSS_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
      e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

      For Each r In _rockets
         r.Draw(e.Graphics)
      Next

      Using b As New SolidBrush(Color.White)
         For Each p In _particles
            p.Draw(e.Graphics, b)
         Next
      End Using
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: KeyDown
   Private Sub frmSS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
      If _isPreview Then Return
      Me.Close()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: MouseDown
   Private Sub frmSS_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
      If _isPreview Then Return
      Me.Close()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: MouseMove
   Private Sub frmSS_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
      If _isPreview Then Return

      ' remember the first position
      If Not mouseStart.HasValue Then
         mouseStart = e.Location
         Return
      End If

      ' exit if the mouse moved more than 4 pixels
      Dim start As Point = mouseStart.Value
      If Math.Abs(e.X - start.X) > 4 OrElse Math.Abs(e.Y - start.Y) > 4 Then
         Me.Close()
      End If
   End Sub

End Class
