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
        List<HistoryList> listAtecedentesOcupacionales,
        List<FamilyMedicalAntecedentsList> listaPatologicosFamiliares,
        List<PersonMedicalHistoryList> listMedicoPersonales,
        List<NoxiousHabitsList> listaHabitoNocivos,
        ServiceList anamnesis,
        List<ServiceComponentList> exams, List<ServiceComponentList> ExamenesServicio
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
            #endregion

            var tamaño_celda = 14f;
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
                    new PdfPCell(new Phrase("INFORME MÉDICO DE SALUD OCUPACIONAL \n EXAMEN MÉDICO ANUAL", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
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
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },

                };

            columnWidths = new float[] { 1f, 20f, 79f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);

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
                if (Bronquitis != null) BronquitisX = "X";
                else BronquitisX = "---";
            }
            else BronquitisX = "---";

            if (Tifoidea.Count() != 0)
            {
                if (Tifoidea != null) TifoideaX = "X";
                else TifoideaX = "---";
            }
            else TifoideaX = "---";
            if (ITS.Count() != 0)
            {
                if (ITS != null) ITSX = "X";
                else ITSX = "---";
            }
            else ITSX = "---";

            if (HTA.Count() != 0)
            {
                if (HTA != null)
                {
                    HTAX = "X";
                }
                else
                {
                    HTAX = "---";
                }
            }
            else
            {
                HTAX = "---";
            }
            if (Asma.Count() != 0)
            {
                if (Asma != null)
                {
                    AsmaX = "X";
                }
                else
                {
                    AsmaX = "---";
                }
            }
            else
            {
                AsmaX = "---";
            }
            if (Alergia.Count() != 0)
            {
                if (Alergia != null)
                {
                    AlergiaX = "X";
                }
                else
                {
                    AlergiaX = "---";
                }
            }
            else
            {
                AlergiaX = "---";
            }

            if (Diabetess.Count() != 0)
            {
                if (Diabetess != null)
                {
                    DiabetesX = "X";
                }
                else
                {
                    DiabetesX = "---";
                }
            }
            else
            {
                DiabetesX = "---";
            }

            if (TBC.Count() != 0)
            {
                if (TBC != null)
                {
                    TBCX = "X";
                }
                else
                {
                    TBCX = "---";
                }
            }
            else
            {
                TBCX = "---";
            }

            if (Hepatitis.Count() != 0)
            {
                if (Hepatitis != null)
                {
                    HepatitisBX = "X";
                }
                else
                {
                    HepatitisBX = "---";
                }
            }
            else
            {
                HepatitisBX = "---";
            }

            if (Neoplasias.Count() != 0)
            {
                if (Neoplasias != null)
                {
                    NeoplasiasX = "X";
                }
                else
                {
                    NeoplasiasX = "---";
                }
            }
            else
            {
                NeoplasiasX = "---";
            }

            if (Convulsiones.Count() != 0)
            {
                if (Convulsiones != null)
                {
                    ConvulsionesX = "X";
                }
                else
                {
                    ConvulsionesX = "---";
                }
            }
            else
            {
                ConvulsionesX = "---";
            }

            if (Quemaduras.Count() != 0)
            {
                if (Quemaduras != null)
                {
                    QuemadurasX = "X";
                }
                else
                {
                    QuemadurasX = "---";
                }
            }
            else
            {
                QuemadurasX = "---";
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
                    IntoxicacionesX = "X";
                }
                else
                {
                    IntoxicacionesX = "---";
                }
            }
            else
            {
                IntoxicacionesX = "---";
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
                    new PdfPCell(new Phrase("- Personales refiere lo siguiente:" + noRefiereAP, fontColumnValueBold)){ Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },       
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

                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, FixedHeight=2F },                                   
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, FixedHeight=2F },

                };

            columnWidths = new float[] {1f, 14f, 10f, 20f, 5f, 20f, 5f, 20f, 5f, };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, null);

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
            
            cell = new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };    
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("- Familiares refiere lo siguiente" + noRefiere, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            //Columna FAMILIAR
            cell = new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };    
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  PADRE", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var PadreDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.PADRE_OK);

            table = HandlingItextSharp.GenerateTableFromList(PadreDx.Count == 0 ? ListaVacia : PadreDx, columnWidths, include, fontColumnValue);
            cell = new PdfPCell(table);
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };    
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  MADRE", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var MadreDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.MADRE_OK);

            table = HandlingItextSharp.GenerateTableFromList(MadreDx.Count == 0 ? ListaVacia : MadreDx, columnWidths, include, fontColumnValue);
            cell = new PdfPCell(table);
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };    
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  HERMANOS", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var HermanosDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.HERMANOS_OK);

            table = HandlingItextSharp.GenerateTableFromList(HermanosDx.Count == 0 ? ListaVacia : HermanosDx, columnWidths, include, fontColumnValue);
            cell = new PdfPCell(table);
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };    
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  ESPOSO(A)", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var EspososDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.ABUELOS_OK);

            table = HandlingItextSharp.GenerateTableFromList(EspososDx.Count == 0 ? ListaVacia : EspososDx, columnWidths, include, fontColumnValue);
            cell = new PdfPCell(table);
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };    
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("  HIJOS", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
            cells.Add(cell);
            var HijosDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.HIJOS_OK);

            table = HandlingItextSharp.GenerateTableFromList(EspososDx.Count == 0 ? ListaVacia : HijosDx, columnWidths, include, fontColumnValue);
            cell = new PdfPCell(table);
            cells.Add(cell);

            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };    
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE, FixedHeight = 2f };
            cells.Add(cell);
            cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK, FixedHeight = 2f };
            cells.Add(cell);
            columnWidths = new float[] { 1f, 10, 59f };


            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,PdfPCell.NO_BORDER, null, null);

            document.Add(table);

            #endregion
            #endregion

            #region ANTECEDENTES LABORALES
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("III. ANTECEDENTES LABORALES", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },   
                };

            columnWidths = new float[] { 33f, 33f, 33f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro);

            document.Add(table);
            #endregion

            #region TRABAJO ACTUAL
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("IV. TRABAJO ACTUAL", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                };

            columnWidths = new float[] { 33f, 33f, 33f };

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

            var per_abdomen = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID) == null ? "SIN RESULTADOS" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).v_Value1;
            var unidadper_abdomen = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).v_MeasurementUnitName;

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
                new PdfPCell(new Phrase(pres_Sist +" / " + pres_Diast + unidadpres_Sist , fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Pulso", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(frec_Card + unidadfrec_Card, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Frec. Respiratoria:", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(frec_Resp + unidadfrec_Resp, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 

                new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Talla (m)", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(talla + unidadtalla, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Peso (Kg)", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(peso + unidadpeso, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("IMC (Kg/m2)", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(imc + unidadimc, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
            
            };
            columnWidths = new float[] { 1f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 9f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EXAMENES AUXILIARES
            cellsTit = new List<PdfPCell>()
                { 
                new PdfPCell(new Phrase("VII. EXAMENES AUXILIARES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
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

            #region Hallazgos y recomendaciones
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("VIII. HALLAZGOS - RECOMENDACIONES", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       

                    new PdfPCell(new Phrase("CIE 10", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       
                    new PdfPCell(new Phrase("ESPECIFICACIONES", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       

                };
            columnWidths = new float[] { 20.6f, 40.6f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            cells = new List<PdfPCell>();

            var filterDiagnosticRepository = Diagnosticos.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);

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
                    new PdfPCell(new Phrase("IX. CONDICIÓN", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("APTO", fontColumnValueBold)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(Apto,fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                    new PdfPCell(new Phrase("APTO CON RESTRICCIONES", fontColumnValueBold)){Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(AptoConRestricciones,fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                    new PdfPCell(new Phrase("NO APTO", fontColumnValueBold)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(NoApto,fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},
                    
                
                 };
            columnWidths = new float[] { 1f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 9f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);

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

            switch (serviceComponent.v_ComponentId)
            {

                case Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID:

                    #region ELECTROCARDIOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 1,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_CONCLUSION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DESCRIPCION);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
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
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_MAMA_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "No se han registrado datos." : descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INMUNIZACIONES_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-ME000000033");
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OIT_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_TORAX_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LUMBOSACRA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N001-ME000000000");
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
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
