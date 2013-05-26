using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_Home : System.Web.UI.Page
{
    static Random r = new Random();
    static int nrintrebari = 3;
    static Dictionary<int, double> d;
    static Dictionary<int, double> feedback = new Dictionary<int, double>();
    static bool finished;
    static bool valid;
    static Question[] questionlist = new Question[nrintrebari];
    static int[][] choices = new int[nrintrebari][];
    
    class UniversalQuestion
    {
        public Label l;
        public Label fl;
        public UniversalQuestion() {
            l = new Label();
            fl = new Label();
            fl.Visible = false;
        }
        public virtual void display(PlaceHolder p){
        }
        public virtual bool verify()
        {
            return false;
        }
        public virtual bool validate()
        {
            return false;
        }
    };
    class ChoiceQuestion : UniversalQuestion {
        static int count = 0;
        public RadioButtonList rbl;
        int answer;
        public ChoiceQuestion(Question q,int[] choice)
        {
            rbl = new RadioButtonList();
            count++;
            base.l.Text = q.QuestionText;
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

        public override bool verify() {
            rbl.Enabled = false;
            if (rbl.SelectedIndex == answer)
            {
                rbl.Items[rbl.SelectedIndex].Attributes.Add("style", "background-color:#ABFF7A;border:2px solid #72CF5F;");
                base.fl.Visible = true;
                base.fl.ForeColor = System.Drawing.Color.Green;
                base.fl.Text = ("ai raspuns corect " + rbl.SelectedIndex + "=" + answer);
                return true;
            }
            rbl.Items[rbl.SelectedIndex].Attributes.Add("style", "background-color:#FF977A;border:2px solid #FF977A;");
            rbl.Items[answer].Attributes.Add("style", "border:2px solid #72CF5F;");
            base.fl.Visible = true;
            base.fl.ForeColor = System.Drawing.Color.Red;
            base.fl.Text = ("ai raspuns gresit " + rbl.SelectedIndex + "!=" + answer);
            return false;
        }
        public override void display(PlaceHolder p) {
            p.Controls.Add(base.l);
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(rbl);
            p.Controls.Add(base.fl);
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(new LiteralControl("<br />"));
        }
    };
    class InputQuestion : UniversalQuestion
    {
        bool isProgram;
        static int icount = 0;
        static int pcount = 0;
        public TextBox t;
        string answer;
        public InputQuestion(Question q, bool ip) {
            t = new TextBox();
            base.l.Text = q.QuestionText;
            answer = q.Answer;
            isProgram = ip;
            if (isProgram) 
            {
                pcount++;
            } else
            {
                icount++;
            }
        }

        public override bool validate()
        {
            if (t.Text.Length==0)
            {
                base.fl.Visible = true;
                base.fl.ForeColor = System.Drawing.Color.Red;
                base.fl.Text = ("nu ai completat campul");
                return false;
            }
            base.fl.Visible = false;
            return true;
        }

        public override bool verify()
        {
            t.Enabled = false;
            bool verify = false;
            if (isProgram)
            {
                verify = false; // verify = answer.Equals(compile_and_run(t.Text));
            }
            else {
            verify=t.Text.Equals(answer);
            }
            
            if (verify)
                {
                    t.BorderColor = System.Drawing.Color.FromArgb(171, 255, 122);
                    base.fl.Visible = true;
                    base.fl.ForeColor = System.Drawing.Color.Green;
                    base.fl.Text = ("ai raspuns corect " + t.Text + "=" + answer);
                    return true;
                }
                t.BorderColor = System.Drawing.Color.FromArgb(255, 151, 122);
                base.fl.Visible = true;
                base.fl.ForeColor = System.Drawing.Color.Red;
                base.fl.Text = ("ai raspuns gresit " + t.Text + "!=" + answer);
                return false;
        }

        public override void display(PlaceHolder p)
        {
            p.Controls.Add(base.l);
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(t);
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(base.fl);
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(new LiteralControl("<br />"));
        }
    };

    static UniversalQuestion[] questions = new UniversalQuestion[nrintrebari];

    protected void Page_Init(object sender, EventArgs e) {
        if (!Page.IsPostBack)
        {
            init();
        }
        for (int i = 0; i < nrintrebari; i++)
        {
            if (questionlist[i].Type.Equals("Tip Grila"))
            {
                questions[i] = new ChoiceQuestion(questionlist[i], choices[i]);
            }
            else if (questionlist[i].Type.Equals("Standard"))
            {
                questions[i] = new InputQuestion(questionlist[i], false);
            }
            questions[i].display(tot);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private int ChooseQuestion() {

        int[] keys = d.Keys.ToArray<int>();
        double[] weights = d.Values.ToArray<double>();
        double numar = 0.0;
        numar = weights.Sum();//numar = suma
        numar = r.NextDouble() * numar; //numar= alegerea
        double partialsum = 0.0;
        int index = keys.Length - 1;
        for (int i = 0; i < keys.Length - 1; i++)
        {
            partialsum += weights[i];
            if (numar < partialsum) { index = i; break; }
        }
        //feedback.Add(keys[index], weights[index]);
        d.Remove(keys[index]);
        return keys[index];
    }

    static private int[] fisher_yates_shuffle() {
        int n = 4;
        int[] p = {0,1,2,3};
        while (n > 1)
        {
            int k = r.Next(n);
            n--;
            int v = p[k];
            p[k] = p[n];
            p[n] = v;
        }
        return p;
    }

    protected void Answer(object sender, EventArgs e)
    {
        int nrfail = 0;
        if (finished&&valid) { Response.Redirect("~/TestRandom.aspx"); }
        else
        {
            valid = true;
            for (int i = 0; i < nrintrebari; i++)
                if (!questions[i].validate()) valid = false;

            if (!valid) return;
            
            //int[] keys = feedback.Keys.ToArray<int>();//to do modify weights
            for (int i = 0; i < nrintrebari; i++)
                if (!questions[i].verify()) nrfail++;

            Chart1.Series["Series1"].Points.Clear();
            int ii = 0;
            if (nrintrebari - nrfail > 0)
            {
                Chart1.Series["Series1"].Points.AddY(nrintrebari - nrfail);
                Chart1.Series["Series1"].Points[0].Color = System.Drawing.Color.FromArgb(171, 255, 122);
                ii = 1;
            }
            if (nrfail > 0)
            {
                Chart1.Series["Series1"].Points.AddY(nrfail);
                Chart1.Series["Series1"].Points[ii].Color = System.Drawing.Color.FromArgb(255, 151, 122);
            }
            finished = true;
            Chart1.Visible = true;
            SubmitButton.Text = "Next Test Please";
        }
    }

    private void init()
    {
        Chart1.Visible = false;
        finished = false;
        SubmitButton.Text = "Submit Answers";
        d = DatabaseManager.GetQuestionsWeights((int)Session["userId"]);
        for (int i = 0; i < nrintrebari; i++)
        {
            questionlist[i] = DatabaseManager.GetQuestion(ChooseQuestion());
            if (questionlist[i].Type.Equals("Tip Grila")) { choices[i] = fisher_yates_shuffle(); }
        }
    }

}