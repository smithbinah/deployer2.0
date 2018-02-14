<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsyncForm.aspx.cs" Inherits="Deployer2._0.AsyncForm" Async="true" %>

 <!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />

        <title>Your Servers</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="GenneratorButton" runat="server" OnClick="GenneratorButton_Click" Text="Generate List"/>
            <asp:BulletedList ID="BulletedList1" runat="server"/>
        </div>
    </form>
    
</body>
</html>
