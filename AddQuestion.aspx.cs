using System;
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
                break;
            case "Tip Grila":
                options.Visible = true;
                break;
        }

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        String qType = questionType.SelectedValue;
        String question = Question.Text.Trim();
        String r1= null, r2=null, r3=null, r4=null;
        if (questionType.SelectedValue == "Tip Grila")
        {
            r1 = option1.Text.Trim();
            r2 = option2.Text.Trim();
            r3 = option3.Text.Trim();
            r4 = option4.Text.Trim();
        }

        String answer = Answer.Text.Trim();
        String subjet = Subject.SelectedValue;

        //insert type, question, r1, r2, r3, r4, answer

        String querry = String.Format("insert into Questions (Type,Question,Answer1,Answer2,Answer3,Answer4,Answer,Domain) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", qType, question, r1, r2, r3, r4, answer, subjet);
        //querry = querry.Replace("\', "");
        DatabaseManager.ExecuteNonQuerry(querry);
        
        Response.Redirect("AddQuestion.aspx");
    }
}