<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMOrdenReportes.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRMOrdenReportes" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .panel2 {
            overflow-x: scroll;
            overflow-y: scroll;
            height:370px;
        }
        
    </style>
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
                <x:Panel ID="Panel3" EnableBackgroundColor="true"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False" AutoScroll="true">
                    <Items>                  
                        <x:GroupPanel runat="server" Title="Seleccionar Ficha(s)" ID="GroupPanel1" EnableBackgroundColor="True" AutoWidth="true">
                            <Items>
                                <x:CheckBoxList ID="chkregistros" Label="" ShowLabel="false" ColumnNumber="1" ColumnVertical="true" 
                                    runat="server"  AutoPostBack="true" OnSelectedIndexChanged="chkregistros_SelectedIndexChanged">
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
