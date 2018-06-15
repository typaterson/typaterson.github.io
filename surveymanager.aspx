<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="surveymanager.aspx.cs" Inherits="surveymanager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function TypeChange() {
            var typeselector = document.getElementById("ContentPlaceHolder1_TypeSelector");
            var targetselector = document.getElementById("ContentPlaceHolder1_TargetSelector");
            targetselector.innerHTML = "";
            if(typeselector.value == "C"){
                var option = document.createElement('option');
                option.text = "None";
                option.value = "NONE";
                targetselector.add(option, 0);
            }
            else if (typeselector.value == "SC") {
                var option = document.createElement('option');
                option.text = "None";
                option.value = "NONE";
                targetselector.add(option, 0);
                for(var i = 1; i <= 10; i++){
                    option = document.createElement('option');
                    option.text = option.value = i;
                    targetselector.add(option);
                }
            }
            else if (typeselector.value == "YN") {
                var option = document.createElement('option');
                option.text = "None";
                option.value = "NONE";
                targetselector.add(option, 0);
                // Yes option
                option = document.createElement('option');
                option.text = option.value = "Yes";
                targetselector.add(option, 0);
                // No option
                option = document.createElement('option');
                option.text = option.value = "No";
                targetselector.add(option, 0);
            }
            else {

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <hr />
            <h4>Survey Manager</h4>
            <hr />

            <div class="container-fluid">
                <div class="row">
                    <div class="col-xl-3 col-sm-3 col-md-3 col-lg-3" style="border:solid 1px black;border-left:solid 2px black; border-top:solid 2px black;
                        border-radius:5px;">
                        <hr />
                        <h5>Your Surveys</h5>
                        <hr />
                
                        <div class="container-fluid" style="border:solid 2px #0094ff;border-radius:5px;">
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="NewSurveylbl" runat="server" Text="">Click button to create a new survey</asp:Label>
                                    <br />
                                    <asp:Button ID="NewSurveybtn" CssClass="btn btn-primary btn-sm" runat="server" 
                                        Text="New Survey" OnClick="NewSurveybtn_OnClick" Style="margin-bottom:10px;"/>
                                </div>
                            </div>
                        </div>
                        <hr />

                        <asp:Label ID="CreatedSurveyslbl" runat="server" Text="Created Surveys"></asp:Label>
                        <hr />
                        <asp:PlaceHolder ID="SurveysPlaceholder" runat="server"></asp:PlaceHolder>
                        <hr />

                        <asp:Label ID="Label1" runat="server" Text="Active Surveys"></asp:Label>
                        <hr />
                        <asp:PlaceHolder ID="ActiveSurveysPlaceholder" runat="server">
                            <p>There are no active surveys.</p>
                        </asp:PlaceHolder>
                        <hr />
                    </div>

            
                    <div class="col-xl-9 col-sm-9 col-md-9 col-lg-9">
                        <div class="container-fluid" style="border:solid 1px black;border-left:solid 2px black; 
                        border-top:solid 2px black;border-radius:5px;">
                            <%-- Default Placeholder --%>
                            <asp:Placeholder ID="DefaultPlaceholder" runat="server">
                                <hr />
                                <h5>Survey Manager</h5>
                                <hr />
                                <p>The survey manager allows you to create surveys.</p>
                                <p>Click the 'New Survey' button or select a survey to edit.</p>
                                <hr />
                            </asp:Placeholder>
                            <%-- New Survey Placeholder --%>
                            <asp:PlaceHolder ID="NewSurveyPlaceholder" runat="server">
                                <hr />
                                <h5>Create New Survey</h5>
                                <hr />

                                <div class="container">
                                    <div class="row">
                                        <div class="col-6" style="border-radius:5px;">
                                            <asp:Label ID="NewSurveyTitleLbl" runat="server" Text="Survey Title" Style="margin-top:10px;"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="NewSurveytxt" CssClass="form-control-sm input-group-lg" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="NewSurveyLayoutSelectorLbl" runat="server" Text="Select Layout" Style="margin-top: 10px;"></asp:Label>
                                            <br />
                                            <asp:DropDownList ID="NewSurveyLayoutSelector" runat="server" Style="margin:auto">
                                                <asp:ListItem>Dummy Item</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:Button ID="SaveNewSurveyBtn" CssClass="btn btn-primary btn-sm" runat="server" 
                                                Text="Save Survey" OnClick="SaveNewSurveybtn_OnClick" Style="margin-bottom:10px;margin-top:10px;"/>
                                            <asp:Button ID="CancelNewSurveyBtn" CssClass="btn btn-secondary btn-sm" runat="server" 
                                                Text="Cancel" OnClick="Cancelbtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                                margin-left:15px;"/>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                            </asp:PlaceHolder>
                            
                            <%-- Edit Survey Placeholder --%>
                            <asp:PlaceHolder ID="EditSurveyPlaceholder" runat="server">
                                <hr />
                                <h5>Edit Survey</h5>
                                <hr />
                                <asp:Button ID="SaveSurveyBtn1" CssClass="btn btn-primary btn-sm" runat="server" 
                                    Text="Save Survey" OnClick="SaveSurveybtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                    margin-right:15px;"/>
                                <asp:Button ID="SetActiveBtn1" CssClass="btn btn-danger btn-sm" runat="server" 
                                    Text="Activate Survey" OnClick="SetActivebtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                    margin-right:15px;"/>
                                <asp:Button ID="EditCancelBtn1" CssClass="btn btn-secondary btn-sm" runat="server" 
                                    Text="Cancel" OnClick="Cancelbtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                    margin-right:15px;"/>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <div class="container" style="border:solid black 1px;border-radius:5px;padding-top:10px;">
                                            <asp:Label ID="Titlelbl" runat="server" Text="Survey Title: "></asp:Label>
                                            <br />
                                            <asp:Label ID="SelectedLayoutlbl" runat="server" Text="Layout: "></asp:Label>
                                            <br />
                                            <asp:Button ID="EditTitleLayoutBtn" CssClass="btn btn-primary btn-sm" runat="server"
                                                Text="Edit" OnClick="EditTitleLayoutBtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                                margin-right:15px;" />
                                            <br />
                                            <hr />
                                            <h6>Questions</h6>
                                            <hr />
                                            <asp:Placeholder ID="QuestionsPlaceholder" runat="server">
                                                <p>There are currently no questions on this survey. Click add quesiton to create one.</p>
                                        
                                            </asp:Placeholder>
                                            <asp:Button ID="NewQuestionBtn" CssClass="btn btn-primary btn-sm" runat="server"
                                                Text="Add Question" OnClick="NewQuestionBtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                                margin-right:15px;" />
                                            <asp:Button ID="AddNPSQuestionBtn" CssClass ="btn btn-primary btn-sm" runat="server"
                                                Text="Add NPS Question" OnClick="AddNPSQuestion_OnClick" Style="margin-bottom:10px;margin-top:10px;" />
                                            <hr />
                                            <h6>Email Notifications</h6>
                                            <hr />
                                            <asp:Label runat="server" Text="Notifications: "></asp:Label>
                                            <asp:Label ID="EmailNoticationLbl" runat="server" Text="N/A"></asp:Label>
                                            <br />
                                            <asp:Button ID="EnableNotificationsBtn" runat="server" Text="Enable Notifications"
                                                OnClick="EnableNotifications_OnClick" CssClass="btn btn-primary btn-sm" Style="margin-top:10px;" />
                                            <asp:Button ID="DisableNotificationsBtn" runat="server" Text="Disable Notifications"
                                                OnClick="DisableNotifications_OnClick" CssClass="btn btn-primary btn-sm" Style="margin-top:10px;" />
                                            <hr />
                                            <asp:Label ID="TakeSurveyURLlbl" runat="server" Text="Label"></asp:Label>
                                    
                                
                                        </div>
                                    </div>
                                    <asp:PlaceHolder ID="EditTitleLayoutPlaceholder" runat="server" Visible="false">
                                        <div class="col-6" style="border-radius:5px;">
                                            <div class="container" style="border:solid black 1px;border-radius:5px;">
                                                <hr />
                                                <h6>Edit Title and Layout</h6>
                                                <hr />
                                                <asp:Label ID="EditTitleLayoutMessagelbl" runat="server" Visible="False"><br /></asp:Label>
                                        
                                                <asp:Label ID="EditTitlelbl" runat="server" Text="Survey Title" Style="margin-top:10px;"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="Edittitletxt" CssClass="form-control-sm input-group-lg" runat="server"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="EditSelectLayoutlbl" runat="server" Text="Select Layout" Style="margin-top: 10px;"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="EditLayoutSelector" runat="server" Style="margin:auto">
                                                    <asp:ListItem>Dummy Item</asp:ListItem>
                                                </asp:DropDownList>
                                                <hr />
                                                <asp:Button ID="UpdateTitleLayoutBtn" CssClass="btn btn-primary btn-sm" runat="server" 
                                                    Text="Update" OnClick="UpdateTitleLayoutBtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                                    margin-right:15px;"/>
                                                <asp:Button ID="Button2" CssClass="btn btn-secondary btn-sm" runat="server" 
                                                    Text="Cancel" OnClick="CancelEditTitleLayoutBtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                                    margin-right:15px;"/>
                                            </div>
                                        </div>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="QuestionEditorPlaceholder" runat="server" Visible="false">
                                        <div class="col-6" style="border-radius:5px;">
                                            <div class="container" style="border:solid black 1px;border-radius:5px;">
                                                <hr />
                                                <asp:PlaceHolder ID="EditQuestionPlaceholder" runat="server"><h6>Question Editor</h6></asp:PlaceHolder>
                                                <asp:Placeholder ID="NewQuestionPlaceholder" runat="server"><h6>New Question</h6></asp:Placeholder>
                                                <hr />
                                                <asp:Label ID="EditQuestionMessagelbl" runat="server" Text="" Visible="false"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" Text="Question Text"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="Questiontxt" runat="server" TextMode="MultiLine" Style="width:100%;height:100px;"></asp:TextBox>
                                                <br /><br />
                                                <div class="container-fluid">
                                                    <div class="row">
        <%--                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>--%>
                                                                <div class="col-auto">
                                                                    <asp:Label runat="server" Text="Select Type"></asp:Label>
                                                                    <br />
                                                                    <asp:DropDownList ID="TypeSelector" runat="server" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="TypeSelector_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-auto">
                                                                    <asp:Label runat="server" Text="Select Target"></asp:Label>
                                                                    <br />
                                                                    <asp:DropDownList ID="TargetSelector" runat="server"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-auto">
                                                                    <asp:Label runat="server" Text="Show Comment Box"></asp:Label>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="CommentRadioBtns" runat="server">
                                                                        <asp:ListItem runat="server" Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem runat="server" Value="0" Text="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            <%--</ContentTemplate>--%>
                                                            <%--<Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="TypeSelector" EventName="OnSelectedIndexChanges"
                                                            </Triggers>--%>
                                                        <%--</asp:UpdatePanel>--%>
                                                
                                                
                                                    </div>
                                                </div>
                                    
                                                <hr />
                                                <asp:Button ID="AddQuestionBtn" CssClass="btn btn-primary btn-sm" runat="server" 
                                                    Text="Add Question" OnClick="AddQuestionBtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                                    margin-right:15px;"/>
                                                <asp:Button ID="UpdateQuestionBtn" CssClass="btn btn-primary btn-sm" runat="server" 
                                                    Text="Update Question" OnClick="UpdateQuestionBtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                                    margin-right:15px;"/>
                                                <asp:Button ID="CancelEditQuestionBtn" CssClass="btn btn-secondary btn-sm" runat="server" 
                                                    Text="Cancel" OnClick="CancelEditQuestionBtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                                    margin-right:15px;"/>
                                                <hr />
                                            </div>
                                        </div>
                                    </asp:PlaceHolder>
                                </div>
                                <hr />
                                <asp:Button ID="SaveSurveyBtn2" CssClass="btn btn-primary btn-sm" runat="server" 
                                    Text="Save Survey" OnClick="SaveSurveybtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                    margin-right:15px;"/>
                                <asp:Button ID="SetActiveBtn2" CssClass="btn btn-danger btn-sm" runat="server" 
                                    Text="Activate Survey" OnClick="SetActivebtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                    margin-right:15px;"/>
                                <asp:Button ID="EditCancelBtn2" CssClass="btn btn-secondary btn-sm" runat="server" 
                                    Text="Cancel" OnClick="Cancelbtn_OnClick" Style="margin-bottom:10px;margin-top:10px;
                                    margin-right:15px;"/>
                                <hr />
                            </asp:PlaceHolder>
                    
                            <asp:PlaceHolder ID="MessagePlaceholder" runat="server" Visible="false">
                                <asp:Label ID="Messagelbl" runat="server" Style="margin-bottom:10px;"><hr /></asp:Label>
                                <hr />
                            </asp:PlaceHolder>
                        </div>
                
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

