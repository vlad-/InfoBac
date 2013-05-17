<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="pages_Login" MasterPageFile="~/notLogIn.master" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
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
</asp:Content>