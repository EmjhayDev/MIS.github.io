Imports System.Data.SqlClient
Public Class Leave
    Inherits System.Web.UI.Page
    Dim Actionform As String = Nothing


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
    Private Sub BindGridLeave(Status_1 As String, Status_2 As String, Status_3 As String)
        Dim source As String = Request.QueryString("source")

        If source = "Leave" Then
            Actionform = "Leave"
        ElseIf source = "Offset" Then
            Actionform = "Offset"
        End If
        gDT = ExecuteQuery("exec [HRIS2].dbo.HRIS_Approval2 '" & Actionform & "','" & gEmployeeID & "','" & Groupings & "','" & Status_1 & "','" & Status_2 & "','" & Status_3 & "'")
        If gDT.Rows.Count > 0 Then
            GridView1.DataSource = gDT
            GridView1.DataBind()
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
    Protected Sub btnView_Click(sender As Object, e As EventArgs)
        gSearch = "LEAVE"
        Dim button As Button = CType(sender, Button)
        Dim leaveNo As String = button.CommandArgument
        ' Get image data from database
        Dim imageData As Byte() = GetImageDataFromDatabase(leaveNo)
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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        empgroupings()
        Dim source As String = Request.QueryString("source")
        Dim loRecordsToBeSaved As Boolean = False
        Dim status As String = Nothing
        ' Get the row index from the CommandArgument
        Dim rowIndex As Integer = Convert.ToInt32(CType(sender, Button).CommandArgument)
        ' Get the row data from the GridView
        Dim row As GridViewRow = GridView1.Rows(rowIndex)
        ' Extract the necessary data from the GridView row
        Dim chkApprove As CheckBox = CType(row.FindControl("chkApprove"), CheckBox)
        Dim chkDisapprove As CheckBox = CType(row.FindControl("chkDisapprove"), CheckBox)
        Dim _Status_ As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "Status").First())).Text
        Dim Type As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "Type").First())).Text
        Dim LeaveNo As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "LeaveNo").First())).Text
        Dim EmployeeID As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "Employee ID").First())).Text
        Dim FullName As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "Full Name").First())).Text
        Dim Ndays As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "NDays").First())).Text
        Dim Reason As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "Reason").First())).Text
        Dim DateFrom As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "Date From").First())).Text
        Dim DateTo As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "Date To").First())).Text
        Dim CoverFrom As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "CoverFrom").First())).Text
        Dim CoverTo As String = row.Cells(GridView1.Columns.IndexOf(GridView1.Columns.Cast(Of DataControlField).Where(Function(c) CType(c, BoundField).DataField = "CoverTo").First())).Text
        If source = "Leave" Then
            Actionform = "Leave"
        ElseIf source = "Offset" Then
            Actionform = "Offset"
        End If
        If GridView1.Rows.Count = 0 Then Exit Sub
        If chkApprove IsNot Nothing AndAlso chkApprove.Checked Then
            loRecordsToBeSaved = True
            status = "Approved"
        ElseIf chkDisapprove IsNot Nothing AndAlso chkDisapprove.Checked Then
            loRecordsToBeSaved = True
            status = "Disapproved"
        End If
        If loRecordsToBeSaved = False Then
            MsgBox("No records to be saved.", vbExclamation, "WEB MIS")
            Exit Sub
        End If
        If MsgBox("Are you sure you want to save these request?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Actionform) = vbNo Then Exit Sub
        If Actionform = "Leave" Then
            gDT = ExecuteQuery("Select Year(getdate()) % 100 Year, Count(1) + 1 Count " &
                               "from tblLeaveApprovalGroup with(nolock)" &
                               "Where SUBSTRING(Approval_No,5,2) = Year(getdate()) % 100 ")
            Dim str As String = Branch_Code & "LD" & gDT(0)("Year") & Format(gDT(0)("Count"), "00000")
            gDT = ExecuteQuery("Insert into tblLeaveApprovalGroup (Approval_No, Leave_No, Date, Status, Reason, Approved_By) Values " &
                                      "('" & str & "','" & LeaveNo & "',getDate(),'" & status & "','','" & gEmployeeID & "')")
            If status = "Disapproved" Then gDT = ExecuteQuery("Update tblLeave set Status = '" & status & "' Where LeaveNo = '" & LeaveNo & "'")
        End If
    End Sub
End Class