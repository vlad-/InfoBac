using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class ChoiceQuestion : UniversalQuestion
{
    static int count = 0;
    public RadioButtonList rbl;
    int answer;
    public ChoiceQuestion(int i, Question q, int[] choice)
    {
        base.domain = q.Domain;
        base.id = i;
        rbl = new RadioButtonList();
        count++;
        base.l.Text = q.Domain + ") " + q.QuestionText;
        String[] s = new String[4];
        s[0] = q.Answer1;
        s[1] = q.Answer2;
        s[2] = q.Answer3;
        s[3] = q.Answer4;
        answer = q.Answer[0] - 'a';
        s[answer] += "  !!!this"; //test purposes
        //trec a prin inversa perm ca sa aflu care dintre cei 4 itemi in ordine e acum raspunsul
        if (choice[0] == answer) { answer = 0; }
        else if (choice[1] == answer) { answer = 1; }
        else if (choice[2] == answer) { answer = 2; }
        else if (choice[3] == answer) { answer = 3; }
        rbl.Items.Clear();
        rbl.Items.Add(new ListItem(s[choice[0]]));
        rbl.Items.Add(new ListItem(s[choice[1]]));
        rbl.Items.Add(new ListItem(s[choice[2]]));
        rbl.Items.Add(new ListItem(s[choice[3]]));
    }

    public override bool validate()
    {
        if (rbl.SelectedIndex == -1)
        {
            base.fl.ForeColor = System.Drawing.Color.Red;
            base.fl.Visible = true;
            base.fl.Text = ("nu ai selectat optiune");
            return false;
        }
        base.fl.Visible = false;
        return true;
    }

    public override bool verify(int user, WeightInfo questionWeightInfo)
    {
        rbl.Enabled = false;
        WeightInfo domainWeightInfo = DatabaseManager.GetDomainWeight(user, domain);
        double newDomainWeight;
        if (rbl.SelectedIndex == answer)
        {
            rbl.Items[rbl.SelectedIndex].Attributes.Add("style", "background-color:#ABFF7A;border:2px solid #72CF5F;");
            base.fl.Visible = true;
            base.fl.ForeColor = System.Drawing.Color.Green;
            base.fl.Text = ("ai raspuns corect " + rbl.SelectedIndex + "=" + answer);
        
            DatabaseManager.SetQuestionWeight(user, id, new WeightInfo(DatabaseManager.DefaultQuestionWeight, questionWeightInfo.Number + 1, questionWeightInfo.MistakesNumber, 0));
            newDomainWeight=domainWeightInfo.QuestionWeight*TestLogic.DomainDecrement;
            if (newDomainWeight<TestLogic.MinDomainWeight) newDomainWeight=TestLogic.MinDomainWeight;
            DatabaseManager.SetDomainWeight(user, domain, new WeightInfo(newDomainWeight, domainWeightInfo.Number + 1, domainWeightInfo.MistakesNumber, 0));
            
            return true;
        }
        rbl.Items[rbl.SelectedIndex].Attributes.Add("style", "background-color:#FF977A;border:2px solid #FF977A;");
        rbl.Items[answer].Attributes.Add("style", "border:2px solid #72CF5F;");
        base.fl.Visible = true;
        base.fl.ForeColor = System.Drawing.Color.Red;
        base.fl.Text = ("ai raspuns gresit " + rbl.SelectedIndex + "!=" + answer);
        
        double newWeight=questionWeightInfo.QuestionWeight*TestLogic.QuestionIncrement;
        if (newWeight>TestLogic.MaxQuestionWeight) {newWeight=TestLogic.MaxQuestionWeight;}
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
        p.Controls.Add(rbl);
        p.Controls.Add(base.fl);
        p.Controls.Add(new LiteralControl("<br />"));
        p.Controls.Add(new LiteralControl("<br />"));
    }
};
