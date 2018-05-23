Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base

Namespace Security_AllowLinkUnlink.Module
	<DefaultClassOptions> _
	Public Class MyPerson
		Inherits XPObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Public Property Name() As String
			Get
				Return GetPropertyValue(Of String)("Name")
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("Name", value)
			End Set
		End Property
		<Association("Organizations-Persons")> _
		Public ReadOnly Property Organizations() As XPCollection(Of MyOrganization)
			Get
				Return GetCollection(Of MyOrganization)("Organizations")
			End Get
		End Property
	End Class

	<DefaultClassOptions> _
	Public Class MyOrganization
		Inherits XPObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Public Property FullName() As String
			Get
				Return GetPropertyValue(Of String)("FullName")
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("FullName", value)
			End Set
		End Property
		<Association("Organizations-Persons")> _
		Public ReadOnly Property Persons() As XPCollection(Of MyPerson)
			Get
				Return GetCollection(Of MyPerson)("Persons")
			End Get
		End Property
	End Class
End Namespace
