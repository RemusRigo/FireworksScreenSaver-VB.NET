'--------------------------------------------------------------------------------------------------
' FireworksSS: Main.vb: Entry point for the application
'    © 2026 Remus Rigo
'       v1.0.20261103
'--------------------------------------------------------------------------------------------------

Module Main

   <STAThread>
   Sub Main(args As String())
#If Not NETFRAMEWORK Then
      Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)

      cfg.Load()   ' settings from HKCU\Software\Remus RIGO\FireworksSS.VB-NET

      Dim mode As String = "/s"
      If args.Length > 0 Then
         mode = args(0).Trim().ToLowerInvariant()

         If mode.StartsWith("/c") Then
            mode = "/c"
         ElseIf mode.StartsWith("/p") Then
            mode = "/p"
         End If
      End If

      Select Case mode
         Case "/c"
            ShowConfig()
         Case "/p"
            Dim h As IntPtr = ParsePreviewHandle(args)
            If h <> IntPtr.Zero Then RunPreview(h)
         Case Else
            RunScreenSaver()
      End Select
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' RunScreenSaver
   Private Sub RunScreenSaver()
      Application.Run(New frmSS())
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ShowConfig
   Private Sub ShowConfig()
      Using frm As New frmCfg()
         frm.ShowDialog()
      End Using
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ParsePreviewHandle: Windows passes the handle as "/p 1234" or "/p:1234"
   Private Function ParsePreviewHandle(args As String()) As IntPtr
      Dim s As String = Nothing
      Dim first As String = args(0).Trim()
      If first.Length > 2 Then
         s = first.Substring(2).TrimStart(":"c)
      ElseIf args.Length > 1 Then
         s = args(1)
      End If

      Dim v As Long
      If s IsNot Nothing AndAlso Long.TryParse(s.Trim(), v) Then Return New IntPtr(v)
      Return IntPtr.Zero
   End Function

   '-----------------------------------------------------------------------------------------------
   ' RunPreview: draw inside the small monitor in Screen Saver Settings
   Private Sub RunPreview(parentHandle As IntPtr)
      Application.Run(New frmSS() With {.PreviewHandle = parentHandle})
   End Sub

End Module
