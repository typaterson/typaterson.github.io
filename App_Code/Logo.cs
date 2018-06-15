using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Logo
/// </summary>
public class Logo
{
    int LogoID, size;
    string name;
    byte[] logo;

    public Logo()
    {
        LogoID = 0;
        size = 0;
        name = "";
        logo = new byte[0];
    }

    public Logo(int LogoID, int size, string name, byte[] logo)
    {
        this.LogoID = LogoID;
        this.size = size;
        this.name = name;
        this.logo = logo;
    }

    public int getLogoID()
    {
        return LogoID;
    }

    public int getSize()
    {
        return size;
    }

    public string getName()
    {
        return name;
    }

    public byte[] getLogo()
    {
        return logo;
    }
}