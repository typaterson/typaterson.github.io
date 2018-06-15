using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;

/// <summary>
/// SQL connector that contains all queries for the application
/// </summary>
public class SQLConnector
{
    private User user;
    private string constring;
    private SqlConnection con;

    public SQLConnector()
    {
        constring = ConfigurationManager.ConnectionStrings["PersonalizedSurveysConnectionString"].ConnectionString;
        con = new SqlConnection(constring);
    }

    // ** Login/Register **
    
    // Login User
    public bool loginUser(string username, string password)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp:LoginUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Username parameter
            SqlParameter paramUsername = new SqlParameter()
            {
                ParameterName = "@Username",
                Value = username
            };
            cmd.Parameters.Add(paramUsername);

            con.Open();
            string result = (string)cmd.ExecuteScalar();
            con.Close();

            if (result.Equals(password))
            {
                cmd.CommandText = "spSelectUser";

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            int UserID = 0;
                            string email = "", firstname = "", lastname = "", company = "";
                            foreach (DataColumn col in dt.Columns)
                            {

                                switch (col.ColumnName.ToString())
                                {
                                    case "UserID":
                                        UserID = Convert.ToInt32(row[col.ColumnName].ToString());
                                        break;

                                    case "Email":
                                        email = row[col.ColumnName].ToString();
                                        break;

                                    case "First_Name":
                                        firstname = row[col.ColumnName].ToString();
                                        break;

                                    case "Last_Name":
                                        lastname = row[col.ColumnName].ToString();
                                        break;

                                    case "Company_Name":
                                        company = row[col.ColumnName].ToString();
                                        break;
                                }
                            }
                            user = new User(UserID, email, firstname, lastname, company, username);
                            loadLayouts();
                            loadSurveys();
                        }
                    }
                }
                return true;
            }
            else { return false; }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // Register User
    public bool registerUser(string company, string firstname, string lastname, string email, 
        string username, string password)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spRegisterUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Username parameter
            SqlParameter paramUsername = new SqlParameter()
            {
                ParameterName = "@Username",
                Value = username
            };
            cmd.Parameters.Add(paramUsername);

            SqlParameter paramPassword = new SqlParameter()
            {
                ParameterName = "@Password",
                Value = password
            };
            cmd.Parameters.Add(paramPassword);

            SqlParameter paramEmail = new SqlParameter()
            {
                ParameterName = "@Email",
                Value = email
            };
            cmd.Parameters.Add(paramEmail);

            SqlParameter paramFirstName = new SqlParameter()
            {
                ParameterName = "@First_Name",
                Value = firstname
            };
            cmd.Parameters.Add(paramFirstName);

            SqlParameter paramLastName = new SqlParameter()
            {
                ParameterName = "@Last_Name",
                Value = lastname
            };
            cmd.Parameters.Add(paramLastName);

            //CompanyName parameter
            SqlParameter paramCompanyName = new SqlParameter()
            {
                ParameterName = "@Company_Name",
                Value = company
            };
            cmd.Parameters.Add(paramCompanyName);

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = -1,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(paramUserID);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result == 1) {
                return true;
            }
            else {
                return false;
            }

    }
        catch (Exception)
        {
            con.Close();
            return false;
        }

    }


    // Logout User
    public bool logoutUser()
    {
        user = null;
        return true;
    }

    // Check Username is not taken
    public bool checkUsername(string username)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spCheckUsername", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUsername = new SqlParameter()
            {
                ParameterName = "@Username",
                Value = username
            };
            cmd.Parameters.Add(paramUsername);

            con.Open();
            int result = (int)cmd.ExecuteScalar();
            con.Close();

            if(result == 0) { return true; }
            else { return false; }

        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // *** Surveys ***

    // Creates a new survey and sends back the result
    // return NULL on failure
    public bool createSurvey(int LayoutID, string Tittle)
    {
        int SurveyID;
        try
        {
            SqlCommand cmd = new SqlCommand("spCreateSurvey", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //Layout ID parameter
            SqlParameter paramLayoutID = new SqlParameter()
            {
                ParameterName = "@LayoutId",
                Value = LayoutID
            };
            cmd.Parameters.Add(paramLayoutID);

            //User ID Parameter
            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = user.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            //Tittle Parameter
            SqlParameter paramTitle = new SqlParameter()
            {
                ParameterName = "@Tittle",
                Value = Tittle
            };
            cmd.Parameters.Add(paramTitle);

            SqlParameter paramSurveyID = new SqlParameter()
            {
                ParameterName = "@SurveyID",
                Value = -1,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(paramSurveyID);

            //Open Connection and run Stored Procedure
            con.Open();
            SurveyID = cmd.ExecuteNonQuery();
            con.Close();
            if(SurveyID > 0)
            {
                return true;
            }
        }
        catch (IOException)
        {
            con.Close();
            return false;
        }
        return false;
    }

    public bool loadSurveys()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spGetSurveys", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = user.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            con.Open();
            using(SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    user.newSurveyList();
                    foreach(DataRow row in dt.Rows)
                    {
                        int LayoutID = 0, SurveyID = 0, UserID = 0;
                        string Tittle = "";
                        bool active = false, notifications = false;
                        foreach(DataColumn col in dt.Columns)
                        {
                            switch (col.ColumnName.ToString())
                            {
                                case "SurveyID":
                                    SurveyID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "LayoutID":
                                    LayoutID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "UserID":
                                    UserID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "Tittle":
                                    Tittle = row[col.ColumnName].ToString();
                                    break;

                                case "Active":
                                    active = Convert.ToBoolean(row[col.ColumnName].ToString());
                                    break;

                                case "Notifications":
                                    notifications = Convert.ToBoolean(row[col.ColumnName].ToString());
                                    break;
                            }
                        }
                        Survey s = new Survey(SurveyID, LayoutID, UserID, Tittle, active, notifications);
                        s = getSurveyQuestions(s);
                        user.addSurvey(s);
                        
                    }

                }
            }
            con.Close();
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // Get survey based on Survey ID
    // Return null on failure
    public Survey getSurvey(int SurveyID)
    {
        try
        {
            Survey s = null;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("spGetSurvey", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Survey ID Parameter
                SqlParameter paramSurveyID = new SqlParameter()
                {
                    ParameterName = "@SurveyID",
                    Value = SurveyID
                };
                cmd.Parameters.Add(paramSurveyID);

                //Open Connection and run Stored Procedure
                con.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            int LayoutID = 1;
                            int UserID = 0;
                            string Tittle = "";
                            bool Active = false, notifications = false;
                            foreach (DataColumn col in dt.Columns)
                            {
                                switch (col.ColumnName.ToString())
                                {
                                    case "SurveyID":
                                        if(SurveyID != Convert.ToInt32(row[col.ColumnName].ToString()))
                                        {
                                            return null;
                                        }
                                        break;

                                    case "LayoutID":
                                        LayoutID = Convert.ToInt32(row[col.ColumnName].ToString());
                                        break;

                                    case "UserID":
                                        UserID = Convert.ToInt32(row[col.ColumnName].ToString());
                                        break;

                                    case "Tittle":
                                        Tittle = row[col.ColumnName].ToString();
                                        break;

                                    case "Active":
                                        Active = Convert.ToBoolean(row[col.ColumnName].ToString());
                                        break;

                                    case "Notifications":
                                        notifications = Convert.ToBoolean(row[col.ColumnName].ToString());
                                        break;
                                }
                            }
                            s = new Survey(SurveyID, LayoutID, UserID, Tittle, Active, notifications);
                        }
                    }
                }
            }
            if (s != null) { return getSurveyQuestions(s); }
        }
        catch (IOException)
        {
            con.Close();
            return null;
        }
        return null;
        
    }

    //Save Survey
    public bool saveSurvey(Survey s)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spUpdateSurvey", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSurveyID = new SqlParameter()
            {
                ParameterName = "@SurveyID",
                Value = s.getSurveyID()
            };
            cmd.Parameters.Add(paramSurveyID);

            SqlParameter paramLayoutID = new SqlParameter()
            {
                ParameterName = "@LayoutID",
                Value = s.getLayoutID()
            };
            cmd.Parameters.Add(paramLayoutID);

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = s.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            SqlParameter paramTittle = new SqlParameter()
            {
                ParameterName = "@Tittle",
                Value = s.getTittle()
            };
            cmd.Parameters.Add(paramTittle);

            SqlParameter paramActive = new SqlParameter()
            {
                ParameterName = "@Active",
                Value = s.getActive()
            };
            cmd.Parameters.Add(paramActive);

            SqlParameter paramNotifications = new SqlParameter()
            {
                ParameterName = "@Notifications",
                Value = s.hasNotifications()
            };
            cmd.Parameters.Add(paramNotifications);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if(result == 1)
            {
                return saveQuestions(s);
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    //Delete Survey
    public bool deleteSurvey(Survey s)
    {
        try
        {
            getSurveyResponses(s.getSurveyID());
            if(s.getSurveysTaken() != null) { deleteSurveyResponses(s.getSurveyID()); }
            if (deleteQuestions(s.getQuestions()))
            {
                SqlCommand cmd = new SqlCommand("spDeleteSurvey", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSureyID = new SqlParameter()
                {
                    ParameterName = "@SurveyID",
                    Value = s.getSurveyID()
                };
                cmd.Parameters.Add(paramSureyID);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();

                if(result > 0)
                {
                    loadSurveys();
                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // ** Questions **

    // Save and Add Questions
    private bool saveQuestions(Survey s)
    {
        try
        {
            foreach(Question q in s.getQuestions())
            {
                SqlCommand cmd;
                if(q.getQuestionID() != 0)
                {
                    cmd = new SqlCommand("spUpdateQuestion", con);

                    SqlParameter paramQuestionID = new SqlParameter()
                    {
                        ParameterName = "@QuestionID",
                        Value = q.getQuestionID()
                    };
                    cmd.Parameters.Add(paramQuestionID);
                }
                else
                {
                    cmd = new SqlCommand("spCreateQuestion", con);

                    SqlParameter paramSurveyID = new SqlParameter()
                    {
                        ParameterName = "@SurveyID",
                        Value = s.getSurveyID()
                    };
                    cmd.Parameters.Add(paramSurveyID);
                }
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramQuestionNumber = new SqlParameter()
                {
                    ParameterName = "@Number",
                    Value = q.getQuestionNumber()
                };
                cmd.Parameters.Add(paramQuestionNumber);

                SqlParameter paramQuestionText = new SqlParameter()
                {
                    ParameterName = "@Text",
                    Value = q.getQuestionText()
                };
                cmd.Parameters.Add(paramQuestionText);

                SqlParameter paramQuestionType = new SqlParameter()
                {
                    ParameterName = "@Type",
                    Value = q.getType()
                };
                cmd.Parameters.Add(paramQuestionType);

                SqlParameter paramQuestionTarget = new SqlParameter()
                {
                    ParameterName = "@Target",
                    Value = q.getTarget()
                };
                cmd.Parameters.Add(paramQuestionTarget);

                SqlParameter paramQuestionCommentBox = new SqlParameter()
                {
                    ParameterName = "@CommentBox",
                    Value = q.getCommentBox()
                };
                paramQuestionCommentBox.ParameterName = "@CommentBox";
                cmd.Parameters.Add(paramQuestionCommentBox);

                string isnps = "0";
                if (q.isNPS())
                {
                    isnps = "1";
                }

                SqlParameter paramIsNPS = new SqlParameter()
                {
                    ParameterName = "@IsNPS",
                    Value = isnps
                };
                cmd.Parameters.Add(paramIsNPS);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // Get Questions for survey
    private Survey getSurveyQuestions(Survey s)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spGetSurveyQuestions", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Survey ID Parameter
            SqlParameter paramSurveyID = new SqlParameter()
            {
                ParameterName = "@SurveyID",
                Value = s.getSurveyID()
            };
            cmd.Parameters.Add(paramSurveyID);

            //Open Connection and run Stored Procedure
            //con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        int QuestionID = 0;
                        int SurveyID = 0;
                        string Text = "";
                        string Type = "false";
                        string Target = "";
                        int number = 0;
                        string CommentBox = "false";
                        bool isNPS = false;
                        foreach (DataColumn col in dt.Columns)
                        {
                            switch (col.ColumnName.ToString())
                            {
                                case "SurveyID":
                                    SurveyID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "QuestionID":
                                    QuestionID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "Text":
                                    Text = row[col.ColumnName].ToString();
                                    break;

                                case "Type":
                                    Type = row[col.ColumnName].ToString();
                                    break;

                                case "Target":
                                    Target = row[col.ColumnName].ToString();
                                    break;

                                case "Number":
                                    number = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "CommentBox":
                                    if (row[col.ColumnName].ToString().Equals("True")) { CommentBox = "1"; }
                                    else { CommentBox = "0"; }
                                    break;

                                case "IsNPS":
                                    isNPS = Convert.ToBoolean(row[col.ColumnName].ToString());
                                    break;
                            }
                        }
                        if (SurveyID == s.getSurveyID() && SurveyID != 0)
                        {
                            Question q = new Question(QuestionID, Text, Type, Target, number, CommentBox, isNPS);
                            s.addQuestion(q);
                        }
                    }
                }
            }
            con.Close();
            return s;
        }
        catch(Exception)
        {
            con.Close();
            return s;
        }
    }


    //DeleteQuestion
    public bool deleteQuestions(LinkedList<Question> deletedquestions)
    {
        if(deletedquestions == null)
        {
            return true;
        }
        try
        {
            SqlCommand cmd = new SqlCommand("spDeleteQuestion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramQuestionID = new SqlParameter()
            {
                ParameterName = "@QuestionID"
            };
            cmd.Parameters.Add(paramQuestionID);

            foreach(Question q in deletedquestions)
            {
                paramQuestionID.Value = q.getQuestionID();
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // ** Layouts **

    // CreateLayout
    public bool creatLayout(Layout layout)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spCreatLayout", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramLayoutID = new SqlParameter()
            {
                ParameterName = "@LayoutID",
                Value = layout.getLayoutID()
            };
            cmd.Parameters.Add(paramLayoutID);

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = layout.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            SqlParameter paramLogoID = new SqlParameter()
            {
                ParameterName = "@LogoID",
                Value = layout.getLogo().getLogoID()
            };
            cmd.Parameters.Add(paramLogoID);

            SqlParameter paramBackgroundColor = new SqlParameter()
            {
                ParameterName = "@BackgroundColor",
                Value = layout.getBackgroundColor().ToString()
            };
            cmd.Parameters.Add(paramBackgroundColor);

            SqlParameter paramLayoutName = new SqlParameter()
            {
                ParameterName = "@LayoutName",
                Value = layout.getLayoutName()
            };
            cmd.Parameters.Add(paramLayoutName);

            SqlParameter paramLogoName = new SqlParameter()
            {
                ParameterName = "@LogoName",
                Value = layout.getLogo().getName()
            };
            cmd.Parameters.Add(paramLogoName);

            SqlParameter paramLogoSize = new SqlParameter()
            {
                ParameterName = "@LogoSize",
                Value = layout.getLogo().getSize()
            };
            cmd.Parameters.Add(paramLogoSize);

            SqlParameter paramLogo = new SqlParameter()
            {
                ParameterName = "@Logo",
                SqlDbType = SqlDbType.VarBinary,
                Value = layout.getLogo().getLogo()
            };
            cmd.Parameters.Add(paramLogo);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            loadLayouts();

            return true;

        //if (result == 1) { return true; }
        //else { return false; }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // Get Layouts
    public bool loadLayouts()
    {
        try{
            SqlCommand cmd = new SqlCommand("spSelectLayouts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = user.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    user.newLayoutList();
                    foreach (DataRow row in dt.Rows)
                    {
                        int LayoutID = 0, LogoID = 0, LogoSize = 0;
                        string LayoutName = "", LogoName = "", backgroundcolor = "";
                        byte[] logo = new byte[0];
                        foreach (DataColumn col in dt.Columns)
                        {
                            switch (col.ColumnName.ToString())
                            {
                                case "LayoutID":
                                    LayoutID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "LogoID":
                                    LogoID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "LayoutName":
                                    LayoutName = row[col.ColumnName].ToString();
                                    break;

                                case "BackgroundColor":
                                    backgroundcolor = row[col.ColumnName].ToString();
                                    break;

                                case "LogoName":
                                    LogoName = row[col.ColumnName].ToString();
                                    break;

                                case "Logo":
                                    logo = (byte[])row[col.ColumnName];
                                    break;

                                case "LogoSize":
                                    LogoSize = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;
                            }
                        }
                        user.addLayout(new Layout(LayoutID, user.getUserID(), backgroundcolor, LayoutName,
                            new Logo(LogoID, LogoSize, LogoName, logo)));
                    }
                }
            }
            con.Close();
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public Layout getLayout(int LayoutID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spGetLayout", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramLayoutID = new SqlParameter()
            {
                ParameterName = "@LayoutID",
                Value = LayoutID
            };
            cmd.Parameters.Add(paramLayoutID);

            //Open Connection and run Stored Procedure
            con.Open();
            Layout layout = null;
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        int LogoID = 0, LogoSize = 0, UserID = 0;
                        string LayoutName = "", LogoName = "", backgroundcolor = "";
                        byte[] logo = new byte[0];
                        foreach (DataColumn col in dt.Columns)
                        {
                            switch (col.ColumnName.ToString())
                            {
                                case "LayoutID":
                                    if (LayoutID != Convert.ToInt32(row[col.ColumnName].ToString()))
                                    {
                                        return null;
                                    }
                                    break;

                                case "LogoSize":
                                    LogoSize = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "UserID":
                                    UserID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "LogoID":
                                    LogoID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "LayoutName":
                                    LayoutName = row[col.ColumnName].ToString();
                                    break;

                                case "LogoName":
                                    LogoName = row[col.ColumnName].ToString();
                                    break;

                                case "BackgroundColor":
                                    backgroundcolor = row[col.ColumnName].ToString();
                                    break;

                                case "Logo":
                                    logo = (byte[])row[col.ColumnName];
                                    break;
                            }
                        }
                        Logo l = new Logo(LogoID, LogoSize, LogoName, logo);
                        layout = new Layout(LayoutID, UserID, backgroundcolor, LayoutName, l);
                    }
                }
            }
            con.Close();
            return layout;
        }
        catch(Exception)
        {
            con.Close();
            return null;
        }
    }

    // EditLayout

    // Update Layout Name
    public bool updateLayoutName(int LayoutID, string newname)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spUpdateLayoutName", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramLayoutID = new SqlParameter()
            {
                ParameterName = "@LayoutID",
                Value = LayoutID
            };
            cmd.Parameters.Add(paramLayoutID);

            SqlParameter paramLayoutName = new SqlParameter()
            {
                ParameterName = "@Name",
                Value = newname
            };
            cmd.Parameters.Add(paramLayoutName);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // Update Logo
    public bool updateLogo(Logo newlogo)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spUpdateLogo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramLogoID = new SqlParameter()
            {
                ParameterName = "@LogoID",
                Value = newlogo.getLogoID()
            };
            cmd.Parameters.Add(paramLogoID);

            SqlParameter paramLogoName = new SqlParameter()
            {
                ParameterName = "@Name",
                Value = newlogo.getName()
            };
            cmd.Parameters.Add(paramLogoName);

            SqlParameter paramLogoSize = new SqlParameter()
            {
                ParameterName = "@Size",
                Value = newlogo.getSize()
            };
            cmd.Parameters.Add(paramLogoSize);

            SqlParameter paramLogo = new SqlParameter()
            {
                ParameterName = "@Logo",
                SqlDbType = SqlDbType.VarBinary,
                Value = newlogo.getLogo()
            };
            cmd.Parameters.Add(paramLogo);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if(result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // Update Background Color
    public bool updateLayoutBackgroundColor(int LayoutID, string backgroundcolor)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spUpdateLayoutBackgroundColor", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramLayoutID = new SqlParameter()
            {
                ParameterName = "@LayoutID",
                Value = LayoutID
            };
            cmd.Parameters.Add(paramLayoutID);

            SqlParameter paramBackgroundColor = new SqlParameter()
            {
                ParameterName = "@BackgroundColor",
                Value = backgroundcolor
            };
            cmd.Parameters.Add(paramBackgroundColor);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if(result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // SaveLayout

    // DeleteLayout
    public bool deleteLayout(Layout layout)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spDeleteLayout", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramLayoutId = new SqlParameter()
            {
                ParameterName = "@LayoutID",
                Value = layout.getLayoutID()
            };
            cmd.Parameters.Add(paramLayoutId);

            SqlParameter paramLogoId = new SqlParameter()
            {
                ParameterName = "@LogoID",
                Value = layout.getLogo().getLogoID()
            };
            cmd.Parameters.Add(paramLogoId);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if(result == 2)
            {
                loadLayouts();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    // ** Take Survey **
    //GetSurvey and Survey Questions
    public bool createSurveyResponse(SurveyTaken takensurvey)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spCreateTakenSurvey", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSurveyTakenID = new SqlParameter()
            {
                ParameterName = "@SurveyTakenID",
                Value = -1,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(paramSurveyTakenID);

            SqlParameter paramSurveyID = new SqlParameter()
            {
                ParameterName = "@SurveyID",
                Value = takensurvey.getSurveyID()
            };
            cmd.Parameters.Add(paramSurveyID);

            con.Open();
            int SurveyTakenID = cmd.ExecuteNonQuery();
            con.Close();

            if(SurveyTakenID > 0)
            {
                cmd = new SqlCommand("spCreateSurveyResponse", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSurveyTakenID2 = new SqlParameter()
                {
                    ParameterName = "@SurveyTakenID",
                    Value = paramSurveyTakenID.Value
                };
                cmd.Parameters.Add(paramSurveyTakenID2);

                SqlParameter paramQuestionID = new SqlParameter()
                {
                    ParameterName = "@QuestionID"
                };
                cmd.Parameters.Add(paramQuestionID);

                SqlParameter paramAnswer = new SqlParameter()
                {
                    ParameterName = "@Answer"
                };
                cmd.Parameters.Add(paramAnswer);

                SqlParameter paramComment = new SqlParameter()
                {
                    ParameterName = "@Comment"
                };
                cmd.Parameters.Add(paramComment);

                SqlParameter paramAnswerID = new SqlParameter()
                {
                    ParameterName = "@AnswerID",
                    Direction = ParameterDirection.Output,
                    Value = -1
                };
                cmd.Parameters.Add(paramAnswerID);


                foreach (Answer a in takensurvey.getAnswers())
                {
                    paramQuestionID.Value = a.getQuestionID().ToString();
                    if (a.getAnswer() != null) { paramAnswer.Value = a.getAnswer(); }
                    else { paramAnswer.Value = ""; }
                    if (a.getCommentText() != null) { paramComment.Value = a.getCommentText(); }
                    else { paramComment.Value = ""; }
                    paramAnswerID.Value = -1;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }

            
            return false;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public bool getSurveyResponses(int SurveyID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spGetSurveysTaken", con);
            cmd.CommandType = CommandType.StoredProcedure;

            user.getSurvey(SurveyID).clearSurveysTaken();

            SqlParameter paramSurveyID = new SqlParameter()
            {
                ParameterName = "@SurveyID",
                Value = SurveyID
            };
            cmd.Parameters.Add(paramSurveyID);

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        int SurveyTakenID = 0;
                        foreach (DataColumn col in dt.Columns)
                        {
                            switch (col.ColumnName.ToString())
                            {
                                case "SurveyID":
                                    if(SurveyID != Convert.ToInt32(row[col.ColumnName].ToString()))
                                    {
                                        return false;
                                    }
                                    break;

                                case "SurveysTakenID":
                                    SurveyTakenID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;
                            }
                        }
                        if(SurveyTakenID > 0)
                        {
                            SurveyTaken takensurvey = new SurveyTaken(SurveyTakenID, SurveyID);
                            takensurvey = getSurveyAnswers(takensurvey);
                            user.getSurvey(SurveyID).addSurveyTaken(takensurvey);
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public bool deleteSurveyResponses(int SurveyID)
    {
        try
        {
            if (user.getSurvey(SurveyID).getSurveysTaken() != null && deleteAnswers(SurveyID))
            {
                SqlCommand cmd = new SqlCommand("spDeleteSurveyResponses", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSurveyID = new SqlParameter()
                {
                    ParameterName = "@SurveyID",
                    Value = SurveyID
                };
                cmd.Parameters.Add(paramSurveyID);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();

                if (result > 1) { return true; }
            }
            return false;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public SurveyTaken getSurveyAnswers(SurveyTaken takensurvey)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spGetAnswers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSurveyTakenID = new SqlParameter()
            {
                ParameterName = "@SurveyTakenID",
                Value = takensurvey.getSurveyTakenID()
            };
            cmd.Parameters.Add(paramSurveyTakenID);

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        int QuestionID = 0;
                        int AnswerID = 0;
                        string CommentText = "";
                        string Answer = "";
                        foreach (DataColumn col in dt.Columns)
                        {
                            switch (col.ColumnName.ToString())
                            {
                                case "QuestionID":
                                    QuestionID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;

                                case "CommentText":
                                    CommentText = row[col.ColumnName].ToString();
                                    break;

                                case "Answer":
                                    Answer = row[col.ColumnName].ToString();
                                    break;

                                case "AnswerID":
                                    AnswerID = Convert.ToInt32(row[col.ColumnName].ToString());
                                    break;
                            }
                        }
                        if (AnswerID > 0)
                        {
                            takensurvey.addAnswer(new Answer(AnswerID, QuestionID, takensurvey.getSurveyTakenID(), 
                                CommentText, Answer));
                        }
                    }
                }
            }
            return takensurvey;
        }
        catch (Exception)
        {
            con.Close();
            return null;
        }
    }

    public bool deleteAnswers(int SurveyID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spDeleteAnswers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSurveyTakenID = new SqlParameter()
            {
                ParameterName = "@SurveyTakenID",
            };
            cmd.Parameters.Add(paramSurveyTakenID);

            foreach(SurveyTaken st in user.getSurvey(SurveyID).getSurveysTaken())
            {
                paramSurveyTakenID.Value = st.getSurveyTakenID();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }
    
    public LinkedList<SurveyType> getTypes()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spGetTypes", con);
            cmd.CommandType = CommandType.StoredProcedure;
            LinkedList<SurveyType> types = new LinkedList<SurveyType>();
            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    foreach(DataRow row in dt.Rows)
                    {
                        string name = "", code = "";
                        foreach (DataColumn col in dt.Columns)
                        {
                            
                            switch (col.ColumnName.ToString())
                            {
                                case "TypeName":
                                    name = row[col.ColumnName].ToString();
                                    break;
                                case "TypeCode":
                                    code = row[col.ColumnName].ToString();
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                        types.AddLast(new SurveyType(name, code));
                    }
                }
            }
            con.Close();
            return types;
        }
        catch (Exception)
        {
            con.Close();
            return null;
        }
    }

    public User getUser()
    {
        return user;
    }

    public string getUserEmail(int UserID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spGetEmail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = UserID
            };
            cmd.Parameters.Add(paramUserID);

            con.Open();
            string result = (string)cmd.ExecuteScalar();
            con.Close();

            return result;
        }
        catch
        {
            con.Close();
            return null;
        }
    }

    // ** Edit Profile **

    public bool updateName(string firstname, string lastname)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spUpdateName", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramFirstName = new SqlParameter()
            {
                ParameterName = "@FirstName",
                Value = firstname
            };
            cmd.Parameters.Add(paramFirstName);

            SqlParameter paramLastName = new SqlParameter()
            {
                ParameterName = "@LastName",
                Value = lastname
            };
            cmd.Parameters.Add(paramLastName);

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = user.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if(result == 1)
            {
                user.setFirstName(firstname);
                user.setLastName(lastname);
                return true;
            }
            else { return false; }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public bool updateEmail(string email)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spUpdateEmail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEmail = new SqlParameter()
            {
                ParameterName = "@Email",
                Value = email
            };
            cmd.Parameters.Add(paramEmail);

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = user.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result == 1)
            {
                user.setEmail(email);
                return true;
            }
            else { return false; }
        }
        catch(Exception)
        {
            con.Close();
            return false;
        }
    }

    public bool updateCompany(string company)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spUpdateCompany", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCompany = new SqlParameter()
            {
                ParameterName = "@Company",
                Value = company
            };
            cmd.Parameters.Add(paramCompany);

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = user.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result == 1)
            {
                user.setCompanyName(company);
                return true;
            }
            else { return false; }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }

    public bool updatePassword(string oldpassword, string newpassword)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp:LoginUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserID = new SqlParameter()
            {
                ParameterName = "@UserID",
                Value = user.getUserID()
            };
            cmd.Parameters.Add(paramUserID);

            con.Open();
            string result = (string)cmd.ExecuteScalar();
            con.Close();

            if (result.Equals(oldpassword))
            {
                cmd = new SqlCommand("spUpdatePassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramPassword = new SqlParameter()
                {
                    ParameterName = "@Password",
                    Value = newpassword
                };
                cmd.Parameters.Add(paramPassword);

                SqlParameter paramUserID2 = new SqlParameter()
                {
                    ParameterName = "@UserID",
                    Value = user.getUserID()
                };
                cmd.Parameters.Add(paramUserID2);

                con.Open();
                int result2 = cmd.ExecuteNonQuery();
                con.Close();

                if(result2 == 1) { return true; }
                else { return false; }
            }
            else { return false; }
        }
        catch (Exception)
        {
            con.Close();
            return false;
        }
    }
}