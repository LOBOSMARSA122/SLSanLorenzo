<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM033J.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRM033J" %>
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
                                                <x:TextArea  ID="txtVertices" Label="Vertices" runat="server" TabIndex="1" Height="30" Width="140"></x:TextArea>
                                                 <x:TextArea  ID="txtCampoPulmo" Label="Campos Pulmonares" runat="server" TabIndex="2" Height="30" Width="140"></x:TextArea>
                                            </Items>
                                        </x:FormRow>
                                      

                                         <x:FormRow ID="FormRow2"  ColumnWidths="300px 390px"  runat="server" >
                                            <Items>
                                                <x:TextArea  ID="txtHilios" Label="Hilios" runat="server" TabIndex="1" Height="30" Width="140"></x:TextArea>
                                                 <x:TextArea  ID="txtSenosCosto" Label="Senos Costodiafragmáticos" runat="server" TabIndex="2" Height="30" Width="140"></x:TextArea>
                                            </Items>
                                        </x:FormRow>

                                         <x:FormRow ID="FormRow3"  ColumnWidths="300px 390px"  runat="server" >
                                            <Items>
                                                <x:TextArea  ID="txtSenosCardio" Label="Senos Cardiofrenicos" runat="server" TabIndex="1" Height="30" Width="140"></x:TextArea>
                                                 <x:TextArea  ID="txtMediastinos" Label="Mediastinos" runat="server" TabIndex="2" Height="30" Width="140"></x:TextArea>
                                            </Items>
                                        </x:FormRow>


                                         <x:FormRow ID="FormRow4"  ColumnWidths="300px 390px"  runat="server" >
                                            <Items>
                                                <x:TextArea  ID="txtSiluetaCardiaca" Label="Silueta Cardiaca" runat="server" TabIndex="1" Height="30" Width="140"></x:TextArea>
                                                 <x:TextArea  ID="txtIndiceCardio" Label="Índice Cardiotoráxico" runat="server" TabIndex="2" Height="30" Width="140"></x:TextArea>
                                            </Items>
                                        </x:FormRow>


                                         <x:FormRow ID="FormRow5"  ColumnWidths="300px 390px"  runat="server" >
                                            <Items>
                                                <x:TextArea  ID="txtPartesBlandas" Label="Partes Blandas" runat="server" TabIndex="1" Height="30" Width="140"></x:TextArea>
                                                 <x:TextArea  ID="txtDescripcion" Label="Descripción" runat="server" TabIndex="2" Height="30" Width="140"></x:TextArea>
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
