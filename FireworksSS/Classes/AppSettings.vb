'--------------------------------------------------------------------------------------------------
' FireworksSS: AppSettings.vb: Class to Save/Load settings to the Registry
'    © 2026 Remus Rigo
'       v1.0.20261104
'--------------------------------------------------------------------------------------------------

Imports Microsoft.Win32

Public Class AppSettings

   Private Const regAppPath As String = "Software\Remus RIGO\FireworksSS.VB-NET"

   ' Allowed ranges (also used by the config form)
   Public Const LaunchRateMin As Integer = 1
   Public Const LaunchRateMax As Integer = 10
   Public Const BurstSizeMin As Integer = 20
   Public Const BurstSizeMax As Integer = 300
   Public Const ParticleLifeMin As Integer = 300
   Public Const ParticleLifeMax As Integer = 5000
   Public Property LaunchRate As Integer = 2       ' rockets launched per second (on average)
   Public Property BurstSize As Integer = 110      ' particles per explosion (on average, full screen)
   Public Property ParticleLife As Integer = 1100  ' how long sparks last, in milliseconds (on average)

   '-----------------------------------------------------------------------------------------------
   ' Save
   Public Sub Save()
      Try
         Using key As RegistryKey = Registry.CurrentUser.CreateSubKey(regAppPath)
            If key Is Nothing Then Return

            key.SetValue(NameOf(LaunchRate), LaunchRate, RegistryValueKind.DWord)
            key.SetValue(NameOf(BurstSize), BurstSize, RegistryValueKind.DWord)
            key.SetValue(NameOf(ParticleLife), ParticleLife, RegistryValueKind.DWord)

            key.DeleteValue("RocketsCount", False)   ' obsolete setting
         End Using
      Catch ex As Exception
         ' Ignore registry errors (e.g. access denied)
      End Try
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Load: missing, corrupt or out-of-range values fall back to / are clamped to valid values
   Public Sub Load()
      Try
         Using key As RegistryKey = Registry.CurrentUser.OpenSubKey(regAppPath, False)
            If key Is Nothing Then Return ' Nothing saved yet, keep defaults

            LaunchRate = ReadInt(key, NameOf(LaunchRate), LaunchRate, LaunchRateMin, LaunchRateMax)
            BurstSize = ReadInt(key, NameOf(BurstSize), BurstSize, BurstSizeMin, BurstSizeMax)
            ParticleLife = ReadInt(key, NameOf(ParticleLife), ParticleLife, ParticleLifeMin, ParticleLifeMax)
         End Using
      Catch ex As Exception
         ' Ignore registry errors, keep defaults
      End Try
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Delete
   Public Sub Delete()
      Try
         Registry.CurrentUser.DeleteSubKeyTree(regAppPath, False)
      Catch ex As Exception
      End Try
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ReadInt: reads one DWord value, keeping the default if it is missing or not a number
   Private Shared Function ReadInt(key As RegistryKey, name As String, def As Integer, min As Integer, max As Integer) As Integer
      Try
         Dim v As Integer = Convert.ToInt32(key.GetValue(name, def))
         Return Math.Min(max, Math.Max(min, v))
      Catch
         Return def
      End Try
   End Function

End Class
