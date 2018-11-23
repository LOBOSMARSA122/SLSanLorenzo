<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEspirometria.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmEspirometria" %>
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
          width:22px
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <x:PageManager ID="PageManager1" runat="server"/>
         <x:Panel ID="Panel2" runat="server" Height="5000px" Width="1000px" ShowBorder="True"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Espirometría">
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
                        <x:Tab ID="TabEspirometria" BodyPadding="5px" Title="Espirometría" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarEspirometria" Text="Grabar Espirometría" Icon="SystemSave" runat="server" OnClick="btnGrabarEspirometria_Click" AjaxLoadingType="Mask"></x:Button>                                   
                                        <x:FileUpload runat="server" ID="fileDoc" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                        Label="Seleccionar Excel" ButtonIcon="SystemSearch" OnFileSelected="fileDoc_FileSelected" AutoPostBack="true" ButtonText="Subir Adjunto" Readonly="False">
                                        </x:FileUpload>
                                         <x:Button ID="btnDescargar" runat="server" Text="Descargar Adjuntos" Icon="ArrowDown"></x:Button>
                                         <%--<x:Label ID="Label746" runat="server" Text="....." ShowLabel="false"></x:Label>--%>
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server" Enabled="false"></x:DropDownList>
                                        <x:Button ID="btnReporteEspiro" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true" ></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel9" Title="PREGUNTAS PARA TODOS LOS CANDIDATOS  A ESPIROMETRÍA (RELACIONADAS A CRITERIOS DE EXCLUSIÓN)" EnableBackgroundColor="true" Height="240px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items> 
                                        <x:Panel ID="Panel10" Width="800px" Height="200px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="CUESTIONARIO">
                                            <Items>
                                                <x:TextBox ID="tLabel28" runat="server" Text="1. ¿TUVO DESPRENDIMIENTO DE LA RETINA O UNA OPERACIÓN (CIRUGÍA) DE LOS OJOS, TÓRAX O ABDOMEN, EN LOS ÚLTIMOS 3 MESES?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="tLabel38" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox1" runat="server" Text="2. ¿HA ESTADO HOSPITALIZADO (A) POR CUALQUIER OTRO PROBLEMA DEL CORAZÓN EN LOS ÚLTIMOS 3 MESES?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label1" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox2" runat="server" Text="3. ¿HA TENIDO ALGÚN ATAQUE CARDÍACO O INFARTO AL CORAZÓN EN LOS ÚLTIMOS 3 MESES?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label2" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox3" runat="server" Text="4. ¿ESTÁ USANDO MEDICAMENTOS PARA LA TUBERCULOSIS, EN ESTE MOMENTO?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label3" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox4" runat="server" Text="5. EN CASO DE SER MUJER: ¿ESTÁ USTED EMBARAZADA ACTUALMENTE?" Width="790" Enabled="False"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel11" Width="160px" EnableBackgroundColor="true" runat="server"
                                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="200px" Title="SI">
                                            <Items>
                                                <%--<x:DropDownList ID="ddlPre1" runat="server" Width="150" Enabled="False"></x:DropDownList>--%>  
                                                <x:CheckBox ID="chkPre1" runat="server" ShowLabel="true"></x:CheckBox> 
                                                <x:Label ID="Label62" runat="server" Text=" "></x:Label>                                             
                                                 <%--<x:DropDownList ID="ddlPre2" runat="server" Width="150" Enabled="False"></x:DropDownList>--%>  
                                                <x:CheckBox ID="chkPre2" runat="server" ShowLabel="true"></x:CheckBox>
                                                <x:Label ID="Label63" runat="server" Text=" "></x:Label>                                              
                                                <%--<x:DropDownList ID="ddlPre3" runat="server" Width="150" Enabled="False"></x:DropDownList>  --%>         
                                                <x:CheckBox ID="chkPre3" runat="server" ShowLabel="true"></x:CheckBox>
                                                <x:Label ID="Label64" runat="server" Text=" "></x:Label>                                                                        
                                                 <%--<x:DropDownList ID="ddlPre4" runat="server" Width="150" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chkPre4" runat="server" ShowLabel="true"></x:CheckBox>
                                                <x:Label ID="Label65" runat="server" Text=" "></x:Label>                                                 
                                                <%--<x:DropDownList ID="ddlPre5" runat="server" Width="150" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chkPre5" runat="server" ShowLabel="true"></x:CheckBox>
                                                <x:Label ID="Label4" runat="server" Text=" "></x:Label>                                                 
                                                <%--<x:DropDownList ID="ddlPre5" runat="server" Width="150" Enabled="False"></x:DropDownList>--%>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel1" Title="ANTECEDENTES MÉDICOS DE IMPORTANCIA" EnableBackgroundColor="true" Height="270px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items> 
                                         <x:Panel ID="Panel3" Width="350px" Height="230px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="ANTECEDENTES">
                                            <Items>
                                                <x:TextBox ID="TextBox5" runat="server" Text="HEMOPTISIS" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label15" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox6" runat="server" Text="PNEUMOTÓRAX" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label16" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox7" runat="server" Text="TRAQUEOSTOMÍA" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label17" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox8" runat="server" Text="SONDA PLEURAL" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label18" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox9" runat="server" Text="ANEURISMAS CEREBRAL, ABDOMEN, TÓRAX" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label19" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox10" runat="server" Text="EMBOLIA PULMONAR" Width="340px" Enabled="False"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel4" Width="130px" EnableBackgroundColor="true" runat="server"
                                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="230px" Title="SI">
                                            <Items>
                                                <%--<x:DropDownList ID="ddlhemoptisis" runat="server" Width="120" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chkhemoptisis" runat="server"></x:CheckBox>                                              
                                                <x:Label ID="Label7" runat="server" Text=" "></x:Label>
                                                <%--<x:DropDownList ID="ddlpneumotorax" runat="server" Width="120" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chkpneumotorax" runat="server"></x:CheckBox>                                                  
                                                <x:Label ID="Label8" runat="server" Text=" "></x:Label>
                                                <%--<x:DropDownList ID="ddltraqueostomia" runat="server" Width="120" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chktraqueostomia" runat="server"></x:CheckBox>                                                  
                                                <x:Label ID="Label9" runat="server" Text=" "></x:Label>
                                                <%--<x:DropDownList ID="ddlsonda_pleural" runat="server" Width="120" Enabled="False"></x:DropDownList>--%> 
                                                <x:CheckBox ID="chksonda_pleural" runat="server"></x:CheckBox>                                                 
                                                <x:Label ID="Label10" runat="server" Text=" "></x:Label>
                                                <%--<x:DropDownList ID="ddlaneurisma_cerebral" runat="server" Width="120" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chkaneurisma_cerebral" runat="server"></x:CheckBox>      
                                                <x:Label ID="Label20" runat="server" Text=" "></x:Label>
                                                <%--<x:DropDownList ID="ddlembolia_pulmonar" runat="server" Width="120" Enabled="False"></x:DropDownList>--%> 
                                                <x:CheckBox ID="chkembolia_pulmonar" runat="server"></x:CheckBox>                  
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel5" Width="350px" Height="230px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="ANTECEDENTES">
                                            <Items>
                                                <x:TextBox ID="TextBox11" runat="server" Text="INFARTO RECIENTE" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label21" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox12" runat="server" Text="INESTABILIDAD CV" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label22" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox13" runat="server" Text="FIEBRE, NÁUSEA VÓMITO" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label23" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox14" runat="server" Text="EMBARAZO AVANZADO" Width="340px" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label24" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox15" runat="server" Text="EMBARAZO COMPLICADO" Width="340px" Enabled="False"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel6" Width="130px" EnableBackgroundColor="true" runat="server"
                                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="230px" Title="SI">
                                            <Items>
                                                <%--<x:DropDownList ID="ddlinfarto_reciente" runat="server" Width="120" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chkinfarto_reciente" runat="server"></x:CheckBox>                                                
                                                <x:Label ID="Label11" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddlinestabilidad_cv" runat="server" Width="120" Enabled="False"></x:DropDownList>--%> 
                                                <x:CheckBox ID="chkinestabilidad_cv" runat="server"></x:CheckBox>                                               
                                                <x:Label ID="Label12" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddlfiebre_nausea" runat="server" Width="120" Enabled="False"></x:DropDownList>--%> 
                                                <x:CheckBox ID="chkfiebre_nausea" runat="server"></x:CheckBox>                                               
                                                <x:Label ID="Label13" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddlembarazo_avanzado" runat="server" Width="120" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chkembarazo_avanzado" runat="server"></x:CheckBox>                                                
                                                <x:Label ID="Label14" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddlembarazo_complicado" runat="server" Width="120" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chkembarazo_complicado" runat="server"></x:CheckBox>                   
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel7" Title="PREGUNTAS PARA TODOS LOS ENTREVISTADOS QUE NO TIENEN LOS CRITERIOS DE EXCLUSIÓN Y QUE POR LO TANTO DEBEN HACER LA ESPIROMETRÍA." EnableBackgroundColor="true" Height="340px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items> 
                                        <x:Panel ID="Panel8" Width="800px" Height="300px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="CUESTIONARIO">
                                            <Items>
                                                <x:TextBox ID="TextBox16" runat="server" Text="1. ¿TUVO UNA INFECCIÓN RESPIRATORIA (RESFRIADO), EN LAS ÚLTIMAS 3 SEMANAS?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label25" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox17" runat="server" Text="2.¿TUVO INFECCIÓN EN EL OÍDO EN LAS ÚLTIMAS 3 SEMANAS?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label26" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox18" runat="server" Text="3.¿USÓ AEROSOLES (SPRAYS INHALADOS) O NEBULIZACIONES CON BRONCODILATADORES, EN LAS ÚLTIMAS 3 HORAS?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label27" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox19" runat="server" Text="4. ¿HA USADO ALGÚN MEDICAMENTO BRONCODILATADOR DURANTE LAS ÚLTIMAS 8 HORAS?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label28" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox20" runat="server" Text="5.¿FUMÓ (CUALQUIER TIPO DE CIGARRO), EN LAS ÚLTIMAS DOS HORAS?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label33" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox21" runat="server" Text="6.¿REALIZÓ ALGÚN EJERCICIO FÍSICO FUERTE (COMO GIMNASIA, CAMINATA O TROTAR), EN LA ÚLTIMA HORA?" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label34" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox22" runat="server" Text="7. ¿COMIÓ EN LA ÚLTIMA HORA?" Width="790" Enabled="False"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel12" Width="160px" EnableBackgroundColor="true" runat="server"
                                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="300px" Title="SI">
                                            <Items>
                                                <%--<x:DropDownList ID="ddl1_tuvo_una_infeccion" runat="server" Width="150" Enabled="False"></x:DropDownList>--%>  
                                                <x:CheckBox ID="chk1_tuvo_una_infeccion" runat="server"></x:CheckBox>                                                 
                                                <x:Label ID="Label29" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddl2_tuvo_infeccion" runat="server" Width="150" Enabled="False"></x:DropDownList>--%> 
                                                <x:CheckBox ID="chk2_tuvo_infeccion" runat="server"></x:CheckBox>                                                  
                                                <x:Label ID="Label30" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddl3_uso_aerosoles" runat="server" Width="150" Enabled="False"></x:DropDownList>--%> 
                                                <x:CheckBox ID="chk3_uso_aerosoles" runat="server"></x:CheckBox>                                                  
                                                <x:Label ID="Label31" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddl4_ha_usado_algun_medicamento" runat="server" Width="150" Enabled="False"></x:DropDownList>--%>
                                                <x:CheckBox ID="chk4_ha_usado_algun_medicamento" runat="server"></x:CheckBox>                                                   
                                                <x:Label ID="Label32" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddl5_ha_fumado_cualquier" runat="server" Width="150" Enabled="False"></x:DropDownList>--%> 
                                                <x:CheckBox ID="chk5_ha_fumado_cualquier" runat="server"></x:CheckBox>       
                                                 <x:Label ID="Label60" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddl6_realizo_algun_ejercicio" runat="server" Width="150" Enabled="False"></x:DropDownList>--%> 
                                                <x:CheckBox ID="chk6_realizo_algun_ejercicio" runat="server"></x:CheckBox>    
                                                 <x:Label ID="Label61" runat="server" Text=" "></x:Label>
                                                 <%--<x:DropDownList ID="ddl7_ingirio_alimentos" runat="server" Width="150" Enabled="False"></x:DropDownList>--%>  
                                                <x:CheckBox ID="chk7_ingirio_alimentos" runat="server"></x:CheckBox>                 
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel13" Title="FUNCIÓN RESPIRATORIA ABS %" EnableBackgroundColor="true" Height="390px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items> 
                                        <x:Panel ID="Panel14" Width="200px" Height="350px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="FUNCIÓN">
                                            <Items>
                                                <x:TextBox ID="TextBox23" runat="server" Text="CVF" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label35" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox24" runat="server" Text="VEF (1)" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label36" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox25" runat="server" Text="VEF1 ÷ CVF" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label37" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox26" runat="server" Text="FET" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label38" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox27" runat="server" Text="FEF (25)(75)" Width="790" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label39" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox28" runat="server" Text="PEF)" Width="790" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label40" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox29" runat="server" Text="MEF " Width="790" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label42" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox30" runat="server" Text="R (50) " Width="790" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label43" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="TextBox31" runat="server" Text="MVV (IND)" Width="790" Enabled="False"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel15" Width="200px" Height="350px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="RESULTADO">
                                            <Items>
                                                <x:TextBox ID="txtcvf" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label44" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtvef_1" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label45" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtvef1_cvf" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label46" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtfet" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label47" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtfef_2575" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label48" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtpef" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label49" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtmef" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label50" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtr_50" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label51" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtmvv_ind" runat="server" Text="" Width="790" Enabled="False"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                          <x:Panel ID="Panel16" Width="560px" Height="350px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="DESCRIPCIÓN">
                                            <Items>
                                                <x:TextBox ID="txtdescripcion_cvf" runat="server" Text="" Width="550" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label52" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtdescripcion_vef_1" runat="server" Text="" Width="550" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label53" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtdescripcion_vef1_cvf" runat="server" Text="" Width="550" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label54" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtdescripcion_fet" runat="server" Text="" Width="550" Enabled="False"></x:TextBox>
                                                <x:Label ID="Label55" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtdescripcion_f_2575" runat="server" Text="" Width="550" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label56" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtdescripcion_pef" runat="server" Text="" Width="550" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label57" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtdescripcion_mef" runat="server" Text=" " Width="550" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label58" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtdescripcion_r_50" runat="server" Text="" Width="550" Enabled="False"></x:TextBox>
                                                 <x:Label ID="Label59" runat="server" Text=" "></x:Label>
                                                 <x:TextBox ID="txtdescripcion_mvvind" runat="server" Text="" Width="550" Enabled="False"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel51" Title="CONCLUSIONES" EnableBackgroundColor="true" Height="30px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>   
                                        <x:Form ID="Form23" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="200px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow60" ColumnWidths="250px 250px 250px" runat="server">
                                                    <Items>                                           
                                                        <x:CheckBox ID="chkespirometria_normal" runat="server" Text="" Label="ESPIROMETRÍA NORMAL"></x:CheckBox>
                                                        <x:CheckBox ID="chkpatron_obstructivo" runat="server" Text="" Label="PATRÓN OBSTRUCTIVO"></x:CheckBox>
                                                        <x:CheckBox ID="chkpatron_restrictivo" runat="server" Text="" Label="PATRÓN RESTRICTIVO"></x:CheckBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>    
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel762" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible="false">
                                    <Items>
                                        <x:Form ID="Form208" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow621" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtEspirometriaAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtEspirometriaAuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtEspirometriaAuditorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow622" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtEspirometriaEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtEspirometriaEvaluadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtEspirometriaEvaluadorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>       
                                                  <Ext:FormRow ID="FormRow1" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtEspirometriaInformador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtEspirometriaInformadorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtEspirometriaInformadorModificacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
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
            Target="Top"  IsModal="True" Width="450px" Height="370px" OnClose="Window2_Close" >
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
