using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SurveyTaken
/// </summary>
public class SurveyTaken
{
    private int SurveyTakenID, SurveyID;
    LinkedList<Answer> answers;

    public SurveyTaken(int SurveyTakenID, int SurveyID)
    {
        this.SurveyTakenID = SurveyTakenID;
        this.SurveyID = SurveyID;
        answers = new LinkedList<Answer>();
    }

    public SurveyTaken(int SurveyID)
    {
        SurveyTakenID = 0;
        this.SurveyID = SurveyID;
        answers = new LinkedList<Answer>();
    }

    public int getSurveyTakenID()
    {
        return SurveyTakenID;
    }

    public int getSurveyID()
    {
        return SurveyID;
    }

    public LinkedList<Answer> getAnswers()
    {
        return answers;
    }

    public bool addAnswer(Answer a)
    {
        answers.AddLast(a);
        return true;
    }

    public Answer getAnswer(int QuestionID)
    {
        foreach(Answer a in answers)
        {
            if(a.getQuestionID() == QuestionID)
            {
                return a;
            }
        }
        return null;
    }

    
}