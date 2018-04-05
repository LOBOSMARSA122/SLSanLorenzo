<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin_.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.frmLogin" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>login</title>
    <link href="CSS/main.css" rel="stylesheet" type="text/css" />
</head>
<body>  
  <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" runat="server" />
   <x:Window ID="Window1" runat="server" Title="Acceso al sistema: " IsModal="false" EnableClose="false"
        WindowPosition="GoldenSection" Width="295px">
        <Items>
            <x:Panel runat="server" ID="Panel1" Layout="Container" Title="SALUD">
                <Items>
                     <x:Image runat="server" ID="Logo"  ImageUrl="~/images/logo/logo.jpg"></x:Image>
                </Items>
            </x:Panel>
             <x:GroupPanel runat="server" Title="Información de Aplicación" ID="GroupPanel2" AutoWidth="true" BoxFlex="1" Height="70" >                
               <Items>
                    <x:SimpleForm ID="SimpleForm2" runat="server" ShowBorder="false" BodyPadding="10px"
                        LabelWidth="80px" EnableBackgroundColor="true" ShowHeader="false">
                        <Items>                   
                            <x:TextBox ID="TextBox1" Label="Sede" Required="true" runat="server" Text="SALUD-Lima" Readonly="true">
                            </x:TextBox>
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:GroupPanel>
             <x:GroupPanel runat="server" Title="Información de Usuario" ID="GroupPanel1" AutoWidth="true" BoxFlex="1" Height="124" >                
               <Items>
                    <x:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" BodyPadding="10px"
                        LabelWidth="80px" EnableBackgroundColor="true" ShowHeader="false">
                        <Items>                   
                            <x:TextBox ID="txtUserName" Label="Usuario" Required="true" runat="server" Text="">
                            </x:TextBox>
                            <x:TextBox ID="txtPassword" Label="Contraseña" TextMode="Password" Required="true" runat="server" Text="">
                            </x:TextBox>
                            <x:Button ID="btnLogin" Text="Login" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Top"
                                runat="server" OnClick="btnLogin_Click">
                            </x:Button>
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:GroupPanel>
        </Items>
    </x:Window>
    </form>
</body>
</html>
