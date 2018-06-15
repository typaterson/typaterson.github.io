<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personalized Surveys</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/MySyles2.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" method="post">
    <div class="vertical-center" >
        <div class="col-md-4 container" style="border: 1px #0094ff solid;border-radius:10px;background-color:#0094ff;">
            <h1 class="jumbotron text-center" style="border:1px #000 solid;margin-top:15px;margin-bottom:0px;
                        padding-bottom:10px;padding-top:10px;">Personalized Surveys</h1>
            <div class="jumbotron" style="border:1px #000 solid;margin-top:15px;margin-bottom:15px;
                        padding-bottom:10px;padding-top:10px;">
                <h4>Login</h4>
                <hr />
                <asp:Table ID="Table1" runat="server">
                    <asp:TableRow ID="Row1">
                        <asp:TableCell>
                            <asp:Label ID="Usernamelbl" runat="server" Text="Label">Username: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="Usernametxt" CssClass="form-control-sm input-group-sm" runat="server" 
                                Name="Usernametxt"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Passwordlbl" runat="server" Text="Label">Password: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="Passwordtxt" CssClass="form-control-sm input-sm" runat="server" 
                                TextMode="Password" Name="Passwordtxt"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="Loginbtn" CssClass="btn btn-primary btn-sm" runat="server" Text="Login"
                                OnClick="Loginbtn_OnClick" />
                            <%--<input id="Button1" class="btn btn-primary btn-sm" type="button" value="Login" />--%>
                            <a href="./Register.aspx" class="btn btn-secondary btn-sm" role="button" 
                                aria-disabled="true" style="float:right;">Register</a>
                            <%--<input id="Button2" class="btn btn-secondary" type="button" value="Register" style="float:right;"/>--%>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <hr />

                <asp:Label ID="Messagelbl" runat="server" Text=""></asp:Label>


                
            </div>
        </div>
    </div>
    </form>
</body>
</html>
