<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CashAdvances.aspx.vb" Inherits="WEBMIS.CashAdvances" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Cash Advances Approval</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <link href="../Content/mainform.css" rel="stylesheet" />
    <link href="../Content/dashboard.css" rel="stylesheet" />
    <link href="../Content/Cashadvances.css" rel="stylesheet" />
    <link href="../Content/Modalstyle.css" rel="stylesheet" />
    <link href="../Content/Paginationstyle.css" rel="stylesheet" />
    <link id="darkmode-css" rel="stylesheet" href="../Content/darkmode.css" disabled>
    <script src="../Scripts/navscript.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
     <script src="../Scripts/Reportviewermodal.js"></script>
    <script src="../Scripts/Darkmodescript.js"></script>
</head>
<body>
    <div class="nav-container">
      <button class="openbtn" onclick="toggleNav()">
          <div class="bar1"></div>
          <div class="bar2"></div>
          <div class="bar3"></div>
      </button>
        <label class="ui-switch">
          <input type="checkbox" id="darkModeToggle">
            <div class="slider">
            <div class="circle"></div>
            </div>
        </label>
    </div>
    <div id="mySidenav" class="sidenav">
        <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
        <asp:Label ID="usernameLabel" runat="server" CssClass="styled-name"></asp:Label>
        <a href="javascript:void(0)" onclick="loadContent('dashboard')"><i class="fas fa-tachometer-alt"></i> Dashboard</a>
        <a href="#"><i class="fas fa-credit-card"></i> Loans</a>
        <a href="javascript:void(0)" onclick="loadContent('CashAdvances')"><i class="fas fa-dollar-sign"></i> Cash Advances</a>
        <a href="javascript:void(0)" onclick="loadContent('Liquidations')"><i class="fas fa-dollar-sign"></i>Liquidations</a>
        <a href="javascript:void(0)" onclick="toggleSubmenu('hrisSubmenu')" class="hris-menu">
        <i class="fas fa-user-tie"></i> HRIS<span class="arrow">&#9662;</span></a>
        <div id="hrisSubmenu" class="submenu">
        <a href="javascript:void(0)" onclick="loadContent('Leave')"><i class="fas fa-calendar-alt"></i> Leave</a>
        <a href="javascript:void(0)" onclick="loadContent('OT')"><i class="fas fa-clock"></i>OT</a>
        <a href="javascript:void(0)" onclick="loadContent('OBT')"><i class="fas fa-briefcase"></i> OBT</a>
        <a href="javascript:void(0)" onclick="loadContent('Offset')"><i class="fas fa-sync-alt"></i> Offset</a></div>
        <a href="#"><i class="fas fa-calendar-day"></i> Postponement</a>
        <a href="#"><i class="fas fa-exchange-alt"></i> Pullout</a>
        <a href="#"><i class="fas fa-file-alt"></i> Reports</a>
        <a href="javascript:void(0);" onclick="if(confirmLogout()) { window.location.href='Login.aspx'; }"><i class="fas fa-sign-out-alt"></i> Logout</a> <!-- Logout link -->
    </div>
  </div>
   <div id="mainContent" class="main">
        <form id="form1" runat="server">
            <div class="container mt-4">
                             <div class="table-responsive">
                       <h2>Cash Advance Approval</h2>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-4">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkApprove" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="idno" HeaderText="Cash Advance No" />
                        <asp:BoundField DataField="purpose" HeaderText="Purpose" />
                        <asp:BoundField DataField="employee" HeaderText="Employee Name" />
                        <asp:BoundField DataField="branch" HeaderText="Branch" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="targetdate" HeaderText="Target Date" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="daterequested" HeaderText="Request Date" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:TemplateField>
                                <ItemTemplate>
                                 <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info" CommandArgument='<%# Eval("idno") %>' OnClick="btnView_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br/>
                           <!-- Modal for displaying the report -->
                <div id="reportModal" class="modal fade" tabindex="-1" role="dialog">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Liquidation Report</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" CssClass="border" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="button-container">
                    <asp:Button ID="btnSave" runat="server" Text="Approve" CssClass="btn btn-primary btn-spacing" Height="49px" Width="127px" />
                    <asp:Button ID="btnVoid" runat="server" Text="Void" OnClick="btnVoid_Click" CssClass="btn btn-danger btn-spacing" Height="49px" Width="95px" />
                </div>
            </div>
        </form>
    </div>
     <script src="../Scripts/Reportviewermodal.js"></script>
    </body>
</html>