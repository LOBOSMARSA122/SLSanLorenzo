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
    public class Informeintensidadfatiga
    {
        public static void CreateInformeintensidadfatiga(ServiceList DataService, organizationDto infoEmpresaPropietaria, List<ServiceComponentFieldValuesList> valores, string filePDF)
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
                    new PdfPCell(new Phrase("ESCALA DE INTENSIDAD DE FATIGA", fontTitle1)){Rowspan =2,Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},       
                  
                   };

            columnWidths = new float[] { 15f, 5f, 30f, 25f, 20f, 5f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion
            var tamaño_celda = 27f;
            var tamaño_celda1 = 43f;
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
                    new PdfPCell(new Phrase(DataService.i_Edad.ToString(), fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
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

            #region Texto01
            
            cells = new List<PdfPCell>()
                   {      
                    //fila                       
                    new PdfPCell(new Phrase("Conteste a las siguientes afirmaciones, segùn los criterios de la siguiente escala", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                    new PdfPCell(new Phrase("TOTAL DESACUERDO 1 2 3 4 5 6 7 TOTALMENTE DE ACUERDO", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                   };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion



            #region Cuadro
            #region grupo 1
            string p1_1 = ""; 
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004187") != null) { p1_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004187").v_Value1Name; }
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
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004188") != null) { p2_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004188").v_Value1Name; }
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
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004189") != null) { p3_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004189").v_Value1Name; }
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
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004190") != null) { p4_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004190").v_Value1Name; }
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
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004191") != null) { p5_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004191").v_Value1Name; }
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
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004192") != null) { p6_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004192").v_Value1Name; }
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
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004193") != null) { p7_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004193").v_Value1Name; }
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
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004194") != null) { p8_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004194").v_Value1Name; }
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
            if (valores.Find(p => p.v_ComponentFieldId == "N009-MF000004195") != null) { p9_1 = valores.Find(p => p.v_ComponentFieldId == "N009-MF000004195").v_Value1Name; }
            var resultado9 = ""; var X9_1 = ""; var X9_2 = ""; var X9_3 = ""; var X9_4 = ""; var X9_5 = ""; var X9_6 = ""; var X9_7 = "";
            if (p9_1 == "1") { resultado9 = "1"; X9_1 = "X"; }
            else if (p9_1 == "2") { resultado9 = "2"; X9_2 = "X"; }
            else if (p9_1 == "3") { resultado9 = "3"; X9_3 = "X"; }
            else if (p9_1 == "4") { resultado9 = "4"; X9_4 = "X"; }
            else if (p9_1 == "5") { resultado9 = "5"; X9_5 = "X"; }
            else if (p9_1 == "6") { resultado9 = "6"; X9_6 = "X"; }
            else if (p9_1 == "7") { resultado9 = "7"; X9_7 = "X"; }
            #endregion
            #region grupo resultado
            var resultadofinal = int.Parse(resultado.ToString()) + int.Parse(resultado2.ToString()) + int.Parse(resultado3.ToString()) + int.Parse(resultado4.ToString()) + int.Parse(resultado5.ToString()) + int.Parse(resultado6.ToString()) + int.Parse(resultado7.ToString()) + int.Parse(resultado8.ToString()) + int.Parse(resultado9.ToString());
            #endregion           
            cells = new List<PdfPCell>()
                   {      
                    //fila     
                    #region cabecera
                    new PdfPCell(new Phrase("", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = tamaño_celda1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, BorderColorRight=BaseColor.WHITE },   
                    new PdfPCell(new Phrase("", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = tamaño_celda1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("1", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },
                    new PdfPCell(new Phrase("2", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },
                    new PdfPCell(new Phrase("3", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },   
                    new PdfPCell(new Phrase("4", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1},
                    new PdfPCell(new Phrase("5", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },    
                    new PdfPCell(new Phrase("6", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1},
                    new PdfPCell(new Phrase("7", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },    
                    #endregion
                    #region pregunta 1
                    new PdfPCell(new Phrase("1", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Mi motivaciòn se reduce cuando estoy fatigado.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X1_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X1_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 2
                    new PdfPCell(new Phrase("2", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("El ejercicion me produce fatiga.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X2_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X2_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X2_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X2_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X2_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X2_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X2_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 3
                    new PdfPCell(new Phrase("3", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me fatigo fácilmente.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(x3_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(x3_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 4
                    new PdfPCell(new Phrase("4", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga me produce con fecuencia problemas.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X4_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X4_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 5
                    new PdfPCell(new Phrase("5", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga me impide hacer ejercicio físico continuado.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X5_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X5_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 6
                    new PdfPCell(new Phrase("6", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga me impide hacer ejercicio físico continuado.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X6_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X6_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 7
                    new PdfPCell(new Phrase("7", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga interfiere en el desempeño de algunas obligaciones y responsabilidades.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X7_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X7_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 8
                    new PdfPCell(new Phrase("8", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga es uno de mis tres síntomas que más me incapacitan.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X8_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X8_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta 9
                    new PdfPCell(new Phrase("9", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga interfiere en mi trabajo, familia o vida social.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_1, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_2, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_3, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase(X9_4, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_5, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_6, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(X9_7, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    #endregion
                    #region pregunta resultado
                    new PdfPCell(new Phrase("Total :   ", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, Colspan=2 },   
                    new PdfPCell(new Phrase(resultadofinal.ToString(), fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, Colspan=7  },
                    #endregion
                   };

            columnWidths = new float[] { 5f, 39f, 8f, 8f, 8f, 8f, 8f, 8f, 8f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region Texto02

            cells = new List<PdfPCell>()
                   {      
                    //fila                       
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                   };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region Puntaje
            var interpretacion = "";
            if (int.Parse(resultadofinal.ToString()) < 36) { interpretacion = "NO PRESENTA FATIGA"; }
            else if (int.Parse(resultadofinal.ToString()) > 36) { interpretacion = "SI PRESENTA FATIGA"; }
          
            cells = new List<PdfPCell>()
                   {      
                    //fila 
                    
                    new PdfPCell(new Phrase("MEDICIÓN", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda },   
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
