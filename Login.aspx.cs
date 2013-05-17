using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_Login : System.Web.UI.Page
{
    public string error_message;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    // la apasarea butonului login se cauta utilizatorul in baza de date si daca este gasit este directionat catre pagina personala
    protected void login_Click(object sender, EventArgs e)
    {
        string userName = username.Text.Trim() , pass = password.Text.Trim();
        int userId = DatabaseManager.VerifyUser(userName, pass, out error_message);
        if (userId != -1) 
        {
            //datele despre utilizator sunt introduse in sesiune
            this.Session.Add("userName", userName);
            Session["userName"] = userName;
            this.Session.Add("userId", userId);
            Session["userId"] = userId;
            Session.Timeout = 60;
            Response.Redirect("Home.aspx");
        }
    }
}