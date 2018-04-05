<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM032A.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.ExternalUser.FRM032A" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel5" runat="server" />
            <x:Panel ID="Panel4" Title="Panel2"  Height="260px" EnableBackgroundColor="true"
                runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false" BoxConfigAlign="Stretch">
                <Items>
                    <x:Image ID="ImgPhoto" runat="server" ImageCssStyle="border:solid 1px #ccc;padding:5px; align:center;"
                        BoxConfigPosition="Center">
                    </x:Image>
                                       
                </Items>
            </x:Panel>
    </form>
</body>
</html>
