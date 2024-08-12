Imports System.Net.Mail
Imports System.Text

Module modHRIS
    Public Groupings As String = Nothing

    Public Sub empgroupings()
        gDT = ExecuteQuery("SELECT * FROM [HRIS2].dbo.tblSection where Section_Head  ='" & gEmployeeID & "'")
        If gDT.Rows.Count > 0 Then
            Groupings = "Section"
        End If
        gDT = ExecuteQuery("SELECT * FROM [HRIS2].dbo.tblDepartment where Department_Head  ='" & gEmployeeID & "'")
        If gDT.Rows.Count > 0 Then
            Groupings = "Department"
        End If
        gDT = ExecuteQuery("SELECT * FROM [HRIS2].dbo.tblGroup where Group_Head  ='" & gEmployeeID & "'")
        If gDT.Rows.Count > 0 Then
            Groupings = "Group"
        End If
    End Sub

    Function Get_ID(EMP_ID As String, ID_For As String)
        'dt = ExecuteQuery("SELECT * FROM tblHRDEmployees where Employee_ID ='" & EMP_ID & "'")
        'If dt.Rows.Count > 0 Then
        If ID_For = "Section" Then
            gDT = ExecuteQuery("select Current_SectionID as 'Section_ID' from tblSection where Section_Head ='" & EMP_ID & "'")
            If gDT.Rows.Count > 0 Then
                Return gDT(0)("Section_ID")
            End If
        ElseIf ID_For = "Department" Then
            gDT = ExecuteQuery("select Current_DepartmentID as 'Department' from tblDepartment where Department_Head = '" & EMP_ID & "'")
            If gDT.Rows.Count > 0 Then
                Return gDT(0)("Department")
            End If
        ElseIf ID_For = "Group" Then
            gDT = ExecuteQuery("select Group_ID from tblGroup where Group_Head = '" & EMP_ID & "'")
            If gDT.Rows.Count > 0 Then
                Return gDT(0)("Group_ID")
            End If
        End If
        'End If
        Return Nothing
    End Function


    Function Send_Email(Title As String, LeaveID As String, EmpID As String, empName As String, Status_ As String,
                       Sender_ As String, Ndays As Double, Optional Reason_ As String = Nothing,
                       Optional DateFrom As String = Nothing, Optional DateTo As String = Nothing,
                       Optional CoverFrom As String = "Wholeday", Optional CoverTo As String = "Wholeday",
                       Optional TimeFrom As String = Nothing, Optional TimeTo As String = Nothing)
        Try

            Dim strb As New StringBuilder

            Dim _Email_Body As String = ""

            gDT = ExecuteQuery("Select Fullname from tblHRDEmployees where Employee_ID = '" & EmpID & "'")
            If gDT.Rows.Count > 0 Then
                empName = gDT(0)(0).ToString
            End If

            'strb.Append("<STYLE TYPE=text/css> TD{font-family:calibre; font-size: 10pt;}</STYLE>" + _
            '       "<Center><h2></h2></center><table bgcolor=lightblue align =center cellpadding =10 style=width:50% border=1>" + _
            '       "<Tr><TD><p align =Center><font size =5 face =calibre><b>" & Me.Text & "<B></font></P></td></tr></table>" + _
            '       "<br><br><div style='text-align:center; margin-right:+525px;font-size:20'><b>Request Details:</b><br><br></div>" + _
            '       "</Table><br><br><br>" + _
            '       "<center><i>This is System-Generated Email</i></center>")


            strb.Append("<STYLE TYPE=text/css> TD{font-family:calibre; font-size: 10pt;}</STYLE> " +
                        "<Center><h2></h2></center> <table bgcolor=Gainsboro align =center cellpadding =10 style=width:50% border=1> " +
                        "<Tr><TD><p align =Center><font size =5 face =calibre><b>" & Title & "<B></font></P></td></tr></table> " +
                        "<br><br><center><b><br></b></center><br><br></div>" +
                        "<table align =center cellpadding =8 style=width:50% border= 5> ")

            '<br>Reason : " & Reason_ & " <br> 

            If Title.Substring(0, 11) = "Disapproved" Or Title.ToString.ToUpper.Contains("CANCELING") Then
                If Title.ToString.ToUpper.Contains("LEAVE") Or Title.ToString.ToUpper.Contains("VL") Or Title.ToString.ToUpper.Contains("SL") Or Title.ToString.ToUpper.Contains("OBT") Or Title.ToString.ToUpper.Contains("OFFSET") Then
                    strb.Append("<br><center> Employee : " & EmpID & " - " & empName & " </Center><br>" &
                                "<br><center> Date From: " & DateFrom & " - " & CoverFrom & " </Center><br>" &
                                "<br><center> Date To: " & DateTo & " - " & CoverTo & " </Center><br>" &
                                "<br><center> Total day(s): " & Ndays & " </Center><br><br>" &
                                "<br><center> Reason: " & Reason_ & " </Center><br><br>" &
                                "<br><Center>This is a computer generated e-mail. <br> <B> DO NOT REPLY</B></Center>")

                ElseIf Title.ToString.ToUpper.Contains("OVERTIME") Then
                    strb.Append("<br><center> Employee : " & EmpID & " - " & empName & " </Center><br>" &
                                "<br><center> Date : " & DateFrom & " </Center><br>" &
                                "<br><center> Time From: " & TimeFrom & " </Center><br>" &
                                "<br><center> Time To: " & TimeTo & " </Center><br>" &
                                "<br><center> Total hours: " & Ndays & " </Center><br><br>" &
                                "<br><center> Reason: " & Reason_ & " </Center><br><br>" &
                                "<br><Center>This is a computer generated e-mail. <br> <B> DO NOT REPLY</B></Center>")
                End If
            Else
                If Title.ToString.ToUpper.Contains("LEAVE") Or Title.ToString.ToUpper.Contains("VL") Or Title.ToString.ToUpper.Contains("SL") Or Title.ToString.ToUpper.Contains("OBT") Or Title.ToString.ToUpper.Contains("OFFSET") Then
                    strb.Append("<br><center> Employee : " & EmpID & " - " & empName & " </Center><br>" &
                                "<br><center> Date From: " & DateFrom & " - " & CoverFrom & " </Center><br>" &
                                "<br><center> Date To: " & DateTo & " - " & CoverTo & " </Center><br>" &
                                "<br><center> Total day(s): " & Ndays & " </Center><br><br>" &
                                "<br><Center>This is a computer generated e-mail. <br> <B> DO NOT REPLY</B></Center>")

                ElseIf Title.ToString.ToUpper.Contains("OVERTIME") Then
                    strb.Append("<br><center> Employee : " & EmpID & " - " & empName & " </Center><br>" &
                                "<br><center> Date : " & DateFrom & " </Center><br>" &
                                "<br><center> Time From: " & TimeFrom & " </Center><br>" &
                                "<br><center> Time To: " & TimeTo & " </Center><br>" &
                                "<br><center> Total hours: " & Ndays & " </Center><br><br>" &
                                "<br><Center>This is a computer generated e-mail. <br> <B> DO NOT REPLY</B></Center>")
                End If
            End If


            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False

            Smtp_Server.Credentials = New Net.NetworkCredential("abiso.ni.it01@gmail.com", "slwsavewnlkadhmi")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"
            Smtp_Server.Timeout = 300000



            e_mail = New MailMessage()
            e_mail.From = New MailAddress("abiso.ni.it01@gmail.com")
            gDT = ExecuteQuery("EXEC get_Approver_Email_new '" & EmpID & "'")

            If Sender_ = "Employee" Then
                If gDT.Rows.Count > 0 Then
                    If gDT(0)("GH Email") <> "" Then e_mail.To.Add(gDT(0)("GH Email").ToString)
                    If gDT(0)("DH Email") <> "" Then e_mail.To.Add(gDT(0)("DH Email").ToString)
                    If gDT(0)("SH Email") <> "" Then e_mail.To.Add(gDT(0)("SH Email").ToString)
                    If gDT(0)("EMP Email") <> "" Then e_mail.CC.Add(gDT(0)("EMP Email").ToString)
                End If
            ElseIf Sender_ = "Section" Then
                If gDT.Rows.Count > 0 Then
                    If gDT(0)("GH Email") <> "" Then e_mail.To.Add(gDT(0)("GH Email").ToString)
                    If gDT(0)("DH Email") <> "" Then e_mail.To.Add(gDT(0)("DH Email").ToString)
                    If gDT(0)("EMP Email") <> "" Then e_mail.To.Add(gDT(0)("EMP Email").ToString)
                    If gDT(0)("SH Email") <> "" Then e_mail.CC.Add(gDT(0)("SH Email").ToString)
                End If
            ElseIf Sender_ = "Department" Then
                If gDT.Rows.Count > 0 Then
                    If gDT(0)("GH Email") <> "" Then e_mail.To.Add(gDT(0)("GH Email").ToString)
                    If gDT(0)("SH Email") <> "" Then e_mail.To.Add(gDT(0)("SH Email").ToString)
                    If gDT(0)("EMP Email") <> "" Then e_mail.To.Add(gDT(0)("EMP Email").ToString)
                    If gDT(0)("DH Email") <> "" Then e_mail.CC.Add(gDT(0)("DH Email").ToString)
                End If
            ElseIf Sender_ = "Group" Then
                If gDT.Rows.Count > 0 Then
                    If gDT(0)("DH Email") <> "" Then e_mail.To.Add(gDT(0)("DH Email").ToString)
                    If gDT(0)("SH Email") <> "" Then e_mail.To.Add(gDT(0)("SH Email").ToString)
                    If gDT(0)("EMP Email") <> "" Then e_mail.To.Add(gDT(0)("EMP Email").ToString)
                    If gDT(0)("GH Email") <> "" Then e_mail.CC.Add(gDT(0)("GH Email").ToString)
                End If
            End If

            e_mail.Subject = Title
            e_mail.IsBodyHtml = True
            _Email_Body += strb.ToString
            e_mail.Body = _Email_Body
            Smtp_Server.Send(e_mail)
            MsgBox("Email Notification Successfully sent!", MsgBoxStyle.Information, Title)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

End Module
