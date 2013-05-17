using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            //userul il redirectam daca nu e logat la default.aspx
            Response.Redirect("~");
        }
        else {
            LoginLabel.Text = "Salut, " + Session["userName"].ToString()+" ";
        }
    }
    protected void LogOut(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~");
    }
}
