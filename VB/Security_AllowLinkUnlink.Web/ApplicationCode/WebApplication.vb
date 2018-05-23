Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Security

Partial Public Class Security_AllowLinkUnlinkAspNetApplication
	Inherits WebApplication
	Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
    Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
	Private module3 As Security_AllowLinkUnlink.Module.Security_AllowLinkUnlinkModule
	Private module4 As Security_AllowLinkUnlink.Module.Web.Security_AllowLinkUnlinkAspNetModule
	Private module5 As DevExpress.ExpressApp.Validation.ValidationModule
    Private module6 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
	Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule

    Friend WithEvents SecurityComplex1 As DevExpress.ExpressApp.Security.SecurityComplex
    Friend WithEvents AuthenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
	Private sqlConnection1 As System.Data.SqlClient.SqlConnection

	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub Security_AllowLinkUnlinkAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
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

	Private Sub InitializeComponent()
        Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule
        Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
        Me.module3 = New Security_AllowLinkUnlink.[Module].Security_AllowLinkUnlinkModule
        Me.module4 = New Security_AllowLinkUnlink.[Module].Web.Security_AllowLinkUnlinkAspNetModule
        Me.module5 = New DevExpress.ExpressApp.Validation.ValidationModule
        Me.module6 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
        Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule
        Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SecurityComplex1 = New DevExpress.ExpressApp.Security.SecurityComplex
        Me.AuthenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'module1
        '
        Me.module1.AdditionalBusinessClasses.Add(GetType(DevExpress.Xpo.XPObjectType))
        '
        'module5
        '
        Me.module5.AllowValidationDetailsAccess = True
        '
        'sqlConnection1
        '
        Me.sqlConnection1.ConnectionString = "Data Source=(local);Initial Catalog=Security_AllowLinkUnlink;Integrated Security=" & _
            "SSPI;Pooling=false"
        Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SecurityComplex1
        '
        Me.SecurityComplex1.Authentication = Me.AuthenticationStandard1
        Me.SecurityComplex1.IsGrantedForNonExistentPermission = False
        Me.SecurityComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.Role)
        Me.SecurityComplex1.UserType = GetType(DevExpress.Persistent.BaseImpl.User)
        '
        'AuthenticationStandard1
        '
        Me.AuthenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
        '
        'Security_AllowLinkUnlinkAspNetApplication
        '
        Me.ApplicationName = "Security_AllowLinkUnlink"
        Me.Connection = Me.sqlConnection1
        Me.Modules.Add(Me.module1)
        Me.Modules.Add(Me.module2)
        Me.Modules.Add(Me.module6)
        Me.Modules.Add(Me.module3)
        Me.Modules.Add(Me.module4)
        Me.Modules.Add(Me.module5)
        Me.Modules.Add(Me.securityModule1)
        Me.Security = Me.SecurityComplex1
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private Sub Security_AllowLinkUnlinkAspNetApplication_LastLogonParametersRead(ByVal sender As System.Object, ByVal e As DevExpress.ExpressApp.LastLogonParametersReadEventArgs) Handles MyBase.LastLogonParametersRead
        Dim logonParams As AuthenticationStandardLogonParameters = CType(e.LogonObject, AuthenticationStandardLogonParameters)
        If String.IsNullOrEmpty(logonParams.UserName) Then
            logonParams.UserName = "Sam"
        End If
    End Sub
End Class

