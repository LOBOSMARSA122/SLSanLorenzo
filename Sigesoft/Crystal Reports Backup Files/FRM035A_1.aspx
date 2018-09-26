<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM035A_1.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRM035A_1" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
     <x:Pagemanager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
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
                 <x:Panel ID="Panel4" Layout="Table" runat="server" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true" TableConfigColumns="2" Width="630px" >
                    <Items>   
                        <x:GroupPanel runat="server" Title="Búsqueda / Filtro" ID="GroupPanel1" AutoWidth="true" Width="590px" BoxFlex="1" Height="360px" TableRowspan="2" >                
                            <Items>
                                <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                    <Rows>
                                       
                                          <x:FormRow ID="FormRow2" ColumnWidths="380px 100px" runat="server">
                                            <Items>  
                                                <x:TextBox ID="txtDx" Label="Cod.-Nom." Width="250px" runat="server"></x:TextBox> 
                                                <x:Button ID="btnCie10" Text="Buscar CIIU" Icon="Find" IconAlign="Left" runat="server" AjaxLoadingType="Mask" CssClass="inline" ValidateForms="Form2" OnClick="btnCie10_Click" ></x:Button>      
                                            </Items>
                                            </x:FormRow>
                                         <x:FormRow ID="FormRow3" ColumnWidths="380px 100px" runat="server">
                                            <Items>  
                                                <x:Grid ID="grdCie10" ShowBorder="true" ShowHeader="false" runat="server" 
                                                    PageSize="9" EnableRowNumber="True"  AllowPaging="true" OnPageIndexChange="grdCie10_PageIndexChange"
                                                    IsDatabasePaging="true" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                                    EnableMouseOverColor="true" ShowGridHeader="true"  DataKeyNames="v_CIIUId,v_Name,v_CodigoEmpresaId"
                                                     EnableTextSelection="true"  EnableAlternateRowColor="true" BoxFlex="2"  BoxMargin="5" 
                                                      Height="245px"  OnRowClick="grdCie10_RowClick" EnableRowClick="true">                                               
                                                    <Columns>
                                                        <x:boundfield Width="55px" DataField="v_CIIUId" DataFormatString="{0}" HeaderText="CIIU" />   
                                                        <x:boundfield Width="350px" DataField="v_Name" DataFormatString="{0}" HeaderText="Sector" />
                                                        <x:boundfield Width="115px" DataField="v_CodigoEmpresaId" DataFormatString="{0}" HeaderText="Código Interno" />                                                                                                        
                                                    </Columns>
                                                </x:Grid>
                                            </Items>
                                        </x:FormRow>
                                          <x:FormRow ID="FormRow4" ColumnWidths="480" runat="server">
                                                <Items>  
                                                     <x:TextBox ID="txtDxModificado" Label="Sector" Width="450px" runat="server"></x:TextBox> 
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

        
    </form>
</body>
</html>
