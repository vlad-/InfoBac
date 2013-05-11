using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class User
{
    #region members

    private string m_name;

    public string UserName
    {
        get { return m_name; }
        set { m_name = value; }
    }

    private string m_password;

    public string Password
    {
        get { return m_password; }
        set { m_password = value; }
    }

    private string m_email;

    public string Email
    {
        get { return m_email; }
        set { m_email = value; }
    }

    private byte m_isAdmin;

    public byte IsAdmin
    {
        get { return m_isAdmin; }
        set { m_isAdmin = value; }
    }

    #endregion

    public User(string name, string password, string email, byte isAdmin)
    {
        m_name = name;
        m_password = password;
        m_email = email;
        m_isAdmin = isAdmin;
    }

    public User()
    {
    }

    public string GetUserCommand()
    {
        return String.Format("'{0}' , '{1}' , '{2}' , {3} , 0", m_name, m_password, m_email, m_isAdmin);
    }
}