﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRMOrdenReporte.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Configuracion.FRMOrdenReporte" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" Layout="Fit" ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
            <Items>  
               <x:Panel ID="Panel2" Layout="Absolute" runat="server" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <x:Grid ID="grdData" ShowBorder="true" ShowHeader="false"  runat="server"  DataKeyNames="" OnRowDataBound="grdData_RowDataBound"
                            AutoScroll="true" Height="350px" SortDirection="ASC" OnSort="grdData_Sort" AllowSorting="true" >                                         
                                <Columns>                       
                                    <x:boundfield Width="260" DataField="v_ComponenteId" DataFormatString="{0}" HeaderText="ComponenteId" />     
                                    <%--<x:boundfield Width="260" DataField="v_NombreReporte" DataFormatString="{0}" HeaderText="Nombre Reporte" />           --%>                            
                                    <%-- <x:TemplateField HeaderText="Orden" Width="60px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="i_Orden" runat="server" Width="40px" TabIndex='<%# Container.DataItemIndex + 10 %>'
                                                Text='<%# Eval("i_Orden") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </x:TemplateField>       --%>             
                                </Columns>
                        </x:Grid>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
