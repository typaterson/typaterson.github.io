using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{

    private SQLConnector sqlconnector;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SQLConnector"] != null)
        {
            sqlconnector = (SQLConnector)Session["SQLConnector"];
            if (sqlconnector.getUser() != null)
            {
                Response.Redirect("./home.aspx");
            }
        }
        sqlconnector = new SQLConnector();
    }

    protected void Registerbtn_OnClick(object sender, EventArgs e)
    {
        string company = Company_Nametxt.Text; // Request.Form["Company_Nametxt"];
        string firstname = First_Nametxt.Text; //Request.Form["First_Nametxt"];
        string lastname = Last_Nametxt.Text; // Request.Form["Last_Nametxt"];
        string email = Emailtxt.Text;           //Request.Form["Emailtxt"];
        string username = Usernametxt.Text;     // Request.Form["Usernametxt"];
        string password = Passwordtxt.Text;     // Request.Form["Passwordtxt"];
        string confirmpassword = Confirm_Passwordtxt.Text; // Request.Form["Confirm_Passwordtxt"];

        if (username != "" && password != "" && firstname != "" && lastname != "")
        {
            if (sqlconnector.checkUsername(username))
            {
                if (password.Equals(confirmpassword))
                {
                    if (sqlconnector.registerUser(company, firstname, lastname, email, username, password))
                    {
                        Session["SQLConnector"] = sqlconnector;
                        Session["IsLogedIn"] = false;
                        Messagelbl.Text = "Registration Successful! Click the login button to go the the login page.";
                        Messagelbl.ForeColor = ColorTranslator.FromHtml("green");
                        //Table1.Visible = false;
                        //Response.Redirect("./home.aspx");
                    }
                    else
                    {
                        Messagelbl.Text = "Failed";
                        Messagelbl.ForeColor = ColorTranslator.FromHtml("red");
                    }
                }
                else
                {
                    Messagelbl.Text = "Passwrods do not match";
                    Messagelbl.ForeColor = ColorTranslator.FromHtml("red");
                }
            }
            else
            {
                Messagelbl.Text = "Username taken.";
                Messagelbl.ForeColor = ColorTranslator.FromHtml("red");
            }
        }
        else
        {
            Messagelbl.Text = "Please check that each required field is filled in.";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("red");
        }
    }
}