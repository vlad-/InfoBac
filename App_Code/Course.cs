using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//clasa pentru cursuri

public class Course
{

    private string course_title;
    private string course_description;
    private string course_content;

    public string Title
    {
        get { return course_title; }
    }

    public string Description
    {
        get { return course_description; }
    }

    public string Content
    {
        get { return course_content; }
    }

    public Course(params Object[] list)
	{

        course_title = list[1].ToString().Trim();
        course_content = list[2].ToString().Trim();
        course_description = list[3].ToString().Trim();

	}
}