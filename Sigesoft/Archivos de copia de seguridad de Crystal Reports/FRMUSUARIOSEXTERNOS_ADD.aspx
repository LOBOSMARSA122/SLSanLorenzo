<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMUSUARIOSEXTERNOS_ADD.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRMUSUARIOSEXTERNOS_ADD" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                                        <x:FormRow ID="FormRow1"  ColumnWidths="300px 390px"  runat="server" >
                                            <Items>
                                                <x:TextBox ID="txtFirstName" Label="Nombres" Required="true" ShowRedStar="true" runat="server" TabIndex="1" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:DropDownList ID="ddlDocType" Label="Tipo Documento"  runat="server" TabIndex="2" AutoPostBack="true" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow2" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtFirstLastName" Label="Apellido Paterno" runat="server" TabIndex="3" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:TextBox ID="txtDocNumber" Label="Número de Documento"  runat="server" TabIndex="4" />                                                                                              
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow3" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtSecondLastName" Label="Apellido Materno"  runat="server" TabIndex="5" MaxLength="50"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 50" />
                                                <x:DropDownList ID="ddlSexType" Label="Género" runat="server" TabIndex="6" />
                                            </Items>
                                        </x:FormRow>                                       
                                        <x:FormRow ID="FormRow5" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtMail" Label="Email" runat="server" RegexPattern="EMAIL" Required="true" ShowRedStar="true"
                                                    TabIndex="7" MaxLength="100" MaxLengthMessage="El número máximo de caracteres permitidos es 100" />
                                                <x:DatePicker ID="dpBirthdate" Label="Fecha de Nacimiento" Required="false" runat="server" TabIndex="8"
                                                    DateFormatString="dd/MM/yyyy" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow6" ColumnWidths="300px 390px" runat="server">
                                            <Items>
                                                <x:TextBox ID="txtTelephoneNumber" Label="Teléfono" Required="false" runat="server" TabIndex="9" MaxLength="15"
                                                    MaxLengthMessage="El número máximo de caracteres permitidos es 15" />
                                                <x:TextBox ID="txtBirthPlace" Label="Lugar de Nacimiento"  runat="server" TabIndex="10" MaxLength="100" CssClass="mayus" />
                                            </Items>
                                        </x:FormRow>                                       
                                    </Rows>
                                </x:Form>
                            </Items>
                        </x:GroupPanel>                  
                        <x:GroupPanel runat="server" Title="Datos de Usuario" ID="GroupPanel2" Height="250">
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
                                         <x:DropDownList ID="ddlEmpresaCliente" runat="server"  Label="Emp." ></x:DropDownList>
                                        <x:Button ID="btnAgregarEmpresa" runat="server" Text="Agregar Empresa" OnClick="btnAgregarEmpresa_Click"></x:Button>  
                                        <x:Grid ID="grdData" ShowBorder="false" ShowHeader="false" Title="Empresas" runat="server"  
                                             EnableRowNumber="True" OnRowCommand="grdData_RowCommand"
                                            IsDatabasePaging="true" EnableRowNumberPaging="true" AutoHeight="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                            EnableMouseOverColor="true" ShowGridHeader="true" 
                                            EnableTextSelection="true" EnableAlternateRowColor="true" Height="80px">    
                                            <Columns>
                                                <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                                ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar Hijo" CommandName="DeleteAction" />
                                                <x:boundfield Width="90px" DataField="Id" DataFormatString="{0}" HeaderText="Id" />  
                                                <x:boundfield Width="250px" DataField="Value1" DataFormatString="{0}" HeaderText="Empresa" />   
                                            </Columns>
                                        </x:Grid>                                                              
                                    </Items>
                                </x:SimpleForm>
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Permisos de Usuario" ID="GroupPanel1" Height="100">
                            <Items>                         
                                <x:Form ID="Form267aa" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="20px" LabelAlign="Left" >
                                    <Rows>
                                        <x:FormRow ID="FormRow706aa" ColumnWidths="120px 30px  110px 30px  100px 30px   100px 30px   110px 20px " runat="server" >
                                            <Items>
                                                <x:Label ID="label1a" runat="server" Text="Adm. de Servicios" ShowLabel="false"></x:Label>
                                                <x:CheckBox ID="chkAdminServicios" runat="server" Label="" ShowLabel="false"></x:CheckBox>

                                                <x:Label ID="label1" runat="server" Text="Cert. de Aptitud" ShowLabel="false"></x:Label>
                                                <x:CheckBox ID="chkCertificado" runat="server" Label="" ShowLabel="false"></x:CheckBox>

                                                 <x:Label ID="label2" runat="server" Text="Examenes" ShowLabel="false"></x:Label>
                                                <x:CheckBox ID="chkExamenes" runat="server" Label="" ShowLabel="false"></x:CheckBox>

                                                 <x:Label ID="label3" runat="server" Text="F. Ocupacional" ShowLabel="false"></x:Label>
                                                <x:CheckBox ID="chkFichaOcupacional" runat="server" Label="" ShowLabel="false"></x:CheckBox>

                                                 <x:Label ID="label4" runat="server" Text="Seg. Trabajador" ShowLabel="false"></x:Label>
                                                <x:CheckBox ID="chkSegTrabajador" runat="server" Label="" ShowLabel="false"></x:CheckBox>                                                
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow4" ColumnWidths="120px 30px  110px 30px" runat="server" >
                                            <Items>
                                                <x:Label ID="label5" runat="server" Text="Agendado" ShowLabel="false"></x:Label>
                                                <x:CheckBox ID="chkAgenda" runat="server" Label="" ShowLabel="false"></x:CheckBox>

                                                <x:Label ID="label6" runat="server" Text="Estadísticas" ShowLabel="false"></x:Label>
                                                <x:CheckBox ID="chkEstadistica" runat="server" Label="" ShowLabel="false"></x:CheckBox>
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                                </x:Form>                                
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
