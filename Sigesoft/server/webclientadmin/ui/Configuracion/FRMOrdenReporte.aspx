º<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMOrdenReporte.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRMOrdenReporte" %>
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
                    <x:Button ID="btnClose" EnablePostBack="false"  Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
            <Items>  
               <x:Panel ID="Panel2" Layout="Absolute" runat="server" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false"  runat="server"  DataKeyNames="" OnRowDataBound="grdData_RowDataBound"
                            AutoScroll="true" Height="350px" SortDirection="ASC" OnSort="grdData_Sort" AllowSorting="true" >                                         
                                <Columns> 
                                    <x:CheckBoxField  ColumnID="b_Seleccionar" ID="b_Seleccionar" Width="30px" RenderAsStaticField="false"
                                                CommandName="CheckBox1" DataField="b_Seleccionar" HeaderText="" SortField="b_Seleccionar" />
                                    <x:boundfield Width="260" DataField="v_NombreReporte" ID="nombrereporte" DataFormatString="{0}" HeaderText="Nombre Reporte" />                                                                                                                                                            
                                    <x:boundfield Width="200" DataField="v_ComponenteId" ID="componenteid" DataFormatString="{0}" HeaderText="Componente Id" />     
                                    <x:TemplateField HeaderText="Orden" Width="60px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="i_Orden" runat="server" Width="40px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                Text='<%# Eval("i_Orden") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </x:TemplateField>
                                    <x:boundfield Width="200" DataField="v_NombreCrystal" ID="nombrecrystal" DataFormatString="{0}" HeaderText="Nombre Crystal" />                                                                                                                                                            
                                    <x:boundfield Width="200" DataField="i_NombreCrystalId" ID="nombrecrystalid" DataFormatString="{0}" HeaderText="Nombre Crystal ID" />             
                                </Columns>
                        </x:Grid>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
