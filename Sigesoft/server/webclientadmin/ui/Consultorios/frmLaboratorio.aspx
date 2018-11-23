<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLaboratorio.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmLaboratorio" %>
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
         <x:Panel ID="Panel2" runat="server" Height="10000px" Width="1000px" ShowBorder="True"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Laboratorio">
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
                <x:TabStrip ID="TabStrip1" Width="1000px" Height="10000px" ShowBorder="true" ActiveTabIndex="0" runat="server" EnableTitleBackgroundColor="False">
                    <Tabs>
                     
                          <x:Tab ID="TabLaboratorio" BodyPadding="5px" Title="Laboratorio" runat="server">
                                 <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarLaboratorio" Text="Grabar Laboratorio" Icon="SystemSave" runat="server" OnClick="btnGrabarLaboratorio_Click" AjaxLoadingType="Mask"></x:Button>                                   
                                     
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporteLab" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                
                                 <x:Panel ID="PanelGrupoyfactorsanguineo" Title="GRUPO Y FACTOR SANGUÍNEO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form268" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow707" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1335" runat="server" Text="Grupo" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlGRUPO_SANGUINEO" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1336" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1380" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label1337" runat="server" Text="Factor RH" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlFACTOR_RH" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1338" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelExamencompletodeorina" Title="EXAMEN COMPLETO DE ORINA" EnableBackgroundColor="true" Height="720" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table"  Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title="EXAMEN MACROSCÓPICO" ID="GroupPanel27" BoxFlex="1" Height="120" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form47" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow91" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1" runat="server" Text="COLOR" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlCOLOR_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label2" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label4" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label8" runat="server" Text="ASPECTO" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlASPECTO_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList> 
                                                             <x:Label ID="label11" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow94" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label41" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow96" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label42" runat="server" Text="DENSIDAD" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtDENSIDAD_EX_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label474" runat="server" Text="g/L" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label475" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label476" runat="server" Text="DENSIDAD DESEABLE" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtDENSIDAD_EX_ORINA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label477" runat="server" Text="g/L" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow98" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label478" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow100" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label479" runat="server" Text="POTENCIAL HIDRÓGENO" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtPH_EX_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label480" runat="server" Text="pH" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label481" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label482" runat="server" Text="POTENCIAL HIDRÓGENO DESEABLE" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtPH_EX_ORINA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label483" runat="server" Text="pH" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                  </Rows>
                                            </x:Form>                
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="EXAMEN MICROSCÓPICO" ID="GroupPanel26" BoxFlex="1" Height="280" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form46" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow3" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label5" runat="server" Text="LEUCOCITOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtLEUCOCITOS_EX_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label6" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label7" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label9" runat="server" Text="HEMATÍES" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATIES_EX_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox> 
                                                             <x:Label ID="label10" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow5" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label12" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow26" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label13" runat="server" Text="GÉRMENES" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlGERMENES_EX_ORINA" runat="server"  Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label14" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label16" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label17" runat="server" Text="CRIST. DEOXALATOS CALCIO" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlCRISTALES_DEOX_CALCIO" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label18" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow28" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label19" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow30" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label20" runat="server" Text="CRIST. DEUR AMORFOS" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlCRISTALES_DEUR_AMORFOS" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label21" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label22" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label23" runat="server" Text="CRIST. FOSF. AMORFOS" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlCRISTALES_FOSF_AMORFOS" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label24" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow59" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label27" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow64" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label28" runat="server" Text="CRIST. FOSF. TRIPLES" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlCRISTALES_FOSF_TRIPLES" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label30" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label31" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label32" runat="server" Text="CELULAS EPITELIALES" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCELULAS_EPITELIALES_EX_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label33" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow66" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label34" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow73" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label35" runat="server" Text="CILINDROS HIALINOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCILINDROS_HILAINOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label37" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label38" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label39" runat="server" Text="CILINDROS GRANULOSOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCILINDROS_GRANULOSOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label40" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow78" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label47" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow80" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label48" runat="server" Text="CILINDROS LEUCOCITARIOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCILINDROS_LEUCOCITARIOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label245" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label458" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label459" runat="server" Text="CILINDROS HEMÁTICOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCILINDROS_HEMATICOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label460" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow82" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label461" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow85" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label462" runat="server" Text="PIOCITOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtPIOCITOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label463" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label464" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label465" runat="server" Text="FILAMENTO MUCOIDE" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlFILAMENTO_MUCOIDE_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label466" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow87" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label467" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow89" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label468" runat="server" Text="LEVADURAS" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlLEVADURAS" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label469" runat="server" Text="xCampo" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label470" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label471" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label472" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label473" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>                
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="EXAMEN QUÍMICO" ID="GroupPanel25" BoxFlex="1" Height="180" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form45" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow47" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label429" runat="server" Text="SANGRE" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlSANGRE_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList> 
                                                             <x:Label ID="label430" runat="server" Text= "" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label431" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label432" runat="server" Text="UROBILINÓGENO" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlUROBILINOGENO_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList> 
                                                             <x:Label ID="label433" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow49" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label434" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow51" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label435" runat="server" Text="BILIRRUBINA" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtBILIRRUBINA_EX_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                             <x:Label ID="label436" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label437" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label438" runat="server" Text="PROTEINAS" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlPROTEINAS_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label439" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow53" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label440" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow55" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label441" runat="server" Text="NITRITOS" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlNITRITOS_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList> 
                                                             <x:Label ID="label442" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label443" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label444" runat="server" Text="CETONAS" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlC_CETONICOS_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label445" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow57" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label446" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow62" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label447" runat="server" Text="GLUCOSA" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlGLUCOSA_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label448" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label449" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label450" runat="server" Text="PIGMENTOS BILIARES" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlPIGMENTOS_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label451" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow68" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label452" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow71" ColumnWidths="160px 145px 60px 180px 160px 145px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label453" runat="server" Text="ÁCIDO ASCÓRBICO" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlACIDO_ASCORBIC_EX_ORINA" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label454" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label455" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label456" runat="server" Text="LEUCOCITOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtLEUCOCITOS_EX_ORINA_QUIMICO"  Label="" ShowLabel="false" CssClass="mright"  runat="server"   Width="140"></x:TextBox>
                                                             <x:Label ID="label457" runat="server" Text=" " ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>                
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel ID="GroupPanelResultado" runat="server" Title="RESULTADO" BoxFlex="1" Height="50" Width="960">
                                        <Items>
                                            <x:Form ID="formResultado" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRowResultado"  ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" Hidden="false">
                                                        <Items>
                                                            <x:Label ID="labelPatologico" runat="server" Text="Patológico" ShowLabel="false"></x:Label>
                                                            <x:RadioButton runat="server" ID="rbPatologico" GroupName="patologico" ShowLabel="false">
                                                            </x:RadioButton>
                                                             <x:Label ID="labelNoPatologico" runat="server" Text="No Patológico" ShowLabel="false"></x:Label>
                                                            <x:RadioButton runat="server" ID="rbNoPatologico" GroupName="patologico" ShowLabel="false">
                                                            </x:RadioButton>
                                                        </Items>
                                                    </x:FormRow>
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel> 
                                </Items>
                                </x:Panel>
                                
                                <%--<x:Panel ID="PanelHemograma" Title="HEMOGRAMA AUTOMATIZADO" EnableBackgroundColor="true" Height="530px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title="HEMOGRAMA" ID="GroupPanel57" BoxFlex="1" Height="250" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form205" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow650" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1128" runat="server" Text="Hemoglobina" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMOGLOBINA_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1129" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label1132" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label1130" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMOGLOBINA_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1131" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                              
                                                            </Items>
                                                    </x:FormRow>
                                                
                                                    <x:FormRow ID="FormRow652" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1134" runat="server" Text="Hematocrito" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATOCRITO_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1135" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label1138" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label1136" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATOCRITO_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1137" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                              
                                                            </Items>
                                                    </x:FormRow>
                                             
                                                    <x:FormRow ID="FormRow644" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="labelHema" runat="server" Text="Hematíes" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATIES_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="labelxx" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label1114" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="labelxsx" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATIES_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="labelxxx" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             
                                                            </Items>
                                                    </x:FormRow>
                                                   
                                                    <x:FormRow ID="FormRow646" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1116" runat="server" Text="Volumen Corpuscular Medio" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVOLUMEN_CORPUSCULAR_MEDIO_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1117" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label1120" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label1118" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVOL_CORP_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1119" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                              
                                                            </Items>
                                                    </x:FormRow>
                                                 
                                                    <x:FormRow ID="FormRow553" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label810" runat="server" Text="Hb Corpuscular Medio" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHB_CORPUSCULAR_MEDIO_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label811" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label814" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label812" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHB_CORP_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label813" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                              
                                                            </Items>
                                                    </x:FormRow>
                                             
                                                    <x:FormRow ID="FormRow554" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label815" runat="server" Text="CC Hb Corpuscular" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCC_HB_CORPUSCULAR_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label816" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label819" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label817" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCC_HB_CORP_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label818" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                              
                                                            </Items>
                                                    </x:FormRow>
                                                
                                                    <x:FormRow ID="FormRow648" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1122" runat="server" Text="Plaquetas" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtPLAQUETAS_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1123" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label1126" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label1124" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtPLAQUETAS_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1125" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             
                                                            </Items>
                                                    </x:FormRow>
                                                  
                                                    <x:FormRow ID="FormRow555" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label820" runat="server" Text="Volumen Plaquetario Medio" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVOL_PLAQUETARIO_MEDIO_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label821" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label829" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label822" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVOL_PLAQUETARIO_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label823" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                              
                                                            </Items>
                                                    </x:FormRow>
                                                    
                                                </Rows>
                                            </x:Form>
                 
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="FÓRMULA LEUCOCITARIA TOTAL Y DIFERENCIAL" ID="GroupPanel58" BoxFlex="1" Height="215" Width="960" >                
                                    <Items>
                                    <x:Form ID="Form214" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow653" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1139" runat="server" Text="Leucocitos Totales" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLEUCOCITOS_TOTALES_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1140" runat="server" Text="um3" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1143" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label1141" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLEUCOCITOS_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1142" runat="server" Text="um3" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>                                        
                                            </Rows>
                                        </x:Form>
                              
                                    <x:Form ID="Form216" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow655" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1145" runat="server" Text="Linfocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLINFOCITOS_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1146" runat="server" Text="pg" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1149" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label1147" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLINFOCITOS_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1148" runat="server" Text="pg" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>                                     
                                            </Rows>
                                        </x:Form>
                                 
                                    <x:Form ID="Form218" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow657" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1151" runat="server" Text="MID (Bas, Eos, Mon)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtMID_BAS_EOS_MON_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1152" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1155" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label1153" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtMID_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1154" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>                                         
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form142" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow561" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label830" runat="server" Text="Neutrófilos Segmentados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtNEUTROFILOS_SEMENTADOS_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label831" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label844" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label832" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtNEUTROFILOS_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label833" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>                                       
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form144" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow562" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label845" runat="server" Text="Linfocitos (10 * 9)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLINFOCITOS_10_9_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label846" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label849" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label847" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLINFOCITOS_10_9_DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label848" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                      
                                                    </Items>
                                            </x:FormRow>
                                           
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form145" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow563" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label850" runat="server" Text="MID (Bas, Eos, Mon) (10*9)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtMID_BAS_EOS_MON_10_9_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label851" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label854" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label852" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtMID_B_E_M_10_9DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label853" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                      
                                                    </Items>
                                            </x:FormRow>                                        
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form146" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow564" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label855" runat="server" Text="Neutrófilos (10 * 9)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtNEUTROFILOS_10_9_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label856" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label859" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label857" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtNEUTROFILOS_10_9DESEABLE_CONST_CORP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label858" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                    </x:GroupPanel>
                                </Items>
                                </x:Panel>--%>

                                <x:Panel ID="ParasitoSeriado" Title="PARASITOLÓGICO SERIADO" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title="PARASITOLÓGICO SERIADO" ID="GroupPanel7" BoxFlex="1" Height="150" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow84" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label169" runat="server" Text="COLOR"  ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlCOLOR_Heces_Primera_Muestra" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            <x:Label ID="label170" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label171" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label172" runat="server" Text="CONSISTENCIA"  ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlCONSISTENCIA_Heces_Primera_Muestra" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            <x:Label ID="label173" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                             
                                                        </Items>
                                                    </x:FormRow>                                                
                                                    <x:FormRow ID="FormRow86" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label175" runat="server" Text="RESTOS ALIMENTICIOS"  ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlRESTOS_ALIMENTICIOS_Heces_Primera_Muestra" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            <x:Label ID="label176" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label177" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label181" runat="server" Text="MOCO"  ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlMOCO_Heces_Primera_Muestra" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            <x:Label ID="label182" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>                                                  
                                                    <x:FormRow ID="FormRow88" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label193" runat="server" Text="HEMATÍES"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtHEMATIES_Heces_Primera_Muestra" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label194" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label195" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label196" runat="server" Text="LEUCOCITOS"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtLEUCOCITOS_Heces_Primera_Muestra" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label197" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                              
                                                        </Items>
                                                    </x:FormRow>                                                 
                                                    <x:FormRow ID="FormRow90" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                              <x:Label ID="label109" runat="server" Text="LEVADURAS"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtLEVADURAS_SERIADO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label142" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label143" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label147" runat="server" Text="LEUCOCITOS"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtGRASAS_SERIADO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label148" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                           
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>
                 
                                        </Items>
                                    </x:GroupPanel>
                                    
                                </Items>
                                </x:Panel>

                                <x:Panel ID="ParasitoSimple" Title="PARASITOLÓGICO SIMPLE" EnableBackgroundColor="true" Height="190px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title=" " ID="GroupPanel6" BoxFlex="1" Height="175" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow70" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label133" runat="server" Text="COLOR"  ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlCOLOR_PAR_SIMPLE" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            <x:Label ID="label134" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label135" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label136" runat="server" Text="CONSISTENCIA"  ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlCONSISTENCIA_PAR_SIMPLE" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            <x:Label ID="label137" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                              
                                                    <x:FormRow ID="FormRow72" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label139" runat="server" Text="RESTOS ALIMENTICIOS"  ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlRESTOS_ALIMENTICIOS_PAR_SIMPLE" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            <x:Label ID="label140" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label141" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label145" runat="server" Text="MOCO"  ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlMOCO_PAR_SIMPLE" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            <x:Label ID="label146" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                                                                                  
                                                        </Items>
                                                    </x:FormRow>
                                               
                                                    <x:FormRow ID="FormRow74" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label157" runat="server" Text="HEMATIES"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtHEMATIES_PAR_SIMPLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label158" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label159" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label160" runat="server" Text="LEUCOCITOS"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtLEUCOCITOS_PAR_SIMPLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label161" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                            
                                                        </Items>
                                                    </x:FormRow>
                                                  
                                                    <x:FormRow ID="FormRow79" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label15" runat="server" Text="LEVADURAS"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtLEVADURAS_SIMPLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label26" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label29" runat="server" Text=" "  ShowLabel="false"></x:Label>
                                                            <x:Label ID="label36" runat="server" Text="GRASAS"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtGRASAS_SIMPLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label43" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                                                                                
                                                        </Items>
                                                    </x:FormRow>
                                               
                                                    <x:FormRow ID="FormRow81" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label25" runat="server" Text="METODO DIRECTO"  ShowLabel="false"></x:Label>
                                                            <x:TextArea ID="txtMTDO_DIRECTO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100" Height="40"></x:TextArea>
                                                            <x:Label ID="label84" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label90" runat="server" Text=" "  ShowLabel="false"></x:Label>
                                                            <x:Label ID="label96" runat="server" Text="METODO SEDIMENTACION"  ShowLabel="false"></x:Label>
                                                            <x:TextArea ID="txtMTDO_SEDIMENDATCION" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100" Height="40"></x:TextArea>
                                                            <x:Label ID="label102" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                                                                                
                                                        </Items>
                                                    </x:FormRow>
                                           </Rows> 
                                          </x:Form>        
                                        </Items>
                                    </x:GroupPanel>
                                </Items>
                                </x:Panel>

                                <x:Panel ID="PanelAglutinacionesenlamina" Title="AGLUTINACIONES EN LÁMINA" EnableBackgroundColor="true" Height="150px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form136" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow546" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                  <x:Label ID="label744" runat="server" Text="TIFICO 'O'" ShowLabel="false"></x:Label>
                                                  <x:TextBox  ID="txtTIFICO_O" runat="server"  ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                    <x:Label ID="label370" runat="server" Text=" " ShowLabel="false"></x:Label>    
                                                    <x:Label ID="label371" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label384" runat="server" Text=" " ShowLabel="false"></x:Label>                                                        
                                                </Items>
                                            </x:FormRow>                                
                                            <x:FormRow ID="FormRow547" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label749" Width="200px" runat="server" Text="TIFICO 'H'" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTIFICO_H" runat="server"  ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                    <x:Label ID="label372" runat="server" Text=" " ShowLabel="false"></x:Label>    
                                                    <x:Label ID="label373" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label383" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>                                       
                                            <x:FormRow ID="FormRow548" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label754" runat="server" Text="PARATIFICO 'A'" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtPARATIFICO_A" runat="server"  ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                    <x:Label ID="label374" runat="server" Text=" " ShowLabel="false"></x:Label>    
                                                    <x:Label ID="label375" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label382" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                   
                                            <x:FormRow ID="FormRow549" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label759" runat="server" Text="PARATIFICO 'B'" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtPARATIFICO_B" runat="server"  ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                    <x:Label ID="label376" runat="server" Text=" " ShowLabel="false"></x:Label>    
                                                    <x:Label ID="label377" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label381" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>

                                             <x:FormRow ID="FormRow550" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label764" runat="server" Text="BRUCELLA" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtBRUCELLA" runat="server"  ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>                                               
                                                    <x:Label ID="label378" runat="server" Text=" " ShowLabel="false"></x:Label>    
                                                    <x:Label ID="label379" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label380" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                 <x:Panel ID="PanelHemograma" Title="HEMOGRAMA COMPLETO" EnableBackgroundColor="true" Height="660px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title="HEMOGRAMA" ID="GroupPanel59" BoxFlex="1" Height="180" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form148" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow565" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label860" runat="server" Text="Hemoglobina" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMOGLOBINA_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label861" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label864" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label862" runat="server" Text="Hemoglobina Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMOGLOBINA_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label863" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow10" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label127" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow567" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label865" runat="server" Text="Hematocrito" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATOCRITO_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label866" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label869" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label867" runat="server" Text="Hematocrito Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATOCRITO_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label868" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow11" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label138" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow570" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label870" runat="server" Text="Hematíes" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATIES_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label871" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label874" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label872" runat="server" Text="Hematíes Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATIES_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label873" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow12" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label144" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow571" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label875" runat="server" Text="Leucocitos" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtLEUCOCITOS_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label876" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label879" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label877" runat="server" Text="Leucocitos Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtLEUCOCITOS_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label878" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow13" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label150" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow572" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label880" runat="server" Text="Recuento de Plaquetas" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtRECUENTO_DE_PLAQUETAS_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label881" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label884" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label882" runat="server" Text="Recuento de Plaquetas Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtPLAQUETAS_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label883" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>
                 
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="FÓRMULA LEUCOCITARIA" ID="GroupPanel60" BoxFlex="1" Height="300" Width="960" >                
                                    <Items>
                                    <x:Form ID="Form151" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow576" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label905" runat="server" Text="Abastonados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtABASTONADO_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label906" runat="server" Text="um3" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label909" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label907" runat="server" Text="Abastonados Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtABASTONADOS_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label908" runat="server" Text="um3" ShowLabel="false" CssClass="red"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow14" ColumnWidths="110px" runat="server" Hidden="false" >
                                                <Items>
                                                    <x:Label ID="label156" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                              
                                    <x:Form ID="Form152" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow577" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label910" runat="server" Text="Segmentados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtSEGMENTADOS_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label911" runat="server" Text="pg" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label914" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label912" runat="server" Text="Segmentados Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtSEGMENTADOS_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label913" runat="server" Text="pg" ShowLabel="false" CssClass="red"></x:Label>
                                                      
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow15" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label162" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                 
                                    <x:Form ID="Form153" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow578" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label915" runat="server" Text="Eosinòfilos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEOSINOFILOS_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label916" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label924" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label917" runat="server" Text="Eosinòfilos Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEOSINOFILOS_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label918" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                      
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow16" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label174" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form154" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow579" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label925" runat="server" Text="Basófilos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtBASOFILOS_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label926" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label929" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label927" runat="server" Text="Basófilos Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtBASOFILOS_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label928" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow17" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label180" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form155" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow581" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label930" runat="server" Text="Monocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtMONOCITOS_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label931" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label934" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label932" runat="server" Text="Monocitos Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtMONOCITOS_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label933" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow18" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label186" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form156" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow582" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label935" runat="server" Text="Linfocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLINFOCITOS_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label936" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label939" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label937" runat="server" Text="Linfocitos Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLINFOCITOS_DESEABLE_HEM_COM" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label938" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow21" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label192" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form41" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow29" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label385" runat="server" Text="Neutrófilos Segmentados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtNEUTROFILOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label386" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label387" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label388" runat="server" Text="Neutrófilos Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtNEUTROFILOS_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label389" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow31" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label390" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form42" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow32" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label391" runat="server" Text="Mielocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtMIELOCITOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label392" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label393" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label394" runat="server" Text="Mielocitos Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtMIELOCITOS_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label395" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow34" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label396" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form43" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow35" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label397" runat="server" Text="Juveniles" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtJUVENILES" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label398" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label399" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                     <x:Label ID="label400" runat="server" Text="Juveniles Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtJUVENILES_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label401" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                     
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow36" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label402" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="VOLUMEN CORPUSCULAR" ID="GroupPanel24" BoxFlex="1" Height="160" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form44" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow37" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label403" runat="server" Text="Vol. Corpuscular" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVOL_CORPUSCULAR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label404" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label405" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label406" runat="server" Text="Vol. Corpuscular Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVOL_CORPUSCULAR_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label407" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow38" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label408" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow41" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label409" runat="server" Text="Hb. Corpuscular Medio" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHB_CORPPUSCULAR_MEDIO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label410" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label411" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label412" runat="server" Text="Hb. Corpuscular Medio Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHB_CORPPUSCULAR_MEDIO_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label413" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow43" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label414" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow45" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label415" runat="server" Text="CC HB Corpuscular" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCC_CORPUSCULAR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label425" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label426" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label427" runat="server" Text="CC HB Corpuscular Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCC_CORPUSCULAR_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label428" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>                
                                        </Items>
                                    </x:GroupPanel>
                                </Items>
                                </x:Panel>
                                <x:Panel ID="PanelCadmio" Title="CADMIO (METALES)" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="METALES PESADOS CADMIO" ID="GrupoPanelCadmio" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="formCadmio" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRowCadmio" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="labelCadmio" runat="server" Text="CADMIO" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtCadmio" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label289" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                                <x:Label ID="label44" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label46" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="labelCadmioDeseable" runat="server" Text="CADMIO DESEABLE" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtCadmioDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label290" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelMagnesioOrina" Title="MAGNESIO EN ORINA" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="MAGNESIO" ID="GroupPanel43" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form67" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow160" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label222" runat="server" Text="MAGNESIO" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtMAGNESIO_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label224" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                                <x:Label ID="label225" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label226" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label489" runat="server" Text="MAGNESIO DESEABLE" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtMAGNESIO_ORINA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label490" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelPlomoSangre" Title="PLOMO EN SANGRE" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="PLOMO" ID="GroupPanel44" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form68" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow161" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label491" runat="server" Text="PLOMO" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtPLOMO_EN_SANGRE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label492" runat="server" Text="mg/dl" ShowLabel="false"></x:Label>
                                                                <x:Label ID="label495" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label496" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label497" runat="server" Text="PLOMO DESEABLE" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtPLOMO_EN_SANGRE_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label498" runat="server" Text="mg/dl" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelMagnesioMetales" Title="MAGNESIO (METALES)" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="MAGNESIO METALES PESADOS" ID="GroupPanel45" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form69" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow162" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label501" runat="server" Text="MAGNESIO" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtMAGNESIO_M_PESADOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label502" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                                <x:Label ID="label503" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label504" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label507" runat="server" Text="MAGNESIO DESEABLE" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtMAGNESIO_M_PESADOS_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label508" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelMPesadosCobre" Title="METALES PESADOS COBRE" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="COBRE" ID="GroupPanel46" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form70" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow163" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label509" runat="server" Text="COBRE" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtMETAL_PES_COBRE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label510" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                                <x:Label ID="label511" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label512" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label513" runat="server" Text="COBRE DESEABLE" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtMETAL_PES_COBRE_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label514" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>

                                <x:Panel ID="PanelReaccionesSerologicas" Title="REACCIONES SEROLÓGICAS (LUE-VDRL)" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="INMUNOLOGÍA" ID="GroupPanel13" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form22" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow1" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label3" runat="server" Text="LUES - VDRL" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlREACCIONES_SEROLOGICAS" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelFactorRematoideo" Title="FACTOR REMATOIDEO (FR)" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="INMUNOLOGÍA" ID="GroupPanel14" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form23" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow2" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label45" runat="server" Text="F. REMATOIDEO" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlFACTOR_REMATOIDEO" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelCultivoHeces" Title="CULTIVO DE HECES PARA SALMONELLA TYPHI" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="MICROBIOLOGÍA-COPROCULTIVO" ID="GroupPanel15" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form27" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow4" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label239" runat="server" Text="SALMONELLA TYPHI" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlSALMONELA_TYPHI" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelExamenSereadoHeces" Title="EXAMEN SEREADO EN HECES" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="MICROBIOLOGÍA-COPROCULTIVO" ID="GroupPanel19" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form36" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow6" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label283" runat="server" Text="EXAMEN SEREADO EN HECES" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtEXAM_SEREADO_HECES" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelDatoCieloAzul" Title="ADICIONALES - DATOS CIELO AZUL" EnableBackgroundColor="true" Height="110px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="I. DATOS GENERALES" ID="GroupPanel20" runat="server" BoxFlex="1" Height="90" Width="960" >
                                            <Items>
                                                <x:Form ID="form37" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow7" ColumnWidths="160px 145px 180px 265px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label293" runat="server" Text="IDENTIFICACIÓN" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtIDENTIFICACION" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                                <x:Label ID="label313" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:DatePicker ID="dpFECH_EMISION" Label="F. Emision" runat="server" Width="140px" DateFormatString="dd/MM/yyyy"/>
                                                                                       
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow8" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label316" runat="server" Text="MEDICO" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtMEDICO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelMicroCieloAzul" Title="MICROBIOLOGÍA 1 - CIELO AZUL" EnableBackgroundColor="true" Height="150px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="II. EXAMEN MACROSCÓPICO" ID="GroupPanel42" BoxFlex="1" Height="140" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form49" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow105" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label208" runat="server" Text="COLOR" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCOLOR_M1" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label210" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label211" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label212" runat="server" Text="CONSISTENCIA" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCONSISTENCIA_M1" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label213" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow106" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label214" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow107" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label216" runat="server" Text="MOCO" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtMOCO_M1" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label217" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label218" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label273" runat="server" Text="THEVENON" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtTHEVENON_M1" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label275" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow148" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label276" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow149" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label277" runat="server" Text="RESTOS ALIMENTICIOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtRESTOS_ALIMENTICIOS_M1" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label278" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label279" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label280" runat="server" Text="LEUCOCITOS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtLEUCOCITOS_M1" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label281" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>                
                                        </Items>
                                    </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                
                                <x:Panel ID="PanelParasitSeriadoCA" Title="PARASITOLÓGICO SERIADO - CIELO AZUL" EnableBackgroundColor="true" Height="170px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="III.EXAMEN MICROSCÓPICO" ID="GroupPanel8" BoxFlex="1" Height="160" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form50" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow108" ColumnWidths="160px 310px 10px 10px 10px 10px 10px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label219" runat="server" Text="METODO DIRECTO" ShowLabel="false"></x:Label>
                                                             <x:TextArea ID="txtMETODO_DIR_PARASIT" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="300" Height="60"></x:TextArea>
                                                            <x:Label ID="label223" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label282" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label284" runat="server" Text="" ShowLabel="false"></x:Label>
                                                            <x:Label ID="label285" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label315" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow109" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label286" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow150" ColumnWidths="160px 310px 10px 10px 10px 10px 10px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label287" runat="server" Text="METODO DE SEDIMENTACIÓN" ShowLabel="false"></x:Label>
                                                             <x:TextArea ID="txtMETODO_SEDIMEN_PARASIT" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="300" Height="60"></x:TextArea>
                                                             <x:Label ID="label288" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label292" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label294" runat="server" Text="" ShowLabel="false"></x:Label>
                                                            <x:Label ID="label298" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label349" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>                
                                        </Items>
                                    </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelCoprocultCieloAzul" Title="COPROCULTIVO - CIELO AZUL" EnableBackgroundColor="true" Height="110px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="I.COPROCULTIVO" ID="GroupPanel21" runat="server" BoxFlex="1" Height="85" Width="960" >
                                            <Items>
                                                <x:Form ID="form38" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow9" ColumnWidths="160px 425px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label314" runat="server" Width="140" BoxMargin="0" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>
                                                                <x:TextArea ID="txtDESCRIPCION" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="420" Height="60"></x:TextArea>
                                                                
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelDosajeAlcohol" Title="DOSAJE DE ALCOHOL" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="DOSAJE DE ALCOHOL" ID="GroupPanel22" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form39" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow25" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label357" runat="server" Text="DOSAJE DE ALCOHOL" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlDOSAJE_ALCOHOL" runat="server"   Width="140px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiCocaMari" Title="TOXICOLÓGICO COCAÍNA-MARIHUANA" EnableBackgroundColor="true" Height="475px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title="1. DECLARACIÓN" ID="GroupPanel10" BoxFlex="1" Height="100" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form18" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow114" ColumnWidths="160px 110px 250px 265px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label264" runat="server" Text="FOTOCHECK N°"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtFOTOCHECK" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label266" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:DatePicker ID="dtFECH_EMPEZO_LABORAR" Label="F. INGRESO AL TRABAJO" runat="server" Width="140px" DateFormatString="dd/MM/yyyy"/>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow119" ColumnWidths="160px 110px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label232" runat="server" Text="PROCEDENTE - RESIDENTE DE"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtPROCEDENTE_RESIDENTE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>                                                        
                                                        </Items>
                                                    </x:FormRow>
                                                    </Rows>
                                            </x:Form>       
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="2. ANTECEDENTES" ID="GroupPanel11" BoxFlex="1" Height="255" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form19" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow123" ColumnWidths="160px 110px 60px 200px 160px 190px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label233" runat="server" Text="1.- ¿Sufre de alguna enfermedad?" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlSUFRE_ENFERM" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label234" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label235" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label236" runat="server" Text="Diga cual" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtDIGA_CUAL_ENFER" runat="server"   Width="180px" ShowLabel="false"></x:TextBox> 
                                                             <x:Label ID="label237" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow124" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label238" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow125" ColumnWidths="160px 110px 60px 200px 160px 190px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label240" runat="server" Text="2.- ¿Consume algún medicamento?" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlCONSUME_MEDICAMENTO" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label241" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label242" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label243" runat="server" Text="Diga cual" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtDIGA_CUAL_MEDICAMENTO" runat="server"   Width="180px" ShowLabel="false"></x:TextBox>                                               
                                                             <x:Label ID="label244" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow133" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label246" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow134" ColumnWidths="160px 110px 60px 200px 160px 190px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label247" runat="server" Text="3.- ¿Chaccha coca?" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlCHACCHA_COCA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label248" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label249" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label250" runat="server" Text="Cuando por última vez" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtULTIMO_DIA" runat="server"   Width="180px" ShowLabel="false"></x:TextBox>                                               
                                                             <x:Label ID="label267" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow135" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label269" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow151" ColumnWidths="160px 110px 60px 200px 160px 190px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label271" runat="server" Text="4.- ¿Hizo su declaración jurada?" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlHIZO_DECLARA_JURADA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label272" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label312" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label352" runat="server" Text="¿Cuando se realizó?" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCUANDO_REALIZO" runat="server"   Width="180px" ShowLabel="false"></x:TextBox>                                               
                                                             <x:Label ID="label355454" runat="server"   Width="180px" ShowLabel="false"></x:Label>                                               
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow152" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label353" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow153" ColumnWidths="160px 110px 60px 200px 160px 190px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label354" runat="server" Text="5.- ¿Toma usted mate de coca?" ShowLabel="false"></x:Label>
                                                             <x:DropDownList ID="ddlTOMA_MATE_COCA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                             <x:Label ID="label355" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label356" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label358" runat="server" Text="¿Cuantas tazas al día?" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCUANTO_DIARIO" runat="server"   Width="180px" ShowLabel="false"></x:TextBox>                                                                                              
                                                             <x:Label ID="label484" runat="server"   Width="180px" ShowLabel="false"></x:Label>                                              
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow154" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label359" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow155" ColumnWidths="160px 110px 60px 200px 160px 190px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label360" runat="server" Text="" ShowLabel="false"></x:Label>
                                                             <x:Label ID="label361" runat="server" Text="" ShowLabel="false"></x:Label>
                                                             <x:Label ID="label362" runat="server" Text="" ShowLabel="false"></x:Label>
                                                             <x:Label ID="label363" runat="server" Text="" ShowLabel="false"></x:Label>
                                                             <x:Label ID="label485" runat="server" Text="¿Cuando tomó por última vez?" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="ddlCUANDO_ULTIMA_VEZ" runat="server"   Width="180px" ShowLabel="false"></x:TextBox>                                                     
                                                             <x:Label ID="label486" runat="server" Text="" ShowLabel="false"></x:Label>                                                                                                    
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>                
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="3. HORA" ID="GroupPanel12" BoxFlex="1" Height="50" Width="960" >                
                                    <Items>
                                    <x:Form ID="Form20" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow115" ColumnWidths="160px 110px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label274" runat="server" Text="HORA DE LECTURA"  ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtHORA_LECTURA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>                                               
                                                </Items>
                                            </x:FormRow>                                      
                                            </Rows>
                                        </x:Form>                            
                                    </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="4. TOXIGOLÓGICOS" ID="GroupPanel30" BoxFlex="1" Height="50" Width="960" >                
                                    <Items>
                                    <x:Form ID="Form54" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow136" ColumnWidths="160px 110px 250px 110px 110px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label251" runat="server" Text="COCAINA"  ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlCOCAINA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList> 
                                                    <x:Label ID="label252" runat="server" Text=""  ShowLabel="false"></x:Label>
                                                    <x:Label ID="label253" runat="server" Text="MARIHUANA"  ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlMARIHUANA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>                                               
                                                </Items>
                                            </x:FormRow>                                      
                                            </Rows>
                                        </x:Form>                            
                                    </Items>
                                    </x:GroupPanel>
                                </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiAnfetaminas" Title="TOXICOLÓGICO ANFETAMINAS" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="ANFETAMINAS" ID="GroupPanel31" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form55" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow137" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label254" runat="server" Text="ANFETAMINAS" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlANFETAMINAS" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiBarbituricos" Title="TOXICOLÓGICO BARBITURICOS" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="BARBITURICOS" ID="GroupPanel32" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form56" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow138" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label255" runat="server" Text="BARBITURICOS" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlBARBITURICOS" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiBenzodiacepinas" Title="TOXICOLÓGICO BENZODIACEPINAS" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="BENZODIACEPINAS" ID="GroupPanel33" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form57" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow139" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label256" runat="server" Text="BENZODIACEPINAS" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlBENZODIACEPINAS" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiMetanfetaminas" Title="TOXICOLÓGICO METANFETAMINAS" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="METANFETAMINAS" ID="GroupPanel34" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form59" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow140" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label257" runat="server" Text="METANFETAMINAS" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlMETANFETAMINAS" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiMorfina" Title="TOXICOLÓGICO MORFINA" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="MORFINA" ID="GroupPanel35" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form60" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow141" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label258" runat="server" Text="MORFINA" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlMORFINA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiOpiaceos" Title="TOXICOLÓGICO OPIACEOS" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="OPIACEOS" ID="GroupPanel36" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form61" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow142" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label259" runat="server" Text="OPIACEOS" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlOPIACEOS" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiMetadona" Title="TOXICOLÓGICO METADONA" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="METADONA" ID="GroupPanel37" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form62" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow143" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label260" runat="server" Text="METADONA" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlMETADONA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiFenciclidina" Title="TOXICOLÓGICO FENCICLIDINA" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="FENCICLIDINA" ID="GroupPanel38" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form63" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow144" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label261" runat="server" Text="FENCICLIDINA" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlFENCICLIDINA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiAlcoholSaliva" Title="TOXICOLÓGICO ALCOHOL EN LA SALIVA" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="ALCOHOL EN LA SALIVA" ID="GroupPanel39" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form64" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow145" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label262" runat="server" Text="ALCOHOL EN LA SALIVA" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlALCOHOL_SALIVA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelToxiExtasis" Title="TOXICOLÓGICO EXTASIS" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="EXTASIS" ID="GroupPanel40" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form65" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow146" ColumnWidths="160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label263" runat="server" Text="EXTASIS" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlEXTASIS" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelPlomo" Title="PLOMO" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="PLOMO EN ORINA" ID="GroupPanel41" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form66" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow147" ColumnWidths="160px 145px 250px 160px 145px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label265" runat="server" Text="PLOMO EN ORINA" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtPLOMO_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                                <x:Label ID="label268" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                <x:Label ID="label270" runat="server" Text="Valor deseable" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtPLOMO_ORINA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="140"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelCadmiOOrina" Title="CADMIO EN ORINA" EnableBackgroundColor="true" Height="77px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                    <Items>
                                        <x:GroupPanel Title="CADMIO" ID="GroupPanel23" runat="server" BoxFlex="1" Height="52" Width="960" >
                                            <Items>
                                                <x:Form ID="form40" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow27" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label364" runat="server" Text="CADMIO" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtCADMIO_ORINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label365" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                                <x:Label ID="label366" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label367" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                <x:Label ID="label368" runat="server" Text="CADMIO DESEABLE" ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtCADMIO_ORINA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label369" runat="server" Text="ug/dl" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PerfilHepatico" Title="PERFIL HEPÁTICO" EnableBackgroundColor="true" Height="300px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title=" " ID="GroupPanel4" BoxFlex="1" Height="280" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow42" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label49" runat="server" Text="PROTEÍNAS TOTALES"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="PROTEINAS_TOTALES_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label50" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label51" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label52" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="PROTEINAS_TOTALES_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label53" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                           
                                                        </Items>
                                                    </x:FormRow>                                           
                                                    <x:FormRow ID="FormRow44" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label55" runat="server" Text="ALBÚMINA"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="ALBUMINA_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label56" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label57" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label58" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="ALBUMINA_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label59" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                            
                                                        </Items>
                                                    </x:FormRow>                                               
                                                    <x:FormRow ID="FormRow46" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label61" runat="server" Text="GLOBULINA"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="GLOBULINA_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label62" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label63" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label64" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="GLOBULINA_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label65" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                             
                                                        </Items>
                                                    </x:FormRow>                                            
                                                    <x:FormRow ID="FormRow48" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label67" runat="server" Text="TGO"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="TGO_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label68" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label69" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            <x:Label ID="label70" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="TGO_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label71" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                             
                                                        </Items>
                                                    </x:FormRow>                                                  
                                                    <x:FormRow ID="FormRow50" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label73" runat="server" Text="TGP"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="TGP_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label74" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label75" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label76" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="TGP_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label77" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             
                                                            </Items>
                                                    </x:FormRow>
                                                
                                                    <x:FormRow ID="FormRow52" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label79" runat="server" Text="FOSFATASA ALCALINA"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="FOSFATASA_ALCALINA_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label80" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label81" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label82" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="FOSFATASA_ALCALINA_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label83" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>

                                                     <x:FormRow ID="FormRow54" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                       <x:Label ID="label85" runat="server" Text="GGTP"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="GGTP_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label86" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label87" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label88" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="GGTP_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label89" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                 
                                                    <x:FormRow ID="FormRow56" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label91" runat="server" Text="BILIRRUBINA TOTAL"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="BILIRRUBINA_TOTAL_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label92" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label93" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label94" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="BILIRRUBINA_TOTAL_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label95" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                   </x:FormRow>
                                                
                                                    <x:FormRow ID="FormRow58" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label97" runat="server" Text="BILIRRUBINA DIRECTA"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="BILIRRUBINA_DIRECTA_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label98" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label99" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label100" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="BILIRRUBINA_DIRECTA_DESEABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label101" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                
                                                    <x:FormRow ID="FormRow60" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label103" runat="server" Text="BILIRRUBINA INDIRECTA"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="BILIRRUBINA_INDIRECTA_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label104" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label105" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label106" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="BILIRRUBINA_INDIRECTA_DESABLE_PERF_HEP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label107" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>
                                                  
                                                  
                                        </Items>
                                    </x:GroupPanel>
                                </Items>
                                </x:Panel>

                                <x:Panel ID="PerfilLipidico" Title="PERFIL LIPÍDICO" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title=" " ID="GroupPanel5" BoxFlex="1" Height="150" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow61" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label54" runat="server" Text="COLESTEROL TOTAL"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="COLESTEROL_TOTAL_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label60" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label66" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label72" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="COLESTEROL_TOTAL_DESEABLE_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label108" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            </Items>
                                                    </x:FormRow>
                                                
                                                    <x:FormRow ID="FormRow63" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label110" runat="server" Text="COLESTEROL HDL"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="COLESTEROL_HDL_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label111" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label112" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label113" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="COLESTEROL_HDL_DESEABLE_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label114" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>                                                           
                                                            </Items>
                                                    </x:FormRow>
                                            
                                                    <x:FormRow ID="FormRow65" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label116" runat="server" Text="COLESTEROL LDL"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="COLESTEROL_LDL_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label117" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label118" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label119" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="COLESTEROL_LDL_DESEABLE_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label120" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             
                                                            </Items>
                                                    </x:FormRow>
                                                 
                                                    <x:FormRow ID="FormRow67" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label122" runat="server" Text="COLESTEROL VLDL"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="COLESTEROL_VLDL_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label123" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label124" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label125" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="COLESTEROL_VLDL_DESEABLE_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label126" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                           
                                                            </Items>
                                                    </x:FormRow>
                                             
                                                    <x:FormRow ID="FormRow69" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label128" runat="server" Text="TRIGLICÉRIDOS"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="TRIGLICERIDOS_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label129" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label130" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label131" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="TRIGLICERIDOS_DESEABLE_PERF_LIP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label132" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                           
                                                            </Items>
                              
                                                    </x:FormRow>
                                           </Rows>
                                          </x:Form>        
                                        </Items>
                                    </x:GroupPanel>
                                </Items>
                                </x:Panel>
                                 <x:Panel ID="PanelHemoglobinaHematocrito" Title="HEMOGLOBINA Y HEMATOCRITO" EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form269" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FrmHemoglobina" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1381" runat="server" Text="Hemoglobina " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHEMOGLOBINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1382" runat="server" Text="gr/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1385" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label1383" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHEMOGLOBINA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1384" runat="server" Text="gr/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>                                    
                                             <x:FormRow ID="FrmHematocrito" ColumnWidths="159px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1386" runat="server" Text="Hematocrito " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHEMATOCRITO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1387" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1390" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label1388" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHEMATOCRITO_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1389" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelHavigmhepatitisa" Title="HAV (IGM)" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form258" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow697" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1295" runat="server" Text="HAV (IgM)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHAV_IGM_HEPATITIS_A" runat="server"  ShowLabel="false" CssClass="mright"  Width="100"></x:TextBox>
                                                     <x:Label ID="label1296" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1299" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label1297" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHAV_IGM_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1298" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelExamendeelisahiv" Title="HIV I-II ANTICUERPO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form259" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow698" ColumnWidths="160px 110px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1300" runat="server" Text="HIV I-II ANTICUERPO" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlHIV_ANTICUERPO" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelHBSAG_A" Title="HBSAG (HEPATITIS A ANTIGENO DE SUPERFICIE)" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form14" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow101" ColumnWidths="160px 110px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label202" runat="server" Text="HEPATITIS A" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlHEPATITIS_ANTIGENO_A" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelHBSAG_B" Title="HBSAG (HEPATITIS B ANTIGENO DE SUPERFICIE)" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form15" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow102" ColumnWidths="160px 110px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label204" runat="server" Text="HEPATITIS B" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlHEPATITIS_ANTIGENO_B" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelPruebaEmbarazo" Title="PRUEBA DE EMBARAZO (B-HCG)" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form16" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow103" ColumnWidths="160px 110px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label205" runat="server" Text="B-HCG" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlPRUEBA_EMBARAZO" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelProteinaReactiva" Title="PROTEINA C REACTIVA (PCR)" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form17" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow104" ColumnWidths="160px 110px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label206" runat="server" Text="PCR" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlPROTEINA_REACTIVA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelREACC_INFLA_HECES" Title="REACCION INFLAMATORIA EN HECES" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form48" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow39" ColumnWidths="160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label121" runat="server" Text="REACC INFL HECES" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtREACC_INFLA_HECES" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label207" runat="server" Text="s/u" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>

                              <x:Panel ID="PanelAcidourico" Title="ÁCIDO ÚRICO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form135" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow545" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label739" runat="server" Text="Acido Urico" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtACIDO_URICO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label740" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label743" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label741" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtACIDO_URICO_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label742" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelAntigenoprostatico" Title="ANTÍGENO PROSTÁTICO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form137" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow556" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label774" runat="server" Text="Antígeno Prostático" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtANTIGENO_PROSTATICO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label775" runat="server" Text="ng/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label778" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label776" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtPAS_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label777" runat="server" Text="ng/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelColesteroltotal" Title="COLESTEROL TOTAL" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form138" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow557" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label779" runat="server" Text="Colesterol Total " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtCOLESTEROL_TOTAL" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label780" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label783" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label781" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtCOLESTEROL_TOTAL_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label782" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelTrigliceridos" Title="TRIGLCÉRIDOS" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form147" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow566" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label824" runat="server" Text="Triglicéridos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTRIGLICERIDOS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label825" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label828" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label826" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTRIGLICERIDOS_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label827" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelGlucosa" Title="GLUCOSA EN AYUNAS" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form140" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow559" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label789" runat="server" Text="Glucosa" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtGLUCOSA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label790" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label793" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label791" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtGLUCOSA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label792" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                                <x:FormRow ID="FormRow40" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label78" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelCreatinina" Title="CREATININA EN SUERO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form139" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow558" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label784" runat="server" Text="Creatinina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtCREATININA_EN_SUERO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label785" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label788" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label786" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtCREATININA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label787" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelUrea" Title="ÙREA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form141" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow560" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label794" runat="server" Text="Urea" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtUREA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label795" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label798" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label796" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtUREA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label797" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelTgo" Title="TGO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form149" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow568" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label834" runat="server" Text="TGO" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTGO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label835" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label838" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label836" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTGO_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label837" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelTgp" Title="TGP" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form150" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow569" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label839" runat="server" Text="TGP" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTGP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label840" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label843" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label841" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTGP_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label842" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                              <x:Panel ID="PanelSubunidadbetacualitativo" Title="BETA HCG" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form283" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow724" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1392" runat="server" Text="BETA HCG" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtSUB_UNIDAD_BETA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1393" runat="server" Text="mUI/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1396" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label1394" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtVALOR_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1395" runat="server" Text="mUI/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelGlucosaGeneral" Title="GLUCOSA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form6" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow83" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label149" runat="server" Text="Glucosa" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtGLUCOSA_GENERAL" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label151" runat="server" Text="mg/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label152" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label153" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtGLUCOSA_GENERAL_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label154" runat="server" Text="mUI/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelProtombina" Title="PROTOMBINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form7" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow92" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label155" runat="server" Text="T. Protombina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTIPO_PROTOMBINA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label163" runat="server" Text="seg" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label164" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label165" runat="server" Text="Valor deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTIPO_PROTOMBINA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label166" runat="server" Text="seg" ShowLabel="false" CssClass="red"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelINR" Title="INR" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form10" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow93" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label167" runat="server" Text="INR" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtINR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label168" runat="server" Text="seg" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label178" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label179" runat="server" Text="INR Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtINR_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label183" runat="server" Text="seg" ShowLabel="false" CssClass="red"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelLabExcepciones" Title="LABORATORIO EXCEPCIONES (EXONERACIÓN)" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow95" ColumnWidths="60px 60px 40px 110px 60px 250px 40px 110px 60px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label295" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label296" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label184" runat="server" Text="SI" ShowLabel="false"></x:Label>
                                                     <x:RadioButton ID="rbEXONERACION_SI" GroupName="exoneracion" runat="server" Text="" ShowLabel="false"></x:RadioButton>
                                                     <x:Label ID="label185" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label187" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label188" runat="server" Text="NO" ShowLabel="false"></x:Label>
                                                     <x:RadioButton ID="rbEXONERACION_NO" GroupName="exoneracion" runat="server" Text="" ShowLabel="false"></x:RadioButton>
                                                     <x:Label ID="label189" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelVSG" Title="VSG" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form12" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow97" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label190" runat="server" Text="Vel. de Sedimentación" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtVEL_SEDIMENTACION" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label191" runat="server" Text="seg" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label198" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label199" runat="server" Text="Valor deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtVEL_SEDIMENTACION_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label200" runat="server" Text="seg" ShowLabel="false" CssClass="red"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelRPR" Title="R. P. R." EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form13" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow99" ColumnWidths="160px 110px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label201" runat="server" Text="R. P. R." ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlRPR" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>                                                     
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="PanelInmunologia" Title="INMUNOLOGÍA" EnableBackgroundColor="true" Height="250px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title="I. EXAMENES" ID="GroupPanel9" BoxFlex="1" Height="240" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form51" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow110" ColumnWidths="160px 110px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label220" runat="server" Text="ANTÍGENO FLAGELAR PARATYFICO A" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtANTIGENO_TYFICO_A" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>                                                       
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow111" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label487" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow112" ColumnWidths="160px 110px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label488" runat="server" Text="ANTÍGENO FLAGELAR PARATYFICO B" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtANTIGENO_TYFICO_B" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow113" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label493" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow116" ColumnWidths="160px 110px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label494" runat="server" Text="ANTÍGENO SOMATICO TYFICO O" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtANTIGENO_TYFICO_O" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>  
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow156" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label499" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow157" ColumnWidths="160px 110px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label500" runat="server" Text="ANTÍGENO FLAGELAR TYFICO H" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtANTIGENO_TYFICO_H" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>                                         
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow158" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label505" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow159" ColumnWidths="160px 110px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label506" runat="server" Text="BRUCELLA ABORTUS" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtBRUCELLA_ABORTUS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>                                         
                                                        </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>                
                                        </Items>
                                    </x:GroupPanel>
                               </Items>
                                </x:Panel>
                                <x:Panel ID="PanelRaspadoUñas" Title="RASPADO DE UÑAS - CIELO AZUL" EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="I. RASPADO DE PIEL Y UÑAS PARA HONGOS" ID="GroupPanel28" BoxFlex="1" Height="50" Width="960" >                
                                            <Items>
                                                 <x:Form ID="Form52" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                    <Rows>
                                                        <x:FormRow ID="FormRow117" ColumnWidths="160px 310px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label228" runat="server" Text="EXAMEN DIRECTO (KOH)"  ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtRASPADO_UÑAS" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="300"></x:TextBox>                                                       
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>          
                                            </Items>
                                        </x:GroupPanel>
                                   </Items>
                                </x:Panel>
                                <x:Panel ID="PanelSecresionFaringea" Title="SECRESIÓN FARINGEA - CIELO AZUL" EnableBackgroundColor="true" Height="100px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="I. EXAMENES" ID="GroupPanel29" BoxFlex="1" Height="80" Width="960" >                
                                            <Items>
                                                 <x:Form ID="Form53" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                    <Rows>
                                                        <x:FormRow ID="FormRow118" ColumnWidths="160px 150px 200px 160px 160px" runat="server" >
                                                            <Items>
                                                                <x:Label ID="label229" runat="server" Text="MUESTRA"  ShowLabel="false"></x:Label>
                                                                <x:TextBox ID="txtMUESTRA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                                <x:Label ID="label230" runat="server" Text=""  ShowLabel="false"></x:Label>
                                                                <x:Label ID="label231" runat="server" Text="GERMEN AISLADO"  ShowLabel="false"></x:Label>
                                                                <x:TextArea ID="txtGERMEN_AISLADO" ShowLabel="false" CssClass="mright" runat="server" Width="150" Height="60"></x:TextArea>                                                       
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>          
                                            </Items>
                                        </x:GroupPanel>
                                   </Items>
                                </x:Panel>
                            <x:Panel ID="PanelBkDirecto" Title="BK DIRECTO" EnableBackgroundColor="true" Height="100px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>

                                    <x:GroupPanel runat="server" Title="RESULTADOS" ID="GroupPanel17" BoxFlex="1" Height="50" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form25" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow121" ColumnWidths="160px 260px 50px 150px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label301" runat="server" Text="RESULTADOS"  ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="RESULTADOS_BK_DIRECTO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="250"></x:TextBox>
                                                        <x:Label ID="label302" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label303" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                        <x:Label ID="label304" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label306" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                        <x:Label ID="label305" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                           
                                                            </Items>
                                                    </x:FormRow>
                                                    </Rows>
                                            </x:Form>
               
                                        </Items>
                                    </x:GroupPanel>
                               </Items>
                                </x:Panel>

                              <x:Panel ID="PanelHepatitisC" Title="INMUNO ENZIMA (HEPETITIS C)" EnableBackgroundColor="true" Height="40px" runat="server"

                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">

                                <Items>

                                    <x:Form ID="Form26" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >

                                        <Rows>

                                           <x:FormRow ID="FormRow122" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >

                                                <Items>

                                                <x:Label ID="label307" runat="server" Text="INMUNO ENZIMA (HEPETITIS C)"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="txtINMUNO_ENZIMA_HEPATITIS_C" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label308" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                <x:Label ID="label309" runat="server" Text=" " ShowLabel="false"></x:Label>

                                                <x:Label ID="label310" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="txtINMUNO_ENZIMA_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label311" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                    </Items>

                                            </x:FormRow>

                                            </Rows>

                                        </x:Form>

                                    </Items>

                                 </x:Panel>

                                <x:Panel ID="PanelBenzeno" Title="BENZENO (EXPOSICIÒN A GAS NATURAL)" EnableBackgroundColor="true" Height="40px" runat="server"

                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">

                                <Items>

                                    <x:Form ID="Form28" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >

                                        <Rows>

                                           <x:FormRow ID="FormRow126" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >

                                                <Items>

                                                <x:Label ID="label317" runat="server" Text="BENZENO (EXPOSICIÒN A GAS NATURAL)"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="RESULTADOS_BENZENO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label318" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                <x:Label ID="label319" runat="server" Text=" " ShowLabel="false"></x:Label>

                                                <x:Label ID="label320" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="BENZENO_DESEABLE" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label321" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>

                                            </x:FormRow>

                                            </Rows>

                                        </x:Form>

                                    </Items>

                                 </x:Panel>

                                <x:Panel ID="PanelMicroalbuminuria" Title="MICROALBUMINURIA" EnableBackgroundColor="true" Height="40px" runat="server"

                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">

                                <Items>

                                    <x:Form ID="Form30" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >

                                        <Rows>

                                           <x:FormRow ID="FormRow127" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >

                                                <Items>

                                                <x:Label ID="label322" runat="server" Text="MICROALBUMINURIA"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="MICROALBUMINURIA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label323" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                <x:Label ID="label324" runat="server" Text=" " ShowLabel="false"></x:Label>

                                                <x:Label ID="label325" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="DESEABLE_MICROALBUMINURIA" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label326" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                    </Items>

                                            </x:FormRow>

                                            </Rows>

                                        </x:Form>

                                    </Items>

                                 </x:Panel>

                                <x:Panel ID="PanelHCV" Title="HCV" EnableBackgroundColor="true" Height="40px" runat="server"

                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">

                                <Items>

                                    <x:Form ID="Form31" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >

                                        <Rows>

                                           <x:FormRow ID="FormRow128" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >

                                                <Items>

                                                <x:Label ID="label327" runat="server" Text="HCV"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="RESULTADO_HCV" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label328" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                <x:Label ID="label329" runat="server" Text=" " ShowLabel="false"></x:Label>

                                                <x:Label ID="label330" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="DESEABLE_HCV" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label331" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                    </Items>

                                            </x:FormRow>

                                            </Rows>

                                        </x:Form>

                                    </Items>

                                 </x:Panel>

                                <x:Panel ID="PanelHBsAg" Title="HBsAg" EnableBackgroundColor="true" Height="40px" runat="server"

                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">

                                <Items>

                                    <x:Form ID="Form32" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >

                                        <Rows>

                                           <x:FormRow ID="FormRow129" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >

                                                <Items>

                                                <x:Label ID="label332" runat="server" Text="HBsAg"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="RESULTADO_HBsAg" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label333" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                <x:Label ID="label334" runat="server" Text=" " ShowLabel="false"></x:Label>

                                                <x:Label ID="label335" runat="server" Text="Valor Deseable"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="DESEABLE_HBsAg" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label336" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>

                                            </x:FormRow>

                                            </Rows>

                                        </x:Form>

                                    </Items>

                                 </x:Panel>

                                <x:Panel ID="PanelKOH" Title="EXAMEN KOH" EnableBackgroundColor="true" Height="40px" runat="server"

                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">

                                <Items>

                                    <x:Form ID="Form33" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >

                                        <Rows>

                                           <x:FormRow ID="FormRow130" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >

                                                <Items>

                                                <x:Label ID="label337" runat="server" Text="MUESTRA"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="MUESTRA_KOH" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label338" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                <x:Label ID="label339" runat="server" Text=" " ShowLabel="false"></x:Label>

                                                <x:Label ID="label340" runat="server" Text="RESULTADOS"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="RESULTADO_KOH" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label341" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                    </Items>

                                            </x:FormRow>

                                            </Rows>

                                        </x:Form>

                                    </Items>

                                 </x:Panel>

                                <x:Panel ID="PanelInsulinaBasal" Title="INSULINA BASAL" EnableBackgroundColor="true" Height="40px" runat="server"

                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">

                                <Items>

                                    <x:Form ID="Form34" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >

                                        <Rows>

                                           <x:FormRow ID="FormRow131" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >

                                                <Items>

                                                <x:Label ID="label342" runat="server" Text="INSULINA BASAL"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="INSULINA_BASAL" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label343" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                <x:Label ID="label344" runat="server" Text=" " ShowLabel="false"></x:Label>

                                                <x:Label ID="label345" runat="server" Text="Valor DeseableL"  ShowLabel="false"></x:Label>

                                                <x:TextBox ID="DESEABLE_INSULINA_BASAL" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>

                                                <x:Label ID="label346" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                    </Items>

                                            </x:FormRow>

                                            </Rows>

                                        </x:Form>

                                    </Items>

                                 </x:Panel>

                                <x:Panel ID="Panel15" Title="AMPLIAR CONCLUSIONES DE LABORATORIO" EnableBackgroundColor="true" Height="450px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="false">
                                <Items>
                                    <x:GroupPanel runat="server" Title="OBSERVACIONES" ID="GroupPanel18" BoxFlex="1" Height="250" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form35" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow132" ColumnWidths="160px 740px 10px" runat="server" >
                                                        <Items>
                                                        <x:Label ID="label347" runat="server" Text="OBSERVACIONES DE LABORATORIO"  ShowLabel="false"></x:Label>

                                                        <x:TextBox ID="OBSERVACIONES_LABORATORIO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="730" ></x:TextBox>

                                                        <x:Label ID="label348" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>

                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow23" ColumnWidths="160px 740px 10px" runat="server" >
                                                        <Items>
                                                         <x:Label ID="label350" runat="server" Text="LABORATORIO NORMAL"  ShowLabel="false"></x:Label>

                                                        <x:CheckBox ID="LABORATORIO_NORMAL" runat="server" Text="" ShowLabel="false"></x:CheckBox>

                                                        <x:Label ID="label351" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>


                                                            </Items>
                                                    </x:FormRow>
                                                    </Rows>
                                            </x:Form>
               
                                        </Items>
                                    </x:GroupPanel>
                               </Items>
                                </x:Panel>

                                <x:Panel ID="PanelTiempodecoagulacion" Title="TIEMPO DE COAGULACIÓN" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form251" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow690" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1260" runat="server" Text="Tiempo de Coagulación" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTIempoCoagulacion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1261" runat="server" Text=" minutos" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1264" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label1262" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTIempoCoagulacionDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1263" runat="server" Text=" minutos" ShowLabel="false" CssClass="red"></x:Label>
                                                      
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>

                              <x:Panel ID="PanelTiempodesangria" Title="TIEMPO DE SANGRÍA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form252" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow691" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1265" runat="server" Text="Tiempo de Sangría" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiempoSangria" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1266" runat="server" Text="minutos" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1269" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label1267" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiempoSangriaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1268" runat="server" Text="minutos" ShowLabel="false" CssClass="red"></x:Label>
                                                      
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>

                             <x:Panel ID="PanelHisopadoFaringeo" Title="HISOPADO FARÍNGEO" EnableBackgroundColor="true" Height="195px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title="MACROSCÓPICO" ID="GroupPanel64" BoxFlex="1" Height="50" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form158" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow573" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label886" runat="server" Text="Color" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCOLOR_HISOP_FAR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label887" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label890" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label888" runat="server" Text="Aspecto" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtASPECTO_HISOP_FAR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label889" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             
                                                            </Items>
                                                    </x:FormRow>
                                                </Rows>
                                               </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title=" MICROSCÓPICO" ID="GroupPanel65" BoxFlex="1" Height="80" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form159" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow574" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label891" runat="server" Text="Leucocitos" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtLEUCOCITOS_HISOP_FAR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label892" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label900" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                             <x:Label ID="label893" runat="server" Text="Hematíes" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHEMATIES_HISOP_FAR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label899" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                              
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow33" ColumnWidths="110px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label115" runat="server" Text="" ShowLabel="false" Hidden="false"></x:Label>
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow575" ColumnWidths="160px 110px 60px 250px 160px 110px 60px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label901" runat="server" Text="Células Epiteliales" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCELULAS_EPITELIALES_HISOP_FAR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label902" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label945" runat="server" Text=" " ShowLabel="false"></x:Label> 
                                                             <x:Label ID="label903" runat="server" Text="Bacterias GRAM" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtBACTERIAS_GRAM_HISOP_FAR" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label904" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                             
                                                            </Items>
                                                    </x:FormRow>
                                                </Rows>
                                               </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                 </x:Panel>

                              <x:Panel ID="PanelHisopadoNaso" Title="HISOPADO NASO-FARÍNGEO" EnableBackgroundColor="true" Height="120px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="true">
                                <Items>
                                    <x:GroupPanel runat="server" Title="MICROBIOLOGÍA-BACILOSCOPIA" ID="GroupPanel66" BoxFlex="1" Height="90" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form160" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow584" ColumnWidths="160px 310px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label946" runat="server" Text="Hisopado Nasofaringeo" ShowLabel="false"></x:Label>
                                                             <x:TextArea ID="txtHISOPADO_NASO" ShowLabel="false" CssClass="mright" runat="server" Width="300" Height="60"></x:TextArea>
                                                            </Items>
                                                    </x:FormRow>
                                                </Rows>
                                               </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>

                                 

                                
                                 <x:Panel ID="PanelInformeLaboratorio" Title="INFORME DE LABORATORIO" EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form21" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow22" ColumnWidths="160px 410px 60px 10px 160px 110px 5px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label203" runat="server" Text="Observaciones" ShowLabel="false"></x:Label>
                                                     <x:TextArea ID="txtOBSERVACIONES_LABORATORIO" ShowLabel="false" CssClass="mright" runat="server" Width="400" Height="60"></x:TextArea>
                                                     <x:Label ID="label209" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label215" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                     <x:Label ID="label221" runat="server" Text="Laboratorio Normal" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkLABORATORIO_NORMAL" runat="server"  ShowLabel="false"></x:CheckBox>
                                                     <x:Label ID="label227" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                                <x:Panel ID="Panel84" Title="AUDITORÍA" EnableBackgroundColor="true" Height="80px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form213" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow654" ColumnWidths="320px 300px 300px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtLabAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtLabAuditorInsertar" runat="server" Text="" Width="180" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtLabAuditorEditar" runat="server" Text="" Width="180" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow656" ColumnWidths="320px 300px 300px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtLabEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtLabEvaluadorInsertar" runat="server" Text="" Width="180" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtLabEvaluadorEvaluar" runat="server" Text="" Width="180" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                                 <Ext:FormRow ID="FormRow24" ColumnWidths="320px 300px 300px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtLabInformador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtLabInformadorInsertar" runat="server" Text="" Width="180" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtLabInformadorActualiza" runat="server" Text="" Width="180" Label="Modificación" Readonly="True"></x:TextBox>
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
      <x:Window ID="WindowAddDXFrecuente" Title="Diagnóstico Frecuente" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddDXFrecuente_Close" IsModal="True" Width="650px" Height="640px">
        </x:Window>
        <x:Window ID="winEdit1" Title="Reporte" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png" 
       CloseAction="HidePostBack" EnableConfirmOnClose="true"  IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
       Target="Top"   IsModal="true"  Height="630px" Width="870px">
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
