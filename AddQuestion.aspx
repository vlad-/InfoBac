<%@ Page Title="Add Question" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddQuestion.aspx.cs" Inherits="AddQuestion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <div>
            Tip Intrebare : 

            <asp:DropDownList ID="questionType" runat="server" AutoPostBack="True">
                <asp:ListItem>Standard</asp:ListItem>
                <asp:ListItem>Tip Grila</asp:ListItem>

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
            </p>


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
                </asp:DropDownList>
            </p>

            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />

        </div>
</asp:Content>
