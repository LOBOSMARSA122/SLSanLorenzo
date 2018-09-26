<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005D.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.FRM005D" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel2" EnableBackgroundColor="true"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:GroupPanel runat="server" Title="Roles agregados al nodo" ID="GroupPanel1" EnableBackgroundColor="True">
                            <Items>
                                <x:Grid ID="grd" ShowBorder="true" ShowHeader="false" runat="server"
                                    PageSize="30" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grd_PageIndexChange"
                                    IsDatabasePaging="false" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="i_NodeId,i_RoleId"
                                    EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5"
                                    OnRowCommand="grd_RowCommand" Height="350px" Width="650px">
                                    <Toolbars>
                                        <x:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <x:Button ID="btnNew" Text="Nuevo Rol" Icon="Add" runat="server">
                                                </x:Button>
                                            </Items>
                                        </x:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                            Icon="Pencil" ToolTip="Editar Rol" DataTextFormatString="{0}"
                                            DataIFrameUrlFields="i_NodeId,i_RoleId" DataIFrameUrlFormatString="FRM005E.aspx?Mode=Edit&&nodeId={0}&&roleId={1}"
                                            DataWindowTitleField="v_RoleName" DataWindowTitleFormatString="Editar permisos del Rol: {0}" />
                                        <x:WindowField ColumnID="myWindowField1" Width="30px" WindowID="winEditRoleNodeComponent" HeaderText=""
                                            Icon="PluginAdd" ToolTip="Editar Componente" DataTextFormatString="{0}"
                                            DataIFrameUrlFields="i_NodeId,i_RoleId,v_RoleName" DataIFrameUrlFormatString="FRM005DA.aspx?Mode=Edit&&nodeId={0}&&roleId={1}&&roleName={2}"
                                            DataWindowTitleField="v_RoleName" DataWindowTitleFormatString="Editar Componentes del Nodo / Rol {0}" />
                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar" CommandName="DeleteAction" />
                                        <x:BoundField Width="550px" DataField="v_RoleName" DataFormatString="{0}" HeaderText="Roles" />  
                                        <x:BoundField Width="550px" DataField="v_NodeName" DataFormatString="{0}" HeaderText="Nodo" />                                        
                                    </Columns>
                                </x:Grid>
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />

        <x:Window ID="winEdit" Title="Nuevo permiso de Rol" Popup="false" EnableIFrame="true" runat="server" Icon="DatabaseAdd"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" OnClose="winEdit_Close" IsModal="True" Width="650px" Height="600px">
        </x:Window>
         <x:Window ID="winEditRoleNodeComponent" Title="Nuevo permiso de Rol" Popup="false" EnableIFrame="true" runat="server" Icon="DatabaseAdd"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" OnClose="winEdit_Close" IsModal="True" Width="650px" Height="540px">
        </x:Window>
    </form>
</body>
</html>

