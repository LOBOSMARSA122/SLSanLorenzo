<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005B.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.FRM005B" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel5" runat="server" />
        <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False"
            EnableBackgroundColor="True" AutoWidth="true">
            <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel2" EnableBackgroundColor="true"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:GroupPanel runat="server" AutoHeight="True" Title="Búsqueda / Filtro" ID="GroupPanel2" AutoWidth="true" BoxFlex="1" Height="70">
                            <Items>
                                <x:SimpleForm ID="Form5" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server">
                                    <Items>
                                        <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item datecontainer" ShowBorder="false" EnableBackgroundColor="true"
                                            Layout="Column" runat="server">
                                            <Items>
                                                <x:Label ID="Label1" Width="50px" runat="server" CssClass="inline" ShowLabel="false" Text="Empresa:" />
                                                <x:TextBox ID="txtOrganizationFilter" Label="Empresa" runat="server" CssClass="mrightmayus" />
                                                <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server"
                                                    AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click">
                                                </x:Button>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:SimpleForm>
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Resultados de la Busqueda" ID="GroupPanel1" EnableBackgroundColor="True" AutoWidth="true">
                            <Items>
                                <x:Grid ID="grd" ShowBorder="true" ShowHeader="false" runat="server"
                                    PageSize="30" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grd_PageIndexChange"
                                    IsDatabasePaging="false" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="i_NodeId,v_OrganizationId,v_LocationId"
                                    EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5"
                                    OnRowCommand="grd_RowCommand" Height="350px" Width="740px">
                                    <Toolbars>
                                        <x:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <x:Button ID="btnNew" Text="Nueva Empresa / Sede / Almacén" Icon="Add" runat="server">
                                                </x:Button>
                                            </Items>
                                        </x:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                            Icon="Pencil" ToolTip="Editar Empresas / Sedes / Almacenes" DataTextFormatString="{0}"
                                            DataIFrameUrlFields="i_NodeId,v_OrganizationId,v_LocationId" DataIFrameUrlFormatString="FRM005C.aspx?Mode=Edit&nodeId={0}&organizationId={1}&locationId={2}"
                                            DataWindowTitleField="v_OrganizationName" DataWindowTitleFormatString="Editar Empresas / Sedes / Almacenes" />
                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar" CommandName="DeleteAction" />
                                        <x:BoundField Width="170px" DataField="v_OrganizationName" DataFormatString="{0}" HeaderText="Empresa" />
                                        <x:BoundField Width="190px" DataField="v_LocationName" DataFormatString="{0}" HeaderText="Sede" />
                                        <x:BoundField Width="570px" DataField="v_WarehouseName" DataFormatString="{0}" HeaderText="Almacén" />
                                    </Columns>
                                </x:Grid>
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />

        <x:Window ID="winEdit" Title="Nueva Empresa / Sede / Almacén del Nodo" Popup="false" EnableIFrame="true" runat="server" Icon="DatabaseAdd"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" OnClose="winEdit_Close" IsModal="True" Width="650px" Height="450px">
        </x:Window>
    </form>
</body>
</html>
