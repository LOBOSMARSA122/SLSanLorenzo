<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMUSUARIOSEXTERNOS.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRMUSUARIOSEXTERNOS" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../CSS/main.css" rel="stylesheet" />
     <style>
        .datecontainer .x-form-field-trigger-wrap 
        {
            margin-right: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <x:Panel ID="Panel1" runat="server" ShowBorder="True" ShowHeader="True" Title="Administración de Usuarios Externos" EnableBackgroundColor="true"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5">
            <Items>
                <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="74">
                    <Items>
                        <x:SimpleForm ID="Form5" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server" >
                            <Items>
                                <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item datecontainer" ShowBorder="false" EnableBackgroundColor="true" Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label1" Width="50px" runat="server" CssClass="inline" ShowLabel="false" Text="Usuario Externo:" />
                                        <x:TextBox ID="txtUserNameFilter" runat="server" />
                                        <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask"
                                            OnClick="btnFilter_Click">
                                        </x:Button>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:SimpleForm>
                    </Items>
                </x:GroupPanel>
                <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" Title="Administración de Usuarios Externos" runat="server"
                    PageSize="30" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grdData_PageIndexChange"
                    IsDatabasePaging="true" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                    EnableMouseOverColor="true" ShowGridHeader="true"  DataKeyNames="i_SystemUserId"
                    EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" OnPreRowDataBound="grdData_PreRowDataBound">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Button ID="btnNew" Text="Nuevo Usuario Externo" Icon="Add" runat="server">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>
                    <Columns>
                        <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEditUser" HeaderText=""
                            Icon="Pencil" ToolTip="Editar Usuario" DataTextFormatString="{0}"
                            DataIFrameUrlFields="i_SystemUserId,v_PersonId" DataIFrameUrlFormatString="FRMUSUARIOSEXTERNOS_ADD.aspx?Mode=Edit&systemUserId={0}&personId={1}"
                            DataWindowTitleField="i_SystemUserId" DataWindowTitleFormatString="Editar Usuario" />                     

                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Usuario Externo" CommandName="DeleteAction" />
                        <x:BoundField Width="100px" DataField="i_SystemUserId" DataFormatString="{0}" HeaderText="Usuario Id" />
                        <x:BoundField Width="100px" DataField="v_PersonId" DataFormatString="{0}" HeaderText="Persona Id" Hidden="true" />
                        <x:BoundField Width="200px" DataField="v_UserName" DataFormatString="{0}" HeaderText="Usuario" />
                        <x:BoundField Width="100px" DataField="v_InsertUser" DataFormatString="{0}" HeaderText="Usuario Crea" />
                        <x:BoundField Width="160px" DataField="d_InsertDate" DataFormatString="{0}" HeaderText="Fecha Crea. " />
                        <x:BoundField Width="100px" DataField="v_UpdateUser" DataFormatString="{0}" HeaderText="Usuario Act." />
                        <x:BoundField Width="160px" DataField="d_UpdateDate" DataFormatString="{0}" HeaderText="Fecha Act." />
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />

        <x:Window ID="winEditUser" Title="Nuevo Usuario" Popup="false" EnableIFrame="true" runat="server" Icon="Pencil"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" OnClose="winEditUser_Close" IsModal="True" Width="750px" Height="620px">
        </x:Window>

      
    </form>
</body>
</html>
