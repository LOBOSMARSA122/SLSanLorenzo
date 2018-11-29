<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAudiometria.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmAudiometria" %>
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
        .x-form-checkbox {
          height:22px;
          width:22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" runat="server" AjaxAspnetControls="aspButtonOD,aspButtonOI,Chart1,Chart2,aspButtonOD_Inter,aspButtonOI_Inter,Chart1_Inter,Chart2_Inter"/>
         <x:Panel ID="Panel2" runat="server" Height="5000px" Width="1000px" ShowBorder="True"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Audiometría">
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
                                            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" >
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
                        <x:Tab ID="TabAudiometria" BodyPadding="5px" Title="Audiometría" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnAudiometria" Text="Grabar Audiometría" Icon="SystemSave" runat="server" OnClick="btnAudiometria_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:DropDownList ID="ddlUsuarioGrabarAudio" runat="server"></x:DropDownList>                                   
                                        <x:Button ID="btnReporteAudioCI" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items> 
                                <x:Panel ID="Panel9" Title="CUESTIONARIO DE AUDIOMETRÍA" EnableBackgroundColor="true" Height="180px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="False" Layout="Column">
                                    <Items>    
                                        <x:Panel ID="Panel10" Width="630px" Height="180px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="CUESTIONARIO DE AUDIOMETRÍA">
                                            <Items>
                                                <x:TextBox ID="tLabel28" runat="server" Text="1. SI HIZO CAMBIOS DE ALTITUD MENORES A 3500 MSNM ¿HA REPOSADO MENOS DE 48 HRS?" Width="620" Enabled="False"></x:TextBox>
                                                <x:Label ID="tLabel38" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox1" runat="server" Text="2.- ¿VIAJES FRECUENTES A ALTURA? " Width="620" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label1" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox2" runat="server" Text="3.- ¿CUANTAS HORAS HA DESCANSADO ANTES DEL EXAMEN?" Width="620" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label2" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox3" runat="server" Text="4. TIEMPO DE TRABAJO" Width="620" Enabled="False"></x:TextBox>
                                                <%--<x:Label ID="Label3" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox4" runat="server" Text="¿CONSUMIÓ ALCOHOL EL DÍA PREVIO?" Width="620" Enabled="False"></x:TextBox>--%>
                                              
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel1" Width="110px" Height="200px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="SI">
                                            <Items>
                                                <x:CheckBox ID="ddlsi_hizo_cambios" runat="server" Width="100"></x:CheckBox>                                               
                                                <x:Label ID="Label41" runat="server" Text=" "></x:Label>
                                                <x:CheckBox ID="ddlestuvo_expuesto" runat="server" Width="100"></x:CheckBox>                                               
                                                <x:Label ID="Label5" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="ddldurmio_mal_la_noche" runat="server" Text="" Width="100" Label="" Enabled="False"></x:TextBox> 
                                                <%--<x:DropDownList ID="ddlpresenta_algun" runat="server" Width="100"></x:DropDownList>--%>                                               
                                                <x:Label ID="Label6" runat="server" Text="." Enabled="False"></x:Label>
                                                <%--<x:DropDownList ID="ddldurmio_mal_la_noche" runat="server" Width="100"></x:DropDownList>--%>                                               
                                                <%--<x:Label ID="Label7" runat="server" Text=" "></x:Label>--%>
                                                <x:TextBox ID="txttiempo_de_trabajo" runat="server" Text="" Width="100" Label="" Enabled="False"></x:TextBox>       
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel runat="server" Title="DATOS DEL AUDIÓMETRO" ID="Panel012" BoxFlex="1" Height="30" TableColspan="2">   
                                            <Items>
                                                <x:Form ID="Form26" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow95" ColumnWidths="315px 315px 315px" runat="server">
                                                            <Items>                                                            
                                                                <x:TextBox ID="txtCalibracion" runat="server" Text="" Width="180" Label="Calibración" Enabled="False" ></x:TextBox> 
                                                                 <x:TextBox ID="txtMarca" runat="server" Text="" Width="180" Label="Marca" Enabled="False"></x:TextBox>     
                                                                <x:TextBox ID="txtModelo" runat="server" Text="" Width="180" Label="Modelo" Enabled="False"></x:TextBox>           
                                                            </Items> 
                                                        </x:FormRow>                                                        
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:Panel>
                                <x:Panel runat="server" Title="APRECIACIÓN DEL RUIDO" ID="Panel17" BoxFlex="1" Height="30" TableColspan="2">   
                                            <Items>
                                                <x:Form ID="Form12" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow25" ColumnWidths="315px 315px 315px" runat="server">
                                                            <Items>                                                            
                                                                <x:DropDownList ID="chkRuidoExtra" runat="server" Text="" Width="180" Label="Protectores" Enabled="False"></x:DropDownList> 
                                                                 <x:DropDownList ID="ddlSarampion" runat="server" Text="" Width="180" Label="Apreciación" Enabled="False"></x:DropDownList>     
                                                                <x:TextBox ID="ddlconsumio_alcohol" runat="server" Text="" Width="180" Label="Tiempo exp.pond.8h" Enabled="False"></x:TextBox>           
                                                            </Items> 
                                                        </x:FormRow>                                                        
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:Panel>
                                 <x:Panel ID="Panel13" Title="SÍNTOMAS ACTUALES" EnableBackgroundColor="true" Height="70px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="30px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow1" ColumnWidths="240px 240px 240px 240px" runat="server">
                                                    <Items>  
                                                        <x:CheckBox ID="chksordera" runat="server" Text="SORDERA-DISM. AUD." ShowLabel="true"></x:CheckBox>
                                                        <x:CheckBox ID="chkacuferos" runat="server" Text="ZUMBIDO" ShowLabel="true"></x:CheckBox>
                                                        <x:CheckBox ID="chkvertigos" runat="server" Text="VÉRTIGOS - MAREOS" ShowLabel="true"></x:CheckBox>
                                                        <x:CheckBox ID="chkotalgia" runat="server" Text="OTALGIA - DOLOR OÍDO" ShowLabel="true"></x:CheckBox>                                                          
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow26" ColumnWidths="240px 240px 240px 240px" runat="server">
                                                    <Items>  
                                                        <x:Label ID="Label3" runat="server" Text=""></x:Label>
                                                        <x:Label ID="Label7" runat="server" Text=""></x:Label>
                                                        <x:Label ID="Label71" runat="server" Text=""></x:Label>
                                                        <x:Label ID="Label72" runat="server" Text=""></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                  <x:FormRow ID="FormRow2" ColumnWidths="240px 240px 240px 240px" runat="server">
                                                    <Items>  
                                                        <x:CheckBox ID="chksecrecion_otica" runat="server" Text="SECRECIÓN ÓTICA" ShowLabel="true"></x:CheckBox>
                                                        <x:CheckBox ID="ddlpresenta_algun" runat="server" Text="OTROS SINT." ShowLabel="true"></x:CheckBox>
                                                        <x:Label ID="Label73" runat="server" Text=""></x:Label>
                                                        <x:Label ID="Label74" runat="server" Text=""></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                
                                            </Rows>
                                        </x:Form>    
                                    </Items>       
                                </x:Panel>
                                <x:Panel ID="Panel3" Title=" ANTECEDENTES MÉDICOS DE IMPORTANCIA" EnableBackgroundColor="true" Height="190px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items>  
                                        <x:Panel ID="Panel4" Width="250px" Height="150px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="DESCRIPCIÓN">
                                            <Items>
                                                <x:TextBox ID="TextBox5" runat="server" Text="INFECCIONES AUDITIVAS" Width="240" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label9" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox6" runat="server" Text="INFECCIONES OROFARINGEAS " Width="240" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label11" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox7" runat="server" Text="RESFRÍOS" Width="240" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label12" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox8" runat="server" Text="ACCIDENTES TRAUMÁTICO AUDITIVOS" Width="240" Enabled="False"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel5" Width="110px" Height="150px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="SI">
                                            <Items>                                                
                                                <x:CheckBox ID="chkOtitisa" runat="server" Text="" ShowLabel="true"></x:CheckBox>                                                                                                                                                    
                                                <x:Label ID="Label10" runat="server" Text=" "></x:Label>
                                                <x:CheckBox ID="chkRinitisa" runat="server" Text="" ShowLabel="true"></x:CheckBox>                                                                    
                                                <x:Label ID="Label14" runat="server" Text=" "></x:Label> 
                                                <x:CheckBox ID="chkSustQuimicasa" runat="server" Text="" ShowLabel="true"></x:CheckBox>                                                                  
                                                <x:Label ID="Label15" runat="server" Text=" "></x:Label>
                                                <x:CheckBox ID="chkSorderaFamiliara" runat="server" Text="" ShowLabel="true"></x:CheckBox>                                                    
                                            </Items>
                                        </x:Panel>
                                         <x:Panel ID="Panel6" Width="250px" Height="150px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="DESCRIPCIÓN">
                                            <Items>
                                                 <x:TextBox ID="TextBox9" runat="server" Text="USO  DE MEDICAMENTOS" Width="240" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label17" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label75" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label18" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label76" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label19" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label77" runat="server" Text=" "></x:Label>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel7" Width="110px" Height="150px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="SI">
                                            <Items>                        
                                                <x:CheckBox ID="chkuso_de_medicamentos" runat="server" Text="" ShowLabel="true"></x:CheckBox>                                               
                                                <x:Label ID="Label25" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label78" runat="server" Text=" "></x:Label>                                         
                                                <x:Label ID="Label26" runat="server" Text=" "></x:Label>
                                               <x:Label ID="Label79" runat="server" Text=" "></x:Label>                                              
                                                <x:Label ID="Label27" runat="server" Text=" "></x:Label>
                                                <%--<x:DropDownList ID="ddlSorderaFamiliar" runat="server" Width="100"></x:DropDownList>--%>     
                                            </Items>
                                        </x:Panel>
                                         <x:Panel ID="Panel8" Width="200px" Height="150px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="DESCRIPCIÓN" visible="false">
                                            <Items>
                                                 <x:TextBox ID="TextBox13" runat="server" Text="MENINGITIS" Width="190"></x:TextBox>
                                                <x:Label ID="Label21" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox14" runat="server" Text="DISLIPIDEMIA" Width="190"></x:TextBox>
                                                <x:Label ID="Label22" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox15" runat="server" Text="ENF. TIROIDEA" Width="190"></x:TextBox>
                                                <x:Label ID="Label23" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox16" runat="server" Text="SUST. QUÍMICAS" Width="190"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel11" Width="110px" Height="150px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="SI" visible="false">
                                            <Items>
                                                 <%--<x:DropDownList ID="ddlMeningitis" runat="server" Width="100"></x:DropDownList>--%>                                               
                                                <x:Label ID="Label29" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlDislipidemia" runat="server" Width="100"></x:DropDownList>                                               
                                                <x:Label ID="Label30" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlTiroida" runat="server" Width="100"></x:DropDownList>                                               
                                                <x:Label ID="Label31" runat="server" Text=" "></x:Label>
                                                <%--<x:DropDownList ID="ddlSustQuimicas" runat="server" Width="100"></x:DropDownList>--%>         
                                            </Items>
                                        </x:Panel>
                                    </Items>  
                                </x:Panel>
                                <x:Panel ID="Panel12" Title="ANTECEDENTES RELACIONADOS" EnableBackgroundColor="true" Height="70px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="30px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow17" ColumnWidths="240px 240px 240px" runat="server">
                                                    <Items>  
                                                        <x:CheckBox ID="ddlDiabetes" runat="server" Text="CONSUMO DE TABACO" ShowLabel="true"></x:CheckBox>
                                                        <x:CheckBox ID="chkTiro" runat="server" Text="SERVICIO MILITAR" ShowLabel="true"></x:CheckBox>
                                                        <x:CheckBox ID="chkUsoMP3" runat="server" Text="HOBBIES C. EXPOS. A RUIDO" ShowLabel="true"></x:CheckBox>
                                                    </Items>
                                                </x:FormRow>
                                                 <x:FormRow ID="FormRow28" ColumnWidths="240px 240px 240px 240px" runat="server">
                                                    <Items>  
                                                        <x:Label ID="Label82" runat="server" Text=""></x:Label>
                                                        <x:Label ID="Label83" runat="server" Text=""></x:Label>
                                                        <x:Label ID="Label84" runat="server" Text=""></x:Label>
                                                        <x:Label ID="Label85" runat="server" Text=""></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow27" ColumnWidths="240px 240px 240px" runat="server">
                                                    <Items>  
                                                        <x:CheckBox ID="ddlMeningitis" runat="server" Text="EXPOS. LABORAL A QUÍMICOS" ShowLabel="true"></x:CheckBox>
                                                        <x:Label ID="Label80" runat="server" Text=""></x:Label>
                                                        <x:Label ID="Label81" runat="server" Text=""></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>    
                                    </Items>                                           
                                </x:Panel>
                               
                                <x:Panel ID="Panel14" Title="OTOSCOPIA" EnableBackgroundColor="true" Height="920px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="." ID="GroupPanel5" BoxFlex="1" Height="70" TableColspan="3">
                                            <Items>
                                                 <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow3" ColumnWidths="90px 380px 90px 380px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label16" runat="server" Text="" Width="90px" Label="OD"></x:Label>
                                                                <x:TextArea ID="txtOidoDerecho" runat="server" Text="" Label="" ShowLabel="false" Height="45" Enabled="False"></x:TextArea>

                                                                 <x:Label ID="Label20" runat="server" Text="" Width="90px" Label="OI"></x:Label>
                                                                <x:TextArea ID="txtOidoIzquierdo" runat="server" Text="" Label="" ShowLabel="false" Height="45" Enabled="False"></x:TextArea>
                                                            </Items>
                                                        </x:FormRow>                                                      
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                       
                                        <x:GroupPanel runat="server" Title="Oído Derecho" ID="GroupPanel9" BoxFlex="1" Height="150" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form23" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow85" ColumnWidths=" 90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:Label ID="TextBox74" runat="server" Text="" Width="90px" Label="Vía Aérea"></x:Label>
                                                                <x:Label ID="TextBox623" runat="server" Text="125 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="TextBox624" runat="server" Text="250 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="TextBox625" runat="server" Text="500 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="TextBox626" runat="server" Text="1000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="TextBox627" runat="server" Text="2000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="TextBox628" runat="server" Text="3000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="TextBox629" runat="server" Text="4000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="TextBox270" runat="server" Text="6000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="TextBox721" runat="server" Text="8000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow82" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox723" runat="server" Text="" Width="90px" Label="Vía Área"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_125" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="1" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_250" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="2" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_500" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="3" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_1000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="4" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_2000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="5" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_3000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="6" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_4000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="7" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_6000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="8" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VA_8000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="9" Enabled="False"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow83" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox54" runat="server" Text="" Width="90px" Label="Vía Ósea"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_125" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="10" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_250" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="11" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_500" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="12" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_1000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="13" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_2000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="14" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_3000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="15" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_4000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="16" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_6000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="17" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_VO_8000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="18" Enabled="False"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow4" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox6488" runat="server" Text="" Width="90px" Label="EM - A"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_125" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="19" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_250" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="20" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_500" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="21" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_1000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="22" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_2000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="23" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_3000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="24" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_4000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="25" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_6000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="26" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_AN_8000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="27" Enabled="False"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow84" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox64" runat="server" Text="" Width="90px" Label="EM - O"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_125" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="19" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_250" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="20" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_500" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="21" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_1000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="22" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_2000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="23" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_3000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="24" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_4000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="25" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_6000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="26" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOD_EM_8000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="27" Enabled="False"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        

                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="Oído Izquierdo" ID="GroupPanel10" BoxFlex="1" Height="150" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form24" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow86" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label28" runat="server" Text="" Width="90px" Label="Vía Aérea"></x:Label>
                                                                <x:Label ID="Label4" runat="server" Text="125 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label8" runat="server" Text="250 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label13" runat="server" Text="500 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label32" runat="server" Text="1000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label33" runat="server" Text="2000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label34" runat="server" Text="3000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label35" runat="server" Text="4000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label36" runat="server" Text="6000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label37" runat="server" Text="8000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow87" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="Label138" runat="server" Text="" Width="90px" Label="Vía Aérea"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_125" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="28" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_250" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="29" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_500" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="30" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_1000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="31" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_2000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="32" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_3000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="33" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_4000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="34" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_6000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="35" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VA_8000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="36" Enabled="False"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow88" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox94" runat="server" Text="" Width="90px" Label="Vía Ósea"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_125" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="37" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_250" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="38" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_500" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="39" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_1000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="40" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_2000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="41" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_3000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="42" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_4000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="43" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_6000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="44" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_VO_8000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="45" Enabled="False"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow5" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox27" runat="server" Text="" Width="90px" Label="EM - A"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_125" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="46" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_250" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="47" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_500" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="48" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_1000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="49" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_2000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="50" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_3000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="51" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_4000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="52" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_6000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="53" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_AN_8000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="54" Enabled="False"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow89" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox104" runat="server" Text="" Width="90px" Label="EM - O"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_125" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="46" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_250" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="47" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_500" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="48" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_1000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="49" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_2000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="50" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_3000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="51" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_4000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="52" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_6000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="53" Enabled="False"></x:TextBox>
                                                                <x:TextBox ID="txtOI_EM_8000" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="54" Enabled="False"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                         
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel ID="GroupPanel4" runat="server" Title="Imagen Oído Derecho - Oído Izquierdo" BoxFlex="1" Height="370" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form193" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow551" ColumnWidths="460px 460px" runat="server">
                                                            <Items>
                                                                <x:ContentPanel ID="ContentPanel1" runat="server" Width="450px" BodyPadding="5px" EnableBackgroundColor="true" ShowBorder="true" ShowHeader="true" Title="Oído Derecho">
                                                                    <asp:Chart ID="Chart1" runat="server" Palette="BrightPastel" BackColor="#F3DFC1" ImageType="Png" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="412px" Height="296px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1">
                                                                        <Legends>
                                                                            <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
                                                                        </Legends>
                                                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                                        <Series>
                                                                            <asp:Series MarkerSize="8" BorderWidth="3" XValueType="Double" Name="Series1" ChartType="Line" MarkerStyle="Circle" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" ShadowOffset="2" YValueType="Double"></asp:Series>
                                                                            <asp:Series MarkerSize="9" BorderWidth="3" XValueType="Double" Name="Series2" ChartType="Line" MarkerStyle="Diamond" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 224, 64, 10" ShadowOffset="2" YValueType="Double"></asp:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                                                <Area3DStyle Rotation="25" Perspective="9" LightStyle="Realistic" Inclination="40" IsRightAngleAxes="False" WallWidth="3" IsClustered="False" />
                                                                                <AxisY LineColor="64, 64, 64, 64">
                                                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                </AxisY>
                                                                                <AxisX LineColor="64, 64, 64, 64">
                                                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                </AxisX>
                                                                            </asp:ChartArea>
                                                                        </ChartAreas>
                                                                    </asp:Chart>
                                                                    <asp:Button ID="aspButtonOD" Text="Ver Gráfico" runat="server" OnClick="aspButtonOD_Click" UseSubmitBehavior="false" />
                                                                </x:ContentPanel>
                                                                <x:ContentPanel ID="ContentPanel2" runat="server" Width="450px" BodyPadding="5px" EnableBackgroundColor="true" ShowBorder="true" ShowHeader="true" Title="Oído Izquierdo">
                                                                    <asp:Chart ID="Chart2" runat="server" Palette="BrightPastel" BackColor="#F3DFC1" ImageType="Png" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="412px" Height="296px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1">
                                                                        <Legends>
                                                                            <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
                                                                        </Legends>
                                                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                                        <Series>
                                                                            <asp:Series MarkerSize="8" BorderWidth="3" XValueType="Double" Name="Series1" ChartType="Line" MarkerStyle="Circle" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" ShadowOffset="2" YValueType="Double"></asp:Series>
                                                                            <asp:Series MarkerSize="9" BorderWidth="3" XValueType="Double" Name="Series2" ChartType="Line" MarkerStyle="Diamond" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 224, 64, 10" ShadowOffset="2" YValueType="Double"></asp:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                                                <Area3DStyle Rotation="25" Perspective="9" LightStyle="Realistic" Inclination="40" IsRightAngleAxes="False" WallWidth="3" IsClustered="False" />
                                                                                <AxisY LineColor="64, 64, 64, 64">
                                                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                </AxisY>
                                                                                <AxisX LineColor="64, 64, 64, 64">
                                                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                </AxisX>
                                                                            </asp:ChartArea>
                                                                        </ChartAreas>
                                                                    </asp:Chart>

                                                                    <asp:Button ID="aspButtonOI" Text="Ver Gráfico" runat="server" OnClick="aspButtonOI_Click" UseSubmitBehavior="false" />
                                                                </x:ContentPanel>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow552AA" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtMultimediaFileId_OD" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtMultimediaFileId_OI" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtServiceComponentMultimediaId_OD" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtServiceComponentMultimediaId_OI" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>                                           
                                </x:Panel>
                                <x:Panel ID="Panel15" Title="CONCLUSIONES" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" visible="false">
                                    <Items>
                                        <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="10px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow6" ColumnWidths="250px 50px 250px 50px 250px 50px" runat="server">
                                                    <Items>  
                                                        <x:Label ID="Label24" runat="server" Text="NORMOACUSIA BILATERAL" ShowLabel="false"></x:Label>    
                                                        <x:CheckBox ID="chknormoacusia_bilateral" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                        <x:Label ID="Label38" runat="server" Text="NORMOACUSIA OIDO DERECHO" ShowLabel="false"></x:Label>    
                                                        <x:CheckBox ID="chknormoacusia_od" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                        <x:Label ID="Label39" runat="server" Text="NORMOACUSIA OIDO IZQUIERDO" ShowLabel="false"></x:Label>    
                                                        <x:CheckBox ID="chknormoacusia_oi" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                        
                                                    </Items>
                                                </x:FormRow>                                              
                                            </Rows>
                                        </x:Form>    
                                    </Items>       
                                </x:Panel>
                            </Items>
                        </x:Tab>

                        <x:Tab ID="TabAudiometriaInternacional" BodyPadding="5px" Title="Audiometría" runat="server" Hidden="false">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar6" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarAudiometriaInternacional" Text="Grabar Audiometría" Icon="SystemSave" runat="server" OnClick="btnGrabarAudiometriaInternacional_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Label ID="Label746" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server" Enabled="false"></x:DropDownList>
                                        <x:Button ID="Button1" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel16" Title="" EnableBackgroundColor="true" Height="1660px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="False" TableConfigColumns="3" Layout="Table">
                                    <Items>

                                        <x:GroupPanel runat="server" Title="CONDICIÓN  A LA EVALUACIÓN" ID="GroupPanel6" BoxFlex="1" Height="60" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form17" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow52" ColumnWidths="160px 150px 625px " runat="server">
                                                            <Items>
                                                                <x:Label ID="lblcondicion" runat="server" Text="Condición a la evaluación" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlAU_Condicion" runat="server" Width="130" ShowLabel="false"></x:DropDownList>
                                                                <x:TextBox ID="txtAU_Observaciones" runat="server" Label="Observaciones" Width="530px"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="Antedecentes Patológicos" ID="GroupPanel7" BoxFlex="1" Height="310" Width="350px">
                                            <Items>
                                                <x:Form ID="Form18" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="250px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow53" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkSupuracion" runat="server" Label="Supuración de oidos"></x:CheckBox>

                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow54" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkVertigo" runat="server" Label="Vertigo"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow55" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkOtitis" runat="server" Label="Otitis Mastoditis"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow56" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkParatodiditis" runat="server" Label="Paratodisitis"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow57" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkMeningitis" runat="server" Label="Meningitis encefalitis"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow58" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkGolpesCefalicos" runat="server" Label="Golpes cefálicos"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow59" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkParalisisFacial" runat="server" Label="Parálisis facial"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow60" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkTTOANTITBC" runat="server" Label="TTO ANTI TBC"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow61" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkTTOOtotoxicos" runat="server" Label="TTO Ototoxicos"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow62" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkConsumoMedicamento" runat="server" Label="Consumo actual medicamento"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow63" ColumnWidths="350px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkExposicionSolventes" runat="server" Label="Exposición a solventes(plomo, plata, etc.)"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="Exposición a Ruído" ID="GroupPanel8" BoxFlex="1" Height="310" Width="610px" TableColspan="2">
                                            <Items>
                                                <x:Form ID="Form19" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="250px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow64" ColumnWidths="600px" runat="server">
                                                            <Items>
                                                                <%--<x:CheckBox ID="chkRuidoExtra" runat="server" Label="*Ruido extra laboral"></x:CheckBox>--%>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow65" ColumnWidths="600px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkRuidoLaboral" runat="server" Label="**Ruido laboral"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow66" ColumnWidths="340px  100px  100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="lbl123" runat="server" Text="(*) RUIDO EXTRA LABORAL" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label40" runat="server" Text="Años" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label42" runat="server" Text="Frecuencia" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow67" ColumnWidths="340px  100px  140px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkServicioMilitar" runat="server" Label="Servicio Militar"></x:CheckBox>
                                                                <x:TextBox ID="txtAnioServicioMilitar" runat="server" Width="90" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtFrecuenciaServicioMilitar" runat="server" Width="130" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow68" ColumnWidths="340px  100px  140px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkDeportesAereos" runat="server" Label="Deportes aéreos"></x:CheckBox>
                                                                <x:TextBox ID="txtAnioDeportesAereos" runat="server" Width="90" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtFrecuenciaDeportesAereos" runat="server" Width="130" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow69" ColumnWidths="340px  100px  140px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkDeportesSubmarinos" runat="server" Label="Deportes submarinos"></x:CheckBox>
                                                                <x:TextBox ID="txtAnioDeporteSubmarino" runat="server" Width="90" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtFrecuenciaDeporteSubmarino" runat="server" Width="130" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow70" ColumnWidths="340px  100px  140px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkManipulacionArmas" runat="server" Label="Manipulación de armas de fuego"></x:CheckBox>
                                                                <x:TextBox ID="txtAnioManipulacionArmas" runat="server" Width="90" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtFrecuenciaManipulacionArmas" runat="server" Width="130" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow71" ColumnWidths="340px  100px  140px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkExposicionMusica" runat="server" Label="Exposición música alta"></x:CheckBox>
                                                                <x:TextBox ID="txtAnioExposicionMusica" runat="server" Width="90" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtFrecuenciaExposicionMusica" runat="server" Width="130" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow72" ColumnWidths="340px  100px  140px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkUsoAudifonos" runat="server" Label="Uso de audífonos"></x:CheckBox>
                                                                <x:TextBox ID="txtAnioUsoaudifnos" runat="server" Width="90" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtFrecuenciaUsoaudifnos" runat="server" Width="130" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow73" ColumnWidths="340px  100px  140px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkMotociclismo" runat="server" Label="Motociclismo"></x:CheckBox>
                                                                <x:TextBox ID="txtAnioMotociclismo" runat="server" Width="90" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtFrecuenciaMotociclismo" runat="server" Width="130" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow74" ColumnWidths="340px  100px  140px" runat="server">
                                                            <Items>
                                                                <x:CheckBox ID="chkOtro" runat="server" Label="Otro"></x:CheckBox>
                                                                <x:TextBox ID="txtAnioOtro" runat="server" Width="90" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtFrecuenciaOtro" runat="server" Width="130" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="(**)Ruído Laboral" ID="GroupPanel11" BoxFlex="1" Height="100" Width="350">
                                            <Items>
                                                <x:Form ID="Form20" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow7" runat="server">
                                                            <Items>
                                                                <x:DropDownList ID="ddlTipoRuido" runat="server" Width="220" Label="Tipo de ruido"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow8" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtIntensidad" runat="server" Label="Intesidad" Width="220px"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="Horas por día" ID="GroupPanel13" BoxFlex="1" Height="100" Width="320">
                                            <Items>
                                                <x:Form ID="Form21" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow9" runat="server">
                                                            <Items>
                                                                <x:DropDownList ID="ddlHorasPorDia" runat="server" Width="290" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow78" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtHoras" runat="server" Label="Horas" Width="235px"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="Protectores" ID="GroupPanel14" BoxFlex="1" Height="100" Width="290">
                                            <Items>
                                                <x:Form ID="Form22" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow79" runat="server">
                                                            <Items>
                                                                <x:DropDownList ID="ddlTapones" runat="server" Width="205" Label="Tapones"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow80" runat="server">
                                                            <Items>
                                                                <x:DropDownList ID="ddlOrejeras" runat="server" Width="205" Label="Orejeras"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow81" runat="server">
                                                            <Items>
                                                                <x:DropDownList ID="ddlAmbos" runat="server" Width="205" Label="Ambos"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="Oído Derecho" ID="GroupPanel15" BoxFlex="1" Height="120" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form6" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow10" ColumnWidths=" 90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label43" runat="server" Text="" Width="90px" Label="Vía Aérea"></x:Label>
                                                                <x:Label ID="Label44" runat="server" Text="125 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label45" runat="server" Text="250 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label46" runat="server" Text="500 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label47" runat="server" Text="1000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label48" runat="server" Text="2000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label49" runat="server" Text="3000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label50" runat="server" Text="4000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label51" runat="server" Text="6000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label52" runat="server" Text="8000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow11" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox17" runat="server" Text="" Width="90px" Label="Vía Área"></x:TextBox>
                                                                <x:NumberBox ID="txtOD_VA_125_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="1" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VA_250_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="2" MaxValue="100" MinValue="-10" ></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VA_500_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="3" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VA_1000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="4" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VA_2000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="5" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VA_3000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="6" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VA_4000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="7" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VA_6000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="8" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VA_8000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="9" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow12" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox28" runat="server" Text="" Width="90px" Label="Vía Ósea"></x:TextBox>
                                                                <x:NumberBox ID="txtOD_VO_125_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="10" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VO_250_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="11" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VO_500_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="12" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VO_1000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="13" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VO_2000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="14" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VO_3000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="15" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VO_4000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="16" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VO_6000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="17" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_VO_8000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="18" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow13" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox38" runat="server" Text="" Width="90px" Label="Enmascaramiento"></x:TextBox>
                                                                <x:NumberBox ID="txtOD_EM_125_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="19" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_EM_250_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="20" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_EM_500_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="21" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_EM_1000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="22" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_EM_2000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="23" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_EM_3000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="24" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_EM_4000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="25" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_EM_6000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="26" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOD_EM_8000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="27" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                            </Items>
                                                        </x:FormRow>

                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="Oído Izquierdo" ID="GroupPanel16" BoxFlex="1" Height="120" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form7" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow14" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label53" runat="server" Text="" Width="90px" Label="Vía Aérea"></x:Label>
                                                                <x:Label ID="Label54" runat="server" Text="125 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label55" runat="server" Text="250 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label56" runat="server" Text="500 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label57" runat="server" Text="1000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label58" runat="server" Text="2000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label59" runat="server" Text="3000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label60" runat="server" Text="4000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label61" runat="server" Text="6000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label62" runat="server" Text="8000 Hz" Width="70px" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow15" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox48" runat="server" Text="" Width="90px" Label="Vía Aérea"></x:TextBox>
                                                                <x:NumberBox ID="txtOI_VA_125_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="28" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VA_250_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="29" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VA_500_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="30" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VA_1000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="31" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VA_2000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="32" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VA_3000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="33" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VA_4000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="34" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VA_6000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="35" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VA_8000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="36" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow16" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox59" runat="server" Text="" Width="90px" Label="Vía Ósea"></x:TextBox>
                                                                <x:NumberBox ID="txtOI_VO_125_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="37" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VO_250_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="38" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VO_500_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="39" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VO_1000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="40" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VO_2000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="41" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VO_3000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="42" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VO_4000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="43" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VO_6000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="44" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_VO_8000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="45" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow18" ColumnWidths="90px 80px 80px 80px 80px 80px 80px 80px 80px 80px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="TextBox70" runat="server" Text="" Width="90px" Label="Enmascaramiento"></x:TextBox>
                                                                <x:NumberBox ID="txtOI_EM_125_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="46" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_EM_250_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="47" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_EM_500_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="48" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_EM_1000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="49" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_EM_2000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="50" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_EM_3000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="51" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_EM_4000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="52" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_EM_6000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="53" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                                <x:NumberBox ID="txtOI_EM_8000_Inter" runat="server" Text="" Width="70px" ShowLabel="false" TabIndex="54" MaxValue="100" MinValue="-10"></x:NumberBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel ID="GroupPanel17" runat="server" Title="Imageen Oído Derecho - Oído Izquierdo" BoxFlex="1" Height="370" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form10" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>

                                                        <x:FormRow ID="FormRow21" ColumnWidths="460px 460px" runat="server">
                                                            <Items>
                                                                <x:ContentPanel ID="ContentPanel3" runat="server" Width="450px" BodyPadding="5px" EnableBackgroundColor="true" ShowBorder="true" ShowHeader="true" Title="Oído Derecho">
                                                                    <asp:Chart ID="Chart1_Inter" runat="server" Palette="BrightPastel" BackColor="#F3DFC1" ImageType="Png" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="412px" Height="296px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1">
                                                                        <Legends>
                                                                            <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
                                                                        </Legends>
                                                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                                        <Series>
                                                                            <asp:Series MarkerSize="8" BorderWidth="3" XValueType="Double" Name="Series1" ChartType="Line" MarkerStyle="Circle" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" ShadowOffset="2" YValueType="Double"></asp:Series>
                                                                            <asp:Series MarkerSize="9" BorderWidth="3" XValueType="Double" Name="Series2" ChartType="Line" MarkerStyle="Diamond" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 224, 64, 10" ShadowOffset="2" YValueType="Double"></asp:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea Name="ChartArea1_Inter" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                                                <Area3DStyle Rotation="25" Perspective="9" LightStyle="Realistic" Inclination="40" IsRightAngleAxes="False" WallWidth="3" IsClustered="False" />
                                                                                <AxisY LineColor="64, 64, 64, 64">
                                                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                </AxisY>
                                                                                <AxisX LineColor="64, 64, 64, 64">
                                                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                </AxisX>
                                                                            </asp:ChartArea>
                                                                        </ChartAreas>
                                                                    </asp:Chart>
                                                                    <asp:Button ID="aspButtonOD_Inter" Text="Ver Gráfico" runat="server" OnClick="aspButtonOD_Inter_Click" UseSubmitBehavior="false" />
                                                                </x:ContentPanel>
                                                                <x:ContentPanel ID="ContentPanel4" runat="server" Width="450px" BodyPadding="5px" EnableBackgroundColor="true" ShowBorder="true" ShowHeader="true" Title="Oído Izquierdo">
                                                                    <asp:Chart ID="Chart2_Inter" runat="server" Palette="BrightPastel" BackColor="#F3DFC1" ImageType="Png" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="412px" Height="296px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1">
                                                                        <Legends>
                                                                            <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
                                                                        </Legends>
                                                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                                        <Series>
                                                                            <asp:Series MarkerSize="8" BorderWidth="3" XValueType="Double" Name="Series1" ChartType="Line" MarkerStyle="Circle" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" ShadowOffset="2" YValueType="Double"></asp:Series>
                                                                            <asp:Series MarkerSize="9" BorderWidth="3" XValueType="Double" Name="Series2" ChartType="Line" MarkerStyle="Diamond" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 224, 64, 10" ShadowOffset="2" YValueType="Double"></asp:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea Name="ChartArea1_Inter" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                                                <Area3DStyle Rotation="25" Perspective="9" LightStyle="Realistic" Inclination="40" IsRightAngleAxes="False" WallWidth="3" IsClustered="False" />
                                                                                <AxisY LineColor="64, 64, 64, 64">
                                                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                </AxisY>
                                                                                <AxisX LineColor="64, 64, 64, 64">
                                                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                </AxisX>
                                                                            </asp:ChartArea>
                                                                        </ChartAreas>
                                                                    </asp:Chart>

                                                                    <asp:Button ID="aspButtonOI_Inter" Text="Ver Gráfico" runat="server" OnClick="aspButtonOI_Inter_Click" UseSubmitBehavior="false" />
                                                                </x:ContentPanel>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow22" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtMultimediaFileId_OD_Inter" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtMultimediaFileId_OI_Inter" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtServiceComponentMultimediaId_OD_Inter" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtServiceComponentMultimediaId_OI_Inter" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title=" " ID="GroupPanel18" BoxFlex="1" Height="160" TableColspan="1">
                                            <Items>
                                                <x:Form ID="Form25" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow90" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label63" runat="server" Text="" Width="50" Label="STS"></x:Label>
                                                                <x:Label ID="Label64" runat="server" Text="Año" Width="50" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label65" runat="server" Text="OD" Width="50" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label66" runat="server" Text="OI" Width="50" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>

                                                        <x:FormRow ID="FormRow91" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label67" runat="server" Text="" Width="50" Label="Base"></x:Label>
                                                                <x:TextBox ID="TextBox85" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="TextBox86" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="TextBox87" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>

                                                        <x:FormRow ID="FormRow92" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label68" runat="server" Text="" Width="50" Label="Referential"></x:Label>
                                                                <x:TextBox ID="TextBox114" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="TextBox115" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="TextBox116" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>

                                                        <x:FormRow ID="FormRow93" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label69" runat="server" Text="" Width="50" Label="Actual"></x:Label>
                                                                <x:TextBox ID="txtActualAnio" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtActualOD" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                                <x:TextBox ID="txtActualOI" runat="server" Text="" Width="50" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>


                                                        <x:FormRow ID="FormRow94" ColumnWidths="80px 240px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label70" runat="server" Text="" Width="50" Label="Menoscabo Auditivo"></x:Label>
                                                                <x:TextBox ID="txtMenoscaboAuditivo" runat="server" Text="" Width="210" ShowLabel="false"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>

                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="Datos del Audiometro" ID="GroupPanel19" BoxFlex="1" Height="160" TableColspan="2">
                                            <Items>
                                                <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow23" ColumnWidths="200px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtCalibracion_Inter" runat="server" Text="" Width="180" Label="Calibración"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow96" ColumnWidths="200px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtMarca_Inter" runat="server" Text="" Width="180" Label="Marca"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow97" ColumnWidths="200px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtModelo_Inter" runat="server" Text="" Width="180" Label="Modelo"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow98" ColumnWidths="200px" runat="server">
                                                            <Items>
                                                                <x:TextArea ID="txtNivelRuidoAmbiental" runat="server" Text="" Width="180" Label="Nivel de ruido Ambiental" Height="50px"></x:TextArea>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <%--Todo: aqui va...--%>
                                        <x:GroupPanel runat="server" Title="HALLAZGOS" ID="GroupPanel64" BoxFlex="1" Height="50" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form197" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="180px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow1555" ColumnWidths="250px 250px 250px" runat="server">
                                                            <Items>
                                                               <x:CheckBox ID="chkNormoBilateral" runat="server" Label="NORMOACUSIA BILATERAL"></x:CheckBox>
                                                                <x:CheckBox ID="chkNormoOD" runat="server" Label="NORMOACUSIA OD"></x:CheckBox>
                                                                <x:CheckBox ID="chkNormoOI" runat="server" Label="NORMOACUSIA OI"></x:CheckBox>
                                                            </Items>

                                                        </x:FormRow>                                                    
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="AUDITORÍA" ID="GroupPanel65" BoxFlex="1" Height="80" TableColspan="3">
                                            <Items>
                                                <x:Form ID="Form207" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow24" ColumnWidths="310px 310px 310px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtAudiometriaAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                                <x:TextBox ID="txtAudiometriaAuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                                <x:TextBox ID="txtAudiometriaAuditorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                            </Items>

                                                        </x:FormRow>
                                                        <Ext:FormRow ID="FormRow620" ColumnWidths="310px 310px 310px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtAudiometriaEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                                <x:TextBox ID="txtAudiometriaEvaluadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                                <x:TextBox ID="txtAudiometriaEvaluadorEvaluacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                            </Items>
                                                        </Ext:FormRow>
                                                         <Ext:FormRow ID="FormRow208" ColumnWidths="320px 320px 320px" runat="server">
                                                            <Items>
                                                                <x:TextBox ID="txtAudiometriaInformador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                                <x:TextBox ID="txtAudiometriaInformadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                                <x:TextBox ID="txtAudiometriaInformadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                            </Items>
                                                        </Ext:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                     
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
       Target="Top" OnClose="winEdit1_Close"  IsModal="true"  Height="630px" Width="870px">
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
