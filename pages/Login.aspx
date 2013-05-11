<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="pages_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="../styles/style.css" />
</head>
<body>
   
    <form id="form2" class="login" runat="server">
   <table>
            <tr>
                <td style="color:red;" colspan="2">
                   <% Response.Write(error_message); %>
                </td>
            </tr>
            <tr>
                <td>Username</td>
                <td>
                    <asp:TextBox ID="username" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Password</td>
                <td>
                    <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
       <tr>
                <td colspan="2">                
                    
                    <asp:Button class="register_button button" ID="login" runat="server" Text="Login" Width="91px" OnClick="login_Click" />
                    
                </td>
            </tr>
       </table>
    </form>
  
</body>
</html>
