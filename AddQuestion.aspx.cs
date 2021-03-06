﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (questionType.SelectedValue)
        {
            case "Standard":
                options.Visible = false;
                Answer.Visible = true;
                GAnswer.Visible = false;
                progInput.Visible = false;
                prog.Visible = false;
                break;
            case "Tip Grila":
                options.Visible = true;
                Answer.Visible = false;
                GAnswer.Visible = true;
                progInput.Visible = false;
                prog.Visible = false;
                break;
            case "Program":
                options.Visible = false;
                Answer.Visible = true;
                GAnswer.Visible = false;
                progInput.Visible = true;
                prog.Visible = true;
                break;
        }

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        String qType = questionType.SelectedValue;
        String question = Question.Text.Trim();
        question = Server.HtmlEncode(question);
        question = question.Replace("\r\n", "<br/>");
        
        String r1= null, r2=null, r3=null, r4=null;
        String answer ="";

        if (questionType.SelectedValue == "Tip Grila")
        {
            r1 = Server.HtmlEncode(option1.Text.Trim());
            r2 = Server.HtmlEncode(option2.Text.Trim());
            r3 = Server.HtmlEncode(option3.Text.Trim());
            r4 = Server.HtmlEncode(option4.Text.Trim());
            answer = GAnswer.SelectedValue;
        }
        else if (questionType.SelectedValue == "Standard")
        {
            answer = Answer.Text.Trim();
            answer = Server.HtmlEncode(answer);
            answer = answer.Replace("\r\n", "<br/>");
        }
        else if (questionType.SelectedValue == "Program") {
            answer = Answer.Text.Trim();
        }
        String subjet = Subject.SelectedValue;
        String lectie = Lectie.SelectedValue;
        String querry;
        if (lectie.Equals("null"))
        {
            //insert type, question, r1, r2, r3, r4, answer
            querry = String.Format("insert into Questions (Type,Question,Answer1,Answer2,Answer3,Answer4,Answer,Domain) values ('{0}',N'{1}','{2}','{3}','{4}','{5}','{6}','{7}') SELECT IDENT_CURRENT('Questions')", qType, question, r1, r2, r3, r4, answer, subjet);
        }
        else {
            querry = String.Format("insert into Questions (Type,Question,Answer1,Answer2,Answer3,Answer4,Answer,Domain,Link) values ('{0}',N'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}') SELECT IDENT_CURRENT('Questions')", qType, question, r1, r2, r3, r4, answer, subjet,lectie);
            
        }

        int insertedQuestionId = DatabaseManager.ExecuteScallar(querry);

        if (questionType.SelectedValue == "Program")
        {
            String pi=progInput.Text.Trim();
            querry = String.Format("insert into ProgramInput (Id,Input) values ('{0}','{1}')", insertedQuestionId, pi);
            DatabaseManager.ExecuteNonQuerry(querry);
        }
        Response.Redirect("AddQuestion.aspx");
    }
}