<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FC5_GradeReport.aspx.cs" Inherits="FC5_GradeReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grades Report</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f7f7f7;
            padding: 20px;
        }

        h1 {
            text-align: center;
            margin-bottom: 20px;
        }

        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .dropdown-label {
            display: block;
            font-weight: bold;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TextBox1" runat="server" BackColor="#3366FF" BorderColor="#3399FF"
                    BorderWidth="5px" Columns="2" Font-Size="X-Large" ForeColor="White" Height="40px"
                    Width="100%" ><div class="StdHeader">Flex | Faculty Profile</div>
            </asp:Label>
            <asp:Label ID="TextBox2" runat="server" BackColor="#000099" BorderColor="#000099"
                    BorderWidth="5px" Font-Size="X-Large" ForeColor="#CC9900" Height="40px"
                    Width="100%" ><div class="StdHeader">Grades Report |
                    <asp:Button Text="Menu" ForeColor="White" runat="server" OnClick="Unnamed_Click" BackColor="#24248B" />
                </div>
            </asp:Label>
        </div>
        <br />
        <asp:Menu ID="Menu1" runat="server" Font-Size="13" OnMenuItemClick="Menu1_MenuItemClick"
            Font-Underline="True" ForeColor="White" Height="15" Width="50%" Visible="false" EnableTheming="True">
            <Items>
                <asp:MenuItem Text="Home" />
                <asp:MenuItem Text="Mark Attendence" />
                <asp:MenuItem Text="Set Marks Distribution" />
                <asp:MenuItem Text="Mark Evaluations" />
                <asp:MenuItem Text="Finalize Grades" />
                <asp:MenuItem Text="Grade Report" />
                <asp:MenuItem Text="Feedback Report" />
                <asp:MenuItem Text="Transcript Report" />
            </Items>
            <StaticHoverStyle BackColor="#0066FF" />
            <StaticMenuItemStyle BorderColor="#3399FF" BorderWidth="2px" Font-Size="20pt" Height="40px" HorizontalPadding="30px" VerticalPadding="10px" />
            <StaticMenuStyle BackColor="#000099" />
        </asp:Menu>
        <br /><br />
        <div class="container">
            <h1>Grade Count</h1>
            <label class="dropdown-label" for="ddlCourses">Select Course:</label>
            <asp:DropDownList ID="ddlCourses" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <br />
            <asp:GridView ID="gvResults" runat="server" CssClass="gridview">

            </asp:GridView>
        </div>
    </form>
</body>
</html>