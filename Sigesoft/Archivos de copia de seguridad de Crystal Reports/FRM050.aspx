<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM050.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.CargaData.FRM050" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carga Masiva de Agenda</title>
      <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1"/>
           <x:Panel ID="Panel1" runat="server"  ShowBorder="True" ShowHeader="True" Title="Agenda" EnableBackgroundColor="true" Layout="VBox" 
          BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigChildMargin="3 7 12 5" >
        <Items>
             <x:GroupPanel runat="server" Title="Datos de Agendado Masivo" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="74" >                
                <Items>
                      <x:SimpleForm  ID="frmFiltro" ShowBorder="False" EnableBackgroundColor="True" ShowHeader="False" runat="server">
                        <Items>                             
                            <x:Panel ID="Panel133" ShowHeader="false" CssClass="x-form-item" ShowBorder="false" EnableBackgroundColor="true" Layout="Column"  runat="server">
                                <Items>
                                    <x:Label ID="Label1" Width="100px" runat="server" CssClass="inline" ShowLabel="false" Text="Fecha Agenda:" />
                                    <x:DatePicker ID="dpFechaInicio"  Width="150px" runat="server"  DateFormatString="dd/MM/yyyy"  Required="true" />     
                                    <x:Label ID="Label2" Width="100px" runat="server" CssClass="inline" ShowLabel="false" Text="Empresa Cliente" />
                                    <x:DropDownList ID="ddlEmpresaCliente" runat="server" ShowLabel="false" Enabled="false" Width="250"></x:DropDownList> 
                                    <x:Label ID="Label3" Width="100px" runat="server" CssClass="inline" ShowLabel="false" Text="Protocolo:" />
                                    <x:DropDownList ID="ddlProtocoloId" runat="server"  Label="Protocolo" Width="340px" AutoPostBack="true" OnSelectedIndexChanged="ddlProtocoloId_SelectedIndexChanged" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido"></x:DropDownList>     
                                    <x:Button ID="btnSubir" Text="Grabar Agenda" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline"  OnClick="btnSubir_Click"  Hidden="true" ></x:Button>                                                                  
                                </Items>
                            </x:Panel> 
                        </Items> 
                    </x:SimpleForm>
                </Items>
            </x:GroupPanel>
          <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false" runat="server"  EnableCheckBoxSelect="false"
            EnableRowNumber="True" EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5"  OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">    
                  <Toolbars>
                    <x:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                              <x:FileUpload runat="server" ID="fileDoc" EmptyText="Por favor seleccione un archivo" Width="300" Height="25"
                                        Label="Seleccionar Excel" ButtonIcon="SystemSearch" OnFileSelected="fileDoc_FileSelected" AutoPostBack="true" ButtonText="Seleccionar" Readonly="False">
                                </x:FileUpload>
                            <x:Button ID="btnAgregar" runat="server" Text="Agendar un solo trabajador" Icon="Add"  ValidateForms="frmFiltro"></x:Button>
                            <x:Button ID="btnDescargarPlantilla" runat="server" Text="Descargar Plantilla" Icon="PageExcel"></x:Button>
                        </Items>
                    </x:Toolbar>
                </Toolbars>   
                <Columns>   
                    <x:boundfield Width="100px" DataField="v_FirstName" DataFormatString="{0}" HeaderText="Nombres" />
                    <x:boundfield Width="100px" DataField="v_FirstLastName" DataFormatString="{0}" HeaderText="Apellido Parteno" />          
                    <x:boundfield Width="100px" DataField="v_SecondLastName" DataFormatString="{0}" HeaderText="Apellido Materno" />
                    <x:boundfield Width="100px" DataField="v_SexTypeName" DataFormatString="{0}" HeaderText="Género." />
                    <x:boundfield Width="100px" DataField="v_DocTypeName" DataFormatString="{0}" HeaderText="Documento" />
                    <x:boundfield Width="100px" DataField="v_DocNumber" DataFormatString="{0}" HeaderText="Nro. Documento" />
                    <x:boundfield Width="100px" DataField="d_Birthdate" DataFormatString="{0:yy-MM-dd}" HeaderText="Fecha Nacimiento" />
                    <x:boundfield Width="1px" DataField="i_DocTypeId" DataFormatString="{0}" HeaderText="DocumentoID" />
                    <x:boundfield Width="1px" DataField="i_SexTypeId" DataFormatString="{0}" HeaderText="GéneroID" />
                    <x:boundfield Width="100px" DataField="v_CurrentOccupation" DataFormatString="{0}" HeaderText="Puesto" />
                    <x:boundfield Width="300px" DataField="v_ProtocoloId" DataFormatString="{0}" HeaderText="Código Protocolo" />
                </Columns>
            </x:Grid>
        </Items>
    </x:Panel>
    
    <x:HiddenField ID="hfRefresh" runat="server" />
    <x:Window ID="winEdit" Title="Nueva Agenda Personal" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="450px" Height="370px" >
    </x:Window>

    <x:Window ID="Window1" Title="Descargar Plantilla" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="Window1_Close" IsModal="True" Width="450px" Height="370px" >
    </x:Window>
    </form>
</body>
</html>
