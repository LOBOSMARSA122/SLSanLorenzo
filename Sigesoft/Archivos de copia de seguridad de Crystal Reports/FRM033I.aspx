<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM033I.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRM033I" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
     <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel5" runat="server" />
        <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False" EnableBackgroundColor="True">
            <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="Form2,SimpleForm2"
                            TabIndex="20">
                        </x:Button>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="21">
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
                                    LabelWidth="145px">
                                    <Rows>
                                        <x:FormRow ID="FormRow1"  ColumnWidths="300px 390px"  runat="server" >
                                            <Items>
                                                <x:DropDownList ID="dllRitmo" Label="Rítmo" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server"
                                                    TabIndex="1"  AutoPostBack="true" />
                                                 <x:TextBox ID="txtFrecuencia" Label="Frecuencia (Lat/mín)" Required="true" ShowRedStar="true" runat="server" TabIndex="2"  />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow2" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtSegPR" Label="Segmento PR" Required="true" ShowRedStar="true" runat="server" TabIndex="3"  />
                                                 <x:TextBox ID="txtOndaQRS" Label="Onda QRS" Required="true" ShowRedStar="true" runat="server" TabIndex="4"  />                                                                                            
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow3" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtSegQT" Label="Segmento QT" Required="true" ShowRedStar="true" runat="server" TabIndex="5"  />
                                                 <x:TextBox ID="txtEjeORS" Label="Onda ORS" Required="true" ShowRedStar="true" runat="server" TabIndex="6"  />        
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow4" runat="server">
                                            <Items>
                                                <x:DropDownList ID="dllConclusiones" Label="Conclusiones" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server" TabIndex="7"  />                                                
                                            </Items>
                                        </x:FormRow>
                                      
                                        <x:FormRow ID="FormRow7" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtDescripcion" Label="Descripción" Required="false" runat="server" TabIndex="8"
                                                    MaxLength="250" MaxLengthMessage="el número máximo de caracteres permitidos es 250" />
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


        <x:HiddenField ID="hfRefresh" runat="server" />


      
    </form>
</body>
</html>
