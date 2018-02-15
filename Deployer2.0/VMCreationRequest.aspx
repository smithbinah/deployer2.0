<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VMCreationRequest.aspx.cs" Inherits="Deployer2._0.VMCreationRequest" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="nameLabel" runat="server" Text="Enter Name here:" >
                <asp:Image ID="nameImage" runat="server" />
                </asp:Label>
            <asp:TextBox ID="VMNameSubmission" runat="server" />
            <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Button" />

        </div>
    </form>
</body>
</html>
