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
   public class AgendaDetallada
    {
       private static void RunFile(string filePDF)
       {
           Process proceso = Process.Start(filePDF);
           proceso.WaitForExit();
           proceso.Close();
       }

       public static void CreateAgendaDetallada(List<CalendarDetail> Agenda, string filePDF)
       {
           Document document = new Document();
           document.SetPageSize(iTextSharp.text.PageSize.A4);

           PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
           pdfPage page = new pdfPage();
           writer.PageEvent = page;
           document.Open();
           var tamaño_celda = 12f;
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
           string[] columnHeaders = null;


           PdfPTable filiationWorker = new PdfPTable(8);
           PdfPTable table = null;
           PdfPCell cell = null;

           #endregion

           #region Title
           cells = new List<PdfPCell>()
                   {    
                    new PdfPCell(new Phrase("AGENDA DETALLADA", fontTitle1)){Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},       
                  
                   };

           columnWidths = new float[] { 15f, 5f, 30f, 25f, 20f, 5f };

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
           document.Add(table);

           #endregion

           #region Detalle
           cells = new List<PdfPCell>();
           foreach (var servicio in Agenda)
           {
               cell = new PdfPCell(new Phrase(servicio.Pacient, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
               cells.Add(cell);             
           }
           columnWidths = new float[] { 100f };

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro);

           document.Add(table);
           #endregion

           document.Close();

       }
    }
}