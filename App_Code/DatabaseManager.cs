using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public static class DatabaseManager
{
    public const double DefaultDomainWeight = 5;
    public const double DefaultQuestionWeight = 5;

    private static SqlConnection connection;
    static DatabaseManager()
    {
        connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Furtuna\Documents\GitHub\InfoBac\App_Data\Database.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        connection.Open();
    }

    private static SqlDataReader SelectQuerry(string sql)
    {
        try
        {
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader rdr = cmd.ExecuteReader();

            return rdr;
        }
        catch (Exception exc)
        {
            Logger.WriteError(exc.Message);
            return null;
        }
    }

    public static int ExecuteNonQuerry(string command)
    {
        try
        {
            SqlCommand cmd = new SqlCommand(command, connection);
            return cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
            return 0;
        }

    }

    private static bool UserExists(string userName)
    {
        SqlDataReader rdr = DatabaseManager.SelectQuerry("select * from Users where UserName = '" + userName + "'");

        return rdr.HasRows;        
    }

    private static bool EmailExists(string email)
    {
        SqlDataReader rdr = DatabaseManager.SelectQuerry("select * from Users where Email = '" + email + "'");

        return rdr.HasRows;        
    }

    public static int VerifyUser(String userName, String password, out String error)
    {
        SqlDataReader rdr = DatabaseManager.SelectQuerry("select Id,Password from Users where UserName = '" + userName + "'");
        error = "";
        if (!rdr.HasRows)
        {
            error = "The username doesn\'t exist";
            return -1;
        }
        else
        {
            try
            {
                rdr.Read();
                string pass = rdr.GetValue(1).ToString().Trim();
                if (pass != password)
                {
                    error = "The username and password don't match. Please review the form!";
                    return -1;
                }
                else
                {
                    return (Int32)rdr.GetValue(0);
                }
            }
            catch (Exception e)
            {
                Logger.WriteError(e.Message);
                return -1;
            }
        }
    }

    public static bool AddUser(User user, out String error)
    {
        error = "";
        if (UserExists(user.UserName))
        {
            error = "Username already exists";
            return false;
        }
        if (EmailExists(user.Email))
        {
            error = "Email already exists";
            return false;
        }

        string command = "insert into Users values (" + user.GetUserCommand() + ")";

        return (ExecuteNonQuerry(command) != 0);
    }

    public static Question GetQuestion(int questionId)
    {
        SqlDataReader rdr = SelectQuerry("select * from Questions where Id = " + questionId);
        if (rdr == null)
            return null;

        try
        {
            if (!rdr.HasRows)
                return null;
            rdr.Read();
            Object[] row = new Object[rdr.FieldCount];

            if (rdr.GetValues(row) != 0)
                return new Question(row);
            else
                return null;
        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
            return null;
        }
        
    }

    //pentru un userId dat returneaza un dictionar in care cheia e intrebarea si valoarea este ponderea ei
    public static Dictionary<int, Double> GetQuestionsWeights(int userId)
    {
        Dictionary<int, Double> questionWeights = new Dictionary<int, Double>();

        string querry1 = String.Format("select Question,Weight from QuestionsWeights where [User] = {0}", userId);
        string querry2 = String.Format("select Id from Questions where Id NOT IN (select Question from QuestionsWeights where [User] = {0})", userId);
        SqlDataReader rdr1 = SelectQuerry(querry1);
        SqlDataReader rdr2 = SelectQuerry(querry2);

        try
        {
            while (rdr1.Read())
            {
                int question = (int)rdr1.GetValue(0);
                Double weight = (Double)rdr1.GetValue(1);
                questionWeights.Add(question, weight);
            }

            while (rdr2.Read())
            {
                int question = (int)rdr2.GetValue(0);
                questionWeights.Add(question, DefaultQuestionWeight);
            }
            
        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
        }

        return questionWeights;

    }

    public static Dictionary<string, Double> GetDomainsWeights(int userId)
    {
        Dictionary<string, Double> domainWeights = new Dictionary<string, Double>();

        string querry1 = String.Format("select Domain,Weight from DomainsWeights where [User] = {0}", userId);
        string querry2 = String.Format("select Name from Domains where Name NOT IN (select Domain from DomainsWeights where [User] = {0})", userId);
        SqlDataReader rdr1 = SelectQuerry(querry1);
        SqlDataReader rdr2 = SelectQuerry(querry2);

        try
        {
            while (rdr1.Read())
            {
                string domain = rdr1.GetValue(0).ToString().Trim();
                Double weight = (Double)rdr1.GetValue(1);
                domainWeights.Add(domain, weight);
            }

            while (rdr2.Read())
            {
                string domain = rdr2.GetValue(0).ToString().Trim();
                domainWeights.Add(domain, DefaultDomainWeight);
            }

        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
        }

        return domainWeights;

    }
}