using iTextSharp.text;
using iTextSharp.text.pdf;
using NetPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Common;

namespace Sigesoft
{
    public class LiquidacionHosp
    {
        public static void LiquidacionHospitalaria(JoinTicketDetails dataRecetaDetail, JoinTicketDetails dataTicketDetail, List<ServiceList> ListServicesAndCost, personDto dataPacient, organizationDto dataOrganization, organizationDto dataAseguradora, List<HospitalizacionCustom> dataHospitalizacion, string pathFile)
        {
            List<ServiceList> _NewListServicesAndCost = new List<ServiceList>();
            foreach (var datas in ListServicesAndCost)
            {
                
                if (datas.i_MasterServiceId != (int)MasterService.Emergencia)
                {
                    if (_NewListServicesAndCost.Count == 0)
                    {
                        _NewListServicesAndCost.Add(datas);
                    }
                    else
                    {
                        _NewListServicesAndCost[0].ListServicesComponents.AddRange(datas.ListServicesComponents);
                    }
                }
                else
                {
                    _NewListServicesAndCost.Add(datas);
                }
            }

            //foreach (var order in _NewListServicesAndCost)
            //{
            //    order.ListServicesComponents.OrderBy(x => x.i_KindOfService.Value);
            //}
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 15f, 41f);

            document.SetPageSize(iTextSharp.text.PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pathFile, FileMode.Create));
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
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Blue));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            #endregion

            var cellsTit = new List<PdfPCell>()
            { 
                
                new PdfPCell(new Phrase("PRE-LIQUIDACIÓN HOSPITALARIA", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("Lima, " + DateTime.Now.ToShortDateString() + " \n" + DateTime.Now.ToShortTimeString(), fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
            };
            columnWidths = new float[] { 60f , 40f};
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);




            #region Cabezera
            #region variables
            //paciente
            string paciente = "---", titular = "---", DocNumber = "---";
            if (dataPacient != null)
            {
                paciente = ": " + dataPacient.v_FirstLastName + " " + dataPacient.v_SecondLastName + ", " +
                           dataPacient.v_FirstName;

                titular = string.IsNullOrEmpty(dataPacient.v_OwnerName) ? ": ---" : ": " + dataPacient.v_OwnerName.Split('|')[0];
                DocNumber = string.IsNullOrEmpty(dataPacient.v_DocNumber) ? ": ---" : ": CSL-" + dataPacient.v_DocNumber;
            }

            //empresa
            string orgName = "---", orgPhone = "---", orgAdress = "---";
            if (dataOrganization != null)
            {
                orgName = string.IsNullOrEmpty(dataOrganization.v_Name) ? ": ---" : ": " + dataOrganization.v_Name;
                orgAdress = string.IsNullOrEmpty(dataOrganization.v_Address) ? ": ---" : ": " + dataOrganization.v_Address;
                orgPhone = string.IsNullOrEmpty(dataOrganization.v_PhoneNumber) ? ": ---" : ": " + dataOrganization.v_PhoneNumber;
            }

            //aseguradora
            string asgName = "---", asgRuc = "---";
            if (dataAseguradora != null)
            {
                asgRuc = string.IsNullOrEmpty(dataAseguradora.v_IdentificationNumber) ? ": ---" : ": " + dataAseguradora.v_IdentificationNumber;
                asgName = string.IsNullOrEmpty(dataAseguradora.v_Name) ? ": ---" : ": " + dataAseguradora.v_Name;
            }

            //hospitalizacion
            int i_dias = 1;
            string FechIngreso = "----", FechAlta = "----", Habit = "---", Dias = ": 1", Habitaciones = ": 1", costoHabit = "---";
            #endregion
            if (dataHospitalizacion != null)
            {
                if (dataHospitalizacion.Count > 0)
                {
                    List<DateTime> DIngreso = new List<DateTime>();
                    List<DateTime> DAlta = new List<DateTime>();
                    foreach (var data in dataHospitalizacion)
                    {
                        DIngreso.Add(data.d_FechaIngreso);
                        DAlta.Add(data.d_FechaAlta);
                    }

                    List<DateTime> ListDIngreso = DIngreso.OrderBy(x => x).ToList();
                    List<DateTime> ListDAlta = DAlta.OrderByDescending(x => x).ToList();

                    Habit = dataHospitalizacion[0].v_Habitacion == null ? ": ---" : ": " + dataHospitalizacion[0].v_Habitacion;
                    List<string> habitaciones = new List<string>();
                    double costo = 0.00;

                    if (ListDIngreso.Count() > 0 && ListDAlta.Count() > 0)
                    {
                        FechIngreso = ListDIngreso.Count() == 0 ? ": ---" : ": " + ListDIngreso[0].ToShortDateString();
                        FechAlta = ListDAlta.Count() == 0 ? ": ---" : ": " + ListDAlta[0].ToShortDateString();


                        TimeSpan ts = ListDAlta[0] - ListDIngreso[0];
                        // Difference in days.
                        int differenceInDays = ts.Days;
                        if (differenceInDays <= 1)
                        {
                            differenceInDays = 1;
                            i_dias = 1;
                        }

                        i_dias = differenceInDays;
                        Dias = ": " + differenceInDays.ToString();
                    }

                    foreach (var dat in dataHospitalizacion)
                    {
                        costo += double.Parse(dat.d_Precio.ToString()) * i_dias;
                        
                        var find = habitaciones.Find(x => x == dat.v_Habitacion);
                        if (find == null)
                        {
                            habitaciones.Add(dat.v_Habitacion);
                        }
                    }
                    costo = Math.Round(costo, 2);
                    costoHabit = costo.ToString();
                    Habitaciones = habitaciones.Count.ToString();
                    // Difference in days, hours, and minutes.
                    
                }

            }

            

		    cells = new List<PdfPCell>()
            {                
                new PdfPCell(new Phrase("SEÑOR(ES)", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(asgName, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FECHA", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(DateTime.Now.ToShortDateString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("RUC", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(asgRuc, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("H.C", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(DocNumber, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("PACIENTE", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(paciente, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("CUARTO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Habit, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("TITULAR", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(titular, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("N° DIAS", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Dias, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("CENTRO DE TRABAJO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgName, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("TELEFONO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgPhone, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("DIRECCIÓN", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgAdress, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("POLIZA", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ----", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("FECHA DE INGRESO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FACTOR HONORARIOS", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ---", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("REGISTRO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ---", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("FECHA DE ALTA", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechIngreso, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FACTOR SERVICIOS", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ---", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("PRELIQ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ---", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("\n", fontColumnValue)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},


            };
            columnWidths = new float[] { 12f, 12f, 16f, 18f, 12f, 15f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table); 
	        #endregion

            #region Cuerpo

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("NOMBRE DEL SERVICIO", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("IMPORTE", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("DESCUENTO", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("TOTAL", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.BLACK},

            };

            columnWidths = new float[] { 55f, 15f, 15f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


            if (_NewListServicesAndCost != null)
            {
                if (_NewListServicesAndCost.Count > 0)
                {
                    int MasterServiceId = -1;
                    double totalConsumoGeneral = 0.00;
                    double totalDesctGeneral = 0.00;
                    bool InsertFarmOperaciones = false;
                    bool InsertFarmReceta = false;
                    bool InsertDays = false;
                    bool EsHospitalizacion = false;
                    List<string> ComponentesAgregados = new List<string>();
                    foreach (var service in _NewListServicesAndCost)
                    {

                        if (MasterServiceId == -1 || MasterServiceId != service.i_MasterServiceId.Value)
                        {
                            MasterServiceId = service.i_MasterServiceId.Value;
                            if (MasterServiceId != (int)MasterService.Emergencia)
                            {
                                //service.v_MasterServiceName = "HOSPITALIZACIÓN";
                            }
                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase(service.v_MasterServiceName, fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                                new PdfPCell(new Phrase("-----------------------------------------------------------------------------------------------------------------------", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                            };

                            columnWidths = new float[] { 40f , 60f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                        }

                        int KindOfService = -1;
                        int contKindOfService = 1;
                        int totalCompIguales = 0;
                        string ComponentName = "";
                        double totaImporte2 = 0.00;
                        double totalConsumos = 0;
                        string KindName = "";
                        double totalImporte = 0.00;
                        foreach (var serComponent in service.ListServicesComponents)
                        {

                            if (KindOfService == -1 || KindOfService != serComponent.i_KindOfService.Value)
                            {
                                if (KindOfService != -1)
                                {
                                    cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("Total : "+ KindName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(totalImporte.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(totalImporte.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                                    };
                                    columnWidths = new float[] { 55f, 15f, 15f, 15f };
                                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                    document.Add(table);
                                    totalConsumos += Math.Round(totalImporte, 2);
                                    totalImporte = 0.00;

                                }

                                KindOfService = serComponent.i_KindOfService == null ? -1 : serComponent.i_KindOfService.Value;
                                cells = new List<PdfPCell>()
                                {
                                    new PdfPCell(new Phrase(contKindOfService.ToString() + " " + serComponent.v_KindOfService, fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                };

                                columnWidths = new float[] { 100f };
                                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                document.Add(table);
                                KindName = serComponent.v_KindOfService;
                                contKindOfService++;
                            }
                            //else
                            //{//Aquí quiere decir que se acabo la primera clase de Servicio

                            //    totalImporte = Math.Round(totalImporte, 2);
                            //    totalConsumos += totalImporte;
                            //    totalConsumos = Math.Round(totalConsumos, 2);
                            //    //totalImporte = 0.00;
                            //}

                            if (!InsertDays && dataHospitalizacion != null)
                            {
                                if (dataHospitalizacion.Count > 0)
                                {
                                    double importe = 0.00;
                                    importe = double.Parse(costoHabit);
                                    totalImporte += Math.Round(importe, 2);
                                    cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("("+ Habitaciones +")" + " Habitación " + Habit + " Del " + FechIngreso + " AL " + FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(costoHabit, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(costoHabit, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                    };

                                    columnWidths = new float[] { 55f, 15f, 15f, 15f };
                                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                    document.Add(table);

                                    
                                }
                                InsertDays = true;
                            }

                            if (!InsertFarmOperaciones)
                            {//para el total de productos usados en las operaciones
                                if (dataTicketDetail != null)
                                {
                                    double importe = 0.00;
                                    importe = double.Parse(dataTicketDetail.v_Amount);
                                    totalImporte += Math.Round(importe, 2);
                                    cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("("+ dataTicketDetail.v_Found +")" + " " + dataTicketDetail.v_Description, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(dataTicketDetail.v_Amount, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(dataTicketDetail.v_Discount, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(dataTicketDetail.v_Total, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                    };

                                    columnWidths = new float[] { 55f, 15f, 15f, 15f };
                                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                    document.Add(table);
                                }

                                InsertFarmOperaciones = true;
                            }

                            if (!InsertFarmReceta)
                            {//para el total de productos usados sin operaciones
                                if (dataRecetaDetail != null)
                                {
                                    double importe = 0.00;
                                    importe = double.Parse(dataRecetaDetail.v_Amount);
                                    totalImporte += Math.Round(importe,2);
                                    //cells = new List<PdfPCell>()
                                    //{
                                    //    new PdfPCell(new Phrase("("+ dataRecetaDetail.v_Found +")" + " " + dataRecetaDetail.v_Description, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                    //    new PdfPCell(new Phrase(dataRecetaDetail.v_Amount, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                    //    new PdfPCell(new Phrase(dataRecetaDetail.v_Discount, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                    //    new PdfPCell(new Phrase(dataRecetaDetail.v_Total, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                    //};

                                    //columnWidths = new float[] { 55f, 15f, 15f, 15f };
                                    //table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                    //document.Add(table);
                                }

                                InsertFarmReceta = true;
                            }

                            if (ComponentName == "" || ComponentName != serComponent.v_ComponentName)
                            {
                                double importe = 0.00;
                                if (MasterServiceId != (int)MasterService.Emergencia)
                                {//busco todos los componentes que pertenecen a hospitalizacion
                                    //si ya se agregó el componente ya no se vuelve a agregar
                                    var findComponent = ComponentesAgregados.FindAll(x => x ==  serComponent.v_ComponentName);
                                    if (findComponent.Count == 0)
                                    {
                                        foreach (var objService in _NewListServicesAndCost)
                                        {
                                            if (objService.i_MasterServiceId != (int)MasterService.Emergencia)
                                            {
                                                var list = objService.ListServicesComponents.FindAll(x => x.v_ComponentName == serComponent.v_ComponentName).ToList();
                                                if (list != null)
                                                {
                                                    foreach (var serComp in list)
                                                    {
                                                        importe = serComp.r_Price.Value + importe;
                                                        totalImporte += Math.Round(importe, 2); ;
                                                    }
                                                    totalCompIguales += list.Count;
                                                }
                                            }

                                        }

                                        importe = Math.Round(importe, 2);

                                        cells = new List<PdfPCell>()
                                        {
                                            new PdfPCell(new Phrase("("+ totalCompIguales.ToString() +")" + " " + serComponent.v_ComponentName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                            new PdfPCell(new Phrase(importe.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                            new PdfPCell(new Phrase(importe.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        };

                                        columnWidths = new float[] { 55f, 15f, 15f, 15f };
                                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                        document.Add(table);
                                        totalCompIguales = 0;
                                        ComponentesAgregados.Add(serComponent.v_ComponentName);
                                    }
                                    
                                }
                                else
                                {

                                    importe = Math.Round(serComponent.r_Price.Value, 2); 
                                    totalImporte += Math.Round(importe, 2); 
                                    cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("("+ totalCompIguales.ToString() +")" + " " + serComponent.v_ComponentName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(importe.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(importe.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                    };

                                    columnWidths = new float[] { 55f, 15f, 15f, 15f };
                                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                    document.Add(table);
                                }
                              
                            }

                            totaImporte2 = totalImporte;
                        }


                        if (KindOfService != -1)
                        {
                            cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("Total : "+ KindName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(totaImporte2.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(totaImporte2.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                                    };
                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            totalConsumos += Math.Round(totaImporte2, 2);
                            totaImporte2 = 0.00;

                        }

                        // aqui debe terminar el primer foreach
                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("TOTAL CONSUMOS:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(totalConsumos.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                            new PdfPCell(new Phrase("MONTO COASEGURO (0) %   :", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},

                        };
                        columnWidths = new float[] { 85f, 15f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);

                        totalConsumoGeneral += Math.Round(totalConsumos, 2);
                    }

                    //
                    //Alfinal de todo el foreach
                    string igv = "0.18";
                    double subTotalGeneral = totalConsumoGeneral - totalDesctGeneral;
                    double IgvGeneral = subTotalGeneral * float.Parse(igv);
                    double totalGeneral = subTotalGeneral + IgvGeneral;
                    subTotalGeneral = Math.Round(subTotalGeneral, 2);
                    IgvGeneral = Math.Round(IgvGeneral, 2);
                    totalGeneral = Math.Round(totalGeneral, 2);
                    string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},

                        new PdfPCell(new Phrase(totalGeneralPalabra, fontColumnValue2)) {Colspan = 1, Rowspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                        new PdfPCell(new Phrase("TOTAL GENERAL CONSUMOS:", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                        new PdfPCell(new Phrase(totalConsumoGeneral.ToString(), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                        new PdfPCell(new Phrase("MONTO GENERAL COASEGURO (0%) :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                        new PdfPCell(new Phrase(totalDesctGeneral.ToString(), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                        
                        new PdfPCell(new Phrase("SUB TOTAL GENERAL :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                        new PdfPCell(new Phrase(subTotalGeneral.ToString(), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                        new PdfPCell(new Phrase("I.G.V. GENERAL 18% :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                        new PdfPCell(new Phrase(IgvGeneral.ToString(), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                        
                        new PdfPCell(new Phrase("TOTAL GENERAL :", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                        new PdfPCell(new Phrase(totalGeneral.ToString(), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                    };
                    columnWidths = new float[] { 40f, 45f, 15f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                }
            }

            
            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }

    }
}
