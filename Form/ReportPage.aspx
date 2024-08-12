<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportPage.aspx.vb" Inherits="WEBMIS.ReportPage" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
    <link href="../Content/Reportpagestyle.css" rel="stylesheet" />
    <script src="../Scripts/Reportpagescript.js"></script>
</head>
<body>
     <form id="form1" runat="server">
        <div class="report-container">
           <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>