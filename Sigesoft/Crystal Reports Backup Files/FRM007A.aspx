<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="FRM007A.aspx.cs" 
    Inherits="Sigesoft.Server.WebClientAdmin.UI.FRM007A" %>
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
                            <x:DropDownList ID="ddlApplicationHierarchyTypeId" runat="server"  Label="Type" ShowRedStar="true" CompareValue="-1" CompareOperator="NotEqual" Resizable="True" TabIndex="0" ></x:DropDownList>   
                            <x:DropDownList ID="ddlTypeFormId" runat="server"  Label="Aplicación" ShowRedStar="true" CompareValue="-1" CompareOperator="NotEqual" Resizable="True" TabIndex="1" ></x:DropDownList>   
                            <x:DropDownList ID="ddlScopeId" runat="server"  Label="Scope" Resizable="True" TabIndex="2"></x:DropDownList> 
                            <x:TextBox ID="txtDescription" Label="Description" runat="server" Required="true" ShowRedStar="true" TabIndex="3"  />                                 
                            <x:TextBox ID="txtForm" Label="Form" runat="server"  TabIndex="4"  />  
                            <x:TextBox ID="txtCode" Label="Code" runat="server"  TabIndex="5"  />  
                            <x:DropDownList ID="ddlBusinessRule" runat="server" Width="400" Label="Regla Nego." Resizable="True" TabIndex="6"></x:DropDownList>
                            <x:DropDownList ID="ddlParentId" runat="server" Width="400" Label="Parent" Resizable="True" TabIndex="7" EnableSimulateTree="true" AutoPostBack="true" ></x:DropDownList> 
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
