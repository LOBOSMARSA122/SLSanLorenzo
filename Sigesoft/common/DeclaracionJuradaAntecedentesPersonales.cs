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
    public class DeclaracionJuradaAntecedentesPersonales
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateDeclaracionJuradaAntecedentesPersonales(ServiceList DataService,
           List<ServiceComponentList> serviceComponent,
           organizationDto infoEmpresa,
           PacientList datosPac,
           string filePDF)
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 45f, 38f);

            document.SetPageSize(iTextSharp.text.PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            pdfPage page = new pdfPage();
            writer.PageEvent = page;
            document.Open();

            #region Declaration Tables
            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.Gray);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
            string[] columnValues = null;
            string[] columnHeaders = null;
            PdfPTable header2 = new PdfPTable(6);
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.WidthPercentage = 100;
            float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            header2.SetWidths(widths1);
            PdfPTable companyData = new PdfPTable(6);
            companyData.HorizontalAlignment = Element.ALIGN_CENTER;
            companyData.WidthPercentage = 100;
            float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            companyData.SetWidths(widthscolumnsCompanyData);
            PdfPTable filiationWorker = new PdfPTable(4);
            PdfPTable table = null;
            PdfPCell cell = null;
            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Fonts
            Font fontTitle1 = FontFactory.GetFont("Calibri", 15, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresa.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 785);
                document.Add(imagenEmpresa);
            }
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("DECLARACIÓN JURADA DE ANTECEDENTES PERSONALES", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 40f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion
            ServiceComponentList autorizacion = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_ID);

            var tamaño_cuerpo = 22f;
            var tamaño_celda = 18f;

            #region Contenido
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\nYO," , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n" + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)){ Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n,de    " + datosPac.Edad.ToString() + "    años de edad y fecha de ", fontColumnValue)){ Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
                
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("nacimiento  " , fontColumnValue)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase( datosPac.d_Birthdate.ToString().Split(' ')[0] ,fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("  con DNI N°    " , fontColumnValue)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase( datosPac.v_DocNumber,fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("  domiciliado en  " , fontColumnValue)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase( datosPac.v_AdressLocation,fontColumnValue)) { Colspan =10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("    ;DECLARO BAJO JURAMENTO " , fontColumnValueBold)){ Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("que los datos consignados en el presente documento son correctos y completos, y me hago" , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("responsable si he falseado u ocultado la verdad, liberando así de responsabilidad a COSAPI;  " , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("así mismo quedo obligado a informar toda circunstancia nueva que pueda influir o alterar" , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("esta información." , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    

            };

            columnWidths = new float[] { 2f, 6f, 5f, 5f, 6f, 5f, 6f, 5f, 5f, 5f, 5f, 5f, 6f, 5f, 6f, 5f, 6f, 5f, 5f, 2f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Cuestionario
            #region respuestas
            var p1 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_1) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_1).v_Value1;
            string p1Si = "", p1No = "";
            if (p1 == "1") { p1Si = "X"; p1No = " "; }
            else if (p1 == "0") { p1Si = " "; p1No = "X"; }

            var p2 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_2) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_2).v_Value1;
            string p2Si = "", p2No = "";
            if (p2 == "1") { p2Si = "X"; p2No = " "; }
            else if (p2 == "0") { p2Si = " "; p2No = " "; }

            var p3 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_3) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_3).v_Value1;
            string p3Si = "", p3No = "";
            if (p3 == "1") { p3Si = "X"; p3No = " "; }
            else if (p3 == "0") { p3Si = " "; p3No = "X"; }

            var p4 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_4) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_4).v_Value1;
            string p4Si = "", p4No = "";
            if (p4 == "1") { p4Si = "X"; p4No = " "; }
            else if (p4 == "0") { p4Si = " "; p4No = "X"; }

            var p5 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_5) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_5).v_Value1;
            string p5Si = "", p5No = "";
            if (p5 == "1") { p5Si = "X"; p5No = " "; }
            else if (p5 == "0") { p5Si = " "; p5No = "X"; }

            var p6 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_6) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_6).v_Value1;
            string p6Si = "", p6No = "";
            if (p6 == "1") { p6Si = "X"; p6No = " "; }
            else if (p6 == "0") { p6Si = " "; p6No = "X"; }

            var p7 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_7) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_7).v_Value1;
            string p7Si = "", p7No = "";
            if (p7 == "1") { p7Si = "X"; p7No = " "; }
            else if (p7 == "0") { p7Si = " "; p7No = "X"; }

            var p8 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_8) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_8).v_Value1;
            string p8Si = "", p8No = "";
            if (p8 == "1") { p8Si = "X"; p8No = " "; }
            else if (p8 == "0") { p8Si = " "; p8No = "X"; }

            var p9 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_9) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_9).v_Value1;
            string p9Si = "", p9No = "";
            if (p9 == "1") { p9Si = "X"; p9No = " "; }
            else if (p9 == "0") { p9Si = " "; p9No = "X"; }

            var p10 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_10) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_10).v_Value1;
            string p10Si = "", p10No = "";
            if (p10 == "1") { p10Si = "X"; p10No = " "; }
            else if (p10 == "0") { p10Si = " "; p10No = "X"; }

            var p11 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_11) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_11).v_Value1;
            string p11Si = "", p11No = "";
            if (p11 == "1") { p11Si = "X"; p11No = " "; }
            else if (p11 == "0") { p11Si = " "; p11No = "X"; }

            var p12 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_12) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_12).v_Value1;
            string p12Si = "", p12No = "";
            if (p12 == "1") { p12Si = "X"; p12No = " "; }
            else if (p12 == "0") { p12Si = " "; p12No = "X"; }

            var p13 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_13) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_13).v_Value1;
            string p13Si = "", p13No = "";
            if (p13 == "1") { p13Si = "X"; p13No = " "; }
            else if (p13 == "0") { p13Si = " "; p13No = "X"; }

            var p14 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_14) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_14).v_Value1;
            string p14Si = "", p14No = "";
            if (p14 == "1") { p14Si = "X"; p14No = " "; }
            else if (p14 == "0") { p14Si = " "; p14No = "X"; }

            var p15 = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_15) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_15).v_Value1;
            string p15Si = "", p15No = "";
            if (p15 == "1") { p15Si = "X"; p15No = " "; }
            else if (p15 == "0") { p15Si = " "; p15No = "X"; }
            var esp = autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_espc) == null ? "" : autorizacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_espc).v_Value1;
            #endregion
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("¿Padece o ha padecido de alguna de las enfermedades mencionadas a continuación?" , fontColumnValue)){ Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("SI" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("NO" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("1. Enfermedades respiratorias (dificultad para respirar, asma, otras): " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p1Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p1No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("2. Enfermedades cardiovasculares que causen: palpitaciones, dolor de pecho, HTA: " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p2Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p2No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("3. Enfermedades digestivas (úlcera, gastritis): " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p3Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p3No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("4. Enfermedades endocrinológicas (diabetes, hipertiroidismo, hipotiroidismo):" , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p4Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p4No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("5. Torceduras, fracturas, luxaciones los últimos 60 días: " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p5Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p5No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("6. Enfermedades oftalmológicas que disminuyan la visión: " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p6Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p6No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("7. Enfermedades Neurológicas (Epilepsia, Parkinson, meningitis): " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p7Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p7No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("8. Enfermedades Musculoesqueléticas (Lumbalgia, tendinitis, dolores musculares): " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p8Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p8No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("9. Enfermedades psiquiátricas: " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p9Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p9No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("10. Cirugías (los últimos 60 días): " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p10Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p10No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("11. Alcoholismo o consumo de drogas: " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p11Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p11No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("12. Uso de medicamentos en forma permanente:" , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p12Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p12No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("13. Uso de medicamentos en forma continuada (últimos 15 días) : " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p13Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p13No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("14. Reacciones alérgicas " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p14Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p14No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("15. Otras enfermedades: " , fontColumnValue1)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p15Si + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("( " + p15No + " )" , fontColumnValue1)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("\nEspecificar:  ", fontColumnValue1)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("\n"+esp == null?"NO HAY ESPECIFICACIONES":esp==""?"NO HAY ESPECIFICACIONES":esp , fontColumnValue1)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

            };

            columnWidths = new float[] { 3f, 7f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 7f, 5f, 5f, 5f, 5f, 3f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            string[] fechaServicio = datosPac.FechaServicio.ToString().Split('/', ' ');
            string mes = "";
            if (fechaServicio[1] == "01") mes = "Enero";
            else if (fechaServicio[1] == "02") mes = "Febrero";
            else if (fechaServicio[1] == "03") mes = "Marzo";
            else if (fechaServicio[1] == "04") mes = "Abril";
            else if (fechaServicio[1] == "05") mes = "Mayo";
            else if (fechaServicio[1] == "06") mes = "Junio";
            else if (fechaServicio[1] == "07") mes = "Julio";
            else if (fechaServicio[1] == "08") mes = "Agosto";
            else if (fechaServicio[1] == "09") mes = "Setiembre";
            else if (fechaServicio[1] == "10") mes = "Octubre";
            else if (fechaServicio[1] == "11") mes = "Noviembre";
            else if (fechaServicio[1] == "12") mes = "Diciembre";

            PdfPCell cellFirmaTrabajador = null;

            PdfPCell cellHuellaTrabajador = null;

            if (DataService.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(DataService.FirmaTrabajador, null, null, 100, 40));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (DataService.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(DataService.HuellaTrabajador, null, null, 40, 60));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            #region Fecha / Firmmal
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("\n \n  "+infoEmpresa.v_SectorName+", "+ fechaServicio[0] + " de " + mes + " del " + fechaServicio[2], fontColumnValue2))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE },    
                
                new PdfPCell(cellFirmaTrabajador ) {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=80, BorderColor=BaseColor.WHITE},
                new PdfPCell(cellHuellaTrabajador ) {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=80, BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("_____________________________________", fontColumnValue2)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("Firma del Paciente", fontColumnValue2)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("N° de DNI: " + datosPac.v_DocNumber, fontColumnValue2)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},

              };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
