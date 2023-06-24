<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FC1_MarkAttendence.aspx.cs" Inherits="MarkAttendence" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attendence</title>
    <link rel = "stylesheet" type = "text/css" href = "~/StyleSheet.css" />
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
                    Width="100%" ><div class="StdHeader">Attendence |
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
        <br />

        <asp:Table ID="Table1" runat="server" Width="90%" CellPadding="5" CellSpacing="10" HorizontalAlign="Center">
            <asp:TableHeaderRow>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Campuslabel" runat="server" Text="Campus" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="SemesterLabel" runat="server" Text="Semester" Width="40px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="CourseLabel" runat="server" Text="Course" Width="40px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Section" runat="server" Text="Section" Width="40px"></asp:Label>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableRow BorderWidth="5">
                <asp:TableCell Width="20%">
                    <asp:DropDownList ID="CampusList" runat="server" Width="100%" ></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:DropDownList ID="SemesterList" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="SemesterList_TextChanged"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:DropDownList ID="CourseList" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="CourseList_TextChanged"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:DropDownList ID="SectionList" runat="server" Width="100%" ></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <div class="CourseInfo">
            <asp:Label ID="TextBox6" runat="server" BackColor="#FF9900" BorderColor="#FF9900" Columns="2"
                Font-Size="X-Large" ForeColor="White" Height="55px" Width="100%" CssClass="OFHeader">
                <p>Attendence Sheet</p>
            </asp:Label>
            <p class="COTxtBoxes">
                <asp:Label ID="DurationLabel" runat="server" Text="Duration" Width="40%"></asp:Label>
                <asp:DropDownList ID="DurationList" runat="server" Width="50%"></asp:DropDownList>
            </p>
            <p class="COTxtBoxes">
                <asp:Label ID="MonthLabel" runat="server" Text="Month" Width="40%"></asp:Label>
                <asp:DropDownList ID="MonthList" runat="server" Width="50%" AutoPostBack="true" OnSelectedIndexChanged="MonthList_TextChanged"></asp:DropDownList>
            </p>
            <p class="COTxtBoxes">
                <asp:Label ID="DayLabel" runat="server" Text="Day" Width="40%"></asp:Label>
                <asp:DropDownList ID="DayList" runat="server" Width="50%"  ></asp:DropDownList>
            </p>
            <p class="COTFxtBoxes">
                <asp:Label ID="SuccessLabel" runat="server" CssClass="COTxtBoxes" ForeColor="#009900" Text="Class Added Successfully" Visible="False" Width="250px"></asp:Label>
            </p>
            <p class="COTFxtBoxes">
                <asp:Label ID="FailLabel" runat="server" CssClass="COTxtBoxes" ForeColor="Red" Text="Class Adding Unsuccessful" Visible="False" Width="250px"></asp:Label>
            </p>
            <p class="COTxtBoxes">
                <asp:Button ID="ClassAddButton" runat="server" Text="Add" Width="250px"  OnClick="ClassAddButton_Click" />
            </p>
       </div>
        <br /><br />
            <p class="COTxtBoxes">
                <asp:Button ID="AttendenceGenerateButton" runat="server" Text="Generate Attendence Sheet" 
                    Width="100%" style="margin-left: 0px" OnClick="SecAttButton_Click" BackColor="#0066FF"
                    Font-Bold="True" Font-Italic="False" Font-Size="Large" ForeColor="White" Height="40px" />
            </p>
        <p class="COTFxtBoxes">
            <asp:Label ID="NoStdExistLabel" runat="server" CssClass="COTxtBoxes" ForeColor="#FF3300" Text="No Class Exists" Visible="False" Width="250px"></asp:Label>
        </p>
        <br />

        <asp:GridView ID="SectionStudentsList" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
            BorderWidth="1px" CellPadding="3" AutoPostBack="true" CssClass="CList" Width="80%" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="Srno" HeaderText="Sr. No." />
                <asp:BoundField DataField="RegNo" HeaderText="Roll Number" />
                <asp:BoundField DataField="Student_Name" HeaderText="Student Name" />
                <asp:TemplateField HeaderText="Presence" ItemStyle-BorderStyle="NotSet">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEnabled" runat="server" Checked="True" Font-Size="20" />
                    </ItemTemplate>
               </asp:TemplateField>
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
        <br>
        <p class="COTxtBoxes">
            <asp:Button ID="SaveUpdatesButton" runat="server" Text="Update Attendence" 
                Width="100%" style="margin-left: 0px" OnClick="SecSaveButton_Click" BackColor="#0066FF"
                Font-Bold="True" Font-Italic="False" Font-Size="Large" ForeColor="White" Height="40px" Visible="False" />
        </p>  
        <p class="COTFxtBoxes">
            <asp:Label ID="SuccessUpdateLabel" runat="server" CssClass="COTxtBoxes" ForeColor="#009900" Text="Attendence Save Successfully" Visible="False" Width="250px"></asp:Label>
        </p>
        <p class="COTFxtBoxes">
            <asp:Label ID="FailUpdateLabel" runat="server" CssClass="COTxtBoxes" ForeColor="Red" Text="Attendence Save Unsuccessful" Visible="False" Width="250px"></asp:Label>
        </p>


        </form>
    </body>
</html>