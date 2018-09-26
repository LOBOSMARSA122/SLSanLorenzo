<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM033.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRM033" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <x:PageManager ID="PageManager1" runat="server" />
    <x:Panel ID="Panel2" runat="server" Height="550px" Width="1300px" ShowBorder="False"
        Layout="Table" TableConfigColumns="2" ShowHeader="False" Title="Panel (Height=450px Width=750px Layout=Table)">
        <Items>
            <x:Panel ID="Panel1" Title="Buscar Servicio" Width="400px" Height="500px" EnableBackgroundColor="true"
                TableRowspan="2" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" BoxConfigAlign="Stretch" AutoScroll="true">
                  <Items>
                        <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="132" >                
                            <Items>
                                <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                        <Rows>
                                            <x:FormRow ID="FormRow1" ColumnWidths="460px" runat="server">
                                                <Items> 
                                                    <x:Form ID="Form3"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                        <Rows>
                                                            <x:FormRow ID="FormRow2" ColumnWidths="150px 150px" runat="server" >
                                                                <Items>
                                                                    <x:DatePicker ID="dpFechaInicio" Label="F.I" Width="90px" runat="server"  DateFormatString="dd/MM/yyyy" />
                                                                    <x:DatePicker ID="dpFechaFin" Label="F.F"  runat="server" Width="90px" DateFormatString="dd/MM/yyyy" />                                       
                                                                </Items>
                                                            </x:FormRow>
                                                            <x:FormRow ID="FormRow3" ColumnWidths="300px" runat="server" >
                                                                <Items>
                                                                   <x:DropDownList ID="ddlEmpresaCliente" runat="server"  Label="Emp." Width="240px"></x:DropDownList>                             
                                                                </Items>
                                                            </x:FormRow>
                                                            <x:FormRow ID="FormRow7" ColumnWidths="300px" runat="server" >
                                                                <Items>
                                                                   <x:DropDownList ID="ddlConsultorio" runat="server"  Label="Consul." Width="240px" AutoPostBack="true" OnSelectedIndexChanged="ddlConsultorio_SelectedIndexChanged"></x:DropDownList>                             
                                                                </Items>
                                                            </x:FormRow>
                                                            <x:FormRow ID="FormRow4" ColumnWidths="220px 100px" runat="server" >
                                                                <Items>
                                                                  <x:TextBox  runat="server" Label="Trabaj." Text="" Width="240px" ID="txtTrabajador"></x:TextBox>     
                                                                  <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline"  ValidateForms="Form2" OnClick="btnFilter_Click" ></x:Button>                                       
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
                   
                        <x:Panel ID="Panel5" runat="server"   ShowBorder="true" ShowHeader="false" Title=""  EnableBackgroundColor="true" Layout="Column" 
                              BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5">
                                <Items>
                                    <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server"  AutoScroll="true" Height="310px"
                                     EnableRowNumber="True" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_ServiceId,v_IdTrabajador,i_AptitudeStatusId,d_FechaNacimiento,v_Genero,v_TipoEso,v_GrupoRiesgo,v_Puesto,v_ObsStatusService,v_ProtocolId,Dni" 
                                    EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="true" BoxFlex="2" BoxMargin="5"  EnableColumnHide="true"
                                    OnRowClick="grdData_RowClick" EnableRowClick="true" >
                                          <Toolbars>
                                            <x:Toolbar ID="Toolbar4" runat="server">
                                                <Items>     
                                                    <x:Button ID="btnNewExamenes" Text="Examenes" Icon="PageWhiteStack" runat="server" Enabled="false"></x:Button>
                                                    <x:Button ID="btnNewFichaOcupacional" Text="Ficha Ocupacional" Icon="clipboard" runat="server" Enabled="false"></x:Button>
                                                </Items>
                                            </x:Toolbar>
                                        </Toolbars>
                                        <Columns>

                                            <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                                                Icon="Star" ToolTip="Dar Aptitud" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="v_ServiceId,i_AptitudeStatusId,v_ObsStatusService" DataIFrameUrlFormatString="FRM033A.aspx?v_ServiceId={0}&i_AptitudeStatusId={1}&v_ObsStatusService={2}"  
                                                DataWindowTitleField="v_Pacient" DataWindowTitleFormatString="Aptitud del trabajador:  {0}" />

                                            <x:WindowField ColumnID="myWindowField1" Width="25px" WindowID="Window1" HeaderText=""
                                                Icon="attach" ToolTip="Descargar Adjuntos" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="Dni" DataIFrameUrlFormatString="../ExternalUser/FRM031I.aspx?Dni={0}" 
                                                DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Archivos Adjuntos" />                                          

                                            <x:WindowField ColumnID="myWindowField2" Width="25px" WindowID="winreporte" HeaderText=""
                                                Icon="Eye" ToolTip="Ver Reporte" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="v_ServiceId" DataIFrameUrlFormatString="../ExternalUser/FRM031B.aspx?v_ServiceId={0}" 
                                                DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Reporte" />

                                            <x:WindowField ColumnID="mywinCardio" Width="25px" WindowID="EditarCardio" HeaderText=""
                                                Icon="ReportEdit" ToolTip="Editar Examen Cardiología" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="v_ServiceId,v_IdTrabajador" DataIFrameUrlFormatString="../Auditar/FRM033I.aspx?v_ServiceId={0}&v_IdTrabajador={1}" 
                                                DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Editar Cardiología"  Hidden="true"/>

                                            <x:WindowField ColumnID="mywinRxTorax" Width="25px" WindowID="EditarRXTorax" HeaderText=""
                                                Icon="ReportEdit" ToolTip="Editar Radiografía Torax" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="v_ServiceId,v_IdTrabajador" DataIFrameUrlFormatString="../Auditar/FRM033J.aspx?v_ServiceId={0}&v_IdTrabajador={1}"  
                                                DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Editar Radiografía Torax"  Hidden="true"/>

                                            <x:WindowField ColumnID="mywinRxOIT" Width="25px" WindowID="EditarRXToraxOIT" HeaderText=""
                                                Icon="ReportEdit" ToolTip="Editar Radiografía Torax OIT" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="v_ServiceId,v_IdTrabajador" DataIFrameUrlFormatString="../Auditar/FRM033K.aspx?v_ServiceId={0}&v_IdTrabajador={1}"  
                                                DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Editar Radiografía Torax OIT"  Hidden="true"/>
                                            
                                            <x:boundfield Width="270px" DataField="v_Pacient" DataFormatString="{0}" HeaderText="Trabajador" />
                                            
                                            <x:boundfield Width="80px" DataField="d_ServiceDate" DataFormatString="{0:d}" HeaderText="Fecha" />                                           
                                            
                                            <x:boundfield Width="250px" DataField="v_ProtocolName" DataFormatString="{0}" HeaderText="Protocolo" />     
                                            
                                            <x:boundfield Width="140px" DataField="v_ServiceId" DataFormatString="{0}" HeaderText="Id Atencion" />  
                                            
                                            <x:WindowField ColumnID="mySubirArchivos" Width="25px" WindowID="winSubirArchivo" HeaderText=""
                                                Icon="Accept" ToolTip="Subir Archivos" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="v_IdTrabajador,v_ServiceId,v_ProtocolId" DataIFrameUrlFormatString="../Auditar/FRM033M.aspx?v_IdTrabajador={0}&v_ServiceId={1}&v_ProtocolId={2}"  
                                                DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Subir Archivos"  />
                                                                                     
                                        </Columns>
                                    </x:Grid>
                            </Items>
                        </x:Panel>
                    </Items>
            </x:Panel>

     <x:Panel ID="Panel3" Title="Diagnósticos" Width="780px" Height="250px" EnableBackgroundColor="true" BoxConfigAlign="Top"
                 runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Table" TableConfigColumns="2">
                <Items>
                   <x:Accordion ID="Accordion1" Title="Accordion Control" runat="server" Width="300px" Height="210px"
                        EnableFill="true" ShowBorder="True" ActiveIndex="0">
                        <Panes>
                            <x:AccordionPane ID="AccordionPane1" runat="server" Title="Antecedentes" Icon="PageWhiteText" Width="270PX"  AutoScroll="true"
                                BodyPadding="2px 5px" ShowBorder="false">
                                <Items>
                                  <x:Grid ID="grdAntecedentes" ShowBorder="true" ShowHeader="false" runat="server" Width="280PX"  AutoScroll="true" Height="130px"
                                     EnableRowNumber="True" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames=""  
                                    EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="false" BoxFlex="2" BoxMargin="5">
            
                                        <Columns>
                                            <x:boundfield Width="90px" DataField="v_DateOrGroup" DataFormatString="{0}" HeaderText="Fecha / Grupo" />
                                            <x:boundfield Width="100px" DataField="v_AntecedentTypeName" DataFormatString="{0:d}" HeaderText="Tipo Antecedente" />                                           
                                            <x:boundfield Width="250px" DataField="v_DiseasesName" DataFormatString="{0}" HeaderText="Descripción" />                                                          
                                        </Columns>
                                    </x:Grid>
                                  
                                </Items>
                            </x:AccordionPane>
                            <x:AccordionPane ID="AccordionPane2" runat="server" Title="Paciente"  Icon="User" 
                                BodyPadding="2px 5px" ShowBorder="false">
                                <Items>
                                   
                                    <x:Label ID="lblEdad" Text="" Label="Edad" runat="server"></x:Label>
                                     <x:Label ID="lblGenero" Text="" Label="Género" runat="server"></x:Label>
                                     <x:Label ID="lblTipoEso" Text="" Label="Tipo ESO" runat="server"></x:Label>
                                     <x:Label ID="lblGrupoRiesgo" Text="" Label="Grupo Riesgo" runat="server"></x:Label>
                                     <x:Label ID="lblPuesto" Text="" Label="Puesto Postula" runat="server"></x:Label>
                                </Items>
                            </x:AccordionPane>
                                
                        </Panes>
                    </x:Accordion>    
                      <x:Panel ID="Panel6" runat="server"   ShowBorder="true" ShowHeader="false" Title=""  EnableBackgroundColor="true" Layout="Column" 
                              BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" AutoScroll="true">
                                <Items>
                                    <x:Grid ID="grdDx" ShowBorder="true" ShowHeader="false" runat="server"  AutoScroll="true" Height="206px" Width="465px"
                                     EnableRowNumber="True" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_DiagnosticRepositoryId,v_ComponentId,i_ServiceComponentStatusId,v_ServiceComponentId" 
                                    EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="false" BoxFlex="2" BoxMargin="5" 
                                    OnRowClick="grdDx_RowClick" EnableRowClick="true" OnRowCommand="grdDx_RowCommand">
                                      <Toolbars>
                                            <x:Toolbar ID="Toolbar1" runat="server">
                                                <Items>
                                                    <x:Button ID="btnNewDiagnosticos" Text="Nuevo Diagnóstico" Icon="Add" runat="server">
                                                    </x:Button>
                                                    <x:Button ID="btnNewDiagnosticosFrecuente" Text="Agregar Diagnóstico Frecuente" Icon="Add" runat="server">
                                                    </x:Button>
                                                </Items>
                                            </x:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                              <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="Window2" HeaderText=""
                                                Icon="Star" ToolTip="Culminar Examen" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="i_ServiceComponentStatusId,v_ServiceComponentId" DataIFrameUrlFormatString="FRM033L.aspx?i_ServiceComponentStatusId={0}&v_ServiceComponentId={1}"  
                                                DataWindowTitleField="v_Pacient" DataWindowTitleFormatString="Culminar Examen" />
                                              <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                                ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Dx" CommandName="DeleteAction" />
                                            <x:boundfield Width="100px" DataField="v_ComponentName" DataFormatString="{0}" HeaderText="Consultorio" />   
                                            <x:boundfield Width="250px" DataField="v_DiseasesName" DataFormatString="{0}" HeaderText="Diagnóstico" />   
                                            <x:boundfield Width="200px" DataField="v_RecomendationsName" DataFormatString="{0}" HeaderText="Recomendaciones" />
                                            <x:boundfield Width="200px" DataField="v_RestrictionsName" DataFormatString="{0}" HeaderText="Restricciones" />                                                                                                      
                                        </Columns>
                                    </x:Grid>
                            </Items>
                        </x:Panel> 
                </Items>
            </x:Panel>

            <x:Panel ID="Panel4" Title="Recomendaciones / Restricciones" Width="780px" Height="250px" EnableBackgroundColor="true"
                runat="server" BodyPadding="15px" ShowBorder="true" ShowHeader="true" Layout="Table" TableConfigColumns="2">
                <Items>

                      <x:Panel ID="Panel7" runat="server"   ShowBorder="true" ShowHeader="false" Title=""  EnableBackgroundColor="true" Layout="Column" 
                              BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" AutoScroll="true">
                                <Items>
                                    <x:Grid ID="grdRecomendaciones" ShowBorder="true" ShowHeader="false" runat="server"  AutoScroll="true" Height="200" Width="370px"
                                     EnableRowNumber="True" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_RecommendationId,v_ServiceId,v_DiagnosticRepositoryId,v_ComponentId,v_MasterRecommendationId" 
                                    EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="false" BoxFlex="2" BoxMargin="5"  OnRowCommand="grdRecomendaciones_RowCommand"
                                   EnableRowClick="true">  
                                        <Toolbars>
                                            <x:Toolbar ID="Toolbar2" runat="server">
                                                <Items>
                                                    <x:Button ID="btnAddRecomendacion" Text="Nueva Recomendación" Icon="Add" runat="server">
                                                    </x:Button>
                                                </Items>
                                            </x:Toolbar>
                                        </Toolbars>                                 
                                        <Columns>  
                                            <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEditReco" HeaderText=""
                                                Icon="Pencil" ToolTip="Agregar Recomendación" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="v_RecommendationId,v_MasterRecommendationId" DataIFrameUrlFormatString="FRM033B.aspx?v_RecommendationId={0}&v_MasterRecommendationId={1}" 
                                                DataWindowTitleField="v_RecommendationName" DataWindowTitleFormatString=" {0}" />

                                             <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                                ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Recomendación" CommandName="DeleteAction" />
                                            <x:boundfield Width="500px" DataField="v_RecommendationName" DataFormatString="{0}" HeaderText="Recomendaciones" />                                                        
                                        </Columns>
                                    </x:Grid>
                            </Items>
                        </x:Panel>            
                      <x:Panel ID="Panel8" runat="server"   ShowBorder="true" ShowHeader="false" Title=""  EnableBackgroundColor="true" Layout="Column" 
                              BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" AutoScroll="true">
                                <Items>
                                    <x:Grid ID="grdRestricciones" ShowBorder="true" ShowHeader="false" runat="server"  AutoScroll="true" Height="200" Width="370px"
                                     EnableRowNumber="True" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_RestrictionId,v_ServiceId,v_DiagnosticRepositoryId" 
                                    EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="false" BoxFlex="2" BoxMargin="5"  OnRowCommand="grdRestricciones_RowCommand"
                                   EnableRowClick="true">     
                                           <Toolbars>
                                            <x:Toolbar ID="Toolbar3" runat="server">
                                                <Items>
                                                    <x:Button ID="btnAddRestriccion" Text="Nueva Restricción" Icon="Add" runat="server">
                                                    </x:Button>
                                                </Items>
                                            </x:Toolbar>
                                        </Toolbars>                                       
                                        <Columns> 
                                            <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEditRestri" HeaderText=""
                                                Icon="Pencil" ToolTip="Agregar Restricción" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="v_RestrictionId,v_MasterRestrictionId" DataIFrameUrlFormatString="FRM033E.aspx?v_RestrictionId={0}&v_MasterRestrictionId={1}" 
                                                DataWindowTitleField="v_RestrictionName" DataWindowTitleFormatString="{0}" />
                                                
                                            <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                                ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Restricción" CommandName="DeleteAction" />
                                              <x:boundfield Width="500px" DataField="v_RestrictionName" DataFormatString="{0}" HeaderText="Restricciones" />                                                         
                                        </Columns>
                                    </x:Grid>
                            </Items>
                        </x:Panel>            

                </Items>
            </x:Panel>

        </Items>
    </x:Panel>
            <x:HiddenField ID="hfRefresh" runat="server" />
    <x:Window ID="winEdit" Title="Aptitud" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="490px" Height="250px" >
    </x:Window>

    <x:Window ID="Window2" Title="Aptitud" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="Window2_Close" IsModal="True" Width="490px" Height="250px" >
    </x:Window>


    <x:Window ID="winEditReco" Title="Recomendación" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEditReco_Close" IsModal="True" Width="600px" Height="400px" >
    </x:Window>

     <x:Window ID="winEditRestri" Title="Restricción" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEditRestri_Close1" IsModal="True" Width="600px" Height="400px" >
    </x:Window>


    <x:Window ID="WindowAddDX" Title="Nuevo Diagnóstico" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="WindowAddDX_Close" IsModal="True" Width="650px" Height="450px" >
    </x:Window>

    <x:Window ID="WindowAddReco" Title="Nueva Recomendación" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="WindowAddReco_Close" IsModal="True" Width="500px" Height="100px" >
    </x:Window>

    <x:Window ID="WindowAddRestri" Title="Nueva Restricción" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="WindowAddRestri_Close" IsModal="True"  Width="600px" Height="400px" >
    </x:Window>

    <x:Window ID="WindowAddDXFrecuente" Title="Diagnóstico Frecuente" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="WindowAddDXFrecuente_Close" IsModal="True" Width="650px" Height="540px" >
    </x:Window>

    <x:Window ID="Window1" IconUrl="~/images/16/11.png" runat="server" Popup="false"
        IsModal="true" Target="Parent" EnableMaximize="false" EnableResize="false"
        Title="Ver Archivos Adjuntos" EnableConfirmOnClose="true" CloseAction="HidePostBack"
        EnableIFrame="true" IFrameUrl="about:blank" Height="210px" Width="300px"  OnClose="Window1_Close">
    </x:Window>

  
        

    <x:Window ID="winEdit3" IconUrl="~/images/16/11.png" runat="server" Popup="false"
        IsModal="true" Target="Parent" EnableMaximize="false" EnableResize="false"
        Title="Lista de Examenes" EnableConfirmOnClose="true" CloseAction="HidePostBack"
        EnableIFrame="true" IFrameUrl="about:blank" Height="550px" Width="280px"  OnClose="winEdit3_Close">
    </x:Window>

    <x:Window ID="winreporte" Title="Certificado(s)" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HidePostBack" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="winreporte_Close"  IsModal="true"  Height="630px" Width="700px" >
    </x:Window>

    <x:Window ID="winEdit2" Title="Ficha Ocupacional" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HideRefresh" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="winEdit2_Close"  IsModal="true"  Height="245px" Width="245px" >
    </x:Window>

    <x:Window ID="EditarCardio" Title="Editar Cardiología" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HideRefresh" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="EditarCardio_Close"  IsModal="true"  Height="245px" Width="800px" >
    </x:Window>

    <x:Window ID="EditarRXTorax" Title="Editar Radiografía de Torax" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HideRefresh" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="EditarRXTorax_Close"  IsModal="true"  Height="310px" Width="800px" >
    </x:Window>

    <x:Window ID="EditarRXToraxOIT" Title="Editar Radiografía de Torax OIT" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HideRefresh" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top" OnClose="EditarRXToraxOIT_Close"  IsModal="true"  Height="450px" Width="800px" >
    </x:Window>
        
  <x:Window ID="winSubirArchivo" IconUrl="~/images/16/11.png" runat="server" Popup="false"
        IsModal="true" Target="Parent" EnableMaximize="false" EnableResize="false"
        Title="Subir Archivos" EnableConfirmOnClose="true" CloseAction="HidePostBack"
        EnableIFrame="true" IFrameUrl="about:blank" Height="380px" Width="570px"  OnClose="winSubirArchivo_Close">
    </x:Window>   

    </form>
</body>
</html>
