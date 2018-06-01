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
    public class AtencionIntegral
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateAtencionIntegral(string filePDF, List<ProblemasList> problemasList, List<TipoAtencionList> planIntegralList)
        {
            Document document = new Document();

            document.SetPageSize(iTextSharp.text.PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            pdfPage page = new pdfPage();
            writer.PageEvent = page;
            document.Open();

            #region Declaration Tables
            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region Title
            cells = new List<PdfPCell>();
           
            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },           
                    new PdfPCell(new Phrase("LISTA DE PROBLEMAS", fontTitle1)) { HorizontalAlignment = PdfPCell.LEFT_BORDER },             
                };
            
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,  null, fontTitleTable);
            document.Add(table);
            #endregion

            #region PROBLEMA CRÓNICOS

            var problemasCronicos = problemasList.FindAll(p => p.i_Tipo ==  (int)Sigesoft.Common.TipoProblema.Cronico);

            cells = new List<PdfPCell>();

            if (problemasCronicos != null && problemasCronicos.Count > 0)
            {
                var count = 1;
                foreach (var item in problemasCronicos)
                {
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_EsControlao, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5F, 20f, 30f, 20f, 25f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 50f, 20f, 30f };
            }
            columnHeaders = new string[] { "N°", "FECHA", "PROBLEMA CRÓNICOS", "INACTIVO", "OBSERVACIÓN" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "LISTA DE PROBLEMAS CRÓNICOS", fontTitleTable, columnHeaders);
            document.Add(table);
            #endregion

            #region PROBLEMA AGUDOS

            var problemasAgudos = problemasList.FindAll(p => p.i_Tipo == (int)Sigesoft.Common.TipoProblema.Agudo);

            cells = new List<PdfPCell>();

            if (problemasAgudos != null && problemasAgudos.Count > 0)
            {
                var count = 1;
                foreach (var item in problemasAgudos)
                {
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);       

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5f, 45f, 20f, 30f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] {100f };
            }
            columnHeaders = new string[] { "N°", "PROBLEMAS AGUDOS", "FECHA", "OBSERVACIÓN" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "LISTA DE PROBLEMAS AGUDOS", fontTitleTable, columnHeaders);
            document.Add(table);
            
            #endregion            

            #region PLAN DE ATENCIÓN INTEGRAL

            cells = new List<PdfPCell>();
            int nro = 1;
            foreach (var plan in planIntegralList)
            {
                columnWidths = new float[] { 30f, 20f, 20f };
                include = "v_Descripcion,d_Fecha,v_Lugar";

                cell = new PdfPCell(new Phrase(nro.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(plan.Value, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);

                table = HandlingItextSharp.GenerateTableFromList(plan.List, columnWidths, include, fontColumnValue);
                cell = new PdfPCell(table);
                cells.Add(cell);

                nro += 1;
            }
            columnWidths = new float[] { 5f, 25f, 70f};
             
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "PLAN DE ATENCIÓN INTEGRAL", fontTitleTable);
            document.Add(table);

            #endregion

            document.Close();
            RunFile(filePDF);
        }

    }
}
