<%@ Page Language="C#" AutoEventWireup="true" CodeFile="takesurvey.aspx.cs" Inherits="takesurvey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personalized Surveys</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/MySyles2.css" rel="stylesheet" />
    
</head>
<body onresize="centercontent()">
    <form id="form1" class="" runat="server">
        <div class="row">
            <div class="col-auto"></div>
            <div class="container-fluid col-4">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" Width="100%" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="SurveyPanel" CssClass="container-fluid col-12" runat="server" Style="border-radius:10px;padding-top:10px;width:100%;height:100%;">
                    <div class="container-fluid col-auto" style="background-color:white;margin-bottom:10px;border-radius:10px;">
                        <div class="row" style="padding:10px 10px 10px 10px; ">
                            <div class="col-12">
                                <asp:image ID="SurveyLogo" width="100px" runat="server" Style="float:right;"></asp:image>
                                <h3 style="margin-right:30px;float:left;">
                                    <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
                                </h3>
                            </div>
                         </div>
                    </div>
            
                    <div class="col-10 container">
                    
                        </div>
                    <div class="jumbotron-fluid" style="min-height:300px;background-color:white;border-radius:5px;margin-bottom:10px;height:100%">
                        <div class="col-12 container" style="min-height:280px">
                            <div class="row" style="min-height:280px">
                                <div class="col-12" style="min-height:280px">

                                    <%-- Questions Placeholder --%>
                                    <asp:PlaceHolder ID="QuestionsPlaceholder" runat="server"></asp:PlaceHolder>

                                    <%-- Finished Panel --%>
                                    <asp:Panel ID="finishedpanel" runat="server" CssClass="container col-auto"
                                        Style="border:2px solid; border-radius:5px;margin-top:10px;margin-bottom:10px;min-height:280px;display:none;">
                                        <hr />
                                        <h3>Survey Finished</h3>
                                        <hr />
                                        <asp:Label ID="finishedlbl" runat="server" Text="You have finished the survey! Click Submit to send it."></asp:Label>
                                        <asp:Label ID="PersonalizedMessageLbl" runat="server" Text="Thank you for taking the time to perticipate in this survey."></asp:Label>
                                        <hr />
                                        <asp:Button ID="SubmitBtn" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" Style=""
                                            OnClick="SumbitBtn_OnClick"/>
                                    </asp:Panel>
                                    
                                    <%-- Survey Submited Panel --%>
                                    <asp:Panel ID="SurveySubmitedPanel" runat="server" Visible="false" CssClass="container col-auto"
                                        Style="border:2px solid; border-radius:5px;margin-top:10px;margin-bottom:10px;min-height:280px">
                                        <hr />
                                        <h4>Survey Submited</h4>
                                        <hr />
                                        <asp:Label ID="Messagelbl" runat="server" Visible="false"></asp:Label>
                                        <hr />
                                        <asp:Label ID="LinkBtnLbl" runat="server" Text="Build your own personlized surveys here! "></asp:Label>
                                        <br />
                                        <asp:Button ID="LinkBtn" runat="server" Text ="Click Here" CssClass="btn btn-primary btn-sm"
                                            OnClick="LinkBtn_OnClick"/>
                                    </asp:Panel>
                                    
                                </div>
                            </div>
                        </div>
                                                    
                    </div>
                    <div class="jumbotron-fluid" style="background-color:white;border-radius:5px;margin-bottom:10px;">
                        <div class="col-12 container">
                            <div class="row">
                                <div id="statusbar" class="col-12">
                                    <asp:PlaceHolder ID="StatusBarPlaceholder" runat="server"></asp:PlaceHolder>
                                </div>
                                <div class="col-12" style="padding:10px 10px 10px 10px;">
                                    <asp:Button ID="PrevBtn" runat="server" Text="Prev" CssClass="btn btn-secondary btn-sm" Style="margin-left:35%;"
                                        OnClientClick="Prev(1,1); return false;"/>
                                    <asp:Button ID="NextBtn" runat="server" Text="Next" CssClass="btn btn-secondary btn-sm" 
                                        OnClientClick="Next(1,2); return false;"/>
                                    <asp:Button ID="ExpandBtn" runat="server" Text="Expand" CssClass="btn btn-primary btn-sm"
                                        OnClientClick="expandQuestions(); return false;" Style="float:right;"/>
                                    <asp:Button ID="ColapseBtn" runat="server" Text="Colapse" CssClass="btn btn-primary btn-sm"
                                        OnClientClick="colapseQuestions(); return false;" Style="display:none;float:right;"/>
                                    <asp:Label ID="Messagelbl2" runat="server" Visible="false"></asp:Label>
                                    
                                </div>
                            </div>
                        </div>
                                                    
                    </div>
            
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
            </div>
            <div class="col-auto"></div>
        </div>
        
    </form>


</body>
    <script>
        centercontent();
        var backgroundcolor = document.getElementById("SurveyPanel").style.backgroundColor;
        document.getElementById("Question1").style.display = "block";
        document.getElementById("finishedpanel").style.display = "none";

        function Next(current, next) {
            document.getElementById("Question" + current).style.display = "none";
            if (current == next) {
                document.getElementById("finishedpanel").style.display = "block";
            }
            else {
                document.getElementById("Question" + next).style.display = "block";
            }
            if (document.getElementById("Question" + (next + 1).toString()) != null) {
                document.getElementById("NextBtn").onclick = function () { Next(next, next + 1); return false; }
            }
            else {
                document.getElementById("NextBtn").onclick = function () { Next(next, next); return false; }
            }
            document.getElementById("PrevBtn").onclick = function () { Prev(next, current); return false; }
            document.getElementById("Status" + current).style.backgroundColor = backgroundcolor;
        }
        function Prev(current, prev) {
            if (current == prev) {
                document.getElementById("finishedpanel").style.display = "none";
            }
            document.getElementById("Question" + current).style.display = "none";
            document.getElementById("Question" + prev).style.display = "block";
            if (document.getElementById("Question" + (prev - 1).toString()) != null) {
                document.getElementById("PrevBtn").onclick = function () { Prev(prev, prev - 1); return false; }
            }
            document.getElementById("NextBtn").onclick = function () { Next(prev, current); return false; }
            document.getElementById("Status" + prev).style.backgroundColor = "grey";
        }
        function expandQuestions() {
            var isquestion = true;
            var count = 1;
            while (isquestion) {
                var question = document.getElementById("Question" + count);
                if (question != null) {
                    question.style.display = "block";
                    count++;
                }
                else { isquestion = false; }
            }
            document.getElementById("finishedpanel").style.display = "block";
            document.getElementById("ExpandBtn").style.display = "none";
            document.getElementById("ColapseBtn").style.display = "block";
            document.getElementById("NextBtn").style.display = "none";
            document.getElementById("PrevBtn").style.display = "none";
            document.getElementById("statusbar").style.display = "none";
        }
        function colapseQuestions() {
            var isquestion = true;
            var count = 2;
            while (isquestion) {
                var question = document.getElementById("Question" + count);
                if (question != null) {
                    question.style.display = "none";
                    count++;
                }
                else { isquestion = false; }
            }

            document.getElementById("finishedpanel").style.display = "none";
            document.getElementById("ExpandBtn").style.display = "block";
            document.getElementById("ColapseBtn").style.display = "none";
            document.getElementById("NextBtn").style.display = "block";
            document.getElementById("PrevBtn").style.display = "block";
            document.getElementById("statusbar").style.display = "block";
        }
        function centercontent() {
            var height = window.innerHeight;
            var content = document.getElementById("form1");
            var content = document.getElementById("form1");
            if (height > content.offsetHeight) {
                var margintop = (height - content.offsetHeight) / 2;
                content.style.marginTop = margintop + "px";
            }
        }
    </script>
</html>
