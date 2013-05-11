using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_Register : System.Web.UI.Page
{
    protected String error_message;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //dupa un register reusit utilizatorul este trimis catre pagina personala
    protected void submit_Click(object sender, EventArgs e)
    {
        if (password.Text.Trim() != cpassword.Text.Trim())
        {
            error_message = "Password does not match the confirm password !";
            return;
        }
        User user = new User(username.Text.Trim(), password.Text.Trim(), email.Text.Trim(), 0);
        if (DatabaseManager.AddUser(user,out error_message))
        {
            //datele despre utilizator sunt introduse in sesiune
            Session["userId"] = user.UserName;
            Session.Timeout = 60;
            Response.Redirect("Home.aspx");
        }
    }
}