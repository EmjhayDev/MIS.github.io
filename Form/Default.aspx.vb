Imports System.Web.UI
Imports System.Data.SqlClient
Imports System.Web.Services
Public Class _Default
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("Username") IsNot Nothing Then
                usernameLabel.Text = Session("Username").ToString()
                cashadvances()
                liquidations()
                'HRIS()
            Else
                usernameLabel.Text = "Guest" ' Or handle as needed
            End If
        End If
    End Sub

    Sub cashadvances()
        gDT = ExecuteQuery("Select count(*) from cashadvances where [remarks]='ENDORSED'")
        If gDT.Rows.Count > 0 Then
            txtcashadvance.Text = gDT(0)(0)
        End If
    End Sub
    Sub liquidations()
        gDT = ExecuteQuery("Select count(*) from liquidations where [remarks]='ENDORSED'")
        If gDT.Rows.Count > 0 Then
            txtliquidation.Text = gDT(0)(0)
        End If
    End Sub

    'Sub HRIS()
    '    gDT = ExecuteQuery("exec [CountHRIS_Approval2] '" & gEmployeeID & "'")
    '    If gDT.Rows.Count > 0 Then
    '        txthris.Text = gDT(0)("TotalCount")
    '    End If
    'End Sub
End Class