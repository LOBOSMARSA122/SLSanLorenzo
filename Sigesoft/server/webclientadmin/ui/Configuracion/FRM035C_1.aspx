<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM035C_1.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRM035C_1" %>
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
                            <x:DropDownList ID="ddlLocation" runat="server"  Label="Sede" Width="140px"  ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" ></x:DropDownList>                                                                                    
                            <x:TextBox ID="txtSede" Label="Grupo Riesgo1" runat="server" Required="true" TabIndex="1" ShowRedStar="true"  CssClass="mayus" />    
                            <x:TextBox ID="txtSede2" Label="Grupo Riesgo2" runat="server"  TabIndex="2" ShowRedStar="true"  CssClass="mayus" />
                            <x:TextBox ID="txtSede3" Label="Grupo Riesgo3" runat="server"  TabIndex="3" ShowRedStar="true"  CssClass="mayus" />
                            <x:TextBox ID="txtSede4" Label="Grupo Riesgo4" runat="server"  TabIndex="4" ShowRedStar="true"  CssClass="mayus" />
                            <x:TextBox ID="txtSede5" Label="Grupo Riesgo5" runat="server"  TabIndex="5" ShowRedStar="true"  CssClass="mayus" />              
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
