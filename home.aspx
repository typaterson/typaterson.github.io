<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="HomeUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <hr />
            <h3>Home</h3>
            <hr />
            <div class="row">
                <div class="col-6">
                    <div class="container-fluid" style="border:solid 2px black;border-radius:5px;">
                        <hr />
                        <h5>Profile</h5>
                        <hr />
                        <asp:PlaceHolder ID="NamePlaceholder" runat="server">
                            <asp:Label runat="server" Text="Name: "></asp:Label>
                            <asp:Label ID="NameLbl" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Button ID="EditNameBtn" runat="server" Text="Edit Name" OnClick="EditNameBtn_OnClick" 
                                CssClass="btn btn-primary btn-sm" Style="margin-top:10px"/>                    
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="EditNamePlaceholder" runat="server" Visible="false">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="FirstNameLbl" runat="server" Text="Fist Name: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="FirstNameTxt" runat="server" Text="" CssClass="form-control-sm input-group-sm"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="LastNameLbl" runat="server" Text="Last Name: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="LastNameTxt" runat="server" Text="" CssClass="form-control-sm input-group-sm"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                            <asp:Button ID="SaveNameBtn" runat="server" Text="Save" OnClick="SaveNameBtn_OnClick" 
                                CssClass="btn btn-primary btn-sm" Style="margin-top:10px"/>
                            <asp:Button ID="CancelBtn" runat="server" Text="Cencel" OnClick="CancelNameBtn_OnClick" 
                                CssClass="btn btn-secondary btn-sm" Style="margin-top:10px;margin-left:10px;"/>
                        </asp:PlaceHolder>
                        <asp:Label ID="NameMessageLbl" Text="" runat="server" Style="margin-left:10px" Visible="false"></asp:Label>
                        <hr />
                        <asp:Label runat="server" Text="Username: "></asp:Label>
                        <asp:Label ID="UsernameLbl" runat="server" Text=""></asp:Label>
                        <hr />
                        <asp:PlaceHolder ID="EmailPlaceholder" runat="server">
                            <asp:Label runat="server" Text="Email: "></asp:Label>
                            <asp:Label ID="EmailLbl" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Button ID="EditEmailBtn" runat="server" Text="Edit Email" OnClick="EditEmailBtn_OnClick" 
                                CssClass="btn btn-primary btn-sm" Style="margin-top:10px"/>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="EditEmailPlaceholder" runat="server" Visible="false">
                            <asp:Table runat="server">                        
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="EditEmailLbl" runat="server" Text="Email: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="EmailTxt" runat="server" Text="" CssClass="form-control-sm input-group-sm" TextMode="Email"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Button ID="SaveEmailBtn" runat="server" Text="Save" OnClick="SaveEmailBtn_OnClick" 
                                CssClass="btn btn-primary btn-sm" Style="margin-top:10px"/>
                            <asp:Button ID="CancelEmailBtn" runat="server" Text="Cencel" OnClick="CancelEmailBtn_OnClick" 
                                CssClass="btn btn-secondary btn-sm" Style="margin-top:10px;margin-left:10px;"/>
                        </asp:PlaceHolder>
                        <asp:Label ID="EmailMessageLbl" Text="" runat="server" Style="margin-left:10px" Visible="false"></asp:Label>
                        <br />
                        <hr />
                        <asp:PlaceHolder ID="CompanyPlaceholder" runat="server">
                            <asp:Label runat="server" Text="Company: "></asp:Label>
                            <asp:Label ID="CompanyLbl" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Button ID="EditCompanyBtn" runat="server" Text="Edit Company" OnClick="EditCompanyBtn_OnClick" 
                                CssClass="btn btn-primary btn-sm" Style="margin-top:10px"/>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="EditCompanyPlaceholder" runat="server" Visible="false">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Company: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="CompanyTxt" runat="server" Text="" CssClass="form-control-sm input-group-sm"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Button ID="SaveCompanyBtn" runat="server" Text="Save" OnClick="SaveCompanyBtn_OnClick" 
                                CssClass="btn btn-primary btn-sm" Style="margin-top:10px"/>
                            <asp:Button ID="CancelCompanyBtn" runat="server" Text="Cencel" OnClick="CancelCompanyBtn_OnClick" 
                                CssClass="btn btn-secondary btn-sm" Style="margin-top:10px;margin-left:10px;"/>
                        </asp:PlaceHolder>
                        <asp:Label ID="CompanyMessageLbl" Text="" runat="server" Style="margin-left:10px" Visible="false"></asp:Label>
                        <hr />
                        <asp:PlaceHolder ID="PasswordPlaceholder" runat="server">
                            <asp:Button ID="EditPasswordBtn" runat="server" Text="Edit Password" OnClick="EditPasswordBtn_OnClick"
                            CssClass="btn btn-primary btn-sm" Style="margin-top:10px" />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="EditPasswordPlaceholder" runat="server" Visible="false">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Old Password: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="OldPasswordtxt" runat="server" CssClass="form-control-sm input-group-sm" TextMode="Password"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="New Password: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="NewPasswordTxt" runat="server" CssClass="form-control-sm input-group-sm" TextMode="Password"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="New Password: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="RepeatNewPasswordTxt" runat="server" CssClass="form-control-sm input-group-sm" TextMode="Password"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Button ID="SavePasswordBtn" runat="server" Text="Save" OnClick="SavePasswordBtn_OnClick" 
                                CssClass="btn btn-primary btn-sm" Style="margin-top:10px"/>
                            <asp:Button ID="CancelPasswordBtn" runat="server" Text="Cencel" OnClick="CancelPasswordBtn_OnClick" 
                                CssClass="btn btn-secondary btn-sm" Style="margin-top:10px;margin-left:10px;"/>
                        </asp:PlaceHolder>
                        <asp:Label ID="PasswordMessageLbl" Text="" runat="server" Style="margin-left:10px" Visible="false"></asp:Label>
                        <hr />
                    </div>
                </div>
            </div>
            <hr />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>