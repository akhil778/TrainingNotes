'******************************************************************
'Name: Pradip Vaghasiya
'Script to get the Actual Working Hours for Day and Week wise
'To Run the script: Keep all files in Same Directory.
'Send Bugs/Suggestions to pradipv@cybage.com if any.
'Change Employee ID Whereever Required
'******************************************************************

Set oShell = CreateObject("Shell.Application")
set fso = CreateObject("Scripting.FileSystemObject")
oShell.ShellExecute "wscript.exe", fso.GetAbsolutePathName(".") &"\TodaysSwipes.vbs -1", "", "runas", 1

