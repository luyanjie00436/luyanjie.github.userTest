;#DropDB UserTestEasyTest

#Application UserTest.Win
#Application UserTest.Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef UserTest.Win

*Action About...
 
*Action OK

#EndIf

#IfDef UserTest.Web
*Action Navigation(My Details)
#EndIf
