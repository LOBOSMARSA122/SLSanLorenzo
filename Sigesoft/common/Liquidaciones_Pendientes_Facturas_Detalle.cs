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
    public class Liquidaciones_Pendientes_Facturas_Detalle
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateLiquidacion_EMO(string filePDF,
            organizationDto infoEmpresaPropietaria, List<LiquidacionEmpresa> Lista, string fechaInicio, string fechaFin)
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

            Font fontColumnValue = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion

            #region TÍTULO

            cells = new List<PdfPCell>();
            
            var tamaño_celda = 15f;

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("LIQUIDACIONES DE EMPRESAS PENDIENTES DE FACTURAR AL " + fechaFin, fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            
            #region Parte Dinámica
            cells = new List<PdfPCell>();
            //int tamañoTickets = 0;
            int nroreco = 1;
            decimal sumatipoExm = 0;
            decimal sumatipoExm_1 = 0;
            decimal igvPerson = 0;
            decimal _igvPerson = 0;
            decimal subTotalPerson = 0;
            decimal _subTotalPerson = 0;
            decimal totalFinal = 0;
            decimal totalFinal_1 = 0;
            foreach (var liq in Lista)
            {
                cell = new PdfPCell(new Phrase("TIPO EXAMEN: " + liq.Esotype, fontColumnValueBold)) { BackgroundColor = BaseColor.GRAY, Colspan = 11, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase("N°", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("PACIENTE", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("EDAD", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("F. EXAMEN", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("DNI", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("CARGO", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("PERFIL", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("IGV", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("SUB TOTAL", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("REF./OBSE.", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                foreach (var item in liq.Detalle)
                {
                    cell = new PdfPCell(new Phrase(nroreco.ToString() + ". ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.Trabajador, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.Edad.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.FechaExamen.ToString().Split(' ')[0], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.NroDocumemto, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.Cargo, fontColumnValue1)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.Perfil, fontColumnValue1)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    decimal _SubTotal = (decimal)item.Precio / (decimal)1.18;
                    _SubTotal = decimal.Round(_SubTotal, 2);
                    decimal _igv = _SubTotal * (decimal)0.18;
                    _igv = decimal.Round(_igv, 2);
                    cell = new PdfPCell(new Phrase(_igv.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(_SubTotal.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    decimal _Precio = (decimal)item.Precio;
                    _Precio = decimal.Round(_Precio, 2);
                    cell = new PdfPCell(new Phrase(_Precio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.CCosto, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    sumatipoExm += (decimal)item.Precio;
                    igvPerson += (decimal)_igv;
                    subTotalPerson += (decimal)_SubTotal;
                    nroreco++;

                }
                sumatipoExm_1 = decimal.Round(sumatipoExm, 2);
                _igvPerson = decimal.Round(igvPerson, 2);
                _subTotalPerson = decimal.Round(subTotalPerson, 2);
                cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL EXAMEN: " + liq.Esotype + " = ", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(_igvPerson.ToString(), fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(_subTotalPerson.ToString(), fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(sumatipoExm_1.ToString(), fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                //decimal IGV = totalFinal * (decimal)0.18;
                //decimal subTotalFinal = totalFinal - IGV;
                sumatipoExm = 0;
                igvPerson = 0;
                subTotalPerson = 0;
                totalFinal += (decimal)sumatipoExm_1;
            }
            totalFinal_1 = decimal.Round(totalFinal, 2);
            decimal subTotalFinal = decimal.Round(totalFinal_1 / (decimal)1.18, 2);
            decimal IGV = decimal.Round(subTotalFinal * (decimal)0.18, 2);


            cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("SUB TOTAL = S/. ", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase(subTotalFinal.ToString(), fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("IGV = S/. ", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase(IGV.ToString(), fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("TOTAL LIQUIDACIÓN = S/. ", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase(totalFinal_1.ToString(), fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            cells.Add(cell);
            columnWidths = new float[] { 4f, 20f, 5f, 10f, 8f, 13f, 15f, 5f, 5f, 8f, 10f };

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
