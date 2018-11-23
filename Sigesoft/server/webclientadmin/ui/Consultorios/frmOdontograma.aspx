<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmOdontograma.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmOdontograma" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title></title>
     <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.2.min.js"  type="text/javascript"></script>
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
    <style media="screen">
			canvas{border:solid;}
		</style>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" runat="server" AjaxAspnetControls="txtServiceId" />
        <x:Panel ID="Panel2" runat="server" Height="5000px" Width="1000px" ShowBorder="True"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Odontograma">
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
                         <x:Tab ID="TabOdontograma" BodyPadding="5px" Title="Odontograma" runat="server">
                              <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarRayosX" Text="Grabar Odontología" Icon="SystemSave" runat="server" OnClick="btnGrabarRayosX_Click" AjaxLoadingType="Mask"></x:Button>                                   
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporteOdonto" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel29" Title="ANTECEDENTES" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form90" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                            <Rows>
                                               <x:FormRow ID="FormRow307" ColumnWidths="800px" runat="server" >
                                                    <Items>
                                                         <x:TextArea ID="txtAntecedentes" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="800px" Height="35px"></x:TextArea>                                                      
                                                    </Items>
                                                </x:FormRow>
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                </x:Panel>
                                <x:Panel ID="Panel1" Title="SÍNTOMAS" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                            <Rows>
                                               <x:FormRow ID="FormRow1" ColumnWidths="800px" runat="server" >
                                                    <Items>
                                                         <x:TextArea ID="txtSintomas" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="800px" Height="35px"></x:TextArea>                                                      
                                                    </Items>
                                                </x:FormRow>
                                                </Rows>
                                            </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel3" Title="DESCRIPCIÓN" EnableBackgroundColor="true" Height="40px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                            <Rows>
                                               <x:FormRow ID="FormRow2" ColumnWidths="150px 120px 150px 120px 150px 120px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label346" runat="server" Text="NRO.DIENTES CON CARIES:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroCaries" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="110px" ></x:TextBox>   
                                                         <x:Label ID="label1" runat="server" Text="NRO.DIENTES AUSENTES:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroAusentes" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="110px" ></x:TextBox>       
                                                         <x:Label ID="label2" runat="server" Text="NRO.DIENTES CURADOS:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroCurados" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="110px" ></x:TextBox>                                                          
                                                    </Items>
                                                </x:FormRow>
                                                </Rows>
                                            </x:Form>
                                    </Items>
                                </x:Panel>                               
                               <x:ContentPanel ID="ContentPanel1" runat="server" Width="975px" BodyPadding="5px"
                                    EnableBackgroundColor="true" ShowBorder="true" ShowHeader="true" Title="Odontograma" Visible="false">   
                                    <div>
                                       Ausente <input type="checkbox" value="3" id="chkAusente" />
                                       Limpiar <input type="checkbox" value="3" id="chkNoAusente" />                                       
                                       <input type="button" id="btnSave" name="btnSave" value="Save the canvas to server" />
                                       <input type="button" id="btnMostrar" name="btnShow" value="mostar Odontograma" onclick="showOdonto()" />
                                    </div>                                
                                    <div id="container" style="position:relative; width:970px; height:500px; margin:0 auto 30px auto;">
                                        <canvas id="lienzo"  width="960" height="500">Su navegador no soporta canvas :( </canvas>
                                        <div id="output" style="position:absolute; padding:5px;"></div>
                                    </div>
                                </x:ContentPanel>
                                 <x:Panel ID="Panel84" Title="AUDITORÍA" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form213" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow654" ColumnWidths="320px 300px 300px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOdontoAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="ttxtOdontoAuditorInsertar" runat="server" Text="" Width="180" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOdontoAuditorEditar" runat="server" Text="" Width="180" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow656" ColumnWidths="320px 300px 300px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOdontoEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOdontoEvaluadorInsertar" runat="server" Text="" Width="180" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="ttxtOdontoEvaluadorEvaluar" runat="server" Text="" Width="180" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>

                                                  <Ext:FormRow ID="FormRow3" ColumnWidths="320px 300px 300px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOdontoInformador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOdontoInformadorInsertar" runat="server" Text="" Width="180" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="ttxtOdontoInformadorEvaluar" runat="server" Text="" Width="180" Label="Modificación" Readonly="True"></x:TextBox>
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
         <x:HiddenField ID="HiddenFieldServiceId" runat="server"></x:HiddenField>
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

     
        let imgPiezaAusente = new Image();
        let imgPiezaSana = new Image();

        function showOdonto() {

            ctx.clearRect(0,0,960,400);
            var highlightRowsServiceIdClientID = '<%= HiddenFieldServiceId.ClientID %>';
            var x = X(highlightRowsServiceIdClientID).getValue();
            d = new Date();
            

            
            imgPiezaAusente.src = "./imgOdonto/PiezaAusente.png?timestamp=" +d.getTime();         
            imgPiezaAusente.onload = function(){              
                console.log("la imagen PiezaAusente fue cargada");
            }

          
            imgPiezaSana.src = "./imgOdonto/PiezaSana.png?timestamp=" +d.getTime();         
            imgPiezaSana.onload = function(){              
                console.log("la imagen PiezaSana fue cargada");               
            }

            let img1 = new Image();
            img1.src = "./imgOdonto/" +x +".jpg?timestamp=" +d.getTime();         
            img1.onload = function(){
              
                console.log("la imagen fue cargada");
                ctx.drawImage(img1,0,0);
                click(imgPiezaAusente,imgPiezaSana);
            }
            img1.addEventListener('error', imageNotFound);

           
           
        }
          
        function imageNotFound() {
            //console.log("la imagen no fue cargada");

            let imgMaster = new Image();
            imgMaster.src = "./imgOdonto/odontograma.png?timestamp=" +d.getTime();  
            imgMaster.onload = function(){
              
                console.log("la imagen Master fue cargada");
                ctx.drawImage(imgMaster,0,0,950,500);
                click();
            }

            //dibujarCuadrante(10, 10, 40, 40);
            //click();
        }



        function dibujarCuadrante(x, y, x2, y2) { //x = 10 ; y = 10 ; x2 = 40 ; y2 = 40
            canvas = document.getElementById("lienzo");
            ctx = canvas.getContext("2d");
            ctx.clearRect(x, y, x2, y2);
            ctx.strokeRect(x, y, x2, y2);
   
            ctx.beginPath();
            ctx.moveTo(x, y);
            ctx.lineTo(x + x2, y + y2);
            ctx.stroke();

            ctx.beginPath();
            ctx.moveTo(x + x2, x);
            ctx.lineTo(x, y2 + x);
            ctx.stroke();

            dibujar(25, 25, 10, 10, 'rgba(250,250,250,255)'); //cuadrante centro

            dibujar(200, 140, 10, 10, 'rgba(250,250,250,255)');//cuadrante arriba

            dibujar(200, 260, 10, 10, 'rgba(250,250,250,255)');//cuadrante abajo

            dibujar(140, 200, 10, 10, 'rgba(250,250,250,255)');//cuadrante izquierda

            dibujar(260, 200, 10, 10, 'rgba(250,250,250,255)');//cuadrante derecha

        }

        //function imageFound() {
        //    //alert('That image is found and loaded');
        //    console.log("la imagen fue cargada");
        //    dibujarCuadrante(130, 130, 190, 190);
        //    ctx.drawImage(img1,0,0);
        //    click();
        //}

        //img1.onload = function(){
        //    console.log("la imagen fue cargada");
           
        //}

        //window.onload = function () {
         
        //    let img1 = new Image();
        //    img1.src = "./imgOdonto/N001-SR000000069.jpg";

        //    dibujarCuadrante(130, 130, 190, 190);
        //    ctx.drawImage(img1,0,0);
          

          
        //    click();
        //}


        function dibujar(x, y, x2, y2,color) {
            canvas = document.getElementById("lienzo");
            ctx = canvas.getContext("2d");
            ctx.fillStyle = color;
            ctx.fillRect(x, y, x2, y2);
            ctx.fill();


        }

        function dibujarAusente(x, y, x2, y2, color) {
            canvas = document.getElementById("lienzo");
            ctx = canvas.getContext("2d");
            ctx.strokeStyle = 'Gray';
            ctx.lineWidth = 10;

            ctx.beginPath();
            ctx.moveTo(x +20 , y +20 );
            ctx.lineTo(x + 190 -20 , y + 190 -20 );
            ctx.stroke();

            ctx.beginPath();
            ctx.moveTo(x + 190 -20 , x  + 20 );
            ctx.lineTo(x  +20  , 190 + x -20 );
            ctx.stroke();

        }

      
      

        function click(imgPiezaAusente,imgPiezaSana) {

            canvas.addEventListener("click",
            function (e) {      
                var mousePos = oMousePos(canvas, e);             
                bAusente = document.getElementById("chkAusente").checked;
                bNoAusente = document.getElementById("chkNoAusente").checked;

                //************Ausente********************************//
                //Ausente 18
                    if (mousePos.x > 9
                        && mousePos.x < 57) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {
                            //ctx.save();
                            //dibujarAusente(130, 130, 190, 190, '255,0,0,255');
                            //ctx.restore();
                            if (bAusente) {
                            //let imgPiezaAusente = new Image();
                            //    imgPiezaAusente.src = "./imgOdonto/PiezaAusente.png?timestamp=" +d.getTime();         
                            //    imgPiezaAusente.onload = function(){
              
                                //        console.log("la imagen PiezaAusente fue cargada otra vez");
                                //alert(imgPiezaAusente);
                                    ctx.drawImage(imgPiezaAusente,9,211);
                                    click();
                                //}
                                document.getElementById("chkAusente").checked = false;
                            }
                            if (bNoAusente) {
                                //let imgPiezaSana = new Image();
                                //imgPiezaSana.src = "./imgOdonto/PiezaSana.png?timestamp=" +d.getTime();         
                                //imgPiezaSana.onload = function(){
              
                                //    console.log("la imagen PiezaSana fue cargada");
                                    ctx.drawImage(imgPiezaSana,9,211);
                                    click();
                                //}

                                document.getElementById("chkNoAusente").checked = false;
                            }
                        }
                    }              

                //Ausente 17               
                    if (mousePos.x > 66
                        && mousePos.x < 112) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,65,211);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,65,211);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }
              
                //Ausente 16              
                    if (mousePos.x > 118
                        && mousePos.x < 165) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,118,211);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,118,211);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }
          
                //Ausente 15              
                    if (mousePos.x > 172
                        && mousePos.x < 220) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,172,211);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,172,211);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 14             
                    if (mousePos.x > 225
                        && mousePos.x < 273) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,225,211);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,225,211);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }
                
                //Ausente 13              
                    if (mousePos.x > 278
                        && mousePos.x < 326) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,278,210);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,278,210);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }                

                //Ausente 12              
                    if (mousePos.x > 328
                        && mousePos.x < 375) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,328,210);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,328,210);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 11             
                    if (mousePos.x > 380
                        && mousePos.x < 428) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,380,211);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,380,211);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 21             
                    if (mousePos.x > 505
                        && mousePos.x < 553) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,505,209);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,505,209);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }


                //Ausente 22             
                    if (mousePos.x > 558
                        && mousePos.x < 606) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,558,209);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,558,209);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }


                //Ausente 23             
                    if (mousePos.x > 613
                        && mousePos.x < 661) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,613,209);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,613,209);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 24             
                    if (mousePos.x > 666
                        && mousePos.x < 714) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,666,209);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,666,209);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }


                //Ausente 25           
                    if (mousePos.x > 719
                        && mousePos.x < 767) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,719,209);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,719,209);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 26             
                    if (mousePos.x > 772
                        && mousePos.x < 820) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,772,209);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,772,209);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 27             
                    if (mousePos.x > 827
                        && mousePos.x < 875) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,827,209);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,827,209);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }



                //Ausente 28             
                    if (mousePos.x > 880
                        && mousePos.x < 928) {
                        if (mousePos.y > 215
                        && mousePos.y < 255) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,880,209);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,880,209);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }



             

                /*/*///*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/

                //Ausente 48
                    if (mousePos.x > 9
                        && mousePos.x < 57) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {                          
                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,8,281);
                                click();                            
                                document.getElementById("chkAusente").checked = false;
                            }
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,8,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }
                        }
                    }              

                //Ausente 47               
                    if (mousePos.x > 66
                        && mousePos.x < 112) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,63,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,63,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }
              
                //Ausente 46             
                    if (mousePos.x > 118
                        && mousePos.x < 165) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,118,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,118,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }
          
                //Ausente 45              
                    if (mousePos.x > 172
                        && mousePos.x < 220) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,172,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,172,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 44             
                    if (mousePos.x > 225
                        && mousePos.x < 273) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,225,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,225,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }
                
                //Ausente 43             
                    if (mousePos.x > 278
                        && mousePos.x < 326) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,277,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,277,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }                

                //Ausente 42             
                    if (mousePos.x > 328
                        && mousePos.x < 375) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,328,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,328,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 41            
                    if (mousePos.x > 380
                        && mousePos.x < 428) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,380,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,380,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }




                //Ausente 31             
                    if (mousePos.x > 505
                        && mousePos.x < 553) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,504,280);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,504,280);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }


                //Ausente 32             
                    if (mousePos.x > 558
                        && mousePos.x < 606) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,558,280);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,558,280);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }


                //Ausente 33             
                    if (mousePos.x > 613
                        && mousePos.x < 661) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,614,280);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,614,280);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 34             
                    if (mousePos.x > 666
                        && mousePos.x < 714) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,666,280);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,666,280);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }


                //Ausente 35           
                    if (mousePos.x > 719
                        && mousePos.x < 767) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,720,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,720,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 36             
                    if (mousePos.x > 772
                        && mousePos.x < 820) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,775,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,775,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

                //Ausente 37             
                    if (mousePos.x > 827
                        && mousePos.x < 875) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,830,281);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,830,281);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }



                //Ausente 38             
                    if (mousePos.x > 880
                        && mousePos.x < 928) {
                        if (mousePos.y > 281
                        && mousePos.y < 324) {

                            if (bAusente) {
                                ctx.drawImage(imgPiezaAusente,883,280);
                                click();
                                document.getElementById("chkAusente").checked = false;
                            }
                           
                            if (bNoAusente) {                              
                                ctx.drawImage(imgPiezaSana,883,280);
                                click();
                                document.getElementById("chkNoAusente").checked = false;
                            }                            
                        }
                    }

































































                //************Caries y Curaciones********************************//

                //cuadrante arriba 18
                    if (mousePos.x > 25
                    && mousePos.x < 43) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(25, 215, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(25, 215, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(25, 215, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 17
                    if (mousePos.x > 80
                    && mousePos.x < 97) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(81, 215, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(81, 215, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(81, 215, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 16
                    if (mousePos.x > 134
                    && mousePos.x < 150) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(134, 215, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(134, 215, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(134, 215, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 15
                    if (mousePos.x > 188
                    && mousePos.x < 204) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(188, 215, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(188, 215, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(188, 215, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 14
                    if (mousePos.x > 242
                    && mousePos.x < 258) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(242, 214, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(242, 214, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(242, 214, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 13
                    if (mousePos.x > 295
                    && mousePos.x < 312) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(295, 213, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(295, 213, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(295, 213, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 12
                    if (mousePos.x > 344
                    && mousePos.x < 360) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(344, 213, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(344, 213, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(344, 213, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 11
                    if (mousePos.x > 397
                    && mousePos.x < 413) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(397, 214, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(397, 214, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(397, 214, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }





                //cuadrante arriba 21
                    if (mousePos.x > 520
                    && mousePos.x < 520 +14) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(520, 211, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(520, 211, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(520, 211, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 22
                    if (mousePos.x > 573
                    && mousePos.x < 573 +14 ) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(573, 211, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(573, 211, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(573, 211, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 23
                    if (mousePos.x > 628
                    && mousePos.x < 628 + 14) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(628, 211, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(628, 211, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(628, 211, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 24
                    if (mousePos.x > 682
                    && mousePos.x < 682 +14) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(682, 211, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(682, 211, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(682, 211, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 25
                    if (mousePos.x > 735
                    && mousePos.x < 735 + 14) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(735, 211, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(735, 211, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(735, 211, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 26
                    if (mousePos.x > 788
                    && mousePos.x < 788 + 14) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(788, 211, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(788, 211, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(788, 211, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 27
                    if (mousePos.x > 843
                    && mousePos.x < 843 + 14) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(843, 211, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(843, 211, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(843, 211, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 28
                    if (mousePos.x > 895
                    && mousePos.x < 895 + 14) {
                        if (mousePos.y > 217
                             && mousePos.y < 227) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(895, 211, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(895, 211, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(895, 211, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }





                //cuadrante arriba 48
                    if (mousePos.x > 25
                    && mousePos.x < 43) {
                        if (mousePos.y > 283
                             && mousePos.y < 283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(25, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(25, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(25, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 47
                    if (mousePos.x > 80
                    && mousePos.x < 97) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(81, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(81, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(81, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 46
                    if (mousePos.x > 134
                    && mousePos.x < 150) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(134, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(134, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(134, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 45
                    if (mousePos.x > 188
                    && mousePos.x < 204) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(188, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(188, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(188, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 44
                    if (mousePos.x > 242
                    && mousePos.x < 258) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(242, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(242, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(242, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 43
                    if (mousePos.x > 295
                    && mousePos.x < 312) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(295, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(295, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(295, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 42
                    if (mousePos.x > 344
                    && mousePos.x < 360) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(344, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(344, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(344, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 41
                    if (mousePos.x > 397
                    && mousePos.x < 413) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(397, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(397, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(397, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }





                //cuadrante arriba 31
                    if (mousePos.x > 520
                    && mousePos.x < 520 +14) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(520, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(520, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(520, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 32
                    if (mousePos.x > 573
                    && mousePos.x < 573 +14 ) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(573, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(573, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(573, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 33
                    if (mousePos.x > 628
                    && mousePos.x < 628 + 14) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(628, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(628, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(628, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 34
                    if (mousePos.x > 682
                    && mousePos.x < 682 +14) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(682, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(682, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(682, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 35
                    if (mousePos.x > 735
                    && mousePos.x < 735 + 14) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(735, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(735, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(735, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 36
                    if (mousePos.x > 788
                    && mousePos.x < 788 + 14) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(788, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(788, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(788, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }

                //cuadrante arriba 37
                    if (mousePos.x > 843
                    && mousePos.x < 843 + 14) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(843, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(843, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(843, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }


                //cuadrante arriba 38
                    if (mousePos.x > 895
                    && mousePos.x < 895 + 14) {
                        if (mousePos.y > 283
                             && mousePos.y <  283 +11) {
                            var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                            if (v_rgba == "250,250,250,255") {   
                                dibujar(895, 283, 15, 10, 'rgba(255, 0, 0, 255)');
                            }
                            else if (v_rgba == "255,0,0,255") {  
                                dibujar(895, 283, 15, 10, 'rgba(0,0,255,255)');
                            }
                            else if (v_rgba == "0,0,255,255") { 
                                dibujar(895, 283, 15, 10, 'rgba(250,250,250,255)');
                            }                        
                        }                   
                    }










                   
              //cuadrante centro 18
                if (mousePos.x > 25
                && mousePos.x < 43) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {
                            dibujar(25, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {
                            dibujar(25, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") {
                            dibujar(25, 227, 15, 10, 'rgba(250,250,250,255)');
                        }
                    }
                }                         


                //cuadrante centro 17
                if (mousePos.x > 80
                && mousePos.x < 97) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(81, 226, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(81, 226, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(81, 226, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 16
                if (mousePos.x > 134
                && mousePos.x < 150) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(134, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(134, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(134, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 15
                if (mousePos.x > 188
                && mousePos.x < 204) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(188, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(188, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(188, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 14
                if (mousePos.x > 242
                && mousePos.x < 258) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(242, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(242, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(242, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 13
                if (mousePos.x > 295
                && mousePos.x < 312) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(295, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(295, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(295, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 12
                if (mousePos.x > 344
                && mousePos.x < 360) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(344, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(344, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(344, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 11
                if (mousePos.x > 397
                && mousePos.x < 413) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(397, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(397, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(397, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante centro 21
                if (mousePos.x > 520
                && mousePos.x < 520 +14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(520, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(520, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(520, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 22
                if (mousePos.x > 573
                && mousePos.x < 573 +14 ) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(573, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(573, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(573, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 23
                if (mousePos.x > 628
                && mousePos.x < 628 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(628, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(628, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(628, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 24
                if (mousePos.x > 682
                && mousePos.x < 682 +14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(682, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(682, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(682, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 25
                if (mousePos.x > 735
                && mousePos.x < 735 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(735, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(735, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(735, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 26
                if (mousePos.x > 788
                && mousePos.x < 788 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(788, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(788, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(788, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 27
                if (mousePos.x > 843
                && mousePos.x < 843 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(843, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(843, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(843, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 28
                if (mousePos.x > 895
                && mousePos.x < 895 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(895, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(895, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(895, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante centro 48
                if (mousePos.x > 25
                && mousePos.x < 43) {
                    if (mousePos.y > 295
                         && mousePos.y < 295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(25, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(25, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(25, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 47
                if (mousePos.x > 80
                && mousePos.x < 97) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(81, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(81, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(81, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 46
                if (mousePos.x > 134
                && mousePos.x < 150) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(134, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(134, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(134, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 45
                if (mousePos.x > 188
                && mousePos.x < 204) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(188, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(188, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(188, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 44
                if (mousePos.x > 242
                && mousePos.x < 258) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11 ) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(242, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(242, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(242, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 43
                if (mousePos.x > 295
                && mousePos.x < 312) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(295, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(295, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(295, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 42
                if (mousePos.x > 344
                && mousePos.x < 360) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11 ) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(344, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(344, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(344, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 41
                if (mousePos.x > 397
                && mousePos.x < 413) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(397, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(397, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(397, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante centro 31
                if (mousePos.x > 520
                && mousePos.x < 520 +14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(520, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(520, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(520, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 32
                if (mousePos.x > 573
                && mousePos.x < 573 +14 ) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(573, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(573, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(573, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 33
                if (mousePos.x > 628
                && mousePos.x < 628 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(628, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(628, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(628, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 34
                if (mousePos.x > 682
                && mousePos.x < 682 +14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(682, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(682, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(682, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 35
                if (mousePos.x > 735
                && mousePos.x < 735 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(735, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(735, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(735, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 36
                if (mousePos.x > 788
                && mousePos.x < 788 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(788, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(788, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(788, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 37
                if (mousePos.x > 843
                && mousePos.x < 843 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(843, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(843, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(843, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 38
                if (mousePos.x > 895
                && mousePos.x < 895 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(895, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(895, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(895, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

























































                //cuadrante abajo
                if (mousePos.x > 25
               && mousePos.x < 43) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {
                            dibujar(25, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {
                            dibujar(25, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") {
                            dibujar(25, 240, 15, 10, 'rgba(250,250,250,255)');
                        }
                    }
                }

                //cuadrante abajo 17
                if (mousePos.x > 80
                && mousePos.x < 97) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(81, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(81, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(81, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 16
                if (mousePos.x > 134
                && mousePos.x < 150) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(134, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(134, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(134, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 15
                if (mousePos.x > 188
                && mousePos.x < 204) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(188, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(188, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(188, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 14
                if (mousePos.x > 242
                && mousePos.x < 258) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(242, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(242, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(242, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 13
                if (mousePos.x > 295
                && mousePos.x < 312) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(295, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(295, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(295, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 12
                if (mousePos.x > 344
                && mousePos.x < 360) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(344, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(344, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(344, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 11
                if (mousePos.x > 397
                && mousePos.x < 413) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(397, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(397, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(397, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante abajo 21
                if (mousePos.x > 520
                && mousePos.x < 520 +14) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(520, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(520, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(520, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 22
                if (mousePos.x > 573
                && mousePos.x < 573 +14 ) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(573, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(573, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(573, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }
                

                //cuadrante abajo 23
                if (mousePos.x > 628
                && mousePos.x < 628 + 14) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(628, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(628, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(628, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 24
                if (mousePos.x > 682
                && mousePos.x < 682 +14) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(682, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(682, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(682, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 25
                if (mousePos.x > 735
                && mousePos.x < 735 + 14) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(735, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(735, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(735, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 26
                if (mousePos.x > 788
                && mousePos.x < 788 + 14) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(788, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(788, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(788, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 27
                if (mousePos.x > 843
                && mousePos.x < 843 + 14) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(843, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(843, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(843, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 28
                if (mousePos.x > 895
                && mousePos.x < 895 + 14) {
                    if (mousePos.y > 240
                         && mousePos.y < 240 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(895, 240, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(895, 240, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(895, 240, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante abajo 48
                if (mousePos.x > 25
                && mousePos.x < 43) {
                    if (mousePos.y > 310
                         && mousePos.y < 310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(25, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(25, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(25, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 47
                if (mousePos.x > 80
                && mousePos.x < 97) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(81, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(81, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(81, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 46
                if (mousePos.x > 134
                && mousePos.x < 150) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(134, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(134, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(134, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 45
                if (mousePos.x > 188
                && mousePos.x < 204) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(188, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(188, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(188, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 44
                if (mousePos.x > 242
                && mousePos.x < 258) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11 ) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(242, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(242, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(242, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 43
                if (mousePos.x > 295
                && mousePos.x < 310) {
                    if (mousePos.y > 310
                         && mousePos.y < 310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                      
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(295, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(295, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(295, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 42
                if (mousePos.x > 344
                && mousePos.x < 360) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11 ) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(344, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(344, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(344, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 41
                if (mousePos.x > 397
                && mousePos.x < 413) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(397, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(397, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(397, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante abajo 31
                if (mousePos.x > 520
                && mousePos.x < 520 +14) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(520, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(520, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(520, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 32
                if (mousePos.x > 573
                && mousePos.x < 573 +14 ) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(573, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(573, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(573, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 33
                if (mousePos.x > 628
                && mousePos.x < 628 + 14) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(628, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(628, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(628, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 34
                if (mousePos.x > 682
                && mousePos.x < 682 +14) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(682, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(682, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(682, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 35
                if (mousePos.x > 735
                && mousePos.x < 735 + 14) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(735, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(735, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(735, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 36
                if (mousePos.x > 788
                && mousePos.x < 788 + 14) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(788, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(788, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(788, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante abajo 37
                if (mousePos.x > 843
                && mousePos.x < 843 + 14) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(843, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(843, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(843, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante abajo 38
                if (mousePos.x > 895
                && mousePos.x < 895 + 14) {
                    if (mousePos.y > 310
                         && mousePos.y <  310 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(895, 310, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(895, 310, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(895, 310, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }




















                //cuadrante izquierda
                if (mousePos.x > 12
               && mousePos.x < 20) {
                    if (mousePos.y > 227
                         && mousePos.y < 242) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {
                            dibujar(12, 227, 10, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {
                            dibujar(12, 227, 10, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") {
                            dibujar(12, 227, 10, 10, 'rgba(250,250,250,255)');
                        }
                    }
                }
                //cuadrante izquierda 17
                if (mousePos.x > 67
                && mousePos.x < 67 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(67, 226, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(67, 226, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(67, 226, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 16
                if (mousePos.x > 120
                && mousePos.x < 120+ 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(120, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(120, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(120, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 15
                if (mousePos.x > 173
                && mousePos.x < 173+ 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(173, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(173, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(173, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 14
                if (mousePos.x > 225
                && mousePos.x < 255+ 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(225, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(225, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(225, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 13
                if (mousePos.x > 278
                && mousePos.x < 278+ 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(278, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(278, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(278, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 12
                if (mousePos.x > 329
                && mousePos.x < 329+ 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(329, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(329, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(329, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 11
                if (mousePos.x > 382
                && mousePos.x < 382+ 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(382, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(382, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(382, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante izquierda 21
                if (mousePos.x > 506
                && mousePos.x < 506 +14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(506, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(506, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(506, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 22
                if (mousePos.x > 560
                && mousePos.x < 560 +14 ) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(560, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(560, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(560, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 23
                if (mousePos.x > 615
                && mousePos.x < 615 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(615, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(615, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(615, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 24
                if (mousePos.x > 668
                && mousePos.x < 668 +14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(668, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(668, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(668, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 25
                if (mousePos.x > 721
                && mousePos.x < 721 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(721, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(721, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(721, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 26
                if (mousePos.x > 773
                && mousePos.x < 773 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(773, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(773, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(773, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 27
                if (mousePos.x > 829
                && mousePos.x < 829 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(829, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(829, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(829, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 28
                if (mousePos.x > 881
                && mousePos.x < 881 + 14) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(881, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(881, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(881, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante izquierda 48
                if (mousePos.x > 12
                && mousePos.x < 12 +14) {
                    if (mousePos.y > 295
                         && mousePos.y < 295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(12, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(12, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(12, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 47
                if (mousePos.x > 67
                && mousePos.x < 67+14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(67, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(67, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(67, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 46
                if (mousePos.x > 120
                && mousePos.x < 120+14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(120, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(120, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(120, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 45
                if (mousePos.x > 173
                && mousePos.x < 173+14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(173, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(173, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(173, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 44
                if (mousePos.x > 225
                && mousePos.x < 225+14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11 ) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(225, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(225, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(225, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 43
                if (mousePos.x > 278
                && mousePos.x < 278+14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(278, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(278, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(278, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 42
                if (mousePos.x > 329
                && mousePos.x < 329+14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11 ) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(329, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(329, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(329, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 41
                if (mousePos.x > 382
                && mousePos.x < 382+14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(382, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(382, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(382, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante izquierda 31
                if (mousePos.x > 506
                && mousePos.x < 506 +14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(506, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(506, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(506, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 32
                if (mousePos.x > 561
                && mousePos.x < 561 +14 ) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(561, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(561, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(561, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 33
                if (mousePos.x > 616
                && mousePos.x < 616 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(616, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(616, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(616, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 34
                if (mousePos.x > 666
                && mousePos.x < 666 +14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(666, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(666, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(666, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 35
                if (mousePos.x > 722
                && mousePos.x < 722 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(722, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(722, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(722, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 36
                if (mousePos.x > 777
                && mousePos.x < 777 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(777, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(777, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(777, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante izquierda 37
                if (mousePos.x > 830
                && mousePos.x < 830 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(830, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(830, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(830, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante izquierda 38
                if (mousePos.x > 881
                && mousePos.x < 881 + 14) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(881, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(881, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(881, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

























































                //cuadrante derecha
               // if (mousePos.x > 42
               //&& mousePos.x < 55) {
               //     if (mousePos.y > 227
               //          && mousePos.y < 242) {
               //         var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
               //         if (v_rgba == "250,250,250,255") {
               //             dibujar(42, 227, 10, 10, 'rgba(255, 0, 0, 255)');
               //         }
               //         else if (v_rgba == "255,0,0,255") {
               //             dibujar(42, 227, 10, 10, 'rgba(0,0,255,255)');
               //         }
               //         else if (v_rgba == "0,0,255,255") {
               //             dibujar(42, 227, 10, 10, 'rgba(250,250,250,255)');
               //         }
               //     }
               // }

             

                //cuadrante centro 18
                if (mousePos.x > 25 +17
                && mousePos.x < 43 + 12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {
                            dibujar(25+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {
                            dibujar(25+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") {
                            dibujar(25+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }
                    }
                }                         


                //cuadrante centro 17
                if (mousePos.x > 80+17
                && mousePos.x < 97+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(81+17, 226, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(81+17, 226, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(8+171, 226, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 16
                if (mousePos.x > 134+17
                && mousePos.x < 150 +12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(134+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(134+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(134+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 15
                if (mousePos.x > 188 +17
                && mousePos.x < 204 +12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(188+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(188+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(188+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 14
                if (mousePos.x > 242+17
                && mousePos.x < 258+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(242+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(242+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(242+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 13
                if (mousePos.x > 295+17
                && mousePos.x < 312+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(295+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(295+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(295+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 12
                if (mousePos.x > 344+17
                && mousePos.x < 360+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(344+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(344+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(344+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 11
                if (mousePos.x > 397+17
                && mousePos.x < 413+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(397+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(397+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(397+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante centro 21
                if (mousePos.x > 520+17
                && mousePos.x < 520 +14+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(520+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(520+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(520+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 22
                if (mousePos.x > 573+17
                && mousePos.x < 573 +14+12 ) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(573+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(573+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(573+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 23
                if (mousePos.x > 628+17
                && mousePos.x < 628 + 14+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(628+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(628+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(628+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 24
                if (mousePos.x > 682+17
                && mousePos.x < 682 +14+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(682+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(682+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(682+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 25
                if (mousePos.x > 735+17
                && mousePos.x < 735 + 14+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(735+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(735+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(735+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 26
                if (mousePos.x > 788+17
                && mousePos.x < 788 + 14+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(788+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(788+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(788+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 27
                if (mousePos.x > 843+17
                && mousePos.x < 843 + 14+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(843+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(843+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(843+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 28
                if (mousePos.x > 895+17
                && mousePos.x < 895 + 14+12) {
                    if (mousePos.y > 227
                         && mousePos.y < 227 + 11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(895+17, 227, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(895+17, 227, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(895+17, 227, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante centro 48
                if (mousePos.x > 25+17
                && mousePos.x < 43+12) {
                    if (mousePos.y > 295
                         && mousePos.y < 295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(25+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(25+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(25+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 47
                if (mousePos.x > 80+17
                && mousePos.x < 97+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(81+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(81+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(81+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 46
                if (mousePos.x > 134+17
                && mousePos.x < 150+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(134+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(134+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(134+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 45
                if (mousePos.x > 188+17
                && mousePos.x < 204+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(188+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(188+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(188+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 44
                if (mousePos.x > 242+17
                && mousePos.x < 258+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11 ) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(242+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(242+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(242+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 43
                if (mousePos.x > 295+17
                && mousePos.x < 312+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(295+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(295+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(295+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 42
                if (mousePos.x > 344+17
                && mousePos.x < 360+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11 ) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(344+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(344+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(344+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 41
                if (mousePos.x > 397+17
                && mousePos.x < 413+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(397+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(397+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(397+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





                //cuadrante centro 31
                if (mousePos.x > 520+17
                && mousePos.x < 520 +14+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(520+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(520+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(520+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 32
                if (mousePos.x > 573+17
                && mousePos.x < 573 +14 +12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(573+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(573+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(573+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 33
                if (mousePos.x > 628+17
                && mousePos.x < 628 + 14+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(628+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(628+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(628+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 34
                if (mousePos.x > 682+17
                && mousePos.x < 682 +14+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(682+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(682+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(682+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 35
                if (mousePos.x > 735+17
                && mousePos.x < 735 + 14+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(735+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(735+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(735+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 36
                if (mousePos.x > 788+17
                && mousePos.x < 788 + 14+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(788+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(788+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(788+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }

                //cuadrante centro 37
                if (mousePos.x > 843+17
                && mousePos.x < 843 + 14 +12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(843+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(843+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(843+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }


                //cuadrante centro 38
                if (mousePos.x > 895+17
                && mousePos.x < 895 + 14+12) {
                    if (mousePos.y > 295
                         && mousePos.y <  295 +11) {
                        var v_rgba = (ctx.getImageData(mousePos.x, mousePos.y, 1, 1).data);
                        if (v_rgba == "250,250,250,255") {   
                            dibujar(895+17, 295, 15, 10, 'rgba(255, 0, 0, 255)');
                        }
                        else if (v_rgba == "255,0,0,255") {  
                            dibujar(895+17, 295, 15, 10, 'rgba(0,0,255,255)');
                        }
                        else if (v_rgba == "0,0,255,255") { 
                            dibujar(895+17, 295, 15, 10, 'rgba(250,250,250,255)');
                        }                        
                    }                   
                }





            }
            , false);
        }

        var canvas = document.getElementById("lienzo");
        if (canvas && canvas.getContext) {
            var ctx = canvas.getContext("2d");
            if (ctx) {
                var output = document.getElementById("output");

                canvas.addEventListener("mousemove", function (evt) {
                    var mousePos = oMousePos(canvas, evt);
                    marcarCoords(output, mousePos.x, mousePos.y)
                }, false);

                canvas.addEventListener("mouseout", function (evt) {
                    limpiarCoords(output);
                }, false);
            }
        }

        function marcarCoords(output, x, y) {
            output.innerHTML = ("x: " + x + ", y: " + y);
            output.style.top = (y + 10) + "px";
            output.style.left = (x + 10) + "px";
            output.style.backgroundColor = "#FFF";
            output.style.border = "1px solid #d9d9d9"
            canvas.style.cursor = "pointer";
        }

        function limpiarCoords(output) {
            output.innerHTML = "";
            output.style.top = 0 + "px";
            output.style.left = 0 + "px";
            output.style.backgroundColor = "transparent"
            output.style.border = "none";
            canvas.style.cursor = "default";
        }

        function oMousePos(canvas, evt) {
            var ClientRect = canvas.getBoundingClientRect();
            return { //objeto
                x: Math.round(evt.clientX - ClientRect.left),
                y: Math.round(evt.clientY - ClientRect.top)
            }
        }


        function GrabarImagen()
        {
            var highlightRowsServiceIdClientID = '<%= HiddenFieldServiceId.ClientID %>';
            var x = X(highlightRowsServiceIdClientID).getValue();
            var image = document.getElementById("lienzo").toDataURL("image/png");

            image = image.replace('data:image/png;base64,', '');
               
              
            $.ajax({

                type: 'POST',

                url: 'canvas1.aspx/UploadImage',

                data: '{ "imageData" : "' + image +'" , "xxx" : "' +x +'"}',

                contentType: 'application/json; charset=utf-8',

                dataType: 'json',
                success: function (msg) {

                    //alert('Image saved successfully !');

                }

            });
        }

        $(function () {

            $("#btnSave").click(function () {
                var highlightRowsServiceIdClientID = '<%= HiddenFieldServiceId.ClientID %>';
                var x = X(highlightRowsServiceIdClientID).getValue();
                var image = document.getElementById("lienzo").toDataURL("image/png");

                image = image.replace('data:image/png;base64,', '');
               
              
                $.ajax({

                    type: 'POST',

                    url: 'canvas1.aspx/UploadImage',

                    data: '{ "imageData" : "' + image +'" , "xxx" : "' +x +'"}',

                    contentType: 'application/json; charset=utf-8',

                    dataType: 'json',
                    success: function (msg) {

                        alert('Image saved successfully !');

                    }

                });

            });

        });
    </script>
</body>
</html>
