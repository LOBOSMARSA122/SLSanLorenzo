<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM052.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.CargaData.FRM052" %>
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
                    <x:Button ID="btnSaveRefresh" Text="Agregar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="SimpleForm1" TabIndex="9">
                    </x:Button>
                    <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="10">
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
                             <x:DropDownList ID="ddlDocumento"  runat="server" Width="150" Label="Documento" Resizable="True"  ShowRedStar="true"  Required="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" TabIndex="1"></x:DropDownList>   
                            <x:TextBox ID="txtNroDocumento" Label="Nro. Doc." runat="server" Required="true" TabIndex="2"  ShowRedStar="true"  OnTextChanged="txtNroDocumento_TextChanged" AutoPostBack="true"   />
                            <x:TextBox ID="txtNombres" Label="Nombres" runat="server" Required="true" TabIndex="3" ShowRedStar="true"  CssClass="mayus" />
                            <x:TextBox ID="txtApellidoPaterno" Label="Ape. Paterno" runat="server"  Required="true"  ShowRedStar="true"  TabIndex="4" CssClass="mayus"     />
                            <x:TextBox ID="txtApellidoMaterno" Label="Ape. Materno" runat="server" Required="true"  ShowRedStar="true"  TabIndex="5"  CssClass="mayus"   />
                            <x:DropDownList ID="ddlGenero"  runat="server" Width="150" Label="Género" Resizable="True"  ShowRedStar="true"  Required="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" TabIndex="6"></x:DropDownList>       
                            <x:DatePicker ID="dpFechaNacimiento" Label="Fecha Nacim." Width="90px" Required="true" runat="server"  ShowRedStar="true"  DateFormatString="dd/MM/yyyy" TabIndex="7" />
                            <%--<x:DropDownList ID="ddlProtocoloId" runat="server"  Label="Protocolo" Width="240px"></x:DropDownList>--%>     
                            <x:TextBox ID="txtPuesto" Label="Puesto" runat="server" Required="true" TabIndex="8" ShowRedStar="true" />
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
