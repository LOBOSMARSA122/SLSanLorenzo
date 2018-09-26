<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="FRM011.aspx.cs" 
    Inherits="Sigesoft.Server.WebClientAdmin.UI.FRM011" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración de Data Jerárquica</title>
     <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Administrador Data Jerárquica" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
            <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="74" >                
                <Items>
                     <x:SimpleForm  ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server">
                        <Items>
                            <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item" ShowBorder="false" EnableBackgroundColor="true" Layout="Column"  runat="server">
                                <Items>
                                    <x:Label ID="Label1" Width="100px" runat="server" CssClass="inline" ShowLabel="false" Text="Parámetro Id:" />
                                    <x:NumberBox ID="txtParameterIdFilter" Label="Item Id" runat="server"  NoDecimal="True" NoNegative="True" CssClass="mrightmayus"/>
                                    <x:Label ID="Label2" Width="60px" runat="server" CssClass="inline" ShowLabel="false" Text="Valor 1:" />
                                     <x:TextBox ID="txtDescriptionFilter" Label="Valor 1" runat="server" CssClass="mrightmayus"/>
                                     <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click" ></x:Button>
                                </Items>
                            </x:Panel>
                        </Items>
                    </x:SimpleForm>
                </Items>

            </x:GroupPanel>
            <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" 
            PageSize="15" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grdData_PageIndexChange"
            IsDatabasePaging="true" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
            EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="i_GroupId,i_ItemId" 
            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" OnRowCommand="grdData_RowCommand" OnPreRowDataBound="grdData_PreRowDataBound">
                <Toolbars>
                    <x:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <x:Button ID="btnNew" Text="Nuevo Grupo" Icon="Add" runat="server">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Toolbars>
                <Columns>
                    <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                        Icon="Pencil" ToolTip="Editar Grupo" DataTextFormatString="{0}" 
                        DataIFrameUrlFields="i_GroupId,i_ItemId" DataIFrameUrlFormatString="FRM011A.aspx?Mode=Edit&i_GroupId={0}&i_ItemId={1}" 
                        DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Editar Grupo {0}" />
                    <x:WindowField ColumnID="myWindowFieldChildren" Width="25px" WindowID="winViewChildren" HeaderText=""
                        Icon="ApplicationViewDetail" ToolTip="Ver Items de Grupo" DataTextFormatString="{0}" 
                        DataIFrameUrlFields="i_GroupId,i_ItemId" DataIFrameUrlFormatString="FRM011B.aspx?Mode=Edit&i_GroupId={0}&i_ItemId={1}" 
                        DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Items  Grupo {0}" />
                    <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                        ColumnID="lbfAction2" Width="25px" ToolTip="Eliminar Grupo" CommandName="DeleteAction" />
                    <x:boundfield Width="65px" DataField="i_GroupId" DataFormatString="{0}" HeaderText="Grupo Id" />
                    <x:boundfield Width="90px" DataField="i_ItemId" DataFormatString="{0}" HeaderText="Item Id" />
                    <x:boundfield Width="220px" DataField="v_Value1" DataFormatString="{0}" HeaderText="Valor 1" />
                    <x:boundfield Width="100px" DataField="v_CreationUser" DataFormatString="{0}" HeaderText="Usuario Crea." />
                    <x:boundfield Width="100px" DataField="d_CreationDate" DataFormatString="{0}" HeaderText="Fecha Crea" />
                    <x:boundfield Width="100px" DataField="v_UpdateUser" DataFormatString="{0}" HeaderText="Usuario Act." />
                    <x:boundfield Width="100px" DataField="d_UpdateDate" DataFormatString="{0}" HeaderText="Fecha Act." />                    
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />

    <x:Window ID="winEdit" Title="Nuevo Grupo / Item" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="450px" Height="320px" >
    </x:Window>

    <x:Window ID="winViewChildren" Title="Editar Item de Grupo" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winViewChildren_Close" IsModal="True" Width="650px" Height="390px" >
    </x:Window>
    </form>
</body>
</html>
