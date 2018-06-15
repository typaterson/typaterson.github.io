using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Layout
/// </summary>
public class Layout
{
    int LayoutID, UserID, LogoID;
    string BackgroundColor;
    string LayoutName, LogoName;
    Logo logo;

    public Layout(int LayoutID, int UserID, string BackgroundColor, string LayoutName, Logo logo)
    {
        this.LayoutID = LayoutID;
        this.UserID = UserID;
        this.BackgroundColor = BackgroundColor;
        this.LayoutName = LayoutName;
        this.logo = logo;
    }

    public int getLayoutID()
    {
        return LayoutID;
    }

    public int getUserID()
    {
        return UserID;
    }

    public string getBackgroundColor()
    {
        return BackgroundColor;
    }

    public string getLayoutName()
    {
        return LayoutName;
    }

    public Logo getLogo()
    {
        return logo;
    }
}