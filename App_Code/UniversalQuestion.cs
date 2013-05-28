using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class UniversalQuestion
{
    protected int id;
    protected string domain;
    public Label l;
    public Label fl;
    public UniversalQuestion()
    {
        l = new Label();
        fl = new Label();
        fl.Visible = false;
    }
    public virtual void display(PlaceHolder p)
    {
    }
    public virtual bool verify(int user, WeightInfo questionWeightInfo)
    {
        return false;
    }
    public virtual bool validate()
    {
        return false;
    }
};
