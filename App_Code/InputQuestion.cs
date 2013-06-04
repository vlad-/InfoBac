using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class InputQuestion : UniversalQuestion
{
    bool isProgram;
    static int icount = 0;
    static int pcount = 0;
    public TextBox t;
    string answer;
    public InputQuestion(int i, Question q, bool ip)
    {
        base.domain = q.Domain;
        base.id = i;
        isProgram = ip;
        t = new TextBox();
        t.TextMode = TextBoxMode.MultiLine;
        t.Rows = 4;
        base.l.Text = q.Domain + ") " + q.QuestionText;
        answer = q.Answer;
        if (q.Link.Length>0)
        {
            fh.Text = q.Link;
            fh.NavigateUrl = "~/Lectii.aspx#" + q.Link;
        }
        else
        {
            fh.Text = "Lectiile";
            fh.NavigateUrl = "~/Lectii.aspx";
        }
        if (isProgram)
        {
            t.Rows = 10;
            pcount++;
        }
        else
        {
            icount++;
        }
    }

    public override bool validate()
    {
        if (t.Text.Length == 0)
        {
            base.fl.Visible = true;
            base.fl.ForeColor = System.Drawing.Color.Red;
            base.fl.Text = ("nu ai completat campul");
            return false;
        }
        base.fl.Visible = false;
        return true;
    }

    public override bool verify(int user, WeightInfo questionWeightInfo)
    {
        t.Enabled = false;
        WeightInfo domainWeightInfo = DatabaseManager.GetDomainWeight(user, domain);
        double newDomainWeight;
        string programReturn="";

        bool vverify = false;
        if (isProgram)
        {
            if (CCompiler.Compile("#include<stdlib.h>\n#include<stdio.h>\nint main(){\n" + t.Text + "\nreturn 0;}\n",DatabaseManager.GetInput(base.id), out programReturn))
                    vverify = answer.Equals(programReturn);
            programReturn = programReturn.Replace("\r\n", "<br/>");
        }
        else
        {
            vverify = t.Text.Equals(answer);
        }

        if (vverify)
        {
            t.BorderColor = System.Drawing.Color.FromArgb(171, 255, 122);
            base.fl.Visible = true;
            base.fl.ForeColor = System.Drawing.Color.Green;
            if (!isProgram) base.fl.Text = ("ai raspuns corect");
            else base.fl.Text = programReturn;
            
            DatabaseManager.SetQuestionWeight(user, id, new WeightInfo(DatabaseManager.DefaultQuestionWeight, questionWeightInfo.Number + 1, questionWeightInfo.MistakesNumber, 0));
            newDomainWeight = domainWeightInfo.QuestionWeight * TestLogic.DomainDecrement;
            if (newDomainWeight < TestLogic.MinDomainWeight) newDomainWeight = TestLogic.MinDomainWeight;
            DatabaseManager.SetDomainWeight(user, domain, new WeightInfo(newDomainWeight, domainWeightInfo.Number + 1, domainWeightInfo.MistakesNumber, 0));
            
            return true;
        }
        t.BorderColor = System.Drawing.Color.FromArgb(255, 151, 122);
        base.fl.Visible = true;
        base.fl.ForeColor = System.Drawing.Color.Red;
        if (!isProgram)
        {
            base.fl.Text = ("ai raspuns gresit, raspunsul corect era " + answer + ", mai citeste ");
        }
        else base.fl.Text = programReturn+"<br/>mai citeste ";
        fh.Visible = true;
            
        double newWeight = questionWeightInfo.QuestionWeight * TestLogic.QuestionIncrement;
        if (newWeight > TestLogic.MaxQuestionWeight) { newWeight = TestLogic.MaxQuestionWeight; }
        DatabaseManager.SetQuestionWeight(user, id, new WeightInfo(newWeight, questionWeightInfo.Number + 1, questionWeightInfo.MistakesNumber + 1, questionWeightInfo.StreakNumber + 1));

        newDomainWeight = domainWeightInfo.QuestionWeight * TestLogic.DomainIncrement;
        if (newDomainWeight > TestLogic.MaxDomainWeight) newDomainWeight = TestLogic.MaxDomainWeight;
        DatabaseManager.SetDomainWeight(user, domain, new WeightInfo(newDomainWeight, domainWeightInfo.Number + 1, domainWeightInfo.MistakesNumber + 1, domainWeightInfo.StreakNumber + 1));
        
        return false;
    }

    public override void display(PlaceHolder p)
    {
        p.Controls.Add(base.l);
        p.Controls.Add(new LiteralControl("<br />"));
        p.Controls.Add(t);
        p.Controls.Add(new LiteralControl("<br />"));
        p.Controls.Add(base.fl); p.Controls.Add(base.fh);
        p.Controls.Add(new LiteralControl("<br />"));
        p.Controls.Add(new LiteralControl("<br />"));
    }
};
