'--------------------------------------------------------------------------------------------------
' FireworksSS: Main.vb: Entry point for the application
'    © 2026 Remus Rigo
'       v1.0.20261110
'--------------------------------------------------------------------------------------------------

Imports FireworksSS.API

Module Main

   <STAThread>
   Sub Main(args As String())
#If Not NETFRAMEWORK Then
      Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)

      cfg.Load()

      Dim mode As String = If(args.Length > 0, args(0).Trim().ToLowerInvariant(), "/c")

      Select Case If(mode.Length >= 2, mode.Substring(0, 2), mode)
         Case "/a" ' /a (legacy password change) or unknown switch: do nothing
         Case "/c"
            ShowConfig(args)
         Case "/p"
            Dim h As IntPtr = ParseHandleArg(args)
            If h <> IntPtr.Zero Then RunPreview(h)
         Case "/s"
            RunScreenSaver()
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
   ' ShowConfig: Windows passes the Screen Saver Settings window as "/c:1234";
   ' use it as the owner so the dialog stays on top of it. ShowDialog only disables windows of
   ' this process, so the owner (another process) is disabled by hand while the dialog is open.
   Private Sub ShowConfig(args As String())
      Dim owner As IntPtr = If(args.Length > 0, ParseHandleArg(args), IntPtr.Zero)

      Using frm As New frmCfg()
         If owner <> IntPtr.Zero AndAlso IsWindow(owner) Then
            Dim nw As New NativeWindow()
            nw.AssignHandle(owner)

            ' Re-enable the owner before the dialog is destroyed, otherwise Windows activates
            ' some other application instead of returning focus to Screen Saver Settings
            AddHandler frm.FormClosing, Sub() EnableWindow(owner, True)

            EnableWindow(owner, False)
            Try
               frm.StartPosition = FormStartPosition.CenterParent
               frm.ShowDialog(nw)
            Finally
               EnableWindow(owner, True)   ' safety net, e.g. if ShowDialog throws
               nw.ReleaseHandle()
            End Try
         Else
            frm.ShowDialog()
         End If
      End Using
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ParseHandleArg: Windows passes a window handle as "/p 1234", "/p:1234", "/c:1234", ...
   Private Function ParseHandleArg(args As String()) As IntPtr
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
