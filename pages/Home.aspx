<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="pages_Home" MasterPageFile="~/pages/Site.master" %>


<asp:Content ContentPlaceHolderID="LoginContent" ID="content1" runat="server">

    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:HyperLink ID="Login" Style="text-decoration: none;" NavigateUrl="Login.aspx" runat="server"><div onmouseover="this.style.background='gray';"  onmouseout="this.style.background='#56758D';" style="background-color:#56758D"  class="HyperLink">Login</div></asp:HyperLink>
    <div style="background-color:#56758D" ><asp:Label ID="LoginLabel" runat="server" Text="Label" Visible="false"></asp:Label></div>
    <asp:HyperLink ID="Register" Style="text-decoration: none;" NavigateUrl="Register.aspx" runat="server"><div onmouseover="this.style.background='gray';"  onmouseout="this.style.background='#56758D';" style="background-color:#56758D" class="HyperLink">Register</div></asp:HyperLink>

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" ID="content2" runat="server">
    Main content
</asp:Content>
