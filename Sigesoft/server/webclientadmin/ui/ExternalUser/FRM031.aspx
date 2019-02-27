<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM031.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.ExternalUser.FRM031" %>

<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración de Servicios</title>
    <link href="../CSS/main.css" rel="stylesheet" />
    <style>
        .StylelblContador {
                color: #0026ff;
                font-weight: bold;
                font-size: 11px;
                text-decoration: none;
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
                                                        <x:DropDownList ID="ddlAptitud" Label="Aptitud" runat="server" />
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                         <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click" ValidateForms="Form2" ></x:Button>                         
                                    </Items>
                                </x:FormRow>
                                <x:FormRow ID="FormRow2" ColumnWidths="460px 460px"  runat="server">
                                    <Items>
                                        <x:Form ID="Form4"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="460px" ID="FormRow3" runat="server" >
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
                                                         <x:DropDownList ID="ddlEmpresa" Label="Empresa" runat="server" />
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                       
                                    </Items>
                                </x:FormRow>
                                <x:FormRow ColumnWidths="460px 460px" ID="FormRow1"  runat="server">
                                    <Items>
                                        <x:Form ID="Form5"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="230px 230px" ID="FormRow4" runat="server" >
                                                    <Items>
                                                       <x:TextBox ID="txtHCL" Label="Nro. HCL" runat="server"/>
                                                        <x:Label runat="server" Text=""></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 

                                          <x:Form ID="Form8"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="70px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="460px" ID="FormRow8" runat="server" >
                                                    <Items>                                                        
                                                         <x:DropDownList ID="ddlProtocolo"  Label="Protocolo" runat="server"  />  <%--CompareValue="-1" CompareOperator="NotEqual" CompareMessage="Campo requerido"--%>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                        
                                    </Items>
                                </x:FormRow>
                                <x:FormRow  ColumnWidths="840px 100px" ID="FormRow6"  runat="server">
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
            EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_ServiceId,v_IdTrabajador,EmpresaCliente,v_Trabajador,Dni,i_SendToTracking,Apellidos" 
            EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="true" BoxFlex="2" BoxMargin="5" 
            OnRowCommand="grdData_RowCommand"  OnRowClick="grdData_RowClick" EnableRowClick="true">
                <Toolbars>
                    <x:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <x:Button ID="btnNewCertificado" Text="Certificado" Icon="PageWhiteText" runat="server" Enabled="false"></x:Button>
                            <x:Button ID="btnExAltura" Text="Test de Altura" Icon="PageWhiteText" runat="server" Enabled="false"></x:Button>
                            <x:Button ID="btnPsico" Text="Psicologia" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                            <x:Button ID="btnToxi" Text="Toxicológico" Icon="Folder" runat="server" Enabled="false"></x:Button>
                            <x:Button ID="btnNewFichaOcupacional" Text="Ficha Ocupacional" Icon="clipboard" runat="server" Enabled="false" ></x:Button>
                            <x:Button ID="btnNewExamenes" Text="Examenes" Icon="PageWhiteStack" runat="server" Enabled="false" Visible="false"></x:Button>
                            <x:Button ID="btnFMT1" Text="Informe Médico" Icon="FilmAdd" runat="server" Enabled="false" ></x:Button>
                            <x:Button ID="btnInterConsulta" Text="Interconsulta" Icon="FilmEject" runat="server" Enabled="false" ></x:Button>
                        </Items>
                    </x:Toolbar>
                </Toolbars>
                <Columns>
                    <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="Window1" HeaderText=""
                        Icon="attach" ToolTip="Archivos Adjuntos" DataTextFormatString="{0}" 
                        DataIFrameUrlFields="Dni,Apellidos" DataIFrameUrlFormatString="FRM031I.aspx?Dni={0}&&Apellidos={1}" 
                        DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Archivos Adjuntos" />
                    <x:boundfield Width="140px" DataField="v_ServiceId" DataFormatString="{0}" HeaderText="Id Atencion" />
                    <x:boundfield Width="270px" DataField="v_Trabajador" DataFormatString="{0}" HeaderText="Trabajador" />
                    <x:boundfield Width="150px" DataField="d_ServiceDate" DataFormatString="{0:d}" HeaderText="Fecha" />
                    <x:boundfield Width="200px" DataField="v_AptitudeStatusName" DataFormatString="{0}" HeaderText="Aptitud" />
                    <%--<x:boundfield Width="400px" DataField="v_Restricction" DataFormatString="{0}" HeaderText="Restricciones" />--%>
                    <x:boundfield Width="250px" DataField="v_ProtocolName" DataFormatString="{0}" HeaderText="Protocolo" />                   
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />

    <x:Window ID="winEdit1" Title="Certificado(s)" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
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
    </x:Window>
    </form>
</body>
</html>
