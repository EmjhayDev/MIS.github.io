Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Transactions
Imports System.Text
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Module modConnection


    'Notepad Information for database
    Public idliq As String
    Public sCatalog As String = "192.168.0.73"
    Public sDatabase3 As String = "CAMFIN"
    Public sUsername As String = "sa"
    Public sPassword As String = "c@mfin123it"
    Public gSystem_Year As String

    'Database COnnection variables
    Public gSQlConn As SqlConnection = New SqlConnection("data source = '" & sCatalog & "'; initial catalog = '" & sDatabase3 & "'; user id = '" & sUsername & "'; password = '" & sPassword & "'")
    Public gSQLComm As New SqlCommand
    Public gSQLDA As New SqlDataAdapter
    Public sqlData As SqlDataReader
    Public gDS As New DataSet
    Public gDT As New DataTable
    Public gDT30 As New DataTable
    Public gDT212 As New DataTable
    Public gDT2 As New DataTable
    Public gDT3 As New DataTable
    Public gDT4 As New DataTable
    Public gDS2 As New DataSet


    'HRIS Database COnnection variables
    Public gSQlConn_HRIS As New SqlConnection
    Public gSQLComm_HRIS As New SqlCommand
    Public gSQLDA_HRIS As New SqlDataAdapter
    Public sqlData_HRIS As SqlDataReader
    Public gDS_HRIS As New DataSet
    Public gDT_HRIS As New DataTable

    'variable for crystal reports
    Public catalog As String
    Public database As String
    Public username As String
    Public password As String
    'Dim live As New Catalog(0)
    'Dim DBa As New Database(0)
    'Dim DBab As New Database(1)
    'Dim userid As New Username(0)
    'Dim PassID As New Password(0)
    'Dim domain As String = "192.168.0.120"
    Public Branch_Code As String


    '#Region "Reading Datasource for database connection"
    '    Public Sub ReadDataSource()
    '        Try
    '            sCatalog = live.Catg
    '            sDatabase = DBa.Data
    '            sDatabase3 = DBab.Data
    '            sUsername = userid.User
    '            sPassword = PassID.Pass
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '    End Sub
    '#End Region


#Region "Database Connection"
    Sub sqlConnect()

        Try
            gSQlConn.ConnectionString = "data source = '" & sCatalog & "'; initial catalog = '" & sDatabase3 & "'; user id = '" & sUsername & "'; password = '" & sPassword & "';"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        '---------------------HRIS--------------------------
        Try
            gSQlConn_HRIS.ConnectionString = "data source = '" & sCatalog & "'; initial catalog = '" & sDatabase3 & "'; user id = '" & sUsername & "'; password = '" & sPassword & "';"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '---------------------HRIS--------------------------

    End Sub
#End Region


#Region "MD5"
    Public Function GetMD5(ByVal pw As String)
        gDT = ExecuteQuery("SELECT CONVERT(VARCHAR(32), HashBytes('MD5', '" & pw & "'), 2)")
        If gDT.Rows.Count > 0 Then
            pw = gDT.Rows(0)(0).ToString
        End If
        Return pw
    End Function
#End Region

#Region "Clear String"
    Public Function ClrStr(ByVal str As String)
        str = Replace(str, "'", "''")
        str = Replace(str, "\", "\\")
        ''str = Replace(str, "/", "//")
        str = Replace(str, "`", "``")
        str = Trim(str)
        Return str
    End Function

#End Region
    Public Function convert2fcmdata(ByVal topic As String, ByVal title As String, ByVal message As String)
        Return "topic=" & topic & "&title=" & title & "&message=" & message
    End Function

    Public Function PHP(ByVal url As String, ByVal method As String, ByVal data As String)
        Try

            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(url)
            request.Method = method
            Dim postData = data
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            Return (responseFromServer)
        Catch ex As Exception
            'Dim error1 As String = ErrorToString()
            'If error1 = "Invalid URI: The format of the URI could not be determined." Then
            '    MsgBox("ERROR! Must have HTTP:// before the URL.")
            'Else
            '    MsgBox(error1)
            'End If
            'Return ("ERROR")
        End Try
    End Function


#Region "SQL ExecuteQuery"
    Public Function ExecuteQuery(ByVal strSQL As String) As DataTable
        'Try
        If gSQlConn.State = ConnectionState.Closed Then gSQlConn.Open()


        gDT = New DataTable
        gSQLDA = New SqlDataAdapter(strSQL, gSQlConn)
        gSQLDA.SelectCommand.CommandTimeout = 60
        gSQLDA.Fill(gDT)
        Return gDT
        'MAYANG CONNECTION TIME OUT UPDATE 60 TO 300
        gSQlConn.Close()
        'Catch ex As Exception
        '    MsgBox("Conflict on ID, please contact your IT Administrator")
        '    Return Nothing
        'End Try
    End Function
#End Region

    '-----HRIS
#Region "SQL ExecuteQuery"
    Public Function ExecuteQuery_HRIS(ByVal strSQL As String) As DataTable
        If gSQlConn_HRIS.State = ConnectionState.Closed Then gSQlConn_HRIS.Open()
        gDT_HRIS = New DataTable
        gSQLDA_HRIS = New SqlDataAdapter(strSQL, gSQlConn_HRIS)
        gSQLDA_HRIS.Fill(gDT_HRIS)
        Return gDT_HRIS

        gSQlConn_HRIS.Close()
    End Function
#End Region

#Region "GetSystemYear"
    Public Function GetSystemYear()
        gDT = ExecuteQuery("SELECT FORMAT(getdate(),'yy')")
        If gDT.Rows.Count > 0 Then
            gSystem_Year = gDT.Rows(0)(0).ToString
        End If
        Return gSystem_Year
    End Function
#End Region



    Public notifaa As String

    Public Function GetLastDayofMonth(yourDate As Date) As Date
        Dim nmonth As Integer
        Dim nLastDay As Integer
        Dim nmodRemainder As Integer
        Dim sDate As String

        nmonth = Month(yourDate)

        If nmonth = 4 Or nmonth = 6 Or nmonth = 9 Or nmonth = 11 Then
            nLastDay = 30
        ElseIf nmonth = 2 Then
            nmodRemainder = Year(yourDate) Mod 4
            If nmodRemainder = 0 Then
                nmodRemainder = Year(yourDate) Mod 100
                If nmodRemainder = 0 Then
                    nmodRemainder = Year(yourDate) Mod 400
                    If nmodRemainder = 0 Then
                        nLastDay = 29                ' Leap year
                    Else
                        nLastDay = 28
                    End If
                Else
                    nLastDay = 29
                End If
            Else
                nLastDay = 28
            End If
        Else
            nLastDay = 31
        End If

        sDate = nmonth & "/" & nLastDay & "/" & Year(yourDate)
        GetLastDayofMonth = sDate
    End Function


#Region "SQL RunQuery"
    Public Function RunQuery(ByVal Query As String) As Boolean

        Using scope As New TransactionScope()
            Try
                If gSQlConn.State = ConnectionState.Closed Then gSQlConn.Open()

                gSQLComm = New SqlCommand(Query, gSQlConn)
                gSQLComm.CommandTimeout = 60
                gSQLComm.ExecuteNonQuery()

                scope.Complete()
                gSQlConn.Close()
                Return True

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try

        End Using
    End Function
#End Region

    Sub Send_Email(Subject As String, Message As String, email As String, att As String)
        Try

            Dim strb As New StringBuilder

            Dim _Email_Body As String = ""
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Dim attach As System.Net.Mail.Attachment = New Net.Mail.Attachment(att)
            attach.Name = (att)



            'strb.Append("<STYLE TYPE=text/css> TD{font-family:calibre; font-size: 10pt;}</STYLE> " +
            '            "<Center><h2></h2></center> <table bgcolor=Gainsboro align =center cellpadding =10 style=width:50% border=1> " +
            '            "<Tr><TD><p align =Center><font size =5 face =calibre><b>" & Subject & "<B></font></P></td></tr></table> " +
            '            "<br><br><center><b><br></b></center><br><br></div>" +
            '            "<table align =center cellpadding =8 style=width:50% border= 5> ")

            '<br>Reason : " & Reason_ & " <br> 
            e_mail.Subject = Subject
            Subject.ToString.Contains("MARIANA ACADEMY PROMO")
            strb.Append("<br><center> " & Message & " </Center><br>" &
                       "<br><Center>This is a computer generated e-mail. <br> <B> DO NOT REPLY</B></Center>")

            e_mail.Body = Message
            'e_mail. = MailFormat.Text
            e_mail.Attachments.Add(attach)




            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("mjgacias10@gmail.com", "rpqybwgtpippjmze")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"
            Smtp_Server.Timeout = 300000




            e_mail.From = New MailAddress("mjgacias10@gmail.com")
            'e_mail.IsBodyHtml = False


            e_mail.To.Add(New MailAddress(email))



            e_mail.Subject = Subject
            e_mail.IsBodyHtml = True
            _Email_Body += strb.ToString
            e_mail.Body = _Email_Body
            Smtp_Server.Send(e_mail)



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Function GetImageDataFromDatabase(id As String) As Byte()
        Dim imageData As Byte() = Nothing
        If gSearch = "LEAVE" Then

            If gSQlConn.State = ConnectionState.Closed Then gSQlConn.Open()
            Dim query As String = "SELECT Image FROM [HRIS2].dbo.tblLeave WHERE LeaveNo = @LeaveNo"
            gSQLComm = New SqlCommand(query, gSQlConn)
            gSQLComm.Parameters.AddWithValue("@LeaveNo", id)
            Using reader As SqlDataReader = gSQLComm.ExecuteReader()
                If reader.Read() Then
                    If Not IsDBNull(reader("Image")) Then
                        imageData = CType(reader("Image"), Byte())
                    End If
                End If
            End Using
        ElseIf gSearch = "OBT" Then

            If gSQlConn.State = ConnectionState.Closed Then gSQlConn.Open()
            Dim query As String = "SELECT Image FROM [HRIS2].dbo.tblLeave WHERE LeaveNo = @OBT"
            gSQLComm = New SqlCommand(query, gSQlConn)
            gSQLComm.Parameters.AddWithValue("@OBT", id)
            Using reader As SqlDataReader = gSQLComm.ExecuteReader()
                If reader.Read() Then
                    If Not IsDBNull(reader("Image")) Then
                        imageData = CType(reader("Image"), Byte())
                    End If
                End If
            End Using
        ElseIf gSearch = "OT" Then

            If gSQlConn.State = ConnectionState.Closed Then gSQlConn.Open()
            Dim query As String = "SELECT Image FROM [HRIS2].dbo.tblOvertime WHERE OTNo = @OTNo"
            gSQLComm = New SqlCommand(query, gSQlConn)
            gSQLComm.Parameters.AddWithValue("@OTNo", id)
            Using reader As SqlDataReader = gSQLComm.ExecuteReader()
                If reader.Read() Then
                    If Not IsDBNull(reader("Image")) Then
                        imageData = CType(reader("Image"), Byte())
                    End If
                End If
            End Using
        End If

        Return imageData
    End Function




End Module
