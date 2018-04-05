<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAntecedenteOcupacional.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmAntecedenteOcupacional" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <x:Panel ID="Panel1" runat="server" Layout="Fit" ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
        <Toolbars>
            <x:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="SimpleForm1" TabIndex="9">
                    </x:Button>
                    <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="10">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
        <Items>
            
           <x:Panel ID="Panel6" AutoHeight="true" EnableBackgroundColor="True"
                    runat="server" BodyPadding="2px" ShowBorder="False" ShowHeader="False">     
                        <Items>
                            <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px">
                                <Rows>
                                    <x:FormRow ID="FormRow1"  ColumnWidths="230px 80px 120px"  runat="server" >
                                        <Items>
                                            <x:DatePicker ID="dtcFechaInicio" runat="server" Label="Fecha de Inicio" TabIndex="1" Width="120"  DateFormatString="dd/MM/yyyy"></x:DatePicker>
                                            <x:Label ID="lblfechafin" runat="server" Text="Fecha Fin" ShowLabel="false"></x:Label>
                                            <x:DatePicker ID="dtcFechaFin" runat="server" Label="Fecha Fin" TabIndex="2"  Width="120" ShowLabel="false" DateFormatString="dd/MM/yyyy"></x:DatePicker>
                                        </Items>
                                    </x:FormRow>
                                    <x:FormRow ID="FormRow8"  ColumnWidths="400px"  runat="server" >
                                        <Items>
                                            <x:CheckBox runat="server" ID="chkSoloAnio"  Label="Tomar solo Año"></x:CheckBox>
                                        </Items>
                                    </x:FormRow>
                                    <x:FormRow ID="FormRow2"  ColumnWidths="400px"  runat="server" >
                                        <Items>
                                            <x:TextBox ID="txtEmpresa" Label="Empresa" Required="true" ShowRedStar="true" runat="server" TabIndex="3" CssClass="mrightmayus"/>
                                        </Items>
                                    </x:FormRow>
                                      <x:FormRow ID="FormRow3"  ColumnWidths="400px"  runat="server" >
                                        <Items>
                                            <x:TextBox ID="txtArea" Label="Área" Required="true" ShowRedStar="true" runat="server" TabIndex="4" CssClass="mrightmayus"/>
                                        </Items>
                                    </x:FormRow>
                                    <x:FormRow ID="FormRow4"  ColumnWidths="400px"  runat="server" >
                                        <Items>
                                            <x:TextBox ID="txtPuestoTrabajo" Label="Puesto Trabajo" ShowRedStar="true" Required="true" runat="server" TabIndex="5" CssClass="mrightmayus"/>     
                                        </Items>
                                    </x:FormRow>
                                     <x:FormRow ID="FormRow5"  ColumnWidths="400px"  runat="server" >
                                        <Items>
                                            <x:CheckBox ID="chkPuestoActual" runat="server" Label="Puesto Actual" TabIndex="6"></x:CheckBox>
                                        </Items>
                                    </x:FormRow>
                                     <x:FormRow ID="FormRow6"  ColumnWidths="400px"  runat="server" >
                                        <Items>
                                            <x:NumberBox ID="txtAlturaGeografica" Label="Alt. Geográfica" runat="server" Width="120"  TabIndex="7"  NoDecimal="True" NoNegative="True"/>
                                        </Items>
                                    </x:FormRow>
                                     <x:FormRow ID="FormRow7"  ColumnWidths="400px"  runat="server" >
                                        <Items>
                                            <x:DropDownList ID="ddlTipoOperacion"  runat="server" Width="120" Label="Tipo Operación" TabIndex="8"></x:DropDownList>  
                                        </Items>
                                    </x:FormRow>
                                </Rows>
                            </x:Form>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
