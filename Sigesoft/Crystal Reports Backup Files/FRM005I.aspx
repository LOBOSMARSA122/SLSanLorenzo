<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005I.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.FRM005I" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" Layout="Fit" ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
            <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="SimpleForm1" TabIndex="7">
                        </x:Button>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel2" EnableBackgroundColor="true" Layout="Fit"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:GroupPanel runat="server" Title="Seleccione un Almacén de Farmacia" ID="GroupPanel3" EnableBackgroundColor="true">
                            <Items>
                                <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true"
                                    AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">
                                    <Items>
                                        <x:DropDownList ID="ddlOrganization" Label="Almacén" Required="false" runat="server" TabIndex="4"
                                            Resizable="true" Width="600px"
                                            ShowRedStar="true" CompareType="String" CompareValue="-1"
                                            CompareOperator="NotEqual" CompareMessage="Por favor seleccione un Almacén!" />
                                    </Items>
                                </x:SimpleForm>
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
