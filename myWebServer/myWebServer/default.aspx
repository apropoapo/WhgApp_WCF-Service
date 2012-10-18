<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="myWebServer._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="my Test Text!!"></asp:Label>
    
        <asp:Button ID="btnStartServer" runat="server" Text="Start Server" />
    
        <asp:Button ID="btnTest" runat="server" Text="Test" Width="85px" />
    
        <asp:Button ID="btnServerOff" runat="server" Text="Server Off" />
    
        <br />
        <asp:ListBox ID="lstBoxLog" runat="server" Height="231px" Width="604px"></asp:ListBox>
    
        <asp:ListBox ID="lstBoxStatus" runat="server" Height="201px" 
            style="margin-top: 0px" Width="270px"></asp:ListBox>
    
    </div>
    </form>
</body>
</html>
