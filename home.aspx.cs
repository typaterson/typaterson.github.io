using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class home : System.Web.UI.Page
{

    SQLConnector sqlconnector = new SQLConnector();

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
        displayPage();
    }

    private void displayPage()
    {
        User u = sqlconnector.getUser();
        NameLbl.Text = u.getFirstName() + " " + u.getLastName();
        UsernameLbl.Text = u.getUsername();
        EmailLbl.Text = u.getEmail();
        CompanyLbl.Text = u.getCompanyName();
    }

    protected void EditNameBtn_OnClick(object sender, EventArgs e)
    {
        NamePlaceholder.Visible = false;
        EditNamePlaceholder.Visible = true;
        FirstNameTxt.Text = sqlconnector.getUser().getFirstName();
        LastNameTxt.Text = sqlconnector.getUser().getLastName();
        NameMessageLbl.Visible = false;
    }

    protected void SaveNameBtn_OnClick(object sender, EventArgs e)
    {
        string firstname = FirstNameTxt.Text;
        string lastname = LastNameTxt.Text;
        if(!firstname.Equals("") && !lastname.Equals(""))
        {
            if(sqlconnector.updateName(firstname, lastname))
            {
                NameLbl.Text = firstname + " " + lastname;
                NamePlaceholder.Visible = true;
                EditNamePlaceholder.Visible = false;
                NameMessageLbl.Text = "Name saved sucssesfully!";
                NameMessageLbl.ForeColor = ColorTranslator.FromHtml("green");
                NameMessageLbl.Visible = true;
            }
            else
            {
                NameMessageLbl.Text = "Something went wrong.";
                NameMessageLbl.ForeColor = ColorTranslator.FromHtml("red");
                NameMessageLbl.Visible = true;
            }
        }
        else
        {
            NameMessageLbl.Text = "Either your first or last name are blank.";
            NameMessageLbl.ForeColor = ColorTranslator.FromHtml("red");
            NameMessageLbl.Visible = true;
        }
    }

    protected void CancelNameBtn_OnClick(object sender, EventArgs e)
    {
        NamePlaceholder.Visible = true;
        EditNamePlaceholder.Visible = false;
        NameMessageLbl.Visible = false;
    }

    protected void EditEmailBtn_OnClick(object sender, EventArgs e)
    {
        EmailPlaceholder.Visible = false;
        EditEmailPlaceholder.Visible = true;
        EmailMessageLbl.Visible = false;
    }

    protected void SaveEmailBtn_OnClick(object sender, EventArgs e)
    {
        string email = EmailTxt.Text;
        if (IsValidEmail(email))
        {
            if (sqlconnector.updateEmail(email))
            {
                EmailLbl.Text = email;
                EmailPlaceholder.Visible = true;
                EditEmailPlaceholder.Visible = false;
                EmailMessageLbl.Text = "Email saved sucssesfully!";
                EmailMessageLbl.ForeColor = ColorTranslator.FromHtml("green");
                EmailMessageLbl.Visible = true;
            }
            else
            {
                EmailMessageLbl.Text = "Something went wrong.";
                EmailMessageLbl.ForeColor = ColorTranslator.FromHtml("red");
                EmailMessageLbl.Visible = true;
            }
        }
        else
        {
            EmailMessageLbl.Text = "Email entered is invalid.";
            EmailMessageLbl.ForeColor = ColorTranslator.FromHtml("red");
            EmailMessageLbl.Visible = true;
        }
    }

    protected void CancelEmailBtn_OnClick(object sender, EventArgs e)
    {
        EmailPlaceholder.Visible = true;
        EditEmailPlaceholder.Visible = false;
        EmailMessageLbl.Visible = false;
    }

    protected void EditCompanyBtn_OnClick(object sender, EventArgs e)
    {
        CompanyPlaceholder.Visible = false;
        EditCompanyPlaceholder.Visible = true;
        CompanyMessageLbl.Visible = false;
    }

    protected void SaveCompanyBtn_OnClick(object sender, EventArgs e)
    {
        string company = CompanyTxt.Text;
        if (!company.Equals(""))
        {
            if (sqlconnector.updateCompany(company))
            {
                CompanyLbl.Text = company;
                CompanyPlaceholder.Visible = true;
                EditCompanyPlaceholder.Visible = false;
                CompanyMessageLbl.Text = "Company saved sucssesfully!";
                CompanyMessageLbl.ForeColor = ColorTranslator.FromHtml("green");
                CompanyMessageLbl.Visible = true;
            }
            else
            {
                CompanyMessageLbl.Text = "Something went wrong.";
                CompanyMessageLbl.ForeColor = ColorTranslator.FromHtml("red");
                CompanyMessageLbl.Visible = true;
            }
        }
        else
        {
            CompanyMessageLbl.Text = "Company Field is blank.";
            CompanyMessageLbl.ForeColor = ColorTranslator.FromHtml("red");
            CompanyMessageLbl.Visible = true;
        }
    }

    protected void CancelCompanyBtn_OnClick(object sender, EventArgs e)
    {
        CompanyPlaceholder.Visible = true;
        EditCompanyPlaceholder.Visible = false;
        CompanyMessageLbl.Visible = false;
    }

    protected void EditPasswordBtn_OnClick(object sender, EventArgs e)
    {
        PasswordPlaceholder.Visible = false;
        EditPasswordPlaceholder.Visible = true;
        PasswordMessageLbl.Visible = false;
    }

    protected void SavePasswordBtn_OnClick(object sender, EventArgs e)
    {
        string oldpassword = OldPasswordtxt.Text;
        string newpassword = NewPasswordTxt.Text;
        string repeatnewpassword = RepeatNewPasswordTxt.Text;

        if (newpassword.Equals(repeatnewpassword))
        {
            if(sqlconnector.updatePassword(oldpassword, newpassword))
            {
                PasswordPlaceholder.Visible = true;
                EditPasswordPlaceholder.Visible = false;
                PasswordMessageLbl.Text = "Password saved sucssesfully!";
                PasswordMessageLbl.ForeColor = ColorTranslator.FromHtml("green");
                PasswordMessageLbl.Visible = true;
            }
            else
            {
                PasswordMessageLbl.Text = "Something went wrong.";
                PasswordMessageLbl.ForeColor = ColorTranslator.FromHtml("red");
                PasswordMessageLbl.Visible = true;
            }
        }
        else
        {
            PasswordMessageLbl.Text = "New Passwords do not match.";
            PasswordMessageLbl.ForeColor = ColorTranslator.FromHtml("red");
            PasswordMessageLbl.Visible = true;
        }
    }
    
    protected void CancelPasswordBtn_OnClick(object sender, EventArgs e)
    {
        PasswordPlaceholder.Visible = true;
        EditPasswordPlaceholder.Visible = false;
        PasswordMessageLbl.Visible = false;
    }

    private bool invalid = false;
    // Check Email
    private bool IsValidEmail(string strIn)
    {
        invalid = false;
        if (String.IsNullOrEmpty(strIn))
            return false;

        // Use IdnMapping class to convert Unicode domain names.
        try
        {
            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }

        if (invalid)
            return false;

        // Return true if strIn is in valid email format.
        try
        {
            return Regex.IsMatch(strIn,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                  RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    private string DomainMapper(Match match)
    {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            invalid = true;
        }
        return match.Groups[1].Value + domainName;
    }
}