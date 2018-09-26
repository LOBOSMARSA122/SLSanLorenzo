<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM036.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRM036" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Administración de Protocolos</title>
    <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
   <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Administrador de Protocolos" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
            <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="60" >                
                <Items>
                    <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                            <Rows>
                                <x:FormRow ID="FormRow1" ColumnWidths="460px 460px 100px" runat="server">
                                    <Items> 
                                        <x:Form ID="Form3"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow2"  runat="server" >
                                                    <Items>
                                                        <x:TextBox ID="txtNombreProtocolo" Label="Nombre" runat="server"/>                   
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form6"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow  ID="FormRow5" runat="server" >
                                                    <Items>
                                                         <x:DropDownList ID="ddlEmpresaCliente" Label="Empresa Cliente" runat="server" />                                                      
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                         <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click" ValidateForms="Form2" ></x:Button>                         
                                    </Items>
                                </x:FormRow>
                            </Rows>
                    </x:Form>
                </Items>
            </x:GroupPanel>
            <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" 
            PageSize="15"  EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grdData_PageIndexChange"
             IsDatabasePaging="true" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
            EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_ProtocolId" 
            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" 
            OnRowCommand="grdData_RowCommand" >     
                <Toolbars>
                    <x:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <x:Button ID="btnNuevoProtocolo" Text="Nuevo Protocolo" Icon="Add" runat="server"></x:Button>                           
                        </Items>
                    </x:Toolbar>
                </Toolbars>     
                <Columns>
                    <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                            Icon="Pencil" ToolTip="Editar Protocolo" DataTextFormatString="{0}" 
                            DataIFrameUrlFields="v_ProtocolId" DataIFrameUrlFormatString="FRM036A.aspx?Mode=Edit&v_ProtocolId={0}" 
                            DataWindowTitleField="v_Protocol" DataWindowTitleFormatString="Editar Protocolo {0}" />
                     <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de clonar este registro?" Icon="Clock" ConfirmTarget="Top"
                        ColumnID="lbfAction3" Width="25px" ToolTip="Clonar Item" CommandName="ClonAction" />
                    <x:boundfield Width="140px" DataField="v_ProtocolId" DataFormatString="{0}" HeaderText="Id Protocolo" />
                    <x:boundfield Width="400px" DataField="v_Protocol" DataFormatString="{0}" HeaderText="Nombre Protocolo" />
                    <x:boundfield Width="400px" DataField="v_Organization" DataFormatString="{0}" HeaderText="Organización" />                              
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>    
    <x:HiddenField ID="hfRefresh" runat="server" />   

        <x:Window ID="winEdit" Title="Nuevo Protocolo" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="850px" Height="660px" >
    </x:Window>
    </form>
</body>
</html>
