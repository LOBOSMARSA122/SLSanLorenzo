<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM002A.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Security.FRM002A" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel5" runat="server" />
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
                                        <x:FormRow  ColumnWidths="300px 390px"  runat="server" >
                                            <Items>
                                                <x:TextBox ID="txtFirstName" Label="Nombres" Required="true" ShowRedStar="true" runat="server" TabIndex="1" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:DropDownList ID="ddlDocType" Label="Tipo Documento" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server"
                                                    TabIndex="8" OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged" AutoPostBack="true" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow1" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtFirstLastName" Label="Apellido Paterno" Required="true" ShowRedStar="true" runat="server" TabIndex="2" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:TextBox ID="txtDocNumber" Label="Número de Documento" Required="true" ShowRedStar="true" runat="server" TabIndex="9" />                                                                                              
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow2" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtSecondLastName" Label="Apellido Materno" Required="true" runat="server" TabIndex="3" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:DropDownList ID="ddlSexType" Label="Género" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server" TabIndex="10" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow3" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:DropDownList ID="ddlMaritalStatus" Label="Estado Civil" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Campo requerido" runat="server" TabIndex="4" />
                                                <x:DropDownList ID="ddlLevelOfId" Label="Nivel de Estudios" Required="false" runat="server" TabIndex="11" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow4" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtMail" Label="Email" runat="server" RegexPattern="EMAIL"
                                                    TabIndex="5" MaxLength="100" MaxLengthMessage="El número máximo de caracteres permitidos es 100" />
                                                <x:DatePicker ID="dpBirthdate" Label="Fecha de Nacimiento" Required="false" runat="server" TabIndex="12"
                                                    DateFormatString="dd/MM/yyyy" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow5" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtTelephoneNumber" Label="Teléfono" Required="false" runat="server" TabIndex="6" MaxLength="15"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 15" />
                                                <x:TextBox ID="txtBirthPlace" Label="Lugar de Nacimiento" Required="false" runat="server" TabIndex="13" MaxLength="100"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 100" CssClass="mayus" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow runat="server">
                                            <Items>
                                                <x:TextBox ID="txtAdressLocation" Label="Dirección" Required="false" runat="server" TabIndex="7"
                                                    MaxLength="250" MaxLengthMessage="el número máximo de caracteres permitidos es 250" />
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                                </x:Form>
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Datos de Profesional" ID="GroupPane22" Layout="Table" TableConfigColumns="3">
                            <Items>
                                <x:Panel ID="Panel3" Title="Panel1"  Height="128px" EnableBackgroundColor="true"
                                    TableRowspan="2" runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <x:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="145px">
                                            <Rows>
                                                <x:FormRow ID="FormRow6" runat="server">
                                                    <Items>
                                                        <x:DropDownList ID="ddlProfession" Label="Profesión" Required="false" runat="server"
                                                            TabIndex="14" Width="250px" AutoPostBack="true" EnableSimulateTree="true" />
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow7" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtProfessionalCode" Label="El número colegiatura" Required="false" runat="server" TabIndex="15"
                                                            MaxLength="20" MaxLengthMessage="El número máximo de caracteres permitidos es 20" />
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow8" runat="server">
                                                    <Items>
                                                        <x:TextBox ID="txtProfessionalInformation" Label="Información adicional" Required="false" runat="server" TabIndex="16"
                                                            MaxLength="100" MaxLengthMessage="el número máximo de caracteres permitidos es 100" />
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow19" runat="server">
                                                    <Items>
                                                        <x:FileUpload runat="server" ID="filePhoto" EmptyText="Por favor seleccione una foto"
                                                            Label="Imagen de la Rúbrica" ButtonIcon="SystemSearch" OnFileSelected="filePhoto_FileSelected" AutoPostBack="true">
                                                        </x:FileUpload>
                                                    </Items>
                                                </x:FormRow>
                                                <x:FormRow ID="FormRow20" runat="server">
                                                    <Items>
                                                        <x:Label ID="label" runat="server"></x:Label>
                                                        <x:Label ID="label11" runat="server"></x:Label>
                                                        <x:Label ID="label1" runat="server"></x:Label>
                                                        <x:Button ID="btnLimpiar" runat="server" ValidateForms="SimpleForm1" Text="Limpiar Foto" 
                                                            IconAlign="Right" Icon="CameraDelete" OnClick="btnLimpiar_Click">
                                                        </x:Button>
                                                    </Items>
                                                </x:FormRow>
                                            </Rows>
                                        </x:Form>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel4" Title="Panel2"  Height="128px" EnableBackgroundColor="true"
                                    runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <x:Image ID="ImgPhoto" runat="server" ImageCssStyle="border:solid 1px #ccc;padding:5px; align:center;"
                                            BoxConfigPosition="Center">
                                        </x:Image>
                                       
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Datos de Usuario" ID="GroupPanel2" Height="180">
                            <Items>
                                <x:SimpleForm ID="SimpleForm2" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true"
                                    AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True" LabelWidth="145px">
                                    <Items>
                                        <x:TextBox ID="txtUserName" Label="Usuario" runat="server" Required="true" ShowRedStar="true" TabIndex="17"
                                            MaxLength="25" MaxLengthMessage="El número máximo de caracteres permitidos es 25" />
                                        <x:TextBox ID="txtPassword1" Label="Password" runat="server" Required="true" ShowRedStar="true" TabIndex="18" TextMode="Password"
                                            MaxLength="25" MinLength="8" MaxLengthMessage="el número máximo de caracteres permitidos es 25"
                                            MinLengthMessage="El número mínimo de caracteres permitidos es 8" />
                                        <x:TextBox ID="txtPassword2" Label="Repetir Password" runat="server"
                                            CompareType="String" CompareControl="txtPassword1"
                                            CompareOperator="Equal" CompareMessage="Las contraseñas introducidas no coinciden. Vuelve a intentarlo"
                                            Required="true" ShowRedStar="true" TabIndex="19" TextMode="Password"
                                            MaxLength="25" MinLength="8" MaxLengthMessage="El número máximo de caracteres permitidos es 25"
                                            MinLengthMessage="el número mínimo de caracteres permitidos es 8" />
                                        <x:DropDownList ID="ddlRolVenta" Label="Rol Venta" 
                                                   runat="server"  />
                                    </Items>
                                </x:SimpleForm>
                            </Items>
                        </x:GroupPanel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>


        <x:HiddenField ID="hfRefresh" runat="server" />


      
    </form>
</body>
</html>
