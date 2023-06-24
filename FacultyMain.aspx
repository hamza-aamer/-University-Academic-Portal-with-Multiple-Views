<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FacultyMain.aspx.cs" Inherits="Faculty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flex Faculty</title>
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
                    Width="100%" ><div class="StdHeader">Home |
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
        <div class="UniInfo">
            <asp:Label ID="Label2" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Faculty Information</div></asp:Label><br>
            <div class="ListUniInfo" style ="border: 3px solid #0000FF">
                <br>
                <asp:Literal ID="FclUsername" runat="server"></asp:Literal><br>
                <asp:Literal ID="FclName" runat="server"></asp:Literal><br>
                <asp:Literal ID="FclGender" runat="server"></asp:Literal><br>
                <asp:Literal ID="FclDOB" runat="server"></asp:Literal><br>
                <asp:Literal ID="FclCNIC" runat="server"></asp:Literal><br>
                <asp:Literal ID="FclDept" runat="server"></asp:Literal><br>
                <br />
            </div>
        </div>
        <br /><br />
        <div class="UniInfo">
            <asp:Label ID="Label1" runat="server" BackColor="#3366FF" BorderColor="#3399FF" BorderWidth="5px"
                Font-Size="X-Large" ForeColor="White" Height="40px"
                Width="100%" ><div class="StdHeader" >Contact Information</div></asp:Label><br>
            <div class="ListUniInfo" style ="border: 3px solid #0000FF">
                <br>
                <asp:Literal ID="FclEmail" runat="server"></asp:Literal><br>
                <asp:Literal ID="FclAddress" runat="server"></asp:Literal><br>
                <asp:Literal ID="FclMobile" runat="server"></asp:Literal><br>
                <br>
            </div>
        </div>
        <br /><br />
    </form>
</body>
</html>
