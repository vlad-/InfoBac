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
    static int[] answer = new int[nrintrebari];
    static Label[] textLabels = new Label[nrintrebari];
    static Label[] feedbackLabels = new Label[nrintrebari]; //for test purposes
    static RadioButtonList[] rblists = new RadioButtonList[nrintrebari];

    protected void Page_Init(object sender, EventArgs e) {
        for (int i = 0; i < nrintrebari; i++)
        {
            textLabels[i] = new Label();
            feedbackLabels[i] = new Label();
            rblists[i] = new RadioButtonList();
            rblists[i].SelectedIndexChanged += new EventHandler(CheckedChanged);
            rblists[i].AutoPostBack = true;
            tot.Controls.Add(textLabels[i]);
            tot.Controls.Add(new LiteralControl("<br />"));
            tot.Controls.Add(rblists[i]);
            tot.Controls.Add(new LiteralControl("<br />"));
            tot.Controls.Add(feedbackLabels[i]);
            tot.Controls.Add(new LiteralControl("<br />"));
            rblists[i].ID = "id" + i;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SubmitButton.Enabled = false;
        if (!Page.IsPostBack)
        {
            NewQuestions();
        }
    }

    private void NewQuestions()
    {
        for (int i = 0; i < nrintrebari; i++)
        {
            NewQuestion(textLabels[i], rblists[i], ref answer[i]);
        }
    }

    private int ChooseQuestion() {
        Dictionary<int, double> d = DatabaseManager.GetQuestionsWeights((int)Session["userId"]);

        int[] keys = d.Keys.ToArray<int>();
        double[] weights = d.Values.ToArray<double>();
        double numar = 0.0;
        for (int i = 0; i < weights.Length; i++)
            numar += weights[i];
        //numar = suma
        numar = r.NextDouble() * numar; //numar= alegerea
        double partialsum = 0.0;
        int index = keys.Length - 1;
        for (int i = 0; i < keys.Length - 1; i++)
        {
            partialsum += weights[i];
            if (numar < partialsum) { index = i; break; }
        }

        return keys[index];
    }

    private int[] fisher_yates_shuffle() {
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

    private void NewQuestion(Label l,RadioButtonList rbl,ref int a)
    {
        Question q = DatabaseManager.GetQuestion(ChooseQuestion());
        l.Text = Server.HtmlEncode(q.QuestionText);
        String[] s = new String[4];
        s[0] = q.Answer1;
        s[1] = q.Answer2;
        s[2] = q.Answer3;
        s[3] = q.Answer4;
        a = q.Answer[0] - 'a';
        s[a] += "  !!!this"; //test purposes
        int[] choice = fisher_yates_shuffle();
        //trec a prin inversa perm ca sa aflu care dintre cei 4 itemi in ordine e acum raspunsul
        if (choice[0] == a) { a = 0; }
        else if (choice[1] == a) { a = 1; }
        else if (choice[2] == a) { a = 2; }
        else if (choice[3] == a) { a = 3; }
        rbl.Items.Clear();
        rbl.Items.Add(new ListItem(Server.HtmlEncode(s[choice[0]])));
        rbl.Items.Add(new ListItem(Server.HtmlEncode(s[choice[1]])));
        rbl.Items.Add(new ListItem(Server.HtmlEncode(s[choice[2]])));
        rbl.Items.Add(new ListItem(Server.HtmlEncode(s[choice[3]])));
    }
    protected void Answer(object sender, EventArgs e)
    {
        for (int i=0;i<nrintrebari;i++){
            int si = rblists[i].SelectedIndex;
            if ( si == answer[i])
            {
                feedbackLabels[i].ForeColor = System.Drawing.Color.Green;
                feedbackLabels[i].Text=("la ultima ai raspuns corect " +si+"=" +answer[i]+ " <br/>");
            }
            else {
                feedbackLabels[i].ForeColor = System.Drawing.Color.Red;
                feedbackLabels[i].Text = ("la ultima ai raspuns gresit " + si + "!=" + answer[i] + " <br/>");
            }
        }
            NewQuestions();
        
    }
    protected void CheckedChanged(object sender, EventArgs e)
    {
        for (int i = nrintrebari-1; i >=0; i--)
        {
            if (rblists[i].SelectedIndex == -1) {
                return;
            }
        }
        SubmitButton.Enabled = true;
    }
}