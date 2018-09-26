<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM031C.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.ExternalUser.FRM031C" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" AutoSizePanelID="Panel5" runat="server" />
        <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False"
            EnableBackgroundColor="True" AutoWidth="true">
            <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>    
                         <x:Button ID="btnSaveRefresh" Text="Visualizar" runat="server" Icon="Eye" ValidateForms="SimpleForm1" Enabled="false" TabIndex="7">
                         </x:Button>                  
                        <x:Button ID="btnClose" EnablePostBack="true" Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="8" OnClick="btnClose_Click">
                    </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel2" EnableBackgroundColor="true"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                    <Items>                  
                        <x:GroupPanel runat="server" Title="Seleccionar Ficha(s)" ID="GroupPanel1" EnableBackgroundColor="True" AutoWidth="true">
                            <Items>
                                <x:CheckBoxList ID="informesSeleccionados" Label="" ShowLabel="false" ColumnNumber="1" ColumnVertical="true"
                                    runat="server"  AutoPostBack="true" OnSelectedIndexChanged="informesSeleccionados_SelectedIndexChanged">
                                </x:CheckBoxList>
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>

            </Items>
        </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />
          <x:Window ID="winEdit1" IconUrl="~/images/16/11.png" runat="server" Popup="false"
        IsModal="False" Target="Top" EnableMaximize="true" EnableResize="true"
        Title="Visualizador de Fichas Ocupacionales" EnableConfirmOnClose="true" CloseAction="HidePostBack"
        EnableIFrame="true" IFrameUrl="about:blank" Height="560px" Width="950px">
    </x:Window>
    </form>
</body>
</html>
