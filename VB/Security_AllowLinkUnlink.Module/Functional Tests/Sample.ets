;#DropDB Security_AllowLinkUnlinkEasyTest

#Application Security_AllowLinkUnlinkWin
#Application Security_AllowLinkUnlinkWeb

*Action Navigation(User)

*ProcessRecord
 Full Name = ''

#IfDef Security_AllowLinkUnlinkWeb
*Action Edit
#EndIf
 
*FillForm
 Full Name = Test User
 
*Action Cancel

#IfDef Security_AllowLinkUnlinkWin
*HandleDialog
 Respond = Yes
#EndIf

*CheckFieldValues
 Full Name = ''
