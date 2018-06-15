using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;

public partial class surveymanager : System.Web.UI.Page
{
    SQLConnector sqlconnector;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        sqlconnector = (SQLConnector)Session["SQLConnector"];

        if (Session["IsLogedIn"] == null || !(bool)Session["IsLogedIn"])
        {
            Response.Redirect("./Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MessagePlaceholder.Visible = false;
        if(Session["Message"] != null && Session["Message"].Equals("NewSurveySave"))
        {
            Session["Message"] = null;
            Messagelbl.Text = "Survey created successfully!";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("green");
            //LoadControlsVisibility("Message")
            MessagePlaceholder.Visible = true;
            LoadControlsVisibility("");
        }
        else if(Session["Message"] != null && Session["Message"].Equals("surveydeleted"))
        {
            Session["Message"] = null;
            Messagelbl.Text = "Survey Deleted Successfully.";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("green");
            MessagePlaceholder.Visible = true;
            LoadControlsVisibility("");
        }
        else if(Session["Message"] != null && Session["Message"].Equals("Question Added"))
        {
            Session["Message"] = null;
            Messagelbl.Text = "Question Added";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("green");
            MessagePlaceholder.Visible = true;
            LoadControlsVisibility("EditSurvey");
        }
        else if(Session["Message"] != null && Session["Message"].Equals("SurveySaved"))
        {
            Session["Message"] = null;
            Messagelbl.Text = "Survey Saved Successfully";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("green");
            MessagePlaceholder.Visible = true;
            LoadControlsVisibility("");
        }
        else if (Session["Message"] != null && Session["Message"].Equals("SurveySetActive"))
        {
            Session["Message"] = null;
            Messagelbl.Text = "Survey Set Active Successfully. Click on the view survey btn to view the URL used to take it.";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("green");
            MessagePlaceholder.Visible = true;
            LoadControlsVisibility("");
        }
        else
        {
            LoadControlsVisibility("");
            MessagePlaceholder.Visible = false;
        }
        //LoadControlsVisibility("");
        LoadControls();
    }

    private void LoadControlsVisibility(string page)
    {
        NewSurveyPlaceholder.Visible = false;
        DefaultPlaceholder.Visible = false;
        EditSurveyPlaceholder.Visible = false;
        QuestionEditorPlaceholder.Visible = false;
        NewQuestionPlaceholder.Visible = false;
        EditQuestionPlaceholder.Visible = false;
        EditTitleLayoutPlaceholder.Visible = false;
        //MessagePlaceholder.Visible = false;
        switch (page)
        {
            case "NewSurvey":
                NewSurveyPlaceholder.Visible = true;
                break;
            case "EditSurvey":
                EditSurveyPlaceholder.Visible = true;
                break;
            case "NewQuestion":
                EditSurveyPlaceholder.Visible = true;
                QuestionEditorPlaceholder.Visible = true;
                NewQuestionPlaceholder.Visible = true;
                break;
            case "EditQuestion":
                EditSurveyPlaceholder.Visible = true;
                QuestionEditorPlaceholder.Visible = true;
                EditQuestionPlaceholder.Visible = true;
                break;
            case "EditTitleLayout":
                EditSurveyPlaceholder.Visible = true;
                EditTitleLayoutPlaceholder.Visible = true;
                break;
            default:
                DefaultPlaceholder.Visible = true;
                break;
        }
    }

    private void LoadControls()
    {
        displaySurveys();
        if(Session["EditedSurvey"] != null)
        {
            editSurvey((Survey)Session["EditedSurvey"]);
        }
    }

    private void displaySurveys()
    {
        PlaceHolder surveysPH = new PlaceHolder();
        PlaceHolder activesurveysPH = new PlaceHolder();

        LinkedList<Survey> surveys = sqlconnector.getUser().getSurveys();

        bool resetactivesurveys = false;
        bool resetsurveys = false;
        foreach(Survey s in surveys)
        {
            if (s.getActive())
            {
                resetactivesurveys = true;
            }
            else
            {
                resetsurveys = true;
            }
        }
        if (resetactivesurveys)
        {
            ActiveSurveysPlaceholder.Controls.Clear();
        }
        else
        {
            ActiveSurveysPlaceholder.Controls.Clear();
            ActiveSurveysPlaceholder.Controls.Add(new Literal() { Text = "There are no active surveys." });
        }
        if (resetsurveys)
        {
            SurveysPlaceholder.Controls.Clear();
        }
        else
        {
            SurveysPlaceholder.Controls.Clear();
            SurveysPlaceholder.Controls.Add(new Literal() { Text = "There are no surveys. Create a new one by clicking the New Survey button above" });
        }

        foreach (Survey s in surveys)
        {
            bool isactive = s.getActive();
            PlaceHolder tempPH = new PlaceHolder();
            tempPH.Controls.Add(new Literal { Text = "<div class='container - fluid' style='border:solid 2px "
                + "#0094ff;border-radius:5px;padding-top:5px;padding-bottom:10px;margin-top:10px;margin-bottom:10px;'>" });
            tempPH.Controls.Add(new Literal { Text = "<div class='row'>" });
            tempPH.Controls.Add(new Literal { Text = "<div class='col-12'" });

            Label l = new Label();
            l.Text = s.getTittle();
            tempPH.Controls.Add(l);

            tempPH.Controls.Add(new Literal { Text = "<br />" });
            
            // Delete Button
            Button deletebtn = new Button();
            deletebtn.ID = "DeleteBtn" + s.getSurveyID();
            deletebtn.Click += new EventHandler(DeleteSurveybtn_OnClick);
            deletebtn.CommandArgument = s.getSurveyID().ToString();
            deletebtn.Text = "Delete";
            deletebtn.CssClass = "btn btn-danger btn-sm btn-edit";
            tempPH.Controls.Add(deletebtn);

            if (!isactive)
            {
                // Edit Button
                Button editbtn = new Button();
                editbtn.ID = "EditBtn" + s.getSurveyID();
                editbtn.Click += new EventHandler(EditSurveybtn_OnClick);
                editbtn.CommandArgument = s.getSurveyID().ToString();
                editbtn.Text = "Edit";
                editbtn.CssClass = "btn btn-primary btn-sm";
                tempPH.Controls.Add(editbtn);
            }
            else
            {
                // Edit Button
                Button viewbtn = new Button();
                viewbtn.ID = "ViewBtn" + s.getSurveyID();
                viewbtn.Click += new EventHandler(ViewSurveybtn_OnClick);
                viewbtn.CommandArgument = s.getSurveyID().ToString();
                viewbtn.Text = "View";
                viewbtn.CssClass = "btn btn-primary btn-sm";
                tempPH.Controls.Add(viewbtn);
            }
            
            tempPH.Controls.Add(new Literal { Text = "</div></div></div>" });

            if (!isactive)
            {
                SurveysPlaceholder.Controls.Add(tempPH);
            }
            else
            {
                ActiveSurveysPlaceholder.Controls.Add(tempPH);
            }
        }
    }

    private void editSurvey(Survey s)
    {
        Titlelbl.Text = "Survey Title: " + s.getTittle();
        LinkedList<Layout> layouts = sqlconnector.getUser().getLayouts();
        foreach (Layout l in layouts)
        {
            if(l.getLayoutID() == s.getLayoutID())
            {
                SelectedLayoutlbl.Text = "Layout: " + l.getLayoutName();
            }
        }

        displayQuestions(s);

        if (s.getActive())
        {
            SetActiveBtn1.Visible = false;
            SetActiveBtn2.Visible = false;
            NewQuestionBtn.Visible = false;
            TakeSurveyURLlbl.Visible = true;
            TakeSurveyURLlbl.Text = "Take Survey URL: localhost:50972/takesurvey.aspx?SurveyID=" + s.getSurveyID() + "<hr/>";
        }
        else
        {
            SetActiveBtn1.Visible = true;
            SetActiveBtn2.Visible = true;
            AddQuestionBtn.Visible = true;
            TakeSurveyURLlbl.Visible = false;
        }

        if (s.hasNotifications())
        {
            EmailNoticationLbl.Text = "Enabled";
            EnableNotificationsBtn.Visible = false;
            DisableNotificationsBtn.Visible = true;
        }
        else
        {
            EmailNoticationLbl.Text = "Disabled";
            EnableNotificationsBtn.Visible = true;
            DisableNotificationsBtn.Visible = false;
        }

        LoadControlsVisibility("EditSurvey");
    }

    private void displayQuestions(Survey s)
    {
        LinkedList<Question> questions = s.getQuestions();
        QuestionsPlaceholder.Controls.Clear();
        
        if (questions.Count > 0)
        {
            bool hasNPS = false;
            int number = 1;
            foreach (Question q in questions)
            {
                QuestionsPlaceholder.Controls.Add(new Literal { Text = "<div id='" + q.getQuestionNumber() + "' class='' style='border:solid 1px #0094ff;'>" });

                QuestionsPlaceholder.Controls.Add(new Literal { Text = "<div style='background-color:#0094ff;color:white;padding-left:10px;"
                    + "'>Question Numer: " + q.getQuestionNumber() });

                if (!s.getActive())
                {
                    Button editbtn = new Button();
                    editbtn.ID = "EditQuestionBtn" + number;
                    editbtn.Click += new EventHandler(EditQuestionBtn_OnClick);
                    editbtn.CommandArgument = q.getQuestionNumber().ToString();
                    editbtn.Text = "Edit";
                    editbtn.CssClass = "btn btn-primary btn-sm";
                    editbtn.Attributes.Add("style", "margin-left:10px;margin-top:5px;margin-bottom:5px");
                    QuestionsPlaceholder.Controls.Add(editbtn);

                    Button deletebtn = new Button();
                    deletebtn.ID = "DeleteQuestionBtn" + number;
                    deletebtn.Click += new EventHandler(DeleteQuestionBtn_OnClick);
                    deletebtn.CommandArgument = q.getQuestionNumber().ToString();
                    deletebtn.Text = "Delete";
                    deletebtn.CssClass = "btn btn-danger btn-sm";
                    deletebtn.Attributes.Add("style", "margin-left:10px;margin-top:5px;margin-bottom:5px");
                    QuestionsPlaceholder.Controls.Add(deletebtn);

                    if (q.isNPS())
                    {
                        editbtn.Enabled = false;
                        hasNPS = true;
                        Label l = new Label();
                        l.Text = "NPS Question";
                        l.ForeColor = ColorTranslator.FromHtml("white");
                        QuestionsPlaceholder.Controls.Add(l);
                    }
                }

                QuestionsPlaceholder.Controls.Add(new Literal { Text = "</div>" });
                QuestionsPlaceholder.Controls.Add(new Literal { Text = "<div style='padding:5px 10px 5px 10px;'>" });
                QuestionsPlaceholder.Controls.Add(new Literal { Text = "Question Text:<br />" });
                QuestionsPlaceholder.Controls.Add(new Literal { Text = "<div style='border:1px black solid;border-radius:3px;padding:5px 5px 5px 5px;'>" + q.getQuestionText() + "</div>"});
                switch (q.getType())
                {
                    case "C":
                        QuestionsPlaceholder.Controls.Add(new Literal { Text = "Type: Comment<br/>" });
                        break;
                    case "SC":
                        QuestionsPlaceholder.Controls.Add(new Literal { Text = "Type: Scale 1-10<br/>" });
                        break;
                    case "YN":
                        QuestionsPlaceholder.Controls.Add(new Literal { Text = "Type: Yes/No<br/>" });
                        break;
                    default:
                        QuestionsPlaceholder.Controls.Add(new Literal { Text = "Type: No Selection<br/>" });
                        break;
                }
                QuestionsPlaceholder.Controls.Add(new Literal { Text = "Target: " + q.getTarget() + "<br/>" });
                switch (q.getCommentBox())
                {
                    case "1":
                        QuestionsPlaceholder.Controls.Add(new Literal { Text = "Show Comment Box: Yes<br/>" });
                        break;
                    case "0":
                        QuestionsPlaceholder.Controls.Add(new Literal { Text = "Show Comment Box: No<br/>" });
                        break;
                }
                QuestionsPlaceholder.Controls.Add(new Literal { Text = "</div>" });
                QuestionsPlaceholder.Controls.Add(new Literal { Text = "</div>" });
                number++;
            }
            if (hasNPS)
            {
                AddNPSQuestionBtn.Visible = false;
            }
            else { AddNPSQuestionBtn.Visible = true; }
        }
        else
        {
            QuestionsPlaceholder.Controls.Add(new Label() { Text = "There are currently no questions on this survey. Click 'Add Question' to create one.<br/>" });
            AddNPSQuestionBtn.Visible = true;
        }
    }

    protected void NewSurveybtn_OnClick(object sender, EventArgs e)
    {
        NewSurveyLayoutSelector.Items.Clear();
        NewSurveytxt.Text = "";
        LinkedList<Layout> layouts = sqlconnector.getUser().getLayouts();
        foreach(Layout l in layouts)
        {
            ListItem item = new ListItem()
            {
                Text = l.getLayoutName(),
                Value = l.getLayoutID().ToString()
            };
            NewSurveyLayoutSelector.Items.Add(item);
        }
        LoadControlsVisibility("NewSurvey");
    }

    protected void SaveNewSurveybtn_OnClick(object sender, EventArgs e)
    {
        string title = NewSurveytxt.Text;
        int layoutID = Convert.ToInt32(NewSurveyLayoutSelector.SelectedValue);
        if (!title.Equals(""))
        {
            if (sqlconnector.createSurvey(layoutID, title))
            {
                sqlconnector.loadSurveys();
                Session["Message"] = "NewSurveySave";
                Response.Redirect(Request.Path);
            }
            else
            {
                Messagelbl.Text = "Something Went Wrong";
                Messagelbl.ForeColor = ColorTranslator.FromHtml("red");
                MessagePlaceholder.Visible = true;
            }
        }
        else
        {
            Messagelbl.Text = "Insert a Title for the survey.";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("red");
            LoadControlsVisibility("NewSurvey");
            MessagePlaceholder.Visible = true;
        }
        
    }

    protected void DeleteSurveybtn_OnClick(object sender, EventArgs e)
    {
        Button deletebtn = (Button)sender;

        if (sqlconnector.deleteSurvey(sqlconnector.getUser().getSurvey(Convert.ToInt32(deletebtn.CommandArgument))))
        {
            Session["message"] = "surveydeleted";
            Response.Redirect(Request.Path);
        }
        else
        {
            Messagelbl.Text = "Failed to delete survey.";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("red");
            MessagePlaceholder.Visible = true; ;
        }
    }

    protected void EditSurveybtn_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int SurveyID = Convert.ToInt32(btn.CommandArgument);
        Survey s = sqlconnector.getUser().getSurvey(SurveyID);
        Session["EditedSurvey"] = new Survey(s.getSurveyID(), s.getLayoutID(), s.getUserID(), s.getTittle(), s.getActive(), s.getQuestions());
        editSurvey(s);
    }

    protected void SaveSurveybtn_OnClick(object sender, EventArgs e)
    {
        Survey s = (Survey)Session["EditedSurvey"];
        LinkedList<Question> deletedquestions = (LinkedList<Question>)Session["DeletedQuestions"];

        if (sqlconnector.saveSurvey(s) && sqlconnector.deleteQuestions(deletedquestions))
        {
            Session["EditedSurvey"] = null;
            Session["Message"] = "SurveySaved";
            sqlconnector.loadSurveys();
            Session["SQLConnector"] = sqlconnector;
            Response.Redirect(Request.Path);
        }
        else
        {
            Messagelbl.Text = "Something went wrong";
            Messagelbl.Visible = true;
        }
    }

    protected void EditTitleLayoutBtn_OnClick(object sender, EventArgs e)
    {
        Survey s = (Survey)Session["EditedSurvey"];
        EditTitleLayoutMessagelbl.Visible = false;
        if(s != null)
        {
            LinkedList<Layout> layouts = sqlconnector.getUser().getLayouts();
            EditLayoutSelector.Items.Clear();
            foreach (Layout l in layouts)
            {
                ListItem item = new ListItem()
                {
                    Text = l.getLayoutName(),
                    Value = l.getLayoutID().ToString()
                };
                EditLayoutSelector.Items.Add(item);
            }
            Edittitletxt.Text = s.getTittle();
            EditLayoutSelector.SelectedValue = s.getLayoutID().ToString();
            displayQuestions(s);
            LoadControlsVisibility("EditTitleLayout");
        }
        
    }

    protected void UpdateTitleLayoutBtn_OnClick(object sender, EventArgs e)
    {
        if (!Edittitletxt.Text.Equals(""))
        {
            Survey s = (Survey)Session["EditedSurvey"];
            s.setTitle(Edittitletxt.Text);
            s.setLayoutID(Convert.ToInt32(EditLayoutSelector.SelectedValue));
            Response.Redirect(Request.Path);
            //LoadControlsVisibility("EditSurvey");
        }
        else
        {
            EditTitleLayoutMessagelbl.Text = "No title entered<br/>";
            EditTitleLayoutMessagelbl.ForeColor = ColorTranslator.FromHtml("red");
            EditTitleLayoutMessagelbl.Visible = true;
            LoadControlsVisibility("EditTitleLayout");
        }

    }

    protected void NewQuestionBtn_OnClick(object sender, EventArgs e)
    {
        AddQuestionBtn.Visible = true;
        UpdateQuestionBtn.Visible = false;
        AddQuestionBtn.CommandArgument = "0";
        Questiontxt.Text = "";
        TypeSelector.Items.Clear();
        TypeSelector.Items.Add(new ListItem()
        {
            Text = "Select Type",
            Value = "NONE"
        });
        foreach (SurveyType st in sqlconnector.getTypes())
        {
            ListItem item = new ListItem()
            {
                Text = st.getName(),
                Value = st.getCode()
            };
            TypeSelector.Items.Add(item);
        }

        TargetSelector.Items.Clear();
        TargetSelector.Items.Add(new ListItem()
        {
            Text = "None",
            Value = "NONE"
        });
        displayQuestions((Survey)Session["EditedSurvey"]);
        LoadControlsVisibility("NewQuestion");
    }

    protected void EditQuestionBtn_OnClick(object sender, EventArgs e)
    {
        Button editbtn = (Button)sender;
        int number = Convert.ToInt32(editbtn.CommandArgument);
        Survey s = (Survey)Session["EditedSurvey"];
        Question q = s.getQuestion(number);

        if (q != null)
        {
            UpdateQuestionBtn.CommandArgument = q.getQuestionID().ToString();
            Questiontxt.Text = q.getQuestionText();
            TypeSelector.Items.Clear();
            string selectedtype = q.getType();
            TypeSelector.Items.Add(new ListItem()
            {
                Text = "Select Type",
                Value = "NONE"
            });
            foreach (SurveyType st in sqlconnector.getTypes())
            {
                ListItem item = new ListItem()
                {
                    Text = st.getName(),
                    Value = st.getCode()
                };
                TypeSelector.Items.Add(item);
                if (selectedtype.Equals(st.getCode()))
                {
                    item.Selected = true;
                }
            }
            
            TargetSelector.Items.Clear();
            if (selectedtype.Equals("SC"))
            {
                TargetSelector.Items.Add(new ListItem()
                {
                    Text = "None",
                    Value = "NONE"
                });
                for (int i = 1; i <= 10; i++)
                {
                    TargetSelector.Items.Add(new ListItem()
                    {
                        Text = i.ToString(),
                        Value = i.ToString()
                    });
                }

            }
            else if (selectedtype.Equals("YN"))
            {
                TargetSelector.Items.Add(new ListItem()
                {
                    Text = "None",
                    Value = "NONE"
                });
                TargetSelector.Items.Add(new ListItem()
                {
                    Text = "Yes",
                    Value = "Yes"
                });
                TargetSelector.Items.Add(new ListItem()
                {
                    Text = "No",
                    Value = "No"
                });
            }
            else
            {
                TargetSelector.Items.Add(new ListItem()
                {
                    Text = "None",
                    Value = "NONE"
                });
            }

            TargetSelector.SelectedValue = q.getTarget();

            CommentRadioBtns.SelectedValue = q.getCommentBox();

            AddQuestionBtn.Visible = false;
            UpdateQuestionBtn.Visible = true;
            UpdateQuestionBtn.CommandArgument = number.ToString();

            displayQuestions((Survey)Session["EditedSurvey"]);
            LoadControlsVisibility("EditQuestion");
        }
        
    }

    protected void DeleteQuestionBtn_OnClick(object sender, EventArgs e)
    {
        if(Session["DeletedQuestions"] == null)
        {
            Session["DeletedQuestions"] = new LinkedList<Question>();
        }
        Button btn = (Button)sender;
        LinkedList<Question> delquestions = (LinkedList<Question>)Session["DeletedQuestions"];
        Survey s = (Survey)Session["EditedSurvey"];
        Question q = s.removeQuestion(Convert.ToInt32(btn.CommandArgument));
        if(q != null)
        {
            delquestions.AddLast(q);
        }
        editSurvey(s);
    }

    protected void TypeSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        TargetSelector.Items.Clear();
        if (TypeSelector.SelectedValue.Equals("C"))
        {
            TargetSelector.Items.Add(new ListItem()
            {
                Text = "None",
                Value = "NONE"
            });
        }
        else if (TypeSelector.SelectedValue.Equals("SC"))
        {
            TargetSelector.Items.Add(new ListItem()
            {
                Text = "None",
                Value = "NONE"
            });
            for(int i = 1; i <= 10; i++)
            {
                TargetSelector.Items.Add(new ListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

        }
        else if (TypeSelector.SelectedValue.Equals("YN"))
        {
            TargetSelector.Items.Add(new ListItem()
            {
                Text = "None",
                Value = "NONE"
            });
            TargetSelector.Items.Add(new ListItem()
            {
                Text = "Yes",
                Value = "Yes"
            });
            TargetSelector.Items.Add(new ListItem()
            {
                Text = "No",
                Value = "No"
            });
        }
        else
        {
            TargetSelector.Items.Add(new ListItem()
            {
                Text = "None",
                Value = "NONE"
            });
        }
        LoadControlsVisibility("NewQuestion");
    }

    protected void AddQuestionBtn_OnClick(object sender, EventArgs e)
    {
        if (!Questiontxt.Text.Equals(""))
        {
            Button btn = (Button)sender;
            int questionid = Convert.ToInt32(btn.CommandArgument);
            string text = Questiontxt.Text;
            string typecode = TypeSelector.SelectedValue;
            string target = TargetSelector.SelectedValue;
            string commentbox = CommentRadioBtns.SelectedValue;
            if (typecode.Equals("C"))
            {
                commentbox = "1";
            }
            Survey s = (Survey)Session["EditedSurvey"];

            if (s != null)
            {
                int number = s.getQuestions().Count() + 1;
                s.addQuestion(questionid, text, typecode, target, number, commentbox);
                displayQuestions(s);
                LoadControlsVisibility("EditSurvey");
            }

        }
        else
        {
            LoadControlsVisibility("NewQuestion");
            EditQuestionMessagelbl.Text = "No question text entered";
            EditQuestionMessagelbl.ForeColor = ColorTranslator.FromHtml("red");
            EditQuestionMessagelbl.Visible = true;
        }
    }

    protected void UpdateQuestionBtn_OnClick(object sender,EventArgs e)
    {
        if (!Questiontxt.Text.Equals(""))
        {
            Button btn = (Button)sender;
            int number = Convert.ToInt32(btn.CommandArgument);
            string text = Questiontxt.Text;
            string typecode = TypeSelector.SelectedValue;
            string target = TargetSelector.SelectedValue;
            string commentbox = CommentRadioBtns.SelectedValue;
            if (typecode.Equals("C"))
            {
                commentbox = "1";
            }
            Survey s = (Survey)Session["EditedSurvey"];

            if(s != null)
            {
                Question q = s.getQuestion(number);
                if(q != null)
                {
                    q.setText(text);
                    q.setType(typecode);
                    q.setTarget(target);
                    q.setCommentBox(commentbox);

                    Session["Message"] = "Question Updated";
                    Response.Redirect(Request.Path);
                }
                else
                {
                    LoadControlsVisibility("NewQuestion");
                    EditQuestionMessagelbl.Text = "A problem occured";
                    EditQuestionMessagelbl.ForeColor = ColorTranslator.FromHtml("red");
                    EditQuestionMessagelbl.Visible = true;
                }
            }
                        
        }
        else
        {
            LoadControlsVisibility("NewQuestion");
            EditQuestionMessagelbl.Text = "No question text entered";
            EditQuestionMessagelbl.ForeColor = ColorTranslator.FromHtml("red");
            EditQuestionMessagelbl.Visible = true;
        }

    }

    protected void SetActivebtn_OnClick(object sender, EventArgs e)
    {
        Survey s = (Survey)Session["EditedSurvey"];
        LinkedList<Question> deletedquestions = (LinkedList<Question>)Session["DeletedQuestions"];
        s.setActive(true);

        if (sqlconnector.saveSurvey(s) && sqlconnector.deleteQuestions(deletedquestions))
        {
            Session["EditedSurvey"] = null;
            Session["Message"] = "SurveySetActive";
            sqlconnector.loadSurveys();
            Session["SQLConnector"] = sqlconnector;
            Response.Redirect(Request.Path);
        }
        else
        {
            Messagelbl.Text = "Something went wrong";
            Messagelbl.Visible = true;
        }
    }

    protected void ViewSurveybtn_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int SurveyID = Convert.ToInt32(btn.CommandArgument);
        Survey s = sqlconnector.getUser().getSurvey(SurveyID);
        Session["EditedSurvey"] = s;
        editSurvey(s);
        //viewSurvey(s);
    }

    protected void Cancelbtn_OnClick(object sender, EventArgs e)
    {
        Session["DeletedQuestions"] = null;
        Session["EditedSurvey"] = null;
        sqlconnector.loadSurveys();
        Response.Redirect(Request.Path);
    }

    protected void CancelEditQuestionBtn_OnClick(object sender, EventArgs e)
    {
        LoadControlsVisibility("EditSurvey");
    }

    protected void CancelEditTitleLayoutBtn_OnClick(object sender, EventArgs e)
    {
        LoadControlsVisibility("EditSurvey");
    }

    protected void EnableNotifications_OnClick(object sender, EventArgs e)
    {
        Survey s = (Survey)Session["EditedSurvey"];
        s.setNotifications(true);
        EmailNoticationLbl.Text = "Enabled";
        EnableNotificationsBtn.Visible = false;
        DisableNotificationsBtn.Visible = true;
    }

    protected void DisableNotifications_OnClick(object sender, EventArgs e)
    {
        Survey s = (Survey)Session["EditedSurvey"];
        s.setNotifications(false);
        EmailNoticationLbl.Text = "Disabled";
        EnableNotificationsBtn.Visible = true;
        DisableNotificationsBtn.Visible = false;
    }

    protected void AddNPSQuestion_OnClick(object sender, EventArgs e)
    {
        string text = "How likely are you to recommend " + sqlconnector.getUser().getCompanyName() + " to others?";
        Survey s = (Survey)Session["EditedSurvey"];
        int number = s.getQuestions().Count + 1;
        Question npsquestion = new Question(0, text, "SC", "6", number, "0", true);
        s.addQuestion(npsquestion);
        displayQuestions(s);
    }
}