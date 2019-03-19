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
    public class EXAMEN_SUF_MED_OPERADORES_EQUIPOS_MOVILES
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        #region Report
        public static void CreateExamenSuficienciaMedicaOperadores(PacientList filiationData,
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF, UsuarioGrabo usuariograbo)
        {

            Document document = new Document(PageSize.A4, 50f, 30f, 45f, 41f);

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

            var tamaño_celda = 18f;
            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresa.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("EXAMEN DE SUFICIENCIA MÉDICA PARA OPERADORES DE EQUIPOS MOVILES \n V 2.0 (RD N° 13674-2007-MTC/15) \n FP-TAN-UM 03-06-06", fontTitle1)) { BackgroundColor= BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS Generales

            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            string[] fechaInforme = datosPac.FechaActualizacion.ToString().Split(' ');

            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;

            cells = new List<PdfPCell>()
            {          
              
                new PdfPCell(new Phrase("Postulante", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.Edad.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Nivel de Instrucción", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.GradoInstruccion, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Ocupación", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Razón Social", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(empr_Conct, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("DNI", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            ServiceComponentList suficiencia_medica = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_ID);
            #region ANAMBESIS CLINICA
            var crsisis_convulsiva = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_CRISIS_CONV) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_CRISIS_CONV).v_Value1;
            string crsisis_convulsiva_1 = "";
            if (crsisis_convulsiva == "1") crsisis_convulsiva_1 = "X";
            else if (crsisis_convulsiva == "0") crsisis_convulsiva_1 = "-";

            var crsisis_ausencia = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_CRISIS_AUSEN) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_CRISIS_AUSEN).v_Value1;
            string crsisis_ausencia_1 = "";
            if (crsisis_ausencia == "1") crsisis_ausencia_1 = "X";
            else if (crsisis_ausencia == "0") crsisis_ausencia_1 = "-";

            var mov_invluntario = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_MOV_INVOLUNTARIO) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_MOV_INVOLUNTARIO).v_Value1;
            string mov_invluntario_1 = "";
            if (mov_invluntario == "1") mov_invluntario_1 = "X";
            else if (mov_invluntario == "0") mov_invluntario_1 = "-";

            var medicacion_psico = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_MEDITACION_PCOTROP) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_MEDITACION_PCOTROP).v_Value1;
            string medicacion_psico_1 = "";
            if (medicacion_psico == "1") medicacion_psico_1 = "X";
            else if (medicacion_psico == "0") medicacion_psico_1 = "-";

            var diabetes_melitus = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_DIABETES_MELITUS) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_DIABETES_MELITUS).v_Value1;
            string diabetes_melitus_1 = "";
            if (diabetes_melitus == "1") diabetes_melitus_1 = "X";
            else if (diabetes_melitus == "0") diabetes_melitus_1 = "-";

            var renal_cronica = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_INSUFICIENCIA_RENAL_CRO) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_INSUFICIENCIA_RENAL_CRO).v_Value1;
            string renal_cronica_1 = "";
            if (renal_cronica == "1") renal_cronica_1 = "X";
            else if (renal_cronica == "1") renal_cronica_1 = "X";

            var insf_cardiaca_cong = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_INSUF_CARDIACA_CONG) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_INSUF_CARDIACA_CONG).v_Value1;
            string insf_cardiaca_cong_1 = "";
            if (insf_cardiaca_cong == "1") insf_cardiaca_cong_1 = "X";
            else if (insf_cardiaca_cong == "0") insf_cardiaca_cong_1 = "-";

            var insf_coronaria = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_INSUF_CORONARIA_CRON) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_INSUF_CORONARIA_CRON).v_Value1;
            string insf_coronaria_1 = "";
            if (insf_coronaria == "1") insf_coronaria_1 = "X";
            else if (insf_coronaria == "0") insf_coronaria_1 = "-";

            var drogas_ilegales = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_CONSUMO_DROGAS_ILEGALES) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_CONSUMO_DROGAS_ILEGALES).v_Value1;
            string drogas_ilegales_1 = "";
            if (drogas_ilegales == "1") drogas_ilegales_1 = "X";
            else if (drogas_ilegales == "0") drogas_ilegales_1 = "-";

            var dipropia_no_corregida = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_DIPOPIA_NO_CORREGIDA) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_DIPOPIA_NO_CORREGIDA).v_Value1;
            string dipropia_no_corregida_1 = "";
            if (dipropia_no_corregida == "1") dipropia_no_corregida_1 = "X";
            else if (dipropia_no_corregida == "0") dipropia_no_corregida_1 = "-";

            var arrtimia_cardiaca = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_ARRITMIA_CARDIACA) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_ARRITMIA_CARDIACA).v_Value1;
            string arrtimia_cardiaca_1 = "";
            if (arrtimia_cardiaca == "1") arrtimia_cardiaca_1 = "X";
            else if (arrtimia_cardiaca == "0") arrtimia_cardiaca_1 = "-";

            var hipertension_maligna = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_HIPERTENSION_MALIGNA) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_HIPERTENSION_MALIGNA).v_Value1;
            string hipertension_maligna_1 = "";
            if (hipertension_maligna == "1") hipertension_maligna_1 = "X";
            else if (hipertension_maligna == "0") hipertension_maligna_1 = "-";

            var marcapaso = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_PORTADOR_MARCAPASO_CARDIACO) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_PORTADOR_MARCAPASO_CARDIACO).v_Value1;
            string marcapaso_1 = "";
            if (marcapaso == "1") marcapaso_1 = "X";
            else if (marcapaso == "0") marcapaso_1 = "-";

            var protesis_valvular = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_PORTADOR_PROTESIS_VALVULAR) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_1_PORTADOR_PROTESIS_VALVULAR).v_Value1;
            string protesis_valvular_1 = "";
            if (protesis_valvular == "1") protesis_valvular_1 = "X";
            else if (protesis_valvular == "0") protesis_valvular_1 = "-";

            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3f },    
                new PdfPCell(new Phrase("1- ANAMNESIS CLINICA", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3f },    

                new PdfPCell(new Phrase(crsisis_convulsiva_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Crisis convulsivas", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(crsisis_ausencia_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Crisis de ausencia", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(mov_invluntario_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Movimientos involuntarios", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(medicacion_psico_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Medicación psicotrópica", fontColumnValue)) { Colspan = 8 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(diabetes_melitus_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Diabetes melitus no controlada", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(renal_cronica_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Insuficiencia renal crónica", fontColumnValue)) { Colspan = 8 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(insf_cardiaca_cong_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Insuficiencia cardiaca congestiva", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(insf_coronaria_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Insuficiencia coronaria crónica", fontColumnValue)) { Colspan = 8 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(drogas_ilegales_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Consumo de drogas ilegales", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(dipropia_no_corregida_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Diplopía no corregida", fontColumnValue)) { Colspan = 8 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(arrtimia_cardiaca_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Arritmia cardiaca", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(hipertension_maligna_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Hipertensión maligna", fontColumnValue)) { Colspan = 8 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(marcapaso_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Portador de marcapaso cardiaco", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(protesis_valvular_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Portador de prótesis valvular", fontColumnValue)) { Colspan = 8 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ANTECEDENTES PSIQUIATRICOS
            var intranquilidad = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_INTRANQUILIDAD) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_INTRANQUILIDAD).v_Value1;
            string intranquilidad_1 = "";
            if (intranquilidad == "1") intranquilidad_1 = "X";
            else if (intranquilidad == "0") intranquilidad_1 = "-";

            var problemas_sueño = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_PROBLEMAS_SUEÑO) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_PROBLEMAS_SUEÑO).v_Value1;
            string problemas_sueño_1 = "";
            if (problemas_sueño == "1") problemas_sueño_1 = "X";
            else if (problemas_sueño == "0") problemas_sueño_1 = "-";

            var labilidad_emocinal = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_LABILIDAD_EMOCIONAL) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_LABILIDAD_EMOCIONAL).v_Value1;
            string labilidad_emocinal_1 = "";
            if (labilidad_emocinal == "1") labilidad_emocinal_1 = "X";
            else if (labilidad_emocinal == "0") labilidad_emocinal_1 = "-";

            var bajo_peso = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_BAJA_PESO_RECIENTE) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_BAJA_PESO_RECIENTE).v_Value1;
            string bajo_peso_1 = "";
            if (bajo_peso == "1") bajo_peso_1 = "X";
            else if (bajo_peso == "0") bajo_peso_1 = "-";

            var fobia_altura = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_FOBIAS_ALTURA) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_FOBIAS_ALTURA).v_Value1;
            string fobia_altura_1 = "";
            if (fobia_altura == "1") fobia_altura_1 = "X";
            else if (fobia_altura == "0") fobia_altura_1 = "-";

            var consumo_drogas = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_CONSUMO_DROGAS) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_2_CONSUMO_DROGAS).v_Value1;
            string consumo_drogas_1 = "";
            if (consumo_drogas == "1") consumo_drogas_1 = "X";
            else if (consumo_drogas == "0") consumo_drogas_1 = "-";

            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3f },    
                new PdfPCell(new Phrase("2- ANTECEDENTES PSIQUIATRICOS", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3f },    

                new PdfPCell(new Phrase(intranquilidad_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Intranquilidad con tremor o sudor", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(problemas_sueño_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Problemas de sueño", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(labilidad_emocinal_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Labilidad emocional", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(bajo_peso_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Bajo peso reciente", fontColumnValue)) { Colspan = 8 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(fobia_altura_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fobias debidas a la altura", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(consumo_drogas_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Consumo de drogas", fontColumnValue)) { Colspan = 8 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
             };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EXAMEN OCUPACIONAL
            var funciones_vitales = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_FUNCIONES_VITALES) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_FUNCIONES_VITALES).v_Value1;
            string funciones_vitales_1 = "";
            if (funciones_vitales == "1") funciones_vitales_1 = "X";
            else if (funciones_vitales == "0") funciones_vitales_1 = "-";

            var aparato_locomotor = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_APARATO_LOCOMOTOR) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_APARATO_LOCOMOTOR).v_Value1;
            string aparato_locomotor_1 = "";
            if (aparato_locomotor == "1") aparato_locomotor_1 = "X";
            else if (aparato_locomotor == "0") aparato_locomotor_1 = "-";

            var fuerza_muscular = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_FUERZA_MUSCULAR) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_FUERZA_MUSCULAR).v_Value1;
            string fuerza_muscular_1 = "";
            if (fuerza_muscular == "1") fuerza_muscular_1 = "X";
            else if (fuerza_muscular == "0") fuerza_muscular_1 = "-";

            var pruebas_vestibulares = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_PRUEBAS_VESTIBULARES) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_PRUEBAS_VESTIBULARES).v_Value1;
            string pruebas_vestibulares_1 = "";
            if (pruebas_vestibulares == "1") pruebas_vestibulares_1 = "X";
            if (pruebas_vestibulares == "0") pruebas_vestibulares_1 = "-";

            var agudeza_visual = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_AGUDEZA_VISUAL) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_AGUDEZA_VISUAL).v_Value1;
            string agudeza_visual_1 = "";
            if (agudeza_visual == "1") agudeza_visual_1 = "X";
            else if (agudeza_visual == "0") agudeza_visual_1 = "-";

            var vision_cromatica = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_VISION_CROMATICA) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_VISION_CROMATICA).v_Value1;
            string vision_cromatica_1 = "";
            if (vision_cromatica == "1") vision_cromatica_1 = "X";
            else if (vision_cromatica == "0") vision_cromatica_1 = "-";

            var vision_estereoscopica = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_VISION_ESTEREOSCOPICA) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_VISION_ESTEREOSCOPICA).v_Value1;
            string vision_estereoscopica_1 = "";
            if (vision_estereoscopica == "1") vision_estereoscopica_1 = "X";
            else if (vision_estereoscopica == "0") vision_estereoscopica_1 = "-";

            var umbral_auditivo = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_UMBRAL_AUDITIVO) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_3_UMBRAL_AUDITIVO).v_Value1;
            string umbral_auditivo_1 = "";
            if (umbral_auditivo == "1") umbral_auditivo_1 = "X";
            else if (umbral_auditivo == "0") umbral_auditivo_1 = "-";

            cells = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3f },    
                new PdfPCell(new Phrase("3- EXAMEN OCUPACIONAL", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3f },    

                new PdfPCell(new Phrase(funciones_vitales_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Funciones vitales: FC > 100 x, FR > 20 x, PAS > 160 mm Hg, PAD > 100 mm Hg", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(aparato_locomotor_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Aparato locomotor::  Déficit estructural o funcional de columna, miembros superiores e inferiores", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(fuerza_muscular_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fuerza muscular: Incapacidad para elevar peso de 5 Kg con cada mano 5 veces consecutivas por encima de la cabeza", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(pruebas_vestibulares_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Anomalía en las Pruebas índice-índice, índice-nariz y Romberg", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(agudeza_visual_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Agudeza visual:  Inferior a 20/20 en cualquier ojo con o sin correctores", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase(vision_cromatica_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Visión cromática:  Déficit de la visión de colores con el Test de Ishihara", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase(vision_estereoscopica_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Visión estereoscópica:  Menos del 80% de aciertos con Láminas de Estereopsis", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase(umbral_auditivo_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Umbral auditivo:  Audición mínima promedio de cualquier oído mayor de 40 dB para operadores de superficie y mayor de 25 dB para operadores de interior mina en las frecuencias de 500, 1000 y 2000 Hz", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

             };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region RESULTADOS
            var apto_si = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_4_APTO_TRABAJO_SI) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_4_APTO_TRABAJO_SI).v_Value1;
            string apto_si_1 = "";
            if (apto_si == "1") apto_si_1 = "X";

            var apto_no = suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_4_APTO_TRABAJO_NO) == null ? "" : suficiencia_medica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_4_APTO_TRABAJO_NO).v_Value1;
            string apto_no_1 = "";
            if (apto_no == "1") apto_no_1 = "X";

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3f },    

                new PdfPCell(new Phrase("APTO PARA TRABAJAR", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("SI", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(apto_si_1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NO", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(apto_no_1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3f },    

             };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Firma y sello Médico
            PdfPCell cellFirmaTrabajador = null;
            PdfPCell cellFirmaDoctor = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 100, 35));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (usuariograbo.Firma != null)
                cellFirmaDoctor = new PdfPCell(HandlingItextSharp.GetImage(usuariograbo.Firma, null, null, 120, 55));
            else
                cellFirmaDoctor = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaDoctor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaDoctor.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;


            cells = new List<PdfPCell>
            {          
                
              //Linea
                    new PdfPCell(cellFirmaTrabajador) {Rowspan = 8, Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight=100},   
                    new PdfPCell(cellFirmaDoctor) {Rowspan = 8, Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight=100},   
                    new PdfPCell(new Phrase("Firma del evaluado", fontColumnValue)){ Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda},
                    new PdfPCell(new Phrase("Firma  y sello del médico evaluador", fontColumnValue)){ Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
             };

            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, };
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
