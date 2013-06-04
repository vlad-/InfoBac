using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_Home : System.Web.UI.Page
{
    static int nrintrebari = 1;
    static int[] chosenkeylist = new int[nrintrebari];
    static Question[] questionlist = new Question[nrintrebari];
    static int[][] choices = new int[nrintrebari][];
    static UniversalQuestion[] questions = new UniversalQuestion[nrintrebari];
    static Dictionary<int, WeightInfo> d;
    static Dictionary<int, WeightInfo> feedback = new Dictionary<int, WeightInfo>();
    static bool finished;
    static bool valid;
    static int userId;
    protected void Page_Init(object sender, EventArgs e) {
        if (!Page.IsPostBack)
        {
            init();
        }
        for (int i = 0; i < nrintrebari; i++)
        {
            if (questionlist[i].Type.Equals("Tip Grila"))
            {
                questions[i] = new ChoiceQuestion(chosenkeylist[i], questionlist[i], choices[i]);
            }
            else if (questionlist[i].Type.Equals("Standard"))
            {
                questions[i] = new InputQuestion(chosenkeylist[i], questionlist[i], false);
            }
            else if (questionlist[i].Type.Equals("Program"))
            {
                questions[i] = new InputQuestion(chosenkeylist[i], questionlist[i], true);
            }
            questions[i].display(tot);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
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
            
            Chart2.Visible = false;
            if (!valid) return;

            {
                // tre sa ma asigur ca e cel mai recent dictionar, si cel complet
                d = DatabaseManager.GetQuestionsWeights(userId);

                //verification stuff
                for (int i = 0; i < nrintrebari; i++)
                    if (!questions[i].verify(userId, d[chosenkeylist[i]])) 
                    { 
                        nrfail++;
                    }
                //chart stuff
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

                Dictionary<string, WeightInfo> domainsWeights = DatabaseManager.GetDomainsWeights(userId);
                Chart2.Series["Raspunsuri Corecte"].Points.Clear();
                Chart2.Series["Raspunsuri Gresite"].Points.Clear();
                Chart2.ChartAreas.FindByName("ChartArea1").AxisX.Interval = 1;

                double newgrade = 0.0;
                int gradeno = 0;
                foreach (KeyValuePair<string, WeightInfo> entry in domainsWeights)
                {
                    Chart2.Series["Raspunsuri Corecte"].Points.AddY(entry.Value.Number - entry.Value.MistakesNumber);
                    Chart2.Series["Raspunsuri Gresite"].Points.AddXY(entry.Key,entry.Value.MistakesNumber);
                    if (entry.Value.Number>0){
                        newgrade += (1.0 + 9.0 * (double)( entry.Value.Number - entry.Value.MistakesNumber )/ (double)entry.Value.Number);
                    gradeno++;
                    }
                }

                newgrade /= gradeno;
                int trunc = (int)(newgrade * 100);
                DatabaseManager.SetGrade(userId, (double)trunc*0.01);

                Chart1.Visible = true;
                Chart2.Visible = true;
                //end chart stuff
                finished = true;
                SubmitButton.Text = "Next Test Please";
            }
        }
    }

    private void init()
    {
        userId = (int)Session["userId"];
        Chart1.Visible = false;
        Chart2.Visible = false;
        finished = false;
        SubmitButton.Text = "Submit Answers";
        Dictionary<string,WeightInfo> domainsWeights = DatabaseManager.GetDomainsWeights(userId);
        feedback.Clear();

        Dictionary<string, int> domainChoices = TestLogic.ChooseDomains(domainsWeights, nrintrebari);
        int i = 0;
        foreach(KeyValuePair<string,int> entry in domainChoices){
            d = DatabaseManager.GetQuestionsWeights(userId, entry.Key);
            for (int j = 0; j < entry.Value; j++)
            {
                chosenkeylist[i] = TestLogic.ChooseQuestion(d, feedback);
                questionlist[i] = DatabaseManager.GetQuestion(chosenkeylist[i]);
                if (questionlist[i].Type.Equals("Tip Grila")) { choices[i] = TestLogic.FisherYatesShuffle(); }
                i++;
            }
        }
    }
}