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
    public class AtencionIntegral
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateAtencionIntegral(string filePDF,
            PacientList datosPac,
            organizationDto infoEmpresaPropietaria,
            List<ServiceComponentList> exams)
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
            iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            imagenMinsa.ScalePercent(10);
            imagenMinsa.SetAbsolutePosition(400, 785);
            document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL DE SALUD", fontTitle1)) { BackgroundColor= BaseColor.ORANGE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Valores
            
            //Antropometria
            ServiceComponentList antro = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            var peso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
            var talla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_Value1;
            var imc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_Value1;


            //Funciones Vitales
            ServiceComponentList funcVit = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
            var temp = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID).v_Value1;
            var pres_Sist = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
            var pres_Diast = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;
            var frecCard = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_Value1;
            var frecResp = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_Value1;


            //Atención Integral
            ServiceComponentList atenInte = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ID);
            var motCons = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_MOT_CONSULTA) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_MOT_CONSULTA).v_Value1;
            var tiempoEnf = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_TIEMPO_EMF) == null ? "" :atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_TIEMPO_EMF).v_Value1;
            var apetito = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APETITO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APETITO).v_Value1;
            var sed = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SED) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SED).v_Value1;
            var sueño = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SUEÑO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SUEÑO).v_Value1;
            var estAnimo = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_EST_ANIMO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_EST_ANIMO).v_Value1;
            var orina = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ORINA) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ORINA).v_Value1;
            var depos = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_DEPOSICIONES) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_DEPOSICIONES).v_Value1;
            var examFis = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_EXAM_FISICO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_EXAM_FISICO).v_Value1;
            var obser = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_OBSERVACIONES) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_OBSERVACIONES).v_Value1;

            
            #endregion

            #region Primera página
            #region Datos del Servicio

            string[] servicio = datosPac.FechaServicio.ToString().Split(' ');
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("CONSULTA MÉDICA", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, Colspan = 3, BackgroundColor=BaseColor.ORANGE, MinimumHeight=15f},  

                    new PdfPCell(new Phrase("Fecha: " + servicio[0], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f},
                    new PdfPCell(new Phrase("Hora: " + servicio[1], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },
                    new PdfPCell(new Phrase("Edad: " + datosPac.Edad + " años.", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },
                };

            columnWidths = new float[] { 40f, 40f, 20f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region Descarte de Signos de Peligro
            if (datosPac.Edad<=12) //verificar que sea niño
            {
                //niño
                ServiceComponentList niño = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ID);
                ///2
                var no_mama_2m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_NO_MAMAR_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_NO_MAMAR_2M).v_Value1;
                string no_mama_2m_1 = "", no_mama_2m_2 = "";
                if (no_mama_2m == "1") no_mama_2m_1 = "X";
                else if (no_mama_2m == "0") no_mama_2m_2 = "X";

                var convulsiones = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_CONVULSIONES_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_CONVULSIONES_2M).v_Value1;
                string convulsiones_1 = "", convulsiones_2 = "";
                if (convulsiones == "1") convulsiones_1 = "X";
                else if (convulsiones == "0") convulsiones_2 = "X";

                var fontanela = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_FONTANELA_ABOMB_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_FONTANELA_ABOMB_2M).v_Value1;
                string fontanela_1 = "", fontanela_2 = "";
                if (fontanela == "1") fontanela_1 = "X";
                else if (fontanela == "0") fontanela_2 = "X";

                var enroj_omb = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ENROJ_OMBLIGO_EXT_PIEL_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ENROJ_OMBLIGO_EXT_PIEL_2M).v_Value1;
                string enroj_omb_1 = "", enroj_omb_2 = "";
                if (enroj_omb == "1") enroj_omb_1 = "X";
                else if (enroj_omb == "0") enroj_omb_2 = "X";

                var fieb_tem_ba = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_FIEBRE_TEMP_BAJA_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_FIEBRE_TEMP_BAJA_2M).v_Value1;
                string fieb_tem_ba_1 = "", fieb_tem_ba_2 = "";
                if (fieb_tem_ba == "1") fieb_tem_ba_1 = "X";
                else if (fieb_tem_ba == "0") fieb_tem_ba_2 = "X";

                var rigi_nunca = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_RIGIDEZ_NUNCA_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_RIGIDEZ_NUNCA_2M).v_Value1;
                string rigi_nunca_1 = "", rigi_nunca_2 = "";
                if (rigi_nunca == "1") rigi_nunca_1 = "X";
                else if (rigi_nunca == "0") rigi_nunca_2 = "X";

                var postulas_much = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_POSTULAS_MUCHAS_EXT_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_POSTULAS_MUCHAS_EXT_2M).v_Value1;
                string postulas_much_1 = "", postulas_much_2 = "";
                if (postulas_much == "1") postulas_much_1 = "X";
                else if (postulas_much == "0") postulas_much_2 = "X";

                var letar_comatoso = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETAR_COMATOSO_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETAR_COMATOSO_2M).v_Value1;
                string letar_comatoso_1 = "", letar_comatoso_2 = "";
                if (letar_comatoso == "1") letar_comatoso_1 = "X";
                else if (letar_comatoso == "0") letar_comatoso_2 = "X";
                ///4
                var no_toma_pecho = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_NO_BEBER_TOMAR_PECHO_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_NO_BEBER_TOMAR_PECHO_2_4M).v_Value1;
                string no_toma_pecho_1 = "", no_toma_pecho_2 = "";
                if (no_toma_pecho == "1") no_toma_pecho_1 = "X";
                else if (no_toma_pecho == "0") no_toma_pecho_2 = "X";

                var convulsiones_4m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_CONVULSIONES_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_CONVULSIONES_2_4M).v_Value1;
                string convulsiones_4m_1 = "", convulsiones_4m_2 = "";
                if (convulsiones_4m == "1") convulsiones_4m_1 = "X";
                else if (convulsiones_4m == "0") convulsiones_4m_2 = "X";

                var letar_comat_4m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETAR_COMAT_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETAR_COMAT_2_4M).v_Value1;
                string letar_comat_4m_1 = "", letar_comat_4m_2 = "";
                if (letar_comat_4m == "1") letar_comat_4m_1 = "X";
                else if (letar_comat_4m == "0") letar_comat_4m_2 = "X";

                var vomito_4m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_VOMITO_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_VOMITO_2_4M).v_Value1;
                string vomito_4m_1 = "", vomito_4m_2 = "";
                if (vomito_4m == "1") vomito_4m_1 = "X";
                else if (vomito_4m == "0") vomito_4m_2 = "X";

                var estridor_4m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESTRIDOR_REPOSO_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESTRIDOR_REPOSO_2_4M).v_Value1;
                string estridor_4m_1 = "", estridor_4m_2 = "";
                if (estridor_4m == "1") estridor_4m_1 = "X";
                else if (estridor_4m == "0") estridor_4m_2 = "X";
                ///todos
                var emaciaciacion_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_EMACIACION_VISIBLE_GRAVE_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_EMACIACION_VISIBLE_GRAVE_T).v_Value1;
                string emaciaciacion_t_1 = "", emaciaciacion_t_2 = "";
                if (emaciaciacion_t == "1") emaciaciacion_t_1 = "X";
                else if (emaciaciacion_t == "0") emaciaciacion_t_2 = "X";

                var piel_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PIEL_VUELVE_LENTO_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PIEL_VUELVE_LENTO_T).v_Value1;
                string piel_t_1 = "", piel_t_2 = "";
                if (piel_t == "1") piel_t_1 = "X";
                else if (piel_t == "0") piel_t_2 = "X";

                var traumatismo_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_TRAUMATISMO_QUEMADURAS_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_TRAUMATISMO_QUEMADURAS_T).v_Value1;
                string traumatismo_t_1 = "", traumatismo_t_2 = "";
                if (traumatismo_t == "1") traumatismo_t_1 = "X";
                else if (traumatismo_t == "0") traumatismo_t_2 = "X";

                var envenenamiento_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ENVENENAMIENTO_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ENVENENAMIENTO_T).v_Value1;
                string envenenamiento_t_1 = "", envenenamiento_t_2 = "";
                if (envenenamiento_t == "1") envenenamiento_t_1 = "X";
                else if (envenenamiento_t == "0") envenenamiento_t_2 = "X";

                var palidez_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PALIDEZ_PALMAR_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PALIDEZ_PALMAR_T).v_Value1;
                string palidez_t_1 = "", palidez_t_2 = "";
                if (palidez_t == "1") palidez_t_1 = "X";
                else if (palidez_t == "0") palidez_t_2 = "X";
                ////tos
                var respiracion_rapida = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_RESPIRACION_RAPIDA_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_RESPIRACION_RAPIDA_TOS).v_Value1;
                string respiracion_rapida_1 = "", respiracion_rapida_2 = "";
                if (respiracion_rapida == "1") respiracion_rapida_1 = "X";
                else if (respiracion_rapida == "0") respiracion_rapida_2 = "X";

                var observar_tiraje_subcostal = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OBSERVAR_TIRAJE_SUBCOSTAL_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OBSERVAR_TIRAJE_SUBCOSTAL_TOS).v_Value1;
                string observar_tiraje_subcostal_1 = "", observar_tiraje_subcostal_2 = "";
                if (observar_tiraje_subcostal == "1") observar_tiraje_subcostal_1 = "X";
                else if (observar_tiraje_subcostal == "0") observar_tiraje_subcostal_2 = "X";

                var escuchar_estridor = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESCUCHAR_ESTRIDOR_REPOSO_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESCUCHAR_ESTRIDOR_REPOSO_TOS).v_Value1;
                string escuchar_estridor_1 = "", escuchar_estridor_2 = "";
                if (escuchar_estridor == "1") escuchar_estridor_1 = "X";
                else if (escuchar_estridor == "0") escuchar_estridor_2 = "X";

                var escuchar_sibilancias = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESCUCHAR_SIBILANCIAS_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESCUCHAR_SIBILANCIAS_TOS).v_Value1;
                string escuchar_sibilancias_1 = "", escuchar_sibilancias_2 = "";
                if (escuchar_sibilancias == "1") escuchar_sibilancias_1 = "X";
                else if (escuchar_sibilancias == "0") escuchar_sibilancias_2 = "X";

                var observar_tiraje_subcostal_grave = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OBSERVAR_TRIAJE_SUBCOSTAL_GRAVE_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OBSERVAR_TRIAJE_SUBCOSTAL_GRAVE_TOS).v_Value1;
                string observar_tiraje_subcostal_grave_1 = "", observar_tiraje_subcostal_grave_2 = "";
                if (observar_tiraje_subcostal_grave == "1") observar_tiraje_subcostal_grave_1 = "X";
                else if (observar_tiraje_subcostal_grave == "0") observar_tiraje_subcostal_grave_2 = "X";

                var sibilancias_1ra = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIBILANCIAS_1RA_VEZ_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIBILANCIAS_1RA_VEZ_TOS).v_Value1;
                string sibilancias_1ra_1 = "", sibilancias_1ra_2 = "";
                if (sibilancias_1ra == "1") sibilancias_1ra_1 = "X";
                else if (sibilancias_1ra == "0") sibilancias_1ra_2 = "X";

                var sibilancias_recurrente = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIBILANCIAS_RECURRENTE_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIBILANCIAS_RECURRENTE_TOS).v_Value1;
                string sibilancias_recurrente_1 = "", sibilancias_recurrente_2 = "";
                if (sibilancias_recurrente == "1") sibilancias_recurrente_1 = "X";
                else if (sibilancias_recurrente == "0") sibilancias_recurrente_2 = "X";
                ///diarrea
                var sangre_heces = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SANGRE_HECES_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SANGRE_HECES_DIARREA).v_Value1;
                string sangre_heces_1 = "", sangre_heces_2 = "";
                if (sangre_heces == "1") sangre_heces_1 = "X";
                else if (sangre_heces == "0") sangre_heces_2 = "X";

                var letargio_comatoso = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETARGICO_COMATOSO_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETARGICO_COMATOSO_DIARREA).v_Value1;
                string letargio_comatoso_1 = "", letargio_comatoso_2 = "";
                if (letargio_comatoso == "1") letargio_comatoso_1 = "X";
                else if (letargio_comatoso == "0") letargio_comatoso_2 = "X";

                var intranquilo = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_INTRANQUILO_IRRITABLE_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_INTRANQUILO_IRRITABLE_DIARREA).v_Value1;
                string intranquilo_1 = "", intranquilo_2 = "";
                if (intranquilo == "1") intranquilo_1 = "X";
                else if (intranquilo == "0") intranquilo_2 = "X";

                var ojos_hundidos = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OJOS_HUNDIDOS_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OJOS_HUNDIDOS_DIARREA).v_Value1;
                string ojos_hundidos_1 = "", ojos_hundidos_2 = "";
                if (ojos_hundidos == "1") ojos_hundidos_1 = "X";
                else if (ojos_hundidos == "0") ojos_hundidos_2 = "X";

                var puede_beber = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PUEDE_BEBER_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PUEDE_BEBER_DIARREA).v_Value1;
                string puede_beber_1 = "", puede_beber_2 = "";
                if (puede_beber == "1") puede_beber_1 = "X";
                else if (puede_beber == "0") puede_beber_2 = "X";

                var bebe_Avidamente = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_BEBE_AVIDAMENTE_SED_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_BEBE_AVIDAMENTE_SED_DIARREA).v_Value1;
                string bebe_Avidamente_1 = "", bebe_Avidamente_2 = "";
                if (bebe_Avidamente == "1") bebe_Avidamente_1 = "X";
                else if (bebe_Avidamente == "0") bebe_Avidamente_2 = "X";

                var sig_pliegue_piel = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIG_PLIEGUE_PIEL_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIG_PLIEGUE_PIEL_DIARREA).v_Value1;
                string sig_pliegue_piel_1 = "", sig_pliegue_piel_2 = "";
                if (sig_pliegue_piel == "1") sig_pliegue_piel_1 = "X";
                else if (sig_pliegue_piel == "0") sig_pliegue_piel_2 = "X";

                var sig_pliegue_piel_cutaneo = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIGNO_PLIEGUE_CUTANEO_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIGNO_PLIEGUE_CUTANEO_DIARREA).v_Value1;
                string sig_pliegue_piel_cutaneo_1 = "", sig_pliegue_piel_cutaneo_2 = "";
                if (sig_pliegue_piel_cutaneo == "1") sig_pliegue_piel_cutaneo_1 = "X";
                else if (sig_pliegue_piel_cutaneo == "0") sig_pliegue_piel_cutaneo_2 = "X";
                cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("(NIÑO) - Descarte de signos de peligro:", fontTitle2)) { Colspan=18, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.ORANGE },
  
                    new PdfPCell(new Phrase("MENOS DE 2 MESES:", fontTitle2)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F},
                    new PdfPCell(new Phrase("NO", fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },

                    new PdfPCell(new Phrase("DE 2 MESES A 4 AÑOS:", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F},
                    new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F},
                    new PdfPCell(new Phrase("NO", fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },

                    new PdfPCell(new Phrase("PARA TODAS LAS EDADES:", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    new PdfPCell(new Phrase("NO", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("No quiere mamar, no succiona", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase(no_mama_2m_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(no_mama_2m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("No puede beber o tomar el pecho", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(no_toma_pecho_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(no_toma_pecho_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Emaciación visible grave", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(emaciaciacion_t_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(emaciaciacion_t_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(convulsiones_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(convulsiones_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(convulsiones_4m_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(convulsiones_4m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Piel vuelve muy lentamente", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(piel_t_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(piel_t_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Fontanela abombada", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(fontanela_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(fontanela_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Letárgico o comatoso", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(letar_comat_4m_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(letar_comat_4m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Traumatismo - Quemaduras", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(traumatismo_t_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(traumatismo_t_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Enrojecimiento del ombligo se extiende a la piel", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(enroj_omb_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(enroj_omb_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Vomita todo", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(vomito_4m_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(vomito_4m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Envenenamiento", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(envenenamiento_t_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(envenenamiento_t_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Fiebre o temperatura baja", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase(fieb_tem_ba_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(fieb_tem_ba_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Estridor en reposo - Tiraje subcostal", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(estridor_4m_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(estridor_4m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Palidez palmar intenso", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(palidez_t_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(palidez_t_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Rigidez de nuca", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(rigi_nunca_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(rigi_nunca_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},

                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase("", fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    new PdfPCell(new Phrase("Pústulas muchas y extensas", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(postulas_much_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(postulas_much_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},

                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Letárquico o comatoso", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(letar_comatoso_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(letar_comatoso_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("TOS O DIFICULTADES RESPIRATORIAS", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    new PdfPCell(new Phrase("NO", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    new PdfPCell(new Phrase("SINTOMAS DE DIARREA", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
                    new PdfPCell(new Phrase("NO", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Respiración Rápida", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(respiracion_rapida_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(respiracion_rapida_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("HAY SANGRE EN LAS HECES", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(sangre_heces_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(sangre_heces_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

                    new PdfPCell(new Phrase("Observar tiraje subcostal", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(observar_tiraje_subcostal_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(observar_tiraje_subcostal_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("Letargico o Comatoso", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(letargio_comatoso_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(letargio_comatoso_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

                    new PdfPCell(new Phrase("Observar escuchar estridor", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(escuchar_estridor_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(escuchar_estridor_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("Intranquilo o Irritable", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(intranquilo_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(intranquilo_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

                    new PdfPCell(new Phrase("Escuchar Sibilancias / Tos", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(escuchar_sibilancias_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(escuchar_sibilancias_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("Tiene los ojos hundidos", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(ojos_hundidos_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(ojos_hundidos_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

                    new PdfPCell(new Phrase("Observar tiraje subcostal - Tos grave", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(observar_tiraje_subcostal_grave_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(observar_tiraje_subcostal_grave_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("Ofrecer líquido al niño", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(puede_beber_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(puede_beber_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

                    new PdfPCell(new Phrase("Sibilancias 1ra vez", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(sibilancias_1ra_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(sibilancias_1ra_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("Bebe avidamente con sed", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(bebe_Avidamente_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(bebe_Avidamente_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

                    new PdfPCell(new Phrase("Sibilancias recurrente", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(sibilancias_recurrente_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase(sibilancias_recurrente_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("SIG. Pliegue Piel", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(sig_pliegue_piel_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(sig_pliegue_piel_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

                    new PdfPCell(new Phrase("", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase("Signo de pliegue cutáneo", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
                    new PdfPCell(new Phrase(sig_pliegue_piel_cutaneo_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase(sig_pliegue_piel_cutaneo_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
                document.Add(table);
            }
            #endregion
            #region Categoria del Adulto Mayor
            if (datosPac.Edad > 64)//Validar que sea adulto mayor
            {
                // Adulto Mayor
                ServiceComponentList adulto_mayor = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_ID);

                var saludable = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SALUDABLE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SALUDABLE).v_Value1;
                string saludable_1 = "";
                if (saludable == "1") saludable_1 = "X";

                var fragil = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_FRAGIL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_FRAGIL).v_Value1;
                string fragil_1 = "";
                if (fragil == "1") fragil_1 = "X";

                var enfermo = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_ENFERMO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_ENFERMO).v_Value1;
                string enfermo_1 = "";
                if (enfermo == "1") enfermo_1 = "X";

                var geriatrico = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_GERIATRICO_COMPLEJO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_GERIATRICO_COMPLEJO).v_Value1;
                string geriatrico_1 = "";
                if (geriatrico == "1") geriatrico_1 = "X";

                var independiente = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DEPENDIENTE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DEPENDIENTE).v_Value1;
                string independiente_1 = "";
                if (independiente == "1") independiente_1 = "X";

                var dependiente = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_INDEPENDIENTE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_INDEPENDIENTE).v_Value1;
                string dependiente_1 = "";
                if (dependiente == "1") dependiente_1 = "X";

                var total = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_TOTAL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_TOTAL).v_Value1;

                var ec_normal = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_NORMAL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_NORMAL).v_Value1;
                string ec_normal_1 = "";
                if (ec_normal == "1") ec_normal_1 = "X";

                var ec_leve = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_LEVE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_LEVE).v_Value1;
                string ec_leve_1 = "";
                if (ec_leve == "1") ec_leve_1 = "X";

                var ec_moderado = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_MODERADO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_MODERADO).v_Value1;
                string ec_moderado_1 = "";
                if (ec_moderado == "1") ec_moderado_1 = "X";

                var severo = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SEVERO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SEVERO).v_Value1;
                string severo_1 = "";
                if (severo == "1") severo_1 = "X";

                var fecha_hoy = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_FECHA_DE_HOY) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_FECHA_DE_HOY).v_Value1;
                string fecha_hoy_1 = "";
                if (fecha_hoy == "1") fecha_hoy_1 = "X";

                var dia_semana = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DIA_DE_SEMANA) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DIA_DE_SEMANA).v_Value1;
                string dia_semana_1 = "";
                if (dia_semana == "1") dia_semana_1 = "X";

                var lugar_estamos = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_LUGAR_ESTAMOS) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_LUGAR_ESTAMOS).v_Value1;
                string lugar_estamos_1 = "";
                if (lugar_estamos == "1") lugar_estamos_1 = "X";

                var numero_telefono_direccion = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR__NUMERO_TELEFONO_DIRECCION) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR__NUMERO_TELEFONO_DIRECCION).v_Value1;
                string numero_telefono_direccion_1 = "";
                if (numero_telefono_direccion == "1") numero_telefono_direccion_1 = "X";

                var años_tiene = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_AÑOS_TIENE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_AÑOS_TIENE).v_Value1;
                string años_tiene_1 = "";
                if (años_tiene == "1") años_tiene_1 = "X";

                var donde_nacio = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DONDE_NACIO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DONDE_NACIO).v_Value1;
                string donde_nacio_1 = "";
                if (donde_nacio == "1") donde_nacio_1 = "X";

                var nombre_presidente = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_NOMBRE_PRESIDENTE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_NOMBRE_PRESIDENTE).v_Value1;
                string nombre_presidente_1 = "";
                if (nombre_presidente == "1") nombre_presidente_1 = "X";

                var nombre_past_president = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_NOMBRE_ANTERIOR_PRESIDENTE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_NOMBRE_ANTERIOR_PRESIDENTE).v_Value1;
                string nombre_past_president_1 = "";
                if (nombre_past_president == "1") nombre_past_president_1 = "X";

                var primer_apellido_madre = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PRIMER_APELLIDO_MADRE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PRIMER_APELLIDO_MADRE).v_Value1;
                string primer_apellido_madre_1 = "";
                if (primer_apellido_madre == "1") primer_apellido_madre_1 = "X";

                var restar_3 = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_RESTAR_3) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_RESTAR_3).v_Value1;

                var ea_sin_md = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_A_SIN_MD) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_A_SIN_MD).v_Value1;
                string ea_sin_md_1 = "";
                if (ea_sin_md == "1") ea_sin_md_1 = "X";

                var ea_con_md = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_A_CON_MD) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_A_CON_MD).v_Value1;
                string ea_con_md_1 = "";
                if (ea_con_md == "1") ea_con_md_1 = "X";

                var buena = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_BUENA) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_BUENA).v_Value1;
                string buena_1 = "";
                if (buena == "1") buena_1 = "X";

                var riesgo_social = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_RIESGO_SOCIAL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_RIESGO_SOCIAL).v_Value1;
                string riesgo_social_1 = "";
                if (riesgo_social == "1") riesgo_social_1 = "X";

                var problema_social = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PROBLEMA_SOCIAL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PROBLEMA_SOCIAL).v_Value1;
                string problema_social_1 = "";
                if (problema_social == "1") problema_social_1 = "X";

                var vida_satisfecho = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SATISFECHO_VIDA) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SATISFECHO_VIDA).v_Value1;
                string vida_satisfecho_1 = "", vida_satisfecho_2 ="";
                if (vida_satisfecho == "1") vida_satisfecho_1 = "X";
                else if (vida_satisfecho == "0") vida_satisfecho_2 = "X";

                var impotente_indefenso = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_IMPOTENTE_INDEFENSO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_IMPOTENTE_INDEFENSO).v_Value1;
                string impotente_indefenso_1 = "", impotente_indefenso_2 = "";
                if (impotente_indefenso == "1") impotente_indefenso_1 = "X";
                else if (impotente_indefenso == "0") impotente_indefenso_2 = "X";

                var problemas_memoria = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PROBLEMAS_MEMORIA) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PROBLEMAS_MEMORIA).v_Value1;
                string problemas_memoria_1 = "", problemas_memoria_2 = "";
                if (problemas_memoria == "1") problemas_memoria_1 = "X";
                else if (problemas_memoria == "0") problemas_memoria_2 = "X";

                var desgano_imposibilitado = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DESGANO_IMPOSIBILITADO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DESGANO_IMPOSIBILITADO).v_Value1;
                string desgano_imposibilitado_1 = "", desgano_imposibilitado_2 = "";
                if (desgano_imposibilitado == "1") desgano_imposibilitado_1 = "X";
                else if (desgano_imposibilitado == "0") desgano_imposibilitado_2 = "X";


                cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("(A.M.)ESTADO DE ENFERMEDAD: ", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 20, BackgroundColor = BaseColor.ORANGE, MinimumHeight=15f },

                    new PdfPCell(new Phrase("SALUDABLE", fontTitle2)) { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(saludable_1, fontTitle2)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
                    new PdfPCell(new Phrase("FRAGIL", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(fragil_1, fontTitle2)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
                    new PdfPCell(new Phrase("ENFERMO", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15f },
                    new PdfPCell(new Phrase(enfermo_1, fontTitle2)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15f  },
                    new PdfPCell(new Phrase("GERIATRICO COMPLEJO", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(geriatrico_1, fontTitle2)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },

                    new PdfPCell(new Phrase("I. FUNCIONAL", fontColumnValueBold)) {Colspan=20, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },

                    new PdfPCell(new Phrase("Independiente", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT , MinimumHeight=15f },
                    new PdfPCell(new Phrase(independiente_1, fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15f  },
                    new PdfPCell(new Phrase("Dependiente", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT , MinimumHeight=15f },
                    new PdfPCell(new Phrase(dependiente_1, fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
                    new PdfPCell(new Phrase("Total", fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(total, fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
      
                    new PdfPCell(new Phrase("II. Mental", fontColumnValueBold)) {Colspan=20,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },

                    new PdfPCell(new Phrase("2.1 Valoración Cognitiva (DC: Deterioro Cognitivo)", fontColumnValue)) {Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("Normal", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(ec_normal_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("DC Leve", fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(ec_leve_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase("DC Moderado", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(ec_moderado_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
                    new PdfPCell(new Phrase("DC Severo", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(severo_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },

                    new PdfPCell(new Phrase("2.2 Estado Cognitivo", fontColumnValue)) {Colspan=5, Rowspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=45f },

                    new PdfPCell(new Phrase("¿Cuál es la fecha de hoy?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(fecha_hoy_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("¿Qué día de la semana?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(dia_semana_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("¿En qué lugar estamos? (cualquier descripción correcta del lugar)", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(lugar_estamos_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    
                    new PdfPCell(new Phrase("¿Cuál es su número de teléfono? ó ¿Cuál es su dirección completa?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(numero_telefono_direccion_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("¿Cuántos años tiene", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(años_tiene_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("¿Dónde nació?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(donde_nacio_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },

                    new PdfPCell(new Phrase("¿Cuál es el nombre del presidente del Perú?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(nombre_presidente_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("¿Cuál es el nombre del anterior presidente del Perú?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(nombre_past_president_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("Dígame el primer apellido de su madre", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(primer_apellido_madre_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    
                    new PdfPCell(new Phrase("Restar de 3 en 3 desde 30 (cualquier error hace errónea la respuesta)", fontColumnValue)) {Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(restar_3, fontColumnValue)) {Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
                   
 
                    new PdfPCell(new Phrase("2.3 Estado Afectivo", fontColumnValue)) {Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
                    new PdfPCell(new Phrase("Sin manifestaciones depresivas", fontColumnValue)) {Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(ea_sin_md_1, fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase("Con manifestaciones depresivas", fontTitle2)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(ea_con_md_1, fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },

                    new PdfPCell(new Phrase("Preguntas", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("Sí", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase("Preguntas", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase("Sí", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },


                    new PdfPCell(new Phrase("¿Está satisfecho con su vida?", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(vida_satisfecho_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase(vida_satisfecho_2, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase("¿Se siente impotente o indefenso?", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(impotente_indefenso_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase(impotente_indefenso_2, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },

                    new PdfPCell(new Phrase("¿Tiene problemas de memoria?", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(problemas_memoria_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase(problemas_memoria_2, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase("¿Siente desgano o se siente imposibilitado respecto a actividades e intereses?", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    new PdfPCell(new Phrase(desgano_imposibilitado_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
                    new PdfPCell(new Phrase(desgano_imposibilitado_2, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },

                    new PdfPCell(new Phrase("III. SOCIO-FAMILIAR", fontColumnValueBold)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  }, 
                    
                    new PdfPCell(new Phrase("Buena", fontColumnValue)) {Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },
                    new PdfPCell(new Phrase(buena_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase("Riesgo social", fontColumnValue)) {Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15f },
                    new PdfPCell(new Phrase(riesgo_social_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
                    new PdfPCell(new Phrase("Problema Social", fontTitle2)) {Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
                    new PdfPCell(new Phrase(problema_social_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15f  },
                };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
                document.Add(table);
            }
            #endregion
            #region Consulta / Enfermedad
            cellsTit = new List<PdfPCell>()
                { 
                    
                    new PdfPCell(new Phrase("DATOS GENERALES DE LA CONSULTA", fontColumnValue)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=15F },
                    
                    new PdfPCell(new Phrase("Motivo de consulta", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase(motCons, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase("Tiempo de enfermedad", fontColumnValueBold)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(tiempoEnf, fontColumnValue)) {Colspan=6 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },

                    new PdfPCell(new Phrase("Apetito", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(apetito, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase("Sed", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(sed, fontColumnValue)) { Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },

                    new PdfPCell(new Phrase("Sueño", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(sueño, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase("Estado de ánimo", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},
                    new PdfPCell(new Phrase(estAnimo, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },

                    new PdfPCell(new Phrase("Orina", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(orina, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase("Deposiciones", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(depos, fontColumnValue)) { Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },

                    new PdfPCell(new Phrase("T°:", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(temp + "° C", fontColumnValue)) {Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("PA:", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(pres_Sist + "/"+  pres_Diast+ " mmHg", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
                    new PdfPCell(new Phrase("FC", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(frecCard + " Lat/min", fontColumnValue)) {Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
                    new PdfPCell(new Phrase("FR:", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase(frecResp + " R/min", fontColumnValue)) {Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15F},
                    new PdfPCell(new Phrase("Peso:", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase(peso + " Kg.", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15F},
                    new PdfPCell(new Phrase("Talla:", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
                    new PdfPCell(new Phrase(talla + " m.", fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15F},
                    new PdfPCell(new Phrase("IMC", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
                    new PdfPCell(new Phrase(imc + " Kg/m2", fontColumnValue)) {Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

                    new PdfPCell(new Phrase("Ex. Físico", fontColumnValueBold)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase(examFis, fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },

                     new PdfPCell(new Phrase("Observaciones", fontColumnValueBold)) {Colspan=20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase(obser, fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },

                };

            columnWidths = new float[] { 3f, 5f, 5f, 3f, 5f, 5f, 3f, 5f, 5f, 3f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 3f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
           

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
