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
        connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\InfoBac\App_Data\Database.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
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
    public static Dictionary<int, double> GetQuestionsWeights(int userId)
    {
        Dictionary<int, double> questionWeights = new Dictionary<int, double>();

        string querry1 = String.Format("select Question,Weight from QuestionsWeights where [User] = {0}", userId);
        string querry2 = String.Format("select Id from Questions where Id NOT IN (select Question from QuestionsWeights where [User] = {0})", userId);
        SqlDataReader rdr1 = SelectQuerry(querry1);
        SqlDataReader rdr2 = SelectQuerry(querry2);
        
        try
        {
            while (rdr1.Read())
            {
                int question = (int)rdr1.GetValue(0);
                WeightInfo weightInfo = new WeightInfo((Double)rdr1.GetValue(1), (int)rdr1.GetValue(2), (int)rdr1.GetValue(3), (int)rdr1.GetValue(4));
                questionWeights.Add(question, weightInfo.QuestionWeight);
            }

            while (rdr2.Read())
            {
                int question = (int)rdr2.GetValue(0);
                WeightInfo weightInfo = new WeightInfo(DefaultQuestionWeight, 0, 0, 0);
                questionWeights.Add(question, weightInfo.QuestionWeight);
            }
            
        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
        }

        return questionWeights;

    }

    public static Dictionary<string, WeightInfo> GetDomainsWeights(int userId)
    {
        Dictionary<string, WeightInfo> domainWeights = new Dictionary<string, WeightInfo>();

        string querry1 = String.Format("select Domain,Weight from DomainsWeights where [User] = {0}", userId);
        string querry2 = String.Format("select Name from Domains where Name NOT IN (select Domain from DomainsWeights where [User] = {0})", userId);
        SqlDataReader rdr1 = SelectQuerry(querry1);
        SqlDataReader rdr2 = SelectQuerry(querry2);

        try
        {
            while (rdr1.Read())
            {
                string domain = rdr1.GetValue(0).ToString().Trim();
                WeightInfo weightInfo = new WeightInfo((Double)rdr1.GetValue(1), (int)rdr1.GetValue(2), (int)rdr1.GetValue(3), (int)rdr1.GetValue(4));
                domainWeights.Add(domain, weightInfo);
            }

            while (rdr2.Read())
            {
                string domain = rdr2.GetValue(0).ToString().Trim();
                WeightInfo weightInfo = new WeightInfo(DefaultDomainWeight, 0, 0, 0);
                domainWeights.Add(domain, weightInfo);
            }

        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
        }

        return domainWeights;

    }

    public static bool SetQuestionWeight(int userId, int questionId, WeightInfo weightInfo)
    {
        string command = String.Format("update QuestionsWeights set Weight = {0}, Number = {1}, MistakesNumber = {2}, SteakNumber = {3} where Question = {4} and [User] = {5}",
            weightInfo.QuestionWeight, weightInfo.Number, weightInfo.MistakesNumber, weightInfo.StreakNumber, questionId, userId);

        if (ExecuteNonQuerry(command) == 0)
        {
            command = String.Format("insert into QuestionsWeights values( {0}, {1}, {2}, {3}, {4}, {5} )",
          userId, questionId, weightInfo.QuestionWeight, weightInfo.Number, weightInfo.MistakesNumber, weightInfo.StreakNumber);
            return (ExecuteNonQuerry(command) != 0);
        }
        else
            return true;
    }

    public static bool SetDomainWeight(int userId, string domain, WeightInfo weightInfo)
    {
        string command = String.Format("update DomainsWeights set Weight = {0}, Number = {1}, MistakesNumber = {2}, SteakNumber = {3} where Domain = '{4}' and [User] = {5}",
            weightInfo.QuestionWeight, weightInfo.Number, weightInfo.MistakesNumber, weightInfo.StreakNumber, domain, userId);

        if (ExecuteNonQuerry(command) == 0)
        {
            command = String.Format("insert into DomainsWeights values( {0}, '{1}', {2}, {3}, {4}, {5} )",
          userId, domain, weightInfo.QuestionWeight, weightInfo.Number, weightInfo.MistakesNumber, weightInfo.StreakNumber);
            return (ExecuteNonQuerry(command) != 0);
        }
        else
            return true;
    }

    public static double GetGrade(int userId)
    {
        SqlDataReader rdr = SelectQuerry("select Note from Users where Id = " + userId);
        if (rdr == null)
            return -1;

        try
        {
            if (!rdr.HasRows)
                return -1;
            rdr.Read();
            double grade = (double)rdr.GetValue(0);
            return grade;
        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
            return -1;
        }
    }

    public static bool SetGrade(int userId, double grade)
    {
        string command = String.Format("update Users set Note = {0} where Id = {1}", grade, userId);

        return (ExecuteNonQuerry(command) != 0);
    }
}