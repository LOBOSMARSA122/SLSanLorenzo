<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM032.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.ExternalUser.FRM032" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../CSS/main.css" rel="stylesheet" />
    <style type="text/css">
        table.x-table-layout td
        {
            padding: 5px;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Segumiento de Trabajador" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
           <%-- GRUPO PANEL DE FILTRO--%>
            <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="85" >                
                    <Items>
                    <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                            <Rows>
                                <x:FormRow ColumnWidths=" 500px 460px 100px" ID="FormRow1" runat="server">
                                    <Items> 
                                        <x:Form ID="Form3"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="200px 300px" ID="FormRow2" runat="server" >
                                                    <Items>
                                                        <x:DatePicker ID="dpFechaInicioBus" CompareType="String" CompareValue="" CompareOperator="NotEqual" Required="true" Label="Fecha" Width="100px" runat="server" DateFormatString="dd/MM/yyyy" />
                                                       <x:DropDownList ID="ddltipoServicioBus" Label="Tipo Servicio" runat="server" Enabled="false"/>                                      
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form6"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="230px 230px" ID="FormRow5" runat="server" >
                                                    <Items>
                                                         <x:DropDownList ID="ddlColaBus" Label="Cola" runat="server" />
                                                        <x:DropDownList ID="ddlEstadoCitaBus" Label="Estado Cita" runat="server" />
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                          <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click"></x:Button>                          
                                    </Items>
                                </x:FormRow>
                                <x:FormRow ColumnWidths=" 500px 460px " ID="FormRow3" runat="server">
                                    <Items>
                                        <x:Form ID="Form4"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ColumnWidths="500px" ID="FormRow4" runat="server" >
                                                    <Items>
                                                        <x:TextBox ID="txtTrabajadorBus" Label="Nombre/DNI" runat="server"/>  
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                        <x:DropDownList ID="ddlEmpresa" Label="Empresa" runat="server" />
                                    </Items>
                                </x:FormRow>                              
                            </Rows>
                    </x:Form>
                    </Items>
            </x:GroupPanel>
           <%-----------------------------------------%>
            

           <x:Panel ID="Panel2" runat="server"   ShowBorder="true" ShowHeader="false" Title=""  EnableBackgroundColor="true" Layout="Column" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5">
            <Items>

                <%--COLUMNA 1--%>
                <x:Panel ID="Panel3" Title="Lista de Agendados" ColumnWidth="50%"  EnableBackgroundColor="true" runat="server" 
                 BodyPadding="5px" ShowBorder="true" Height="450px" BoxConfigAlign="Stretch"   ShowHeader="true" Layout="VBox" >
                    <Items>
                        <x:GroupPanel runat="server" Title="Resumen de Pacientes" ID="GroupPanel3" AutoWidth="true" BoxFlex="1" Height="58" >                
                                <Items>
                                <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                        <Rows>
                                            <x:FormRow ColumnWidths="600px" ID="FormRow14" runat="server">
                                                <Items> 
                                                    <x:Form ID="Form12"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="110px" LabelAlign="Left">
                                                        <Rows>
                                                            <x:FormRow ColumnWidths="150px 150px 150px" ID="FormRow15" runat="server" >
                                                                <Items>                                                               
                                                                    <x:TextBox ID="txtNroAtenciones" Label="En Atención" Width="25px" runat="server"/>      
                                                                     <x:TextBox ID="txtNroAtender" Label="Por Atender" Width="25px" runat="server"/> 
                                                                    <x:TextBox ID="txtNroAtendidos" Label="Atendidos" Width="25px" runat="server"/>                              
                                                                </Items>
                                                            </x:FormRow>
                                                        </Rows>
                                                    </x:Form>
                                                </Items>
                                            </x:FormRow>                                      
                                        </Rows>
                                </x:Form>
                                </Items>
                            </x:GroupPanel>
                        <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server"  AutoWidth="true" OnRowSelect="grdData_RowSelect" EnableRowSelect="true"
                        ShowGridHeader="true"  EnableRowClick="true" OnRowClick="grdData_RowClick"  DataKeyNames="v_ServiceId,v_Pacient,v_WorkingOrganizationName,v_ServiceName,v_EsoTypeName" AutoPostBack="true"
                        EnableTextSelection="true" EnableRowNumber="True" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5">   
                   
                             <Columns>   
                                <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                                Icon="UserSuit" ToolTip="Foto del Paiente" DataTextFormatString="{0}" 
                                DataIFrameUrlFields="v_PersonId" DataIFrameUrlFormatString="FRM032A.aspx?PersonId={0}" 
                                DataWindowTitleField="v_Pacient" DataWindowTitleFormatString="Paciente: {0}" />                            
                                <x:boundfield Width="100px" DataField="d_DateTimeCalendar" DataFormatString="{0:hh:mm tt}" HeaderText="Hora Ingreso" />
                                <x:boundfield Width="100px" DataField="d_SalidaCM" DataFormatString="{0:hh:mm tt}" HeaderText="Hora Salida" />
                                <x:boundfield Width="290px" DataField="v_Pacient" DataFormatString="{0}" HeaderText="Trabajador" />
                                <x:boundfield Width="110px" DataField="v_NewContinuationName" DataFormatString="{0}" HeaderText="Modalidad" />
                                <x:boundfield Width="100px" DataField="v_LineStatusName" DataFormatString="{0}" HeaderText="Estado cola" />
                                <x:boundfield Width="250px" DataField="v_CalendarStatusName" DataFormatString="{0}" HeaderText="Estado Cita" />                   
                            </Columns>
                        </x:Grid>
                    </Items>
                </x:Panel>
                <%--------------------------------------------------------%>










                <%--COLUMNA 2--%>
            <x:Panel ID="Panel4" Title="Trabajador" ColumnWidth="50%"  EnableBackgroundColor="true" runat="server"
                BodyPadding="5px" ShowBorder="true" ShowHeader="true"  Height="450px"  BoxConfigAlign="Stretch"  Layout="VBox">
                <Items>
                           <x:GroupPanel runat="server" Title="Datos Generales" ID="GroupPanel2" AutoWidth="true" BoxFlex="1" Height="115" >                
                                <Items>
                                <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                        <Rows>
                                            <x:FormRow ColumnWidths="500px" ID="FormRow6" runat="server">
                                                <Items> 
                                                    <x:Form ID="Form7"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                        <Rows>
                                                            <x:FormRow ColumnWidths="500px" ID="FormRow7" runat="server" >
                                                                <Items>                                                               
                                                                    <x:TextBox ID="txtTrabajador" Label="Trabajador" runat="server"/>                               
                                                                </Items>
                                                            </x:FormRow>
                                                        </Rows>
                                                    </x:Form>
                                                </Items>
                                            </x:FormRow>
                                             <x:FormRow ColumnWidths="500px" ID="FormRow12" runat="server">
                                                <Items>
                                                    <x:Form ID="Form8"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                        <Rows>
                                                            <x:FormRow ColumnWidths="500px" ID="FormRow9" runat="server" >
                                                                <Items>
                                                                     <x:TextBox ID="txtEmpresaTrabajo" Label="Emp. Trabajo" runat="server"/>    
                                                                </Items>
                                                            </x:FormRow>
                                                        </Rows>
                                                    </x:Form>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ColumnWidths="250px 240px" ID="FormRow10" runat="server">
                                                <Items>
                                                    <x:Form ID="Form9"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                        <Rows>
                                                            <x:FormRow ColumnWidths="250px" ID="FormRow11" runat="server" >
                                                                <Items>
                                                                    <x:TextBox ID="txtServicio" Label="Servicio" runat="server"/>
                                                                </Items>
                                                            </x:FormRow>
                                                        </Rows>
                                                    </x:Form> 
                                                     <x:Form ID="Form10"  runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                        <Rows>
                                                            <x:FormRow ColumnWidths="240px" ID="FormRow13" runat="server" >
                                                                <Items>
                                                                    <x:TextBox ID="txttipoEso" Label="Tipo ESO" runat="server"/>
                                                                </Items>
                                                            </x:FormRow>
                                                        </Rows>
                                                    </x:Form> 
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                </x:Form>
                                </Items>
                            </x:GroupPanel>
                                   <x:Grid ID="grdData2" ShowBorder="true" ShowHeader="true" Title="Lista de Examenes"  AutoWidth="true" 
                                    runat="server"  OnRowDataBound="grdData2_RowDataBound" Height="295px"
                                    EnableRowNumber="True">
                                         
                                    <Columns>    
                                        <x:boundfield Width="250px" DataField="v_CategoryName" DataFormatString="{0}" HeaderText="Examen"  />
                                        <x:boundfield Width="150px" DataField="v_ServiceComponentStatusName" DataFormatString="{0}" HeaderText="Estado Cola" />          
                                        <x:boundfield Width="150px" DataField="v_QueueStatusName" DataFormatString="{0}" HeaderText="Estado Examen" />
                                    </Columns>
                                </x:Grid>
               
                </Items>
            </x:Panel>
                   <%--------------------------------------------------------%>


            </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />
    <x:Window ID="winEdit" Title="Foto" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" IsModal="True" Width="340px" Height="295px" >
    </x:Window>

    </form>
</body>
</html>
