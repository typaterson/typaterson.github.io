using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SurveyType
/// </summary>
public class SurveyType
{
    private string name, code;

    public SurveyType(string Name, string Code)
    {
        name = Name;
        code = Code;
    }

    public string getName()
    {
        return name;
    }
    
    public string getCode()
    {
        return code;
    }
}