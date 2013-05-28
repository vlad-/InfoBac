using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Courses : System.Web.UI.Page
{

    private string CLASS_NAME = "Courses";

    protected void Page_Load(object sender, EventArgs e)
    {

        CoursesPanel.Visible = true;

        Dictionary<int, Course> courses = DatabaseManager.GetCourses();

        if (courses == null)
        {

            Logger.WriteError(CLASS_NAME + "-> Problem fetching courses");
            return;
        }

        for (int i = 0; i < courses.Count; ++i)
        {
            Label cLabel = new Label();
            cLabel.Text = courses.Values.ElementAt(i).Title;
            cLabel.Width = 500;
            cLabel.BackColor = Color.Gainsboro;
            cLabel.ID = courses.Keys.ElementAt(i).ToString();

            CoursesPanel.Controls.Add(cLabel);

            cLabel = new Label();
            cLabel.Text = courses.Values.ElementAt(i).Description;
            cLabel.Width = 500;
            cLabel.BackColor = Color.WhiteSmoke;

            CoursesPanel.Controls.Add(cLabel);
        }

    }

    private void labelClick( object sender, EventArgs e )
    {

    }
}