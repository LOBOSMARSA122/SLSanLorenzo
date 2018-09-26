<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM030.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Sync.FRM030" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Server Node Sync</title>
</head>
<body>
    <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1"/>
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Server Node Sync" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
             <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="74" >                
                <Items>
                      <x:SimpleForm  ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server">
                        <Items>                             
                            <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item" ShowBorder="false" EnableBackgroundColor="true" Layout="Column"  runat="server">
                                <Items>
                                    <x:Label ID="Label1" Width="200px" runat="server" CssClass="inline" ShowLabel="false" Text="Nodo" />   
                                    <x:DropDownList ID="ddlNodeId"  Width="400" runat="server"  Label="Software Component" Resizable="True" TabIndex="1"></x:DropDownList>                                      
                                    <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click" ></x:Button>                                 
                                </Items>
                            </x:Panel> 
                        </Items> 
                    </x:SimpleForm>
                </Items>
            </x:GroupPanel>
          <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" 
            PageSize="15" EnableRowNumber="True" AllowPaging="true"
            IsDatabasePaging="true" EnableRowNumberPaging="true"  RowNumberWidth="40" AjaxLoadingType="Default"
            EnableMouseOverColor="true" ShowGridHeader="true" 
            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" >             
                <Columns>         
                    <x:boundfield Width="150px" DataField="v_NodeName" DataFormatString="{0}" HeaderText="Node" />
                    <x:boundfield Width="150px" DataField="v_DataSyncVersion" DataFormatString="{0}" HeaderText="Data Sync Version" /> 
                    <x:boundfield Width="250px" DataField="i_DataSyncFrecuency" DataFormatString="{0}" HeaderText="Data Sync Frecuency" />
                    <x:boundfield Width="250px" DataField="i_Enabled" DataFormatString="{0}" HeaderText="Enabled" />
                    <x:boundfield Width="250px" DataField="d_LastSuccessfulDataSync" DataFormatString="{0}" HeaderText="Last Success fulDataSync" />
                    <x:boundfield Width="250px" DataField="i_LastServerProcessStatus" DataFormatString="{0}" HeaderText="Last Server Process Status" />
                    <x:boundfield Width="250px" DataField="i_LastNodeProcessStatus" DataFormatString="{0}" HeaderText="Last Node Process Status" />
                    <x:boundfield Width="250px" DataField="d_NextDataSync" DataFormatString="{0}" HeaderText="Next Data Sync" />
                    <x:boundfield Width="250px" DataField="v_LastServerPackageFileName" DataFormatString="{0}" HeaderText="Last Server Package File Name" />
                    <x:boundfield Width="100px" DataField="v_LastServerProcessErrorMessage" DataFormatString="{0}" HeaderText="Last Server Process Error Message" />
                    <x:boundfield Width="100px" DataField="v_LastNodeProcessErrorMessage" DataFormatString="{0}" HeaderText="Last Node Process Error Message" />
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />

    </form>
</body>
</html>
