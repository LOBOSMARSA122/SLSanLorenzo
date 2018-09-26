<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM028A.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Sync.FRM012A" %>
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
            <x:Panel ID="Panel2" Layout="Fit" runat="server" ShowBorder="false" ShowHeader="false" >
                <Items>
                    <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true" 
                        AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">                        
                        <Items>
                            
                            <x:DropDownList ID="ddlSoftwareComponentId"  runat="server"  Label="Software Component" Resizable="True" TabIndex="1" ShowRedStar="true" CompareType="String" CompareValue="-1" CompareOperator="NotEqual" CompareMessage="Campo requerido"></x:DropDownList> 
                            <x:TextBox ID="txtSoftwareComponentVersion" Label="Software Component Version" runat="server" Required="true"  ShowRedStar="true"  CssClass="mayus" TabIndex="2"/>                                   
                            <x:NumberBox ID="txtDeploymentFileId" Label="Id Deployment File" Required="true" NoDecimal="True" ShowRedStar="true" runat="server" TabIndex="3" DecimalPrecision="0" />
                            <x:DatePicker ID="dpReleaseDate" Label="Release Date" Required="true" ShowRedStar="true" runat="server"  DateFormatString="dd/MM/yyyy"  TabIndex="4" EnableChineseAltFormats="True" />
                            <x:TextBox ID="txtDatabaseVersionRequired" Label="Database Version Required" runat="server" Required="true" ShowRedStar="true"  CssClass="mayus" TabIndex="5"/>
                            <x:TextBox ID="txtReleaseNotes" Label="Release Notes" runat="server"  Required="true"  ShowRedStar="true"   CssClass="mayus"  TabIndex="6"/>
                            <x:TextBox ID="txtAdditionalInformation1" Label="Additional Information 1" runat="server" Required="true"  ShowRedStar="true" CssClass="mayus" TabIndex="7" />
                            <x:TextBox ID="txtAdditionalInformation2" Label="Additional Information 2" runat="server" Required="true" ShowRedStar="true"   CssClass="mayus" TabIndex="8"/>
                            <x:NumberBox ID="txtIsPublished" Label="Is Published" Required="true"  NoDecimal="True" ShowRedStar="true" runat="server" TabIndex="9"/>
                            <x:NumberBox ID="txtIsLastVersion" Label="Is LastVersion" Required="true" NoDecimal="True"  ShowRedStar="true" runat="server" TabIndex="10"/>                           
                        </Items>
                    </x:SimpleForm>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
