<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmOftalmologia.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmOftalmologia" %>
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
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Oftalmología">
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
                        <x:Tab ID="TabOftalmo" BodyPadding="5px" Title="Oftalmología" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarOftalmologia" Text="Grabar Oftalmología" Icon="SystemSave" runat="server" OnClick="btnGrabarOftalmologia_Click" AjaxLoadingType="Mask"></x:Button>                                   
                                      
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <%--<x:Panel ID="panel1" Title="USO DE CORRECTORES" EnableBackgroundColor="true" Height="40px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left" >
                                            <Rows>
                                                 <x:FormRow ID="FormRow22" ColumnWidths="50px 150px 50px 150px 150px 410px " runat="server" >
                                                    <Items>
                                                        <x:Label ID="label30" runat="server" Text="SI" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkuso_de_correctores_si" runat="server" Text="" ShowLabel="false"></x:CheckBox>  

                                                         <x:Label ID="label1" runat="server" Text="NO" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkuso_de_correctores_no" runat="server" Text="" ShowLabel="false"></x:CheckBox>  

                                                         <x:Label ID="label2" runat="server" Text="ÚLTIMA REFRACCIÓN" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtultima_refraccion" runat="server" Text="" ShowLabel="false"></x:TextBox>                                                                                     
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>--%>
                                <x:Panel ID="panel3" Title="ANTECEDENTES LABORALES:" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left" >
                                            <Rows>
                                                <%--<x:FormRow ID="FormRow1" ColumnWidths="150px 50px  150px 50px  150px 50px  150px 50px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label3" runat="server" Text="DIABETES" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkdiabetes" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label4" runat="server" Text="HIPERTENSIÓN" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkhipertension" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label5" runat="server" Text="SUST QUÍMICAS " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chksust_quimicas" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label6" runat="server" Text="EXP A RADIACIÓN" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkexp_a_radiacion" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                    </Items>
                                                </x:FormRow>--%>
                                                <%--  <x:FormRow ID="FormRow3" ColumnWidths="960px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label11" runat="server" Text=". " ShowLabel="false"></x:Label>                                                      
                                                    </Items>
                                                </x:FormRow>--%>
                                                <%--<x:FormRow ID="FormRow2" ColumnWidths="150px 50px  150px 50px  150px 50px  150px 50px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label7" runat="server" Text="MIOPÍA " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkmiopia" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label8" runat="server" Text="CIRUGÍA OCULAR" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkcirugia_ocular" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label9" runat="server" Text="TRAUMA OCULAR" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chktrauma_ocular" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label10" runat="server" Text="GLAUCOMA" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkglaucoma" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                    </Items>
                                                </x:FormRow>--%>
                                                <%--<x:FormRow ID="FormRow4" ColumnWidths="960px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label12" runat="server" Text=". " ShowLabel="false"></x:Label>                                                      
                                                    </Items>
                                                </x:FormRow>--%>
                                                <x:FormRow ID="FormRow5" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <%--<x:Label ID="label13" runat="server" Text="ASTIGMATISMO" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkastigmatismo" runat="server" Text="" ShowLabel="false"></x:CheckBox>  --%>
                                                        <x:Label ID="label14" runat="server" Text="ANTECEDENTES LABORALES:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtotros_especificar" runat="server" Text="" Width="790px" Height="40px" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel4" Title="ANTECEDENTES PATOLÓGICOS:" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left" >
                                            <Rows>
                                               <%-- <x:FormRow ID="FormRow6" ColumnWidths="150px 50px  150px 50px  150px 50px  150px 50px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label15" runat="server" Text="PTOSIS PALPEBRAL" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkptosis_palpebral" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label16" runat="server" Text="CONJUNTIVITIS" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkconjuntivitis" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label17" runat="server" Text="PTERIGION" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkpterigium" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label18" runat="server" Text="ESTRABISMO" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkestrabismo" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                    </Items>
                                                </x:FormRow>--%>
                                               <%--  <x:FormRow ID="FormRow7" ColumnWidths="960px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label19" runat="server" Text=". " ShowLabel="false"></x:Label>                                                      
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow8" ColumnWidths="150px 50px  150px 50px  150px 50px  150px 50px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label20" runat="server" Text="TRANS DE LA CÓRNEA " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chktrans_de_cornea" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label21" runat="server" Text="CATARATAS" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkcataratas" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label22" runat="server" Text="CHALAZION " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkchalazion" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                         <x:Label ID="label23" runat="server" Text="SIN PATOLOGÍA" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chksin_patologias" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                    </Items>
                                                </x:FormRow>--%>
                                               <%-- <x:FormRow ID="FormRow9" ColumnWidths="960px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label24" runat="server" Text=". " ShowLabel="false"></x:Label>                                                      
                                                    </Items>
                                                </x:FormRow>--%>
                                                <x:FormRow ID="FormRow10" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>                                                     
                                                        <x:Label ID="label26" runat="server" Text="OTRAS PATOLOGÍAS" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtotras_patologia" runat="server" Text="" Width="790px" Height="40px"  ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form> 
                                    </Items>
                                </x:Panel>                             
                                <x:Panel ID="Panel22" Title="AGUDEZA VISUAL" EnableBackgroundColor="true" Height="125px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form79" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow260" ColumnWidths="240px 240px 240px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label289" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label290" runat="server" Text="-------------SIN CORREGIR------------" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label291" runat="server" Text="---------------CORREGIDA--------------" ShowLabel="false"></x:Label> 
                                                   <%-- <x:Label ID="Label292" runat="server" Text="------AGUJERO ESTENOPEICO------" ShowLabel="false"></x:Label> --%> 
                                                </Items>
                                          </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                        <x:Form ID="Form80" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow261" ColumnWidths="120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label293" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label297" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label294" runat="server" Text="DERECHO" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label295" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label296" runat="server" Text="DERECHO" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label298" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                   <%-- <x:Label ID="Label299" runat="server" Text="DERECHO" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label300" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label> --%>
                                                </Items>
                                          </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                    <x:Form ID="Form81" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow262" ColumnWidths="120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label301" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label302" runat="server" Text="VISIÓN DE CERCA" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="txtsc_cerca_ojo_derecho" runat="server" Width="80" ShowLabel="false"></x:DropDownList> 
                                                    <x:DropDownList ID="txtsc_cerca_ojo_izquierdo" runat="server" Width="80" ShowLabel="false"></x:DropDownList> 
                                                    <x:DropDownList ID="txtcc_cerca_ojo_derecho" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    <x:DropDownList ID="txtcc_cerca_ojo_izquierdo" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:TextBox ID="txtsc_cerca_ojo_derecho" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>--%> 
                                                    <%--<x:TextBox ID="txtsc_cerca_ojo_izquierdo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>--%>
                                                    <%--<x:TextBox ID="txtcc_cerca_ojo_derecho" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>--%> 
                                                    <%--<x:TextBox ID="txtcc_cerca_ojo_izquierdo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>--%>
                                                    <%--<x:TextBox ID="txtae_cerca_ojo_derecho" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                    <x:TextBox ID="txtae_cerca_izquierdo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> --%>
                                                </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                        <x:Form ID="Form83" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow264" ColumnWidths="120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label305" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                        <x:Form ID="Form82" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow263" ColumnWidths="120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label303" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label304" runat="server" Text="VISIÓN DE LEJOS" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="txtsc_lejos_ojo_derecho" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    <x:DropDownList ID="txtsc_lejos_ojo_izquierdo" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    <x:DropDownList ID="txtcc_lejos_ojo_derecho" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    <x:DropDownList ID="txtcc_lejos_ojo_izquierdo" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    <%--<x:TextBox ID="txtsc_lejos_ojo_derecho" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> --%>
                                                    <%--<x:TextBox ID="txtsc_lejos_ojo_izquierdo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>--%>
                                                    <%--<x:TextBox ID="txtcc_lejos_ojo_derecho" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> --%>
                                                    <%--<x:TextBox ID="txtcc_lejos_ojo_izquierdo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>--%>
                                                    <%--<x:TextBox ID="txtae_lejos_ojo_derecho" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                    <x:TextBox ID="txtae_lejos_ojo_izquierdo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> --%>
                                                </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel9" Title="VISIÓN CROMÁTICA Test de Ishihara" EnableBackgroundColor="true" Height="50px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true" Layout="Column">
                                    <Items>
                                        <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left" >
                                            <Rows>
                                                 <x:FormRow ID="FormRow11" ColumnWidths="150px 50px  150px 50px 100px 400px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label42" runat="server" Text="SIN ALTERACIÓN " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chknormal_ishihara" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                        <x:Label ID="label48" runat="server" Text="ALTERADO" ShowLabel="false"></x:Label>                                                        
                                                        <x:CheckBox ID="chkanormal_ishihara" runat="server" Text="" ShowLabel="false"></x:CheckBox> 
                                                        <x:Label ID="label49" runat="server" Text="DISCRIMINACIÓN:" ShowLabel="false"></x:Label>  
                                                        <x:DropDownList ID="ddldescripcion_ishihara" runat="server" Text="" Width="190px"  ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>                               
                                <x:Panel ID="panel10" Title="VISIÓN DE PROFUNDIDAD Test de Estereopsis" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true" Layout="Column">
                                    <Items>
                                         <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                                <%--                                                 <x:FormRow ID="FormRow13" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label53" runat="server" Text="TIEMPO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txttiempo_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label54" runat="server" Text="RECUPERACIÓN" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtrecuperacion_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>--%>
                                                 <x:FormRow ID="FormRow12" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <%--<x:Label ID="label50" runat="server" Text="SIN ALTERACIÓN " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkanormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                        <x:Label ID="label51" runat="server" Text="ALTERADO" ShowLabel="false"></x:Label>                                                        
                                                        <x:CheckBox ID="chknormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox> --%>
                                                        <x:Label ID="label52" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>  
                                                        <x:TextBox ID="txtencandilamiento_estereopsis" runat="server" Text="" Width="790px" Height="40px" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel1" Title="REFLEJOS PUPILARES" EnableBackgroundColor="true" Height="40px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true" Layout="Column">
                                    <Items>
                                         <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                                <%--                                                 <x:FormRow ID="FormRow13" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label53" runat="server" Text="TIEMPO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txttiempo_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label54" runat="server" Text="RECUPERACIÓN" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtrecuperacion_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>--%>
                                                 <x:FormRow ID="FormRow1" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <%--<x:Label ID="label50" runat="server" Text="SIN ALTERACIÓN " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkanormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                        <x:Label ID="label51" runat="server" Text="ALTERADO" ShowLabel="false"></x:Label>                                                        
                                                        <x:CheckBox ID="chknormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox> --%>
                                                        <x:Label ID="label1" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>  
                                                        <x:DropDownList ID="ddlReflejospupilares" runat="server" Width="180" ShowLabel="false"></x:DropDownList> 
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel14" Title="FUNDOSCOPÍA" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true" Layout="Column">
                                    <Items>
                                         <x:Form ID="Form16" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                                <%--                                                 <x:FormRow ID="FormRow13" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label53" runat="server" Text="TIEMPO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txttiempo_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label54" runat="server" Text="RECUPERACIÓN" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtrecuperacion_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>--%>
                                                 <x:FormRow ID="FormRow3" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <%--<x:Label ID="label50" runat="server" Text="SIN ALTERACIÓN " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkanormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                        <x:Label ID="label51" runat="server" Text="ALTERADO" ShowLabel="false"></x:Label>                                                        
                                                        <x:CheckBox ID="chknormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox> --%>
                                                        <x:Label ID="label4" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>  
                                                        <x:TextBox ID="txtcampimetria_oi" runat="server" Text="" Width="790px" Height="40px" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel15" Title="EXÁMENES AUXILIARES" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true" Layout="Column">
                                    <Items>
                                         <x:Form ID="Form17" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                                <%--                                                 <x:FormRow ID="FormRow13" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label53" runat="server" Text="TIEMPO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txttiempo_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label54" runat="server" Text="RECUPERACIÓN" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtrecuperacion_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>--%>
                                                 <x:FormRow ID="FormRow4" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <%--<x:Label ID="label50" runat="server" Text="SIN ALTERACIÓN " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkanormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                        <x:Label ID="label51" runat="server" Text="ALTERADO" ShowLabel="false"></x:Label>                                                        
                                                        <x:CheckBox ID="chknormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox> --%>
                                                        <x:Label ID="label5" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>  
                                                        <x:TextBox ID="txtultima_refraccion" runat="server" Text="" Width="790px" Height="40px" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel13" Title="PRESIÓN INTRAOCULAR" EnableBackgroundColor="true" Height="50px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true" Layout="Column">
                                    <Items>
                                         <x:Form ID="Form15" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                                 <x:FormRow ID="FormRow2" ColumnWidths="110px 75px 50px 110px 75px 50px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label2" runat="server" Text="OJO DERECHO" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txttonometria_od" runat="server" Text="" ShowLabel="false" Width="75"></x:TextBox>
                                                    <x:Label ID="Label12" runat="server" Text="mmHg" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label3" runat="server" Text="OJO IZQUIERDO" ShowLabel="false"></x:Label> 
                                                    <x:TextBox ID="txttonometria_oi" runat="server" Text="" ShowLabel="false" Width="75"></x:TextBox>
                                                    <x:Label ID="Label22" runat="server" Text="mmHg" ShowLabel="false"></x:Label>                                                     
                                                </Items> 
                                                </x:FormRow>                                                                                             
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel16" Title="OBSERVACIONES" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true" Layout="Column">
                                    <Items>
                                         <x:Form ID="Form18" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                                <%--                                                 <x:FormRow ID="FormRow13" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <x:Label ID="label53" runat="server" Text="TIEMPO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txttiempo_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label54" runat="server" Text="RECUPERACIÓN" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtrecuperacion_estereopsis" runat="server" Text="" Width="150px"  ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>--%>
                                                 <x:FormRow ID="FormRow6" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                        <%--<x:Label ID="label50" runat="server" Text="SIN ALTERACIÓN " ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkanormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox>  
                                                        <x:Label ID="label51" runat="server" Text="ALTERADO" ShowLabel="false"></x:Label>                                                        
                                                        <x:CheckBox ID="chknormal_estereopsis" runat="server" Text="" ShowLabel="false"></x:CheckBox> --%>
                                                        <x:Label ID="label6" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>  
                                                        <x:TextBox ID="txtrecuperacion_estereopsis" runat="server" Text="" Width="790px" Height="40px" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel17" Title="APTITUD" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true" Layout="Column">
                                    <Items>
                                         <x:Form ID="Form19" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                                <x:FormRow ID="FormRow7" ColumnWidths="80px 60px 100px 80px 60px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label7" runat="server" Text="APTO" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkuso_de_correctores_si" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="Label9" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label8" runat="server" Text="NO APTO" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkuso_de_correctores_no" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                          </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                
                                
                            </Items>
                        </x:Tab>                   
                        <x:Tab ID="TabOftalmologia_Internacional" BodyPadding="5px" Title="Oftalmología" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar10" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarOftalmologiaInternacional" Text="Grabar Oftalmología" Icon="SystemSave" runat="server" OnClick="btnGrabarOftalmologiaInternacional_Click" AjaxLoadingType="Mask"></x:Button>
                                          <x:Label ID="Label746" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporteOftalmoCI" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel20" Title="ANAMNESIS / ANTECEDENTES" EnableBackgroundColor="true" Height="70px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form75" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow237" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label265" runat="server" Text="ANAMNESIS:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtOftalmoAnamnesis_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow231" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label259" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow232" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label260" runat="server" Text="ANTECEDENTES:" ShowLabel="false"></x:Label>
                                                  
                                                        <x:TextBox ID="txtOftalmoAntecedentes_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow233" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label261" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel21" Title="" EnableBackgroundColor="true" Height="225px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="False" TableConfigColumns="3" Layout="Table">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="-------------OJO DERECHO--------------" ID="GroupPanel30" BoxFlex="1" Height="200" Width="250">
                                            <Items>
                                                <x:Form ID="Form76" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow238" ColumnWidths="140px 40px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label268" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label269" runat="server" Text="Normal" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                     
                                                        <x:FormRow ID="FormRow234" ColumnWidths="149px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label262" runat="server" Text="PARPADO" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkParpadoDerecho_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                     
                                                        <x:FormRow ID="FormRow235" ColumnWidths="150px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label263" runat="server" Text="CONJUNTIVA" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkConjuntivaDerecha_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                  
                                                        <x:FormRow ID="FormRow247" ColumnWidths="149px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label276" runat="server" Text="CÓRNEA" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkCorneaDerecha_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                     
                                                        <x:FormRow ID="FormRow249" ColumnWidths="150px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label278" runat="server" Text="IRIS" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkIrisDerecho_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                      
                                                        <x:FormRow ID="FormRow251" ColumnWidths="149px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label280" runat="server" Text="MOV. OCULAR" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkMovOcularDerecho_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="------------EXAMEN CLÌNICO EXTERNO / SEGMENTO ANTERIOR-----------" ID="GroupPanel31" BoxFlex="1" Height="200" Width="460">
                                            <Items>
                                                <x:Form ID="Form77" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow236" runat="server">
                                                            <Items>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="-------------OJO IZQUIERDO------------" ID="GroupPanel32" BoxFlex="1" Height="200" Width="250">
                                            <Items>
                                                <x:Form ID="Form78" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow239" ColumnWidths="140px 40px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label264" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label266" runat="server" Text="Normal" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                  
                                                        <x:FormRow ID="FormRow241" ColumnWidths="150px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label270" runat="server" Text="PARPADO" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkParpadoIzquierdo_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    
                                                        <x:FormRow ID="FormRow244" ColumnWidths="149px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label273" runat="server" Text="CONJUNTIVA" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkConjuntivaIzquierda_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        
                                                        <x:FormRow ID="FormRow254" ColumnWidths="150px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label283" runat="server" Text="CÓRNEA" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkCorneaIzquierda_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        
                                                        <x:FormRow ID="FormRow256" ColumnWidths="149px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label285" runat="server" Text="IRIS" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkIrisIzquierdo_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                      
                                                        <x:FormRow ID="FormRow258" ColumnWidths="150px 30px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label287" runat="server" Text="MOV. OCULAR" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkMovOcularIzquierdo_Internacional" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                       
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel11" Title="AGUDEZA VISUAL" EnableBackgroundColor="true" Height="125px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form6" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow14" ColumnWidths="240px 240px 240px 240px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label55" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label56" runat="server" Text="-------------SIN CORREGIR------------" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label57" runat="server" Text="---------------CORREGIDA--------------" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label58" runat="server" Text="------AGUJERO ESTENOPEICO------" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form7" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow15" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label59" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label60" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label61" runat="server" Text="DERECHO" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label62" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label63" runat="server" Text="DERECHO" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label64" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label65" runat="server" Text="DERECHO" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label66" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form10" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow16" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label67" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label68" runat="server" Text="VISIÓN DE LEJOS" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAELejosOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtAELejosOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtSinCorrectCercaOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtSinCorrectCercaOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        
                                                        <x:TextBox ID="txtConCorrectCercaOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtAECercaOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form12" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow17" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label69" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form13" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow18" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label70" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label71" runat="server" Text="VISIÓN DE CERCA" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtConCorrectLejosOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtConCorrectLejosOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtSinCorrectLejosOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtSinCorrectLejosOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtConCorrectCercaOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        <x:TextBox ID="txtAECercaOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                        
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel23" Title="TEST DE COLORES:" EnableBackgroundColor="true" Height="70px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form84" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow265" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label306" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTestColoresOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow266" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label307" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow267" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label308" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTestColoresOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow268" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label309" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel24" Title="TONOMETRÍA:" EnableBackgroundColor="true" Height="70px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form85" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow269" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label310" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTonometriaOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow270" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label311" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow271" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label312" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTonometriaOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow272" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label313" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel25" Title="ESTEREOPSIS:" EnableBackgroundColor="true" Height="100px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form86" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow273" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label314" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtEstereopsisOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow274" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label315" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow275" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label316" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtEstereopsisOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow276" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label317" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow277" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label318" runat="server" Text="TIEMPO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtEstereopsisTiempo_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow278" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label319" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel26" Title="TEST DE ENCANDILAMIENTO:" EnableBackgroundColor="true" Height="70px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form87" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow279" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label320" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTestEncandilamientoOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow280" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label321" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow281" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label322" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTestEncandilamientoOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow282" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label323" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel12" Title="FONDO DE OJO (FO):" EnableBackgroundColor="true" Height="70px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form88" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow283" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label324" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtFondoOjoOD_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow284" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label325" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow285" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label326" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtFondoOjoOI_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow286" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label327" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>

                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>

                                <%--TODO: Aqui va...--%>
                                <x:Panel ID="Panel53" Title="RECOMENDACIONES Y RESTRICCIONES" EnableBackgroundColor="true" Height="40px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form14" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="180px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow21" ColumnWidths="250px" runat="server">
                                                    <Items>
                                                        <x:CheckBox ID="chkEmetrope" runat="server" Label="EMÉTROPE"></x:CheckBox>                                                  
                                                    </Items>
                                                </x:FormRow>                                                    
                                            </Rows>
                                        </x:Form>
                                    </Items>

                                </x:Panel>

                                <x:Panel ID="Panel62" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form209" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow623" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOftalmologiaAuditoria" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOftalmologiaAuditoriaInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOftalmologiaAuditoriaModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow641" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOftalmologiaEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOftalmologiaEvaluadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOftalmologiaEvaluadorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>    
                                                 <Ext:FormRow ID="FormRow23" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOftalmologiaInformador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOftalmologiaInformadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOftalmologiaInformadorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>                                            
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Tab>
                        <x:Tab ID="TabOftalmoYanacocha" BodyPadding="5px" Title="Oftalmlogía" runat="server">
                             <Toolbars>
                                <x:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarYanacocha" Text="Grabar Oftalmología" Icon="SystemSave" runat="server" OnClick="btnGrabarYanacocha_Click" AjaxLoadingType="Mask"></x:Button> 
                                        <x:Label ID="Label1000" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label1001" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlOftalmoYanacocha" runat="server" Enabled="True"></x:DropDownList>
                                      <x:Button ID="btnReporteOftalmo" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel7" Title="AGUDEZA VISUAL" EnableBackgroundColor="true" Height="185px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form22" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow13" ColumnWidths="240px 240px 240px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label13" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label15" runat="server" Text="-------------SIN CORREGIR------------" ShowLabel="false" ></x:Label> 
                                                    <x:Label ID="Label16" runat="server" Text="---------------CORREGIDA--------------" ShowLabel="false"></x:Label> 
                                               </Items>
                                          </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                        <x:Form ID="Form23" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow22" ColumnWidths="120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label17" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label18" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label19" runat="server" Text="DERECHO" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label20" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label21" runat="server" Text="DERECHO" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label23" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                </Items>
                                          </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                        <x:Form ID="Form24" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow24" ColumnWidths="120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label24" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label25" runat="server" Text="VISIÓN DE CERCA" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlSCVCOD_Yana" runat="server" Width="80" ShowLabel="false" Enabled="false"></x:DropDownList> 
                                                    <x:DropDownList ID="ddlSCVCOI_Yana" runat="server" Width="80" ShowLabel="false" Enabled="false"></x:DropDownList> 
                                                    <x:DropDownList ID="ddlCCVCOD_Yana" runat="server" Width="80" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:DropDownList ID="ddlCCVCOI_Yana" runat="server" Width="80" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                 </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                        <x:Form ID="Form25" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow25" ColumnWidths="120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label27" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                        <x:Form ID="Form26" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow26" ColumnWidths="120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label28" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label29" runat="server" Text="VISIÓN DE LEJOS" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlSCVLOD_Yana" runat="server" Width="80" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:DropDownList ID="ddlSCVLOI_Yana" runat="server" Width="80" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:DropDownList ID="ddlCCVLOD_Yana" runat="server" Width="80" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:DropDownList ID="ddlCCVLOI_Yana" runat="server" Width="80" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                        <x:Form ID="Form27" Title=" " runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="true" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow28" ColumnWidths="140px 180px " runat="server" >
                                                <Items>
                                                    <x:Label ID="Label34" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label35" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                        <x:Form ID="Form20" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow8" ColumnWidths="140px 180px 140px 180px 140px 180px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label30" runat="server" Text="Enfermedades Oculares" ShowLabel="false"></x:Label>
                                                    <x:TextArea ID="txtEnfOculares_Yana" runat="server" Text="" Label="" ShowLabel="false" Height="50" Enabled="false"></x:TextArea>
                                                    <x:Label ID="Label10" runat="server" Text="Test de Ishihara" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlTestIshihara_Yana" runat="server" Width="110" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:Label ID="Label11" runat="server" Text="Reflejos Pupilares" ShowLabel="false"></x:Label>                                                    
                                                    <x:DropDownList ID="ddlReflejos_Yana" runat="server" Width="110" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel18" Title="FONDO DE OJO  (FUNDOSCOPIA)" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                         <x:Form ID="Form21" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow9" ColumnWidths="140px 180px 140px 180px 140px 180px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label31" runat="server" Text="MÁCULA OD " ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlMaculaOD_Yana" runat="server" Width="140" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:Label ID="Label32" runat="server" Text="NERVIO ÓPT OD " ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlNervioOD_Yana" runat="server" Width="140" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:Label ID="Label33" runat="server" Text="RETINA OD " ShowLabel="false"></x:Label>                                                    
                                                    <x:DropDownList ID="ddlRetinaOD_Yana" runat="server" Width="140" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                </Items>
                                          </x:FormRow>
                                           <x:FormRow ID="FormRow27" ColumnWidths="140px 180px 140px 180px 140px 180px" runat="server" >
                                                <Items>
                                                     <x:Label ID="Label46" runat="server" Text="MÁCULA OI " ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlMaculaOI_Yana" runat="server" Width="140" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:Label ID="Label47" runat="server" Text="NERVIO ÓPT OI " ShowLabel="false" ></x:Label>
                                                    <x:DropDownList ID="ddlNervioOI_Yana" runat="server" Width="140" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                    <x:Label ID="Label50" runat="server" Text="RETINA OI " ShowLabel="false"></x:Label>                                                    
                                                    <x:DropDownList ID="ddlRetinaOI_Yana" runat="server" Width="140" ShowLabel="false" Enabled="false"></x:DropDownList>
                                                </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="panel29" Title="PRESIÓN INTRAOCULAR" EnableBackgroundColor="true" Height="50px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                         <x:Form ID="Form33" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                                 <x:FormRow ID="FormRow32" ColumnWidths="140px 80px 40px 190px 190px 140px 80px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label37" runat="server" Text="OJO DERECHO" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtPIOD_Yana" runat="server" Text="" ShowLabel="false" Width="75" Enabled="false"></x:TextBox>
                                                    <x:Label ID="Label38" runat="server" Text="mmHg" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label36" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label43" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label39" runat="server" Text="OJO IZQUIERDO" ShowLabel="false"></x:Label> 
                                                    <x:TextBox ID="txtPIOI_Yana" runat="server" Text="" ShowLabel="false" Width="75" Enabled="false"></x:TextBox>
                                                    <x:Label ID="Label40" runat="server" Text="mmHg" ShowLabel="false"></x:Label>                                                     
                                                </Items> 
                                                </x:FormRow>                                                                                             
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                               <%-- <x:Panel ID="panel30" Title="OBSERVACIONES" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                         <x:Form ID="Form34" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left" >
                                            <Rows>
                                               <x:FormRow ID="FormRow33" ColumnWidths="150px 800px" runat="server" >
                                                    <Items>
                                                      <x:Label ID="label41" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>  
                                                        <x:TextBox ID="txtObsv_Yana" runat="server" Text="" Width="790px" Height="40px" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>--%>
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
