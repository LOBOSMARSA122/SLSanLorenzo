<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMOrdenReportes.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRMOrdenReportes" %>
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
                    <x:Button ID="btnGenerarPDF" Text="Generar PDF" runat="server" Icon="SystemSave" OnClick="btnGenerarPDF_Click" ValidateForms="SimpleForm1" TabIndex="7">
                    </x:Button>                
                    <x:Button ID="btnClose" EnablePostBack="false"  Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
            <Items>               
                <x:Panel ID="Panel3" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False" AutoScroll="true">
                    <Items>   
                        <x:Form ID="Form2" runat="server"  ShowBorder="False" ShowHeader="False" LabelWidth="1px" LabelAlign="Left">
                            <Rows>
                                <x:FormRow ID="FormRow1" ColumnWidths="20px 200px" runat="server">
                                    <Items>
                                        <x:CheckBox ID="chktodos" runat="server" OnCheckedChanged="chktodos_CheckedChanged" AutoPostBack="true"></x:CheckBox>
                                        <x:Label ID="lbltodos"  Text="Seleccionar Todos" runat="server" />
                                    </Items>                                     
                            </x:FormRow>
                            </Rows>                           
                        </x:Form>                                                                                
                        <x:GroupPanel  runat="server" Title="Seleccionar Reportes" ID="GroupPanel1" EnableBackgroundColor="True" AutoWidth="true">
                            <Items>
                                <x:CheckBoxList ID="chkregistros" Label="" ShowLabel="false" ColumnNumber="1" ColumnVertical="true" 
                                    runat="server" OnSelectedIndexChanged="chkregistros_SelectedIndexChanged" >
                                </x:CheckBoxList>
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
