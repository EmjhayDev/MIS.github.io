Public Class OT
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("Username") IsNot Nothing Then
                usernameLabel.Text = Session("Username").ToString()
            Else
                usernameLabel.Text = "Guest" ' Or handle as needed
            End If
            empgroupings()
            If Groupings = "Section" Then
                BindGridLeave("Pending", "", "")
            ElseIf Groupings = "Department" Then
                BindGridLeave("For Approval", "Pending", "")
            ElseIf Groupings = "Group" Then
                BindGridLeave("For Final Approval", "For Approval", "Pending")
            End If

        End If
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        empgroupings()
        If Groupings = "Section" Then
            BindGridLeave("Pending", "", "")
        ElseIf Groupings = "Department" Then
            BindGridLeave("For Approval", "Pending", "")
        ElseIf Groupings = "Group" Then
            BindGridLeave("For Final Approval", "For Approval", "Pending")
        End If
    End Sub
    Private Sub BindGridLeave(Status_1 As String, Status_2 As String, Status_3 As String)
        gDT = ExecuteQuery("exec [HRIS2].dbo.HRIS_Approval2 'OT','" & gEmployeeID & "','" & Groupings & "','" & Status_1 & "','" & Status_2 & "','" & Status_3 & "'")
        If gDT.Rows.Count > 0 Then
            GridView1.DataSource = gDT
            GridView1.DataBind()
        End If
    End Sub
    Protected Sub btnView_Click(sender As Object, e As EventArgs)
        gSearch = "OT"
        Dim button As Button = CType(sender, Button)
        Dim OT As String = button.CommandArgument
        ' Get image data from database
        Dim imageData As Byte() = GetImageDataFromDatabase(OT)
        If imageData IsNot Nothing Then
            ' Convert the byte array to a base64 string
            Dim base64String As String = Convert.ToBase64String(imageData)
            ' Set the ImageUrl of the Image control to display the image
            imgModal.ImageUrl = "data:image/jpeg;base64," & base64String
        Else
            ' Set the ImageUrl to a default 'No Image' placeholder
            imgModal.ImageUrl = ResolveUrl("~/images/no-image.png")
        End If
        ' Register a script to open the modal
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenModal", "$('#imageModal').modal('show');", True)
    End Sub
End Class