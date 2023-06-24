<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SC3_ViewEvaluations.aspx.cs" Inherits="SC3_ViewEvaluations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Marks</title>
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
                    Width="100%" ><div class="StdHeader">Evaluations |
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
        <div class="CourseInfo" style="border: 3px solid #0000FF">
            <p style="height:10px"></p>
            <p class="CourseSelect" >
                <asp:Label ID="CourseLabel" runat="server" Text="Course" Width="40%" Height="70%" Font-Size="16pt" ></asp:Label>
                <asp:DropDownList ID="CourseList" runat="server" Width="50%" Height="70%" Font-Size="16pt" AutoPostBack="True" 
                    OnSelectedIndexChanged="CourseOptionSelected"></asp:DropDownList>
            </p>
        </div>
        <br><br /><br>
        <asp:GridView ID="AttendenceList" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
            BorderWidth="1px" CellPadding="3" Width="80%" AutoGenerateColumns="False" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Evaluation" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Weightage" HeaderText="Weightage" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Range" HeaderText="Total Marks" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Obtained" HeaderText="Obtained Marks" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </form>
</body>
</html>
