using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelectPdf;
using System.IO;

public partial class takesurvey : System.Web.UI.Page
{
    SQLConnector sqlconnector = null;
    Survey survey;
    Layout layout;
    LinkedList<Answer> answers;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if(sqlconnector == null)
        {
            sqlconnector = new SQLConnector();
        }
        int SurveyID = 0;
        if (Request.QueryString["SurveyID"] != null)
        {
            SurveyID = Convert.ToInt32(Request.QueryString["SurveyID"]);
        }
        if(SurveyID != 0)
        {
            survey = sqlconnector.getSurvey(SurveyID);
            layout = sqlconnector.getLayout(survey.getLayoutID());
        }
        
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(survey != null)
        {
            SurveyPanel.BackColor = ColorTranslator.FromHtml(layout.getBackgroundColor());
            finishedpanel.Attributes.Add("style", "border:2px solid "
                + layout.getBackgroundColor() + ";border-radius:5px;margin-top:10px;margin-bottom:10px;display:none;min-height:280px");
            SurveySubmitedPanel.Attributes.Add("style", "border:2px solid "
                + layout.getBackgroundColor() + ";border-radius:5px;margin-top:10px;margin-bottom:10px;min-height:280px");
            TitleLbl.Text = survey.getTittle();
            byte[] bytes = layout.getLogo().getLogo();
            string strBase64 = Convert.ToBase64String(bytes);
            SurveyLogo.ImageUrl = "data:Image/png;base64," + strBase64;
            if (Session["Message"] != null && Session["Message"].Equals("SurveySubmited"))
            {
                Session["Message"] = null;
                Messagelbl.Visible = true;
                Messagelbl.Text = "Survey Submited Successfully!<br/>Thank you for taking the survey. your responses are greatly appreciated.";
                SubmitBtn.Visible = false;
                SurveySubmitedPanel.Visible = true;
                PrevBtn.Visible = false;
                NextBtn.Visible = false;
            }
            else if (survey.getActive())
            {

                displayQuestions();
            }
            else
            {

            }
        }
        else
        {
            SurveyPanel.BackColor = ColorTranslator.FromHtml("#0094ff");
            TitleLbl.Text = "Personalized Surveys";
            Messagelbl.Visible = true;
            Messagelbl.Text = "There is no survey selected";
        }
    }

    private void displayQuestions()
    {
        int qcount = 0;
        foreach(Question q in survey.getQuestions())
        {
            qcount++;
            QuestionsPlaceholder.Controls.Add(new Literal()
            {
                Text = "<div id='Question" + q.getQuestionNumber() + "' class='container col-auto' style='border:2px solid "
                + layout.getBackgroundColor() + ";border-radius:5px;margin-top:10px;margin-bottom:10px;display:none;min-height:280px'>"
            });
            QuestionsPlaceholder.Controls.Add(new Literal() { Text = "<hr/>" });

            QuestionsPlaceholder.Controls.Add(new Literal() { Text = "Question " + q.getQuestionNumber() + "<hr/>"});
            QuestionsPlaceholder.Controls.Add(new Literal() { Text = q.getQuestionText() + "<br/>"});

            if (q.getType().Equals("YN"))
            {
                RadioButtonList buttonlist = new RadioButtonList();
                buttonlist.ID = "Question" + q.getQuestionNumber() + "Answer";
                buttonlist.RepeatDirection = RepeatDirection.Horizontal;
                buttonlist.Attributes.Add("style", "padding-top:10px");

                ListItem yesbtn = new ListItem()
                {
                    Text = "Yes",
                    Value = "1"
                };
                buttonlist.Items.Add(yesbtn);

                ListItem nobtn = new ListItem()
                {
                    Text = "No",
                    Value = "0"
                };
                buttonlist.Items.Add(nobtn);
                QuestionsPlaceholder.Controls.Add(buttonlist);
                QuestionsPlaceholder.Controls.Add(new Literal() { Text = "<br/>" });
            }
            else if (q.getType().Equals("SC"))
            {
                RadioButtonList buttonlist = new RadioButtonList();
                buttonlist.ID = "Question" + q.getQuestionNumber() + "Answer";
                buttonlist.RepeatDirection = RepeatDirection.Horizontal;
                buttonlist.RepeatLayout = RepeatLayout.Flow;
                buttonlist.TextAlign = TextAlign.Left;

                for (int i = 1; i <= 10; i++)
                {
                    ListItem radiobtn = new ListItem()
                    {
                        Text = i.ToString(),
                        Value = i.ToString()
                    };
                    radiobtn.Attributes.Add("style", "margin-left:10px");
                    buttonlist.Items.Add(radiobtn);
                }
                QuestionsPlaceholder.Controls.Add(buttonlist);
                QuestionsPlaceholder.Controls.Add(new Literal() { Text = "<br/>" });
            }

            if (q.getCommentBox().Equals("1")){
                TextBox tb = new TextBox()
                {
                    ID = "Question" + q.getQuestionNumber() + "CommentBox",
                    TextMode = TextBoxMode.MultiLine,
                    
                };
                tb.Attributes.Add("style", "width:100%");
                QuestionsPlaceholder.Controls.Add(tb);
            }
            QuestionsPlaceholder.Controls.Add(new Literal() { Text = "</div>" });
        }
        
        insertStatusBar(qcount);
    }

    private void insertStatusBar(int numquestions)
    {
        Table statusbar = new Table();
        statusbar.ID = "StatusBar";
        TableRow tr = new TableRow();
        statusbar.Rows.Add(tr);
        
        for (int i = 0; i < numquestions; i++)
        {
            TableCell td = new TableCell();
            td.ID = "Status" + (i + 1);
            tr.Cells.Add(td);
        }
        StatusBarPlaceholder.Controls.Add(statusbar);
    }

    private void sendEmail(SurveyTaken surveyresponse, Survey survey)
    {
        //Build Email Body and Subject
        bool yes_no_hit = false, comment_entered = false, detractor = false;

        string recipient = sqlconnector.getUserEmail(survey.getUserID());

        MailMessage mailMessage = new MailMessage("tyler.paterson@copysystemsinc.com", recipient);
        mailMessage.Subject = "New Survey Response | " + survey.getTittle();
        StringBuilder emailBody = new StringBuilder();

        string emailBodyreasons = "";
        if (detractor)
        {
            mailMessage.Subject += "DETRACTOR | ";
            emailBodyreasons += "Detractor | ";
        }
        if (yes_no_hit)
        {
            mailMessage.Subject += "YES/NO TARGET HIT | ";
            emailBodyreasons += "Yes/No Target Hit | ";
        }
        if (comment_entered)
        {
            mailMessage.Subject += "COMMENT ENTERED | ";
            emailBodyreasons += "Comment Entered";
        }

        //mailMessage.Subject += "Survey response alert for " + survey.getTittle() + ".";
        mailMessage.Body += "<p>See attached for survey response.</p>";
        mailMessage.Body += "<p>You are reieving this because you have notifications enabled. You can disable them by editing the survey.</p>";

        // Build PDF
        SelectPdf.PdfDocument doc = buildPDF(surveyresponse, survey);

        using (MemoryStream memoryStream = new MemoryStream())
        {
            doc.Save(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), survey.getTittle() + "_Response_" + surveyresponse.getSurveyTakenID() + ".pdf"));
        }

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.Credentials = new System.Net.NetworkCredential()
        {
            UserName = "tyler.paterson@copysystemsinc.com",
            Password = "Paty2719"
        };
        mailMessage.IsBodyHtml = true;
        smtpClient.EnableSsl = true;
        smtpClient.Send(mailMessage);
    }

    private SelectPdf.PdfDocument buildPDF(SurveyTaken surveyresponse, Survey survey)
    {
        string s = ""
            + "<h3>Survey Response for " + survey.getTittle() + "</h3>"
            + "<div style = 'float:none;' class='call-info' id='modal-call-info' style='border: solid black 2px;'>";
        
        s += "<table style = 'width:100%;background-color:lightgrey'>";
        s += "<tr>";
        s += "<td style ='width:25%;padding:5px 5px 5px 5px'>Survey Response ID: </td>";
        s += "<td id = 'modal-call-number' style='width:25%;padding:5px 5px 5px 5px'>" + surveyresponse.getSurveyID() + "</td>";

        s += "</div>";
        s += "<table id = 'modal-questions-table'>";

        
        foreach(Question q in survey.getQuestions())
        {
            Answer a = surveyresponse.getAnswer(q.getQuestionID());
            string answer = a.getAnswer();
            s += "<tr><td colspan='3' style='height:5px;background-color:firebrick;'></td></tr>";

            if (q.getType().Equals("SC"))
            {
                if (Convert.ToInt32(a.getAnswer()) > Convert.ToInt32(q.getTarget()))
                {
                    s += "<tr class='modal-question-tr' style='border-top:thick firebrick solid;'>";
                }
                else s += "<tr class='modal-question-tr' style='color:red;border-top:thick firebrick solid;'>";
            }
            else if (q.getType().Equals("YN"))
            {
                if (answer.Equals("1")) { answer = "Yes"; }
                else { answer = "No"; }
                if (!answer.Equals(q.getTarget()))
                {
                    s += "<tr class='modal-question-tr' style='border-top:thick firebrick solid;'>";
                }
                else s += "<tr class='modal-question-tr' style='color:red;border-top:thick firebrick solid;'>";
            }
            else
            {
                s += "<tr class='modal-question-tr' style='border-top:thick firebrick solid;'>";
            }

            s += "<td class='modal-question-td' style='position:static;'>";
            s += "<p>" + q.getQuestionText() + "<p>";
            if (a.getCommentText() != null && !a.getCommentText().Equals(""))
            {
                s += "<p style='margin-left:50px;color:blue;'>" + a.getCommentText() + "</p>";
            }
            s += "</td>";
            s += "<td class='modal-target' style='width:80px;white-space:nowrap;font-weight:bold;font-size: smaller;font-style:italic;'>";
            s += "Target:  " + q.getTarget();
            s += "</td><td class='modal-answer' style='white-space:nowrap;'>";
            if (q.getType().Equals("SC"))
            {
                s += "<p class='answer' style='white-space:nowrap;margin:5px 5px 1px 5px;font-weight:bold;'>" + answer + " of 10</p>";
                s += "<p class='type' style='white-space:nowrap;margin:0px 0px 0px 0px;font-weight:bold;font-size: smaller;font-style:italic;'>"
                    + q.getType() + "</p></td></tr>";
            }
            else
            {
                s += "<p class='answer' style='white-space:nowrap;margin:5px 5px 1px 5px;font-weight:bold;'>" + answer + "</p>";
                s += "<p class='type' style='white-space:nowrap;margin:0px 0px 0px 0px;font-weight:bold;font-size: smaller;font-style:italic;'>"
                    + q.getType() + "</p></td></tr>";
            }
        }
        



        s += "</table>";
        s += "</div>";

        HtmlToPdf converter = new HtmlToPdf();
        SelectPdf.PdfDocument doc = converter.ConvertHtmlString(s);
        return doc;
    }

    protected void SumbitBtn_OnClick(object sender, EventArgs e)
    {
        SurveyTaken takensurvey = new SurveyTaken(survey.getSurveyID());
        foreach (Question q in survey.getQuestions())
        {
            string answer = Request.Form["Question" + q.getQuestionNumber() + "Answer"];
            string comment = Request.Form["Question" + q.getQuestionNumber() + "CommentBox"];
            
            takensurvey.addAnswer(new Answer(q.getQuestionID(), comment, answer));
        }
        if (sqlconnector.createSurveyResponse(takensurvey))
        {
            Session["Message"] = "SurveySubmited";
            if (survey.hasNotifications())
            {
                sendEmail(takensurvey, survey);
            }
            Response.Redirect(Request.Path + "?SurveyID=" + survey.getSurveyID());
        }
        else
        {
            Messagelbl2.Text = "Something went wrong";
            Messagelbl2.Visible = true;
        }
    }

    protected void LinkBtn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("http://localhost:50972/Register.aspx");
    }
}