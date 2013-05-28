<%@ Page Title="Test Random" Language="C#" AutoEventWireup="true" CodeFile="TestRandom.aspx.cs" Inherits="pages_Home" MasterPageFile="~/logIn.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
    <asp:Chart ID="Chart1" runat="server" Visible="False">
        <Series>
            <asp:Series Name="Series1" ChartType="Doughnut" Font="Arial, 27pt, style=Bold, Italic" IsValueShownAsLabel="True" LabelForeColor="#303030">
                <Points>
                </Points>
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <asp:Chart ID="Chart2" runat="server" IsSoftShadows="False" Width="600px" >
        <Series>
            <asp:Series Name="Nr. raspunsuri">
                <Points>
                </Points>
            </asp:Series>
        </Series>
        <Series>
            <asp:Series Name="Nr. greseli">
                <Points>
                </Points>
            </asp:Series>
        </Series>
        <Series>
            <asp:Series Name="Nr. greseli la rand">
                <Points>
                </Points>
            </asp:Series>
        </Series>
        <Legends>
            <asp:Legend>

            </asp:Legend>
        </Legends>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <br/>
    <asp:PlaceHolder ID="tot" runat="server"></asp:PlaceHolder>
    <br/>
    <asp:Button ID="SubmitButton" runat="server" Text="cant touch this" OnClick="Answer"/>
    <br/><br/>
</asp:Content>
