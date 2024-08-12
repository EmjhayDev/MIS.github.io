Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Public Class Liquidations
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
            If Session("Username") IsNot Nothing Then
                usernameLabel.Text = Session("Username").ToString()
            Else
                usernameLabel.Text = "Guest" ' Or handle as needed
            End If
        End If
    End Sub
    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    Private Sub BindGrid()
        gDT = ExecuteQuery("EXEC dbo.spCA_Liquidation_approval  '" & gEmployeeID & "', 'ENDORSED', 'Liquidation' ")
        If gDT.Rows.Count > 0 Then
            GridView1.DataSource = gDT
            GridView1.DataBind()
        End If
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim strquery As String = Nothing
        For Each row As GridViewRow In GridView1.Rows
            Dim chkApprove As CheckBox = CType(row.FindControl("chkApprove"), CheckBox)
            Dim Liquidationno As String = row.Cells(1).Text ' Assuming the first column is LiquidationNo
            If chkApprove IsNot Nothing AndAlso chkApprove.Checked Then
                strquery = strquery + "UPDATE Liquidations SET Approved_by = '" & gEmployeeID.ToUpper & "',Date_Approved = GETDATE(), Remarks = 'APPROVED' WHERE LiquidationNo = '" & Liquidationno & "'"
            End If
        Next
        ' Display success message if rows were affected
        If RunQuery(strquery) = True Then
            Dim script As String = "alert('Transaction completed successfully!');"
            ClientScript.RegisterStartupScript(Me.GetType(), "TransactionSuccess", script, True)
            'Optionally re-bind the grid to refresh data
            BindGrid()
        End If
    End Sub

    Protected Sub btnVoid_Click(sender As Object, e As EventArgs) Handles btnVoid.Click
        Dim strquery As String = Nothing
        For Each row As GridViewRow In GridView1.Rows
            Dim chkApprove As CheckBox = CType(row.FindControl("chkApprove"), CheckBox)
            Dim Liquidationno As String = row.Cells(1).Text ' Assuming the first column is LiquidationNo
            If chkApprove IsNot Nothing AndAlso chkApprove.Checked Then
                strquery = strquery + "UPDATE Liquidations SET Approved_by = '" & gEmployeeID.ToUpper & "',Date_Approved = GETDATE(), Remarks = 'VOID' WHERE LiquidationNo = '" & Liquidationno & "'"
            End If
        Next
        ' Display success message if rows were affected
        If RunQuery(strquery) = True Then
            Dim script As String = "alert('Transaction voided successfully!');"
            ClientScript.RegisterStartupScript(Me.GetType(), "TransactionVoided", script, True)
            'Optionally re-bind the grid to refresh data
            BindGrid()
        End If
    End Sub

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As EventArgs)
        gReport = "LIQUIDATION"
        Dim btn As Button = CType(sender, Button)
        Dim liquidationNo As String = btn.CommandArgument
        If Not String.IsNullOrEmpty(liquidationNo) Then
            ' Correct URL path to ReportPage.aspx
            Response.Redirect(String.Format("~/Form/ReportPage.aspx?liqno={0}", liquidationNo))
        Else
            ' Handle the case where liquidationNo is empty or null
            ' You can show an error message or log the issue
        End If
    End Sub
End Class