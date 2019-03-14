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
    public class FichaPsicologicaGoldfields
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        #region Reporte ficha
        public static void CreateFichaPsicologicaGoldfields(PacientList filiationData,
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF, UsuarioGrabo DatosGrabo)
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 42f, 41f);

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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            var tamaño_celda = 11f;
            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresa.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FICHA PSICOLÓGICA OCUPACIONAL", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS GENERALES
            string preOcp = "", Ocup="", postOcup="";
            if (filiationData.i_EsoTypeId == 1)
            {
                preOcp = "X";
            }
            else if (filiationData.i_EsoTypeId == 2)
            {
                Ocup = "X";
            }
            else if (filiationData.i_EsoTypeId == 3)
            {
                postOcup = "X";
            }
           

            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');

            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;

            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("CLÍNICA:", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(infoEmpresa.v_Name, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha de Entrevista", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("I. Datos Generales:", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("Apellidos Y Nombres", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Edad", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString() , fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Fecha de Nacimiento:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(datosPac.d_Birthdate.ToString().Split(' ')[0], fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Estado Civil:", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_MaritalStatus, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Empresa", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(empr_Conct, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Cargo", fontColumnValueBold)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
              
                new PdfPCell(new Phrase("Tipo de Evaluación", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Pre Ocupacional" , fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(preOcp , fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Ocupacional", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(Ocup, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Post Ocupacional" , fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(postOcup , fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            ServiceComponentList ficha_psico_goldfields = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS);

            #region DATOS OCUPACIONALES
            //var empresa_Actual = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_EMPRESA_ACTUAL) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_EMPRESA_ACTUAL).v_Value1;
            var area_trabajo = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_AREA_LUGAR) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_AREA_LUGAR).v_Value1;
            var provincia_depart = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PROVINCIA_DEPARTAMENTO) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PROVINCIA_DEPARTAMENTO).v_Value1;
            var tiempo_laborando = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_TIEMPO_TOTAL_LAB) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_TIEMPO_TOTAL_LAB).v_Value1;
            var experiencia = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_EXPERIENCIA_PUESTO) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_EXPERIENCIA_PUESTO).v_Value1;
            var riesgos = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PRINCIPALES_RIESGOS) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PRINCIPALES_RIESGOS).v_Value1;
            var medidasSeguridad = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_MEDIDAS_SEGURIDAD) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_MEDIDAS_SEGURIDAD).v_Value1;

            var ingreso1 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_FECHA_INGRESO1) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_FECHA_INGRESO1).v_Value1;
            var empresa1 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_NOMBRE_EMPRESA1) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_NOMBRE_EMPRESA1).v_Value1;
            var actividad1 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_ACTIVIDAD_EMPRESA1) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_ACTIVIDAD_EMPRESA1).v_Value1;
            var puesto1 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PUESTO_EMPRESA1) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PUESTO_EMPRESA1).v_Value1;
            var tiempo1 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PERMANENCIA_TIEMPO1) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PERMANENCIA_TIEMPO1).v_Value1;
            var salida1 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PERMANENCIA_FECHA_SALIDA1) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PERMANENCIA_FECHA_SALIDA1).v_Value1;
            var cese1 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_CAUSA_CESE1) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_CAUSA_CESE1).v_Value1;

            var ingreso2 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_FECHA_INGRESO2) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_FECHA_INGRESO2).v_Value1;
            var empresa2 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_NOMBRE_EMPRESA2) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_NOMBRE_EMPRESA2).v_Value1;
            var actividad2 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_ACTIVIDAD_EMPRESA2) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_ACTIVIDAD_EMPRESA2).v_Value1;
            var puesto2 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PUESTO_EMPRESA2) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PUESTO_EMPRESA2).v_Value1;
            var tiempo2 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PERMANENCIA_TIEMPO2) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PERMANENCIA_TIEMPO2).v_Value1;
            var salida2 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PERMANENCIA_FECHA_SALIDA2) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_PERMANENCIA_FECHA_SALIDA2).v_Value1;
            var cese2 = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_CAUSA_CESE2) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_CAUSA_CESE2).v_Value1;


            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("II. Datos Ocupacionale:", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("2.1 Empresa Actual", fontColumnValueBold)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },    
                
                new PdfPCell(new Phrase("Área, lugar de trabajo", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(area_trabajo, fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Provincia, departamento", fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(provincia_depart, fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
              
                new PdfPCell(new Phrase("Tiempo total laborando", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(tiempo_laborando, fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Experiencia en el puesto", fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(experiencia, fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
              
                new PdfPCell(new Phrase("Principales riesgos", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(riesgos, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Medidas de seguridad", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(medidasSeguridad, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("2.2 Anteriores Empresas (Experiencia Laboral): ", fontColumnValueBold)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE, FixedHeight=1f},    

                new PdfPCell(new Phrase("Fecha de ingreso", fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Nombre de la Empresa", fontColumnValue)) { Colspan = 3,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Actividad de la Empresa", fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Puesto", fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Permanencia", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Causa de cese", fontColumnValue)) { Colspan = 4, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Tiempo", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha salida", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase(ingreso1, fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(empresa1, fontColumnValue)) { Colspan = 3,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(actividad1, fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(puesto1, fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(tiempo1, fontColumnValue)) { Colspan = 2, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(salida1, fontColumnValue)) { Colspan = 2, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(cese1, fontColumnValue)) { Colspan = 4, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase(ingreso2, fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(empresa2, fontColumnValue)) { Colspan = 3,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(actividad2, fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(puesto2, fontColumnValue)) { Colspan = 3, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(tiempo2, fontColumnValue)) { Colspan = 2, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(salida2, fontColumnValue)) { Colspan = 2, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(cese2, fontColumnValue)) { Colspan = 4, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region HISTORIA FAMILIAR
            var historia_familiar = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_HISTORIA_FAMILIAR) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_HISTORIA_FAMILIAR).v_Value1;
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("III. Historia Familiar (Con quien vive actualmente, pasatiempos, actividades fuera del trabajo", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(historia_familiar, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region HISTORIA FAMILIAR
            var enfermedades_accidentes = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_ENFERMEDADES_ACCIDENTES) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_ENFERMEDADES_ACCIDENTES).v_Value1;
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("IV Enfermedades y accidentes (Durante el tiempo del trabajo)", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(enfermedades_accidentes, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ENFERMEDADES Y ACCIDENTES
            var enfermedad_salud_mental = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_ENFERMEDAD_SALUD_MENTAL) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_ENFERMEDAD_SALUD_MENTAL).v_Value1;
            string enfermedad_salud_mentalSINO ="";
            if (enfermedad_salud_mental=="1") enfermedad_salud_mentalSINO="SI";
            if (enfermedad_salud_mental=="0") enfermedad_salud_mentalSINO="NO";
            var descripcion_salud_mental = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_DESCRIPCION_SALUD_MENTAL) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_DESCRIPCION_SALUD_MENTAL).v_Value1;
            var otras_enfermedades_accidentes = ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_OTRAS_ENFEREMDADES) == null ? "" : ficha_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_GOLDFIELDS_OTRAS_ENFEREMDADES).v_Value1;

            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("V. Antecedentes personales", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("a) Para ser llenado por el trabajador", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },       

                new PdfPCell(new Phrase("Yo, " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName + "con DNI: " +datosPac.v_DocNumber+ " manifiesto que " +enfermedad_salud_mentalSINO +" se me ha identificado una", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("enfermedad de salud mental, o psiquiátrica.", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                
                new PdfPCell(new Phrase("\n En el caso de marcar SI, describir en breve palabras el diagnostico de la enfermedad mental, y si recibió tratamiento;", fontColumnValueApendice)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE }, 
            
                new PdfPCell(new Phrase("b) Otras enfermedades de salud, y/o accidentes:", fontColumnValueBold)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase(otras_enfermedades_accidentes, fontColumnValue)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ENFERMEDADES Y ACCIDENTES
         
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("VI. Antecedentes personales", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
               
                new PdfPCell(new Phrase("Con la firma del presente documento, confirmo y doy fe que la información brindada sobre mi salud es completa y verdadera, haciéndome", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("\nresponsable de cualquier omisión o dato incorrecto; así mismo quedo obligado a informar toda circunstancia nueva que pueda influir", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("\no alterar esta información.", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            //var psico = serviceComponent.Find(p => p.i_CategoryId == (int)Sigesoft.Common.Consultorio.Psicología);

            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls
            PdfPCell cellFirmaTrabajador = null;
            PdfPCell cellHuellaTrabajador = null;
            PdfPCell cellFirma = null;

            // Firma del trabajador ***************************************************


            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 80, 40));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaTrabajador.FixedHeight = 50F;
            // Huella del trabajador **************************************************

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 30, 45));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellHuellaTrabajador.FixedHeight = 50F;
            // Firma del doctor Auditor **************************************************
            if (DatosGrabo.Firma != null)
                cellFirma = new PdfPCell(HandlingItextSharp.GetImage(DatosGrabo.Firma, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            else
                cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirma.FixedHeight = 50F;
            #endregion

            cells = new List<PdfPCell>()
            {
                new PdfPCell(cellFirmaTrabajador){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(cellHuellaTrabajador){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("FIRMA Y SELLO DEL PSICÓLOGO", fontColumnValueBold)) {Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(cellFirma){Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
 
                new PdfPCell(new Phrase("FIRMA DEL EXAMINADO", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},    
                new PdfPCell(new Phrase("HUELLA DEL EXAMINADO", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},    

                new PdfPCell(new Phrase("CON LA CUAL DECLARA QUE LA INFORMACIÓN DECLARADA ES VERAZ", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    

            };
            columnWidths = new float[] { 25f, 25f, 25f, 25f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
        #endregion
    }
}
