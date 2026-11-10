'--------------------------------------------------------------------------------------------------
' FireworksSS: frmCfg.vb
'    © 2026 Remus Rigo
'       v1.0.20261110
'--------------------------------------------------------------------------------------------------

Imports FireworksSS.API

Public Class frmCfg

   Private Const SYSMENU_ABOUT_ID As Integer = &H3F0   ' low 4 bits must be 0

   '-----------------------------------------------------------------------------------------------
   ' create menu
   Protected Overrides Sub OnHandleCreated(e As EventArgs)
      MyBase.OnHandleCreated(e)
      Dim hSysMenu As IntPtr = GetSystemMenu(Me.Handle, False)
      ' Add a separator and then your custom item
      AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty)
      AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "About...")
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' append menu to app system menu
   Protected Overrides Sub WndProc(ByRef m As Message)
      MyBase.WndProc(m)
      ' Windows uses the low 4 bits of wParam internally, so mask them off before comparing
      If m.Msg = WM_SYSCOMMAND AndAlso (m.WParam.ToInt64() And &HFFF0L) = SYSMENU_ABOUT_ID Then
         Using f As New frmAbout()
            f.ShowDialog(Me)
         End Using
      End If
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Save settings
   Private Sub SaveSettings()
      cfg.LaunchRate = CInt(nudLaunchRate.Value)
      cfg.BurstSize = CInt(nudBurstSize.Value)
      cfg.ParticleLife = CInt(nudParticleLife.Value)
      cfg.Save()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Load settings
   Private Sub LoadSettings()
      cfg.Load()

      ' Ranges come from AppSettings, so the spinners can't hold an invalid value
      SetupSpinner(nudLaunchRate, AppSettings.LaunchRateMin, AppSettings.LaunchRateMax, 1, cfg.LaunchRate)
      SetupSpinner(nudBurstSize, AppSettings.BurstSizeMin, AppSettings.BurstSizeMax, 10, cfg.BurstSize)
      SetupSpinner(nudParticleLife, AppSettings.ParticleLifeMin, AppSettings.ParticleLifeMax, 100, cfg.ParticleLife)
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Setup a NumericUpDown spinner with min, max, step and value
   Private Shared Sub SetupSpinner(nud As NumericUpDown, min As Integer, max As Integer, stepBy As Integer, value As Integer)
      nud.Minimum = min
      nud.Maximum = max
      nud.Increment = stepBy
      nud.Value = Math.Min(max, Math.Max(min, value))
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmCfg: Load
   Private Sub frmCfg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Me.Text = AppData.appName & " Settings"
      LoadSettings()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmCfg: FormClosing
   ' only save if user clicked OK, X or Esc closes the form without saving
   Private Sub frmCfg_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
      If Me.DialogResult = DialogResult.OK Then SaveSettings()
   End Sub


End Class
