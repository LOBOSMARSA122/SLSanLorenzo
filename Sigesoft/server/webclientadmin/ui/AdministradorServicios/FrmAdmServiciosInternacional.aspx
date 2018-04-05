<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmAdmServiciosInternacional.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Administrador_Servicios.FrmAdmServiciosInternacional" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title></title>
      <link href="../css/main.css" rel="stylesheet" type="text/css" />
      <style type="text/css">
        .highlight
        {
            background-color: lightgreen;
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
                                    <x:FormRow ID="FormRow1" ColumnWidths="460px 460px 100px" runat="server">
                                        <Items> 
                                            <x:Form ID="Form3"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow2" ColumnWidths="230px 230px" runat="server" >
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
                                                            <x:DropDownList ID="ddlAptitud" Label="Aptitud" runat="server" />
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
                                                             <x:DropDownList ID="ddlEmpresa" Label="Empresa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" />
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
                                                            <x:Label ID="Label1" runat="server" Text=""></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                </Rows>
                                            </x:Form> 

                                              <x:Form ID="Form8"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="70px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ColumnWidths="460px" ID="FormRow9" runat="server" >
                                                        <Items>                                                        
                                                             <x:DropDownList ID="ddlProtocolo"  Label="Protocolo" runat="server" />  
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
                    EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_ServiceId,v_IdTrabajador,EmpresaCliente,v_Pacient,Dni,i_StatusLiquidation,i_AptitudeStatusId" 
                    EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="true" BoxFlex="2" BoxMargin="5" 
                    OnRowCommand="grdData_RowCommand"  OnRowClick="grdData_RowClick" EnableRowClick="true" OnRowDataBound="grdData_RowDataBound"> 
                    <Toolbars>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Button ID="btnCambiarFechaServicio" Text="Cambiar Fecha Servicio" Icon="CalendarViewDay" runat="server" ></x:Button>
                                <x:Button ID="btnReporte" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>               
                    <Columns>
                        <x:LinkButtonField TextAlign="Center" ConfirmText="Se generará los reportes de este servicio.¿Está seguro de generar los reportes?" Icon="FolderDatabase" ConfirmTarget="Top"
                        ColumnID="lbfAction2" Width="30px" ToolTip="Generar Reportes" CommandName="GenerarReportes" />                      
                        <x:boundfield Width="140px" DataField="v_ServiceId" DataFormatString="{0}" HeaderText="Id Atencion" />
                        <x:boundfield Width="270px" DataField="v_Pacient" DataFormatString="{0}" HeaderText="Trabajador" />
                        <x:boundfield Width="150px" DataField="d_ServiceDate" DataFormatString="{0:d}" HeaderText="Fecha" />
                        <x:boundfield Width="200px" DataField="v_AptitudeStatusName" DataFormatString="{0}" HeaderText="Aptitud" />
                        <x:boundfield Width="400px" DataField="v_Restricction" DataFormatString="{0}" HeaderText="Restricciones" />
                        <x:boundfield Width="250px" DataField="v_ProtocolName" DataFormatString="{0}" HeaderText="Protocolo" />                   
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />
           <x:Window ID="WinFechaServicio" Title="Cambiar Fecha Servicio" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="WinFechaServicio_Close" IsModal="True" Width="450px" Height="270px" >
    </x:Window>
          <x:Window ID="winEdit1" Title="Reporte" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HidePostBack" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top"  IsModal="true"  Height="630px" Width="700px" OnClose="winEdit1_Close" >
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
