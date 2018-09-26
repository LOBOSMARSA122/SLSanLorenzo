<%@ Page Language="C#" 
AutoEventWireup="true" 
CodeBehind="FRM028.aspx.cs" 
Inherits="Sigesoft.Server.WebClientAdmin.UI.Sync.FRM012" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Software Component Release</title>
    <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1"/>
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Software Component Release" EnableBackgroundColor="true" Layout="VBox" 
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
                                    <x:Label ID="Label2" Width="200px" runat="server" CssClass="inline" ShowLabel="false" Text="Software Component Version" /> 
                                    <x:TextBox ID="txtSoftwareComponentVersion" Width="150px" Label="Valor 1" runat="server" CssClass="mrightmayus" RegexIgnoreCase="True" />
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
            EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="i_SoftwareComponentId,v_SoftwareComponentVersion" 
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
                        DataIFrameUrlFields="i_SoftwareComponentId,v_SoftwareComponentVersion" DataIFrameUrlFormatString="FRM028A.aspx?Mode=Edit&i_SoftwareComponentId={0}&v_SoftwareComponentVersion={1}" 
                        DataWindowTitleField="v_Value1" DataWindowTitleFormatString="Editar {0}" />
                    <x:boundfield Width="150px" DataField="i_SoftwareComponentId" DataFormatString="{0}" HeaderText="SoftwareComponentId" />
                    <x:boundfield Width="150px" DataField="v_SoftwareComponentVersion" DataFormatString="{0}" HeaderText="SoftwareComponentVersion" /> 
                    <x:boundfield Width="250px" DataField="v_DatabaseVersionRequired" DataFormatString="{0}" HeaderText="DatabaseVersionRequired" />
                    <x:boundfield Width="250px" DataField="v_ReleaseNotes" DataFormatString="{0}" HeaderText="ReleaseNotes" />
                    <x:boundfield Width="250px" DataField="v_AdditionalInformation1" DataFormatString="{0}" HeaderText="AdditionalInformation 1" />
                    <x:boundfield Width="250px" DataField="v_AdditionalInformation2" DataFormatString="{0}" HeaderText="v_AdditionalInformation 2" />
                    <x:boundfield Width="250px" DataField="i_IsPublished" DataFormatString="{0}" HeaderText="IsPublished" />
                    <x:boundfield Width="250px" DataField="i_IsLastVersion" DataFormatString="{0}" HeaderText="IsLastVersion" />
                    <x:boundfield Width="250px" DataField="d_ReleaseDate" DataFormatString="{0}" HeaderText="ReleaseDate" />
                    <x:boundfield Width="100px" DataField="d_InsertDate" DataFormatString="{0}" HeaderText="Fecha Crea" />
                    <x:boundfield Width="100px" DataField="d_UpdateDate" DataFormatString="{0}" HeaderText="Fecha Act." />
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />

 <x:Window ID="winEdit" Title="Nuevo Software Component Release" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="570px" Height="420px" >
    </x:Window>
    </form>
</body>

</html>
