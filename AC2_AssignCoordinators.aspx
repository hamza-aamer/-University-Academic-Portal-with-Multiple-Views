<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AC2_AssignCoordinators.aspx.cs" Inherits="AssignCoordinator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assign Coordinators</title>
    <link rel = "stylesheet" type = "text/css" href = "~/StyleSheet.css" />
    <style>
        body {
            background-color: #f5f5f5;
            font-family: Arial, sans-serif;
            margin: 0 auto;
            background-color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TextBox1" runat="server" BackColor="#3366FF" BorderColor="#3399FF"
                BorderWidth="5px" Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" CssClass="StdHeader">Flex | Admin Profile
            </asp:Label>
            <asp:Label ID="TextBox2" runat="server" BackColor="#000099" BorderColor="#000099"
                    BorderWidth="5px" Font-Size="X-Large" ForeColor="#CC9900" Height="40px"
                    Width="100%" CssClass="StdHeader">Assign Coordinators |
                <asp:Button Text="Menu" ForeColor="White" runat="server" OnClick="Unnamed_Click" Horizontal-Align="Left" BackColor="#24248B" />
            </asp:Label>
        </div>
        <br />
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
        <br />
        <div class="CourseInfo">
            <asp:Label ID="TextBox6" runat="server" BackColor="#FF9900" BorderColor="#FF9900" Columns="2"
                Font-Size="X-Large" ForeColor="White" Height="55px" Width="100%" CssClass="OFHeader">
                <p>Course Allocation Form</p>
            </asp:Label>
            <p class="COTxtBoxes">
                <asp:Label ID="RefNo" runat="server" Text="Reference Number" Width="40%"></asp:Label>
                <asp:TextBox ID="Course_Id" runat="server" Width="50%" OnTextChanged="RefNo_TextChanged" AutoPostBack ="true"></asp:TextBox>
            </p>
            <p class="COTxtBoxes">
                <asp:Label ID="CoordLabel" runat="server" Text="Coordinator" Width="40%"></asp:Label>
                <asp:DropDownList ID="CoordCode" runat="server" Width="50%"></asp:DropDownList>
            </p>
            <p class="COTFxtBoxes">
                <asp:Label ID="SuccessLabel" runat="server" CssClass="COTxtBoxes" ForeColor="#009900" Text="Section Allocated Successfully" Visible="False" Width="250px"></asp:Label>
            </p>
            <p class="COTFxtBoxes">
                <asp:Label ID="FailLabel" runat="server" CssClass="COTxtBoxes" ForeColor="Red" Text="Section Already Allocated" Visible="False" Width="250px"></asp:Label>
            </p>
            <p class="COTxtBoxes">
                <asp:Button ID="CoordinateExecButton" runat="server" Text="Assign" Width="100%" style="margin-left: 0px" OnClick="CrsCrtButton_Click" BackColor="#0066FF" Font-Bold="True" Font-Italic="False" Font-Size="Large" ForeColor="White" Height="40px" />
            </p>
       </div>
        <br /><br /><br />
        <div style="width: 100%; height:100%; margin-left: 10%; margin-right: 10%;">
            <asp:GridView ID="CourseCoordinatorList" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
            BorderWidth="2px" CellPadding="3" Width="80%" Height="100%" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Course_Id" HeaderText="Referrence No." />
                    <asp:BoundField DataField="Dept_Code" HeaderText="Department" />
                    <asp:BoundField DataField="Course_Code" HeaderText="Course Code" />
                    <asp:BoundField DataField="Coordinator" HeaderText="Coordinator" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
         </div>
    </form>
</body>
</html>
