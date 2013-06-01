using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;



public static class DatabaseManager
{
    public const double DefaultDomainWeight = 0;
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

    public static int ExecuteScallar(string command)
    {
        //returneaza un id, daca este int
        try
        {
            SqlCommand cmd = new SqlCommand(command, connection);
            return Decimal.ToInt32((Decimal)cmd.ExecuteScalar());
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

    public static Course GetCourse(int courseId)
    {
        SqlDataReader rdr = SelectQuerry("select * from Courses where Id = " + courseId);
        if (rdr == null)
            return null;

        try
        {
            if (!rdr.HasRows)
                return null;
            rdr.Read();
            Object[] row = new Object[rdr.FieldCount];

            if (rdr.GetValues(row) != 0)
                return new Course(row);
            else
                return null;
        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
            return null;
        }
    }

    public static Dictionary<int, Course> GetCourses()
    {
        Dictionary<int, Course> courses = new Dictionary<int,Course>();

        SqlDataReader rdr = SelectQuerry("Select * from Courses");
        if( rdr == null )
            return null;

        try
        {
            if( !rdr.HasRows ) return null;

            while (rdr.Read())
            {

                Object[] row = new Object[rdr.FieldCount];
                if (rdr.GetValues(row) == 0) return null;

                int id = (int)row[0];

                Course tmpCourse = new Course( row );

                courses.Add( id, tmpCourse );
            }
        }
        catch( Exception e )
        {
            Logger.WriteError(e.Message);
            return null;
        }

        return courses;
    }

    //pentru un userId dat returneaza un dictionar in care cheia e intrebarea si valoarea este ponderea ei
    public static Dictionary<int, WeightInfo> GetQuestionsWeights(int userId)
    {
        Dictionary<int, WeightInfo> questionWeights = new Dictionary<int, WeightInfo>();

        string querry1 = String.Format("select Question, Weight, Number, MistakesNumber, StreakNumber from QuestionsWeights where [User] = {0}", userId);
        string querry2 = String.Format("select Id from Questions where Id NOT IN (select Question from QuestionsWeights where [User] = {0})", userId);
        SqlDataReader rdr1 = SelectQuerry(querry1);
        SqlDataReader rdr2 = SelectQuerry(querry2);
        
        try
        {
            while (rdr1.Read())
            {
                int question = (int)rdr1.GetValue(0);
                WeightInfo weightInfo = new WeightInfo((Double)rdr1.GetValue(1), (int)rdr1.GetValue(2), (int)rdr1.GetValue(3), (int)rdr1.GetValue(4));
                questionWeights.Add(question, weightInfo);
            }

            while (rdr2.Read())
            {
                int question = (int)rdr2.GetValue(0);
                WeightInfo weightInfo = new WeightInfo(DefaultQuestionWeight, 0, 0, 0);
                questionWeights.Add(question, weightInfo);
            }
            
        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
        }

        return questionWeights;

    }

    //intrebarile dintrun anumit domeniu
    public static Dictionary<int, WeightInfo> GetQuestionsWeights(int userId,string domain)
    {
        Dictionary<int, WeightInfo> questionWeights = new Dictionary<int, WeightInfo>();

        string querry1 = String.Format("select qw.Question, qw.Weight, qw.Number, qw.MistakesNumber, qw.StreakNumber from QuestionsWeights qw, Questions q where qw.[User] = {0} and q.Id = qw.Question and q.Domain = '{1}'", userId,domain);
        string querry2 = String.Format("select Id from Questions where Domain = '{1}' and Id NOT IN (select Question from QuestionsWeights where [User] = {0})", userId,domain);
        SqlDataReader rdr1 = SelectQuerry(querry1);
        SqlDataReader rdr2 = SelectQuerry(querry2);

        try
        {
            while (rdr1.Read())
            {
                int question = (int)rdr1.GetValue(0);
                WeightInfo weightInfo = new WeightInfo((Double)rdr1.GetValue(1), (int)rdr1.GetValue(2), (int)rdr1.GetValue(3), (int)rdr1.GetValue(4));
                questionWeights.Add(question, weightInfo);
            }

            while (rdr2.Read())
            {
                int question = (int)rdr2.GetValue(0);
                WeightInfo weightInfo = new WeightInfo(DefaultQuestionWeight, 0, 0, 0);
                questionWeights.Add(question, weightInfo);
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

        string querry1 = String.Format("select Domain, Weight, Number, MistakesNumber, StreakNumber from DomainsWeights where [User] = {0}", userId);
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

    public static WeightInfo GetDomainWeight(int userId, string domain)
    {
        string querry = String.Format("select Weight, Number, MistakesNumber, StreakNumber from DomainsWeights where [User] = {0} and Domain = '{1}'", userId, domain);
        SqlDataReader rdr = SelectQuerry(querry);
        try
        {
            if (!rdr.HasRows)
                return new WeightInfo(DefaultDomainWeight, 0, 0, 0);
            rdr.Read();
            return new WeightInfo((Double)rdr.GetValue(0), (int)rdr.GetValue(1), (int)rdr.GetValue(2), (int)rdr.GetValue(3));
        }
        catch (Exception e)
        {
            Logger.WriteError(e.Message);
            return null;
        }
    }

    public static bool SetQuestionWeight(int userId, int questionId, WeightInfo weightInfo)
    {
        string command = String.Format("update QuestionsWeights set Weight = {0}, Number = {1}, MistakesNumber = {2}, StreakNumber = {3} where Question = {4} and [User] = {5}",
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
        string command = String.Format("update DomainsWeights set Weight = {0}, Number = {1}, MistakesNumber = {2}, StreakNumber = {3} where Domain = '{4}' and [User] = {5}",
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