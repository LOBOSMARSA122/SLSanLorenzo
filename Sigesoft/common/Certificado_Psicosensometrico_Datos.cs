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
    public class Certificado_Psicosensometrico_Datos
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        #region 
        public static void CreateCertificadoPsicosensometricoDatos(PacientList filiationData,
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF,UsuarioGrabo usuariograbo)
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

            var tamaño_celda = 20f;
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
                    new PdfPCell(new Phrase("CERTIFICADO DE APTITUD DE EVALUACIÓN PSICOSENSOMETRICO", fontTitle1)) { BackgroundColor= BaseColor.ORANGE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS Generales
            
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            string[] fechaInforme = datosPac.FechaActualizacion.ToString().Split(' ');
            string[] fechanac= datosPac.d_Birthdate.ToString().Split(' ');

            ServiceComponentList psicosensometrico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID);
            var nLicencia = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_N_LICENCIA) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_N_LICENCIA).v_Value1;
            var clase = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_LICENCIA_CLASE_CATEGORIA) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_LICENCIA_CLASE_CATEGORIA).v_Value1;
            var vehiculo = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_EQUIPO_VEHICULO) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_EQUIPO_VEHICULO).v_Value1;

            var primer_Ev = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_1_EVALUACION) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_1_EVALUACION).v_Value1;
            string primer_Ev_1 = "";
            if (primer_Ev == "1") primer_Ev_1 = "X";
            var primer_ReEv = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_1_RE_EVALUACION) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_1_RE_EVALUACION).v_Value1;
            string primer_ReEv_1 = "";
            if (primer_ReEv == "1") primer_ReEv_1 = "X";
            var segunda_ReEv = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_2_RE_EVALUACION) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_2_RE_EVALUACION).v_Value1;
            string segunda_ReEv_1 = "";
            if (segunda_ReEv == "1") segunda_ReEv_1 = "X";

            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("N° de Informe", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.N_Informe, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Fecha de Evaluación:", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha de Informe:", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(fechaInforme[0], fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                 new PdfPCell(new Phrase("Empresa", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(empresageneral, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Contrata", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(empr_Conct, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("Apellidos Y Nombres", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.Edad.ToString(), fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha de nacimiento: ", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(fechanac[0] , fontColumnValue)) { Colspan = 8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Genero", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.Genero, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("N° Licencia de Conducir", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(nLicencia, fontColumnValue)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Telefono:", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.v_TelephoneNumber , fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Clase y Categoría de Licencia", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(clase, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Equipo/Vehículo a operar (*):", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(vehiculo, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Documento de Identidad (DNI)", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("1ER Evaluación", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(primer_Ev_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("1ER Re-Evaluación", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(primer_ReEv_1, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("2DA Re-Evaluación", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(segunda_ReEv_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region RESULTADOS


            var agudeza_visual = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_AGUDEZA_VISUAL) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_AGUDEZA_VISUAL).v_Value1;
            string agudeza_visual_1 = "";
            if (agudeza_visual == "1") agudeza_visual_1 = "EVALUADO";
            else if (agudeza_visual == "2") agudeza_visual_1 = "APROBADO";
            else if (agudeza_visual == "3") agudeza_visual_1 = "REPROBADO";

            var test_palancas = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_PALANCAS) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_PALANCAS).v_Value1;
            string test_palancas_1 = "";
            if (test_palancas == "1") test_palancas_1 = "EVALUADO";
            else if (test_palancas == "2") test_palancas_1 = "APROBADO";
            else if (test_palancas == "3") test_palancas_1 = "REPROBADO";

            var vision_profundidad = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_VISION_PROFUNDIDAD) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_VISION_PROFUNDIDAD).v_Value1;
            string vision_profundidad_1 = "";
            if (vision_profundidad == "1") vision_profundidad_1 = "EVALUADO";
            else if (vision_profundidad == "2") vision_profundidad_1 = "APROBADO";
            else if (vision_profundidad == "3") vision_profundidad_1 = "REPROBADO";

            var test_punteo = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_PUNTEO) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_PUNTEO).v_Value1;
            string test_punteo_1 = "";
            if (test_punteo == "1") test_punteo_1 = "EVALUADO";
            else if (test_punteo == "2") test_punteo_1 = "APROBADO";
            else if (test_punteo == "3") test_punteo_1 = "REPROBADO";

            var discriminacion_colores = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_DISCRIMINACION_COLORES) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_DISCRIMINACION_COLORES).v_Value1;
            string discriminacion_colores_1 = "";
            if (discriminacion_colores == "1") discriminacion_colores_1 = "DISCROMATOPSIA";
            else if (discriminacion_colores == "2") discriminacion_colores_1 = "APROBADO";
            else if (discriminacion_colores == "3") discriminacion_colores_1 = "REPROBADO";

            var test_reaccion_simple = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_REACCION_SIMPLE) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_REACCION_SIMPLE).v_Value1;
            string test_reaccion_simple_1 = "";
            if (test_reaccion_simple == "1") test_reaccion_simple_1 = "EVALUADO";
            else if (test_reaccion_simple == "2") test_reaccion_simple_1 = "APROBADO";
            else if (test_reaccion_simple == "3") test_reaccion_simple_1 = "REPROBADO";

            var test_foria = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_FORIA) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_FORIA).v_Value1;
            string test_foria_1 = "";
            if (test_foria == "1") test_foria_1 = "EVALUADO";
            else if (test_foria == "2") test_foria_1 = "APROBADO";
            else if (test_foria == "3") test_foria_1 = "REPROBADO";

            var test_anticipacion = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_ANTICIPACION) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_ANTICIPACION).v_Value1;
            string test_anticipacion_1 = "";
            if (test_anticipacion == "1") test_anticipacion_1 = "EVALUADO";
            else if (test_anticipacion == "2") test_anticipacion_1 = "APROBADO";
            else if (test_anticipacion == "3") test_anticipacion_1 = "REPROBADO";

            var vision_nocturna = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_VISION_NOCTURNA) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_VISION_NOCTURNA).v_Value1;
            string vision_nocturna_1 = "";
            if (vision_nocturna == "1") vision_nocturna_1 = "EVALUADO";
            else if (vision_nocturna == "2") vision_nocturna_1 = "APROBADO";
            else if (vision_nocturna == "3") vision_nocturna_1 = "REPROBADO";

            var test_reacciones_multiples = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_REACCIONES_MULTIPLES) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_REACCIONES_MULTIPLES).v_Value1;
            string test_reacciones_multiples_1 = "";
            if (test_reacciones_multiples == "1") test_reacciones_multiples_1 = "EVALUADO";
            else if (test_reacciones_multiples == "2") test_reacciones_multiples_1 = "APROBADO";
            else if (test_reacciones_multiples == "3") test_reacciones_multiples_1 = "REPROBADO";

            var encandilamiento = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_ENCANDILAMIENTO) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_ENCANDILAMIENTO).v_Value1;
            string encandilamiento_1 = "";
            if (encandilamiento == "1") encandilamiento_1 = "EVALUADO";
            else if (encandilamiento == "2") encandilamiento_1 = "APROBADO";
            else if (encandilamiento == "3") encandilamiento_1 = "REPROBADO";

            var monotonia = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_REACCION_MONOTONIA_CANSANCIO) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_REACCION_MONOTONIA_CANSANCIO).v_Value1;
            string monotonia_1 = "";
            if (monotonia == "1") monotonia_1 = "EVALUADO";
            else if (monotonia == "2") monotonia_1 = "APROBADO";
            else if (monotonia == "3") monotonia_1 = "REPROBADO";

            var perimetria = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_PERIMETRIA) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_PERIMETRIA).v_Value1;
            string perimetria_1 = "";
            if (perimetria == "1") perimetria_1 = "EVALUADO";
            else if (perimetria == "2") perimetria_1 = "APROBADO";
            else if (perimetria == "3") perimetria_1 = "REPROBADO";

            var coordinacion = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_COORDINACION_BIMANUAL) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_TEST_COORDINACION_BIMANUAL).v_Value1;
            string coordinacion_1 = "";
            if (coordinacion == "1") coordinacion_1 = "EVALUADO";
            else if (coordinacion == "2") coordinacion_1 = "APROBADO";
            else if (coordinacion == "3") coordinacion_1 = "REPROBADO";

            var oido_derecho = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_OIDO_DERECHO) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_OIDO_DERECHO).v_Value1;
            string oido_derecho_1 = "";
            if (oido_derecho == "1") oido_derecho_1 = "EVALUADO";
            else if (oido_derecho == "2") oido_derecho_1 = "APROBADO";
            else if (oido_derecho == "3") oido_derecho_1 = "REPROBADO";

            var oido_izquierdo = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_OIDO_IZQUIERDO) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_OIDO_IZQUIERDO).v_Value1;
            string oido_izquierdo_1 = "";
            if (oido_izquierdo == "1") oido_izquierdo_1 = "EVALUADO";
            else if (oido_izquierdo == "2") oido_izquierdo_1 = "APROBADO";
            else if (oido_izquierdo == "3") oido_izquierdo_1 = "REPROBADO";

            var resultado_final = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_RESULTADO_FINAL) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_RESULTADO_FINAL).v_Value1;
            string resultado_final_1 = "";
            if (resultado_final == "1") resultado_final_1 = "EVALUADO";
            else if (resultado_final == "2") resultado_final_1 = "APROBADO";
            else if (resultado_final == "3") resultado_final_1 = "REPROBADO";

            var restricciones = psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_RESTRICCIONES) == null ? "" : psicosensometrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_RESTRICCIONES).v_Value1;
           
            cells = new List<PdfPCell>
            {          
                
                new PdfPCell(new Phrase("Agudeza Visual", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(agudeza_visual_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Test de Palancas", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(test_palancas_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("Visión de Profundidad", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(vision_profundidad_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Test de punteo", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(test_punteo_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("Discriminación de Colores", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(discriminacion_colores_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Test de reacción simple", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(test_reaccion_simple_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("Test de Foria", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(test_foria_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Test de anticipación", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(test_anticipacion_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("Visión Nocturna", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(vision_nocturna_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Test de reacciones múltiples", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(test_reacciones_multiples_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("Test de Encandilamiento", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(encandilamiento_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Test de reacción a la monotonía y cansancio", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(monotonia_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("Test de Perimetría o Test de campo visual", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(perimetria_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Test de coordinación bimanual", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(coordinacion_1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("Audición", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Oído Derecho", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(oido_derecho_1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Oído Izquierdo", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(oido_izquierdo_1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
               
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 3,BackgroundColor=BaseColor.GRAY}, 
                new PdfPCell(new Phrase("Resultado Final", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(resultado_final_1, fontColumnValue)) { Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Restricciones", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(restricciones, fontColumnValue)) { Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 

             };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Firma y sello Médico
            PdfPCell cellFirmaTrabajador = null;
            PdfPCell cellHuellaTrabajador = null;
            PdfPCell cellFirmaDoctor = null;
         
            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 100, 35));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 45, 60));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;


            if (usuariograbo.Firma != null)
                cellFirmaDoctor = new PdfPCell(HandlingItextSharp.GetImage(usuariograbo.Firma, null, null, 120, 55));
            else
                cellFirmaDoctor = new PdfPCell(new Phrase(" ", fontColumnValue));

                cellFirmaDoctor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellFirmaDoctor.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;


            cells = new List<PdfPCell>
            {
              //Linea

                    new PdfPCell(cellFirmaDoctor) {Rowspan = 8, Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight=100},   
                    new PdfPCell(cellFirmaTrabajador) {Rowspan = 8, Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight=100, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(cellHuellaTrabajador) {Rowspan = 8, Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight=100, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase("Firma del Evaluador", fontColumnValue)){ Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                    new PdfPCell(new Phrase("Firma del Evaluado", fontColumnValue)){ Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda},

                    new PdfPCell(new Phrase("Nombre", fontColumnValue)){ Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                    new PdfPCell(new Phrase(usuariograbo.Nombre, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("Nombre", fontColumnValue)){ Colspan=1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                    new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    

                    new PdfPCell(new Phrase("Registro", fontColumnValue)){ Colspan=1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT, FixedHeight = tamaño_celda},
                    new PdfPCell(new Phrase(usuariograbo.CMP, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    

             };

            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion


            #region NOTA
            cells = new List<PdfPCell>
            {          
                
                new PdfPCell(new Phrase("(*) Definirá la categoría que obliga al reglamento en MTC, para los casos de los equipos fuera de carretera, como tractores, rodillos, etc., los parámetros de aplicar será los correspondientes a la licencia AIIIC", fontColumnValue)) { Colspan = 30, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30 },    
              
             };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
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
