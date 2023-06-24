<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AC4_UserRegisteration.aspx.cs" Inherits="AC4_UserRegisteration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Registeration</title>
    <link rel = "stylesheet" type = "text/css" href = "~/StyleSheet.css" />
    <style>
        body {
            background-color: #f5f5f5;
            font-family: Arial, sans-serif;
            margin: 0 auto;
            background-color: #fff;
            box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.1);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TextBox1" runat="server" BackColor="#3366FF" BorderColor="#3399FF"
                BorderWidth="5px" Columns="2" Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" CssClass="StdHeader">Flex | Admin Profile
            </asp:Label>
            <asp:Label ID="TextBox2" runat="server" BackColor="#000099" BorderColor="#000099"
                    BorderWidth="5px" Columns="2" Font-Size="X-Large" ForeColor="#CC9900" Height="40px"
                    Width="100%" CssClass="StdHeader">User Registeration |
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
        <div class="UserInfo" style="border: 3px solid #0000FF">
            <p></p>
            <p class="CourseSelect" >
                <asp:Label ID="UserTypeLabel" runat="server" Text="User Type" Width="40%" Height="70%" Font-Size="16pt" ></asp:Label>
                <asp:DropDownList ID="UserTypeList" runat="server" Width="50%" Height="70%" BackColor="White" Font-Size="16pt" AutoPostBack="True"
                    OnSelectedIndexChanged="UserTypeList_SelectedIndexChanged" ></asp:DropDownList>
            </p>
            <br />
            <div class="UserRegTables"style="border: 3px solid #3399FF">
            <asp:Label ID="Label2" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Admin Information</div></asp:Label><br>
                <p class="COTxtBoxes">
                    <asp:Label ID="UsernameLabel" runat="server" Text="Username" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="UsernameText" runat="server" Width="57%" AutoPostBack="true" OnTextChanged="UsernameText_TextChanged" Font-Size="14pt"></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="PasswordLabel" runat="server" Text="Password" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="PasswordText" runat="server" Width="57%" AutoPostBack="true" Font-Size="14pt" ></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="EmailLabel" runat="server" Text="Email" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="EmailText" runat="server" Width="57%" AutoPostBack="true" OnTextChanged="EmailText_TextChanged" Font-Size="14pt" ></asp:TextBox>
                </p>
                <br>
            </div>
            <br /><br />
            <div class="UserRegTables"style="border: 3px solid #3399FF">
            <asp:Label ID="StdUniInfo" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >User Information</div></asp:Label><br>
                <br>
                <p class="COTxtBoxes">
                    <asp:Label ID="FirstNameLabel" runat="server" Text="First Name" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="FirstNameText" runat="server" Width="57%" Font-Size="14pt"></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="LastNameLabel" runat="server" Text="Last Name" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="LastNameText" runat="server" Width="57%" AutoPostBack="true" Font-Size="14pt" ></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="CNICLabel" runat="server" Text="CNIC" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="CNICText" runat="server" Width="57%" AutoPostBack="true" OnTextChanged="CNICText_TextChanged" Font-Size="14pt"></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="GenderLabel" runat="server" Text="Gender" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:DropDownList ID="GenderList" runat="server" Width="58%" Font-Size="14pt"  ></asp:DropDownList>
                </p>
                <p class="COTxtBoxes">
                    <asp:Table runat="server" Width="80%" HorizontalAlign="Center" Font-Size="14pt">
                        <asp:TableRow >
                            <asp:TableCell Width="7%">
                                <asp:Label ID="YearLabel" runat="server" Text="Year" Font-Size="14"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="25%">
                                <asp:DropDownList ID="YearList" runat="server" Width="85%" Font-Size="14"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Width="10%">
                                <asp:Label ID="MonthLabel" runat="server" Text="Month" Font-Size="14"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="30%">
                                <asp:DropDownList ID="MonthList" runat="server" Width="85%" AutoPostBack="true" Font-Size="14"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Width="7%">
                                <asp:Label ID="DayLabel" runat="server" Text="Day" Font-Size="14"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="30%">
                                <asp:DropDownList ID="DayList" runat="server" Width="100%" Font-Size="14"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </p>
                <br />
            </div>
            <br /><br />
            <div class="UserRegTables"style="border: 3px solid #3399FF">
            <asp:Label ID="Label3" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Contact Information</div></asp:Label><br>
                <p class="COTxtBoxes">
                    <asp:Label ID="CityLabel" runat="server" Text="City" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="CityText" runat="server" Width="57%" Font-Size="14pt"></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="AddressLabel" runat="server" Text="Address" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="AddressText" runat="server" Width="57%" Font-Size="14pt" ></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="PhoneLabel" runat="server" Text="Phone Number" Width="40%" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="PhoneText" runat="server" Width="57%" OnTextChanged="PhoneText_TextChanged" Font-Size="14pt" ></asp:TextBox>
                </p>
                <br>
            </div>
            <br /><br />
            <div class="UserRegTables"style="border: 3px solid #3399FF">
            <asp:Label ID="UserTypeHeader" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >User Specific Information</div></asp:Label><br>
                <p class="COTxtBoxes">
                    <asp:Label ID="CampusLabel" runat="server" Text="Campus" Width="40%" Visible="False" Font-Size="14pt"></asp:Label>
                    <asp:DropDownList ID="CampusList" runat="server" Width="57%" Visible="False" Font-Size="14pt"></asp:DropDownList>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="DepartmentLabel" runat="server" Text="Department" Width="40%" Visible="False" Font-Size="14pt"></asp:Label>
                    <asp:DropDownList ID="DepartmentList" runat="server" Width="57%" Visible="False" Font-Size="14pt"></asp:DropDownList>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="JobTitleLabel" runat="server" Text="Job Title" Width="40%" Visible="False" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="JobTitleList" runat="server" Width="57%" Visible="False" Font-Size="14pt"></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="SalaryLabel" runat="server" Text="Salary" Width="40%" Visible="False" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="SalaryText" runat="server" Width="57%" Visible="False" Font-Size="14pt" ></asp:TextBox>
                </p>

                <p class="COTxtBoxes">
                    <asp:Label ID="DegreeLabel" runat="server" Text="Degree" Width="40%" Visible="False" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="DegreeBox" runat="server" Width="57%" Visible="False" Font-Size="14pt"></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="BatchLabel" runat="server" Text="Batch" Width="40%" Visible="False" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="BatchText" runat="server" Width="57%" Visible="False" Font-Size="14pt"></asp:TextBox>
                </p>
                <p class="COTxtBoxes">
                    <asp:Label ID="ParentSectionLabel" runat="server" Text="Parent Section" Width="40%" Visible="False" Font-Size="14pt"></asp:Label>
                    <asp:TextBox ID="ParentSectionBox" runat="server" Width="57%" Visible="False" AutoPostBack="true" OnTextChanged="ParentSectionBox_TextChanged" Font-Size="14pt"></asp:TextBox>
                </p>
                <br>
            </div>
            <p class="COTxtBoxes">
                <asp:Label ID="FailLabel" runat="server" Text="User Registeration Failed" ForeColor="Red" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="SuccessLabel" runat="server" Text="User Registered Successfully" ForeColor="Green" Visible="False"></asp:Label>
            </p>
            <p class="COTxtBoxes">
                <asp:Button ID="AttendenceGenerateButton" runat="server" Text="Register User" 
                    Width="100%" style="margin-left: 0px" OnClick="RegisterButton_Click" BackColor="#0066FF"
                    Font-Bold="True" Font-Italic="False" Font-Size="Large" ForeColor="White" Height="40px" />
            </p>  
            <br />
       </div>

<!--FirstName, LastName, CNIC, Phone, Gender, Address, City-->


    </form>
</body>
</html>
