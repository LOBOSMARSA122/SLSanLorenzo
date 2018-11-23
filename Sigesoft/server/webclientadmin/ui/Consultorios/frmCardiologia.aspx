<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCardiologia.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmCardiologia" %>
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
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Cardiología">
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
                        <x:Tab ID="TabElectrocardiograma" BodyPadding="5px" Title="Electrocardiograma" runat="server">
                                <Toolbars>
                                    
                                <x:Toolbar ID="Toolbar1" runat="server">

                                    <Items>
                                        <x:Button ID="btnGrabarElectroCardiograma" Text="Grabar Electrocardiograma" Icon="SystemSave" runat="server" OnClick="btnGrabarElectrocardiograma_Click" AjaxLoadingType="Mask"></x:Button>                                   
                                          <x:FileUpload runat="server" ID="FileUpload1" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                        Label="Seleccionar Excel" ButtonIcon="SystemSearch" OnFileSelected="fileDoc_FileSelected" AutoPostBack="true" ButtonText="Subir Adjunto" Readonly="False">
                                        </x:FileUpload>
                                         <x:Button ID="btnDescarga_Yana" runat="server" Text="Descargar Adjuntos" Icon="ArrowDown"></x:Button>
                                        <x:Label ID="Label20" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label21" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server"></x:DropDownList>
                                        <x:Button ID="btnElectroCardio" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                                </Toolbars>
                             <Items>
                               <x:Panel ID="panel3" Title="ELECTROCARDIOGRAMA" EnableBackgroundColor="true" Height="320px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="INFORME DESCRIPTIVO" ID="GroupPanel7" AutoWidth="true" BoxFlex="1" Height="110">
                                            <Items>
                                                <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow1" ColumnWidths="160px 60px  160px 110px  160px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label201" runat="server" Text="FRECUENCIA CARDIACA:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtECG_Frecuencia" runat="server" Text="" ShowLabel="false" ></x:TextBox>  

                                                            <x:Label ID="label211" runat="server" Text="RITMO CARDIACO:" ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlECG_Ritmo" runat="server" Text="" ShowLabel="false" Width="100px"></x:DropDownList>  

                                                            <x:Label ID="label221" runat="server" Text="INTERVALO PR:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtECG_PR" runat="server" Text="" ShowLabel="false" ></x:TextBox> 
                                                    
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow3" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label5" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow4" ColumnWidths="160px 60px  160px 110px  160px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label231" runat="server" Text="INTERVALO QT:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtECG_QT" runat="server" Text="" ShowLabel="false"></x:TextBox>  

                                                            <x:Label ID="label241" runat="server" Text="EJE CARDIACO:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtECG_Eje" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>  

                                                            <x:Label ID="label251" runat="server" Text="SEGMENTO ST:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtECG_ST" runat="server" Text="" ShowLabel="false"></x:TextBox>          
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow12" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label1" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow13" ColumnWidths="160px 60px  " runat="server" >
                                                        <Items>
                                                            <x:Label ID="label2" runat="server" Text="INTERVALO QRS:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtECG_QRS" runat="server" Text="" ShowLabel="false"></x:TextBox>                                                                                                                                                               
                                                        </Items>
                                                    </x:FormRow>                          
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="OTRAS ALTERACIONES ELECTROCARDIOGRÁFICAS" ID="GroupPanel8" AutoWidth="true" BoxFlex="1" Height="90">
                                            <Items>
                                                <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left" >
                                                    <Rows>
                                                         <x:FormRow ID="FormRow8" ColumnWidths="100px 300px 100px 310px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label26" runat="server" Text="DESCRIPCION:" ShowLabel="false"></x:Label>
                                                                <x:TextArea ID="txtECG_OtrasAlter" runat="server" Text="" Label="" ShowLabel="false" Width="200" Height="60"></x:TextArea>
                                                                <x:Label ID="label22" runat="server" Text="CONCLUSIONES:" ShowLabel="false"></x:Label>  
                                                                <x:DropDownList ID="ddlECG_Conclusiones" runat="server" ShowLabel="false" Width="300px" ></x:DropDownList>  
                                                            </Items>
                                                        </x:FormRow>
                                                     </Rows>
                                                </x:Form>                                 
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="CONCLUSIONES" ID="GroupPanel9" AutoWidth="true" BoxFlex="1" Height="50">
                                            <Items>
                                                <x:Form ID="Form13" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow6" ColumnWidths="100px 160px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label4" runat="server" Text="CONCLUSIONES:" ShowLabel="false"></x:Label>  
                                                                <x:DropDownList ID="ddlECG_CONCLUSIONES_GEN" runat="server" ShowLabel="false" Width="150px" ></x:DropDownList>  
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>                                         
                                            </Items>
                                        </x:GroupPanel>
                                        
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Tab>
                        <x:Tab ID="TabApendiceEKG" BodyPadding="5px" Title="Apéndice N°5 - EKG" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarApendiceN5" Text="Grabar Apendice N°5 - EKG" Icon="SystemSave" runat="server" OnClick="btnGrabarApendiceN5_Click" AjaxLoadingType="Mask"></x:Button>                                   
                                          <x:FileUpload runat="server" ID="fileDoc" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                        Label="Seleccionar Excel" ButtonIcon="SystemSearch" OnFileSelected="fileDoc_FileSelected" AutoPostBack="true" ButtonText="Subir Adjunto" Readonly="False">
                                        </x:FileUpload>
                                         <x:Button ID="btnDescargar" runat="server" Text="Descargar Adjuntos" Icon="ArrowDown"></x:Button>
                                        <x:Label ID="Label746" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabarApendice" runat="server"></x:DropDownList>
                                        <x:Button ID="apendiceN5EKG" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items> 
                                <x:Panel ID="panel4" Title="INFORME DESCRIPTIVO" EnableBackgroundColor="true" Height="320px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="1.- INTERPRETACIÓN DEL ELECTROCARDIOGRAMA" ID="GroupPanel10000" AutoWidth="true" BoxFlex="1" Height="100">
                                            <Items>
                                                <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow91" ColumnWidths="160px 60px  160px 110px  160px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="lblFC" runat="server" Text="FRECUENCIA CARDIACA:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtFRECUENCIA_PR_APENDICE_EKG" runat="server" Text="" ShowLabel="false" ></x:TextBox>  

                                                            <x:Label ID="lblRC" runat="server" Text="RITMO CARDIACO:" ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlRITMO_APENDICE_EKG" runat="server" ShowLabel="false"  Width="100px"></x:DropDownList>

                                                            <x:Label ID="lblPR" runat="server" Text="INTERVALO PR:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtINTERVALO_PR_APENDICE_EKG" runat="server" Text="" ShowLabel="false" ></x:TextBox>                                                            
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow94" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label41" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow96" ColumnWidths="160px 60px  160px 110px  160px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label6" runat="server" Text="INTERVALO QT:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtINTERVALO_QT_APENDICE_EKG" runat="server" Text="" ShowLabel="false" ></x:TextBox>  

                                                            <x:Label ID="label7" runat="server" Text="EJE CARDIACO:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtEJE_CARDIACO_APENDICE_EKG" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>  

                                                            <x:Label ID="label8" runat="server" Text="SEGMENTO ST:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtSEGMENTO_ST_APENDICE_EKG" runat="server" Text="" ShowLabel="false" ></x:TextBox>
                                                                   
                                                        </Items>
                                                    </x:FormRow>                        
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="2.- OTRAS ALTERACIONES ELECTROCARDIOGRÁFICAS" ID="GroupPanel4" AutoWidth="true" BoxFlex="1" Height="90">
                                            <Items>
                                                <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow11" ColumnWidths="160px 210px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label13" runat="server" Text="OTRAS ALTERACIONES" ShowLabel="false"></x:Label>
                                                                <x:TextArea ID="txtOTRAS_ALTER_APENDICE_EKG" runat="server" Text="" ShowLabel="false" Width="200" Height="60"></x:TextArea>
                                                            </Items>
                                                        </x:FormRow>                            
                                                    </Rows>
                                                </x:Form>
                                                
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="3.- CONCLUSIONES" ID="GroupPanel5" AutoWidth="true" BoxFlex="1" Height="50">
                                            <Items>
                                                <x:Form ID="Form10" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow10" ColumnWidths="160px 310px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label14" runat="server" Text="CONCLUSIONES" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlCONCLUSIONES_APENDICE_EKG" runat="server" ShowLabel="false" Width="300px"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>                            
                                                    </Rows>
                                                </x:Form>
                                                
                                            </Items>
                                        </x:GroupPanel>
                                        
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelPruebaEsfuerzo" Title="PRUEBA DE ESFUERZO" EnableBackgroundColor="true" Height="75px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="PRUEBA DE ESFUERZO" ID="GroupPanel6" AutoWidth="true" BoxFlex="1" Height="50">
                                            <Items>
                                                <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow5" ColumnWidths="160px 210px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label15" runat="server" Text="EXAMEN NORMAL" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlEXAMEN_NORMAL" runat="server" ShowLabel="false" Width="200px"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>                            
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>                                       
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel366" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form2142" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow6495" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtCardiologiaAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtCardiologiaAuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtCardiologiaAuditorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow6451" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtCardiologiaEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtCardiologiaEvaluadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtCardiologiaEvaluadorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>     
                                                 <Ext:FormRow ID="FormRow208" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtCardiologiaInformador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtCardiologiaInformadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtCardiologiaInformadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>                                   
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>


                            </Items>
                        </x:Tab>
                        <x:Tab ID="TabInformeElecCardio" BodyPadding="5px" Title="Inf. Electrocardio" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <x:Button ID="btnGuardarInfElecCardio" Text="Grabar Informe" Icon="SystemSave" runat="server" OnClick="btnGuardarInfElecCardio_Click" AjaxLoadingType="Mask"></x:Button>                                   
                                          <x:FileUpload runat="server" ID="FileUpload2" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                        Label="Seleccionar Excel" ButtonIcon="SystemSearch" OnFileSelected="fileDoc_FileSelected" AutoPostBack="true" ButtonText="Subir Adjunto" Readonly="False">
                                        </x:FileUpload>
                                         <x:Button ID="Button2" runat="server" Text="Descargar Adjuntos" Icon="ArrowDown"></x:Button>
                                        <x:Label ID="Label3" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label9" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabarInfElecCardio" runat="server"></x:DropDownList>
                                        <x:Button ID="Button4" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items> 
                                <x:Panel ID="panel1" Title="INFORME ELECTRO-CARDIO" EnableBackgroundColor="true" Height="270px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="1.- INFORME ELECTRO-CARDIOGRÁFICO" ID="GroupPanel10" AutoWidth="true" BoxFlex="1" Height="100">
                                            <Items>
                                                <x:Form ID="Form6" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow2" ColumnWidths="160px 60px  160px 110px  160px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="Label10" runat="server" Text="FRECUENCIA CARDIACA:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtFRECU_ELEC_CARDIO" runat="server" Text="" ShowLabel="false" ></x:TextBox>  

                                                            <x:Label ID="Label11" runat="server" Text="RITMO CARDIACO:" ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlRITMO_ELEC_CARDIO" runat="server" ShowLabel="false"  Width="100px"></x:DropDownList>

                                                            <x:Label ID="Label12" runat="server" Text="INTERVALO PR:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtINTERVALO_ELEC_CARDIO" runat="server" Text="" ShowLabel="false" ></x:TextBox>                                                            
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow7" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label16" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow9" ColumnWidths="160px 60px  160px 110px  160px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label17" runat="server" Text="COMPLE QRS:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtCOMPLEJO_QRS_ELEC_CARDIO" runat="server" Text="" ShowLabel="false" ></x:TextBox>  

                                                            <x:Label ID="label18" runat="server" Text="INTERVALO QTC:" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtINTERVALO_QTC_ELEC_CARDIO" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>  

                                                            <x:Label ID="label19" runat="server" Text="EJE CARDIO" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtEJE_ELEC_CARDIO" runat="server" Text="" ShowLabel="false" ></x:TextBox>
                                                                   
                                                        </Items>
                                                    </x:FormRow>                        
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="2.- CONCLUSIONES" ID="GroupPanel11" AutoWidth="true" BoxFlex="1" Height="90">
                                            <Items>
                                                <x:Form ID="Form7" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow14" ColumnWidths="160px 310px 160px 210px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label23" runat="server" Text="HALLAZGO" ShowLabel="false"></x:Label>
                                                                <x:TextArea ID="txtHALLAZGO_ELEC_CARDIO" runat="server" Text="" ShowLabel="false" Width="200" Height="60"></x:TextArea>
                                                                <x:Label ID="Label27" runat="server" Text="OBSERVACIONES" ShowLabel="false"></x:Label>
                                                                <x:TextArea ID="txtOBSERVACIONES_ELEC_CARDIO" runat="server" Text="" ShowLabel="false" Width="200" Height="60"></x:TextArea>
                                                            </Items>
                                                        </x:FormRow>                            
                                                    </Rows>
                                                </x:Form>
                                                
                                            </Items>
                                        </x:GroupPanel>                                       
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Tab>                             
                    </Tabs>
                </x:TabStrip>
            </Items>
        </x:Panel>
          <x:HiddenField ID="hfRefresh" runat="server" />
        <x:Window ID="Window2" Title="Descargar" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
            Target="Top"  IsModal="True" Width="450px" Height="370px" >
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
       Target="Top"  IsModal="true"  Height="630px" Width="870px" >
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
