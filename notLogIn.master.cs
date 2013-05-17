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
        if (Session.Count != 0)
        {
            //userul il redirectam dupa logare la home.aspx
            Response.Redirect("~/Home.aspx");
        }
    }
}
