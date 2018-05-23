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
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()

			If ObjectSpace.GetObjects(Of User)().Count = 0 Then
				Dim user As User = ObjectSpace.CreateObject(Of User)()
				user.UserName = "Sam"
				user.Save()

				Dim userRole As Role = ObjectSpace.CreateObject(Of Role)()
				userRole.AddPermission(New ObjectAccessPermission(GetType(Object), ObjectAccess.AllAccess, ObjectAccessModifier.Allow))
				userRole.AddPermission(New ObjectAccessPermission(GetType(MyPerson), ObjectAccess.AllAccess, ObjectAccessModifier.Allow))
				userRole.AddPermission(New ObjectAccessPermission(GetType(MyOrganization), ObjectAccess.Create Or ObjectAccess.Delete Or ObjectAccess.Write, ObjectAccessModifier.Deny))

				userRole.Users.Add(user)

				userRole.Save()

				Dim organization As MyOrganization = ObjectSpace.CreateObject(Of MyOrganization)()
				organization.FullName = "Organization 1"
				organization.Save()

				Dim person As MyPerson = ObjectSpace.CreateObject(Of MyPerson)()
				person.Name = "Person 1"
				person.Save()
			End If
		End Sub
	End Class
End Namespace
