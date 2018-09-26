<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM033B.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRM033B" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
     <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
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
                 <x:Panel ID="Panel4" Layout="Fit" runat="server" ShowBorder="false" ShowHeader="false" >
                    <Items>
                        <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true" 
                        AutoScroll="true" runat="server" EnableCollapse="True" Height="330px" >                        
                            <Items>   
                                    <x:TextBox ID="txtRecoBus" runat="server" Label="Buscar" AutoPostBack="true" OnTextChanged="txtRecoBus_TextChanged"></x:TextBox>  
                                    <x:DropDownList ID="ddlRecomendaciones" runat="server"  Label="Recomendaciones"  AutoPostBack="true" OnSelectedIndexChanged="ddlRecomendaciones_SelectedIndexChanged" ></x:DropDownList>        
                                    <x:Label ID="lblName" runat="server" Label="Ver Completo"></x:Label>   
                                   <x:GroupPanel runat="server" Title="Crear Nueva Recomendación" ID="GroupPanel1" Width="560px" AutoWidth="true" BoxFlex="1" Height="90" > 
                                        <Items>
                                             <x:TextBox ID="txtNuevaRecomendacion" runat="server" Label="Nombre" AutoPostBack="true"></x:TextBox>  
                                             <x:Button ID="btnGrabar" Text="Guardar nueva Recomendación" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline"  ValidateForms="Form2" OnClick="btnGrabar_Click" ></x:Button>                                       
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
