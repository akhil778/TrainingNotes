'******************************************************************
'Name: Pradip Vaghasiya
'Script to get the Actual Working Hours for Current Working Day.
'To Run the script: Change Employee ID below
'Send Bugs/Suggestions to pradipv@cybage.com if any.
'Version 1.4
'******************************************************************

empolyeeID = InputBox("Enter your Employee ID") 
numberOfResultColumn = 5
coumnNoForMachine = 3
coumnNoForDirection = 4
coumnNoForTime = 5
LakshmanRekhaArray = Array("Tripod","Flap Barrier","Basement Door","Basement Entrance")

set WshShell = WScript.CreateObject("WScript.Shell")  

set IE = CreateObject("InternetExplorer.Application") 
IE.Visible = True

'IE.Navigate "http://cybagemis.cybage.com/Framework/Iframe.aspx" 
'waitToLoad IE

'IE.Document.GetElementById("MTt95").click
'waitToLoad IE

IE.Navigate "http://cybagemis.cybage.com/Report Builder/RPTN/ReportPage.aspx" 
WScript.sleep 200
waitToLoad IE

'IE.Document.GetElementById("TempleteTreeViewt4").click
GoToLink("Today's and Yesterday's Swipe Log")
WScript.sleep 200
waitToLoad IE

MsgBox("Test01")
IE.Document.GetElementById("DayDropDownList8665").selectedIndex=1

AWHSecondsToday = calculateAWH("Today")

IE.Navigate "http://cybagemis.cybage.com/Report Builder/RPTN/ReportPage.aspx" 
WScript.sleep 200
waitToLoad IE

GoToLink("Today's and Yesterday's Swipe Log")
WScript.sleep 200
waitToLoad IE

On Error Resume Next

IE.Document.GetElementById("DayDropDownList8665").selectedIndex=2
AWHSecondsYesterday = calculateAWH("Yesterday")



If (28800 - AWHSecondsToday) > 0 Then 
	TodaysSwipesText = "TODAY" & Chr(13) & "  Working Hours: " & showAWH(AWHSecondsToday) & Chr(13) & "  Probable End Time(8 Hours): " & DateAdd("s",28800 - AWHSecondsToday,Now())
Else
	TodaysSwipesText = "TODAY" & Chr(13) & "  Working Hours: " & showAWH(AWHSecondsToday)
End If

YesterdaysSwipesText = "YESTERDAY" & Chr(13) & "  Working Hours: " & showAWH(AWHSecondsYesterday)




Dim TotalSecondsWeek1,TotalSecondsWeek2,IsItWeek2
IsItWeek2 =False
calculateWEEKLY()

If IsItWeek2 Then
	TotalSecondsWeek2 = TotalSecondsWeek2 + AWHSecondsYesterday + AWHSecondsToday
Else
	TotalSecondsWeek1 = TotalSecondsWeek1 + AWHSecondsYesterday + AWHSecondsToday
End If

If Weekday(Now()) = 6 And IsItWeek2 And (288000 - TotalSecondsWeek1 - TotalSecondsWeek2 + AWHSecondsToday) > 21600 Then
	MsgBox(TodaysSwipesText & Chr(13) & Chr(13) & YesterdaysSwipesText & Chr(13) & Chr(13) &  "WEEKLY " & Chr(13) &"  Week 1: " & showAWH(TotalSecondsWeek1) & " Balance: " & showAWH(TotalSecondsWeek1 - 144000) & Chr(13) &"  Week 2: " & showAWH(TotalSecondsWeek2)& " Balance: " & showAWH(TotalSecondsWeek2 - 144000 + (TotalSecondsWeek1 - 144000)) &" (Week1 Balance adjusted)"  & Chr(13) & Chr(13) & "FORTNIGHTLY " & Chr(13) &"  Hours Completed : " & showAWH(TotalSecondsWeek1 + TotalSecondsWeek2) & Chr(13) &"  Hours To Complete : " & showAWH(288000 - TotalSecondsWeek1 -TotalSecondsWeek2)& Chr(13) & Chr(13) & "You can leave today by " & DateAdd("s",288000 - TotalSecondsWeek1 -TotalSecondsWeek2,Now()) & " !!")

Else
	MsgBox(TodaysSwipesText & Chr(13) & Chr(13) & YesterdaysSwipesText & Chr(13) & Chr(13) &  "WEEKLY " & Chr(13) &"  Week 1: " & showAWH(TotalSecondsWeek1) & " Balance: " & showAWH(TotalSecondsWeek1 - 144000) & Chr(13) &"  Week 2: " & showAWH(TotalSecondsWeek2)& " Balance: " & showAWH(TotalSecondsWeek2 - 144000 + (TotalSecondsWeek1 - 144000)) &" (Week1 Balance adjusted)" & Chr(13) & Chr(13) & "FORTNIGHTLY " & Chr(13) &"  Hours Completed : " & showAWH(TotalSecondsWeek1 + TotalSecondsWeek2) & Chr(13) &"  Hours To Complete : " & showAWH(288000 - TotalSecondsWeek1 -TotalSecondsWeek2))
End If


Function calculateWEEKLY
	numberOfResultColumn = 9
	coumnNoForDate = 3
	coumnNoForAWH = 8
    columnNoForStatus = 9

	IE.Navigate "http://cybagemis.cybage.com/Core/Common/Introduction.aspx" 
	WScript.sleep 200
	waitToLoad IE

	Week1String = IE.Document.GetElementById("NoticeList").Children(0).innerText
	startdate = Mid(Week1String,InStr(Week1String,"Week 1:") + 8,9)

	IE.Navigate "http://cybagemis.cybage.com/Report Builder/RPTN/ReportPage.aspx" 
	WScript.sleep 200
	waitToLoad IE

	'IE.Document.GetElementById("TempleteTreeViewt1").click
	GoToLink("Attendance Log Report")
	WScript.sleep 200
	waitToLoad IE

	'MsgBox(startdate)
	'IE.Document.GetElementById("DMNDateDateRangeControl4392_FromDateCalender").MinDate = startdate
	IE.Document.GetElementById("DMNDateDateRangeControl4392_FromDateCalender").value = startdate 
	IE.Document.GetElementById("DMNDateDateRangeControl4392_FromDateCalender_DTB").value = startdate 
	WScript.sleep 200
	waitToLoad IE

	IE.Document.GetElementById("ViewReportImageButton").click
	WScript.sleep 200
	waitToLoad IE

	ReDim inOutTimings(numberOfResultColumn,1)
	resultColumn = 0
	result = -1

	startShowing = False
	For page = 1 to IE.Document.GetElementById("ReportViewer1").ClientController.TotalPages
		resultColumn = 0
		result = result + 1
		redim preserve inOutTimings(numberOfResultColumn,result + 1)
	 
		For each tableTag in IE.Document.GetElementsByTagName("TABLE")
		For each row in tableTag.Rows
			For each cell in row.cells 
				if cell.innerText = empolyeeID then
					startShowing = True
				end if
				
				if startShowing Then
					if resultColumn = numberOfResultColumn then
						if cell.innerText =  empolyeeID then
							resultColumn = 0
							result = result + 1
							redim preserve inOutTimings(numberOfResultColumn,result + 1)
						else
							startShowing= False
							Exit For
						end if 
					end if
					
					inOutTimings(resultColumn,result) = cell.innerText
					resultColumn = resultColumn + 1	
				end if
			Next
		Next
		Next	
		
		IE.Document.GetElementById("ReportViewer1_ctl01_ctl01_ctl05_ctl00").click
		waitToLoad IE
	Next

	IE.quit

	TotalSecondsWeek1 = 0
	TotalSecondsWeek2 = 0
	TotalDays = 0
    
    For row = 0 To result
        If InStr(inOutTimings(columnNoForStatus-1,row),"Weekly Off") > 0 Then
            'Do Nothing
        else
        If InStr(inOutTimings(columnNoForStatus-1,row),"Leave") > 0 Or InStr(inOutTimings(columnNoForStatus-1,row),"Holiday") > 0 Or InStr(inOutTimings(columnNoForStatus-1,row),"Absent") > 0Then
            	currentAWH = "8:00"
        Else
		currentAWH = inOutTimings(coumnNoForAWH - 1,row)
        End If	
        
	If Len(currentAWH) > 1 Then
			currentDate = CDate(inOutTimings(coumnNoForDate - 1,row))
			'If (Weekday(currentDate) <> 1 AND Weekday(currentDate) <> 7) Then	
				TotalDays = TotalDays + 1
				If TotalDays <= 5 Then
					TotalSecondsWeek1 = TotalSecondsWeek1 + DateDiff("s", CDate(DateValue(Now()) & " 00:00:00") , CDate(DateValue(Now()) & " " & currentAWH & ":00"))			
				Else
					IsItWeek2 = True
					TotalSecondsWeek2 = TotalSecondsWeek2 + DateDiff("s", CDate(DateValue(Now()) & " 00:00:00") , CDate(DateValue(Now()) & " " & currentAWH & ":00"))
				End If
				
				If TotalDays = 5 Then
					IsItWeek2 = True
				End If
			'End If
		End If
        End If
	Next
End Function

Function calculateAWH(TodayOrYesterday)

	IE.Document.GetElementById("ViewReportImageButton").click
	waitToLoad IE
	Dim inOutTimings()

	ReDim inOutTimings(numberOfResultColumn,1)
	resultColumn = 0
	result = -1

	startShowing = False
	For page = 1 to IE.Document.GetElementById("ReportViewer1").ClientController.TotalPages
		resultColumn = 0
		result = result + 1
		redim preserve inOutTimings(numberOfResultColumn,result + 1)
	 
		For each tableTag in IE.Document.GetElementsByTagName("TABLE")
			For each row in tableTag.Rows
				For each cell in row.cells 
					if cell.innerText = empolyeeID then
						startShowing = True
					end if
					
					if startShowing Then
						if resultColumn = numberOfResultColumn then
							if cell.innerText =  empolyeeID then
								resultColumn = 0
								result = result + 1
								redim preserve inOutTimings(numberOfResultColumn,result + 1)
							else
								startShowing= False
								Exit For
							end if 
						end if
						
						inOutTimings(resultColumn,result) = cell.innerText
						'MsgBox(inOutTimings(resultColumn,result))
						resultColumn = resultColumn + 1	
					end if
				Next
			Next
		Next	
		
		IE.Document.GetElementById("ReportViewer1_ctl01_ctl01_ctl05_ctl00").click
		waitToLoad IE
	Next

	dim startDateTime,AWHInSeconds 
	PersonInsideLakshmanRekha = False
	AWHInSeconds = 0
	PersonEntered = False
	PersonExited = False
	startDateTime = "00:00:00"
	startDateTime =  CDate(DateValue(Now()) & " 00:00:00")
	ExitDateTime =  startDateTime


	For row = 0 To result
		For Each LakshmanRekhaArrayKeyWord in LakshmanRekhaArray
		if InStr(inOutTimings(coumnNoForMachine-1,row),LakshmanRekhaArrayKeyWord) > 0 then
			if inOutTimings(coumnNoForDirection-1,row) = "Entry" then
				If Not PersonEntered and PersonExited Then
					AWHInSeconds = AWHInSeconds + DateDiff("s", startDateTime ,ExitDateTime)
				End If 

                If InStr(TodayOrYesterday, "Today") > 0 Then
				    startDateTime = CDate(DateValue(Now()) & " " & inOutTimings(coumnNoForTime-1,row))
                Else
                    startDateTime = CDate(DateValue(Now()) - 1 & " " & inOutTimings(coumnNoForTime-1,row))
                End If
                PersonInsideLakshmanRekha = True
				PersonExited = False
				PersonEntered = True
			else
                If InStr(TodayOrYesterday, "Today") > 0 Then
				    ExitDateTime =  CDate(DateValue(Now()) & " " & inOutTimings(coumnNoForTime-1,row))
                Else
                    ExitDateTime =  CDate(DateValue(Now()) - 1 & " " & inOutTimings(coumnNoForTime-1,row))
                End If
				
                PersonInsideLakshmanRekha = False
				PersonExited = True
				PersonEntered = False
			end if
		end if
		Next
	Next

	if PersonInsideLakshmanRekha then
		AWHInSeconds = AWHInSeconds + DateDiff("s", startDateTime , Now())
	Else
		AWHInSeconds = AWHInSeconds + DateDiff("s", startDateTime ,ExitDateTime)
	End If
	
	calculateAWH = AWHInSeconds

End Function


Function showAWH(AWHInSeconds)

	If AWHInSeconds < 0 Then
		AWHInSeconds = -AWHInSeconds
		Hours = Int(AWHInSeconds/3600)
		reminderHours = AWHInSeconds - (Hours * 3600)
		Minutes = Int(reminderHours/60)
		Seconds = reminderHours - (Minutes*60)

		showAWH = "-" & Hours & ":" & Minutes & ":" & Seconds
		
	Else
		Hours = Int(AWHInSeconds/3600)
		reminderHours = AWHInSeconds - (Hours * 3600)
		Minutes = Int(reminderHours/60)
		Seconds = reminderHours - (Minutes*60)

		showAWH = Hours & ":" & Minutes & ":" & Seconds
	End If

End Function

Sub GoToLink(innerText)
	For each aTag in IE.Document.getElementsByTagName("a")
		If aTag.innerText = innerText Then
			aTag.click
		End If
	Next
End Sub

Function waitToLoad(IE)
    maxTry = 50
    try = 1
    WScript.sleep 100
    Do While (try < maxTry AND IE.busy)
    	WScript.sleep 100
     	try = try + 1
    Loop

End Function

'TempleteTreeViewt4