<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMPACIENTE.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.FRMPACIENTE" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" runat="server"/>
  <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False" EnableBackgroundColor="True">
            <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="Form2,SimpleForm2"
                            TabIndex="20">
                        </x:Button>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="21">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel6" AutoHeight="true" EnableBackgroundColor="True"
                    runat="server" BodyPadding="2px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:GroupPanel runat="server" Title="Datos Generales" ID="GroupPanel4">
                            <Items>                                
                                <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False"
                                    LabelWidth="145px">
                                    <Rows>
                                        <x:FormRow ID="FormRow1"  ColumnWidths="300px 390px"  runat="server" >
                                            <Items>
                                                <x:TextBox ID="txtFirstName" Label="Nombres" Required="true" ShowRedStar="true" runat="server" TabIndex="1" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:DropDownList ID="ddlDocType" Label="Tipo Documento" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server"
                                                    TabIndex="8" OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged" AutoPostBack="true" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow2" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtFirstLastName" Label="Apellido Paterno" Required="true" ShowRedStar="true" runat="server" TabIndex="2" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:TextBox ID="txtDocNumber" Label="Número de Documento" Required="true" ShowRedStar="true" runat="server" TabIndex="9" />                                                                                              
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow3" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtSecondLastName" Label="Apellido Materno" Required="true" runat="server" TabIndex="3" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:DropDownList ID="ddlSexType" Label="Género" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server" TabIndex="10" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow4" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:DropDownList ID="ddlMaritalStatus" Label="Estado Civil" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server" TabIndex="4" />
                                                <x:DropDownList ID="ddlLevelOfId" Label="Nivel de Estudios" Required="false" runat="server" TabIndex="11" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow5" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtMail" Label="Email" runat="server" RegexPattern="EMAIL"
                                                    TabIndex="5" MaxLength="100" MaxLengthMessage="El número máximo de caracteres permitidos es 100" />
                                                <x:DatePicker ID="dpBirthdate" Label="Fecha de Nacimiento" Required="false" runat="server" TabIndex="12"
                                                    DateFormatString="dd/MM/yyyy" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow6" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtTelephoneNumber" Label="Teléfono" Required="false" runat="server" TabIndex="6" MaxLength="15"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 15" />
                                                <x:TextBox ID="txtBirthPlace" Label="Lugar de Nacimiento" Required="false" runat="server" TabIndex="13" MaxLength="100"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 100" CssClass="mayus" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow7" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtAdressLocation" Label="Dirección" Required="false" runat="server" TabIndex="7"
                                                    MaxLength="250" MaxLengthMessage="el número máximo de caracteres permitidos es 250" />
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                                </x:Form>
                         
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Foto - Huella - Firma" ID="GroupPane22" Layout="Table" TableConfigColumns="3">
                            <Items>
                            <x:Panel ID="Panel4" Title="Panel2"  Height="168px" EnableBackgroundColor="true"
                                    runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <x:Image ID="ImgPhoto" runat="server" ImageCssStyle="border:solid 1px #ccc;padding:5px; align:center;"
                                            BoxConfigPosition="Center">
                                        </x:Image>                                       
                                    </Items>
                                </x:Panel>
                            <x:Panel ID="Panel1" Title="Panel2"  Height="168px" EnableBackgroundColor="true"
                                    runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <x:Image ID="Image1" runat="server" ImageCssStyle="border:solid 1px #ccc;padding:5px; align:center;"
                                            BoxConfigPosition="Center">
                                        </x:Image>
                                       
                                    </Items>
                                </x:Panel>
                         
                            <x:Panel ID="Panel2" Title="Panel2"  Height="168px" EnableBackgroundColor="true"
                                    runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <x:Image ID="Image2" runat="server" ImageCssStyle="border:solid 1px #ccc;padding:5px; align:center;"
                                            BoxConfigPosition="Center">
                                        </x:Image>
                                       
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:GroupPanel>                 
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
   
    </form>
</body>
</html>
