<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMAgenda.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.ExternalUser.FRMAgenda" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/main.css" rel="stylesheet" />
    <style type="text/css">
        .highlight
        {
            background-color: darkgray;
        }
        .highlight .x-grid3-col
        {
            background-image: none;
        }
        
        .x-grid3-row-selected .highlight
        {
            background-color: yellow;
        }
        .x-grid3-row-selected .highlight .x-grid3-col
        {
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
         <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Administrador de Servicios" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
            <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="132" >                
                <Items>
                    <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                            <Rows>
                                <x:FormRow ColumnWidths="460px 460px 100px" runat="server">
                                    <Items> 
                                        <x:Form ID="Form3"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="230px 230px" runat="server" >
                                                    <Items>
                                                        <x:DatePicker ID="dpFechaInicio" Label="Atenciones del" Width="120px" runat="server" DateFormatString="dd/MM/yyyy" />
                                                        <x:DatePicker ID="dpFechaFin" Label="Al"  runat="server" Width="120px" DateFormatString="dd/MM/yyyy" />                                       
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form6"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="70px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="215px 245px"  ID="FormRow5" runat="server" >
                                                    <Items>
                                                         <x:DropDownList ID="ddlTipoESO" Label="Tipo ESO" runat="server" />
                                                        <x:DropDownList ID="ddlAptitud" Label="Aptitud" runat="server"  Hidden="true"/>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                         <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click" ValidateForms="Form2" ></x:Button>                         
                                    </Items>
                                </x:FormRow>
                                <x:FormRow ID="FormRow3" ColumnWidths="460px 460px"  runat="server">
                                    <Items>
                                        <x:Form ID="Form4"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="460px" ID="FormRow4" runat="server" >
                                                    <Items>
                                                        <x:TextBox ID="txtTrabajador" Label="Nombre" runat="server"/>  
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                        <x:Form ID="Form7"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="70px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="460px" ID="FormRow7" runat="server" >
                                                    <Items>
                                                          <x:DropDownList ID="ddlEmpresa"  Label="Empresa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged"  Enabled="false"/>  
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                       
                                    </Items>
                                </x:FormRow>
                                <x:FormRow ColumnWidths="460px 460px" ID="FormRow6"  runat="server">
                                    <Items>
                                        <x:Form ID="Form5"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="230px 230px" ID="FormRow8" runat="server" >
                                                    <Items>
                                                       <x:TextBox ID="txtHCL" Label="Nro. HCL" runat="server"/>
                                                        <x:Label runat="server" Text=""></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 

                                          <x:Form ID="Form8"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="70px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="460px" ID="FormRow9" runat="server" >
                                                    <Items>                                                        
                                                         <x:DropDownList ID="ddlProtocolo"  Label="Protocolo" runat="server" AutoPostBack="true"/>  
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                        
                                    </Items>
                                </x:FormRow>
                                <x:FormRow  ColumnWidths="840px 100px" ID="FormRow10"  runat="server">
                                    <Items>
                                            <x:Label runat="server" ID="lblContador" Text="Se encontraron 0 registros" Width="800px" CssClass="StylelblContador"></x:Label>                                         
                                    </Items> 
                                </x:FormRow>
                            </Rows>
                    </x:Form>
                </Items>
            </x:GroupPanel>
            <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" 
             EnableRowNumber="True" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
            EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_ServiceId,v_CalendarId,v_ProtocolId,v_PersonId" 
            EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="true" BoxFlex="2" BoxMargin="5" 
            OnRowCommand="grdData_RowCommand"  OnRowClick="grdData_RowClick" EnableRowClick="true" OnRowDataBound="grdData_RowDataBound">
                <Toolbars>
                    <x:Toolbar ID="Toolbar1" runat="server">
                        <Items>     
                            <x:Button ID="btnHojaRuta" Text="Imprimir Hoja de Ruta" Icon="Printer" runat="server" Enabled="true"></x:Button>
                            <x:Button ID="btnReagendar" Text="Reagendar" Icon="Book" runat="server" Enabled="true"></x:Button>
                            <x:Button ID="btnEliminarAgenda" Text="Cancelar Agenda" Icon="Delete" runat="server" Enabled="true"></x:Button>
                        </Items>
                    </x:Toolbar>
                </Toolbars>
                <Columns>
                    <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                    Icon="UserSuit" ToolTip="Foto del Paiente" DataTextFormatString="{0}" 
                    DataIFrameUrlFields="v_PersonId" DataIFrameUrlFormatString="FRM032A.aspx?PersonId={0}" 
                    DataWindowTitleField="v_Pacient" DataWindowTitleFormatString="Paciente: {0}" />    
                        
                      <x:boundfield Width="150px" DataField="d_DateTimeCalendar" HeaderText="Fecha Hora Agenda" />
                    <x:boundfield Width="100px" DataField="d_EntryTimeCM" DataFormatString="{0:hh:mm tt}" HeaderText="Hora Ingreso" />
                    <x:boundfield Width="100px" DataField="d_SalidaCM" DataFormatString="{0:hh:mm tt}" HeaderText="Hora Salida" />
                    <x:boundfield Width="290px" DataField="v_Pacient" DataFormatString="{0}" HeaderText="Trabajador" />
                    <x:boundfield Width="110px" DataField="v_NewContinuationName" DataFormatString="{0}" HeaderText="Modalidad" />
                    <x:boundfield Width="100px" DataField="v_LineStatusName" DataFormatString="{0}" HeaderText="Estado cola" />
                    <x:boundfield Width="250px" DataField="v_CalendarStatusName" DataFormatString="{0}" HeaderText="Estado Cita" />        
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    <x:HiddenField ID="hfRefresh" runat="server" />

    <x:Window ID="winEdit" Title="Foto" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" IsModal="True" Width="340px" Height="295px" >
        </x:Window>

    <x:Window ID="winEdit1" Title="Hoja(s) de Ruta(s)" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HidePostBack" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="winEdit1_Close"  IsModal="true"  Height="630px" Width="700px" >
    </x:Window>

    <x:Window ID="Window1" Title="Aptitud" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="Window1_Close" IsModal="True" Width="490px" Height="250px" >
    </x:Window>

    <x:Window ID="Window2" Title="Cancelar Agenda" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="Window1_Close" IsModal="True" Width="490px" Height="100px" >
    </x:Window>

    <x:HiddenField ID="highlightRows" runat="server"></x:HiddenField>
    </form>
        <script type="text/javascript">
            var highlightRowsClientID = '<%= highlightRows.ClientID %>';
            var gridClientID = '<%= grdData.ClientID %>';

            function highlightRows() {
                var highlightRows = X(highlightRowsClientID);
                var grid = X(gridClientID);

                grid.el.select('.x-grid3-row table.highlight').removeClass('highlight');

                Ext.each(highlightRows.getValue().split(','), function (item, index) {
                    if (item !== '') {
                        var row = grid.getView().getRow(parseInt(item, 10));
                        Ext.get(row).first().addClass('highlight');
                    }
                });

            }


            function onReady() {
                var grid = X(gridClientID);
                grid.addListener('viewready', function () {
                    highlightRows();
                });
            }


            function onAjaxReady() {
                highlightRows();
            }
    </script>
</body>
</html>
