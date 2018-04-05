<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM033F.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRM033F" %>
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
                        <x:Button ID="btnSaveRefresh" Text="Seleccionar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="Form2,SimpleForm2"
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
                        AutoScroll="true" runat="server" EnableCollapse="True" Height="180px" >                        
                            <Items>     
                                <x:DropDownList ID="ddlConsultorio" runat="server"  Label="Consultorio" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlConsultorio_SelectedIndexChanged"></x:DropDownList>    
                                <x:DropDownList ID="ddlExamen" runat="server"  Label="Examen" Width="140px"></x:DropDownList>     
                                <x:TextBox ID="txtDx" Label="Diagnóstico" Width="500" runat="server"></x:TextBox> 
                                <x:Button ID="btnCie10" Text="Buscar CIE10" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" ValidateForms="Form2" ></x:Button>                                                                                 
                            </Items>
                        </x:SimpleForm>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>

         <x:HiddenField ID="hfRefresh" runat="server" />
        <x:Window ID="WindowCie10" Title="Nuevo Diagnóstico" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="WindowCie10_Close" IsModal="True" Width="650px" Height="550px" >
    </x:Window>
    </form>
</body>
</html>
