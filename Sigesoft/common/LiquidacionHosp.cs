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
        public static void LiquidacionHospitalaria(JoinTicketDetails dataRecetaDetail, List<TiposCuenta> dataFarmaHops, List<ComponentForLiquiCustom> ListServicesAndCost, personDto dataPacient, organizationDto dataOrganization, organizationDto dataAseguradora, List<HospitalizacionCustom> dataHospitalizacion, string pathFile)
        {

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
                new PdfPCell(new Phrase("Cajamarca, " + DateTime.Now.ToShortDateString() + " \n" + DateTime.Now.ToShortTimeString(), fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
            };
            columnWidths = new float[] { 60f, 40f };
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
            int nroHabit = 1;
            string FechIngreso = "----", FechAlta = "----", Habit = "---", Dias = ": 0", Habitaciones = ": 1", costoHabit = "---";
            double constoUnitaro = 0.00;
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
                    double costoUnit = 0.00;
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
                        constoUnitaro = double.Parse(dat.d_Precio.ToString());
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
                new PdfPCell(new Phrase("SEÑOR(ES)", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(asgName, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FECHA", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(DateTime.Now.ToShortDateString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("RUC", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(asgRuc, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("H.C", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(DocNumber, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("PACIENTE", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(paciente, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("CUARTO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Habit, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("TITULAR", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(titular, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("N° DIAS", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Dias, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("CENTRO DE TRABAJO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgName, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("TELEFONO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgPhone, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("DIRECCIÓN", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgAdress, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("POLIZA", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ----", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("FECHA DE INGRESO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechIngreso, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FACTOR HONORARIOS", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ---", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("REGISTRO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ---", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("FECHA DE ALTA", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FACTOR SERVICIOS", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ---", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("PRELIQ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": ---", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("\n", fontColumnValue)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},


            };
            columnWidths = new float[] { 12f, 12f, 16f, 18f, 12f, 15f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Cuerpo

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("NOMBRE DEL SERVICIO", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("IMPORTE", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("DESCUENTO", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("TOTAL", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},

            };

            columnWidths = new float[] { 55f, 15f, 15f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


            if (ListServicesAndCost != null)
            {
                if (ListServicesAndCost.Count > 0)
                {
                    double totalConsumoGeneral = 0.00;
                    double TotalDeducibleGlob = 0.00;
                    double totalDesctGeneral = 0.00;
                    bool InsertFarmOperaciones = false;
                    bool InsertFarmReceta = false;
                    bool InsertDays = false;
                    bool EsHospitalizacion = false;
                    List<string> ComponentesAgregados_ = new List<string>();
                    List<string> ComponentesAgregados = new List<string>();
                    int contKindOfService = 1;
                    double totalImporte = 0.00;
                    double totalDescuento = 0.00;
                    double totalConDescuento = 0.00;
                    double totalDeducible = 0.00;
                    double totalConsumos = 0;
                    foreach (var service in ListServicesAndCost)
                    {

                        if (service.MasterServiceId.Value != (int)MasterService.Emergencia && service.MasterServiceName != "SEGUROS")
                        {
                            service.MasterServiceName = "HOSPITALIZACIÓN";
                        }
                        
                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(service.MasterServiceName, fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                            new PdfPCell(new Phrase("-----------------------------------------------------------------------------------------------------------------------", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                        };

                        columnWidths = new float[] { 40f, 60f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);

                        string KindName = "";
                        foreach (var serComponent in service.ListKindOfServices)
                        {

                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase(contKindOfService.ToString() + ") " + serComponent.KindOfServiceName, fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                            columnWidths = new float[] { 100f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            KindName = serComponent.KindOfServiceName;
                            contKindOfService++;
                            int ServiciosAuxiliares = 2;
                            foreach (var categorias in serComponent.ListCategoryForKOS)
                            {
                                if (serComponent.KindOfServiceId == ServiciosAuxiliares)
                                {
                                    cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(categorias.CategoryName, fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        
                                    };

                                    columnWidths = new float[] { 5f, 95f};
                                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                    document.Add(table);

                                    foreach (var comp in categorias.ListServicesComponentForKOS)
                                    {
                                        double importe = 0.00;
                                        double discount = 0.00;
                                        double total = 0.00;
                                        var findComp = ComponentesAgregados_.Find(x => x == comp.ComponentName);
                                        if (findComp == null)
                                        {
                                            int totalEncontrados = 0;
                                            foreach (var comFind in categorias.ListServicesComponentForKOS)
                                            {
                                                if (comp.ComponentName == comFind.ComponentName)
                                                {
                                                    if (comFind.SaldoPaciente != null)
                                                    {
                                                        totalDeducible += double.Parse(comFind.SaldoPaciente.Value.ToString("N2")); 
                                                    }
                                                    if (service.Discount.Value > 0)
                                                    {
                                                        double importeActual = double.Parse(comFind.Price.ToString()) / (1 - (service.Discount.Value / 100));
                                                        importe = importeActual;
                                                        discount = importeActual * service.Discount.Value;
                                                        
                                                    }
                                                    else
                                                    {
                                                        importe = double.Parse(comFind.Price.ToString());
                                                    }
                                                    total += double.Parse(comFind.Price.ToString());
                                                    totalEncontrados++;
                                                }
                                            }

                                            total = Math.Round(total, 2);
                                            var prinImporte = importe / 1.18;
                                            cells = new List<PdfPCell>()
                                            {
                                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase("("+ totalEncontrados.ToString() +")" + " [" + comp.CodigoSegus + "] " + comp.ComponentName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(prinImporte.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(discount.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(total.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                            };

                                            columnWidths = new float[] { 5f, 50f, 15f, 15f, 15f };
                                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                            document.Add(table);
                                        }
                                        ComponentesAgregados_.Add(comp.ComponentName);
                                        totalImporte += Math.Round(importe, 2);
                                        totalConDescuento += Math.Round(total, 2);
                                        totalDescuento += Math.Round(discount, 2);
                                    }

                                }
                                else
                                {
                                    foreach (var componentes in categorias.ListServicesComponentForKOS)
                                    {
                                        double importe = 0.00;
                                        double discount = 0.00;
                                        double total = 0.00;
                                        var findComp = ComponentesAgregados_.Find(x => x == componentes.ComponentName);
                                        if (findComp == null)
                                        {
                                            int totalEncontrados = 0;
                                            foreach (var comFind in categorias.ListServicesComponentForKOS)
                                            {
                                                if (componentes.ComponentName == comFind.ComponentName)
                                                {
                                                    if (comFind.SaldoPaciente != null)
                                                    {
                                                        totalDeducible += double.Parse(comFind.SaldoPaciente.Value.ToString("N2")); 
                                                    }
                                                    if (service.Discount.Value > 0)
                                                    {
                                                        double importeActual = double.Parse(comFind.Price.ToString()) / (1 - (service.Discount.Value / 100));
                                                        importe = importeActual;
                                                        discount = importeActual * service.Discount.Value;
                                                    }
                                                    else
                                                    {
                                                        importe = double.Parse(comFind.Price.ToString());
                                                    }
                                                    total += double.Parse(comFind.Price.ToString());
                                                    totalEncontrados++;
                                                }
                                            }

                                            importe = Math.Round(importe, 2);
                                            var prinImporte = importe / 1.18;
                                            cells = new List<PdfPCell>()
                                            {
                                                new PdfPCell(new Phrase("("+ totalEncontrados.ToString() +")" + " [" + componentes.CodigoSegus + "] " + componentes.ComponentName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(prinImporte.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(discount.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(total.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                            };

                                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                            document.Add(table);
                                            totalImporte += Math.Round(importe, 2);
                                            totalConDescuento += Math.Round(total, 2);
                                            totalDescuento += Math.Round(discount, 2);
                                        }
                                        ComponentesAgregados_.Add(componentes.ComponentName);
                                    }
                                }


                            }

                            //Esto se ejecuta cuando llega al último de la lista
                            var printImporteTotal = totalImporte / 1.18;
                            cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("Total : "+ KindName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(printImporteTotal.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(totalDescuento.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(totalConDescuento.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                                    };
                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            totalConsumos += Math.Round(totalConDescuento, 2);

                            totalImporte = 0.00;
                            totalDescuento = 0.00;
                            totalConDescuento = 0.00;
                            string deducible = service.ImporteDeducible == null
                                ? "0"
                                : service.ImporteDeducible.Value.ToString();
                            cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("TOTAL DEDUCIBLE: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(totalDeducible.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                    };
                            columnWidths = new float[] { 85f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            TotalDeducibleGlob += Math.Round(totalDeducible, 2); 
                            totalDeducible = 0.00;
                        }

                }
                ///////
                if (!InsertDays && dataHospitalizacion != null)
                {
                    if (dataHospitalizacion.Count > 0)
                    {

                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") HABITACIONES" , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                        columnWidths = new float[] { 100f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                        contKindOfService++;

                        double importe = 0.00;
                        importe = double.Parse(costoHabit);
                        totalImporte += Math.Round(importe, 2);
                        double costoUnitsinIgv = constoUnitaro / 1.18;
                        cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("("+ Habitaciones +")" + " Habitación " + Habit + " Del " + FechIngreso + " AL " + FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(importe.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                        columnWidths = new float[] { 55f, 15f, 15f, 15f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);

                        InsertDays = true;
                    }

                }
                if (InsertDays)
                {
                    double costoUnitsinIgv = constoUnitaro / 1.18;
                    cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                                new PdfPCell(new Phrase("Total : HABITACIÓN", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(totalImporte.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                            };
                    columnWidths = new float[] { 55f, 15f, 15f, 15f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                    totalConsumos += Math.Round(totalImporte, 2);
                    totalImporte = 0.00;

                }

                /////
                ///PARA LA FARMACIA
                /// 
                if (dataFarmaHops != null)
                {
                    double totalCoaseguro = 0.00;
                    if (dataFarmaHops.Count > 0)
                    {//para el total de productos usados en las operaciones
                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") FARMACIA" , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                        columnWidths = new float[] { 100f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                        contKindOfService++;

                        double importeSinIgv = 0.00;
                        int totalProd = 1;
                        foreach (var data in dataFarmaHops)
                        {

                            double importe = 0.00;
                            importe = double.Parse(data.TotalImporte.Value.ToString());
                            importeSinIgv = importe / 1.18;
                            totalImporte += Math.Round(importe, 2);
                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("("+ data.Encontrados.ToString() +")" + " [FARMACIA]" + data.NombreCuenta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(importeSinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(importe.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                            columnWidths = new float[] { 5f, 50f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            totalCoaseguro += double.Parse(data.TotalSaldoPaciente.Value.ToString("N2"));
                        }

                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                            new PdfPCell(new Phrase("Total : FARMACIA HOSP", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(importeSinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(totalImporte.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                        };
                        columnWidths = new float[] { 55f, 15f, 15f, 15f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                        totalConsumos += Math.Round(totalImporte, 2);
                        totalImporte = 0.00;

                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("MONTO COASEGURO ("+ dataFarmaHops[0].ImporteCoaseguro.ToString() +") %   :", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(totalCoaseguro.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},

                        };
                        columnWidths = new float[] { 85f, 15f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }


                    totalDesctGeneral += double.Parse(totalCoaseguro.ToString());
                }
                ////para los medicamentos que se dieron con receta
                if (dataRecetaDetail != null)
                {
                    cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("FARMACIA", fontSubTitleNegroNegrita)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                    contKindOfService++;

                    double importe = 0.00;
                    importe = double.Parse(dataRecetaDetail.v_Amount);
                    double importeSinIgv = importe / 1.18;
                    totalImporte += Math.Round(importe, 2);
                    cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("("+ dataRecetaDetail.v_Found +")" + " " + dataRecetaDetail.v_Description, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(importeSinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(dataRecetaDetail.v_Discount, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(dataRecetaDetail.v_Total, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };


                    columnWidths = new float[] { 55f, 15f, 15f, 15f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                    cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                            new PdfPCell(new Phrase("Total : " + dataRecetaDetail.v_Description, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(importeSinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(totalImporte.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                        };
                    columnWidths = new float[] { 55f, 15f, 15f, 15f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                    totalConsumos += Math.Round(totalImporte, 2);
                    totalImporte = 0.00;

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("MONTO COASEGURO ("+ dataRecetaDetail +") %   :" , fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        new PdfPCell(new Phrase(dataRecetaDetail.SaldoPaciente.Value.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},

                    };
                    columnWidths = new float[] { 85f, 15f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                    totalDesctGeneral += double.Parse(dataRecetaDetail.SaldoPaciente.Value.ToString()); 
                }
                // aqui debe terminar el primer foreach
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("TOTAL CONSUMOS:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(totalConsumos.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                };
                columnWidths = new float[] { 85f, 15f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);

                totalConsumoGeneral += Math.Round(totalConsumos, 2);


                //
                //Alfinal de todo el foreach
                string igv = "0.18";
                double subTotalGeneral = totalConsumoGeneral - totalDesctGeneral - TotalDeducibleGlob;
                double IgvGeneral = subTotalGeneral * float.Parse(igv);
                double totalGeneral = subTotalGeneral + IgvGeneral;
                subTotalGeneral = Math.Round(subTotalGeneral, 2);
                IgvGeneral = Math.Round(IgvGeneral, 2);
                totalGeneral = Math.Round(totalGeneral, 2);
                string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},

                    new PdfPCell(new Phrase(totalGeneralPalabra, fontColumnValue2)) {Colspan = 1, Rowspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("TOTAL GENERAL CONSUMOS:", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(totalConsumoGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("MONTO GENERAL COASEGURO :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("-" + totalDesctGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("MONTO GENERAL DEDUCIBLE :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("-" + TotalDeducibleGlob.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("SUB TOTAL GENERAL :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(subTotalGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("I.G.V. GENERAL 18% :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(IgvGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    
                    new PdfPCell(new Phrase("TOTAL GENERAL :", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(totalGeneral.ToString("N2"), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                };
                columnWidths = new float[] { 40f, 45f, 15f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);

            }


            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
      }
    }
}
