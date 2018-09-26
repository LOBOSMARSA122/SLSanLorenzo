<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM036A.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRM036A" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../CSS/main.css" rel="stylesheet" />
     <style type="text/css">
        .highlight
        {
            background-color: lightgreen;
        }
        .highlight .x-grid3-col
        {
            background-image: none;
        }
        
        .x-grid3-row-selected .highlight
        {
            background-color: yellow;
        }
        .x-grid3-row-selected .highlight .x-grid3-col
        {
            background-image: none;
        }
    </style>
</head>
<body>
          <form id="form2" runat="server">
   <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <x:Panel ID="Panel1" runat="server"  ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
        <Toolbars>
            <x:Toolbar ID="Toolbar1" runat="server">
                <Items>          
                      <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="Form3"
                            TabIndex="10"> </x:Button>
                    <x:Button ID="btnClose" EnablePostBack="false"  Text="Cerrar" runat="server" Icon="SystemClose" TabIndex="11">
                    </x:Button>
                </Items>
            </x:Toolbar>
        </Toolbars>
        <Items>      
            <x:Panel ID="Panel2" runat="server" Layout="Table" TableConfigColumns="2" ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
                <Items>
                   <x:GroupPanel runat="server" Title="Datos del Protocolo" ID="GroupPanel1" Width="820" BoxFlex="1" Height="170" >       
                      <Items>
                            <x:Form ID="Form3"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="100px" LabelAlign="Left">
                                <Rows>
                                    <x:FormRow ID="FormRow2" ColumnWidths="500px 280px" runat="server" >
                                        <Items>
                                          <x:TextBox  runat="server" Label="Protocolo" Text="" Width="380px" ID="txtProtocolName" Required="true" ShowRedStar="true" TabIndex="1"></x:TextBox>     
                                           <x:DropDownList ID="cbGeso" runat="server"  Label="Grupo Riesgo" Width="170"  ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" TabIndex="2"></x:DropDownList>                                         
                                        </Items>
                                    </x:FormRow>
                                    <x:FormRow ID="FormRow1" ColumnWidths="500px 280px" runat="server" >
                                        <Items>
                                            <x:DropDownList ID="cbOrganizationInvoice" runat="server"  Label="Emp. Cliente" Width="380"  OnSelectedIndexChanged="cbOrganizationInvoice_SelectedIndexChanged" ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" TabIndex="3" AutoPostBack="true"></x:DropDownList>
                                            <x:DropDownList ID="cbServiceType" runat="server"  Label="Tipo Servicio" Width="170" OnSelectedIndexChanged="cbServiceType_SelectedIndexChanged" AutoPostBack="true"  ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" TabIndex="4"></x:DropDownList>                           
                                        </Items>
                                    </x:FormRow>
                                    <x:FormRow ID="FormRow3" ColumnWidths="500px 280px" runat="server" >
                                        <Items>
                                            <x:DropDownList ID="cbOrganization" runat="server"  Label="Emp. Emplea." Width="380"  AutoPostBack="true" ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" TabIndex="5"></x:DropDownList>
                                            <x:DropDownList ID="cbService" runat="server"  Label="Servicio" Width="170" ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" TabIndex="6"></x:DropDownList>                           
                                        </Items>
                                    </x:FormRow>
                                     <x:FormRow ID="FormRow4" ColumnWidths="500px 280px" runat="server" >
                                        <Items>
                                            <x:DropDownList ID="cbIntermediaryOrganization" runat="server"  Label="Emp. Trabajo" Width="380"  ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" TabIndex="7"></x:DropDownList>
                                            <x:TextBox  runat="server" Label="C/Costo" Text="" Width="170" ID="txtCostCenter" TabIndex="8"></x:TextBox>                                                                    
                                        </Items>
                                    </x:FormRow>
                                     <x:FormRow ID="FormRow5" ColumnWidths="500px 280px" runat="server" >
                                        <Items>
                                            <x:DropDownList ID="cbEsoType" runat="server"  Label="Tipo Examen" Width="380" ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" TabIndex="9"></x:DropDownList>
                                            <x:Label  runat="server" Label="" Text="" Width="170" ID="TextBox1"></x:Label>                                                                    
                                        </Items>
                                    </x:FormRow>
                                </Rows>
                            </x:Form>                  
                      </Items>   
            </x:GroupPanel>
                </Items>
            </x:Panel>
         
             <x:GroupPanel runat="server" Title="Examenes del Protocolo" ID="GroupPanel2" Width="820" BoxFlex="1" Height="390" >
                 <Items>
                       <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false"  runat="server"  DataKeyNames="v_ProtocolComponentId" OnRowDataBound="grdData_RowDataBound"
                           AutoScroll="true" Height="350px" SortDirection="ASC" OnSort="grdData_Sort" AllowSorting="true" >                                         
                                        <Columns>           
                                            <x:CheckBoxField ColumnID="CheckBoxField2" Width="30px" RenderAsStaticField="false"
                                                CommandName="CheckBox1" DataField="AtSchool" HeaderText="" SortField="AtSchool" />    
                                                                        
                                            <x:boundfield Width="260" DataField="v_Name" DataFormatString="{0}" HeaderText="Examen" />
                                            <x:boundfield Width="120" DataField="v_CategoryName" DataFormatString="{0:d}" HeaderText="Categoria" SortField="v_CategoryName,v_Name" />                                           
                                            <x:boundfield Width="50" DataField="r_BasePrice" DataFormatString="{0}" HeaderText="P.Base" />    
                                            <x:TemplateField HeaderText="P.Real" Width="60px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="r_Price" runat="server" Width="40px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("r_Price") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>                                            
                                             <x:boundfield Width="80" DataField="v_ComponentId" DataFormatString="{0}" HeaderText="v_ComponentId" Hidden="true" />    
                                           
                                             <x:CheckBoxField ColumnID="CheckBoxField3" Width="30px" RenderAsStaticField="false"
                                               CommandName="CheckBox2" DataField="Adicional" HeaderText="A" />    
                                           
                                             <x:CheckBoxField ColumnID="CheckBoxField4" Width="30px" RenderAsStaticField="false"
                                                 CommandName="CheckBox3" DataField="Condicional" HeaderText="C" />
                                            
                                            <x:TemplateField Width="70px" HeaderText="Operador" >
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlOperador" AutoPostBack="false">
                                                        <asp:ListItem Text="Selecc" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="X!= A" Value="2" ></asp:ListItem>
                                                        <asp:ListItem Text="X < A" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="X <= A" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="X = A" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="X > A" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="X >= A" Value="6"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </x:TemplateField>    
                                            
                                            <x:TemplateField HeaderText="Valor" Width="50px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="i_Age" runat="server" Width="30px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                        Text='<%# Eval("i_Age") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </x:TemplateField>  
                                             
                                            <x:TemplateField Width="70px" HeaderText="Género" >
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlGender" AutoPostBack="false" >
                                                        <asp:ListItem Text="Ambos" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Femenino" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Masculino" Value="1"></asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </x:TemplateField>                                      
                                        </Columns>
                        </x:Grid>
                 </Items>
               
                        
             </x:GroupPanel>



        </Items>
    </x:Panel>

        <x:HiddenField ID="hfRefresh" runat="server" />
        <x:HiddenField ID="highlightRows" runat="server">
        </x:HiddenField>
    </form>
    <script type="text/javascript">
        var highlightRowsClientID = '<%= highlightRows.ClientID %>';
        var gridClientID = '<%= grdData.ClientID %>';

        function highlightRows() {
            var highlightRows = X(highlightRowsClientID);
            var grid = X(gridClientID);

            grid.el.select('.x-grid3-row table.highlight').removeClass('highlight');

            Ext.each(highlightRows.getValue().split(','), function (item, index) {
                if (item !== '') {
                    var row = grid.getView().getRow(parseInt(item, 10));
                    Ext.get(row).first().addClass('highlight');
                }
            });

        }


        function onReady() {
            var grid = X(gridClientID);
            grid.addListener('viewready', function () {
                highlightRows();
            });
        }


        function onAjaxReady() {
            highlightRows();
        }
    </script>
</body>
</html>
