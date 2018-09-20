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
    public class Informe_Resultados_San_Martinm
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateInforme_Resultados_San_Martinm(ServiceList DataService,
        PacientList filiationData,
        List<DiagnosticRepositoryList> Diagnosticos,
        List<ServiceComponentList> serviceComponent,
        organizationDto infoEmpresa,
        PacientList datosPac,
        string filePDF,
        string RecoAudio,
        string RecoElectro,
        string RecoEspiro,
        string RecoNeuro,
        string RecoAltEst,
        string RecoActFis,
        string RecoCustNor,
        string RecoAlt7D,
        string RecoExaFis,
        string RecoExaFis7C,
        string RecoOsteoMus1,
        string RecoTamDer,
        string RecoOdon,
        string RecoPsico,
        string RecoRx,
        string RecoOit,
        string RecoOft,
        string Restricciton,
        string Aptitud,
        List<HistoryList> listAtecedentesOcupacionalesA, List<HistoryList> listAtecedentesOcupacionalesB,
        List<FamilyMedicalAntecedentsList> listaPatologicosFamiliares,
        List<PersonMedicalHistoryList> listMedicoPersonales,
        List<NoxiousHabitsList> listaHabitoNocivos,
        ServiceList anamnesis,
        List<ServiceComponentList> exams, List<ServiceComponentList> ExamenesServicio, List<ServiceComponentFieldValuesList> ExamenFisco,
        List<ServiceComponentFieldValuesList> TestIshihara, List<ServiceComponentFieldValuesList> TestEstereopsis, List<ServiceComponentFieldValuesList> Oftalmologia
        )
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

            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
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
            string TipoExamen = "";
            if (DataService.i_EsoTypeId == 1)
            {
                TipoExamen = "PRE OCUPACIONAL";
            }
            else if (DataService.i_EsoTypeId == 2)
            {
                TipoExamen = "PERIODICO";
            }
            else if (filiationData.i_EsoTypeId == 3)
            {
                TipoExamen = "RETIRO";
            }
            else if (DataService.i_EsoTypeId == 4)
            {
                TipoExamen = "PREVENTIVO";
            }
            else if (filiationData.i_EsoTypeId == 5)
            {
                TipoExamen = "REUBICACIÓN";
            }
            else if (DataService.i_EsoTypeId == 6)
            {
                TipoExamen = "CHEQUEO";
            }
            var servicioo = DataService.i_EsoTypeId;
            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("EVALUACIÓN MEDICO " + TipoExamen +" - SAN MARTÍN CONTRATISTAS GENERALES S.A \nINFORME DE RESULTADOS", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                                };
            columnWidths = new float[] { 10f, 80f , 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS GENERALES

            cells = new List<PdfPCell>()
            {        
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(": " +datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("SEDE", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(": " + DataService.EmpresaPropietaria, fontColumnValue)) { Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("PUESTO", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(": " + datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("FECHA DE EXAMEN", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(": " + datosPac.FechaServicio.ToString().Split(' ')[0], fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region EVALUACIÓN MÉDICA

            //Antropometria
            ServiceComponentList antro = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            var peso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
            var unidadpeso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_MeasurementUnitName;

            var talla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_Value1;
            var unidadtalla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_MeasurementUnitName;

            var imc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_Value1;
            var unidadimc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_MeasurementUnitName;

            //Funciones Vitales
            ServiceComponentList funcVit = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
            var pres_Sist = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "SIN RESULTADOS" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
            var unidadpres_Sist = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_MeasurementUnitName;

            var pres_Diast = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;
            var unidadpres_Diast = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_MeasurementUnitName;

            var frec_Card = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "SIN RESULTADOS" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_Value1;
            var unidadfrec_Card = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_MeasurementUnitName;

            var frec_Resp = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "SIN RESULTADOS" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_Value1;
            var unidadfrec_Resp = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_MeasurementUnitName;

            // Orina Completo
            var xOrinaCompleto = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);

            var Hemo = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA);


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("ANTROPOMETRÍA: ", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("PESO : " , fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(peso + " " + unidadpeso,fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("" , fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("TALLA : " , fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(talla + " " + unidadtalla,fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("" , fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("IMC : " , fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(imc + " " + unidadimc,fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("FUNCIONES VITALES: PRESIÓN ARTERIAL: ", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(pres_Sist +" / " + pres_Diast + " " + unidadpres_Sist ,fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("" , fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("PRUEBAS DE LABORATORIO: ", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

               
            };
            columnWidths = new float[] { 1f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 9f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EVALUACIÓN MÉDICA

            //Antropometria
            ServiceComponentList hemoglobinasolo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_ID);
            ServiceComponentList hemograma = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA);
 
            string hemoglobina="", hemoglobinaUNIDAD="";
            if (hemoglobinasolo != null)
	        {
                hemoglobina = hemoglobinasolo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_ID) == null ? "SIN RESULTADOS" : hemoglobinasolo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_ID).v_Value1;
                hemoglobinaUNIDAD = hemoglobinasolo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_ID) == null ? "" : hemoglobinasolo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_ID).v_MeasurementUnitName;
            } else if (hemograma!=null)
	        {
                hemoglobina = hemoglobinasolo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA) == null ? "SIN RESULTADOS" : hemoglobinasolo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA).v_Value1;
                hemoglobinaUNIDAD = hemoglobinasolo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA) == null ? "" : hemoglobinasolo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA).v_MeasurementUnitName;
            } else{
                hemoglobina = "NO APLICA";
                hemoglobinaUNIDAD = "";
            }
            
            ServiceComponentList grupoFactor = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID);
            string grupoSang="", factorSang="";
            if (grupoFactor!=null)
	        {
                grupoSang = grupoFactor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID) == null ? "SIN RESULTADOS" : grupoFactor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID).v_Value1;
                factorSang = grupoFactor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID) == null ? "" : grupoFactor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID).v_Value1;
            } else {
                grupoSang = "NO APLICA";
                factorSang = "";
            }
            ServiceComponentList orina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);
            string patologico="", nopatologico="", resultado="";
            if (orina!=null)
	        {
                patologico = orina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_RESULTADOS_ID) == null ? "SIN RESULTADOS" : orina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_RESULTADOS_ID).v_Value1;
                nopatologico = orina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_RESULTADO_ID) == null ? "SIN RESULTADOS" : orina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_RESULTADO_ID).v_Value1;
                if (patologico=="1")
	            {
		            resultado = "PATOLOGICO";
	            }
                else if (nopatologico=="1"){
                    resultado = "NO PATOLOGICO";
                }
            } else {
                resultado = "NO APLICA";
            }
            
            ServiceComponentList rpr = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.R_P_R_ID);
            string rpr_res="";
            if(rpr != null)
            {
                rpr_res = rpr.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INMUNOLOGIA_R_P_R_ID) == null ? "SIN RESULTADOS" : rpr.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INMUNOLOGIA_R_P_R_ID).v_Value1;
            }else{
                rpr_res="NO APLICA";
            }
            ServiceComponentList glucosa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GLUCOSA_ID);
            string glucosaa="", glucosaValor="";
            if (true)
	        {
                glucosaa = glucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_POST_PANDRIAL) == null ? "SIN RESULTADOS" : glucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_POST_PANDRIAL).v_Value1;
                glucosaValor = glucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_POST_PANDRIAL) == null ? "" : glucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_POST_PANDRIAL).v_MeasurementUnitName;
	        }
            else{
                glucosaa="NO APLICA";
            }

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase(hemoglobina + " " + hemoglobinaUNIDAD,fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("GRUPO Y FACTOR", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase(grupoSang + " " + factorSang,fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("EXAMEN DE ORINA", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase(resultado,fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("SEROLOGIA (RPR)", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase(rpr_res,fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("GLUCOSA", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase(glucosaa + " " + glucosaValor,fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
            
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("",fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
            
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("",fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
            
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                
            };
            columnWidths = new float[] { 1f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 9f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EXAMENES AUXILIARES
            cellsTit = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("EXAMENES AUXILIARES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            string[] excludeComponents = { Sigesoft.Common.Constants.ANTROPOMETRIA_ID,
                                                 Sigesoft.Common.Constants.FUNCIONES_VITALES_ID,
                                                 Sigesoft.Common.Constants.EXAMEN_FISICO_ID,
                                                 "N005-ME000000117",
                                                 "N005-ME000000116",
                                                 "N005-ME000000046"

                                             };

            var otherExams = ExamenesServicio.FindAll(p => !excludeComponents.Contains(p.v_ComponentId));
            foreach (var oe in otherExams)
            {
                table = TableBuilderReportFor312(oe, fontTitleTable, fontSubTitleNegroNegrita, fontColumnValue, subTitleBackGroundColor);

                if (table != null)
                    document.Add(table);
            }

            #endregion

            #region HALLAZGOS
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("CONCLUSIONES", fontColumnValueBold)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },       
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("N°", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },       
                    new PdfPCell(new Phrase("CIE 10", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13 ,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK},       
                    new PdfPCell(new Phrase("DIAGNOSTICO", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13 ,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK},       
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                };
            columnWidths = new float[] { 5, 5f, 20f, 65f, 5f};
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);

            cells = new List<PdfPCell>();

            int nro = 1;
            var dx = Diagnosticos.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);
            if (dx != null && dx.Count > 0)
            {
                foreach (var item in dx)
                {
                    cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(nro.ToString() + ". ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.i_DiagnosticTypeId == (int)Sigesoft.Common.TipoDx.Normal ? "---" : item.v_Dx_CIE10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    nro++;
                }

                columnWidths = new float[] { 5, 5f, 20f, 65f, 5f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)));
                columnWidths = new float[] { 5f, 90f, 5f };
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro);

            document.Add(table);
            #endregion

            #region RECOMENDACIONES

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("RECOMENDACIONES", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},       
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("N°", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                    new PdfPCell(new Phrase("DESCRIPCIÓN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },                           
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                };

            columnWidths = new float[] { 5f, 5f, 85f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

            document.Add(filiationWorker);


            cells = new List<PdfPCell>();

            int nroreco = 1;
            if (dx != null && dx.Count > 0)
            {
                foreach (var item in dx)
                {
                    columnWidths = new float[] { 95f };
                    include = "v_RecommendationName";

                    cell = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell); 

                    cell = new PdfPCell(new Phrase(nroreco.ToString() + ". ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);

                    table = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue, PdfPCell.NO_BORDER, null, null);
                    cell = new PdfPCell(table);
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell); 

                    nroreco++;
                }

                columnWidths = new float[] { 5f, 5f, 85f, 5f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)));
                columnWidths = new float[] { 5f, 90f, 5f };
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro, null);

            document.Add(table);


            #endregion

            #region DATOS GENERALES

            string[] servicio = datosPac.FechaServicio.ToString().Split(' ');
            string[] fechaInforme = datosPac.FechaActualizacion.ToString().Split(' ');

            cells = new List<PdfPCell>()
            {        
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("El Trabajador; deja expresa constancia de haber recibido el informe de fecha ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(datosPac.FechaServicio.ToString().Split(' ')[0], fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(", que declara conocer en todos sus", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("SEDE", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(": " + DataService.EmpresaPropietaria, fontColumnValue)) { Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("PUESTO", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(": " + datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("FECHA DE EXAMEN", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(": " + datosPac.FechaServicio.ToString().Split(' ')[0], fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
  
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
        }


         private static PdfPTable TableBuilderReportFor312(ServiceComponentList serviceComponent, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor)
        {
            PdfPTable table = null;
            List<PdfPCell> cells = null;
            PdfPCell cell = null;
            float[] columnWidths = null;
            float tamañocelda=15f;
            switch (serviceComponent.v_ComponentId)
            {

                case Sigesoft.Common.Constants.ELECTRO_GOLD:

                    #region ELECTROCARDIOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("ELECTROCARDIOGRAMA : ", fontSubTitle))
                    {
                        Colspan = 1,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight= tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ELECTRO_GOLD);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight=tamañocelda});
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight=tamañocelda});
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,PdfPCell.NO_BORDER);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID:

                    #region ELECTROCARDIOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 1,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight = tamañocelda });
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight = tamañocelda });
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID:

                    #region OFTALMOLOGIA SIMPLE

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("OFTALMOLOGÍA: ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight = tamañocelda });
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight = tamañocelda });
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID:

                    #region OFTALMOLOGIA COMPLETO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("OFTALMOLOGÍA: ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight = tamañocelda });
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight = tamañocelda });
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID:

                    #region OFTALMOLOGIA YANACOCHA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("OFTALMOLOGÍA: ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight = tamañocelda });
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight = tamañocelda });
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID:

                    #region OFTALMOLOGIA HUDBAY

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("OFTALMOLOGÍA: ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight = tamañocelda });
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight = tamañocelda });
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.ESPIROMETRIA_ID:

                    #region ESPIROMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("ESPIROMETRÍA: ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight = tamañocelda });
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight = tamañocelda });
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;

                    case Sigesoft.Common.Constants.AUDIOMETRIA_ID:

                    #region AUIDIOMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("AUDIOMETRÍA: ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight=tamañocelda});
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight=tamañocelda});
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;

                    case Sigesoft.Common.Constants.OIT_ID:

                    #region RX OIT

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("RADIOGRAFÍA DE TÓRAX: ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OIT_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight=tamañocelda});
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight=tamañocelda});
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.RX_TORAX_ID:

                    #region RX TORAX

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("RADIOGRAFÍA DE TÓRAX: ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_TORAX_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight=tamañocelda});
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight=tamañocelda});
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.PSICOLOGIA_ID:

                    #region PSICO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("PSICOLOGÍA", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight=tamañocelda});
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight=tamañocelda});
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;

                default:
                    break;
            }

            return table;

        }

    }
}
