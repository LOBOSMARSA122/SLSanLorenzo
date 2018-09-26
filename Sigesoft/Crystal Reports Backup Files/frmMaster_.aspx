<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMaster_.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.frmMaster_" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OMEGANET</title>
     <style>
        .header .title {
            background-image: url(../images/logo/logo2.gif);
            background-position: left 2px;
            background-repeat: no-repeat;
            padding-left: 0px;
            margin-top: 5px;
            margin-left: 5px;
        }

            .header .title a {
                color: #fff;
                font-weight: bold;
                font-size: 25px;
                text-decoration: none;
            }

        .header .version {
            position: absolute;
            right: 200px;
            top: 8px;
        }

            .header .version a {
                color: #fff;
                font-weight: bold;
                font-size: 18px;
            }

        .intro {
            font-family: arial,tahoma,helvetica,sans-serif;
            font-size: 13px;
            line-height: 1.5em;
        }

        .htoolbar {
            background-image: url("../images/logo/logo2.gif");
            background-position: center center;
            background-repeat: no-repeat;
        }

        #logo {
            position: absolute;
            bottom: 0px;
            right: 0px;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            opacity: 0.8;
            z-index: 100000;
        }
        .White
        {
            font-weight: bold;
            color: white;
        }
       
    </style>
      <script language="javascript" type="text/javascript">
          javascript: window.history.forward(1);

          var segundos = 0
          var CONTROLADOR = "Handler1.ashx";
          function MantenSesion() {
              var head = document.getElementsByTagName('head').item(0);
              script = document.createElement('script');
              script.src = CONTROLADOR;
              script.setAttribute('type', 'text/javascript');
              script.defer = true;
              head.appendChild(script);
          }
          setInterval("MantenSesion()", 54000)
      </script> 
</head>
<body>
     <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server">
    </x:PageManager>
    <x:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
        <Regions>
            <x:Region ID="Region1" Margins="0 0 0 0" Height="60px" ShowBorder="false" ShowHeader="false"
                    Position="Top" Layout="Fit" runat="server">
                    <Items>
                        <x:ContentPanel ShowBorder="false" CssClass="header" ShowHeader="false" BodyStyle="background-color:#C0D8EF;"
                            ID="ContentPanel1" runat="server">
                            <table>
                                <tr>
                                    <td class="title">
                                        <x:HyperLink ID="HyperLink3" NavigateUrl="~/frmMaster_.aspx" runat="server" Text="">
                                        </x:HyperLink>
                                    </td>
                                                             
                                </tr>
                                <tr>
                                    <td style="color: white; position: absolute; right: 157px; top: 30px;">
                                        <x:Label ID="lblDescripcion" runat="server" Text="Bienvenido:"></x:Label>
                                    </td>
                                    <td style="position: absolute; right: 93px; top: 30px;">
                                        <asp:LinkButton ID="LinkButton1" CssClass="White" runat="server">(Mi cuenta)</asp:LinkButton>
                                    </td>
                                    <td style="position: absolute; right: 17px; top: 30px;">
                                        <asp:LinkButton ID="btnClose" CssClass="White" runat="server" OnClick="btnClose_Click" >Cerrar sesión</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </x:ContentPanel> 
                    </Items>
                </x:Region>
            <x:Region ID="Region2" Split="true" Width="200px" Margins="0 0 0 0" ShowHeader="false"
                Title="Folder" EnableCollapse="true" Layout="Fit" Position="Left" runat="server">
                <Items>
                    <x:Accordion Width="250px" Height="450px" runat="server" ShowBorder="false" ShowHeader="false"
                        ShowCollapseTool="true">
                        <Panes>
                            <x:AccordionPane runat="server" Title="Menú" IconUrl="~/images/16/1.png" BodyPadding="2px 5px"
                                Layout="Fit" ShowBorder="false">
                                <Items>
                                    <x:Tree runat="server" EnableArrows="true" ShowBorder="false" ShowHeader="false"
                                        AutoScroll="true" ID="treeMenu">
                                    </x:Tree>
                                </Items>
                            </x:AccordionPane>
                           
                        </Panes>
                    </x:Accordion>
                </Items>
            </x:Region>
            <x:Region ID="Region3" ShowHeader="false" EnableIFrame="true" IFrameUrl="~/images/master.png" 
                IFrameName="main" Margins="0 0 0 0" Position="Center" runat="server">
            </x:Region>
        </Regions>
    </x:RegionPanel>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/menu.xml"></asp:XmlDataSource>
    </form>
</body>
</html>
