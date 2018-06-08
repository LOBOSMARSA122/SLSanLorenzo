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

        public static void CreateAtencionIntegral(string filePDF)
        {
            Document document = new Document();

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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region Title
            cells = new List<PdfPCell>();

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },           
                    new PdfPCell(new Phrase("CONSULTA - NOMBRE DEL PROTOCOLO", fontTitle2)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },             
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Primera página
            #region Datos del Servicio
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("ENFERMEDAD ACTUAL", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 3 },  

                    new PdfPCell(new Phrase("Fecha: 99/99/99", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Hora: 11:11 p.m.", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Edad: 99 años", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                };

            columnWidths = new float[] { 40f, 40f, 20f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region Descarte de Signos de Peligro
            if (true) //verificar que sea niño
            {
                cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("Descarte de signos de peligro: (marcar los hallazgos) NIÑO", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.YELLOW, Colspan = 9 },
  
                    new PdfPCell(new Phrase("MENOS DE 2 MESES:", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("DE 2 MESES A 4 AÑOS:", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("PARA TODAS LAS EDADES:", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("No quiere mamar, no succiona", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("No puede beber o tomar el pecho", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Eminación visible grave", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Piel vuelve muy lentamente", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Fontanela abombada", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Letárgico o comatoso", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Traumatismo - Quemaduras", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Enrojecimiento del ombligo se extiende a la piel", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Vomita todo", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Envenenamiento", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Fiebre o temperatura baja", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Estridor en reposo - Tiraje subcostal", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Palidez palmar intenso", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Rigidez de nuca", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Pústulas muchas y extensas", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                };

                columnWidths = new float[] { 20f, 5f, 10f, 20f, 5f, 10f, 20f, 5f, 10 };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
                document.Add(table);
            }
            #endregion
            #region Consulta / Enfermedad
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("Motivo de consulta", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 3 },  
                    new PdfPCell(new Phrase("Tiempo de enfermedad", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 4 },
  
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 3 },  
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 4 }, 

                    new PdfPCell(new Phrase("Apetito", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 2 },
                    new PdfPCell(new Phrase("Sed", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 3 },

                    new PdfPCell(new Phrase("Sueño", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 2 },
                    new PdfPCell(new Phrase("Estado de ánimo", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 3 },

                    new PdfPCell(new Phrase("Orina", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 2 },
                    new PdfPCell(new Phrase("Deposiciones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 3 },

                    new PdfPCell(new Phrase("T°:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("PA:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("FC:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("FR:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Peso:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Talla:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("IMC:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Ex. Físico", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 7 },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 7 },
                };

            columnWidths = new float[] { 10f, 30f, 10f, 10f, 15f, 10f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region Adulto Mayor
            if (true)//Validar que sea adulto mayor
            {
                cellsTit = new List<PdfPCell>()
                { 

                    new PdfPCell(new Phrase("ADULTO MAYOR", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 9, BackgroundColor = BaseColor.YELLOW },

                    new PdfPCell(new Phrase("I. FUNCIONAL", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Independiente", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Dependiente", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Total", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
      
                    new PdfPCell(new Phrase("II. Mental", fontTitle2)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT,Colspan = 10 },
             
                    new PdfPCell(new Phrase("2.1 Estado Cognitivo", fontTitle2)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT, PaddingLeft = 5f },
                    new PdfPCell(new Phrase("Normal", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("DC Leve", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("DC Moderado", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
 
                    new PdfPCell(new Phrase("2.2 Estado Afectivo", fontTitle2)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT, PaddingLeft = 5f },
                    new PdfPCell(new Phrase("Sin manifestaciones depresivas", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Colspan = 3 },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Con manifestaciones depresivas", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Colspan = 3 },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("III. SOCIO-FAMILIAR", fontTitle2)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("Buena", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Riesgo social", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Problema Social", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                };

                columnWidths = new float[] { 40f,12f,3f,12f,3f,12f,3f,12f,3f };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
                document.Add(table);
            }
            #endregion
            #region Categoria del Adulto Mayor
            if (true)//Validar que sea adulto mayor
            {
                cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("CATEGORIAS DEL ADULTO MAYOR", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 4, BackgroundColor = BaseColor.YELLOW },

                    new PdfPCell(new Phrase("Saludable", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Fragil", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
      
                    new PdfPCell(new Phrase("Enfermo", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Geriatrico Complejo", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                    new PdfPCell(new Phrase("", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                };

                columnWidths = new float[] { 20f, 5f, 50f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
                document.Add(table);
            }
            #endregion
            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
