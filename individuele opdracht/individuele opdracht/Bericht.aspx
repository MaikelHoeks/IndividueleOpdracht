<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bericht.aspx.cs" Inherits="individuele_opdracht.Bericht" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bericht</title>
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
                <a class="navbar-brand" href="Home.aspx">Individuele opdracht</a>
            </div>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div>
            <asp:Table align="center" runat="server" Width="932px">
                <asp:TableRow>
                     <asp:TableCell>
                         <asp:Label ID="Labelgegevens" runat="server" Text="Label"></asp:Label>
                         </asp:TableCell>
                    <asp:TableCell>
                        <asp:Panel ID="GroupBox3" runat="server" Height="450px" Width="190px">
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnLike" runat="server" class="btn btn-primary" Text="Like" Width="88px" OnClick="btnLike_Click" />
                            <asp:Button ID="btnDislike" runat="server" class="btn btn-primary" Text="Dislike" Width="88px" OnClick="btnDislike_Click" />
                            <br />
                            <asp:Button ID="btnReport" runat="server" class="btn btn-primary" Text="Report" Width="180px" OnClick="btnReport_Click" />
                            <br />
                            <asp:TextBox ID="tbComment" runat="server" Height="185px" Width="175px" CssClass="whitespace"></asp:TextBox>
                            <br />
                            <asp:Button ID="btnComment" runat="server" Width="180px" class="btn btn-primary" Text="Commentaar toevoegen" OnClick="btnComment_Click" />
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
