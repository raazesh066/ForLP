<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateBill.aspx.cs" Inherits="GermanHomeopathic.GenerateBill" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:ScriptManager ID="ScriptManager2" runat="server" ></asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer2" runat="server" AsyncRendering="false" Height="690px" Width="850px"  ></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
