<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="FRM001A.aspx.cs" 
    Inherits="Sigesoft.Server.WebClientAdmin.UI.FRM001A" %>
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
            
            <x:Panel ID="Panel2" Layout="Fit" runat="server" ShowBorder="false" ShowHeader="false" >
                <Items>
                    <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true" 
                        AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">                        
                        <Items>
                            <x:NumberBox ID="txtGroupId" Label="Id Grupo" Required="true" ShowRedStar="true" runat="server" TabIndex="0"/>
                            <x:NumberBox ID="txtParameterId" Label="Id Parámetro" Required="true" ShowRedStar="true" runat="server" TabIndex="1" NoDecimal="True" NoNegative="True" />
                            <x:NumberBox ID="txtUserInterfaceOrder" Label="Ordenamiento" Required="false" runat="server" TabIndex="2"   NoDecimal="True" NoNegative="True"/>                                      
                            <x:TextBox ID="txtDescription" Label="Valor 1" runat="server" Required="true" TabIndex="3" ShowRedStar="true"  CssClass="mayus" />
                            <x:TextBox ID="txtDescription2" Label="Valor 2" runat="server"  Required="false"  TabIndex="4"  CssClass="mayus"  />
                            <x:TextBox ID="txtField" Label="Campo referencial" runat="server" Required="false" TabIndex="5"  CssClass="mayus" />
                            <x:GroupPanel runat="server" AutoHeight="True" Title="Información de Grupo / Parametros (Opcional)" ID="GroupPanel1" EnableCollapse="True">
                                <Items>                              
                                 <x:DropDownList ID="ddlParentParameterId"  runat="server" Width="150" Label="Id Parametro Padre" Resizable="True" EnableSimulateTree="true" ></x:DropDownList>                                    
                                </Items>
                            </x:GroupPanel>
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
