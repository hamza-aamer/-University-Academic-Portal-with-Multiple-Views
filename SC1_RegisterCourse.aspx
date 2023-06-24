<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SC1_RegisterCourse.aspx.cs" Inherits="RegisterCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Registeration</title>
    <style>
        table {
        border-collapse: collapse;
            margin-left: 100px;
        }
        th, td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        th {
            background-color: #f2f2f2;
        }
        td {
            font-size: 14px;
            background-color: #fff;
        }
        td.CourseCode {
            width: 20%;
        }
        td.CourseName {
            width: 70%;
        }
        td.CourseCrdHrs {
            width: 10%;
        }
        .RegButton {
            margin-left: 100px;
        }
    </style>
    <link rel = "stylesheet" type = "text/css" href = "~/StyleSheet.css" />
</head>
<body style="temp">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TextBox1" runat="server" BackColor="#3366FF" BorderColor="#3399FF"
                    BorderWidth="5px" Columns="2" Font-Size="X-Large" ForeColor="White" Height="40px"
                    Width="100%" ><div class="StdHeader">Flex | Student Profile</div>
            </asp:Label>
            <asp:Label ID="TextBox2" runat="server" BackColor="#000099" BorderColor="#000099"
                    BorderWidth="5px" Font-Size="X-Large" ForeColor="#CC9900" Height="40px"
                    Width="100%" ><div class="StdHeader">Course Registeration |
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
        <br />
        <br /><br />
            <asp:Button ID="RegisterButton" runat="server" Text="Register" OnClick="Button1_Click" Width="236px" CssClass="RegButton" HorizontalAlign="Center" />
        <br /><br />
        <br>
        <p>
            <asp:Label ID="UnavailableLabel" runat="server" Text="Course Limit Reached" ForeColor="Red" Visible="False"></asp:Label>
        </p>
        <div class="UniInfo">
            <asp:Label ID="Label2" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Available Courses</div></asp:Label><br><br />        
            <asp:GridView ID="OfferedCourseList" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" CellSpacing="2" CssClass="CList" Width="80%" ForeColor="Red" AutoGenerateColumns="False"
                OnRowDataBound="OfferedCrsList_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEnabled" runat="server"/>
                    </ItemTemplate>
               </asp:TemplateField>
                <asp:BoundField DataField="Course_Code" HeaderText="Code" />
                <asp:BoundField DataField="Course_Name" HeaderText="Name" />
                <asp:BoundField DataField="CrdHrs" HeaderText="Credits" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="#0066FF" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
            </div>
        <p>
            <asp:Label ID="FailLabel" runat="server" Text="Course Registeration Failed" ForeColor="Red" Visible="False"></asp:Label>
        </p>
        <br />
        <div class="UniInfo">
            <asp:Label ID="Label1" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Registered Courses</div></asp:Label><br><br />
                <asp:GridView ID="RegisteredCourses" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
                    BorderWidth="1px" CellPadding="3" CellSpacing="2" CssClass="CList" Width="80%" ForeColor="Red" AutoGenerateColumns="False">
                        <Columns>
                        <asp:BoundField DataField="Course_Code" HeaderText="Code" />
                        <asp:BoundField DataField="Course_Name" HeaderText="Name" />
                    </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="#0066FF" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <br />
    </form>
</body>
</html>