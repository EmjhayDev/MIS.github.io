Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.IO
Public Class ReportPage
    Inherits System.Web.UI.Page

#Region "crystalmethods"
    Private Function GetPayeeId(ByVal cashAdvanceNo As String) As String
        Dim payeeId As String = Nothing
        gDT = ExecuteQuery($"select Employee_ID from CashAdvances where Cash_AdvanceNo = '{cashAdvanceNo}'")
        If gDT.Rows.Count > 0 Then
            payeeId = gDT(0)(0).ToString()
        End If
        Return payeeId
    End Function
    Private Sub SetDataSourceConnections(ByVal rpt As ReportDocument, ByVal servers() As String)
        Dim connInfos As ConnectionInfo = New ConnectionInfo()
        connInfos.ServerName = servers(0) ' Your SQL Server
        connInfos.DatabaseName = "CAMFIN" ' Your database name
        connInfos.UserID = "sa" ' Your username
        connInfos.Password = "c@mfin123it" ' Your password

        For Each table As Table In rpt.Database.Tables
            Dim tableLogon As TableLogOnInfo = table.LogOnInfo
            tableLogon.ConnectionInfo = connInfos
            table.ApplyLogOnInfo(tableLogon)
        Next
    End Sub

    Sub CrystalReport(ByVal rpt As ReportDocument, ByVal id As String)
        Dim srvr() As String = {"192.168.0.73"} ' Ensure this is your correct server
        SetDataSourceConnections(rpt, srvr)
        For Each subReport As ReportDocument In rpt.Subreports
            SetDataSourceConnections(subReport, srvr)
        Next
        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.DataBind()
        Dim stream As Stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat)
        Using memoryStream As New MemoryStream()
            stream.CopyTo(memoryStream)
            Dim buffer As Byte() = memoryStream.ToArray()
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "inline;filename='" & id & "'.pdf")
            Response.BinaryWrite(buffer)
            Response.End()
        End Using
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(gReport) Then
                Select Case gReport
                    Case "LIQUIDATION"
                        Dim liquidationNo As String = Request.QueryString("liqno")
                        If Not String.IsNullOrEmpty(liquidationNo) Then
                            Dim rpt As New ReportDocument()
                            rpt.Load(Path.Combine(Server.MapPath("~/Reports"), "rptLiquidation.rpt"))
                            rpt.SetParameterValue("@liqno", liquidationNo)
                            CrystalReport(rpt, liquidationNo)
                        Else
                            Response.Write("Liquidation number is missing.")
                        End If
                    Case "CASHADVANCE"
                        Dim cashAdvanceNo As String = Request.QueryString("cashadv")
                        Dim payeeId As String = GetPayeeId(cashAdvanceNo)
                        If Not String.IsNullOrEmpty(cashAdvanceNo) Then
                            Dim rpt As New ReportDocument()
                            rpt.Load(Path.Combine(Server.MapPath("~/Reports"), "rptCashAdvance.rpt"))
                            rpt.SetParameterValue("@cadvanceno", cashAdvanceNo)
                            rpt.SetParameterValue("@payeeID", payeeId)
                            CrystalReport(rpt, cashAdvanceNo)
                        Else
                            Response.Write("Cash Advance number is missing.")
                        End If
                    Case Else
                        Response.Write("Invalid report type specified.")
                End Select
            End If
        End If
    End Sub
End Class