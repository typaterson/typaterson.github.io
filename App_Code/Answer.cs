using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Answer
/// </summary>
public class Answer
{
    int AnswerID, QuestionID, SurveyTakenID;
    string commenttext, answer;

    public Answer(int AnswerID, int QuestionID, int SurveyTakenID, string commenttext, string answer)
    {
        this.AnswerID = AnswerID;
        this.QuestionID = QuestionID;
        this.SurveyTakenID = SurveyTakenID;
        this.commenttext = commenttext;
        this.answer = answer;
    }

    public Answer(int QuestionID, string commenttext, string answer)
    {
        AnswerID = 0;
        this.QuestionID = QuestionID;
        SurveyTakenID = 0;
        this.commenttext = commenttext;
        this.answer = answer;
    }

    public int getAnswerID()
    {
        return AnswerID;
    }

    public int getQuestionID()
    {
        return QuestionID;
    }

    public int getSurveyTakenID()
    {
        return SurveyTakenID;
    }

    public string getCommentText()
    {
        return commenttext;
    }

    public string getAnswer()
    {
        return answer;
    }
}