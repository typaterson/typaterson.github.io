<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewsurveys.aspx.cs" Inherits="viewsurveys" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <hr />
            <h4>View Surveys</h4>
            <hr />
            <div class="row">
                <div class="col-3">
                    <div class="container"  style="border:solid 1px black;border-left:solid 2px black; border-top:solid 2px black;
                        border-radius:5px;">
                        <hr />
                        <h5>Active Surveys</h5>
                        <hr />
                        <asp:PlaceHolder ID="SurveysPlaceholder" runat="server">
                            There are currently no surveys set active.
                        </asp:PlaceHolder>
                    </div>
                </div>
                <div class="col-9">
                    <div class="container-fluid"  style="border:solid 1px black;border-left:solid 2px black; border-top:solid 2px black;
                        border-radius:5px;">
                        <hr />
                        <h5>
                            <asp:Label ID="TitleLbl" runat="server" Text="View Surveys"></asp:Label>
                        </h5>
                        <hr />
                        <asp:Label ID="ViewSurveyMessageLbl" runat="server" Visible ="false"></asp:Label>
                        <asp:PlaceHolder ID="ViewSurveyPlaceholder" runat="server" Visible="false">
                            <asp:Button ID="ViewSurveyCancelBtn1" runat="server" Text="Cancel" OnClick="ViewSurveyCancelBtn_OnClick"
                                CssClass="btn btn-secondary btn-sm" Style="margin-bottom:10px;"/>
                            <asp:GridView ID="ViewSurveyGridView" runat="server" onrowcommand="ViewSurveyGridView_RowCommand"
                                CssClass="">
                                <Columns>
                                    <asp:TemplateField HeaderText="View Survey">
                                        <ItemTemplate>
                                            <asp:Button ID="ViewButton" runat="server" CommandName="ViewResponse"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="View" CssClass="btn btn-primary btn-sm"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="ViewSurveyCancelBtn2" runat="server" Text="Cancel" OnClick="ViewSurveyCancelBtn_OnClick"
                                CssClass="btn btn-secondary btn-sm" Style="margin-top:10px;"/>
                        </asp:PlaceHolder>
                        
                        <asp:PlaceHolder ID="ViewResponsePlaceholder" runat="server" Visible="false">
                            <asp:Button ID="ViewResponseBackBtn1" runat="server" Text="Back" OnClick="ViewResponseBackBtn_OnClick"
                                CssClass="btn btn-secondary btn-sm" Style="margin-bottom:10px;"/>
                            <asp:GridView ID="ViewResponseGridView" runat="server"></asp:GridView>
                            <asp:Button ID="ViewResponseBackBtn2" runat="server" Text="Back" OnClick="ViewResponseBackBtn_OnClick"
                                CssClass="btn btn-secondary btn-sm" Style="margin-top:10px;"/>
                        </asp:PlaceHolder>
                        
                        <hr />
                        <asp:PlaceHolder ID="NPSPlaceholder" runat="server" Visible="false">
                            <asp:Label runat="server" Text="Net Promoter Score: "></asp:Label>
                            <asp:Label ID="NPSLbl" runat="server" Text=""></asp:Label>
                            <asp:PlaceHolder ID="NPSGraphPlaceholder" runat="server">

                            </asp:PlaceHolder>
                            <hr />
                        </asp:PlaceHolder>
                        
                        <asp:Label ID="Messagelbl" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>

            <hr />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

