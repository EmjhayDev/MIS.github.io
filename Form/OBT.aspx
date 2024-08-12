<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OBT.aspx.vb" Inherits="WEBMIS.OBT" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>OBT Approval</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <link href="../Content/mainform.css" rel="stylesheet" />
    <link href="../Content/dashboard.css" rel="stylesheet" />
    <link href="../Content/Cashadvances.css" rel="stylesheet" />
    <link href="../Content/Paginationstyle.css" rel="stylesheet" />
   <link id="darkmode-css" rel="stylesheet" href="../Content/darkmode.css" disabled>
    <script src="../Scripts/navscript.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
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
        <br>
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
                    <h2>OBT Approval</h2>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-4"
                        AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                               <ItemTemplate>
                                 <asp:CheckBox ID="chkApprove" runat="server" Text="	&#x2714;" />
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                               <ItemTemplate>
                                 <asp:CheckBox ID="chkDisapprove" runat="server" Text="X"/>
                               </ItemTemplate>
                           </asp:TemplateField>
                            <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info" 
                                        CommandArgument='<%# Eval("OBT No") %>' OnClick="btnView_Click" />
                                 </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="Employee ID" HeaderText="Employee ID" />
                        <asp:BoundField DataField="Full Name" HeaderText="Full Name" />
                        <asp:BoundField DataField="OBT No" HeaderText="OBT Number" />
                        <asp:BoundField DataField="Date Encoded" HeaderText="Date Encode" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="Type" HeaderText="Type" />
                        <asp:BoundField DataField="CoverFrom" HeaderText="Cover From" />
                        <asp:BoundField DataField="Date From" HeaderText="Date From" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="CoverTo" HeaderText="Cover To" />
                        <asp:BoundField DataField="Date To" HeaderText="Date To" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="Ndays" HeaderText="NDays" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Reason" HeaderText="Reason" />
                        <asp:BoundField DataField="Section Approval No." HeaderText="Section Approval No." />
                        <asp:BoundField DataField="Section: Approved By" HeaderText="Section: Approved By" />
                        <asp:BoundField DataField="Section: Date" HeaderText="Section: Date" />
                        <asp:BoundField DataField="Section: Status" HeaderText="Section: Status" />
                        <asp:BoundField DataField="Section: Reason" HeaderText="Section: Reason" />
                        <asp:BoundField DataField="Department Approval No." HeaderText="Department Approval No." />
                        <asp:BoundField DataField="Department: Approved By" HeaderText="Department: Approved By" />
                        <asp:BoundField DataField="Department: Date" HeaderText="Department: Date" />
                        <asp:BoundField DataField="Department: Status" HeaderText="Department: Status" />
                        <asp:BoundField DataField="Department: Reason" HeaderText="Department: Reason" />
                        <asp:BoundField DataField="Group Approval No." HeaderText="Group Approval No." />
                        <asp:BoundField DataField="Group: Approved By" HeaderText="Group: Approved By" />
                        <asp:BoundField DataField="Group: Date" HeaderText="Group: Date" />
                        <asp:BoundField DataField="Group: Status" HeaderText="Group: Status" />
                        <asp:BoundField DataField="Group: Reason" HeaderText="Group: Reason" />
                       </Columns>
                    </asp:GridView>
                </div>
                <br/>
                 <div class="button-container">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-spacing" Height="49px" Width="127px" />
               </div>
                <!-- Modal -->
                <div class="modal fade" id="imageModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Image Preview</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:Image ID="imgModal" runat="server" CssClass="img-fluid" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>