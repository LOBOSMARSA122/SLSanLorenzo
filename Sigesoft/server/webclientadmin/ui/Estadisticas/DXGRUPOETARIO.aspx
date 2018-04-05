<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DXGRUPOETARIO.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Estadisticas.DXGRUPOETARIO" %>

<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<%@ Register Assembly="PdfViewer" Namespace="PdfViewer" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" runat="server" AjaxAspnetControls="Chart1,aspButtonOI" />
    <x:Panel ID="Panel2" runat="server" Height="6000px" Width="1000px" ShowBorder="True" EnableBackgroundColor="true" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" BoxConfigChildMargin="0 0 5 0" ShowHeader="false" Title="">
        <Items>
            <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="115">
                <Items>
                    <x:SimpleForm ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="true" ShowHeader="False" runat="server">
                        <Items>
                            <x:Form ID="Form8" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                <Rows>
                                    <x:FormRow ID="FormRow19" ColumnWidths="460px " runat="server">
                                        <Items>
                                            <x:Form ID="Form9" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                                <Rows>
                                                    <x:FormRow ID="FormRow20" ColumnWidths="200px 200px 550px 150px" runat="server">
                                                        <Items>
                                                            <x:DatePicker ID="dpFechaInicio" Label="F.I" Width="90px" runat="server" DateFormatString="dd/MM/yyyy" />
                                                            <x:DatePicker ID="dpFechaFin" Label="F.F" runat="server" Width="90px" DateFormatString="dd/MM/yyyy" />
                                                            <x:DropDownList ID="ddlCustomerOrganization" runat="server" Label="Empresa" Width="340px" OnSelectedIndexChanged="ddlCustomerOrganization_SelectedIndexChanged" AutoPostBack="true" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                            CompareOperator="NotEqual" CompareMessage="Campo requerido"></x:DropDownList>
                                                            <x:Button ID="btnFilter" Text="Graficar" Icon="ChartBar" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click" ValidateForms="Form9" Hidden="true"></x:Button>
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow1" ColumnWidths="250px 400px 250px" runat="server">
                                                        <Items>   
                                                            <x:DropDownList ID="ddlTipoESO"  Label="Tipo ESO" runat="server"  Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoESO_SelectedIndexChanged" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                            CompareOperator="NotEqual" CompareMessage="Campo requerido" />                                                        
                                                            <x:DropDownList ID="ddlProtocolo"  Label="Protocolo" runat="server"  Width="290px" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                            CompareOperator="NotEqual" CompareMessage="Campo requerido" />   
                                                              <x:DropDownList ID="ddlConsultorio"  Label="Consultorio" runat="server" Width="150px"/>                                                               
                                                        </Items>
                                                    </x:FormRow>
                                                    <x:FormRow ID="FormRow3" ColumnWidths="100px  130px 60px 150px 200px 200px" runat="server">
                                                        <Items>                                                           
                                                          
                                                            <x:Label ID="labedad" Text="Grupo Etario" ShowLabel="false" runat="server"></x:Label>
                                                             <x:DropDownList ID="ddlGrupoEtario"  Label="" runat="server" ShowLabel="false"/>
                                                            <x:Label ID="Label1" Text="GESO" ShowLabel="false" runat="server"></x:Label>
                                                            <x:DropDownList ID="ddlGESO"  Label="GESO" runat="server" ShowLabel="false"/>
                                                            <x:DropDownList ID="ddlGenero"  Label="Género" runat="server"/>   
                                                             <x:NumberBox ID="txtTop"  Label="Top" MaxValue="9" MinValue="1" runat="server" Width="30" Text="9" Required="true"/>                                                         
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
            <x:GroupPanel runat="server" Title="Gráfico" ID="GroupPanel3" AutoWidth="true" BoxFlex="1" Height="580px">
                 <Items>
                    <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                        <Rows>
                             <x:FormRow ID="FormRow2" ColumnWidths="965px" runat="server">
                                <Items>
                                     <x:ContentPanel ID="ContentPanel2" runat="server" Width="965px" BodyPadding="5px" EnableBackgroundColor="true" ShowBorder="true" ShowHeader="false" Title="">
                                                    <asp:chart id="Chart1" runat="server" BackColor="#F3DFC1" Width="950px" Height="500px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" >
							                        <titles>
								                        
							                        </titles>
							                        <borderskin SkinStyle="Emboss"></borderskin>
							                        <series>
								                        <asp:Series IsValueShownAsLabel="true" ChartArea="ChartArea1" Name="Default" CustomProperties="DrawingStyle=Cylinder" BorderColor="64, 0, 0, 0" Palette="BrightPastel" Color="224, 64, 10" ></asp:Series>
							                        </series>
							                        <chartareas>
								                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Transparent" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									                    
									                        <axisy LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
										                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										                        <MajorGrid LineColor="64, 64, 64, 64" />
									                        </axisy>
									                        <axisx LineColor="64, 64, 64, 64" IsLabelAutoFit="true" ArrowStyle="Triangle">
										                        <LabelStyle Font="Trebuchet MS, 8.25pt"/>
										                        <MajorGrid LineColor="64, 64, 64, 64" />
									                        </axisx>
								                        </asp:ChartArea>
							                        </chartareas>
						                        </asp:chart>
                                            <asp:Button ID="aspButtonOI" Text="Ver Gráfico" runat="server" OnClick="aspButtonOI_Click" UseSubmitBehavior="false" />
                                    </x:ContentPanel>                                    
                                </Items>
                            </x:FormRow>
                        </Rows>
                    </x:Form>
                </Items>           
            </x:GroupPanel>



            <x:GroupPanel runat="server" Title="Información Detallada" ID="GroupPanel2" AutoWidth="true" BoxFlex="1" Height="500px">
                <Items>
                    <x:Form ID="Form193" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                        <Rows>
                            <x:FormRow ID="FormRow551" ColumnWidths="600px 350px" runat="server">
                                <Items>
                                    <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" 
                                      EnableRowNumber="True"  Height="450px" Width="550px"
                                        IsDatabasePaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                        EnableMouseOverColor="true" ShowGridHeader="true"
                                        EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" >                        
                                            <Columns>         
                                                <x:boundfield Width="200px" DataField="DxNombre" DataFormatString="{0}" HeaderText="Diagnóstico" />
                                                <x:boundfield Width="200px" DataField="Trabajador" DataFormatString="{0}" HeaderText="Trabajador" />    
                                            </Columns>
                                        </x:Grid>

                                    <x:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" 
                                      EnableRowNumber="True"  Height="450px" Width="350px"
                                        IsDatabasePaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                        EnableMouseOverColor="true" ShowGridHeader="true"
                                        EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" >                        
                                            <Columns>         
                                                <x:boundfield Width="250px" DataField="DxNombre" DataFormatString="{0}" HeaderText="X" />
                                                <x:boundfield Width="50px" DataField="NroTrabajadores" DataFormatString="{0}" HeaderText="Y" />    
                                                <x:boundfield Width="50px" DataField="P_Parc" DataFormatString="{0}" HeaderText="%Parc" />
                                                <x:boundfield Width="50px" DataField="P_Total" DataFormatString="{0}" HeaderText="%Total" />    
                                            </Columns>
                                        </x:Grid>
                                </Items>
                            </x:FormRow>
                        </Rows>
                    </x:Form>

                </Items>
            </x:GroupPanel>
            <x:HiddenField ID="hfRefresh" runat="server" />
        </Items>
    </x:Panel>

    </form>
</body>
</html>
