<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM033.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.ExternalUser.FRM031_" %>

<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Administración de Servicios</title>
    <link href="../CSS/main.css" rel="stylesheet" />
    <style>
        .StylelblContador {
                color: #0026ff;
                font-weight: bold;
                font-size: 11px;
                text-decoration: none;
        }
        #form1 {
            max-height: 150px !important;
            overflow: hidden;
        }
        #cont-table {
            width: 100%;
            overflow: scroll;
            max-height: 500px;
        }
        .noWrap {
            white-space: nowrap;
        }

    </style>
</head>
<body>

   <form id="form1" runat="server">      
       
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Administrador de Servicios" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
            <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="80" >                
                <Items>
                    <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                            <Rows>
                                <x:FormRow ID="FormRow1" ColumnWidths="460px 460px 100px 180px" runat="server">
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
                                        <x:Form ID="Form7"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="70px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="460px" ID="FormRow7" runat="server" >
                                                    <Items>
                                                        <x:DropDownList ID="ddlEmpresa" OnSelectedIndexChanged="SetValue" AutoPostBack="false" Label="Empresa" runat="server" />
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                        
                                        <x:Button runat="server" ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" CssClass="inline"  ValidateForms="Form2" ></x:Button> 
                                        <x:Button runat="server" ID="btnExportar" Text="Exportar Excel"  Icon="PageExcel" IconAlign="Left" ></x:Button>
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
            
        </Items>
         
    </x:Panel>
       
    <x:HiddenField ID="hfRefresh" runat="server" />

<%--    <x:Window ID="winEdit1" Title="Certificado(s)" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HidePostBack" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="winEdit1_Close"  IsModal="true"  Height="630px" Width="700px" >
    </x:Window>

    <x:Window ID="winEdit2" Title="Ficha Ocupacional" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HideRefresh" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="winEdit2_Close"  IsModal="true"  Height="245px" Width="245px" >
    </x:Window>

    <x:Window ID="Window2" Title="Informe Médico" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HidePostBack" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="Window2_Close"  IsModal="true"  Height="630px" Width="700px" >
    </x:Window>

    <x:Window ID="winEdit3" IconUrl="~/images/16/11.png" runat="server" Popup="false"
        IsModal="true" Target="Parent" EnableMaximize="false" EnableResize="false"
        Title="Lista de Examenes" EnableConfirmOnClose="true" CloseAction="HidePostBack"
        EnableIFrame="true" IFrameUrl="about:blank" Height="600px" Width="350px"  OnClose="winEdit3_Close">
    </x:Window>

    <x:Window ID="Window1" IconUrl="~/images/16/11.png" runat="server" Popup="false"
        IsModal="true" Target="Parent" EnableMaximize="false" EnableResize="false"
        Title="Ver Archivos Adjuntos" EnableConfirmOnClose="true" CloseAction="HidePostBack"
        EnableIFrame="true" IFrameUrl="about:blank" Height="210px" Width="300px"  OnClose="Window1_Close">
    </x:Window>--%>
    </form>
<div id="cont-table">
    <table id="tabla-exportar" class="table table-bordered">
        <thead>
            <tr id="cabecera-tabla">
            </tr>
        </thead>
        <tbody id="cuerpo-tabla">

        </tbody>
    </table>
</div>
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />


    <script>
        var btnFilterClientID = '<%= btnFilter.ClientID %>';
        

        $(document).ready(function () {
            
            $("#form1").on('submit', function (evt) {
                evt.preventDefault();
            });

            var btnFilterClientID = '<%= btnFilter.ClientID %>';
            

            

            $("#" + btnFilterClientID).on("click", function () {
               
                var dpFechaInicioClientID = '<%= dpFechaInicio.ClientID %>';
                var dpFechaFinClientID = '<%= dpFechaFin.ClientID %>';
                var ddlEmpresaClientID = '<%= ddlEmpresa.ClientID %>';
                var fechaInicio = document.getElementById(dpFechaInicioClientID).value;
                var fechaFin = document.getElementById(dpFechaFinClientID).value;
                var organizationId = document.getElementById(ddlEmpresaClientID).parentElement.firstChild.value;
                console.log(organizationId);
                var actionData = "{'FechaInicio': '" + fechaInicio + "','FechaFin': '" + fechaFin + "','OrganizationId': '" + organizationId + "'}";
                $.ajax({
                    url: 'FRM031.aspx/ImprimirTabla',
                    type: 'POST',
                    async: false,
                    cache: false,
                    data: actionData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",                    
                    success: function(json) {
                        SetDataTable(json.d);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.error("ERROR", jqXHR, textStatus, errorThrown);
                    }
                })
            })
        })
        

        function SetDataTable(valuesBoard) {
            var values = valuesBoard.ListaMatriz;
            var ordenReporte = valuesBoard.ListaOrdenReporte;
            console.log(valuesBoard);

            var arrKeys = [];

            for (var a = 0; a < ordenReporte.length; a++) {
                arrKeys.push(ordenReporte[a].v_ColumnaId);
            }
            console.log(arrKeys);
            if (values.length > 0) {
                var html = "";
                var arrHtmlCabecera = []
                for (var key in values[0]) {
                    if (arrKeys.indexOf(key) !== -1) {
                        arrHtmlCabecera.push('<th id=' + key + ' scope="col">' + key + '</th>');
                    }                    
                }


                var rows = "";
                for (var i = 0; i < values.length; i++) {

                    rows += "<tr>";

                    for (var column in values[i]) {

                        if (arrKeys.indexOf(column) !== -1) {
                            if (column == "FechaNacimiento" || column == "d_Fur" || column == "FechaExamen" || column == "FechaSegundaProgramacion" || column == "FechaProgramacion" || column == "FechaDeVencimiento") {
                                if (values[i][column] != null) {
                                    var year = new Date(parseInt(values[i][column].substr(6))).getFullYear();
                                    var month = new Date(parseInt(values[i][column].substr(6))).getMonth();
                                    var day = new Date(parseInt(values[i][column].substr(6))).getDate();
                                    values[i][column] = day + "/" + month + "/" + year;
                                }

                            }
                            if (values[i][column] == "null" || values[i][column] == null) {
                                values[i][column] = "";
                            }
                            rows += '<td nowrap>' + values[i][column] + '</td>';
                        }

                        
                        
                    }
                    rows += "</tr>";
                }

                $("#cabecera-tabla").html(arrHtmlCabecera.join(" "));

                $("#cuerpo-tabla").html(rows);

            }



            var btnExportarClientID = '<%= btnExportar.ClientID %>';
            $("#" + btnExportarClientID).on("click", function() {
                exportTableToExcel("tabla-exportar");
            })
        }

        function exportTableToExcel(tableID) {
            var filename = "";
            var downloadLink;
            var dataType = 'application/vnd.ms-excel';
            var tableSelect = document.getElementById(tableID);
            var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');
            
            // Specify file name
            filename = filename?filename+'.xls':'matriz_data.xls';
    
            // Create download link element
            downloadLink = document.createElement("a");
    
            document.body.appendChild(downloadLink);
    
            if(navigator.msSaveOrOpenBlob){
                var blob = new Blob(['ufeff', tableHTML], {
                    type: dataType
                });
                navigator.msSaveOrOpenBlob( blob, filename);
            }else{
                // Create a link to the file
                downloadLink.href = 'data:' + dataType + ', ' + tableHTML;
    
                // Setting the file name
                downloadLink.download = filename;
        
                //triggering the function
                downloadLink.click();
            }
        }
    </script>
</body>
</html>
