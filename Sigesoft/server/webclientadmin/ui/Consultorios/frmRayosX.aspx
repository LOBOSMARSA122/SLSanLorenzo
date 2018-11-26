<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRayosX.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmRayosX" %>
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
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Rayos X">
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
                        <x:Tab ID="TabRayosX" BodyPadding="5px" Title="Rayos X" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarRayosX" Text="Grabar Rayos X" Icon="SystemSave" runat="server" OnClick="btnGrabarRayosX_Click" AjaxLoadingType="Mask" Visible="true"></x:Button>                                   
                                        <x:FileUpload runat="server" ID="fileDocRX" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                        Label="Seleccionar Archivo" ButtonIcon="SystemSearch" OnFileSelected="fileDocRX_FileSelected" AutoPostBack="true" ButtonText="Subir Placa" Readonly="False">
                                        </x:FileUpload>
                                        
                                         <x:FileUpload runat="server" ID="FileInforme" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                        Label="Seleccionar Archivo" ButtonIcon="SystemSearch" OnFileSelected="FileInforme_FileSelected" AutoPostBack="true" ButtonText="Subir Informe" Readonly="False">
                                        </x:FileUpload>
                                        

                                         <x:Button ID="btnDescargarRX" runat="server" Text="Descargar Adjuntos" Icon="ArrowDown" Visible="False"></x:Button>
                                        <x:Label ID="Label746" runat="server" Text="....." ShowLabel="false" Visible="False"></x:Label>
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false" Visible="False"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server" Visible="true"></x:DropDownList>
                                        <x:Button ID="btnReporteRX" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" Visible="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                            <x:Panel ID="Panel29" Title="DATOS DE LA PLACA: " EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="False">
                                <Items>
                                    <x:Form ID="Form90" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow307" ColumnWidths="240px 240px 240px 240px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label346" runat="server" Text="CÓDIGO DE PLACA:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXNroPlaca" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="240"></x:TextBox>
                                                    <x:Label ID="label347" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label348" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                            </x:Panel>
                            <x:Panel ID="Panel28" Title="LA RADIOGRAFÍA DE TÓRAX EN PROYECCIÓN FRONTAL EN INCIDENCIA POSTERO - ANTERIOR; MUESTRA: " EnableBackgroundColor="true" Height="250px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="False">
                                <Items>
                                    <x:Form ID="Form89" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow287" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label187" runat="server" Text="VÉRTICES:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXVertices" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                      
                                            <x:FormRow ID="FormRow289" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label328" runat="server" Text="CAMPOS PULMONARES :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXCamposPulmonares" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                       
                                            <x:FormRow ID="FormRow291" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label330" runat="server" Text="HILIOS : " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXHilios" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                    
                                            <x:FormRow ID="FormRow293" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label332" runat="server" Text="SENOS COSTODIAFRAGMÁTICOS :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXSenosDiafrag" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                     
                                            <x:FormRow ID="FormRow295" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label334" runat="server" Text="SENOS CARDIOFRÉNICOS :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXSenosCardiofre" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                       
                                            <x:FormRow ID="FormRow297" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label336" runat="server" Text="MEDIASTINOS :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXMediastinos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                      
                                            <x:FormRow ID="FormRow299" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label338" runat="server" Text="SILUETA CARDIACA :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXSiluetaCard" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                    
                                            <x:FormRow ID="FormRow301" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label340" runat="server" Text="ÍNDICE CARDIOTORÁXICO :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXInidice" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                        
                                            <x:FormRow ID="FormRow303" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label342" runat="server" Text="PARTES BLANDAS Y ESTRUCTURAS ÓSEAS :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXPartesBlandas" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>                                       
                                                                                  
                                         </Rows>
                                    </x:Form>
                                </Items>
                              </x:Panel>
                            <x:Panel ID="Panel1" Title="DESCRIPCIÓN" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="true">
                                <Items>
                                    <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow1" ColumnWidths="240px 240px 240px 240px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1" runat="server" Text="PLACA NORMAL" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkPlacaNormal" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="240"></x:CheckBox>
                                                    <x:Label ID="label2" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label3" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                            </x:Panel>
                            <x:Panel ID="Panel64" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="False">
                                    <Items>
                                        <x:Form ID="Form210" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow642" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtRadiografiaAuditoria" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtRadiografiaAuditoriaInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtRadiografiaAuditoriaModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow643" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtRadiografiaEvaluacion" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtRadiografiaEvaluacionInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtRadiografiaEvaluacionModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>     
                                                
                                                  <Ext:FormRow ID="FormRow2" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtRadiografiaInformador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtRadiografiaInformadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtRadiografiaInformadorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>                                             
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Tab>
                        <x:Tab ID="TabOIT" BodyPadding="5px" Title="OIT" runat="server" Enabled="true">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarOIT" Text="Grabar OIT" Icon="SystemSave" runat="server" OnClick="btnGrabarOIT_Click" AjaxLoadingType="Mask"></x:Button>     
                                         <x:FileUpload runat="server" ID="fileDocOIT" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                        Label="Seleccionar Excel" ButtonIcon="SystemSearch" OnFileSelected="fileDocOIT_FileSelected" AutoPostBack="true" ButtonText="Subir Adjunto" Readonly="False" Visible="false">
                                        </x:FileUpload>
                                         <x:Button ID="btnDescargarOIT" runat="server" Text="Descargar Adjuntos" Icon="ArrowDown"></x:Button>       
                                        <x:Label ID="Label13" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label14" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabaOIT" runat="server"></x:DropDownList>       
                                        <x:Button ID="btnReporteOIT" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" Visible="true"></x:Button>              
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel35" Title="DATOS DE LA PLACA: " EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="true">
                                    <Items>
                                               <x:Form ID="Form101" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow354" ColumnWidths="160px 160px 160px 160px 160px 160px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label398" runat="server" Text="Código de Placa:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOITCodigoPlaca" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label399" runat="server" Text="Fecha de Lectura:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOITFechaLectura" Label="" ShowLabel="false" CssClass="mright" Width="100px" runat="server" ></x:TextBox>
                                                     <x:Label ID="label400" runat="server" Text="Fecha de Radiografía:" ShowLabel="false"></x:Label>
                                                     <x:TextBox  ID="txtOITFechaToma" Label="" ShowLabel="false" CssClass="mright" Width="100px" runat="server"  ></x:TextBox>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow356" ColumnWidths="160px 160px 160px 160px 160px 160px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label407" runat="server" Text="" ShowLabel="false"></x:Label>
                                                 </Items>
                                            </x:FormRow>
                                             <x:FormRow ID="FormRow355" ColumnWidths="160px 320px 160px 320px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label401" runat="server" Text="Calidad Radiográfica :" ShowLabel="false" ></x:Label>
                                                     <x:DropDownList ID="ddlOITCalidad" runat="server"   Width="300px" ShowLabel="false"></x:DropDownList>
                                                      <x:Label ID="label604" runat="server" Text="Comentarios: " ShowLabel="false" ></x:Label>
                                                     <x:TextBox ID="txtOITComentarios" runat="server" Label="" Width="300px" ShowLabel="false"></x:TextBox>
                                               </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel3" Title="CAUSAS DE MALA CALIDAD: " EnableBackgroundColor="true" Height="65px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="true">
                                    <Items>
                                               <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow3" ColumnWidths="240px 240px 240px 240px" runat="server" >
                                                <Items>
                                                    <x:CheckBox ID="ChckNinguna" runat="server" Text="Ninguna" ShowLabel="true"></x:CheckBox>
                                                    <x:CheckBox ID="ChckSobreExp" runat="server" Text="Sobre-Exposición" ShowLabel="true"></x:CheckBox>
                                                    <x:CheckBox ID="ChckSubExp" runat="server" Text="Sub-Exposición" ShowLabel="true"></x:CheckBox>
                                                    <x:CheckBox ID="ChckPosCent" runat="server" Text="Posición Centrada" ShowLabel="true"></x:CheckBox>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow4" ColumnWidths="160px 160px 160px 160px 160px 160px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label18" runat="server" Text="" ShowLabel="false"></x:Label>
                                                 </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow5" ColumnWidths="240px 240px 240px 240px" runat="server" >
                                                <Items>
                                                    <x:CheckBox ID="ChckBajaInsp" runat="server" Text="Baja Inspiración" ShowLabel="true"></x:CheckBox>
                                                    <x:CheckBox ID="ChckEscapula" runat="server" Text="Escápulas" ShowLabel="true"></x:CheckBox>
                                                    <x:CheckBox ID="ChckArtefact" runat="server" Text="Artefactos" ShowLabel="true"></x:CheckBox>
                                                    <x:CheckBox ID="ChckOtros" runat="server" Text="Otros" ShowLabel="true"></x:CheckBox>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                 <x:Panel ID="Panel4" Title="LECTURA DE PLACA: " EnableBackgroundColor="true" Height="140px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="true">
                                    <Items>
                                       <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow6" ColumnWidths="120px 200px 120px 200px 120px 200px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label16" runat="server" Text="Vértices :" ShowLabel="false" ></x:Label>
                                                     <x:TextArea ID="txtLectVertice" runat="server" Text="" Label="" ShowLabel="False" Height="50" Width="180px"></x:TextArea>
                                                     <x:Label ID="label17" runat="server" Text="Senos :" ShowLabel="false" ></x:Label>
                                                     <x:TextArea ID="txtLectSenos" runat="server" Text="" Label="" ShowLabel="False" Height="50" Width="180px"></x:TextArea>
                                                     <x:Label ID="label19" runat="server" Text="Campos Pulmonares :" ShowLabel="false" ></x:Label>
                                                     <x:TextArea ID="txtLectCampos" runat="server" Text="" Label="" ShowLabel="False" Height="50" Width="180px"></x:TextArea>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow7" ColumnWidths="160px 160px 160px 160px 160px 160px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label15" runat="server" Text="" ShowLabel="false"></x:Label>
                                                 </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow8" ColumnWidths="120px 200px 120px 200px 120px 200px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label20" runat="server" Text="Mediastinos :" ShowLabel="false" ></x:Label>
                                                     <x:TextArea ID="txtLectMediast" runat="server" Text="" Label="" ShowLabel="False" Height="50" Width="180px"></x:TextArea>
                                                    <x:Label ID="label21" runat="server" Text="Hilios :" ShowLabel="false" ></x:Label>
                                                     <x:TextArea ID="txtLectHilios" runat="server" Text="" Label="" ShowLabel="False" Height="50" Width="180px"></x:TextArea>
                                                    <x:Label ID="label22" runat="server" Text="Silueta Cardiovascular :" ShowLabel="false" ></x:Label>
                                                     <x:TextArea ID="txtLectSilueta" runat="server" Text="" Label="" ShowLabel="False" Height="50" Width="180px"></x:TextArea>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel36" Title="II.  ANORMALIDADES PARENQUIMATOSAS" EnableBackgroundColor="true" Height="160px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="4" Layout="Table" Visible="true">
                                    <Items>
                                                    <x:GroupPanel runat="server" Title="2.1. Zonas  Afectadas " ID="GroupPanel38" BoxFlex="1" Height="125"  Width="280">   
                                        <Items>
                                            <x:Form ID="Form105" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                 <x:FormRow ID="FormRow361" ColumnWidths="110px 73px 73px" runat="server">
                                                <Items>
                                                    <x:Label ID="label403" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label404" runat="server" Text="DER." ShowLabel="false"></x:Label>
                                                    <x:Label ID="label405" runat="server" Text="IZQ." ShowLabel="false"></x:Label>  
                                                </Items>
                                                  </x:FormRow>
                                                  <x:FormRow ID="FormRow363" ColumnWidths="110px 73px 73px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label408" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                     <x:FormRow ID="FormRow362" ColumnWidths="110px 73px 73px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label406" runat="server" Text="Superior" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITSuperiorDer" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                               
                                                    <x:CheckBox ID="chkOITSuperiorIzq" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                </Items>
                                            </x:FormRow>
                                                    <x:FormRow ID="FormRow364" ColumnWidths="110px 73px 73px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label409" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                     <x:FormRow ID="FormRow365" ColumnWidths="110px 73px 73px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label410" runat="server" Text="Medio" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITMedioDer" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                               
                                                    <x:CheckBox ID="chkOITMedioIzq" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                </Items>
                                            </x:FormRow> 
                                                    <x:FormRow ID="FormRow366" ColumnWidths="110px 73px 73px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label411" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                     <x:FormRow ID="FormRow367" ColumnWidths="110px 73px 73px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label412" runat="server" Text="Inferior" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITInferiorDer" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                               
                                                    <x:CheckBox ID="chkOITInferiorIzq" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                </Items>
                                            </x:FormRow>   
                                                </Rows>
                                            </x:Form>
                                        </Items>             
                                    </x:GroupPanel>
                                        
                                    <x:GroupPanel runat="server" Title="2.2. Profusión  (opacidades pequeñas)" ID="GroupPanel39" BoxFlex="1" Height="125"  Width="280">
                                         <Items>
                                            <x:Form ID="Form103" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                 <x:FormRow ID="FormRow368" ColumnWidths="30px 55px 30px 55px 30px 55px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label413" runat="server" Text="0/-" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT0_" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>
                                                    <x:Label ID="label414" runat="server" Text="0/0" ShowLabel="false" ></x:Label>
                                                    <x:CheckBox ID="chkOIT0_0" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>
                                                    <x:Label ID="label415" runat="server" Text="0/1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT0_1" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>  
                                                </Items>
                                                  </x:FormRow>
                                                  <x:FormRow ID="FormRow369" ColumnWidths="85px 85px 85px" runat="server">
                                                <Items>
                                                    <x:Label ID="label4" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                  <x:FormRow ID="FormRow370" ColumnWidths="30px 55px 30px 55px 30px 55px" runat="server">
                                                <Items>
                                                    <x:Label ID="label5" runat="server" Text="1/0" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT1_0" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>
                                                    <x:Label ID="label6" runat="server" Text="1/1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT1_1" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>
                                                    <x:Label ID="label7" runat="server" Text="1/2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT1_2" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox> 
                                                </Items>
                                            </x:FormRow>
                                                    <x:FormRow ID="FormRow371" ColumnWidths="85px 85px 85px" runat="server">
                                                <Items>
                                                    <x:Label ID="label8" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                    <x:FormRow ID="FormRow372" ColumnWidths="30px 55px 30px 55px 30px 55px" runat="server">
                                                <Items>
                                                    <x:Label ID="label9" runat="server" Text="2/1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT2_1" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>
                                                    <x:Label ID="label10" runat="server" Text="2/2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT2_2" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>
                                                    <x:Label ID="label11" runat="server" Text="2/3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT2_3" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>  
                                                </Items>
                                            </x:FormRow> 
                                                    <x:FormRow ID="FormRow373" ColumnWidths="85px 85px 85px" runat="server">
                                                <Items>
                                                    <x:Label ID="label12" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                     <x:FormRow ID="FormRow374" ColumnWidths="30px 55px 30px 55px 30px 55px" runat="server">
                                                <Items>
                                                    <x:Label ID="label425" runat="server" Text="3/2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chOIT3_2" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>
                                                    <x:Label ID="label426" runat="server" Text="3/3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chOIT3_3" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>
                                                    <x:Label ID="label427" runat="server" Text="3/+" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chOIT3_" runat="server" Label="" ShowLabel="false" OnCheckedChanged="ChkProfusion_OnCheckedChanged" AutoPostBack="true"></x:CheckBox>                                            
                                                </Items>
                                            </x:FormRow>   
                                                </Rows>
                                            </x:Form>
                                        </Items>                
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="2.3.  Forma y Tamaño : " ID="GroupPanel40" BoxFlex="1" Height="125"  Width="280">      
                                         <Items >
                                             <x:Form ID="Form104" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                 <x:FormRow ID="FormRow375" ColumnWidths="120px 120px" runat="server" >
                                            <Items>
                                                    <x:Label ID="label429" runat="server" Text="Primaria" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label430" runat="server" Text="Secundaria" ShowLabel="false"></x:Label>  
                                                </Items>
                                                    </x:FormRow>
                                                      <x:FormRow ID="FormRow376" ColumnWidths="10px 50px 10px 50px 10px 50px 10px 50px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label428" runat="server" Text="p" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITPrimariap" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label431" runat="server" Text="s" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITPrimarias" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label432" runat="server" Text="p" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkSecundariap" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label434" runat="server" Text="s" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkSecundarias" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                  <x:FormRow ID="FormRow377" ColumnWidths="85px 85px 85px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label433" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                    <x:FormRow ID="FormRow378" ColumnWidths="10px 50px 10px 50px 10px 50px 10px 50px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label435" runat="server" Text="q" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITPrimariaq" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label436" runat="server" Text="t" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITPrimariat" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label437" runat="server" Text="q" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITSecundariaq" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label438" runat="server" Text="t" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITSecundariat" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                  <x:FormRow ID="FormRow379" ColumnWidths="85px 85px 85px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label439" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                    <x:FormRow ID="FormRow380" ColumnWidths="10px 50px 10px 50px 10px 50px 10px 50px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label440" runat="server" Text="r" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITPrimariar" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label441" runat="server" Text="u" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITPrimariau" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label442" runat="server" Text="r" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITSecundariar" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label443" runat="server" Text="u" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOITSecundariau" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                </Rows>
                                            </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="2.4.Opacidades" ID="GroupPanel34" BoxFlex="1" Height="125"  Width="120">      
                                         <Items>
                                            <x:Form ID="Form102" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="10px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow357" runat="server">
                                                        <Items>                                                           
                                                            <x:CheckBox ID="chkOITOpacidades0" runat="server" Text="0" ShowLabel="true"></x:CheckBox>                                                               
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow358" runat="server">
                                                        <Items>                                                           
                                                            <x:CheckBox ID="chkOITOpacidadesA" runat="server" Text="A" ShowLabel="true"></x:CheckBox>                                                               
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow359" runat="server">
                                                        <Items>                                                           
                                                            <x:CheckBox ID="chkOITOpacidadesB" runat="server" Text="B" ShowLabel="true"></x:CheckBox>                                                                
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow360" runat="server">
                                                        <Items>                                                           
                                                            <x:CheckBox ID="chkOITOpacidadesC" runat="server" Text="C" ShowLabel="true"></x:CheckBox>                                                                
                                                        </Items> 
                                                    </x:FormRow>     
                                                </Rows>
                                            </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel37" Title="III. ANORMALIDADES PLEURALES " EnableBackgroundColor="true" Height="35px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="true">
                                    <Items>
                                        <x:Form ID="Form106" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                            <Rows>
                                               <x:FormRow ID="FormRow381" ColumnWidths="320px 160px 160px " runat="server" >
                                                    <Items>
                                                         <x:Label ID="label444" runat="server" Text="(si NO hay anormalidades pase a símbolos *)" ShowLabel="false"></x:Label>
                                                        
                                                        <%--<x:CheckBox ID="chkAnormalidadesSI" runat="server" Text="SI" ShowLabel="true"></x:CheckBox>--%>
                                                        <x:RadioButton ID="rdoAnormalidadesSI" GroupName="anormalidades" runat="server" Text="SI" ShowLabel="true" OnCheckedChanged="RbnAnormalidadesPleurales_OnCheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                        <%--<x:CheckBox ID="chkAnormalidadesNO" runat="server" Text="NO" ShowLabel="true"></x:CheckBox>--%> 
                                                        <x:RadioButton ID="rdoAnormalidadesNO" GroupName="anormalidades" runat="server" Text="NO" ShowLabel="true" OnCheckedChanged="RbnAnormalidadesPleurales_OnCheckedChanged" AutoPostBack="true"></x:RadioButton> 
                                                        </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel38" Title="3.1. Placas Pleurales (0=Ninguna, D=Hemitórax derecho; I= Hemitórax izquierdo) " EnableBackgroundColor="true" Height="140px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="4" Layout="Table" Visible="true">
                                    <Items>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel36" BoxFlex="1" Height="115"  Width="280">   
                                        <Items>
                                           <x:Form ID="Form107" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow382" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label445" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow383" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label446" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow384" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label447" runat="server" Text="Sitio (Marque  las  casillas adecuadas)" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow385" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label448" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                          </x:Form>
                                        </Items>             
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel37" BoxFlex="1" Height="115"  Width="120">
                                         <Items>
                                          <x:Form ID="Form108" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow386" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label449" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow387" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label450" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow388" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label451" runat="server" Text="Calcificación (marque)" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow389" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label452" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                          </x:Form> 
                                        </Items>                
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel41" BoxFlex="1" Height="115"  Width="280">      
                                         <Items>
                                             <x:Form ID="Form109" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow390" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label453" runat="server" Text="Extensión  (pared  Torácica; combinada  para  placas  de  perfil  y de frente)" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow391" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label454" runat="server" Text="1. <  ¼  de  la  pared  lateral de tórax" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow392" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label455" runat="server" Text="2.  Entre ¼ y ½ de la pared lateral del tórax" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow393" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label456" runat="server" Text="3. >  ½  de  la  pared  lateral del tórax" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                          </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel42" BoxFlex="1" Height="115"  Width="280">      
                                         <Items>
                                            <x:Form ID="Form110" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow394" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label457" runat="server" Text="Ancho (opcional) (ancho mínimo exigido: 3 mm)" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow395" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label458" runat="server" Text="a. De 3 a 5 mm " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow396" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label459" runat="server" Text="b. De 5 a 10 mm " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow397" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label460" runat="server" Text="c. Mayor a 10 mm " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                          </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel39" Title="" EnableBackgroundColor="true" Height="225px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" TableConfigColumns="4" Layout="Table" Visible="true">
                                    <Items>
                                         <x:GroupPanel runat="server" Title="-" ID="GroupPanel43" BoxFlex="1" Height="195"  Width="280">   
                                        <Items>
                                          <x:Form ID="Form111" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                <x:FormRow ID="FormRow399" ColumnWidths="90px 150px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label463" runat="server" Text="De Perfil" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlPerfilPlacaPleurales" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox38" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label465" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox39" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label466" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox40" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow398" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label461" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow400" ColumnWidths="90px 150px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label462" runat="server" Text="De frente" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlFrentePlacaPleurales" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox37" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label468" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox41" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label469" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox42" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow401" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label470" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow402" ColumnWidths="90px 150px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label471" runat="server" Text="Diafragma" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlDiafragmaPlacaPleurales" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox43" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label473" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox44" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label474" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox45" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow403" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label475" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow404" ColumnWidths="90px 150px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label476" runat="server" Text="Otro(s) Sitio(s)" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlOtrosPlacaPleurales" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox46" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label478" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox47" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label479" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox48" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow405" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label480" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                   </Rows>
                                              </x:Form>
                                        </Items>             
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel44" BoxFlex="1" Height="195"  Width="120">
                                         <Items>
                                          <x:Form ID="Form112" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                <x:FormRow ID="FormRow406" ColumnWidths="90px" runat="server" >
                                                <Items>
                                                   <x:DropDownList ID="ddlPerfilCalcifica" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox49" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label483" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox50" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label484" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox51" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow407" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label485" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow408" ColumnWidths="90px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlFrenteCalcifica" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox52" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label488" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox53" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label489" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox54" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow409" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label490" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow410" ColumnWidths="90px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlDiafragmaCalcifica" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox55" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label493" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox56" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label494" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox57" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow411" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label495" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow412" ColumnWidths="90px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlOtrosCalcifica" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox58" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label498" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox59" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label499" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox60" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow413" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label500" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    </Rows>
                                              </x:Form>
                                        </Items>                
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel45" BoxFlex="1" Height="195"  Width="280">      
                                         <Items>
                                            <x:Form ID="Form113" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                            <Rows>
                                            <x:FormRow ID="FormRow414" ColumnWidths="23px 43px 43px 43px 43px 43px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label481" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label501" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label486" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label502" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label491" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label503" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow415" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label496" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow416" ColumnWidths="90px 60px 90px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlExtensionDerPlacas" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox61" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label505" runat="server" Text="2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox62" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label506" runat="server" Text="3" ShowLabel="false"></x:Label>--%>
                                                    <%--<x:CheckBox ID="CheckBox63" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                      <x:Label ID="label511" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlExtensionIzqPlacas" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                   <%-- <x:CheckBox ID="CheckBox64" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label508" runat="server" Text="2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox65" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label509" runat="server" Text="3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox66" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow417" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label510" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow423" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label531" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow424" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label532" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow425" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label533" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow426" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label534" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow427" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label535" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow428" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label536" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow429" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label537" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                  <x:FormRow ID="FormRow422" ColumnWidths="90px 150px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label527" runat="server" Text="Obliteración del Angulo Costofrenico" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlObliAngulo" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox73" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label529" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox74" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label530" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox75" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                            </Rows>
                                            </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel46" BoxFlex="1" Height="195"  Width="280">      
                                         <Items>
                                           
                                            <x:Form ID="Form114" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                            <Rows>
                                            <x:FormRow ID="FormRow418" ColumnWidths="43px 43px 43px 43px 43px 43px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label512" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label513" runat="server" Text="D " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label514" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label515" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label516" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label517" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow419" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label518" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow420" ColumnWidths="120px 18px 120px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlAnchoDerPlacas" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox67" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label520" runat="server" Text="b" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox68" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label521" runat="server" Text="c" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox69" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                      <x:Label ID="label522" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                   <x:DropDownList ID="ddlAnchoIzqPlacas" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox70" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label524" runat="server" Text="b" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox71" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label525" runat="server" Text="c" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox72" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow421" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label526" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                            </x:Form>
                                                                
                                        </Items>                          
                                    </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel40" Title="3.2. Engrosamiento Difuso de la Pleura (0=Ninguna, D=Hemitórax derecho; I= Hemitórax izquierdo) " EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="4" Layout="Table" Visible="true">
                                    <Items>
                                          <x:GroupPanel runat="server" Title="-" ID="GroupPanel47" BoxFlex="1" Height="40"  Width="240">   
                                        <Items>
                                           <x:Form ID="Form115" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow432" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label540" runat="server" Text="Pared Torácica " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                          </x:Form>
                                        </Items>             
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel48" BoxFlex="1" Height="40"  Width="240">
                                         <Items>
                                          <x:Form ID="Form116" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow436" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label544" runat="server" Text="Calcificación" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                          </x:Form> 
                                        </Items>                
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel49" BoxFlex="1" Height="40"  Width="240">      
                                         <Items>
                                             <x:Form ID="Form117" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow438" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label546" runat="server" Text="Extensión " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                          </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel50" BoxFlex="1" Height="40"  Width="240">      
                                         <Items>
                                            <x:Form ID="Form118" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow442" ColumnWidths="280px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label550" runat="server" Text="Ancho" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                          </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel41" Title="" EnableBackgroundColor="true" Height="130px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" TableConfigColumns="4" Layout="Table" Visible="true">
                                    <Items>
                                         <x:GroupPanel runat="server" Title="-" ID="GroupPanel51" BoxFlex="1" Height="100"  Width="240">   
                                        <Items>
                                          <x:Form ID="Form119" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                <x:FormRow ID="FormRow446" ColumnWidths="60px 150px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label554" runat="server" Text="De Perfil" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlPerfilEngrosa" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox76" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label556" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox77" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label557" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox78" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow447" ColumnWidths="60px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label558" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow448" ColumnWidths="60px 150px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label559" runat="server" Text="De frente" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlFrenteEngrosa" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                   <%-- <x:CheckBox ID="CheckBox79" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label561" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox80" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label562" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox81" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                    </Rows>
                                              </x:Form>
                                        </Items>             
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel52" BoxFlex="1" Height="100"  Width="240">
                                         <Items>
                                          <x:Form ID="Form120" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                <x:FormRow ID="FormRow454" ColumnWidths="180px" runat="server" >
                                                <Items>
                                                   <x:DropDownList ID="ddlPerfilCalcificaEngrosa" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox88" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label575" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox89" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label576" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox90" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow455" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label577" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow456" ColumnWidths="180px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlFrenteCalcificaEngrosa" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox91" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label579" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox92" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label580" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox93" runat="server" Label="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                   <x:FormRow ID="FormRow461" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label589" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    </Rows>
                                              </x:Form>
                                        </Items>                
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel53" BoxFlex="1" Height="100"  Width="240">      
                                         <Items>
                                            <x:Form ID="Form121" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                            <Rows>
                                            <x:FormRow ID="FormRow462" ColumnWidths="35px 35px 35px 35px 35px 35px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label590" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label591" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label592" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label593" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label594" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label595" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow463" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label596" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow464" ColumnWidths="99px 18px 99px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlExtensionEngrosaDer" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox100" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label598" runat="server" Text="2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox101" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label599" runat="server" Text="3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox102" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                      <x:Label ID="label600" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlExtensionEngrosaIzq" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox103" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label602" runat="server" Text="2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox104" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label603" runat="server" Text="3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox105" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                             </Rows>
                                            </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel54" BoxFlex="1" Height="100"  Width="240">      
                                         <Items>
                                           
                                            <x:Form ID="Form122" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                            <Rows>
                                            <x:FormRow ID="FormRow474" ColumnWidths="35px 35px 35px 35px 35px 35px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label616" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label617" runat="server" Text="D " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label618" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label619" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label620" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label621" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow475" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label622" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow476" ColumnWidths="99px 18px 99px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlAnchoEngrosaDer" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox109" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label624" runat="server" Text="b" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox110" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label625" runat="server" Text="c" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox111" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                      <x:Label ID="label626" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlAnchoEngrosaIzq" runat="server"   Width="80px" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:CheckBox ID="CheckBox112" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label628" runat="server" Text="b" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox113" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label629" runat="server" Text="c" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox114" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow477" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label630" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                            </x:Form>
                                                                
                                        </Items>                          
                                    </x:GroupPanel>
                                   </Items>
                                </x:Panel>
                                <x:Panel ID="Panel42" Title="IV. SIMBOLOS * " EnableBackgroundColor="true" Height="35px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="true">
                                    <Items>
                                        <x:Form ID="Form123" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow430" ColumnWidths="320px 160px 160px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label538" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <%--<x:CheckBox ID="chkSimboloSi" runat="server" Text="SI" ShowLabel="true"></x:CheckBox>--%>
                                                     <x:RadioButton ID="rdoSimboloSi" GroupName="simbolos" runat="server" Text="SI" ShowLabel="true" OnCheckedChanged="RbnSimbolos_OnCheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                    <%--<x:CheckBox ID="chkSimboloNo" runat="server" Text="NO" ShowLabel="true"></x:CheckBox>--%>  
                                                    <x:RadioButton ID="rdoSimboloNo" GroupName="simbolos" runat="server" Text="NO" ShowLabel="true" OnCheckedChanged="RbnSimbolos_OnCheckedChanged" AutoPostBack="true"></x:RadioButton>                                                   
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel43" Title="(Marque la respuesta adecuada; si marcaa od, escriba a continuación un COMENTARIO) " EnableBackgroundColor="true" Height="105px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="2" Layout="Table" Visible="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="-" ID="GroupPanel55" BoxFlex="1" Height="75"  Width="900">   
                                        <Items>
                                          <x:Form ID="Form124" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow431" ColumnWidths="15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label539" runat="server" Text="aa" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITaa" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label541" runat="server" Text="at" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITat" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label542" runat="server" Text="ax" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITax" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label543" runat="server" Text="bu" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITbu" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label545" runat="server" Text="ca" ShowLabel="false"></x:Label>  
                                                     <x:CheckBox ID="chkOITca" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label547" runat="server" Text="cg" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITcg" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label548" runat="server" Text="cn" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITcn" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label549" runat="server" Text="co" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITco" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label551" runat="server" Text="cp" ShowLabel="false"></x:Label> 
                                                     <x:CheckBox ID="chkOITcp" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label552" runat="server" Text="cv" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITcv" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label553" runat="server" Text="di" ShowLabel="false"></x:Label>  
                                                     <x:CheckBox ID="chkOITdi" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label563" runat="server" Text="ef" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITef" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label564" runat="server" Text="em" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITem" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label565" runat="server" Text="es" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITes" runat="server" Text="" ShowLabel="false"></x:CheckBox> 
                                                    </Items>
                                            </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                            
                                        <x:Form ID="Form125" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow434" ColumnWidths="15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label567" runat="server" Text="fr " ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITfr" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label568" runat="server" Text="hi " ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOIThi" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label569" runat="server" Text="ho" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITho" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label570" runat="server" Text="id" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITid" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label571" runat="server" Text="ih" ShowLabel="false"></x:Label>  
                                                     <x:CheckBox ID="chkOITih" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label572" runat="server" Text="kl" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITkl" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label573" runat="server" Text="me" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITme" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label581" runat="server" Text="pa" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITpa" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label582" runat="server" Text="pb" ShowLabel="false"></x:Label> 
                                                     <x:CheckBox ID="chkOITpb" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label583" runat="server" Text="pi " ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITpi" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label584" runat="server" Text="px" ShowLabel="false"></x:Label>  
                                                     <x:CheckBox ID="chkOITpx" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label585" runat="server" Text="ra" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITra" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label586" runat="server" Text="rp" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITrp" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label587" runat="server" Text="tb" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITtb" runat="server" Text="" ShowLabel="false"></x:CheckBox> 
                                                    </Items>
                                            </x:FormRow>
                                               </Rows>
                                                </x:Form>
                                    </Items>             
                                    </x:GroupPanel>
                                          <x:GroupPanel runat="server" Title="-" ID="GroupPanel56" BoxFlex="1" Height="75"  Width="60">
                                         <Items>
                                        <x:Form ID="Form126" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow433" ColumnWidths="15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px 15px 45px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label566" runat="server" Text="od" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkOITod" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                        </Items>                
                                    </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel44" Title="COMENTARIOS " EnableBackgroundColor="true" Height="35px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="true">
                                <Items>
                                    <x:Form ID="Form127" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow435" ColumnWidths="120px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label588" runat="server" Text="Comentarios:" ShowLabel="false"></x:Label>
                                                      <x:TextBox ID="txtOITComentariosod" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                      
                                                    </Items>
                                            </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel83" Title="HALLAZGOS" EnableBackgroundColor="true" Height="40px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="true">
                                    <Items>
                                         <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="180px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow17" ColumnWidths="250px" runat="server">
                                                    <Items>
                                                        <x:CheckBox ID="chkSinNeumoconi" runat="server" Label="SIN NEUMOCONIOSIS"></x:CheckBox>                                                  
                                                    </Items>
                                                </x:FormRow>                                                    
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>  
                                <x:Panel ID="Panel65" Title="AUDITORÍA" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="false">
                                    <Items>
                                        <x:Form ID="Form211" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow645" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOITAuditoria" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOITAuditoriaInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOITAuditoriaModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow647" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOITEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOITEvaluadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOITEvaluadorActualizador" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
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
        <x:Window ID="Window2" Title="Descargar" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
            Target="Top"  IsModal="True" Width="450px" Height="370px">
        </x:Window>
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
