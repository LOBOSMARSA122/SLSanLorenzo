<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEpp.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmEpp" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel5" runat="server" />
          <x:Panel ID="Panel5" runat="server" ShowBorder="True" BodyPadding="5px" ShowHeader="False"
            EnableBackgroundColor="True" AutoWidth="true">
               <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cerrar" runat="server" Icon="SystemClose">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
                <Items>
                    <x:Panel ID="Panel2" EnableBackgroundColor="true"
                        runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                        <Items>
                            <x:GroupPanel runat="server" Title="Uso de E.P.P" ID="GroupPanel3" EnableBackgroundColor="True">
                                <Items>
                                     <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False"
                                        LabelWidth="80px">
                                         <Rows>
                                             <x:FormRow ID="FormRow1" runat="server">
                                                <Items>
                                                    <x:DropDownList ID="ddlEpps" Label="Epps" Required="false" runat="server" 
                                                        AutoPostBack="true" 
                                                        ShowRedStar="true" CompareType="String" CompareValue="-1" Width="545px"
                                                        CompareOperator="NotEqual" CompareMessage="Por favor seleccione un Item." EnableSimulateTree="true" />
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow2" runat="server" ColumnWidths="270px 60px 30px" >
                                                <Items>
                                                    <x:Label runat="server" ID="Label6" ShowLabel="false" Text="Porcentaje de uso durante la jornada laboral"></x:Label>
                                                    <x:NumberBox ID="txtPorcentaje11" runat="server" ShowLabel="false" Width="50" MinValue="0" MaxValue="100" ></x:NumberBox>  
                                                    <x:Label runat="server" ID="Label1" ShowLabel="false" Text="%"></x:Label>                                                  
                                                </Items>
                                            </x:FormRow>
                                            <x:FormRow ID="FormRow3" runat="server" ColumnWidths="90px">
                                                <Items>                                                   
                                                    <x:Button ID="btnAdd" Text="Agregar" runat="server" Icon="Add"
                                                        ValidateForms="Form2" OnClick="btnAdd_Click">
                                                    </x:Button>
                                                </Items>
                                            </x:FormRow>
                                         </Rows>
                                    </x:Form>
                                </Items>
                            </x:GroupPanel>
                            <x:GroupPanel runat="server" Title="Protección" ID="GroupPanel1" EnableBackgroundColor="True" AutoWidth="true">
                                <Items>
                                    <x:Grid ID="grd" ShowBorder="true" ShowHeader="false" runat="server"
                                         EnableRowNumber="True" 
                                       EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                        EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="v_TypeofEEPId"
                                        EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5"
                                        OnRowCommand="grd_RowCommand" Height="240px" Width="690px">
                                        <Columns>
                                            <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                                ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar" CommandName="DeleteRegistro" />
                                            <x:BoundField Width="250px" DataField="v_TypeofEEPName" DataFormatString="{0}" HeaderText="Peligro" />
                                            <x:BoundField Width="170px" DataField="r_Percentage" DataFormatString="{0}" HeaderText="Categoria" />
                                        </Columns>
                                    </x:Grid>
                                </Items>
                            </x:GroupPanel>
                        </Items>
                    </x:Panel>
                </Items>
              </x:Panel>
    </form>
</body>
</html>
