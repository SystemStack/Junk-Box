<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="FindingAPI_WebApp_Sample._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Finding Service Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Enter the query keyword(s): "></asp:Label>
            <asp:TextBox ID="txtKeywords" runat="server" Width="200px"></asp:TextBox>&nbsp;&nbsp;
            <asp:Button ID="btn_SOAPSearch" runat="server" OnClick="btn_SOAPSearch_Click" Text="SOAP_Search" /><br />
            <asp:TextBox ID="txtResult" runat="server" TextMode="MultiLine" Rows="15" Columns="75"
                ReadOnly="True"></asp:TextBox>
        </div>
    </form>
</body>
</html>
