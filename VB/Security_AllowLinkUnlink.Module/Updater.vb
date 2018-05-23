Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl

Namespace Security_AllowLinkUnlink.Module
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As ObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()

			If New XPCollection(Of User)(Session).Count = 0 Then
				Dim user As New User(Session)
				user.UserName = "Sam"
				user.Save()

				Dim userRole As New Role(Session)
				userRole.AddPermission(New ObjectAccessPermission(GetType(Object), ObjectAccess.AllAccess, ObjectAccessModifier.Allow))
				userRole.AddPermission(New ObjectAccessPermission(GetType(MyPerson), ObjectAccess.AllAccess, ObjectAccessModifier.Allow))
				userRole.AddPermission(New ObjectAccessPermission(GetType(MyOrganization), ObjectAccess.Create Or ObjectAccess.Delete Or ObjectAccess.Write, ObjectAccessModifier.Deny))

				userRole.Users.Add(user)

				userRole.Save()

				Dim organization As New MyOrganization(Session)
				organization.FullName = "Organization 1"
				organization.Save()

				Dim person As New MyPerson(Session)
				person.Name = "Person 1"
				person.Save()
			End If
		End Sub
	End Class
End Namespace
