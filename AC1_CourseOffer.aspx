<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AC1_CourseOffer.aspx.cs" Inherits="AC1_CourseOffer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Offer Courses</title>
    <style type="text/css">
        #Semester {
            width: 200px;
        }
        #Course_Id {
            width: 200px;
        }
        #Dept_Id {
            width: 200px;
        }
        body {
            background-color: #f5f5f5;
            font-family: Arial, sans-serif;
            margin: 0 auto;
            background-color: #fff;
            height:100%;
        }
    </style>
    <link rel = "stylesheet" type = "text/css" href = "~/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server" style="height: 100%">
        <div>
            <asp:Label ID="TextBox1" runat="server" BackColor="#3366FF" BorderColor="#3399FF"
                BorderWidth="5px" Columns="2" Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" CssClass="StdHeader">Flex | Admin Profile
            </asp:Label>
            <asp:Label ID="TextBox2" runat="server" BackColor="#000099" BorderColor="#000099"
                    BorderWidth="5px" Columns="2" Font-Size="X-Large" ForeColor="#CC9900" Height="40px"
                    Width="100%" CssClass="StdHeader">Offer Courses |
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

        <div class="CourseInfo">
                <asp:Label ID="TextBox6" runat="server" BackColor="#FF9900" BorderColor="#FF9900" Columns="2"
                    Font-Size="X-Large" ForeColor="White" Height="55px" Width="100%" CssClass="OFHeader">
                    <p>Course Offer Form</p>
                </asp:Label>
            <p class="COTxtBoxes">
                <asp:Label ID="DeptLabel" runat="server" Text="Department Code" Width="200px"></asp:Label>
                <asp:TextBox ID="DeptCode" runat="server" Width="200"></asp:TextBox>
                <asp:Label ID="InvalidDeptCode" runat="server" Text="Error: Invalid Department Code"
                    CssClass="ErrSpace" ForeColor="#FF3300" Height="25px" Visible="False"></asp:Label>
            </p>
            <p class="COTxtBoxes">
                <asp:Label ID="CrsLabel" runat="server" Text="Course Code" Width="200"></asp:Label>
                <asp:TextBox ID="CrsCode" runat="server" Width="200"></asp:TextBox>
                <asp:Label ID="InvalidCrsCode" runat="server" Text="Error: Invalid Course Code"
                    CssClass="ErrSpace" ForeColor="#FF3300" Height="25px" Visible="False"></asp:Label>
            </p>
            <p class="COTFxtBoxes">
                <asp:Label ID="SuccessLabel" runat="server" CssClass="COTxtBoxes" ForeColor="#009900" Text="Course Offered Successfully" Visible="False" Width="250px"></asp:Label>
            </p>
            <p class="COTFxtBoxes">
                <asp:Label ID="FailLabel" runat="server" CssClass="COTxtBoxes" ForeColor="Red" Text="Course Already Offered" Visible="False" Width="250px"></asp:Label>
            </p>
            <br>
            <p class="COTxtBoxes">
                <asp:Button ID="Button1" runat="server" Text="Confirm" OnClick="Button1_Click" Width="125px" />
            </p>
       </div>


    </form>
    </body>
</html>
