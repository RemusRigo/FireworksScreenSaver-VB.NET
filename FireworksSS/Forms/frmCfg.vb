'--------------------------------------------------------------------------------------------------
' FireworksSS: frmCfg.vb
'    © 2026 Remus Rigo
'       v1.0.20261104
'--------------------------------------------------------------------------------------------------

Imports FireworksSS.API

Public Class frmCfg

   Private Const SYSMENU_ABOUT_ID As UInteger = 1000

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
      If m.Msg = WM_SYSCOMMAND Then
         If CUInt(m.WParam) = SYSMENU_ABOUT_ID Then
            frmAbout.ShowDialog()
         End If
      End If
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
      cfg.Load()

      ' Ranges come from AppSettings, so the spinners can't hold an invalid value
      SetupSpinner(nudLaunchRate, AppSettings.LaunchRateMin, AppSettings.LaunchRateMax, 1, cfg.LaunchRate)
      SetupSpinner(nudBurstSize, AppSettings.BurstSizeMin, AppSettings.BurstSizeMax, 10, cfg.BurstSize)
      SetupSpinner(nudParticleLife, AppSettings.ParticleLifeMin, AppSettings.ParticleLifeMax, 100, cfg.ParticleLife)
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmCfg: FormClosing
   Private Sub frmCfg_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
      cfg.LaunchRate = CInt(nudLaunchRate.Value)
      cfg.BurstSize = CInt(nudBurstSize.Value)
      cfg.ParticleLife = CInt(nudParticleLife.Value)
      cfg.Save()
   End Sub


End Class
