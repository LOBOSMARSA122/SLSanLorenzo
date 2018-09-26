<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM002B.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Security.FRM002B" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" AutoSizePanelID="Panel5" runat="server" />
        <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False"
            EnableBackgroundColor="True" AutoWidth="true" Height="450px">
            <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="Form2,SimpleForm2" TabIndex="7">
                        </x:Button>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel2" EnableBackgroundColor="true"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:TabStrip ID="TabStrip1" ShowBorder="True" ActiveTabIndex="0" runat="server" EnableTitleBackgroundColor="False"
                            Height="450px">
                            <Tabs>
                                <x:Tab ID="Tab1" Title="Globales" EnableBackgroundColor="true" BodyPadding="5px" runat="server">
                                    <Items>
                                        <x:Tree ID="tvGlobalPermissions" OnNodeCheck="tvGlobalPermissions_NodeCheck" ShowHeader="False" runat="server"
                                            AutoScroll="true" EnableCollapse="false" Expanded="true" Height="400" />
                                    </Items>
                                </x:Tab>
                            </Tabs>
                        </x:TabStrip>
                    </Items>
                </x:Panel>

            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />

        <x:Window ID="winEdit" Title="Nuevos Permisos por Nodo / Organización" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" OnClose="winEdit_Close" IsModal="True" Width="600px" Height="550px">
        </x:Window>

        <x:HiddenField ID="hfMode" runat="server" />
    </form>
</body>
</html>
