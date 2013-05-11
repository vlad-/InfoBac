<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="pages_Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link rel="stylesheet" type="text/css" href="../styles/style.css" />
</head>
<body>
   <form id="form1" class="register" runat="server">
        <table>
            <tr>
                <td class="error_message" style="color:red;" colspan="2">
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
                <td>Email</td>
                <td>
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>Password</td>
                <td>
                    <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>Confirm password</td>
                <td>
                    <asp:TextBox ID="cpassword" runat="server" TextMode="Password"></asp:TextBox>
                 </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="submit" class="register_button button" runat="server" OnClientClick="register_post" Text="Register" OnClick="submit_Click" />
                </td>
            </tr>
        
        </table>
        </form>
</body>
</html>
