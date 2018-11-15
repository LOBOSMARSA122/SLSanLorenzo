<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM035.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRM035" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración de Parámetros</title>
    <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
 <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1"/>

     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Administración de Empresas" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
             <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="74" >                
                <Items>
                      <x:SimpleForm  ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server">
                        <Items>                             
                            <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item" ShowBorder="false" EnableBackgroundColor="true" Layout="Column"  runat="server">
                                <Items>
                                    <x:Label ID="Label3" Width="80px" runat="server" CssClass="inline" ShowLabel="false" Text="Tipo Organización:" />    
                                     <x:DropDownList ID="ddlTipoEmpresa" runat="server"  Label="Tipo Organización" Width="140px"></x:DropDownList>    
                                                             
                                    <x:Label ID="Label1" Width="80px" runat="server" CssClass="inline" ShowLabel="false" Text="Razón Social:" />                                    
                                    <x:TextBox ID="txtName1" Width="100px" Label="Valor 1" runat="server" CssClass="mrightmayus" RegexIgnoreCase="True" />

                                    <x:Label ID="Label2" Width="50px" runat="server" CssClass="inline" ShowLabel="false" Text="RUC:" />
                                    <x:NumberBox ID="txtIdentificationNumber1" Width="100px" Label="Parámetro Id" runat="server" NoDecimal="True" NoNegative="True" CssClass="mrightmayus"/>
                                   
                                     <x:Button ID="btnFilter" Text="Filtrar" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" OnClick="btnFilter_Click" ></x:Button>                                 
                                    <x:HiddenField ID="HiddenField1" runat="server"></x:HiddenField>
                                </Items>
                            </x:Panel> 
                        </Items> 
                    </x:SimpleForm>
                </Items>
            </x:GroupPanel>
          <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" 
            PageSize="15" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grdData_PageIndexChange"
            IsDatabasePaging="true" EnableRowNumberPaging="true"  RowNumberWidth="40" AjaxLoadingType="Default"
            EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_OrganizationId" 
            EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5" OnRowCommand="grdData_RowCommand" OnPreRowDataBound="grdData_PreRowDataBound">
                <Toolbars>
                    <x:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <x:Button ID="btnEmpresa" Text="Nuevo Empresa" Icon="Add" runat="server"></x:Button>                           
                        </Items>
                    </x:Toolbar>
                </Toolbars>
                <Columns>     

                    <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winEdit" HeaderText=""
                                Icon="Pencil" ToolTip="Editar Empresa" DataTextFormatString="{0}" 
                                DataIFrameUrlFields="v_OrganizationId" DataIFrameUrlFormatString="FRM035A.aspx?Mode=Edit&v_OrganizationId={0}" 
                                DataWindowTitleField="v_Name" DataWindowTitleFormatString="Editar Empresa {0}" />

                    <x:WindowField ColumnID="myWindowFieldSede" Width="25px" WindowID="winEditSede" HeaderText=""
                                Icon="bullethome" ToolTip="Editar Sede" DataTextFormatString="{0}" 
                                DataIFrameUrlFields="v_OrganizationId,v_Name" DataIFrameUrlFormatString="FRM035B.aspx?Mode=Edit&v_OrganizationId={0}&v_Name={1}" 
                                DataWindowTitleField="v_Name" DataWindowTitleFormatString="Editar Sedes de Empresa {0}" />

                     <x:WindowField ColumnID="myWindowFieldGESO" Width="25px" WindowID="winEditGESO" HeaderText=""
                                Icon="group" ToolTip="Editar Grupo de Riesgo" DataTextFormatString="{0}" 
                                DataIFrameUrlFields="v_OrganizationId,v_Name" DataIFrameUrlFormatString="FRM035C.aspx?Mode=Edit&v_OrganizationId={0}&v_Name={1}" 
                                DataWindowTitleField="v_Name" DataWindowTitleFormatString="Editar GESO de Empresa {0}" />

                      <x:WindowField ColumnID="myWindowOrdenReporte" Width="25px" WindowID="winEditOrdenReporte" HeaderText=""
                                Icon="Outline" ToolTip="Orden de Reporte" DataTextFormatString="{0}" 
                                DataIFrameUrlFields="v_OrganizationId,v_Name" DataIFrameUrlFormatString="FRMOrdenReporte.aspx?Mode=Edit&v_OrganizationId={0}&v_Name={1}" 
                                DataWindowTitleField="v_Name" DataWindowTitleFormatString="Orden de Reporte de la Empresa {0}" />

                    <x:boundfield Width="250px" DataField="v_Name" DataFormatString="{0}" HeaderText="Razón Social" />
                    <x:boundfield Width="90px" DataField="v_IdentificationNumber" DataFormatString="{0}" HeaderText="RUC" />    
                    <x:boundfield Width="100px" DataField="v_CreationUser" DataFormatString="{0}" HeaderText="Usuario Crea." />
                    <x:boundfield Width="100px" DataField="d_CreationDate" DataFormatString="{0}" HeaderText="Fecha Crea" />
                    <x:boundfield Width="100px" DataField="v_UpdateUser" DataFormatString="{0}" HeaderText="Usuario Act." />
                    <x:boundfield Width="100px" DataField="d_UpdateDate" DataFormatString="{0}" HeaderText="Fecha Act." />
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />


      <x:Window ID="winEdit" Title="Nueva Empresa" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close_Empresa" IsModal="True" Width="650px" Height="450px" >
    </x:Window>

       <x:Window ID="winEditSede" Title="Nueva Sede" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEditSede_Close" IsModal="True" Width="450px" Height="370px" >
    </x:Window>

       <x:Window ID="winEditGESO" Title="Nuevo Grupo de Riesgo" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEditGESO_Close" IsModal="True" Width="450px" Height="400px" >
    </x:Window>


     <x:Window ID="winEditOrdenReporte" Title="Orden de Reporte" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEditOrdenReporte_Close" IsModal="True" Width="1000px" Height="400px" >
    </x:Window>
    </form>
</body>
</html>
