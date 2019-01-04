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
    public class SinLiquidar_General
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateEmpresasSinLiquidaciones_General(string filePDF,
            organizationDto infoEmpresaPropietaria, List<GerenciaTreeCredito> Lista_1, string fechaInicio, string fechaFin, List<GerenciaTreeCredito> Lista_2, List<GerenciaTreeCredito> Lista_3, List<GerenciaTreeCredito> Lista_4
            , List<GerenciaTreeCredito> Lista_5, List<GerenciaTreeCredito> Lista_6, List<GerenciaTreeCredito> Lista_7, List<GerenciaTreeCredito> Lista_8, List<GerenciaTreeCredito> Lista_9, List<GerenciaTreeCredito> Lista_10
            , List<GerenciaTreeCredito> Lista_11, List<GerenciaTreeCredito> Lista_12, List<GerenciaTreeCredito> Lista_total, List<GerenciaTreeCredito> ListaAños_Atras)
        {
            //Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);
            Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

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

            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            var tamaño_celda = 15f;

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("EMPRESAS SIN LIQUIDAR -- SIN FACTURAR", fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 18f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 2f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(" AL " + fechaFin, fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 18f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS
            #region l1
            decimal _debe_la_1 = 0;
            decimal _debe_la_2 = 0;
            decimal _debe_la_3 = 0;
            decimal _debe_la_4 = 0;
            foreach (var liq in ListaAños_Atras)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo =="SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_la_1 += item2.Total.Value;
                            }
                        }
                    }
                    
                }
                _debe_la_2 = _debe_la_1;
                decimal _debe1 = decimal.Round(_debe_la_2, 2);
                _debe_la_3 = _debe_la_2;

                _debe_la_2 = 0;
            }
            _debe_la_4 = _debe_la_3;
            _debe_la_4 = decimal.Round(_debe_la_4, 2);
            #endregion
            #region l1
            decimal _debe_l1_1 = 0;
            decimal _debe_l1_2 = 0;
            decimal _debe_l1_3 = 0;
            decimal _debe_l1_4 = 0;
            foreach (var liq in Lista_1)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l1_1 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l1_2 = _debe_l1_1;
                decimal _debe1 = decimal.Round(_debe_l1_2, 2);
                _debe_l1_3 = _debe_l1_2;

                _debe_l1_2 = 0;
            }
            _debe_l1_4 = _debe_l1_3;
            _debe_l1_4 = decimal.Round(_debe_l1_4, 2);
            #endregion
            #region l2
            decimal _debe_l2_1 = 0;
            decimal _debe_l2_2 = 0;
            decimal _debe_l2_3 = 0;
            decimal _debe_l2_4 = 0;
            foreach (var liq in Lista_2)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l2_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l2_2 = _debe_l2_1;
                decimal _debe1 = decimal.Round(_debe_l2_2, 2);
                _debe_l2_3 = _debe_l2_2;

                _debe_l2_2 = 0;
            }
            _debe_l2_4 = _debe_l2_3;
            _debe_l2_4 = decimal.Round(_debe_l2_4, 2);

            #endregion
            #region l3
            decimal _debe_l3_1 = 0;
            decimal _debe_l3_2 = 0;
            decimal _debe_l3_3 = 0;
            decimal _debe_l3_4 = 0;
            foreach (var liq in Lista_3)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l3_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l3_2 = _debe_l3_1;
                decimal _debe1 = decimal.Round(_debe_l3_2, 2);
                _debe_l3_3 = _debe_l3_2;

                _debe_l3_2 = 0;
            }
            _debe_l3_4 = _debe_l3_3;
            _debe_l3_4 = decimal.Round(_debe_l3_4, 2);
            #endregion
            #region l4
            decimal _debe_l4_1 = 0;
            decimal _debe_l4_2 = 0;
            decimal _debe_l4_3 = 0;
            decimal _debe_l4_4 = 0;
            foreach (var liq in Lista_4)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l4_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l4_2 = _debe_l4_1;
                decimal _debe1 = decimal.Round(_debe_l4_2, 2);
                _debe_l4_3 = _debe_l4_2;

                _debe_l4_2 = 0;
            }
            _debe_l4_4 = _debe_l4_3;
            _debe_l4_4 = decimal.Round(_debe_l4_4, 2);
            #endregion
            #region l5
            decimal _debe_l5_1 = 0;
            decimal _debe_l5_2 = 0;
            decimal _debe_l5_3 = 0;
            decimal _debe_l5_4 = 0;
            foreach (var liq in Lista_5)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l5_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l5_2 = _debe_l5_1;
                decimal _debe1 = decimal.Round(_debe_l5_2, 2);
                _debe_l5_3 = _debe_l5_2;

                _debe_l5_2 = 0;
            }
            _debe_l5_4 = _debe_l5_3;
            _debe_l5_4 = decimal.Round(_debe_l5_4, 2);
            #endregion
            #region l6
            decimal _debe_l6_1 = 0;
            decimal _debe_l6_2 = 0;
            decimal _debe_l6_3 = 0;
            decimal _debe_l6_4 = 0;
            foreach (var liq in Lista_6)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l6_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l6_2 = _debe_l6_1;
                decimal _debe1 = decimal.Round(_debe_l6_2, 2);
                _debe_l6_3 = _debe_l6_2;

                _debe_l6_2 = 0;
            }
            _debe_l6_4 = _debe_l6_3;
            _debe_l6_4 = decimal.Round(_debe_l6_4, 2);
            #endregion
            #region l7
            decimal _debe_l7_1 = 0;
            decimal _debe_l7_2 = 0;
            decimal _debe_l7_3 = 0;
            decimal _debe_l7_4 = 0;
            foreach (var liq in Lista_7)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l7_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l7_2 = _debe_l7_1;
                decimal _debe1 = decimal.Round(_debe_l7_2, 2);
                _debe_l7_3 = _debe_l7_2;

                _debe_l3_2 = 0;
            }
            _debe_l7_4 = _debe_l7_3;
            _debe_l7_4 = decimal.Round(_debe_l7_4, 2);
            #endregion
            #region l8
            decimal _debe_l8_1 = 0;
            decimal _debe_l8_2 = 0;
            decimal _debe_l8_3 = 0;
            decimal _debe_l8_4 = 0;
            foreach (var liq in Lista_8)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l8_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l8_2 = _debe_l8_1;
                decimal _debe1 = decimal.Round(_debe_l8_2, 2);
                _debe_l8_3 = _debe_l8_2;

                _debe_l8_2 = 0;
            }
            _debe_l8_4 = _debe_l8_3;
            _debe_l8_4 = decimal.Round(_debe_l8_4, 2);
            #endregion
            #region l9
            decimal _debe_l9_1 = 0;
            decimal _debe_l9_2 = 0;
            decimal _debe_l9_3 = 0;
            decimal _debe_l9_4 = 0;
            foreach (var liq in Lista_9)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l9_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l9_2 = _debe_l3_1;
                decimal _debe1 = decimal.Round(_debe_l9_2, 2);
                _debe_l9_3 = _debe_l9_2;

                _debe_l9_2 = 0;
            }
            _debe_l9_4 = _debe_l9_3;
            _debe_l9_4 = decimal.Round(_debe_l9_4, 2);
            #endregion
            #region l10
            decimal _debe_l10_1 = 0;
            decimal _debe_l10_2 = 0;
            decimal _debe_l10_3 = 0;
            decimal _debe_l10_4 = 0;
            foreach (var liq in Lista_10)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l10_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l10_2 = _debe_l10_1;
                decimal _debe1 = decimal.Round(_debe_l10_2, 2);
                _debe_l10_3 = _debe_l10_2;

                _debe_l10_2 = 0;
            }
            _debe_l10_4 = _debe_l10_3;
            _debe_l10_4 = decimal.Round(_debe_l10_4, 2);
            #endregion
            #region l11
            decimal _debe_l11_1 = 0;
            decimal _debe_l11_2 = 0;
            decimal _debe_l11_3 = 0;
            decimal _debe_l11_4 = 0;
            foreach (var liq in Lista_11)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l11_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l11_2 = _debe_l11_1;
                decimal _debe1 = decimal.Round(_debe_l11_2, 2);
                _debe_l11_3 = _debe_l11_2;

                _debe_l11_2 = 0;
            }
            _debe_l11_4 = _debe_l11_3;
            _debe_l11_4 = decimal.Round(_debe_l11_4, 2);
            #endregion
            #region l12
            decimal _debe_l12_1 = 0;
            decimal _debe_l12_2 = 0;
            decimal _debe_l12_3 = 0;
            decimal _debe_l12_4 = 0;
            foreach (var liq in Lista_12)
            {
                foreach (var item1 in liq.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                _debe_l12_2 += item2.Total.Value;
                            }
                        }
                    }

                }
                _debe_l12_2 = _debe_l12_1;
                decimal _debe1 = decimal.Round(_debe_l12_2, 2);
                _debe_l12_3 = _debe_l12_2;

                _debe_l12_2 = 0;
            }
            _debe_l12_4 = _debe_l12_3;
            _debe_l12_4 = decimal.Round(_debe_l12_4, 2);
            #endregion
            decimal suma = _debe_la_4 + _debe_l1_4 + _debe_l2_4 + _debe_l3_4 + _debe_l4_4 + _debe_l5_4 + _debe_l6_4 + _debe_l7_4 + _debe_l8_4 + _debe_l9_4 + _debe_l10_4 + _debe_l11_4 + _debe_l12_4;
            cells = new List<PdfPCell>()
            {
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("ANT", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("EN", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("FEB", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("MAR", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("ABR", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("MAY", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("JUN", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("JUL", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("AGO", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("SET", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("OCT", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("NOV", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("DIC", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("TOTAL", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("TOTAL A LIQUIDAR", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(_debe_la_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l1_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l2_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l3_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l4_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l5_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l6_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l7_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l8_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l9_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l10_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l11_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(_debe_l12_4.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(suma.ToString(), fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    //new PdfPCell(new Phrase("", fontColumnValue)){Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
            };
            columnWidths = new float[] { 2f, 26f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 2f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Parte Dinámica

            decimal debe = 0;
            decimal debe_1 = 0;
            decimal debe_2 = 0;

            decimal debe_ant1 = 0;
            decimal debe_ant1_1 = 0;

            decimal debe_ant2 = 0;
            decimal debe_ant2_1 = 0;

            decimal debe_ant3 = 0;
            decimal debe_ant3_1 = 0;
            decimal debe_ant4 = 0;
            decimal debe_ant4_1 = 0;
            decimal debe_ant5 = 0;
            decimal debe_ant5_1 = 0;
            decimal debe_ant6 = 0;
            decimal debe_ant6_1 = 0;
            decimal debe_ant7 = 0;
            decimal debe_ant7_1 = 0;
            decimal debe_ant8 = 0;
            decimal debe_ant8_1 = 0;
            decimal debe_ant9 = 0;
            decimal debe_ant9_1 = 0;
            decimal debe_ant10 = 0;
            decimal debe_ant10_1 = 0;
            decimal debe_ant11 = 0;
            decimal debe_ant11_1 = 0;
            decimal debe_ant12 = 0;
            decimal debe_ant12_1 = 0;
            decimal debe_ant1_total = 0;
            decimal debe_ant1_1_total = 0;
            decimal debe_atras = 0;
            decimal debe_atras_1 = 0;

            List<decimal> _listaAnteriores = new List<decimal>();
            decimal d_ant_1 = 0;
            decimal d_ant_2 = 0;
            foreach (var item in ListaAños_Atras)
            {
                foreach (var item1 in item.Tipos)
                {
                    if (item1.Tipo == "SIN LIQUIDACION")
                    {
                        foreach (var item2 in item1.Empresas)
                        {
                            if (item2.Total != 0)
                            {
                                d_ant_1 += item2.Total.Value;
                            }
                        }
                    }

                }
                //foreach (var deuda in item.detalle)
                //{
                //    d_ant_1 += deuda.d_Pago.Value;
                //}
                d_ant_2 = d_ant_1;
                _listaAnteriores.Add(d_ant_2);
                d_ant_1 = 0;
            }

            List<decimal> _lista2 = new List<decimal>();
            decimal d_2_1 = 0;
            decimal d_2_2 = 0;
            foreach (var item in Lista_2)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_2_1 += deuda.d_Pago.Value;
                //}
                d_2_2 = d_2_1;
                _lista2.Add(d_2_2);
                d_2_1 = 0;
            }

            List<decimal> _lista3 = new List<decimal>();
            decimal d_3_1 = 0;
            decimal d_3_2 = 0;
            foreach (var item in Lista_3)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_3_1 += deuda.d_Pago.Value;
                //}
                d_3_2 = d_3_1;
                _lista3.Add(d_3_2);
                d_3_1 = 0;
            }

            List<decimal> _lista4 = new List<decimal>();
            decimal d_4_1 = 0;
            decimal d_4_2 = 0;
            foreach (var item in Lista_4)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_4_1 += deuda.d_Pago.Value;
                //}
                d_4_2 = d_4_1;
                _lista2.Add(d_4_2);
                d_4_1 = 0;
            }

            List<decimal> _lista5 = new List<decimal>();
            decimal d_5_1 = 0;
            decimal d_5_2 = 0;
            foreach (var item in Lista_5)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_5_1 += deuda.d_Pago.Value;
                //}
                d_5_2 = d_5_1;
                _lista2.Add(d_5_2);
                d_5_1 = 0;
            }
            List<decimal> _lista6 = new List<decimal>();
            decimal d_6_1 = 0;
            decimal d_6_2 = 0;
            foreach (var item in Lista_6)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_6_1 += deuda.d_Pago.Value;
                //}
                d_6_2 = d_6_1;
                _lista2.Add(d_6_2);
                d_6_1 = 0;
            }

            List<decimal> _lista7 = new List<decimal>();
            decimal d_7_1 = 0;
            decimal d_7_2 = 0;
            foreach (var item in Lista_7)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_7_1 += deuda.d_Pago.Value;
                //}
                d_7_2 = d_7_1;
                _lista2.Add(d_7_2);
                d_7_1 = 0;
            }

            List<decimal> _lista8 = new List<decimal>();
            decimal d_8_1 = 0;
            decimal d_8_2 = 0;
            foreach (var item in Lista_8)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_8_1 += deuda.d_Pago.Value;
                //}
                d_8_2 = d_8_1;
                _lista8.Add(d_8_2);
                d_8_1 = 0;
            }

            List<decimal> _lista9 = new List<decimal>();
            decimal d_9_1 = 0;
            decimal d_9_2 = 0;
            foreach (var item in Lista_9)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_9_1 += deuda.d_Pago.Value;
                //}
                d_9_2 = d_9_1;
                _lista9.Add(d_9_2);
                d_9_1 = 0;
            }

            List<decimal> _lista10 = new List<decimal>();
            decimal d_10_1 = 0;
            decimal d_10_2 = 0;
            foreach (var item in Lista_10)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_10_1 += deuda.d_Pago.Value;
                //}
                d_10_2 = d_10_1;
                _lista10.Add(d_10_2);
                d_10_1 = 0;
            }

            List<decimal> _lista11 = new List<decimal>();
            decimal d_11_1 = 0;
            decimal d_11_2 = 0;
            foreach (var item in Lista_11)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_11_1 += deuda.d_Pago.Value;
                //}
                d_11_2 = d_11_1;
                _lista11.Add(d_11_2);
                d_11_1 = 0;
            }

            List<decimal> _lista12 = new List<decimal>();
            decimal d_12_1 = 0;
            decimal d_12_2 = 0;
            foreach (var item in Lista_12)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_12_1 += deuda.d_Pago.Value;
                //}
                d_12_2 = d_12_1;
                _lista12.Add(d_12_2);
                d_12_1 = 0;
            }

            List<decimal> _listatotal = new List<decimal>();
            decimal d_total_1 = 0;
            decimal d_total_2 = 0;
            foreach (var item in Lista_total)
            {
                //foreach (var deuda in item.detalle)
                //{
                //    d_total_1 += deuda.d_Pago.Value;
                //}
                d_total_2 = d_total_1;
                _listatotal.Add(d_total_2);
                d_total_1 = 0;
            }
            cells = new List<PdfPCell>();
            foreach (var liq in Lista_1)
            {
                cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(liq.Agrupador, fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);

                if (ListaAños_Atras.Count != 0)
                {
                    foreach (var item in _listaAnteriores)
                    {
                        debe_atras = item;
                        _listaAnteriores.Remove(item);
                        break;
                    }
                    debe_atras_1 = debe_atras;
                    decimal _debe_atras = decimal.Round(debe_atras_1, 2);

                    cell = new PdfPCell(new Phrase(_debe_atras.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_atras = 0;
                    debe_atras_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                //cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                //cells.Add(cell);

                //foreach (var item in liq.detalle)
                //{
                //    if (item.d_Pago != 0)
                //    {
                //        debe += item.d_Pago.Value;
                //    }
                //}
                debe_1 = debe;
                decimal _debe1 = decimal.Round(debe_1, 2);

                cell = new PdfPCell(new Phrase(_debe1.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                cells.Add(cell);

                if (Lista_2.Count != 0)
                {
                    foreach (var item in _lista2)
                    {
                        debe_ant2 = item;
                        _lista2.Remove(item);
                        break;
                    }
                    debe_ant2_1 = debe_ant2;
                    decimal _debe_l2 = decimal.Round(debe_ant2_1, 2);

                    cell = new PdfPCell(new Phrase(_debe_l2.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant2 = 0;
                    debe_ant2_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_3.Count != 0)
                {
                    foreach (var item in _lista3)
                    {
                        debe_ant3 = item;
                        _lista3.Remove(item);
                        break;
                    }
                    debe_ant3_1 = debe_ant3;
                    decimal _debel3 = decimal.Round(debe_ant3_1, 2);

                    cell = new PdfPCell(new Phrase(_debel3.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant3 = 0;
                    debe_ant3_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_4.Count != 0)
                {
                    foreach (var item in _lista4)
                    {
                        debe_ant4 = item;
                        _lista4.Remove(item);
                        break;
                    }
                    debe_ant4_1 = debe_ant4;
                    decimal _debel4 = decimal.Round(debe_ant4_1, 2);

                    cell = new PdfPCell(new Phrase(_debel4.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant3 = 0;
                    debe_ant3_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_5.Count != 0)
                {
                    foreach (var item in _lista5)
                    {
                        debe_ant5 = item;
                        _lista5.Remove(item);
                        break;
                    }
                    debe_ant5_1 = debe_ant5;
                    decimal _debel5 = decimal.Round(debe_ant5_1, 2);

                    cell = new PdfPCell(new Phrase(_debel5.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant5 = 0;
                    debe_ant5_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_6.Count != 0)
                {
                    foreach (var item in _lista6)
                    {
                        debe_ant6 = item;
                        _lista6.Remove(item);
                        break;
                    }
                    debe_ant6_1 = debe_ant6;
                    decimal _debel6 = decimal.Round(debe_ant6_1, 2);

                    cell = new PdfPCell(new Phrase(_debel6.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant6 = 0;
                    debe_ant6_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_7.Count != 0)
                {
                    foreach (var item in _lista7)
                    {
                        debe_ant7 = item;
                        _lista7.Remove(item);
                        break;
                    }
                    debe_ant7_1 = debe_ant7;
                    decimal _debel7 = decimal.Round(debe_ant7_1, 2);

                    cell = new PdfPCell(new Phrase(_debel7.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant7 = 0;
                    debe_ant7_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_8.Count != 0)
                {
                    foreach (var item in _lista8)
                    {
                        debe_ant8 = item;
                        _lista8.Remove(item);
                        break;
                    }
                    debe_ant8_1 = debe_ant7;
                    decimal _debel8 = decimal.Round(debe_ant8_1, 2);

                    cell = new PdfPCell(new Phrase(_debel8.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant8 = 0;
                    debe_ant8_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_9.Count != 0)
                {
                    foreach (var item in _lista9)
                    {
                        debe_ant9 = item;
                        _lista9.Remove(item);
                        break;
                    }
                    debe_ant9_1 = debe_ant9;
                    decimal _debel9 = decimal.Round(debe_ant9_1, 2);

                    cell = new PdfPCell(new Phrase(_debel9.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant9 = 0;
                    debe_ant9_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_10.Count != 0)
                {
                    foreach (var item in _lista10)
                    {
                        debe_ant10 = item;
                        _lista10.Remove(item);
                        break;
                    }
                    debe_ant10_1 = debe_ant10;
                    decimal _debel10 = decimal.Round(debe_ant10_1, 2);

                    cell = new PdfPCell(new Phrase(_debel10.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant10 = 0;
                    debe_ant10_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_11.Count != 0)
                {
                    foreach (var item in _lista11)
                    {
                        debe_ant11 = item;
                        _lista11.Remove(item);
                        break;
                    }
                    debe_ant11_1 = debe_ant11;
                    decimal _debel11 = decimal.Round(debe_ant11_1, 2);

                    cell = new PdfPCell(new Phrase(_debel11.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant11 = 0;
                    debe_ant11_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_12.Count != 0)
                {
                    foreach (var item in _lista12)
                    {
                        debe_ant12 = item;
                        _lista12.Remove(item);
                        break;
                    }
                    debe_ant12_1 = debe_ant12;
                    decimal _debel12 = decimal.Round(debe_ant12_1, 2);

                    cell = new PdfPCell(new Phrase(_debel12.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant12 = 0;
                    debe_ant12_1 = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }

                if (Lista_total.Count != 0)
                {
                    foreach (var item in _listatotal)
                    {
                        debe_ant1_total = item;
                        _listatotal.Remove(item);
                        break;
                    }
                    debe_ant1_1_total = debe_ant1_total;
                    decimal _debe11 = decimal.Round(debe_ant1_1_total, 2);

                    cell = new PdfPCell(new Phrase(_debe11.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);

                    debe_ant1_total = 0;
                    debe_ant1_1_total = 0;
                }
                else
                {
                    cell = new PdfPCell(new Phrase("0", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
                    cells.Add(cell);
                }
                cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
            }
            columnWidths = new float[] { 2f, 26f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 2f };

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
