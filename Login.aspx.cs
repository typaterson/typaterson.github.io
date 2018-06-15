using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    private SQLConnector sqlconnector;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SQLConnector"] != null)
        {
            sqlconnector = (SQLConnector)Session["SQLConnector"];
            if(sqlconnector.getUser() != null)
            {
                Response.Redirect("./home.aspx");
            }            
        }
        sqlconnector = new SQLConnector();        
    }


    protected void Loginbtn_OnClick(object sender, EventArgs e)
    {
        string username = Request.Form["Usernametxt"];
        string password = Request.Form["Passwordtxt"];
        if (sqlconnector.loginUser(username, password))
        {
            Session["SQLConnector"] = sqlconnector;
            Session["IsLogedIn"] = true;
            Response.Redirect("./home.aspx");
        }
        else
        {
            Messagelbl.Text = "Failed";
        }
    }
}