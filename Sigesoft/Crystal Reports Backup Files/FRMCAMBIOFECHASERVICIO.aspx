<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMCAMBIOFECHASERVICIO.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Servicios.FRMCAMBIOFECHASERVICIO" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <x:PageManager ID="PageManager1" runat="server"/>
         <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False" EnableBackgroundColor="True">
        <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" 
                            TabIndex="20">
                        </x:Button>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="21">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
              <Items>
                 <x:Panel ID="Panel4" Layout="Fit" runat="server" ShowBorder="false" ShowHeader="false" >
                    <Items>
                        <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true" 
                        AutoScroll="true" runat="server" EnableCollapse="True"  LabelWidth="130px" >                        
                            <Items>     
                                 <x:DatePicker ID="dpFechaServicio" Label="Fecha del Servicio" Width="110px" runat="server" DateFormatString="dd/MM/yyyy" />      
                            </Items>
                        </x:SimpleForm>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
