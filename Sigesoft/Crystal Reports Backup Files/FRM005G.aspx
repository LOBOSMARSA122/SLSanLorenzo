<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005G.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.FRM005G" %>
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
                        <x:GroupPanel runat="server" Title="Resultados de la Busqueda" ID="GroupPanel1" EnableBackgroundColor="True" AutoWidth="true">
                            <Items>
                                <x:Grid ID="grd" ShowBorder="true" ShowHeader="false" runat="server"
                                    PageSize="30" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grd_PageIndexChange"
                                    IsDatabasePaging="false" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_AttentionInAreaId"
                                    EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5"
                                    OnRowCommand="grd_RowCommand" Height="350px" Width="740px">
                                    <Toolbars>
                                        <x:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <x:Button ID="btnNew" Text="Nueva Area de Atención" Icon="Add" runat="server">
                                                </x:Button>
                                            </Items>
                                        </x:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                            Icon="Pencil" ToolTip="Editar Area de Atención" DataTextFormatString="{0}"
                                            DataIFrameUrlFields="v_AttentionInAreaId"
                                             DataIFrameUrlFormatString="FRM005H.aspx?Mode=Edit&v_AttentionInAreaId={0}"
                                            DataWindowTitleField="v_AttentionInAreaId" DataWindowTitleFormatString="Editar Area de Atención" />
                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar" CommandName="DeleteAction" />
                                         <x:BoundField Width="100px" DataField="v_AttentionInAreaId" DataFormatString="{0}" HeaderText="ID" />
                                         <x:BoundField Width="170px" DataField="v_Name" DataFormatString="{0}" HeaderText="Área" />
                                        <x:BoundField Width="190px" DataField="v_OfficeNumber" DataFormatString="{0}" HeaderText="Número Oficina" />
                                        <x:boundfield Width="100px" DataField="v_CreationUser" DataFormatString="{0}" HeaderText="Usuario Crea." />
                                        <x:boundfield Width="100px" DataField="d_CreationDate" DataFormatString="{0}" HeaderText="Fecha Crea" />
                                        <x:boundfield Width="100px" DataField="v_UpdateUser" DataFormatString="{0}" HeaderText="Usuario Act." />
                                        <x:boundfield Width="100px" DataField="d_UpdateDate" DataFormatString="{0}" HeaderText="Fecha Act." />   
                                    </Columns>
                                </x:Grid>
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />

        <x:Window ID="winEdit" Title="Nueva Area de Atención" Popup="false" EnableIFrame="true" runat="server" Icon="DatabaseAdd"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="true"
            Target="Top" OnClose="winEdit_Close" IsModal="True" Width="400px" Height="480px">
        </x:Window>
    </form>
</body>
</html>
