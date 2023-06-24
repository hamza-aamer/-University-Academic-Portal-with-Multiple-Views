<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" type = "text/css" href = "~/StyleSheet.css" />
    <title>Flex</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label4" runat="server" CssClass="loginHeader" BackColor="#0066FF" BorderColor="#3399FF"
                BorderWidth="5px" Font-Size="X-Large" ForeColor="White" Height="40px" Text="Log In" Width="100%"></asp:Label>
        </div>
        <div class="logInFormat">
            <p dir="ltr">
                &nbsp;</p>
            <p>
                <asp:Label ID="Label2" runat="server" Text="Username" Width="127px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Height="16px" Width="190px"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label3" runat="server" Text="Password" Width="127px"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Height="16px" Width="190px"></asp:TextBox>
            </p>
            <p style="height: 0px">
                &nbsp;</p>
            <p>
                <asp:Button ID="LogInButton" runat="server" OnClick="Button1_Click" Text="Login" CssClass="logInButton" />
            </p>
            <p>
                <asp:Label ID="LoginErrorLabel" runat="server" CssClass="loginError" ForeColor="Red" Text="Invalid Username or Password Entered" Visible="False"></asp:Label>
            </p>
            <p>
                &nbsp;</p>
        </div>
    </form>
</body>
</html>
