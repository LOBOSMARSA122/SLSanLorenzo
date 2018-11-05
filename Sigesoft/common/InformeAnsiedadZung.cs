using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;
namespace NetPdf
{
   public class InformeAnsiedadZung
    {
       public static void CreateInformeAnsiedadZung(ServiceList DataService, organizationDto infoEmpresaPropietaria, List<ServiceComponentFieldValuesList> valores, string filePDF)
       {
           Document document = new Document();
           document.SetPageSize(iTextSharp.text.PageSize.A4);

           // step 2: we create a writer that listens to the document
           PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
           //holaaaaaaa
           //create an instance of your PDFpage class. This is the class we generated above.
           pdfPage page = new pdfPage();
           //page.FirmaTrabajador = DataService.FirmaTrabajador;
           //page.HuellaTrabajador = DataService.HuellaTrabajador;
           //page.Dni = DataService.v_DocNumber;
           //page.EmpresaId = DataService.EmpresaClienteId;
           //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
           writer.PageEvent = page;

           // step 3: we open the document
           document.Open();
           // step 4: we Add content to the document
           // we define some fonts

           #region Fonts

           Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
           Font fontTitle2 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
           Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
           Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
           Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
           Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

           Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
           Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
           Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

           #endregion

           #region Declaration Tables
           var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
           string include = string.Empty;
           List<PdfPCell> cells = null;
           float[] columnWidths = null;
           //string[] columnValues = null;
           string[] columnHeaders = null;


           PdfPTable filiationWorker = new PdfPTable(8);

           PdfPTable table = null;

           PdfPCell cell = null;

           #endregion

           #region Title

           PdfPCell CellLogo = null;
                     
           if (infoEmpresaPropietaria.b_Image != null)
           {
               CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 20F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
           }
           else
           {
               CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
           }                   


           cells = new List<PdfPCell>()
                   {      
                    //fila 
                    new PdfPCell(CellLogo){Rowspan =2, Colspan = 2, Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("ESCALA DE AUTOVALORACIÓN DE LA ANSIEDAD DE ZUNG", fontTitle1)){Rowspan =2,Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},       
                  
                   };

           columnWidths = new float[] { 15f, 5f, 30f, 25f, 20f, 5f };

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
           document.Add(table);

           #endregion
           var tamaño_celda = 24f;
           var tamaño_celda1 = 40f;
           #region Datos Personales

           cells = new List<PdfPCell>()
                   {      
                    //fila 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("Nombre:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_Pacient, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Sexo:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_GenderName, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Edad:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.i_Edad.ToString()+" años", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Estado Civil:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.EstadoCivil, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Fecha:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.d_ServiceDate.Value.ToString("dd/MM/yyyy"), fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Historia Clínica:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_ServiceId, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE,},
                    new PdfPCell(new Phrase("DNI:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_DocNumber, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Dirección:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_AdressLocation, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                   };

           columnWidths = new float[] { 15f, 35f, 15f, 35f, };

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
           document.Add(table);
           #endregion

           #region Cuadro
           #region grupo 1
           string p1_1 = ""; string p1_2 = ""; string p1_3 = ""; string p1_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004106") != null) {p1_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004106").v_Value1;}
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004107") != null) {p1_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004107").v_Value1;}
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004108") != null) {p1_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004108").v_Value1;}
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004109") != null) {p1_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004109").v_Value1;}
           var resultado = ""; var X1_1 = ""; var X1_2 = ""; var X1_3 = ""; var X1_4 = "";
           if (p1_1 == "1") { resultado = "1"; X1_1 = "X"; }
           else if (p1_2 == "1") { resultado = "2"; X1_2 = "X"; }
           else if (p1_3 == "1") { resultado = "3"; X1_3 = "X"; }
           else if (p1_4 == "1") { resultado = "4"; X1_4 = "X"; }
           #endregion
           #region grupo 2
           string p2_1 = ""; string p2_2 = ""; string p2_3 = ""; string p2_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004110") != null) { p2_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004110").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004111") != null) { p2_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004111").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004112") != null) { p2_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004112").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004113") != null) { p2_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004113").v_Value1; }
           var resultado2 = ""; var X2_1 = ""; var X2_2 = ""; var X2_3 = ""; var X2_4 = "";
           if (p2_1 == "1") { resultado2 = "1"; X2_1 = "X"; }
           else if (p2_2 == "1") { resultado2 = "2"; X2_2 = "X"; }
           else if (p2_3 == "1") { resultado2 = "3"; X2_3 = "X"; }
           else if (p2_4 == "1") { resultado2 = "4"; X2_4 = "X"; }
           #endregion
           #region grupo 3
           string p3_1 = ""; string p3_2 = ""; string p3_3 = ""; string p3_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004114") != null) { p3_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004114").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004115") != null) { p3_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004115").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004116") != null) { p3_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004116").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004117") != null) { p3_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004117").v_Value1; }
           var resultado3 = ""; var X3_1 = ""; var X3_2 = ""; var X3_3 = ""; var X3_4 = "";
           if (p3_1 == "1") { resultado3 = "1"; X3_1 = "X"; }
           else if (p3_2 == "1") { resultado3 = "2"; X3_2 = "X"; }
           else if (p3_3 == "1") { resultado3 = "3"; X3_3 = "X"; }
           else if (p3_4 == "1") { resultado3 = "4"; X3_4 = "X"; }
           #endregion
           #region grupo 4
           string p4_1 = ""; string p4_2 = ""; string p4_3 = ""; string p4_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004118") != null) { p4_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004118").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004119") != null) { p4_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004119").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004120") != null) { p4_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004120").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004121") != null) { p4_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004121").v_Value1; }
           var resultado4 = ""; var X4_1 = ""; var X4_2 = ""; var X4_3 = ""; var X4_4 = "";
           if (p4_1 == "1") { resultado4 = "1"; X4_1 = "X"; }
           else if (p4_2 == "1") { resultado4 = "2"; X4_2 = "X"; }
           else if (p4_3 == "1") { resultado4 = "3"; X4_3 = "X"; }
           else if (p4_4 == "1") { resultado4 = "4"; X4_4 = "X"; }
           #endregion
           #region grupo 5
           string p5_1 = ""; string p5_2 = ""; string p5_3 = ""; string p5_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004122") != null) { p5_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004122").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004123") != null) { p5_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004123").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004124") != null) { p5_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004124").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004125") != null) { p5_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004125").v_Value1; }
           var resultado5 = ""; var X5_1 = ""; var X5_2 = ""; var X5_3 = ""; var X5_4 = "";
           if (p5_1 == "1") { resultado5 = "4"; X5_1 = "X"; }
           else if (p5_2 == "1") { resultado5 = "3"; X5_2 = "X"; }
           else if (p5_3 == "1") { resultado5 = "2"; X5_3 = "X"; }
           else if (p5_4 == "1") { resultado5 = "1"; X5_4 = "X"; }
           #endregion
           #region grupo 6
           string p6_1 = ""; string p6_2 = ""; string p6_3 = ""; string p6_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004126") != null) { p6_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004126").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004127") != null) { p6_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004127").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004128") != null) { p6_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004128").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004129") != null) { p6_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004129").v_Value1; }
           var resultado6 = ""; var X6_1 = ""; var X6_2 = ""; var X6_3 = ""; var X6_4 = "";
           if (p6_1 == "1") { resultado6 = "1"; X6_1 = "X"; }
           else if (p6_2 == "1") { resultado6 = "2"; X6_2 = "X"; }
           else if (p6_3 == "1") { resultado6 = "3"; X6_3 = "X"; }
           else if (p6_4 == "1") { resultado6 = "4"; X6_4 = "X"; }
           #endregion
           #region grupo 7
           string p7_1 = ""; string p7_2 = ""; string p7_3 = ""; string p7_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004130") != null) { p7_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004130").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004131") != null) { p7_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004131").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004132") != null) { p7_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004132").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004133") != null) { p7_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004133").v_Value1; }
           var resultado7 = ""; var X7_1 = ""; var X7_2 = ""; var X7_3 = ""; var X7_4 = "";
           if (p7_1 == "1") { resultado7 = "1"; X7_1 = "X"; }
           else if (p7_2 == "1") { resultado7 = "2"; X7_2 = "X"; }
           else if (p7_3 == "1") { resultado7 = "3"; X7_3 = "X"; }
           else if (p7_4 == "1") { resultado7 = "4"; X7_4 = "X"; }
           #endregion
           #region grupo 8
           string p8_1 = ""; string p8_2 = ""; string p8_3 = ""; string p8_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004134") != null) { p8_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004134").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004135") != null) { p8_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004135").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004136") != null) { p8_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004136").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004137") != null) { p8_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004137").v_Value1; }
           var resultado8 = ""; var X8_1 = ""; var X8_2 = ""; var X8_3 = ""; var X8_4 = "";
           if (p8_1 == "1") { resultado8 = "1"; X8_1 = "X"; }
           else if (p8_2 == "1") { resultado8 = "2"; X8_2 = "X"; }
           else if (p8_3 == "1") { resultado8 = "3"; X8_3 = "X"; }
           else if (p8_4 == "1") { resultado8 = "4"; X8_4 = "X"; }
           #endregion
           #region grupo 9
           string p9_1 = ""; string p9_2 = ""; string p9_3 = ""; string p9_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004138") != null) { p9_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004138").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004139") != null) { p9_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004139").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004140") != null) { p9_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004140").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004141") != null) { p9_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004141").v_Value1; }
           var resultado9 = ""; var X9_1 = ""; var X9_2 = ""; var X9_3 = ""; var X9_4 = "";
           if (p9_1 == "1") { resultado9 = "4"; X9_1 = "X"; }
           else if (p9_2 == "1") { resultado9 = "3"; X9_2 = "X"; }
           else if (p9_3 == "1") { resultado9 = "2"; X9_3 = "X"; }
           else if (p9_4 == "1") { resultado9 = "1"; X9_4 = "X"; }
           #endregion           
           #region grupo 10
           string p10_1 = ""; string p10_2 = ""; string p10_3 = ""; string p10_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004142") != null) { p10_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004142").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004143") != null) { p10_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004143").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004144") != null) { p10_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004144").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004145") != null) { p10_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004145").v_Value1; }
           var resultado10 = ""; var X10_1 = ""; var X10_2 = ""; var X10_3 = ""; var X10_4 = "";
           if (p10_1 == "1") { resultado10 = "1"; X10_1 = "X"; }
           else if (p10_2 == "1") { resultado10 = "2"; X10_2 = "X"; }
           else if (p10_3 == "1") { resultado10 = "3"; X10_3 = "X"; }
           else if (p10_4 == "1") { resultado10 = "4"; X10_4 = "X"; }
           #endregion           
           #region grupo 11
           string p11_1 = ""; string p11_2 = ""; string p11_3 = ""; string p11_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004146") != null) { p11_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004146").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004147") != null) { p11_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004147").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004148") != null) { p11_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004148").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004149") != null) { p11_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004149").v_Value1; }
           var resultado11 = ""; var X11_1 = ""; var X11_2 = ""; var X11_3 = ""; var X11_4 = "";
           if (p11_1 == "1") { resultado11 = "1"; X11_1 = "X"; }
           else if (p11_2 == "1") { resultado11 = "2"; X11_2 = "X"; }
           else if (p11_3 == "1") { resultado11 = "3"; X11_3 = "X"; }
           else if (p11_4 == "1") { resultado11 = "4"; X11_4 = "X"; }
           #endregion           
           #region grupo 12
           string p12_1 = ""; string p12_2 = ""; string p12_3 = ""; string p12_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004150") != null) { p12_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004150").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004151") != null) { p12_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004151").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004152") != null) { p12_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004152").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004153") != null) { p12_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004153").v_Value1; }
           var resultado12 = ""; var X12_1 = ""; var X12_2 = ""; var X12_3 = ""; var X12_4 = "";
           if (p12_1 == "1") { resultado12 = "1"; X12_1 = "X"; }
           else if (p12_2 == "1") { resultado12 = "2"; X12_2 = "X"; }
           else if (p12_3 == "1") { resultado12 = "3"; X12_3 = "X"; }
           else if (p12_4 == "1") { resultado12 = "4"; X12_4 = "X"; }
           #endregion           
           #region grupo 13
           string p13_1 = ""; string p13_2 = ""; string p13_3 = ""; string p13_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004154") != null) { p13_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004154").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004155") != null) { p13_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004155").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004156") != null) { p13_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004156").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004157") != null) { p13_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004157").v_Value1; }
           var resultado13 = ""; var X13_1 = ""; var X13_2 = ""; var X13_3 = ""; var X13_4 = "";
           if (p13_1 == "1") { resultado13 = "4"; X13_1 = "X"; }
           else if (p13_2 == "1") { resultado13 = "3"; X13_2 = "X"; }
           else if (p13_3 == "1") { resultado13 = "2"; X13_3 = "X"; }
           else if (p13_4 == "1") { resultado13 = "1"; X13_4 = "X"; }
           #endregion           
           #region grupo 14
           string p14_1 = ""; string p14_2 = ""; string p14_3 = ""; string p14_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004158") != null) { p14_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004158").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004159") != null) { p14_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004159").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004160") != null) { p14_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004160").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004161") != null) { p14_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004161").v_Value1; }
           var resultado14 = ""; var X14_1 = ""; var X14_2 = ""; var X14_3 = ""; var X14_4 = "";
           if (p14_1 == "1") { resultado14 = "1"; X14_1 = "X"; }
           else if (p14_2 == "1") { resultado14 = "2"; X14_2 = "X"; }
           else if (p14_3 == "1") { resultado14 = "3"; X14_3 = "X"; }
           else if (p14_4 == "1") { resultado14 = "4"; X14_4 = "X"; }
           #endregion           
           #region grupo 15
           string p15_1 = ""; string p15_2 = ""; string p15_3 = ""; string p15_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004162") != null) { p15_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004162").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004163") != null) { p15_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004163").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004164") != null) { p15_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004164").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004165") != null) { p15_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004165").v_Value1; }
           var resultado15 = ""; var X15_1 = ""; var X15_2 = ""; var X15_3 = ""; var X15_4 = "";
           if (p15_1 == "1") { resultado15 = "1"; X15_1 = "X"; }
           else if (p15_2 == "1") { resultado15 = "2"; X15_2 = "X"; }
           else if (p15_3 == "1") { resultado15 = "3"; X15_3 = "X"; }
           else if (p15_4 == "1") { resultado15 = "4"; X15_4 = "X"; }
           #endregion           
           #region grupo 16
           string p16_1 = ""; string p16_2 = ""; string p16_3 = ""; string p16_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004166") != null) { p16_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004166").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004167") != null) { p16_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004167").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004168") != null) { p16_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004168").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004169") != null) { p16_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004169").v_Value1; }
           var resultado16 = ""; var X16_1 = ""; var X16_2 = ""; var X16_3 = ""; var X16_4 = "";
           if (p16_1 == "1") { resultado16 = "1"; X16_1 = "X"; }
           else if (p16_2 == "1") { resultado16 = "2"; X16_2 = "X"; }
           else if (p16_3 == "1") { resultado16 = "3"; X16_3 = "X"; }
           else if (p16_4 == "1") { resultado16 = "4"; X16_4 = "X"; }
           #endregion           
           #region grupo 17
           string p17_1 = ""; string p17_2 = ""; string p17_3 = ""; string p17_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004170") != null) { p17_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004170").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004171") != null) { p17_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004171").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004172") != null) { p17_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004172").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004173") != null) { p17_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004173").v_Value1; }
           var resultado17 = ""; var X17_1 = ""; var X17_2 = ""; var X17_3 = ""; var X17_4 = "";
           if (p17_1 == "1") { resultado17 = "4"; X17_1 = "X"; }
           else if (p17_2 == "1") { resultado17 = "3"; X17_2 = "X"; }
           else if (p17_3 == "1") { resultado17 = "2"; X17_3 = "X"; }
           else if (p17_4 == "1") { resultado17 = "1"; X17_4 = "X"; }
           #endregion           
           #region grupo 18
           string p18_1 = ""; string p18_2 = ""; string p18_3 = ""; string p18_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004174") != null) { p18_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004174").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004175") != null) { p18_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004175").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004176") != null) { p18_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004176").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004177") != null) { p18_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004177").v_Value1; }
           var resultado18 = ""; var X18_1 = ""; var X18_2 = ""; var X18_3 = ""; var X18_4 = "";
           if (p18_1 == "1") { resultado18 = "1"; X18_1 = "X"; }
           else if (p18_2 == "1") { resultado18 = "2"; X18_2 = "X"; }
           else if (p18_3 == "1") { resultado18 = "3"; X18_3 = "X"; }
           else if (p18_4 == "1") { resultado18 = "4"; X18_4 = "X"; }
           #endregion           
           #region grupo 19
           string p19_1 = ""; string p19_2 = ""; string p19_3 = ""; string p19_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004178") != null) { p19_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004178").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004179") != null) { p19_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004179").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004180") != null) { p19_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004180").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004181") != null) { p19_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004181").v_Value1; }
           var resultado19 = ""; var X19_1 = ""; var X19_2 = ""; var X19_3 = ""; var X19_4 = "";
           if (p19_1 == "1") { resultado19 = "4"; X19_1 = "X"; }
           else if (p19_2 == "1") { resultado19 = "3"; X19_2 = "X"; }
           else if (p19_3 == "1") { resultado19 = "2"; X19_3 = "X"; }
           else if (p19_4 == "1") { resultado19 = "1"; X19_4 = "X"; }
           #endregion           
           #region grupo 20
           string p20_1 = ""; string p20_2 = ""; string p20_3 = ""; string p20_4 = "";
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004182") != null) { p20_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004182").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004183") != null) { p20_2 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004183").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004184") != null) { p20_3 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004184").v_Value1; }
           if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004185") != null) { p20_4 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004185").v_Value1; }
           var resultado20 = ""; var X20_1 = ""; var X20_2 = ""; var X20_3 = ""; var X20_4 = "";
           if (p20_1 == "1") { resultado20 = "1"; X20_1 = "X"; }
           else if (p20_2 == "1") { resultado20 = "2"; X20_2 = "X"; }
           else if (p20_3 == "1") { resultado20 = "3"; X20_3 = "X"; }
           else if (p20_4 == "1") { resultado20 = "4"; X20_4 = "X"; }
           #endregion           
           #region grupo resultado           
           var resultadofinal = int.Parse(resultado.ToString()) + int.Parse(resultado2.ToString()) + int.Parse(resultado3.ToString()) + int.Parse(resultado4.ToString()) + int.Parse(resultado5.ToString()) + int.Parse(resultado6.ToString()) + int.Parse(resultado7.ToString()) + int.Parse(resultado8.ToString()) + int.Parse(resultado9.ToString()) + int.Parse(resultado10.ToString()) + int.Parse(resultado11.ToString()) + int.Parse(resultado12.ToString()) + int.Parse(resultado13.ToString()) + int.Parse(resultado14.ToString()) + int.Parse(resultado15.ToString()) + int.Parse(resultado16.ToString()) + int.Parse(resultado17.ToString()) + int.Parse(resultado18.ToString()) + int.Parse(resultado19.ToString()) + int.Parse(resultado20.ToString());
           
           #endregion           
           cells = new List<PdfPCell>()
                   {      
                    //fila  
                    #region  cabecera
                    new PdfPCell(new Phrase("", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = tamaño_celda1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, BorderColorRight=BaseColor.WHITE },   
                    new PdfPCell(new Phrase("", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = tamaño_celda1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("Nunca o casi nunca", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },
                    new PdfPCell(new Phrase("A veces", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },
                    new PdfPCell(new Phrase("Con bastante frecuencia", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },   
                    new PdfPCell(new Phrase("Siempre o caso siempre", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1},
                    new PdfPCell(new Phrase("Puntos", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },    
                    #endregion    
                    #region pregunta 1
                    new PdfPCell(new Phrase("1", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me siento más intranquilo y nervioso que de costumbre", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X2_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(resultado, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 2
                    new PdfPCell(new Phrase("2", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me siento atemorizado sin motivo", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X2_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X2_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X2_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X2_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion             
                    #region pregunta 3
                    new PdfPCell(new Phrase("3", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me altero o me angustio fácilmente", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X3_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X3_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X3_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X3_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion         
                    #region pregunta 4
                    new PdfPCell(new Phrase("4", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Siento como si me estuviera deshaciendo en pedazos", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X4_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X4_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X4_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion            
                    #region pregunta 5
                    new PdfPCell(new Phrase("5", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Creo que todo está bien y no va a pasar nada malo", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X5_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X5_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X5_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion        
                    #region pregunta 6
                    new PdfPCell(new Phrase("6", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me tiemblan los brazos y las piernas", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X6_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X6_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X6_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion               
                    #region pregunta 7
                    new PdfPCell(new Phrase("7", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Sufro dolores de cabeza, del cuello y de la espalda", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X7_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X7_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X7_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 8
                    new PdfPCell(new Phrase("8", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me siento débil y me canso fácilmente", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X8_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X8_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X8_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado8, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 9
                    new PdfPCell(new Phrase("9", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me siento intranquilo y me es fácil estar tranquilo", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X9_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X9_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X9_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado9, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion        
                    #region Pregunta 10
                    new PdfPCell(new Phrase("10", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Siento que el corazón me late a prisa", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(X10_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X10_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X10_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X10_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado10, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 11
                    new PdfPCell(new Phrase("11", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Sufro mareos", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X11_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X11_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X11_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X11_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado11, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 12
                    new PdfPCell(new Phrase("12", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me desmayo o siento que voy a desmayarme", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X12_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X12_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X12_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X12_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(resultado12, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion    
                    #region pregunta 13
                    new PdfPCell(new Phrase("13", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Puedo respirar fácilmente", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X13_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X13_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X13_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X13_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado13, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 14
                    new PdfPCell(new Phrase("14", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Se me duermen y me hormiguean los dedos de las manos y de los pies", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X14_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X14_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X14_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X14_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado14, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion   
                    #region pregunta 15
                    new PdfPCell(new Phrase("15", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Sufro dolores de estómago o indigestión", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X15_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X15_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X15_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X15_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado15, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 16
                    new PdfPCell(new Phrase("16", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Tengo que orinar con mucha frecuencia", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X16_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X16_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X16_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X16_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado16, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 17
                    new PdfPCell(new Phrase("17", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Generalmente tengo las manos secas y calientes", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X17_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X17_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X17_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X17_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado17, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 18
                    new PdfPCell(new Phrase("18", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La cara se me pone caliente y roja", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X18_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X18_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X18_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X18_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado18, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta 19
                    new PdfPCell(new Phrase("19", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me duermo fácilmente y descanso bien por la noche", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X19_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X19_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X19_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X19_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado19, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion 
                    #region pregunta 20
                    new PdfPCell(new Phrase("20", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Tengo pesadillas", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X20_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X20_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(X20_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },   
                    new PdfPCell(new Phrase(X20_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    new PdfPCell(new Phrase(resultado20, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                    #region pregunta resultado
                    new PdfPCell(new Phrase("Total :   ", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, Colspan=6 },   
                    new PdfPCell(new Phrase(resultadofinal.ToString(), fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE  },
                    #endregion
                   };

           columnWidths = new float[] { 5f, 35f, 12f, 12f, 12f, 12f, 12f };
           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
           document.Add(table);
           #endregion
           #region Puntaje
           var interpretacion = "";
           if (int.Parse(resultadofinal.ToString()) <= 50) { interpretacion = "DENTRO DE LO NORMAL"; }
           else if (int.Parse(resultadofinal.ToString()) >= 51 && int.Parse(resultadofinal.ToString()) <= 60) { interpretacion = "ANSIEDAD LEVE"; }
           else if (int.Parse(resultadofinal.ToString()) >= 61 && int.Parse(resultadofinal.ToString()) <= 70) { interpretacion = "ANSIEDAD MODERADA"; }
           else if (int.Parse(resultadofinal.ToString()) >= 71) { interpretacion = "ANSIEDAD INTENSA"; }
           cells = new List<PdfPCell>()
                   {      
                    //fila 
                    
                    new PdfPCell(new Phrase("ÍNDICE EEA", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda },   
                    new PdfPCell(new Phrase("INTERPRETACIÓN", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(resultadofinal.ToString(), fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda },
                    new PdfPCell(new Phrase(interpretacion, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },                 
                    
                   };

           columnWidths = new float[] { 20f, 80f };

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTable);
           document.Add(table);
           #endregion

           document.Close();
       }
    }
}
