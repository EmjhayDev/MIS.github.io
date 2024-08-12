<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="WEBMIS._Default" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Management Information System</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
       <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <link href="../Content/mainform.css" rel="stylesheet" />
    <link href="../Content/dashboard.css" rel="stylesheet" />
    <link id="darkmode-css" rel="stylesheet" href="../Content/darkmode.css" disabled>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="../Scripts/navscript.js"></script>
    <script src="../Scripts/Darkmodescript.js"></script>
    <script src="../Scripts/weatherscript.js"></script>

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
      <div id="mainContent" class="main">
        <!-- Content for each section will be loaded here -->
        <div class="dashboard-content">
            <h2>MANAGEMENT INFORMATION SYSTEM</h2>
            <h3>Current Weather</h3>
            <div id="weather">
                <div class="clouds">
                    <div class="cloud cloud1"></div>
                    <div class="cloud cloud2"></div>
                    <div class="cloud cloud3"></div>
                </div>
                <p id="weather-description">Description: <span id="weather-desc"></span></p>
                <p id="weather-temp">Temperature: <span id="weather-temp-value"></span> °C</p>
                <p id="weather-city">City: <span id="weather-city-name"></span></p>
            </div>
        
            <!-- First row with three cards -->
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Loans</h5>
                            <p class="card-text">0</p>
                            <a href="javascript:void(0)" onclick="loadContent('Loans')" class="btn btn-primary">View Details</a> <!-- Link to details page -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Cash Advances</h5>
                            <asp:Label ID="txtcashadvance" runat="server" CssClass="card-text">0</asp:Label>
                            <a href="javascript:void(0)" onclick="loadContent('CashAdvances')" class="btn btn-primary">View Details</a> <!-- Link to details page -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Liquidations</h5>
                            <asp:Label ID="txtliquidation" runat="server" CssClass="card-text">0</asp:Label>
                            <a href="javascript:void(0)" onclick="loadContent('Liquidations')" class="btn btn-primary">View Details</a> <!-- Link to details page -->
                        </div>
                    </div>
                </div>
            </div>
        
            <!-- Second row with three cards -->
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">HRIS</h5>
                            <asp:Label ID="txthris" runat="server" CssClass="card-text">0</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Postponement</h5>
                            <p class="card-text">0</p>
                            <a href="javascript:void(0)" onclick="loadContent('Postponement')" class="btn btn-primary">View Details</a> <!-- Link to details page -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Pullout</h5>
                            <p class="card-text">0</p>
                            <a href="javascript:void(0)" onclick="loadContent('Pullout')" class="btn btn-primary">View Details</a> <!-- Link to details page -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>