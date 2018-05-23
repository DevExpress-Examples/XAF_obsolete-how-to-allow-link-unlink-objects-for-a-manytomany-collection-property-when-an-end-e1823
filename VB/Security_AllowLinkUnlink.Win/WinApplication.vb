Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Security

Partial Public Class Security_AllowLinkUnlinkWindowsFormsApplication
	Inherits WinApplication
	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub Security_AllowLinkUnlinkWindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
#If EASYTEST Then
        e.Updater.Update()
        e.Handled = True
#Else
        If System.Diagnostics.Debugger.IsAttached Then
            e.Updater.Update()
            e.Handled = True
        Else
            Throw New InvalidOperationException( _
             "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & Constants.vbCrLf & _
             "The automatic update is disabled, because the application was started without debugging." & Constants.vbCrLf & _
             "You should start the application under Visual Studio, or modify the " & _
             "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & _
             "or manually create a database using the 'DBUpdater' tool.")
        End If
#End If
    End Sub

    Private Sub Security_AllowLinkUnlinkWindowsFormsApplication_LastLogonParametersRead(ByVal sender As System.Object, ByVal e As DevExpress.ExpressApp.LastLogonParametersReadEventArgs) Handles MyBase.LastLogonParametersRead
        Dim logonParams As AuthenticationStandardLogonParameters = CType(e.LogonObject, AuthenticationStandardLogonParameters)
        If String.IsNullOrEmpty(logonParams.UserName) Then
            logonParams.UserName = "Sam"
        End If
    End Sub
End Class
