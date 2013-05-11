using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Question q =  DatabaseManager.GetQuestion(4);
        Dictionary<int, Double> qw = DatabaseManager.GetQuestionsWeights(1);
        Dictionary<string, Double> dw = DatabaseManager.GetDomainsWeights(1);

        if (Session.Count != 0)
        {
            Login.Visible = false;
            LoginLabel.Visible = true;
            LoginLabel.Text = "Salut, " + Session["userName"].ToString();
        }
    }
}