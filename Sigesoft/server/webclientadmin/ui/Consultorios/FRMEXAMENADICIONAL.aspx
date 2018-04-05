<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMEXAMENADICIONAL.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Servicios.FRMEXAMENADICIONAL" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <x:Panel ID="Panel1" runat="server"  ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
        <Toolbars>
            <x:Toolbar ID="Toolbar1" runat="server">
                <Items>          
                      <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="Form3" TabIndex="10"> </x:Button>
                    <x:Button ID="btnClose" EnablePostBack="false"  Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="11">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
        <Items>
            <x:GroupPanel runat="server" Title="Examenes del Protocolo" ID="GroupPanel2" Width="500" BoxFlex="1" Height="370" >
                <Items>
                     <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false"  runat="server"  DataKeyNames="v_ProtocolComponentId" AutoScroll="true" Height="330px">  
                        <Columns>   
                            <x:CheckBoxField ColumnID="CheckBoxField2" Width="80px" RenderAsStaticField="false" 
                                CommandName="CheckBox1" DataField="Adicional" HeaderText="Seleccionar" />    
                            <x:boundfield Width="270" DataField="v_ComponentName" DataFormatString="{0}" HeaderText="Examen" />
                         
                            <x:TemplateField HeaderText="P.Real" Width="60px">
                                <ItemTemplate>
                                    <asp:TextBox ID="r_Price" runat="server" Width="40px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                        Text='<%# Eval("r_Price") %>'></asp:TextBox>
                                </ItemTemplate>
                            </x:TemplateField>    
                               <x:boundfield Width="120" DataField="v_ComponentId" DataFormatString="{0}" HeaderText="ExamenID" />    
                             <x:boundfield Width="120" DataField="Flag" DataFormatString="{0}" HeaderText="FLAG" /> 
                            <x:boundfield Width="120" DataField="v_ServiceComponentId" DataFormatString="{0}" HeaderText="FLAG" />   
                        </Columns>
                    </x:Grid>
                </Items>               
            </x:GroupPanel> 
        </Items>
    </x:Panel>
    </form>
</body>
</html>
