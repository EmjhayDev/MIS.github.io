Public Class Login
    Inherits System.Web.UI.Page
    Dim PW As String
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim strusername As String = txtUsername.Text
        Dim strPassword As String = txtPassword.Text
        If txtPassword.Text = "" Then
            lblMessage.Text = "Invalid username or password."
            lblMessage.CssClass = "text-danger"
            Exit Sub
        End If
        gDT = ExecuteQuery("SELECT Fullname FROM Employees WHERE EmployeeID = '" & txtUsername.Text & "'")
        If gDT.Rows.Count > 0 Then
            gEmployeeID = txtUsername.Text
            strusername = gDT.Rows(0)(0)
        Else
            lblMessage.Text = "Invalid username or password."
            lblMessage.CssClass = "text-danger"
            Exit Sub
        End If
        strPassword = GetUserPass(txtUsername.Text)
        PW = GetMD5(txtPassword.Text)
        If PW = strPassword Then
            If strPassword = GetMD5("12345678") Then
                lblMessage.Text = "Invalid username or password."
                lblMessage.CssClass = "text-danger"
            Else
                ' Assume authentication is successful
                Session("Username") = "HI! " + strusername
                Response.Redirect("Default.aspx")
            End If
        Else
            ' Invalid credentials
            lblMessage.Text = "Invalid username or password."
            lblMessage.CssClass = "text-danger"
        End If
    End Sub
    Private Function GetUserPass(ByVal Emp_ID As String)
        Dim User_Password As String = ""
        gDT = ExecuteQuery("EXEC spAccessRoles_GetUserPasswords '" & Emp_ID & "'")
        If gDT.Rows.Count > 0 Then
            User_Password = gDT.Rows(0)("password").ToString
        End If
        Return User_Password
    End Function
End Class