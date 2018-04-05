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
   public class HojaCotizacion
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();

        }

        public static void CrearHojaCotizacion(List<CotizacionProtocolo> lCotizacion, string EmpresaCliente, OrganizationList infoEmpresaPropietaria, string filePDF)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //try
            //{NO_BORDER
            // step 2: we create aPA writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
            //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
            writer.PageEvent = page;

            // step 3: we open the document
            document.Open();
            // step 4: we Add content to the document
            // we define some fonts
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

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region Title
            //List<PdfPCell> cells = null;
            PdfPCell CellLogo = null;
            //float[] columnWidths = null;
            cells = new List<PdfPCell>();
            PdfPCell cellPhoto1 = null;

            //if (filiationData.b_Photo != null)
            //    cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
            //else
            //    cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            columnWidths = new float[] { 100f };

            var cellsTit = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("PROFORMA SERVICIOS DE SALUD OCUPACIONAL ", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("CLÍNICA HOLOSALUD", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(" ", fontTitle2))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
              
            };

            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(table));
            //cells.Add(cellPhoto1);

            columnWidths = new float[] { 20f, 80f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            document.Add(new Paragraph("\r\n"));

            #endregion

            cells = new List<PdfPCell>()
               {
                    new PdfPCell(new Phrase("PROFORMA SERVICIOS DE SALUD OCUPACIONAL", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER},    
                    new PdfPCell(new Phrase("DETALLE DE SERVICIOS SOLICITADOS SEGÚN RM N°312-2011 MINSA EXAMENES", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER},    
                    new PdfPCell(new Phrase("MEDICOS SOLICITADOS", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER},    
               };

            columnWidths = new float[] { 100f};

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);

            document.Add(filiationWorker);
            document.Add(new Paragraph("\r\n"));

            #region Parte Dinámica

           

            if (lCotizacion != null)
            {

                foreach (var item in lCotizacion)
                {
                    cells = new List<PdfPCell>();
                    var x = item.Detalle.Sum(p => p.costo);
                    var Igv = x * 0.18;
                    var Total = Igv + x;
                    //Columna Protocolo
                    cell = new PdfPCell(new Phrase(item.ProtocoloNombre + " Precio: S/. "+ x.ToString() + " I.G.V: S/." + Igv + " Total: S/." +Total.ToString(), fontColumnValueNegrita)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_LEFT };
                    cells.Add(cell);

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

                    document.Add(table);

                    if (item.Detalle.Count > 0)
                    {
                        List<PdfPCell> cells1 = null;
                        cells1 = new List<PdfPCell>();
                        //string Cadena = "";
                        foreach (var item1 in item.Detalle)
                        {
                            PdfPCell cell1 = null;
                            cell1 = new PdfPCell(new Phrase(item1.ComponenteNombre, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_LEFT };
                            cells1.Add(cell1);

                            string Costo = "S/." + item1.costo.ToString();
                            cell1 = new PdfPCell(new Phrase(Costo, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
                            cells1.Add(cell1);

                        }
                    
                        columnWidths = new float[] { 50f, 50f };

                        PdfPTable table1 = null;
                        columnHeaders = new string[] { "EXÁMENES", "PRECIO"};

                        table1 = HandlingItextSharp.GenerateTableFromCells(cells1, columnWidths, null, fontTitleTable, columnHeaders);

                        document.Add(table1);

                        document.Add(new Paragraph("\r\n"));

                    }

                 
                }
          
            }


           
            #endregion


            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
