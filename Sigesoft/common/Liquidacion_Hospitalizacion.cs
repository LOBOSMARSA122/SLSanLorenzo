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
    public class Liquidacion_Hospitalizacion
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }        
        public static void CreateLiquidacion(string filePDF,
            organizationDto infoEmpresaPropietaria, List<HospitalizacionList> ListaHospit,
            PacientList datosPac, int cargo, hospitalizacionDto hospit, hospitalizacionhabitacionDto hospitHabit,  MedicoTratanteAtenciones medico)
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
            string cargoNombre = "";
            if (cargo == 1) cargoNombre = "MÉDICO TRATANTE";
            else if (cargo == 2) cargoNombre = "PACIENTE";
            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("LIQUIDACIÓN - " + cargoNombre, fontTitle1)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region DATOS
            string titular = "";
            if (datosPac.v_OwnerName == "")
            {
                titular = datosPac.v_FirstName + " " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName;
            }
            else {
                titular = datosPac.v_OwnerName;
            }
            string n_habitac = "";
            if (hospitHabit.i_HabitacionId == 1) n_habitac = "201";
            else if (hospitHabit.i_HabitacionId == 2) n_habitac = "202";
            else if (hospitHabit.i_HabitacionId == 3) n_habitac = "203";
            else if (hospitHabit.i_HabitacionId == 4) n_habitac = "204";
            else if (hospitHabit.i_HabitacionId == 5) n_habitac = "205";
            else if (hospitHabit.i_HabitacionId == 6) n_habitac = "206";
            else if (hospitHabit.i_HabitacionId == 7) n_habitac = "207";
            else if (hospitHabit.i_HabitacionId == 8) n_habitac = "208";
            else if (hospitHabit.i_HabitacionId == 9) n_habitac = "301";
            else if (hospitHabit.i_HabitacionId == 10) n_habitac = "302";
            else if (hospitHabit.i_HabitacionId == 11) n_habitac = "303";
            else if (hospitHabit.i_HabitacionId == 12) n_habitac = "304";
            else if (hospitHabit.i_HabitacionId == 13) n_habitac = "305";
            else if (hospitHabit.i_HabitacionId == 14) n_habitac = "306";
            else if (hospitHabit.i_HabitacionId == 15) n_habitac = "307";
            else if (hospitHabit.i_HabitacionId == 16) n_habitac = "308";
            string med = "";

            if (medico != null)
            {
                med = medico.Nombre;
            }
            else
            {
                med = "CLINICA SAN LORENZO";
            }
            cells = new List<PdfPCell>()
            {
                    new PdfPCell(new Phrase("ASEGURADORA", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(datosPac.v_OrganitationName, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("CÓDIGO INTERNO", fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    
                    new PdfPCell(new Phrase("PACIENTE", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_FirstName + " " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("SOLICITUD/CART GARANT", fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("TITULAR", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(titular, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("DEDUCIBLE", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("COPAGO", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
  
                    new PdfPCell(new Phrase("EMPRESA", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_Employer, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("F. INGRESO", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(hospit.d_FechaIngreso.ToString()==null?"":hospit.d_FechaIngreso.ToString().Split(' ')[0], fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("F. ALTA", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(hospit.d_FechaAlta.ToString()==null ?"":hospit.d_FechaAlta.ToString().Split(' ')[0], fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                    new PdfPCell(new Phrase("PLAN DE SALUD", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("HIST. CLINICA", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("CAMA", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(n_habitac, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                    new PdfPCell(new Phrase("COBERTURA", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("MEDICO A CARGO", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(med, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
            
            };
            columnWidths = new float[] {15f,30f, 15f, 15f, 10F, 15F};
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS
            cells = new List<PdfPCell>()
            {
                    new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("TICKET", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("SERVICIO", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("CANT. V.", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("P. UNIT", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("V. TOTAL", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
            };
            columnWidths = new float[] { 11f, 15f, 42f, 10f, 10F, 12F };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Parte Dinámica
            cells = new List<PdfPCell>();
            int tamañoTickets = 0;
            
            foreach (var hospitalizacion in ListaHospit)
            {
                var ListaServicios = hospitalizacion.Servicios.FindAll(p=>p.v_ServiceId != null);
                decimal totalParcialMedicina = 0;
                decimal sumaMedicina = 0;
                #region
                decimal sumaServicio = 0;
                foreach (var servicios in ListaServicios)
                {
                    if (servicios.Tickets != null)
                    {
                        if (cargo == 1)
                        {
                            var ListaTickets = servicios.Tickets.FindAll(p => p.i_conCargoA == 1);
                            if (ListaTickets.Count() >= 1)
                            {
                                foreach (var tickets in ListaTickets)
                                {
                                    var detalletickets = tickets.Productos.FindAll(p => p.d_Cantidad != 0);
                                    tamañoTickets = detalletickets.Count();
                                    cell = new PdfPCell(new Phrase(tickets.d_Fecha.ToString().Split(' ')[0], fontColumnValue)) { Rowspan = tamañoTickets, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                    cells.Add(cell);

                                    cell = new PdfPCell(new Phrase(tickets.i_tipoCuenta.ToString() == "1" ? tickets.v_TicketId + "\n-\nSOP" : tickets.i_tipoCuenta.ToString() == "2" ? tickets.v_TicketId + "\n-\nPROC - SOP" : tickets.i_tipoCuenta.ToString() == "3" ? tickets.v_TicketId + "\n-\nHOSP" : tickets.i_tipoCuenta.ToString() == "4" ? tickets.v_TicketId + "\n-\nI. SERV." : string.Empty, fontColumnValue)) { Rowspan = tamañoTickets, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                    cells.Add(cell);

                                    foreach (var Detalle in detalletickets)
                                    {
                                        cell = new PdfPCell(new Phrase(Detalle.v_Descripcion , fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                        cells.Add(cell);
                                        int cantidad = (int)Detalle.d_Cantidad;
                                        cell = new PdfPCell(new Phrase(cantidad.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                        cells.Add(cell);
                                        decimal precioVenta = (decimal)Detalle.d_PrecioVenta;
                                        precioVenta = decimal.Round(precioVenta, 2);
                                        cell = new PdfPCell(new Phrase(precioVenta.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                        cells.Add(cell);
                                        totalParcialMedicina = (decimal)(Detalle.d_PrecioVenta * cantidad);
                                        decimal _totalParcialMedicina = totalParcialMedicina;
                                        _totalParcialMedicina = decimal.Round(_totalParcialMedicina, 2);
                                        cell = new PdfPCell(new Phrase(_totalParcialMedicina.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                        cells.Add(cell);
                                        sumaMedicina += (decimal)totalParcialMedicina;
                                    }
                                }
                            }
                            else {
                                totalParcialMedicina = decimal.Round(totalParcialMedicina,2);
                            }
                        }
                        else if (cargo == 2)
                        {
                            var ListaTickets = servicios.Tickets.FindAll(p => p.i_conCargoA == 2);
                            if (ListaTickets.Count() >= 1)
                            {
                                foreach (var tickets in ListaTickets)
                                {
                                    
                                    var detalletickets = tickets.Productos.FindAll(p => p.d_Cantidad != 0);
                                    tamañoTickets = detalletickets.Count();
                                    cell = new PdfPCell(new Phrase(tickets.d_Fecha.ToString().Split(' ')[0], fontColumnValue)) { Rowspan = tamañoTickets, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                    cells.Add(cell);

                                    cell = new PdfPCell(new Phrase(tickets.i_tipoCuenta.ToString() == "1" ? tickets.v_TicketId + "\n-\nSOP" : tickets.i_tipoCuenta.ToString() == "2" ? tickets.v_TicketId + "\n-\nPROC - SOP" : tickets.i_tipoCuenta.ToString() == "3" ? tickets.v_TicketId + "\n-\nHOSP" : tickets.i_tipoCuenta.ToString() == "4" ? tickets.v_TicketId + "\n-\nI. SERV." : string.Empty, fontColumnValue)) { Rowspan = tamañoTickets, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                    cells.Add(cell);

                                    foreach (var Detalle in detalletickets)
                                    {
                                        cell = new PdfPCell(new Phrase(Detalle.v_Descripcion, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                        cells.Add(cell);
                                        int cantidad = (int)Detalle.d_Cantidad;
                                        cell = new PdfPCell(new Phrase(cantidad.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                        cells.Add(cell);

                                        cell = new PdfPCell(new Phrase(Detalle.d_PrecioVenta.ToString().Split('.')[0] + "." + Detalle.d_PrecioVenta.ToString().Split('.')[1].Substring(0, 2), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                        cells.Add(cell);
                                        totalParcialMedicina = (decimal)(Detalle.d_PrecioVenta * cantidad);
                                        totalParcialMedicina = decimal.Round(totalParcialMedicina, 2);
                                        cell = new PdfPCell(new Phrase(totalParcialMedicina.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                                        cells.Add(cell);
                                        sumaMedicina += (decimal)totalParcialMedicina;
                                    }
                                } 
                            }
                            else
                            {
                                totalParcialMedicina = decimal.Round(totalParcialMedicina,2);
                            }
                            
                        }
                    }

                   if (cargo == 1)
                    {
                        var ListaComponentes = servicios.Componentes.FindAll(p => p.Precio != 0 && p.i_conCargoA == 1);
                        foreach (var compo in ListaComponentes)
                        {
                            cell = new PdfPCell(new Phrase(compo.Ingreso.ToString().Split(' ')[0], fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            cell = new PdfPCell(new Phrase(compo.ServiceComponentId, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            cell = new PdfPCell(new Phrase(compo.Componente, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            cell = new PdfPCell(new Phrase("1".ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);
                            decimal compoPrecio = (decimal)compo.Precio;
                            compoPrecio = decimal.Round(compoPrecio, 2);
                            cell = new PdfPCell(new Phrase(compoPrecio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            cell = new PdfPCell(new Phrase(compoPrecio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            sumaServicio += compoPrecio;
                        }
                    }
                    else if (cargo == 2)
                    {
                        var ListaComponentes = servicios.Componentes.FindAll(p => p.Precio != 0 && p.i_conCargoA == 2);
                        foreach (var compo in ListaComponentes)
                        {
                            cell = new PdfPCell(new Phrase(compo.Ingreso.ToString().Split(' ')[0], fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            cell = new PdfPCell(new Phrase(compo.ServiceComponentId, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            cell = new PdfPCell(new Phrase(compo.Componente, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            cell = new PdfPCell(new Phrase("1".ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);
                            decimal compoPrecio = (decimal)compo.Precio;
                            compoPrecio = decimal.Round(compoPrecio, 2);
                            cell = new PdfPCell(new Phrase(compoPrecio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            cell = new PdfPCell(new Phrase(compoPrecio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                            cells.Add(cell);

                            sumaServicio += compoPrecio;
                        }
                    }
                    
                }
                
                decimal totalParcialHabitacion = 0;
                decimal sumaHabitacion = 0;
                if (cargo == 1)
                {
                    var ListaHabitaciones = hospitalizacion.Habitaciones.FindAll(p => p.i_conCargoA == 1);
                    foreach (var habitacion in ListaHabitaciones)
                    {
                        cell = new PdfPCell(new Phrase("Del " + habitacion.d_StartDate.ToString().Split(' ')[0] + "\n" + "Al   " + habitacion.d_EndDate.ToString().Split(' ')[0], fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);
                        DateTime inicio = habitacion.d_StartDate.Value.Date;
                        DateTime fin;

                        if (habitacion.d_EndDate != null || habitacion.d_EndDate.ToString()=="00/00/0000 0:0:0")
                        {
                            fin = habitacion.d_EndDate.Value.Date;
                        }
                        else {
                            fin = DateTime.Now.Date;
                            
                        }

                        TimeSpan nDias = fin - inicio;

                        int tSpan = nDias.Days;

                        //+ 1
                        int dias = 0;
                        if (tSpan == 0)
                        {
                            dias = tSpan + 1;
                        }
                        else
                        {
                            dias = tSpan;
                        }

                        cell = new PdfPCell(new Phrase("Habitación N  " + '\u0022' + habitacion.NroHabitacion + '\u0022', fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase(dias.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);
                        decimal _habitacionPrecio = (decimal)habitacion.d_Precio;
                        _habitacionPrecio = decimal.Round(_habitacionPrecio, 2);
                        cell = new PdfPCell(new Phrase(_habitacionPrecio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);

                        totalParcialHabitacion = (decimal)(habitacion.d_Precio * dias);
                        totalParcialHabitacion = decimal.Round(totalParcialHabitacion, 2);

                        cell = new PdfPCell(new Phrase(totalParcialHabitacion.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);
                        sumaHabitacion += (decimal)totalParcialHabitacion;
                    }
                }
                else if (cargo == 2)
                {
                    var ListaHabitaciones = hospitalizacion.Habitaciones.FindAll(p => p.i_conCargoA == 2);
                    foreach (var habitacion in ListaHabitaciones)
                    {
                        cell = new PdfPCell(new Phrase("Del " + habitacion.d_StartDate.ToString().Split(' ')[0] + "\n" + "Al   " + habitacion.d_EndDate.ToString().Split(' ')[0], fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);
                        DateTime inicio = habitacion.d_StartDate.Value.Date;
                        DateTime fin;

                        if (habitacion.d_EndDate != null || habitacion.d_EndDate.ToString() == "00/00/0000 0:0:0")
                        {
                            fin = habitacion.d_EndDate.Value.Date;
                        }
                        else
                        {
                            fin = DateTime.Now.Date;

                        }

                        TimeSpan nDias = fin - inicio;

                        int tSpan = nDias.Days;

                        //+ 1
                        int dias = 0;
                        if (tSpan == 0)
                        {
                            dias = tSpan + 1;
                        }
                        else
                        {
                            dias = tSpan;
                        }

                        cell = new PdfPCell(new Phrase("Habitación N  " + '\u0022' + habitacion.NroHabitacion + '\u0022', fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase(dias.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);
                        decimal _habitacionPrecio = (decimal)habitacion.d_Precio;
                        _habitacionPrecio = decimal.Round(_habitacionPrecio, 2);
                        cell = new PdfPCell(new Phrase(_habitacionPrecio.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);

                        totalParcialHabitacion = (decimal)(habitacion.d_Precio * dias);
                        totalParcialHabitacion = decimal.Round(totalParcialHabitacion, 2);

                        cell = new PdfPCell(new Phrase(totalParcialHabitacion.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                        cells.Add(cell);
                        sumaHabitacion += (decimal)totalParcialHabitacion;
                    }
                }
            #endregion
                decimal totalFinal = sumaMedicina + sumaServicio + sumaHabitacion;

                decimal subTotalFinal = totalFinal / (decimal)1.18;

                decimal IGV = totalFinal - subTotalFinal;
                
                

                totalFinal = decimal.Round(totalFinal, 2);
                IGV = decimal.Round(IGV, 2);
                subTotalFinal = decimal.Round(subTotalFinal, 2);
                cell = new PdfPCell(new Phrase("", fontColumnValue)) 
                    {   Colspan = 4, 
                        HorizontalAlignment = PdfPCell.ALIGN_RIGHT,
                        UseVariableBorders = true, 
                        BorderColorLeft = BaseColor.WHITE, 
                        BorderColorRight = BaseColor.WHITE, 
                        BorderColorBottom = BaseColor.WHITE, 
                        BorderColorTop = BaseColor.WHITE, 
                        MinimumHeight = 15f
                    };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("Sub Total: S/.", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(subTotalFinal.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("IGV 18% S/.", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(IGV.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalFinal.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE, MinimumHeight = 15f };
                cells.Add(cell);
                //.Split('.')[0] + "." + totalFinal.ToString().Split('.')[1].Substring(0, 2)
            }

            columnWidths = new float[] { 11f, 15f, 42f, 10f, 10F, 12F };

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

