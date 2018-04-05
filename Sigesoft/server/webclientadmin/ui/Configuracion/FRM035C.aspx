<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM035C.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRM035C" %>
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
                    <x:Button ID="btnClose" EnablePostBack="false"  Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
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
                            <x:DropDownList ID="ddlLocation" runat="server"  Label="Sede" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></x:DropDownList>    
                                         
                            <x:Grid ID="grdData" ShowBorder="false" ShowHeader="false" Title="Tabla de GESO" runat="server"  
                                 EnableRowNumber="True" AllowPaging="false" OnPageIndexChange="grdData_PageIndexChange"
                                IsDatabasePaging="true" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                EnableMouseOverColor="true" ShowGridHeader="true"  OnRowCommand="grdData_RowCommand" DataKeyNames="v_GroupOccupationId,v_OrganizationId,v_Name,v_LocationId" 
                                EnableTextSelection="true" EnableAlternateRowColor="true" Height="270px">    
                                 <Toolbars>
                                    <x:Toolbar ID="Toolbar2" runat="server">
                                        <Items>
                                            <x:Button ID="btnNew" Text="Nuevo Grupo Riesgo" Icon="Add" runat="server">
                                            </x:Button>
                                        </Items>
                                    </x:Toolbar>
                                </Toolbars>        
                                <Columns>
                                    <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                        Icon="Pencil" ToolTip="Editar Grupo Riesgo" DataTextFormatString="{0}" 
                                        DataIFrameUrlFields="v_GroupOccupationId,v_Name,v_OrganizationId,v_LocationId" DataIFrameUrlFormatString="FRM035C_1.aspx?Mode=Edit&v_GroupOccupationId={0}&v_Name={1}&v_OrganizationId={2}&v_LocationId={3}" 
                                        DataWindowTitleField="v_Name" DataWindowTitleFormatString="Editar GESO {0}" />
                                    <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                    ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Grupo Riesgo" CommandName="DeleteAction" />                             
                                    <x:boundfield Width="250px" DataField="v_Name" DataFormatString="{0}" HeaderText="Grupo Riesgo" />     
                                </Columns>
                            </x:Grid>   
                        </Items>
                    </x:SimpleForm>                           
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />
         <x:Window ID="winEdit" Title="Nuevo Grupo Riesgo" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown" OnClose="winEdit_Close"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top"  IsModal="True" Width="450px" Height="320px" >
    </x:Window>
    </form>
</body>
</html>
