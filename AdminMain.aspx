<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminMain.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flex Admin</title>
    <link rel = "stylesheet" type = "text/css" href = "~/StyleSheet.css" />
    <style>
        body {
            background-color: #f5f5f5;
            font-family: Arial, sans-serif;
            margin: 0 auto;
            background-color: #fff;
            box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.1);
            height:100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TextBox1" runat="server" BackColor="#3366FF" BorderColor="#3399FF"
                    BorderWidth="5px" Columns="2" Font-Size="X-Large" ForeColor="White" Height="40px"
                    Width="100%" ><div class="StdHeader">Flex | Admin Profile</div>
            </asp:Label>
            <asp:Label ID="TextBox2" runat="server" BackColor="#000099" BorderColor="#000099"
                    BorderWidth="5px" Font-Size="X-Large" ForeColor="#CC9900" Height="40px"
                    Width="100%" ><div class="StdHeader">Home |
                    <asp:Button Text="Menu" ForeColor="White" runat="server" OnClick="Unnamed_Click" BackColor="#24248B" />
                </div>
            </asp:Label>
        </div>
        <asp:Menu ID="Menu1" runat="server" Font-Size="13" OnMenuItemClick="Menu1_MenuItemClick"
            Font-Underline="True" ForeColor="White" Height="15" Width="50%" Visible="false" EnableTheming="True">
            <Items>
                <asp:MenuItem Text="Home" />
                <asp:MenuItem Text="Offer Courses" />
                <asp:MenuItem Text="Assign Coordinators" />
                <asp:MenuItem Text="Allocate Instructors" />
                <asp:MenuItem Text="Register Users" />
                <asp:MenuItem Text="Audit Trail Report" />
                <asp:MenuItem Text="Offered Courses Report" />
                <asp:MenuItem Text="Course Allocation Report" />
                <asp:MenuItem Text="Student Sections Report" />
            </Items>
            <StaticHoverStyle BackColor="#0066FF" />
            <StaticMenuItemStyle BorderColor="#3399FF" BorderWidth="2px" Font-Size="20pt" Height="40px" HorizontalPadding="30px" VerticalPadding="10px" />
            <StaticMenuStyle BackColor="#000099" />
        </asp:Menu>
        <br />
        <div class="UniInfo">
            <asp:Label ID="TextBox3" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Admin Information</div></asp:Label><br>
            <div class="ListUniInfo" style ="border: 3px solid #0000FF">
                <br>
                <asp:Literal ID="AdmUsername" runat="server"></asp:Literal><br>
                <asp:Literal ID="AdmName" runat="server"></asp:Literal><br>
                <asp:Literal ID="AdmGender" runat="server"></asp:Literal><br>
                <asp:Literal ID="AdmDOB" runat="server"></asp:Literal><br>
                <asp:Literal ID="AdmCNIC" runat="server"></asp:Literal><br>
                <br />
            </div>
        </div>
        <br /><br />
        <div class="UniInfo" >
            <asp:Label ID="Label2" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Contact Information</div>
            </asp:Label><br>
            <div class="ListUniInfo" style ="border: 3px solid #0000FF">
                <br>
                <asp:Literal ID="AdmEmail" runat="server"></asp:Literal><br>
                <asp:Literal ID="AdmAddress" runat="server"></asp:Literal><br>
                <asp:Literal ID="AdmMobile" runat="server"></asp:Literal><br>
                <br />
            </div>
        </div>
        <br /><br />
        <div class="UniInfo" >
            <asp:Label ID="Label1" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Admin Operations</div>
            </asp:Label><br>
            <div style ="border: 3px solid #0000FF">
                <div class="AdminOptions" >
                    <div class="AdmSpace"> </div>

                    <asp:Button ID="HyperLink1" runat="server" CssClass="AOption" Font-Size="Large" ForeColor="White" Height="50px" Width="60%" OnClick="HyperLink1_Click" Text="Offer Courses"  />
                    <div class="AdmSpace"> </div>
                    <asp:Button ID="HyperLink2" runat="server" CssClass="AOption" Font-Size="Large" ForeColor="White" Height="50px" Width="60%" OnClick="HyperLink2_Click" Text="Assign Coordinators" />
                    <div class="AdmSpace"> </div>
                    <asp:Button ID="HyperLink3" runat="server" CssClass="AOption" Font-Size="Large" ForeColor="White" Height="50px" Width="60%" OnClick="HyperLink3_Click"  Text="Allocate Courses" />
                    <div class="AdmSpace"> </div>
                    <asp:Button ID="HyperLink7" runat="server" CssClass="AOption" Font-Size="Large" ForeColor="White" Height="50px" Width="60%" OnClick="HyperLink7_Click"  Text="User Registeration"  />
                    <div class="AdmSpace"> </div>

                    <asp:Button ID="HyperLink4" runat="server" CssClass="AOption" Font-Size="Large" ForeColor="White" Height="50px" Width="60%" OnClick="HyperLink4_Click"  Text="Audit Trail Report" />
                    <div class="AdmSpace"> </div>
                    <asp:Button ID="HyperLink5" runat="server" CssClass="AOption" Font-Size="Large" ForeColor="White" Height="50px" Width="60%" OnClick="HyperLink5_Click"  Text="Offered Courses Report" />
                    <div class="AdmSpace"> </div>
                    <asp:Button ID="HyperLink6" runat="server" CssClass="AOption" Font-Size="Large" ForeColor="White" Height="50px" Width="60%" OnClick="HyperLink6_Click"  Text="Course Allocation Report" />
                    <div class="AdmSpace"> </div>
                    <asp:Button ID="HyperLink8" runat="server" CssClass="AOption" Font-Size="Large" ForeColor="White" Height="50px" Width="60%" OnClick="HyperLink8_Click"  Text="Student Sections Report" />
                    <div class="AdmSpace"> </div>
                </div>
            </div>
        </div>
        <br />
    </form>
</body>
</html>
