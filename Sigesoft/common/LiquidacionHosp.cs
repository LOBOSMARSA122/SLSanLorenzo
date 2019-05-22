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

namespace Sigesoft
{
    public class LiquidacionHosp
    {
        public static void LiquidacionHospitalaria(personDto dataPacient, organizationDto dataOrganization, organizationDto dataAseguradora, HospitalizacionCustom dataHospitalizacion, string pathFile)
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 15f, 41f);

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
            string FechIngreso = "----", FechAlta = "----", Habit = "---", Dias = ": 1";
            #endregion
            if (dataHospitalizacion != null)
            {
                FechIngreso = dataHospitalizacion.d_FechaIngreso == null ? ": ---" : ": " + dataHospitalizacion.d_FechaIngreso.ToShortDateString();
                FechAlta = dataHospitalizacion.d_FechaAlta == null ? ": ---" : ": " + dataHospitalizacion.d_FechaAlta.ToShortDateString();
                Habit = dataHospitalizacion.v_Habitacion == null ? ": ---" : ": " + dataHospitalizacion.v_Habitacion;
                // Difference in days, hours, and minutes.
                if (dataHospitalizacion.d_FechaIngreso != null && dataHospitalizacion.d_FechaAlta != null)
                {
                    TimeSpan ts = dataHospitalizacion.d_FechaAlta - dataHospitalizacion.d_FechaIngreso;
                    // Difference in days.
                    int differenceInDays = ts.Days;
                    if (differenceInDays <= 1)
                    {
                        differenceInDays = 1;
                    }
                    Dias = ": " + differenceInDays.ToString();
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


            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("HOSPITALIZACIÓN--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
            };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("1 CLINICA", fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
            };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("01 CUARTO 405-I Del 06/05/2019 AL 08/05/2019 A 200", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("400.00", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("400.00", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("04 SALA DE RECUPERACIÓN", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("74.41", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("74.41", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("05 SALA DE OPERACIONES", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("31.68", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("31.68", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("09 FARMACIA PISO Y EN OT.SERVICIO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("3,036.10", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("759.03", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("2,277.07", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("10 FARMACIA SALA DE OPERACIONES", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("659.57", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("164.89", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("494.68", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("12 USO DE EQUIPOS", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("86.18", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("86.18", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("25 INSTRUMENTISTA", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("6.34", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("6.34", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},

            };

            columnWidths = new float[] { 55f, 15f, 15f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            cells = new List<PdfPCell>()
            {

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                new PdfPCell(new Phrase("Total : CLINICA", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("4,294.28", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("3,370.36", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

            };
            columnWidths = new float[] { 55f, 15f, 15f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            // aqui debe terminar el primer foreach
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("TOTAL CONSUMOS:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("3,541.96", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("MONTO COASEGURO (20) %   :", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("-708.39", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},

            };
            columnWidths = new float[] { 85f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);




            //Alfinal de todo el foreach
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},

                new PdfPCell(new Phrase("TOTAL GENERAL CONSUMOS:", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("4,264.10", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("MONTO GENERAL COASEGURO (20%) :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("-708.39", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("SUB TOTAL GENERAL :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("3,555.71", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("I.G.V. GENERAL 18% :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("640.03", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

                
                new PdfPCell(new Phrase("TOTAL GENERAL :", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("640.03", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 18f, BorderColor = BaseColor.WHITE},

            };
            columnWidths = new float[] { 85f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }


        public string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }
    }
}
