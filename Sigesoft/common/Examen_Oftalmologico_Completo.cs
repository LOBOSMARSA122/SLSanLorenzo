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
    public class Examen_Oftalmologico_Completo
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateExamen_Oftalmologico_Completo(PacientList filiationData, 
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF, UsuarioGrabo DatosGrabo, List<DiagnosticRepositoryList> Diagnosticos)
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
            List<PdfPCell> cells1 = null;
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
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
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
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase("EXAMEN OFTALMOLÓGICO", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK},
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},
                
                };
            columnWidths = new float[] {30f, 40f , 30f, };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            //ServiceComponentList informe_psico_goldfields = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS);

            #region DATOS GENERALES

            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;
            cells = new List<PdfPCell>()
            {         
               
                new PdfPCell(new Phrase("APELLIDOS Y NOMBRES:", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                new PdfPCell(new Phrase("DNI:", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                
                new PdfPCell(new Phrase("CONTRATA:", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                new PdfPCell(new Phrase(empr_Conct, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

             };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            ServiceComponentList informeOftalmoCompleto = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID);

            #region AGUDEZA VISUAL

            var vlscod = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOD).v_Value1Name;
            var vlscoi = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOI).v_Value1Name;
            var vlccod = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOD).v_Value1Name;
            var vlccoi = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOI).v_Value1Name;

            var vcscod = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOD).v_Value1Name;
            var vcscoi = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOI).v_Value1Name;
            var vcccod = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOD).v_Value1Name;
            var vcccoi = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOI).v_Value1Name;

           
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("A. V. PARA LEJOS", fontColumnValueBold)) { Colspan = 5, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("OD", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(vlscod, fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("MEJORA A", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(vlccod, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("CC", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("OI", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(vlscoi, fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("MEJORA A", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(vlccoi, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("CC", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("A. V. PARA CERCA", fontColumnValueBold)) { Colspan = 5, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("OD", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(vcscod, fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("MEJORA A", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(vcccod, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("CC", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("OI", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(vcscoi, fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("MEJORA A", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(vcccoi, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("CC", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region VISIÓN CROMÁTICA
            var testIshihara = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_ISHIHARA) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_ISHIHARA).v_Value1Name;
            var testWaggoner = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_WAGGONER) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_WAGGONER).v_Value1;
            var testLegrand = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_LEGRAND) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_LEGRAND).v_Value1;

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("VISIÓN CROMÁTICA", fontColumnValueBold)) { Colspan = 5, Rowspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("TEST DE ISHIHARA", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(testIshihara, fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("TEST DE WAGGONER", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(testWaggoner, fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("TEST DE LEGRAND", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(testLegrand, fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
               // new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region OTROS EXAM
            var flyTest = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FLY_TEST) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FLY_TEST).v_Value1;

            var meo = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_MEO) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_MEO).v_Value1;
            var sa = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_SA) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_SA).v_Value1;
            var anexos = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ANEXOS) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ANEXOS).v_Value1;
            var fondoOjo = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FONDO_OJO) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FONDO_OJO).v_Value1;

            var tonometriaOD = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OD).v_Value1;
            var tonometriaODUnidad = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OD) == null ? "" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OD).v_MeasurementUnitName;
            var tonometriaOI = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OI).v_Value1;
            var tonometriaOIUnidad = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OI) == null ? "" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OI).v_MeasurementUnitName;

            var refraccion = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFRACCION) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFRACCION).v_Value1;
            var reflejosPupilares = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFLEJOS_PUPILARES) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFLEJOS_PUPILARES).v_Value1Name;

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("VISIÓN ESTEREOSCÓPICA: FLY TEST:", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(flyTest, fontColumnValueBold)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("M. E. O.:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(meo, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("S. A.:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(sa, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("ANEXOS:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(anexos, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("FONDO DE OJO:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(fondoOjo, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("TONOMETRÍA:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("O. D.", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(tonometriaOD + " " +tonometriaODUnidad, fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("(schiotz)", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("O. I", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(tonometriaOI + " " + tonometriaOIUnidad, fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("REFRACCIÓN:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(refraccion, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("REFLEJOS PUPILARES:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(reflejosPupilares, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            var diagnosticosOfatlmoCompleto = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID);
            int tamañoDiag = diagnosticosOfatlmoCompleto.Count == 0 ? 1 : diagnosticosOfatlmoCompleto.Count;

            #region DIAGNÓSTICO
            cells = new List<PdfPCell>();
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');

            cells.Add(new PdfPCell(new Phrase("DIAGNÓSTICO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, Rowspan = tamañoDiag, MinimumHeight = tamaño_celda });
            
            if (diagnosticosOfatlmoCompleto.Count != 0)
            {
                foreach (var item in diagnosticosOfatlmoCompleto)
                {
                    cells.Add(new PdfPCell(new Phrase(item.v_DiseasesName == null ? "---" : item.v_DiseasesName == "" ? "---" : item.v_DiseasesName, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda });
                }
            }
            else {
                cells.Add(new PdfPCell(new Phrase("FALTA LLENAR", fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda });
            }
            

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region RP
            var rp = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_RP) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_RP).v_Value1;

            var cellsRp = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("R. P.:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(rp, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsRp, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region RECOMENDACIÓN
            cells = new List<PdfPCell>();
            int anchoReco = 1;
            cells.Add(new PdfPCell(new Phrase("RECOMENDACIÓN", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Rowspan = tamañoDiag });

            if (diagnosticosOfatlmoCompleto.Count != 0)
            {
                foreach (var item in diagnosticosOfatlmoCompleto)
                {
                    //anchoReco = item.;
                    //if (item.Recomendations.Count == anchoReco)
                    //{
                    //}
                    //anchoReco--;
                    foreach (var item2 in item.Recomendations)
                    {
                        cells.Add(new PdfPCell(new Phrase(item2.v_RecommendationName == null ? "---" : item2.v_RecommendationName == "" ? "---" : item2.v_RecommendationName, fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda });
                    }
                }
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("RECOMENDACIÓN", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Rowspan = anchoReco });
                cells.Add(new PdfPCell(new Phrase("FALTA LLENAR", fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda });

            }

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 5f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("FECHA DE EVALUACIÓN:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValueBold)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
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
                if (DatosGrabo != null)
                {
                    if (DatosGrabo.Firma != null)
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


            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
