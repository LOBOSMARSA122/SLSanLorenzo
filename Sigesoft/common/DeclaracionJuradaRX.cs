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
    public class DeclaracionJuradaRX
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateDeclaracionJurada(PacientList filiationData, string filePDF,
            PacientList datosPac,
            organizationDto infoEmpresaPropietaria,
            List<ServiceComponentList> exams,
            List<DiagnosticRepositoryList> Diagnosticos,
            List<ServiceComponentList> serviceComponent)
        {
            Document document = new Document(PageSize.A4, 40f, 40f, 80f, 50f);


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
            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Fonts
            Font fontTitle1 = FontFactory.GetFont("Calibri", 18, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);


            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("DECLARACIÓN JURADA", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor=BaseColor.WHITE },
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            var tamaño_celda = 25f;
            string tipodoc = "";
            if (datosPac.i_DocTypeId == 1) { tipodoc = "DNI"; }
            else if (datosPac.i_DocTypeId == 2) { tipodoc = "Pasaporte"; }
            else if (datosPac.i_DocTypeId == 3) { tipodoc = "Licencia de Conducir"; }
            else if (datosPac.i_DocTypeId == 4) { tipodoc = "Carnet de Extranjeria"; }

            ServiceComponentList declaracion_jurada = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXCEPCIONES_RX_AUTORIZACION_ID);
            var fecha_regla = declaracion_jurada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXCEPCIONES_RX_FECHA_ULTIMA_REGLA) == null ? "" : declaracion_jurada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXCEPCIONES_RX_FECHA_ULTIMA_REGLA).v_Value1;
            
            #region Contenido
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("\n \n YO :                  " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE},    
               
                new PdfPCell(new Phrase("\nIdentificado (a) con " + tipodoc + " N°  " + datosPac.v_DocNumber+" DECLARO BAJO JURAMENTO", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
                
                new PdfPCell(new Phrase("que la fecha de mi última regla se inició el: " +fecha_regla+"; por tal razón doy de mi consentimiento", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
                
                new PdfPCell(new Phrase("para que se me practique el procedimmiento de Radiología respectivo (Radiografía de Tórax),", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
                
                new PdfPCell(new Phrase("exonerando de responsabilidad alguna a: "+ infoEmpresaPropietaria.v_Name +" , por dicho examen.", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
                
            
            
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
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

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 100, 35));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 60, 80));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            #region Fecha / Firmma
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("\n \n \n \n "+infoEmpresaPropietaria.v_SectorName+", "+ fechaServicio[0] + " de " + mes + " del " + fechaServicio[2], fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE },    
                
                new PdfPCell(cellFirmaTrabajador ) {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=180, BorderColor=BaseColor.WHITE},
                new PdfPCell(cellHuellaTrabajador ) {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=180, BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("_____________________________________", fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("           Firma del Paciente        ", fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},

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
