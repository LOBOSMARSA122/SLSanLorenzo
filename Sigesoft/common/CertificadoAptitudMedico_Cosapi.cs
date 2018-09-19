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
    public class CertificadoAptitudMedico_Cosapi
    {
        public static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        #region
        public static void CreateCertificadoMedicoOcupacional_Cosapi(ServiceList DataService,
            PacientList filiationData,
             List<DiagnosticRepositoryList> Diagnosticos,
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF
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
                imagenEmpresa.SetAbsolutePosition(40, 785);
                document.Add(imagenEmpresa);
            }
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("CERTIFICADO DE APTITUD MÉDICO OCUPACIONAL", fontTitle1)) { Colspan=2, BackgroundColor= BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                };
            columnWidths = new float[] { 80f, 20f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region DATOS GENERALES
            string sexM = " ", sexF = " ";
            if (datosPac.i_SexTypeId == 1) sexM = "X";
            else if (datosPac.i_SexTypeId == 2) sexF = "X";

            string PreOcupacional = "", Periodica = "", Retiro = "", Otros = "";
            if (DataService != null)
            {
                if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.PreOcupacional)
                {
                    PreOcupacional = "X";
                }
                else if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.PeriodicoAnual)
                {
                    Periodica = "X";
                }
                else if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.Retiro)
                {
                    Retiro = "X";
                }
                else
                {
                    Otros = "X";
                }

            }
            cells = new List<PdfPCell>()
            {     
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan=16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                new PdfPCell(new Phrase("Código", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                new PdfPCell(new Phrase(datosPac.v_IdService, fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},

                new PdfPCell(new Phrase("Certifica que el Sr.(a)", fontColumnValue)) { Colspan = 15, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Grupo Sanguineo y Factor" , fontColumnValue)) { Colspan = 3, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_BloodGroupName, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_BloodFactorName, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Nombres y Apellidos", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Documento de identidad (DNI ó C.E):", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(datosPac.v_DocNumber , fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString() , fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("Género:", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("M", fontColumnValue)) { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(sexM, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                new PdfPCell(new Phrase("F", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(sexF, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                
                new PdfPCell(new Phrase("Tipo de examen", fontColumnValue)) { Colspan = 4, Rowspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Preocupacional", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(PreOcupacional, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Periódico", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(Periodica, fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Retiro", fontColumnValue)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(Retiro, fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("Otro(Especifique)", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(Otros, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            
                new PdfPCell(new Phrase("Empresa", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Proyecto/Sede - Cliente", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(filiationData.v_NombreProtocolo, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Puesto de trabajo", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("N° de Historia Clínica", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase(null, fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2 },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Hallazgos y recomendaciones
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Recomendaciones", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       
                };
            columnWidths = new float[] { 20.6f, 40.6f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            cells = new List<PdfPCell>();

            var filterDiagnosticRepository = Diagnosticos.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);

            if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
            {
                columnWidths = new float[] { 100f };
                include = "Valor1";

                foreach (var item in filterDiagnosticRepository)
                {
                    ListaComun oListaComun = null;
                    List<ListaComun> Listacomun = new List<ListaComun>();

                    foreach (var Reco in item.Recomendations)
                    {
                        oListaComun = new ListaComun();
                        oListaComun.Valor1 = Reco.v_RecommendationName;
                        Listacomun.Add(oListaComun);
                    }

                    table = HandlingItextSharp.GenerateTableFromList(Listacomun, columnWidths, include, fontColumnValue);
                    cell = new PdfPCell(table);

                    cells.Add(cell);

                }
                columnWidths = new float[] { 100f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                columnWidths = new float[] { 100f };
            }
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null);
            document.Add(table);
            /////

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Restricciones", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       
                };
            columnWidths = new float[] { 20.6f, 40.6f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            cells = new List<PdfPCell>();

            var filterDiagnosticRepository_1 = Diagnosticos.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);

            if (filterDiagnosticRepository_1 != null && filterDiagnosticRepository_1.Count > 0)
            {
                columnWidths = new float[] { 100f };
                include = "Valor1";

                //var desc = Diagnosticos.Find(p => p.Restrictions != (int)Sigesoft.Common.FinalQualification.Descartado);
                foreach (var item_1 in filterDiagnosticRepository_1)
                {
                    ListaComun oListaComun = null;
                    List<ListaComun> Listacomun = new List<ListaComun>();

                    foreach (var Rest_1 in item_1.Restrictions)
                    {
                        oListaComun = new ListaComun();
                        oListaComun.Valor1 = Rest_1.v_RestrictionName;
                        Listacomun.Add(oListaComun);
                    }
                    table = HandlingItextSharp.GenerateTableFromList(Listacomun, columnWidths, include, fontColumnValue);
                    cell = new PdfPCell(table);
                    cells.Add(cell);

                }
                columnWidths = new float[] { 100f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                columnWidths = new float[] { 100 };
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null);
            document.Add(table);
            #endregion

            #region RESULTADO DE APROBACION / FECHAS
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
                    new PdfPCell(new Phrase("Resultado para Trabajar", fontColumnValue)){ Colspan=20,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor=BaseColor.GRAY, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase("APTO", fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(Apto,fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                    new PdfPCell(new Phrase("APTO CON RESTRICCIONES", fontColumnValue)){Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(AptoConRestricciones,fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                    new PdfPCell(new Phrase("NO APTO", fontColumnValue)){ Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(NoApto,fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},
                 };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion
            #region APTITUDES ESPECÍFICAS

            cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("APTITUDES ESPECÍFICAS:", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);

            ServiceComponentList geografico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_ID);
            if (geografico != null)
            {
                var alt_geograf = geografico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_APTO_ID) == null ? "" : geografico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_APTO_ID).v_Value1;
                string alt_geograf_1 = "", alt_geograf_2 = "", alt_geograf_3 = "", alt_geograf_4 = "";
                if (alt_geograf == "1") alt_geograf_1 = "X";
                else if (alt_geograf == "2") alt_geograf_2 = "X";
                else if (alt_geograf == "3") alt_geograf_3 = "X";
                else if (alt_geograf == "4") alt_geograf_4 = "X";


                cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Trabajador en altura geográfica mayor a 2500 m.s.n.m.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase("APTO", fontColumnValue)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(alt_geograf_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                        new PdfPCell(new Phrase("NO APTO", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(alt_geograf_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                        new PdfPCell(new Phrase("APTO CON RESTRIC.", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(alt_geograf_3, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                        new PdfPCell(new Phrase("OBSERVADO", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(alt_geograf_4, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            }
            else
            {
                cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Trabajador en altura geográfica mayor a 2500 m.s.n.m.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase("NO APLICA", fontColumnValue)) { Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            }

            ServiceComponentList estructural = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID);
            if (estructural != null)
            {
                var alt_estruc = estructural.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_APTO_ID) == null ? "" : estructural.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_APTO_ID).v_Value1;
                string alt_estruc_1 = "", alt_estruc_2 = "", alt_estruc_3 = "", alt_estruc_4 = "";
                if (alt_estruc == "1") alt_estruc_1 = "X";
                else if (alt_estruc == "2") alt_estruc_2 = "X";
                else if (alt_estruc == "3") alt_estruc_3 = "X";
                else if (alt_estruc == "4") alt_estruc_4 = "X";
                cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Trabajos en altura estructural mayor a 1.8m:", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase("APTO", fontColumnValue)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(alt_estruc_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                        new PdfPCell(new Phrase("NO APTO", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(alt_estruc_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                        new PdfPCell(new Phrase("APTO CON RESTRIC.", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(alt_estruc_3, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                        new PdfPCell(new Phrase("OBSERVADO", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(alt_estruc_4, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            }
            else
            {
                cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Trabajos en altura estructural mayor a 1.8m:", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase("NO APLICA", fontColumnValue)) { Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            }
            #endregion
            #region FECHA  NOMBRE DE MEDICO
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            string[] fechacaducidad = datosPac.FechaCaducidad.ToString().Split(' ');
            cells = new List<PdfPCell>()
                 {
                    
                    new PdfPCell(new Phrase("FECHA DE EXAMEN", fontColumnValue)){Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(fechaServicio[0],fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                    new PdfPCell(new Phrase("FECHA DE CADUCIDAD", fontColumnValue)){ Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(fechacaducidad[0],fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},
                    
                    new PdfPCell(new Phrase("MÉDICO: ", fontColumnValue)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(DataService.NombreDoctor, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase("Número de CMP:", fontColumnValue)){ Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(DataService.CMP, fontColumnValue)){ Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight= tamaño_celda},
                 };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

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
        #endregion
    }
}
