<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="layoutmanager.aspx.cs" Inherits="layoutmanager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function creatNewLayout() {
            PageMethods.creatNewLayout(onSuccess, onError);
            function onSuccess(result) {
                document.getElementById("LayoutEditordiv").innerHTML = result;
            }
            function onError(result) {
                alert("Something went wrong.");
            }
        }
        function hello() {
            document.getElementById("LayoutEditordiv").innerHTML = "Hello world!";
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <hr />
    <h4>Layout Manager</h4>
    <hr />
    <div class="container-fluid">
        <div class="row">
            <div class="col-xl-3 col-sm-3 col-md-3 col-lg-3" style="border:solid 1px black;border-left:solid 2px black; border-top:solid 2px black;
                border-radius:5px;">
                <hr />
                <h5>Your Layouts</h5>
                <hr />
                
                <div class="container-fluid" style="border:solid 2px #0094ff;border-radius:5px;">
                    <div class="row">
                        <div class="col-12">
                            <asp:Label ID="NewLayoutlbl" runat="server" Text="">Click button to create a new layout</asp:Label>
                            <br />
                            <asp:Button ID="NewLayoutbtn" CssClass="btn btn-primary btn-sm" runat="server" 
                                Text="New Layout" OnClick="NewLayoutbtn_OnClick" Style="margin-bottom:10px;"/>
                        </div>
                    </div>
                </div>
                
                <hr />
                <asp:Label ID="CreatedLayoutslbl" runat="server" Text="Created Layouts"></asp:Label>

                <asp:PlaceHolder ID="LayoutsPlaceholder" runat="server"></asp:PlaceHolder>
                <hr />
            </div>
            <div class="col-xl-9 col-sm-9 col-md-9 col-lg-9">
                <div class="col-auto" style="border:solid 1px black;border-left:solid 2px black; border-top:solid 2px black;
                border-radius:5px;">
                    <hr />
                    <h5>Layout Editor</h5>
                    <hr />
                    <asp:PlaceHolder ID="LayoutEditorPlaceholder" runat="server">
                        <asp:PlaceHolder ID="DefaultLayoutEditorPlaceholder" runat="server">
                            <div id="LayoutEditordiv"class="container-fluid">
                            <p>The Layout Editor allows you to create or edit a new layout for your survey.</p> 
                            <p>Click the "New Layout" button to create a new layout or click "Edit" to change an existing layout.</p>
                                
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="NewLayoutPlaceholder" runat="server" Visible="false">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="container-fluid">
                                            <asp:Label ID="NameLbl" runat="server" Text="Layout Name"></asp:Label>
                                            <br />
                                             <asp:TextBox ID="NewLayoutNametxt" CssClass="form-control-sm input-group-sm" runat="server" Name="NewLayoutNametxt"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="container-fluid" style="margin-top:10px;">
                                            <asp:Label ID="Upload_Logo" runat="server" Text="Upload Logo"></asp:Label>
                                            <br />
                                            <asp:FileUpload ID="NewLogoFileUpload" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="container-fluid"style="margin-top:10px;">
                                            <asp:Label ID="Color_SelectorLbl" runat="server" Text="Click to choose a color" 
                                            Style="margin-right:15px;"></asp:Label>
                                        <input type='color' name='backgroundcolor' value='#ff0000' />
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="container-fluid"style="margin-top:10px;">
                                            <asp:Button ID="SaveNewLayoutBtn" runat="server" Text="Create Layout" OnClick="SaveNewLayoutbtn_OnClick"
                                                CssClass="btn btn-primary btn-sm"/>
                                            <asp:Button ID="CancelNewLayoutBtn" runat="server" Text="Cancel" OnClick="Cancelbtn_OnClick"
                                                CssClass="btn btn-secondary btn-sm" Style ="margin-left:60px;"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="EditLayoutPlaceholder" runat="server" Visible="false">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-5">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="container-fluid">
                                                    <asp:Label ID="EditLayoutNamelbl" runat="server" Text="Layout Name"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="EditLayoutNametxt" CssClass="form-control-sm input-group-sm" runat="server" Name="EditLayoutNametxt"></asp:TextBox>
                                                    <br />
                                                    <asp:Button ID="EditLayoutNameBtn" CssClass="btn btn-primary btn-sm" runat="server" 
                                                        Text="Update Name" Style="margin-top:10px;" OnClick="EditLayoutNameBtn_OnClick"/>
                                                    <br />
                                                    <asp:Label ID="EditLayoutNameMessagelbl" runat="server" Text="" ForeColor="Red"
                                                        Style="margin-top:10px;"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="container-fluid" style="margin-top:10px;">
                                                    <asp:Label ID="EditUploadLogolbl" runat="server" Text="Upload Logo"></asp:Label>
                                                    <br />
                                                    <asp:FileUpload ID="EditUploadFile" runat="server" />
                                                    <br />
                                                    <asp:Button ID="EditUploadFileBtn" CssClass="btn btn-primary btn-sm" runat="server" 
                                                        Text="Update Logo" Style="margin-top:10px;" OnClick="EditUpdateLogoBtn_OnClick"/>
                                                    <br />
                                                    <asp:Label ID="EditUploadFileMessagelbl" runat="server" Text="" ForeColor="Red"
                                                        Style="margin-top:10px;"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="container-fluid"style="margin-top:10px;">
                                                    <asp:Label ID="Label3" runat="server" Text="Click to choose a color" 
                                                    Style="margin-right:15px;"></asp:Label>
                                                    <asp:PlaceHolder ID="EditChooseColorPlaceholder" runat="server"></asp:PlaceHolder>
                                                    <br />
                                                    <asp:Button ID="EditChooseColorBtn" CssClass="btn btn-primary btn-sm" runat="server" Text="Update Color" 
                                                        OnClick="EditChooseColorBtn_OnClick" Style="margin-top:10px;"/>
                                                    <br />
                                                    <asp:Label ID="EditChooseColorMessageLbl" runat="server" Text="" ForeColor="Red"
                                                        Style="margin-top:10px;"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    
                                        <asp:PlaceHolder ID="SampleLayoutPlaceholder" runat="server">
                                        <div class="col-7">
                                            <asp:Panel ID="Panel1" CssClass="jumbotron-fluid" runat="server" Style="border:solid black 1px;">
                                                <div class="jumbotron-fluid" style="background-color:white;margin:10px 10px 10px 10px;border-radius:5px;">
                                                    <div class="col-10 container">
                                                        <div class="row" style="padding:10px 10px 10px 10px">
                                                            <div class="col-9">
                                                                <h3 style="margin-right:30px;">Sample Tittle</h3>
                                                            </div>
                                                            <div class="col-1">
                                                                <asp:image ID="SampleLogo" width="100px" runat="server"></asp:image>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="jumbotron-fluid" style="background-color:white;margin:10px 10px 10px 10px;border-radius:5px;">
                                                    <div class="col-12 container">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                This is a sample question. That would apear on a survey.
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                                

                                            </asp:Panel>
                                        </div>
                                            </asp:PlaceHolder>
                                </div>
                            </div>
                            
                        </asp:PlaceHolder>
                        <div class="container-fluid">
                            <asp:PlaceHolder ID="MessagePlaceholder" runat="server"></asp:PlaceHolder>
                        </div>
                        
                    </asp:PlaceHolder>
                    <hr />
                    
                </div>
                
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
</asp:Content>