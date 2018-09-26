<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM029.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Sync.FRM029" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deployment File</title>
</head>
<body>
    <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1"/>
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Deployment File" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
             <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="74" >                
                <Items>
                      <x:SimpleForm  ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server">
                        <Items>                             
                            <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item" ShowBorder="false" EnableBackgroundColor="true" Layout="Column"  runat="server">
                                <Items>
                                    <x:Label ID="Label1" Width="200px" runat="server" CssClass="inline" ShowLabel="false" Text="Software Component Release" />   
                                    <x:DropDownList ID="ddlSoftwareComponentId"  Width="400" runat="server"  Label="Software Component" Resizable="True" TabIndex="1"></x:DropDownList>                                   
                                    <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click" ></x:Button>                                 
                                </Items>
                            </x:Panel> 
                        </Items> 
                    </x:SimpleForm>
                </Items>
            </x:GroupPanel>
          <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" 
            PageSize="15" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grdData_PageIndexChange"
            IsDatabasePaging="true" EnableRowNumberPaging="true"  RowNumberWidth="40" AjaxLoadingType="Default"
            EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="i_DeploymentFileId" 
            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" >
                <Toolbars>
                    <x:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <x:Button ID="btnNew" Text="Nuevo" Icon="Add" runat="server">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Toolbars>
                <Columns>                   
                    <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                        Icon="Pencil" ToolTip="Editar" DataTextFormatString="{0}" 
                        DataIFrameUrlFields="i_DeploymentFileId" DataIFrameUrlFormatString="FRM029A.aspx?Mode=Edit&i_DeploymentFileId={0}" 
                        DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Editar {0}" />
                    <x:boundfield Width="150px" DataField="v_FileName" DataFormatString="{0}" HeaderText="File Name" />
                    <x:boundfield Width="150px" DataField="b_FileData" DataFormatString="{0}" HeaderText="File Data" /> 
                    <x:boundfield Width="250px" DataField="v_Description" DataFormatString="{0}" HeaderText="Description" />
                    <x:boundfield Width="250px" DataField="i_SoftwareComponentId" DataFormatString="{0}" HeaderText="Software Component Id" />
                    <x:boundfield Width="250px" DataField="v_TargetSoftwareComponentVersion" DataFormatString="{0}" HeaderText="Target Software Component Version" />
                    <x:boundfield Width="250px" DataField="v_PackageFiles" DataFormatString="{0}" HeaderText="Package Files" />
                    <x:boundfield Width="250px" DataField="r_PackageSizeKb" DataFormatString="{0}" HeaderText="Package Size Kb" />
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />

 <x:Window ID="winEdit" Title="Nuevo Deployment File" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="570px" Height="500px" >
    </x:Window>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </form>
</body>
</html>
