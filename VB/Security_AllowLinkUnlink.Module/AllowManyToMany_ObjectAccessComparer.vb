Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.ExpressApp.Security

Namespace Security_AllowLinkUnlink.Module
	Public Class AllowManyToMany_ObjectAccessComparer
		Inherits ObjectAccessComparer
		Public Overrides Function IsSubsetOf(ByVal sourcePermission As ObjectAccessPermission, ByVal targetPermission As ObjectAccessPermission) As Boolean
			If sourcePermission.AccessItemList.Count = 1 Then
				Dim sourceAccessItem As ParticularAccessItem = sourcePermission.AccessItemList(0)
				If GetType(MyOrganization).IsAssignableFrom(sourceAccessItem.ObjectType) AndAlso sourceAccessItem.Access = ObjectAccess.Write AndAlso sourceAccessItem.Modifier = ObjectAccessModifier.Allow Then
					If sourcePermission.Contexts IsNot Nothing AndAlso sourcePermission.Contexts.CollectionPropertyContext IsNot Nothing AndAlso "Organizations" = sourcePermission.Contexts.CollectionPropertyContext.CollectionPropertyName AndAlso GetType(MyPerson).IsAssignableFrom(sourcePermission.Contexts.CollectionPropertyContext.MasterObjectType) Then
						Return True
					End If
				End If
			End If
			Return MyBase.IsSubsetOf(sourcePermission, targetPermission)
		End Function
	End Class
End Namespace
