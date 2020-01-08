#DropDB MainDemo
#Application MainDemoWin
#Application MainDemoWeb

*FillForm
 User Name = Sam
*Action Log In

*Action Navigation(Roles)

*Action New

*Action Save and Close

*CheckValidationResult
 Message = Problems were detected during data validation. Please read the information below to understand what the issues are and how you can correct them.
 Info = "Name" must not be empty.
*OptionalAction Close

*FillForm
 Name = TechWriter
*Action Save and Close