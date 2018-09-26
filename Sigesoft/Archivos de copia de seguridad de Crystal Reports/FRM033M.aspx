<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM033M.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRM033M" %>
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
                    <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="SimpleForm1" TabIndex="7">
                    </x:Button>
                    <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
           <Items>            
            <x:Panel ID="Panel4" Layout="Fit" runat="server" ShowBorder="false" ShowHeader="false" >
                <Items>
                    <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true" 
                        AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">                        
                        <Items>
                            <x:DropDownList ID="ddlConsultorio" runat="server"  Label="Consultorio" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlConsultorio_SelectedIndexChanged"></x:DropDownList>    
                            <x:DropDownList ID="ddlExamen" runat="server"  Label="Examen" Width="140px"></x:DropDownList>     
                                                        
                            <x:TextBox ID="txtFileName" Label="File Name" runat="server" Required="true" ShowRedStar="true"  CssClass="mayus" TabIndex="3"/>
                            <x:FileUpload runat="server" ID="filePhoto" EmptyText="Por favor seleccione un archivo" Label="Archivo" ButtonIcon="SystemSearch" OnFileSelected="btnSubmit_Click"  AutoPostBack="true" ButtonOnly="false">
                            </x:FileUpload>
                            <x:Grid ID="grdData" ShowHeader="false" runat="server" Height="190" OnRowCommand="grdData_RowCommand">
                                <Columns>
                                    <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top" ColumnID="lbfAction2" Width="25px" ToolTip="Eliminar Item" CommandName="DeleteAction" />
                                    <x:boundfield  DataField="FileName" DataFormatString="{0}" HeaderText="File Name" Width="400" />                 
                                </Columns>
                            </x:Grid>
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:Panel>
        </Items>

    </x:Panel>
    </form>
</body>
</html>
