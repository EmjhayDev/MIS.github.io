function printReport() {
            document.getElementById('<%= CrystalReportViewer1.ClientID %>').PrintReport();
        }
        function exportReport(format) {
            document.getElementById('<%= CrystalReportViewer1.ClientID %>').ExportReport(format);
        }
        function zoomReport(level) {
            document.getElementById('<%= CrystalReportViewer1.ClientID %>').Zoom(level);
        }