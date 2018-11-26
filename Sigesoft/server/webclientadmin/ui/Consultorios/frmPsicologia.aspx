<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPsicologia.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmPsicologia" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
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
     <x:PageManager ID="PageManager1" runat="server"/>
         <x:Panel ID="Panel2" runat="server" Height="5000px" Width="1000px" ShowBorder="True"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Psicología">
            <Items>
                <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="60">
                    <Items>
                        <x:SimpleForm ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="false" ShowHeader="False" runat="server">
                            <Items>
                                <x:Form ID="Form8" runat="server" EnableBackgroundColor="false" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow19" ColumnWidths="460px" runat="server">
                                            <Items>
                                                <x:Form ID="Form9" runat="server" EnableBackgroundColor="false" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow20" ColumnWidths="150px 150px 100px 300px 220px " runat="server">
                                                            <Items>
                                                                <x:DatePicker ID="dpFechaInicio" Label="F.I" Width="90px" runat="server" DateFormatString="dd/MM/yyyy" />
                                                                <x:DatePicker ID="dpFechaFin" Label="F.F" runat="server" Width="90px" DateFormatString="dd/MM/yyyy" />     
                                                                <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click"></x:Button>                                                         
                                                                <x:DropDownList ID="ddlConsultorio" runat="server" Label="Consul." Width="240px"></x:DropDownList>
                                                                <x:TextBox runat="server" Label="Trabaj." Text="" Width="240px" ID="txtTrabajador"></x:TextBox>
                                                                
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                                </x:Form>
                            </Items>
                        </x:SimpleForm>
                    </Items>
                </x:GroupPanel>
                <x:GroupPanel runat="server" Title="Resultado de la búsqueda" ID="GroupPanel2" AutoWidth="true" BoxFlex="1" Height="280">
                    <Items>
                          <x:Form ID="Form58" runat="server" EnableBackgroundColor="false" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                            <Rows>
                                <x:FormRow ID="FormRow206" ColumnWidths="700px 15px 250px" runat="server" >
                                    <Items>
                                        <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" Height="240px"
                                            EnableRowNumber="True" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Mask"
                                            EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_ServiceId,v_IdTrabajador,v_Genero,i_AptitudeStatusId,i_EsoTypeId,v_ExploitedMineral,i_AltitudeWorkId,i_PlaceWorkId,v_Pacient,Dni,d_ServiceDate,v_TipoEso,v_Puesto,d_FechaNacimiento,EmpresaCliente,AreaEmpresa,v_SectorName,ComentarioAptitud"
                                            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" EnableCheckBoxSelect="false"
                                            OnRowClick="grdData_RowClick" EnableRowClick="true" OnRowCommand="grdData_RowCommand"  OnRowDataBound="grdData_RowDataBound" >                         
                                            <Columns>
                                                 <x:CheckBoxField Width="30px" RenderAsStaticField="true" DataField="AtSchool" HeaderText="" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea llamar al paciente a consultorio?" Icon="sound" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="LLamar Paciente" CommandName="LlamarPaciente" />
                                                <x:BoundField Width="270px" DataField="v_Pacient" DataFormatString="{0}" HeaderText="Trabajador" />
                                                <x:BoundField Width="80px" DataField="d_ServiceDate" DataFormatString="{0:d}" HeaderText="Fecha" />
                                                <x:BoundField Width="300px" DataField="v_ProtocolName" DataFormatString="{0}" HeaderText="Protocolo" />
                                                <x:BoundField Width="140px" DataField="v_ServiceId" DataFormatString="{0}" HeaderText="Id Atencion" />
                                            </Columns>
                                        </x:Grid>
                                           <x:Label ID="lblEspacio" runat="server" Text="" ShowLabel="false"></x:Label>
                                        <x:Grid ID="grdComponentes" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" Height="240px"
                                            EnableRowNumber="True" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Mask"
                                            EnableMouseOverColor="true" ShowGridHeader="true" 
                                            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" 
                                            >
                                            <Columns>
                                                <x:CheckBoxField Width="30px" RenderAsStaticField="true" DataField="AtSchool" HeaderText="" />
                                                <x:BoundField Width="270px" DataField="v_ComponentName" DataFormatString="{0}" HeaderText="Examen" />
                                                <x:BoundField Width="270px" DataField="i_ServiceComponentStatusId" DataFormatString="{0}" HeaderText="Estado" />
                                            </Columns>                                            
                                        </x:Grid>
                                    </Items>
                                </x:FormRow>
                            </Rows>
                        </x:Form>
                       
                    </Items>
                </x:GroupPanel>
                <x:GroupPanel runat="server" Title="Datos de Trabajador" ID="GroupPanel3" AutoWidth="true" BoxFlex="1" Height="120">
                    <Items>
                         <x:Form ID="Form29" runat="server" EnableBackgroundColor="false" ShowBorder="False" ShowHeader="False" LabelWidth="10px" LabelAlign="Left">
                            <Rows>
                                <x:FormRow ID="FormRow75" ColumnWidths="110px   400px 110px   330px " runat="server">
                                    <Items>
                                        <x:Label ID="Label416" runat="server" Text="EMPRESA" ShowLabel="false"></x:Label>    
                                        <x:TextBox ID="txtEmpresaClienteCabecera" ShowLabel="false" CssClass="mright" runat="server" Readonly="true"></x:TextBox>       
                                         <x:Label ID="Label417" runat="server" Text="ACTIVIDAD" ShowLabel="false"></x:Label>    
                                        <x:TextBox ID="txtActividadEmpresaClienteCabecera" ShowLabel="false" CssClass="mright" runat="server" Readonly="true"></x:TextBox>                                                                                       
                                    </Items> 
                                </x:FormRow> 
                                <x:FormRow ID="FormRow76" ColumnWidths="110px 400px 110px 180px 50px 100px" runat="server">
                                    <Items>
                                         <x:Label ID="Label418" runat="server" Text="TRABAJADOR" ShowLabel="false" Readonly="true"></x:Label>    
                                        <x:TextBox ID="txtTrabajadorCabecera" ShowLabel="false" CssClass="mright" runat="server" Readonly="true"></x:TextBox> 
                                        <x:Label ID="Label419" runat="server" Text="D.N.I" ShowLabel="false" Readonly="true"></x:Label>    
                                        <x:TextBox ID="txtDNICabecera" ShowLabel="false" CssClass="mright" runat="server" Readonly="true"></x:TextBox>     
                                         <x:Label ID="Label422" runat="server" Text="FECHA" ShowLabel="false" Readonly="true"></x:Label>    
                                        <x:TextBox ID="txtFechaCabecera" ShowLabel="false" CssClass="mright" runat="server" Readonly="true"></x:TextBox>     
                                    </Items>
                                </x:FormRow>
                                <x:FormRow ID="FormRow77" ColumnWidths="109px 200px 100px 100px  110px 180px 50px 100px" runat="server">
                                    <Items>
                                         <x:Label ID="Label420" runat="server" Text="TIPO EXAMEN" ShowLabel="false"></x:Label>    
                                        <x:TextBox ID="txtTipoExamenCabecera" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox> 
                                        <x:Label ID="Label421" runat="server" Text="GÉNERO" ShowLabel="false"></x:Label>    
                                        <x:TextBox ID="txtGeneroCabecera" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox> 
                                         <x:Label ID="Label424" runat="server" Text="PUESTO" ShowLabel="false"></x:Label>    
                                        <x:TextBox ID="txtPuestoCabecera" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox>     
                                        <x:Label ID="Label423" runat="server" Text="EDAD" ShowLabel="false"></x:Label>    
                                        <x:TextBox ID="txtEdadCabecera" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox>                                         
                                    </Items>
                                </x:FormRow>
                            </Rows>
                        </x:Form>
                    </Items>
                </x:GroupPanel>
                <x:Accordion ID="Accordion1" Title="Accordion Control" runat="server" Width="350px" Height="450px"
                    EnableFill="true" ShowBorder="True" ActiveIndex="0" ShowHeader="false" Enabled="false">
                    <Panes>
                        <x:AccordionPane ID="AccordionPane1" runat="server" Title="DIAGNÓSTICOS" Icon="Note" Width="270PX" AutoScroll="true" BodyPadding="2px 5px" ShowBorder="false">
                            <Items>
                                <x:Panel ID="Panel51AA" Title="" EnableBackgroundColor="false" Height="390px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="false" Layout="Table" TableConfigColumns="3">
                                    <Items>
                                        <x:Panel ID="Panel51AAA" runat="server" ShowBorder="true" ShowHeader="false" Title="" EnableBackgroundColor="false" TableColspan="3"
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5">
                                            <Items>
                                                <x:Grid ID="grdDx" ShowBorder="true" ShowHeader="false" runat="server" Height="170px" Width="960px"
                                                    EnableRowNumber="True" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_DiagnosticRepositoryId,v_ComponentId"
                                                    EnableTextSelection="true" EnableAlternateRowColor="true" BoxMargin="5"  EnableRowClick="true" OnRowCommand="grdDx_RowCommand" OnRowClick="grdDx_RowClick" OnRowDataBound="grdDx_RowDataBound">
                                                    <Toolbars>
                                                        <x:Toolbar ID="Toolbar16" runat="server">
                                                            <Items>
                                                                <x:Button ID="btnNewDiagnosticos" Text="Nuevo Diagnóstico" Icon="Add" runat="server" Visible="false" >
                                                                </x:Button>
                                                                <x:Button ID="btnNewDiagnosticosFrecuente" Text="Agregar Diagnóstico" Icon="Add" runat="server" >
                                                                </x:Button>
                                                            </Items>
                                                        </x:Toolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Al eliminar este diagnóstico se le eliminará también las recomendaciones y restricciónes vinculadas a este diagnóstico.¿Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Dx" CommandName="DeleteAction" />
                                                        <x:BoundField Width="150px" DataField="v_ComponentName" DataFormatString="{0}" HeaderText="Consultorio" />
                                                        <x:BoundField Width="700px" DataField="v_DiseasesName" DataFormatString="{0}" HeaderText="Diagnóstico" />
                                                    </Columns>
                                                </x:Grid>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel52AA" runat="server" ShowBorder="true" ShowHeader="false" Title="" Height="20" EnableBackgroundColor="false" TableColspan="3"
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
                                            <Items>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel53AA" runat="server" ShowBorder="true" Title="RECOMENDACIONES" Height="190" EnableBackgroundColor="false" TableColspan="1"
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="480" >
                                            <Items>
                                                <x:Grid ID="grdRecomendaciones" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" Height="160px" Width="480px"
                                                    EnableRowNumber="True" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                                    EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_RecommendationId,v_ServiceId,v_DiagnosticRepositoryId,v_ComponentId,v_MasterRecommendationId"
                                                    EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="false" BoxFlex="2" BoxMargin="5" OnRowCommand="grdRecomendaciones_RowCommand"
                                                    EnableRowClick="true">
                                                    <Toolbars>
                                                        <x:Toolbar ID="Toolbar17" runat="server">
                                                            <Items>
                                                                <x:Button ID="btnAddRecomendacion" Text="Nueva Recomendación" Icon="Add" runat="server">
                                                                </x:Button>
                                                            </Items>
                                                        </x:Toolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEditReco" HeaderText=""
                                                            Icon="Pencil" ToolTip="Agregar Recomendación" DataTextFormatString="{0}"
                                                            DataIFrameUrlFields="v_RecommendationId,v_MasterRecommendationId" DataIFrameUrlFormatString="../Auditar/FRM033B.aspx?v_RecommendationId={0}&v_MasterRecommendationId={1}"
                                                            DataWindowTitleField="v_RecommendationName" DataWindowTitleFormatString=" {0}" />

                                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Recomendación" CommandName="DeleteAction" />
                                                        <x:BoundField Width="300px" DataField="v_RecommendationName" DataFormatString="{0}" HeaderText="Recomendaciones" />
                                                    </Columns>
                                                </x:Grid>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel54AA" runat="server" ShowBorder="true" Title="" ShowHeader="false" Height="5" EnableBackgroundColor="false" TableColspan="1"
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="20" >
                                            <Items>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel55AA" runat="server" ShowBorder="true" Title="RESTRICCIONES" Height="190" EnableBackgroundColor="false" TableColspan="1"
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="460" >
                                            <Items>
                                                <x:Grid ID="grdRestricciones" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" Height="160px" Width="460px"
                                                    EnableRowNumber="True" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                                    EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_RestrictionId,v_ServiceId,v_DiagnosticRepositoryId"
                                                    EnableTextSelection="true" EnableAlternateRowColor="true" EnableCheckBoxSelect="false" BoxFlex="2" BoxMargin="5" OnRowCommand="grdRestricciones_RowCommand"
                                                    EnableRowClick="true">
                                                    <Toolbars>
                                                        <x:Toolbar ID="Toolbar18" runat="server">
                                                            <Items>
                                                                <x:Button ID="btnAddRestriccion" Text="Nueva Restricción" Icon="Add" runat="server">
                                                                </x:Button>
                                                            </Items>
                                                        </x:Toolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEditRestri" HeaderText=""
                                                            Icon="Pencil" ToolTip="Agregar Restricción" DataTextFormatString="{0}"
                                                            DataIFrameUrlFields="v_RestrictionId,v_MasterRestrictionId" DataIFrameUrlFormatString="../Auditar/FRM033E.aspx?v_RestrictionId={0}&v_MasterRestrictionId={1}"
                                                            DataWindowTitleField="v_RestrictionName" DataWindowTitleFormatString="{0}" />

                                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Restricción" CommandName="DeleteAction" />
                                                        <x:BoundField Width="300px" DataField="v_RestrictionName" DataFormatString="{0}" HeaderText="Restricciones" />
                                                    </Columns>
                                                </x:Grid>
                                            </Items>
                                        </x:Panel>

                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:AccordionPane>
                                               <x:AccordionPane ID="AccordionPane2" runat="server" Title="APTITUD" Icon="User" Width="150PX" AutoScroll="true" BodyPadding="2px 5px" ShowBorder="false">
                            <Items>
                                <x:Toolbar ID="Toolbar19" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarAptitud" Text="Grabar Aptitud" Icon="SystemSave" runat="server" OnClick="btnGrabarAptitud_Click"  AjaxLoadingType="Mask"></x:Button>
                                        <x:Button ID="btnCertificadoAptitud" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                                <x:Panel ID="Panel105aa" Title="" EnableBackgroundColor="true" Height="160px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="false">
                                    <Items>
                                        <x:Form ID="Form267aa" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow706aa" ColumnWidths="280px 280px" runat="server">
                                                    <Items>
                                                        <x:DropDownList ID="ddlAptitud" runat="server" Label="Aptitud" Width="140px" ShowLabel="true"></x:DropDownList>
                                                        <x:DropDownList ID="ddlFirmaAuditor" runat="server" Label="Auditor" Width="140px" ShowLabel="true"></x:DropDownList>

                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow706qq" ColumnWidths="280px" runat="server">
                                                    <Items>
                                                        <x:TextArea ID="txtComentarioAptitud" runat="server" Text="" Label="Comentario" ShowLabel="true" Height="50"></x:TextArea>
                                                    </Items>
                                                </x:FormRow>                                          
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:AccordionPane>
                    </Panes>
                </x:Accordion>

          
                <x:TabStrip ID="TabStrip1" Width="1000px" Height="5670px" ShowBorder="true" ActiveTabIndex="0" runat="server" EnableTitleBackgroundColor="False">
                    <Tabs>
                        <x:Tab ID="TabPsicologia" BodyPadding="5px" Title="Psicología" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarPsicologia" Text="Grabar Psicología" Icon="SystemSave" runat="server" OnClick="btnGrabarPsicologia_Click" AjaxLoadingType="Mask"></x:Button>                                   
                                        
                                        <x:Label ID="Label746" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReportePsico" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel29" Title="MOTIVO DE LA EVALUACIÓN" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:TextBox ID="txtMotivoEvaluacion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="960"></x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel1" Title="HISTORIA PSICOLÓGICA" EnableBackgroundColor="true" Height="570px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="DATOS OCUPACIONALES" ID="GroupPanel4" AutoWidth="true" BoxFlex="1" Height="50">
                                            <Items>
                                                 <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="10px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow1" ColumnWidths="100px  150px 100px  150px 200px  200px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label1" runat="server" Text="SUPERFICIE" ShowLabel="false"></x:Label>    
                                                                <x:CheckBox ID="chkSuperficie" ShowLabel="false" CssClass="mright" runat="server"></x:CheckBox>       
                                                                <x:Label ID="Label2" runat="server" Text="SUBSUELO " ShowLabel="false"></x:Label>    
                                                                <x:CheckBox ID="chkSubsuelo" ShowLabel="false" CssClass="mright" runat="server" ></x:CheckBox>   
                                                                <x:Label ID="Label3" runat="server" Text="TIEMPO TOTAL LABORANDO " ShowLabel="false"></x:Label>    
                                                                <x:TextBox ID="txttiempo_total_laborando" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox>  
                                                            </Items> 
                                                        </x:FormRow> 
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="EMPRESA ACTUAL" ID="GroupPanel5" AutoWidth="true" BoxFlex="1" Height="70">
                                            <Items>
                                                 <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="10px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow2" ColumnWidths="100px  350px 100px 350px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label4" runat="server" Text="PRINCIPALES RIESGOS" ShowLabel="false"></x:Label>    
                                                                <x:TextArea ID="txtprincipales_riesgos" ShowLabel="false" CssClass="mright" runat="server" Height="40" ></x:TextArea>       
                                                                <x:Label ID="Label5" runat="server" Text="MEDIDAS DE SEGURIDAD" ShowLabel="false"></x:Label>    
                                                                <x:TextBox ID="txtmedidas_de_seguridad" ShowLabel="false" CssClass="mright" runat="server" Height="40"></x:TextBox>                                                                 
                                                            </Items> 
                                                        </x:FormRow> 
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="ANTERIORES EMPRESAS" ID="GroupPanel6" AutoWidth="true" BoxFlex="1" Height="80">
                                            <Items>
                                                <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="10px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow3" ColumnWidths="100px  150px 100px  150px 100px  300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label6" runat="server" Text="FECHA" ShowLabel="false"></x:Label>    
                                                               <x:TextBox ID="txtfecha_1" runat="server" ShowLabel="false"></x:TextBox>                                                                  
                                                                <x:Label ID="Label7" runat="server" Text="ACTIVIDAD" ShowLabel="false"></x:Label>    
                                                                <x:TextBox ID="txtactividad_1" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox> 
                                                                <x:Label ID="Label8" runat="server" Text="EMPRESA" ShowLabel="false"></x:Label>    
                                                                <x:TextBox ID="txtnombre_1" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox>    
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow4" ColumnWidths="100px  150px 100px  150px 100px 100px 100px 100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label9" runat="server" Text="PUESTO" ShowLabel="false"></x:Label>    
                                                                <x:TextBox ID="txtpuesto_1" ShowLabel="false" CssClass="mright" runat="server"></x:TextBox>       
                                                                <x:Label ID="Label10" runat="server" Text="CAUSA DEL RETIRO" ShowLabel="false"></x:Label>    
                                                                <x:TextBox ID="txtcausa_1" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox>                                                                
                                                                <x:Label ID="Label12" runat="server" Text="SUP" ShowLabel="false"></x:Label>    
                                                                <x:TextBox ID="txtsup_1" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox>    
                                                                <x:Label ID="Label13" runat="server" Text="SUB" ShowLabel="false"></x:Label>    
                                                                <x:TextBox ID="txtsub_1" ShowLabel="false" CssClass="mright" runat="server" ></x:TextBox>    
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>        
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="HISTORIA FAMILIAR" ID="GroupPanel7" AutoWidth="true" BoxFlex="1" Height="50">
                                            <Items>
                                                <x:TextBox ID="txthistoria_familiar" ShowLabel="false" CssClass="mright" runat="server"></x:TextBox>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title=" ACCIDENTES Y ENFERMEDADES: (durante el tiempo de trabajo)" ID="GroupPanel8" AutoWidth="true" BoxFlex="1" Height="50">
                                            <Items>
                                                <x:TextBox ID="txtaccidentes_y_enf" ShowLabel="false" CssClass="mright" runat="server"></x:TextBox>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="HABITOS (pasatiempos, consumo de tabaco, alcohol y/o drogas)" ID="GroupPanel9" AutoWidth="true" BoxFlex="1" Height="50">
                                            <Items>
                                                <x:TextBox ID="txthabitos" ShowLabel="false" CssClass="mright" runat="server"></x:TextBox>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="OTRAS OBSERVACIONES" ID="GroupPanel10" AutoWidth="true" BoxFlex="1" Height="50">
                                            <Items>
                                                <x:TextBox ID="txtobservaciones" ShowLabel="false" CssClass="mright" runat="server"></x:TextBox>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel3" Title="EVALUACIÓN" EnableBackgroundColor="true" Height="360px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="2" Layout="Table">
                                    <Items>
                                    <x:GroupPanel runat="server" Title="1. OBSERVACION DE CONDUCTAS" ID="GroupPanel27" BoxFlex="1" Height="330" Width="480" >                
                                        <Items>
                                            <x:Form ID="Form42" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow145" ColumnWidths="160px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label113" runat="server" Text="Presentación" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlPresentacion" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow146" ColumnWidths="161px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label115" runat="server" Text="Postura" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlPostura" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                   <x:FormRow ID="FormRow152" ColumnWidths="160px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label122" runat="server" Text="Discurso Ritmo" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlDisRitmo" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow147" ColumnWidths="161px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label116" runat="server" Text="Discurso Tono" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlDisTono" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow148" ColumnWidths="160px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label118" runat="server" Text="Discurso Articulación" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlDisArticulacion" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow149" ColumnWidths="161px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label119" runat="server" Text="Orientación Espacio" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlOrientacionEspacio" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow150" ColumnWidths="160px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label120" runat="server" Text="Orientación Tiempo" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlOrientacionTiempo" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow151" ColumnWidths="161px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label121" runat="server" Text="Orientación Persona" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlOrientacionPersona" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>                                                   
                                                </Rows>
                                                </x:Form>
                                            </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="2. PROCESOS COGNITIVOS" ID="GroupPanel28" BoxFlex="1" Height="330" Width="480" >                
                                        <Items>
                                            <x:Form ID="Form43" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="250px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow153" ColumnWidths="160px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label123" runat="server" Text="Lucido, Atento" ShowLabel="false"></x:Label>                                                   
                                                            <x:TextBox ID="txtLucido" runat="server" Text=""  Width="202"  ShowLabel="false"></x:TextBox>   
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow154" ColumnWidths="161px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="plabel124" runat="server" Text="Pensamiento" ShowLabel="false"></x:Label>                                                   
                                                             <x:DropDownList ID="ddlpensamiento" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                   <x:FormRow ID="pFormRow155" ColumnWidths="160px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label125" runat="server" Text="Percepción" ShowLabel="false"></x:Label>    
                                                             <x:DropDownList ID="DropDownList1" runat="server" Text="" ShowLabel="false"></x:DropDownList>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="pFormRow156" ColumnWidths="161px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label126" runat="server" Text="Memoria Corto P." ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlmemoria_corto_plazo" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow5" ColumnWidths="160px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label11" runat="server" Text="Memoria Mediano P." ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlmemoria_mediano_plazo" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow6" ColumnWidths="161px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label14" runat="server" Text="Memoria Largo P." ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlmemoria_largo_plazo" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="pFormRow157" ColumnWidths="160px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label127" runat="server" Text="Inteligencia" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlinteligencia1" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="pFormRow158" ColumnWidths="161px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="plabel128" runat="server" Text="Afectividad" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlAfectividad" runat="server" Text="" Width="200" ShowLabel="false"></x:DropDownList>    
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="pFormRow159" ColumnWidths="160px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="plabel129" runat="server" Text="Personalidad" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlPersonalidad" runat="server" Text="" Width="201" ShowLabel="false"></x:DropDownList>    
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow7" ColumnWidths="161px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="Label15" runat="server" Text="Conducta Sexual" ShowLabel="false"></x:Label>                                                   
                                                            <x:TextBox ID="txtconducta_sexual" runat="server" Text="" Width="201" ShowLabel="false"></x:TextBox>    
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow8" ColumnWidths="160px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="Label16" runat="server" Text="Apetito" ShowLabel="false"></x:Label>                                                   
                                                            <x:TextBox ID="txtapetito1" runat="server" Text="" Width="201" ShowLabel="false"></x:TextBox>    
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow9" ColumnWidths="161px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="Label17" runat="server" Text="Sueño" ShowLabel="false"></x:Label>                                                   
                                                            <x:TextBox ID="txtSuenio" runat="server" Text="" Width="201" ShowLabel="false"></x:TextBox>    
                                                        </Items>
                                                    </x:FormRow>                                                    
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>                                     
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="pPanel14" Title="TEST PSICOLÓGICOS" EnableBackgroundColor="true" Height="230px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Table">
                                <Items>
                                    <x:GroupPanel runat="server" Title="Pjte ..................................Nombre...................................................Pjte...................................................Nombre..................................................." ID="GroupPanel29" BoxFlex="1" Height="200" Width="960" >                
                                        <Items>
                                            <x:Form ID="pForm44" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow160" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtinventario_millon_de_estilos" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel130" runat="server" Text="INVENTARIO MILLÓN DE ESTILOS" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtescala_de_motivaciones" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel131" runat="server" Text="ESCALA DE MOTIVACIONES PSlCOSOCIALES - MPS" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form45" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow161" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtluria_dna_diagnostico" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel132" runat="server" Text="LURIA - DNA DIAGNOSTICO NEUROPSICOLÓGICO DE" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtescala_del_estres" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel133" runat="server" Text="ESCALA DE APRECIACIÓN DEL ESTRÉS -EAE" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form46" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow162" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtinventario_de_burnout" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel134" runat="server" Text=" INVENTARIO DE BURNOUT DE MASLACH" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtclima_laboral" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel135" runat="server" Text="CLIMA LABORAL" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form47" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow163" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtwais" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel136" runat="server" Text="WAIS" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtbateria_de_conductores" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel137" runat="server" Text="BATERÍA DE CONDUCTORES" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                            <x:Form ID="Form48" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow164" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txttest_benton" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel139" runat="server" Text="TEST BENTON" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txttest_bender" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel140" runat="server" Text="TEST BENDER" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                            <x:Form ID="Form49" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow165" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtinventario_de_la_ansiedad_zung" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel141" runat="server" Text="INVENTARIO DE LA ANSIEDAD ZUNG" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtinventarlo_de_la_depresion_zung" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel142" runat="server" Text="INVENTARLO DE LA DEPRESIÓN ZUNG" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                            <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow10" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtescala_de_memoria_de_wechsler" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="Label18" runat="server" Text="ESCALA DE MEMORIA DE WECHSLER" ShowLabel="false"></x:Label>
                                                            <x:Label ID="Label19" runat="server" Text="." ShowLabel="false"></x:Label>
                                                            <x:Label ID="Label20" runat="server" Text="." ShowLabel="false"></x:Label>
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>   
                                </Items>
                            </x:Panel>
                                <x:Panel ID="Panel4" Title="OTROS TEST PSICOLÓGICOS" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:TextBox ID="txtotros" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="960"></x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel5" Title="RESULTADOS DE LA EVALUACIÓN" EnableBackgroundColor="true" Height="140px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                         <x:Form ID="Form57" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                            <Rows>
                                                    <x:FormRow ID="FormRow11" ColumnWidths="100px  350px 100px 350px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label21" runat="server" Text="NIVEL INTELECTUAL" ShowLabel="false"></x:Label>    
                                                        <x:TextArea ID="txtnivel_intelectual" ShowLabel="false" CssClass="mright" runat="server" Height="40" ></x:TextArea>       
                                                        <x:Label ID="Label22" runat="server" Text="COORDINACIÓN" ShowLabel="false"></x:Label>    
                                                        <x:TextBox ID="txtcoordinacion_visomotriz" ShowLabel="false" CssClass="mright" runat="server" Height="40"></x:TextBox>                                                                 
                                                    </Items> 
                                                </x:FormRow> 
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form6" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                            <Rows>
                                                    <x:FormRow ID="FormRow12" ColumnWidths="100px  350px 100px 350px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label23" runat="server" Text="NIVEL DE MEMORIA" ShowLabel="false"></x:Label>    
                                                        <x:TextArea ID="txtnivel_de_memoria" ShowLabel="false" CssClass="mright" runat="server" Height="40" ></x:TextArea>       
                                                        <x:Label ID="Label24" runat="server" Text="PERSONALIDAD" ShowLabel="false"></x:Label>    
                                                        <x:TextBox ID="txtpersonalidad2" ShowLabel="false" CssClass="mright" runat="server" Height="40"></x:TextBox>                                                                 
                                                    </Items> 
                                                </x:FormRow> 
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form7" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                            <Rows>
                                                    <x:FormRow ID="FormRow13" ColumnWidths="100px  350px 100px 350px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label25" runat="server" Text="AFECTIVIDAD" ShowLabel="false"></x:Label>    
                                                        <x:TextArea ID="txtafectividad2" ShowLabel="false" CssClass="mright" runat="server" Height="40" ></x:TextArea>       
                                                        <x:Label ID="Label26" runat="server" Text="." ShowLabel="false"></x:Label>    
                                                        <x:Label ID="Label27" runat="server" Text="." ShowLabel="false"></x:Label>                                                              
                                                    </Items> 
                                                </x:FormRow> 
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel6" Title="RESULTADOS DE LA EVALUACIÓN" EnableBackgroundColor="true" Height="140px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                         <x:Form ID="Form10" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                            <Rows>
                                                    <x:FormRow ID="FormRow14" ColumnWidths="100px  350px 100px 350px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label28" runat="server" Text="ÁREA COGNITIVA " ShowLabel="false"></x:Label>    
                                                        <x:TextArea ID="txtarea_cognitiva" ShowLabel="false" CssClass="mright" runat="server" Height="40" ></x:TextArea>       
                                                        <x:Label ID="Label29" runat="server" Text="AREA EMOCIONAL" ShowLabel="false"></x:Label>    
                                                        <x:TextBox ID="txtarea_emocional" ShowLabel="false" CssClass="mright" runat="server" Height="40"></x:TextBox>                                                                 
                                                    </Items> 
                                                </x:FormRow> 
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                            <Rows>
                                                <x:FormRow ID="FormRow15" ColumnWidths="100px  350px 100px 350px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label30" runat="server" Text="APTITUD PSICOLÓGICA " ShowLabel="false"></x:Label>    
                                                        <Ext:TextBox ID="txtAptitud_psicologica" runat="server" Text="" ShowLabel="false"></Ext:TextBox>     
                                                        <x:Label ID="Label31" runat="server" Text=" ÁREA PERSONAL" ShowLabel="false"></x:Label>    
                                                        <x:TextBox ID="txtarea_personal" ShowLabel="false" CssClass="mright" runat="server" Height="40"></x:TextBox>   
                                                    </Items> 
                                                </x:FormRow> 
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form12" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                            <Rows>
                                                <x:FormRow ID="FormRow16" ColumnWidths="100px  850px " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label32" runat="server" Text="EVALUACIÓN NORMAL" ShowLabel="false"></x:Label>      
                                                        <x:CheckBox ID="chkEvaluacionNormal" Text="" ShowLabel="false" runat="server"></x:CheckBox>    
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel7" Title="RECOMENDACIONES / RESTRICCIONES" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                     <Items>
                                         <x:TextArea ID="txtRecomendacion" ShowLabel="false" CssClass="mright" runat="server" Height="40"  Width="960"></x:TextArea> 
                                     </Items>
                                </x:Panel>

                                    <x:Panel ID="Panel366" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form2142" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow6495" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtPsicoAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtPsicoAuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtPsicoAuditorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow6451" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtPsicoEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtPsicoEvaluadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtPsicoEvaluadorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>  
                                                 <Ext:FormRow ID="FormRow17" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtPsicoInformador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtPsicoInformadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtPsicoInformadorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>        
                                                                                      
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>


                            </Items>
                        </x:Tab>
                         <x:Tab ID="TabVacio" BodyPadding="5px" Title=" " runat="server">
                            <Items>

                            </Items>
                        </x:Tab>
                    </Tabs>
                </x:TabStrip>
            </Items>
        </x:Panel>
         <x:HiddenField ID="hfRefresh" runat="server" />
          <x:Window ID="WindowAddDX" Title="Nuevo Diagnóstico" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddDX_Close" IsModal="True" Width="650px" Height="480px">
        </x:Window>

        <x:Window ID="WindowAddDXFrecuente" Title="Diagnóstico Frecuente" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddDXFrecuente_Close" IsModal="True" Width="650px" Height="640px">
        </x:Window>

         <x:Window ID="winEdit1" Title="Reporte" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HidePostBack" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top"  IsModal="true"  Height="630px" Width="870px">
    </x:Window>

          <x:Window ID="winEditReco" Title="Recomendación" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
        Target="Top" OnClose="winEditReco_Close" IsModal="True" Width="600px" Height="410px">
    </x:Window>

    <x:Window ID="winEditRestri" Title="Restricción" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
        Target="Top" OnClose="winEditRestri_Close" IsModal="True" Width="600px" Height="410px">
    </x:Window>

    <x:HiddenField ID="highlightRows" runat="server"></x:HiddenField>
    </form>
         <script type="text/javascript">
             var highlightRowsClientID = '<%= highlightRows.ClientID %>';
             var gridClientID = '<%= grdDx.ClientID %>';

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
