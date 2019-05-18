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
           
            decimal total = 0; decimal saldocoaseguropaciente = 0; decimal saldoaseguradora = 0; decimal saldodeduciblepaciente = 0;
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
            var cadena1 = "select SR.v_NroLiquidacion, LQ.v_LiquidacionId from service SR inner join liquidacion LQ on SR.v_NroLiquidacion = LQ.v_NroLiquidacion where SR.v_ServiceId='"+historia+"'";
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
                "select SR.v_NroCartaSolicitud, PP.v_OwnerName, OO.v_Name, PR.v_ProtocolId, PR.v_Name " +
                "from service SR  " +
                "inner join person PP on SR.v_PersonId = PP.v_PersonId  " +
                "inner join protocol PR on SR.v_ProtocolId = PR.v_ProtocolId " +
                "inner join organization OO on PR.v_CustomerOrganizationId = OO.v_OrganizationId " +
                "inner join servicecomponent SC on SR.v_ServiceId = SC.v_ServiceId " +
                "inner join component CP on CP.v_ComponentId = SC.v_ComponentId " +
                "where SR.v_ServiceId='"+historia+"' " +
                "group by SR.v_NroCartaSolicitud, PP.v_OwnerName, OO.v_Name, PR.v_ProtocolId, PR.v_Name ";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string Nrocarta = "NO REGISTRADO"; string NombreTitular = "NO REGISTRADO"; string NombreEmpresa = "NO REGISTRADO"; string PlanSalud = "NO REGISTRADO"; string Cobertura = "NO REGISTRADO";
            while (lector.Read())
            {
                Nrocarta = lector.GetValue(0).ToString();
                NombreTitular =  lector.GetValue(1).ToString();
                NombreEmpresa = lector.GetValue(2).ToString();
                PlanSalud = lector.GetValue(3).ToString();
                Cobertura =  lector.GetValue(4).ToString();
            }
            lector.Close();
            cadena1 = "select HP.d_FechaAlta, SP.v_Value1 " +
                      "from service SR " +
                      "inner join person PP on SR.v_PersonId = PP.v_PersonId  " +
                      "inner join hospitalizacion HP on HP.v_PersonId=PP.v_PersonId " +
                      "inner join hospitalizacionservice HS on HS.v_ServiceId=SR.v_ServiceId " +
                      "inner join hospitalizacionhabitacion HH on HH.v_HopitalizacionId=HP.v_HopitalizacionId " +
                      "inner join systemparameter SP on SP.i_ParameterId=HH.i_HabitacionId and SP.i_GroupId=309 " +
                      "where SR.v_ServiceId='"+historia+"' " +
                      "group by HP.d_FechaAlta, SP.v_Value1";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string fechaAlta = "N/A"; string NroHab = "N/A";
            while (lector.Read())
            {
                fechaAlta = lector.GetValue(0).ToString();
                NroHab = lector.GetValue(1).ToString();
            }
            lector.Close();
            cadena1 = "select PP.v_FirstName+', '+PP.v_FirstLastName+' '+PP.v_SecondLastName as MedicoTratante  " +
                      "from servicecomponent SC " +
                      "inner join systemuser SU on SU.i_SystemUserId=SC.i_MedicoTratanteId " +
                      "inner join person PP on SU.v_PersonId=PP.v_PersonId " +
                      "where v_ServiceId='"+historia+"' and v_ComponentId='N009-ME000000405'";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string MedicoTratante = "N/A";
            while (lector.Read())
            {
                MedicoTratante = lector.GetValue(0).ToString();
            }
            lector.Close();
            cadena1 =
                "select PL.d_Importe as deducible, PL.d_ImporteCo as coaseguro, OO.r_Factor as factor, OO.r_FactorMed as pps " +
                "from service SR " +
                "inner join servicecomponent SC on SR.v_ServiceId=SC.v_ServiceId " +
                "inner join protocol PR on PR.v_ProtocolId=SR.v_ProtocolId " +
                "inner join organization OO on PR.v_AseguradoraOrganizationId=OO.v_OrganizationId " +
                "inner join [dbo].[plan] PL on PL.v_ProtocoloId=PR.v_ProtocolId and SC.v_IdUnidadProductiva=PL.v_IdUnidadProductiva " +
                "where SR.v_ServiceId='"+historia+"'  and SC.v_IdUnidadProductiva<>'N001-LN000000005' " +
                "group by PL.d_Importe, PL.d_ImporteCo, OO.r_Factor, OO.r_FactorMed";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string deducible = "- - -";
            string copago = "- - -";
            string factor = "- - -";
            decimal pps = 0;
            while (lector.Read())
            {
                deducible = lector.GetValue(0).ToString();
                copago = lector.GetValue(1).ToString();
                factor = lector.GetValue(2).ToString();
                pps = (decimal)(lector.GetValue(3));
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
                    new PdfPCell(new Phrase(deducible, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("COPAGO:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(copago+" %", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(NombreEmpresa, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("F. INGRESO:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(fecha, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("F: ALTA:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(fechaAlta, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("PLAN DE SALUD:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(PlanSalud, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("HIST. CLINICA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(historia, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("CAMA:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(NroHab, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("COBERTURA:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(Cobertura, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("MEDICO A CARGO:", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(MedicoTratante, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("FACTOR:", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase(factor, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
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
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("SUB TOTAL", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. TOTAL", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                new PdfPCell(new Phrase("CONSULTA", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =9,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
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
                "select CONVERT(Decimal(15,2),SC.d_SaldoPaciente,2) as d_SaldoPaciente, CONVERT(Decimal(15,2),SC.d_SaldoAseguradora,2) as d_SaldoAseguradora,CONVERT(Decimal(15,2),SC.r_Price/1.18,2) as subtotal, CONVERT(Decimal(15,2),SC.r_Price*0.18,2) as igv,CONVERT(Decimal(15,2),SC.r_Price,2) as total, " +
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

                if (EsCoaseguroCP == "SI")
                {
                    cell = new PdfPCell(new Phrase(fecha, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(historia, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(DescripcionCP, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("1.0", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(subtotalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(subtotalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(totalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    saldocoaseguropaciente += decimal.Parse(d_SaldoPacienteCP);
                    saldoaseguradora += decimal.Parse(d_SaldoAseguradoraCP);
                }
                else if (EsDeducibleCP == "SI")
                {
                    cell = new PdfPCell(new Phrase(fecha, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(historia, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(DescripcionCP, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("1.0", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(subtotalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(subtotalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(totalCP, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    saldodeduciblepaciente += decimal.Parse(d_SaldoPacienteCP);
                    saldoaseguradora += decimal.Parse(d_SaldoAseguradoraCP);
                }
                
                
            }
            lector.Close();
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            var cellsTit4 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("TICKET", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("SERVICIO", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("CANT.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. UNIT", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("PPS: "+pps+" %", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("SUB TOTAL", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. TOTAL", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                

                new PdfPCell(new Phrase("MEDICINAS", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =9,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit4, columnWidths, null, fontTitleTable);
            document.Add(table);
            cells = new List<PdfPCell>();
            cadena1 =
                "select CONVERT(Decimal(15,2),RC.d_SaldoPaciente,2) as d_SaldoPacienteRC, CONVERT(Decimal(15,2),RC.d_SaldoAseguradora,2) as d_SaldoAseguradoraRC, " +
                "RC.v_IdProductoDetalle as productoRC, SR.d_ServiceDate as fechaRC, " +
                "CONVERT(Decimal(15,2),(RC.d_SaldoPaciente + RC.d_SaldoAseguradora)/RC.d_Cantidad,2) as PrecioUnitario, CONVERT(Decimal(15,1),RC.d_Cantidad,1) as cantidadRC, " +
                "CONVERT(Decimal(15,2),(((RC.d_SaldoPaciente + RC.d_SaldoAseguradora)/RC.d_Cantidad)/1.18),2) as subtotalRC, " +
                "CONVERT(Decimal(15,2),(RC.d_SaldoPaciente + RC.d_SaldoAseguradora)*0.18,2) as igvRC, " +
                "CONVERT(Decimal(15,2),RC.d_SaldoPaciente + RC.d_SaldoAseguradora,2) as totalRC " +
                "from service SR " +
                "inner join receta RC on SR.v_ServiceId=RC.v_ServiceId " +
                "where SR.v_ServiceId='" + historia + "' and RC.i_Lleva=1";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string d_SaldoPacienteRC = ""; string d_SaldoAseguradoraRC = ""; string productoRC = ""; string fechaRC = ""; decimal cantidadRC = 0;
            string subtotalRC = ""; string igvRC = ""; string totalRC = ""; decimal PrecioUnitario = 0;
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
                PrecioUnitario = (decimal) lector.GetValue(4);
                cantidadRC = (decimal) lector.GetValue(5);
                subtotalRC = lector.GetValue(6).ToString();
                igvRC = lector.GetValue(7).ToString();
                totalRC = lector.GetValue(8).ToString();
                decimal pUnitSinIgv = PrecioUnitario / (decimal)1.18;
                decimal descpps = pUnitSinIgv / (1 - (pps / 100));
                
                cell = new PdfPCell(new Phrase(fechaRC, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(productoRC, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(descripcionRC, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(cantidadRC.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(descpps.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(pUnitSinIgv.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(subtotalRC, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalRC, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                saldocoaseguropaciente += decimal.Parse(d_SaldoPacienteRC);
                saldoaseguradora += decimal.Parse(d_SaldoAseguradoraRC);

            }
            lector.Close();
            cadena1 =
                "select CONVERT(Decimal(15,2),TKD.d_SaldoPaciente,2) as d_SaldoPacienteTK, CONVERT(Decimal(15,2),TKD.d_SaldoAseguradora,2) as d_SaldoAseguradoraTK, " +
                "TKD.v_Descripcion as medicamentoTK, TKD.d_InsertDate as fechaTK, " +
                "CONVERT(Decimal(15,2),TKD.d_PrecioVenta,2) as PrecioUnitarioTK, CONVERT(Decimal(15,1),TKD.d_Cantidad,1) as d_CantidadTK, " +
                "CONVERT(Decimal(15,2),((TKD.d_SaldoPaciente+TKD.d_SaldoAseguradora)/1.18),2) as subtotalTK, " +
                "CONVERT(Decimal(15,2),(TKD.d_SaldoPaciente+TKD.d_SaldoAseguradora)*0.18,2) as igvTK, " +
                "CONVERT(Decimal(15,2),(TKD.d_SaldoPaciente+TKD.d_SaldoAseguradora),2) as totalTK, TK.v_TicketId as  v_TicketId " +
                " from service SR  " +
                "inner join ticket TK on SR.v_ServiceId=TK.v_ServiceId " +
                "inner join ticketdetalle TKD on TK.v_TicketId=TKD.v_TicketId " +
                "where SR.v_ServiceId='"+historia+"' ";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string d_SaldoPacienteTK = ""; string d_SaldoAseguradoraTK = ""; string medicamentoTK = ""; string fechaTK = ""; decimal d_CantidadTK = 0;
            string subtotalTK = ""; string igvTK = ""; string totalTK = ""; decimal PrecioUnitarioTK = 0; string v_TicketId;
            while (lector.Read())
            {
                d_SaldoPacienteTK = lector.GetValue(0).ToString();
                d_SaldoAseguradoraTK = lector.GetValue(1).ToString();
                medicamentoTK = lector.GetValue(2).ToString();
                fechaTK = lector.GetValue(3).ToString();
                PrecioUnitarioTK = (decimal) lector.GetValue(4);
                d_CantidadTK = (decimal) lector.GetValue(5);
                subtotalTK = lector.GetValue(6).ToString();
                igvTK = lector.GetValue(7).ToString();
                totalTK = lector.GetValue(8).ToString();
                v_TicketId = lector.GetValue(9).ToString();
                decimal pUnitSinIgv = PrecioUnitarioTK / (decimal)1.18;
                pUnitSinIgv = decimal.Round(pUnitSinIgv, 2);
                decimal descpps = pUnitSinIgv / (1 - (pps / 100));
                descpps = decimal.Round(descpps, 2);
                cell = new PdfPCell(new Phrase(fechaTK, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(v_TicketId, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(medicamentoTK, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(d_CantidadTK.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(descpps.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(pUnitSinIgv.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(subtotalTK, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalTK, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                if (d_SaldoPacienteTK.ToString() != "")
                {
                    saldocoaseguropaciente += decimal.Parse(d_SaldoPacienteTK);
                }

                if (d_SaldoAseguradoraTK.ToString() != "")
                {
                    saldoaseguradora += decimal.Parse(d_SaldoAseguradoraTK);
                }
                
               
            }
            lector.Close();
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            var cellsTit5 = new List<PdfPCell>()
            { 
                 new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("TICKET", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("SERVICIO", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("CANT.", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. UNIT", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("SUB TOTAL", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("V. TOTAL", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                
                new PdfPCell(new Phrase("CAMAS HOSPITALIZACIÓN", fontColumnValueBold)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan =9,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit5, columnWidths, null, fontTitleTable);
            document.Add(table);
            cells = new List<PdfPCell>();
            cadena1 =
                "select CONVERT(Decimal(15,2),HH.d_SaldoPaciente,1) as d_SaldoPacienteHH, CONVERT(Decimal(15,2),HH.d_SaldoAseguradora,2) as d_SaldoAseguradoraHH, " +
                "HOS.d_FechaIngreso as d_StartDateHH, HOS.d_FechaAlta as d_EndDateHH, CONVERT(Decimal(15,2),HH.d_Precio/1.18,2) as d_Precio, " +
                " CONVERT(decimal, (HOS.d_FechaAlta-HOS.d_FechaIngreso)) as dias, HH.v_HopitalizacionId as hospiId " +
                "from hospitalizacionservice HS " +
                "inner join service SR on SR.v_ServiceId= HS.v_ServiceId " +
                "inner join hospitalizacionhabitacion HH on HH.v_HopitalizacionId=HS.v_HopitalizacionId " +
                "inner join hospitalizacion HOS on HOS.v_HopitalizacionId=HH.v_HopitalizacionId "+
                "where SR.v_ServiceId='" + historia + "' ";
                
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            string d_SaldoPacienteHH = ""; string d_SaldoAseguradoraHH = ""; string d_StartDateHH = ""; string d_EndDateHH = ""; decimal d_Precio = 0;
            string dias = ""; string hospiId;
            while (lector.Read())
            {
                d_SaldoPacienteHH = lector.GetValue(0).ToString();
                d_SaldoAseguradoraHH = lector.GetValue(1).ToString();
                d_StartDateHH = lector.GetValue(2).ToString();
                d_EndDateHH = lector.GetValue(3).ToString();
                d_Precio = (decimal) lector.GetValue(4);
                dias = lector.GetValue(5).ToString() == "0" ? "1" : lector.GetValue(5).ToString();
                hospiId = lector.GetValue(6).ToString();
                decimal diashospi = decimal.Parse(dias);
                if (diashospi > 0 && diashospi < 1){diashospi = 1;}
                else{decimal d;diashospi = Math.Round(diashospi, 0);}
                decimal totalHH = diashospi * d_Precio;
                totalHH = decimal.Round(totalHH, 2);
                decimal precioSinIgv = d_Precio / (decimal) 1.18;
                precioSinIgv = decimal.Round(precioSinIgv, 2);
                cell = new PdfPCell(new Phrase(d_StartDateHH + " - " + d_EndDateHH, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(hospiId, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("CAMA DE HOSPITAL", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(diashospi.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(precioSinIgv.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(precioSinIgv.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalHH.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                saldodeduciblepaciente += decimal.Parse(d_SaldoPacienteHH) * diashospi;
                saldoaseguradora += decimal.Parse(d_SaldoAseguradoraHH) * diashospi;
            }
            lector.Close();
            conectasam.closesigesoft();
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion
            #region Total

            decimal totalliq = (saldocoaseguropaciente + saldodeduciblepaciente + saldoaseguradora);
            totalliq = decimal.Round(totalliq, 2);
            decimal igvlig = (totalliq/(decimal)1.18) * (decimal) 0.18;
            igvlig = decimal.Round(igvlig, 2);
            decimal subtotalliq = totalliq - igvlig;
            subtotalliq = decimal.Round(subtotalliq, 2);
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

           
          

            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);
        }
    }
}
