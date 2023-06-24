<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FC4_FinalizeGrades.aspx.cs" Inherits="FC4_FinalizeGrades" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Finalize Grades</title>
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
                    Width="100%" ><div class="StdHeader">Finalize Grades |
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
        <div class="CourseAllocate" >
            <div style="width: 100%; height:100%;">
                <asp:GridView ID="CourseList" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                    BorderWidth="1px" CellPadding="3" AutoPostBack="true" Width="100%" AutoGenerateColumns="False" 
                    OnRowCommand="CourseList_RowCommand" >
                    <Columns>
                        <asp:BoundField DataField="Srno" HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Course_Code" HeaderText="Course Code" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Course_Name" HeaderText="Course Name" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Section_Name" HeaderText="Section Name" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Finalize" ItemStyle-BorderStyle="NotSet">
                            <ItemTemplate>
                                <asp:Button ID="FinalizeLbl" runat="server" Width="98%" Text="Finalize Grades" 
                                    CommandName="Confirm" CommandArgument='<%# Eval("Srno") %>' ItemStyle-HorizontalAlign="Center" />
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

    </form>
</body>
</html>
