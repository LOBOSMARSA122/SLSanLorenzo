<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMedicina.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmMedicina" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/main.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">
        .highlight {
            background-color: lightgreen;
        }

            .highlight .x-grid3-col {
                background-image: none;
            }

        .x-grid3-row-selected .highlight {
            background-color: yellow;
        }

            .x-grid3-row-selected .highlight .x-grid3-col {
                background-image: none;
            }

		#imgCanvas{
			border: 1px solid  #000;
			box-shadow: 2px 2px 10px #333;
		}

		#botonera{
			margin-left: 20px;
			margin-top: 20px;
		}

		.botones{
			display: inline-block;
			height: 32px;
			width: 32px;
            transition: all 0.3s ease;
		}

        #btn1 {
            background-color: #fffc6d
        }

        #btn1:hover {
            transform:translateY(-8px)
        }

        #btn2 {
            background-color: #4286f4
        }#btn2:hover {
             transform:translateY(-8px)
        }

        #btn3 {
            background-color:#f44171
        }#btn3:hover {
            transform:translateY(-8px)
        }
        #btn4 {
            background-color: #25dd69
        }#btn4:hover {
            transform:translateY(-8px)
        }

		#rellenoFondo{
			background: #333;
			border: 2px solid #000;
			border-radius: 5px 5px 5px 5px;
			height: 50px;
			margin-bottom: 10px;
			width: 50px;			
		}

        #cargarDibujo:hover {
            transform:rotate(-360deg);
            transition: all 2s linear;
            cursor:pointer;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" runat="server" />
        <x:Panel ID="Panel2" runat="server" Height="5000px" Width="1000px" ShowBorder="True"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="Consultorio Medicina">
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
                                                                <x:DropDownList ID="ddlConsultorio" runat="server" Label="Consul." Width="240px" Enabled="false"></x:DropDownList>
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
                                <x:FormRow ID="FormRow206" ColumnWidths="700px 15px 250px" runat="server">
                                    <Items>
                                        <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" Height="240px"
                                            EnableRowNumber="True" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Mask"
                                            EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_ServiceId,v_IdTrabajador,v_Genero,i_AptitudeStatusId,i_EsoTypeId,v_ExploitedMineral,i_AltitudeWorkId,i_PlaceWorkId,v_Pacient,Dni,d_ServiceDate,v_TipoEso,v_Puesto,d_FechaNacimiento,EmpresaCliente,AreaEmpresa,v_SectorName"
                                            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" EnableCheckBoxSelect="false"
                                            OnRowClick="grdData_RowClick" EnableRowClick="true" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar23" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnCambiarFechaServicio" Text="Cambiar Fecha Servicio" Icon="CalendarViewDay" runat="server" Visible="false"></x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:CheckBoxField Width="30px" RenderAsStaticField="true" DataField="AtSchool" HeaderText="" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea llamar al paciente a consultorio?" Icon="sound" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="LLamar Paciente" CommandName="LlamarPaciente" />
                                                <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEditExaAdicional" HeaderText=""
                                                    Icon="LayoutEdit" ToolTip="Agregar exámenes" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_ServiceId" DataIFrameUrlFormatString="FRMEXAMENADICIONAL.aspx?v_ServiceId={0}"
                                                    DataWindowTitleField="v_Pacient" DataWindowTitleFormatString="Examen adicional al Trabajador: {0}" />

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
                                            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5">
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
                                        <x:TextBox ID="txtTipoExamenCabecera" ShowLabel="false" CssClass="mright" runat="server" Readonly="true"></x:TextBox>
                                        <x:Label ID="Label421" runat="server" Text="GÉNERO" ShowLabel="false"></x:Label>
                                        <x:TextBox ID="txtGeneroCabecera" ShowLabel="false" CssClass="mright" runat="server" Readonly="true"></x:TextBox>
                                        <x:Label ID="Label424" runat="server" Text="PUESTO" ShowLabel="false"></x:Label>
                                        <x:TextBox ID="txtPuestoCabecera" ShowLabel="false" CssClass="mright" runat="server"></x:TextBox>
                                        <x:Label ID="Label423" runat="server" Text="EDAD" ShowLabel="false"></x:Label>
                                        <x:TextBox ID="txtEdadCabecera" ShowLabel="false" CssClass="mright" runat="server" Readonly="true"></x:TextBox>
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
                                                    EnableTextSelection="true" EnableAlternateRowColor="true" BoxMargin="5" EnableRowClick="true" OnRowCommand="grdDx_RowCommand" OnRowClick="grdDx_RowClick" OnRowDataBound="grdDx_RowDataBound">
                                                    <Toolbars>
                                                        <x:Toolbar ID="Toolbar16" runat="server">
                                                            <Items>
                                                                <x:Button ID="btnNewDiagnosticos" Text="Nuevo Diagnóstico" Icon="Add" runat="server" Visible="false">
                                                                </x:Button>
                                                                <x:Button ID="btnNewDiagnosticosFrecuente" Text="Agregar Diagnóstico" Icon="Add" runat="server">
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
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5">
                                            <Items>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel53AA" runat="server" ShowBorder="true" Title="RECOMENDACIONES" Height="190" EnableBackgroundColor="false" TableColspan="1"
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="480">
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
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="20">
                                            <Items>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel55AA" runat="server" ShowBorder="true" Title="RESTRICCIONES" Height="190" EnableBackgroundColor="false" TableColspan="1"
                                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="460">
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
                                        <x:Button ID="btnGrabarAptitud" Text="Grabar Aptitud" Icon="SystemSave" runat="server" OnClick="btnGrabarAptitud_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Button ID="btnCertificadoAptitud" Text="Ver Certificado" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                        <x:Button ID="btnInformeCI" Text="Ver Informe" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
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
                        <x:Tab ID="TabAnexo312" BodyPadding="5px" Title="Anexo - 312" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <x:Button ID="btnGrabarAnexo312" Text="Grabar Anexo - 312" Icon="SystemSave" runat="server" OnClick="btnGrabarAnexo312_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:FileUpload runat="server" ID="fileDoc" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                            Label="Seleccionar Excel" ButtonIcon="SystemSearch" OnFileSelected="fileDoc_FileSelected" AutoPostBack="true" ButtonText="Subir Adjunto" Readonly="False">
                                        </x:FileUpload>
                                        <x:Button ID="btnDescargar" runat="server" Text="Descargar Adjuntos" Icon="ArrowDown"></x:Button>
                                        <x:Label ID="Label746" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="llll" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabar" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporte312" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel3" Title="DATOS DE LA EMPRESA" EnableBackgroundColor="true" Height="120px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow1" ColumnWidths="150px 630px 80px 100px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label1" runat="server" Text="RAZÓN SOCIAL" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtRazonSocial" ShowLabel="false" CssClass="mright" runat="server" Width="600" Readonly="true"></x:TextBox>
                                                        <x:Label ID="label2" runat="server" Text="RUC NRO:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtRucEmpresa" ShowLabel="false" CssClass="mright" runat="server" Width="100" Readonly="true"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow2" ColumnWidths="149px 810px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label3" runat="server" Text="ACTIVIDAD ECONÓMICA" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtActividadEconomica" ShowLabel="false" CssClass="mright" runat="server" Width="810" Readonly="true"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow3" ColumnWidths="150px 810px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label4" runat="server" Text="LUGAR DE TRABAJO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtLugarTrabajo" ShowLabel="false" CssClass="mright" runat="server" Width="810" Readonly="true"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow4" ColumnWidths="149px 810px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label5" runat="server" Text="PUESTO DE TRABAJO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtPuestoTrabajo" ShowLabel="false" CssClass="mright" runat="server" Width="810"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>

                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel1" Title="FILIACIÓN DEL TRABAJADOR" EnableBackgroundColor="true" Height="230px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow5" ColumnWidths="150px 810px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label6" runat="server" Text="NOMBRES Y APELLIDOS" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNombreCompletoTrabajador" ShowLabel="false" CssClass="mright" runat="server" Width="810" Readonly="true"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow6" ColumnWidths="149px 160px 120px 200px 170px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label7" runat="server" Text="FECHA NACIMIENTO" ShowLabel="false"></x:Label>
                                                        <x:DatePicker ID="dpcFechaNacimiento" Label="F.I" Width="100px" runat="server" DateFormatString="dd/MM/yyyy" ShowLabel="false" />
                                                        <x:Label ID="label8" runat="server" Text="TIPO DOCUMENTO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTipoDocumento" runat="server" Width="150px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label9" runat="server" Text="NÚMERO DE DOCUMENTO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroDocumento" ShowLabel="false" CssClass="mright" runat="server" Width="160"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow7" ColumnWidths="150px 160px 120px 200px 170px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label10" runat="server" Text="EDAD" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtEdad" ShowLabel="false" CssClass="mright" runat="server" Width="100" Readonly="true"></x:TextBox>
                                                        <x:Label ID="label11" runat="server" Text="TIPO EVALUACIÓN" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTipoEvaluacion" runat="server" Width="150px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label12" runat="server" Text="TIPO DE SEGURO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTipoSeguro" runat="server" Width="160px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow8" ColumnWidths="149px 480px 170px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label13" runat="server" Text="DIRECCIÓN FISCAL" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtDireccionFiscal" ShowLabel="false" CssClass="mright" runat="server" Width="430"></x:TextBox>
                                                        <x:Label ID="label59" runat="server" Text="LUGAR NACIMIENTO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtLugarNacimiento" ShowLabel="false" CssClass="mright" runat="server" Width="160"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow9" ColumnWidths="150px 160px 120px 200px 170px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label16" runat="server" Text="DEPARTAMENTO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlDepartamento" runat="server" Width="160px" ShowLabel="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged"></x:DropDownList>
                                                        <x:Label ID="label15" runat="server" Text="PROVINCIA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlProvincia" runat="server" Width="150px" ShowLabel="false" AutoPostBack="true" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"></x:DropDownList>
                                                        <x:Label ID="label14" runat="server" Text="DISTRITO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlDistrito" runat="server" Width="150px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow10" ColumnWidths="220px 250px 330px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label17" runat="server" Text="RESIDENCIA EN LUGAR DE TRABAJO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlResideSiNo" runat="server" Width="45px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label18" runat="server" Text="TIEMPO DE RESIDENCIA EN LUGAR DE TRABAJO (Años)" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTiempoResidencia" ShowLabel="false" CssClass="mright" runat="server" Width="160"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow11" ColumnWidths="150px 550px 100px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label19" runat="server" Text="CORREO ELECTRÓNICO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtCorreoElectronico" ShowLabel="false" CssClass="mright" runat="server" Width="540"></x:TextBox>
                                                        <x:Label ID="label20" runat="server" Text="TELÉFONO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTelefonos" ShowLabel="false" CssClass="mright" runat="server" Width="160"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow12" ColumnWidths="149px 160px 120px 200px 110px 60px 130px 30px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label21" runat="server" Text="ESTADO CIVIL" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEstadoCivil" runat="server" Width="120px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label22" runat="server" Text="GR. INSTRUCCIÓN" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlGradoInstruccion" runat="server" Width="175px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label23" runat="server" Text="# HIJOS VIVOS" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroHijosVivos" ShowLabel="false" CssClass="mright" runat="server" Width="30"></x:TextBox>
                                                        <x:Label ID="label24" runat="server" Text="# HIJOS MUERTOS" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroHijosMuertos" ShowLabel="false" CssClass="mright" runat="server" Width="30"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel4" runat="server" ShowBorder="True" ShowHeader="True" Title="ANTECEDENTES OCUPACIONALES" EnableBackgroundColor="true" Layout="VBox"
                                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Height="200">
                                    <Items>
                                        <x:Grid ID="grdAntecedentesOcupacionales" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" EnableRowNumber="True"
                                            AjaxLoadingType="Mask" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_HistoryId" EnableTextSelection="true"
                                            EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" OnRowCommand="grdAntecedentesOcupacionales_RowCommand">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar8" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnNuevoAntecedenteOcupacional" Text="Agregar Antecedente Ocupacional" Icon="Add" runat="server">
                                                        </x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="WindowAddAntecedenteOcupacional" HeaderText=""
                                                    Icon="Pencil" ToolTip="Editar Antecedente Ocupacional" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_HistoryId" DataIFrameUrlFormatString="frmAntecedenteOcupacional.aspx?Mode=Edit&v_HistoryId={0}"
                                                    DataWindowTitleField="v_Organization" DataWindowTitleFormatString="Editar Antecedente Ocupacional {0}" />
                                                <x:WindowField ColumnID="myWindowFieldPeligros" Width="25px" WindowID="xxx" HeaderText=""
                                                    Icon="Cog" ToolTip="Agregar Peligros" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_HistoryId" DataIFrameUrlFormatString="frmPeligros.aspx?v_HistoryId={0}"
                                                    DataWindowTitleField="v_Organization" DataWindowTitleFormatString=" {0}" />
                                                <x:WindowField ColumnID="myWindowFieldEPPs" Width="25px" WindowID="Window1" HeaderText=""
                                                    Icon="Shield" ToolTip="Agregar EPP" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_HistoryId" DataIFrameUrlFormatString="frmEPP.aspx?v_HistoryId={0}"
                                                    DataWindowTitleField="v_Organization" DataWindowTitleFormatString=" {0}" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea Eliminar este registro?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar registro" CommandName="DeleteRegistro" />
                                                <x:BoundField Width="80px" DataField="d_StartDate" DataFormatString="{0:d}" HeaderText="Fecha Inicio" />
                                                <x:BoundField Width="80px" DataField="d_EndDate" DataFormatString="{0:d}" HeaderText="Fecha Fin" />
                                                <x:BoundField Width="230px" DataField="v_Organization" DataFormatString="{0}" HeaderText="Empresa" />
                                                <x:BoundField Width="120px" DataField="v_TypeActivity" DataFormatString="{0}" HeaderText="Actividad" />
                                                <x:BoundField Width="120px" DataField="i_GeografixcaHeight" DataFormatString="{0}" HeaderText="Altura Geográfica" />
                                                <x:BoundField Width="180px" DataField="v_workstation" DataFormatString="{0}" HeaderText="Puesto de Trabajo" />
                                            </Columns>
                                        </x:Grid>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel5" runat="server" ShowBorder="True" ShowHeader="True" Title="ANTECEDENTES PATOLÓGICOS PERSONALES" EnableBackgroundColor="true" Layout="VBox"
                                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Height="200">
                                    <Items>
                                        <x:Grid ID="grdAntecedentesPersonales" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" EnableRowNumber="True"
                                            AjaxLoadingType="Mask" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_PersonMedicalHistoryId" EnableTextSelection="true"
                                            EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" OnRowCommand="grdAntecedentesPersonales_RowCommand">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar9" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnNuevoAntecedentePersonal" Text="Agregar Antecedente Personal" Icon="Add" runat="server">
                                                        </x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="WindowAddAntecedentePersonal" HeaderText=""
                                                    Icon="Pencil" ToolTip="Editar Antecedente Personal" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_PersonMedicalHistoryId,v_DiseasesName" DataIFrameUrlFormatString="frmAntecedentePersonal.aspx?Mode=Edit&v_PersonMedicalHistoryId={0}&v_DiseasesName={1}"
                                                    DataWindowTitleField="v_DiseasesName" DataWindowTitleFormatString="Editar Antededente  {0}" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea Eliminar este registro?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar registro" CommandName="DeleteRegistro" />
                                                <x:BoundField Width="100px" DataField="v_GroupName" DataFormatString="{0}" HeaderText="Grupo" />
                                                <x:BoundField Width="280px" DataField="v_DiseasesName" DataFormatString="{0}" HeaderText="Diagnóstico" />
                                                <x:BoundField Width="160px" DataField="v_TypeDiagnosticName" DataFormatString="{0}" HeaderText="Tipo Diagnóstico" />
                                                <x:BoundField Width="80px" DataField="d_StartDate" DataFormatString="{0:d}" HeaderText="Fecha Inicio" />
                                                <x:BoundField Width="200px" DataField="v_DiagnosticDetail" DataFormatString="{0}" HeaderText="Detalle" />
                                                <x:BoundField Width="100px" DataField="v_TreatmentSite" DataFormatString="{0}" HeaderText="Tratamiento" />
                                            </Columns>
                                        </x:Grid>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel6" runat="server" ShowBorder="True" ShowHeader="True" Title="HÁBITOS NOCIVOS" EnableBackgroundColor="true" Layout="VBox"
                                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Height="200">
                                    <Items>
                                        <x:Grid ID="grdHabitosNocivos" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" EnableRowNumber="True"
                                            AjaxLoadingType="Mask" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_NoxiousHabitsId" EnableTextSelection="true"
                                            EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" OnRowCommand="grdHabitosNocivos_RowCommand">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar10" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnAgregarHabitoNocivo" Text="Agregar Hábito Nocivos" Icon="Add" runat="server">
                                                        </x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="WindowAddHabitoNocivo" HeaderText=""
                                                    Icon="Pencil" ToolTip="Editar Hábito Nocivo" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_NoxiousHabitsId" DataIFrameUrlFormatString="frmHabitosNocivos.aspx?Mode=Edit&v_NoxiousHabitsId={0}"
                                                    DataWindowTitleField="v_TypeHabitsName" DataWindowTitleFormatString="Editar Hábito Nocivo  {0}" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea Eliminar este registro?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar registro" CommandName="DeleteRegistro" />
                                                <x:BoundField Width="200px" DataField="v_TypeHabitsName" DataFormatString="{0}" HeaderText="Hábito" />
                                                <x:BoundField Width="280px" DataField="v_Frequency" DataFormatString="{0}" HeaderText="Frecuencia" />
                                                <x:BoundField Width="280px" DataField="v_Comment" DataFormatString="{0}" HeaderText="Comentario" />
                                            </Columns>
                                        </x:Grid>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel7" runat="server" ShowBorder="True" ShowHeader="True" Title="ANTECEDENTES PATOLÓGICOS FAMILIARES" EnableBackgroundColor="true" Layout="VBox"
                                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Height="200">
                                    <Items>
                                        <x:Grid ID="grdAntecedenteFamiliar" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" EnableRowNumber="True"
                                            AjaxLoadingType="Mask" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_FamilyMedicalAntecedentsId" EnableTextSelection="true"
                                            EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" OnRowCommand="grdAntecedenteFamiliar_RowCommand">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar11" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnAgregarAntecedenteFamiliar" Text="Agregar Antecedente Familiar" Icon="Add" runat="server">
                                                        </x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="WindowAddAntecedenteFamiliar" HeaderText=""
                                                    Icon="Pencil" ToolTip="Editar Antecedente Familiar" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_FamilyMedicalAntecedentsId" DataIFrameUrlFormatString="frmAntecedenteFamiliar.aspx?Mode=Edit&v_FamilyMedicalAntecedentsId={0}"
                                                    DataWindowTitleField="v_TypeFamilyName" DataWindowTitleFormatString="Editar Antecedente Familiar  {0}" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea Eliminar este registro?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar registro" CommandName="DeleteRegistro" />
                                                <x:BoundField Width="100px" DataField="v_TypeFamilyName" DataFormatString="{0}" HeaderText="Grupo" />
                                                <x:BoundField Width="280px" DataField="v_DiseaseName" DataFormatString="{0}" HeaderText="Diagnóstico" />
                                                <x:BoundField Width="280px" DataField="v_Comment" DataFormatString="{0}" HeaderText="Comentario" />
                                            </Columns>
                                        </x:Grid>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelGinecologico" Title="ANTECEDENTES GINECOLÓGICOS" EnableBackgroundColor="true" Height="120px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:Form ID="Form10" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow18" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left">
                                                            <Rows>
                                                                <x:FormRow ID="FormRow22" ColumnWidths="70px 150px        270px      50px 120px       180px 120px  " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label30" runat="server" Text="Menarquia" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtMenarquia" Label="Menarquia" CssClass="mright" runat="server" Width="130" ShowLabel="false"></x:TextBox>

                                                                        <x:TextBox ID="txtGestacion" Label="Gestación y Paridad" CssClass="mright" runat="server" Width="130"></x:TextBox>

                                                                        <x:Label ID="label31" runat="server" Text="FUM" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtFum" Label="" CssClass="mright" runat="server" Width="100" ShowLabel="false"></x:TextBox>

                                                                        <x:Label ID="label32" runat="server" Text="MAC(Método anticonceptivo)" ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlMac" runat="server" Width="110" ShowLabel="false"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow23" ColumnWidths="70px 150px   270px   50px 120px   180px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label33" runat="server" Text="Régimen Catamenial" ShowLabel="false"></x:Label>
                                                                        <x:TextArea ID="txtRegimenCatamenial" Label="" CssClass="mright" runat="server" Width="130" ShowLabel="false" Height="35"></x:TextArea>

                                                                        <x:TextBox ID="txtCirugiaGineco" Label="Cirugía Ginecológicas" CssClass="mright" runat="server" Width="130" Height="35"></x:TextBox>

                                                                        <x:Label ID="label34" runat="server" Text="Último PAP" ShowLabel="false"></x:Label>
                                                                        <x:TextArea ID="txtUltimoPAP" Label="" CssClass="mright" runat="server" Width="100" ShowLabel="false" Height="35"></x:TextArea>

                                                                        <x:Label ID="label35" runat="server" Text="Resultado del último PAP" ShowLabel="false"></x:Label>
                                                                        <x:TextArea ID="txtResultadoPAP" Label="" CssClass="mright" runat="server" Width="110" ShowLabel="false" Height="35"></x:TextArea>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow24" ColumnWidths="70px 150px   740px  " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label36" runat="server" Text="Última Mamografía" ShowLabel="false"></x:Label>
                                                                        <x:TextArea ID="txtUltimaMamo" Label="" CssClass="mright" runat="server" Width="130" ShowLabel="false" Height="35"></x:TextArea>

                                                                        <x:TextArea ID="txtResultadoMamo" Label="Resultado Mamografía" CssClass="mright" runat="server" Width="130" Height="35"></x:TextArea>


                                                                    </Items>
                                                                </x:FormRow>
                                                            </Rows>
                                                        </x:Form>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel8" Title="EVALUACIÓN MÉDICA" EnableBackgroundColor="true" Height="110px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form6" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow13" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:Form ID="Form7" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left">
                                                            <Rows>
                                                                <x:FormRow ID="FormRow14" ColumnWidths="70px 890px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label25" runat="server" Text="ANAMNESIS" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtAnamnesis" runat="server" Text="" Width="820" TabIndex="0"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow15" ColumnWidths="135px  120px  130px  120px  120px  140px  140px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label26" runat="server" Text="EXAMEN CLÍNICO" ShowLabel="false"></x:Label>
                                                                        <x:NumberBox ID="txtTalla" runat="server" Text="" Width="40" Label="TALLA(m)" OnTextChanged="txtTalla_TextChanged" AutoPostBack="true" TabIndex="1" NoDecimal="false"></x:NumberBox>
                                                                        <x:NumberBox ID="txtPeso" runat="server" Text="" Width="45" Label="PESO(kg.)" OnTextChanged="txtPeso_TextChanged" AutoPostBack="true" TabIndex="2" NoDecimal="false"></x:NumberBox>
                                                                        <x:NumberBox ID="txtImc" runat="server" Text="" Width="40" Label="IMC" NoDecimal="false"></x:NumberBox>
                                                                        <x:NumberBox ID="txtIcc" runat="server" Text="" Width="40" Label="ICC" NoDecimal="false"></x:NumberBox>
                                                                        <x:NumberBox ID="txtfres" runat="server" Text="" Width="50" Label="F.RESP" TabIndex="3" NoDecimal="false"></x:NumberBox>
                                                                        <x:NumberBox ID="txtFcar" runat="server" Text="" Width="50" Label="F.CARD" TabIndex="4" NoDecimal="false"></x:NumberBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow16" ColumnWidths="135px  120px  130px  120px  120px  140px  140px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label27" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                        <x:NumberBox ID="txtParterial" runat="server" Text="" Width="40" Label="P.A.S" TabIndex="5"></x:NumberBox>
                                                                        <x:NumberBox ID="txtTemp" runat="server" Text="" Width="45" Label="TEMP(C)" TabIndex="7"></x:NumberBox>
                                                                        <x:NumberBox ID="txtPcadera" runat="server" Text="" Width="40" Label="P.CADERA" OnTextChanged="txtPcadera_TextChanged" AutoPostBack="true" TabIndex="8"></x:NumberBox>
                                                                        <x:NumberBox ID="txtPadb" runat="server" Text="" Width="40" Label="P.ADB." OnTextChanged="txtPadb_TextChanged" AutoPostBack="true" TabIndex="9"></x:NumberBox>
                                                                        <x:NumberBox ID="txtGcorporal" runat="server" Text="" Width="50" Label="%G.CORP." TabIndex="10"></x:NumberBox>
                                                                        <x:NumberBox ID="txtSatO2" runat="server" Text="" Width="50" Label="SAT. O2" TabIndex="11"></x:NumberBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow73" ColumnWidths="135px  120px  130px  120px  120px  140px  140px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label404" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                        <x:NumberBox ID="txtParterialDiatolica" runat="server" Text="" Width="40" Label="P.A.D" TabIndex="6"></x:NumberBox>
                                                                        <x:Label ID="label405" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label406" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label407" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label408" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label409" runat="server" Text=" " ShowLabel="false"></x:Label>

                                                                    </Items>
                                                                </x:FormRow>
                                                            </Rows>
                                                        </x:Form>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel9" Title="EXAMEN FÍSICO" EnableBackgroundColor="true" Height="960px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items>
                                        <x:Panel ID="Panel10" Width="200px" Height="910px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="ÓRGANO Y SISTEMA">
                                            <Items>
                                                <x:TextBox ID="tLabel28" runat="server" Text="PIEL" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="tLabel38" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="tLabel29" runat="server" Text="CABELLO" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="tLabel39" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="tLabel37" runat="server" Text="OJOS Y ANEXOS" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="tLabel40" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="tLabel47" runat="server" Text="OÍDOS" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="tLabel48" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox1" runat="server" Text="NARIZ" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label28" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox2" runat="server" Text="BOCA" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label29" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox3" runat="server" Text="FARINGE" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label37" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox4" runat="server" Text="CUELLO" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label38" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox5" runat="server" Text="APARATO RESPIRATORIO" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label39" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox6" runat="server" Text="APARATO CARDIOVASCULAR" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label40" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox7" runat="server" Text="APARATO DIGESTIVO" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label47" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox8" runat="server" Text="APARATO GENITOURINARIO" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label48" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox9" runat="server" Text="APARATO LOCOMOTOR" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label51" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox10" runat="server" Text="MARCHA" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label52" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox11" runat="server" Text="COLUMNA" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label53" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox12" runat="server" Text="EXTREMIDADES SUPERIORES" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label54" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox13" runat="server" Text="EXTREMIDADES INFERIORES" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label55" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox14" runat="server" Text="LINFÁTICOS" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label56" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox15" runat="server" Text="SISTEMA NERVIOSO" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label57" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox16" runat="server" Text="ECTOSCOPÍA" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label58" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox17" runat="server" Text="ESTADO MENTAL" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label759" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox95" runat="server" Text="RESUMEN" Width="190" Readonly="true"></x:TextBox>
                                                <x:Label ID="Label760" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox96" runat="server" Text="PERSONA SANA" Width="190" Readonly="true"></x:TextBox>

                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel11" ColumnWidth="15%" EnableBackgroundColor="true" runat="server"
                                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="910px" Title="SIN HALLAZGO">
                                            <Items>
                                                <x:DropDownList ID="ddlPiel" runat="server" Width="100" TabIndex="12" OnSelectedIndexChanged="ddlPiel_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label41" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlCabello" runat="server" Width="100" OnSelectedIndexChanged="ddlCabello_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label42" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlOjosAnexos" runat="server" Width="100" OnSelectedIndexChanged="ddlOjosAnexos_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label43" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlOidos" runat="server" Width="100" OnSelectedIndexChanged="ddlOidos_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label49" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlNariz" runat="server" Width="100" OnSelectedIndexChanged="ddlNariz_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label60" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlBoca" runat="server" Width="100" OnSelectedIndexChanged="ddlBoca_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label61" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlFaringe" runat="server" Width="100" OnSelectedIndexChanged="ddlFaringe_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label62" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlCuello" runat="server" Width="100" OnSelectedIndexChanged="ddlCuello_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label63" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlApaRespiratorio" runat="server" Width="100" OnSelectedIndexChanged="ddlApaRespiratorio_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label64" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlApaCardiovascular" runat="server" Width="100" OnSelectedIndexChanged="ddlApaCardiovascular_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label65" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlApaDigestivo" runat="server" Width="100" OnSelectedIndexChanged="ddlApaDigestivo_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label66" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlApaGenito" runat="server" Width="100" OnSelectedIndexChanged="ddlApaGenito_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label67" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlApaLocomotor" runat="server" Width="100" OnSelectedIndexChanged="ddlApaLocomotor_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label68" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlMarcha" runat="server" Width="100" OnSelectedIndexChanged="ddlMarcha_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label69" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlColumna" runat="server" Width="100" OnSelectedIndexChanged="ddlColumna_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label70" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlExtreSuper" runat="server" Width="100" OnSelectedIndexChanged="ddlExtreSuper_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label71" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlExtreInfe" runat="server" Width="100" OnSelectedIndexChanged="ddlExtreInfe_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label72" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlLinfaticos" runat="server" Width="100" OnSelectedIndexChanged="ddlLinfaticos_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label73" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlSistNerviso" runat="server" Width="100" OnSelectedIndexChanged="ddlSistNerviso_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label74" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlEctoscopia" runat="server" Width="100" OnSelectedIndexChanged="ddlEctoscopia_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label75" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlEstadoMental" runat="server" Width="100" OnSelectedIndexChanged="ddlEstadoMental_SelectedIndexChanged" AutoPostBack="true"></x:DropDownList>
                                                <x:Label ID="Label761" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label762" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label764" runat="server" Text=" "></x:Label>
                                                <x:CheckBox ID="chkPersonaSana" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel12" ColumnWidth="85%" EnableBackgroundColor="true" runat="server"
                                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="910px" Title="HALLAZGOS">
                                            <Items>
                                                <x:TextBox ID="txtHallazgoPiel" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label44" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtHallazgoCabello" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label45" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtOjosAnexos" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label46" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtOidos" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label50" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtNariz" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label77" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtBoca" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label78" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtFaringe" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label79" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtCuello" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label80" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtApaRespira" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label81" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtApaCardiovas" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label82" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtApaDigestivo" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label83" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtApaGenito" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label84" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtApaLocomotor" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label85" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtMarcha" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label86" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtColumna" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label87" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtExtreSuperio" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label88" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtExtreInfer" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label93" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtLinfaticos" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label89" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtSistemaNervisos" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label90" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtExtoscopia" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label91" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtEstadoMental" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label763" runat="server" Text=" "></x:Label>
                                                <x:TextArea ID="txtResumen" ShowLabel="false" CssClass="mright" runat="server" Width="810" Height="40"></x:TextArea>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>

                                <x:Panel ID="Panel162" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form1207" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow6120" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtMedicina1Auditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtMedicina1AuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtMedicina1AuditorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow1621" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtMedicina1Evaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtMedicina1EvaluadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtMedicina1EvaluadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                                <Ext:FormRow ID="FormRow207" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtMedicinaInformador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtMedicinaInformadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtMedicinaInformadorActualiza" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Tab>
                        
                        <x:Tab ID="TabAnexo16" BodyPadding="5px" Title="Anexo - 16" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <x:Button ID="btngrabarAnexo16" Text="Grabar Anexo - 16" Icon="SystemSave" runat="server" OnClick="btngrabarAnexo16_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Label ID="Label747" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label748" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabaAnexo16" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporte16" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel14" Title="DATOS DE LA EMPRESA" EnableBackgroundColor="true" Height="160px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow21" ColumnWidths="150px 630px 80px 100px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label76" runat="server" Text="RAZÓN SOCIAL" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtRazonSocial_16" ShowLabel="false" CssClass="mright" runat="server" Width="600"></x:TextBox>
                                                        <x:Label ID="label92" runat="server" Text="RUC N.R.O" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtRucEmpresa_16" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow25" ColumnWidths="149px 810px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label94" runat="server" Text="ACTIVIDAD ECONÓMICA" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtActividadEconomica_16" ShowLabel="false" CssClass="mright" runat="server" Width="810"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow26" ColumnWidths="150px 810px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label95" runat="server" Text="LUGAR DE TRABAJO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtLugarTrabajo_16" ShowLabel="false" CssClass="mright" runat="server" Width="810"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow27" ColumnWidths="149px 810px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label96" runat="server" Text="PUESTO DE TRABAJO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtPuestoTrabajo_16" ShowLabel="false" CssClass="mright" runat="server" Width="810"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow47" ColumnWidths="150px 230px 120px 250px 110px 100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label165" runat="server" Text="MINERALES EXPLOTADOS O PROCESADOS" ShowLabel="false"></x:Label>
                                                        <x:TextArea ID="txtMineralesExplotados_16" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextArea>
                                                        <x:Label ID="label181" runat="server" Text="LUGAR DE LABOR" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlLugarLabor_16" runat="server" Width="160px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label182" runat="server" Text="ALTITUD LABOR" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAltitudLabor_16" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>

                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel15" Title="FILIACIÓN DEL TRABAJADOR" EnableBackgroundColor="true" Height="230px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form12" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow28" ColumnWidths="150px 810px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label97" runat="server" Text="NOMBRES Y APELLIDOS" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNombreCompletoTrabajador_16" ShowLabel="false" CssClass="mright" runat="server" Width="810"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow29" ColumnWidths="149px 160px 120px 200px 170px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label98" runat="server" Text="FECHA NACIMIENTO" ShowLabel="false"></x:Label>
                                                        <x:DatePicker ID="dpcFechaNacimiento_16" Label="F.I" Width="100px" runat="server" DateFormatString="dd/MM/yyyy" ShowLabel="false" />
                                                        <x:Label ID="label99" runat="server" Text="TIPO DOCUMENTO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTipoDocumento_16" runat="server" Width="150px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label100" runat="server" Text="NÚMERO DE DOCUMENTO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroDocumento_16" ShowLabel="false" CssClass="mright" runat="server" Width="160"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow30" ColumnWidths="150px 160px 120px 200px 170px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label101" runat="server" Text="EDAD" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtEdad_16" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                        <x:Label ID="label102" runat="server" Text="TIPO EVALUACIÓN" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTipoEvaluacion_16" runat="server" Width="150px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label103" runat="server" Text="TIPO DE SEGURO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTipoSeguro_16" runat="server" Width="160px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow31" ColumnWidths="149px 810px 170px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label104" runat="server" Text="DIRECCIÓN FISCAL" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtDireccionFiscal_16" ShowLabel="false" CssClass="mright" runat="server" Width="430"></x:TextBox>
                                                        <x:Label ID="label714" runat="server" Text="LUGAR NACIMIENTO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtLugarNacimiento_16" ShowLabel="false" CssClass="mright" runat="server" Width="160"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow32" ColumnWidths="150px 160px 120px 200px 170px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label107" runat="server" Text="DEPARTAMENTO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlDepartamento_16" runat="server" Width="160px" ShowLabel="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_16_SelectedIndexChanged"></x:DropDownList>
                                                        <x:Label ID="label106" runat="server" Text="PROVINCIA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlProvincia_16" runat="server" Width="150px" ShowLabel="false" AutoPostBack="true" OnSelectedIndexChanged="ddlProvincia_16_SelectedIndexChanged"></x:DropDownList>
                                                        <x:Label ID="label105" runat="server" Text="DISTRITO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlDistrito_16" runat="server" Width="150px" ShowLabel="false"></x:DropDownList>

                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow33" ColumnWidths="220px 90px 320px 170px 60px 100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label108" runat="server" Text="RESIDENCIA EN LUGAR DE TRABAJO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlResideSiNo_16" runat="server" Width="45px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label109" runat="server" Text="TIEMPO DE RESIDENCIA EN LUGAR TRABAJO(Años)" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTiempoResidencia_16" ShowLabel="false" CssClass="mright" runat="server" Width="150"></x:TextBox>
                                                        <x:Label ID="label183" runat="server" Text="GÉNERO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlGenero_16" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow34" ColumnWidths="150px 550px 100px 160px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label110" runat="server" Text="CORREO ELECTRÓNICO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtCorreoElectronico_16" ShowLabel="false" CssClass="mright" runat="server" Width="540"></x:TextBox>
                                                        <x:Label ID="label111" runat="server" Text="TELÉFONO" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTelefonos_16" ShowLabel="false" CssClass="mright" runat="server" Width="160"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow35" ColumnWidths="149px 160px 120px 200px 110px 60px 130px 30px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label112" runat="server" Text="ESTADO CIVIL" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEstadoCivil_16" runat="server" Width="120px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label113" runat="server" Text="GR. INSTRUCCIÓN" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlGradoInstruccion_16" runat="server" Width="175px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label114" runat="server" Text="# HIJOS VIVOS" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroHijosVivos_16" ShowLabel="false" CssClass="mright" runat="server" Width="30"></x:TextBox>
                                                        <x:Label ID="label115" runat="server" Text="# HIJOS MUERTOS" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtNroHijosMuertos_16" ShowLabel="false" CssClass="mright" runat="server" Width="30"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel16" runat="server" ShowBorder="True" ShowHeader="True" Title="ANTECEDENTES OCUPACIONALES" EnableBackgroundColor="true" Layout="VBox"
                                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Height="200">
                                    <Items>
                                        <x:Grid ID="grdAntecedentesOcupacionales_16" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" EnableRowNumber="True"
                                            AjaxLoadingType="Mask" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_HistoryId" EnableTextSelection="true"
                                            EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar12" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnNuevoAntecedenteOcupacional_16" Text="Agregar Antecedente Ocupacional" Icon="Add" runat="server">
                                                        </x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="WindowAddAntecedenteOcupacional" HeaderText=""
                                                    Icon="Pencil" ToolTip="Editar Antecedente Ocupacional" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_HistoryId" DataIFrameUrlFormatString="frmAntecedenteOcupacional.aspx?Mode=Edit&v_HistoryId={0}"
                                                    DataWindowTitleField="v_Organization" DataWindowTitleFormatString="Editar Antecedente Ocupacional {0}" />
                                                <x:WindowField ColumnID="myWindowFieldPeligros" Width="25px" WindowID="xxx" HeaderText=""
                                                    Icon="Cog" ToolTip="Agregar Peligros" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_WorkstationDangersId" DataIFrameUrlFormatString="frmPeligros.aspx?v_HistoryId={0}"
                                                    DataWindowTitleField="v_Organization" DataWindowTitleFormatString=" {0}" />
                                                <x:WindowField ColumnID="myWindowFieldEPPs" Width="25px" WindowID="Window1" HeaderText=""
                                                    Icon="Shield" ToolTip="Agregar EPP" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_TypeofEEPId" DataIFrameUrlFormatString="frmEPP.aspx?v_HistoryId={0}"
                                                    DataWindowTitleField="v_Organization" DataWindowTitleFormatString=" {0}" />

                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea Eliminar este registro?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar registro" CommandName="DeleteRegistro" />
                                                <x:BoundField Width="80px" DataField="d_StartDate" DataFormatString="{0:d}" HeaderText="Fecha Inicio" />
                                                <x:BoundField Width="80px" DataField="d_EndDate" DataFormatString="{0:d}" HeaderText="Fecha Fin" />
                                                <x:BoundField Width="230px" DataField="v_Organization" DataFormatString="{0}" HeaderText="Empresa" />
                                                <x:BoundField Width="180px" DataField="v_TypeActivity" DataFormatString="{0}" HeaderText="Actividad" />
                                                <x:BoundField Width="100px" DataField="i_GeografixcaHeight" DataFormatString="{0}" HeaderText="Altura Geográfica" />
                                                <x:BoundField Width="180px" DataField="v_workstation" DataFormatString="{0}" HeaderText="Estación" />
                                            </Columns>
                                        </x:Grid>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel17" runat="server" ShowBorder="True" ShowHeader="True" Title="ANTECEDENTES PATOLÓGICOS PERSONALES" EnableBackgroundColor="true" Layout="VBox"
                                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Height="200">
                                    <Items>
                                        <x:Grid ID="grdAntecedentesPersonales_16" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" EnableRowNumber="True"
                                            AjaxLoadingType="Mask" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_PersonMedicalHistoryId" EnableTextSelection="true"
                                            EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar13" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnNuevoAntecedentePersonal_16" Text="Agregar Antecedente Personal" Icon="Add" runat="server">
                                                        </x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                                    Icon="Pencil" ToolTip="Editar Antecedente Personal" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_PersonMedicalHistoryId" DataIFrameUrlFormatString="frmAntecedentePersonal.aspx?Mode=Edit&v_PersonMedicalHistoryId={0}"
                                                    DataWindowTitleField="v_DiseasesName" DataWindowTitleFormatString="Editar Antededente  {0}" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea Eliminar este registro?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar registro" CommandName="DeleteRegistro" />
                                                <x:BoundField Width="100px" DataField="v_GroupName" DataFormatString="{0}" HeaderText="Grupo" />
                                                <x:BoundField Width="280px" DataField="v_DiseasesName" DataFormatString="{0}" HeaderText="Diagnóstico" />
                                                <x:BoundField Width="160px" DataField="v_TypeDiagnosticName" DataFormatString="{0}" HeaderText="Tipo Diagnóstico" />
                                                <x:BoundField Width="80px" DataField="d_StartDate" DataFormatString="{0:d}" HeaderText="Fecha Inicio" />
                                                <x:BoundField Width="200px" DataField="v_DiagnosticDetail" DataFormatString="{0}" HeaderText="Detalle" />
                                                <x:BoundField Width="100px" DataField="v_TreatmentSite" DataFormatString="{0}" HeaderText="Tratamiento" />
                                            </Columns>
                                        </x:Grid>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel18" runat="server" ShowBorder="True" ShowHeader="True" Title="HÁBITOS NOCIVOS" EnableBackgroundColor="true" Layout="VBox"
                                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Height="200">
                                    <Items>
                                        <x:Grid ID="grdHabitosNocivos_16" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" EnableRowNumber="True"
                                            AjaxLoadingType="Mask" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_NoxiousHabitsId" EnableTextSelection="true"
                                            EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar14" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnAgregarHabitoNocivo_16" Text="Agregar Hábito Nocivos" Icon="Add" runat="server">
                                                        </x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                                    Icon="Pencil" ToolTip="Editar Hábito Nocivo" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_NoxiousHabitsId" DataIFrameUrlFormatString="frmHabitosNocivos.aspx?Mode=Edit&v_NoxiousHabitsId={0}"
                                                    DataWindowTitleField="v_TypeHabitsName" DataWindowTitleFormatString="Editar Hábito Nocivo  {0}" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea Eliminar este registro?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar registro" CommandName="DeleteRegistro" />
                                                <x:BoundField Width="200px" DataField="v_TypeHabitsName" DataFormatString="{0}" HeaderText="Hábito" />
                                                <x:BoundField Width="280px" DataField="v_Frequency" DataFormatString="{0}" HeaderText="Frecuencia" />
                                                <x:BoundField Width="280px" DataField="v_Comment" DataFormatString="{0}" HeaderText="Comentario" />
                                            </Columns>
                                        </x:Grid>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel19" runat="server" ShowBorder="True" ShowHeader="True" Title="ANTECEDENTES PATOLÓGICOS FAMILIARES" EnableBackgroundColor="true" Layout="VBox"
                                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Height="200">
                                    <Items>
                                        <x:Grid ID="grdAntecedenteFamiliar_16" ShowBorder="true" ShowHeader="false" runat="server" AutoScroll="true" EnableRowNumber="True"
                                            AjaxLoadingType="Mask" EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_FamilyMedicalAntecedentsId" EnableTextSelection="true"
                                            EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5">
                                            <Toolbars>
                                                <x:Toolbar ID="Toolbar15" runat="server">
                                                    <Items>
                                                        <x:Button ID="btnAgregarAntecedenteFamiliar_16" Text="Agregar Antecedente Familiar" Icon="Add" runat="server">
                                                        </x:Button>
                                                    </Items>
                                                </x:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                                    Icon="Pencil" ToolTip="Editar Antecedente Familiar" DataTextFormatString="{0}"
                                                    DataIFrameUrlFields="v_FamilyMedicalAntecedentsId" DataIFrameUrlFormatString="frmAntecedenteFamiliar.aspx?Mode=Edit&v_FamilyMedicalAntecedentsId={0}"
                                                    DataWindowTitleField="v_TypeFamilyName" DataWindowTitleFormatString="Editar Antecedente Familiar  {0}" />
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="¿Está seguro que desea Eliminar este registro?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar registro" CommandName="DeleteRegistro" />
                                                <x:BoundField Width="100px" DataField="v_TypeFamilyName" DataFormatString="{0}" HeaderText="Grupo" />
                                                <x:BoundField Width="280px" DataField="v_DiseaseName" DataFormatString="{0}" HeaderText="Diagnóstico" />
                                                <x:BoundField Width="280px" DataField="v_Comment" DataFormatString="{0}" HeaderText="Comentario" />
                                            </Columns>
                                        </x:Grid>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="PanelGinecologico_16" Title="ANTECEDENTES GINECOLÓGICOS" EnableBackgroundColor="true" Height="120px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                                    <Items>
                                        <x:Form ID="Form13" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow36" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:Form ID="Form14" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left">
                                                            <Rows>
                                                                <x:FormRow ID="FormRow37" ColumnWidths="70px 150px        270px      50px 120px       180px 120px  " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label116" runat="server" Text="Menarquia" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtMenarquia_16" Label="Menarquia" CssClass="mright" runat="server" Width="130" ShowLabel="false"></x:TextBox>

                                                                        <x:TextBox ID="txtGestacion_16" Label="Gestación y Paridad" CssClass="mright" runat="server" Width="130"></x:TextBox>

                                                                        <x:Label ID="label117" runat="server" Text="FUM" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtFum_16" Label="" CssClass="mright" runat="server" Width="100" ShowLabel="false"></x:TextBox>

                                                                        <x:Label ID="label118" runat="server" Text="MAC(Método anticonceptivo)" ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlMac_16" runat="server" Width="110" ShowLabel="false"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow38" ColumnWidths="70px 150px   270px   50px 120px   180px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label119" runat="server" Text="Régimen Catamenial" ShowLabel="false"></x:Label>
                                                                        <x:TextArea ID="txtRegimenCatamenial_16" Label="" CssClass="mright" runat="server" Width="130" ShowLabel="false" Height="35"></x:TextArea>

                                                                        <x:TextBox ID="txtCirugiaGineco_16" Label="Cirugía Ginecológicas" CssClass="mright" runat="server" Width="130" Height="35"></x:TextBox>

                                                                        <x:Label ID="label120" runat="server" Text="Último PAP" ShowLabel="false"></x:Label>
                                                                        <x:TextArea ID="txtUltimoPAP_16" Label="" CssClass="mright" runat="server" Width="100" ShowLabel="false" Height="35"></x:TextArea>

                                                                        <x:Label ID="label121" runat="server" Text="Resultado del último PAP" ShowLabel="false"></x:Label>
                                                                        <x:TextArea ID="txtResultadoPAP_16" Label="" CssClass="mright" runat="server" Width="110" ShowLabel="false" Height="35"></x:TextArea>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow39" ColumnWidths="70px 150px   740px  " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label122" runat="server" Text="Última Mamografía" ShowLabel="false"></x:Label>
                                                                        <x:TextArea ID="txtUltimaMamo_16" Label="" CssClass="mright" runat="server" Width="130" ShowLabel="false" Height="35"></x:TextArea>

                                                                        <x:TextArea ID="txtResultadoMamo_16" Label="Resultado Mamografía" CssClass="mright" runat="server" Width="130" Height="35"></x:TextArea>


                                                                    </Items>
                                                                </x:FormRow>
                                                            </Rows>
                                                        </x:Form>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel21" Title="EVALUACIÓN MÉDICA" EnableBackgroundColor="true" Height="110px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form15" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow40" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:Form ID="Form16" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left">
                                                            <Rows>
                                                                <x:FormRow ID="FormRow41" ColumnWidths="70px 890px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label123" runat="server" Text="ANAMNESIS" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtAnamnesis_16" runat="server" Text="Paciente Asintomático, no refiere síntomas" Width="820" TabIndex="1"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow42" ColumnWidths="135px  120px  130px  120px  120px  140px  140px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label124" runat="server" Text="EXAMEN CLÍNICO" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtTalla_16" runat="server" Text="" Width="40" Label="TALLA(m)" OnTextChanged="txtTalla_TextChanged" AutoPostBack="true" TabIndex="2"></x:TextBox>
                                                                        <x:TextBox ID="txtPeso_16" runat="server" Text="" Width="45" Label="PESO(kg.)" OnTextChanged="txtPeso_TextChanged" AutoPostBack="true" TabIndex="3"></x:TextBox>
                                                                        <x:TextBox ID="txtImc_16" runat="server" Text="" Width="40" Label="IMC" Enabled="false"></x:TextBox>
                                                                        <x:TextBox ID="txtIcc_16" runat="server" Text="" Width="40" Label="ICC" Enabled="false" TabIndex="4"></x:TextBox>
                                                                        <x:TextBox ID="txtfres_16" runat="server" Text="" Width="50" Label="F.RESP" TabIndex="5"></x:TextBox>
                                                                        <x:TextBox ID="txtFcar_16" runat="server" Text="" Width="50" Label="F.CARD" TabIndex="6"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow43" ColumnWidths="135px  120px  130px  120px  120px  140px  140px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label125" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtParterial_16" runat="server" Text="" Width="40" Label="P.ARTERIAL" TabIndex="7"></x:TextBox>
                                                                        <x:TextBox ID="txtTemp_16" runat="server" Text="" Width="45" Label="TEMP(C)" TabIndex="8"></x:TextBox>
                                                                        <x:TextBox ID="txtPcadera_16" runat="server" Text="" Width="40" Label="P.CADERA" OnTextChanged="txtPcadera_TextChanged" AutoPostBack="true" TabIndex="9"></x:TextBox>
                                                                        <x:TextBox ID="txtPadb_16" runat="server" Text="" Width="40" Label="P.ADB." OnTextChanged="txtPadb_TextChanged" AutoPostBack="true" TabIndex="10"></x:TextBox>
                                                                        <x:TextBox ID="txtGcorporal_16" runat="server" Text="" Width="50" Label="%G.CORP." TabIndex="11"></x:TextBox>
                                                                        <x:TextBox ID="txtSatO2_16" runat="server" Text="" Width="50" Label="SAT. O2" TabIndex="12"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow74" ColumnWidths="135px  120px  130px  120px  120px  140px  140px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label410" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtPresionArterialDiastolica" runat="server" Text="" Width="40" Label="P.A.D" TabIndex="13"></x:TextBox>
                                                                        <x:Label ID="label411" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label412" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label413" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label414" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label415" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>

                                                            </Rows>
                                                        </x:Form>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel22" Title="EXAMEN FÍSICO" EnableBackgroundColor="true" Height="670px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items>
                                        <x:Panel ID="Panel23" Width="200px" Height="630px" EnableBackgroundColor="true"
                                            runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="ÓRGANO Y SISTEMA">
                                            <Items>
                                                <x:TextBox ID="TextBox49" runat="server" Text="CABEZA" Width="190"></x:TextBox>
                                                <x:Label ID="Label126" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox50" runat="server" Text="CUELLO" Width="190"></x:TextBox>
                                                <x:Label ID="Label127" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox53" runat="server" Text="NARIZ" Width="190"></x:TextBox>
                                                <x:Label ID="Label128" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox52" runat="server" Text="BOCA, ADMIGDALA, FARINGE, LARINGE" Width="190"></x:TextBox>
                                                <x:Label ID="Label129" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox51" runat="server" Text="REFLEJOS PUPILARES" Width="190"></x:TextBox>
                                                <x:Label ID="Label130" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox64" runat="server" Text="EXTREMIDADES SUPERIORES" Width="190"></x:TextBox>
                                                <x:Label ID="Label141" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox65" runat="server" Text="EXTREMIDADES INFERIORES" Width="190"></x:TextBox>
                                                <x:Label ID="Label142" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox54" runat="server" Text="REFLEJOS OSTEO TENDINOSOS" Width="190"></x:TextBox>
                                                <x:Label ID="Label131" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox62" runat="server" Text="MARCHA" Width="190"></x:TextBox>
                                                <x:Label ID="Label139" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox55" runat="server" Text="COLUMNA VERTEBRAL" Width="190"></x:TextBox>
                                                <x:Label ID="Label132" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox56" runat="server" Text="ABDOMEN" Width="190"></x:TextBox>
                                                <x:Label ID="Label133" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox57" runat="server" Text="ANILLOS INGUINALES" Width="190"></x:TextBox>
                                                <x:Label ID="Label134" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox58" runat="server" Text="HERNIAS" Width="190"></x:TextBox>
                                                <x:Label ID="Label135" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox59" runat="server" Text="VÁRICES" Width="190"></x:TextBox>
                                                <x:Label ID="Label136" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox60" runat="server" Text="ÓRGANOS GENITALES" Width="190"></x:TextBox>
                                                <x:Label ID="Label137" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="TextBox61" runat="server" Text="GANGLIOS" Width="190"></x:TextBox>

                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel24" ColumnWidth="15%" EnableBackgroundColor="true" runat="server"
                                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="630px" Title="SIN HALLAZGO">
                                            <Items>
                                                <x:DropDownList ID="ddlCabeza_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label146" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlCuello_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label147" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlNariz_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label148" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlBAFL_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label149" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlReflejosPupilares_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label150" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlExtreSuper_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label161" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlExtreInfe_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label162" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlReflejosOsteo_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label151" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlMarcha_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label152" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlColumnaVertebral_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label153" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlAbdomen_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label154" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlAnillosAngui_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label155" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlHernias_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label156" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlVarices_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label157" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlOrganosGenitales_16" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label158" runat="server" Text=" "></x:Label>

                                                <x:DropDownList ID="ddlGanglios_16" runat="server" Width="100"></x:DropDownList>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel25" ColumnWidth="85%" EnableBackgroundColor="true" runat="server"
                                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="630px" Title="HALLAZGOS">
                                            <Items>
                                                <x:TextBox ID="txtHallazgoCabeza_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label166" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtCuello_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label167" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtNariz_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label168" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtBAFL_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label169" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtReflejosPupilares_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label170" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtExtreSuper_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label171" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtExtreInfe_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label172" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtReflejosOsteo_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label173" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtMarcha_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label174" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtColumnaVertebral_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label175" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtAbdomen_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label176" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtAnillosAngui_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label177" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtHernias_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label178" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtVarices_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label179" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtOrganosGenitales_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>
                                                <x:Label ID="Label180" runat="server" Text=" "></x:Label>

                                                <x:TextBox ID="txtGanglios_16" Label="" CssClass="mright" runat="server" Width="630" ShowLabel="false"></x:TextBox>

                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel26" Title="" EnableBackgroundColor="true" Height="150px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="false">
                                    <Items>
                                        <x:Form ID="Form17" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow44" ColumnWidths="200px 60px 30px 60px 30px 80px 490px" runat="server">
                                                    <Items>                                                       
                                                        <x:Label ID="label186" runat="server" Text="PULMONES" ShowLabel="false"></x:Label>
                                                        <x:Label ID="label138" runat="server" Text="NORMAL" ShowLabel="false"></x:Label>
                                                        <%--<x:CheckBox ID="chkPulmonNormal" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                        <x:RadioButton ID="rdoPulmonNormal" GroupName="pulmones" runat="server" Text="" ShowLabel="false"></x:RadioButton>
                                                        <x:Label ID="label140" runat="server" Text="ANORMAL" ShowLabel="false"></x:Label>
                                                        <%--<x:CheckBox ID="chkPulmonAnormal" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                        <x:RadioButton ID="rdoPulmonAnormal" GroupName="pulmones" runat="server" Text="" ShowLabel="false"></x:RadioButton>
                                                        <x:Label ID="label143" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtPulmonDescripcion" runat="server" Text="" Width="490" ShowLabel="false"></x:TextBox>
                                                    </Items> 
                                              
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow45" ColumnWidths="200px 60px 30px 60px 30px 120px 30px 80px 340px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label144" runat="server" Text="TACTO RECTAL" ShowLabel="false"></x:Label>
                                                        <x:Label ID="label145" runat="server" Text="NORMAL" ShowLabel="false"></x:Label>
                                                        <%--<x:CheckBox ID="chkTactoRectalNormal" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                        <x:RadioButton ID="rdoTactoRectalNormal" GroupName="tactorectal" runat="server" Text="" ShowLabel="false"></x:RadioButton>
                                                        <x:Label ID="label159" runat="server" Text="ANORMAL" ShowLabel="false"></x:Label>
                                                        <%--<x:CheckBox ID="chkTactoRectalAnormal" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                        <x:RadioButton ID="rdoTactoRectalAnormal" GroupName="tactorectal" runat="server" Text="" ShowLabel="false"></x:RadioButton>
                                                        <x:Label ID="label164" runat="server" Text="NO SE REALIZÓ" ShowLabel="false"></x:Label>
                                                        <%--<x:CheckBox ID="chkTactoRectalNoRealizo" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                        <x:RadioButton ID="rdoTactoRectalNoRealizo" GroupName="tactorectal" runat="server" Text="" ShowLabel="false"></x:RadioButton>
                                                        <x:Label ID="label160" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtTactoRectalDescripcion" runat="server" Text="" Width="340" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                    <Items>

                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow46" ColumnWidths="810px 100px 50px " runat="server">
                                                    <Items>
                                                        <x:TextArea ID="txtResumen_16" ShowLabel="false" CssClass="mright" runat="server" Width="810" Height="40"></x:TextArea>
                                                        <x:Label ID="label163" runat="server" Text="Persona Sana" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkPersonaSana_16" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>

                                <x:Panel ID="Panel85" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form53" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow196" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtAnexo16Auditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAnexo16AuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAnexo16Actualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow197" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtAnexo16Evaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAnexo16EvaluadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAnexo16EvaluadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                                <Ext:FormRow ID="FormRow208" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtAnexo16Informador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAnexo16InformadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAnexo16InformadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Tab>

                        <x:Tab ID="TabOsteomuscular" BodyPadding="5px" Title="Osteomuscular" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <x:Button ID="btnOsteomuscular" Text="Grabar Osteomuscular" Icon="SystemSave" runat="server" OnClick="btnOsteomuscular_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Button ID="btnReporteOsteo" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel20" Title="IDENTIFICACIÓN DE FACTORES DE RIESGO ERGONÓMICO" EnableBackgroundColor="true" Height="390px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items>
                                        <x:Panel ID="Panel27" Width="300px" Height="350px" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="FACTORES DE RIESGO ERGONÓMICO">
                                            <Items>
                                                <x:TextBox ID="TextBox18" runat="server" Text="MANIPULACIÓN LEVANTAR-CARGAS" Width="290"></x:TextBox>
                                                <x:Label ID="Label184" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox19" runat="server" Text="MANIPULACIÓN EMPUJAR-CARGAS" Width="290"></x:TextBox>
                                                <x:Label ID="Label185" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox20" runat="server" Text="MANIPULACIÓN JALAR-CARGAS" Width="290"></x:TextBox>
                                                <x:Label ID="Label187" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox21" runat="server" Text="PESOS SUPERIORES A 25 KG." Width="290"></x:TextBox>
                                                <x:Label ID="Label188" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox22" runat="server" Text="LEVANTAMIENTO POR ENCIMA DEL HOMBRO" Width="290"></x:TextBox>
                                                <x:Label ID="Label189" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox23" runat="server" Text="MANIPULACIÓN DE VÁLVULAS" Width="290"></x:TextBox>
                                                <x:Label ID="Label190" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox24" runat="server" Text="MOVIMIENTOS REPETITIVOS" Width="290"></x:TextBox>
                                                <x:Label ID="Label191" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox25" runat="server" Text="POSTURA FORZADA" Width="290"></x:TextBox>
                                                <x:Label ID="Label192" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox26" runat="server" Text="POSTURA SEDENTARIA" Width="290"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel28" ColumnWidth="17%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="350px" Title="SI / NO">
                                            <Items>
                                                <x:DropDownList ID="ddlManiLevanCargas" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label194" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlManiEmpujarCargas" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label195" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlManiJalarCargas" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label196" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPesoMayor25" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label197" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlEncimaHombro" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label198" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlManiValvulas" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label199" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlMoviRepetitivos" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label200" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlMoviForzada" runat="server" Width="100"></x:DropDownList>
                                                <x:Label ID="Label201" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPosturaForzada" runat="server" Width="100"></x:DropDownList>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel29" ColumnWidth="83%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="350px" Title="POCO / REGULAR / FRECUENTE">
                                            <Items>
                                                <x:DropDownList ID="ddlPRFManiLevanCargas" runat="server" Width="150"></x:DropDownList>
                                                <x:Label ID="Label193" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPRFManiEmpujarCargas" runat="server" Width="150"></x:DropDownList>
                                                <x:Label ID="Label202" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPRFManiJalarCargas" runat="server" Width="150"></x:DropDownList>
                                                <x:Label ID="Label203" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPRFPesoMayor25" runat="server" Width="150"></x:DropDownList>
                                                <x:Label ID="Label204" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPRFEncimaHombro" runat="server" Width="150"></x:DropDownList>
                                                <x:Label ID="Label205" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPRFManiValvulas" runat="server" Width="150"></x:DropDownList>
                                                <x:Label ID="Label206" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPRFMoviRepetitivos" runat="server" Width="150"></x:DropDownList>
                                                <x:Label ID="Label207" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPRFMoviForzada" runat="server" Width="150"></x:DropDownList>
                                                <x:Label ID="Label208" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlPRFPosturaForzada" runat="server" Width="150"></x:DropDownList>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel30" Title="SÍNTOMAS" EnableBackgroundColor="true" Height="50px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form18" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow48" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:TextArea ID="txtsintomas" ShowLabel="false" CssClass="mright" runat="server" Width="950" Height="40"></x:TextArea>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel31" Title=" EVALUACIÓN  MÚSCULO - ESQUELÉTICA  BÁSICA" EnableBackgroundColor="true" Height="490px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items>
                                        <x:Panel ID="Panel32" Width="150px" Height="450px" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="DESCRIPCIÓN">
                                            <Items>
                                                <x:Label ID="Label210" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label211" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox27" runat="server" Text="ABDOMEN" Width="140"></x:TextBox>
                                                <x:Label ID="Label209" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label212" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label213" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label216" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label217" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox28" runat="server" Text="CADERA" Width="140"></x:TextBox>
                                                <x:Label ID="Label218" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label219" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label220" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label221" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label222" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox29" runat="server" Text="MUSLO" Width="140"></x:TextBox>
                                                <x:Label ID="Label223" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label224" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label225" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label226" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label227" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox30" runat="server" Text="ABDOMEN LATERAL" Width="140"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel33" ColumnWidth="12%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="EXCELENTE 1">
                                            <Items>
                                                <x:Image ID="Image3" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/01.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbAbdomen1" runat="server" GroupName="grupoabdomen" OnCheckedChanged="rbAbdomen1_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image5" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/cad01.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbCadera1" runat="server" GroupName="grupoCadera" OnCheckedChanged="rbCadera1_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image6" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/muslo01.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbMuslo1" runat="server" GroupName="grupoMuslo" OnCheckedChanged="rbMuslo1_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image7" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/abdomen1.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbLateral1" runat="server" GroupName="grupoLateral" AutoPostBack="true" OnCheckedChanged="rbLateral1_CheckedChanged"></x:RadioButton>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel34" ColumnWidth="12%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="PROMEDIO 2">
                                            <Items>
                                                <x:Image ID="Image1" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/011.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbAbdomen2" runat="server" GroupName="grupoabdomen" OnCheckedChanged="rbAbdomen2_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image8" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/cad02.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbCadera2" runat="server" GroupName="grupoCadera" OnCheckedChanged="rbCadera2_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image9" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/muslo02.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbMuslo2" runat="server" GroupName="grupoMuslo" OnCheckedChanged="rbMuslo2_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image10" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/abdomen2.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbLateral2" runat="server" GroupName="grupoLateral" OnCheckedChanged="rbLateral2_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel35" ColumnWidth="12%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="REGULAR 3">
                                            <Items>
                                                <x:Image ID="Image2" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/22.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbAbdomen3" runat="server" GroupName="grupoabdomen" OnCheckedChanged="rbAbdomen3_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image11" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/cad03.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbCadera3" runat="server" GroupName="grupoCadera" OnCheckedChanged="rbCadera3_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image12" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/muslo03.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbMuslo3" runat="server" GroupName="grupoMuslo" OnCheckedChanged="rbMuslo3_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image13" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/abd03.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbLateral3" runat="server" GroupName="grupoLateral" OnCheckedChanged="rbLateral3_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel36" ColumnWidth="12%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="POBRE 4">
                                            <Items>
                                                <x:Image ID="Image4" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/33.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbAbdomen4" runat="server" GroupName="grupoabdomen" OnCheckedChanged="rbAbdomen4_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image14" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/cad04.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbCadera4" runat="server" GroupName="grupoCadera" OnCheckedChanged="rbCadera4_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image15" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/muslo04.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbMuslo4" runat="server" GroupName="grupoMuslo" OnCheckedChanged="rbMuslo4_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image16" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/abd04.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbLateral4" runat="server" GroupName="grupoLateral" OnCheckedChanged="rbLateral4_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel37" ColumnWidth="15%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="PUNTOS">
                                            <Items>
                                                <x:Label ID="Label214" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label215" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtAbdomenPuntos" runat="server" Width="100" Text=""></x:TextBox>
                                                <x:Label ID="Label228" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label229" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label230" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label231" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label232" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtCaderaPuntos" runat="server" Width="100" Text=""></x:TextBox>
                                                <x:Label ID="Label233" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label234" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label235" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label236" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label237" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtMusloPuntos" runat="server" Width="100" Text=""></x:TextBox>
                                                <x:Label ID="Label238" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label239" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label240" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label241" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label242" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtLateralPuntos" runat="server" Width="100" Text=""></x:TextBox>
                                                <x:Label ID="Label246" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label247" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtTotal1" runat="server" Text="" Width="100" ShowLabel="false"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel38" ColumnWidth="37%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="OBSERVACIONES">
                                            <Items>
                                                <x:TextArea ID="txtAbdomenObs" ShowLabel="false" CssClass="mright" runat="server" Width="290" Height="85"></x:TextArea>
                                                <x:Label ID="Label243" runat="server" Text=" "></x:Label>
                                                <x:TextArea ID="txtCaderaObs" ShowLabel="false" CssClass="mright" runat="server" Width="290" Height="85"></x:TextArea>
                                                <x:Label ID="Label244" runat="server" Text=" "></x:Label>
                                                <x:TextArea ID="txtMusloObs" ShowLabel="false" CssClass="mright" runat="server" Width="290" Height="85"></x:TextArea>
                                                <x:Label ID="Label245" runat="server" Text=" "></x:Label>
                                                <x:TextArea ID="txtLateralObs" ShowLabel="false" CssClass="mright" runat="server" Width="290" Height="85"></x:TextArea>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel39" Title="RANGOS ARTICULARES" EnableBackgroundColor="true" Height="490px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items>
                                        <x:Panel ID="Panel40" Width="230px" Height="450px" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="DESCRIPCIÓN">
                                            <Items>
                                                <x:Label ID="Label248" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label249" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox31" runat="server" Text="ABDUCCIÓN DE HOMBRO (NORMAL 0° - 180°)" Width="220"></x:TextBox>
                                                <x:Label ID="Label250" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label251" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label252" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label253" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label254" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox32" runat="server" Text="ABDUCCIÓN DE HOMBRO (NORMAL 0° - 60°)" Width="220"></x:TextBox>
                                                <x:Label ID="Label255" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label256" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label257" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label258" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label259" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox33" runat="server" Text="ROTACIÓN EXTERNA (0° - 90°)" Width="220"></x:TextBox>
                                                <x:Label ID="Label260" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label261" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label262" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label263" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label264" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox34" runat="server" Text="ROTACIÓN EXTERNA DE HOMBRO (INTERNA)" Width="220"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel41" ColumnWidth="13%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="EXCELENTE 1">
                                            <Items>
                                                <x:Image ID="Image17" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/ab01.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombroA180_1" runat="server" GroupName="grupoHombro180" AutoPostBack="true" OnCheckedChanged="rbHombroA180_1_CheckedChanged"></x:RadioButton>
                                                <x:Image ID="Image18" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/ad01.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombroB1801_1" runat="server" GroupName="grupoHombro60" AutoPostBack="true" OnCheckedChanged="rbHombroB1801_1_CheckedChanged"></x:RadioButton>
                                                <x:Image ID="Image19" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/rot9001.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombro90_1" runat="server" GroupName="grupoHombro90" OnCheckedChanged="rbHombro90_1_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                                <x:Image ID="Image20" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/rotet01.jpg">
                                                </x:Image>

                                                <x:RadioButton ID="rbHombroInternal_1" runat="server" GroupName="grupoHombroInternal" OnCheckedChanged="rbHombroInternal_1_CheckedChanged" AutoPostBack="true"></x:RadioButton>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel42" ColumnWidth="13%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="PROMEDIO 2">
                                            <Items>
                                                <x:Image ID="Image21" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/ab02.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombroA180_2" runat="server" GroupName="grupoHombro180" AutoPostBack="true" OnCheckedChanged="rbHombroA180_2_CheckedChanged"></x:RadioButton>
                                                <x:Image ID="Image22" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/ad02.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombroB1801_2" runat="server" GroupName="grupoHombro60" AutoPostBack="true" OnCheckedChanged="rbHombroB1801_2_CheckedChanged"></x:RadioButton>
                                                <x:Image ID="Image23" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/rot9002.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombro90_2" runat="server" GroupName="grupoHombro90" AutoPostBack="true" OnCheckedChanged="rbHombro90_2_CheckedChanged"></x:RadioButton>
                                                <x:Image ID="Image24" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/rotet02.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombroInternal_2" runat="server" GroupName="grupoHombroInternal" AutoPostBack="true" OnCheckedChanged="rbHombroInternal_2_CheckedChanged"></x:RadioButton>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel43" ColumnWidth="13%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="REGULAR 3">
                                            <Items>
                                                <x:Image ID="Image25" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/ab03.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombroA180_3" runat="server" GroupName="grupoHombro180" AutoPostBack="true" OnCheckedChanged="rbHombroA180_3_CheckedChanged"></x:RadioButton>
                                                <x:Image ID="Image26" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/ad03.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombroB1801_3" runat="server" GroupName="grupoHombro60" AutoPostBack="true" OnCheckedChanged="rbHombroB1801_3_CheckedChanged"></x:RadioButton>
                                                <x:Image ID="Image27" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/rot9003.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombro90_3" runat="server" GroupName="grupoHombro90" AutoPostBack="true" OnCheckedChanged="rbHombro90_3_CheckedChanged"></x:RadioButton>
                                                <x:Image ID="Image28" runat="server" ImageWidth="70" ImageHeight="70" ImageCssStyle="border:solid 1px #ccc;padding:5px;" ImageUrl="../images/Osteo/rotet03.jpg">
                                                </x:Image>
                                                <x:RadioButton ID="rbHombroInternal_3" runat="server" GroupName="grupoHombroInternal" AutoPostBack="true" OnCheckedChanged="rbHombroInternal_3_CheckedChanged"></x:RadioButton>
                                            </Items>
                                        </x:Panel>

                                        <x:Panel ID="Panel45" ColumnWidth="15%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="PUNTOS">
                                            <Items>
                                                <x:Label ID="Label265" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label266" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtAbduccionHombro180Puntos" runat="server" Width="100" Text=""></x:TextBox>
                                                <x:Label ID="Label267" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label268" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label269" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label270" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label271" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtAduccionHombro60Puntos" runat="server" Width="100" Text=""></x:TextBox>
                                                <x:Label ID="Label272" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label273" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label274" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label275" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label276" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtRotacionHombro90Puntos" runat="server" Width="100" Text=""></x:TextBox>
                                                <x:Label ID="Label277" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label278" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label279" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label280" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label281" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtInternaHombroPuntos" runat="server" Width="100" Text=""></x:TextBox>
                                                <x:Label ID="Label282" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label283" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="txtTotal2" runat="server" Text="" Width="100" ShowLabel="false"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel46" ColumnWidth="46%" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Height="450px" Title="DOLOR CONTRA RESISTENCIA">
                                            <Items>
                                                <x:Label ID="Label349" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label348" runat="server" Text=" "></x:Label>
                                                <x:CheckBox ID="chkHombro180SI" runat="server" Text="SI"></x:CheckBox>
                                                <%--<x:RadioButton ID="rbHombro180SI" GroupName="hombro180" runat="server" Text="SI"></x:RadioButton>--%>
                                                <%--<x:RadioButton ID="rbHombro180NO" GroupName="hombro180" runat="server" Text="NO"></x:RadioButton>--%>
                                                <x:Label ID="Label284" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label350" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label285" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label286" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label361" runat="server" Text=" "></x:Label>
                                                <x:CheckBox ID="chkHombro60SI" runat="server" Text="SI"></x:CheckBox>
                                                <%--<x:RadioButton ID="rbHombro60SI" GroupName="hombro60" runat="server" Text="SI"></x:RadioButton>--%>
                                                <%--<x:RadioButton ID="rbHombro60NO" GroupName="hombro60" runat="server" Text="NO"></x:RadioButton>--%>
                                                <x:Label ID="Label351" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label352" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label353" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label354" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label362" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label364" runat="server" Text=" "></x:Label>
                                                <x:CheckBox ID="chkHombro90SI" runat="server" Text="SI"></x:CheckBox>
                                                <%--<x:RadioButton ID="rbHombro90SI" GroupName="hombro90" runat="server" Text="SI"></x:RadioButton>--%>
                                                <%--<x:RadioButton ID="rbHombro90NO" GroupName="hombro90" runat="server" Text="NO"></x:RadioButton>--%>
                                                <x:Label ID="Label355" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label356" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label357" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label358" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label363" runat="server" Text=" "></x:Label>
                                                <x:CheckBox ID="chkHombroInternoSI" runat="server" Text="SI"></x:CheckBox>
                                                <%--<x:RadioButton ID="rbHombroInternoSI" GroupName="hombrointerno" runat="server" Text="SI"></x:RadioButton>--%>
                                                <%--<x:RadioButton ID="rbHombroInternoNO" GroupName="hombrointerno" runat="server" Text="NO"></x:RadioButton>--%>
                                                <x:Label ID="Label359" runat="server" Text=" "></x:Label>
                                                <x:Label ID="Label360" runat="server" Text=" "></x:Label>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel44" Title="OBSERVACIONES" EnableBackgroundColor="true" Height="50px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form19" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow49" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:TextArea ID="txtObservaciones" ShowLabel="false" CssClass="mright" runat="server" Width="950" Height="40"></x:TextArea>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel47" Title="EVALUACIÓN DE MIEMBROS SUPERIORES E INFERIORES: MOVILIDAD" EnableBackgroundColor="true" Height="350px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form32" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow113" ColumnWidths="140px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label287" runat="server" Text="ARTICULACIÓN" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label288" runat="server" Text="ABDUCCIÓN" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label289" runat="server" Text="ADUCCIÓN" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label290" runat="server" Text="FLEXIÓN" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label291" runat="server" Text="EXTENSIÓN" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label292" runat="server" Text="R.EXTERNA" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label293" runat="server" Text="R.INTERNA" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label294" runat="server" Text="FUERZA/TONO" ShowLabel="false"></x:Label>
                                                        <x:Label ID="Label295" runat="server" Text="DOLOR" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow114" ColumnWidths=" 139px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label296" runat="server" Text="HOMBRO DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlHD1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHD2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHD3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHD4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHD5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHD6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHD7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHD8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow115" ColumnWidths=" 140px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label297" runat="server" Text="HOMBRO IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlHI1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHI2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHI3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHI4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHI5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHI6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHI7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlHI8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>

                                                <x:FormRow ID="FormRow117" ColumnWidths="  139px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label298" runat="server" Text="CODO DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCD1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCD2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCD3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCD4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCD5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCD6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCD7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCD8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow118" ColumnWidths=" 140px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label299" runat="server" Text="CODO IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCI1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCI2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCI3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCI4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCI5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCI6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCI7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCI8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>

                                                <x:FormRow ID="FormRow120" ColumnWidths="  139px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label300" runat="server" Text="MUÑECA DERECHA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlMuneD1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneD2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneD3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneD4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneD5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneD6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneD7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneD8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow121" ColumnWidths=" 140px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label301" runat="server" Text="MUÑECA IZQUIERDA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlMuneI1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneI2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneI3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneI4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneI5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneI6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneI7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlMuneI8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>

                                                <x:FormRow ID="FormRow123" ColumnWidths="  139px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label302" runat="server" Text="CADERA DERCHA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCaderaD1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaD2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaD3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaD4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaD5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaD6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaD7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaD8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow124" ColumnWidths=" 140px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label303" runat="server" Text="CADERA IZQUIERDA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCaderaI1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaI2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaI3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaI4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaI5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaI6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaI7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlCaderaI8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>

                                                <x:FormRow ID="FormRow126" ColumnWidths="  139px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label304" runat="server" Text="TOBILLO DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTobilloD1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloD2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloD3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloD4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloD5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloD6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloD7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloD8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow127" ColumnWidths=" 140px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label305" runat="server" Text="TOBILLO IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTobilloI1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloI2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloI3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloI4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloI5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloI6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloI7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlTobilloI8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>

                                                <x:FormRow ID="FormRow129" ColumnWidths="  139px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label306" runat="server" Text="RODILLA DERECHA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlRodillaD1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaD2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaD3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaD4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaD5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaD6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaD7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaD8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow130" ColumnWidths=" 140px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label307" runat="server" Text="RODILLA IZQUIERDA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlRodillaI1" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaI2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaI3" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaI4" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaI5" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaI6" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaI7" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                        <x:DropDownList ID="ddlRodillaI8" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel48" Title="OBSERVACIONES" EnableBackgroundColor="true" Height="50px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form20" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow50" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:TextArea ID="txtobservacionOsteo1" ShowLabel="false" CssClass="mright" runat="server" Width="950" Height="40"></x:TextArea>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel49" Title="MANIOBRAS" EnableBackgroundColor="true" Height="190px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form21" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow51" ColumnWidths="150px 240px  160px 240px  " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label308" runat="server" Text="LASEGUE DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlLasegueDere" runat="server" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label309" runat="server" Text="LASEGUE IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlLasegueIzq" runat="server" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow52" ColumnWidths="149px 240px  160px 240px  " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label310" runat="server" Text="ADAM DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddladam_derecho" runat="server" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label311" runat="server" Text="ADAM IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddladam_izquierdo" runat="server" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow53" ColumnWidths="150px 240px  160px 240px  " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label312" runat="server" Text="PHALEN DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlphalen_derecho" runat="server" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label313" runat="server" Text="PHALEN IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlphalen_izquierdo" runat="server" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow54" ColumnWidths="149px 240px  160px 240px  " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label314" runat="server" Text="TINEL DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddltinel_derecho" runat="server" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label315" runat="server" Text="TINEL IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddltinel_izquierdo" runat="server" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow55" ColumnWidths="150px 240px  160px 240px  " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label316" runat="server" Text="FINKELSTEIN DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlfinkelstein_derecho" runat="server" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label317" runat="server" Text="FINKELSTEIN IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlfinkelstein_izquierdo" runat="server" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow56" ColumnWidths="149px 240px  160px 240px  " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label318" runat="server" Text="PIE CAVO DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlpie_cavo_derecho" runat="server" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label319" runat="server" Text="PIE CAVO IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlpie_cavo_izquierdo" runat="server" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow57" ColumnWidths="150px 240px  160px 240px  " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label320" runat="server" Text="PIE PLANO DERECHO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlpie_plano_derecho" runat="server" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label321" runat="server" Text="PIE PLANO  IZQUIERDO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlpie_plano_izquierdo" runat="server" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel50" Title="EVALUACIÓN DE COLUMNA VERTEBRAL" EnableBackgroundColor="true" Height="420px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="Eje Antero Posterior" ID="GroupPanel13" BoxFlex="1" Height="120" TableColspan="1" Width="940px">
                                            <Items>
                                                <x:Form ID="Form27" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow58" ColumnWidths=" 400px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label322" runat="server" Text="CURVAS FISIOLÓGICAS (ant-post)" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label323" runat="server" Text="Eje Lateral" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow99" ColumnWidths="100px   300px  100px  300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label324" runat="server" Text="CERVICAL" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlcervical1" runat="server" Width="250" ShowLabel="false"></x:DropDownList>

                                                                <x:Label ID="Label325" runat="server" Text="DORSAL" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddldorsal1" runat="server" Width="250" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow101" ColumnWidths="99px   300px  100px  300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label326" runat="server" Text="DORSAL" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddldorsal2" runat="server" Width="250" ShowLabel="false"></x:DropDownList>

                                                                <x:Label ID="Label327" runat="server" Text="LUMBAR" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddllumbar1" runat="server" Width="250" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow102" ColumnWidths="100px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label328" runat="server" Text="LUMBAR" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddllumbar2" runat="server" Width="250" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="MOVILIDAD-DOLOR (valorar según tabla1)" ID="GroupPanel14" BoxFlex="1" Height="110" TableColspan="1">
                                            <Items>
                                                <x:Form ID="Form22" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow103" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label329" runat="server" Text="COLUMNA VERTEBRAL" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label330" runat="server" Text="FLEXIÓN" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label331" runat="server" Text="EXTENSIÓN" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label332" runat="server" Text="LATERIZACIÓN DERECHA" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label333" runat="server" Text="LATERIZACIÓN IQUIERDA" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label334" runat="server" Text="ROTACIÓN DERECHA" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label335" runat="server" Text="ROTACIÓN IXQUIERDA" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label336" runat="server" Text="IRRADICACIÓN" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow104" ColumnWidths="880px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label337" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow59" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label338" runat="server" Text="CERVICAL" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlcervical2" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlcervical_extenxion" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlcervical_lateralizacion_derecha" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlcervical_lateralizacion_izquierda" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlcervical_rotacion_derecha" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlcervical_rotacion_izquierda" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlcervical_irradiacion" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow105" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label339" runat="server" Text="DORSO LUMBAR" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddldorsallumbar" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddldorsallumbar_extension" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddldorsallumbar_lateral_derecha" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddldorsallumbar_lateral_izquierda" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddldorsallumbar_roacion_derecha" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddldorsallumbar_roacion_izquierda" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddldorsallumbar_irradiacion" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                        <x:GroupPanel runat="server" Title="PALPITACIÓN" ID="GroupPanel18" BoxFlex="1" Height="110" TableColspan="1">
                                            <Items>
                                                <x:Form ID="Form31" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow109" ColumnWidths="200px  300px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label340" runat="server" Text=". " ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label341" runat="server" Text="APÓFISIS ESPINOSAS" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label342" runat="server" Text="CONTRACTURA MUSCULAR" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow110" ColumnWidths="200px  300px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label343" runat="server" Text="COLUMNA CERVICAL" ShowLabel="false"></x:Label>
                                                                <%--<x:CheckBox ID="chkColumnaCervicalApofisis" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                                <x:DropDownList ID="ddlColumnaCervicalApofisis" runat="server" ShowLabel="false" Width="80"></x:DropDownList>
                                                                <%--<x:CheckBox ID="chkColumnaCervicalContractura" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                                <x:DropDownList ID="ddlColumnaCervicalContractura" runat="server" ShowLabel="false" Width="80"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow111" ColumnWidths="199px  300px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label344" runat="server" Text="COLUMNA DORSAL" ShowLabel="false"></x:Label>
                                                                <%--<x:CheckBox ID="chkColumnaDorsalApofisis" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                                <x:DropDownList ID="ddlColumnaDorsalApofisis" runat="server" ShowLabel="false" Width="80"></x:DropDownList>
                                                                <%--<x:CheckBox ID="chkColumnaDorsalContractura" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                                <x:DropDownList ID="ddlColumnaDorsalContractura" runat="server" ShowLabel="false" Width="80"></x:DropDownList>

                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow112" ColumnWidths="198px  300px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label345" runat="server" Text="COLUMNA LUMBAR" ShowLabel="false"></x:Label>
                                                                <%--<x:CheckBox ID="chkColumnaLumbarApofisis" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                                <x:DropDownList ID="ddlColumnaLumbarApofisis" runat="server" ShowLabel="false" Width="80"></x:DropDownList>
                                                                <%--<x:CheckBox ID="chkColumnaLumbarContractura" runat="server" Text="" ShowLabel="false"></x:CheckBox>--%>
                                                                <x:DropDownList ID="ddlColumnaLumbarContractura" runat="server" ShowLabel="false" Width="80"></x:DropDownList>

                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel51" Title="CONCLUSIONES" EnableBackgroundColor="true" Height="50px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form23" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow60" ColumnWidths="590px 160px 30px 70px 100px" runat="server">
                                                    <Items>
                                                        <x:TextArea ID="txtdescripcion" ShowLabel="false" CssClass="mright" runat="server" Width="580" Height="40"></x:TextArea>
                                                        <x:Label ID="label346" runat="server" Text="EXAMEN NORMAL" BoxMargin="0px" ShowLabel="false" OffsetRight="5px"></x:Label>
                                                        <x:CheckBox ID="chkevaluacion_normal" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                        <x:Label ID="Label347" runat="server" Text="APTITUD" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlaptitudOsteo" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Tab>

                        <x:Tab ID="TabAlturaEstructural" BodyPadding="5px" Title="Altura Estructural 1-8" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar3" runat="server">
                                    <Items>
                                        <x:Button ID="btnAlturaEsctructural" Text="Grabar Osteomuscula" Icon="SystemSave" runat="server" OnClick="btnAlturaEsctructural_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Button ID="btnAlturaReporte" Text="Ver Reporte" runat="server" Icon="PageWhiteText" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel56" Title="ANTECEDENTES PERSONALES  Y FAMILIARES, EVALUACIÓN  PSICONEUROLÓGICA" EnableBackgroundColor="true" Height="310px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items>
                                        <x:Panel ID="Panel53" Width="300px" Height="270px" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="ANTECEDENTES">
                                            <Items>
                                                <x:TextBox ID="label365" runat="server" Text="ANTECEDENTES DE TEC" ShowLabel="false" Width="290"></x:TextBox>
                                                <x:Label ID="label372" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="label366" runat="server" Text="CONVULSIONES / EPILEPSIA" ShowLabel="false" Width="290"></x:TextBox>
                                                <x:Label ID="label373" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="label367" runat="server" Text="MAREOS, MIOCLONIAS,  ACATISIA" ShowLabel="false" Width="290"></x:TextBox>
                                                <x:Label ID="label374" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="label368" runat="server" Text="AGAROFOBIA" ShowLabel="false" Width="290"></x:TextBox>
                                                <x:Label ID="label375" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="label369" runat="server" Text="ACROFOBIA" ShowLabel="false" Width="290"></x:TextBox>
                                                <x:Label ID="label376" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="label370" runat="server" Text="INSUFICIENCIA  CARDÍACA" ShowLabel="false" Width="290"></x:TextBox>
                                                <x:Label ID="label377" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="label371" runat="server" Text="ESTEREOPSIA  ALTERADA" ShowLabel="false" Width="290"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel54" Width="140px" Height="270px" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="SI-NO">
                                            <Items>
                                                <x:DropDownList ID="ddlAnteTEC" runat="server" Width="120" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="label378" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:DropDownList ID="ddlConvulsiones" runat="server" Width="120" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="label379" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:DropDownList ID="ddlMareosMioclo" runat="server" Width="120" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="label380" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:DropDownList ID="ddlAgarofobia" runat="server" Width="120" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="label381" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:DropDownList ID="ddlAcrofobia" runat="server" Width="120" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="label382" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:DropDownList ID="ddlInsuficiCardiaca" runat="server" Width="120" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="label383" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:DropDownList ID="ddlEstereopsiaAlterada" runat="server" Width="120" ShowLabel="false"></x:DropDownList>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel55" Width="520px" Height="270px" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="OBSERVACIONES">
                                            <Items>
                                                <x:TextBox ID="txtAnteTEC" runat="server" Text="" ShowLabel="false" Width="510"></x:TextBox>
                                                <x:Label ID="label384" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtConvulsiones" runat="server" Text="" ShowLabel="false" Width="510"></x:TextBox>
                                                <x:Label ID="label385" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtMareosMioclo" runat="server" Text="" ShowLabel="false" Width="510"></x:TextBox>
                                                <x:Label ID="label386" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtAgarofobia" runat="server" Text="" ShowLabel="false" Width="510"></x:TextBox>
                                                <x:Label ID="label387" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtAcrofobia" runat="server" Text="" ShowLabel="false" Width="510"></x:TextBox>
                                                <x:Label ID="label388" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtInsuficiCardiaca" runat="server" Text="" ShowLabel="false" Width="510"></x:TextBox>
                                                <x:Label ID="label389" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtEstereopsiaAlterada" runat="server" Text="" ShowLabel="false" Width="510"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel52" Title="EXAMEN MÉDICO DIRIGIDO" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form24" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow61" ColumnWidths="135px  120px  130px  120px  140px  140px  140px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label390" runat="server" Text="EXAMEN CLÍNICO" ShowLabel="false" Readonly="true"></x:Label>
                                                        <x:TextBox ID="txtTalla_18" runat="server" Text="" Width="40" Label="TALLA(m)" Readonly="true"></x:TextBox>
                                                        <x:TextBox ID="txtPeso_18" runat="server" Text="" Width="45" Label="PESO(kg.)" Readonly="true"></x:TextBox>
                                                        <x:TextBox ID="txtIMC_18" runat="server" Text="" Width="40" Label="IMC" Readonly="true"></x:TextBox>
                                                        <x:TextBox ID="txtPA_18" runat="server" Text="" Width="70" Label="P.A.S-P.A.D" Readonly="true"></x:TextBox>
                                                        <x:TextBox ID="txtFreRes_18" runat="server" Text="" Width="50" Label="F.RESP" Readonly="true"></x:TextBox>
                                                        <x:TextBox ID="txtFreCar_18" runat="server" Text="" Width="50" Label="F.CARD" Readonly="true"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel57" Title="EXAMEN DE LOS OJOS" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form25" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow62" ColumnWidths="180px  50px  180px  50px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label391" runat="server" Text="NISTAGMUS ESPONTÁNEO" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkNistagmusEspontaneo" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                        <x:Label ID="label392" runat="server" Text="NISTAGMUS PROVOCADO" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkNistagmusProvocado" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel58" Title=" ENTRENAMIENTO" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form26" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow63" ColumnWidths="320px  50px  350px  50px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label393" runat="server" Text="¿RECIBIÓ ENTRENAMIENTO EN PRIMEROS AUXILIOS?" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkPrimerosAuxi" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                        <x:Label ID="label394" runat="server" Text="¿RECIBIÓ ENTRENAMIENTO PARA TRABAJOS SOBRE NIVEL?" ShowLabel="false"></x:Label>
                                                        <x:CheckBox ID="chkTrabjNivel" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel59" Title=" CUANTIFICAR  LOS SIGUIENTES ÍTEMS" EnableBackgroundColor="true" Height="250px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form28" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow64" ColumnWidths="400px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label395" runat="server" Text="TÍMPANOS" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTimpanos" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow65" ColumnWidths="399px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label396" runat="server" Text="EQUILIBRIO" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEquilibrio" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow66" ColumnWidths="400px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label397" runat="server" Text="SUSTENTACIÓN EN PIE POR 20" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSustentacion" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow67" ColumnWidths="399px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label398" runat="server" Text="CAMINAR LIBRE SOBRE RECTA 3m (SIN DESVÍO)" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCaminarRecta" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow68" ColumnWidths="400px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label399" runat="server" Text="CAMINAR LIBRE A OJOS VENDADOS 3m (SIN DESVÍO)" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCaminarOjosVendados" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow69" ColumnWidths="399px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label400" runat="server" Text="CAMINAR LIBRE A OJOS VENDADOS PUNTA TALÓN 3m (SIN DESVÍO)" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCaminarPuntaTalon" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow70" ColumnWidths="400px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label401" runat="server" Text="ROTAR SILLA ROTATORIA Y LUEGO VERIFICAR EQUILIBRIO DE PIE" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlRotarSilla" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow71" ColumnWidths="399px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label402" runat="server" Text="ADIADOCOQUINESIA DIRECTA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAdiadocoquinesiaDirecta" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow72" ColumnWidths="400px  120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label403" runat="server" Text="ADIADOCOQUINESIA CRUZADA" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAdiadocoquinesiaCruzada" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel60" Title="APTITUD" EnableBackgroundColor="true" Height="30px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:DropDownList ID="ddlAptitudAltura18" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel61" Title="RESULTADO FINAL" EnableBackgroundColor="true" Height="50px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:TextArea ID="txtResultadoAltura18" ShowLabel="false" CssClass="mright" runat="server" Width="940" Height="40"></x:TextArea>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Tab>
                        <x:Tab ID="TabFototipo" BodyPadding="5px" Title="Fototipo" runat="server" Hidden="false">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar24" runat="server">
                                    <Items>
                                        <x:Button ID="btngrabarfoto" Text="Grabar Fototipo" Icon="SystemSave" runat="server" AjaxLoadingType="Mask" OnClick="btngrabarfoto_Click"></x:Button>

                                        <x:Label ID="Label729" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label730" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlGrabarUsuarioFototipo" runat="server"></x:DropDownList>
                                        <x:Button ID="Button2" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                                <Items>   
                                    <x:Panel ID="Panel90" EnableBackgroundColor="true" Title="Fototipo" Height="920px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                        <Items>
                                            <x:Form ID="Form59" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow214" ColumnWidths="240px 720px 240px 240px" runat="server">
                                                    <Items>
                                                        <x:ContentPanel ID="ContentPanel1" runat="server" Width="950px" BodyPadding="0px" EnableBackgroundColor="true" ShowBorder="false" ShowHeader="false">
                                                    <div>
                                                        <div id="rellenoFondo" ></div>
                                                        <div class="botones" id="btn1" onclick="cambioColor('fffc6d')"></div>
                                                        <div class="botones" id="btn2" onclick="cambioColor('4286f4')"></div>
                                                        <div class="botones" id="btn3" onclick="cambioColor('f44171')"></div>
                                                        <div class="botones" id="btn4" onclick="cambioColor('25dd69')"></div>
                                                        <div class="botones" onclick="cargarDibujo()"><img src="../images/Fototipo/clean.png" style="width:32px"/></div>
                                                        <div class="botones" onclick="mostrarimagen()"><img src="../images/Fototipo/reload.png" style="width:35px"/></div>
                                                        <div><x:TextBox runat="server" ID="txtMultimediaFileId_Inter" Hidden="true"/></div>
                                                        <div><x:TextBox runat="server" ID="txtServiceComponentMultimediaId_Inter" Hidden="true" /></div>
                                                        <div><x:TextArea Width="500" Height="500" ID="texturl" runat="server" Hidden="true"></x:TextArea></div>                                          
                                                    </div>                        
                                                    <div>
                                                        <canvas id="imgCanvas" width="800" height="800"></canvas> 
                                                    </div>
                                                        </x:ContentPanel>   
                                                    </Items>  
                                                </x:FormRow> 
                                            </Rows>
                                            </x:Form>
                                        </Items>                                      
                                    </x:Panel>
                                                                                         
                            </Items>
                        </x:Tab>
                        <x:Tab ID="TabAltura18_Internacional" BodyPadding="5px" Title="Examen Altura 1.8" runat="server" Hidden="false">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar20" runat="server">
                                    <Items>
                                        <x:Button ID="btnAltura18_Internacional" Text="Grabar Altura 1.8" Icon="SystemSave" runat="server" OnClick="btnAltura18_Internacional_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Label ID="Label749" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label750" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabaAltura" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporteAltura18C" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel74" Title="1. ACTIVIDAD: " EnableBackgroundColor="true" Height="40px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form128" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow440" ColumnWidths="240px 720px 240px 240px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label608" runat="server" Text="Actividad a realizar:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAltura1_8Activiad_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="240"></x:TextBox>
                                                        <x:Label ID="label609" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="label610" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel75" Title="2.  ANTECEDENTES PERSONALES Y FAMILIARES: " EnableBackgroundColor="true" Height="305px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form129" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow473" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label646" runat="server" Text="a.  Personales: " ShowLabel="false"></x:Label>

                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow441" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label611" runat="server" Text="Trabajos previos sobre nivel, uso de arnés?" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAltura1_8Trabajos_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow443" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label612" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow444" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label613" runat="server" Text="Cardiovasculares y respiratorios:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAltura1_8Cardiovasc_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow445" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label614" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow449" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label615" runat="server" Text="Quirúrgicos: " ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAltura1_8Quirurg_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow450" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label631" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow451" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label632" runat="server" Text="Fobias (acrofobia, agarofobia) :" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAltura1_8Fobias_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow452" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label633" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow453" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label634" runat="server" Text="Antecedentes de uso o abuso de alcohol y drogas:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAltura1_8AntecedAlcohol_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow457" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label635" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow458" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label636" runat="server" Text="Fármacos de consumo actual:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAltura1_8FarmacoActual_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow459" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label637" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow460" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label638" runat="server" Text="b.  Familiares: " ShowLabel="false"></x:Label>

                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow465" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label639" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow466" ColumnWidths="240px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label640" runat="server" Text="Psiquiátricos:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtAltura1_8AntecedPsiquiat_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow467" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label641" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel76" Title="3.  EXAMEN MÉDICO DIRIGIDO:" EnableBackgroundColor="true" Height="755px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form130" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow468" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:Form ID="Form131" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="215px" LabelAlign="Left">
                                                            <Rows>
                                                                <x:FormRow ID="FormRow469" ColumnWidths="320px  320px  320px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="txtAltura18FC_Internacional" runat="server" Text="" Width="50" Label="Frecuencia Cardiaca(Latx min)" Enabled="false"></x:TextBox>
                                                                        <x:TextBox ID="txtAltura18PS_Internacional" runat="server" Text="" Width="50" Label="Presión Arterial Sistólica(mm Hg.)" Enabled="false"></x:TextBox>
                                                                        <x:TextBox ID="txtAltura18PD_Internacional" runat="server" Text="" Width="50" Label="Presión Arterial Diastólica(mm Hg.)" Enabled="false"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow470" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label642" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow471" ColumnWidths="320px  320px  320px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="txtAltura18FR_Internacional" runat="server" Text="" Width="50" Label="Frecuencia Respiratoria(Resp x min)" Enabled="false"></x:TextBox>
                                                                        <x:TextBox ID="txtAltura18PESO_Internacional" runat="server" Text="" Width="50" Label="Peso (Kg)" Enabled="false"></x:TextBox>
                                                                        <x:TextBox ID="txtAltura18TALLA_Internacional" runat="server" Text="" Width="50" Label="Talla (m)" Enabled="false"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow486" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label653" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow472" ColumnWidths="240px 720px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label643" runat="server" Text="Aparato Cardiovascular:" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtAltura1_8ExamenCardiovasc_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow478" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label644" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow479" ColumnWidths="240px 720px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label645" runat="server" Text="Aparato Respiratorio::" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtAltura1_8ExamenRespiratorio_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow480" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label647" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow481" ColumnWidths="240px 720px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label648" runat="server" Text="Sistema Nervioso:" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtAltura1_8ExamenNervioso_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                                    </Items>

                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow483" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label650" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow482" ColumnWidths="240px 720px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label649" runat="server" Text="Nistagmus Espontáneo:" ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8Nistagmus_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label654" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label655" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow484" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label651" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow485" ColumnWidths="240px 720px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label652" runat="server" Text="Manifestaciones o estigmas sugestivos de alcoholismo:" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtAltura1_8Manifestaciones_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                                    </Items>

                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow487" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label656" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow488" ColumnWidths="240px 720px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label657" runat="server" Text="a.  ¿Recibió entrenamiento en Primeros Auxilios?  " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8PrimerosAux_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label658" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label659" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow489" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label660" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow490" ColumnWidths="240px 720px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label661" runat="server" Text="b.  Cuantificar los Siguientes Ítems: " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label664" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label662" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label663" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow491" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label665" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow492" ColumnWidths="480px 240px 120px 120px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label666" runat="server" Text=" DESCRIPCION DE ITEMS " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label668" runat="server" Text="SI/NO" ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label667" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label669" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow493" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label670" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow494" ColumnWidths="480px 240px 120px 120px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label671" runat="server" Text=" 1.  TIMPANOS NORMALES  " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8Timpanos_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label672" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label673" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow495" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label675" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow496" ColumnWidths="480px 240px 120px 120px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label674" runat="server" Text=" 2.  EQUILIBRIO NORMAL  " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8Equilibrio_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label676" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label677" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow497" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label678" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow498" ColumnWidths="480px 240px 120px 120px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label679" runat="server" Text=" 3.  SUSTENTACIÓN EN PIE POR 20’’  " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8Sustentacion_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label680" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label681" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow499" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label682" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow500" ColumnWidths="480px 240px 120px 120px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label683" runat="server" Text=" 4.  CAMINAR LIBRE SOBRE RECTA (SIN DESVÍO)  " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8Caminar_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label684" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label685" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow501" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label686" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow502" ColumnWidths="480px 240px 120px 120px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label687" runat="server" Text="5.  ADIADOCOCINESIA DIRECTA " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8Adiadococinesia_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label688" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label689" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow503" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label690" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow504" ColumnWidths="480px 240px 120px 120px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label691" runat="server" Text=" 6.  INDICE-NARIZ/TALON-RODILLA  " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8IndiceNariz_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label692" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label693" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow505" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label694" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow506" ColumnWidths="480px 240px 120px 120px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label695" runat="server" Text="7.  RECIBIO CURSO DE SEGURIDAD PARA TRABAJO EN ALTURA MAYOR 1.8m  " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8RecibioCurso_Internacional" runat="server" Width="90" ShowLabel="false"></x:DropDownList>
                                                                        <x:Label ID="label696" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                        <x:Label ID="label697" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow507" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label698" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow508" ColumnWidths="240px 720px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label699" runat="server" Text=" Resultado de: " ShowLabel="false"></x:Label>

                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow509" ColumnWidths="240px 720px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label700" runat="server" Text="1.  Electrocardiograma:" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtElectrocardioAltura_CI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow510" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label701" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow511" ColumnWidths="240px 120px 220px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label702" runat="server" Text="2.  Colesterol total: " ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtColesterolAltura_CI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="220"></x:TextBox>
                                                                        <x:Label ID="label725" runat="server" Text="mg/dl" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow512" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label703" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow513" ColumnWidths="240px 120px 220px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label704" runat="server" Text="3.  Triglicéridos:" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtTrigliceridoAltura_CI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                                        <x:Label ID="label727" runat="server" Text="mg/dl" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow514" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label705" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow515" ColumnWidths="240px 720px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label706" runat="server" Text="Aptitud:   " ShowLabel="false"></x:Label>
                                                                        <x:DropDownList ID="ddlAltura1_8Aptitud_Internacional" runat="server" Width="250" ShowLabel="false"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow516" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label707" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow517" ColumnWidths="240px 720px" runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label708" runat="server" Text="OBSERVACIONES:" ShowLabel="false"></x:Label>
                                                                        <x:TextBox ID="txtAltura1_8Observaciones_Internacional" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow518" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                                    <Items>
                                                                        <x:Label ID="label709" runat="server" Text="" ShowLabel="false"></x:Label>
                                                                    </Items>
                                                                </x:FormRow>
                                                            </Rows>
                                                        </x:Form>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>


                                <x:Panel ID="Panel86" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form54" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow198" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtAlturaCIAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAlturaCIAuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAlturaCIActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow199" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtAlturaCIEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAlturaCIEvaluadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAlturaCIEvaluadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                                <Ext:FormRow ID="FormRow210" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtAlturaCIInformador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAlturaCIInformadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtAlturaCIInformadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Tab>

                        <x:Tab ID="Tab7D" BodyPadding="5px" Title="7 D" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar5" runat="server">
                                    <Items>
                                        <x:Button ID="btn7D" Text="Grabar 7D" Icon="SystemSave" runat="server" OnClick="btn7D_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Label ID="Label751" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label752" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGraba7D" runat="server"></x:DropDownList>
                                        <x:Button ID="btn7D_CI" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel63" Title="ACTIVIDAD A REALIZAR" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:TextBox ID="txtActividadRealizar_7D" runat="server" Text="" ShowLabel="false" Width="500"></x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel62" Title="FUNCIONES VITALES" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form30" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow78" ColumnWidths="50px  100px  120px  100px  50px 100px 50px 100px 50px 100px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label425" runat="server" Text="F.CARD" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtFc_7D" runat="server" Text="" Width="50" Label="" Readonly="true" ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label426" runat="server" Text="P.A.S-P.A.D" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtPAS_7D" runat="server" Text="" Width="70" Label="" Readonly="true" ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label427" runat="server" Text="F.RESP" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtFR_7D" runat="server" Text="" Width="50" Label="" Readonly="true" ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label428" runat="server" Text="IMC" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtIMC_7D" runat="server" Text="" Width="40" Label="" Readonly="true" ShowLabel="false"></x:TextBox>
                                                        <x:Label ID="label429" runat="server" Text="SAT" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtSAT_7D" runat="server" Text="" Width="45" Label="" Readonly="true" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel64" Title="EL / LA PRESENTA O HA PRESENTADO EN LOS ÚLTIMOS 6 MESES" EnableBackgroundColor="true" Height="620px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Column">
                                    <Items>
                                        <x:Panel ID="Panel65" Width="350px" Height="580px" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="ENFERMEDAD">
                                            <Items>
                                                <x:TextBox ID="TextBox35" runat="server" Text="ANEMIA" Width="290"></x:TextBox>
                                                <x:Label ID="Label430" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox36" runat="server" Text="CIRUGÍA MAYOR RECIENTE" Width="290"></x:TextBox>
                                                <x:Label ID="Label431" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox37" runat="server" Text="DESÓRDENES DE LA COAGULACIÓN, TROMBOSIS, ETC." Width="290"></x:TextBox>
                                                <x:Label ID="Label432" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox38" runat="server" Text="DIABETES MELLITUS" Width="290"></x:TextBox>
                                                <x:Label ID="Label433" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox39" runat="server" Text="HIPERTENSIÓN ARTERIAL" Width="290"></x:TextBox>
                                                <x:Label ID="Label434" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox40" runat="server" Text="EMBARAZO" Width="290"></x:TextBox>
                                                <x:Label ID="Label435" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox41" runat="server" Text="PROBLEMAS NEUROLÓGICOS: EPILEPSIA, VÉRTIGO, ETC." Width="290"></x:TextBox>
                                                <x:Label ID="Label436" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox42" runat="server" Text="INFECCIONES RECIENTES (ESPECIALMENTE OÍDOS, NARIZ, GARGANTA)" Width="290"></x:TextBox>
                                                <x:Label ID="Label437" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox43" runat="server" Text="PROBLEMAS CARDÍACOS: MARCAPASOS, CORONARIOPATÍA, ETC." Width="290"></x:TextBox>
                                                <x:Label ID="Label446" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox69" runat="server" Text="OBESIDAD MÓRBIDA (IMC MAYOR a 35)" Width="290"></x:TextBox>
                                                <x:Label ID="Label447" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox70" runat="server" Text="PROBLEMAS RESPIRATORIOS: ASMA, EPOC, ETC." Width="290"></x:TextBox>
                                                <x:Label ID="Label448" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox71" runat="server" Text="PROBLEMAS OFTALMOLÓGICOS: RETINOPATÍA, GLAUCOMA, ETC." Width="290"></x:TextBox>
                                                <x:Label ID="Label449" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox72" runat="server" Text="PROBLEMAS DIGESTIVOS: ÚLCERA PÉPTICA, HEPATITIS, ETC." Width="290"></x:TextBox>
                                                <x:Label ID="Label450" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox73" runat="server" Text="APNEA DEL SUEÑO" Width="290"></x:TextBox>
                                                <x:Label ID="Label451" runat="server" Text=" "></x:Label>
                                                <x:TextBox ID="TextBox74" runat="server" Text="ALERGIAS" Width="290"></x:TextBox>
                                            </Items>
                                        </x:Panel>
                                        <x:Panel ID="Panel66" Width="120px" Height="580px" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="SI">
                                            <Items>
                                                <x:DropDownList ID="ddlAnemia_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label438" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlCirugia_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label439" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlDesprdenes_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label440" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlDiabetes_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label441" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlHipertension_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label442" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlEmbarazo_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label443" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlNeurologicos_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label444" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlInfecciones_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label445" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlProblemasCardiacos_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label452" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlObesidad_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label453" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlProblemasRespi_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label454" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlProblemasOftal_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label455" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlProblemasDigestivos_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label456" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlApnea_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                <x:Label ID="Label457" runat="server" Text=" "></x:Label>
                                                <x:DropDownList ID="ddlAlergias_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel67" Title="OTRA CONDICIÓN MÉDICA IMPORTANTE" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:TextBox ID="txtOtraCondicion_7D" runat="server" Text="" Width="960" ShowLabel="false"></x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel68" Title="USO DE MEDICACIÓN ACTUAL" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:TextBox ID="txtUsoMedicacion_7D" runat="server" Text="" ShowLabel="false" Width="960"></x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel69" Title="APTITUD" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:DropDownList ID="ddlAptitud_7D" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel70" Title="OBSERVACIÓN" EnableBackgroundColor="true" Height="40px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:TextBox ID="txtObservacion_7D" runat="server" Text="" ShowLabel="false" Width="960"></x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel87" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form55" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow200" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txt7DAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txt7DAuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txt7DActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow201" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txt7DEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txt7DEvaluadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txt7DEvaluadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                                <Ext:FormRow ID="FormRow211" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txt7DInformador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txt7DInformadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txt7DInformadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Tab>

                        <x:Tab ID="TabSintomaticoRespiratorio" BodyPadding="5px" Title="Sintomático Respiratorio" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar6" runat="server">
                                    <Items>
                                        <x:Button ID="btnSintomaticoRespiratorio" Text="Grabar Sintomático Respiratorio" Icon="SystemSave" runat="server" OnClick="btnSintomaticoRespiratorio_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Label ID="Label753" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label754" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabaSintomatico" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporteSintomatico" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel71" Title="USTED HA SIDO DIAGNOSTICADO ALGUNA VEZ DE:" EnableBackgroundColor="true" Height="72px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form132" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow519" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label710" runat="server" Text="1.  Tuberculosis   " ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSintomaticoTuberculosis" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow520" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label711" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow521" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label712" runat="server" Text="2. ¿Ha tenido tos por más de 15 días?  " ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSintomaticoTos15dias" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow522" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label713" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel72" Title="EN LA ACTUALIDAD USTED PRESENTA:" EnableBackgroundColor="true" Height="235px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form133" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow523" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label718" runat="server" Text="3.  Baja de peso inexplicable:" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSintomaticoBajaPeso" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow524" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label715" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow525" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label720" runat="server" Text="4.  Sudoración nocturna importante:" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSintomaticoSudoracion" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>


                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow526" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label717" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow527" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label716" runat="server" Text="5.  Expectoración con sangre:" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSintomaticoExpecto" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>

                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow528" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label719" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow529" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label722" runat="server" Text="6.  Familiares o amigos con Tuberculosis:" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSintomaticoFamiliares" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>

                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow530" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label721" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow531" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label724" runat="server" Text="7.  Sospecha de Tuberculosis:" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSintomaticoSospecha" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>

                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow532" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label723" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow533" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label726" runat="server" Text="Observaciones" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtSintomaticoObservacion" runat="server" Label="" Width="480px" ShowLabel="false"></x:TextBox>

                                                    </Items>
                                                </x:FormRow>

                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel73" Title="CONCLUSIÓN:" EnableBackgroundColor="true" Height="135px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form134" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow537" ColumnWidths="290px 120px 290px 260px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label728" runat="server" Text="Por lo que certifico que EL/LA paciente" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSintomaticoConclusion" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label732" runat="server" Text="es considerado SINTOMÁTICO RESPIRATORIO." ShowLabel="false"></x:Label>
                                                        <x:Label ID="label733" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow540" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label731" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow541" ColumnWidths="245px 290px 290px 40px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label734" runat="server" Text="Resultados de BK de esputo:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtSintomaticoBK1" runat="server" Width="240px" Label="1ª"></x:TextBox>
                                                        <x:TextBox ID="txtSintomaticoBK2" runat="server" Width="240px" Label="2ª"></x:TextBox>
                                                        <x:Label ID="label736" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow542" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label737" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow543" ColumnWidths="290px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label735" runat="server" Text="Resultado de Radiografía de Tórax:" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtSintomaticoResultRX" runat="server" Label="" Width="480px" ShowLabel="false"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow544" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label738" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>

                                <x:Panel ID="Panel89" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form57" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow204" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtSintomaticoAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtSintomaticoAuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtSintomaticoAuditorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow205" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtSintomaticoEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtSintomaticoEvaluadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtSintomaticoEvaluadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                                <Ext:FormRow ID="FormRow212" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtSintomaticoInformador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtSintomaticoInformadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtSintomaticoInformadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Tab>

                        <x:Tab ID="TabTamizajeDermatologico" BodyPadding="5px" Title="Tamizaje Dermatológico" runat="server">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar7" runat="server">
                                    <Items>
                                        <x:Button ID="btnTamizajeDermatologico" Text="Grabar Tamizaje Dermatológico" Icon="SystemSave" runat="server" OnClick="btnTamizajeDermatologico_Click" AjaxLoadingType="Mask"></x:Button>

                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="a1" Title="ANAMNESIS DERMATOLÓGICA EN RELACIÓN AL RIESGO" EnableBackgroundColor="true" Height="140px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="a2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="a3" ColumnWidths="240px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label458" runat="server" Text="Alergias dérmicas" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAlergiasDermicas" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a4" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label459" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a5" ColumnWidths="239px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label460" runat="server" Text="Alergias medicamentosas" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAlergiasMedicamentosas" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a6" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label461" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a7" ColumnWidths="241px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label462" runat="server" Text="Enfermedades propia de la piel y anexos" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEnfPropiaPiel" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a8" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label463" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a9" ColumnWidths="239px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label464" runat="server" Text="Describir" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtDescribirAnamnesis" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="a10" Title="Enfermedades sistémicas que afecten la piel y anexos:" EnableBackgroundColor="true" Height="120px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="a11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="a12" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="plabel113" runat="server" Text="Lupus eritematoso sistemico" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlLupusEritematoso" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="plabel115" runat="server" Text="Enfermedad tiroidea" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEnfermedadTiroidea" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="plabel116" runat="server" Text="Artritis reumatoide" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlArtritisReumatoide" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="plabel118" runat="server" Text="Hepatopatias" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlHepatopatias" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a13" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="plabel123" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a14" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="plabel119" runat="server" Text="Psoriasis" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPsoriasis" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="plabel120" runat="server" Text="Sindrome de ovario poliquístico" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSindromeOvario" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="plabel121" runat="server" Text="Diabetes Mellitus tipo 2" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlDiabetesMellitus" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="plabel122" runat="server" Text="Otras" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlOtrasEnfermedadesSistemicas" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a15" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label465" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a16" ColumnWidths="120px 720px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="plabel125" runat="server" Text="Describir" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtDescribirEnfermedades" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                        <x:Label ID="plabel126" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="a17" Title="EVALUACION DERMATOLOGICA Lesiones primarias:" EnableBackgroundColor="true" Height="200px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="a18" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="a19" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="plabel127" runat="server" Text="Mácula" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlMacula" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label466" runat="server" Text="Vesícula" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlVesicula" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label467" runat="server" Text="Nódulo" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlNodulo" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label468" runat="server" Text="Púrpura" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPurpura" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a20" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label469" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a21" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>

                                                        <x:Label ID="label470" runat="server" Text="Pápula" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPapula" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label471" runat="server" Text="Ampolla" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAmpolla" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label472" runat="server" Text="Placa" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPlaca" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label473" runat="server" Text="Comedones" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlComedones" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a22" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label474" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a23" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label475" runat="server" Text="Tubérculo" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTuberculo" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label476" runat="server" Text="Pústula" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPustula" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label477" runat="server" Text="Quiste" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlQuiste" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label478" runat="server" Text="Telangiectasia" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTelangiectasia" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a24" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label479" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a25" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label480" runat="server" Text="Escama" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEscama" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label481" runat="server" Text="Petequia" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPetequia" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label482" runat="server" Text="Angioedema" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAngioedema" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label483" runat="server" Text="Tumor" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTumor" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a26" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label484" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a27" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label485" runat="server" Text="Habón" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlHabon" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label486" runat="server" Text="Equímosis" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEquimosis" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label487" runat="server" Text="Discromías" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlDiscromias" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label488" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="label489" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a28" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label490" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a29" ColumnWidths="120px 720px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label491" runat="server" Text="Otros" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtOtrosLesionesPrimarias" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                        <x:Label ID="label492" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="a30" Title="EVALUACION DERMATOLOGICA Lesiones secundarias:" EnableBackgroundColor="true" Height="160px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="a31" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="a32" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label493" runat="server" Text="Escamas" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEscamas" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label494" runat="server" Text="Escaras" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEscaras" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label495" runat="server" Text="Fisura" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlFisura" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label496" runat="server" Text="Excoriaciones" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlExcoriaciones" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a33" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label497" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a34" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="plabel161" runat="server" Text="Costras" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCostras" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="plabel162" runat="server" Text="Cicatrices" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCicatrices" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label498" runat="server" Text="Atrofia" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAtrofia" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label499" runat="server" Text="Liquenificación" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlLiquenificacion" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a35" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="plabel175" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a36" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label500" runat="server" Text="Esclerosis" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEsclerosis" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label501" runat="server" Text="Ulceras" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlUlceras" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label502" runat="server" Text="Erosión" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlErosion" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label503" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="label504" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a37" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label505" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="a38" ColumnWidths="120px 720px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label506" runat="server" Text="Otros" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtOtrosLesionesSecundarias" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                        <x:Label ID="label507" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>

                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Tab>

                        <x:Tab ID="TabDermatologico_Internacional" BodyPadding="5px" Title="Tamizaje Dermatológico" runat="server" Hidden="false">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar22" runat="server">
                                    <Items>
                                        <x:Button ID="btnDermatologicoInternacional" Text="Grabar Tamizaje Dermatológico" Icon="SystemSave" runat="server" OnClick="btnDermatologicoInternacional_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Label ID="Label755" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label756" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabaDermatologico" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporteTamizajeCI" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>
                                <x:Panel ID="Panel79" Title="ANAMNESIS DERMATOLÓGICA EN RELACIÓN AL RIESGO" EnableBackgroundColor="true" Height="140px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form49" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow166" ColumnWidths="240px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label565" runat="server" Text="Alergias dérmicas" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAlergiasDermicas_Inter" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow167" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label566" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow168" ColumnWidths="239px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label567" runat="server" Text="Alergias medicamentosas" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAlergiasMedicamentosas_Inter" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow169" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label568" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow170" ColumnWidths="241px 500px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label569" runat="server" Text="Enfermedades propia de la piel y anexos" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEnfPropiaPiels_Inter" runat="server" Width="100px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow171" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label570" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow172" ColumnWidths="239px 720px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label571" runat="server" Text="Describir" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtDescribirAnamnesiss_Inter" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel80" Title="Enfermedades sistémicas que afecten la piel y anexos:" EnableBackgroundColor="true" Height="120px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form50" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow173" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label572" runat="server" Text="Lupus eritematoso sistemico" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlLupusEritematosos_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label573" runat="server" Text="Enfermedad tiroidea" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEnfermedadTiroideas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label574" runat="server" Text="Artritis reumatoide" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlArtritisReumatoides_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label575" runat="server" Text="Hepatopatias" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlHepatopatiass_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow174" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label576" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow175" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label577" runat="server" Text="Psoriasis" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPsoriasiss_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label578" runat="server" Text="Sindrome de ovario poliquístico" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlSindromeOvarios_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label579" runat="server" Text="Diabetes Mellitus tipo 2" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlDiabetesMellituss_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label580" runat="server" Text="Otras" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlOtrasEnfermedadesSistemicass_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow176" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label581" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow177" ColumnWidths="120px 720px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="Label582" runat="server" Text="Describir" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtDescribirEnfermedadess_Inter" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                        <x:Label ID="Label583" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel81" Title="EVALUACION DERMATOLOGICA Lesiones primarias:" EnableBackgroundColor="true" Height="200px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form51" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow178" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label584" runat="server" Text="Mácula" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlMaculas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label585" runat="server" Text="Vesícula" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlVesiculas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label586" runat="server" Text="Nódulo" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlNodulos_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label587" runat="server" Text="Púrpura" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPurpuras_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow179" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label588" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow180" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label589" runat="server" Text="Pápula" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPapulas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label590" runat="server" Text="Ampolla" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAmpollas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label591" runat="server" Text="Placa" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPlacas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label592" runat="server" Text="Comedones" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlComedoness_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow181" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label593" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow182" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label594" runat="server" Text="Tubérculo" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTuberculos_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label595" runat="server" Text="Pústula" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPustulas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label596" runat="server" Text="Quiste" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlQuistes_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label597" runat="server" Text="Telangiectasia" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTelangiectasias_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow183" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label598" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow184" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label599" runat="server" Text="Escama" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEscamas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label600" runat="server" Text="Petequia" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlPetequias_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label601" runat="server" Text="Angioedema" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAngioedemas_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label602" runat="server" Text="Tumor" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlTumors_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow185" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label603" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow186" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label604" runat="server" Text="Habón" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlHabons_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label605" runat="server" Text="Equímosis" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEquimosiss_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label606" runat="server" Text="Discromías" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlDiscromiass_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label607" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="label616" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow187" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label617" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow188" ColumnWidths="120px 720px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label618" runat="server" Text="Otros" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtOtrosLesionesPrimariass_Inter" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                        <x:Label ID="label619" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel82" Title="EVALUACION DERMATOLOGICA Lesiones secundarias:" EnableBackgroundColor="true" Height="160px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form52" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow189" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label620" runat="server" Text="Escamas" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEscamass_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label621" runat="server" Text="Escaras" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEscarass_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label622" runat="server" Text="Fisura" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlFisuras_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label623" runat="server" Text="Excoriaciones" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlExcoriacioness_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow190" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label624" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow191" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label625" runat="server" Text="Costras" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCostrass_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="Label626" runat="server" Text="Cicatrices" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlCicatricess_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label627" runat="server" Text="Atrofia" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlAtrofias_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label628" runat="server" Text="Liquenificación" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlLiquenificacions_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow192" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="Label629" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow193" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label630" runat="server" Text="Esclerosis" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlEsclerosiss_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label739" runat="server" Text="Ulceras" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlUlcerass_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label740" runat="server" Text="Erosión" ShowLabel="false"></x:Label>
                                                        <x:DropDownList ID="ddlErosions_Inter" runat="server" Width="70px" ShowLabel="false"></x:DropDownList>
                                                        <x:Label ID="label741" runat="server" Text="" ShowLabel="false"></x:Label>
                                                        <x:Label ID="label742" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow194" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server">
                                                    <Items>
                                                        <x:Label ID="label743" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow195" ColumnWidths="120px 720px 120px" runat="server">
                                                    <Items>
                                                        <x:Label ID="label744" runat="server" Text="Otros" ShowLabel="false"></x:Label>
                                                        <x:TextBox ID="txtOtrosLesionesSecundariass_Inter" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                        <x:Label ID="label745" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                                </x:FormRow>

                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel13" Title="HALLAZGOS" EnableBackgroundColor="true" Height="40px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form197" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="180px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow1555" ColumnWidths="250px" runat="server">
                                                    <Items>
                                                        <x:CheckBox ID="chkSinDermatopatias" runat="server" Label="SIN DERMATOPATÍAS"></x:CheckBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel88" Title="AUDITORÍA" EnableBackgroundColor="true" Height="90px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form56" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow202" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtDermatoloCIAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtDermatoloCIAuditorInsercion" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtDermatoloCIActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow203" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtDermatoloCIEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtDermatoloCIEvaluadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtDermatoloCIEvaluadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                                <Ext:FormRow ID="FormRow213" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtDermatoloCIInformador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtDermatoloCIInformadorInserta" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtDermatoloCIInformadorActualizacion" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Tab>

                        <x:Tab ID="TabOsteomuscularInternacional" BodyPadding="5px" Title="Osteomuscular" runat="server" Hidden="false">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar21" runat="server">
                                    <Items>
                                        <x:Button ID="btnOsteoInternacional" Text="Grabar Osteomuscular" Icon="SystemSave" runat="server" OnClick="btnOsteoInternacional_Click" AjaxLoadingType="Mask"></x:Button>
                                        <x:Label ID="Label757" runat="server" Text="....." ShowLabel="false"></x:Label>
                                        <x:Label ID="Label758" runat="server" Text="Firma Usuario" ShowLabel="false"></x:Label>
                                        <x:DropDownList ID="ddlUsuarioGrabaOsteo" runat="server"></x:DropDownList>
                                        <x:Button ID="btnReporteOsteoCI" Text="Ver Reporte" Icon="PageWhiteText" runat="server" Enabled="true"></x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <Items>

                                <x:Panel ID="Panel77" Title="1. CUESTONARIO DE SINTOMAS  ( comentar si tiene relación o no con el puesto de trabajo)" EnableBackgroundColor="true" Height="740px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form33" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow79" ColumnWidths="950px" runat="server">
                                                    <Items>
                                                        <x:Form ID="Form34" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                            <Rows>
                                                                <x:FormRow ID="FormRow80" ColumnWidths="480px 480px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox47" Label="" CssClass="mright" runat="server" Width="460" Text="                                 RESPONDA EN TODOS LOS CASOS" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:TextBox ID="TextBox48" Label="" CssClass="mright" runat="server" Width="460" Text="                      RESPONDA SOLAMENTE SI HA TENIDO PROBLEMAS" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow81" ColumnWidths="480px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextArea ID="TextArea1" Label="" CssClass="mright" runat="server" Width="460" Text="Ha tenido problemas (dolor, aumento de volumen, curvaturas, etc.) en los 12 meses, a nivel de:" ShowLabel="false" Readonly="true" Height="65"></x:TextArea>
                                                                        <x:TextArea ID="TextArea2" Label="" CssClass="mright" runat="server" Width="220" Text="Durante los últimos 12 meses ha estado incapacitado para su trabajo (en casa, o fuera) por causa del problema:" ShowLabel="false" Readonly="true" Height="65"></x:TextArea>
                                                                        <x:TextArea ID="TextArea3" Label="" CssClass="mright" runat="server" Width="220" Text="Ha tenido problemas en los últimos siete días:" ShowLabel="false" Readonly="true" Height="65"></x:TextArea>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow82" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox63" Label="" CssClass="mright" runat="server" Width="130" Text="Nuca / Cuello" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlNuca1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlNuca2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlNuca3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow83" ColumnWidths="960px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox66" Label="" CssClass="mright" runat="server" Width="130" Text="Hombros" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow84" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox67" Label="" CssClass="mright" runat="server" Width="130" Text="Hombro Derecho" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlHombroDerecho1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlHombroDerecho2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlHombroDerecho3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow85" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox68" Label="" CssClass="mright" runat="server" Width="130" Text="Hombro Izquierdo" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlHombroIzquierdo1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlHombroIzquierdo2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlHombroIzquierdo3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow86" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox75" Label="" CssClass="mright" runat="server" Width="130" Text="Ambos Hombros" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlAmbosHombros1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlAmbosHombros2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlAmbosHombros3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow87" ColumnWidths="960px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox76" Label="" CssClass="mright" runat="server" Width="130" Text="Codos" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow88" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox77" Label="" CssClass="mright" runat="server" Width="130" Text="Codo Derecho" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlCodoDerecho1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlCodoDerecho2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlCodoDerecho3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow89" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox78" Label="" CssClass="mright" runat="server" Width="130" Text="Codo Izquierdo" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlCodoIzquierdo1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlCodoIzquierdo2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlCodoIzquierdo3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>


                                                                <x:FormRow ID="FormRow90" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox79" Label="" CssClass="mright" runat="server" Width="130" Text="Ambos Codos" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlAmboscodos1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlAmboscodos2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlAmboscodos3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>

                                                                <x:FormRow ID="FormRow91" ColumnWidths="960px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox80" Label="" CssClass="mright" runat="server" Width="130" Text="Muñecas / Manos" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow92" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox81" Label="" CssClass="mright" runat="server" Width="130" Text="Derecha" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlManosDerecha1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlManosDerecha2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlManosDerecha3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow93" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox82" Label="" CssClass="mright" runat="server" Width="130" Text="Izquierda" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlManosIzquierda1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlManosIzquierda2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlManosIzquierda3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow94" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox83" Label="" CssClass="mright" runat="server" Width="130" Text="Ambas" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlAmbasManos1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlAmbasManos2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlAmbasManos3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>

                                                                <x:FormRow ID="FormRow95" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox84" Label="" CssClass="mright" runat="server" Width="130" Text="Columna Dorsal" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlColumnadorsal1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlColumnadorsal2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlColumnadorsal3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>




                                                                <x:FormRow ID="FormRow96" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox85" Label="" CssClass="mright" runat="server" Width="130" Text="Columna Lumbar" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlColumnaLumbar1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlColumnaLumbar2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlColumnaLumbar3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>

                                                                <x:FormRow ID="FormRow97" ColumnWidths="960px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox86" Label="" CssClass="mright" runat="server" Width="130" Text="Caderas" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow98" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox87" Label="" CssClass="mright" runat="server" Width="130" Text="Derecha" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlCaderaDerecha1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlCaderaDerecha2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlCaderaDerecha3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow100" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox88" Label="" CssClass="mright" runat="server" Width="130" Text="Izquierda" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlCaderaIzquierda1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlCaderaIzquierda2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlCaderaIzquierda3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow106" ColumnWidths="960px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox89" Label="" CssClass="mright" runat="server" Width="130" Text="Rodillas" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow107" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox90" Label="" CssClass="mright" runat="server" Width="130" Text="Derecha" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlRodillaDerecha1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlRodillaDerecha2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlRodillaDerecha3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow108" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox91" Label="" CssClass="mright" runat="server" Width="130" Text="Izquierda" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlRodillaIzquierda1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlRodillaIzquierda2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlRodillaIzquierda3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>




                                                                <x:FormRow ID="FormRow116" ColumnWidths="960px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox92" Label="" CssClass="mright" runat="server" Width="130" Text="Tobillos / Pies" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow119" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox93" Label="" CssClass="mright" runat="server" Width="130" Text="Derecho" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlTobillosDerecho1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlTobillosDerecho2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlTobillosDerecho3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                                <x:FormRow ID="FormRow122" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                    <Items>
                                                                        <x:TextBox ID="TextBox94" Label="" CssClass="mright" runat="server" Width="130" Text="Izquierdo" ShowLabel="false" Readonly="true"></x:TextBox>
                                                                        <x:DropDownList ID="ddlTobillosIzquierdo1_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlTobillosIzquierdo2_Inter" runat="server" Width="130"></x:DropDownList>
                                                                        <x:DropDownList ID="ddlTobillosIzquierdo3_Inter" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                                </x:FormRow>
                                                            </Rows>
                                                        </x:Form>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel78" Title="EXAMEN FÍSICO - COLUMNA VERTEBRAL" EnableBackgroundColor="true" Height="1300px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:GroupPanel runat="server" Title="Eje Antero Posterior" ID="GroupPanel4" BoxFlex="1" Height="120" TableColspan="1" Width="940px">
                                            <Items>
                                                <x:Form ID="Form35" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow125" ColumnWidths=" 400px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label508" runat="server" Text="Curvas Fisiológicas (ant-post)" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label509" runat="server" Text="Eje Lateral" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow128" ColumnWidths="100px   300px  100px  300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label510" runat="server" Text="Cervical" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlCervical_Inter" runat="server" Width="250" ShowLabel="false"></x:DropDownList>

                                                                <x:Label ID="Label511" runat="server" Text="dorsal" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlDorsalEjeLateral_Inter" runat="server" Width="250" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow131" ColumnWidths="99px   300px  100px  300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label512" runat="server" Text="dorsal" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlDorsal_Inter" runat="server" Width="250" ShowLabel="false"></x:DropDownList>

                                                                <x:Label ID="Label513" runat="server" Text="Lumbar" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlLumbarEjeLateral_Inter" runat="server" Width="250" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow132" ColumnWidths="100px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label514" runat="server" Text="Lumbar" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlLumbar_Inter" runat="server" Width="250" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>

                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="MOVILIDAD-DOLOR (valorar según tabla1)" ID="GroupPanel5" BoxFlex="1" Height="110" TableColspan="1">
                                            <Items>
                                                <x:Form ID="Form36" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow133" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label515" runat="server" Text="Columna Vertebral" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label516" runat="server" Text="Flexión" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label517" runat="server" Text="Extensión" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label518" runat="server" Text="Lateralización Derecha" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label519" runat="server" Text="Lateralización Izquierda" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label520" runat="server" Text="Rotación Derecha" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label521" runat="server" Text="Rotación Izquierda" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label522" runat="server" Text="Irradiación" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow134" ColumnWidths="880px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label523" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow135" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label524" runat="server" Text="Cervical" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlCervicalFlexion_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCervicalExtension_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCervicalLatDere_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCervicalLatIzq_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCervicalRotaDere_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCervicalRotaIzq_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCervicalIrradiacion_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow136" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                            <Items>
                                                                <x:Label ID="Label525" runat="server" Text="Dorso Lumbar" ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlDorsoFlexion_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlDorsoExtension_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlDorsoLateDere_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlDorsoLateIzq_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlDorsoRotaDere_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlDorsoRotaIzq_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlDorsoIrradiacion_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="TEST ESPECÍFICOS" ID="GroupPanel15" BoxFlex="1" Height="120" TableColspan="1">
                                            <Items>
                                                <x:Form ID="Form37" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow137" ColumnWidths="450px 450px" runat="server">
                                                            <Items>

                                                                <x:GroupPanel runat="server" Title="LASEGUE" ID="GroupPanel16" BoxFlex="1" Height="50" TableColspan="1">
                                                                    <Items>
                                                                        <x:Form ID="Form38" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                                            <Rows>
                                                                                <x:FormRow ID="FormRow138" ColumnWidths="80px 120px 80px 120px" runat="server">
                                                                                    <Items>
                                                                                        <x:Label ID="Label526" runat="server" Text="Derecha" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlLasegueDere_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                        <x:Label ID="Label527" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlLasegueIzq_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                    </Items>
                                                                                </x:FormRow>
                                                                            </Rows>
                                                                        </x:Form>
                                                                    </Items>
                                                                </x:GroupPanel>

                                                                <x:GroupPanel runat="server" Title="SCHOBER" ID="GroupPanel17" BoxFlex="1" Height="50" TableColspan="1">
                                                                    <Items>
                                                                        <x:Form ID="Form39" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                                            <Rows>
                                                                                <x:FormRow ID="FormRow139" ColumnWidths="80px 120px 80px 120px" runat="server">
                                                                                    <Items>
                                                                                        <x:Label ID="Label528" runat="server" Text="Derecha" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlSchoberDere_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                        <x:Label ID="Label529" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlSchoberIzq_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                    </Items>
                                                                                </x:FormRow>
                                                                            </Rows>
                                                                        </x:Form>
                                                                    </Items>
                                                                </x:GroupPanel>

                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="PALPACIÓN" ID="GroupPanel6" BoxFlex="1" Height="110" TableColspan="1">
                                            <Items>
                                                <x:Form ID="Form40" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow140" ColumnWidths="200px  300px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label530" runat="server" Text=". " ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label531" runat="server" Text="Apófisis Espinosas" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label532" runat="server" Text="Contractura Muscular" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow141" ColumnWidths="200px  300px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label533" runat="server" Text="Columna Cervical" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkColumnaCervicalApofisis_Inter" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                                <x:CheckBox ID="chkColumnaCervicalContractura_Inter" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow142" ColumnWidths="199px  300px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label534" runat="server" Text="Columna Dorsal" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkColumnaDorsalApofisis_Inter" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                                <x:CheckBox ID="chkColumnaDorsalContractura_Inter" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow143" ColumnWidths="198px  300px   300px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label535" runat="server" Text="Columna Lumbar" ShowLabel="false"></x:Label>
                                                                <x:CheckBox ID="chkColumnaLumbarApofisis_Inter" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                                <x:CheckBox ID="chkColumnaLumbarContractura_Inter" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="ARTICULACIONES: movilidad y dolor (valor según tabla 1)" ID="GroupPanel19" BoxFlex="1" Height="400" TableColspan="1">
                                            <Items>
                                                <x:Form ID="Form41" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow144" ColumnWidths="100px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label536" runat="server" Text="Articulación" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label537" runat="server" Text="Abducción" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label538" runat="server" Text="Adución" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label539" runat="server" Text="Flexión" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label540" runat="server" Text="Extensión" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label541" runat="server" Text="R.Externa" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label542" runat="server" Text="R.Interna" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label543" runat="server" Text="Irradiación" ShowLabel="false"></x:Label>
                                                                <x:Label ID="Label544" runat="server" Text="Alter.Masa" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow145" ColumnWidths=" 99px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label545" runat="server" Text="Hombro Der." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlHD1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHD2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHD3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHD4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHD5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHD6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHD7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHD8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow146" ColumnWidths=" 100px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label546" runat="server" Text="Hombro Izq." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlHI1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHI2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHI3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHI4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHI5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHI6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHI7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlHI8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>

                                                        <x:FormRow ID="FormRow147" ColumnWidths="  99px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label547" runat="server" Text="Codo Der." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlCD1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCD2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCD3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCD4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCD5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCD6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCD7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCD8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow148" ColumnWidths=" 100px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label548" runat="server" Text="Codo Izq." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlCI1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCI2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCI3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCI4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCI5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCI6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCI7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCI8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>

                                                        <x:FormRow ID="FormRow149" ColumnWidths="  99px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label549" runat="server" Text="Mune. Der." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlMuneD1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneD2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneD3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneD4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneD5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneD6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneD7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneD8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow150" ColumnWidths=" 100px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label550" runat="server" Text="Mune. Izq." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlMuneI1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneI2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneI3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneI4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneI5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneI6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneI7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlMuneI8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>

                                                        <x:FormRow ID="FormRow151" ColumnWidths="  99px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label551" runat="server" Text="Cadera Der." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlCaderaD1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaD2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaD3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaD4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaD5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaD6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaD7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaD8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow152" ColumnWidths=" 100px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label552" runat="server" Text="Cadera Izq." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlCaderaI1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaI2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaI3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaI4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaI5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaI6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaI7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlCaderaI8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>

                                                        <x:FormRow ID="FormRow153" ColumnWidths="  99px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label553" runat="server" Text="Tobillo Der." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlTobilloD1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloD2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloD3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloD4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloD5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloD6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloD7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloD8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow154" ColumnWidths=" 100px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label554" runat="server" Text="Tobillo Izq." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlTobilloI1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloI2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloI3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloI4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloI5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloI6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloI7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlTobilloI8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>

                                                        <x:FormRow ID="FormRow155" ColumnWidths="  99px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label555" runat="server" Text="Rodilla Der." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlRodillaD1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaD2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaD3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaD4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaD5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaD6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaD7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaD8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow156" ColumnWidths=" 100px 100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                            <Items>
                                                                <x:Label ID="Label556" runat="server" Text="Rodilla Izq." ShowLabel="false"></x:Label>
                                                                <x:DropDownList ID="ddlRodillaI1_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaI2_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaI3_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaI4_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaI5_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaI6_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaI7_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                                <x:DropDownList ID="ddlRodillaI8_Inter" runat="server" Width="80" ShowLabel="false"></x:DropDownList>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>

                                        <x:GroupPanel runat="server" Title="TEST ESPECÍFICOS" ID="GroupPanel20" BoxFlex="1" Height="280" TableColspan="1">
                                            <Items>
                                                <x:Form ID="Form42" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow157" ColumnWidths="450px 450px" runat="server">
                                                            <Items>

                                                                <x:GroupPanel runat="server" Title="TEST DE PHALEN" ID="GroupPanel21" BoxFlex="1" Height="50" TableColspan="1">
                                                                    <Items>
                                                                        <x:Form ID="Form43" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                                            <Rows>
                                                                                <x:FormRow ID="FormRow158" ColumnWidths="80px 120px 80px 120px" runat="server">
                                                                                    <Items>
                                                                                        <x:Label ID="Label557" runat="server" Text="Derecha" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlPhalenDerecha_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                        <x:Label ID="Label558" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlPhalenIzquierda_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                    </Items>
                                                                                </x:FormRow>
                                                                            </Rows>
                                                                        </x:Form>
                                                                    </Items>
                                                                </x:GroupPanel>

                                                                <x:GroupPanel runat="server" Title="TEST DE TINEL" ID="GroupPanel22" BoxFlex="1" Height="50" TableColspan="1">
                                                                    <Items>
                                                                        <x:Form ID="Form44" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                                            <Rows>
                                                                                <x:FormRow ID="FormRow159" ColumnWidths="80px 120px 80px 120px" runat="server">
                                                                                    <Items>
                                                                                        <x:Label ID="Label559" runat="server" Text="Derecha" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlTinelDerecha_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                        <x:Label ID="Label560" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlTinelIzquierda_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                    </Items>
                                                                                </x:FormRow>
                                                                            </Rows>
                                                                        </x:Form>
                                                                    </Items>
                                                                </x:GroupPanel>

                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow160" ColumnWidths="450px 450px" runat="server">
                                                            <Items>

                                                                <x:GroupPanel runat="server" Title="CODO" ID="GroupPanel23" BoxFlex="1" Height="50" TableColspan="1">
                                                                    <Items>
                                                                        <x:Form ID="Form45" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                                            <Rows>
                                                                                <x:FormRow ID="FormRow161" ColumnWidths="80px 120px 80px 100px" runat="server">
                                                                                    <Items>
                                                                                        <x:Label ID="Label561" runat="server" Text="Derecha" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlCodoDerecho_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                        <x:Label ID="Label562" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlCodoIzquierdo_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                    </Items>
                                                                                </x:FormRow>

                                                                            </Rows>
                                                                        </x:Form>
                                                                    </Items>
                                                                </x:GroupPanel>

                                                                <x:GroupPanel runat="server" Title="PIE" ID="GroupPanel24" BoxFlex="1" Height="50" TableColspan="1">
                                                                    <Items>
                                                                        <x:Form ID="Form46" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                                            <Rows>

                                                                                <x:FormRow ID="FormRow162" ColumnWidths="80px 120px  80px 100px" runat="server">
                                                                                    <Items>
                                                                                        <x:Label ID="Label563" runat="server" Text="Derecha" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlPieDerecho_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                        <x:Label ID="Label564" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>
                                                                                        <x:DropDownList ID="ddlPieIzquierdo_Inter" runat="server" Width="100" ShowLabel="false"></x:DropDownList>
                                                                                    </Items>
                                                                                </x:FormRow>

                                                                            </Rows>
                                                                        </x:Form>
                                                                    </Items>
                                                                </x:GroupPanel>

                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow163" ColumnWidths="450px 450px" runat="server">
                                                            <Items>

                                                                <x:GroupPanel runat="server" Title="AMPLIAR DESCRIPCIÓN DE ARTICULACIÓN,  COLUMNA VERTEBRAL" ID="GroupPanel25" BoxFlex="1" Height="90" TableColspan="1">
                                                                    <Items>
                                                                        <x:Form ID="Form47" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                                            <Rows>
                                                                                <x:FormRow ID="FormRow164" ColumnWidths="400px" runat="server">
                                                                                    <Items>
                                                                                        <x:TextArea ID="txtAmpliar_Inter" runat="server" Text="" ShowLabel="false" Width="420"></x:TextArea>
                                                                                    </Items>
                                                                                </x:FormRow>

                                                                            </Rows>
                                                                        </x:Form>
                                                                    </Items>
                                                                </x:GroupPanel>

                                                                <x:GroupPanel runat="server" Title="CONCLUSIÓN" ID="GroupPanel26" BoxFlex="1" Height="90" TableColspan="1">
                                                                    <Items>
                                                                        <x:Form ID="Form48" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                                            <Rows>

                                                                                <x:FormRow ID="FormRow165" ColumnWidths="400px" runat="server">
                                                                                    <Items>
                                                                                        <x:TextArea ID="txtConclusion_Inter" runat="server" Text="" ShowLabel="false" Width="420"></x:TextArea>
                                                                                    </Items>
                                                                                </x:FormRow>

                                                                            </Rows>
                                                                        </x:Form>
                                                                    </Items>
                                                                </x:GroupPanel>
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel83" Title="HALLAZGOS" EnableBackgroundColor="true" Height="40px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="180px" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow17" ColumnWidths="250px" runat="server">
                                                    <Items>
                                                        <x:CheckBox ID="chkEvaluacionNormalOsteo_CI" runat="server" Label="EVALUACIÓN NORMAL"></x:CheckBox>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>


                                <x:Panel ID="Panel84" Title="AUDITORÍA" EnableBackgroundColor="true" Height="60px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form213" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                            <Rows>
                                                <x:FormRow ID="FormRow654" ColumnWidths="320px 300px 300px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtOsteoMuscularAuditor" runat="server" Text="" Width="200" Label="Auditor" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOsteoMuscularAuditorInsertar" runat="server" Text="" Width="180" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txtOsteoMuscularAuditorEditar" runat="server" Text="" Width="180" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>

                                                </x:FormRow>
                                                <Ext:FormRow ID="FormRow656" ColumnWidths="320px 300px 300px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txttxtOsteoMuscularEvaluador" runat="server" Text="" Width="200" Label="Evaluador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txttxtOsteoMuscularEvaluadorInsertar" runat="server" Text="" Width="180" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txttxtOsteoMuscularEvaluadorEvaluar" runat="server" Text="" Width="180" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                                <Ext:FormRow ID="FormRow209" ColumnWidths="320px 320px 320px" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txttxtOsteoMuscularInformador" runat="server" Text="" Width="200" Label="Informador" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txttxtOsteoMuscularInformadorInsertar" runat="server" Text="" Width="200" Label="Inserción" Readonly="True"></x:TextBox>
                                                        <x:TextBox ID="txttxtOsteoMuscularInformadorEvaluar" runat="server" Text="" Width="200" Label="Modificación" Readonly="True"></x:TextBox>
                                                    </Items>
                                                </Ext:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Tab>                      
                    </Tabs>
                </x:TabStrip>
            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />
        <x:Window ID="WindowAddAntecedenteOcupacional" Title="Nuevo Antecedente Ocupacional" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddAntecedenteOcupacional_Close" IsModal="True" Width="550px" Height="300px">
        </x:Window>

        <x:Window ID="xxx" runat="server" Title="Nuevo Peligro" Popup="false" EnableIFrame="true" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" IsModal="True" Width="750px" Height="500px" OnClose="xxx_Close">
        </x:Window>

        <x:Window ID="Window1" runat="server" Title="Nuevo Epp" Popup="false" EnableIFrame="true" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" IsModal="True" Width="750px" Height="500px" OnClose="Window1_Close">
        </x:Window>

        <x:Window ID="WindowAddAntecedentePersonal" Title="Nuevo Antecedente Personal" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddAntecedentePersonal_Close" IsModal="True" Width="450px" Height="345px">
        </x:Window>

        <x:Window ID="WindowAddAntecedenteFamiliar" Title="Nuevo Antecedente Familiar" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddAntecedenteFamiliar_Close" IsModal="True" Width="650px" Height="250px">
        </x:Window>

        <x:Window ID="WindowAddHabitoNocivo" Title="Nuevo Hábito Nocivo" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddHabitoNocivo_Close" IsModal="True" Width="450px" Height="210px">
        </x:Window>

        <x:Window ID="WindowAddDX" Title="Nuevo Diagnóstico" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddDX_Close" IsModal="True" Width="650px" Height="480px">
        </x:Window>

        <x:Window ID="WindowAddDXFrecuente" Title="Diagnóstico Frecuente" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WindowAddDXFrecuente_Close" IsModal="True" Width="650px" Height="640px">
        </x:Window>

        <x:Window ID="Window2" Title="Descargar" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="Window1_Close" IsModal="True" Width="450px" Height="370px">
        </x:Window>

        <x:Window ID="winEditExaAdicional" Title="Agregar Examenes " Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="winEditExaAdicional_Close" IsModal="True" Width="545px" Height="455px">
        </x:Window>

        <x:Window ID="WinFechaServicio" Title="Cambiar Fecha Servicio" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false"
            Target="Top" OnClose="WinFechaServicio_Close" IsModal="True" Width="450px" Height="270px">
        </x:Window>

        <x:Window ID="winEdit1" Title="Reporte" Popup="false" EnableIFrame="true" runat="server" IconUrl="~/images/16/11.png"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="true" EnableResize="true"
            Target="Top" IsModal="true" Height="630px" Width="870px">
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
        var texturlID = '<%= texturl.ClientID %>';

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

        var canvas = document.getElementById("imgCanvas");
        var context = canvas.getContext("2d");
        var imagenFondo = new Image();
        imagenFondo.src = '../images/Fototipo/rostro.png';

        context.drawImage(imagenFondo, 0, 0);
        
        imagenFondo.onload = function () {
            context.drawImage(imagenFondo, 0, 0);
        }
        var estoyDibujando = false;

        function cargarDibujo() {
            context.clearRect(0, 0, canvas.width, canvas.height);
            var imagenFondo = new Image();
            imagenFondo.src = '../images/Fototipo/rostro.png';
            context.drawImage(imagenFondo, 0, 0);
            cambiarUrl()
        }


        function cambioColor(reciboColor) {
            color = "#" + reciboColor;
            document.getElementById('rellenoFondo').style.background = color;
        }

        function cambioGrosor(reciboGrosor) {
            grosor = reciboGrosor;
        }

        function cambioFondo() {
            document.getElementById('imgCanvas').style.background = color;
        }

        function pulsaRaton(e) {
            estoyDibujando = true;
            context.beginPath();
            var pos = getMousePos(canvas, e);
            posx = pos.x;
            posy = pos.y;
            context.moveTo(e.posx, e.posx);
        }

        function mueveRaton(e) {
            if (estoyDibujando) {
                var pos = getMousePos(canvas, e);
                posx = pos.x;
                posy = pos.y;
                context.fillStyle = color;
                context.fillRect(posx, posy, 10, 10);                
            }
        }

        function levantaRaton(e) {
            context.closePath();
            estoyDibujando = false;
            cambiarUrl()
        }

        imgCanvas.addEventListener('mousemove', mueveRaton, false);

        imgCanvas.addEventListener('mousedown', pulsaRaton, false);

        imgCanvas.addEventListener('mouseup', levantaRaton, false);

        color = "#333";

        function getMousePos(canvas, evt) {
            var rect = canvas.getBoundingClientRect();
            return {
                x: (evt.clientX - rect.left) / (rect.right - rect.left) * canvas.width,
                y: (evt.clientY - rect.top) / (rect.bottom - rect.top) * canvas.height
            };            
        }       
        function cambiarUrl() {
            document.getElementById('<%=texturl.ClientID%>').value = canvas.toDataURL("image/png");
        }       
            
        function mostrarimagen() {          
            if (document.getElementById('<%=texturl.ClientID%>').value == "") {
                cargarDibujo()
            } else {
                context.clearRect(0, 0, canvas.width, canvas.height);
                var imagenFondo1 = new Image();
                imagenFondo1.src = document.getElementById('<%=texturl.ClientID%>').value;
                context.drawImage(imagenFondo1, 0, 0);
            }          

        }
    </script>
</body>
</html>
