<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="individuele_opdracht.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
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
        <asp:Table align="center" runat="server" Width="932px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Panel ID="GroupBox1" runat="server" Height="450px" Width="170px">
                        <br />
                        <br />
                        <br />
                        Titel:
                        <asp:TextBox ID="tbTitel" runat="server" Width="162px" CssClass="whitespace"></asp:TextBox>
                        Text:
                        <asp:TextBox ID="tbBericht" runat="server" Width="162px" Height="185" CssClass="whitespace"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnVoegCategorieToe" runat="server" class="btn btn-primary" Text="Voeg Bericht toe" Width="168px" OnClick="btnUploadfile_Click" />
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="WhiteSpace1" runat="server" Height="450px" Width="70px">
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="GroupBox2" runat="server" Width="450px" Height="450px">
                        <asp:ListBox ID="lbFileSharing" runat="server" Width="450px" Height="518px" />
                        <asp:Button ID="ButtonCategorie" runat="server" class="btn btn-primary" Text="Selecteer categorie" Width="200px" OnClick="lbFileSharing_DoubleClick" />
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="WhiteSpace2" runat="server" Height="450px" Width="70px">
                    </asp:Panel>
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
                <asp:TableCell>
                    <asp:Panel ID="WhiteSpace3" runat="server" Height="450px" Width="70px">
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DataGrid ID="likesdislikes" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:BoundColumn DataField="LIKES" HeaderText="Likes"/>
                            <asp:BoundColumn DataField="DISLIKES" HeaderText="Dislikes"/>
                        </Columns>
                    </asp:DataGrid>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GetlikesConnectie %>" ProviderName="<%$ ConnectionStrings:GetlikesConnectie.ProviderName %>" SelectCommand="SELECT SUM(LIKES) AS likes, SUM(DISLIKES) AS dislikes FROM DBI260972.BESTAND"></asp:SqlDataSource>
    </form>
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
