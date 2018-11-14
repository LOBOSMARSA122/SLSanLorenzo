using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.PropertyGridInternal;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;
using Font = iTextSharp.text.Font;

namespace NetPdf
{
    public class Liquidacion_EMO_EMPRESAS
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateLiquidacion_EMO_EMPRESAS(string filePDF,
            organizationDto infoEmpresaPropietaria, List<LiquidacionEmpresa> Listaliq, string fechaInicio, string fechaFin)
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);

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

            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion

            var estatico_1 = 15f;
            var alto_Celda_1 = 15f;
            var alto_Celda_2 = 30f;
            var alto_Celda_3 = 45f;
            var alto_Celda_4 = 60f;
            var alto_Celda_6 = 90f;
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
            var tamaño_celda = 15f;

            var cellsTit = new List<PdfPCell>()
                { 
                    //new PdfPCell(new Phrase("REPORTE DE EMPRESAS Y SALDOS", fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("CONSOLIDADO DE CUENTAS X COBRAR AL ", fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 7f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("DEL "+ fechaInicio + " AL " + fechaFin, fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 7f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Parte Dinámica
            cells = new List<PdfPCell>();
            //int tamañoTickets = 0;
            int nroreco = 1;
            decimal debe = 0;
            decimal debe_1 = 0;
            decimal debe_2 = 0;
            decimal debe_3 = 0;
            decimal pago = 0;
            decimal pago_1 = 0;
            decimal pago_2 = 0;
            decimal pago_3 = 0;
            decimal subtotal = 0;
            decimal subtotal_1 = 0;
            decimal subtotal_2 = 0;
            decimal subtotal_3 = 0;

            foreach (var liq in Listaliq)
            {
                cell = new PdfPCell(new Phrase(liq.v_OrganizationName, fontColumnValueBold)) { BackgroundColor = BaseColor.GRAY, Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase("N°", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("LIQUIDACIÓN", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("DEBE", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("PAGO", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL  S/. ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);
               
                foreach (var item in liq.detalle)
                {
                    cell = new PdfPCell(new Phrase(nroreco.ToString() + ". ", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.v_NroLiquidacion, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    decimal _d_Debe = (decimal)item.d_Debe;
                    _d_Debe = decimal.Round(_d_Debe, 2);
                    cell = new PdfPCell(new Phrase(_d_Debe.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    decimal _d_Pago = (decimal)item.d_Pago;
                    _d_Pago = decimal.Round(_d_Pago, 2);
                    cell = new PdfPCell(new Phrase(_d_Pago.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    decimal _d_Total = (decimal)item.d_Total;
                    _d_Total = decimal.Round(_d_Total, 2);
                    cell = new PdfPCell(new Phrase(_d_Total.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    debe += item.d_Debe.Value;
                    pago += item.d_Pago.Value;
                    subtotal += item.d_Total.Value;
                    nroreco++;
                }
                debe_1 = debe;
                decimal _debe1 = decimal.Round(debe_1, 2);
                pago_1 = pago;
                decimal _pago1 = decimal.Round(pago_1, 2);
                subtotal_1 = subtotal;
                decimal _subtotal_1 = decimal.Round(subtotal_1, 2);

                cell = new PdfPCell(new Phrase("TOTAL EMPRESA   S/.", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(_debe1.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(_pago1.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(_subtotal_1.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 10f };
                cells.Add(cell);

                debe_2 += debe_1;
                pago_2 += pago_1;
                subtotal_2 += subtotal_1;

                debe = 0;
                debe_1 = 0;
                pago = 0;
                pago_1 = 0;
                subtotal = 0;
                subtotal_1 = 0;
            }

            debe_3 = debe_2;
            pago_3 = pago_2;
            subtotal_3 = subtotal_2;
            debe_3 = decimal.Round(debe_3, 2);
            pago_3 = decimal.Round(pago_3, 2);
            subtotal_3 = decimal.Round(subtotal_3, 2);
            cell = new PdfPCell(new Phrase("TOTAL GLOBAL", fontColumnValueBold)) { BackgroundColor = BaseColor.GRAY, Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase(debe_3.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase(pago_3.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase(subtotal_3.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            columnWidths = new float[] { 4f, 25f, 5f, 10f, 8f, 18f, 15f, 8f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);
        }
    }
}
