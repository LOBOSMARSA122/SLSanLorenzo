<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM033K.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Auditar.FRM033K" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
           <x:PageManager ID="PageManager1" runat="server" />
         <x:Panel ID="Panel1" Title="Buscar Servicio" Width="1500px" Height="200px" EnableBackgroundColor="true"
                TableRowspan="2" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" BoxConfigAlign="Stretch" AutoScroll="true" Layout="HBox" > 
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
                    <x:GroupPanel runat="server" Title="Zonas Afectadas" ID="GroupPanel1" Width="250" BoxFlex="1" Height="90">                
                        <Items>
                            <x:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow1" ColumnWidths="360px" runat="server">
                                            <Items> 
                                                <x:Form ID="Form3"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="80px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow2" ColumnWidths="100px 100px" runat="server" >
                                                            <Items>
                                                               <x:CheckBox ID="chkSupDer" Label="Sup. Der." runat="server"></x:CheckBox>
                                                               <x:CheckBox ID="chkSupIzq" Label="Sup. Izq." runat="server"></x:CheckBox>                                       
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow12" ColumnWidths="100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chkMedDer" Label="Med. Der." runat="server"></x:CheckBox>
                                                               <x:CheckBox ID="chkMedIzq" Label="Med. Izq." runat="server"></x:CheckBox>                                 
                                                            </Items>
                                                        </x:FormRow>
                                                         <x:FormRow ID="FormRow11" ColumnWidths="100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chkInfDer" Label="Inf. Der." runat="server"></x:CheckBox>
                                                               <x:CheckBox ID="chkInfIzq" Label="Inf. Izq." runat="server"></x:CheckBox>                                 
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>                                                               
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                            </x:Form>
                        </Items>
                    </x:GroupPanel>
                    <x:GroupPanel runat="server" Title="Profusión" ID="GroupPanel2" Width="320" BoxFlex="1" Height="132" >                
                        <Items>
                            <x:Form ID="Form4" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow3" ColumnWidths="400px" runat="server">
                                            <Items> 
                                                <x:Form ID="Form5"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow4" ColumnWidths="100px 100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chk0menos" Label="0/-" runat="server"></x:CheckBox>
                                                               <x:CheckBox ID="chk00" Label="0/0" runat="server"></x:CheckBox>           
                                                                <x:CheckBox ID="chk01" Label="0/1" runat="server"></x:CheckBox>                              
                                                            </Items>
                                                        </x:FormRow>
                                                           <x:FormRow ID="FormRow13" ColumnWidths="100px 100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chk10" Label="1/0" runat="server"></x:CheckBox>
                                                               <x:CheckBox ID="chk11" Label="1/1" runat="server"></x:CheckBox>           
                                                                <x:CheckBox ID="chk12" Label="1/2" runat="server"></x:CheckBox>                              
                                                            </Items>
                                                        </x:FormRow>
                                                         <x:FormRow ID="FormRow14" ColumnWidths="100px 100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chk21" Label="2/1" runat="server"></x:CheckBox>
                                                               <x:CheckBox ID="chk22" Label="2/2" runat="server"></x:CheckBox>           
                                                                <x:CheckBox ID="chk23" Label="2/3" runat="server"></x:CheckBox>                              
                                                            </Items>
                                                        </x:FormRow>
                                                         <x:FormRow ID="FormRow15" ColumnWidths="100px 100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chk32" Label="3/2" runat="server"></x:CheckBox>
                                                               <x:CheckBox ID="chk33" Label="3/3" runat="server"></x:CheckBox>           
                                                                <x:CheckBox ID="chk3mas" Label="3/+" runat="server"></x:CheckBox>                              
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>                  
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                            </x:Form>
                        </Items>
                    </x:GroupPanel>
                      <x:GroupPanel runat="server" Title="Forma y Tamaño" ID="GroupPanel3" Width="450" BoxFlex="1" Height="132" >                
                        <Items>
                            <x:Form ID="Form6" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow5" ColumnWidths="300px" runat="server">
                                            <Items> 
                                                <x:Form ID="Form7"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="50px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow6" ColumnWidths="100px 100px 100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chkPriP" Label="Prim. p" runat="server"></x:CheckBox>
                                                                <x:CheckBox ID="chkPriS" Label="Prim. s" runat="server"></x:CheckBox>           
                                                                <x:CheckBox ID="chkSecP" Label="Sec. p" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkSecS" Label="Sec. s" runat="server"></x:CheckBox>                                       
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow16" ColumnWidths="100px 100px 100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chkPriQ" Label="Prim. q" runat="server"></x:CheckBox>
                                                                <x:CheckBox ID="chkPriT" Label="Prim. t" runat="server"></x:CheckBox>           
                                                                <x:CheckBox ID="chkSecQ" Label="Sec. q" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkSecT" Label="Sec. t" runat="server"></x:CheckBox>                                       
                                                            </Items>
                                                        </x:FormRow>
                                                        <x:FormRow ID="FormRow17" ColumnWidths="100px 100px 100px 100px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chkPriR" Label="Prim. r" runat="server"></x:CheckBox>
                                                                <x:CheckBox ID="chkPriU" Label="Prim. u" runat="server"></x:CheckBox>           
                                                                <x:CheckBox ID="chkSecR" Label="Sec. r" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkSecU" Label="Sec. u" runat="server"></x:CheckBox>                                       
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>                  
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                            </x:Form>
                        </Items>
                    </x:GroupPanel>

                      <x:GroupPanel runat="server" Title="Opacidades grandes" ID="GroupPanel4"  Width="150" AutoWidth="true" BoxFlex="1" Height="132" >                
                        <Items>
                            <x:Form ID="Form8" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow7" ColumnWidths="150px" runat="server">
                                            <Items> 
                                                <x:Form ID="Form9"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="40px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow8" ColumnWidths="150px" runat="server" >
                                                            <Items>
                                                               <x:CheckBox ID="chkO" Label="O" runat="server"></x:CheckBox>
                                                                                                           
                                                            </Items>
                                                        </x:FormRow>
                                                           <x:FormRow ID="FormRow18" ColumnWidths="150px" runat="server" >
                                                            <Items>
                                                             <x:CheckBox ID="chkA" Label="A" runat="server"></x:CheckBox>           
                                                                                                   
                                                            </Items>
                                                        </x:FormRow>
                                                         <x:FormRow ID="FormRow19" ColumnWidths="150px" runat="server" >
                                                            <Items>
                                                                 <x:CheckBox ID="chkB" Label="B" runat="server"></x:CheckBox>  
                                                                                                       
                                                            </Items>
                                                        </x:FormRow>
                                                         <x:FormRow ID="FormRow20" ColumnWidths="150px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chkC" Label="C" runat="server"></x:CheckBox>                                           
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>                  
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                            </x:Form>
                        </Items>
                    </x:GroupPanel>
                </Items>
             </x:Panel>

         <x:Panel ID="Panel2" Title="Buscar Servicio" Width="1500px" Height="200px" EnableBackgroundColor="true"
                TableRowspan="2" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" BoxConfigAlign="Stretch" AutoScroll="true"> 
                  <Items>
                    <x:GroupPanel runat="server" Title="Símbolos" ID="GroupPanel5" AutoWidth="true" BoxFlex="1" Height="132" >                
                        <Items>
                            <x:Form ID="Form10" runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="90px" LabelAlign="Left">
                                    <Rows>
                                        <x:FormRow ID="FormRow9" ColumnWidths="1400px" runat="server">
                                            <Items> 
                                                <x:Form ID="Form11"   runat="server" EnableBackgroundColor="true" ShowBorder="False" ShowHeader="False" LabelWidth="20px" LabelAlign="Left">
                                                    <Rows>
                                                        <x:FormRow ID="FormRow10" ColumnWidths="70px 70px" runat="server" >
                                                            <Items>
                                                              <x:CheckBox ID="chkSI" Label="SI" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkNO" Label="NO" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkaa" Label="aa" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkat" Label="at" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkax" Label="ax" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkbu" Label="bu" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkca" Label="ca" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkcg" Label="cg" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chken" Label="en" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkco" Label="co" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkcp" Label="cp" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkcv" Label="cv" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkdi" Label="di" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkef" Label="ef" runat="server"></x:CheckBox>  
                                                                                                      
                                                            </Items>
                                                        </x:FormRow>
                                                           <x:FormRow ID="FormRow21" ColumnWidths="70px 70px" runat="server" >
                                                            <Items>
                                                              <x:CheckBox ID="chkem" Label="em" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkes" Label="es" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkod" Label="od" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkfr" Label="fr" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkhi" Label="hi" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkid" Label="id" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkoh" Label="oh" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkih" Label="ih" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkkl" Label="kl" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkme" Label="me" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkpa" Label="pa" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkpb" Label="pb" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkpi" Label="pi" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkpx" Label="px" runat="server"></x:CheckBox>  
                                                                                                      
                                                            </Items>
                                                        </x:FormRow>
                                                         <x:FormRow ID="FormRow22" ColumnWidths="70px 70px" runat="server" >
                                                            <Items>
                                                                <x:CheckBox ID="chkra" Label="ra" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chkrq" Label="rq" runat="server"></x:CheckBox>  
                                                                <x:CheckBox ID="chktb" Label="tb" runat="server"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox311" Label="tb" runat="server" Hidden="true" ></x:CheckBox>
                                                                   
                                                                <x:CheckBox ID="CheckBox31" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox32" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox33" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox34" Label="tb" runat="server" Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox35" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox36" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox37" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox38" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox39" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                <x:CheckBox ID="CheckBox40" Label="tb" runat="server"  Hidden="true"></x:CheckBox>
                                                                                      
                                                            </Items>
                                                        </x:FormRow>
                                                         <x:FormRow ID="FormRow23" ColumnWidths="100px 350px 100px 350px" runat="server"  >
                                                            <Items>
                                                                <x:Label ID="lbl1" runat="server" Text="Comentario"></x:Label>  
                                                                <x:TextBox ID="txtComentario" Text="" runat="server" Width="300" TableColspan="5"></x:TextBox>    
                                                                <x:Label ID="Label1" runat="server" Text="Conclusiones"></x:Label>  
                                                                <x:TextBox ID="txtConclusiones" Text="" runat="server"  Width="300" TableColspan="5"></x:TextBox>      
                                                            </Items>
                                                        </x:FormRow>
                                                    </Rows>
                                                </x:Form>                  
                                            </Items>
                                        </x:FormRow>
                                    </Rows>
                            </x:Form>
                        </Items>
                    </x:GroupPanel>
                </Items>
             </x:Panel>
        
    </form>
</body>
</html>
