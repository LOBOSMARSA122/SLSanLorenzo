<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="FRM011B.aspx.cs" 
    Inherits="Sigesoft.Server.WebClientAdmin.UI.FRM011B" %>
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
                    <x:Button ID="btnClose" EnablePostBack="false" Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
        <Items>            
            <x:Panel ID="Panel2" Layout="Absolute" runat="server" ShowBorder="true" ShowHeader="false" Height="900" Width="900">
                <Items>
                    <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true"
                        AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">                        
                        <Items>
                            <x:NumberBox ID="txtParameterId" Label="Item Id" runat="server"  />
                            <x:TextBox ID="txtDescription" Label="Valor 1" runat="server"   />                                 
                            <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" Title="Tabla de Items" runat="server"  
                                 EnableRowNumber="True" AllowPaging="false" OnPageIndexChange="grdData_PageIndexChange"
                                IsDatabasePaging="true" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                EnableMouseOverColor="true" ShowGridHeader="true"  OnRowCommand="grdData_RowCommand"   DataKeyNames="i_GroupId,i_ItemId"
                                EnableTextSelection="true" EnableAlternateRowColor="true" Height="260px">    
                                 <Toolbars>
                                    <x:Toolbar ID="Toolbar2" runat="server">
                                        <Items>
                                            <x:Button ID="btnNew" Text="Nuevo Item" Icon="Add" runat="server">
                                            </x:Button>
                                        </Items>
                                    </x:Toolbar>
                                </Toolbars>        
                                <Columns>  
                                    <x:WindowField ColumnID="myWindowField" Width="30px" WindowID="winEdit" HeaderText=""
                                        Icon="Pencil" ToolTip="Editar Item" DataTextFormatString="{0}" 
                                        DataIFrameUrlFields="i_GroupId,i_ItemId" DataIFrameUrlFormatString="FRM011A.aspx?Mode=Edit&i_GroupId={0}&i_ItemId={1}" 
                                        DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Editar Item {0}" />
                                    <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                    ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Item" CommandName="DeleteAction" />                
                                    <x:boundfield Width="400px" DataField="v_Value1" DataFormatString="{0}" HeaderText="Valor 1" DataSimulateTreeLevelField="Level"  ExpandUnusedSpace="true"/>  
                                    <x:boundfield Width="50px" DataField="i_ItemId" DataFormatString="{0}" HeaderText="Id Item"   ExpandUnusedSpace="true"/>                                     
                                </Columns>
                            </x:Grid>   
                        </Items>
                    </x:SimpleForm>
                                                                
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
        <x:HiddenField ID="hfRefresh" runat="server" />
         <x:Window ID="winEdit" Title="Nuevo Item" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="450px" Height="320px" >
    </x:Window>
    </form>
</body>
</html>
