<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="FRM007.aspx.cs" 
    Inherits="Sigesoft.Server.WebClientAdmin.UI.FRM007" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración Aplicación Jerárquica</title>
</head>
<body>
   <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1"/>
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Administración Aplicación Jerárquica" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
            <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" 
            PageSize="15" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grdData_PageIndexChange"
            IsDatabasePaging="true" EnableRowNumberPaging="true"  RowNumberWidth="40" AjaxLoadingType="Default"
            EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="i_ApplicationHierarchyId" 
            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" OnRowCommand="grdData_RowCommand">
                <Toolbars>
                    <x:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <x:Button ID="btnNew" Text="Nuevo Itrm" Icon="Add" runat="server">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Toolbars>
                <Columns>                   
                    <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                        Icon="Pencil" ToolTip="Editar Item" DataTextFormatString="{0}" 
                        DataIFrameUrlFields="i_ApplicationHierarchyId" DataIFrameUrlFormatString="FRM007A.aspx?Mode=Edit&i_ApplicationHierarchyId={0}" 
                        DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Editar Aplicación Jerárquica {0}" />
                    <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                        ColumnID="lbfAction2" Width="25px" ToolTip="Eliminar Item" CommandName="DeleteAction" />
                    <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de clonar este registro?" Icon="Calculator" ConfirmTarget="Top"
                        ColumnID="lbfAction3" Width="25px" ToolTip="Clonar Item" CommandName="ClonAction" />
                    <x:boundfield Width="400px" DataField="v_Value1" DataFormatString="{0}" HeaderText="Descripción" DataSimulateTreeLevelField="Level"/>  
                    <x:boundfield Width="50px" DataField="i_ApplicationHierarchyId" DataFormatString="{0}" HeaderText="Id"/>  
                    <x:boundfield Width="150px" DataField="v_Form" DataFormatString="{0}" HeaderText="Form"  />  
                    <x:boundfield Width="150px" DataField="v_Code" DataFormatString="{0}" HeaderText="Code"  />  
                    <x:boundfield Width="150px" DataField="v_ScopeName" DataFormatString="{0}" HeaderText="Scope" />  
                    <x:boundfield Width="150px" DataField="v_ApplicationHierarchyTypeName" DataFormatString="{0}" HeaderText="Tipo"/>
                    <x:boundfield Width="150px" DataField="v_BusinessRuleName" DataFormatString="{0}" HeaderText="Regla Negocio"/>   
               </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />

    <x:Window ID="winEdit" Title="Nuevo Item Aplicación Jerárquica" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="550px" Height="300px" >
    </x:Window>
    </form>
</body>
</html>
