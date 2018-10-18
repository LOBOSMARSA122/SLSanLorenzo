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
    public class Liquidacion_EMO
    {

        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateLiquidacion_EMO(string filePDF,
            organizationDto infoEmpresaPropietaria, List<Liquidacion> Listaliq,
            ServiceList DataService)
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

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
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
                    new PdfPCell(new Phrase("LIQUIDACIÓN DE EXAMENES MÉDICOS OCUPACIONALES N° " + Listaliq.Select(p => p.Detalle.Select(r=> r.v_NroLiquidacion)), fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region DATOS

            cells = new List<PdfPCell>()
            {
                    new PdfPCell(new Phrase("EMPRESA A FACTURAR: ", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(DataService.v_CustomerOrganizationName, fontColumnValue)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("RUC :", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(DataService.RUC, fontColumnValue)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("DIRECCION", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(DataService.DireccionEmpresaTrabajo, fontColumnValue)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},

            };
            columnWidths = new float[] { 20f, 25f, 15f, 15f, 10F, 15F };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Parte Dinámica
            cells = new List<PdfPCell>();
            //int tamañoTickets = 0;
            int nroreco = 1;
            float sumatipoExm = 0;
            float totalFinal = 0;
            foreach (var liq in Listaliq)
            {
                cell = new PdfPCell(new Phrase("TIPO EXAMEN: " + liq.Esotype, fontColumnValueBold)) { BackgroundColor=BaseColor.GRAY, Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase("N°", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("PACIENTE", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("EDAD", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("F. EXAMEN", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("CARGO", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("REF./OBSE.", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                foreach (var item in liq.Detalle)
                {
                    cell = new PdfPCell(new Phrase(nroreco.ToString() + ". ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.Trabajador , fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.Edad.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.FechaExamen.ToString().Split(' ')[0], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.Cargo, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.Precio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.CCosto, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                    cells.Add(cell);
                    sumatipoExm += item.Precio;
                    nroreco++;

                }
                cell = new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL EXAMEN: " + liq.Esotype + " = ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(sumatipoExm.ToString(), fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                //decimal IGV = totalFinal * (decimal)0.18;
                //decimal subTotalFinal = totalFinal - IGV;

                totalFinal += (float)sumatipoExm;
            }
            cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("TOTAL LIQUIDACIÓN = ", fontColumnValueBold)) { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase(totalFinal.ToString(), fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK, MinimumHeight = 15f };
            cells.Add(cell);
            columnWidths = new float[] { 5f, 30f, 5f, 10f, 25f, 10F, 15f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            //{
            //    var ListaServicios = hospitalizacion.Servicios.FindAll(p => p.v_ServiceId != null);
            //    decimal totalParcialMedicina = 0;
            //    decimal sumaMedicina = 0;

            //    decimal sumaServicio = 0;
            //    foreach (var servicios in ListaServicios)
            //    {
            //        if (servicios.Tickets != null)
            //        {
            //            if (cargo == 1)
            //            {
            //                var ListaTickets = servicios.Tickets.FindAll(p => p.i_conCargoA == 1);
            //                if (ListaTickets.Count() >= 1)
            //                {
            //                    foreach (var tickets in ListaTickets)
            //                    {
            //                        var detalletickets = tickets.Productos.FindAll(p => p.d_Cantidad != 0);
            //                        tamañoTickets = detalletickets.Count();
            //                        cell = new PdfPCell(new Phrase(tickets.d_Fecha.ToString().Split(' ')[0], fontColumnValue)) { Rowspan = tamañoTickets, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                        cells.Add(cell);

            //                        cell = new PdfPCell(new Phrase(tickets.v_TicketId, fontColumnValue)) { Rowspan = tamañoTickets, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                        cells.Add(cell);

            //                        foreach (var Detalle in detalletickets)
            //                        {
            //                            cell = new PdfPCell(new Phrase(Detalle.v_Descripcion, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                            cells.Add(cell);
            //                            int cantidad = (int)Detalle.d_Cantidad;
            //                            cell = new PdfPCell(new Phrase(cantidad.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                            cells.Add(cell);

            //                            cell = new PdfPCell(new Phrase(Detalle.d_PrecioVenta.ToString().Split('.')[0] + "." + Detalle.d_PrecioVenta.ToString().Split('.')[1].Substring(0, 2), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                            cells.Add(cell);
            //                            totalParcialMedicina = (decimal)(Detalle.d_PrecioVenta * cantidad);
            //                            cell = new PdfPCell(new Phrase(totalParcialMedicina.ToString().Split('.')[0] + "." + totalParcialMedicina.ToString().Split('.')[1].Substring(0, 2), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                            cells.Add(cell);
            //                            sumaMedicina += (decimal)totalParcialMedicina;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    totalParcialMedicina = 0;
            //                }
            //            }
            //            else if (cargo == 2)
            //            {
            //                var ListaTickets = servicios.Tickets.FindAll(p => p.i_conCargoA == 2);
            //                if (ListaTickets.Count() >= 1)
            //                {
            //                    foreach (var tickets in ListaTickets)
            //                    {
            //                        var detalletickets = tickets.Productos.FindAll(p => p.d_Cantidad != 0);
            //                        tamañoTickets = detalletickets.Count();
            //                        cell = new PdfPCell(new Phrase(tickets.d_Fecha.ToString().Split(' ')[0], fontColumnValue)) { Rowspan = tamañoTickets, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                        cells.Add(cell);

            //                        cell = new PdfPCell(new Phrase(tickets.v_TicketId, fontColumnValue)) { Rowspan = tamañoTickets, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                        cells.Add(cell);

            //                        foreach (var Detalle in detalletickets)
            //                        {
            //                            cell = new PdfPCell(new Phrase(Detalle.v_Descripcion, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                            cells.Add(cell);
            //                            int cantidad = (int)Detalle.d_Cantidad;
            //                            cell = new PdfPCell(new Phrase(cantidad.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                            cells.Add(cell);

            //                            cell = new PdfPCell(new Phrase(Detalle.d_PrecioVenta.ToString().Split('.')[0] + "." + Detalle.d_PrecioVenta.ToString().Split('.')[1].Substring(0, 2), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                            cells.Add(cell);
            //                            totalParcialMedicina = (decimal)(Detalle.d_PrecioVenta * cantidad);
            //                            cell = new PdfPCell(new Phrase(totalParcialMedicina.ToString().Split('.')[0] + "." + totalParcialMedicina.ToString().Split('.')[1].Substring(0, 2), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                            cells.Add(cell);
            //                            sumaMedicina += (decimal)totalParcialMedicina;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    totalParcialMedicina = 0;
            //                }

            //            }
            //        }
            //        if (cargo == 2)
            //        {
            //            if (servicios.Componentes != null)
            //            {
            //                var ListaComponentes = servicios.Componentes.FindAll(p => p.Precio != 0.00);
            //                foreach (var compo in ListaComponentes)
            //                {
            //                    cell = new PdfPCell(new Phrase(compo.Ingreso.ToString().Split(' ')[0], fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                    cells.Add(cell);

            //                    cell = new PdfPCell(new Phrase(compo.ServiceComponentId, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                    cells.Add(cell);

            //                    cell = new PdfPCell(new Phrase(compo.Componente, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                    cells.Add(cell);

            //                    cell = new PdfPCell(new Phrase("1".ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                    cells.Add(cell);
            //                    decimal compoPrecio = (decimal)compo.Precio;
            //                    cell = new PdfPCell(new Phrase(compoPrecio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                    cells.Add(cell);

            //                    cell = new PdfPCell(new Phrase(compoPrecio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //                    cells.Add(cell);

            //                    sumaServicio += compoPrecio;
            //                }
            //            }
            //        }
            //    }
            //    var ListaHabitaciones = hospitalizacion.Habitaciones.FindAll(p => p.v_HopitalizacionId != null);
            //    decimal totalParcialHabitacion = 0;
            //    decimal sumaHabitacion = 0;
            //    if (cargo == 2)
            //    {
            //        foreach (var habitacion in ListaHabitaciones)
            //        {
            //            cell = new PdfPCell(new Phrase("Del " + habitacion.d_StartDate.ToString().Split(' ')[0] + "\n" + "Al   " + habitacion.d_EndDate.ToString().Split(' ')[0], fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //            cells.Add(cell);
            //            DateTime inicio = habitacion.d_StartDate.Value;
            //            DateTime fin;
            //            if (habitacion.d_EndDate != null || habitacion.d_EndDate.ToString() == "00/00/0000 0:0:0")
            //            {
            //                fin = habitacion.d_EndDate.Value;
            //            }
            //            else
            //            {
            //                fin = DateTime.Now;

            //            }

            //            TimeSpan tSpan = fin - inicio;

            //            int dias = tSpan.Days + 1;

            //            cell = new PdfPCell(new Phrase("-", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //            cells.Add(cell);

            //            cell = new PdfPCell(new Phrase("Habitación N  " + '\u0022' + habitacion.NroHabitacion + '\u0022', fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //            cells.Add(cell);

            //            cell = new PdfPCell(new Phrase(dias.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //            cells.Add(cell);

            //            cell = new PdfPCell(new Phrase(habitacion.d_Precio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //            cells.Add(cell);

            //            totalParcialHabitacion = (decimal)(habitacion.d_Precio * dias);

            //            cell = new PdfPCell(new Phrase(totalParcialHabitacion.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //            cells.Add(cell);
            //            sumaHabitacion += (decimal)totalParcialHabitacion;
            //        }
            //    }

            //    decimal totalFinal = 0;
            //    totalFinal = sumaMedicina + sumaServicio + sumaHabitacion;
            //    decimal IGV = totalFinal * (decimal)0.18;
            //    decimal subTotalFinal = totalFinal - IGV;
            //    cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);
            //    cell = new PdfPCell(new Phrase("Sub Total: S/.", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);
            //    cell = new PdfPCell(new Phrase(subTotalFinal.ToString().Split('.')[0] + "." + subTotalFinal.ToString().Split('.')[1].Substring(0, 2), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);

            //    cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);
            //    cell = new PdfPCell(new Phrase("IGV 18% S/.", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);
            //    cell = new PdfPCell(new Phrase(IGV.ToString().Split('.')[0] + "." + IGV.ToString().Split('.')[1].Substring(0, 2), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);

            //    cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);
            //    cell = new PdfPCell(new Phrase("TOTAL", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);
            //    cell = new PdfPCell(new Phrase(totalFinal.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
            //    cells.Add(cell);
            //    //.Split('.')[0] + "." + totalFinal.ToString().Split('.')[1].Substring(0, 2)
            //}

            //columnWidths = new float[] { 11f, 15f, 42f, 10f, 10F, 12F };

            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            //document.Add(filiationWorker);
            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);
        }
    }
}
