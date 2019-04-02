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
using Sigesoft.Node.WinClient.BE.Custom;
using System.Data.SqlClient;
using Sigesoft.Common;

namespace NetPdf
{
    public class LiquidacionAseguradoraReport
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateLiquidacionAseguradora(string filePDF, List<LiquidacionAseguradora> ListResult, organizationDto infoEmpresaPropietaria)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 45f, 41f);

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

            var estatico_1 = 15f; var alto_Celda_1 = 15f; var alto_Celda_2 = 30f; var alto_Celda_3 = 45f; var alto_Celda_4 = 60f;
            var alto_Celda_6 = 90f; var tamaño_celda = 15f; 
            #endregion
            
            #region TÍTULO
            var Detalle_report = ListResult[0].Detalle;
            cells = new List<PdfPCell>();
            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);imagenEmpresa.SetAbsolutePosition(40, 790);document.Add(imagenEmpresa);
            }
            string aseguradora = ""; string paciente = ""; string empresa = ""; string fecha = ""; string historia = "";
            foreach (var item in ListResult)
            {
                aseguradora = item.Aseguradora;paciente = item.Paciente;empresa = item.EmpresaId;fecha = item.FechaServicio.ToShortDateString();historia = item.ServicioId; 
            }
            string [] valor = null; string valortipo = "";
            decimal total = 0; decimal? saldocoaseguropaciente = 0; decimal? saldoaseguradora = 0; decimal? saldodeduciblepaciente = 0;
            #region OLD
            //foreach (var item in Detalle_report)
            //{
            //    if (item.Tipo == "DEDUCIBLE")
            //    {
            //        valortipo = item.Valor;
            //        valor = valortipo.Split(' ');
            //        saldoaseguradora += item.SaldoAseguradora;
            //    }
            //    else
            //    {
            //        saldocoaseguro += item.SaldoPaciente;
            //        saldoaseguradora += item.SaldoAseguradora;
            //    }

            //    total += item.SubTotal;
            //}
            #endregion
            
            decimal subtotal_2 = Decimal.Round((total / (decimal)1.18), 2);
            decimal igv = Decimal.Round((total - (total / (decimal)1.18)), 2);
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion
            var cadena1 = "select SR.v_NroLiquidacion, LQ.v_LiquidacionId from service SR inner join liquidacion LQ on SR.v_NroLiquidacion = LQ.v_NroLiquidacion where v_ServiceId='"+historia+"'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string nroliq = "";
            string codigointerno = "";
            while (lector.Read())
            {
                nroliq = lector.GetValue(0).ToString();
                codigointerno = lector.GetValue(1).ToString();
            }
            lector.Close();

            cadena1 =
                "select SR.v_NroCartaSolicitud, PP.v_OwnerName, OO.v_Name, PR.v_ProtocolId, PR.v_Name, HP.d_FechaAlta, SP.v_Value1 " +
                "from service SR  " +
                "inner join person PP on SR.v_PersonId = PP.v_PersonId  " +
                "inner join protocol PR on SR.v_ProtocolId = PR.v_ProtocolId " +
                "inner join organization OO on PR.v_CustomerOrganizationId = OO.v_OrganizationId " +
                "inner join servicecomponent SC on SR.v_ServiceId = SC.v_ServiceId " +
                "inner join component CP on CP.v_ComponentId = SC.v_ComponentId " +
                "inner join hospitalizacion HP on HP.v_PersonId=PP.v_PersonId " +
                "inner join hospitalizacionservice HS on HS.v_ServiceId=SR.v_ServiceId " +
                "inner join hospitalizacionhabitacion HH on HH.v_HopitalizacionId=HP.v_HopitalizacionId " +
                "inner join systemparameter SP on SP.i_ParameterId=HH.i_HabitacionId and SP.i_GroupId=309 " +
                "where SR.v_ServiceId='" + historia + "' " +
                "group by SR.v_NroCartaSolicitud, PP.v_OwnerName, OO.v_Name, PR.v_ProtocolId, PR.v_Name, HP.d_FechaAlta, SP.v_Value1";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string Nrocarta = ""; string NombreTitular = ""; string NombreEmpresa = ""; string PlanSalud = ""; string Cobertura = "";
            string[] fechaAlta = null;  string NroHab = ""; string MedicoTratante = "";
            while (lector.Read())
            {
                Nrocarta = lector.GetValue(0).ToString() == "" ? "NO REGISTRADO" : lector.GetValue(0).ToString();
                NombreTitular = lector.GetValue(1).ToString() == "" ? "NO REGISTRADO" : lector.GetValue(1).ToString();
                NombreEmpresa = lector.GetValue(2).ToString() == "" ? "NO REGISTRADO" : lector.GetValue(2).ToString();
                PlanSalud = lector.GetValue(3).ToString() == "" ? "NO REGISTRADO" : lector.GetValue(3).ToString();
                Cobertura = lector.GetValue(4).ToString() == "" ? "NO REGISTRADO" : lector.GetValue(4).ToString();
                if (lector.GetValue(5).ToString() != "")
                {
                    fechaAlta = lector.GetValue(5).ToString().Split(' ');
                }
                else
                {
                    fechaAlta[0] = "NO REGISTRADO";
                }
                NroHab = lector.GetValue(6).ToString() == "" ? "NO REGISTRADO" : lector.GetValue(6).ToString();
            }
            lector.Close();

            var cellsTit2 = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("LIQUIDACIÓN N° : "+nroliq, fontTitle1)) { Colspan =10,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("ASEGURADORA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(aseguradora, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("CÓDIGO INTERNO", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(codigointerno, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("PACIENTE:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(paciente, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("SOLICITUD/CART. GARANT:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(Nrocarta, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("TITULAR:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(NombreTitular, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("DEDUCIBLE:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(valortipo, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("COPAGO:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(NombreEmpresa, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("F. INGRESO:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(fecha, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("F: ALTA:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(fechaAlta[0], fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("PLAN DE SALUD:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(PlanSalud, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("HIST. CLINICA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(historia, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("CAMA:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(NroHab, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("COBERTURA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(Cobertura, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("MEDICO A CARGO:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(MedicoTratante, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit2, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region DETALLE
            var cellsTit3 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("TICKET", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("SERVICIO", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("CANT.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. UNIT", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. TOTAL", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                new PdfPCell(new Phrase("CONSULTA", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };
            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit3, columnWidths, null, fontTitleTable);
            document.Add(table);
            cells = new List<PdfPCell>();
            #region OLD_
            // foreach (var item in Detalle_report)
            //{
            //    decimal preciounitario = Decimal.Round((item.PrecioUnitario),2);
            //    decimal subtotal = Decimal.Round((item.SubTotal), 2);
            //    decimal cantidad = Decimal.Round(item.Cantidad, 1);
            //    if (item.Tipo == "DEDUCIBLE")
            //    {
                    
            //        cell = new PdfPCell(new Phrase(fecha, fontColumnValue)){Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15f,UseVariableBorders = true,BorderColorLeft = BaseColor.WHITE,BorderColorRight = BaseColor.WHITE,BorderColorBottom = BaseColor.WHITE,BorderColorTop = BaseColor.WHITE};
            //        cells.Add(cell);
            //        cell = new PdfPCell(new Phrase("", fontColumnValue)){Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15f,UseVariableBorders = true,BorderColorLeft = BaseColor.WHITE,BorderColorRight = BaseColor.WHITE,BorderColorBottom = BaseColor.WHITE,BorderColorTop = BaseColor.WHITE};
            //        cells.Add(cell);
            //        cell = new PdfPCell(new Phrase(item.Descripcion, fontColumnValue)){Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15f,UseVariableBorders = true,BorderColorLeft = BaseColor.WHITE,BorderColorRight = BaseColor.WHITE,BorderColorBottom = BaseColor.WHITE,BorderColorTop = BaseColor.WHITE};
            //        cells.Add(cell);
            //        cell = new PdfPCell(new Phrase(cantidad.ToString(), fontColumnValue)){Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15f,UseVariableBorders = true,BorderColorLeft = BaseColor.WHITE,BorderColorRight = BaseColor.WHITE,BorderColorBottom = BaseColor.WHITE,BorderColorTop = BaseColor.WHITE};
            //        cells.Add(cell);
            //        cell = new PdfPCell(new Phrase(preciounitario.ToString(), fontColumnValue)){Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15f,UseVariableBorders = true,BorderColorLeft = BaseColor.WHITE,BorderColorRight = BaseColor.WHITE,BorderColorBottom = BaseColor.WHITE,BorderColorTop = BaseColor.WHITE};
            //        cells.Add(cell);
            //        cell = new PdfPCell(new Phrase(subtotal.ToString(), fontColumnValue)){Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15f,UseVariableBorders = true,BorderColorLeft = BaseColor.WHITE,BorderColorRight = BaseColor.WHITE,BorderColorBottom = BaseColor.WHITE,BorderColorTop = BaseColor.WHITE};
            //        cells.Add(cell);
            //    }
            //}
            #endregion

            cadena1 =
                "select SC.d_SaldoPaciente as d_SaldoPaciente, SC.d_SaldoAseguradora as d_SaldoAseguradora,SC.r_Price-(SC.r_Price*0.18) as subtotal, SC.r_Price*0.18 as igv,SC.r_Price as total, " +
                "SC.d_InsertDate as fecha, CP.v_Name as Descripcion, " +
                "CP.v_IdUnidadProductiva as v_IdUnidadProductiva, SR.v_ServiceId as v_ServiceId, " +
                "case when SC.i_TipoDesc = 1 then 'SI' else 'NO' end as i_EsDeducible, " +
                "case when SC.i_TipoDesc = 2 then 'SI' else 'NO' end as i_EsCoaseguro " +
                "from service SR " +
                "inner join servicecomponent SC on SR.v_ServiceId=SC.v_ServiceId " +
                "inner join protocol PR on SR.v_ProtocolId=PR.v_ProtocolId " +
                "inner join component CP on SC.v_ComponentId=CP.v_ComponentId " +
                "inner join [dbo].[plan] PL on PR.v_ProtocolId=PL.v_ProtocoloId " +
                "where SR.v_ServiceId='"+historia+"' and SC.r_Price<>0 " +
                "group by SC.d_SaldoPaciente,SC.d_SaldoAseguradora,SC.r_Price-(SC.r_Price*0.18),SC.r_Price*0.18, SC.r_Price,SC.d_InsertDate,CP.v_Name,CP.v_IdUnidadProductiva,SC.i_TipoDesc,SR.v_ServiceId";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string d_SaldoPacienteCP = ""; string d_SaldoAseguradoraCP = ""; string subtotalCP = ""; string igvCP = ""; string totalCP = "";
            string fechaSC = ""; string DescripcionCP = ""; string v_IdUnidadProductivaCP = ""; string v_ServiceIdCP = ""; string EsDeducibleCP = "";
            string EsCoaseguroCP = "";
            while (lector.Read())
            {
                d_SaldoPacienteCP = lector.GetValue(0).ToString();
                d_SaldoAseguradoraCP = lector.GetValue(1).ToString();
                subtotalCP = lector.GetValue(2).ToString();
                igvCP = lector.GetValue(3).ToString();
                totalCP = lector.GetValue(4).ToString();
                fechaSC = lector.GetValue(5).ToString();
                DescripcionCP = lector.GetValue(6).ToString();
                v_IdUnidadProductivaCP = lector.GetValue(7).ToString();
                v_ServiceIdCP = lector.GetValue(8).ToString();
                EsDeducibleCP = lector.GetValue(9).ToString();
                EsCoaseguroCP = lector.GetValue(10).ToString();
                if (EsDeducibleCP == "SI")
                {
                    cell = new PdfPCell(new Phrase(fecha, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(historia, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(DescripcionCP, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(totalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(totalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    saldodeduciblepaciente += decimal.Parse(d_SaldoPacienteCP);
                    saldoaseguradora += decimal.Parse(d_SaldoAseguradoraCP);
                }
                else if (EsCoaseguroCP == "SI")
                {
                    cell = new PdfPCell(new Phrase(fecha, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(historia, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(DescripcionCP, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(totalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(totalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    saldocoaseguropaciente += decimal.Parse(d_SaldoPacienteCP);
                    saldoaseguradora += decimal.Parse(d_SaldoAseguradoraCP);
                }
                
            }
            lector.Close();
            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            var cellsTit4 = new List<PdfPCell>()
            { 
                
                new PdfPCell(new Phrase("MEDICINAS", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };
            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit4, columnWidths, null, fontTitleTable);
            document.Add(table);
            cells = new List<PdfPCell>();
            cadena1 =
                "select RC.d_SaldoPaciente as d_SaldoPacienteRC, RC.d_SaldoAseguradora as d_SaldoAseguradoraRC, " +
                "RC.v_IdProductoDetalle as productoRC, SR.d_ServiceDate as fechaRC, " +
                "(RC.d_SaldoPaciente + RC.d_SaldoAseguradora)/RC.d_Cantidad as PrecioUnitario, RC.d_Cantidad as cantidadRC, " +
                "(RC.d_SaldoPaciente + RC.d_SaldoAseguradora)-((RC.d_SaldoPaciente + RC.d_SaldoAseguradora)*0.18) as subtotalRC, " +
                "(RC.d_SaldoPaciente + RC.d_SaldoAseguradora)*0.18 as igvRC, " +
                "RC.d_SaldoPaciente + RC.d_SaldoAseguradora as totalRC " +
                "from service SR " +
                "inner join receta RC on SR.v_ServiceId=RC.v_ServiceId " +
                "where SR.v_ServiceId='" + historia + "' and RC.i_Lleva=1";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string d_SaldoPacienteRC = ""; string d_SaldoAseguradoraRC = ""; string productoRC = ""; string fechaRC = ""; string cantidadRC = "";
            string subtotalRC = ""; string igvRC = ""; string totalRC = ""; string PrecioUnitario = "";
            while (lector.Read())
            {
                d_SaldoPacienteRC = lector.GetValue(0).ToString();
                d_SaldoAseguradoraRC = lector.GetValue(1).ToString();
                productoRC = lector.GetValue(2).ToString();
                #region Conexion SAMBHS
                ConexionSambhs conectaConexionSambhs = new ConexionSambhs(); conectaConexionSambhs.openSambhs();
                var cadenasam = "select PP.v_Descripcion " +
                                "from productodetalle PD " +
                                "inner join producto PP on PD.v_IdProducto=PP.v_IdProducto " +
                                "where PD.v_IdProductoDetalle='"+productoRC+"'";
                var comandosam = new SqlCommand(cadenasam, connection: conectaConexionSambhs.conectarSambhs);
                var lectorsam = comandosam.ExecuteReader();
                string descripcionRC = "";
                while (lectorsam.Read())
                {
                    descripcionRC = lectorsam.GetValue(0).ToString();
                }
                lectorsam.Close();
                conectaConexionSambhs.closeSambhs();
                #endregion
                fechaRC = lector.GetValue(3).ToString();
                PrecioUnitario = lector.GetValue(4).ToString();
                cantidadRC = lector.GetValue(5).ToString();
                subtotalRC = lector.GetValue(6).ToString();
                igvRC = lector.GetValue(7).ToString();
                totalRC = lector.GetValue(8).ToString();
                cell = new PdfPCell(new Phrase(fechaRC, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(productoRC, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(descripcionRC, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(cantidadRC, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(PrecioUnitario, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalRC, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                saldocoaseguropaciente += decimal.Parse(d_SaldoPacienteRC);
                saldoaseguradora += decimal.Parse(d_SaldoAseguradoraRC);

            }
            lector.Close();
            cadena1 =
                "select TKD.d_SaldoPaciente as d_SaldoPacienteTK, TKD.d_SaldoAseguradora as d_SaldoAseguradoraTK, " +
                "TKD.v_Descripcion as medicamentoTK, TKD.d_InsertDate as fechaTK, " +
                "TKD.d_PrecioVenta as PrecioUnitarioTK, TKD.d_Cantidad as d_CantidadTK, " +
                "(TKD.d_SaldoPaciente+TKD.d_SaldoAseguradora)-((TKD.d_SaldoPaciente+TKD.d_SaldoAseguradora)*0.18) as subtotalTK, " +
                "(TKD.d_SaldoPaciente+TKD.d_SaldoAseguradora)*0.18 as igvTK, " +
                "(TKD.d_SaldoPaciente+TKD.d_SaldoAseguradora) as totalTK, TK.v_TicketId as  v_TicketId " +
                " from service SR  " +
                "inner join ticket TK on SR.v_ServiceId=TK.v_ServiceId " +
                "inner join ticketdetalle TKD on TK.v_TicketId=TKD.v_TicketId " +
                "where SR.v_ServiceId='"+historia+"' ";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string d_SaldoPacienteTK = ""; string d_SaldoAseguradoraTK = ""; string medicamentoTK = ""; string fechaTK = ""; string d_CantidadTK = "";
            string subtotalTK = ""; string igvTK = ""; string totalTK = ""; string PrecioUnitarioTK = ""; string v_TicketId;
            while (lector.Read())
            {
                d_SaldoPacienteTK = lector.GetValue(0).ToString();
                d_SaldoAseguradoraTK = lector.GetValue(1).ToString();
                medicamentoTK = lector.GetValue(2).ToString();
                fechaTK = lector.GetValue(3).ToString();
                PrecioUnitarioTK = lector.GetValue(4).ToString();
                d_CantidadTK = lector.GetValue(5).ToString();
                subtotalTK = lector.GetValue(6).ToString();
                igvTK = lector.GetValue(7).ToString();
                totalTK = lector.GetValue(8).ToString();
                v_TicketId = lector.GetValue(9).ToString();
                cell = new PdfPCell(new Phrase(fechaTK, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(v_TicketId, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(medicamentoTK, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(d_CantidadTK, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(PrecioUnitarioTK, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalTK, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                saldocoaseguropaciente += decimal.Parse(d_SaldoPacienteTK);
                saldoaseguradora += decimal.Parse(d_SaldoAseguradoraTK);
            }
            lector.Close();
            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            var cellsTit5 = new List<PdfPCell>()
            { 
                
                new PdfPCell(new Phrase("CAMAS HOSPITALIZACIÓN", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };
            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit5, columnWidths, null, fontTitleTable);
            document.Add(table);
            cells = new List<PdfPCell>();
            cadena1 =
                "select HH.d_SaldoPaciente as d_SaldoPacienteHH, HH.d_SaldoAseguradora as d_SaldoAseguradoraHH, " +
                "HOS.d_FechaIngreso as d_StartDateHH, HOS.d_FechaAlta as d_EndDateHH, HH.d_Precio as d_Precio, " +
                " CONVERT(decimal, (HOS.d_FechaAlta-HOS.d_FechaIngreso)) as dias, HH.v_HopitalizacionId as hospiId " +
                "from hospitalizacionservice HS " +
                "inner join service SR on SR.v_ServiceId= HS.v_ServiceId " +
                "inner join hospitalizacionhabitacion HH on HH.v_HopitalizacionId=HS.v_HopitalizacionId " +
                "inner join hospitalizacion HOS on HOS.v_HopitalizacionId=HH.v_HopitalizacionId "+
                "where SR.v_ServiceId='" + historia + "' ";
                
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string d_SaldoPacienteHH = ""; string d_SaldoAseguradoraHH = ""; string d_StartDateHH = ""; string d_EndDateHH = ""; string d_Precio = "";
            string dias = ""; string hospiId;
            while (lector.Read())
            {
                d_SaldoPacienteHH = lector.GetValue(0).ToString();
                d_SaldoAseguradoraHH = lector.GetValue(1).ToString();
                d_StartDateHH = lector.GetValue(2).ToString();
                d_EndDateHH = lector.GetValue(3).ToString();
                d_Precio = lector.GetValue(4).ToString();
                dias = lector.GetValue(5).ToString() == "0" ? "1" : lector.GetValue(5).ToString();
                hospiId = lector.GetValue(6).ToString();
                decimal diashospi = decimal.Parse(dias);
                if (diashospi > 0 && diashospi < 1){diashospi = 1;}
                else{decimal d;diashospi = Math.Round(diashospi, 0);}
                decimal totalHH = diashospi * decimal.Parse(d_Precio);
                cell = new PdfPCell(new Phrase(d_StartDateHH + " - " + d_EndDateHH, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(hospiId, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("CAMA DE HOSPITAL", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(diashospi.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(d_Precio, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalHH.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                saldodeduciblepaciente += decimal.Parse(d_SaldoPacienteHH) * diashospi;
                saldoaseguradora += decimal.Parse(d_SaldoAseguradoraHH) * diashospi;
            }
            lector.Close();
            conectasam.closesigesoft();
            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion
            #region Total

            decimal? totalliq = saldocoaseguropaciente + saldodeduciblepaciente + saldoaseguradora;
            decimal? igvlig = totalliq * (decimal) 0.18;
            decimal? subtotalliq = totalliq - igvlig;
            var cellsTit6 = new List<PdfPCell>()
            { 
               
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("SUB - TOTAL S/.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(subtotalliq.ToString(), fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("IGV 18% S/.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(igvlig.ToString(), fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("TOTAL S/.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(totalliq.ToString(), fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("( - ) DEDUCIBLE S/.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(saldodeduciblepaciente.ToString(), fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("( - ) COASEGURO S/.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(saldocoaseguropaciente.ToString(), fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("PAGO ASEGURADORA S/.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(saldoaseguradora.ToString(), fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                
            };
            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit6, columnWidths, null, fontTitleTable);
            document.Add(table);

            document.NewPage();


            #region TÍTULO
            
            cells = new List<PdfPCell>();
            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25); imagenEmpresa.SetAbsolutePosition(40, 790); document.Add(imagenEmpresa);
            }
            var cellsTit9 = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("LIQUIDACIÓN N° : "+nroliq, fontTitle1)) { Colspan =10,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("ASEGURADORA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(aseguradora, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("CÓDIGO INTERNO", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(codigointerno, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("PACIENTE:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(paciente, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("SOLICITUD/CART. GARANT:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(Nrocarta, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("TITULAR:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(NombreTitular, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("DEDUCIBLE:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(valortipo, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("COPAGO:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(NombreEmpresa, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("F. INGRESO:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(fecha, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("F: ALTA:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("PLAN DE SALUD:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(PlanSalud, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("HIST. CLINICA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(historia, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("CAMA:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("COBERTURA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(Cobertura, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("MEDICO A CARGO:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(MedicoTratante, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit9, columnWidths, null, fontTitleTable);
            document.Add(table);

            var cellsTit10 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("TICKET", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("SERVICIO", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("CANT.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. UNIT", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. TOTAL", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                new PdfPCell(new Phrase("CONSULTA", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };
            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit10, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
          

            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);
        }
    }
}
