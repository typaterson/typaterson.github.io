using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Question
{
    int QuestionID, number;
    string text, type, target, commentbox;
    bool isnps;

    public Question(int QuestionID, string text, string type, string target, int number, string commentbox, bool isnps)
    {
        this.QuestionID = QuestionID;
        this.number = number;
        this.text = text;
        this.type = type;
        this.target = target;
        this.commentbox = commentbox;
        this.isnps = isnps;
    }

    public int getQuestionID()
    {
        return QuestionID;
    }

    public int getQuestionNumber()
    {
        return number;
    }

    public string getQuestionText()
    {
        return text;
    }

    public string getType()
    {
        return type;
    }

    public string getTarget()
    {
        return target;
    }

    public string getCommentBox()
    {
        return commentbox;
    }

    public bool setText(string text)
    {
        this.text = text;
        return true;
    }

    public bool setQuestionNumber(int number)
    {
        this.number = number;
        return true;
    }

    public bool setType(string type)
    {
        this.type = type;
        return true;
    }

    public bool setTarget(string target)
    {
        this.target = target;
        return true;
    }

    public bool setCommentBox(string commentbox)
    {
        this.commentbox = commentbox;
        return true;
    }

    public bool isNPS()
    {
        return isnps;
    }

    public bool setIsNPS(bool b)
    {
        isnps = b;
        return true;
    }
}