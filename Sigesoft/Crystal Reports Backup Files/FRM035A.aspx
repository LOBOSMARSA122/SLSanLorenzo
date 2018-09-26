<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM035A.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRM035A" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/main.css" rel="stylesheet" />
</head>
<body>
       <form id="form1" runat="server">
     <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <x:Panel ID="Panel1" runat="server" Layout="Fit" ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
        <Toolbars>
            <x:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="Form2" TabIndex="10">
                    </x:Button>
                    <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
        <Items>
            
          <x:Panel ID="Panel6" AutoHeight="true" EnableBackgroundColor="True"
                    runat="server" BodyPadding="2px" ShowBorder="False" ShowHeader="False" TableConfigColumns="2" Layout="Table">
                    <Items>
                         <x:GroupPanel runat="server" Title="Datos Generales" ID="GroupPanel4" Width="390">
                            <Items>
                            <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False"
                                    LabelWidth="95px" >
                                    <Rows>
                                        <x:FormRow ID="FormRow1"   runat="server" >
                                            <Items>
                                               <x:DropDownList ID="ddlTipoEmpresa"  runat="server" Width="150" Label="Tipo Empresa" Resizable="True" EnableSimulateTree="true" TabIndex="0"
                                                    ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" ></x:DropDownList>                                 
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow2" ColumnWidths="200px 100px" runat="server">
                                            <Items>
                                              <x:TextBox ID="txtCIIU" Label="CIIU" Required="true" ShowRedStar="true" runat="server" Readonly="true" Width="120"></x:TextBox>
                                                <x:Button runat="server" ID="btnCIIU" Text="Buscar CIIU" TabIndex="1"></x:Button>                                                                                       
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow3" runat="server">
                                            <Items>
                                                <x:TextArea ID="txtSector" Label="Sector" runat="server" Required="true" ShowRedStar="true"   Readonly="true" CssClass="mayus" Width="253" Height="50" ></x:TextArea>
                                        
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow4" runat="server">
                                            <Items>
                                               <x:NumberBox ID="txtRUC" Label="RUC" Required="true" ShowRedStar="true" runat="server" TabIndex="2" NoDecimal="True" NoNegative="True" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow5" runat="server">
                                            <Items>
                                               <x:TextBox ID="txtRazonSocial" Label="Razón Social" runat="server" Required="true" TabIndex="3" ShowRedStar="true"  CssClass="mayus" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow6" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                              <x:TextBox ID="txtContacto" Label="Contacto" runat="server"  TabIndex="4"   CssClass="mayus" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow7" runat="server">
                                            <Items>
                                               <x:TextBox ID="txtEmail" Label="Email" runat="server"  TabIndex="5"  CssClass="mayus" />
                                            </Items>
                                        </x:FormRow>
                                           <x:FormRow ID="FormRow8" runat="server">
                                            <Items>
                                               <x:TextBox ID="txtDireccion" Label="Dirección" runat="server"  TabIndex="6"   CssClass="mayus" />
                                            </Items>
                                        </x:FormRow>
                                           <x:FormRow ID="FormRow9" runat="server">
                                            <Items>
                                               <x:TextBox ID="txtTelefono" Label="Teléfonos" runat="server"  TabIndex="7"   CssClass="mayus" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow19" runat="server">
                                            <Items>
                                                <x:FileUpload runat="server" ID="filePhoto" EmptyText="Por favor seleccione una Imagen"
                                                    Label="Logo Empresa" ButtonIcon="SystemSearch" OnFileSelected="filePhoto_FileSelected" AutoPostBack="true">
                                                </x:FileUpload>
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow10" runat="server">
                                            <Items>
                                                <x:Button ID="btnLimpiar" runat="server" ValidateForms="SimpleForm1" Text="Limpiar Logo" 
                                                            IconAlign="Right" Icon="CameraDelete" OnClick="btnLimpiar_Click">
                                                </x:Button>
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                                </x:Form>
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Logo Empresa" ID="GroupPanel1" Width="220" Height="345"  >
                            <Items>
                                <x:Panel ID="Panel44" Title="Panel2"  Height="200" EnableBackgroundColor="true"
                                    runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                         <x:Image ID="ImgPhoto" runat="server" ImageCssStyle="border:solid 1px #ccc;padding:5px; align:Left;"
                                            BoxConfigPosition="Left">                                            
                                        </x:Image>     
                                    </Items>
                                </x:Panel>                    
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>
        </Items>
    </x:Panel>
           
            <x:HiddenField ID="hfRefresh" runat="server" />
             <x:Window ID="winEdit" Title="Tabla CIIU" Popup="false" EnableIFrame="true" runat="server" Icon="UserBrown"
        CloseAction="HidePostBack" EnableConfirmOnClose="true" IFrameUrl="about:blank" EnableMaximize="false" EnableResize="false" 
        Target="Top" OnClose="winEdit_Close" IsModal="True" Width="650px" Height="450px" >
    </x:Window>
    </form>
</body>
</html>
