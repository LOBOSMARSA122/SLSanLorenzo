<%@ Page Language="C#" 
AutoEventWireup="true" 
CodeBehind="FRM009A.aspx.cs" 
Inherits="Sigesoft.Server.WebClientAdmin.UI.FRM009A" %>
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
                    <x:Button ID="btnClose" EnablePostBack="false" Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
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
                            <x:TextBox ID="txtLogId" runat="server" Label="Usuario" Readonly="true"/>  
                            <x:DropDownList ID="ddlNodeId" runat="server" Label="Node" Readonly="true"></x:DropDownList>                            
                            <x:DropDownList ID="ddlEventTypeId" runat="server" Label="Tipo de Evento" Readonly="true"></x:DropDownList>
                            <x:DropDownList ID="ddlSuccess" runat="server" Label="Satisfactorio" Readonly="true"></x:DropDownList>   
                            <x:TextBox ID="txtUserName" runat="server" Label="Usuario" Readonly="true"/>  
                            <x:TextBox ID="txtProcessEntity"  runat="server" Label="Proceso" Readonly="true"></x:TextBox>
                            <x:TextBox ID="txtElementItem"  runat="server" Label="Item" Readonly="true"></x:TextBox>
                            <x:DatePicker ID="txtExpirationDate" Label="Fecha" runat="server" DateFormatString="dd/MM/yyyy"  Readonly="true"/>
                            <x:TextArea ID="txtError" runat="server" Label="Error" Readonly="true"></x:TextArea>
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
