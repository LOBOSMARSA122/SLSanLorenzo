<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005C.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.FRM005C" %>

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
            EnableBackgroundColor="True" AutoWidth="true">
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
                <x:Panel ID="Panel2" EnableBackgroundColor="true"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:GroupPanel runat="server" Title="Datos Generales" ID="GroupPanel3" EnableBackgroundColor="True">
                            <Items>
                                <x:SimpleForm ID="SimpleForm5" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="True"
                                    AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True" LabelWidth="75px">
                                    <Items>
                                        <x:DropDownList ID="ddlOrganization" Label="Organization" Required="false" runat="server" TabIndex="4"
                                            AutoPostBack="true" Resizable="true" OnSelectedIndexChanged="ddlOrganization_SelectedIndexChanged"
                                            ShowRedStar="true" CompareType="String" CompareValue="-1"
                                            CompareOperator="NotEqual" CompareMessage="Por favor seleccione una Organización!" />
                                        <x:DropDownList ID="ddlLocation" Label="Sede" Required="false" runat="server" TabIndex="3"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"
                                            ShowRedStar="true" CompareType="String" CompareValue="-1"
                                            CompareOperator="NotEqual" CompareMessage="Por favor seleccione una Sede!" />
                                    </Items>
                                </x:SimpleForm>
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Seleccione un Almacén" ID="GroupPanel1" EnableBackgroundColor="True" AutoWidth="true">
                            <Items>
                                <x:Tree ID="tvWarehouse" ShowHeader="False" runat="server"
                                    AutoScroll="true" EnableCollapse="false" Expanded="true" Height="200" />
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>

            </Items>
        </x:Panel>

    </form>
</body>
</html>
