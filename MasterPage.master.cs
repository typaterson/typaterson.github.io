using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SQLConnector sqlconnector;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlconnector = (SQLConnector)Session["SQLConnector"];
        if (sqlconnector != null && sqlconnector.getUser() != null)
        {
            Usernamelbl.Text = "Logged in as " + sqlconnector.getUser().getUsername();
        }
        else
        {
            Response.Redirect("./login.aspx");
        }
    }

    protected void Logoutbtn_OnClick(object sender, EventArgs e)
    {
        sqlconnector.logoutUser();
        Session["SQLConnector"] = sqlconnector;
        Session["IsLogedIn"] = false;
        Response.Redirect("./login.aspx");
    }
}
