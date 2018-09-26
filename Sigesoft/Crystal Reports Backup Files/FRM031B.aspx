<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM031B.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.ExternalUser.FRM031B" %>
<%@ Register Assembly="PdfViewer" Namespace="PdfViewer" TagPrefix="cc1" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <cc1:ShowPdf ID="ShowPdf1" runat="server" BorderStyle="Inset" BorderWidth="2px"
                Height="560px"
                Width="650px" />
    </form>
</body>
</html>
