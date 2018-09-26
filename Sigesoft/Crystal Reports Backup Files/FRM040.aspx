<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM040.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Servicios.FRM040" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
   <style>
        .red
        {
            color: Red;
            font-style: italic;
        }
        .blue
        {
            color: Blue;
            font-style: italic;
        }
        .blue label.x-form-item-label
        {
            color: Blue;
            font-weight: bold;
        }
        .red label.x-form-item-label
        {
            color: Red;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" runat="server" />
    <x:Panel ID="Panel2" runat="server" Height="6000px" Width="1000px" ShowBorder="True"
        Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
        BoxConfigChildMargin="0 0 5 0" ShowHeader="True" Title="" >
        <Items>
            <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="60" >                
                <Items>
                      <x:SimpleForm  ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="false" ShowHeader="False" runat="server">
                        <Items>                             
                               <x:Form ID="Form8" runat="server" EnableBackgroundColor="false" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow19" ColumnWidths="460px" runat="server">
                                            <Items> 
                                                <x:Form ID="Form9"   runat="server" EnableBackgroundColor="false" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow20" ColumnWidths="150px 150px 300px 220px 100px" runat="server" >
                                                            <Items>
                                                                <x:DatePicker ID="dpFechaInicio" Label="F.I" Width="90px" runat="server"  DateFormatString="dd/MM/yyyy" />
                                                                <x:DatePicker ID="dpFechaFin" Label="F.F"  runat="server" Width="90px" DateFormatString="dd/MM/yyyy" />                                       
                                                                <x:DropDownList ID="ddlEmpresaCliente" runat="server"  Label="Emp." Width="240px"></x:DropDownList>                             
                                                                <x:TextBox  runat="server" Label="Trabaj." Text="" Width="240px" ID="txtTrabajador"></x:TextBox>     
                                                                <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline"   OnClick="btnFilter_Click" ></x:Button>     
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
            <x:GroupPanel runat="server" Title="Resultado de la búsqueda" ID="GroupPanel2" AutoWidth="true" BoxFlex="1" Height="160" >                
                <Items>
                    <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server"  AutoScroll="true" Height="120px"
                                EnableRowNumber="True" EnableRowNumberPaging="true"  RowNumberWidth="40" AjaxLoadingType="Mask"
                            EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_ServiceId,v_IdTrabajador,i_AptitudeStatusId,d_FechaNacimiento,v_Genero,v_TipoEso,v_GrupoRiesgo,v_Puesto,Dni" 
                            EnableTextSelection="true" EnableAlternateRowColor="true"  BoxFlex="2" BoxMargin="5" 
                            OnRowClick="grdData_RowClick" EnableRowClick="true" >                        
                        <Toolbars>
                    <x:Toolbar ID="Toolbar17" runat="server">
                        <Items>     
                            <x:Button ID="btnNewExamenes" Text="Examenes" Icon="PageWhiteStack" runat="server" Enabled="true"></x:Button>
                            </Items>
                    </x:Toolbar>
                        </Toolbars>    
                        <Columns>
                              <x:WindowField ColumnID="myWindowField1" Width="25px" WindowID="Window1" HeaderText=""
                                                Icon="attach" ToolTip="Descargar Adjuntos" DataTextFormatString="{0}" 
                                                DataIFrameUrlFields="Dni" DataIFrameUrlFormatString="../ExternalUser/FRM031I.aspx?Dni={0}" 
                                                DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Archivos Adjuntos" />                                          

                                <x:boundfield Width="270px" DataField="v_Pacient" DataFormatString="{0}" HeaderText="Trabajador" />
                                <x:boundfield Width="80px" DataField="d_ServiceDate" DataFormatString="{0:d}" HeaderText="Fecha" />                                           
                                <x:boundfield Width="250px" DataField="v_ProtocolName" DataFormatString="{0}" HeaderText="Protocolo" />     
                                <x:boundfield Width="140px" DataField="v_ServiceId" DataFormatString="{0}" HeaderText="Id Atencion" />                                                        
                            </Columns>
                    </x:Grid>
                </Items> 
            </x:GroupPanel>
             <x:Accordion ID="Accordion1" Title="Accordion Control" runat="server" Width="350px" Height="370px"
                        EnableFill="true" ShowBorder="True" ActiveIndex="0" ShowHeader="false" Enabled="false">
                <Panes>
                    <x:AccordionPane ID="AccordionPane1" runat="server" Title="DIAGNÓSTICOS" Icon="Note" Width="270PX"  AutoScroll="true" BodyPadding="2px 5px" ShowBorder="false">
                        <Items>
                <x:Panel ID="Panel51AA" Title="" EnableBackgroundColor="false" Height="310px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="false" Layout="Table" TableConfigColumns="3">
                <Items>     
                    <x:Panel ID="Panel51AAA" runat="server"   ShowBorder="true" ShowHeader="false" Title=""  EnableBackgroundColor="false" TableColspan="3"
                            BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5">
                        <Items>
                            <x:Grid ID="grdDx" ShowBorder="true" ShowHeader="false" runat="server"   Height="130px" Width="980px"
                            EnableRowNumber="True" EnableMouseOverColor="true" ShowGridHeader="true"   DataKeyNames="v_DiagnosticRepositoryId,v_ComponentId" 
                            EnableTextSelection="true" EnableAlternateRowColor="true" BoxMargin="5" OnRowClick="grdDx_RowClick" EnableRowClick="true">
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
                                    <x:LinkButtonField TextAlign="Center" ConfirmText="Al eliminar este diagnóstico se le eliminará también las recomendaciones y restricciónes vinculadas a este diagnóstico.¿Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                        ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Dx" CommandName="DeleteAction" />
                                    <x:boundfield Width="100px" DataField="v_ComponentName" DataFormatString="{0}" HeaderText="Consultorio" />   
                                    <x:boundfield Width="250px" DataField="v_DiseasesName" DataFormatString="{0}" HeaderText="Diagnóstico" />   
                                    <x:boundfield Width="200px" DataField="v_RecomendationsName" DataFormatString="{0}" HeaderText="Recomendaciones" />
                                    <x:boundfield Width="200px" DataField="v_RestrictionsName" DataFormatString="{0}" HeaderText="Restricciones" />                                                                                                      
                                </Columns>
                            </x:Grid>
                        </Items>
                    </x:Panel>   
                    <x:Panel ID="Panel52AA" runat="server"   ShowBorder="true" ShowHeader="false" Title="" Height="20"  EnableBackgroundColor="false" TableColspan="3"
                        BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5">
                        <Items>

                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel53AA" runat="server"   ShowBorder="true"  Title="RECOMENDACIONES" Height="150"  EnableBackgroundColor="false" TableColspan="1"
                        BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="480" >
                        <Items>
                            <x:Grid ID="grdRecomendaciones" ShowBorder="true" ShowHeader="false" runat="server"  AutoScroll="true" Height="120px" Width="480px"
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
                                        DataIFrameUrlFields="v_RecommendationId,v_MasterRecommendationId" DataIFrameUrlFormatString="../Auditar/FRM033B.aspx?v_RecommendationId={0}&v_MasterRecommendationId={1}" 
                                        DataWindowTitleField="v_RecommendationName" DataWindowTitleFormatString=" {0}" />

                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                        ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Recomendación" CommandName="DeleteAction" />
                                    <x:boundfield Width="500px" DataField="v_RecommendationName" DataFormatString="{0}" HeaderText="Recomendaciones" />                                                        
                                </Columns>
                            </x:Grid>
                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel54AA" runat="server"   ShowBorder="true"  Title="" ShowHeader="false" Height="5"  EnableBackgroundColor="false" TableColspan="1"
                        BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="20">
                        <Items>

                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel55AA" runat="server"   ShowBorder="true"  Title="RESTRICCIONES" Height="150"  EnableBackgroundColor="false" TableColspan="1"
                        BoxConfigAlign="Top" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" Width="480">
                        <Items>
                            <x:Grid ID="grdRestricciones" ShowBorder="true" ShowHeader="false" runat="server"  AutoScroll="true" Height="120px" Width="480px"
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
                                        DataIFrameUrlFields="v_RestrictionId,v_MasterRestrictionId" DataIFrameUrlFormatString="../Auditar/FRM033E.aspx?v_RestrictionId={0}&v_MasterRestrictionId={1}" 
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
                    </x:AccordionPane>   
                    <x:AccordionPane ID="AccordionPane2" runat="server" Title="APTITUD" Icon="User" Width="270PX"  AutoScroll="true" BodyPadding="2px 5px" ShowBorder="false">
                        <Items>
                            <x:Toolbar runat="server">
                                <Items>
                                    <x:Button ID="btnGrabarAptitud" Text="Grabar Aptitud" Icon="SystemSave" runat="server" OnClick="btnGrabarAptitud_Click"></x:Button>
                                </Items>
                            </x:Toolbar>
                            <x:Panel ID="Panel105aa" Title="" EnableBackgroundColor="true" Height="280px" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="false">
                                <Items>
                                    <x:Form ID="Form267aa" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow706aa" ColumnWidths="280px" runat="server" >
                                                <Items>
                                                    <x:DropDownList ID="ddlAptitud" runat="server"  Label="Aptitud" Width="140px" ShowLabel="true"></x:DropDownList> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow706qq" ColumnWidths="280px" runat="server" >
                                                <Items>
                                                   <x:TextArea ID="txtComentarioAptitud" runat="server" Text="" Label="Comentario" ShowLabel="true" Height="40"></x:TextArea>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow707qq" ColumnWidths="280px" runat="server" >
                                                <Items>
                                                     <x:TextArea ID="txtComentarioMedico" runat="server" Text="" Label="Comentario Médico" ShowLabel="true" Height="40" Hidden="true"></x:TextArea>
                                                </Items>
                                            </x:FormRow>
                                              <x:FormRow ID="FormRow706aaa" ColumnWidths="280px" runat="server" >
                                                <Items>
                                                     <x:TextArea ID="txtRestriccionesAptitud" runat="server" Text="" Label="Restricciones" ShowLabel="true" Height="40" Hidden="true"></x:TextArea>
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
                     <x:Tab ID="TabAnexo312" BodyPadding="5px" Title="Anexo - 312" runat="server"  Hidden="false"   Visible="false">
                        <Toolbars>
                            <x:Toolbar ID="Toolbar4" runat="server">
                                <Items>
                                    <x:Button ID="btnTooAnexo312" Text="Grabar Anexo - 312" Icon="SystemSave" runat="server" OnClick="btnGrabar_Click" ></x:Button>
                                    <x:Button ID="btnTooReporte312" Text="Visualizar Reporte Anexo - 312" Icon="Report" runat="server" OnClientClick="openHelloFineUI();">
                                    </x:Button>
                                </Items>
                            </x:Toolbar>
                        </Toolbars>
                        <Items>

            <x:Panel ID="Panel1" Title="FILIACIÓN" EnableBackgroundColor="true" Height="150px" runat="server"
                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                <Items>
                     <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                        <Rows>
                            <x:FormRow ID="FormRow1" ColumnWidths="950px" runat="server">
                                <Items> 
                                    <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow2" ColumnWidths="140px 100px 100px 120px 80px 40px 80px 120px 180px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label1" runat="server" Text="Fecha de Nacimiento" ShowLabel="false"></x:Label>
                                                    <x:NumberBox ID="txtDia" Label="Día" CssClass="mright" Required="true" runat="server" Width="30" MaxValue="31" MinValue="1" MinLengthMessage="Fuera de rango"></x:NumberBox>      
                                                    <x:NumberBox ID="txtMes" Label="Mes" CssClass="mright" Required="true" runat="server" Width="30" MaxValue="12" MinValue="1"> </x:NumberBox>  
                                                    <x:NumberBox ID="txtAnio" Label="Año" CssClass="mright" Required="true" runat="server" Width="50" MaxValue="2200" MinValue="1900"> </x:NumberBox>      
                                                    <x:NumberBox ID="txtEdad" Label="Edad" CssClass="mright"  runat="server" Width="30" Readonly="true"></x:NumberBox>       
                                                    <x:Label ID="label2" runat="server" Text="(años)" ShowLabel="false"></x:Label>   
                                                    <x:Label ID="label3" runat="server" Text="Documento:" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlDocumento" runat="server"  Label="Documento" Width="100px" ShowLabel="false"></x:DropDownList>    
                                                    <x:TextBox ID="txtNroDocumento" Label="Nro" CssClass="mright"  runat="server" Width="100" Required="true" AutoPostBack="true"></x:TextBox>     
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow3" ColumnWidths="140px 800px " runat="server" >
                                                <Items>
                                                    <x:Label ID="label4" runat="server" Text="Dirección Fiscal:" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtDireccionFiscal"  ShowLabel="false" CssClass="mright"  runat="server" Width="785" Required="true" AutoPostBack="true"></x:TextBox>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow4" ColumnWidths="140px 130px    70px 150px      60px 150px     180px 50px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label5" runat="server" Text="Departamento" ShowLabel="false"></x:Label>
                                                      <x:DropDownList ID="ddlDepartamento" runat="server"  Label="Departamento" Width="120px" ShowLabel="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged"></x:DropDownList> 

                                                     <x:Label ID="label6" runat="server" Text="Provincia" ShowLabel="false"></x:Label>   
                                                     <x:DropDownList ID="ddlProvincia" runat="server"  Label="Provincia" Width="120px" ShowLabel="false" AutoPostBack="true" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"></x:DropDownList>  

                                                     <x:Label ID="label7" runat="server" Text="Distrito" ShowLabel="false"></x:Label>  
                                                     <x:DropDownList ID="ddlDistrito" runat="server"  Label="Distrito" Width="120px" ShowLabel="false"></x:DropDownList>    

                                                     <x:Label ID="label8" runat="server" Text="Reside en lugar de Trabajo" ShowLabel="false"></x:Label>  
                                                     <x:DropDownList ID="ddlResideSiNo" runat="server"  Width="45px" ShowLabel="false"></x:DropDownList>    
                                                </Items>
                                            </x:FormRow>      
                                            <x:FormRow ID="FormRow5" ColumnWidths="140px 40px    79px 210px    80px 150px  225px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label9" runat="server" Text="Tiempo de Residencia" ShowLabel="false"></x:Label>
                                                     <x:NumberBox ID="txtTiempoResidenci"  ShowLabel="false" CssClass="mright" Required="true" runat="server" Width="30" MaxValue="100" MinValue="1" MinLengthMessage="Fuera de rango"></x:NumberBox>      

                                                     <x:Label ID="label10" runat="server" Text="Teléfono(s)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTelefonos" ShowLabel="false" Label="Teléfono" CssClass="mright"  runat="server" Width="203" Required="true" ></x:TextBox>  

                                                      <x:Label ID="label11" runat="server" Text="Tipo Segu." ShowLabel="false"></x:Label>  
                                                     <x:DropDownList ID="ddlTipoSeguro" runat="server"   Width="120px" ShowLabel="false"></x:DropDownList>    

                                                     <x:TextBox ID="txtEmail" ShowLabel="true" Label="Email" CssClass="mright"  runat="server" Width="180" Required="true" ></x:TextBox>  
                                                </Items>
                                            </x:FormRow>      
                                            <x:FormRow ID="FormRow6" ColumnWidths="140px 130px    130px 190px    200px   150px  " runat="server" >
                                                <Items>
                                                     <x:Label ID="label12" runat="server" Text="Estado Civil" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEstadoCivil" runat="server"  Label="Distrito" Width="120px" ShowLabel="false"></x:DropDownList>   

                                                     <x:Label ID="label13" runat="server" Text="Grado de Intrucción" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlGradoInstruccion" runat="server"  Width="175px" ShowLabel="false"></x:DropDownList> 
                                                      
                                                  
                                                    <x:NumberBox ID="txtNroHijosVivos"  Label="Hijos Vivos" CssClass="mright" Required="true" runat="server" Width="30" MaxValue="31" MinValue="1" MinLengthMessage="Fuera de rango"></x:NumberBox>      
                                                   
                                                   
                                                    <x:NumberBox ID="txtNroHijosMuertos" Label="Hijos Muertos" CssClass="mright" Required="true" runat="server" Width="30" MaxValue="31" MinValue="1" MinLengthMessage="Fuera de rango"></x:NumberBox>      
                                                   
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
            <x:Panel ID="Panel3" Title="ANTECEDENTES OCUPACIONALES" EnableBackgroundColor="true" Height="170px" runat="server"
                BodyPadding="5px" ShowBorder="true" ShowHeader="true" AutoScroll="true">
                <Items>
                     <x:Form ID="Form12" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                        <Rows>
                            <x:FormRow ID="FormRow22"  runat="server" >
                                <Items>                            
                                    <x:Grid ID="grdDataHistoriaOcupacional" ShowBorder="true" ShowHeader="false"  runat="server"  AutoScroll="true" Height="150px">  
                                        <Columns> 
                                            <x:TemplateField HeaderText="Empresa" Width="180px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="v_Organization" runat="server" Width="160px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("v_Organization") %>' TextMode="MultiLine" Height="30"></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>   
                                            <x:TemplateField HeaderText="Área" Width="110px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="v_TypeActivity" runat="server" Width="90px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("v_TypeActivity") %>' TextMode="MultiLine" Height="30"></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>  
                                              <x:TemplateField HeaderText="Ocupación" Width="150px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="v_Workstation" runat="server" Width="130px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("v_Workstation") %>' TextMode="MultiLine" Height="30"></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>  
                                              <x:TemplateField HeaderText="Fecha" Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="v_Fechas" runat="server" Width="80px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("v_Fechas") %>'  TextMode="MultiLine" Height="30"></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>  
                                              <x:TemplateField HeaderText="Exposición" Width="150px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="v_Exposicion" runat="server" Width="130px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("v_Exposicion") %>' TextMode="MultiLine" Height="30"></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>  
                                            <x:TemplateField HeaderText="Tiempo Trabajo" Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="v_TiempoTrabajo" runat="server" Width="80px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("v_TiempoTrabajo") %>' TextMode="MultiLine" Height="30"></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>   
                                            <x:TemplateField HeaderText="EPP" Width="150px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="v_Epps" runat="server" Width="130px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("v_Epps") %>' TextMode="MultiLine" Height="30"></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>  
                                            <x:boundfield Width="150px" DataField="v_HistoryId" DataFormatString="{0}" HeaderText="v_HistoryId"  /> 
                                        </Columns>
                                    </x:Grid>
                                </Items>
                            </x:FormRow>
                        </Rows>
                    </x:Form>
                </Items>
            </x:Panel>
            <x:Panel ID="Panel4" Title="ANTECEDENTES PATOLÓGICOS PERSONALES" EnableBackgroundColor="true" Height="140px" runat="server"
                BodyPadding="5px" ShowBorder="true" ShowHeader="true" AutoScroll="true">
                <Items>
                       <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                        <Rows>
                            <x:FormRow ID="FormRow7" ColumnWidths="950px" runat="server">
                                <Items> 
                                    <x:Form ID="Form5" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow8"  runat="server" >
                                                <Items>
                                                     <x:Grid ID="grdDataPatologicoPersonal" ShowBorder="true" ShowHeader="false"  runat="server"  AutoScroll="true" Height="560px">  
                                                       
                                                         <Columns> 
                                                            <x:TemplateField HeaderText="ENFERMEDAD" Width="140px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Enfermedad" runat="server" Width="120px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                                        Text='<%# Eval("Enfermedad") %>' ReadOnly="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </x:TemplateField>  

                                                            <x:TemplateField HeaderText="FECHA" Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="v_FechaInicio" runat="server" Width="80px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                                        Text='<%# Eval("v_FechaInicio") %>' ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </x:TemplateField>  

                                                           <x:CheckBoxField ColumnID="CheckBoxField2" Width="80px" RenderAsStaticField="false"
                                                                CommandName="CheckBox3" DataField="AsociadoTrabajo" HeaderText="TRABAJO" TextAlign="Center" />  
                                                                                                      
                                                            <x:TemplateField HeaderText="DETALLE" Width="540px" TextAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="v_DiagnosticDetail" runat="server" Width="520px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                                        Text='<%# Eval("v_DiagnosticDetail") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </x:TemplateField>  

                                                              <x:TemplateField HeaderText="DESCANSO" Width="80px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="v_TreatmentSite" runat="server" Width="60px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                                        Text='<%# Eval("v_TreatmentSite") %>' ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </x:TemplateField>  
                                                            <x:boundfield Width="150px" DataField="v_PersonMedicalHistoryId" DataFormatString="{0}" HeaderText="v_PersonMedicalHistoryId" Hidden="false" />
                                                            <x:boundfield Width="150px" DataField="v_DiseasesId" DataFormatString="{0}" HeaderText="v_DiseasesId" Hidden="false" />
                                                        </Columns>
                                                    </x:Grid>
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
            <x:Panel ID="Panel5" Title="HÁBITOS NOCIVOS" EnableBackgroundColor="true" Height="135px" runat="server"
                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                <Items>
                    <x:Grid ID="grdDataHabitosNocivos" ShowBorder="true" ShowHeader="false"  runat="server"  AutoScroll="true" Height="120px" OnRowDataBound="grdDataHabitosNocivos_RowDataBound">  
                        <Columns> 
                             <x:TemplateField HeaderText="HÁBITOS NOCIVOS" Width="180px">
                                <ItemTemplate>
                                    <asp:TextBox ID="Habito" runat="server" Width="160px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                        Text='<%# Eval("Habito") %>' ReadOnly="true"></asp:TextBox>
                                </ItemTemplate>
                            </x:TemplateField>   
                            <x:TemplateField Width="200px" HeaderText="FRECUENCIA" >
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlFrecuencia" AutoPostBack="false" Width="180">
                                        <asp:ListItem Text="Seleccionar" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Exceso" Value="1" ></asp:ListItem>
                                        <asp:ListItem Text="Habitual" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Nada" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Poco" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </x:TemplateField>  
                            <x:TemplateField HeaderText="DESCRIPCIÓN" Width="580px">
                                <ItemTemplate>
                                    <asp:TextBox ID="v_Comment" runat="server" Width="560px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                        Text='<%# Eval("v_Comment") %>'></asp:TextBox>
                                </ItemTemplate>
                            </x:TemplateField>     
                            <x:boundfield Width="150px" DataField="v_NoxiousHabitsId" DataFormatString="{0}" HeaderText="v_NoxiousHabitsId"  />
                            <x:boundfield Width="150px" DataField="i_TypeHabitsId" DataFormatString="{0}" HeaderText="i_TypeHabitsId"  />
                        </Columns>
                    </x:Grid>
                </Items>
            </x:Panel>
            <x:Panel ID="Panel6" Title="ANTECEDENTES PATOLÓGICOS FAMILIARES" EnableBackgroundColor="true" Height="150px" runat="server"
                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                <Items>
                    <x:Grid ID="grdDataAntecedentesPatologicosFamiliares" ShowBorder="true" ShowHeader="false"  runat="server"  DataKeyNames="v_ProtocolComponentId" AutoScroll="true" Height="135px">  
                        <Columns> 
                            <x:TemplateField HeaderText="FAMILIA" Width="180px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFamilia" runat="server" Width="160px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                        Text='<%# Eval("v_Familia") %>' ReadOnly="true"></asp:TextBox>
                                </ItemTemplate>
                            </x:TemplateField>  
                            <x:TemplateField HeaderText="DESCRIPCIÓN" Width="780px">
                                <ItemTemplate>
                                    <asp:TextBox ID="v_CommentFamili" runat="server" Width="760px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                        Text='<%# Eval("v_CommentFamili") %>'></asp:TextBox>
                                </ItemTemplate>
                            </x:TemplateField>  
                            <x:boundfield Width="150px" DataField="v_FamilyMedicalAntecedentsId" DataFormatString="{0}" HeaderText="v_FamilyMedicalAntecedentsId"  />
                             <x:boundfield Width="150px" DataField="i_TypeFamilyId" DataFormatString="{0}" HeaderText="i_TypeFamilyId"  />
                        </Columns>
                    </x:Grid>
                </Items>
            </x:Panel>
            <x:Panel ID="PanelGinecologico" Title="ANTECEDENTES GINECOLÓGICOS" EnableBackgroundColor="true" Height="120px" runat="server"
                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Enabled="true">
                <Items>
                    <x:Form ID="Form10" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                        <Rows>
                            <x:FormRow ID="FormRow9" ColumnWidths="950px" runat="server">
                                <Items> 
                                    <x:Form ID="Form11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="120px" LabelAlign="Left" >
                                        <Rows>
                                             <x:FormRow ID="FormRow10" ColumnWidths="70px 150px        270px      50px 120px       180px 120px  " runat="server" >
                                                <Items>
                                                    <x:Label ID="label19" runat="server" Text="Menarquia" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtMenarquia" Label="Menarquia" CssClass="mright"  runat="server" Width="130" ShowLabel="false"></x:TextBox>      

                                                    <x:TextBox ID="txtGestacion" Label="Gestación y Paridad" CssClass="mright"  runat="server" Width="130"></x:TextBox>  

                                                    <x:Label ID="label20" runat="server" Text="FUM" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtFum" Label="" CssClass="mright"  runat="server" Width="100" ShowLabel="false"></x:TextBox>      

                                                    <x:Label ID="label21" runat="server" Text="MAC(Método anticonceptivo)" ShowLabel="false"></x:Label>                                                  
                                                    <x:DropDownList ID="ddlMac" runat="server" Width="110" ShowLabel="false"></x:DropDownList>                                                    
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow11" ColumnWidths="70px 150px   270px   50px 120px   180px 120px " runat="server" >
                                                <Items>
                                                    <x:Label ID="label22" runat="server" Text="Régimen Catamenial" ShowLabel="false"></x:Label>
                                                    <x:TextArea ID="txtRegimenCatamenial" Label="" CssClass="mright"  runat="server" Width="130" ShowLabel="false" Height="35"></x:TextArea>      
                                                 
                                                    <x:TextBox ID="txtCirugiaGineco" Label="Cirugía Ginecológicas" CssClass="mright"  runat="server" Width="130" Height="35"></x:TextBox>  
                                                   
                                                    <x:Label ID="label23" runat="server" Text="Último PAP" ShowLabel="false"></x:Label>
                                                    <x:TextArea ID="txtUltimoPAP" Label="" CssClass="mright"  runat="server" Width="100" ShowLabel="false" Height="35"></x:TextArea>      

                                                    <x:Label ID="label24" runat="server" Text="Resultado del último PAP" ShowLabel="false"></x:Label>
                                                    <x:TextArea ID="txtResultadoPAP" Label="" CssClass="mright"  runat="server" Width="110"  ShowLabel="false" Height="35"></x:TextArea>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow21" ColumnWidths="70px 150px   740px  " runat="server" >
                                                <Items>
                                                    <x:Label ID="label25" runat="server" Text="Última Mamografía" ShowLabel="false"></x:Label>
                                                    <x:TextArea ID="txtUltimaMamo" Label="" CssClass="mright"  runat="server" Width="130" ShowLabel="false" Height="35"></x:TextArea>      

                                                    <x:TextArea ID="txtResultadoMamo" Label="Resultado Mamografía" CssClass="mright"  runat="server" Width="130" Height="35"></x:TextArea>  
                                                   
                 
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
            <x:Panel ID="Panel7" Title="EVALUACIÓN MÉDICA" EnableBackgroundColor="true" Height="645px" runat="server"
                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                <Items>
                    <x:Form ID="Form6" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                        <Rows>
                            <x:FormRow ID="FormRow12" ColumnWidths="950px" runat="server">
                                <Items> 
                                    <x:Form ID="Form7" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow13" ColumnWidths="70px 890px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label14" runat="server" Text="ANAMNESIS" ShowLabel="false"></x:Label>  
                                                    <x:TextBox ID="txtAnamnesis" runat="server" Text="" Width="820" ></x:TextBox>     
                                                </Items>
                                            </x:FormRow>     
                                            <x:FormRow ID="FormRow14" ColumnWidths="135px  120px  130px  120px  120px  140px  140px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label15" runat="server" Text="EXAMEN CLÍNICO" ShowLabel="false"></x:Label>  
                                                    <x:TextBox ID="txtTalla" runat="server" Text="" Width="40" Label="TALLA(m)" OnTextChanged="txtTalla_TextChanged" AutoPostBack="true"></x:TextBox>    
                                                    <x:TextBox ID="txtPeso" runat="server" Text="" Width="45" Label="PESO(kg.)" OnTextChanged="txtPeso_TextChanged"  AutoPostBack="true"></x:TextBox> 
                                                    <x:TextBox ID="txtImc" runat="server" Text="" Width="40" Label="IMC" Enabled="false"></x:TextBox> 
                                                    <x:TextBox ID="txtIcc" runat="server" Text="" Width="40" Label="ICC" Enabled="false"></x:TextBox> 
                                                    <x:TextBox ID="txtfres" runat="server" Text="" Width="50" Label="F.RESP" ></x:TextBox> 
                                                    <x:TextBox ID="txtFcar" runat="server" Text="" Width="50" Label="F.CARD" ></x:TextBox>  
                                                </Items>
                                            </x:FormRow>      
                                            <x:FormRow ID="FormRow15" ColumnWidths="135px  120px  130px  120px  120px  140px  140px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label16" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                    <x:TextBox ID="txtParterial" runat="server" Text="" Width="40" Label="P.ARTERIAL" ></x:TextBox>    
                                                    <x:TextBox ID="txtTemp" runat="server" Text="" Width="45" Label="TEMP(C)" ></x:TextBox> 
                                                    <x:TextBox ID="txtPcadera" runat="server" Text="" Width="40" Label="P.CADERA" OnTextChanged="txtPcadera_TextChanged"  AutoPostBack="true"></x:TextBox> 
                                                    <x:TextBox ID="txtPadb" runat="server" Text="" Width="40" Label="P.ADB." OnTextChanged="txtPadb_TextChanged"  AutoPostBack="true"></x:TextBox> 
                                                    <x:TextBox ID="txtGcorporal" runat="server" Text="" Width="50" Label="%G.CORP." ></x:TextBox> 
                                                    <x:TextBox ID="txtSatO2" runat="server" Text="" Width="50" Label="SAT. O2" ></x:TextBox>  
                                                </Items>
                                            </x:FormRow>     
                                            <x:FormRow ID="FormRow16" ColumnWidths="135px  250px  115px  250px  " runat="server" >
                                                <Items>
                                                    <x:Label ID="label17" runat="server" Text="ECTOSCOPIA" ShowLabel="false"></x:Label>  
                                                    <x:TextBox ID="TextBox14" runat="server" Text="" Width="240" Label="" ShowLabel="false" ></x:TextBox>  
                                                    
                                                      <x:Label ID="label18" runat="server" Text="ESTADO MENTAL" ShowLabel="false"></x:Label>  
                                                    <x:TextBox ID="TextBox15" runat="server" Text="" Width="240" Label="" ShowLabel="false" ></x:TextBox>  
                                                </Items>
                                            </x:FormRow>    
                                            <x:FormRow ID="FormRow17" ColumnWidths="135px  150px  650px  " runat="server" >
                                                <Items>
                                                     <x:Grid ID="grdDataOrgano" ShowBorder="true" ShowHeader="false"  runat="server"  DataKeyNames="v_ProtocolComponentId" AutoScroll="true" Height="500px">  
                                                        <Columns> 

                                                            <x:TemplateField HeaderText="ÓRGANO O SISTEMA" Width="180px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Organo" runat="server" Width="160px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                                        Text='<%# Eval("Organo") %>' ReadOnly="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </x:TemplateField>  

                                                           <x:CheckBoxField ColumnID="CheckBoxField1" Width="120px" RenderAsStaticField="false"
                                                                CommandName="CheckBox2" DataField="SinHallazgo" HeaderText="SIN HALLAZGOS" TextAlign="Center" />
                                            
                                                            <x:TemplateField HeaderText="HALLAZGOS" Width="650px" TextAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Hallazgos" runat="server" Width="600px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                                        Text='<%# Eval("Hallazgos") %>' ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </x:TemplateField>  
                                                            <x:boundfield Width="270px" DataField="v_ComponentFieldId_1" DataFormatString="{0}" HeaderText="v_ComponentFieldId_1"  />
                                                            <x:boundfield Width="270px" DataField="v_ComponentFieldId_2" DataFormatString="{0}" HeaderText="v_ComponentFieldId_2"  />
                                                        </Columns>
                                                    </x:Grid>
                                                </Items>
                                            </x:FormRow>   
                                            <x:FormRow ID="FormRow18" ColumnWidths="135px" runat="server" >
                                                <Items>
                                                    <x:Button Id="btnGrabar" runat="server" Icon="SystemSave" Text="---------Grabar-------------" OnClick="btnGrabar_Click" ></x:Button>
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
                            
                    
                              
                        </Items>
                    </x:Tab>
               
                    <x:Tab ID="TabOsteomuscular" BodyPadding="5px" Title="Osteomuscular" runat="server"  Hidden="false"  Visible="false" >
                        <Toolbars>
                            <x:Toolbar ID="Toolbar5" runat="server">
                                <Items>
                                    <x:Button ID="Button1" Text="Grabar Osteomuscular" Icon="SystemSave" runat="server" OnClick="btnOsteoMuscular_Click"></x:Button>                            
                                </Items>
                            </x:Toolbar>
                        </Toolbars>
                        <Items>

                         <x:Panel ID="Panel8" Title="1. CUESTONARIO DE SINTOMAS  ( comentar si tiene relación o no con el puesto de trabajo)" EnableBackgroundColor="true" Height="740px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                     <x:Form ID="Form13" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                        <Rows>
                                            <x:FormRow ID="FormRow23" ColumnWidths="950px" runat="server">
                                                <Items> 
                                                    <x:Form ID="Form14" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                        <Rows>
                                                            <x:FormRow ID="FormRow24" ColumnWidths="480px 480px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox1" Label="" CssClass="mright"  runat="server" Width="460" Text="                                 RESPONDA EN TODOS LOS CASOS" ShowLabel="false" Readonly="true"></x:TextBox>     
                                                                    <x:TextBox ID="TextBox5" Label="" CssClass="mright"  runat="server" Width="460" Text="                      RESPONDA SOLAMENTE SI HA TENIDO PROBLEMAS" ShowLabel="false" Readonly="true"></x:TextBox>     
                                                                </Items>
                                                            </x:FormRow>  


                                                            <x:FormRow ID="FormRow25" ColumnWidths="480px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextArea ID="TextBox2" Label="" CssClass="mright"  runat="server" Width="460" Text="Ha tenido problemas (dolor, aumento de volumen, curvaturas, etc.) en los 12 meses, a nivel de:" ShowLabel="false" Readonly="true" Height="65" ></x:TextArea>     
                                                                    <x:TextArea ID="TextBox3" Label="" CssClass="mright"  runat="server" Width="220" Text="Durante los últimos 12 meses ha estado incapacitado para su trabajo (en casa, o fuera) por causa del problema:" ShowLabel="false" Readonly="true" Height="65"></x:TextArea>     
                                                                    <x:TextArea ID="TextBox4" Label="" CssClass="mright"  runat="server" Width="220" Text="Ha tenido problemas en los últimos siete días:" ShowLabel="false" Readonly="true" Height="65"></x:TextArea>     
                                                                </Items>
                                                            </x:FormRow>  


                                                            <x:FormRow ID="FormRow26" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextArea1" Label="" CssClass="mright"  runat="server" Width="130" Text="Nuca / Cuello" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlNuca1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlNuca2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlNuca3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  


                                                            <x:FormRow ID="FormRow27" ColumnWidths="960px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox6" Label="" CssClass="mright"  runat="server" Width="130" Text="Hombros" ShowLabel="false" Readonly="true"  ></x:TextBox>                                                                       
                                                                </Items>
                                                            </x:FormRow>  


                                                             <x:FormRow ID="FormRow28" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox7" Label="" CssClass="mright"  runat="server" Width="130" Text="Hombro Derecho" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlHombroDerecho1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlHombroDerecho2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlHombroDerecho3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  


                                                            <x:FormRow ID="FormRow29" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox8" Label="" CssClass="mright"  runat="server" Width="130" Text="Hombro Izquierdo" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlHombroIzquierdo1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlHombroIzquierdo2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlHombroIzquierdo3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  


                                                            <x:FormRow ID="FormRow30" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox9" Label="" CssClass="mright"  runat="server" Width="130" Text="Ambos Hombros" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlAmbosHombros1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlAmbosHombros2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlAmbosHombros3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  


                                                            <x:FormRow ID="FormRow31" ColumnWidths="960px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox10" Label="" CssClass="mright"  runat="server" Width="130" Text="Codos" ShowLabel="false" Readonly="true"  ></x:TextBox>                                                             
                                                                    </Items>
                                                            </x:FormRow>  


                                                             <x:FormRow ID="FormRow32" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox11" Label="" CssClass="mright"  runat="server" Width="130" Text="Codo Derecho" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlCodoDerecho1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlCodoDerecho2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlCodoDerecho3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  


                                                            <x:FormRow ID="FormRow33" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox12" Label="" CssClass="mright"  runat="server" Width="130" Text="Codo Izquierdo" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlCodoIzquierdo1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlCodoIzquierdo2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlCodoIzquierdo3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  


                                                            <x:FormRow ID="FormRow34" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox13" Label="" CssClass="mright"  runat="server" Width="130" Text="Ambos Codos" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlAmboscodos1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlAmboscodos2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlAmboscodos3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  

                                                             <x:FormRow ID="FormRow35" ColumnWidths="960px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox16" Label="" CssClass="mright"  runat="server" Width="130" Text="Muñecas / Manos" ShowLabel="false" Readonly="true"  ></x:TextBox>                                                             
                                                                    </Items>
                                                            </x:FormRow>  
                                                             <x:FormRow ID="FormRow36" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox17" Label="" CssClass="mright"  runat="server" Width="130" Text="Derecha" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlManosDerecha1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlManosDerecha2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlManosDerecha3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  
                                                            <x:FormRow ID="FormRow37" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox18" Label="" CssClass="mright"  runat="server" Width="130" Text="Izquierda" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlManosIzquierda1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlManosIzquierda2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlManosIzquierda3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  
                                                            <x:FormRow ID="FormRow38" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox19" Label="" CssClass="mright"  runat="server" Width="130" Text="Ambas" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlAmbasManos1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlAmbasManos2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlAmbasManos3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow> 
                                                            
                                                            <x:FormRow ID="FormRow39" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox20" Label="" CssClass="mright"  runat="server" Width="130" Text="Columna Dorsal" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlColumnadorsal1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlColumnadorsal2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlColumnadorsal3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  
                                                            
                                                             


                                                             <x:FormRow ID="FormRow40" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox21" Label="" CssClass="mright"  runat="server" Width="130" Text="Columna Lumbar" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlColumnaLumbar1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlColumnaLumbar2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlColumnaLumbar3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  

                                                            <x:FormRow ID="FormRow41" ColumnWidths="960px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox22" Label="" CssClass="mright"  runat="server" Width="130" Text="Caderas" ShowLabel="false" Readonly="true"  ></x:TextBox>                                                             
                                                                    </Items>
                                                            </x:FormRow>  
                                                             <x:FormRow ID="FormRow42" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox23" Label="" CssClass="mright"  runat="server" Width="130" Text="Derecha" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlCaderaDerecha1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlCaderaDerecha2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlCaderaDerecha3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  
                                                            <x:FormRow ID="FormRow43" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox24" Label="" CssClass="mright"  runat="server" Width="130" Text="Izquierda" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlCaderaIzquierda1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlCaderaIzquierda2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlCaderaIzquierda3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  
                                                            <x:FormRow ID="FormRow44" ColumnWidths="960px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox25" Label="" CssClass="mright"  runat="server" Width="130" Text="Rodillas" ShowLabel="false" Readonly="true"  ></x:TextBox>                                                             
                                                                    </Items>
                                                            </x:FormRow>  
                                                             <x:FormRow ID="FormRow45" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox26" Label="" CssClass="mright"  runat="server" Width="130" Text="Derecha" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlRodillaDerecha1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlRodillaDerecha2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlRodillaDerecha3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  
                                                            <x:FormRow ID="FormRow46" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox27" Label="" CssClass="mright"  runat="server" Width="130" Text="Izquierda" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlRodillaIzquierda1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlRodillaIzquierda2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlRodillaIzquierda3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  




                                                            <x:FormRow ID="FormRow47" ColumnWidths="960px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox28" Label="" CssClass="mright"  runat="server" Width="130" Text="Tobillos / Pies" ShowLabel="false" Readonly="true"  ></x:TextBox>                                                             
                                                                    </Items>
                                                            </x:FormRow>  
                                                             <x:FormRow ID="FormRow48" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox29" Label="" CssClass="mright"  runat="server" Width="130" Text="Derecho" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlTobillosDerecho1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlTobillosDerecho2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlTobillosDerecho3" runat="server" Width="130"></x:DropDownList>
                                                                    </Items>
                                                            </x:FormRow>  
                                                            <x:FormRow ID="FormRow49" ColumnWidths="150px 330px 240px 240px" runat="server">
                                                                <Items>
                                                                    <x:TextBox ID="TextBox30" Label="" CssClass="mright"  runat="server" Width="130" Text="Izquierdo" ShowLabel="false" Readonly="true"  ></x:TextBox>     
                                                                    <x:DropDownList ID="ddlTobillosIzquierdo1" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlTobillosIzquierdo2" runat="server" Width="130"></x:DropDownList>
                                                                    <x:DropDownList ID="ddlTobillosIzquierdo3" runat="server" Width="130"></x:DropDownList>
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
                        <x:Panel ID="Panel9" Title="EXAMEN FÍSICO - COLUMNA VERTEBRAL" EnableBackgroundColor="true" Height="1380px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" >
                                <Items>                        
                                    <x:GroupPanel runat="server" Title="Eje Antero Posterior" ID="GroupPanel13" BoxFlex="1" Height="120" TableColspan="1" Width="940px" >                
                                        <Items>
                                            <x:Form ID="Form27" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow50" ColumnWidths =" 400px   300px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label49" runat="server" Text="Curvas Fisiológicas (ant-post)" ShowLabel="false"></x:Label> 
                                                            <x:Label ID="Label53" runat="server" Text="Eje Lateral" ShowLabel="false"></x:Label>                                                                                  
                                                        </Items> 
                                                    </x:FormRow>  
                                                    <x:FormRow ID="FormRow99" ColumnWidths="100px   300px  100px  300px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label50" runat="server" Text="Cervical" ShowLabel="false"></x:Label>    
                                                            <x:DropDownList ID="ddlCervical" runat="server" Width="250" ShowLabel="false" ></x:DropDownList>      

                                                            <x:Label ID="Label54" runat="server" Text="dorsal" ShowLabel="false"></x:Label>    
                                                            <x:DropDownList ID="ddlDorsalEjeLateral" runat="server" Width="250" ShowLabel="false" ></x:DropDownList>                                                                     
                                                        </Items> 
                                                    </x:FormRow>  
                                                    <x:FormRow ID="FormRow101" ColumnWidths="99px   300px  100px  300px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label51" runat="server" Text="dorsal" ShowLabel="false"></x:Label>    
                                                            <x:DropDownList ID="ddlDorsal" runat="server" Width="250" ShowLabel="false" ></x:DropDownList>    
                                                                            
                                                            <x:Label ID="Label55" runat="server" Text="Lumbar" ShowLabel="false"></x:Label>    
                                                            <x:DropDownList ID="ddlLumbarEjeLateral" runat="server" Width="250" ShowLabel="false" ></x:DropDownList>                                                                       
                                                        </Items> 
                                                    </x:FormRow>  
                                                        <x:FormRow ID="FormRow102" ColumnWidths="100px   300px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label52" runat="server" Text="Lumbar" ShowLabel="false"></x:Label>    
                                                            <x:DropDownList ID="ddlLumbar" runat="server" Width="250" ShowLabel="false" ></x:DropDownList>                                                                       
                                                        </Items> 
                                                    </x:FormRow>  
                                                          
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="MOVILIDAD-DOLOR (valorar según tabla1)" ID="GroupPanel14" BoxFlex="1" Height="110" TableColspan="1" >                
                                        <Items>
                                            <x:Form ID="Form16" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow103" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                        <Items>
                                                            <x:Label ID="Label56" runat="server" Text="Columna Vertebral" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="Label57" runat="server" Text="Flexión" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="Label58" runat="server" Text="Extensión" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="Label59" runat="server" Text="Lateralización Derecha" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="Label60" runat="server" Text="Lateralización Izquierda" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="Label61" runat="server" Text="Rotación Derecha" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="Label62" runat="server" Text="Rotación Izquierda" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="Label63" runat="server" Text="Irradiación" ShowLabel="false"></x:Label>                                                                 
                                                        </Items> 
                                                    </x:FormRow>  
                                                    <x:FormRow ID="FormRow104" ColumnWidths="880px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label65" runat="server" Text=" " ShowLabel="false"></x:Label>                                                                                                           
                                                        </Items> 
                                                    </x:FormRow>  
                                                    <x:FormRow ID="FormRow51" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                        <Items>
                                                            <x:Label ID="Label64" runat="server" Text="Cervical" ShowLabel="false"></x:Label>    
                                                           <x:DropDownList ID="ddlCervicalFlexion" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>   
                                                            <x:DropDownList ID="ddlCervicalExtension" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlCervicalLatDere" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlCervicalLatIzq" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlCervicalRotaDere" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlCervicalRotaIzq" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlCervicalIrradiacion" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                          
                                                        </Items> 
                                                    </x:FormRow>  
                                                    <x:FormRow ID="FormRow105" ColumnWidths="110px   110px  110px  110px 110px   110px  110px  110px " runat="server">
                                                        <Items>
                                                            <x:Label ID="Label66" runat="server" Text="Dorso Lumbar" ShowLabel="false"></x:Label>    
                                                           <x:DropDownList ID="ddlDorsoFlexion" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>   
                                                            <x:DropDownList ID="ddlDorsoExtension" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlDorsoLateDere" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlDorsoLateIzq" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlDorsoRotaDere" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlDorsoRotaIzq" runat="server" Width="80" ShowLabel="false" ></x:DropDownList> 
                                                            <x:DropDownList ID="ddlDorsoIrradiacion" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                          
                                                        </Items> 
                                                    </x:FormRow>  
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>
                                            
                                    <x:GroupPanel runat="server" Title="TEST ESPECÍFICOS" ID="GroupPanel15" BoxFlex="1" Height="120" TableColspan="1" >                
                                        <Items>
                                            <x:Form ID="Form15" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow106" ColumnWidths="450px 450px" runat="server">
                                                        <Items>
                                            
                                                            <x:GroupPanel runat="server" Title="LASEGUE" ID="GroupPanel16" BoxFlex="1" Height="50" TableColspan="1" >                
                                                                <Items>
                                                                    <x:Form ID="Form29" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                                        <Rows>
                                                                            <x:FormRow ID="FormRow107" ColumnWidths="80px 120px 80px 120px" runat="server">
                                                                                <Items>
                                                                                    <x:Label ID="Label67" runat="server" Text="Derecha" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlLasegueDere" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>   
                                                                                    <x:Label ID="Label68" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlLasegueIzq" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>    
                                                                                </Items>
                                                                            </x:FormRow>
                                                                        </Rows>
                                                                    </x:Form>
                                                                </Items>
                                                            </x:GroupPanel>
                                                            
                                                            <x:GroupPanel runat="server" Title="SCHOBER" ID="GroupPanel17" BoxFlex="1" Height="50" TableColspan="1" >                
                                                                <Items>
                                                                     <x:Form ID="Form30" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                                        <Rows>
                                                                            <x:FormRow ID="FormRow108"  ColumnWidths="80px 120px 80px 120px" runat="server">
                                                                                <Items>
                                                                                    <x:Label ID="Label69" runat="server" Text="Derecha" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlSchoberDere" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>   
                                                                                    <x:Label ID="Label70" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlSchoberIzq" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>    
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

                                    <x:GroupPanel runat="server" Title="PALPITACIÓN" ID="GroupPanel18" BoxFlex="1" Height="110" TableColspan="1" >                
                                        <Items>
                                            <x:Form ID="Form31" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow109" ColumnWidths ="200px  300px   300px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label71" runat="server" Text=". " ShowLabel="false"></x:Label> 
                                                            <x:Label ID="Label72" runat="server" Text="Apófisis Espinosas" ShowLabel="false"></x:Label>  
                                                            <x:Label ID="Label73" runat="server" Text="Contractura Muscular" ShowLabel="false"></x:Label>                                                                                
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow110" ColumnWidths ="200px  300px   300px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label74" runat="server" Text="Columna Cervical" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkColumnaCervicalApofisis" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            <x:CheckBox ID="chkColumnaCervicalContractura" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                                                        
                                                        </Items> 
                                                    </x:FormRow> 
                                                     <x:FormRow ID="FormRow111" ColumnWidths ="199px  300px   300px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label75" runat="server" Text="Columna Dorsal" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkColumnaDorsalApofisis" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            <x:CheckBox ID="chkColumnaDorsalContractura" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                     <x:FormRow ID="FormRow112" ColumnWidths ="198px  300px   300px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label76" runat="server" Text="Columna Lumbar" ShowLabel="false"></x:Label> 
                                                             <x:CheckBox ID="chkColumnaLumbarApofisis" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                            <x:CheckBox ID="chkColumnaLumbarContractura" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                                                             
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="ARTICULACIONES: movilidad y dolor (valor según tabla 1)" ID="GroupPanel19" BoxFlex="1" Height="460" TableColspan="1" >                
                                        <Items>
                                            <x:Form ID="Form32" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow113" ColumnWidths ="100px  100px   100px  100px  100px   100px  100px  100px   100px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label77" runat="server" Text="Articulación" ShowLabel="false"></x:Label> 
                                                            <x:Label ID="Label78" runat="server" Text="Abducción" ShowLabel="false"></x:Label>  
                                                            <x:Label ID="Label79" runat="server" Text="Adución" ShowLabel="false"></x:Label>   
                                                            <x:Label ID="Label80" runat="server" Text="Flexión" ShowLabel="false"></x:Label>
                                                            <x:Label ID="Label81" runat="server" Text="Extensión" ShowLabel="false"></x:Label>  
                                                            <x:Label ID="Label82" runat="server" Text="R.Externa" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="Label83" runat="server" Text="R.Interna" ShowLabel="false"></x:Label>  
                                                            <x:Label ID="Label84" runat="server" Text="Irradiación" ShowLabel="false"></x:Label>   
                                                            <x:Label ID="Label85" runat="server" Text="Alter.Masa" ShowLabel="false"></x:Label>                                                                                 
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow114" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label86" runat="server" Text="Hombro Der." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlHD1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlHD2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHD3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHD4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHD5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHD6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHD7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHD8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow115" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label87" runat="server" Text="Hombro Izq." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlHI1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlHI2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHI3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHI4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHI5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHI6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHI7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlHI8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow116" ColumnWidths ="720px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label88" runat="server" Text="." ShowLabel="false"></x:Label> 
                                                                                                                                   
                                                        </Items> 
                                                    </x:FormRow> 
                                                       <x:FormRow ID="FormRow117" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label89" runat="server" Text="Codo Der." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlCD1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlCD2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCD3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCD4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCD5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCD6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCD7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCD8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow118" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label90" runat="server" Text="Codo Izq." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlCI1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlCI2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCI3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCI4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCI5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCI6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCI7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCI8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                     <x:FormRow ID="FormRow119" ColumnWidths ="720px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label91" runat="server" Text="." ShowLabel="false"></x:Label> 
                                                                                                                                   
                                                        </Items> 
                                                    </x:FormRow> 
                                                       <x:FormRow ID="FormRow120" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label92" runat="server" Text="Mune. Der." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlMuneD1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlMuneD2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneD3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneD4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneD5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneD6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneD7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneD8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow121" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label93" runat="server" Text="Mune. Izq." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlMuneI1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlMuneI2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneI3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneI4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneI5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneI6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneI7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlMuneI8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                     <x:FormRow ID="FormRow122" ColumnWidths ="720px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label94" runat="server" Text="." ShowLabel="false"></x:Label> 
                                                                                                                                   
                                                        </Items> 
                                                    </x:FormRow> 
                                                       <x:FormRow ID="FormRow123" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label95" runat="server" Text="Cadera Der." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlCaderaD1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlCaderaD2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaD3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaD4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaD5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaD6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaD7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaD8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow124" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label96" runat="server" Text="Cadera Izq." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlCaderaI1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlCaderaI2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaI3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaI4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaI5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaI6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaI7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlCaderaI8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow125" ColumnWidths ="720px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label97" runat="server" Text="." ShowLabel="false"></x:Label> 
                                                                                                                                   
                                                        </Items> 
                                                    </x:FormRow> 
                                                       <x:FormRow ID="FormRow126" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label98" runat="server" Text="Tobillo Der." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlTobilloD1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlTobilloD2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloD3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloD4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloD5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloD6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloD7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloD8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow127" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label99" runat="server" Text="Tobillo Izq." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlTobilloI1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlTobilloI2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloI3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloI4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloI5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloI6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloI7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlTobilloI8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow128" ColumnWidths ="720px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label100" runat="server" Text="." ShowLabel="false"></x:Label> 
                                                                                                                                   
                                                        </Items> 
                                                    </x:FormRow> 
                                                       <x:FormRow ID="FormRow129" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label101" runat="server" Text="Rodilla Der." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlRodillaD1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlRodillaD2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaD3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaD4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaD5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaD6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaD7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaD8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow130" ColumnWidths ="90px   90px  90px  90px   90px  90px  90px   90px" runat="server">
                                                        <Items>
                                                            <x:Label ID="Label102" runat="server" Text="Rodilla Izq." ShowLabel="false"></x:Label> 
                                                            <x:DropDownList ID="ddlRodillaI1" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>     
                                                            <x:DropDownList ID="ddlRodillaI2" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaI3" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaI4" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaI5" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaI6" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaI7" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>
                                                            <x:DropDownList ID="ddlRodillaI8" runat="server" Width="80" ShowLabel="false" ></x:DropDownList>                                                                          
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="TEST ESPECÍFICOS" ID="GroupPanel20" BoxFlex="1" Height="280" TableColspan="1" >                
                                        <Items>
                                            <x:Form ID="Form33" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow131" ColumnWidths="450px 450px" runat="server">
                                                        <Items>
                                            
                                                            <x:GroupPanel runat="server" Title="TEST DE PHALEN" ID="GroupPanel21" BoxFlex="1" Height="50" TableColspan="1" >                
                                                                <Items>
                                                                    <x:Form ID="Form34" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                                        <Rows>
                                                                            <x:FormRow ID="FormRow132" ColumnWidths="80px 120px 80px 120px" runat="server">
                                                                                <Items>
                                                                                    <x:Label ID="Label103" runat="server" Text="Derecha" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlPhalenDerecha" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>   
                                                                                    <x:Label ID="Label104" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlPhalenIzquierda" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>    
                                                                                </Items>
                                                                            </x:FormRow>
                                                                        </Rows>
                                                                    </x:Form>
                                                                </Items>
                                                            </x:GroupPanel>
                                                            
                                                            <x:GroupPanel runat="server" Title="TEST DE TINEL" ID="GroupPanel22" BoxFlex="1" Height="50" TableColspan="1" >                
                                                                <Items>
                                                                     <x:Form ID="Form35" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                                        <Rows>
                                                                            <x:FormRow ID="FormRow133"  ColumnWidths="80px 120px 80px 120px" runat="server">
                                                                                <Items>
                                                                                    <x:Label ID="Label105" runat="server" Text="Derecha" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlTinelDerecha" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>   
                                                                                    <x:Label ID="Label106" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlTinelIzquierda" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>    
                                                                                </Items>
                                                                            </x:FormRow>
                                                                        </Rows>
                                                                    </x:Form>
                                                                </Items>
                                                            </x:GroupPanel>
                                                                                                                                
                                                        </Items> 
                                                    </x:FormRow>  
                                                    <x:FormRow ID="FormRow134" ColumnWidths="450px 450px" runat="server">
                                                        <Items>
                                            
                                                            <x:GroupPanel runat="server" Title="CODO" ID="GroupPanel23" BoxFlex="1" Height="50" TableColspan="1" >                
                                                                <Items>
                                                                    <x:Form ID="Form36" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                                        <Rows>                                                                           
                                                                            <x:FormRow ID="FormRow135" ColumnWidths="80px 120px 80px 100px" runat="server">
                                                                                <Items>
                                                                                    <x:Label ID="Label107" runat="server" Text="Derecha" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlCodoDerecho" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>  
                                                                                    <x:Label ID="Label109" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlCodoIzquierdo" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>    
                                                                                </Items>
                                                                            </x:FormRow>
                                                                           
                                                                        </Rows>
                                                                    </x:Form>
                                                                </Items>
                                                            </x:GroupPanel>
                                                            
                                                            <x:GroupPanel runat="server" Title="PIE" ID="GroupPanel24" BoxFlex="1" Height="50" TableColspan="1" >                
                                                                <Items>
                                                                     <x:Form ID="Form37" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                                        <Rows>
                                                                           
                                                                          <x:FormRow ID="FormRow136" ColumnWidths="80px 120px  80px 100px" runat="server">
                                                                                <Items>
                                                                                    <x:Label ID="Label108" runat="server" Text="Derecha" ShowLabel="false"></x:Label>    
                                                                                    <x:DropDownList ID="ddlPieDerecho" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>    
                                                                                     <x:Label ID="Label110" runat="server" Text="Izquierda" ShowLabel="false"></x:Label>   
                                                                                    <x:DropDownList ID="ddlPieIzquierdo" runat="server" Width="100" ShowLabel="false" ></x:DropDownList>      
                                                                                </Items>
                                                                            </x:FormRow>
                                                                         
                                                                        </Rows>
                                                                    </x:Form>
                                                                </Items>
                                                            </x:GroupPanel>
                                                                                                                                
                                                        </Items> 
                                                    </x:FormRow>  
                                                     <x:FormRow ID="FormRow137" ColumnWidths="450px 450px" runat="server">
                                                        <Items>
                                            
                                                            <x:GroupPanel runat="server" Title="AMPLIAR DESCRIPCIÓN DE ARTICULACIÓN,  COLUMNA VERTEBRAL" ID="GroupPanel25" BoxFlex="1" Height="90" TableColspan="1" >                
                                                                <Items>
                                                                    <x:Form ID="Form39" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                                        <Rows>                                                                           
                                                                            <x:FormRow ID="FormRow138" ColumnWidths="400px" runat="server">
                                                                                <Items>
                                                                                   <x:TextArea ID="txtAmpliar" runat="server" Text="" ShowLabel="false" Width="420"></x:TextArea>
                                                                                </Items>
                                                                            </x:FormRow>
                                                                           
                                                                        </Rows>
                                                                    </x:Form>
                                                                </Items>
                                                            </x:GroupPanel>
                                                            
                                                            <x:GroupPanel runat="server" Title="CONCLUSIÓN" ID="GroupPanel26" BoxFlex="1" Height="90" TableColspan="1" >                
                                                                <Items>
                                                                     <x:Form ID="Form40" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left" >
                                                                        <Rows>
                                                                           
                                                                          <x:FormRow ID="FormRow139" ColumnWidths="400px" runat="server">
                                                                                <Items>
                                                                                    <x:TextArea ID="txtConclusion" runat="server" Text="" ShowLabel="false" Width="420"></x:TextArea>
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

                                     <x:Form ID="Form38" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow141" ColumnWidths="135px" runat="server" >
                                                <Items>
                                                    <x:Button Id="btnOsteoMuscular" runat="server" Icon="SystemSave" Text="---------Grabar Osteomuscular-------------" OnClick="btnOsteoMuscular_Click" ></x:Button>
                                                </Items>
                                            </x:FormRow>     
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                        </Items>
                    </x:Tab>
              
                    <x:Tab ID="TabAudiometria" BodyPadding="5px" Title="Audiometría" runat="server" Hidden="false" >
                        <Toolbars>
                            <x:Toolbar ID="Toolbar6" runat="server">
                                <Items>
                                    <x:Button ID="Button2" Text="Grabar Audiometría" Icon="SystemSave" runat="server" OnClick="btnGrabarAudiometria_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                               <x:Panel ID="Panel10" Title="" EnableBackgroundColor="true" Height="1050px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="False" TableConfigColumns="3" Layout="Table" >
                                <Items>
                          
                                    <x:GroupPanel runat="server" Title="CONDICIÓN  A LA EVALUACIÓN" ID="GroupPanel5" BoxFlex="1" Height="60" TableColspan="3" >                
                                        <Items>
                                            <x:Form ID="Form17" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow52" ColumnWidths="160px 150px 625px " runat="server">
                                                        <Items>
                                                            <x:Label ID="lblcondicion" runat="server" Text="Condición a la evaluación" ShowLabel="false"></x:Label>
                                                            <x:DropDownList ID="ddlAU_Condicion" runat="server" Width="130" ShowLabel="false" ></x:DropDownList>
                                                            <x:TextBox ID="txtAU_Observaciones" runat="server" Label="Observaciones" Width="530px"></x:TextBox>
                                                        </Items> 
                                                    </x:FormRow>  
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="Antedecentes Patológicos" ID="GroupPanel3" BoxFlex="1" Height="310" Width="350px" >                
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

                                    <x:GroupPanel runat="server" Title="Exposición a Ruído" ID="GroupPanel4" BoxFlex="1" Height="310" Width="610px" TableColspan="2">                
                                        <Items>
                                            <x:Form ID="Form19" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="250px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow64" ColumnWidths="600px" runat="server">
                                                        <Items>    
                                                            <x:CheckBox ID="chkRuidoExtra" runat="server" Label="*Ruido extra laboral"></x:CheckBox>           
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
                                                            <x:Label ID="Label26" runat="server" Text="Años" ShowLabel="false"></x:Label>  
                                                            <x:Label ID="Label27" runat="server" Text="Frecuencia" ShowLabel="false"></x:Label>  
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

                                    <x:GroupPanel runat="server" Title="(**)Ruído Laboral" ID="GroupPanel6" BoxFlex="1" Height="100"  Width="350">   
                                        <Items>
                                            <x:Form ID="Form20" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow75" runat="server">
                                                        <Items>                                                           
                                                            <x:DropDownList ID="ddlTipoRuido" runat="server" Width="220" Label="Tipo de ruido" ></x:DropDownList>                                                               
                                                        </Items> 
                                                    </x:FormRow>  
                                                    <x:FormRow ID="FormRow76" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtIntensidad" runat="server" Label="Intesidad" Width="220px"></x:TextBox>                                                               
                                                        </Items> 
                                                    </x:FormRow>  
                                                </Rows>
                                            </x:Form>
                                        </Items>             
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="Horas por día" ID="GroupPanel7" BoxFlex="1" Height="100"  Width="320">
                                         <Items>
                                            <x:Form ID="Form21" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow77" runat="server">
                                                        <Items>                                                           
                                                            <x:DropDownList ID="ddlHorasPorDia" runat="server" Width="290" ShowLabel="false" ></x:DropDownList>                                                               
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

                                    <x:GroupPanel runat="server" Title="Protectores" ID="GroupPanel8" BoxFlex="1" Height="100"  Width="290">      
                                         <Items>
                                            <x:Form ID="Form22" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow79" runat="server">
                                                        <Items>                                                           
                                                            <x:DropDownList ID="ddlTapones" runat="server" Width="205" Label="Tapones" ></x:DropDownList>                                                               
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow80" runat="server">
                                                        <Items>                                                           
                                                            <x:DropDownList ID="ddlOrejeras" runat="server" Width="205" Label="Orejeras" ></x:DropDownList>                                                               
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow81" runat="server">
                                                        <Items>                                                           
                                                            <x:DropDownList ID="ddlAmbos" runat="server" Width="205" Label="Ambos" ></x:DropDownList>                                                               
                                                        </Items> 
                                                    </x:FormRow>     
                                                </Rows>
                                            </x:Form>
                                        </Items>                          
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="Oído Derecho" ID="GroupPanel9" BoxFlex="1" Height="120" TableColspan="3">  
                                        <Items>
                                            <x:Form ID="Form23" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow85" ColumnWidths="170px 100px 100px 100px 100px 100px 100px 100px 100px 100px 100px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="TextBox74" runat="server" Text="" Width="90" Label="Vía Aérea" ></x:Label>     
                                                            <x:Label ID="TextBox623" runat="server" Text="125 Hz" Width="70" ShowLabel="false" ></x:Label>  
                                                            <x:Label ID="TextBox624" runat="server" Text="250 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="TextBox625" runat="server" Text="500 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="TextBox626" runat="server" Text="1000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="TextBox627" runat="server" Text="2000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="TextBox628" runat="server" Text="3000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="TextBox629" runat="server" Text="4000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="TextBox270" runat="server" Text="6000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="TextBox721" runat="server" Text="8000 Hz" Width="70" ShowLabel="false" ></x:Label>                                                         
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow82" ColumnWidths="170px 100px 100px 100px 100px 100px 100px 100px 100px 100px 100px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="TextBox723" runat="server" Text="" Width="90" Label="Vía Área" ></x:TextBox>     
                                                            <x:TextBox ID="txtOD_VA_125" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="txtOD_VA_250" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VA_500" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VA_1000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VA_2000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VA_3000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VA_4000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VA_6000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VA_8000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>                                                         
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow83" ColumnWidths="170px 100px 100px 100px 100px 100px 100px 100px 100px 100px 100px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="TextBox54" runat="server" Text="" Width="90" Label="Vía Ósea" ></x:TextBox>     
                                                            <x:TextBox ID="txtOD_VO_125" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="txtOD_VO_250" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VO_500" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VO_1000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VO_2000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VO_3000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VO_4000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VO_6000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_VO_8000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>                                                         
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow84" ColumnWidths="170px 100px 100px 100px 100px 100px 100px 100px 100px 100px 100px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="TextBox64" runat="server" Text="" Width="90" Label="Enmascaramiento" ></x:TextBox>     
                                                            <x:TextBox ID="txtOD_EM_125" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="txtOD_EM_250" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_EM_500" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_EM_1000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_EM_2000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_EM_3000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_EM_4000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_EM_6000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOD_EM_8000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>                                                         
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="Oído Izquierdo" ID="GroupPanel10" BoxFlex="1" Height="120" TableColspan="3">   
                                        <Items>
                                            <x:Form ID="Form24" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow86" ColumnWidths="170px 100px 100px 100px 100px 100px 100px 100px 100px 100px 100px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label28" runat="server" Text="" Width="90" Label="Vía Aérea" ></x:Label>     
                                                            <x:Label ID="Label29" runat="server" Text="125 Hz" Width="70" ShowLabel="false" ></x:Label>  
                                                            <x:Label ID="Label30" runat="server" Text="250 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="Label31" runat="server" Text="500 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="Label32" runat="server" Text="1000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="Label33" runat="server" Text="2000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="Label34" runat="server" Text="3000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="Label35" runat="server" Text="4000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="Label36" runat="server" Text="6000 Hz" Width="70" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="Label37" runat="server" Text="8000 Hz" Width="70" ShowLabel="false" ></x:Label>                                                         
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow87" ColumnWidths="170px 100px 100px 100px 100px 100px 100px 100px 100px 100px 100px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="Label138" runat="server" Text="" Width="90" Label="Vía Aérea" ></x:TextBox>     
                                                            <x:TextBox ID="txtOI_VA_125" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="txtOI_VA_250" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VA_500" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VA_1000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VA_2000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VA_3000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VA_4000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VA_6000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VA_8000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>                                                         
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow88" ColumnWidths="170px 100px 100px 100px 100px 100px 100px 100px 100px 100px 100px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="TextBox94" runat="server" Text="" Width="90" Label="Vía Ósea" ></x:TextBox>     
                                                            <x:TextBox ID="txtOI_VO_125" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="txtOI_VO_250" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VO_500" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VO_1000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VO_2000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VO_3000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VO_4000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VO_6000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_VO_8000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>                                                         
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow89" ColumnWidths="170px 100px 100px 100px 100px 100px 100px 100px 100px 100px 100px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="TextBox104" runat="server" Text="" Width="90" Label="Enmascaramiento" ></x:TextBox>     
                                                            <x:TextBox ID="txtOI_EM_125" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="txtOI_EM_250" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_EM_500" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_EM_1000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_EM_2000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_EM_3000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_EM_4000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_EM_6000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtOI_EM_8000" runat="server" Text="" Width="70" ShowLabel="false" ></x:TextBox>                                                         
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title=" " ID="GroupPanel11" BoxFlex="1" Height="160" TableColspan="1">   
                                        <Items>
                                            <x:Form ID="Form25" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow90" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label38" runat="server" Text="" Width="50" Label="STS" ></x:Label>     
                                                            <x:Label ID="Label39" runat="server" Text="Año" Width="50" ShowLabel="false" ></x:Label>  
                                                            <x:Label ID="Label40" runat="server" Text="OD" Width="50" ShowLabel="false" ></x:Label>
                                                            <x:Label ID="Label41" runat="server" Text="OI" Width="50" ShowLabel="false" ></x:Label>                                                                                                      
                                                        </Items> 
                                                    </x:FormRow> 

                                                    <x:FormRow ID="FormRow91" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label42" runat="server" Text="" Width="50" Label="Base" ></x:Label>     
                                                            <x:TextBox ID="Label43" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="Label44" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="Label45" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>                                                                                                      
                                                        </Items> 
                                                    </x:FormRow> 

                                                      <x:FormRow ID="FormRow92" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label46" runat="server" Text="" Width="50" Label="Referential" ></x:Label>     
                                                            <x:TextBox ID="TextBox114" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="TextBox115" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="TextBox116" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>                                                                                                      
                                                        </Items> 
                                                    </x:FormRow> 

                                                      <x:FormRow ID="FormRow93" ColumnWidths="80px 80px 80px 80px " runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label47" runat="server" Text="" Width="50" Label="Actual" ></x:Label>     
                                                            <x:TextBox ID="txtActualAnio" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>  
                                                            <x:TextBox ID="txtActualOD" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>
                                                            <x:TextBox ID="txtActualOI" runat="server" Text="" Width="50" ShowLabel="false" ></x:TextBox>                                                                                                      
                                                        </Items> 
                                                    </x:FormRow> 


                                                    <x:FormRow ID="FormRow94" ColumnWidths="80px 240px " runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label48" runat="server" Text="" Width="50" Label="Menoscabo Auditivo" ></x:Label>     
                                                            <x:TextBox ID="txtMenoscaboAuditivo" runat="server" Text="" Width="210" ShowLabel="false" ></x:TextBox>                                                                                                    
                                                        </Items> 
                                                    </x:FormRow> 

                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="Datos del Audiometro" ID="GroupPanel12" BoxFlex="1" Height="160" TableColspan="2">   
                                        <Items>
                                            <x:Form ID="Form26" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow95" ColumnWidths="200px" runat="server">
                                                        <Items>                                                            
                                                            <x:TextBox ID="txtCalibracion" runat="server" Text="" Width="180" Label="Calibración" ></x:TextBox>       
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow96" ColumnWidths="200px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtMarca" runat="server" Text="" Width="180" Label="Marca" ></x:TextBox>       
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow97" ColumnWidths="200px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtModelo" runat="server" Text="" Width="180" Label="Modelo" ></x:TextBox>       
                                                        </Items> 
                                                    </x:FormRow> 
                                                    <x:FormRow ID="FormRow98" ColumnWidths="200px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextArea ID="txtNivelRuidoAmbiental" runat="server" Text="" Width="180" Label="Nivel de ruido Ambiental"  Height="50px"></x:TextArea>       
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>

                                    <x:Form ID="Form28" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left" >
                                        <Rows>
                                                <x:FormRow ID="FormRow100" ColumnWidths="135px" runat="server" >
                                                <Items>
                                                    <x:Button Id="btnGrabarAudiometria" runat="server" Icon="SystemSave" Text="---------Grabar Audiometría-------------" OnClick="btnGrabarAudiometria_Click" ></x:Button>
                                                </Items>
                                            </x:FormRow>     
                                        </Rows>
                                    </x:Form>

                                    </Items>
                                    </x:Panel>
                         
                        </Items>
                    </x:Tab>
            
                    <x:Tab ID="TabPsicologia" BodyPadding="5px" Title="Psicología" runat="server"  Hidden="false"  Visible="false">
                           <Toolbars>
                            <x:Toolbar ID="Toolbar7" runat="server">
                                <Items>
                                    <x:Button ID="Button3" Text="Grabar Psicología" Icon="SystemSave" runat="server" OnClick="btnGrabarPsico_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                            <x:Panel ID="Panel13" Title="EXAMEN MENTAL" EnableBackgroundColor="true" Height="260px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="2" Layout="Table">
                                <Items>
                                    <x:GroupPanel runat="server" Title="1. OBSERVACION DE CONDUCTAS" ID="GroupPanel27" BoxFlex="1" Height="230" Width="480" >                
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
                                    <x:GroupPanel runat="server" Title="2. PROCESOS COGNITIVOS" ID="GroupPanel28" BoxFlex="1" Height="230" Width="480" >                
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
                                                            <x:TextBox ID="txtPensamiento" runat="server" Text="" Width="202"  ShowLabel="false"></x:TextBox>    
                                                        </Items>
                                                    </x:FormRow>
                                                   <x:FormRow ID="pFormRow155" ColumnWidths="160px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label125" runat="server" Text="Percepción" ShowLabel="false"></x:Label>                                                   
                                                            <x:TextBox ID="txtPercepcion" runat="server" Text="" Width="201" ShowLabel="false"></x:TextBox>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="pFormRow156" ColumnWidths="161px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label126" runat="server" Text="Memoria" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlMemoria" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="pFormRow157" ColumnWidths="160px 200px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="label127" runat="server" Text="Inteligencia" ShowLabel="false"></x:Label>                                                   
                                                            <x:DropDownList ID="ddlInteligencia" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>  
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="pFormRow158" ColumnWidths="161px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="plabel128" runat="server" Text="Apetito" ShowLabel="false"></x:Label>                                                   
                                                            <x:TextBox ID="txtApetito" runat="server" Text="" Width="200" ShowLabel="false"></x:TextBox>    
                                                        </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="pFormRow159" ColumnWidths="160px 202px" runat="server" >
                                                        <Items>
                                                            <x:Label ID="plabel129" runat="server" Text="Sueño" ShowLabel="false"></x:Label>                                                   
                                                            <x:TextBox ID="txtSuenio" runat="server" Text="" Width="201" ShowLabel="false"></x:TextBox>    
                                                        </Items>
                                                    </x:FormRow>
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>
                                </Items>
                            </x:Panel>   
                            <x:Panel ID="pPanel14" Title="TEST PSICOLÓGICOS" EnableBackgroundColor="true" Height="530px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Layout="Table">
                                <Items>
                                    <x:GroupPanel runat="server" Title="Pjte ..................................Nombre...................................................Pjte...................................................Nombre..................................................." ID="GroupPanel29" BoxFlex="1" Height="500" Width="960" >                
                                        <Items>
                                            <x:Form ID="pForm44" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow160" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje1" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel130" runat="server" Text="Inventario Millon de Estilos de Personalidad - MIPS" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje2" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel131" runat="server" Text="Test de personalidad - (TPH / TPBLL)" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form45" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow161" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje3" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel132" runat="server" Text="Escala de Votivacones Psicosociales - MPS" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje4" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel133" runat="server" Text="Prueba básica 312" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form46" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow162" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje5" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel134" runat="server" Text="Luria - DNA Diagnóstico neuropsicológco de Adultos" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje6" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel135" runat="server" Text="Test de Barranquilla" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form47" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow163" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje7" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel136" runat="server" Text="Escala de apreciacón del Estrés • EAE" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje8" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel137" runat="server" Text="Test de Radomsky (Espacios confinados)" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                            <x:Form ID="Form60" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow176" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="plabel165" runat="server" Text="" ShowLabel="false"></x:Label>     
                                                            <x:Label ID="plabel163" runat="server" Text="" ShowLabel="false"></x:Label>                                                               
                                                            <x:Label ID="plabel166" runat="server" Text="" ShowLabel="false"></x:Label>     
                                                            <x:Label ID="plabel164" runat="server" Text="TEST DE ISTAS 21 (Riesgos Psicosociales)" ShowLabel="false" CssClass="red"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                            <x:Form ID="Form48" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow164" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje9" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel139" runat="server" Text="Inventario de Burnout de Mashlach" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje10" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel140" runat="server" Text="Exigencas Pscosociales" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form49" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow165" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje11" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel141" runat="server" Text="Clima Laboral" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje12" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel142" runat="server" Text="Trabajo Activo y Posibilidades de Desarrollo" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form50" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow166" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje13" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel143" runat="server" Text="Batería de Conductores" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje14" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel144" runat="server" Text="Seguridad" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form51" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow167" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje15" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel145" runat="server" Text="WAIS" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje16" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel146" runat="server" Text="Apoyo Social y Liderazgo" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form52" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow168" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje17" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel147" runat="server" Text="Test BENTON" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje18" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel148" runat="server" Text="Doble Presenca" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form53" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow169" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje19" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel149" runat="server" Text="Test Bender" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje20" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel150" runat="server" Text="Estima" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form54" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow170" ColumnWidths="50px 365px   50px 400px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="plabel167" runat="server" Text="" ShowLabel="false"></x:Label>    
                                                            <x:Label ID="plabel151" runat="server" Text="" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:Label ID="label168" runat="server" Text="" ShowLabel="false"></x:Label>   
                                                            <x:Label ID="plabel152" runat="server" Text="Test de Salamanca (proyección Transtorno de Personalidad)" ShowLabel="false" CssClass="red"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form55" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="pFormRow171" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje21" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel153" runat="server" Text="Inventario de la ansiedad ZUNG" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje22" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel154" runat="server" Text="Proyecciones" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form56" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow172" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje23" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel155" runat="server" Text="Inventario de la depresión ZUNG" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje24" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel156" runat="server" Text="Proyecciones" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                             <x:Form ID="Form57" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow173" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje25" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel157" runat="server" Text="Escala de Memoria de Wechsler" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje26" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel158" runat="server" Text="Proyecciones" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                            <x:Form ID="Form58" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow174" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje27" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="plabel159" runat="server" Text="Test de Yositake (Fatiga)" ShowLabel="false"></x:Label>   
                                                            
                                                           <x:Label ID="label169" runat="server" Text="" ShowLabel="false"></x:Label>   
                                                        <x:Label ID="plabel160" runat="server" Text="" ShowLabel="false"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                            <x:Form ID="Form59" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow175" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="label170" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                            <x:Label ID="label161" runat="server" Text="" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:Label ID="label171" runat="server" Text="" ShowLabel="false"></x:Label>   
                                                            <x:Label ID="label162" runat="server" Text="Test de Cohen (Acrofóbia)" ShowLabel="false" CssClass="red"></x:Label>     
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                            <x:Form ID="Form61" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow177" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje28" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="label172" runat="server" Text="Test de Epworth (Somnoencia)" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje29" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="label173" runat="server" Text="Ansiedad" ShowLabel="false"></x:Label>    
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                                   <x:Form ID="Form62" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                                <Rows>
                                                    <x:FormRow ID="FormRow178" ColumnWidths="50px 365px   50px 300px" runat="server">
                                                        <Items>                                                           
                                                            <x:TextBox ID="txtPtje30" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="label175" runat="server" Text="Test de Moca (Cognoscitivo)" ShowLabel="false"></x:Label>   
                                                            
                                                            <x:TextBox ID="txtPtje31" runat="server" Text="" Width="30"  ShowLabel="false"></x:TextBox>   
                                                            <x:Label ID="label174" runat="server" Text="Evitación" ShowLabel="false"></x:Label>      
                                                        </Items> 
                                                    </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                        </Items>
                                    </x:GroupPanel>   
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel15" Title="RESULTADOS DE EVALUACIÓN" EnableBackgroundColor="true" Height="170px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form63" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                        <Rows>
                                            <x:FormRow ID="FormRow179" ColumnWidths="155px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="xx" runat="server" Text="Nivel Intelectual" ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtNivelIntelectual" runat="server" Text="" Width="805"  ShowLabel="false"></x:TextBox>   
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow180" ColumnWidths="154px 805px" runat="server" >
                                                <Items>
                                                    <x:Label ID="pLabel176" runat="server" Text="Coordinación Visomotriz" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtCoodinacionViso" runat="server" Text="" Width="805"  ShowLabel="false"></x:TextBox>   
                                                </Items>
                                            </x:FormRow>
                                             <x:FormRow ID="FormRow181" ColumnWidths="155px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="pLabel177" runat="server" Text="Nivel de Memoria" ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtNivelMemoria" runat="server" Text="" Width="805"  ShowLabel="false"></x:TextBox>   
                                                </Items>
                                            </x:FormRow>
                                               <x:FormRow ID="FormRow182" ColumnWidths="154px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="pLabel178" runat="server" Text="Personalidad" ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtPersonalidad" runat="server" Text="" Width="805"  ShowLabel="false"></x:TextBox>   
                                                </Items>
                                            </x:FormRow>
                                             <x:FormRow ID="FormRow183" ColumnWidths="155px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="pLabel179" runat="server" Text="Afectividad" ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtAfectividad" runat="server" Text="" Width="805"  ShowLabel="false"></x:TextBox>   
                                                </Items>
                                            </x:FormRow>
                                             <x:FormRow ID="FormRow184" ColumnWidths="154px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="pLabel180" runat="server" Text="Otros" ShowLabel="false"></x:Label>
                                                <x:TextBox ID="txtOtros" runat="server" Text="" Width="805"  ShowLabel="false"></x:TextBox>   
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>                                 
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel16" Title="DIAGNÓSTICO FINAL / CONCLUSIONES" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" >
                                <Items>
                                     <x:Form ID="Form69" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                        <Rows>
                                            <x:FormRow ID="FormRow185" ColumnWidths="155px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="pLabel181" runat="server" Text="Área Cognitiva" ShowLabel="false"></x:Label>
                                                <x:TextArea ID="txtAreaCognitiva" runat="server" Text="" Width="805"  ShowLabel="false" Height="40"></x:TextArea>   
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                     <x:Form ID="Form64" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                        <Rows>
                                            <x:FormRow ID="FormRow186" ColumnWidths="155px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="pLabel182" runat="server" Text="Área Emocional" ShowLabel="false"></x:Label>
                                                <x:TextArea ID="txtAreaEmocional" runat="server" Text="" Width="805"  ShowLabel="false" Height="40"></x:TextArea>   
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                     <x:Form ID="Form65" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                        <Rows>
                                            <x:FormRow ID="FormRow187" ColumnWidths="155px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="pLabel183" runat="server" Text="Evaluación espacios confinados" ShowLabel="false"></x:Label>
                                                <x:TextArea ID="txtEvaEspaciosConfinados" runat="server" Text="" Width="805"  ShowLabel="false" Height="40"></x:TextArea>   
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                     <x:Form ID="Form66" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                        <Rows>
                                            <x:FormRow ID="FormRow188" ColumnWidths="155px 805px" runat="server" >
                                                <Items>
                                                  <x:Label ID="Label184" runat="server" Text="Evaluación  trabajos altura estructural" ShowLabel="false"></x:Label>
                                                <x:TextArea ID="txtEvaTrabajoAltura" runat="server" Text="" Width="805"  ShowLabel="false" Height="40"></x:TextArea>   
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                             <x:Panel ID="Panel17" Title="RECOMENDACIONES Y CONTROLES" EnableBackgroundColor="true" Height="170px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" >
                                <Items>
                                   
                                    <x:Form ID="Form68" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                        <Rows>
                                            <x:FormRow ID="FormRow190" ColumnWidths="660px 260px" runat="server" >
                                                <Items>
                                                    <x:TextArea ID="txtReco1" runat="server" Text="" ShowLabel="false" Height="40" EmptyText="Recomendación 1"></x:TextArea>
                                                    <x:TextArea ID="txtControl1" runat="server" Text="" ShowLabel="false" Height="40" EmptyText="Control 1"></x:TextArea>  
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                    <x:Form ID="Form67" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                        <Rows>
                                            <x:FormRow ID="FormRow189" ColumnWidths="660px 260px" runat="server" >
                                                <Items>
                                                    <x:TextArea ID="txtReco2" runat="server" Text="" ShowLabel="false" Height="40" EmptyText="Recomendación 2"></x:TextArea>
                                                    <x:TextArea ID="txtControl2" runat="server" Text="" ShowLabel="false" Height="40" EmptyText="Control 2"></x:TextArea>  
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                    <x:Form ID="Form70" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Right">
                                        <Rows>
                                            <x:FormRow ID="FormRow191" ColumnWidths="660px 260px" runat="server" >
                                                <Items>
                                                    <x:TextArea ID="txtReco3" runat="server" Text="" ShowLabel="false" Height="40" EmptyText="Recomendación 3"></x:TextArea>
                                                    <x:TextArea ID="txtControl3" runat="server" Text="" ShowLabel="false" Height="40" EmptyText="Control 3"></x:TextArea>  
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                    <x:Form ID="Form71" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="60px" LabelAlign="Left" >
                                        <Rows>
                                                <x:FormRow ID="FormRow192" ColumnWidths="135px" runat="server" >
                                                <Items>
                                                    <x:Button Id="btnGrabarPsico" runat="server" Icon="SystemSave" Text="---------Grabar Psicología-------------" OnClick="btnGrabarPsico_Click" ></x:Button>
                                                </Items>
                                            </x:FormRow>     
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>

                        </Items>
                    </x:Tab>
              
                    <x:Tab ID="TabDermatologico" BodyPadding="5px" Title="Tamizaje Dermatológico" runat="server"   Hidden="false"  Visible="false"     >
                         <Toolbars>
                            <x:Toolbar ID="Toolbar8" runat="server">
                                <Items>
                                    <x:Button ID="Button4" Text="Grabar Tamizaje Dermatológico" Icon="SystemSave" runat="server" OnClick="btnGrabarTamizaje_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                            <x:Panel ID="a1" Title="ANAMNESIS DERMATOLÓGICA EN RELACIÓN AL RIESGO" EnableBackgroundColor="true" Height="140px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="a2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="a3" ColumnWidths="240px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label111" runat="server" Text="Alergias dérmicas" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAlergiasDermicas" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="a4" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label165" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a5" ColumnWidths="239px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label114" runat="server" Text="Alergias medicamentosas" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAlergiasMedicamentosas" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="a6" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label166" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a7" ColumnWidths="241px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label117" runat="server" Text="Enfermedades propia de la piel y anexos" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEnfPropiaPiel" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="a8" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label167" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a9" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label112" runat="server" Text="Describir" ShowLabel="false"></x:Label>
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
                                    <x:Form ID="a11" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="a12" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="plabel113" runat="server" Text="Lupus eritematoso sistemico" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLupusEritematoso" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="plabel115" runat="server" Text="Enfermedad tiroidea" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEnfermedadTiroidea" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="plabel116" runat="server" Text="Artritis reumatoide" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlArtritisReumatoide" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="plabel118" runat="server" Text="Hepatopatias" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlHepatopatias" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a13" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="plabel123" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a14" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="plabel119" runat="server" Text="Psoriasis" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlPsoriasis" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="plabel120" runat="server" Text="Sindrome de ovario poliquístico" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSindromeOvario" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="plabel121" runat="server" Text="Diabetes Mellitus tipo 2" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlDiabetesMellitus" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="plabel122" runat="server" Text="Otras" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlOtrasEnfermedadesSistemicas" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a15" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label124" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                           <x:FormRow ID="a16" ColumnWidths="120px 720px 120px" runat="server" >
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
                                    <x:Form ID="a18" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="a19" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="plabel127" runat="server" Text="Mácula" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlMacula" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label128" runat="server" Text="Vesícula" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlVesicula" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label129" runat="server" Text="Nódulo" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlNodulo" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label130" runat="server" Text="Púrpura" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlPurpura" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a20" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label131" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a21" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label132" runat="server" Text="Pápula" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlPapula" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label133" runat="server" Text="Ampolla" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAmpolla" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label134" runat="server" Text="Placa" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlPlaca" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label135" runat="server" Text="Comedones" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlComedones" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a22" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label136" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a23" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label137" runat="server" Text="Tubérculo" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlTuberculo" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label139" runat="server" Text="Pústula" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlPustula" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label140" runat="server" Text="Quiste" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlQuiste" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label141" runat="server" Text="Telangiectasia" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlTelangiectasia" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a24" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label142" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a25" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label143" runat="server" Text="Escama" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEscama" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label144" runat="server" Text="Petequia" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlPetequia" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label145" runat="server" Text="Angioedema" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAngioedema" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label146" runat="server" Text="Tumor" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlTumor" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a26" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label147" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a27" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label148" runat="server" Text="Habón" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlHabon" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label149" runat="server" Text="Equímosis" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEquimosis" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label150" runat="server" Text="Discromías" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlDiscromias" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label154" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label155" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a28" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label152" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a29" ColumnWidths="120px 720px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label151" runat="server" Text="Otros" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOtrosLesionesPrimarias" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    <x:Label ID="label153" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                    </Items>
                                           </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="a30" Title="EVALUACION DERMATOLOGICA Lesiones secundarias:" EnableBackgroundColor="true" Height="160px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="a31" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="a32" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label156" runat="server" Text="Escamas" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEscamas" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label157" runat="server" Text="Escaras" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEscaras" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label158" runat="server" Text="Fisura" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlFisura" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label159" runat="server" Text="Excoriaciones" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlExcoriaciones" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a33" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label160" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a34" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="plabel161" runat="server" Text="Costras" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlCostras" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="plabel162" runat="server" Text="Cicatrices" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlCicatrices" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label163" runat="server" Text="Atrofia" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAtrofia" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label164" runat="server" Text="Liquenificación" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLiquenificacion" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                           </x:FormRow>
                                           <x:FormRow ID="a35" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="plabel175" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a36" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label176" runat="server" Text="Esclerosis" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEsclerosis" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label177" runat="server" Text="Ulceras" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlUlceras" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label178" runat="server" Text="Erosión" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlErosion" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label179" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label180" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a37" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label181" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="a38" ColumnWidths="120px 720px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label182" runat="server" Text="Otros" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOtrosLesionesSecundarias" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                    <x:Label ID="label183" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                    </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow3088" ColumnWidths="135px" runat="server" >
                                                <Items>
                                                    <x:Button Id="btnGrabarTamizaje" runat="server" Icon="SystemSave" Text="---------Grabar Tamizaje Dermatológico-------------" OnClick="btnGrabarTamizaje_Click" ></x:Button>
                                                </Items>
                                            </x:FormRow>                                         
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                        
                        </Items>
                    </x:Tab>
               
                    <x:Tab ID="TabEspirometria" BodyPadding="5px" Title="Espirometría" runat="server"  Hidden="false"  Visible="false">
                        <Toolbars>
                            <x:Toolbar ID="Toolbar9" runat="server">
                                <Items>
                                    <x:Button ID="Button5" Text="Grabar Espirometría" Icon="SystemSave" runat="server" OnClick="btnGrabarEspirometria_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                     <Items>
                            <x:Panel ID="Panel11" Title="PREGUNTAS PARA TODOS LOS CANDIDATOS A ESPIROMETRIA (RELACIONADAS A CRITERIOS DE EXCLUSION)" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form41" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow140" ColumnWidths="800px 160px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label185" runat="server" Text="_________________________________________________CUESTIONARIO_________________________________________________" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label194" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow142" ColumnWidths="800px 160px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label186" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow143" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label189" runat="server" Text="1. ¿TUVO DESPRENDIMIENTO DE LA RETINA O UNA OPERACIÓN (CIRUGÍA) DE LOS OJOS, 
      TÓRAX O ABDOMEN, EN LOS ÚLTIMOS 3 MESES?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario1Pregunta1" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow156" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label192" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow155" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label191" runat="server" Text="2. ¿HA ESTADO HOSPITALIZADO (A) POR CUALQUIER OTRO PROBLEMA DEL CORAZÓN EN    
      LOS ÚLTIMOS 3 MESES?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario1Pregunta2" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow157" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label193" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                           <x:FormRow ID="FormRow144" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label190" runat="server" Text="3. ¿HA TENIDO ALGÚN ATAQUE CARDÍACO O INFARTO AL CORAZÓN EN LOS ÚLTIMOS 3 
      MESES?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario1Pregunta3" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow158" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label195" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow159" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label196" runat="server" Text="4. ¿ESTÁ USANDO MEDICAMENTOS PARA LA TUBERCULOSIS, EN ESTE MOMENTO?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario1Pregunta4" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow160" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label197" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                           <x:FormRow ID="FormRow161" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label198" runat="server" Text="5. EN CASO DE SER MUJER: ¿ESTÁ USTED EMBARAZADA ACTUALMENTE?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario1Pregunta5" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow162" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label199" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                         <x:Panel ID="Panel12" Title="ANTECEDENTES MÉDICOS DE IMPORTANCIA" EnableBackgroundColor="true" Height="150px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form44" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow163" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label200" runat="server" Text="HEMOPTISIS" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesHemoptisis" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label201" runat="server" Text="INFARTO RECIENTE" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesInfarto" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label202" runat="server" Text="PNEUMOTORAX" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesPneumotorax" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label203" runat="server" Text="INESTABILIDAD CV" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesInestabilidad" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow164" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label205" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow165" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label204" runat="server" Text="TRAQUEOSTOMIA" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesTraqueostomia" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label206" runat="server" Text="FIEBRE, NAUSEA VOMITO" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesFiebre" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label207" runat="server" Text="SONDA PLEURAL" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesSonda" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label208" runat="server" Text="EMBARAZO AVANZADO" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesEmbarazo" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow166" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label209" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow168" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label210" runat="server" Text="ANEURISMAS CEREBRAL, ABDOMEN, TORAX" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesAneurisma" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label211" runat="server" Text="EMBARAZO COMPLICADO" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesEmbarazoComplicado" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label212" runat="server" Text="EMBOLIA PULMONAR" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAntecedentesEmbolia" runat="server"   Width="70px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label213" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label215" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow169" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label214" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                        </x:Panel>
                        <x:Panel ID="Panel14" Title="PREGUNTAS PARA TODOS LOS ENTREVISTADOS QUE NO TIENEN LOS CRITERIOS DE EXCLUSION Y QUE POR LO TANTO DEBEN HACER LA ESPIROMETRIA." EnableBackgroundColor="true" Height="260px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form72" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow170" ColumnWidths="800px 160px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label216" runat="server" Text="_________________________________________________CUESTIONARIO_________________________________________________" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label217" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow171" ColumnWidths="800px 160px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label218" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow193" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label219" runat="server" Text="1. TUVO UNA INFECCIÓN RESPIRATORIA (RESFRIADO), EN LAS ÚLTIMAS 3 SEMANAS?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario2Pregunta1" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow194" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label220" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow195" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label221" runat="server" Text="2.TUVO INFECCIÓN EN EL OÍDO EN LAS ÚLTIMAS 3 SEMANAS?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario2Pregunta2" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow196" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label222" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                           <x:FormRow ID="FormRow197" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label223" runat="server" Text="3.USO AEROSOLES (SPRAYS INHALADOS) O NEBULIZACIONES CON BRONCODILATADORES, 
   EN LAS ÚLTIMAS 3 HORAS?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario2Pregunta3" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow198" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label224" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow199" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label225" runat="server" Text="4. ¿HA USADO ALGÚN MEDICAMENTO BRONCODILATADOR TOMA EN LAS ÚLTIMAS 8 HORAS?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario2Pregunta4" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow200" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label226" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                           <x:FormRow ID="FormRow201" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label227" runat="server" Text="5.¿FUMO (CUALQUIER TIPO DE CIGARRO), EN LAS ÚLTIMAS DOS HORAS?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario2Pregunta5" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow202" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label228" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow203" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label229" runat="server" Text="6.¿REALIZÓ ALGÚN EJERCICIO FÍSICO FUERTE (COMO GIMNASIA, CAMINATA O TROTAR), EN 
    LA ÚLTIMA HORA?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario2Pregunta6" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow204" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label230" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow205" ColumnWidths="800px 160px" runat="server">
                                                <Items>
                                                     <x:Label ID="label231" runat="server" Text="7. ¿COMIÓ EN LA ÚLTIMA HORA?" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlEspiroCuestionario2Pregunta7" runat="server"   Width="100px" ShowLabel="false" ></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow206" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label232" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                         <x:Panel ID="Panel18" Title="DATOS ADICIONALES" EnableBackgroundColor="true" Height="105px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form73" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow207" ColumnWidths="240px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label233" runat="server" Text="Origen Étnico" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlOrigenEtnico" runat="server"   Width="250px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow208" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label234" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow209" ColumnWidths="239px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label235" runat="server" Text="Tabaquismo" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlTabaquismo" runat="server"   Width="250px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow210" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label236" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                           <x:FormRow ID="FormRow213" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label239" runat="server" Text="Tiempo de trabajo" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTiempotrabajo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="250"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                          <x:Panel ID="Panel19" Title="RESULTADOS" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form74" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow211" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label237" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label240" runat="server" Text="VALOR" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label238" runat="server" Text="DESCRIPCIÓN" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow214" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label242" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow212" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label241" runat="server" Text=" CVF" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroCVF" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroCVFDescrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow215" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label243" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow216" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label244" runat="server" Text=" VEF (1)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroVEF" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroVEFDescrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow217" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label245" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow218" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label246" runat="server" Text=" VEF1 / CVF" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroVEF1" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroVEF1Descrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow219" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label247" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow220" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label248" runat="server" Text=" FET" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroFET" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroFETDescrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow221" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label249" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow222" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label250" runat="server" Text=" FEF (25)(75)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroFEF" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroFEFDescrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow223" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label251" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow224" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label252" runat="server" Text=" PEF" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroPEF" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroPEFDescrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow225" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label253" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow226" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label254" runat="server" Text=" MEF" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroMEF" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroMEFDescrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow227" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label255" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow228" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label256" runat="server" Text=" R (50)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroR50" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroR50Descrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow229" ColumnWidths="239px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label257" runat="server" Text="" ShowLabel="false"></x:Label>
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow230" ColumnWidths="200px 200px 560px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label258" runat="server" Text=" MVV (IND)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEspiroMVV" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox> 
                                                     <x:TextBox ID="txtEspiroMVVdescrip" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="560"></x:TextBox> 
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow3098" ColumnWidths="135px" runat="server" >
                                                <Items>
                                                    <x:Button Id="btnGrabarEspirometria" runat="server" Icon="SystemSave" Text="---------Grabar Espirometría-------------" OnClick="btnGrabarEspirometria_Click" ></x:Button>
                                                </Items>
                                            </x:FormRow>        
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                          

                        </Items>
                   </x:Tab>

                   <x:Tab ID="TabOftalmologia" BodyPadding="5px" Title="Oftalmología" runat="server"    Hidden="false">
                           <Toolbars>
                            <x:Toolbar ID="Toolbar10" runat="server">
                                <Items>
                                    <x:Button ID="Button6" Text="Grabar Oftalmología" Icon="SystemSave" runat="server" OnClick="btnGrabarOftalmologia_Click"></x:Button> 
                                    <x:Button ID="btnReporteOftalmologia" Text="Reporte Oftalmología" Icon="SystemSave" runat="server" OnClick="btnReporteOftalmologia_Click" ></x:Button>                           
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                            <x:Panel ID="Panel20" Title="ANAMNESIS / ANTECEDENTES" EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form75" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow237" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label265" runat="server" Text="ANAMNESIS:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOftalmoAnamnesis" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow231" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label259" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow232" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label260" runat="server" Text="ANTECEDENTES:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOftalmoAntecedentes" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow233" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label261" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel21" Title="" EnableBackgroundColor="true" Height="225px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="False" TableConfigColumns="3" Layout="Table" >
                                <Items>
                                    <x:GroupPanel runat="server" Title="-------------OJO DERECHO--------------" ID="GroupPanel30" BoxFlex="1" Height="185"  Width="250">   
                                        <Items>
                                            <x:Form ID="Form76" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow238" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label268" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                            <x:Label ID="Label269" runat="server" Text="Normal" ShowLabel="false"></x:Label>                                                              
                                                        </Items> 
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow243" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label272" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>  
                                                        <x:FormRow ID="FormRow234" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label262" runat="server" Text="PARPADO" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkParpadoDerecho" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow245" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label274" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>    
                                                    <x:FormRow ID="FormRow235" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label263" runat="server" Text="CONJUNTIVA" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkConjuntivaDerecha" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow246" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label275" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                    <x:FormRow ID="FormRow247" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label276" runat="server" Text="CÓRNEA" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkCorneaDerecha" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow248" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label277" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                     <x:FormRow ID="FormRow249" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label278" runat="server" Text="IRIS" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkIrisDerecho" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow250" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label279" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                     <x:FormRow ID="FormRow251" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label280" runat="server" Text="MOV. OCULAR" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkMovOcularDerecho" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow252" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label281" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>        
                                                </Rows>
                                            </x:Form>
                                        </Items>             
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="------------EXAMEN CLÌNICO EXTERNO / SEGMENTO ANTERIOR-----------" ID="GroupPanel31" BoxFlex="1" Height="185"  Width="460">
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

                                    <x:GroupPanel runat="server" Title="-------------OJO IZQUIERDO------------" ID="GroupPanel32" BoxFlex="1" Height="185"  Width="250">      
                                         <Items>
                                            <x:Form ID="Form78" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow239" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label264" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                            <x:Label ID="Label266" runat="server" Text="Normal" ShowLabel="false"></x:Label>                                                              
                                                        </Items> 
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow240" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label267" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>  
                                                        <x:FormRow ID="FormRow241" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label270" runat="server" Text="PARPADO" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkParpadoIzquierdo" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow242" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label271" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>    
                                                    <x:FormRow ID="FormRow244" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label273" runat="server" Text="CONJUNTIVA" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkConjuntivaIzquierda" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow253" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label282" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                    <x:FormRow ID="FormRow254" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label283" runat="server" Text="CÓRNEA" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkCorneaIzquierda" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow255" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label284" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                     <x:FormRow ID="FormRow256" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label285" runat="server" Text="IRIS" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkIrisIzquierdo" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow257" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label286" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                     <x:FormRow ID="FormRow258" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label287" runat="server" Text="MOV. OCULAR" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkMovOcularIzquierdo" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow259" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label288" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                </Rows>
                                            </x:Form>
                                        </Items>
                          </x:GroupPanel>
                        </Items>
                    </x:Panel>         
                    <x:Panel ID="Panel22" Title="AGUDEZA VISUAL" EnableBackgroundColor="true" Height="125px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>
                                        <x:Form ID="Form79" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow260" ColumnWidths="240px 240px 240px 240px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label289" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label290" runat="server" Text="-------------SIN CORREGIR------------" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label291" runat="server" Text="---------------CORREGIDA--------------" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label292" runat="server" Text="------AGUJERO ESTENOPEICO------" ShowLabel="false"></x:Label>  
                                                </Items>
                                          </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                        <x:Form ID="Form80" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow261" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label293" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label297" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label294" runat="server" Text="DERECHO" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label295" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label296" runat="server" Text="DERECHO" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label298" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label299" runat="server" Text="DERECHO" ShowLabel="false"></x:Label> 
                                                    <x:Label ID="Label300" runat="server" Text="IZQUIERDO" ShowLabel="false"></x:Label> 
                                                </Items>
                                          </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                    <x:Form ID="Form81" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow262" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label301" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label302" runat="server" Text="VISIÓN DE CERCA" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtSinCorrectCercaOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                    <x:TextBox ID="txtSinCorrectCercaOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                    <x:TextBox ID="txtConCorrectCercaOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                    <x:TextBox ID="txtConCorrectCercaOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                    <x:TextBox ID="txtAECercaOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                    <x:TextBox ID="txtAECercaOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                        <x:Form ID="Form83" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow264" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label305" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                        <x:Form ID="Form82" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow263" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px" runat="server" >
                                                <Items>
                                                    <x:Label ID="Label303" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="Label304" runat="server" Text="VISIÓN DE LEJOS" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtSinCorrectLejosOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                    <x:TextBox ID="txtSinCorrectLejosOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                    <x:TextBox ID="txtConCorrectLejosOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                    <x:TextBox ID="txtConCorrectLejosOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox>
                                                    <x:TextBox ID="txtAELejosOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                    <x:TextBox ID="txtAELejosOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="60"></x:TextBox> 
                                                </Items>
                                          </x:FormRow>
                                         </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            <x:Panel ID="Panel23" Title="TEST DE COLORES:" EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form84" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow265" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label306" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTestColoresOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow266" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label307" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow267" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label308" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTestColoresOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow268" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
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
                                    <x:Form ID="Form85" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow269" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label310" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTonometriaOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow270" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label311" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow271" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label312" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTonometriaOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow272" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
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
                                    <x:Form ID="Form86" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow273" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label314" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEstereopsisOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow274" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label315" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow275" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label316" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEstereopsisOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow276" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label317" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow277" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label318" runat="server" Text="TIEMPO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtEstereopsisTiempo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow278" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
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
                                    <x:Form ID="Form87" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow279" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label320" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTestEncandilamientoOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow280" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label321" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow281" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label322" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTestEncandilamientoOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow282" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label323" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel27" Title="FONDO DE OJO (FO):" EnableBackgroundColor="true" Height="90px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form88" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow283" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label324" runat="server" Text="OJO DERECHO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtFondoOjoOD" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow284" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label325" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow285" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label326" runat="server" Text="OJO IZQUIERDO:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtFondoOjoOI" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow286" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label327" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow3108" ColumnWidths="135px" runat="server" >
                                                <Items>
                                                    <x:Button Id="btnGrabarOftalmologia" runat="server" Icon="SystemSave" Text="---------Grabar Oftalmología-------------" OnClick="btnGrabarOftalmologia_Click" ></x:Button>
                                                </Items>
                                            </x:FormRow>   
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                    
                         </Items>
                     </x:Tab>

                       <x:Tab ID="TabRx" BodyPadding="5px" Title="Radiografía de Tórax" runat="server"  Hidden="false">
                               <Toolbars>
                            <x:Toolbar ID="Toolbar11" runat="server">
                                <Items>
                                    <x:Button ID="Button7" Text="Grabar Rx" Icon="SystemSave" runat="server" OnClick="btnGrabarRx_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                             <x:Panel ID="Panel29" Title="DATOS DE LA PLACA: " EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
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
                            <x:Panel ID="Panel28" Title="LA RADIOGRAFÍA DE TÓRAX EN PROYECCIÓN FRONTAL EN INCIDENCIA POSTERO - ANTERIOR; MUESTRA: " EnableBackgroundColor="true" Height="360px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form89" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow287" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label187" runat="server" Text="VÉRTICES:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXVertices" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow288" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label188" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow289" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label328" runat="server" Text="CAMPOS PULMONARES :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXCamposPulmonares" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow290" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label329" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow291" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label330" runat="server" Text="HILIOS : " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXHilios" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow292" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label331" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow293" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label332" runat="server" Text="SENOS COSTODIAFRAGMÁTICOS :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXSenosDiafrag" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow294" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label333" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow295" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label334" runat="server" Text="SENOS CARDIOFRÉNICOS :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXSenosCardiofre" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow296" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label335" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow297" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label336" runat="server" Text="MEDIASTINOS :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXMediastinos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow298" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label337" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow299" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label338" runat="server" Text="SILUETA CARDIACA :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXSiluetaCard" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow300" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label339" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow301" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label340" runat="server" Text="ÍNDICE CARDIOTORÁXICO :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXInidice" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow302" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label341" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow303" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label342" runat="server" Text="PARTES BLANDAS Y ESTRUCTURAS ÓSEAS :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXPartesBlandas" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow304" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label343" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                         <x:FormRow ID="FormRow305" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label344" runat="server" Text="CONCLUSIONES :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtRXConclusiones" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow306" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label345" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow3118" ColumnWidths="135px" runat="server" >
                                                <Items>
                                                    <x:Button Id="btnGrabarRx" runat="server" Icon="SystemSave" Text="---------Grabar Radiografís de Tórax-------------" OnClick="btnGrabarRx_Click" ></x:Button>
                                                </Items>
                                            </x:FormRow>   
                                         </Rows>
                                    </x:Form>
                                </Items>
                              </x:Panel>
                 
                        </Items>
                    </x:Tab>

                     <x:Tab ID="Tab7D" BodyPadding="5px" Title="Examen 7D" runat="server"  Hidden="false"  Visible="false">
                             <Toolbars>
                            <x:Toolbar ID="Toolbar12" runat="server">
                                <Items>
                                    <x:Button ID="btnGrabar7D" Text="Grabar 7 D" Icon="SystemSave" runat="server" OnClick="btnGrabar7D_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                             <x:Panel ID="Panel30" Title="ACTIVIDAD: " EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form91" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow308" ColumnWidths="215px 240px 240px 240px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label349" runat="server" Text="Actividad a realizar:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txt7DActividad" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="240"></x:TextBox>
                                                    <x:Label ID="label350" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label351" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="Panel31" Title="FUNCIONES VITALES:" EnableBackgroundColor="true" Height="75px" runat="server"
                            BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                            <Items>
                                <x:Form ID="Form92" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow309" ColumnWidths="950px" runat="server">
                                            <Items> 
                                                <x:Form ID="Form93" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="215px" LabelAlign="Left" >
                                                    <Rows>
                                                        <x:FormRow ID="FormRow311" ColumnWidths="320px  320px  320px" runat="server" >
                                                            <Items>
                                                                <x:TextBox ID="TextBox44" runat="server" Text="" Width="50" Label="Frecuencia Cardiaca(Latx min)" OnTextChanged="txtTalla_TextChanged" AutoPostBack="true"></x:TextBox>    
                                                                <x:TextBox ID="TextBox45" runat="server" Text="" Width="50" Label="Presión Arterial Sistólica(mm Hg.)" OnTextChanged="txtPeso_TextChanged"  AutoPostBack="true"></x:TextBox> 
                                                                <x:TextBox ID="TextBox43" runat="server" Text="" Width="50" Label="Presión Arterial Diastólica(mm Hg.)" OnTextChanged="txtPeso_TextChanged"  AutoPostBack="true"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow312" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                            <Items>
                                                                <x:Label ID="label352" runat="server" Text="" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow310" ColumnWidths="320px  320px  320px" runat="server" >
                                                            <Items>
                                                                <x:TextBox ID="TextBox52" runat="server" Text="" Width="50" Label="Frecuencia Respiratoria(Resp x min)" Enabled="false"></x:TextBox> 
                                                                <x:TextBox ID="TextBox53" runat="server" Text="" Width="50" Label="Indice de Masa Corporal" Enabled="false"></x:TextBox> 
                                                                <x:TextBox ID="TextBox55" runat="server" Text="" Width="50" Label="Saturación de O2(%)" ></x:TextBox> 
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
                             <x:Panel ID="Panel32" Title="EL / LA PRESENTA O HA PRESENTADO EN LOS ÚLTIMOS 6 MESES:" EnableBackgroundColor="true" Height="300px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="3" Layout="Table" >
                                <Items>
                                    <x:GroupPanel runat="server" Title="." ID="GroupPanel33" BoxFlex="1" Height="275"  Width="480">   
                                        <Items>
                                            <x:Form ID="Form94" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="400px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow313" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label353" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                            <x:Label ID="Label354" runat="server" Text="SI" ShowLabel="false"></x:Label>                                                              
                                                        </Items> 
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow314" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label355" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>  
                                                        <x:FormRow ID="FormRow315" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label356" runat="server" Text="ANEMIA" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DAnemia" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow316" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label357" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>    
                                                    <x:FormRow ID="FormRow317" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label358" runat="server" Text="CIRUGÍA MAYOR RECIENTE" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chkCirugiaMayor" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow318" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label359" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                    <x:FormRow ID="FormRow319" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label360" runat="server" Text="DESÓRDENES DE LA COAGULACIÓN, TROMBOSIS, ETC." ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DCoagulacion" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow320" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label361" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                     <x:FormRow ID="FormRow321" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label362" runat="server" Text="DIABETES MELLITUS" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DDiabetes" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow322" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label363" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                     <x:FormRow ID="FormRow323" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label364" runat="server" Text="HIPERTENSIÓN ARTERIAL" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DHipertension" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow324" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label365" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>
                                                    <x:FormRow ID="FormRow325" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label379" runat="server" Text="EMBARAZO" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DEmbarazo" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow338" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label380" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>
                                                    <x:FormRow ID="FormRow339" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label381" runat="server" Text="PROBLEMAS NEUROLÓGICOS: EPILEPSIA, VÉRTIGO, ETC." ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DNeurologico" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow340" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label382" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>
                                                    <x:FormRow ID="FormRow341" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label383" runat="server" Text="INFECCIONES RECIENTES (ESPECIALMENTE OÍDOS, NARIZ, GARGANTA)" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DInfecciones" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow342" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label384" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>        
                                                </Rows>
                                            </x:Form>
                                        </Items>             
                                    </x:GroupPanel>

                                   <x:GroupPanel runat="server" Title="." ID="GroupPanel35" BoxFlex="1" Height="275"  Width="480">      
                                         <Items>
                                            <x:Form ID="Form96" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="300px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow326" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label366" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                            <x:Label ID="Label367" runat="server" Text="SI" ShowLabel="false"></x:Label>                                                              
                                                        </Items> 
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow327" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label368" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>  
                                                        <x:FormRow ID="FormRow328" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label369" runat="server" Text="PROBLEMAS CARDIACOS: MARCAPASOS, CORONARIOPATÍA, ETC." ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DCardiacos" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow329" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label370" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>    
                                                    <x:FormRow ID="FormRow330" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label371" runat="server" Text="OBESIDAD MÓRBIDA (IMC MAYOR a 35 Kg/m2)" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DObesidad" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow331" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label372" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                    <x:FormRow ID="FormRow332" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label373" runat="server" Text="PROBLEMAS RESPIRATORIOS: ASMA, EPOC, ETC." ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DRespiratorios" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow333" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label374" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                     <x:FormRow ID="FormRow334" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label375" runat="server" Text="PROBLEMAS OFTALMOLÓGICOS: RETINOPATÍA, GLAUCOMA, ETC." ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DOftalmo" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow335" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label376" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow> 
                                                     <x:FormRow ID="FormRow336" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label377" runat="server" Text="PROBLEMAS DIGESTIVOS: ÚLCERA PÉPTICA, HEPATITIS, ETC." ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DDigestivos" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow337" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label378" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>
                                                    <x:FormRow ID="FormRow343" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label385" runat="server" Text="APNEA DEL SUEÑO" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DApnea" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow344" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label386" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>
                                                    <x:FormRow ID="FormRow345" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label387" runat="server" Text="ALERGIAS" ShowLabel="false"></x:Label> 
                                                            <x:CheckBox ID="chk7DAlergias" runat="server" Text="" ShowLabel="false"></x:CheckBox>                                                              
                                                        </Items> 
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow346" ColumnWidths="390px  50px" runat="server">
                                                        <Items>                                                           
                                                            <x:Label ID="Label388" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                         </Items>
                                                        </x:FormRow>   
                                                </Rows>
                                            </x:Form>
                                        </Items>
                          </x:GroupPanel>
                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel33" Title="." EnableBackgroundColor="true" Height="80px" runat="server"
                        BodyPadding="5px" ShowBorder="true" ShowHeader="false">
                        <Items>
                             <x:Form ID="Form99" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                <Rows>
                                    <x:FormRow ID="FormRow350" ColumnWidths="320px 640px" runat="server" >
                                        <Items>
                                           
                                            <x:Label ID="label391" runat="server" Text="" ShowLabel="false"></x:Label>
                                           
                                            </Items>
                                    </x:FormRow>
                                 </Rows>
                            </x:Form>
                            <x:Form ID="Form95" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                <Rows>
                                    <x:FormRow ID="FormRow347" ColumnWidths="320px 640px" runat="server" >
                                        <Items>
                                            <x:Label ID="label389" runat="server" Text="OTRA CONDICIÓN MÉDICA IMPORTANTE:" ShowLabel="false"></x:Label>
                                            <x:TextBox ID="txt7DOtraCondMedica" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="590"></x:TextBox>
                                           
                                            </Items>
                                    </x:FormRow>
                                 </Rows>
                            </x:Form>
                            <x:Form ID="Form98" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                <Rows>
                                    <x:FormRow ID="FormRow349" ColumnWidths="320px 640px" runat="server" >
                                        <Items>
                                           
                                            <x:Label ID="label390" runat="server" Text="" ShowLabel="false"></x:Label>
                                           
                                            </Items>
                                    </x:FormRow>
                                 </Rows>
                            </x:Form>
                            <x:Form ID="Form97" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                <Rows>
                                    <x:FormRow ID="FormRow348" ColumnWidths="320px 640px" runat="server" >
                                        <Items>
                                           
                                            <x:Label ID="label392" runat="server" Text="USO DE MEDICACIÓN ACTUAL:" ShowLabel="false"></x:Label>
                                            <x:TextBox ID="txt7DMedicaActual" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="590"></x:TextBox>
                                            </Items>
                                    </x:FormRow>
                                 </Rows>
                            </x:Form>
                        </Items>
                    </x:Panel>
                             <x:Panel ID="Panel34" Title="CONCLUSIÓN" EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form100" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow351" ColumnWidths="320px 240px 290px 110px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label393" runat="server" Text="POR LO QUE CERTIFICO QUE EL / LA SE ENCUENTRA" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddl7DConclusion" runat="server"   Width="200px" ShowLabel="false"></x:DropDownList>
                                                    <x:Label ID="label394" runat="server" Text="PARA   ASCENDER   A   GRANDES  ALTITUDES." ShowLabel="false"></x:Label>
                                                    <x:Label ID="label395" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow353" ColumnWidths="320px 640px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label397" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow352" ColumnWidths="320px 640px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label396" runat="server" Text="OBSERVACIONES" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txt7DObeservaciones" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="640"></x:TextBox>
                                                </Items>
                                            </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                 </Items>
             </x:Tab>

            <x:Tab ID="TabOIT" BodyPadding="5px" Title="Radiografía OIT" runat="server"  Hidden="false">
                    <Toolbars>
                            <x:Toolbar ID="Toolbar13" runat="server">
                                <Items>
                                    <x:Button ID="btnGrabarOIT" Text="Grabar OIT" Icon="SystemSave" runat="server" OnClick="btnGrabarOIT_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                             <x:Panel ID="Panel35" Title="DATOS DE LA PLACA: " EnableBackgroundColor="true" Height="100px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form101" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow354" ColumnWidths="160px 160px 160px 160px 160px 160px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label398" runat="server" Text="Código de Placa:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOITCodigoPlaca" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label399" runat="server" Text="Fecha de Lectura:" ShowLabel="false"></x:Label>
                                                     <x:DatePicker ID="dptOITFechaLectura" Label="" ShowLabel="false" Width="100px" runat="server"  DateFormatString="dd/MM/yyyy" />
                                                     <x:Label ID="label400" runat="server" Text="Fecha de Radiografía:" ShowLabel="false"></x:Label>
                                                     <x:DatePicker ID="dptOITFechaToma" Label="" ShowLabel="false" Width="100px" runat="server"  DateFormatString="dd/MM/yyyy" /> 
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
                                                     <x:Label ID="label402" runat="server" Text="Causas de Mala Calidad:" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlOITCausas" runat="server"   Width="300px" ShowLabel="false"></x:DropDownList>
                                               </Items>
                                            </x:FormRow>
                                             <x:FormRow ID="FormRow439" ColumnWidths="160px 160px 160px 160px 160px 160px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label605" runat="server" Text="" ShowLabel="false"></x:Label>
                                                 </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow437" ColumnWidths="160px 320px 160px 320px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label604" runat="server" Text="Comentarios: " ShowLabel="false" ></x:Label>
                                                     <x:TextBox ID="txtOITComentarios" runat="server" Label="" Width="300px" ShowLabel="false"></x:TextBox>
                                                     <x:Label ID="label606" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label607" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                         <x:Panel ID="Panel36" Title="II.  ANORMALIDADES PARENQUIMATOSAS" EnableBackgroundColor="true" Height="160px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="4" Layout="Table" >
                                <Items>
                                   
                                    <x:GroupPanel runat="server" Title="2.1. Zonas  Afectadas " ID="GroupPanel38" BoxFlex="1" Height="125"  Width="280">   
                                        <Items>
                                            <x:Form ID="Form105" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                 <x:FormRow ID="FormRow361" ColumnWidths="110px 73px 73px" runat="server" >
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
                                                    <x:CheckBox ID="chkOIT0_" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label414" runat="server" Text="0/0" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT0_0" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label415" runat="server" Text="0/1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT0_1" runat="server" Label="" ShowLabel="false"></x:CheckBox>  
                                                </Items>
                                                  </x:FormRow>
                                                  <x:FormRow ID="FormRow369" ColumnWidths="85px 85px 85px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label416" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                  <x:FormRow ID="FormRow370" ColumnWidths="30px 55px 30px 55px 30px 55px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label417" runat="server" Text="1/0" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT1_0" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label419" runat="server" Text="1/1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT1_1" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label421" runat="server" Text="1/2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT1_2" runat="server" Label="" ShowLabel="false"></x:CheckBox> 
                                                </Items>
                                            </x:FormRow>
                                                    <x:FormRow ID="FormRow371" ColumnWidths="85px 85px 85px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label418" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                    <x:FormRow ID="FormRow372" ColumnWidths="30px 55px 30px 55px 30px 55px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label422" runat="server" Text="2/1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT2_1" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label423" runat="server" Text="2/2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT2_2" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label424" runat="server" Text="2/3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chkOIT2_3" runat="server" Label="" ShowLabel="false"></x:CheckBox>  
                                                </Items>
                                            </x:FormRow> 
                                                    <x:FormRow ID="FormRow373" ColumnWidths="85px 85px 85px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label420" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                </Items>
                                            </x:FormRow>
                                                     <x:FormRow ID="FormRow374" ColumnWidths="30px 55px 30px 55px 30px 55px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label425" runat="server" Text="3/2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chOIT3_2" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label426" runat="server" Text="3/3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chOIT3_3" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label427" runat="server" Text="3/+" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="chOIT3_" runat="server" Label="" ShowLabel="false"></x:CheckBox>  
                                                </Items>
                                            </x:FormRow>   
                                                </Rows>
                                            </x:Form>
                                        </Items>                
                                    </x:GroupPanel>

                                    <x:GroupPanel runat="server" Title="2.3.  Forma y Tamaño : " ID="GroupPanel40" BoxFlex="1" Height="125"  Width="280">      
                                         <Items>
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
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form106" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow381" ColumnWidths="320px 160px 160px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label444" runat="server" Text="(si NO hay anormalidades pase a símbolos *)" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="chkAnormalidadesSI" runat="server" Text="SI" ShowLabel="true"></x:CheckBox>
                                                     <x:CheckBox ID="chkAnormalidadesNO" runat="server" Text="NO" ShowLabel="true"></x:CheckBox>  
                                                    </Items>
                                            </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                             <x:Panel ID="Panel38" Title="3.1. Placas Pleurales (0=Ninguna, D=Hemitórax derecho; I= Hemitórax izquierdo) " EnableBackgroundColor="true" Height="140px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="4" Layout="Table" >
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
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" TableConfigColumns="4" Layout="Table" >
                                <Items>
                                   
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel43" BoxFlex="1" Height="195"  Width="280">   
                                        <Items>
                                          <x:Form ID="Form111" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                <x:FormRow ID="FormRow399" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label463" runat="server" Text="De Perfil" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label464" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox38" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label465" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox39" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label466" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox40" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow398" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label461" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow400" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label462" runat="server" Text="De frente" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label467" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox37" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label468" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox41" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label469" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox42" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow401" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label470" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow402" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label471" runat="server" Text="Diafragma" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label472" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox43" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label473" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox44" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label474" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox45" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow403" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label475" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow404" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label476" runat="server" Text="Otro(s) Sitio(s)" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label477" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox46" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label478" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox47" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label479" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox48" runat="server" Label="" ShowLabel="false"></x:CheckBox>
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
                                                <x:FormRow ID="FormRow406" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                   <x:Label ID="label482" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox49" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label483" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox50" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label484" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox51" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow407" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label485" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow408" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label487" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox52" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label488" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox53" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label489" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox54" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow409" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label490" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow410" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label492" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox55" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label493" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox56" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label494" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox57" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow411" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label495" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow412" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label497" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox58" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label498" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox59" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label499" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox60" runat="server" Label="" ShowLabel="false"></x:CheckBox>
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
                                            <x:FormRow ID="FormRow414" ColumnWidths="43px 43px 43px 43px 43px 43px" runat="server" >
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
                                                <x:FormRow ID="FormRow416" ColumnWidths="10px 30px 10px 30px 10px 30px 18px 10px 30px 10px 30px 10px 30px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label504" runat="server" Text="1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox61" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label505" runat="server" Text="2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox62" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label506" runat="server" Text="3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox63" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                      <x:Label ID="label511" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label507" runat="server" Text="1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox64" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label508" runat="server" Text="2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox65" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label509" runat="server" Text="3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox66" runat="server" Text="" ShowLabel="false"></x:CheckBox>
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
                                                  <x:FormRow ID="FormRow422" ColumnWidths="90px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label527" runat="server" Text="Obliteración del Angulo Costofrenico" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label528" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox73" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label529" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox74" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label530" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox75" runat="server" Label="" ShowLabel="false"></x:CheckBox>
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
                                                <x:FormRow ID="FormRow420" ColumnWidths="10px 30px 10px 30px 10px 30px 18px 10px 30px 10px 30px 10px 30px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label519" runat="server" Text="a" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox67" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label520" runat="server" Text="b" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox68" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label521" runat="server" Text="c" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox69" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                      <x:Label ID="label522" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label523" runat="server" Text="a" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox70" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label524" runat="server" Text="b" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox71" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label525" runat="server" Text="c" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox72" runat="server" Text="" ShowLabel="false"></x:CheckBox>
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
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="4" Layout="Table" >
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
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" TableConfigColumns="4" Layout="Table" >
                                <Items>
                                   
                                    <x:GroupPanel runat="server" Title="-" ID="GroupPanel51" BoxFlex="1" Height="100"  Width="240">   
                                        <Items>
                                          <x:Form ID="Form119" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                                <Rows>
                                                <x:FormRow ID="FormRow446" ColumnWidths="60px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label554" runat="server" Text="De Perfil" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label555" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox76" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label556" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox77" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label557" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox78" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow447" ColumnWidths="60px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label558" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow448" ColumnWidths="60px 10px 40px 10px 40px 10px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label559" runat="server" Text="De frente" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label560" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox79" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label561" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox80" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label562" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox81" runat="server" Label="" ShowLabel="false"></x:CheckBox>
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
                                                <x:FormRow ID="FormRow454" ColumnWidths="20px 40px 20px 40px 20px 40px" runat="server" >
                                                <Items>
                                                   <x:Label ID="label574" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox88" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label575" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox89" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label576" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox90" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                </Items>
                                                  </x:FormRow>
                                                     <x:FormRow ID="FormRow455" ColumnWidths="10px 20px 10px 20px 10px 20px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label577" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                </Items>
                                                  </x:FormRow>
                                                    <x:FormRow ID="FormRow456" ColumnWidths="20px 40px 20px 40px 20px 40px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label578" runat="server" Text="O" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox91" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label579" runat="server" Text="D" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox92" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label580" runat="server" Text="I" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox93" runat="server" Label="" ShowLabel="false"></x:CheckBox>
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
                                                <x:FormRow ID="FormRow464" ColumnWidths="10px 23px 10px 23px 10px 23px 18px 10px 23px 10px 23px 10px 23px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label597" runat="server" Text="1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox100" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label598" runat="server" Text="2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox101" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label599" runat="server" Text="3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox102" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                      <x:Label ID="label600" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label601" runat="server" Text="1" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox103" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label602" runat="server" Text="2" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox104" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label603" runat="server" Text="3" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox105" runat="server" Text="" ShowLabel="false"></x:CheckBox>
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
                                                <x:FormRow ID="FormRow476" ColumnWidths="10px 23px 10px 23px 10px 23px 18px 10px 23px 10px 23px 10px 23px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label623" runat="server" Text="a" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox109" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label624" runat="server" Text="b" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox110" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label625" runat="server" Text="c" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox111" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                      <x:Label ID="label626" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label627" runat="server" Text="a" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox112" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label628" runat="server" Text="b" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox113" runat="server" Text="" ShowLabel="false"></x:CheckBox>
                                                    <x:Label ID="label629" runat="server" Text="c" ShowLabel="false"></x:Label>
                                                    <x:CheckBox ID="CheckBox114" runat="server" Text="" ShowLabel="false"></x:CheckBox>
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
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form123" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow430" ColumnWidths="320px 160px 160px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label538" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:CheckBox ID="CheckBox82" runat="server" Text="SI" ShowLabel="true"></x:CheckBox>
                                                     <x:CheckBox ID="CheckBox83" runat="server" Text="NO" ShowLabel="true"></x:CheckBox>  
                                                    </Items>
                                            </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                            <x:Panel ID="Panel43" Title="(Marque la respuesta adecuada; si marcaa od, escriba a continuación un COMENTARIO) " EnableBackgroundColor="true" Height="105px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="2" Layout="Table" >
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
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form127" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow435" ColumnWidths="120px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label588" runat="server" Text="Comentarios od" ShowLabel="false"></x:Label>
                                                      <x:TextBox ID="txtOITComentariosod" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>
                                                      
                                                    </Items>
                                            </x:FormRow>
                                        </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                      
                        </Items>
                </x:Tab>

                    <x:Tab ID="TabAltura18" BodyPadding="5px" Title="Examen Altura 1.8" runat="server"  Hidden="false"  Visible="false">
                            <Toolbars>
                            <x:Toolbar ID="Toolbar14" runat="server">
                                <Items>
                                    <x:Button ID="btnAltura18" Text="Grabar Altura 1.8" Icon="SystemSave" runat="server" OnClick="btnAltura18_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                             <x:Panel ID="Panel45" Title="1. ACTIVIDAD: " EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form128" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow440" ColumnWidths="240px 720px 240px 240px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label608" runat="server" Text="Actividad a realizar:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8Activiad" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="240"></x:TextBox>
                                                    <x:Label ID="label609" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label610" runat="server" Text="" ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="Panel46" Title="2.  ANTECEDENTES PERSONALES Y FAMILIARES: " EnableBackgroundColor="true" Height="305px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form129" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow473" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label646" runat="server" Text="a.  Personales: " ShowLabel="false"></x:Label>
                                                       
                                                    </Items>
                                            </x:FormRow>
                                           <x:FormRow ID="FormRow441" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label611" runat="server" Text="Trabajos previos sobre nivel, uso de arnés?" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8Trabajos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow443" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label612" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow444" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label613" runat="server" Text="Cardiovasculares y respiratorios:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8Cardiovasc" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow445" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label614" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow449" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label615" runat="server" Text="Quirúrgicos: " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8Quirurg" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow450" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label631" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow451" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label632" runat="server" Text="Fobias (acrofobia, agarofobia) :" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8Fobias" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow452" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label633" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow453" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label634" runat="server" Text="Antecedentes de uso o abuso de alcohol y drogas:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8AntecedAlcohol" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow457" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label635" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow458" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label636" runat="server" Text="Fármacos de consumo actual:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8FarmacoActual" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow459" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label637" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow460" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label638" runat="server" Text="b.  Familiares: " ShowLabel="false"></x:Label>
                                                      
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow465" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label639" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow466" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label640" runat="server" Text="Psiquiátricos:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8AntecedPsiquiat" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow467" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label641" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                         <x:Panel ID="Panel47" Title="3.  EXAMEN MÉDICO DIRIGIDO:" EnableBackgroundColor="true" Height="755px" runat="server"
                            BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                            <Items>
                                <x:Form ID="Form130" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow468" ColumnWidths="950px" runat="server">
                                            <Items> 
                                                <x:Form ID="Form131" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="215px" LabelAlign="Left" >
                                                    <Rows>
                                                        <x:FormRow ID="FormRow469" ColumnWidths="320px  320px  320px" runat="server" >
                                                            <Items>
                                                                <x:TextBox ID="TextBox38" runat="server" Text="" Width="50" Label="Frecuencia Cardiaca(Latx min)" OnTextChanged="txtTalla_TextChanged" AutoPostBack="true"></x:TextBox>    
                                                                <x:TextBox ID="TextBox40" runat="server" Text="" Width="50" Label="Presión Arterial Sistólica(mm Hg.)" OnTextChanged="txtPeso_TextChanged"  AutoPostBack="true"></x:TextBox> 
                                                                <x:TextBox ID="TextBox41" runat="server" Text="" Width="50" Label="Presión Arterial Diastólica(mm Hg.)" OnTextChanged="txtPeso_TextChanged"  AutoPostBack="true"></x:TextBox>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow470" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                            <Items>
                                                                <x:Label ID="label642" runat="server" Text="" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow471" ColumnWidths="320px  320px  320px" runat="server" >
                                                            <Items>
                                                                <x:TextBox ID="TextBox42" runat="server" Text="" Width="50" Label="Frecuencia Respiratoria(Resp x min)" Enabled="false"></x:TextBox> 
                                                                <x:TextBox ID="TextBox46" runat="server" Text="" Width="50" Label="Peso (Kg)" Enabled="false"></x:TextBox> 
                                                                <x:TextBox ID="TextBox47" runat="server" Text="" Width="50" Label="Talla (m)" ></x:TextBox> 
                                                            </Items>
                                                        </x:FormRow>
                                                     <x:FormRow ID="FormRow486" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                            <Items>
                                                                <x:Label ID="label653" runat="server" Text="" ShowLabel="false"></x:Label>
                                                            </Items>
                                                        </x:FormRow>
                                            <x:FormRow ID="FormRow472" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label643" runat="server" Text="Aparato Cardiovascular:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8ExamenCardiovasc" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow478" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label644" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow479" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label645" runat="server" Text="Aparato Respiratorio::" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8ExamenRespiratorio" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow480" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label647" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow481" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label648" runat="server" Text="Sistema Nervioso:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8ExamenNervioso" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>

                                            </x:FormRow>
                                                        <x:FormRow ID="FormRow483" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label650" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow482" ColumnWidths="240px 720px 240px 240px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label649" runat="server" Text="Nistagmus Espontáneo:" ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlAltura1_8Nistagmus" runat="server" Width="90" ShowLabel="false"></x:DropDownList>  
                                                    <x:Label ID="label654" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label655" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                                        <x:FormRow ID="FormRow484" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label651" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow485" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label652" runat="server" Text="Manifestaciones o estigmas sugestivos de alcoholismo:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8Manifestaciones" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>

                                            </x:FormRow>
                                                          <x:FormRow ID="FormRow487" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label656" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow488" ColumnWidths="240px 720px 240px 240px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label657" runat="server" Text="a.  ¿Recibió entrenamiento en Primeros Auxilios?  " ShowLabel="false"></x:Label>
                                                    <x:DropDownList ID="ddlAltura1_8PrimerosAux" runat="server" Width="90" ShowLabel="false"></x:DropDownList>  
                                                    <x:Label ID="label658" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label659" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                                          <x:FormRow ID="FormRow489" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label660" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow490" ColumnWidths="240px 720px 240px 240px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label661" runat="server" Text="b.  Cuantificar los Siguientes Ítems: " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label664" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label662" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label663" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                                 <x:FormRow ID="FormRow491" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label665" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow492" ColumnWidths="480px 240px 120px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label666" runat="server" Text=" DESCRIPCION DE ITEMS " ShowLabel="false"></x:Label>
                                                     <x:Label ID="label668" runat="server" Text="SI/NO" ShowLabel="false"></x:Label>
                                                    <x:Label ID="label667" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    <x:Label ID="label669" runat="server" Text="" ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow493" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label670" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                        <x:FormRow ID="FormRow494" ColumnWidths="480px 240px 120px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label671" runat="server" Text=" 1.  TIMPANOS NORMALES  " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAltura1_8Timpanos" runat="server" Width="90" ShowLabel="false"></x:DropDownList> 
                                                    <x:Label ID="label672" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label673" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow495" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label675" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                     <x:FormRow ID="FormRow496" ColumnWidths="480px 240px 120px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label674" runat="server" Text=" 2.  EQUILIBRIO NORMAL  " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAltura1_8Equilibrio" runat="server" Width="90" ShowLabel="false"></x:DropDownList> 
                                                    <x:Label ID="label676" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label677" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow497" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label678" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow498" ColumnWidths="480px 240px 120px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label679" runat="server" Text=" 3.  SUSTENTACIÓN EN PIE POR 20’’  " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAltura1_8Sustentacion" runat="server" Width="90" ShowLabel="false"></x:DropDownList> 
                                                    <x:Label ID="label680" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label681" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow499" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label682" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow500" ColumnWidths="480px 240px 120px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label683" runat="server" Text=" 4.  CAMINAR LIBRE SOBRE RECTA (SIN DESVÍO)  " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAltura1_8Caminar" runat="server" Width="90" ShowLabel="false"></x:DropDownList> 
                                                    <x:Label ID="label684" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label685" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow501" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label686" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow502" ColumnWidths="480px 240px 120px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label687" runat="server" Text="5.  ADIADOCOCINESIA DIRECTA " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAltura1_8Adiadococinesia" runat="server" Width="90" ShowLabel="false"></x:DropDownList> 
                                                    <x:Label ID="label688" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label689" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow503" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label690" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow504" ColumnWidths="480px 240px 120px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label691" runat="server" Text=" 6.  INDICE-NARIZ/TALON-RODILLA  " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAltura1_8IndiceNariz" runat="server" Width="90" ShowLabel="false"></x:DropDownList> 
                                                    <x:Label ID="label692" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label693" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow505" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label694" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                         <x:FormRow ID="FormRow506" ColumnWidths="480px 240px 120px 120px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label695" runat="server" Text="7.  RECIBIO CURSO DE SEGURIDAD PARA TRABAJO EN ALTURA MAYOR 1.8m  " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlAltura1_8RecibioCurso" runat="server" Width="90" ShowLabel="false"></x:DropDownList> 
                                                    <x:Label ID="label696" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    <x:Label ID="label697" runat="server" Text=" " ShowLabel="false"></x:Label>
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow507" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label698" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                                        <x:FormRow ID="FormRow508" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label699" runat="server" Text=" Resultado de: " ShowLabel="false"></x:Label>
                                                       
                                                    </Items>
                                            </x:FormRow>
                                           <x:FormRow ID="FormRow509" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label700" runat="server" Text="1.  Electrocardiograma:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="TextBox56" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow510" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label701" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow511" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label702" runat="server" Text="2.  Colesterol total: " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="TextBox57" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow512" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label703" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow513" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label704" runat="server" Text="3.  Triglicéridos:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="TextBox58" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow514" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label705" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow515" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label706" runat="server" Text="Aptitud:   " ShowLabel="false"></x:Label>
                                                      <x:DropDownList ID="ddlAltura1_8Aptitud" runat="server" Width="250" ShowLabel="false" ></x:DropDownList>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow516" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label707" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow517" ColumnWidths="240px 720px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label708" runat="server" Text="OBSERVACIONES:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAltura1_8Observaciones" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="720"></x:TextBox>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow518" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
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
                            </Items>
                        </x:Tab>

                     <x:Tab ID="TabSintomaticoRespiratorio" BodyPadding="5px" Title="Sintomático Respiratorio" runat="server" Hidden="false"   Visible="false">
                             <Toolbars>
                            <x:Toolbar ID="Toolbar15" runat="server">
                                <Items>
                                    <x:Button ID="btnGrabarSintomaticoResp" Text="Grabar Sintomático Respitatorio" Icon="SystemSave" runat="server" OnClick="btnGrabarSintomaticoResp_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                            <x:Panel ID="Panel48" Title="USTED HA SIDO DIAGNOSTICADO ALGUNA VEZ DE:" EnableBackgroundColor="true" Height="72px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form132" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow519" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label710" runat="server" Text="1.  Tuberculosis   " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoTuberculosis" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow520" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label711" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow521" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label712" runat="server" Text="2.  De haberla tenido, ¿recibió usted tratamiento?  " ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoRecibioTto" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow522" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label713" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel49" Title="EN LA ACTUALIDAD USTED PRESENTA:" EnableBackgroundColor="true" Height="235px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form133" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow523" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label714" runat="server" Text="3.  Tos con expectoración por más de 15 días:" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoTos" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow524" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label715" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow525" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label716" runat="server" Text="4.  Expectoración con sangre:" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoExpecto" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow526" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label717" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow527" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label718" runat="server" Text="5.  Baja de peso inexplicable:" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoBajaPeso" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow528" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label719" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow529" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label720" runat="server" Text="6.  Sudoración nocturna importante:" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoSudoracion" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow530" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label721" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow531" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label722" runat="server" Text="7.  Familiares o amigos con Tuberculosis:" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoFamiliares" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow532" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label723" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow533" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label724" runat="server" Text="8.  Sospecha de Tuberculosis:" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoSospecha" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow534" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label725" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow535" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label726" runat="server" Text="Observaciones" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtSintomaticoObservacion" runat="server" Label="" Width="480px" ShowLabel="false"></x:TextBox>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow536" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label727" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>

                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                             <x:Panel ID="Panel50" Title="CONCLUSIÓN:" EnableBackgroundColor="true" Height="135px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                <Items>
                                    <x:Form ID="Form134" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                            <x:FormRow ID="FormRow537" ColumnWidths="290px 120px 290px 260px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label728" runat="server" Text="Por lo que certifico que EL/LA paciente" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoCertifica" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label732" runat="server" Text="es considerado SINTOMÁTICO RESPIRATORIO." ShowLabel="false"></x:Label>
                                                    <x:Label ID="label733" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow538" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label729" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow539" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label730" runat="server" Text="Requiere estudios ampliatorios:" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlSintomaticoRequiere" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow540" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label731" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow541" ColumnWidths="245px 290px 290px 40px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label734" runat="server" Text="Resultados de BK de esputo:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtSintomaticoBK1" runat="server"   Width="240px" Label="1ª"></x:TextBox>
                                                     <x:TextBox ID="txtSintomaticoBK2" runat="server"   Width="240px" Label="2ª"></x:TextBox>
                                                    <x:Label ID="label736" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow542" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label737" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                            <x:FormRow ID="FormRow543" ColumnWidths="290px 500px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label735" runat="server" Text="Resultado de Radiografía de Tórax:" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtSintomaticoResultRX" runat="server" Label="" Width="480px" ShowLabel="false"></x:TextBox>  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow544" ColumnWidths="120px 120px 120px 120px 120px 120px 120px 120px " runat="server" >
                                                <Items>
                                                     <x:Label ID="label738" runat="server" Text="" ShowLabel="false"></x:Label>
                                               </Items>
                                           </x:FormRow>
                                        </Rows>
                                    </x:Form>
                                </Items>
                            </x:Panel>
                            </Items>
                         </x:Tab>

                    <x:Tab ID="TabLaboratorio" BodyPadding="5px" Title="Laboratorio" runat="server" >
                            <Toolbars>
                            <x:Toolbar ID="Toolbar16" runat="server">
                                <Items>
                                    <x:Button ID="btnGrabarLaboratio" Text="Grabar Laboratorio" Icon="SystemSave" runat="server" OnClick="btnGrabarLaboratio_Click"></x:Button>                            
                                </Items>
                        </x:Toolbar>
                        </Toolbars>
                        <Items>
                            <x:Panel ID="PanelExamencompletodeorina" Title="EXAMEN COMPLETO DE ORINA" EnableBackgroundColor="true" Height="420" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table"  Visible ="false">
                                <Items>
                                    <x:GroupPanel runat="server" Title=" EXAMEN MACROSCÓPICO" ID="GroupPanel61" BoxFlex="1" Height="70" Width="960"  Hidden="false">                
                                        <Items>
                                             <x:Form ID="Form261" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" Hidden="false" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow700" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" Hidden="false" >
                                                        <Items>
                                                            <x:Label ID="label1310" runat="server" Text="Color" ShowLabel="false" Hidden="false"></x:Label>
                                                            <x:TextBox ID="txtOrinaColor" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label1311" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1312" runat="server" Text="Aspecto" ShowLabel="false"></x:Label>
                                                            <x:TextBox ID="txtOrinaAspecto" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                            <x:Label ID="label1313" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1314" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow702" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1316" runat="server" Text="Densidad" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtOrinaDensidad" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1317" runat="server" Text="g/L" ShowLabel="false" CssClass="red"></x:Label>
                                                             <x:Label ID="label1318" runat="server" Text="Ph" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtOrinaPh" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1319" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1320" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                        </Items>
                                                    </x:FormRow>
                                                </Rows>
                                            </x:Form>                                 
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="EXAMEN MICROSCÓPICO" ID="GroupPanel62" BoxFlex="1" Height="145" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form270" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow709" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1339" runat="server" Text="Células Epiteliales" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaCelulas" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1340" runat="server" Text="xcampo" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1341" runat="server" Text="Leucocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaLeucocitos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1342" runat="server" Text="xcampo" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1343" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                             <x:FormRow ID="FormRow711" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1345" runat="server" Text="Hematíes" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaHematies" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1346" runat="server" Text="xcampo" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1347" runat="server" Text="Cristales" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaCristales" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1348" runat="server" Text="xcampo" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1349" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow713" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1351" runat="server" Text="Gérmenes" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaGermenes" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1352" runat="server" Text="xcampo" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1353" runat="server" Text="Cilindros" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaCilindros" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1354" runat="server" Text="xcampo" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1355" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                              <x:FormRow ID="FormRow705" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1323" runat="server" Text="Filamento Mucoide" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaFilamento" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1324" runat="server" Text="xcampo" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1325" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label1328" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label1326" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1327" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               
                                    </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="EXAMEN QUÍMICO" ID="GroupPanel63" BoxFlex="1" Height="125" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form275" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow714" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1356" runat="server" Text="Sangre" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaSangre" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1357" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1358" runat="server" Text="Urobilinogeno" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaUrobilinogeno" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1359" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1360" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                             <x:FormRow ID="FormRow716" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1362" runat="server" Text="Bilirrubina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaBilirrubina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1363" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1364" runat="server" Text="Proteínas" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaProteina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1365" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1366" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                              <x:FormRow ID="FormRow718" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1368" runat="server" Text="Nitritos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaNitritos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1369" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1370" runat="server" Text="C. Cetonicos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaCetonico" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1371" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1372" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                              <x:FormRow ID="FormRow720" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1374" runat="server" Text="Glucosa" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaGlucosa" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1375" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1376" runat="server" Text="Hemoglobina " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtOrinaHemoglobina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1377" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1378" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                
                                    
                                        </Items>
                                    </x:GroupPanel>
                                    
                                    </Items>
                                </x:Panel>
                            <x:Panel ID="PanelHemograma" Title="HEMOGRAMA AUTOMATIZADO" EnableBackgroundColor="true" Height="820px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="false">
                                <Items>
                                    <x:GroupPanel runat="server" Title="HEMOGRAMA" ID="GroupPanel57" BoxFlex="1" Height="150" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form205" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow650" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1128" runat="server" Text="Hemoglobina" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoAutoHemoglobina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1129" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1130" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoAutoHemoglobinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1131" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1132" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow652" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1134" runat="server" Text="Hematocrito" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoAutoHematcrito" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1135" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1136" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoAutoHematcritoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1137" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1138" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow644" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="labelHema" runat="server" Text="Hematíes" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoAutoHematies" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="labelxx" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="labelxsx" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoAutoHematiesDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="labelxxx" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1114" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow646" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1116" runat="server" Text="Volumen Corpuscular Medio" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVolCorpsMedio" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1117" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1118" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVolCorpsMediosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1119" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1120" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow553" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label810" runat="server" Text="Hb Corpuscular Medio" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHbCorpsMedio" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label811" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label812" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHbCorpsMedioDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label813" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label814" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow554" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label815" runat="server" Text="CC Hb Corpuscular" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCCHbCorps" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label816" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label817" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtCCHbCorpsDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label818" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label819" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow648" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label1122" runat="server" Text="Plaquetas" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemogramaPlaquetas" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1123" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1124" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemogramaPlaquetasDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label1125" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label1126" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow555" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label820" runat="server" Text="Volumen Plaquetario Medio" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVolumenPlaquetarioMedio" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label821" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label822" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtVolumenPlaquetarioMedioDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label823" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label829" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    
                                                </Rows>
                                            </x:Form>
                 
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="FÓRMULA LEUCOCITARIA TOTAL Y DIFERENCIAL" ID="GroupPanel58" BoxFlex="1" Height="100" Width="960" >                
                                    <Items>
                                    <x:Form ID="Form214" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow653" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1139" runat="server" Text="Leucocitos Totales" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoLeucocTotales" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1140" runat="server" Text="um3" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1141" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoLeucocTotalesDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1142" runat="server" Text="um3" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1143" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                              
                                    <x:Form ID="Form216" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow655" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1145" runat="server" Text="Linfocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoLinfocitos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1146" runat="server" Text="pg" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1147" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoLinfocitosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1148" runat="server" Text="pg" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1149" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                 
                                    <x:Form ID="Form218" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow657" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1151" runat="server" Text="MID (BAS, EOS, MON)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoMIDBasEosMon" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1152" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1153" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoMIDBasEosMonDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1154" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1155" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form142" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow561" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label830" runat="server" Text="NEUTRÓFILOS SEGMENTADOS" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoNeutroSegm" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label831" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label832" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoNeutroSegmDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label833" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label844" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form144" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow562" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label845" runat="server" Text="LINFOCITOS (10 * 9)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoLinfocitos109" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label846" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label847" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoLinfocitos109Deseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label848" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label849" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form145" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow563" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label850" runat="server" Text="MID (BAS, EOS, MON) (10*9)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoMIDBasEos109" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label851" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label852" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoMIDBasEos109Deseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label853" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label854" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form146" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow564" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label855" runat="server" Text="NEUTRÓFILOS (10 * 9)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoNeutro109" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label856" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label857" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoAutoNeutro109Deseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label858" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label859" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                    </x:GroupPanel>
                                </Items>
                                </x:Panel>
                            <x:Panel ID="Panel51" Title="HEMOGRAMA COMPLETO" EnableBackgroundColor="true" Height="820px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="false">
                                <Items>
                                    <x:GroupPanel runat="server" Title="HEMOGRAMA" ID="GroupPanel59" BoxFlex="1" Height="150" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form148" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow565" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label860" runat="server" Text="Hemoglobina" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompHemoglobina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label861" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label862" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompHemoglobinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label863" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label864" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow567" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label865" runat="server" Text="Hematocrito" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompHematocrito" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label866" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label867" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompHematocritoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label868" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label869" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow570" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label870" runat="server" Text="Hematíes" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompHematies" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label871" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label872" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompHematiesDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label873" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label874" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow571" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label875" runat="server" Text="Leucocitos" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompLeucocitos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label876" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label877" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompLeucocitosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label878" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label879" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow572" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label880" runat="server" Text="Recuento de Plaquetas" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompRecuentoPlaq" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label881" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label882" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHemoCompRecuentoPlaqDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label883" runat="server" Text="mm3" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label884" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                   </Rows>
                                            </x:Form>
                 
                                        </Items>
                                    </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title="FÓRMULA LEUCOCITARIA" ID="GroupPanel60" BoxFlex="1" Height="100" Width="960" >                
                                    <Items>
                                    <x:Form ID="Form151" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow576" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label905" runat="server" Text="Abastonados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompAbastonados" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label906" runat="server" Text="um3" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label907" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompAbastonadosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label908" runat="server" Text="um3" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label909" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                              
                                    <x:Form ID="Form152" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow577" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label910" runat="server" Text="Segmentados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompSegmentados" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label911" runat="server" Text="pg" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label912" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompSegmentadosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label913" runat="server" Text="pg" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label914" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                 
                                    <x:Form ID="Form153" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow578" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label915" runat="server" Text="Eosinòfilos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompEosinofilos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label916" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label917" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompEosinofilosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label918" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label924" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form154" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow579" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label925" runat="server" Text="Basófilos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompBasofilos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label926" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label927" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompBasofilosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label928" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label929" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form155" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow581" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label930" runat="server" Text="Monocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompMonocitos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label931" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label932" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompMonocitosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label933" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label934" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form156" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow582" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label935" runat="server" Text="Linfocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompLinfocitos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label936" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label937" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompLinfocitosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label938" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label939" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                        <x:Form ID="Form157" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow583" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label940" runat="server" Text="Conclusiones" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHemoCompConclusiones" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label941" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label942" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label885" runat="server" Text="" ShowLabel="false"></x:Label>
                                                     <x:Label ID="label943" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label944" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                    </x:GroupPanel>
                                </Items>
                                </x:Panel>
                            <x:Panel ID="Panel52" Title="HISOPADO FARÍNGEO" EnableBackgroundColor="true" Height="820px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="false">
                                <Items>
                                    <x:GroupPanel runat="server" Title=" MACROSCÓPICO" ID="GroupPanel64" BoxFlex="1" Height="150" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form158" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow573" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label886" runat="server" Text="Color" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHisopadoColor" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label887" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label888" runat="server" Text="Aspecto" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHisopadoAspecto" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label889" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label890" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                </Rows>
                                               </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    <x:GroupPanel runat="server" Title=" MICROSCÓPICO" ID="GroupPanel65" BoxFlex="1" Height="150" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form159" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow574" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label891" runat="server" Text="Leucocitos" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHisopadoLeucocitos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label892" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label893" runat="server" Text="Hematíes" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHisopadoHematies" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label899" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label900" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                     <x:FormRow ID="FormRow575" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label901" runat="server" Text="Células Epiteliales" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHisopadoCelEpit" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label902" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label903" runat="server" Text="Bacterias GRAM" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtHisopadoBactGram" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label904" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label945" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                </Rows>
                                               </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="Panel53" Title="HISOPADO NASO-FARÍNGEO" EnableBackgroundColor="true" Height="820px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" TableConfigColumns="1" Layout="Table" Visible ="false">
                                <Items>
                                    <x:GroupPanel runat="server" Title=" HALLAZGOS" ID="GroupPanel66" BoxFlex="1" Height="150" Width="960" >                
                                        <Items>
                                             <x:Form ID="Form160" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                                <Rows>
                                                    <x:FormRow ID="FormRow584" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label946" runat="server" Text="Tipo de Muestra" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtNasoFarTipoMuestra" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label947" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label948" runat="server" Text="Levadura" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtNasoFarLevadura" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label949" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label950" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow586" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                        <Items>
                                                             <x:Label ID="label951" runat="server" Text="Antibiograma" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtNasoFarAntibiograma" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label952" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label953" runat="server" Text="Frotis GRAM" ShowLabel="false"></x:Label>
                                                             <x:TextBox ID="txtNasoFarFrotisGram" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                             <x:Label ID="label954" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                            <x:Label ID="label955" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                            </Items>
                                                    </x:FormRow>
                                                </Rows>
                                               </x:Form>
                                            </Items>
                                        </x:GroupPanel>
                                    </Items>
                                </x:Panel>
                            <%--      <x:Panel ID="PanelReticulocitos" Title="RETICULOCITOS " EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form249" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow688" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1250" runat="server" Text="Reticulocitos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabReticulocitos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1251" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1252" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabReticulocitosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1253" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1254" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelVsg" Title="VSG" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form250" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow689" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1255" runat="server" Text="VSG" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabVSG" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1256" runat="server" Text="mm/h" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1257" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabVSGDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1258" runat="server" Text="mm/h" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1259" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelTiempodecoagulacion" Title="TIEMPO DE COAGULACIÓN" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form251" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow690" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1260" runat="server" Text="Tiempo de coagulación" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTIempoCoagulacion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1261" runat="server" Text=" minutos" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1262" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTIempoCoagulacionDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1263" runat="server" Text=" minutos" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1264" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelTiempodesangria" Title="TIEMPO DE SANGRÍA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form252" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow691" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1265" runat="server" Text="Tiempo de Sangría" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiempoSangria" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1266" runat="server" Text="minutos" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1267" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiempoSangriaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1268" runat="server" Text="minutos" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1269" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelTiempoparcialdetromboplastina" Title="TIEMPO PARCIAL DE TROMBOPLASTINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form253" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow692" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1270" runat="server" Text="Tiempo Parcial de Tromboplastina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiempoTromboplast" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1271" runat="server" Text="segundos" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1272" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiempoTromboplastDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1273" runat="server" Text="segundos" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1274" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelTiempodeprotombina" Title="TIEMPO DE PROTOMBINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form254" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow693" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1275" runat="server" Text="Tiempo de Protombina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiempoProtombina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1276" runat="server" Text="segundos" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1277" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiempoProtombinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1278" runat="server" Text="segundos" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1279" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelToxicologicocarboxihemoglobina" Title="CARBOXIHEMOGLOBINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form255" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow694" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1280" runat="server" Text="Carboxihemoglobina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCarboxihemoglobina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1281" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1282" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCarboxihemoglobinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1283" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1284" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelVdrl" Title="VDRL" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form256" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow695" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1285" runat="server" Text="VDRL" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabVDRL" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1286" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1287" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtVDRDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1288" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1289" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelHepatitisb" Title="HEPATITIS B" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form257" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow696" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1290" runat="server" Text="Hepatitis B" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabHepatitisB" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1291" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1292" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHepatitisBDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1293" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1294" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelHavigmhepatitisa" Title="HAV (IGM)" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form258" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow697" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1295" runat="server" Text="HAV (IgM)" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabHAVIgM" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1296" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1297" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHAVIgMDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1298" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1299" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelExamendeelisahiv" Title="HIV" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form259" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow698" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1300" runat="server" Text="HIV" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabHIV" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1301" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1302" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHIVDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1303" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1304" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%-- <x:Panel ID="PanelAntihepatitisc" Title="ANTI HEPATITIS C" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form260" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow699" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1305" runat="server" Text="Anti Hepatitis C" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabAntiHepC" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1306" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1307" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAntiHepCDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1308" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1309" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelAcidourico" Title="ÁCIDO ÚRICO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form135" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow545" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label739" runat="server" Text="Acido Urico" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAcidoUrico" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label740" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label741" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAcidoUricoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label742" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label743" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelAglutinacionesenlamina" Title="AGLUTINACIONES EN LÁMINA" EnableBackgroundColor="true" Height="165px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form136" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow546" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label744" runat="server" Text="TIFICO 'O'" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabTificoO" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label745" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label746" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                      <x:TextBox ID="txtLabTificoODeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label747" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label748" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                           
                                
                                            <x:FormRow ID="FormRow547" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label749" runat="server" Text="TIFICO 'H'" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabTificoH" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label750" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label751" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                      <x:TextBox ID="txtLabTificoHDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label752" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label753" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                      
                                            <x:FormRow ID="FormRow548" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label754" runat="server" Text="PARATIFICO 'A'" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabParatificoA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label755" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label756" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtLabParatificoADeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label757" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label758" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            
                                        
                                            <x:FormRow ID="FormRow549" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label759" runat="server" Text="PARATIFICO 'B'" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabParatificoB" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label760" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label761" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabParatificoBDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label762" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label763" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            
                                      
                                             <x:FormRow ID="FormRow550" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label764" runat="server" Text="BRUCELLA" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabBrucella" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label765" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label766" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBrucellaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label767" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label768" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                           
                                       
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelAglutinacionesentubo" Title="AGLUTINACIONES EN TUBO" EnableBackgroundColor="true" Height="165px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form173" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow592" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label954" runat="server" Text="TIFICO 'O'" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlLabTuboTifico" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label955" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label956" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                      <x:TextBox ID="txtLabTuboTificoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label957" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label958" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                           
                                        
                                            <x:FormRow ID="FormRow594" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label960" runat="server" Text="TIFICO 'H'" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlTuboTificoH" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label961" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label962" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                      <x:TextBox ID="txtTuboTificoHDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label963" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label964" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            
                                        
                                            <x:FormRow ID="FormRow596" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label966" runat="server" Text="PARATIFICO 'A'" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlTuboParatificoA" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label967" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label968" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtTuboParatificoADeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label969" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label970" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            
                                     
                                            <x:FormRow ID="FormRow598" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label972" runat="server" Text="PARATIFICO 'B'" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlTuboParatificoB" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label973" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label974" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTuboParatificoBDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label975" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label976" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            
                                          
                                             <x:FormRow ID="FormRow600" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label978" runat="server" Text="BRUCELLA" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlTuboBrucella" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label979" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label980" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtTuboBrucellaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label981" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label982" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>                                           
                                         
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelAntigenoprostatico" Title="ANTÍGENO PROSTÁTICO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form137" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow556" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label774" runat="server" Text="Antígeno Prostático" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAntigenoProst" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label775" runat="server" Text="ng/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label776" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtAntigenoProstDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label777" runat="server" Text="ng/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label778" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelColesteroltotal" Title="COLESTEROL TOTAL" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form138" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow557" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label779" runat="server" Text="Colesterol Total " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtColesterolTotal" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label780" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label781" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtColesterolTotalDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label782" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label783" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelColesterolhdl" Title="COLESTEROL HDL" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form144" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow563" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label809" runat="server" Text="Colesterol HDL" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabColesterolHDL" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label810" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label811" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabColesterolHDLDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label812" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label813" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelColesterolldl" Title="COLESTEROL LDL" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form145" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow564" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label814" runat="server" Text="Colesterol LDL" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabColesterolLDL" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label815" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label816" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabColesterolLDLDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label817" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label818" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelColesterolvldl" Title="COLESTEROL VLDL" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form146" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow565" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label819" runat="server" Text="Colesterol VLDL" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabColesterolVLDL" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label820" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label821" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabColesterolVLDLDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label822" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label823" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelTrigliceridos" Title="TRIGLCÉRIDOS" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form147" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow566" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label824" runat="server" Text="Triglicéridos" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTrigliceridos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label825" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label826" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTrigliceridosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label827" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label828" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelLipidostotales" Title="LÍPIDOS TOTALES" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form148" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow567" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label829" runat="server" Text="Lípidos Totales" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabLipidosTotales" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label830" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label831" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabLipidosTotalesDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label832" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label833" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelCreatinina" Title="CREATININA EN SUERO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form139" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow558" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label784" runat="server" Text="Creatinina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCreatinina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label785" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label786" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCreatininaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label787" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label788" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelGlucosa" Title="GLUCOSA EN AYUNAS" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form140" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow559" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label789" runat="server" Text="Glucosa" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabGlucosa" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label790" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label791" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabGlucosaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label792" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label793" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelUrea" Title="ÙREA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form141" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow560" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label794" runat="server" Text="Urea" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabUrea" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label795" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label796" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabUreaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label797" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label798" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelTgo" Title="TGO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form149" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow568" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label834" runat="server" Text="TGO" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTGO" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label835" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label836" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTGODeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label837" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label838" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelTgp" Title="TGP" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form150" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow569" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label839" runat="server" Text="TGP" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTGP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label840" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label841" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTGPDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label842" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label843" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelGgtp" Title="GGTP" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form151" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow570" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label844" runat="server" Text="GGTP" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabGGTP" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label845" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label846" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabGGTPDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label847" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label848" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelBilirrubinatotal" Title="BILIRRUBINAS TOTALES" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form152" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow571" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label849" runat="server" Text="Bilirrubinas Totales" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBilirrTotal" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label850" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label851" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBilirrTotalDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label852" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label853" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelBilirrubinadirecta" Title="BILIRRUBINAS DIRECTA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form153" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow572" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label854" runat="server" Text="Bilirrubinas Directa" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBilirrDirecta" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label855" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label856" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBilirrDirectaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label857" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label858" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelBilirrubinaindirecta" Title="BILIRRUBINAS INDIRECTA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form154" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow573" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label859" runat="server" Text="Bilirrubinas Indirecta" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBilirrIndirecta" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label860" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label861" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBilirrIndirectaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label862" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label863" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelProteinastotales" Title="PROTEÍNAS TOTALES" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form155" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow574" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label864" runat="server" Text="Proteínas Totales" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabProteinasTotales" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label865" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label866" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabProteinasTotalesDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label867" runat="server" Text="gr/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label868" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelFosfatasaalcalina" Title="FOSFATASA ALCALINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form156" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow575" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label869" runat="server" Text="Fosfatasa Alcalina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabFosfatasa" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label870" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label871" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabFosfatasaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label872" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label873" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelAlbumina" Title="ALBUMINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form157" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow576" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label874" runat="server" Text="Albumina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAlbumina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label875" runat="server" Text="gr/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label876" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAlbuminaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label877" runat="server" Text="gr/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label878" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelGlobulina" Title="GLOBULINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form158" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow577" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label879" runat="server" Text="Globulina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabGlobulina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label880" runat="server" Text="gr/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label881" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabGlobulinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label882" runat="server" Text="gr/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label883" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelIndicealbumina_globulina" Title="ÍNDICE ALBÚMINA/GLOBULINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form159" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow578" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label884" runat="server" Text="ALB/GLOB" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabIndiceAlbGlob" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label885" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label886" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabIndiceAlbGlobDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label887" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label888" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelIndicederiesgocoronario" Title="ÍNDICE DE RIESGO CORONARIO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form160" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow579" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label889" runat="server" Text="Indice de Riesgo Coronario" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabIndiceRiesgoCoronario" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label890" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label891" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabIndiceRiesgoCoronarioDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label892" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label893" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelToxicologicocolinesterasa" Title="COLINESTERASA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form161" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow580" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label894" runat="server" Text="Colinesterasa" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabColinesterasa" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label895" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label896" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabColinesterasaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label897" runat="server" Text="U/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label898" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelCeaantigenocarcinoembrionario" Title="CEA (ANTÍGENO CARCINOEMBRIONARIO)" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form162" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow581" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label899" runat="server" Text="CEA (Antígeno Carcinoembrionario)" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAntigenoCarcEmbrion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label900" runat="server" Text="ng/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label901" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAntigenoCarcEmbrionDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label902" runat="server" Text="ng/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label903" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelMetahemoglobina" Title="METAHEMOGLOBINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form163" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow582" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label904" runat="server" Text="Metahemoglobina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabMetahemoglobina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label905" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label906" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabMetahemoglobinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label907" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label908" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelVitaminab12" Title="VITAMINA B12" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form164" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow583" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label909" runat="server" Text="Vitamina B12" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabVitaminaB12" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label910" runat="server" Text="pg/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label911" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabVitaminaB12Deseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label912" runat="server" Text="pg/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label913" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelPcr" Title="PCR" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form165" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow584" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label914" runat="server" Text="PCR" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="tatLabPRC" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label915" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label916" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="tatLabPRCDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label917" runat="server" Text="mg/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label918" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelInsulina" Title="INSULINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form166" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow585" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label919" runat="server" Text="Insulina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabInsulina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label920" runat="server" Text="uUI/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label921" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabInsulinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label922" runat="server" Text="uUI/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label923" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%-- <x:Panel ID="PanelTiocianatoenorina" Title="TIOCIANATO EN ORINA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form167" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow586" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label924" runat="server" Text="Tiocianato en Orina" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiocinatoOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label925" runat="server" Text="mg/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label926" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTiocinatoOrinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label927" runat="server" Text="mg/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label928" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelHemoglobinaglicosilada" Title="HEMOGLOBINA GLICOSILADA" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form168" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow587" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label929" runat="server" Text="Hemoglobina Glicosilada" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHemoglobGlicos" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label930" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label931" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHemoglobGlicosDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label932" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label933" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelCa19_9" Title="CA19-9" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form169" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow588" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label934" runat="server" Text="CA19-9" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCA19_9" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label935" runat="server" Text="U/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label936" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCA19_9Deseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label937" runat="server" Text="U/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label938" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelTsh" Title="TSH" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false"  Visible ="false">
                                <Items>
                                    <x:Form ID="Form170" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow589" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label939" runat="server" Text="TSH" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTSH" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label940" runat="server" Text="mUl/l" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label941" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabTSHDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label942" runat="server" Text="mUl/l" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label943" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelT4libre" Title="T4 LIBRE" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false"  Visible ="false">
                                <Items>
                                    <x:Form ID="Form171" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow590" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label944" runat="server" Text="T4 Libre" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabT4Libre" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label945" runat="server" Text="ng/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label946" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabT4LibreDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label947" runat="server" Text="ng/dl" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label948" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelT3libre" Title="T3 LIBRE" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form172" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow591" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label949" runat="server" Text="T3 Libre" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabT3Libre" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label950" runat="server" Text="pg/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label951" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabT3LibreDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label952" runat="server" Text="pg/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label953" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelBkdirecto" Title="BK – DIRECTO " EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="TRUE" Visible ="false">
                                <Items>
                                    <x:Form ID="Form267" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow706" ColumnWidths="110px 110px 60px 110px 110px 60px 90px 260px 50px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1329" runat="server" Text="MUESTRA" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtBKDirectoMuestra" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1330" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1331" runat="server" Text="Coloración" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtBKDirectoColoracion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1332" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1333" runat="server" Text="Resultados" ShowLabel="false"></x:Label> 
                                                     <x:TextBox ID="txtBKDirectoResultados" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="250"></x:TextBox>
                                                    <x:Label ID="label1334" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelGrupoyfactorsanguineo" Title="GRUPO Y FACTOR SANGUÍNEO" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="true">
                                <Items>
                                    <x:Form ID="Form268" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow707" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1335" runat="server" Text="Grupo" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlGrupoSanguineo" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1336" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1337" runat="server" Text="Factor RH" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlFactorSanguineo" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1338" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1380" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelHemoglobinaHematocrito" Title="HEMOGLOBINA Y HEMATOCRITO" EnableBackgroundColor="true" Height="70px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form269" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FrmHemoglobina" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1381" runat="server" Text="Hemoglobina " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHemoglobinaSolo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1382" runat="server" Text="gr/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1383" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHemoglobinaSoloDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1384" runat="server" Text="gr/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1385" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                           
                                             <x:FormRow ID="FrmHematocrito" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1386" runat="server" Text="Hematocrito " ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHematocritoSolo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1387" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1388" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabHematocritoSoloDesable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1389" runat="server" Text="%" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1390" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelSubunidadbetacualitativo" Title="BETA HCG" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Visible ="false">
                                <Items>
                                    <x:Form ID="Form283" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow724" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1392" runat="server" Text="BETA HCG" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBetaHcg" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1393" runat="server" Text="mUI/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1394" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBetaHcgDesable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1395" runat="server" Text="mUI/ml" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1396" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelParasitologicosimple" Title="PARASITOLÓGICO SIMPLE" EnableBackgroundColor="true" Height="60px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form284" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow725" ColumnWidths="110px 210px 60px 110px 210px 60px 200px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1397" runat="server" Text="Resultados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtParasitoResultados" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextBox>
                                                     <x:Label ID="label1398" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1399" runat="server" Text="Observación" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtParasitoObservacion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextBox>
                                                     <x:Label ID="label1400" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1401" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelParasitologicoseriado" Title="PARASITOLÓGICO SERIADO" EnableBackgroundColor="true" Height="60px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form285" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow726" ColumnWidths="110px 210px 60px 110px 210px 60px 200px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1402" runat="server" Text="Resultados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtParasitoSeriadoResultados" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextBox>
                                                     <x:Label ID="label1403" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1404" runat="server" Text="Observación" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtParasitoSeriadoObservacion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextBox>
                                                     <x:Label ID="label1405" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1406" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelThevenon" Title="THEVENON" EnableBackgroundColor="true" Height="60px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form286" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow727" ColumnWidths="110px 210px 60px 110px 210px 60px 200px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1407" runat="server" Text="Resultados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtThenevonResultados" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextBox>
                                                     <x:Label ID="label1408" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1409" runat="server" Text="Observación" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtThenevonObservacion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextBox>
                                                     <x:Label ID="label1410" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1411" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelCoprocultivo" Title="COPROCULTIVO" EnableBackgroundColor="true" Height="60px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form287" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow728" ColumnWidths="110px 210px 60px 110px 210px 60px 200px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1412" runat="server" Text="Resultados" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCoprocultivoResultados" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextBox>
                                                     <x:Label ID="label1413" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1414" runat="server" Text="Observación" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCoprocultivoObservacion" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="200" Height="40"></x:TextBox>
                                                     <x:Label ID="label1415" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1416" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelToxicologicodecocaina" Title=" TOXICOLÓGICO COCAINA Y MARIHUANA" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form174" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FrmToxicologicodecocaina" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label984" runat="server" Text="Cocaina" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlToxiCocaina" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label985" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label986" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                      <x:TextBox ID="txtToxiCocainaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label987" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label988" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                           <x:FormRow ID="FrmToxicologicomarihuana" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label990" runat="server" Text="Marihuana" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlToxiMarihuana" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label991" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label992" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                      <x:TextBox ID="txtToxiMarihuanaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label993" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label994" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelToxicologicoextasis" Title=" TOXICOLÓGICO EXTASIS" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form142" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                        
                                            <x:FormRow ID="FrmToxicologicoextasis" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label996" runat="server" Text="Extasis" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlToxiExtasis" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label997" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label998" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                    <x:TextBox ID="txtToxiExtasisDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label999" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label1000" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelToxicologicobenzodiazepinas" Title=" TOXICOLÓGICO BENZODIACEPINAS" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form143" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                        
                                            <x:FormRow ID="FrmToxicologicobenzodiazepinas" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label1002" runat="server" Text="Benzodiacepinas" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlToxiBenzodiac" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1003" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label1004" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtToxiBenzodiacDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1005" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label1006" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelToxicologicoanfetaminas" Title=" TOXICOLÓGICO ANFETAMINA" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form175" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                         
                                             <x:FormRow ID="FrmToxicologicoanfetaminas" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label1008" runat="server" Text="Anfetamina" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlToxiAnfetam" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1009" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label1010" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtToxiAnfetamDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1011" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label1012" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelToxicologicometanfetaminas" Title=" TOXICOLÓGICO METANFETAMINAS" EnableBackgroundColor="true" Height="200px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form176" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                         
                                            <x:FormRow ID="FrmToxicologiconetanfetaminas" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                    <x:Label ID="label1014" runat="server" Text="Metanfetaminas" ShowLabel="false"></x:Label>
                                                     <x:DropDownList ID="ddlToxiMetanfeta" runat="server"   Width="100px" ShowLabel="false"></x:DropDownList>
                                                     <x:Label ID="label1015" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label1016" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtToxiMetanfetaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1017" runat="server" Text="" ShowLabel="false" ></x:Label>
                                                    <x:Label ID="label1018" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                        
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelPlomoensangre" Title="PLOMO EN SANGRE" EnableBackgroundColor="true" Height="150px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form177" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FrmPlomoensangre" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label799" runat="server" Text="Plomo" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabPlomo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label800" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label801" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabPlomoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label802" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label803" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <%-- <x:Panel ID="PanelMercurioensangre" Title="MERCURIO EN SANGRE" EnableBackgroundColor="true" Height="150px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form178" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                                <x:FormRow ID="FrmMercurioensangre" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1020" runat="server" Text="Mercurio" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabMercurio" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1021" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1022" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabMercurioDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1023" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1024" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelCromo" Title="CROMO EN SANGRE" EnableBackgroundColor="true" Height="150px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form179" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                               <x:FormRow ID="FrmCromo" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1025" runat="server" Text="Cromo" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCromo" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1026" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1027" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCromoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1028" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1029" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelCadmio" Title="CADMIO EN SANGRE" EnableBackgroundColor="true" Height="150px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form180" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                              <x:FormRow ID="FrmCadmio" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1030" runat="server" Text="Cadmio" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCadmio" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1031" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1032" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCadmioDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1033" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1034" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelArsenico" Title="ARSÉNICO EN SANGRE" EnableBackgroundColor="true" Height="150px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form181" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                              <x:FormRow ID="FrmArsenico" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1035" runat="server" Text="Arsénico" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabArsenico" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1036" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1037" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabArsenicoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1038" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1039" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelBencenoenorina" Title="BENCENO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form183" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FrmBencenoenorina" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1044" runat="server" Text="Benceno" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBenceno" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1045" runat="server" Text="mg/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1046" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabBencenoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1047" runat="server" Text="mg/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1048" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                            <%--<x:Panel ID="PanelCromoenorina" Title="CROMO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form182" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                             <x:FormRow ID="FrmCromoenorina" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1050" runat="server" Text="Cromo" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCromoOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1051" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1052" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCromoOrinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1053" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1054" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelCobreenorina" Title="COBRE EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form184" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                              <x:FormRow ID="FrmCobreenorina" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1056" runat="server" Text="Cobre" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCobreOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1057" runat="server" Text="ug/24h" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1058" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCobreOrinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1059" runat="server" Text="ug/24h" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1060" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelCadmioenorina" Title="CADMIO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form185" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                             <x:FormRow ID="FrmCadmioenorina" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1062" runat="server" Text="Cadmio" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCadmioOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1063" runat="server" Text="ug/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1064" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabCadmioOrinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1065" runat="server" Text="ug/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1066" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelMercurioenorina" Title="MERCURIO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form186" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                             <x:FormRow ID="FrmMercurioenorina" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1068" runat="server" Text="Mercurio" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabMercurioOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1069" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1070" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabMercurioOrinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1071" runat="server" Text="ug/dL" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1072" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelPlomoenorina" Title="PLOMO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form187" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                             <x:FormRow ID="FrmPlomoenorina" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1073" runat="server" Text="Plomo" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabPlomoOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1074" runat="server" Text="ug/g Creatinina" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1075" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabPlomoOrinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1076" runat="server" Text="ug/g Creatinina" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1077" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelNiquelenorina" Title="NIQUEL EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form188" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                                       <x:FormRow ID="FrmNiquelenorina" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1085" runat="server" Text="Niquel" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabNiquelOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1086" runat="server" Text="ug/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1087" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabNiquelOrinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1088" runat="server" Text="ug/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1089" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelMagnesioenorina" Title="MAGNESIO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form189" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                             <x:FormRow ID="FrmMagnesioenorina" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1091" runat="server" Text="Magnesio" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabMagnesioOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1092" runat="server" Text="mg/ 24Hrs" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1093" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="TextBox169" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="txtLabMagnesioOrinaDeseable" runat="server" Text="mg/ 24Hrs" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1095" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelArsenicoenorina" Title="ARSÉNICO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form190" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                             <x:FormRow ID="FrmArsenicoenorina" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1097" runat="server" Text="Arsénico" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabArsenicoOrina" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1098" runat="server" Text="ug/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1099" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabArsenicoOrinaDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1100" runat="server" Text="ug/L" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1101" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelAc_metilhipurico_xileno" Title="AC. METILHIPÚRICO = XILENO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form191" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                               <x:FormRow ID="FrmAc_metilhipurico_xileno" ColumnWidths="109px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1103" runat="server" Text="Ac.Metilhipúrico = Xileno" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAcMetilhipurico" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1104" runat="server" Text="g/g Creatinina" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1105" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabAcMetilhipuricoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1106" runat="server" Text="g/g Creatinina" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label1107" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                               </Items>
                                 </x:Panel>
                             <x:Panel ID="PanelXilenoac_metilhipurico" Title="XILENO = AC.METILHIPÚRICO EN ORINA" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form192" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                              <x:FormRow ID="FrmXilenoac_metilhipurico" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1109" runat="server" Text="Xileno = Ac. Metilhipurico" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabXileno" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1110" runat="server" Text="g/g Creatinina" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1111" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtLabXilenoDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1112" runat="server" Text="g/g Creatinina" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1113" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>--%>
                            <x:Panel ID="PanelFecatest" Title="FECATEST" EnableBackgroundColor="true" Height="40px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="TRUE" Visible ="false">
                                <Items>
                                    <x:Form ID="FormFecatest" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                           <x:FormRow ID="FormRow551" ColumnWidths="110px 110px 60px 110px 110px 60px 90px 260px 50px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label769" runat="server" Text="RESULTADOS" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtResultadosFecatest" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label770" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label771" runat="server" Text="DESABLE" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtDeseableFecatest" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label772" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                    <x:Label ID="label773" runat="server" Text="OBSERVACIÓN" ShowLabel="false"></x:Label> 
                                                     <x:TextBox ID="txtObservacionFecatest" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="250"></x:TextBox>
                                                    <x:Label ID="label804" runat="server" Text="" ShowLabel="false"></x:Label> 
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelHBsAg" Title="HBsAg" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="Form192" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                              <x:FormRow ID="FrmHBsAg" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label1109" runat="server" Text="HBsAg" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHBsAg" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1110" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1111" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHBsAgDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label1112" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label1113" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                 </x:Panel>
                            <x:Panel ID="PanelHCV" Title="HCV" EnableBackgroundColor="true" Height="350px" runat="server"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="true" Visible ="false">
                                <Items>
                                    <x:Form ID="FormHCV" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left" >
                                        <Rows>
                                              <x:FormRow ID="FormRow552" ColumnWidths="110px 110px 60px 110px 110px 60px 400px" runat="server" >
                                                <Items>
                                                     <x:Label ID="label805" runat="server" Text="HCV" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHCV" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label806" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label807" runat="server" Text="Valor Deseable" ShowLabel="false"></x:Label>
                                                     <x:TextBox ID="txtHCVDeseable" Label="" ShowLabel="false" CssClass="mright" runat="server" Width="100"></x:TextBox>
                                                     <x:Label ID="label808" runat="server" Text="" ShowLabel="false" CssClass="red"></x:Label>
                                                     <x:Label ID="label809" runat="server" Text=" " ShowLabel="false"></x:Label>  
                                                    </Items>
                                            </x:FormRow>
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
        <x:Window ID="WindowAddDX" Title="Nuevo Diagnóstico" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
            Target="Top" OnClose="WindowAddDX_Close" IsModal="True" Width="650px" Height="450px" >
        </x:Window>

        <x:Window ID="winEditReco" Title="Recomendación" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
            Target="Top" OnClose="winEditReco_Close" IsModal="True" Width="600px" Height="400px" >
        </x:Window>

        <x:Window ID="winEditRestri" Title="Restricción" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
            Target="Top" OnClose="winEditRestri_Close" IsModal="True" Width="600px" Height="400px" >
        </x:Window>

        <x:Window ID="WindowAddDXFrecuente" Title="Diagnóstico Frecuente" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
            Target="Top" OnClose="WindowAddDXFrecuente_Close" IsModal="True" Width="650px" Height="540px" >
        </x:Window>

          <x:Window ID="WindowReporte" Title="Visor de Reportes" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
            CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
            Target="Top" OnClose="WindowReporte_Close" IsModal="True" Width="650px" Height="540px" >
        </x:Window>

        <x:Window ID="winEdit3" IconUrl="~/images/16/11.png" runat="server" Popup="false"
        IsModal="true" Target="Parent" EnableMaximize="false" EnableResize="false"
        Title="Lista de Examenes" EnableConfirmOnClose="true" CloseAction="HidePostBack"
        EnableIFrame="true" IFrameUrl="about:blank" Height="550px" Width="280px"  OnClose="winEdit3_Close">


    </x:Window>

         <x:Window ID="Window1" IconUrl="~/images/16/11.png" runat="server" Popup="false"
        IsModal="true" Target="Parent" EnableMaximize="false" EnableResize="false"
        Title="Ver Archivos Adjuntos" EnableConfirmOnClose="true" CloseAction="HidePostBack"
        EnableIFrame="true" IFrameUrl="about:blank" Height="210px" Width="300px"  OnClose="Window1_Close">
    </x:Window>
    </form>    
</body>
</html>
