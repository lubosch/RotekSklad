strComputer = "." 
Set objWMIService = GetObject("winmgmts:" _ 
    & "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2") 
 
Set colSoftware = objWMIService.ExecQuery _ 
    ("Select * from Win32_Product Where Name = 'Rotek sklad'") 
 
For Each objSoftware in colSoftware 
    objSoftware.Uninstall() 
Next 