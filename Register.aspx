<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personalized Surveys</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/MySyles2.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" method="post">
    <div class="vertical-center">
        <div class="col-md-4 container" style="border: 1px #0094ff solid;border-radius:10px;background-color:#0094ff">
            <h1 class="jumbotron text-center" style="border:1px #000 solid;margin-top:15px;margin-bottom:0px;
                        padding-bottom:10px;padding-top:10px;">Personalized Surveys</h1>
            <div class="jumbotron" style="border:1px #000 solid;margin-top:15px;margin-bottom:15px;
                        padding-bottom:10px;padding-top:10px;">
                <h4>Register</h4>
                <hr />
                <asp:Table ID="Table1" runat="server">
                    <asp:TableRow ID="Row1">
                        <asp:TableCell>
                            <asp:Label ID="Company_Namelb" runat="server" Text="Label" >Company Name: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="Company_Nametxt" CssClass="form-control-sm input-group-sm" runat="server" 
                                Name="Company_Nametxt"></asp:TextBox>
                            <%--<input id="Company_Nametxt" class="form-control-sm input-group-sm" type="text" name="Company_Nametxt"/>--%>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="Row2">
                        <asp:TableCell>
                            <asp:Label ID="First_Namelb" runat="server" Text="Label">First Name: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="First_Nametxt" CssClass="form-control-sm input-group-sm" runat="server" 
                                Name="First_Nametxt"></asp:TextBox>
                            <%--<input id="First_Nametxt" class="form-control-sm input-group-sm" name="First_Nametxt" type="text" />--%>
                            <%--<font color="red">*</font>--%> 
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="red">*</font> 
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="Row3">
                        <asp:TableCell>
                            <asp:Label ID="Last_Namelb" runat="server" Text="Label">Last Name: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="Last_Nametxt" CssClass="form-control-sm input-group-sm" runat="server" 
                                Name="Last_Nametxt"></asp:TextBox>
                            <%--<input id="Last_Nametxt" class="form-control-sm input-group-sm" name="Last_Nametxt" type="text" />--%>
                            <%--<font color="red">*</font>--%> 
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="red">*</font> 
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="Row4">
                        <asp:TableCell>
                            <asp:Label ID="Emaillb" runat="server" Text="Label">Email: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="Emailtxt" CssClass="form-control-sm input-group-sm" runat="server" 
                                Name="Emailtxt" TextMode="Email"></asp:TextBox>
                            <%--<input id="Emailtxt" class="form-control-sm input-group-sm" name="Emailtxt" type="text" />--%>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="Row5">
                        <asp:TableCell>
                            <asp:Label ID="Usernamelb" runat="server" Text="Label">Username: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="Usernametxt" CssClass="form-control-sm input-group-sm" runat="server" 
                                Name="Usernametxt"></asp:TextBox>
                            <%--<input id="Usernametxt" class="form-control-sm input-group-sm" name="Usernametxt" type="text" />--%>
                            
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="red">*</font> 
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Passwrodlb" runat="server" Text="Label">Password: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="Passwordtxt" CssClass="form-control-sm input-group-sm" runat="server" 
                                Name="Passwordtxt" TextMode="Password"></asp:TextBox>
                            <%--<input id="Passwordtxt" class="form-control-sm input-group-sm" name="Passwordtxt" type="text" />--%>
                            <%--<font color="red">*</font>--%> 
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="red">*</font> 
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Confirm_Passwordlb" runat="server" Text="Label">Confirm Password: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="Confirm_Passwordtxt" CssClass="form-control-sm input-group-sm" runat="server" 
                                Name="Confirm_Passwordtxt" TextMode="Password"></asp:TextBox>
                            <%--<input id="Confirm_Passwordtxt" class="form-control-sm input-group-sm" name="Confirm_Passwordtxt" type="text" />--%>
                            <%--<font color="red">*</font>--%> 
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="red">*</font> 
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <%--<input id="Button1" class="btn btn-secondary" type="button" value="Login" style="float:right;"/>--%>
                            <%--<input id="Button2" class="btn btn-primary btn-sm" type="button" value="Register" />--%>
                            <asp:Button ID="Registerbtn" CssClass="btn btn-primary btn-sm" runat="server" Text="Register"
                                OnClick="Registerbtn_OnClick" />
                            <a href="./Login.aspx" class="btn btn-secondary btn-sm" role="button" 
                                aria-disabled="true" style="float:right;">Login</a>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <hr />
                <asp:Label ID="Messagelbl2" runat="server" Text=""><font color="red">*</font> Requried field</asp:Label>
                <br />
                <asp:Label ID="Messagelbl" runat="server" Text="" ForeColor="Red"></asp:Label>

            </div>       
        </div>
    </div>

    </form>
</body>
</html>
