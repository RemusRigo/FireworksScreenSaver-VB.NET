'--------------------------------------------------------------------------------------------------
' FireworksSS: Rocket.vb
'    © 2026 Remus Rigo
'       v1.0.20261104
'--------------------------------------------------------------------------------------------------

Public Class Rocket

   Public Const Gravity As Single = 0.25F

   Public X As Single
   Public Y As Single
   Public VX As Single
   Public VY As Single
   Public RocketColor As Color
   Public Exploded As Boolean

   Private ReadOnly _trail As New List(Of PointF)
   Private Const MaxTrail As Integer = 14

   '-----------------------------------------------------------------------------------------------
   ' Constructor
   Public Sub New(screenW As Integer, screenH As Integer, rnd As Random)
      X = CSng(rnd.Next(CInt(screenW * 0.1), CInt(screenW * 0.9)))
      Y = screenH
      VX = CSng((rnd.NextDouble() - 0.5) * 2)

      ' Pick an apex height, then compute the launch speed that reaches it
      Dim apexY As Single = CSng(screenH * (0.12 + rnd.NextDouble() * 0.38))
      Dim rise As Single = screenH - apexY
      VY = -CSng(Math.Sqrt(2 * Gravity * rise)) ' speed needed to reach apexY under constant acceleration of Gravity

      ' Bright random colour
      RocketColor = Color.FromArgb(rnd.Next(120, 256), rnd.Next(120, 256), rnd.Next(120, 256))
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Update the rocket's position and trail
   Public Sub Update()
      _trail.Add(New PointF(X, Y))
      If _trail.Count > MaxTrail Then _trail.RemoveAt(0)

      X += VX
      Y += VY
      VY += Gravity

      If VY >= 0 Then Exploded = True   ' reached the apex
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Draw the rocket and its trail
   Public Sub Draw(g As Graphics)
      ' Fading trail
      For i As Integer = 1 To _trail.Count - 1
         Dim alpha As Integer = CInt(255.0 * i / _trail.Count)
         Using p As New Pen(Color.FromArgb(alpha, RocketColor), 2)
            g.DrawLine(p, _trail(i - 1), _trail(i))
         End Using
      Next

      ' Head
      Using b As New SolidBrush(Color.White)
         g.FillEllipse(b, X - 2, Y - 2, 4, 4)
      End Using
   End Sub

End Class