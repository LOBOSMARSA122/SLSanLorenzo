<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMCancelarAgenda.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.ExternalUser.FRMCancelarAgenda" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False" EnableBackgroundColor="True">
        <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnSaveRefresh" Text="Cancelar Agenda" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms=""
                            TabIndex="20">
                        </x:Button>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="21">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>             
        </x:Panel>
    </form>
</body>
</html>
