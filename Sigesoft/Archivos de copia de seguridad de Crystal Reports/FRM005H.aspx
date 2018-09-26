<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM005H.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.FRM005H" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <x:Panel ID="Panel1" runat="server" Layout="Fit" ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">

        
        <Toolbars>
            <x:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="SimpleForm1" TabIndex="7">
                    </x:Button>
                    <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
        <Items>            
             <x:Panel ID="Panel2" EnableBackgroundColor="true" runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                <Items>
                        <x:GroupPanel runat="server" Title="Datos Generales" ID="GroupPanel3" EnableBackgroundColor="True">
                            <Items>
                                <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true" 
                                    AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">                        
                                    <Items>
                                        <x:TextBox ID="txtName" Label="Área" runat="server" Required="true" TabIndex="1" ShowRedStar="true"  CssClass="mayus" />
                                        <x:TextBox ID="txtOfficeNumber" Label="Número oficina" runat="server" Required="true" TabIndex="2" ShowRedStar="true"  CssClass="mayus" />
                                       
                                   </Items>
                                </x:SimpleForm>
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Seleccione uno o varios Componentes" ID="GroupPanel11" EnableBackgroundColor="True" AutoWidth="true">
                            <Items>
                                <x:Tree ID="tvComponent" ShowHeader="False" runat="server" AutoScroll="true" EnableCollapse="false" Expanded="true" Height="245" />
                            </Items>
                        </x:GroupPanel>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
