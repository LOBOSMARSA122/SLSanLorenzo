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

        public static void CreateAtencionIntegral(string filePDF, List<ProblemasList> problemasList)
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

            #region PROBLEMA CRONICOS

            var problemasCronicos = problemasList.FindAll(p => p.i_Tipo ==  (int)Sigesoft.Common.TipoProblema.Cronico);


            //var fecha = problemasCronicos.Find(p => p.d_Fecha );

                cells = new List<PdfPCell>();

                cells.Add(new PdfPCell(new Phrase("N°", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("PROBLEMA CRONICOS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("INACTIVO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("OBSERVACIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                   
              // cells.Add(new PdfPCell(new Phrase(TrigliceridoValord == null ? string.Empty : TrigliceridoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
              
               // cells.Add(new PdfPCell(new Phrase(problemasCronicos, fontColumnValueBold)) {Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER });

                //cells.Add(new PdfPCell(new Phrase(TrigliceridoValord == null ? string.Empty : TrigliceridoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
              
             
                
            
                


            columnWidths = new float[] { 5F, 20f, 30f, 20f, 25f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
            document.Add(table);
            
            #endregion

            #region PROBLEMAS AGUDOS
            var problemasAgudo = problemasList.FindAll(p => p.i_Tipo == (int)Sigesoft.Common.TipoProblema.Agudo);

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("N°", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("PROBLEMA AGUDOS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("OBSERVACIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });


            columnWidths = new float[] { 15F, 40f, 20f, 25f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
            document.Add(table);
            #endregion

            #region SubTitle 

            cells = new List<PdfPCell>()
                { 
                     new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL", fontTitle1)) { HorizontalAlignment = PdfPCell.LEFT_BORDER },             
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion
            
            #region Evaluacion General

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            
            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "EVALUACIÓN GENERAL, CRECIMIENTO Y DESARROLLO ", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

            #region Inmunizaciones

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "INMUNIZACIONES ", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

            #region Evaluacion Bucal

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "EVALUACIÓN BUCAL ", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

            #region Integracion Preventivas

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "INTEGRACIÓN PREVENTIVAS ", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

            #region Administracion de micronutrientes

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "ADMINISTRACIÓN DE MICRONUTRIENTES", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

            #region Consejeria Integral

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "CONSEJERIA INTEGRAL", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

            #region Vista Domiciliara

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "Vista Domiciliaria", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

            #region Temas Educativos

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "TEMAS EDUCATIVOS", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

            #region Propiedad Sanitaria

            cells = new List<PdfPCell>();

            cells.Add(new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("LUGAR", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 50f, 25f, 25f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "ATENCIÓN DE PRIORIDADES SANITARIAS", fontTitleTable);
            document.Add(filiationWorker);

            #endregion

        }

    }
}
