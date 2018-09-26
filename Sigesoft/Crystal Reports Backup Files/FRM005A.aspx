<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005A.aspx.cs" Inherits="Sigesoft.WebClient.Main.Common.FRM005A" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel5" runat="server" />
        <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False" EnableBackgroundColor="True">
            <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="Form2,SimpleForm2" 
                            TabIndex="8">
                        </x:Button>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="9">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel6" AutoHeight="true" EnableBackgroundColor="True"
                    runat="server" BodyPadding="2px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:GroupPanel runat="server" Title="Datos Generales" ID="GroupPanel4">
                            <Items>
                                <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False"
                                    LabelWidth="160px">
                                    <Rows>
                                        <x:FormRow ID="FormRow1" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtDescription" Label="Nombre de Nodo" Required="true" ShowRedStar="true" runat="server" TabIndex="1" MaxLength="250" 
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 250" CssClass="mayus"/>                                              
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow2" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtGeografyLocationId" Label="Ubicación geográfica Cod." Required="true" ShowRedStar="true" runat="server" TabIndex="2" MaxLength="6"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 6" CssClass="mayus"/>                                           
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow3" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtGeografyLocationDescription" Label="Ubicación geográfica" Required="true" runat="server" TabIndex="3" MaxLength="250" 
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 250" CssClass="mayus" />                                            
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow4" runat="server">
                                            <Items>
                                                <x:DropDownList ID="ddlNodeType" Label="Tipo de Nodo" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server" TabIndex="4" />                                            
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow5" runat="server">
                                            <Items>                
                                                <x:DatePicker ID="dpBeginDate" Label="Fecha Inicio" Required="true" ShowRedStar="true" runat="server" TabIndex="5"
                                                    DateFormatString="dd/MM/yyyy" EmptyText="Por favor seleccione una fecha de inicio." />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow6" runat="server">
                                            <Items>
                                                <x:DatePicker ID="dpEndDate" Label="Fecha Fin" Required="false" runat="server" TabIndex="6"
                                                    DateFormatString="dd/MM/yyyy"  />                                              
                                            </Items>
                                        </x:FormRow>
                                       
                                    </Rows>
                                </x:Form>
                            </Items>
                        </x:GroupPanel>
                    
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
      
    </form>
</body>
</html>
