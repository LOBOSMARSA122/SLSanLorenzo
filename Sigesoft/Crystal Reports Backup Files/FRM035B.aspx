<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM035B.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRM035B" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
       <form id="form2" runat="server">
   <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <x:Panel ID="Panel1" runat="server" Layout="Fit" ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
        <Toolbars>
            <x:Toolbar ID="Toolbar1" runat="server">
                <Items>                 
                    <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
        <Items>            
            <x:Panel ID="Panel2" Layout="Absolute" runat="server" ShowBorder="false" ShowHeader="false">
                <Items>
                    <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true"
                        AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">                        
                        <Items>                                            
                            <x:TextBox ID="txtEmpresa" Label="Empresa" runat="server"  TabIndex="3"  />                        
                            <x:Grid ID="grdData" ShowBorder="false" ShowHeader="false" Title="Tabla de Sedes" runat="server"  
                                 EnableRowNumber="True" AllowPaging="false" OnPageIndexChange="grdData_PageIndexChange"
                                IsDatabasePaging="true" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                EnableMouseOverColor="true" ShowGridHeader="true"  OnRowCommand="grdData_RowCommand" DataKeyNames="v_LocationId,v_OrganizationId,v_Name" 
                                EnableTextSelection="true" EnableAlternateRowColor="true" Height="260px">    
                                 <Toolbars>
                                    <x:Toolbar ID="Toolbar2" runat="server">
                                        <Items>
                                            <x:Button ID="btnNew" Text="Nuevo Sede" Icon="Add" runat="server">
                                            </x:Button>
                                        </Items>
                                    </x:Toolbar>
                                </Toolbars>        
                                <Columns>
                                    <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                        Icon="Pencil" ToolTip="Editar Sede" DataTextFormatString="{0}" 
                                        DataIFrameUrlFields="v_LocationId,v_Name,v_OrganizationId" DataIFrameUrlFormatString="FRM035B_1.aspx?Mode=Edit&v_LocationId={0}&v_Name={1}&v_OrganizationId={2}" 
                                        DataWindowTitleField="v_Name" DataWindowTitleFormatString="Editar Sede {0}" />
                                    <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                    ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Hijo" CommandName="DeleteAction" />                             
                                    <x:boundfield Width="250px" DataField="v_Name" DataFormatString="{0}" HeaderText="Sede" />     
                                </Columns>
                            </x:Grid>   
                        </Items>
                    </x:SimpleForm>                           
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />
         <x:Window ID="winEdit" Title="Nuevo Sede" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown" OnClose="winEdit_Close"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top"  IsModal="True" Width="450px" Height="120px" >
    </x:Window>
    </form>
</body>
</html>
