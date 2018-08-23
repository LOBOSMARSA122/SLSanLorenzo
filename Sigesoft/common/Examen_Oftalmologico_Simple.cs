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
    public class Examen_Oftalmologico_Simple
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateExamen_Oftalmologico_Simple(PacientList filiationData, ServiceList DataService,
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF)
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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            var tamaño_celda = 15f;
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
                    new PdfPCell(new Phrase("N° DE FICHA", fontColumnValue2)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_IdService, fontColumnValue2)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase("INFORME OFTALMOLÓGICO", fontTitle1)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                };
            columnWidths = new float[] { 75f, 25f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region DATOS GENERALES
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');

            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("\nDATOS PERSONALES: ", fontColumnValue2)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       

                new PdfPCell(new Phrase("NOMBRES: ", fontColumnValue2)) {Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                new PdfPCell(new Phrase(datosPac.v_FirstName, fontColumnValue1)) {Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       

                new PdfPCell(new Phrase("APELLIDOS: ", fontColumnValue2)) {Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValue1)) {Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       

                new PdfPCell(new Phrase("EDAD: ", fontColumnValue2)) {Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                new PdfPCell(new Phrase(datosPac.Edad.ToString(), fontColumnValue1)) {Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       

                new PdfPCell(new Phrase("TELEFONO: ", fontColumnValue2)) {Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                new PdfPCell(new Phrase(datosPac.v_TelephoneNumber, fontColumnValue1)) {Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       

                new PdfPCell(new Phrase("DOMICILIO: ", fontColumnValue2)) {Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue1)) {Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       

                new PdfPCell(new Phrase("EMPRESA: ", fontColumnValue2)) {Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                new PdfPCell(new Phrase(datosPac.v_FullWorkingOrganizationName, fontColumnValue1)) {Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       
                
                new PdfPCell(new Phrase("FECHA: ", fontColumnValue2)) {Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue1)) {Colspan = 15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       
             };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            ServiceComponentList informeOftalmoSimple = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID);

            #region ANTECEDENTES LABORALES
            var antecedentes_laborales = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_DESCRIPCION_LAB) == null ? "NO REFIERE ANTECEDENTES PERSONALES" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_DESCRIPCION_LAB).v_Value1;

            cells = new List<PdfPCell>()
            {         
               
                new PdfPCell(new Phrase("ANTECEDENTES LABORALES: ", fontColumnValue2)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                
                new PdfPCell(new Phrase(antecedentes_laborales, fontColumnValue)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20F, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
              };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ANTECEDENTES PATOLÓGICOS
            var antecedentes_patologicos = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_DESCRIPCION_PAT) == null ? "NO REFIERE ANTECEDENTES PATOLÓGICOS" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_DESCRIPCION_PAT).v_Value1;

            cells = new List<PdfPCell>()
            {         
               
                new PdfPCell(new Phrase("ANTECEDENTES PATOLÓGICOS: ", fontColumnValue2)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                
                new PdfPCell(new Phrase(antecedentes_patologicos, fontColumnValue)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20F, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE  },       
                
                new PdfPCell(new Phrase("", fontColumnValue2)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2F, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       
  
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region OJOS

            var vcscod = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOD) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOD).v_Value1Name;
            var vcscoi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOI) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOI).v_Value1Name;
            var vcccod = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOD) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOD).v_Value1Name;
            var vcccoi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOI) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOI).v_Value1Name;

            var vlscod = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOD) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOD).v_Value1Name;
            var vlscoi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOI) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOI).v_Value1Name;
            var vlccod = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOD) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOD).v_Value1Name;
            var vlccoi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOI) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOI).v_Value1Name;

            var visioncolores = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VISION_COLORES) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VISION_COLORES).v_Value1Name;
            var enfermedadesOculares = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ENFERMEDADES_OCULARES) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ENFERMEDADES_OCULARES).v_Value1;
            var reflejosPupilares = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_REFLEJOS_PUPILARES) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_REFLEJOS_PUPILARES).v_Value1;

            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("OJOS", fontColumnValueBold)) { Colspan =4,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValueBold)) { Colspan =8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("CORREGIDA", fontColumnValueBold)) { Colspan =8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("OJO DERECHO", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("OJO IZQUIERDO", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("OJO DERECHO", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("OJO IZQUIERDO", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
         
                new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vcscod, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vcscoi, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vcccod, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vcccoi, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vlscod, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vlscoi, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vlccod, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(vlccoi, fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(visioncolores, fontColumnValue)) { Colspan =16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                
                new PdfPCell(new Phrase("ENFERMEDADES OCULARES", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(enfermedadesOculares, fontColumnValue)) { Colspan =16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                
                new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(reflejosPupilares, fontColumnValue)) { Colspan =16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2F, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK  },       

             };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region  APTITUD OFTALMOLOGO
            //var aptitud = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_APTITUD_PSICO) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_APTITUD_PSICO).v_Value1;
            //var conclusion_desc = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_APTITUD_PSICO_DESC) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_APTITUD_PSICO_DESC).v_Value1;
            var aptoSi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_APTITUD_SI) == null ? "" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_APTITUD_SI).v_Value1;
            var aptoNo = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_APTITUD_NO) == null ? "" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_APTITUD_NO).v_Value1;

            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("APTO PARA TRABAJAR", fontColumnValueBold)) { Colspan = 6,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       
                new PdfPCell(new Phrase("NOMBRES Y APELLIDOS DEL MÉDICO", fontColumnValueBold)) { Colspan = 9,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       
                new PdfPCell(new Phrase("CMP/RNE", fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       

                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(aptoSi == "1"?"X":aptoSi =="0"?"":aptoSi, fontColumnValue)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) { Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(aptoNo == "1"?"X":aptoNo =="0"?"":aptoNo, fontColumnValue)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase(DataService.NombreDoctor, fontColumnValueBold)) { Colspan = 9,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       
                new PdfPCell(new Phrase(DataService.CMP, fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },       
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            //var psico = serviceComponent.Find(p => p.i_CategoryId == (int)Sigesoft.Common.Consultorio.Psicología);

            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls

            // Firma del trabajador ***************************************************
            PdfPCell cellFirmaTrabajador = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 70, 30));
            else

                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            // Huella del trabajador **************************************************
            PdfPCell cellHuellaTrabajador = null;

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 30, 30));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));


            // Firma del doctor Auditor **************************************************


            PdfPCell cellFirma = null;

            if (DataService.FirmaMedicoMedicina != null)
                cellFirma = new PdfPCell(HandlingItextSharp.GetImage(DataService.FirmaMedicoMedicina, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
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
            cell = new PdfPCell(new Phrase("FIRMA Y SELLO DEL PSICÓLOGO", fontColumnValue)) { Rowspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cells.Add(cell);

            // 3 celda (Imagen)
            cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirma.FixedHeight = 40F;
            cellFirma.UseVariableBorders = true;
            cellFirma.BorderColorLeft = BaseColor.BLACK;
            cellFirma.BorderColorRight = BaseColor.BLACK;
            cellFirma.BorderColorBottom = BaseColor.WHITE;
            cellFirma.BorderColorTop = BaseColor.BLACK;
            cells.Add(cellFirma);

            cells.Add(new PdfPCell(new Phrase("CON LA CUAL DECLARA QUE LA INFORMACIÓN DECLARADA ES VERAZ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE });

            columnWidths = new float[] { 35f, 35f, 30f, 40F };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(table);

            #endregion

            #region EXAMENES AUXILIARES COMPLEMENTARIOS
            var examenesAuxiliares = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_EXAMENES_AUXILIARES) == null ? "NO REFIERE EXAMENES AUXILIARES" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_EXAMENES_AUXILIARES).v_Value1;

            cells = new List<PdfPCell>()
            {         
               
                new PdfPCell(new Phrase("EXAMENES AUXILIARES COMPLEMENTARIOS: ", fontColumnValue2)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK  },       
                
                new PdfPCell(new Phrase(examenesAuxiliares, fontColumnValue)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20F, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },       
                
                //new PdfPCell(new Phrase("", fontTitle3)) {Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2F, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK  },       
  
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
