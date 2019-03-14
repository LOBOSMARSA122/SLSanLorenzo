using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;

namespace NetPdf
{
    public class CERTIFICADO_SUFICIENCIA_MEDICA_TC
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        #region Reporte SAS
        public static void CreateCertificadoSuficienciaTC(PacientList filiationData, 
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF, UsuarioGrabo usuariograbo)
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

            var tamaño_celda = 15f;
            #region TÍTULO
            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));
            cells = new List<PdfPCell>();
            if (infoEmpresa.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 780);
                document.Add(imagenEmpresa);
            }

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("CERTIFICADO DE SUFICIENCIA MEDICA PARA TRABAJOS"+ "\n" +" EN ESPACIOS CONFINADOS", fontTitle1)) { BackgroundColor= BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region FILIACIÓN
            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;

            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("1. Filiación", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("Apellidos Y Nombres", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Empresa", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(empr_Conct, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString() , fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Sede/Proyecto", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(filiationData.v_NombreProtocolo, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("DNI", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(datosPac.v_DocNumber , fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Puesto de trabajo", fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ANTECEDENTES
            ServiceComponentList cert_suf_med_ec = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_ID);
            var antec = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_DESCRIPCION) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_DESCRIPCION).v_Value1;


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("2. Antecedentes de importancia", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("Descripción", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(antec, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region FUNCIONES VITALES / TRIAJE
            var perimetro_tor_inspiracion = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_P_T_INSPIRACION) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_P_T_INSPIRACION).v_Value1;
            var unidadperimetro_tor_inspiracion = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_P_T_INSPIRACION) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_P_T_INSPIRACION).v_MeasurementUnitName;

            var perimetro_tor_espiracion = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_P_T_ESPIRACION) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_P_T_ESPIRACION).v_Value1;
            var unidadperimetro_tor_espiracion = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_P_T_ESPIRACION) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_P_T_ESPIRACION).v_MeasurementUnitName;


            //Antropometria
            ServiceComponentList antro = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            var peso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
            var unidadpeso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_MeasurementUnitName;

            var talla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_Value1;
            var unidadtalla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_MeasurementUnitName;

            var imc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_Value1;
            var unidadimc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_MeasurementUnitName;

            var per_abdomen = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).v_Value1;
            var unidadper_abdomen = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).v_MeasurementUnitName;

            var per_cadera = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).v_Value1;
            var unidadper_cadera = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).v_MeasurementUnitName;

            var icc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).v_Value1;
            var unidadicc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).v_MeasurementUnitName;


            //Funciones Vitales
            ServiceComponentList funcVit = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
            var pres_Sist = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "SIN RESULTADOS" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
            var unidadpres_Sist = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_MeasurementUnitName;

            var pres_Diast = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "SIN RESULTADOS" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;
            var unidadpres_Diast = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_MeasurementUnitName;

            var so2 = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID) == null ? "SIN RESULTADOS" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID).v_Value1;
            var unidadso2 = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID).v_MeasurementUnitName;

            var frec_Card = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "SIN RESULTADOS" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_Value1;
            var unidadfrec_Card = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_MeasurementUnitName;


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("3. Funciones vitales/Triaje", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                
                new PdfPCell(new Phrase("Peso", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(peso + unidadpeso, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("PA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(pres_Sist +" / " + pres_Diast + unidadpres_Diast , fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
              
                new PdfPCell(new Phrase("Talla", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(talla + unidadtalla, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("SO2", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(so2 + unidadso2, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("IMC", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(imc + unidadimc, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("FC", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(frec_Card + unidadfrec_Card, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Perímetro de cintura/cadera", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(per_abdomen + unidadper_abdomen, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(per_cadera + unidadper_cadera, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Perímetro torácico en inspiración/espiración", fontColumnValue)) { Colspan = 6, Rowspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(perimetro_tor_inspiracion + unidadperimetro_tor_inspiracion, fontColumnValue)) { Colspan = 2, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(perimetro_tor_espiracion + unidadperimetro_tor_espiracion, fontColumnValue)) { Colspan = 2, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
            
                new PdfPCell(new Phrase("ICC", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(icc + unidadicc, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EVALUACIÓN CARDIOVASCULAR
            var evaluacion_cardiovascular_ec = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_EVALUACION_CLINICA_EC) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_EVALUACION_CLINICA_EC).v_Value1;
            string evaluacion_cardiovascular_ec_1 = "", evaluacion_cardiovascular_ec_2 = "";
            if (evaluacion_cardiovascular_ec == "1") evaluacion_cardiovascular_ec_2 = "X";
            else if (evaluacion_cardiovascular_ec == "2") evaluacion_cardiovascular_ec_1 = "X";
            var evaluacion_cardiovascular_ec_obs = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA).v_Value1;

            var evaluacion_cardiovascular_ekg = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_EKG_EC) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_EKG_EC).v_Value1;
            string evaluacion_cardiovascular_ekg_1 = "", evaluacion_cardiovascular_ekg_2 = "";
            if (evaluacion_cardiovascular_ekg == "1") evaluacion_cardiovascular_ekg_2 = "X";
            else if (evaluacion_cardiovascular_ekg == "2") evaluacion_cardiovascular_ekg_1 = "X";
            var evaluacion_cardiovascular_ekg_obs = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_EKG) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_EKG).v_Value1;

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("4. Evaluación Cardiovascular", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("a) Evaluación clínica", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_cardiovascular_ec_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("ANORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_cardiovascular_ec_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Precisar anomalía:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(antec, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("b) EKG", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_cardiovascular_ekg_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("ANORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_cardiovascular_ekg_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Precisar anomalía:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_cardiovascular_ekg_obs, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EVALUACIÓN PULMONAR
            var evaluacion_pulmonar_ec = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_EVALUACION_CLINICA_EP) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_EVALUACION_CLINICA_EP).v_Value1;
            string evaluacion_pulmonar_ec_1 = "", evaluacion_pulmonar_ec_2 = "";
            if (evaluacion_pulmonar_ec == "1") evaluacion_pulmonar_ec_2 = "X";
            else if (evaluacion_pulmonar_ec == "2") evaluacion_pulmonar_ec_1 = "X";
            var evaluacion_pulmonar_ec_obs = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_EC) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_EC).v_Value1;

            var evaluacion_pulmonar_e = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_ESPIROMETRIA_EP) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_ESPIROMETRIA_EP).v_Value1;
            string evaluacion_pulmonar_e_1 = "", evaluacion_pulmonar_e_2 = "";
            if (evaluacion_pulmonar_e == "1") evaluacion_pulmonar_e_2 = "X";
            else if (evaluacion_pulmonar_e == "2") evaluacion_pulmonar_e_1 = "X";
            var evaluacion_pulmonar_e_obs = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_E) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_E).v_Value1;

            var evaluacion_pulmonar_rt = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_RADIOGRAFIA_TORAX_EP) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_RADIOGRAFIA_TORAX_EP).v_Value1;
            string evaluacion_pulmonar_rt_1 = "", evaluacion_pulmonar_rt_2 = "";
            if (evaluacion_pulmonar_rt == "1") evaluacion_pulmonar_rt_2 = "X";
            else if (evaluacion_pulmonar_rt == "2") evaluacion_pulmonar_rt_1 = "X";
            var evaluacion_pulmonar_rt_obs = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_RT) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_RT).v_Value1;


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("5. Evaluación Pulmonar", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("a) Evaluación clínica", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_ec_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("ANORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_ec_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Precisar anomalía:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_ec_obs, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("b) Espirometría", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_e_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("ANORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_e_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Precisar anomalía:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_e_obs, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("c) Radiografía de tórax", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_rt_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("ANORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_rt_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Precisar anomalía:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_pulmonar_rt_obs, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EVALUACIÓN NEUROLÓGICA
            var evaluacion_neurologica_ec = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_EVALUACION_CLINICA_EN) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_EVALUACION_CLINICA_EN).v_Value1;
            string evaluacion_neurologica_ec_1 = "", evaluacion_neurologica_ec_2 = "";
            if (evaluacion_neurologica_ec == "1") evaluacion_neurologica_ec_2 = "X";
            else if (evaluacion_neurologica_ec == "2") evaluacion_neurologica_ec_1 = "X";
            var evaluacion_neurologica_ec_obs = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_EP) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_PRECISAR_ANOMALIA_EP).v_Value1;

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("5. Evaluación Neurológica", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("a) Evaluación clínica", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_neurologica_ec_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("ANORMAL", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_neurologica_ec_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Precisar anomalía:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(evaluacion_neurologica_ec_obs, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region CUESTIONARIO DE CLAUSTROFOBIA
            var res_claustrofobia = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_RESULTADO_EVALUACION) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_RESULTADO_EVALUACION).v_Value1;


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("7. Cuestionario de Claustrofobia CLAUSTROPHOBIA QUESTIONNAIRE (CLQ)*", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase(res_claustrofobia == null ? "SIN RESULTADOS":res_claustrofobia, fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region CONCLUSIONES - RECOMENDACIONES - RESTRICCIONES
            var conclusiones = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_DESCRIPCION_C) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_DESCRIPCION_C).v_Value1;


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("8. Conclusiones, recomendaciones y restricciones", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase(conclusiones == null ? "SIN RESULTADOS":conclusiones, fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region CALIFICACIÓN

            var apto = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_APTO) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_APTO).v_Value1;
            string apto_1 = "";
            if (apto == "1") apto_1 = "X";

            var no_apto = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_NO_APTO) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_NO_APTO).v_Value1;
            string no_apto_1 = "";
            if (no_apto == "1") no_apto_1 = "X";

            var fecha_desde = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_DESDE) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_DESDE).v_Value1;

            var fecha_hasta = cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_HASTA) == null ? "" : cert_suf_med_ec.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_HASTA).v_Value1;


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("9. Calificación", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                
                new PdfPCell(new Phrase("Apto", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(apto_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Vigencia", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("No Apto", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(no_apto_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Desde :    " + fecha_desde + "\n" + "Hasta :   " + fecha_hasta, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
               
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls

            // Firma del trabajador ***************************************************
            PdfPCell cellFirmaTrabajador = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 70, 30));
            else

                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));
            PdfPCell cellHuellaTrabajador = null;

            //DirectoryInfo rutaHuella = null;
            //rutaHuella = new DirectoryInfo(WebConfigurationManager.AppSettings["FirmaHuella"].ToString());
            //iTextSharp.text.Image Huellajpg = iTextSharp.text.Image.GetInstance(rutaHuella + DataService.v_DocNumber + "_Huella.jpg");

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 30, 30));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));
            //cellHuellaTrabajador = new PdfPCell(Huellajpg);

            // Firma del doctor Auditor **************************************************

            PdfPCell cellFirma = null;

            if (usuariograbo.Firma != null)
                cellFirma = new PdfPCell(HandlingItextSharp.GetImage(usuariograbo.Firma, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            else
                cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

            #endregion

            #region Crear tablas en duro (para la Firma y huella del trabajador)

            cells = new List<PdfPCell>();

            cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaTrabajador.Border = PdfPCell.NO_BORDER;
            cellFirmaTrabajador.FixedHeight = 40F;
            cells.Add(cellFirmaTrabajador);
            cells.Add(new PdfPCell(new Phrase("FIRMA DEL EXAMINADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            columnWidths = new float[] { 100f };

            var tableFirmaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            //***********************************************

            cells = new List<PdfPCell>();

            cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaTrabajador.Border = PdfPCell.NO_BORDER;
            cellHuellaTrabajador.FixedHeight = 40F;
            cells.Add(cellHuellaTrabajador);
            cells.Add(new PdfPCell(new Phrase("HUELLA DEL EXAMINADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            columnWidths = new float[] { 100f };

            var tableHuellaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            #endregion

            cells = new List<PdfPCell>();

            // 1 celda vacia              
            cells.Add(new PdfPCell(tableFirmaTrabajador));

            // 1 celda vacia
            cells.Add(new PdfPCell(tableHuellaTrabajador));

            // 2 celda
            cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue)) { Rowspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cells.Add(cell);

            // 3 celda (Imagen)
            cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirma.FixedHeight = 40F;
            cells.Add(cellFirma);

            cells.Add(new PdfPCell(new Phrase("CON LA CUAL DECLARA QUE LA INFORMACIÓN DECLARADA ES VERAZ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE });

            columnWidths = new float[] { 35f, 35f, 30f, 40F };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(table);

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
        #endregion
    }
}
