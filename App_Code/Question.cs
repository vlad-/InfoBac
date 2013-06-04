using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Question
{
    private string m_type;

    public string Type
    {
        get { return m_type; }
    }

    private string m_questionText;

    public string QuestionText
    {
        get { return m_questionText; }
    }

    private string m_answer1;

    public string Answer1
    {
        get { return m_answer1; }
    }

    private string m_answer2;

    public string Answer2
    {
        get { return m_answer2; }
    }

    private string m_answer3;

    public string Answer3
    {
        get { return m_answer3; }
    }

    private string m_answer4;

    public string Answer4
    {
        get { return m_answer4; }
    }

    private string m_answer;

    public string Answer
    {
        get { return m_answer; }
    }

    private string m_domain;

    public string Domain
    {
        get { return m_domain; }
    }

    private string m_link;

    public string Link
    {
        get { return m_link; }
    }

    public Question(params Object[] list)
	{
            m_type = list[1].ToString().Trim();
            m_questionText = list[2].ToString().Trim();
            m_answer1 = list[3].ToString().Trim();
            m_answer2 = list[4].ToString().Trim();
            m_answer3 = list[5].ToString().Trim();
            m_answer4 = list[6].ToString().Trim();
            m_answer = list[7].ToString().Trim();
            m_domain = list[8].ToString().Trim();
            m_link = list[9].ToString().Trim();
    }
}