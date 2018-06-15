using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    int UserID;
    string email, firstname, lastname, company, username;
    LinkedList<Survey> surveys;
    LinkedList<Layout> layouts;

    public User(int UserID, string email, string firstname, string lastname, string company, string username)
    {
        this.UserID = UserID;
        this.email = email;
        this.firstname = firstname;
        this.lastname = lastname;
        this.company = company;
        this.username = username;
        layouts = new LinkedList<Layout>();
        surveys = new LinkedList<Survey>();
    }

    // *** Surveys ***

    public void newSurveyList()
    {
        surveys = new LinkedList<Survey>();
    }

    public bool addSurvey(Survey s)
    {
        surveys.AddLast(s);
        return true;
    }

    public LinkedList<Survey> getSurveys()
    {
        return surveys;
    }

    public Survey getSurvey(int id)
    {
        foreach (Survey s in surveys)
        {
            if (s.getSurveyID() == id)
            {
                return s;
            }
        }
        return null;
    }

    // *** Layouts ***

    public void newLayoutList()
    {
        layouts = new LinkedList<Layout>();
    }

    public void addLayout(Layout layout)
    {
        layouts.AddLast(layout);
    }

    public LinkedList<Layout> getLayouts()
    {
        return layouts;
    }

    public Layout getLayout(int id)
    {
        foreach(Layout l in layouts)
        {
            if(l.getLayoutID() == id)
            {
                return l;
            }
        }
        return null;
    }

    public int getUserID()
    {
        return UserID;
    }

    public string getEmail()
    {
        return email;
    }

    public string getFirstName()
    {
        return firstname;
    }

    public string getLastName()
    {
        return lastname;
    }

    public string getCompanyName()
    {
        return company;
    }

    public string getUsername()
    {
        return username;
    }

    public bool setFirstName(string firstname)
    {
        this.firstname = firstname;
        return true;
    }

    public bool setLastName(string lastname)
    {
        this.lastname = lastname;
        return true;
    }

    public bool setEmail(string email)
    {
        this.email = email;
        return true;
    }

    public bool setCompanyName(string companyname)
    {
        company = companyname;
        return true;
    }
}