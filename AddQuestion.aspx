<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuestion.aspx.cs" Inherits="AddQuestion" MasterPageFile="~/logIn.master" ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
        <div>
            <p>
                Subiect :
                <asp:DropDownList ID="Subject" runat="server" AutoPostBack="False">
                    <asp:ListItem>1.1</asp:ListItem>
                    <asp:ListItem>1.2</asp:ListItem>
                    <asp:ListItem>2.1</asp:ListItem>
                    <asp:ListItem>2.2</asp:ListItem>
                    <asp:ListItem>2.3</asp:ListItem>
                    <asp:ListItem>2.4</asp:ListItem>
                    <asp:ListItem>2.5</asp:ListItem>
                    <asp:ListItem>3.1</asp:ListItem>
                    <asp:ListItem>3.2</asp:ListItem>
                    <asp:ListItem>3.3</asp:ListItem>
                    <asp:ListItem>3.4</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                Lectia :
                <asp:DropDownList ID="Lectie" runat="server" AutoPostBack="False">
                    <asp:ListItem Text="nu stiu / greu de spus" Value="null"></asp:ListItem>
                    <asp:ListItem Text="pseudocod si algoritmi de baza" Value="Algoritmi"></asp:ListItem>
                    <asp:ListItem>Limbajul_c</asp:ListItem>
                    <asp:ListItem>Tablouri</asp:ListItem>
                    <asp:ListItem>Algoritmi_elementari</asp:ListItem>
                    <asp:ListItem>Recursivitate</asp:ListItem>
                    <asp:ListItem>Backtracking</asp:ListItem>
                    <asp:ListItem>Combinatorica</asp:ListItem>
                    <asp:ListItem>Grafuri</asp:ListItem>
                </asp:DropDownList>
            </p>
            Tip Intrebare : 

            <asp:DropDownList ID="questionType" runat="server" AutoPostBack="True">
                <asp:ListItem>Standard</asp:ListItem>
                <asp:ListItem>Tip Grila</asp:ListItem>
                <asp:ListItem>Program</asp:ListItem>
            </asp:DropDownList>

            <br />

            Intrebare :
            <br />
            <asp:TextBox ID="Question" runat="server"  Width="800px" TextMode="MultiLine" Rows="8"></asp:TextBox>
            
            <br />
            <br />

            <p runat="server" id="options" visible="false">
                Varianta a :
            <asp:TextBox ID="option1" runat="server" Width="500px"></asp:TextBox>
                <br />
                Varianta b :
            <asp:TextBox ID="option2" runat="server" Width="500px"></asp:TextBox>
                <br />
                Varianta c :
            <asp:TextBox ID="option3" runat="server" Width="500px"></asp:TextBox>
                <br />
                Varianta d :
            <asp:TextBox ID="option4" runat="server" Width="500px"></asp:TextBox>
            </p>

            <p>
                Raspuns :
                <br />
                <asp:TextBox ID="Answer" runat="server"  Width="800px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                <asp:Label ID="prog" runat="server" Text="<br/>Input la program pentru raspunsul de mai sus:<br/>"></asp:Label>
                <asp:TextBox ID="progInput" runat="server"  Width="800px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                <asp:DropDownList ID="GAnswer" runat="server" Visible="false">
                    <asp:ListItem>a</asp:ListItem>
                    <asp:ListItem>b</asp:ListItem>
                    <asp:ListItem>c</asp:ListItem>
                    <asp:ListItem>d</asp:ListItem>
                </asp:DropDownList>
            </p>
            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />

        </div>
</asp:Content>