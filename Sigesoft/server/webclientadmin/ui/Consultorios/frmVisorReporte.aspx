<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVisorReporte.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmVisorReporte" %>
<%@ Register Assembly="PdfViewer" Namespace="PdfViewer" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">       
    <asp:Label ID="lblmensaje" runat="server" Text=""></asp:Label>
          <cc1:ShowPdf ID="ShowPdf1" runat="server" BorderStyle="Inset" BorderWidth="2px"
                Height="760px"
                Width="820px" />
    </form>
</body>
</html>
