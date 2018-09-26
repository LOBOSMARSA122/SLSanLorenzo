<%@ Page Language="C#" 
 AutoEventWireup="true"
 CodeBehind="FRM009.aspx.cs" 
 Inherits="Sigesoft.Server.WebClientAdmin.UI.FRM009" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta de Logs</title>
    <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
  <form id="form1" runat="server">      
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1"/>
     <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Consulta de Logs" BodyPadding="5px" EnableBackgroundColor="true"  Layout="VBox" 
            BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 3 5" >
        <Items>
            <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="100">
                <Items>
                    <x:SimpleForm  ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server">
                        <Items>
                             <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item datecontainer" ShowBorder="false" EnableBackgroundColor="true" Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label1" Width="50px" runat="server" CssClass="inline" ShowLabel="false" Text="Nodo:" />
                                    <x:DropDownList ID="ddlNodeId" runat="server"  Width="140" TabIndex="1"></x:DropDownList>
                                    <x:Label ID="Label6" Width="40px" runat="server" CssClass="inline" ShowLabel="false" Text="Tipo:" />
                                    <x:DropDownList ID="ddlEventTypeId" runat="server"  Width="140" TabIndex="3"></x:DropDownList>     
                                    <x:Label ID="Label7" Width="50px" runat="server" CssClass="inline" ShowLabel="false" Text="Satisf.:" />
                                    <x:DropDownList ID="ddlSuccess" runat="server" Width="70" TabIndex="4"></x:DropDownList>                          
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel2" ShowHeader="false" CssClass="x-form-item" ShowBorder="false" EnableBackgroundColor="true" Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label2" Width="50px" runat="server" CssClass="inline" ShowLabel="false" Text="Usuario:" />
                                    <x:TextBox ID="txtUserName"  runat="server"  Width="145" CssClass="mrightmayus" TabIndex="2"/>                                   
                                    <x:Label ID="Label3" Width="80px" runat="server" CssClass="inline" ShowLabel="false" Text="Proceso:" />
                                    <x:TextBox ID="txtProcessEntity" runat="server" CssClass="mrightmayus" Width="145" TabIndex="4"></x:TextBox>
                                    <x:Label ID="Label5" Width="42px" runat="server" CssClass="inline" ShowLabel="false" Text="Item:" />
                                    <x:TextBox ID="txtElementItem" runat="server" Width="145" TabIndex="5" CssClass="mright"></x:TextBox>
                                    <x:Button ID="btnFilter" Text="Filtrar" Icon="Find"  runat="server" OnClick="btnFilter_Click" TabIndex="6" ValidateForms="frmFiltro" ></x:Button>  
                                </Items>
                            </x:Panel>
                        </Items>
                    </x:SimpleForm>                    
                </Items>
            </x:GroupPanel>
            <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server" AutoWidth="true"  AutoScroll="true"
            PageSize="30" EnableRowNumber="True"  AllowPaging="true" OnPageIndexChange="grdData_PageIndexChange" IsDatabasePaging="true"
             AutoHeight="true" AjaxLoadingType="Default"
            EnableMouseOverColor="true" ShowGridHeader="true" 
             EnableAlternateRowColor="true" DataKeyNames="v_LogId" BoxFlex="2" BoxMargin="5">
                <Columns>
                    <x:WindowField ColumnID="myWindowField" Width="25px" WindowID="winView" HeaderText=""
                        Icon="Eye" ToolTip="Ver Detalle" DataTextFormatString="{0}" 
                        DataIFrameUrlFields="v_LogId" DataIFrameUrlFormatString="FRM009A.aspx?v_LogId={0}" 
                        DataWindowTitleField="v_LogId" DataWindowTitleFormatString="Ver Log" />
                    <x:boundfield Width="140px" DataField="d_Date"  HeaderText="Fecha" />    
                    <x:boundfield Width="110px" DataField="v_NodeName"  HeaderText="Nodo" />        
                    <x:boundfield Width="30px" DataField="v_LogId" HeaderText="Id" />  
                    <x:boundfield Width="100px" DataField="v_SystemUserName"  HeaderText="Usuario" />                           
                    <x:boundfield Width="140px" DataField="v_EventTypeName"  HeaderText="Tipo de Evento" />  
                    <x:boundfield Width="130px" DataField="v_ProcessEntity"  HeaderText="Proceso / Entidad" />                             
                    <x:boundfield Width="250px" DataField="v_ElementItem"  HeaderText="Elemento / Item" />         
                    <x:boundfield Width="70px" DataField="v_SuccessName"  HeaderText="Satisfactorio" />                             
                    <x:boundfield Width="250px" DataField="v_OrganizationName"  HeaderText="Organización" />
                    <x:boundfield Width="500px" DataField="v_ErrorException"  HeaderText="Error / Excepción" />                                                                               
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
        <x:Window ID="winView" Title="Log" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="500px" Height="400px" >
    </x:Window>
    </form>
</body>
</html>
