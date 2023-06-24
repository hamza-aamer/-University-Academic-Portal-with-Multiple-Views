﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentMain.aspx.cs" Inherits="Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flex Student</title>
    <link rel = "stylesheet" type = "text/css" href = "~/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TextBox1" runat="server" BackColor="#3366FF" BorderColor="#3399FF"
                    BorderWidth="5px" Columns="2" Font-Size="X-Large" ForeColor="White" Height="40px"
                    Width="100%" ><div class="StdHeader">Flex | Student Profile</div>
            </asp:Label>
            <asp:Label ID="TextBox2" runat="server" BackColor="#000099" BorderColor="#000099"
                    BorderWidth="5px" Font-Size="X-Large" ForeColor="#CC9900" Height="40px"
                    Width="100%" ><div class="StdHeader">Home |
                    <asp:Button Text="Menu" ForeColor="White" runat="server" OnClick="Unnamed_Click" BackColor="#24248B" />
                </div>
            </asp:Label>
        </div>
        <br />
        <asp:Menu ID="Menu1" runat="server" Font-Size="13" OnMenuItemClick="Menu1_MenuItemClick"
            Font-Underline="True" ForeColor="White" Height="15" Width="50%" Visible="false" EnableTheming="True">
            <Items>
                <asp:MenuItem Text="Home" />
                <asp:MenuItem Text="Course Registeration" />
                <asp:MenuItem Text="Attendence" />
                <asp:MenuItem Text="Evaluations" />
                <asp:MenuItem Text="Transcript" />
                <asp:MenuItem Text="Course Feedback" />
            </Items>
            <StaticHoverStyle BackColor="#0066FF" />
            <StaticMenuItemStyle BorderColor="#3399FF" BorderWidth="2px" Font-Size="20pt" Height="40px" HorizontalPadding="30px" VerticalPadding="10px" />
            <StaticMenuStyle BackColor="#000099" />
        </asp:Menu>
        <br /><br />
        <div class="UniInfo">
            <asp:Label ID="Label1" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >University Information</div></asp:Label><br>
            <div class="ListUniInfo" style ="border: 3px solid #0000FF">
                <br>
                <asp:Literal ID="StdRollNo" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdSection" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdDegree" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdCampus" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdBatch" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdStatus" runat="server"></asp:Literal><br>
                <br>
            </div>
        </div>

        <br><br>


        <div class="UniInfo">
            <asp:Label ID="TextBox3" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Student Information</div></asp:Label><br>
            <div class="ListUniInfo" style ="border: 3px solid #0000FF">
                <br>
                <asp:Literal ID="StdName" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdGender" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdDOB" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdCNIC" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdEmail" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdMobile" runat="server"></asp:Literal><br>
                <br>
            </div>
        </div>

        <br><br>
        <div class="UniInfo">
            <asp:Label ID="Label2" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Contact Information</div></asp:Label><br>
            <div class="ListUniInfo" style ="border: 3px solid #0000FF">
                <br>
                <asp:Literal ID="StdAddress" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdCity" runat="server"></asp:Literal><br>
                <asp:Literal ID="StdCountry" runat="server"></asp:Literal><br>
                <br>
            </div>
        </div>
        <br><br>
    </form>
</body>
</html>
