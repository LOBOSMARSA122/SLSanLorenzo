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
    public class InformePsicologicoGoldfields
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        #region Reporte informe
        public static void CreateInformePsicologicoGoldfields(PacientList filiationData,
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

            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            var tamaño_celda = 10f;
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
                    new PdfPCell(new Phrase("INFORME PSICOLÓGICO OCUPACIONAL", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            ServiceComponentList informe_psico_goldfields = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS);

            #region DATOS GENERALES
           string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');

           var supervisor = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SUPERVISOR) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SUPERVISOR).v_Value1;
           var puesto_trabajo = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUESTO_TRABAJO) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUESTO_TRABAJO).v_Value1;
           var lugar_trabajo = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_LUGAR_TRABAJO) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_LUGAR_TRABAJO).v_Value1;
           var brevete = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_BREVETE) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_BREVETE).v_Value1;
           string breveteXSI = "", breveteXNO = "";
           if (brevete == "1") breveteXSI = "X";
           else if (brevete == "0") breveteXNO = "X";
           var breveteSI = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SI_BREVETE) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SI_BREVETE).v_Value1;

           var conducira = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_BREVETE) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_BREVETE).v_Value1;
           string conduciraXSI = "", conduciraXNO = "";
           if (conducira == "1") conduciraXSI = "X";
           else if (conducira == "0") conduciraXNO = "X";


           string empresageneral = filiationData.empresa_;
           string empresacontrata = filiationData.contrata;
           string empresasubcontrata = filiationData.subcontrata;

           string empr_Conct = "";
           if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
           else empr_Conct = empresacontrata;


            cells = new List<PdfPCell>()
            {         
               
                new PdfPCell(new Phrase("I. INFORMACIÓN GENERAL", fontColumnValueBold)) {BackgroundColor = BaseColor.GRAY,  Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK  },       

                new PdfPCell(new Phrase("Apellidos Y Nombres", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("DNI", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.v_DocNumber , fontColumnValue)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Edad", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString() , fontColumnValue)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Fecha de Nacimiento:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(datosPac.d_Birthdate.ToString().Split(' ')[0], fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Estado Civil:", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_MaritalStatus, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Teléfono", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_TelephoneNumber, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Grado de Instrucción", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.GradoInstruccion, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Domicilio", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("Empresa", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(empr_Conct, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Supervisor", fontColumnValueBold)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(supervisor, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
              
                new PdfPCell(new Phrase("Puesto de trabajo", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(puesto_trabajo, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Lugar de trabajo", fontColumnValueBold)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(lugar_trabajo, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
              
                new PdfPCell(new Phrase("Brevete", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(breveteXSI, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(breveteXNO, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("N°", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(breveteSI, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Conducira", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conduciraXSI, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conduciraXNO, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("CLÍNICA:", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(infoEmpresa.v_Name, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha de Evauación", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region MOTIVO DE CONSULTA
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
           
            cells = new List<PdfPCell>()
            {         
               
                new PdfPCell(new Phrase("II. MOTIVO DE CONSULTA", fontColumnValueBold)) {BackgroundColor = BaseColor.GRAY, Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},       
   
                new PdfPCell(new Phrase("Pre Ocupacional" , fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(preOcp , fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Ocupacional", fontColumnValueBold)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(Ocup, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Post Ocupacional" , fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(postOcup , fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda }, 
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region OBSERVACIÓN CONDUCTA
            var observacion_conducta = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONDUCTA_OBS) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONDUCTA_OBS).v_Value1;
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("III. OBSERVACIÓN DE CONDUCTA", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(observacion_conducta, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ASPECTOS INTELECTUALES
            var coeficiente_intelectual = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COEFICIENTE_INTELECTUAL) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COEFICIENTE_INTELECTUAL).v_Value1;
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("IV. ASPECTOS INTELECTUALES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("Coeficiente intelectual", fontColumnValueBold)) { Colspan =5,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Inferior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal inferior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal Superior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Superior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase(coeficiente_intelectual=="-1"?"-":coeficiente_intelectual == "1" ?"X":coeficiente_intelectual=="2"?"":coeficiente_intelectual=="3"?"":coeficiente_intelectual=="4"?"":coeficiente_intelectual=="5"?"":coeficiente_intelectual, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(coeficiente_intelectual=="-1"?"-":coeficiente_intelectual == "1" ?"":coeficiente_intelectual=="2"?"X":coeficiente_intelectual=="3"?"":coeficiente_intelectual=="4"?"":coeficiente_intelectual=="5"?"":coeficiente_intelectual, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(coeficiente_intelectual=="-1"?"-":coeficiente_intelectual == "1" ?"":coeficiente_intelectual=="2"?"":coeficiente_intelectual=="3"?"X":coeficiente_intelectual=="4"?"":coeficiente_intelectual=="5"?"":coeficiente_intelectual, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(coeficiente_intelectual=="-1"?"-":coeficiente_intelectual == "1" ?"":coeficiente_intelectual=="2"?"":coeficiente_intelectual=="3"?"":coeficiente_intelectual=="4"?"X":coeficiente_intelectual=="5"?"":coeficiente_intelectual, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(coeficiente_intelectual=="-1"?"-":coeficiente_intelectual == "1" ?"":coeficiente_intelectual=="2"?"":coeficiente_intelectual=="3"?"":coeficiente_intelectual=="4"?"":coeficiente_intelectual=="5"?"X":coeficiente_intelectual, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            var no_Sec = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SEC_INCOMP) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SEC_INCOMP).v_Value1;
            var sec = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SEC_COMP) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SEC_COMP).v_Value1;
            var geren = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_GERENTE) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_GERENTE).v_Value1;
            string numeracionFP = "", numeracionPS = "", numeracionC = "", numeracionR = "", numeracionRP = "";
            if (no_Sec =="1")
            {
                #region  FUNCIONES COGNITIVAS - NO SECUNDARIA
                var orientacion = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ORIENTACION) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ORIENTACION).v_Value1;
                var atencion = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ATENCION_CONCENTRACION) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ATENCION_CONCENTRACION).v_Value1;
                var memoria = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_MEMORIA) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_MEMORIA).v_Value1;
                var comprension = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COMPRENSION) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COMPRENSION).v_Value1;
                var stroop = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_STROOP) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_STROOP).v_Value1;

                var funciones_comprometidas_1 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONCLUSION_FUNC_COG_COMPROMETIDAS) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONCLUSION_FUNC_COG_COMPROMETIDAS).v_Value1;
                var funciones_cgnitivas_1 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONCLUSION_FUNC_COG_NORMALES) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONCLUSION_FUNC_COG_NORMALES).v_Value1;
                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("V. FUNCIONES COGNITIVAS", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("DIMENSIONES", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Severo", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Moderado", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal Superior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Superior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("Orientación", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion == "-1" ?"-":orientacion == "1" ?"X":orientacion=="2"?"":orientacion=="3"?"":orientacion=="4"?"":orientacion=="5"?"":orientacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion == "-1" ?"-":orientacion == "1" ?"":orientacion=="2"?"X":orientacion=="3"?"":orientacion=="4"?"":orientacion=="5"?"":orientacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion == "-1" ?"-":orientacion == "1" ?"":orientacion=="2"?"":orientacion=="3"?"X":orientacion=="4"?"":orientacion=="5"?"":orientacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion == "-1" ?"-":orientacion == "1" ?"":orientacion=="2"?"":orientacion=="3"?"":orientacion=="4"?"X":orientacion=="5"?"":orientacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion == "-1" ?"-":orientacion == "1" ?"":orientacion=="2"?"":orientacion=="3"?"":orientacion=="4"?"":orientacion=="5"?"X":orientacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Atención y concentración", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion == "-1" ?"-":atencion == "1" ?"X":atencion=="2"?"":atencion=="3"?"":atencion=="4"?"":atencion=="5"?"":atencion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion == "-1" ?"-":atencion == "1" ?"":atencion=="2"?"X":atencion=="3"?"":atencion=="4"?"":atencion=="5"?"":atencion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion == "-1" ?"-":atencion == "1" ?"":atencion=="2"?"":atencion=="3"?"X":atencion=="4"?"":atencion=="5"?"":atencion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion == "-1" ?"-":atencion == "1" ?"":atencion=="2"?"":atencion=="3"?"":atencion=="4"?"X":atencion=="5"?"":atencion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion == "-1" ?"-":atencion == "1" ?"":atencion=="2"?"":atencion=="3"?"":atencion=="4"?"":atencion=="5"?"X":atencion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Memoria", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria == "-1" ?"-":memoria == "1" ?"X":memoria=="2"?"":memoria=="3"?"":memoria=="4"?"":memoria=="5"?"":memoria, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria == "-1" ?"-":memoria == "1" ?"":memoria=="2"?"X":memoria=="3"?"":memoria=="4"?"":memoria=="5"?"":memoria, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria == "-1" ?"-":memoria == "1" ?"":memoria=="2"?"":memoria=="3"?"X":memoria=="4"?"":memoria=="5"?"":memoria, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria == "-1" ?"-":memoria == "1" ?"":memoria=="2"?"":memoria=="3"?"":memoria=="4"?"X":memoria=="5"?"":memoria, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria == "-1" ?"-":memoria == "1" ?"":memoria=="2"?"":memoria=="3"?"":memoria=="4"?"":memoria=="5"?"X":memoria, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Comprensión", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension == "-1" ?"-":comprension == "1" ?"X":comprension=="2"?"":comprension=="3"?"":comprension=="4"?"":comprension=="5"?"":comprension, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension == "-1" ?"-":comprension == "1" ?"":comprension=="2"?"X":comprension=="3"?"":comprension=="4"?"":comprension=="5"?"":comprension, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension == "-1" ?"-":comprension == "1" ?"":comprension=="2"?"":comprension=="3"?"X":comprension=="4"?"":comprension=="5"?"":comprension, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension == "-1" ?"-":comprension == "1" ?"":comprension=="2"?"":comprension=="3"?"":comprension=="4"?"X":comprension=="5"?"":comprension, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension == "-1" ?"-":comprension == "1" ?"":comprension=="2"?"":comprension=="3"?"":comprension=="4"?"":comprension=="5"?"X":comprension, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Stroop", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20},    
                new PdfPCell(new Phrase(stroop, fontColumnValue)) { Colspan =15,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20},    
                
                new PdfPCell(new Phrase("CONCLUSIÓN", fontColumnValueBold)) { Colspan =5,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Funciones Cognitivas Comprometidas", fontColumnValueBold)) { Colspan =8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Funciones Cognitivas Normales", fontColumnValueBold)) { Colspan =7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(funciones_comprometidas_1 == "1" ?"X":funciones_comprometidas_1=="0"?"":funciones_comprometidas_1, fontColumnValue)) { Colspan =8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(funciones_cgnitivas_1 == "1" ?"X":funciones_cgnitivas_1=="0"?"":funciones_cgnitivas_1, fontColumnValue)) { Colspan =8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                #region  ASPECTOS INTRA E INTERPERSONALES - NO SECUNDARIA
                var trabajo_equipo = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_TRABAJO_EQUIPO) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_TRABAJO_EQUIPO).v_Value1;
                var comunicacion = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COMUNICACION) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COMUNICACION).v_Value1;
                var compromiso = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COMPROMISO) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COMPROMISO).v_Value1;
                var motivacion = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_MOTIVACION) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_MOTIVACION).v_Value1;
                var descripcion_ASP = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_DESCRIP_ASP_INTRAE_INTER_1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_DESCRIP_ASP_INTRAE_INTER_1).v_Value1;

                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("VI. ASPECTOS INTRA E INTERPERSONALES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("COMPETENCIAS", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Inferior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal Inferior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal Superior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Superior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("Trabajo en equipo", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(trabajo_equipo == "-1" ?"-":trabajo_equipo == "1" ?"X":trabajo_equipo=="2"?"":trabajo_equipo=="3"?"":trabajo_equipo=="4"?"":trabajo_equipo=="5"?"":trabajo_equipo, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(trabajo_equipo == "-1" ?"-":trabajo_equipo == "1" ?"":trabajo_equipo=="2"?"X":trabajo_equipo=="3"?"":trabajo_equipo=="4"?"":trabajo_equipo=="5"?"":trabajo_equipo, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(trabajo_equipo == "-1" ?"-":trabajo_equipo == "1" ?"":trabajo_equipo=="2"?"":trabajo_equipo=="3"?"X":trabajo_equipo=="4"?"":trabajo_equipo=="5"?"":trabajo_equipo, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(trabajo_equipo == "-1" ?"-":trabajo_equipo == "1" ?"":trabajo_equipo=="2"?"":trabajo_equipo=="3"?"":trabajo_equipo=="4"?"X":trabajo_equipo=="5"?"":trabajo_equipo, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(trabajo_equipo == "-1" ?"-":trabajo_equipo == "1" ?"":trabajo_equipo=="2"?"":trabajo_equipo=="3"?"":trabajo_equipo=="4"?"":trabajo_equipo=="5"?"X":trabajo_equipo, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Comunicación", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comunicacion == "-1" ?"-":comunicacion == "1" ?"X":comunicacion=="2"?"":comunicacion=="3"?"":comunicacion=="4"?"":comunicacion=="5"?"":comunicacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comunicacion == "-1" ?"-":comunicacion == "1" ?"":comunicacion=="2"?"X":comunicacion=="3"?"":comunicacion=="4"?"":comunicacion=="5"?"":comunicacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comunicacion == "-1" ?"-":comunicacion == "1" ?"":comunicacion=="2"?"":comunicacion=="3"?"X":comunicacion=="4"?"":comunicacion=="5"?"":comunicacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comunicacion == "-1" ?"-":comunicacion == "1" ?"":comunicacion=="2"?"":comunicacion=="3"?"":comunicacion=="4"?"X":comunicacion=="5"?"":comunicacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comunicacion == "-1" ?"-":comunicacion == "1" ?"":comunicacion=="2"?"":comunicacion=="3"?"":comunicacion=="4"?"":comunicacion=="5"?"X":comunicacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Compromiso", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(compromiso == "-1" ?"-":compromiso == "1" ?"X":compromiso=="2"?"":compromiso=="3"?"":compromiso=="4"?"":compromiso=="5"?"":compromiso, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(compromiso == "-1" ?"-":compromiso == "1" ?"":compromiso=="2"?"X":compromiso=="3"?"":compromiso=="4"?"":compromiso=="5"?"":compromiso, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(compromiso == "-1" ?"-":compromiso == "1" ?"":compromiso=="2"?"":compromiso=="3"?"X":compromiso=="4"?"":compromiso=="5"?"":compromiso, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(compromiso == "-1" ?"-":compromiso == "1" ?"":compromiso=="2"?"":compromiso=="3"?"":compromiso=="4"?"X":compromiso=="5"?"":compromiso, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(compromiso == "-1" ?"-":compromiso == "1" ?"":compromiso=="2"?"":compromiso=="3"?"":compromiso=="4"?"":compromiso=="5"?"X":compromiso, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Motivación", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(motivacion == "-1" ?"-":motivacion == "1" ?"X":motivacion=="2"?"":motivacion=="3"?"":motivacion=="4"?"":motivacion=="5"?"":motivacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(motivacion == "-1" ?"-":motivacion == "1" ?"":motivacion=="2"?"X":motivacion=="3"?"":motivacion=="4"?"":motivacion=="5"?"":motivacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(motivacion == "-1" ?"-":motivacion == "1" ?"":motivacion=="2"?"":motivacion=="3"?"X":motivacion=="4"?"":motivacion=="5"?"":motivacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(motivacion == "-1" ?"-":motivacion == "1" ?"":motivacion=="2"?"":motivacion=="3"?"":motivacion=="4"?"X":motivacion=="5"?"":motivacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(motivacion == "-1" ?"-":motivacion == "1" ?"":motivacion=="2"?"":motivacion=="3"?"":motivacion=="4"?"":motivacion=="5"?"X":motivacion, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("OBSERVACIÓN:  " + descripcion_ASP == null ?"NO PRESENTA OBSERVACIONES": descripcion_ASP, fontColumnValueBold)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
            
            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                #region PERFIL DE PERSONALIDAD TEST DE COLORES - NO SECUNDARIA
                var perfil_personalidad_test_colores = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PERFIL_PERSONALIDAD_TEST_COLORES) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PERFIL_PERSONALIDAD_TEST_COLORES).v_Value1;
                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("VII. PERFIL DE PERSONALIDAD TEST DE COLORES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(perfil_personalidad_test_colores, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},    
            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                #region ADAPTABILIDAD, CAPACIDAD DE AFRONTE - HOMBRE BAJO LA LLUVIA - NO SECUNDARIA
                var adaptabilidad_hombre_bajo_lluvia = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PERFIL_PERSONALIDAD_TEST_COLORES) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PERFIL_PERSONALIDAD_TEST_COLORES).v_Value1;
                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("VIII. ADAPTABILIDAD, CAPACIDAD DE AFRONTE - HOMBRE BAJO LA LLUVIA", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(adaptabilidad_hombre_bajo_lluvia, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},    
            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion
                
                numeracionFP = "IX";
                numeracionRP = "X";
                numeracionPS = "XI";
                numeracionC = "XII";
                numeracionR = "XIII";

            }
            else if (sec == "1")
            {

                #region  FUNCIONES COGNITIVAS - SECUNDARIA
                var orientacion1 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ORIENTACION1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ORIENTACION1).v_Value1;
                var atencion1 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ATENCION_CONCENTRACION1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ATENCION_CONCENTRACION1).v_Value1;
                var memoria1 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_MEMORIA1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_MEMORIA1).v_Value1;
                var habilidades_visoconstructivas = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_HABILIDADES_VISIOCONSTRUCTIVAS1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_HABILIDADES_VISIOCONSTRUCTIVAS1).v_Value1;
                var comprension1 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COMPRENSION1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_COMPRENSION1).v_Value1;

                var funciones_comprometidas_2 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONCLUSION_FUNC_COG_COMPROMETIDAS1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONCLUSION_FUNC_COG_COMPROMETIDAS1).v_Value1;
                var funciones_cgnitivas_2 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONCLUSION_FUNC_COG_NORMALES1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_CONCLUSION_FUNC_COG_NORMALES1).v_Value1;
                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("V. FUNCIONES COGNITIVAS", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("DIMENSIONES", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Severo", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Moderado", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Normal Superior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Superior", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("Orientación", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion1 == "-1" ?"-":orientacion1 == "1" ?"X":orientacion1=="2"?"":orientacion1=="3"?"":orientacion1=="4"?"":orientacion1=="5"?"":orientacion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion1 == "-1" ?"-":orientacion1 == "1" ?"":orientacion1=="2"?"X":orientacion1=="3"?"":orientacion1=="4"?"":orientacion1=="5"?"":orientacion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion1 == "-1" ?"-":orientacion1 == "1" ?"":orientacion1=="2"?"":orientacion1=="3"?"X":orientacion1=="4"?"":orientacion1=="5"?"":orientacion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion1 == "-1" ?"-":orientacion1 == "1" ?"":orientacion1=="2"?"":orientacion1=="3"?"":orientacion1=="4"?"X":orientacion1=="5"?"":orientacion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(orientacion1 == "-1" ?"-":orientacion1 == "1" ?"":orientacion1=="2"?"":orientacion1=="3"?"":orientacion1=="4"?"":orientacion1=="5"?"X":orientacion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Atención y concentración", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion1 == "-1" ?"-":atencion1 == "1" ?"X":atencion1=="2"?"":atencion1=="3"?"":atencion1=="4"?"":atencion1=="5"?"":atencion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion1 == "-1" ?"-":atencion1 == "1" ?"":atencion1=="2"?"X":atencion1=="3"?"":atencion1=="4"?"":atencion1=="5"?"":atencion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion1 == "-1" ?"-":atencion1 == "1" ?"":atencion1=="2"?"":atencion1=="3"?"X":atencion1=="4"?"":atencion1=="5"?"":atencion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion1 == "-1" ?"-":atencion1 == "1" ?"":atencion1=="2"?"":atencion1=="3"?"":atencion1=="4"?"X":atencion1=="5"?"":atencion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(atencion1 == "-1" ?"-":atencion1 == "1" ?"":atencion1=="2"?"":atencion1=="3"?"":atencion1=="4"?"":atencion1=="5"?"X":atencion1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Memoria", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria1 == "-1" ?"-":memoria1 == "1" ?"X":memoria1=="2"?"":memoria1=="3"?"":memoria1=="4"?"":memoria1=="5"?"":memoria1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria1 == "-1" ?"-":memoria1 == "1" ?"":memoria1=="2"?"X":memoria1=="3"?"":memoria1=="4"?"":memoria1=="5"?"":memoria1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria1 == "-1" ?"-":memoria1 == "1" ?"":memoria1=="2"?"":memoria1=="3"?"X":memoria1=="4"?"":memoria1=="5"?"":memoria1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria1 == "-1" ?"-":memoria1 == "1" ?"":memoria1=="2"?"":memoria1=="3"?"":memoria1=="4"?"X":memoria1=="5"?"":memoria1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(memoria1 == "-1" ?"-":memoria1 == "1" ?"":memoria1=="2"?"":memoria1=="3"?"":memoria1=="4"?"":memoria1=="5"?"X":memoria1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Habilidades visoconstructivas", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(habilidades_visoconstructivas == "-1" ?"-":habilidades_visoconstructivas == "1" ?"X":habilidades_visoconstructivas=="2"?"":habilidades_visoconstructivas=="3"?"":habilidades_visoconstructivas=="4"?"":habilidades_visoconstructivas=="5"?"":habilidades_visoconstructivas, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(habilidades_visoconstructivas == "-1" ?"-":habilidades_visoconstructivas == "1" ?"":habilidades_visoconstructivas=="2"?"X":habilidades_visoconstructivas=="3"?"":habilidades_visoconstructivas=="4"?"":habilidades_visoconstructivas=="5"?"":habilidades_visoconstructivas, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(habilidades_visoconstructivas == "-1" ?"-":habilidades_visoconstructivas == "1" ?"":habilidades_visoconstructivas=="2"?"":habilidades_visoconstructivas=="3"?"X":habilidades_visoconstructivas=="4"?"":habilidades_visoconstructivas=="5"?"":habilidades_visoconstructivas, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(habilidades_visoconstructivas == "-1" ?"-":habilidades_visoconstructivas == "1" ?"":habilidades_visoconstructivas=="2"?"":habilidades_visoconstructivas=="3"?"":habilidades_visoconstructivas=="4"?"X":habilidades_visoconstructivas=="5"?"":habilidades_visoconstructivas, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(habilidades_visoconstructivas == "-1" ?"-":habilidades_visoconstructivas == "1" ?"":habilidades_visoconstructivas=="2"?"":habilidades_visoconstructivas=="3"?"":habilidades_visoconstructivas=="4"?"":habilidades_visoconstructivas=="5"?"X":habilidades_visoconstructivas, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("Comprensión", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension1 == "-1" ?"-":comprension1 == "1" ?"X":comprension1=="2"?"":comprension1=="3"?"":comprension1=="4"?"":comprension1=="5"?"":comprension1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension1 == "-1" ?"-":comprension1 == "1" ?"":comprension1=="2"?"X":comprension1=="3"?"":comprension1=="4"?"":comprension1=="5"?"":comprension1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension1 == "-1" ?"-":comprension1 == "1" ?"":comprension1=="2"?"":comprension1=="3"?"X":comprension1=="4"?"":comprension1=="5"?"":comprension1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension1 == "-1" ?"-":comprension1 == "1" ?"":comprension1=="2"?"":comprension1=="3"?"":comprension1=="4"?"X":comprension1=="5"?"":comprension1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(comprension1 == "-1" ?"-":comprension1 == "1" ?"":comprension1=="2"?"":comprension1=="3"?"":comprension1=="4"?"":comprension1=="5"?"X":comprension1, fontColumnValue)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("CONCLUSIÓN", fontColumnValueBold)) { Colspan =5,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Funciones Cognitivas Comprometidas", fontColumnValueBold)) { Colspan =8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Funciones Cognitivas Normales", fontColumnValueBold)) { Colspan =7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(funciones_comprometidas_2 == "1" ?"X":funciones_comprometidas_2=="0"?"":funciones_comprometidas_2, fontColumnValue)) { Colspan =8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(funciones_cgnitivas_2 == "1" ?"X":funciones_cgnitivas_2=="0"?"":funciones_cgnitivas_2, fontColumnValue)) { Colspan =8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                #region  ASPECTOS INTRA E INTERPERSONALES - SECUNDARIA
                var conclusiones_generales = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_DESCRIP_ASP_INTRAE_INTER_2) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_DESCRIP_ASP_INTRAE_INTER_2).v_Value1;

                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("VI. ASPECTOS INTRA E INTERPERSONALES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("CONCLUSIONES GENERALES DE PERSONALIDAD:  " + conclusiones_generales, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20},    
            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                #region  PERFIL DE PERSONALIDAD - SECUNDARIA
                var n01 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESCALA_SIG_1_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESCALA_SIG_1_PERFIL_PERSONALIDAD).v_Value1Name;
                var n02 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESCALA_SIG_2_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESCALA_SIG_2_PERFIL_PERSONALIDAD).v_Value1Name;
                var n03 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESCALA_SIG_3_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESCALA_SIG_3_PERFIL_PERSONALIDAD).v_Value1Name;
                var n04 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESCALA_SIG_4_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESCALA_SIG_4_PERFIL_PERSONALIDAD).v_Value1Name;

                var p01 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUNTAJE_1_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUNTAJE_1_PERFIL_PERSONALIDAD).v_Value1;
                var p02 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUNTAJE_2_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUNTAJE_2_PERFIL_PERSONALIDAD).v_Value1;
                var p03 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUNTAJE_3_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUNTAJE_3_PERFIL_PERSONALIDAD).v_Value1;
                var p04 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUNTAJE_4_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PUNTAJE_4_PERFIL_PERSONALIDAD).v_Value1;

                var observaciones_perfilPersonalidad = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_DESC_PERFIL_PERSONALIDAD) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_DESC_PERFIL_PERSONALIDAD).v_Value1;

                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("VII. PERFIL DE PERSONALIDAD", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                
                new PdfPCell(new Phrase("ESCALAS SIGNIFICATIVAS", fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("N° 01", fontColumnValueBold)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(n01, fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("N° 02", fontColumnValueBold)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(n02, fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("N° 03", fontColumnValueBold)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(n03, fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("N° 04", fontColumnValueBold)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(n04, fontColumnValueBold)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("PUNTAJE \"T\"", fontColumnValue)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(p01, fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(p02, fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(p03, fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(p04, fontColumnValueBold)) { Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("OBSERVACIÓN:  " + observaciones_perfilPersonalidad == null ?"NO PRESENTA OBSERVACIONES": observaciones_perfilPersonalidad, fontColumnValueBold)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    

            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                #region  ADAPTABILIDAD - CLIMA SOCIAL, LABORAL - SECUNDARIA
                var relaciones = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ADAPTABILIDAD_RELACIONES1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ADAPTABILIDAD_RELACIONES1).v_Value1;
                var autorrealizacion = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ADAPTABILIDAD_AUTORRELACIONES1) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ADAPTABILIDAD_AUTORRELACIONES1).v_Value1;
                var estabilidad_cambios = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ADAPTABILIDAD_ESTABILIDAD_CAMBIOS) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ADAPTABILIDAD_ESTABILIDAD_CAMBIOS).v_Value1;
                var obs_Adaptabilidad = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ADAPTABILIDAD_DESC) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ADAPTABILIDAD_DESC).v_Value1;


                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("VIII. ADAPTABILIDAD - CLIMA SOCIAL, LABORAL", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                
                new PdfPCell(new Phrase("DIMENSIONES", fontColumnValue)) { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Relaciones", fontColumnValueBold)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Autorrealización", fontColumnValueBold)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Estabilidad y Cambios", fontColumnValueBold)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("CATEGORIAS", fontColumnValue)) { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(relaciones, fontColumnValueBold)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(autorrealizacion, fontColumnValueBold)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(estabilidad_cambios, fontColumnValueBold)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("OBSERVACIÓN:  " + obs_Adaptabilidad == null ?"NO PRESENTA OBSERVACIONES": obs_Adaptabilidad, fontColumnValueBold)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    

            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                #region  EVALUACIONES COMPLEMENTARIAS (MASLACH, PITTSBURG, INTELIGENCIA EMOCIONAL, BARRAT) - SECUNDARIA
                var evaluaciones_comp = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_EVALUACIONES_COMPLEMENTARIAS_MASLACH) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_EVALUACIONES_COMPLEMENTARIAS_MASLACH).v_Value1;

                cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("IX. EVALUACIONES COMPLEMENTARIAS (MASLACH, PITTSBURG, INTELIGENCIA EMOCIONAL, BARRAT)", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(evaluaciones_comp == null ?"NO PRESENTA EVALUACIONES COMPLEMENTARIAS":evaluaciones_comp, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion
                
                numeracionFP = "X";
                numeracionRP = "XI";
                numeracionPS = "XII";
                numeracionC = "XIII";
                numeracionR = "XIV";
            }

            #region  FACTORES PSICOSOCIALES
            var esencia_tarea = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESENCIA_TAREA) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ESENCIA_TAREA).v_Value1;
            var sistema_trabajo = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SISTEMA_TRABAJO) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_SISTEMA_TRABAJO).v_Value1;
            var interaccion_social = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_INTERACCION_SOCIAL) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_INTERACCION_SOCIAL).v_Value1;
            var organizaciones = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ORGANIZACIONALES) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_ORGANIZACIONALES).v_Value1;
            
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase(numeracionFP+". FACTORES PSICOSOCIALES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                
                new PdfPCell(new Phrase("DIMENSIONES", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Alto", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Medio", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Bajo o Nulo", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
              
             
                new PdfPCell(new Phrase("Esencia de la tarea", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(esencia_tarea == "-1" ?"-":esencia_tarea == "1" ?"X":esencia_tarea=="2"?"":esencia_tarea=="3"?"":esencia_tarea, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(esencia_tarea == "-1" ?"-":esencia_tarea == "1" ?"":esencia_tarea=="2"?"X":esencia_tarea=="3"?"":esencia_tarea, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(esencia_tarea == "-1" ?"-":esencia_tarea == "1" ?"":esencia_tarea=="2"?"":esencia_tarea=="3"?"X":esencia_tarea, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
               
                new PdfPCell(new Phrase("Sistema de trabajo", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(sistema_trabajo == "-1" ?"-":sistema_trabajo == "1" ?"X":sistema_trabajo=="2"?"":sistema_trabajo=="3"?"":sistema_trabajo, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(sistema_trabajo == "-1" ?"-":sistema_trabajo == "1" ?"":sistema_trabajo=="2"?"X":sistema_trabajo=="3"?"":sistema_trabajo, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(sistema_trabajo == "-1" ?"-":sistema_trabajo == "1" ?"":sistema_trabajo=="2"?"":sistema_trabajo=="3"?"X":sistema_trabajo, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
               
                new PdfPCell(new Phrase("Interacción social", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(interaccion_social == "-1" ?"-":interaccion_social == "1" ?"X":interaccion_social=="2"?"":interaccion_social=="3"?"":interaccion_social, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(interaccion_social == "-1" ?"-":interaccion_social == "1" ?"":interaccion_social=="2"?"X":interaccion_social=="3"?"":interaccion_social, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(interaccion_social == "-1" ?"-":interaccion_social == "1" ?"":interaccion_social=="2"?"":interaccion_social=="3"?"X":interaccion_social, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
               
                new PdfPCell(new Phrase("Organizaciones", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(organizaciones == "-1" ?"-":organizaciones == "1" ?"X":organizaciones=="2"?"":organizaciones=="3"?"":organizaciones, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(organizaciones == "-1" ?"-":organizaciones == "1" ?"":organizaciones=="2"?"X":organizaciones=="3"?"":organizaciones, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(organizaciones == "-1" ?"-":organizaciones == "1" ?"":organizaciones=="2"?"":organizaciones=="3"?"X":organizaciones, fontColumnValue)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region PSICOSENSOMETRICO
            var riesgospsicosociales = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_RIESGOS_PSICOSOCIALES) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_RIESGOS_PSICOSOCIALES).v_Value1;
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase(numeracionRP+". FACTORES PSICOSOCIALES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(riesgospsicosociales, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region PSICOSENSOMETRICO
            var psicosensometrico = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PSICOSENSOMETRICO_DESC) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PSICOSENSOMETRICO_DESC).v_Value1;
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase(numeracionPS+". PSICOSENSOMÉTRICO", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(psicosensometrico, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region  CONCLUSIONES
            var aptitud = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_APTITUD_PSICO) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_APTITUD_PSICO).v_Value1;
            var conclusion_desc = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_APTITUD_PSICO_DESC) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_APTITUD_PSICO_DESC).v_Value1;

            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase(numeracionC+". CONCLUSIONES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                
                new PdfPCell(new Phrase("RESULTADO - APTITUD PSICOLOGICA", fontColumnValueBold)) { Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("APTO", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(aptitud == "-1" ?"":aptitud == "1" ?"X":aptitud=="2"?"":aptitud=="3"?"":aptitud, fontColumnValue)) { Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("OBSERVADO", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(aptitud == "-1" ?"":aptitud == "1" ?"":aptitud=="2"?"X":aptitud=="3"?"":aptitud, fontColumnValue)) { Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("NO APTO", fontColumnValueBold)) { Colspan =3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(aptitud == "-1" ?"":aptitud == "1" ?"":aptitud=="2"?"":aptitud=="3"?"X":aptitud, fontColumnValue)) { Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("OBSERVACIONES: " + conclusion_desc == null ?"NO PRESENTA OBSERVACIONES - APTO":conclusion_desc, fontColumnValueBold)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            //var psico = serviceComponent.Find(p => p.i_CategoryId == (int)Sigesoft.Common.Consultorio.Psicología);

           #region  RECOMENDACIONES
            var tipo_recomendacion_1 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_RECOMEN_INTERVENCION_TERAPEUTICA) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_RECOMEN_INTERVENCION_TERAPEUTICA).v_Value1;
            var tipo_recomendacion_2 = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_RECOMEN_CONSEJERIA_REFORZAMIENTO) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_RECOMEN_CONSEJERIA_REFORZAMIENTO).v_Value1;
            var recomendacion = informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_RECOMEN_DESC) == null ? "" : informe_psico_goldfields.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_RECOMEN_DESC).v_Value1;

            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase(numeracionR+". RECOMENDACIONES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                
                new PdfPCell(new Phrase("RESULTADO - APTITUD PSICOLOGICA", fontColumnValueBold)) { Colspan =5, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Intervención Terapéutico (Psicólogo - Psiquiatra)", fontColumnValueBold)) { Colspan =9,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Consejería - Reforzamiento", fontColumnValueBold)) { Colspan =6,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase(tipo_recomendacion_1 == "1" ?"X":tipo_recomendacion_1=="0"?"":tipo_recomendacion_1, fontColumnValue)) { Colspan =9,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase(tipo_recomendacion_2 == "1" ?"X":tipo_recomendacion_2=="0"?"":tipo_recomendacion_2, fontColumnValue)) { Colspan =6,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                new PdfPCell(new Phrase("RECOMENDACIÓN: " + recomendacion == null ?"NO REFIERE RECOMENDACIONE":recomendacion, fontColumnValueBold)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f},    
                
                new PdfPCell(new Phrase("ATENDIDO POR: ", fontColumnValue)){ Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                new PdfPCell(new Phrase(DatosGrabo.Nombre, fontColumnValue)){ Colspan=15, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                //new PdfPCell(new Phrase(psico.v_CreationUser, fontColumnValue)){ Colspan=15, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
     
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
