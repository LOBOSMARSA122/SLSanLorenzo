<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.FRM005" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/main.css" rel="stylesheet" />
    <style>
        .datecontainer .x-form-field-trigger-wrap {
            margin-right: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <x:Panel ID="Panel1" runat="server" ShowBorder="True" ShowHeader="True" Title="Administración de Nodos" EnableBackgroundColor="true"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5">
            <Items>
                <x:GroupPanel runat="server" AutoHeight="True" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="70">
                    <Items>
                        <x:SimpleForm ID="Form5" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server">
                            <Items>
                                <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item datecontainer" ShowBorder="false" EnableBackgroundColor="true"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label1" Width="50px" runat="server" CssClass="inline" ShowLabel="false" Text="Nodo:" />
                                        <x:TextBox ID="txtNodeFilter" Label="Nodo" runat="server" CssClass="mrightmayus" />
                                        <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server"
                                            AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click">
                                        </x:Button>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:SimpleForm>
                    </Items>
                </x:GroupPanel>
                <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" Title="Administración de Nodos" runat="server"
                    PageSize="15" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grdData_PageIndexChange"
                    IsDatabasePaging="true" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                    EnableMouseOverColor="true" ShowGridHeader="true" BoxFlex="2" BoxMargin="5" OnRowCommand="grdData_RowCommand"
                    DataKeyNames="i_NodeId" EnableTextSelection="true" EnableAlternateRowColor="true" OnPreDataBound="grdData_PreDataBound">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Button ID="btnNew" Text="Nuevo Nodo" Icon="Add" runat="server">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>
                    <Columns>
                        <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                            Icon="Pencil" ToolTip="Editar Nodo" DataTextFormatString="{0}"
                            DataIFrameUrlFields="i_NodeId" DataIFrameUrlFormatString="FRM005A.aspx?Mode=Edit&nodeId={0}"
                            DataWindowTitleField="v_Description" DataWindowTitleFormatString="Editar Nodo: {0}" />

                        <x:WindowField ColumnID="myWindowField1" Width="25px" WindowID="winEdit1" HeaderText=""
                            Icon="House" ToolTip="Asignar / Desasignar Empresas / Sedes / Almacenes al nodo." DataTextFormatString="{0}"
                            DataIFrameUrlFields="i_NodeId,v_Description" DataIFrameUrlFormatString="FRM005B.aspx?nodeId={0}&nodeName={1}"
                            DataWindowTitleField="v_Description" DataWindowTitleFormatString="Asignar / Desasignar [Empresas / Sedes / Almacenes] al nodo: {0}" />

                        <x:WindowField ColumnID="myWindowField3" Width="25px" WindowID="winEditNodeRol" HeaderText=""
                            Icon="ComputerKey" ToolTip="Editar Nodo Rol" DataTextFormatString="{0}"
                            DataIFrameUrlFields="i_NodeId" DataIFrameUrlFormatString="FRM005D.aspx?Mode=Edit&nodeId={0}"
                            DataWindowTitleField="v_Description" DataWindowTitleFormatString="Editar Roles del Nodo: {0}" />

                         <x:WindowField ColumnID="myWindowField4" Width="25px" WindowID="WinEditNodeServiceProfile" HeaderText=""
                            Icon="ServerConnect" ToolTip="Editar Servicio" DataTextFormatString="{0}"
                            DataIFrameUrlFields="i_NodeId,v_Description" DataIFrameUrlFormatString="FRM005F.aspx?Mode=Edit&nodeId={0}&nodeName={1}"
                            DataWindowTitleField="v_Description" DataWindowTitleFormatString="Editar Servicos del Nodo: {0}" />

                         <x:WindowField ColumnID="myWindowField5" Width="25px" WindowID="winEditAttentionInArea" HeaderText=""
                            Icon="ComputerKey" ToolTip="Lista de Atenciones por Nodo" DataTextFormatString="{0}"
                            DataIFrameUrlFields="i_NodeId" DataIFrameUrlFormatString="FRM005G.aspx?Mode=Edit&nodeId={0}"
                            DataWindowTitleField="v_Description" DataWindowTitleFormatString="Consultorios y examenes del Nodo {0}" />

                        <x:WindowField ColumnID="myWindowField6" Width="25px" WindowID="winEditAlmacenFarmacia" HeaderText=""
                            Icon="ShapeMoveForwards" ToolTip="Lista de Almacenes de Farmacia del Nodo" DataTextFormatString="{0}"
                            DataIFrameUrlFields="i_NodeId" DataIFrameUrlFormatString="FRM005I.aspx?Mode=Edit&nodeId={0}"
                            DataWindowTitleField="v_Description" DataWindowTitleFormatString="Lista de Almacenes de Farmacia del Nodo {0}" />

                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                            ColumnID="lbfAction2" Width="25px" ToolTip="Eliminar Nodo" CommandName="DeleteAction" />
                        <x:BoundField Width="60px" DataField="i_NodeId" DataFormatString="{0}" HeaderText="Nodo Id" />
                        <x:BoundField Width="200px" DataField="v_Description" DataFormatString="{0}" HeaderText="Nodo" />
                        <x:BoundField Width="100px" DataField="v_GeografyLocationId" DataFormatString="{0}" HeaderText="Ubigeo" />
                        <x:BoundField Width="150px" DataField="v_GeografyLocationDescription" DataFormatString="{0}" HeaderText="Ubicacion" />
                        <x:BoundField Width="200px" DataField="v_NodeType" DataFormatString="{0}" HeaderText="Tipo de Nodo" />
                        <x:BoundField Width="170px" DataField="d_BeginDate" DataFormatString="{0}" HeaderText="Fecha Inicio" />
                        <x:BoundField Width="170px" DataField="d_EndDate" DataFormatString="{0}" HeaderText="Fecha Fin" />
                        <x:BoundField Width="100px" DataField="v_InsertUser" DataFormatString="{0}" HeaderText="Usuario Crea." />
                        <x:BoundField Width="160px" DataField="d_InsertDate" DataFormatString="{0}" HeaderText="Fecha Crea. " />
                        <x:BoundField Width="100px" DataField="v_UpdateUser" DataFormatString="{0}" HeaderText="Usuario Act." />
                        <x:BoundField Width="160px" DataField="d_UpdateDate" DataFormatString="{0}" HeaderText="Fecha Act." />
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />

        <x:Window ID="winEdit" Title="Nuevo Nodo" Popup="false" EnableIFrame="true" runat="server" Icon="Pencil"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" OnClose="winEdit_Close" IsModal="True" Width="750px" Height="300px" >
        </x:Window>

        <x:Window ID="winEdit1" Title="Nuevo Organización" Popup="false" EnableIFrame="true" runat="server" Icon="House"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" IsModal="True" Width="800px" Height="550px">
        </x:Window>

          <x:Window ID="winEditNodeRol" Title="Nuevo Nodo Rol" Popup="false" EnableIFrame="true" runat="server" Icon="ComputerKey"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" IsModal="True" Width="700px" Height="480px" >
        </x:Window>

         <x:Window ID="WinEditNodeServiceProfile" Title="Nuevo Servicio" Popup="false" EnableIFrame="true" runat="server" Icon="ServerConnect"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" IsModal="True" Width="750px" Height="570px">
        </x:Window>

            <x:Window ID="winEditAttentionInArea" Title="Atención en Área" Popup="false" EnableIFrame="true" runat="server" Icon="ServerConnect"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" IsModal="True" Width="750px" Height="470px">
        </x:Window>

        <x:Window ID="winEditAlmacenFarmacia" Title="Almacén de Farmacia" Popup="false" EnableIFrame="true" runat="server" Icon="ShapeMoveForwards"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" IsModal="True" Width="800px" Height="310px">
        </x:Window>

    </form>
</body>
</html>
