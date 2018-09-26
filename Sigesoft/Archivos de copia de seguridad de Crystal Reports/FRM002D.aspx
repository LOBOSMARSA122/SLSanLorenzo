<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM002D.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Security.FRM002D" %>

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
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="8">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
                <x:Panel ID="Panel2" EnableBackgroundColor="true"
                    runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="False">
                    <Items>
                        <x:GroupPanel runat="server" Title="Datos Generales" ID="GroupPanel3" EnableBackgroundColor="True">
                            <Items>
                                <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False"
                                    LabelWidth="80px">
                                    <Rows>
                                        <x:FormRow ID="FormRow1" runat="server">
                                            <Items>
                                                <x:DropDownList ID="ddlNode" Label="Nodo" Required="false" runat="server" TabIndex="3"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlNode_SelectedIndexChanged"
                                                    ShowRedStar="true" CompareType="String" CompareValue="-1" Width="485px"
                                                    CompareOperator="NotEqual" CompareMessage="Por favor seleccione un Nodo!" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow2" runat="server">
                                            <Items>
                                                <x:DropDownList ID="ddlRole" Label="Rol" Required="false" runat="server" TabIndex="4"
                                                    Resizable="true" Width="485px"
                                                    ShowRedStar="true" CompareType="String" CompareValue="-1"
                                                    CompareOperator="NotEqual" CompareMessage="Por favor seleccione un Rol!" />
                                            </Items>
                                        </x:FormRow>
                                        <x:FormRow ID="FormRow3" runat="server" ColumnWidths="300">
                                            <Items>
                                                <x:Button ID="btnAdd" Text="Agregar" runat="server" Icon="Add"
                                                    ValidateForms="Form2" TabIndex="7" OnClick="btnAdd_Click">
                                                </x:Button>
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                                </x:Form>                             
                            </Items>
                        </x:GroupPanel>
                        <x:GroupPanel runat="server" Title="Nodo Rol" ID="GroupPanel1" EnableBackgroundColor="True" AutoWidth="true">
                            <Items>
                                <x:Grid ID="grd" ShowBorder="true" ShowHeader="false" runat="server"
                                    PageSize="15" EnableRowNumber="True" AllowPaging="true" OnPageIndexChange="grd_PageIndexChange"
                                    IsDatabasePaging="false" EnableRowNumberPaging="true" RowNumberWidth="40" AjaxLoadingType="Default"
                                    EnableMouseOverColor="true" ShowGridHeader="true" DataKeyNames="i_NodeId,i_RoleId,i_SystemUserId"
                                    EnableTextSelection="true" EnableAlternateRowColor="true" BoxFlex="2" BoxMargin="5"
                                    OnRowCommand="grd_RowCommand" Height="180px" Width="610px">
                                    <Columns>
                                        <x:LinkButtonField TextAlign="Center" ConfirmText="Está seguro de eliminar el item seleccionado?" Icon="Delete" ConfirmTarget="Top"
                                            ColumnID="lbfAction2" Width="30px" ToolTip="Eliminar" CommandName="DeleteAction" />
                                        <x:BoundField Width="150px" DataField="v_NodeName" DataFormatString="{0}" HeaderText="Nodo" />
                                        <x:BoundField Width="390px" DataField="v_RoleName" DataFormatString="{0}" HeaderText="Rol" />
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
