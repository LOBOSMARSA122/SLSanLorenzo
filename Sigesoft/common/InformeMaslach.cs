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
    public class InformeMaslach
    {
        public static void CreateInformeMaslach(ServiceList DataService, organizationDto infoEmpresaPropietaria, List<ServiceComponentFieldValuesList> valores, string filePDF)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            //holaaaaaaa
            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
            page.FirmaTrabajador = DataService.FirmaTrabajador;
            page.HuellaTrabajador = DataService.HuellaTrabajador;
            page.Dni = DataService.v_DocNumber;
            page.EmpresaId = DataService.EmpresaClienteId;
            //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
            writer.PageEvent = page;

            // step 3: we open the document
            document.Open();
            // step 4: we Add content to the document
            // we define some fonts

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
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
                    new PdfPCell(new Phrase("000000000)", fontTitle1)){Rowspan =2,Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},       
                  
                   };

            columnWidths = new float[] { 15f, 5f, 30f, 25f, 20f, 5f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion
            var tamaño_celda = 24f;            

            #region Cabecera
            cells = new List<PdfPCell>()
                   {      
                    //fila                      
                    new PdfPCell(new Phrase("A continuación encontrará una serie de enunciados acerca de su trabajo y de sus sentimientos en él. Le pedimos su colaboración respondiendo a ellos como lo siente. No existen respuestas mejores o peores, la respuesta correcta es aquella que expresa verídicamente su propia existencia. Los resultados de este cuestionario son estrictamente confidenciales y en ningún caso accesibles a otras personas. Su objeto es contribuir al conocimiento de las condiciones de su trabajo y mejorar su nivel de satisfacción.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase("A cada una de las frases debe responder expresando la frecuencia con que tiene ese sentimiento de la siguiente forma: ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase("(1)  Nunca - (2) Algunas veces al año - (3)  Algunas veces al mes - (4)  Algunas veces a la semana - (5) Diariamente", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },                    
                   };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Datos Personales

            cells = new List<PdfPCell>()
                   {      
                    //fila                     
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("Nombre:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_Pacient, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("DNI:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_DocNumber, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                     new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                   };

            columnWidths = new float[] { 15f, 35f, 15f, 35f, };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Cuadro
            #region grupo 1
            string p1_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004197") != null) { p1_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004197").v_Value1Name; }
            var resultado = ""; var X1_1 = ""; var X1_2 = ""; var X1_3 = ""; var X1_4 = ""; var X1_5 = ""; var X1_6 = ""; var X1_7 = "";
            if (p1_1 == "1") { resultado = "1"; X1_1 = "X"; }
            else if (p1_1 == "2") { resultado = "2"; X1_2 = "X"; }
            else if (p1_1 == "3") { resultado = "3"; X1_3 = "X"; }
            else if (p1_1 == "4") { resultado = "4"; X1_4 = "X"; }
            else if (p1_1 == "5") { resultado = "5"; X1_5 = "X"; }
            else if (p1_1 == "6") { resultado = "6"; X1_6 = "X"; }
            else if (p1_1 == "7") { resultado = "7"; X1_7 = "X"; }
            #endregion
            #region grupo 2
            string p2_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004198") != null) { p2_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004198").v_Value1Name; }
            var resultado2 = ""; var X2_1 = ""; var X2_2 = ""; var X2_3 = ""; var X2_4 = ""; var X2_5 = ""; var X2_6 = ""; var X2_7 = "";
            if (p2_1 == "1") { resultado2 = "1"; X2_1 = "X"; }
            else if (p2_1 == "2") { resultado2 = "2"; X2_2 = "X"; }
            else if (p2_1 == "3") { resultado2 = "3"; X2_3 = "X"; }
            else if (p2_1 == "4") { resultado2 = "4"; X2_4 = "X"; }
            else if (p2_1 == "5") { resultado2 = "5"; X2_5 = "X"; }
            else if (p2_1 == "6") { resultado2 = "6"; X2_6 = "X"; }
            else if (p2_1 == "7") { resultado2 = "7"; X2_7 = "X"; }
            #endregion
            #region grupo 3
            string p3_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004199") != null) { p3_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004199").v_Value1Name; }
            var resultado3 = ""; var x3_1 = ""; var x3_2 = ""; var x3_3 = ""; var x3_4 = ""; var x3_5 = ""; var x3_6 = ""; var x3_7 = "";
            if (p3_1 == "1") { resultado3 = "1"; x3_1 = "X"; }
            else if (p3_1 == "2") { resultado3 = "2"; x3_2 = "X"; }
            else if (p3_1 == "3") { resultado3 = "3"; x3_3 = "X"; }
            else if (p3_1 == "4") { resultado3 = "4"; x3_4 = "X"; }
            else if (p3_1 == "5") { resultado3 = "5"; x3_5 = "X"; }
            else if (p3_1 == "6") { resultado3 = "6"; x3_6 = "X"; }
            else if (p3_1 == "7") { resultado3 = "7"; x3_7 = "X"; }
            #endregion
            #region grupo 4
            string p4_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004200") != null) { p4_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004200").v_Value1Name; }
            var resultado4 = ""; var X4_1 = ""; var X4_2 = ""; var X4_3 = ""; var X4_4 = ""; var X4_5 = ""; var X4_6 = ""; var X4_7 = "";
            if (p4_1 == "1") { resultado4 = "1"; X4_1 = "X"; }
            else if (p4_1 == "2") { resultado4 = "2"; X4_2 = "X"; }
            else if (p4_1 == "3") { resultado4 = "3"; X4_3 = "X"; }
            else if (p4_1 == "4") { resultado4 = "4"; X4_4 = "X"; }
            else if (p4_1 == "5") { resultado4 = "5"; X4_5 = "X"; }
            else if (p4_1 == "6") { resultado4 = "6"; X4_6 = "X"; }
            else if (p4_1 == "7") { resultado4 = "7"; X4_7 = "X"; }
            #endregion
            #region grupo 5
            string p5_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004201") != null) { p5_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004201").v_Value1Name; }
            var resultado5 = ""; var X5_1 = ""; var X5_2 = ""; var X5_3 = ""; var X5_4 = ""; var X5_5 = ""; var X5_6 = ""; var X5_7 = "";
            if (p5_1 == "1") { resultado5 = "1"; X5_1 = "X"; }
            else if (p5_1 == "2") { resultado5 = "2"; X5_2 = "X"; }
            else if (p5_1 == "3") { resultado5 = "3"; X5_3 = "X"; }
            else if (p5_1 == "4") { resultado5 = "4"; X5_4 = "X"; }
            else if (p5_1 == "5") { resultado5 = "5"; X5_5 = "X"; }
            else if (p5_1 == "6") { resultado5 = "6"; X5_6 = "X"; }
            else if (p5_1 == "7") { resultado5 = "7"; X5_7 = "X"; }
            #endregion
            #region grupo 6
            string p6_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004202") != null) { p6_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004202").v_Value1Name; }
            var resultado6 = ""; var X6_1 = ""; var X6_2 = ""; var X6_3 = ""; var X6_4 = ""; var X6_5 = ""; var X6_6 = ""; var X6_7 = "";
            if (p6_1 == "1") { resultado6 = "1"; X6_1 = "X"; }
            else if (p6_1 == "2") { resultado6 = "2"; X6_2 = "X"; }
            else if (p6_1 == "3") { resultado6 = "3"; X6_3 = "X"; }
            else if (p6_1 == "4") { resultado6 = "4"; X6_4 = "X"; }
            else if (p6_1 == "5") { resultado6 = "5"; X6_5 = "X"; }
            else if (p6_1 == "6") { resultado6 = "6"; X6_6 = "X"; }
            else if (p6_1 == "7") { resultado6 = "7"; X6_7 = "X"; }
            #endregion
            #region grupo 7
            string p7_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004203") != null) { p7_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004203").v_Value1Name; }
            var resultado7 = ""; var X7_1 = ""; var X7_2 = ""; var X7_3 = ""; var X7_4 = ""; var X7_5 = ""; var X7_6 = ""; var X7_7 = "";
            if (p7_1 == "1") { resultado7 = "1"; X7_1 = "X"; }
            else if (p7_1 == "2") { resultado7 = "2"; X7_2 = "X"; }
            else if (p7_1 == "3") { resultado7 = "3"; X7_3 = "X"; }
            else if (p7_1 == "4") { resultado7 = "4"; X7_4 = "X"; }
            else if (p7_1 == "5") { resultado7 = "5"; X7_5 = "X"; }
            else if (p7_1 == "6") { resultado7 = "6"; X7_6 = "X"; }
            else if (p7_1 == "7") { resultado7 = "7"; X7_7 = "X"; }
            #endregion
            #region grupo 8
            string p8_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004204") != null) { p8_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004204").v_Value1Name; }
            var resultado8 = ""; var X8_1 = ""; var X8_2 = ""; var X8_3 = ""; var X8_4 = ""; var X8_5 = ""; var X8_6 = ""; var X8_7 = "";
            if (p8_1 == "1") { resultado8 = "1"; X8_1 = "X"; }
            else if (p8_1 == "2") { resultado8 = "2"; X8_2 = "X"; }
            else if (p8_1 == "3") { resultado8 = "3"; X8_3 = "X"; }
            else if (p8_1 == "4") { resultado8 = "4"; X8_4 = "X"; }
            else if (p8_1 == "5") { resultado8 = "5"; X8_5 = "X"; }
            else if (p8_1 == "6") { resultado8 = "6"; X8_6 = "X"; }
            else if (p8_1 == "7") { resultado8 = "7"; X8_7 = "X"; }
            #endregion
            #region grupo 9
            string p9_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004205") != null) { p9_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004205").v_Value1Name; }
            var resultado9 = ""; var X9_1 = ""; var X9_2 = ""; var X9_3 = ""; var X9_4 = ""; var X9_5 = ""; var X9_6 = ""; var X9_7 = "";
            if (p9_1 == "1") { resultado9 = "1"; X9_1 = "X"; }
            else if (p9_1 == "2") { resultado9 = "2"; X9_2 = "X"; }
            else if (p9_1 == "3") { resultado9 = "3"; X9_3 = "X"; }
            else if (p9_1 == "4") { resultado9 = "4"; X9_4 = "X"; }
            else if (p9_1 == "5") { resultado9 = "5"; X9_5 = "X"; }
            else if (p9_1 == "6") { resultado9 = "6"; X9_6 = "X"; }
            else if (p9_1 == "7") { resultado9 = "7"; X9_7 = "X"; }
            #endregion
            #region grupo 10
            string p10_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004206") != null) { p10_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004206").v_Value1Name; }
            var resultado10 = ""; var X10_1 = ""; var X10_2 = ""; var X10_3 = ""; var X10_4 = ""; var X10_5 = ""; var X10_6 = ""; var X10_7 = "";
            if (p10_1 == "1") { resultado10 = "1"; X10_1 = "X"; }
            else if (p10_1 == "2") { resultado10 = "2"; X10_2 = "X"; }
            else if (p10_1 == "3") { resultado10 = "3"; X10_3 = "X"; }
            else if (p10_1 == "4") { resultado10 = "4"; X10_4 = "X"; }
            else if (p10_1 == "5") { resultado10 = "5"; X10_5 = "X"; }
            else if (p10_1 == "6") { resultado10 = "6"; X10_6 = "X"; }
            else if (p10_1 == "7") { resultado10 = "7"; X10_7 = "X"; }
            #endregion
            #region grupo 11
            string p11_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004207") != null) { p11_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004207").v_Value1Name; }
            var resultado11 = ""; var X11_1 = ""; var X11_2 = ""; var X11_3 = ""; var X11_4 = ""; var X11_5 = ""; var X11_6 = ""; var X11_7 = "";
            if (p11_1 == "1") { resultado11 = "1"; X11_1 = "X"; }
            else if (p11_1 == "2") { resultado11 = "2"; X11_2 = "X"; }
            else if (p11_1 == "3") { resultado11 = "3"; X11_3 = "X"; }
            else if (p11_1 == "4") { resultado11 = "4"; X11_4 = "X"; }
            else if (p11_1 == "5") { resultado11 = "5"; X11_5 = "X"; }
            else if (p11_1 == "6") { resultado11 = "6"; X11_6 = "X"; }
            else if (p11_1 == "7") { resultado11 = "7"; X11_7 = "X"; }
            #endregion
            #region grupo 12
            string p12_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004208") != null) { p12_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004208").v_Value1Name; }
            var resultado12 = ""; var X12_1 = ""; var X12_2 = ""; var X12_3 = ""; var X12_4 = ""; var X12_5 = ""; var X12_6 = ""; var X12_7 = "";
            if (p12_1 == "1") { resultado12 = "1"; X12_1 = "X"; }
            else if (p12_1 == "2") { resultado12 = "2"; X12_2 = "X"; }
            else if (p12_1 == "3") { resultado12 = "3"; X12_3 = "X"; }
            else if (p12_1 == "4") { resultado12 = "4"; X12_4 = "X"; }
            else if (p12_1 == "5") { resultado12 = "5"; X12_5 = "X"; }
            else if (p12_1 == "6") { resultado12 = "6"; X12_6 = "X"; }
            else if (p12_1 == "7") { resultado12 = "7"; X12_7 = "X"; }
            #endregion
            #region grupo 13
            string p13_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004209") != null) { p13_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004209").v_Value1Name; }
            var resultado13 = ""; var X13_1 = ""; var X13_2 = ""; var X13_3 = ""; var X13_4 = ""; var X13_5 = ""; var X13_6 = ""; var X13_7 = "";
            if (p13_1 == "1") { resultado13 = "1"; X13_1 = "X"; }
            else if (p13_1 == "2") { resultado13 = "2"; X13_2 = "X"; }
            else if (p13_1 == "3") { resultado13 = "3"; X13_3 = "X"; }
            else if (p13_1 == "4") { resultado13 = "4"; X13_4 = "X"; }
            else if (p13_1 == "5") { resultado13 = "5"; X13_5 = "X"; }
            else if (p13_1 == "6") { resultado13 = "6"; X13_6 = "X"; }
            else if (p13_1 == "7") { resultado13 = "7"; X13_7 = "X"; }
            #endregion
            #region grupo 14
            string p14_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004210") != null) { p14_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004210").v_Value1Name; }
            var resultado14 = ""; var X14_1 = ""; var X14_2 = ""; var X14_3 = ""; var X14_4 = ""; var X14_5 = ""; var X14_6 = ""; var X14_7 = "";
            if (p14_1 == "1") { resultado14 = "1"; X14_1 = "X"; }
            else if (p14_1 == "2") { resultado14 = "2"; X14_2 = "X"; }
            else if (p14_1 == "3") { resultado14 = "3"; X14_3 = "X"; }
            else if (p14_1 == "4") { resultado14 = "4"; X14_4 = "X"; }
            else if (p14_1 == "5") { resultado14 = "5"; X14_5 = "X"; }
            else if (p14_1 == "6") { resultado14 = "6"; X14_6 = "X"; }
            else if (p14_1 == "7") { resultado14 = "7"; X14_7 = "X"; }
            #endregion
            #region grupo 15
            string p15_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004211") != null) { p15_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004211").v_Value1Name; }
            var resultado15 = ""; var X15_1 = ""; var X15_2 = ""; var X15_3 = ""; var X15_4 = ""; var X15_5 = ""; var X15_6 = ""; var X15_7 = "";
            if (p15_1 == "1") { resultado15 = "1"; X15_1 = "X"; }
            else if (p15_1 == "2") { resultado15 = "2"; X15_2 = "X"; }
            else if (p15_1 == "3") { resultado15 = "3"; X15_3 = "X"; }
            else if (p15_1 == "4") { resultado15 = "4"; X15_4 = "X"; }
            else if (p15_1 == "5") { resultado15 = "5"; X15_5 = "X"; }
            else if (p15_1 == "6") { resultado15 = "6"; X15_6 = "X"; }
            else if (p15_1 == "7") { resultado15 = "7"; X15_7 = "X"; }
            #endregion
            #region grupo 16
            string p16_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004212") != null) { p16_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004212").v_Value1Name; }
            var resultado16 = ""; var X16_1 = ""; var X16_2 = ""; var X16_3 = ""; var X16_4 = ""; var X16_5 = ""; var X16_6 = ""; var X16_7 = "";
            if (p16_1 == "1") { resultado16 = "1"; X16_1 = "X"; }
            else if (p16_1 == "2") { resultado16 = "2"; X16_2 = "X"; }
            else if (p16_1 == "3") { resultado16 = "3"; X16_3 = "X"; }
            else if (p16_1 == "4") { resultado16 = "4"; X16_4 = "X"; }
            else if (p16_1 == "5") { resultado16 = "5"; X16_5 = "X"; }
            else if (p16_1 == "6") { resultado16 = "6"; X16_6 = "X"; }
            else if (p16_1 == "7") { resultado16 = "7"; X16_7 = "X"; }
            #endregion
            #region grupo 17
            string p17_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004213") != null) { p17_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004213").v_Value1Name; }
            var resultado17 = ""; var X17_1 = ""; var X17_2 = ""; var X17_3 = ""; var X17_4 = ""; var X17_5 = ""; var X17_6 = ""; var X17_7 = "";
            if (p17_1 == "1") { resultado17 = "1"; X17_1 = "X"; }
            else if (p17_1 == "2") { resultado17 = "2"; X17_2 = "X"; }
            else if (p17_1 == "3") { resultado17 = "3"; X17_3 = "X"; }
            else if (p17_1 == "4") { resultado17 = "4"; X17_4 = "X"; }
            else if (p17_1 == "5") { resultado17 = "5"; X17_5 = "X"; }
            else if (p17_1 == "6") { resultado17 = "6"; X17_6 = "X"; }
            else if (p17_1 == "7") { resultado17 = "7"; X17_7 = "X"; }
            #endregion
            #region grupo 18
            string p18_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004214") != null) { p18_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004214").v_Value1Name; }
            var resultado18 = ""; var X18_1 = ""; var X18_2 = ""; var X18_3 = ""; var X18_4 = ""; var X18_5 = ""; var X18_6 = ""; var X18_7 = "";
            if (p18_1 == "1") { resultado18 = "1"; X18_1 = "X"; }
            else if (p18_1 == "2") { resultado18 = "2"; X18_2 = "X"; }
            else if (p18_1 == "3") { resultado18 = "3"; X18_3 = "X"; }
            else if (p18_1 == "4") { resultado18 = "4"; X18_4 = "X"; }
            else if (p18_1 == "5") { resultado18 = "5"; X18_5 = "X"; }
            else if (p18_1 == "6") { resultado18 = "6"; X18_6 = "X"; }
            else if (p18_1 == "7") { resultado18 = "7"; X18_7 = "X"; }
            #endregion
            #region grupo 19
            string p19_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004215") != null) { p19_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004215").v_Value1Name; }
            var resultado19 = ""; var X19_1 = ""; var X19_2 = ""; var X19_3 = ""; var X19_4 = ""; var X19_5 = ""; var X19_6 = ""; var X19_7 = "";
            if (p19_1 == "1") { resultado19 = "1"; X19_1 = "X"; }
            else if (p19_1 == "2") { resultado19 = "2"; X19_2 = "X"; }
            else if (p19_1 == "3") { resultado19 = "3"; X19_3 = "X"; }
            else if (p19_1 == "4") { resultado19 = "4"; X19_4 = "X"; }
            else if (p19_1 == "5") { resultado19 = "5"; X19_5 = "X"; }
            else if (p19_1 == "6") { resultado19 = "6"; X19_6 = "X"; }
            else if (p19_1 == "7") { resultado19 = "7"; X19_7 = "X"; }
            #endregion
            #region grupo 20
            string p20_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004216") != null) { p20_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004216").v_Value1Name; }
            var resultado20 = ""; var X20_1 = ""; var X20_2 = ""; var X20_3 = ""; var X20_4 = ""; var X20_5 = ""; var X20_6 = ""; var X20_7 = "";
            if (p20_1 == "1") { resultado20 = "1"; X20_1 = "X"; }
            else if (p20_1 == "2") { resultado20 = "2"; X20_2 = "X"; }
            else if (p20_1 == "3") { resultado20 = "3"; X20_3 = "X"; }
            else if (p20_1 == "4") { resultado20 = "4"; X20_4 = "X"; }
            else if (p20_1 == "5") { resultado20 = "5"; X20_5 = "X"; }
            else if (p20_1 == "6") { resultado20 = "6"; X20_6 = "X"; }
            else if (p20_1 == "7") { resultado20 = "7"; X20_7 = "X"; }
            #endregion
            #region grupo 21
            string p21_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004217") != null) { p21_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004217").v_Value1Name; }
            var resultado21 = ""; var X21_1 = ""; var X21_2 = ""; var X21_3 = ""; var X21_4 = ""; var X21_5 = ""; var X21_6 = ""; var X21_7 = "";
            if (p21_1 == "1") { resultado21 = "1"; X21_1 = "X"; }
            else if (p21_1 == "2") { resultado21 = "2"; X21_2 = "X"; }
            else if (p21_1 == "3") { resultado21 = "3"; X21_3 = "X"; }
            else if (p21_1 == "4") { resultado21 = "4"; X21_4 = "X"; }
            else if (p21_1 == "5") { resultado21 = "5"; X21_5 = "X"; }
            else if (p21_1 == "6") { resultado21 = "6"; X21_6 = "X"; }
            else if (p21_1 == "7") { resultado21 = "7"; X21_7 = "X"; }
            #endregion
            #region grupo 22
            string p22_1 = "";
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004218") != null) { p22_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004218").v_Value1Name; }
            var resultado22 = ""; var X22_1 = ""; var X22_2 = ""; var X22_3 = ""; var X22_4 = ""; var X22_5 = ""; var X22_6 = ""; var X22_7 = "";
            if (p22_1 == "1") { resultado22 = "1"; X22_1 = "X"; }
            else if (p22_1 == "2") { resultado22 = "2"; X22_2 = "X"; }
            else if (p22_1 == "3") { resultado22 = "3"; X22_3 = "X"; }
            else if (p22_1 == "4") { resultado22 = "4"; X22_4 = "X"; }
            else if (p22_1 == "5") { resultado22 = "5"; X22_5 = "X"; }
            else if (p22_1 == "6") { resultado22 = "6"; X22_6 = "X"; }
            else if (p22_1 == "7") { resultado22 = "7"; X22_7 = "X"; }
            #endregion
            #region grupo resultado
            var resultadofinal = int.Parse(resultado.ToString()) + int.Parse(resultado2.ToString()) + int.Parse(resultado3.ToString()) + int.Parse(resultado4.ToString()) + int.Parse(resultado5.ToString()) + int.Parse(resultado6.ToString()) + int.Parse(resultado7.ToString()) + int.Parse(resultado8.ToString()) + int.Parse(resultado9.ToString()) + int.Parse(resultado10.ToString()) + int.Parse(resultado11.ToString()) + int.Parse(resultado12.ToString()) + int.Parse(resultado13.ToString()) + int.Parse(resultado14.ToString()) + int.Parse(resultado15.ToString()) + int.Parse(resultado16.ToString()) + int.Parse(resultado17.ToString()) + int.Parse(resultado18.ToString()) + int.Parse(resultado19.ToString()) + int.Parse(resultado20.ToString()) + int.Parse(resultado21.ToString()) + int.Parse(resultado22.ToString());
            #endregion           
            cells = new List<PdfPCell>()
                   {      
                    //fila                     
                    
                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("1. Me siento emocionalmente defraudado en mi trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan=5, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(resultado, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                   
                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("2. Cuando termino mi jornada de trabajo me siento agotado ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan=5, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                   
                    new PdfPCell(new Phrase(resultado2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("3. Cuando me levanto por la mañana y me enfrento a otra jornada de trabajo me siento agotado ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(x3_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },


                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("4. Siento que puedo entender fácilmente a las personas que tengo que atender", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X4_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },


                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("5. Siento que estoy tratando a algunos beneficiados de mí, como si fuesen objetos impersonales ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X5_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },


                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("6. Siento que trabajar todo el día con la gente me cansa ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X6_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },


                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("7. Siento que trato con mucha efectividad los problemas de las personas a las que tengo que atender  ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X7_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("8. Siento que mi trabajo me está desgastando ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X8_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },


                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("9. Siento que estoy influyendo positivamente en las vidas de otras personas a través de mi trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X9_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("10. Siento que me he hecho más duro con la gente ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X10_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X10_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X10_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X10_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X10_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    
                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("11. Me preocupa que este trabajo me está endureciendo emocionalmente ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X11_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X11_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X11_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X11_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X11_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("12. Me siento muy enérgico en mi trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X12_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X12_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X12_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X12_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X12_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
    
                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("13. Me siento frustrado por el trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X13_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X13_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X13_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X13_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X13_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("14. Siento que estoy demasiado tiempo en mi trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X14_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X14_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X14_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X14_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X14_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("15. Siento que realmente no me importa lo que les ocurra a las personas a las que tengo que atender profesionalmente ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X15_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X15_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X15_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X15_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X15_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("16. Siento que trabajar en contacto directo con la gente me cansa  ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X16_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X16_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X16_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X16_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X16_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("17. Siento que puedo crear con facilidad un clima agradable en mi trabajo  ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X17_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X17_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X17_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X17_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X17_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("18.  Me siento estimulado después de haber trabajado íntimamente con quienes tengo que atender ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X18_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X18_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X18_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X18_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X18_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("19. Creo que consigo muchas cosas valiosas en este trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X19_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X19_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X19_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X19_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X19_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("20. Me siento como si estuviera al límite de mis posibilidades ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X20_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X20_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X20_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X20_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X20_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("21. Siento que en mi trabajo los problemas emocionales son tratados de forma adecuada ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X21_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X21_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X21_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X21_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X21_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },

                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("22. Me parece que los beneficiarios de mi trabajo me culpan de algunos problemas ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X22_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X22_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X22_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X22_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X22_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                     #region pregunta resultado
                    new PdfPCell(new Phrase("Total :   ", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT,  VerticalAlignment=PdfPCell.ALIGN_MIDDLE, Colspan=2 },   
                    new PdfPCell(new Phrase(resultadofinal.ToString(), fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, Colspan=5  },
                    #endregion
                   };

            columnWidths = new float[] { 5f, 55f, 8f, 8f, 8f, 8f, 8f};

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Texto02
            cells = new List<PdfPCell>()
                   {      
                    //fila    
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("A. E.  AGOTAMIENTO EMOCIONAL", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase("D.      DESPERSONALIZAICÓN", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase("R. P.  REALIZACIÓN PERSONAL", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    
                    
                   };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            document.Close();
        }
    }
}
