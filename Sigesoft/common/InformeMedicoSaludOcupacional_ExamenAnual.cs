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
    public class InformeMedicoSaludOcupacional_ExamenAnual
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateInformeMedicoOcupacionalExamenMedicoAnual(ServiceList DataService,
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
                TipoExamen = "PERIODICO ANUAL";
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
                    new PdfPCell(new Phrase("INFORME MÉDICO DE SALUD OCUPACIONAL \n EXAMEN MÉDICO " + TipoExamen, fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                };
            columnWidths = new float[] {100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS GENERALES

            string[] servicio = datosPac.FechaServicio.ToString().Split(' ');
            string[] fechaInforme = datosPac.FechaActualizacion.ToString().Split(' ');

            cells = new List<PdfPCell>()
            {        
                new PdfPCell(new Phrase("I. DATOS PERSONALES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Apellidos y Nombres", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("Edad", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString() , fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Sexo", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.Genero, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Profesión u ocupación", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Lugar de nacimiento", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_BirthPlace, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Procedencia", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.v_DistrictName + "-" + datosPac.v_ProvinceName + "-"+ datosPac.v_DepartamentName, fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Empresa", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Estado Civil:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_MaritalStatus, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Medico evaluador", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(DataService.NombreDoctor, fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha de evaluación:", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(servicio[0], fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha de informe:", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(fechaInforme[0], fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
              
                new PdfPCell(new Phrase("II. ANTECEDENTES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
            };
            columnWidths = new float[] { 1f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 9f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region Hábitos Nocivos

            List<NoxiousHabitsList> Alcohol = null;
            List<NoxiousHabitsList> Tabaco = null;
            List<NoxiousHabitsList> Drogas = null;

            if (listaHabitoNocivos != null)
            {
                Alcohol = listaHabitoNocivos.FindAll(p => p.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.Alcohol);
                Tabaco = listaHabitoNocivos.FindAll(p => p.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.Tabaco);
                Drogas = listaHabitoNocivos.FindAll(p => p.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.Drogas);
            }


            cells = new List<PdfPCell>()
                {
                     //fila
                    //new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    //new PdfPCell(new Phrase("HÁBITOS NOCIVOS ", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                    //new PdfPCell(new Phrase("FRECUENCIA ", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },

                    //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("- Consumo de alcohol:  ", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                    new PdfPCell(new Phrase(Alcohol ==  null || Alcohol.Count == 0 ? string.Empty :Alcohol[0].v_Frequency, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },

                    //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("- Fuma:", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                    new PdfPCell(new Phrase(Tabaco ==  null || Tabaco.Count == 0 ? string.Empty :Tabaco[0].v_Frequency, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },

                    //fila
                    //new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    //new PdfPCell(new Phrase("DROGAS ", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                    //new PdfPCell(new Phrase(Drogas ==  null || Drogas.Count == 0 ? string.Empty :Drogas[0].v_Frequency, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, FixedHeight=2f },    
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, FixedHeight=2f }, 
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, FixedHeight=2f },

                };

            columnWidths = new float[] { 1f, 20f, 79f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion
            #region Antecedentes Patológicos Personales

            string AlergiaX = "", DiabetesX = "", HepatitisBX = "", TBCX = "", AsmaX = "", HTAX = "", ITSX = "", TifoideaX = "", BronquitisX = "", NeoplasiasX = "", ConvulsionesX = "", QuemadurasX = "", CirugiasX = "", IntoxicacionesX = "";

            var Alergia = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.ALERGIA_NO_ESPECIFICADA);
            var Diabetess = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.DIABETES_MELLITUS);
            var TBC = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.TUBERCULOSIS);
            var Hepatitis = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.HEPATITISB);
            var Asma = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.ASMA);
            var HTA = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.HTA);
            var ITS = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.ITS);
            var Tifoidea = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.TIFOIDEA);
            var Bronquitis = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.BRONQUITIS);

            var Neoplasias = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.NEOPLASIAS);
            var Convulsiones = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.CONVULSIONES);
            var Quemaduras = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.QUEMADURAS);
            var Cirugias = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.CIRUGIAS);
            var Intoxicaciones = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.INTOXICACIONES);

            #region Marcar con X

            if (Bronquitis.Count() != 0)
            {
                if (Bronquitis != null) BronquitisX = "SI";
                else BronquitisX = "NO";
            }
            else BronquitisX = "NR";

            if (Tifoidea.Count() != 0)
            {
                if (Tifoidea != null) TifoideaX = "SI";
                else TifoideaX = "NO";
            }
            else TifoideaX = "NR";
            if (ITS.Count() != 0)
            {
                if (ITS != null) ITSX = "SI";
                else ITSX = "NO";
            }
            else ITSX = "NR";

            if (HTA.Count() != 0)
            {
                if (HTA != null)
                {
                    HTAX = "SI";
                }
                else
                {
                    HTAX = "NO";
                }
            }
            else
            {
                HTAX = "NR";
            }
            if (Asma.Count() != 0)
            {
                if (Asma != null)
                {
                    AsmaX = "SI";
                }
                else
                {
                    AsmaX = "NO";
                }
            }
            else
            {
                AsmaX = "NR";
            }
            if (Alergia.Count() != 0)
            {
                if (Alergia != null)
                {
                    AlergiaX = "SI";
                }
                else
                {
                    AlergiaX = "NO";
                }
            }
            else
            {
                AlergiaX = "NR";
            }

            if (Diabetess.Count() != 0)
            {
                if (Diabetess != null)
                {
                    DiabetesX = "SI";
                }
                else
                {
                    DiabetesX = "NO";
                }
            }
            else
            {
                DiabetesX = "NR";
            }

            if (TBC.Count() != 0)
            {
                if (TBC != null)
                {
                    TBCX = "SI";
                }
                else
                {
                    TBCX = "NO";
                }
            }
            else
            {
                TBCX = "NR";
            }

            if (Hepatitis.Count() != 0)
            {
                if (Hepatitis != null)
                {
                    HepatitisBX = "SI";
                }
                else
                {
                    HepatitisBX = "NO";
                }
            }
            else
            {
                HepatitisBX = "NR";
            }

            if (Neoplasias.Count() != 0)
            {
                if (Neoplasias != null)
                {
                    NeoplasiasX = "SI";
                }
                else
                {
                    NeoplasiasX = "NO";
                }
            }
            else
            {
                NeoplasiasX = "NR";
            }

            if (Convulsiones.Count() != 0)
            {
                if (Convulsiones != null)
                {
                    ConvulsionesX = "SI";
                }
                else
                {
                    ConvulsionesX = "NO";
                }
            }
            else
            {
                ConvulsionesX = "NR";
            }

            if (Quemaduras.Count() != 0)
            {
                if (Quemaduras != null)
                {
                    QuemadurasX = "SI";
                }
                else
                {
                    QuemadurasX = "NO";
                }
            }
            else
            {
                QuemadurasX = "NR";
            }

            if (Cirugias.Count() != 0)
            {
                if (Cirugias != null)
                {
                    CirugiasX = "(X) " + Cirugias[0].v_DiagnosticDetail;
                }
                else
                {
                    CirugiasX = "---";
                }
            }
            else
            {
                CirugiasX = "---";
            }

            if (Intoxicaciones.Count() != 0)
            {
                if (Intoxicaciones != null)
                {
                    IntoxicacionesX = "SI";
                }
                else
                {
                    IntoxicacionesX = "NO";
                }
            }
            else
            {
                IntoxicacionesX = "NR";
            }


            #endregion
            #region No Refiere

            var noRefiereAP = string.Empty;

            if (Alergia.Count == 0 && Diabetess.Count == 0 && TBC.Count == 0 && Hepatitis.Count == 0 && Hepatitis.Count == 0
                && Asma.Count == 0 && HTA.Count == 0 && ITS.Count == 0 && Tifoidea.Count == 0
                && Bronquitis.Count == 0 && Neoplasias.Count == 0 && Convulsiones.Count == 0 && Quemaduras.Count == 0
                && Cirugias.Count == 0 && Intoxicaciones.Count == 0)
            {
                noRefiereAP = ": NO REFIERE";
            }

            #endregion

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("- Personales refiere lo siguiente " + noRefiereAP, fontColumnValueBold)){ Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },       
                    //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("  ALERGIAS", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },                                   
                    new PdfPCell(new Phrase(AlergiaX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },                                    
                    new PdfPCell(new Phrase("DIABETES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                    new PdfPCell(new Phrase(DiabetesX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("TBC", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase(TBCX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },                                        
                    new PdfPCell(new Phrase("HEPATITIS B", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase(HepatitisBX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    

                    //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("  ASMA ", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },                                   
                    new PdfPCell(new Phrase(AsmaX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },                                    
                    new PdfPCell(new Phrase("HTA", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase(HTAX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("ITS", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase(ITSX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },                                       
                    new PdfPCell(new Phrase("TIFOIDEA", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase(TifoideaX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE }, 

                    //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("  BRONQUITIS", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },                                   
                    new PdfPCell(new Phrase(BronquitisX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },                                    
                    new PdfPCell(new Phrase("NEOPLASIA", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                    new PdfPCell(new Phrase(NeoplasiasX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("CONVULSIONES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase(ConvulsionesX, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },                                        
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
 
                    //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("  QUEMADURAS", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },                                   
                    new PdfPCell(new Phrase(QuemadurasX, fontColumnValue)){Colspan=7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },

                    //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("  CIRUGÍAS", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },                                   
                    new PdfPCell(new Phrase(CirugiasX, fontColumnValue)){Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("INTOXICACIONES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },                                   
                    new PdfPCell(new Phrase(IntoxicacionesX, fontColumnValue)){Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },

                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 2f },    
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, FixedHeight=2F },                                   
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, FixedHeight=2F },

                };

            columnWidths = new float[] { 1f, 14f, 10f, 20f, 5f, 20f, 5f, 20f, 5f, };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, null);

            document.Add(filiationWorker);

            #endregion
            #region Antecedentes Patológicos Familiares

            cells = new List<PdfPCell>();

            var noRefiere = string.Empty;

            if (listaPatologicosFamiliares == null && listaPatologicosFamiliares.Count < 0)
            {
                noRefiere = ": NO REFIERE";
            }
            columnWidths = new float[] { 7f };
            include = "DxAndComment";

            List<FamilyMedicalAntecedentsList> ListaVacia = new List<FamilyMedicalAntecedentsList>();
            FamilyMedicalAntecedentsList oFamilyMedicalAntecedentsList = new FamilyMedicalAntecedentsList();

            oFamilyMedicalAntecedentsList.DxAndComment = "NO REFIERE ANTECEDENTES";
            ListaVacia.Add(oFamilyMedicalAntecedentsList);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("- Familiares refiere lo siguiente" + noRefiere, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            //Columna FAMILIAR
            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  PADRE", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var PadreDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.PADRE_OK);

            table = HandlingItextSharp.GenerateTableFromList(PadreDx.Count == 0 ? ListaVacia : PadreDx, columnWidths, include, fontColumnValue, PdfPCell.NO_BORDER,null,null);
            cell = new PdfPCell(table) { UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  MADRE", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var MadreDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.MADRE_OK);

            table = HandlingItextSharp.GenerateTableFromList(MadreDx.Count == 0 ? ListaVacia : MadreDx, columnWidths, include, fontColumnValue, PdfPCell.NO_BORDER, null, null);
            cell = new PdfPCell(table) { UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  HERMANOS", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var HermanosDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.HERMANOS_OK);

            table = HandlingItextSharp.GenerateTableFromList(HermanosDx.Count == 0 ? ListaVacia : HermanosDx, columnWidths, include, fontColumnValue, PdfPCell.NO_BORDER, null, null);
            cell = new PdfPCell(table) { UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  ESPOSO(A)", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var EspososDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.ABUELOS_OK);

            table = HandlingItextSharp.GenerateTableFromList(EspososDx.Count == 0 ? ListaVacia : EspososDx, columnWidths, include, fontColumnValue, PdfPCell.NO_BORDER, null, null);
            cell = new PdfPCell(table) { UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  HIJOS", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var HijosDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.HIJOS_OK);

            table = HandlingItextSharp.GenerateTableFromList(EspososDx.Count == 0 ? ListaVacia : HijosDx, columnWidths, include, fontColumnValue, PdfPCell.NO_BORDER, null, null);
            cell = new PdfPCell(table) { UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, FixedHeight=2f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, FixedHeight = 2f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK, FixedHeight = 2f };
            cells.Add(cell);
            columnWidths = new float[] { 1f, 10, 59f };


            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, null);

            document.Add(table);

            #endregion
            #region ANTECEDENTES LABORALES
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("III. ANTECEDENTES LABORALES", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },   
                };

            columnWidths = new float[] { 33f, 33f, 33f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro);

            document.Add(table);

            #region Antecedentes Ocupacionales

            cells = new List<PdfPCell>()
                {
                        new PdfPCell(new Phrase("EMPRESA", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },
                        new PdfPCell(new Phrase("PUESTO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },
                        new PdfPCell(new Phrase("ALTITUD (m.s.n.m)", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },
                        new PdfPCell(new Phrase("INICIO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },
                        new PdfPCell(new Phrase("FIN", fontColumnValueBold))  { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },
            };

            columnWidths = new float[] { 25f, 25f, 20f, 15f, 15f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro);

            document.Add(table);

            cells = new List<PdfPCell>();

            if (listAtecedentesOcupacionalesA != null && listAtecedentesOcupacionalesA.Count > 0)
            {
                columnWidths = new float[] { 5f, 7f };

                foreach (var item in listAtecedentesOcupacionalesA)
                {
                        //Columna EMPRESA
                        cell = new PdfPCell(new Phrase(item.v_Organization, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase(item.v_workstation, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase(item.i_GeografixcaHeight.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase(item.d_StartDate.Value.ToString("MM/yyyy"), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase(item.d_EndDate.Value.ToString("MM/yyyy"), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                }
                columnWidths = new float[] { 25f, 25f, 20f, 15f, 15f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 25f, 25f, 20f, 15f, 15f};
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,PdfPCell.NO_BORDER, null, fontColumnValueBold);

            document.Add(table);

            #endregion
            #endregion

            #region TRABAJO ACTUAL
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("IV. TRABAJO ACTUAL", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                };

            columnWidths = new float[] { 33f, 33f, 33f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro);

            document.Add(table);

            cells = new List<PdfPCell>();

            if (listAtecedentesOcupacionalesB != null && listAtecedentesOcupacionalesB.Count > 0)
            {
                columnWidths = new float[] { 5f, 7f };

                foreach (var item in listAtecedentesOcupacionalesB)
                {
                    //Columna OCUPACIÓN
                    cell = new PdfPCell(new Phrase(item.i_Trabajo_Actual == 1 ? item.v_workstation + " Puesto Actual " : item.v_workstation, fontColumnValue)) {Colspan=2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("  Uso de material de protección: ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE , FixedHeight=2f};
                    cells.Add(cell);
                    //Columna EXPOSICIÓN
                    cell = new PdfPCell(new Phrase("  Riesgos ocupacionales: ", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("  " + item.Exposicion == "" ? "NO REFIERE PELIGROS EN EL PUESTO" : item.Exposicion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase("  Uso de material de protección: ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, FixedHeight=3f };
                    cells.Add(cell);
                    //Columna EPPS
                    cell = new PdfPCell(new Phrase("  Uso de material de protección: ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("  " + item.Epps == "" ? "NO USÓ EPP" : item.Epps, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                }

                columnWidths = new float[] {20f , 80f };

            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 20f, 80f };
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro);

            document.Add(table);

            #endregion



            #region Anamnesis

            var rpta = anamnesis.i_HasSymptomId == null || anamnesis.i_HasSymptomId == 0 ? "No" : "Si";
            var sinto = anamnesis.v_MainSymptom == null ? "NO REFIERE" : anamnesis.v_MainSymptom + " / " + anamnesis.i_TimeOfDisease + "días";
            var relato = anamnesis.v_Story == null ? "PACIENTE ASINTOMÁTICO" : anamnesis.v_Story;

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("V. ANAMNESIS", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                    new PdfPCell(new Phrase("¿PRESENTA SÍNTOMAS?: " + rpta, fontColumnValue)),                   
                    new PdfPCell(new Phrase("SÍNTOMAS PRINCIPALES: " + sinto, fontColumnValue)){ Colspan = 2 ,HorizontalAlignment = Element.ALIGN_LEFT },                               
                    new PdfPCell(new Phrase("RELATO: " + relato, fontColumnValue)) { Colspan = 3 ,HorizontalAlignment = Element.ALIGN_LEFT },                        
                                                   
                };

            columnWidths = new float[] { 33f, 33f, 33f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTableNegro);

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
                new PdfPCell(new Phrase("VI. EXAMEN CLÍNICO", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Presión arterial:", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(pres_Sist +" / " + pres_Diast + " " + unidadpres_Sist , fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Pulso", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(frec_Card + " " + unidadfrec_Card, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Frec. Respiratoria:", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(frec_Resp + " "+unidadfrec_Resp, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Talla (m)", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(talla + " " +unidadtalla, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Peso (Kg)", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(peso + " " +unidadpeso, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("IMC (Kg/m2)", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(imc + " "+unidadimc, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
            
            };
            columnWidths = new float[] { 1f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 9f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EXAMEN FISICO
            string Estoscopia = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)).v_Value1;
            //}


            //     Estoscopia = ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)).v_Value1 == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)).v_Value1;

            string Estado_Mental = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_ESTADO_METAL_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_ESTADO_METAL_DESCRIPCION_ID)).v_Value1;

            string PielX = "";
            string Piel = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID)).v_Value1;
            string PielHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_ID)).v_Value1;
            if (PielHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) PielX = "X";

            string CabelloX = "";
            string Cabello = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID)).v_Value1;
            string CabelloHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_ID)).v_Value1;
            if (CabelloHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) CabelloX = "X";


            string OidoX = "";
            string Oido = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID)).v_Value1;
            string OidoHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_ID)).v_Value1;
            if (OidoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) OidoX = "X";


            string NarizX = "";
            string Nariz = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID)).v_Value1;
            string NarizHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_ID)).v_Value1;
            if (NarizHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) NarizX = "X";


            string BocaX = "";
            string Boca = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID)).v_Value1;
            string BocaHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_ID)).v_Value1;
            if (BocaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) BocaX = "X";


            string FaringeX = "";
            string Faringe = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID)).v_Value1;
            string FaringeHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_ID)).v_Value1;
            if (FaringeHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) FaringeX = "X";


            string CuelloX = "";
            string Cuello = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID)).v_Value1;
            string CuelloHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_ID)).v_Value1;
            if (CuelloHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) CuelloX = "X";


            string ApaRespiratorioX = "";
            string ApaRespiratorio = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID)).v_Value1;
            string ApaRespiratorioHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATORESPIRATORIO_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATORESPIRATORIO_ID)).v_Value1;
            if (ApaRespiratorioHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaRespiratorioX = "X";


            string ApaCardioVascularX = "";
            string ApaCardioVascular = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID)).v_Value1;
            string ApaCardioVascularHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_ID)).v_Value1;
            if (ApaCardioVascularHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaCardioVascularX = "X";


            string ApaDigestivoX = "";
            string ApaDigestivo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID)).v_Value1;
            string ApaDigestivoHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_ID)).v_Value1;
            if (ApaDigestivoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaDigestivoX = "X";


            string ApaGenitoUrinarioX = "";
            string ApaGenitoUrinario = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID)).v_Value1;
            string ApaGenitoUrinarioHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_GENITOURINARIO_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_GENITOURINARIO_ID)).v_Value1;
            if (ApaGenitoUrinarioHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaGenitoUrinarioX = "X";

            // Alejandro
            // si es mujer el trabajador mostrar sus antecedentes

            int? sex = DataService.i_SexTypeId;

            if (sex == (int?)Sigesoft.Common.Gender.FEMENINO)
            {
                ApaGenitoUrinario = string.Format("MENARQUIA: {0} ," +
                                                   "FUM: {1} ," +
                                                   "RÉGIMEN CATAMENIAL: {2} ," +
                                                   "GESTACIÓN Y PARIDAD: {3} ," +
                                                   "MAC: {4} ," +
                                                   "CIRUGÍA GINECOLÓGICA: {5}", string.IsNullOrEmpty(DataService.v_Menarquia) ? "NO REFIERE" : DataService.v_Menarquia,
                                                                                DataService.d_Fur == null ? "NO REFIERE" : DataService.d_Fur.Value.ToShortDateString(),
                                                                                string.IsNullOrEmpty(DataService.v_CatemenialRegime) ? "NO REFIERE" : DataService.v_CatemenialRegime,
                                                                                DataService.v_Gestapara,
                                                                                DataService.v_Mac,
                                                                                string.IsNullOrEmpty(DataService.v_CiruGine) ? "NO REFIERE" : DataService.v_CiruGine);

            }

            string ApaLocomotorX = "";
            string ApaLocomotor = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID)).v_Value1;
            string ApaLocomotorHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_ID)).v_Value1;
            if (ApaLocomotorHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaLocomotorX = "X";


            string MarchaX = "";
            string Marcha = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID)).v_Value1;
            string MarchaHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_ID)).v_Value1;
            if (MarchaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) MarchaX = "X";


            string ColumnaX = "";
            string Columna = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID)).v_Value1;
            string ColumnaHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLMNA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLMNA_ID)).v_Value1;
            if (ColumnaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ColumnaX = "X";


            string SuperioresX = "";
            string Superiores = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID)).v_Value1;
            string SuperioresHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADE_SUPERIORES_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADE_SUPERIORES_ID)).v_Value1;
            if (SuperioresHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SuperioresX = "X";


            string InferioresX = "";
            string Inferiores = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID)).v_Value1;
            string InferioresHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_ID)).v_Value1;
            if (InferioresHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) InferioresX = "X";


            string SistemaLinfaticoX = "";
            string SistemaLinfatico = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID)).v_Value1;
            string SistemaLinfaticoHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_ID)).v_Value1;
            if (SistemaLinfaticoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SistemaLinfaticoX = "X";


            string SistemaNerviosoX = "";
            string SistemaNervioso = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID)).v_Value1;
            string SistemaNerviosoHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_ID)).v_Value1;
            if (SistemaNerviosoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SistemaNerviosoX = "X";



            string Hallazgos = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_HALLAZGOS_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_HALLAZGOS_ID)).v_Value1;
            string AgudezaVisualOjoDerechoSC = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_DERECHO)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_DERECHO)).v_Value1;
            string AgudezaVisualOjoIzquierdoSC = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO)).v_Value1;


            //var ff = Oftalmologia.Find(p => p.v_Value1 == "20 / 30");
            string AgudezaVisualOjoDerechoCC = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO)).v_Value1;
            string AgudezaVisualOjoIzquierdoCC = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_IZQUIERDO)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_IZQUIERDO)).v_Value1;

            var ss = Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_NORMAL_ID);
            //var oTestIshihara = Oftalmologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_ID);

            //TEST DE ESTEREOPSIS:Frec. 10 seg/arc, Normal.
            string TestEstereopsisNormal = TestEstereopsis.Count() == 0 || ((ServiceComponentFieldValuesList)TestEstereopsis.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_NORMAL)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestEstereopsis.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_NORMAL)).v_Value1;
            string TestEstereopsisAnormal = TestEstereopsis.Count() == 0 || ((ServiceComponentFieldValuesList)TestEstereopsis.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_ANORMAL)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestEstereopsis.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_ANORMAL)).v_Value1;
            string TiempoEstereopsis = TestEstereopsis.Count() == 0 || ((ServiceComponentFieldValuesList)TestEstereopsis.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_TIEMPO)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestEstereopsis.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_TIEMPO)).v_Value1;

            string VisonProfundidad = "";
            if (TestEstereopsisNormal == "1")
            {
                VisonProfundidad = "NORMAL";
            }
            else if (TestEstereopsisAnormal == "1")
            {
                VisonProfundidad = "ANORMAL";
            }

            //TEST DE ISHIHARA: Anormal, Discromatopsia: No definida.
            string TestIshiharaNormal = TestIshihara.Count() == 0 || ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL)).v_Value1;
            string TestIshiharaAnormal = TestIshihara.Count() == 0 || ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL)).v_Value1;
            string Dicromatopsia = TestIshihara.Count() == 0 || ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC)).v_Value1Name;

            string VisonColores = "";
            if (TestIshiharaNormal == "1")
            {
                VisonColores = "NORMAL";
            }
            else if (TestIshiharaAnormal == "1")
            {

                VisonColores = " ANORMAL" + " DISCROMATOPSIA: " + Dicromatopsia;
            }


            string OjoAnexoX = "";
            string OjoAnexo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID)).v_Value1;
            string OjoAnexoHallazgo = ExamenFisco.Count() == 0 || ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_ID)).v_Value1;

            ServiceComponentList findOftalmologia = ExamenesServicio.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);

            string ValorOD_VC_SC = "", ValorOI_VC_SC = "", ValorOD_VC_CC = "", ValorOI_VC_CC = "";
            string ValorOD_VL_SC = "", ValorOI_VL_SC = "", ValorOD_VL_CC = "", ValorOI_VL_CC = "";

            if (findOftalmologia != null)
            {
                var OD_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_CERCA_OJO_DERECHO_ID);
                if (OD_VC_SC != null)
                {
                    if (OD_VC_SC.v_Value1 != null) ValorOD_VC_SC = OD_VC_SC.v_Value1Name;
                }

                var OI_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO);
                if (OI_VC_SC != null)
                {
                    if (OI_VC_SC.v_Value1 != null) ValorOI_VC_SC = OI_VC_SC.v_Value1Name;
                }

                var OD_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO);
                if (OD_VC_CC != null)
                {
                    if (OD_VC_CC.v_Value1 != null) ValorOD_VC_CC = OD_VC_CC.v_Value1Name;
                }

                var OI_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID);
                if (OI_VC_CC != null)
                {
                    if (OI_VC_CC.v_Value1 != null) ValorOI_VC_CC = OI_VC_CC.v_Value1Name;
                }

                var OD_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID);
                if (OD_VL_SC != null)
                {
                    if (OD_VL_SC.v_Value1 != null) ValorOD_VL_SC = OD_VL_SC.v_Value1Name;
                }

                var OI_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID);
                if (OI_VL_SC != null)
                {
                    if (OI_VL_SC.v_Value1 != null) ValorOI_VL_SC = OI_VL_SC.v_Value1Name;
                }

                var OD_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID);
                if (OD_VL_CC != null)
                {
                    if (OD_VL_CC.v_Value1 != null) ValorOD_VL_CC = OD_VL_CC.v_Value1Name;
                }

                var OI_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID);
                if (OI_VL_CC != null)
                {
                    if (OI_VL_CC.v_Value1 != null) ValorOI_VL_CC = OI_VL_CC.v_Value1Name;
                }


            }
            if (OjoAnexoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) OjoAnexoX = "X";

            cells = new List<PdfPCell>()
               {
                    //fila 
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                    new PdfPCell(new Phrase("Examen físico", fontColumnValueBold)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE  },    

                    new PdfPCell(new Phrase("ÓRGANO O SISTEMA", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },                        
                    new PdfPCell(new Phrase("NORMAL", fontColumnValue)){ Colspan= 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },
                    new PdfPCell(new Phrase("HALLAZGOS", fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },

                    new PdfPCell(new Phrase("CABELLOS", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    new PdfPCell(new Phrase(CabelloX, fontColumnValue)){ Colspan= 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    new PdfPCell(new Phrase(Cabello, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                      
                    new PdfPCell(new Phrase("OJOS Y ANEXOS", fontColumnValue)){Colspan=3,Rowspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },                        
                    new PdfPCell(new Phrase(OjoAnexoX, fontColumnValue)){ Colspan= 2, Rowspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    //new PdfPCell(new Phrase("HALLAZGOS", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    new PdfPCell(new Phrase(OjoAnexo, fontColumnValue)){ Colspan= 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },

                    //new PdfPCell(new Phrase("AGUDEZA VISUAL", fontColumnValue)){Rowspan=2, Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    ////Linea
                    ////linea en blanco
                    //new PdfPCell(new Phrase("O.D", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase("O.I", fontColumnValue)){Colspan=1,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase("O.D", fontColumnValue)){Colspan=1,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase("O.I", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    ////Linea
                    //new PdfPCell(new Phrase("VISIÓN DE LEJOS",fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    //new PdfPCell(new Phrase(ValorOD_VC_SC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase(ValorOI_VC_SC, fontColumnValue)){Colspan=1,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase(ValorOD_VC_CC, fontColumnValue)){Colspan=1,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase(ValorOI_VC_CC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    ////Linea
                    //new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    //new PdfPCell(new Phrase(ValorOD_VL_SC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase(ValorOI_VL_SC, fontColumnValue)){Colspan=1,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase(ValorOD_VL_CC, fontColumnValue)){Colspan=1,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase(ValorOI_VL_CC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //    //fila
                    //new PdfPCell(new Phrase("VISIÓN DE PROFUNDIDAD", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    //new PdfPCell(new Phrase(TiempoEstereopsis == "" ? "NO APLICA":"TEST DE ESTEREOPSIS: FREC. " + TiempoEstereopsis + "seg/arc, " +VisonProfundidad, fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    //new PdfPCell(new Phrase(VisonColores == "" ? "NO APLICA":"TEST DE ISHIHARA: " +VisonColores, fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    ////fila
                    //new PdfPCell(new Phrase("EXAMEN CLÍNICO", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    //new PdfPCell(new Phrase(OjoAnexo, fontColumnValue)){ Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    new PdfPCell(new Phrase("OÍDO", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },                        
                    new PdfPCell(new Phrase(OidoX, fontColumnValue)){ Colspan= 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    new PdfPCell(new Phrase(Oido, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    
                    new PdfPCell(new Phrase("NARIZ", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },                        
                    new PdfPCell(new Phrase(NarizX, fontColumnValue)){ Colspan= 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    new PdfPCell(new Phrase(Nariz, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },

                    new PdfPCell(new Phrase("FARINGE", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },                        
                    new PdfPCell(new Phrase(FaringeX, fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    new PdfPCell(new Phrase(Faringe, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                     
                    new PdfPCell(new Phrase("CUELLO", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },                        
                    new PdfPCell(new Phrase(CuelloX, fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    new PdfPCell(new Phrase(Cuello, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                     
                    new PdfPCell(new Phrase("COLUMNA", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },                        
                    new PdfPCell(new Phrase(ColumnaX, fontColumnValue)){ Colspan= 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    new PdfPCell(new Phrase(Columna, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    
                    new PdfPCell(new Phrase("MIEMBROS SUPERIORES", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },                        
                    new PdfPCell(new Phrase(SuperioresX, fontColumnValue)){ Colspan= 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    new PdfPCell(new Phrase(Superiores, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    
                    new PdfPCell(new Phrase("MIEMBROS INFERIORES", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },                        
                    new PdfPCell(new Phrase(InferioresX, fontColumnValue)){ Colspan= 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },
                    new PdfPCell(new Phrase(Inferiores, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },
                    

                    //new PdfPCell(new Phrase("PIEL", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },                        
                    //new PdfPCell(new Phrase(PielX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },
                    //new PdfPCell(new Phrase(Piel, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },

                    //new PdfPCell(new Phrase("BOCA", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(BocaX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(Boca, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },

                    //new PdfPCell(new Phrase("APARATO RESPIRATORIO", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(ApaRespiratorioX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(ApaRespiratorio, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                     
                    //new PdfPCell(new Phrase("APARATO CARDIO -VASCULAR", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(ApaCardioVascularX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(ApaCardioVascular, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                                              
                    //new PdfPCell(new Phrase("APARATO DIGESTIVO", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(ApaDigestivoX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(ApaDigestivo, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    
                    //new PdfPCell(new Phrase("APARATO GENITO -URINARIO", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(ApaGenitoUrinarioX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(ApaGenitoUrinario, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                            
                    //new PdfPCell(new Phrase("APARATO LOCOMOTOR", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(ApaLocomotorX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(ApaLocomotor, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    
                    //new PdfPCell(new Phrase("MARCHA", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(MarchaX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(Marcha, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    
                    //new PdfPCell(new Phrase("SISTEMA LINFÁTICO", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(SistemaLinfaticoX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(SistemaLinfatico, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    
                    //new PdfPCell(new Phrase("SISTEMA NERVIOSO", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },                        
                    //new PdfPCell(new Phrase(SistemaNerviosoX, fontColumnValue)){ Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
                    //new PdfPCell(new Phrase(SistemaNervioso, fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },
            };

            columnWidths = new float[] { 1f, 13f, 13f, 9f, 9f, 9f, 9f, 16f, 9f, 9f, 10f, 13f, 7f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region EXAMENES AUXILIARES
            cellsTit = new List<PdfPCell>()
                { 
                new PdfPCell(new Phrase("VII. EXAMENES AUXILIARES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                };

            columnWidths = new float[] { 1f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 9f };
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
                    new PdfPCell(new Phrase("VIII. HALLAZGOS", fontColumnValueBold)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },       
                   
                    new PdfPCell(new Phrase("N°", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },       
                    new PdfPCell(new Phrase("CIE 10", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13 ,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK},       
                    new PdfPCell(new Phrase("DIAGNOSTICO", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13 ,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK},       
                };
            columnWidths = new float[] { 5f, 20f, 75f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,null, fontTitleTableNegro, null);
            document.Add(filiationWorker);

            cells = new List<PdfPCell>();

            int nro = 1;
            var dx = Diagnosticos.FindAll(p => p.i_DiagnosticTypeId != (int)Sigesoft.Common.TipoDx.Normal);
            if (dx != null && dx.Count > 0)
            {
                foreach (var item in dx)
                {
                    cell = new PdfPCell(new Phrase(nro.ToString() + ".- ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.i_DiagnosticTypeId == (int)Sigesoft.Common.TipoDx.Normal ? "---" : item.v_Dx_CIE10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                  
                    nro++;
                }

                columnWidths = new float[] { 5f, 20f, 75f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                columnWidths = new float[] { 100 };
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTableNegro);

            document.Add(table);
            #endregion

            #region FACTORES DE RIESGO
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("IX. FACTORES DE RIESGO", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                };

            columnWidths = new float[] { 33f, 33f, 33f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro);

            document.Add(table);
            #endregion

            #region RECOMENDACIONES

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("X. RECOMENDACIONES", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},       

                    new PdfPCell(new Phrase("N°", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                    new PdfPCell(new Phrase("DESCRIPCIÓN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },                           
                };

            columnWidths = new float[] { 5f, 95f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,null, fontTitleTableNegro, null);

            document.Add(filiationWorker);


            cells = new List<PdfPCell>();

            int nroreco = 1;
            if (dx != null && dx.Count > 0)
            {
                foreach (var item in dx)
                {
                    columnWidths = new float[] { 95f };
                    include = "v_RecommendationName";

                    cell = new PdfPCell(new Phrase(nroreco.ToString() + ". ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);

                    table = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include,fontColumnValue, PdfPCell.NO_BORDER,null,null);
                    cell = new PdfPCell(table);
                    cells.Add(cell);
                    nroreco++;
                }

                columnWidths = new float[] { 5f, 95f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                columnWidths = new float[] { 100 };
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro,null);

            document.Add(table);


            #endregion

            #region CONDICION
            string Apto = "", NoApto = "", AptoConRestricciones = "", AptoObs = "";

            if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.Apto)
            {
                Apto = "X";
            }
            else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.NoApto)
            {
                NoApto = "X";
            }
            else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.AptRestriccion)
            {
                AptoConRestricciones = "X";
            }
            else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.AptoObs)
            {
                AptoObs = "X";
            }
            cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("IX. CONDICIÓN", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    

                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                    new PdfPCell(new Phrase("APTO", fontColumnValueBold)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase(Apto,fontColumnValue)){Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase("", fontColumnValueBold)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},

                    new PdfPCell(new Phrase("NO APTO", fontColumnValueBold)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase(NoApto,fontColumnValue)){Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase("",fontColumnValue)){Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("APTO CON RESTRICCIONES", fontColumnValueBold)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase(AptoConRestricciones,fontColumnValue)){Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase("",fontColumnValue)){Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},

                    new PdfPCell(new Phrase("OBSERVADO", fontColumnValueBold)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase(AptoObs,fontColumnValue)){Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},
                    new PdfPCell(new Phrase("",fontColumnValue)){Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},

                   
                    new PdfPCell(new Phrase("- Nombre: " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + ", " +  datosPac.v_FirstName , fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},    

                 };
            columnWidths = new float[] { 1f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 9f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,null, fontTitleTable);

            document.Add(filiationWorker);


            #endregion

            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls

            // Firma del trabajador ***************************************************
            PdfPCell cellFirmaTrabajador = null;
            //DirectoryInfo rutaFirma = null;
            //rutaFirma = new DirectoryInfo(WebConfigurationManager.AppSettings["FirmaHuella"].ToString());
            //iTextSharp.text.Image Firmajpg = iTextSharp.text.Image.GetInstance(rutaFirma +DataService.v_DocNumber + "_Firma.jpg");


            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 70, 30));
            else

                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));
            //cellFirmaTrabajador = new PdfPCell(Firmajpg);

            // Huella del trabajador **************************************************
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
            cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue)) { Rowspan = 2 };
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

                case Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID:

                    #region ELECTROCARDIOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 1,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight= tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID);
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

                case Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_ID:

                    #region EVALUACION_ERGONOMICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_CONCLUSION_ID);
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
                case Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID:

                    #region ALTURA_ESTRUCTURAL

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight =tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID);
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

                case Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_ID:

                    #region ALTURA_GEOGRAFICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)) { FixedHeight=tamañocelda});
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight = tamañocelda});
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.OSTEO_MUSCULAR_ID_1:

                    #region OSTEO_MUSCULAR

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DESCRIPCION);
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

                case Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID:

                    #region PRUEBA_ESFUERZO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID);
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
                case Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_ID:

                    #region TAMIZAJE_DERMATOLOGICO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID);
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

                case Sigesoft.Common.Constants.ODONTOGRAMA_ID:

                    #region ODONTOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
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
                case Sigesoft.Common.Constants.GINECOLOGIA_ID:

                    #region EVALUACION_GINECOLOGICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "No se han registrado datos." : descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight=tamañocelda});
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EXAMEN_MAMA_ID:

                    #region EXAMEN_MAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_MAMA_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "No se han registrado datos." : descripcion.v_Value1, fontColumnValue)) { FixedHeight=tamañocelda});

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight=tamañocelda});
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.AUDIOMETRIA_ID:

                    #region AUIDIOMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
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

                case Sigesoft.Common.Constants.ESPIROMETRIA_ID:

                    #region ESPIROMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
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

                case Sigesoft.Common.Constants.OFTALMOLOGIA_ID:

                    #region OFTALMOLOGIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
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

                case Sigesoft.Common.Constants.INMUNIZACIONES_ID:

                    #region INMUNIZACIONES

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INMUNIZACIONES_ID);
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

                case "N002-ME000000033":

                    #region PSICOLOGIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-ME000000033");
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
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
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
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
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

                case Sigesoft.Common.Constants.LUMBOSACRA_ID:

                    #region RX LUMBAR

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LUMBOSACRA_ID);
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

                case "N001-ME000000000":

                    #region INFORME LABORATORIO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight=tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N001-ME000000000");
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

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

        private static PdfPTable Anamnesis(ServiceComponentList serviceComponent, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor)
        {
            PdfPTable table = null;
            List<PdfPCell> cells = null;
            PdfPCell cell = null;
            float[] columnWidths = null;
            float tamañocelda = 15f;
            switch (serviceComponent.v_ComponentId)
            {

                case Sigesoft.Common.Constants.EXAMEN_FISICO_ID:

                    #region EXAMEN FÍSICO
                    
                    //var rpta = anamnesis.i_HasSymptomId == null || anamnesis.i_HasSymptomId == 0 ? "No" : "Si";
                    //var sinto = anamnesis.v_MainSymptom == null ? "NO REFIERE" : anamnesis.v_MainSymptom + " / " + anamnesis.i_TimeOfDisease + "días";
                    //var relato = anamnesis.v_Story == null ? "PACIENTE ASINTOMÁTICO" : anamnesis.v_Story;

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle)){ Colspan = 1,HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = tamañocelda };
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

                case Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_ID:

                    #region EVALUACION_ERGONOMICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_CONCLUSION_ID);
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
                case Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID:

                    #region ALTURA_ESTRUCTURAL

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID);
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

                case Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_ID:

                    #region ALTURA_GEOGRAFICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_DESCRIPCION_ID);
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


                case Sigesoft.Common.Constants.OSTEO_MUSCULAR_ID_1:

                    #region OSTEO_MUSCULAR

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DESCRIPCION);
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

                case Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID:

                    #region PRUEBA_ESFUERZO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID);
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
                case Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_ID:

                    #region TAMIZAJE_DERMATOLOGICO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID);
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

                case Sigesoft.Common.Constants.ODONTOGRAMA_ID:

                    #region ODONTOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
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
                case Sigesoft.Common.Constants.GINECOLOGIA_ID:

                    #region EVALUACION_GINECOLOGICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "No se han registrado datos." : descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { FixedHeight = tamañocelda });
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EXAMEN_MAMA_ID:

                    #region EXAMEN_MAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_MAMA_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "No se han registrado datos." : descripcion.v_Value1, fontColumnValue)) { FixedHeight = tamañocelda });

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
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
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
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
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

                case Sigesoft.Common.Constants.OFTALMOLOGIA_ID:

                    #region OFTALMOLOGIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
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

                case Sigesoft.Common.Constants.INMUNIZACIONES_ID:

                    #region INMUNIZACIONES

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INMUNIZACIONES_ID);
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

                case "N002-ME000000033":

                    #region PSICOLOGIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-ME000000033");
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
                case Sigesoft.Common.Constants.OIT_ID:

                    #region RX OIT

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OIT_ID);
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

                case Sigesoft.Common.Constants.RX_TORAX_ID:

                    #region RX TORAX

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_TORAX_ID);
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

                case Sigesoft.Common.Constants.LUMBOSACRA_ID:

                    #region RX LUMBAR

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LUMBOSACRA_ID);
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

                case "N001-ME000000000":

                    #region INFORME LABORATORIO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        FixedHeight = tamañocelda
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N001-ME000000000");
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

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

                default:
                    break;
            }

            return table;

        }

    }
}
