<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005DA.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.FRM005DA" %>
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
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel2" EnableBackgroundColor="true"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:GroupPanel runat="server" Title="Datos del Nodo / Rol" ID="GroupPanel3" EnableBackgroundColor="True">
                            <Items>
                                <x:SimpleForm ID="SimpleForm5" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="True"
                                    AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True" LabelWidth="75px">
                                    <Items>
                                       <x:TextBox ID="txtRoleNode" Label="Nodo / Rol" runat="server" CssClass="mrightmayus" Enabled="false" />
                                    </Items>
                                </x:SimpleForm>
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Componentes agregados" ID="GroupPanel1" EnableBackgroundColor="True">
                            <Items>
                                <x:Grid ID="grd" ShowBorder="true" ShowHeader="false" runat="server"
                                    PageSize="30" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grd_PageIndexChange"
                                    IsDatabasePaging="false" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_RoleNodeComponentId,i_NodeId,i_RoleId"
                                    EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5"
                                    OnRowCommand="grd_RowCommand" Height="350px" Width="600px">
                                    <Toolbars>
                                        <x:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <x:Button ID="btnNew" Text="Nuevo Componente" Icon="Add" runat="server">
                                                </x:Button>
                                            </Items>
                                        </x:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEditRoleNodeComponent" HeaderText=""
                                            Icon="Pencil" ToolTip="Editar Rol / Componente" DataTextFormatString="{0}"
                                            DataIFrameUrlFields="v_RoleNodeComponentId" DataIFrameUrlFormatString="FRM005DAA.aspx?Mode=Edit&&roleNodeComponentId={0}"
                                            DataWindowTitleField="v_RoleName" DataWindowTitleFormatString="Editar Componentes del Rol: {0}" />                                      
                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar" CommandName="DeleteAction" />
                                        <x:BoundField Width="200px" DataField="v_ComponentName" DataFormatString="{0}" HeaderText="Examen" />
                                        <x:BoundField Width="70px" DataField="v_Read" DataFormatString="{0}" HeaderText="Lectura" />
                                        <x:BoundField Width="70px" DataField="v_Write" DataFormatString="{0}" HeaderText="Escritura" /> 
                                        <x:BoundField Width="70px" DataField="v_Dx" DataFormatString="{0}" HeaderText="Diagnostico." /> 
                                        <x:BoundField Width="70px" DataField="v_Approved" DataFormatString="{0}" HeaderText="Aprobado" />                                      
                                    </Columns>
                                </x:Grid>
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />
 
         <x:Window ID="winEditRoleNodeComponent" Title="Nuevo permiso de Nodo Rol / Componente" Popup="false" EnableIFrame="true" runat="server" Icon="DatabaseAdd"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" OnClose="winEditRoleNodeComponent_Close" IsModal="True" Width="500px" Height="330px">
        </x:Window>
    </form>
</body>
</html>
