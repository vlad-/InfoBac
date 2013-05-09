using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public static class DatabaseManager
{
    private static SqlConnection connection;
    static DatabaseManager()
    {
        connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Furtuna\Documents\GitHub\InfoBac\App_Data\Database.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        connection.Open();
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
}