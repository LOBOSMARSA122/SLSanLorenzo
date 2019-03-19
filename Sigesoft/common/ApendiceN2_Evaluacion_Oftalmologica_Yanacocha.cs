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
    public class ApendiceN2_Evaluacion_Oftalmologica_Yanacocha
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateApendiceN2_Evaluacion_Oftalmologica_Yanacocha(PacientList filiationData,
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF, UsuarioGrabo DatosGrabo, List<ServiceComponentList> ExamenesServicio, List<DiagnosticRepositoryList> Diagnosticos)
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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

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
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("APÉNDICE N° 02: EVALUACIÓN OFTALMOLÓGICA", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);
            document.Add(table);
            #endregion
            #region DATOS GENERALES
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            string preOcp = "", Ocup = "", postOcup = "";
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

            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;

            cells = new List<PdfPCell>()
            {                     
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
    
                new PdfPCell(new Phrase("\nApellidos Y Nombres", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName +" "+datosPac.v_SecondLastName+", "+ datosPac.v_FirstName, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Edad", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString() + " Años" , fontColumnValue)) { Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Evaluación:" , fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Pre Ocupacional" , fontColumnValue1)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("( "+ preOcp + " )" , fontColumnValue1)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Ocupacional", fontColumnValue1)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("( "+ Ocup + " )", fontColumnValue1)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Post Ocupacional" , fontColumnValue1)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("( "+ postOcup + " )" , fontColumnValue1)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
            
                new PdfPCell(new Phrase("Empresa", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(empr_Conct, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
            
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);
            document.Add(table);
            #endregion
            ServiceComponentList apendice2Yanacocha = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);

            #region EXAMENES

            var vcscod = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCSCOD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCSCOD).v_Value1Name;
            var vcscoi = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCSCOI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCSCOI).v_Value1Name;
            var vcccod = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCCCOD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCCCOD).v_Value1Name;
            var vcccoi = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCCCOI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCCCOI).v_Value1Name;

            var vlscod = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLSCOD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLSCOD).v_Value1Name;
            var vlscoi = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLSCOI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLSCOI).v_Value1Name;
            var vlccod = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLCCOD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLCCOD).v_Value1Name;
            var vlccoi = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLCCOI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLCCOI).v_Value1Name;

            var enfermedadesOculares = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_ENFERMEDADES_OCULARES) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_ENFERMEDADES_OCULARES).v_Value1;

            var testIshihara = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_TEST_ISHIHARA) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_TEST_ISHIHARA).v_Value1Name;
            var reflejosPupilares = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_REFLEJOS_PUPILARES) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_REFLEJOS_PUPILARES).v_Value1Name;

            var maculaOD = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_MACULA_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_MACULA_OD).v_Value1Name;
            var maculaOI = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_MACULA_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_MACULA_OI).v_Value1Name;
            var nervioOpticoOD = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_NERVIO_OPTICO_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_NERVIO_OPTICO_OD).v_Value1Name;
            var nervioOpticoOI = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_NERVIO_OPTICO_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_NERVIO_OPTICO_OI).v_Value1Name;
            var retinaOD = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_RETINA_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_RETINA_OD).v_Value1Name;
            var retinaOI = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_RETINA_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_RETINA_OI).v_Value1Name;

            var presionIntraOcOD = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OD).v_Value1;
            var presionIntraOcOI = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OI).v_Value1;
            var npresionIntraOcODMed = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OD).v_MeasurementUnitName;
            var presionIntraOcOIMed = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OI).v_MeasurementUnitName;
               
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    

                new PdfPCell(new Phrase("OJOS", fontColumnValueBold)) { Colspan =3,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValueBold)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("CORREGIDA", fontColumnValueBold)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("ENFERMEDADES OCULARES", fontColumnValueBold)) { Colspan =5,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("OJO DERECHO", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("OJO IZQUIERDO", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("OJO DERECHO", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("OJO IZQUIERDO", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
         
                new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vcscod, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vcscoi, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vcccod, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vcccoi, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(enfermedadesOculares, fontColumnValue)) { Colspan =5,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vlscod, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vlscoi, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vlccod, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vlccoi, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("VISIÓN DE COLORES \n (TEST DE ISHIHARA)", fontColumnValueBold)) { Colspan =3,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(testIshihara, fontColumnValue1)) { Colspan =12, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(reflejosPupilares, fontColumnValue)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                
                new PdfPCell(new Phrase("FONDO DE OJO \n (FUNDOSCOPIA)", fontColumnValueBold)) { Colspan =3,Rowspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("MÁCULA OD", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase(maculaOD, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase("MÁCULA OI", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase(maculaOI, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase("PRESIÓN INTRAOCULAR", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("NERVIO ÓPT OD", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase(nervioOpticoOD, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase("NERVIO ÓPT OI", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase(nervioOpticoOI, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase("OD", fontColumnValueBold)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(presionIntraOcOD + " " + npresionIntraOcODMed, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("RETINA OD", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase(retinaOD, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase("RETINA OI", fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase(retinaOI, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                new PdfPCell(new Phrase("OI", fontColumnValueBold)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(presionIntraOcOI + " " + presionIntraOcOIMed, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("NOMBRES Y APELLIDOS DEL MÉDICO", fontColumnValueBold)) { Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       
                new PdfPCell(new Phrase("CMP/RNE", fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       

                new PdfPCell(new Phrase(DatosGrabo.Nombre, fontColumnValueBold)) { Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       
                new PdfPCell(new Phrase(DatosGrabo.CMP, fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

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
         
            if (DatosGrabo.Firma != null){
                cellFirma = new PdfPCell(HandlingItextSharp.GetImage(DatosGrabo.Firma, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            }
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
                new PdfPCell(new Phrase("FIRMA Y SELLO DEL MÉDICO", fontColumnValueBold)) {Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(cellFirma){Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
 
                new PdfPCell(new Phrase("FIRMA DEL EXAMINADO", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},    
                new PdfPCell(new Phrase("HUELLA DEL EXAMINADO", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},    

                new PdfPCell(new Phrase("CON LA CUAL DECLARA QUE LA INFORMACIÓN DECLARADA ES VERAZ", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    

            };
            columnWidths = new float[] { 25f, 25f, 25f, 25f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region OBSERVACIONES
            //var diagnosticosYanacocha = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);
            //cells = new List<PdfPCell>()
            //{
            //    new PdfPCell(new Phrase("Observaciones: ", fontColumnValueBold)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK  },       
            //    //new PdfPCell(new Phrase(observaciones, fontColumnValue)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20F, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       
            //};

            //if (diagnosticosYanacocha.Count != 0)
            //{
            //    foreach (var item in diagnosticosYanacocha)
            //    {
            //        cells.Add(new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20F, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE });
            //        //cells.Add(new PdfPCell(new Phrase(item.Recomendations, fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20F, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE });
            //        foreach (var item2 in item.Recomendations)
            //        {
            //            cells.Add(new PdfPCell(new Phrase(item2.v_RecommendationName, fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20F, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE });
            //        }
            //    }
            //}

            //columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //document.Add(table);
            #endregion
            #region Hallazgos y recomendaciones
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("HALLAZGOS", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       

                    new PdfPCell(new Phrase("CIE 10", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       
                    new PdfPCell(new Phrase("ESPECIFICACIONES", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       

                };
            columnWidths = new float[] { 20.6f, 40.6f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            cells = new List<PdfPCell>();

            var filterDiagnosticRepository = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);

            if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
            {
                columnWidths = new float[] { 0.7f, 23.6f };
                include = "i_Item,Valor1";

                foreach (var item in filterDiagnosticRepository)
                {
                    if (item.v_DiseasesId == "N009-DD000000029")
                    {
                        cell = new PdfPCell(new Phrase("")) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }

                    ListaComun oListaComun = null;
                    List<ListaComun> Listacomun = new List<ListaComun>();

                    if (item.Recomendations.Count > 0)
                    {
                        oListaComun = new ListaComun();
                        oListaComun.Valor1 = "RECOMENDACIONES";
                        oListaComun.i_Item = "#";
                        Listacomun.Add(oListaComun);
                    }


                    int Contador = 1;
                    foreach (var Reco in item.Recomendations)
                    {
                        oListaComun = new ListaComun();

                        oListaComun.Valor1 = Reco.v_RecommendationName;
                        oListaComun.i_Item = Contador.ToString();
                        Listacomun.Add(oListaComun);
                        Contador++;
                    }

                    if (item.Restrictions.Count > 0)
                    {
                        oListaComun = new ListaComun();
                        oListaComun.Valor1 = "RESTRICCIONES";
                        oListaComun.i_Item = "#";
                        Listacomun.Add(oListaComun);

                    }
                    int Contador1 = 1;
                    foreach (var Rest in item.Restrictions)
                    {
                        oListaComun = new ListaComun();
                        oListaComun.Valor1 = Rest.v_RestrictionName;
                        oListaComun.i_Item = Contador1.ToString();
                        Listacomun.Add(oListaComun);
                        Contador1++;
                    }

                    // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                    table = HandlingItextSharp.GenerateTableFromList(Listacomun, columnWidths, include, fontColumnValue);
                    cell = new PdfPCell(table);

                    cells.Add(cell);
                }

                columnWidths = new float[] { 20.6f, 40.6f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                columnWidths = new float[] { 100 };
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null);
            document.Add(table);
            #endregion

            
            document.Close();
            writer.Close();
            writer.Dispose();
        }

    }
}
