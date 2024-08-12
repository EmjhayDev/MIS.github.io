<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WEBMIS.Login" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <link href="../Content/loginstyle.css" rel="stylesheet" />
</head>
<body>
    <div class="login-container">
        <div class="login-form">
            <h2 class="text-center">Login</h2>
            <form id="loginForm" runat="server">
                <div class="form-group">
                    <label for="username"><i class="fas fa-user"></i> Username</label>
                    <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server" placeholder="Enter username" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="password"><i class="fas fa-lock"></i> Password</label>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password" placeholder="Enter password" required></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" CssClass="btn btn-primary btn-block" runat="server" Text="Login" OnClick="btnLogin_Click" />
            </form>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>