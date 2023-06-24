<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FC3_MarkEvaluations.aspx.cs" Inherits="FC3_MarkEvaluations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mark Evaluations</title>
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
                    Width="100%" ><div class="StdHeader">Mark Evaluations |
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
                <asp:MenuItem Text="Feedack Report" />
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
                    <asp:Label ID="CourseLabel" runat="server" Text="Course" Width="40px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="SectionLabel" runat="server" Text="Section" Width="40px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="EvaluationLabel" runat="server" Text="Evaluation" Width="40px"></asp:Label>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableRow BorderWidth="5">
                <asp:TableCell Width="20%">
                    <asp:DropDownList ID="CourseList" runat="server" Width="100%" AutoPostBack="true" 
                        OnSelectedIndexChanged="CourseList_TextChanged"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:DropDownList ID="SectionList" runat="server" Width="100%" AutoPostBack="true"
                        OnSelectedIndexChanged="SectionList_TextChanged"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:DropDownList ID="EvaluationList" runat="server" Width="100%" AutoPostBack="true"
                        OnSelectedIndexChanged="EvalList_TextChanged"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br /><br />
        <asp:Table ID="EvalDetails" runat="server" Width="80%" CellPadding="2" CellSpacing="5" HorizontalAlign="Center"
            Visible="false" BackColor="#66CCFF" BorderColor="Blue" BorderWidth="2px" Font-Bold="True" ForeColor="Black" GridLines="Vertical">
            <asp:TableHeaderRow HorizontalAlign="Center">
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Evaluation" Width="40px" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label2" runat="server" Text="Weightage" Width="40px"  ></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label3" runat="server" Text="Range" Width="40px" ></asp:Label>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableRow BorderWidth="5" HorizontalAlign="Center">
                <asp:TableCell Width="20%">
                    <asp:Label ID="EvalName" runat="server" Width="100%" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="EvalWeightage" runat="server" Width="100%" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="EvalRange" runat="server" Width="100%" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <div class="CourseAllocate" >
            <div style="width: 100%; height:100%;">
                <asp:GridView ID="SectionStudentsList" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                    BorderWidth="1px" CellPadding="3" AutoPostBack="true" Width="100%" AutoGenerateColumns="False" >
                    <Columns>
                        <asp:BoundField DataField="Srno" HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="RegNo" HeaderText="Roll Number" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Student_Name" HeaderText="Student Name" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Obtained Marks" ItemStyle-BorderStyle="NotSet">
                            <ItemTemplate>
                                <asp:TextBox ID="MarksTxt" runat="server" Width="98%" ItemStyle-HorizontalAlign="Center" />
                            </ItemTemplate>
                       </asp:TemplateField>
                    </Columns>            
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
        <br /><br />
        <div class="MarkUpdate">
            <asp:Button ID="TriggerMarksUpdation" runat="server" Text="Confirm Updates" Width="100%" style="margin-left: 0px" OnClick="CrsCrtButton_Click"
                BackColor="#0066FF" Font-Bold="True" Font-Italic="False" Font-Size="Large" ForeColor="White" Height="35px" Visible="False" />
            <br />
            <asp:Label ID="SuccessLabel0" runat="server" CssClass="COTxtBoxes" ForeColor="#009900" Text="Changes Made Successfully" Visible="False" Width="250px" Height="23px"></asp:Label>
        </div>
    </form>
</body>
</html>
