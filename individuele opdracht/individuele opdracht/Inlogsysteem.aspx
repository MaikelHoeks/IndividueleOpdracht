<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inlogsysteem.aspx.cs" Inherits="individuele_opdracht.Inlogsysteem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Inlog Systeem</title>
    <link href="css/bootstrap.min.css" rel="Stylesheet" type="text/css" />
   
</head>
<body>
    <nav class="navbar navbar-inverse" role="navigation">
        <div class="container-fluid">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="inlogsysteem.aspx">Individuele opdracht</a>
            </div>
        </div>
    </nav>


    <div class="container">
        <form class="form-signin" role="form" runat="server">
            <h2 class="form-signin-heading">Please sign in</h2>
            <asp:TextBox type="text" class="form-control" placeholder="Inlognaam" id="inlognaam" runat="server" required="required" autofocus="autofocus"/>
            <asp:TextBox type="password" class="form-control" placeholder="Wachtwoord" required="required" runat="server" id="wachtwoord"/>
            <asp:Label class="checkbox" runat="server"/>
                 <asp:CheckBox value="remember-me" id="Persist" runat="server"/>
                Remember me
            <asp:Button ID="loginbutton" class="btn btn-lg btn-primary btn-block" type="button"
                OnClick="Button1_click" runat="server" Text="Sign in" />
        </form>
    </div>
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
