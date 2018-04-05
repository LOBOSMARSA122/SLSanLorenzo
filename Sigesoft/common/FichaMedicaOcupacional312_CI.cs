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
    public class FichaMedicaOcupacional312_CI
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateFichaMedicalOcupacional312Report(ServiceList DataService,
                                                                    PacientList filiationData,
                                                                    List<HistoryList> listAtecedentesOcupacionales,
                                                                    List<FamilyMedicalAntecedentsList> listaPatologicosFamiliares,
                                                                    List<PersonMedicalHistoryList> listMedicoPersonales,
                                                                    List<NoxiousHabitsList> listaHabitoNocivos,
                                                                    List<ServiceComponentFieldValuesList> Antropometria,
                                                                    List<ServiceComponentFieldValuesList> FuncionesVitales,
                                                                    List<ServiceComponentFieldValuesList> ExamenFisco,
                                                                    List<ServiceComponentFieldValuesList> Oftalmologia,
                                                                    List<ServiceComponentFieldValuesList> Psicologia,
                                                                    List<ServiceComponentFieldValuesList> OIT,
                                                                    List<ServiceComponentFieldValuesList> RX,
                                                                    List<ServiceComponentFieldValuesList> Laboratorio,
                                                                    string Audiometria,
                                                                    List<ServiceComponentFieldValuesList> Espirometria,
                                                                    List<DiagnosticRepositoryList> ListDiagnosticRepository,
                                                                    List<RecomendationList> ListRecomendation,
                                                                    List<ServiceComponentList> ExamenesServicio,
                                                                    List<ServiceComponentFieldValuesList> ValoresDxLaboratorio,
                                                                    organizationDto infoEmpresaPropietaria,
                                                                    List<ServiceComponentFieldValuesList> TestIshihara,
                                                                    List<ServiceComponentFieldValuesList> TestEstereopsis,
                                                                    string filePDF)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //try
            //{NO_BORDER
            // step 2: we create aPA writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
            page.Dato = "Anexo-312/"+  DataService.v_Pacient;
            //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
            writer.PageEvent = page;

            // step 3: we open the document
            document.Open();
            // step 4: we Add content to the document
            // we define some fonts
            #region Declaration Tables

            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
            //string[] columnValues = null;
            string[] columnHeaders = null;


            PdfPTable filiationWorker = new PdfPTable(8);

            PdfPTable table = null;

            PdfPCell cell = null;

            #endregion

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            //Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion

            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));
            #region Title

            PdfPCell CellLogo = null;
            cells = new List<PdfPCell>();
            PdfPCell cellPhoto1 = null;

            if (filiationData.b_Photo != null)
                cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
            else
                cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            columnWidths = new float[] { 100f };

            var cellsTit = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("FICHA MÉDICO OCUPACIONAL", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("(ANEXO N° 02 - RM. N° 312-2011/MINSA)", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(table));
            cells.Add(cellPhoto1);

            columnWidths = new float[] { 20f, 60f, 20f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion


            #region Cabecera del Reporte

            string PreOcupacional = "", Periodica = "", Retiro = "", Otros = "";

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

            string Mes = "";
            Mes = Sigesoft.Common.Utils.Getmouth(DataService.i_MesV);


            cells = new List<PdfPCell>()
                 {
                    //fila
                    new PdfPCell(new Phrase("N° DE FICHA MÉDICA: ", fontColumnValue)),                                   
                    new PdfPCell(new Phrase(DataService.v_ServiceId, fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                    
                    new PdfPCell(new Phrase("FECHA DE ATENCIÓN", fontColumnValue))
                                             { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("DÍA", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.i_DiaV.ToString(), fontColumnValue))
                                             { HorizontalAlignment = PdfPCell.ALIGN_LEFT},                    
                    new PdfPCell(new Phrase("MES", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Mes.ToUpper(), fontColumnValue))
                                          { HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("AÑO", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.i_AnioV.ToString(), fontColumnValue))
                                              { HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    
                     //fila
                    new PdfPCell(new Phrase("TIPO DE EVALUACIÓN: ", fontColumnValue)),                                                       
                    new PdfPCell(new Phrase("PRE EMPLEO", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                    new PdfPCell(new Phrase(PreOcupacional, fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("ANUAL", fontColumnValue))
                                 { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Periodica, fontColumnValue))
                                          { HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("RETIRO", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Retiro, fontColumnValue))
                                          { HorizontalAlignment = PdfPCell.ALIGN_LEFT},                 
                    new PdfPCell(new Phrase("OTROS", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Otros, fontColumnValue))
                                          { HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                     //fila
                    new PdfPCell(new Phrase("LUGAR DE EXAMEN:", fontColumnValue)),                                                       
                    new PdfPCell(new Phrase(DataService.v_OwnerOrganizationName, fontColumnValue)) { Colspan = 9},   
                 
                 
                 };

            columnWidths = new float[] { 15f, 15f, 7f, 5f, 5f, 5f, 5f, 10f, 7f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Datos de la Empresa

            String PuestoPostula = "";
            if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.PreOcupacional)
            {
                PuestoPostula = DataService.v_CurrentOccupation;
            }
            else
            {
                PuestoPostula = "";
            }
            cells = new List<PdfPCell>()
                {
                     //fila
                    new PdfPCell(new Phrase("RAZÓN SOCIAL:", fontColumnValue)),  
                    new PdfPCell(new Phrase(DataService.EmpresaTrabajo, fontColumnValue))
                                 { Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("R.U.C. NRO: ", fontColumnValue)),   
                     new PdfPCell(new Phrase(DataService.RUC, fontColumnValue)),

                    //fila
                    new PdfPCell(new Phrase("ACTIVIDAD ECONÓMICA:", fontColumnValue)),
                                new PdfPCell(new Phrase(DataService.RubroEmpresaTrabajo, fontColumnValue))
                                 { Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                    //fila
                    new PdfPCell(new Phrase("LUGAR DE TRABAJO:", fontColumnValue)),
                                new PdfPCell(new Phrase(DataService.DireccionEmpresaTrabajo, fontColumnValue)) 
                                    { Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                     //fila
                    new PdfPCell(new Phrase("PUESTO DE TRABAJO:", fontColumnValue)),
                                new PdfPCell(new Phrase(DataService.v_CurrentOccupation, fontColumnValue)) 
                                    { Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                    //fila
                    new PdfPCell(new Phrase("PUESTO AL QUE POSTULA (SOLO PRE EMPLEO): ", fontColumnValue))
                                    { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(PuestoPostula, fontColumnValue))
                                    { Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
            columnWidths = new float[] { 15f, 10f, 7f, 5f, 5f, 5f, 5f, 5f, 8f, 9f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "I. DATOS DE LA EMPRESA", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Filiación del trabajador

            //PdfPCell cellPhoto = null;

            //if (filiationData.b_Photo != null)
            //    cellPhoto = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 15F));
            //else
            //    cellPhoto = new PdfPCell(new Phrase("Sin Foto", fontColumnValue));

            //cellPhoto.Rowspan = 5;
            //cellPhoto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //cellPhoto.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            string ResidenciaLugarTrabajoSI = "", ResidenciaLugarTrabajoNO = "";

            if (DataService.i_ResidenceInWorkplaceId == (int)Sigesoft.Common.SiNo.SI)
            {
                ResidenciaLugarTrabajoSI = "X";
            }
            else
            {
                ResidenciaLugarTrabajoNO = "X";
            }


            string ESSALUD = "", EPS = "", SCTR = "", OTRO = "";

            if (DataService.i_TypeOfInsuranceId == (int)Sigesoft.Common.TypeOfInsurance.ESSALUD)
            {
                ESSALUD = "X";
            }
            else if (DataService.i_TypeOfInsuranceId == (int)Sigesoft.Common.TypeOfInsurance.EPS)
            {
                EPS = "X";
            }
            else
            {
                OTRO = "X";
            }

            string DNI = "", Pass = "", Carnet = "";

            if (DataService.i_DocTypeId == 1)
            {
                cells.Add(new PdfPCell(new Phrase("ESTE EXAMEN NO APLICA AL PROTOCOLO DE ATENCIÓN.", fontColumnValue)));
                DNI = "X";
            }
            else if (DataService.i_DocTypeId == 2)
            {
                Pass = "X";
            }
            else if (DataService.i_DocTypeId == 4)
            {
                Carnet = "X";
            }
            string Mes1 = "";
            Mes1 = Sigesoft.Common.Utils.Getmouth(DataService.i_MesN);

            cells = new List<PdfPCell>()
                {
                    //fila
                    new PdfPCell(new Phrase("NOMBRES Y APELLIDOS: ", fontColumnValue)), 
                    new PdfPCell(new Phrase(DataService.v_Pacient, fontColumnValue))
                                                                        { Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    //new PdfPCell(new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 15F)) )
                    //                                                   { Rowspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, 
                    //                                                     VerticalAlignment = PdfPCell.ALIGN_MIDDLE 
                    //                                                   },
                     //new PdfPCell(cellPhoto),
                    //fila
                    new PdfPCell(new Phrase("FECHA DE NACIMIENTO: ", fontColumnValue)),                                   
                    new PdfPCell(new Phrase("DÍA: ", fontColumnValue))
                                                     { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.i_DiaN.ToString(), fontColumnValue)), 
                    new PdfPCell(new Phrase("MES: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Mes1.ToUpper(), fontColumnValue))
                                                     { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("AÑO: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.i_AnioN.ToString(), fontColumnValue)),
                 
                    //fila
                    new PdfPCell(new Phrase("EDAD: ", fontColumnValue)), 
                    new PdfPCell(new Phrase(DataService.i_Edad.ToString(), fontColumnValue))
                                                    { Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                   
           

                    //fila
                    new PdfPCell(new Phrase("[DOCUMENTO DE IDENTIDAD CARNET DE EXTRANJERÍA (  " + Carnet +"  ), DNI (  " + DNI+"  ), PASAPORTE (  " + Pass + "  )]: ", fontColumnValue))
                                                { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DataService.v_DocNumber, fontColumnValue))
                                                                        { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
                  
                    //fila
                    new PdfPCell(new Phrase("DIRECCIÓN FISCAL: ", fontColumnValue)) {  HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                    new PdfPCell(new Phrase(DataService.v_AdressLocation, fontColumnValue))
                                                                        { Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                    //fila
                    new PdfPCell(new Phrase("DISTRITO: ", fontColumnValue)){  HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.DistritoPaciente, fontColumnValue)) 
                                              { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},   
                    new PdfPCell(new Phrase("PROVINCIA: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(DataService.ProvinciaPaciente, fontColumnValue)),
                    new PdfPCell(new Phrase("DEPARTAMENTO: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(DataService.DepartamentoPaciente, fontColumnValue)) 
                                                         { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 


                    //fila
                    new PdfPCell(new Phrase("RESIDENCIA EN LUGAR DE TRABAJO: ", fontColumnValue)){  HorizontalAlignment = PdfPCell.ALIGN_RIGHT},                                  
                    new PdfPCell(new Phrase("SI: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},                                 
                    new PdfPCell(new Phrase(ResidenciaLugarTrabajoSI, fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("NO: ", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ResidenciaLugarTrabajoNO, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA EN LUGAR DE TRABAJO: ", fontColumnValue))
                                         { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DataService.v_ResidenceTimeInWorkplace, fontColumnValue)),
                    new PdfPCell(new Phrase("AÑOS ", fontColumnValue)),
                 

                    //fila
                    new PdfPCell(new Phrase("ESSALUD", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},                                  
                    new PdfPCell(new Phrase(ESSALUD, fontColumnValue)),                                    
                    new PdfPCell(new Phrase("EPS", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},   
                    new PdfPCell(new Phrase(EPS, fontColumnValue)),
                    new PdfPCell(new Phrase("OTRO", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                     new PdfPCell(new Phrase("", fontColumnValue)),                                   
                    new PdfPCell(new Phrase("SCTR", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},                                      
                    new PdfPCell(new Phrase(" ", fontColumnValue)), 
                    new PdfPCell(new Phrase("OTRO", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                    new PdfPCell(new Phrase(OTRO, fontColumnValue)),

                     //fila
                    new PdfPCell(new Phrase("CORREO ELECTRÓNICO: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},                                  
                    new PdfPCell(new Phrase(DataService.Email == "" ? "NO REFIERE" : DataService.Email, fontColumnValue))       
                                    { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},        
                    new PdfPCell(new Phrase("TELÉFONO: ", fontColumnValue))
                                    { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.Telefono, fontColumnValue))
                                    { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila
                    new PdfPCell(new Phrase("ESTADO CIVIL: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},                                
                    new PdfPCell(new Phrase(DataService.EstadoCivil, fontColumnValue))       
                                    { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},        
                    new PdfPCell(new Phrase("GRADO DE INSTRUCCIÓN: ", fontColumnValue))
                                    { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.GradoInstruccion, fontColumnValue))
                                    { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                     //fila
                    new PdfPCell(new Phrase("N° TOTAL DE HIJOS VIVOS: ", fontColumnValue))
                                    { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},               
                    new PdfPCell(new Phrase(filiationData.i_NumberLivingChildren.ToString(), fontColumnValue))       
                                    { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},        
                    new PdfPCell(new Phrase("N° FALLECIDOS: ", fontColumnValue))
                                    { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(filiationData.i_NumberDependentChildren.ToString(), fontColumnValue))
                                    { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                                                
                                                   
                };

            columnWidths = new float[] { 15f, 13f, 7f, 10f, 14f, 14f, 5f, 5f, 8f, 9f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "II. FILIACIÓN DEL TRABAJADOR", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Antecedentes Ocupacionales

            cells = new List<PdfPCell>();

            if (listAtecedentesOcupacionales != null && listAtecedentesOcupacionales.Count > 0)
            {
                columnWidths = new float[] { 5f, 7f };

                foreach (var item in listAtecedentesOcupacionales)
                {
                    //Columna EMPRESA
                    cell = new PdfPCell(new Phrase(item.v_Organization, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna ÁREA
                    cell = new PdfPCell(new Phrase(item.v_TypeActivity, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna OCUPACIÓN
                    cell = new PdfPCell(new Phrase(item.i_Trabajo_Actual == 1 ? item.v_workstation + " Puesto Actual " : item.v_workstation, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Crear Tabla en duro FECHA
                    List<PdfPCell> cells1 = new List<PdfPCell>();
                    cells1.Add(new PdfPCell(new Phrase("FECHA INICIO: ", fontColumnValueAntecedentesOcupacionales)));
                    cells1.Add(new PdfPCell(new Phrase(item.i_SoloAnio ==1 ?item.d_StartDate.Value.ToString("yyyy") : item.d_StartDate.Value.ToString("MM/yyyy"), fontColumnValueAntecedentesOcupacionales)));

                    cells1.Add(new PdfPCell(new Phrase("FECHA FIN: ", fontColumnValueAntecedentesOcupacionales)));
                    cells1.Add(new PdfPCell(new Phrase(item.i_SoloAnio == 1 ? item.d_EndDate.Value.ToString("yyyy") : item.d_EndDate.Value.ToString("MM/yyyy"), fontColumnValueAntecedentesOcupacionales)));

                    table = HandlingItextSharp.GenerateTableFromCells(cells1, columnWidths, "", fontTitleTable);

                    cell = new PdfPCell(table);
                    cells.Add(cell);

                    //Columna EXPOSICIÓN
                    cell = new PdfPCell(new Phrase(item.Exposicion == "" ? "NO REFIERE PELIGROS EN EL PUESTO" : item.Exposicion, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna TIPO OPERACIÓN
                    cell = new PdfPCell(new Phrase(item.TiempoLabor, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna EPPS
                    cell = new PdfPCell(new Phrase(item.Epps == "" ? "NO USÓ EPP" : item.Epps, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                }

                columnWidths = new float[] { 15f, 15f, 15f, 15f, 15f, 15f, 15f };

            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 15f, 15f, 15f, 15f, 15f, 15f, 15f };
            }

            columnHeaders = new string[] { "EMPRESA", "ÁREA", "OCUPACIÓN", "FECHA", "EXPOSICIÓN", "TIEMPO DE TRABAJO", "EPP" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "III. ANTECEDENTES OCUPACIONALES", fontTitleTableAntecedentesOcupacionales, columnHeaders);

            document.Add(table);

            #endregion

            //// Salto de linea
            //document.Add(new Paragraph("\r\n"));

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
            // Alejandro
            var Neoplasias = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.NEOPLASIAS);
            var Convulsiones = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.CONVULSIONES);
            var Quemaduras = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.QUEMADURAS);
            var Cirugias = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.CIRUGIAS);
            var Intoxicaciones = listMedicoPersonales.FindAll(p => p.v_DiseasesId == Sigesoft.Common.Constants.INTOXICACIONES);

            #region Marcar con X

            if (Bronquitis.Count() != 0)
            {
                if (Bronquitis != null)
                {
                    BronquitisX = "X";
                }
                else
                {
                    BronquitisX = "---";
                }
            }
            else
            {
                BronquitisX = "---";
            }

            if (Tifoidea.Count() != 0)
            {
                if (Tifoidea != null)
                {
                    TifoideaX = "X";
                }
                else
                {
                    TifoideaX = "---";
                }
            }
            else
            {
                TifoideaX = "---";
            }
            if (ITS.Count() != 0)
            {
                if (ITS != null)
                {
                    ITSX = "X";
                }
                else
                {
                    ITSX = "---";
                }
            }
            else
            {
                ITSX = "---";
            }

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

            // Alejandro
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
                    //listMedicoPersonales
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

            // Alejandro
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

                    //fila
                    new PdfPCell(new Phrase("ALERGIAS", fontColumnValue)),                                   
                    new PdfPCell(new Phrase(AlergiaX, fontColumnValue)),                                    
                    new PdfPCell(new Phrase("DIABETES", fontColumnValue)), 
                    new PdfPCell(new Phrase(DiabetesX, fontColumnValue)),
                    new PdfPCell(new Phrase("TBC", fontColumnValue)),
                    new PdfPCell(new Phrase(TBCX, fontColumnValue)),                                        
                    new PdfPCell(new Phrase("HEPATITIS B", fontColumnValue)),
                    new PdfPCell(new Phrase(HepatitisBX, fontColumnValue)),    

                    //fila
                    new PdfPCell(new Phrase("ASMA ", fontColumnValue)),                                   
                    new PdfPCell(new Phrase(AsmaX, fontColumnValue)),                                    
                    new PdfPCell(new Phrase("HTA", fontColumnValue)), 
                    new PdfPCell(new Phrase(HTAX, fontColumnValue)),
                    new PdfPCell(new Phrase("ITS", fontColumnValue)),
                    new PdfPCell(new Phrase(ITSX, fontColumnValue)),                                        
                    new PdfPCell(new Phrase("TIFOIDEA", fontColumnValue)),
                    new PdfPCell(new Phrase(TifoideaX, fontColumnValue)),    

                    //fila
                    new PdfPCell(new Phrase("BRONQUITIS", fontColumnValue)),                                   
                    new PdfPCell(new Phrase(BronquitisX, fontColumnValue)),                                    
                    new PdfPCell(new Phrase("NEOPLASIA", fontColumnValue)), 
                    new PdfPCell(new Phrase(NeoplasiasX, fontColumnValue)),
                    new PdfPCell(new Phrase("CONVULSIONES", fontColumnValue)),
                    new PdfPCell(new Phrase(ConvulsionesX, fontColumnValue)),                                        
                    new PdfPCell(new Phrase("OTROS", fontColumnValue)),
                    new PdfPCell(new Phrase("---", fontColumnValue)), 
 
                    //fila
                    new PdfPCell(new Phrase("QUEMADURAS", fontColumnValue)),                                   
                    new PdfPCell(new Phrase(QuemadurasX, fontColumnValue))
                                     { Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    //fila
                    new PdfPCell(new Phrase("CIRUGÍAS", fontColumnValue)),                                   
                    new PdfPCell(new Phrase(CirugiasX, fontColumnValue))
                                     { Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("INTOXICACIONES", fontColumnValue)),                                   
                    new PdfPCell(new Phrase(IntoxicacionesX, fontColumnValue))
                                     { Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                };

            columnWidths = new float[] { 20f, 5f, 20f, 5f, 20f, 5f, 20f, 5f, };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "IV. ANTECEDENTES PATOLÓGICOS PERSONALES" + noRefiereAP, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            //// Salto de linea
            //document.Add(new Paragraph("\r\n"));

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
                    new PdfPCell(new Phrase("HÁBITOS NOCIVOS ", fontColumnValue)), 
                    //new PdfPCell(new Phrase("Tipo ", fontColumnValue)),
                    //new PdfPCell(new Phrase("Cantidad ", fontColumnValue)),
                    new PdfPCell(new Phrase("FRECUENCIA ", fontColumnValue)),

                    //fila
                    new PdfPCell(new Phrase("ALCOHOL ", fontColumnValue)), 
                    //new PdfPCell(new Phrase(Alcohol.Count == 0 ? string.Empty : Alcohol[0].v_DescriptionHabit, fontColumnValue)),
                    //new PdfPCell(new Phrase(Alcohol.Count == 0 ? string.Empty :Alcohol[0].v_DescriptionQuantity, fontColumnValue)),
                    new PdfPCell(new Phrase(Alcohol ==  null || Alcohol.Count == 0 ? string.Empty :Alcohol[0].v_Frequency, fontColumnValue)),

                    //fila
                    new PdfPCell(new Phrase("TABACO ", fontColumnValue)), 
                    //new PdfPCell(new Phrase(Tabaco.Count == 0 ? string.Empty :Tabaco[0].v_DescriptionHabit, fontColumnValue)),
                    //new PdfPCell(new Phrase(Tabaco.Count == 0 ? string.Empty :Tabaco[0].v_DescriptionQuantity, fontColumnValue)),
                    new PdfPCell(new Phrase(Tabaco ==  null || Tabaco.Count == 0 ? string.Empty :Tabaco[0].v_Frequency, fontColumnValue)),

                    //fila
                    new PdfPCell(new Phrase("DROGAS ", fontColumnValue)), 
                    //new PdfPCell(new Phrase(Drogas.Count == 0 ? string.Empty :Drogas[0].v_DescriptionHabit, fontColumnValue)),
                    //new PdfPCell(new Phrase(Drogas.Count == 0 ? string.Empty :Drogas[0].v_DescriptionQuantity, fontColumnValue)),
                    new PdfPCell(new Phrase(Drogas ==  null || Drogas.Count == 0 ? string.Empty :Drogas[0].v_Frequency, fontColumnValue)),

                    //fila
                    //new PdfPCell(new Phrase("Medicamentos ", fontColumnValue)), 
                    //new PdfPCell(new Phrase(" ", fontColumnValue))
                    //              { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                };

            columnWidths = new float[] { 20f, 80f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Antecedentes Patológicos Familiares

            cells = new List<PdfPCell>();

            var noRefiere = string.Empty;

            if (listaPatologicosFamiliares != null && listaPatologicosFamiliares.Count > 0)
            {
                columnWidths = new float[] { 7f };
                include = "DxAndComment";

                List<FamilyMedicalAntecedentsList> ListaVacia = new List<FamilyMedicalAntecedentsList>();
                FamilyMedicalAntecedentsList oFamilyMedicalAntecedentsList = new FamilyMedicalAntecedentsList();

                oFamilyMedicalAntecedentsList.DxAndComment = "NO REFIERE ANTECEDENTES";
                ListaVacia.Add(oFamilyMedicalAntecedentsList);

                //Columna FAMILIAR
                cell = new PdfPCell(new Phrase("PADRE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);
                var PadreDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.PADRE_OK);


                // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                table = HandlingItextSharp.GenerateTableFromList(PadreDx.Count == 0 ? ListaVacia : PadreDx, columnWidths, include, fontColumnValue);
                cell = new PdfPCell(table);
                cells.Add(cell);


                //Columna FAMILIAR
                cell = new PdfPCell(new Phrase("MADRE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);
                var MadreDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.MADRE_OK);
                // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                table = HandlingItextSharp.GenerateTableFromList(MadreDx.Count == 0 ? ListaVacia : MadreDx, columnWidths, include, fontColumnValue);
                cell = new PdfPCell(table);
                cells.Add(cell);


                //Columna FAMILIAR
                cell = new PdfPCell(new Phrase("HERMANOS", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);
                var HermanosDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.HERMANOS_OK);
                // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                table = HandlingItextSharp.GenerateTableFromList(HermanosDx.Count == 0 ? ListaVacia : HermanosDx, columnWidths, include, fontColumnValue);
                cell = new PdfPCell(table);
                cells.Add(cell);

                //Columna FAMILIAR
                cell = new PdfPCell(new Phrase("ESPOSO(A)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);
                var EspososDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.ABUELOS_OK);
                // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                table = HandlingItextSharp.GenerateTableFromList(EspososDx.Count == 0 ? ListaVacia : EspososDx, columnWidths, include, fontColumnValue);
                cell = new PdfPCell(table);
                cells.Add(cell);

                //Columna FAMILIAR
                cell = new PdfPCell(new Phrase("HIJOS", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);
                var HijosDx = listaPatologicosFamiliares.FindAll(p => p.i_TypeFamilyId == (int)Sigesoft.Common.TypeFamily.HIJOS_OK);
                // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                table = HandlingItextSharp.GenerateTableFromList(EspososDx.Count == 0 ? ListaVacia : HijosDx, columnWidths, include, fontColumnValue);
                cell = new PdfPCell(table);
                cells.Add(cell);


                cell = new PdfPCell(table);
                cells.Add(cell);

                columnWidths = new float[] { 10, 60f };

            }
            else
            {
                noRefiere = ": NO REFIERE";
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "V. ANTECEDENTES PATOLÓGICOS FAMILIARES" + noRefiere, fontTitleTable, null);

            document.Add(table);

            cells = new List<PdfPCell>()
                {
                   //fila
                    new PdfPCell(new Phrase("Nro. HIJOS VIVOS", fontColumnValue)){  HorizontalAlignment = PdfPCell.ALIGN_RIGHT},   
                    new PdfPCell(new Phrase(DataService.HijosVivos == null ? "" : DataService.HijosVivos.ToString(), fontColumnValue)){  HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
                    new PdfPCell(new Phrase("NRO. HIJOS MUERTOS", fontColumnValue)){ Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},        
                    new PdfPCell(new Phrase(DataService.HijosDependientes == null ? "" : DataService.HijosDependientes.ToString(), fontColumnValue)){  HorizontalAlignment = PdfPCell.ALIGN_LEFT},                   
                    new PdfPCell(new Phrase("Nro. HERMANOS", fontColumnValue)){ Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                    new PdfPCell(new Phrase(DataService.i_NroHermanos == null ? "" : DataService.i_NroHermanos.ToString(), fontColumnValue)){  HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
  
                };

            columnWidths = new float[] { 15f, 5f, 5f, 5f, 15f, 5f, 5f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            cells = new List<PdfPCell>()
               {
                     //fila
                    new PdfPCell(new Phrase("OTROS ANTECEDENTES Y ABSENTISMO: ENFERMEDADES Y ACCIDENTES (ASOCIADOS A TRABAJO O NO) ", fontColumnValueNegrita))
                                    { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                                                                      
                    //fila
                    new PdfPCell(new Phrase("ENFERMEDAD, ACCIDENTE", fontColumnValue))
                                    { Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("ASOCIADO AL TRABAJO", fontColumnValue))
                                    { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("AÑO", fontColumnValue))
                                     { Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("DÍAS DE DESCANSO", fontColumnValue))
                                     { Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //fila
              
                    new PdfPCell(new Phrase("SI", fontColumnValue))
                                    { HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("NO", fontColumnValue))
                                    { HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase("", fontColumnValue)),
                    //new PdfPCell(new Phrase("", fontColumnValue)),
                    
               };

            foreach (var item in listMedicoPersonales)
            {
                cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_LEFT };
                cells.Add(cell);

                if (item.i_TypeDiagnosticId == (int)Sigesoft.Common.TipoDx.Accidente_Ocupacional || item.i_TypeDiagnosticId == (int)Sigesoft.Common.TipoDx.Enfermedad_Ocupacional)
                {
                    cell = new PdfPCell(new Phrase("X", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase("X", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                }

                cell = new PdfPCell(new Phrase(item.d_StartDate == null ? string.Empty : item.d_StartDate.Value.ToString("yyyy").ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(item.v_TreatmentSite, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);

            }

            columnWidths = new float[] { 40f, 10f, 10f, 10f, 30f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            document.NewPage();
            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));
            #region Title

            CellLogo = null;
            cells = new List<PdfPCell>();
            cellPhoto1 = null;

            //if (filiationData.b_Photo != null)
            //    cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
            //else
            cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            columnWidths = new float[] { 100f };

            var cellsTit1 = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

            table = HandlingItextSharp.GenerateTableFromCells(cellsTit1, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(table));
            cells.Add(cellPhoto1);

            columnWidths = new float[] { 20f, 60f, 20f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region Evaluación Médica

            //Antropometria
            string Talla = ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID)).v_Value1;
            string Peso = ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID)).v_Value1;
            string IMC = ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID)).v_Value1;
            string ICC = ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID)).v_Value1;
            string PerimetroCadera = ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID)).v_Value1;
            string PerimetroAbdominal = ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID)).v_Value1;
            string PorcentajeGrasaCorporal = ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_PORCENTAJE_GRASA_CORPORAL_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Antropometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ANTROPOMETRIA_PORCENTAJE_GRASA_CORPORAL_ID)).v_Value1;

            //Funciones Vitales
            string FrecResp = ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID)).v_Value1;
            string frecCard = ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)).v_Value1;
            string PAD = ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID)).v_Value1;
            string PAS = ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID)).v_Value1;
            string Temperatura = ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID)).v_Value1;
            string SaturacionOxigeno = ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)FuncionesVitales.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID)).v_Value1;

            string ConcatenadoOtros = "";

            if (PerimetroCadera != "0" && PerimetroCadera != string.Empty)
            {
                ConcatenadoOtros = ConcatenadoOtros + " PERÍMETRO CADERA : " + PerimetroCadera + "cm" + "; ";
            }

            if (PerimetroAbdominal != "0.00" && PerimetroAbdominal != "0,00" && PerimetroAbdominal != string.Empty)
            {
                ConcatenadoOtros = ConcatenadoOtros + "PERÍMETRO ABDOMINAL : " + PerimetroAbdominal + "cm" + "; ";
            }

            //if (PorcentajeGrasaCorporal != "0.00" && PorcentajeGrasaCorporal != "0,00" && PorcentajeGrasaCorporal != string.Empty)
            //{
            //    ConcatenadoOtros = ConcatenadoOtros + "% GRASA CORPORAL : " + PorcentajeGrasaCorporal + "; ";
            //}

            if (SaturacionOxigeno != "0" && SaturacionOxigeno != string.Empty)
            {
                ConcatenadoOtros = ConcatenadoOtros + "SAT. DE O2 : " + SaturacionOxigeno + "%" + "; ";
            }


            ConcatenadoOtros = ConcatenadoOtros == "" ? string.Empty : ConcatenadoOtros.Substring(0, ConcatenadoOtros.Length - 2);
            //Examen fisico
            //string Estoscopia = "";
            //if (((ServiceComponentFieldValuesList)ExamenFisco.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)).v_Value1 != null)
            //{
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


            string OjoDerecho = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == "N005-MF000000177")) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == "N005-MF000000177")).v_Value1;
            string OjoIzquierdo = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == "N005-MF000000226")) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == "N005-MF000000226")).v_Value1;

            string OjoDerecho1 = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == "N005-MF000000224")) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == "N005-MF000000224")).v_Value1;
            string OjoIzquierdo1 = Oftalmologia.Count() == 0 || ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == "N005-MF000000719")) == null ? string.Empty : ((ServiceComponentFieldValuesList)Oftalmologia.Find(p => p.v_ComponentFieldId == "N005-MF000000719")).v_Value1;
            

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

            ServiceComponentList findOftalmologia = ExamenesServicio.Find(p => p.v_ComponentId == "N005-ME000000028");

            string ValorOD_VC_SC = "", ValorOI_VC_SC = "", ValorOD_VC_CC = "", ValorOI_VC_CC = "";
            string ValorOD_VL_SC = "", ValorOI_VL_SC = "", ValorOD_VL_CC = "", ValorOI_VL_CC = "";

            if (findOftalmologia != null)
            {
                var OD_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N005-MF000000233");
                if (OD_VC_SC != null)
                {
                    if (OD_VC_SC.v_Value1 != null) ValorOD_VC_SC = OD_VC_SC.v_Value1;
                }

                var OI_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N005-MF000000227");
                if (OI_VC_SC != null)
                {
                    if (OI_VC_SC.v_Value1 != null) ValorOI_VC_SC = OI_VC_SC.v_Value1;
                }

                var OD_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N005-MF000000235");
                if (OD_VC_CC != null)
                {
                    if (OD_VC_CC.v_Value1 != null) ValorOD_VC_CC = OD_VC_CC.v_Value1;
                }

                var OI_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N005-MF000000646");
                if (OI_VC_CC != null)
                {
                    if (OI_VC_CC.v_Value1 != null) ValorOI_VC_CC = OI_VC_CC.v_Value1;
                }

                var OD_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N005-MF000000234");
                if (OD_VL_SC != null)
                {
                    if (OD_VL_SC.v_Value1 != null) ValorOD_VL_SC = OD_VL_SC.v_Value1;
                }

                var OI_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N005-MF000000230");
                if (OI_VL_SC != null)
                {
                    if (OI_VL_SC.v_Value1 != null) ValorOI_VL_SC = OI_VL_SC.v_Value1;
                }

                var OD_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N005-MF000000231");
                if (OD_VL_CC != null)
                {
                    if (OD_VL_CC.v_Value1 != null) ValorOD_VL_CC = OD_VL_CC.v_Value1;
                }

                var OI_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N005-MF000000236");
                if (OI_VL_CC != null)
                {
                    if (OI_VL_CC.v_Value1 != null) ValorOI_VL_CC = OI_VL_CC.v_Value1;
                }


            }
            if (OjoAnexoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) OjoAnexoX = "X";

            cells = new List<PdfPCell>()
               {
                     //fila
                    new PdfPCell(new Phrase("ANAMNESIS", fontColumnValue)),
                    new PdfPCell(new Phrase(DataService.v_Story, fontColumnValue))
                                    { Colspan=11, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                     
                    //fila
                    new PdfPCell(new Phrase("EXAMEN CLÍNICO", fontColumnValue))
                                    { Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},             
                    new PdfPCell(new Phrase("TALLA(m)", fontColumnValue)),                                        
                    new PdfPCell(new Phrase(Talla, fontColumnValue))
                                    { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("PESO (kg.)", fontColumnValue)){ Colspan = 2},
                    new PdfPCell(new Phrase(Peso, fontColumnValue)){ Colspan = 2},
                     new PdfPCell(new Phrase("IMC", fontColumnValue)),                                   
                    new PdfPCell(new Phrase(IMC, fontColumnValue)),                                               
                    new PdfPCell(new Phrase(ICC == "0.0" ? "" :"ICC", fontColumnValue))
                                     { HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(ICC == "0.0" ? " " : ICC, fontColumnValue)),

                     //fila                                                
                    new PdfPCell(new Phrase("FRECUENCIA RESPIRATORIA            (resp. x min)", fontColumnValue))
                                             { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(FrecResp, fontColumnValue)),
                                  
                    new PdfPCell(new Phrase("FRECUENCIA CARDÍACA           (lat x min)", fontColumnValue))
                                    { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(frecCard, fontColumnValue)),
                                    
                     new PdfPCell(new Phrase("PRESIÓN ARTERIAL (mmHg)", fontColumnValue))
                                  { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},         
                    new PdfPCell(new Phrase(PAS + " / " + PAD , fontColumnValue)), 
                                              
                    new PdfPCell(new Phrase(Temperatura == "0.0" ? "" :"TEMPERATURA (°C)", fontColumnValue)),
                    new PdfPCell(new Phrase(Temperatura == "0.0" || Temperatura == "0,0" || string.IsNullOrEmpty(Temperatura) ? "" : double.Parse(Temperatura).ToString("#.#"), fontColumnValue)),

                     //fila                                                
                    new PdfPCell(new Phrase("OTROS", fontColumnValue)),         
                    new PdfPCell(new Phrase(ConcatenadoOtros, fontColumnValue))
                                     { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila                                                
                    new PdfPCell(new Phrase("EXAMEN FÍSICO", fontColumnValue))
                                      { Colspan=12, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila    
                    new PdfPCell(new Phrase("ECTOSCOPÍA", fontColumnValue)),                        
                    new PdfPCell(new Phrase(Estoscopia, fontColumnValue))
                                      { Colspan=11, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila                                                
                     new PdfPCell(new Phrase("ESTADO MENTAL", fontColumnValue)),                      
                    new PdfPCell(new Phrase(Estado_Mental, fontColumnValue))
                                      { Colspan=11, HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                    //fila                                                
                     new PdfPCell(new Phrase("ÓRGANO O SISTEMA", fontColumnValue)),                        
                    new PdfPCell(new Phrase("SIN HALLAZGOS", fontColumnValue)),
                    new PdfPCell(new Phrase("HALLAZGOS", fontColumnValue))
                                      { Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila                                                
                     new PdfPCell(new Phrase("PIEL", fontColumnValue)),                       
                    new PdfPCell(new Phrase(PielX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Piel, fontColumnValue))
                                      { Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //flinea en blancoila                                                
                     new PdfPCell(new Phrase("CABELLOS", fontColumnValue)),                       
                    new PdfPCell(new Phrase(CabelloX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Cabello, fontColumnValue))
                                      { Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                         //fila                                                
                     new PdfPCell(new Phrase("OJOS Y ANEXOS", fontColumnValue))
                                { Rowspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(OjoAnexoX, fontColumnValue))
                                { Rowspan=7, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("HALLAZGOS", fontColumnValue))
                                { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(Hallazgos, fontColumnValue))
                                      { Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila

                    new PdfPCell(new Phrase("AGUDEZA VISUAL", fontColumnValue)){Rowspan=2, Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                

                    //Linea
                    //linea en blanco
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                 

                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE LEJOS",fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorOD_VC_SC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VC_SC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOD_VC_CC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VC_CC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorOD_VL_SC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VL_SC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOD_VL_CC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VL_CC, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                        //fila
                    new PdfPCell(new Phrase("VISIÓN DE PROFUNDIDAD", fontColumnValue))
                                { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(OjoDerecho == "" ? "NO APLICA":"OJO DERECHO: " + OjoDerecho + " / OJO IZQUIERDO: " +OjoIzquierdo, fontColumnValue))
                                { Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValue))
                                    { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(OjoDerecho1 == "" ? "NO APLICA":"OJO DERECHO: " + OjoDerecho1 + " / OJO IZQUIERDO: " +OjoIzquierdo1, fontColumnValue))
                                { Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                    //fila
                    new PdfPCell(new Phrase("EXAMEN CLÍNICO", fontColumnValue))
                                { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(OjoAnexo, fontColumnValue))
                                { Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila                                                
                     new PdfPCell(new Phrase("OÍDO", fontColumnValue)),                   
                    new PdfPCell(new Phrase(OidoX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Oido, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},       
                     new PdfPCell(new Phrase("NARIZ", fontColumnValue)),  
                    new PdfPCell(new Phrase(NarizX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Nariz, fontColumnValue))
                                      { Colspan=4708, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                                         //fila                                                
                     new PdfPCell(new Phrase("BOCA", fontColumnValue)),                        
                    new PdfPCell(new Phrase(BocaX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Boca, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                               
                     new PdfPCell(new Phrase("FARINGE", fontColumnValue)),                        
                    new PdfPCell(new Phrase(FaringeX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Faringe, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                                         //fila                                                
                     new PdfPCell(new Phrase("CUELLO", fontColumnValue)),                       
                    new PdfPCell(new Phrase(CuelloX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Cuello, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                           
                     new PdfPCell(new Phrase("APARATO RESPIRATORIO", fontColumnValue)),                  
                    new PdfPCell(new Phrase(ApaRespiratorioX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaRespiratorio, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                                         //fila                                                
                     new PdfPCell(new Phrase("APARATO CARDIO -VASCULAR", fontColumnValue)),                        
                    new PdfPCell(new Phrase(ApaCardioVascularX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaCardioVascular, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                           
                     new PdfPCell(new Phrase("APARATO DIGESTIVO", fontColumnValue)),                  
                    new PdfPCell(new Phrase(ApaDigestivoX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaDigestivo, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                                       //fila                                                
                     new PdfPCell(new Phrase("APARATO GENITO -URINARIO", fontColumnValue)),                      
                    new PdfPCell(new Phrase(ApaGenitoUrinarioX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaGenitoUrinario, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                               
                     new PdfPCell(new Phrase("APARATO LOCOMOTOR", fontColumnValue)),                     
                    new PdfPCell(new Phrase(ApaLocomotorX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaLocomotor, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                                       //fila                                                
                     new PdfPCell(new Phrase("MARCHA", fontColumnValue)),                        
                    new PdfPCell(new Phrase(MarchaX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Marcha, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                               
                     new PdfPCell(new Phrase("COLUMNA", fontColumnValue)),                      
                    new PdfPCell(new Phrase(ColumnaX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Columna, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                                       //fila                                                
                     new PdfPCell(new Phrase("MIEMBROS SUPERIORES", fontColumnValue)),                        
                    new PdfPCell(new Phrase(SuperioresX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Superiores, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                                
                     new PdfPCell(new Phrase("MIEMBROS INFERIORES", fontColumnValue)),                        
                    new PdfPCell(new Phrase(InferioresX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Inferiores, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                                       //fila                                                
                     new PdfPCell(new Phrase("SISTEMA LINFÁTICO", fontColumnValue)),
                     new PdfPCell(new Phrase(SistemaLinfaticoX, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase(SistemaLinfatico, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                               
                     new PdfPCell(new Phrase("SISTEMA NERVIOSO", fontColumnValue)),                        
                    new PdfPCell(new Phrase(SistemaNerviosoX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(SistemaNervioso, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}





               };

            columnWidths = new float[] { 14f, 13f, 9f, 9f, 9f, 9f, 16f, 9f, 9f, 10f, 16f, 7f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "VI. EVALUACIÓN MÉDICA (LLENAR CON LETRA CLARA O MARCAR CON UNA 'X')", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Evaluación Psicológicas

            //PSICOLOGIA
            //string AreaCognitiva = Psicologia.Count() == 0 || ((ServiceComponentFieldValuesList)Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_COGNITIVA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_COGNITIVA_ID)).v_Value1Name;
            //string AreaEmocional = Psicologia.Count() == 0 || ((ServiceComponentFieldValuesList)Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_EMOCIONAL_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_EMOCIONAL_ID)).v_Value1Name;
            string PsicologiaConclusiones = Psicologia.Count() == 0 || ((ServiceComponentFieldValuesList)Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID)).v_Value1;

            List<ServiceComponentList> ListaExamenesPsicologicos = ExamenesServicio.FindAll(p => p.i_CategoryId == 7).ToList();
            string DiagnosticosPsicologicos = "";

            foreach (var item in ListaExamenesPsicologicos)
            {
                foreach (var item1 in ListDiagnosticRepository)
                {
                    if (item1.v_ComponentId == item.v_ComponentId)
                    {
                        DiagnosticosPsicologicos = item1.v_DiseasesName + ";";
                    }
                }
            }

            DiagnosticosPsicologicos = DiagnosticosPsicologicos == "" ? string.Empty : DiagnosticosPsicologicos.Substring(0, DiagnosticosPsicologicos.Length - 1);
            cells = new List<PdfPCell>();

            if (Psicologia.Count() != 0)
            {
                cells = new List<PdfPCell>()
                  {
                       //fila
                        new PdfPCell(new Phrase(PsicologiaConclusiones, fontColumnValue)), 
                         //fila
                        //new PdfPCell(new Phrase("Hallazgos: " + DiagnosticosPsicologicos , fontColumnValue)), 

                  };

                columnWidths = new float[] { 100f };
            }
            else
            {
                if (ListaExamenesPsicologicos != null)
                {
                    //cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("ESTE EXAMEN NO APLICA AL PROTOCOLO DE ATENCIÓN.", fontColumnValue)));
                }
                else
                {
                    cells.Add(new PdfPCell(new Phrase("ESTE EXAMEN NO APLICA AL PROTOCOLO DE ATENCIÓN.", fontColumnValue)));
                }

                //cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                columnWidths = new float[] { 100f };
            }


            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "VII. CONCLUSIONES DE EVALUACIÓN PSICOLÓGICA", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Conclusiones Radiográficas

            //RX
            //string ConclusionesOIT = OIT.Count() == 0 || OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_ID) == null ? string.Empty : OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_ID).v_Value1Name;
            //string ConclusionesOITDescripcion = OIT.Count() == 0 || ((ServiceComponentFieldValuesList)OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID)).v_Value1;

            //string ConclusionesRadiografica = RX.Count() == 0 || ((ServiceComponentFieldValuesList)RX.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)RX.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_ID)).v_Value1Name;


            var Lista = ListDiagnosticRepository.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.RX_TORAX_ID || p.v_ComponentId == Sigesoft.Common.Constants.OIT_ID);
            var ListaConcatenada = string.Join(", ", Lista.Select(p => p.v_DiseasesName));

            //string ConclusionesRadiograficaDescripcion = RX.Count() == 0 || ((ServiceComponentFieldValuesList)RX.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)RX.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID)).v_Value1;



            //string ExposicionPolvo = OIT.Count() == 0 || OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_EXPOSICION_POLVO_ID) == null ? string.Empty : OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_EXPOSICION_POLVO_ID).v_Value1Name;
            //string ExposicionPolvoDescripcion = OIT.Count() == 0 || OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_EXPOSICION_POLVO_DESCRIPCION_ID) == null ? string.Empty : OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_EXPOSICION_POLVO_DESCRIPCION_ID).v_Value1;


            //List<ServiceComponentList> ListaExamenesRX = ExamenesServicio.FindAll(p => p.i_CategoryId == 6).ToList();
            //string DiagnosticosRx = "";

            //foreach (var item in ListaExamenesRX)
            //{
            //    foreach (var item1 in ListDiagnosticRepository)
            //    {
            //        if (item1.v_ComponentId == item.v_ComponentId)
            //        {
            //            DiagnosticosRx = item1.v_DiseasesName + ";";
            //        }
            //    }
            //}

            //DiagnosticosRx = DiagnosticosRx == "" ? string.Empty : DiagnosticosRx.Substring(0, DiagnosticosRx.Length - 1);

            // Alejandro
            cells = new List<PdfPCell>();

            var xConcluOIT = string.Empty;

            //if (!string.IsNullOrEmpty(ConclusionesOIT))
            //{              
            //    xConcluOIT = ConclusionesOIT;
            //}

            //if (!string.IsNullOrEmpty(ConclusionesOITDescripcion))
            //{               
            //    xConcluOIT += ", " + ConclusionesOITDescripcion;               
            //}

            //if (!string.IsNullOrEmpty(xConcluOIT))
            //{
            //     cells.Add(new PdfPCell(new Phrase("CONCLUSIONES OIT: " + xConcluOIT, fontColumnValue)));
            //}

            //if (!string.IsNullOrEmpty(ExposicionPolvoDescripcion))
            //{
            //    cells.Add(new PdfPCell(new Phrase("EXPOSICIÓN AL POLVO: " + ExposicionPolvoDescripcion, fontColumnValue)));
            //}

            if (!string.IsNullOrEmpty(ListaConcatenada))
            {
                cells.Add(new PdfPCell(new Phrase(ListaConcatenada, fontColumnValue)));
            }

            //if (string.IsNullOrEmpty(xConcluOIT) 
            //    && string.IsNullOrEmpty(ExposicionPolvoDescripcion)
            //    && string.IsNullOrEmpty(ConclusionesRadiograficaDescripcion))
            //{
            //    //cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
            //    cells.Add(new PdfPCell(new Phrase("ESTE EXAMEN NO APLICA AL PROTOCOLO DE ATENCIÓN.", fontColumnValue)));
            //}



            columnWidths = new float[] { 100f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "VIII. CONCLUSIONES RADIOGRÁFICAS", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Hallazgos Patológicos de Laboratorio

            string ValoresLaboratorio = "";


            foreach (var item1 in ValoresDxLaboratorio.FindAll(p => p.v_ComponentFieldId != "N009-MF000002132"))
            {

                ValoresLaboratorio += item1.v_ComponentFielName + ": " + item1.v_Value1 + " " + item1.v_UnidadMedida + " ;";

            }


            ValoresLaboratorio = ValoresLaboratorio == "" ? string.Empty : ValoresLaboratorio.Substring(0, ValoresLaboratorio.Length - 1);
            cells = new List<PdfPCell>();


            var DxLabo = ListDiagnosticRepository.FindAll(p => p.i_CategoryId == 1);
            string DxLabConcatenados = string.Join(", ", DxLabo.Select(p => p.v_DiseasesName));

            if (Laboratorio.Count() != 0)
            {
                cells = new List<PdfPCell>()
                    {
                        //fila
                        new PdfPCell(new Phrase(ValoresLaboratorio, fontColumnValue)),
                        //fila
                        new PdfPCell(new Phrase(DxLabConcatenados, fontColumnValue)), 
                       
                    };

                columnWidths = new float[] { 100f };
            }
            else
            {
                if (ValoresDxLaboratorio != null)
                {
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));
                }
                else
                {
                    cells.Add(new PdfPCell(new Phrase("ESTE EXAMEN NO APLICA AL PROTOCOLO DE ATENCIÓN.", fontColumnValue)));
                }
                columnWidths = new float[] { 100f };
            }


            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "IX. HALLAZGOS DE LABORATORIO", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Conclusión Audiometría

            // Verificar si el examen esta contenida en el protocolo
            var existeAudio = ExamenesServicio.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIOMETRIA_ID || p.v_ComponentId == "N005-ME000000005");
            cells = new List<PdfPCell>();

            if (existeAudio != null) // El examen esta contemplado en el protocolo del paciente
            {
                //Audiometria
                //string ConclusionesAudiometria = Audiometria.Count() == 0 || ((ServiceComponentFieldValuesList)Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.AUDIOMETRIA_CONCLUSIONES_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.AUDIOMETRIA_CONCLUSIONES_ID)).v_Value1;
                string ConclusionesAudiometria = Audiometria;
                //&& p.v_DiseasesId != Sigesoft.Common.Constants.NORMOACUSIA_OIDO_IZQUIERDO
                var ListaAudioMetriaDx = ListDiagnosticRepository.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIOMETRIA_ID || p.v_ComponentId == "N005-ME000000005");
                string DiagnosticoAudiometria = "";

                foreach (var item in ListaAudioMetriaDx)
                {
                    DiagnosticoAudiometria += item.v_DiseasesName + ";";
                }


                //string ResultadoDxAudiometria = "";

                //if (DiagnosticoAudiometria == "" && existeAudio.i_ServiceComponentStatusId == 3)
                //{
                //    ResultadoDxAudiometria = "SIN ALTERACIÓN";
                //}
                //else if (DiagnosticoAudiometria == "" && existeAudio.i_ServiceComponentStatusId != 3)
                //{
                //    ResultadoDxAudiometria = "NO SE HAN REGISTRADO DATOS";
                //}
                //else if (DiagnosticoAudiometria != "")
                //{
                //    ResultadoDxAudiometria = DiagnosticoAudiometria;
                //}

                //DiagnosticoAudiometria = ResultadoDxAudiometria;// DiagnosticoAudiometria == "" ? string.Empty : DiagnosticoAudiometria.Substring(0, DiagnosticoAudiometria.Length - 1);


                //if (ConclusionesAudiometria != "")
                //{
                cells = new List<PdfPCell>()
                        {
                            //fila
                            new PdfPCell(new Phrase(DiagnosticoAudiometria, fontColumnValue)), 
                        };

                columnWidths = new float[] { 100f };
                //}
                //else
                //{
                //    cells.Add(new PdfPCell(new Phrase("SIN ALTERACIÓN", fontColumnValue)));
                //    columnWidths = new float[] { 100f };
                //}
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO APLICA.", fontColumnValue)));
            }

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "X. CONCLUSIONES AUDIOMETRÍA", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Conclusión Espirometría

            // Verificar si el examen esta contenida en el protocolo
            var existeEspiro = ExamenesServicio.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
            cells = new List<PdfPCell>();

            if (existeEspiro != null) // El examen esta contemplado en el protocolo del paciente
            {
                //ESPIROMETRIA
                //string ResultadoEspirometria = Espirometria.Count() == 0 || ((ServiceComponentFieldValuesList)Espirometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ESPIROMETRIA_RESULTADO_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Espirometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ESPIROMETRIA_RESULTADO_ID)).v_Value1Name;
                //string ObservacionEspirometria = Espirometria.Count() == 0 || ((ServiceComponentFieldValuesList)Espirometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ESPIROMETRIA_OBSERVACION_ID)) == null ? string.Empty : ((ServiceComponentFieldValuesList)Espirometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ESPIROMETRIA_OBSERVACION_ID)).v_Value1;

                var ListaEspirometriaDx = ListDiagnosticRepository.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
                string DiagnosticoEspirometria = "";

                foreach (var item in ListaEspirometriaDx)
                {
                    DiagnosticoEspirometria = item.v_DiseasesName + ";";
                }

                string ResultadoDxEspirometria = "";

                if (DiagnosticoEspirometria == "" && existeEspiro.i_ServiceComponentStatusId == 3)
                {
                    ResultadoDxEspirometria = "SIN ALTERACIÓN";
                }
                else if (DiagnosticoEspirometria == "" && existeEspiro.i_ServiceComponentStatusId != 3)
                {
                    ResultadoDxEspirometria = "NO SE HAN REGISTRADO DATOS";
                }
                else if (DiagnosticoEspirometria != "")
                {
                    ResultadoDxEspirometria = DiagnosticoEspirometria;
                }

                //DiagnosticoEspirometria = DiagnosticoEspirometria == "" ? string.Empty : DiagnosticoEspirometria.Substring(0, DiagnosticoEspirometria.Length - 1);
                //cells = new List<PdfPCell>();

                if (Espirometria.Count() != 0)
                {
                    cells = new List<PdfPCell>()
                        {
                           //fila
                            new PdfPCell(new Phrase(ResultadoDxEspirometria , fontColumnValue)), 
                               //fila
                            //new PdfPCell(new Phrase("Hallazgos: " + DiagnosticoEspirometria, fontColumnValue)), 
                        };

                    columnWidths = new float[] { 100f };
                }
                else
                {
                    cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    columnWidths = new float[] { 100f };
                }
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("ESTE EXAMEN NO APLICA AL PROTOCOLO DE ATENCIÓN.", fontColumnValue)));
            }

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "XI. CONCLUSIONES DE ESPIROMETRÍA", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Otros

            string[] excludeComponents = {   Sigesoft.Common.Constants.PSICOLOGIA_ID,
                                                 Sigesoft.Common.Constants.RX_TORAX_ID,
                                                 Sigesoft.Common.Constants.INFORME_LABORATORIO_ID ,
                                                 Sigesoft.Common.Constants.AUDIOMETRIA_ID,
                                                 Sigesoft.Common.Constants.ESPIROMETRIA_ID,
                                                 Sigesoft.Common.Constants.ANTROPOMETRIA_ID,
                                                 Sigesoft.Common.Constants.FUNCIONES_VITALES_ID,
                                                 Sigesoft.Common.Constants.EXAMEN_FISICO_ID,
                                                 Sigesoft.Common.Constants.OFTALMOLOGIA_ID,
                                                 Sigesoft.Common.Constants.OIT_ID
                                                
                                             };

            int[] excludeCategoryTypeExam = {  (int)Sigesoft.Common.CategoryTypeExam.Laboratorio,
                                                   (int)Sigesoft.Common.CategoryTypeExam.Psicologia,
                                                
                                                };
            //

            var otherExams = ExamenesServicio.FindAll(p => !excludeComponents.Contains(p.v_ComponentId) &&
                                                           !excludeCategoryTypeExam.Contains(p.i_CategoryId.Value));

            // Utilizado Solo para mostrar titulo <OTROS>
            cells = new List<PdfPCell>()
            {

            };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "XII. OTROS", fontTitleTable);
            document.Add(table);

            // Otros Examenes

            foreach (var oe in otherExams)
            {
                table = TableBuilderReportFor312(oe, fontTitleTable, fontSubTitleNegroNegrita, fontColumnValue, subTitleBackGroundColor);

                if (table != null)
                    document.Add(table);
            }

            #endregion

            #region DIAGNÓSTICO MÉDICO OCUPACIONAL

            var DxOcupacionales = ListDiagnosticRepository.FindAll(p => p.i_DiagnosticTypeId == (int)Sigesoft.Common.TipoDx.Enfermedad_Ocupacional || p.i_DiagnosticTypeId == (int)Sigesoft.Common.TipoDx.Accidente_Ocupacional);

            cells = new List<PdfPCell>();

            cells = new List<PdfPCell>()
                  {
                       //fila
                        new PdfPCell(new Phrase(" ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                        new PdfPCell(new Phrase("P", fontColumnValue)), 
                        new PdfPCell(new Phrase("D", fontColumnValue)), 
                        new PdfPCell(new Phrase("R", fontColumnValue)), 
                        new PdfPCell(new Phrase("CIE-10", fontTitleTable)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    
                  };


            if (DxOcupacionales != null && DxOcupacionales.Count > 0)
            {
                columnWidths = new float[] { 5f, 7f };

                foreach (var item in DxOcupacionales)
                {
                    cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_LEFT };
                    cells.Add(cell);


                    if (item.i_FinalQualificationId == (int)Sigesoft.Common.FinalQualification.Presuntivo)
                    {
                        cell = new PdfPCell(new Phrase("X", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase(item.v_Cie10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                    else if (item.i_FinalQualificationId == (int)Sigesoft.Common.FinalQualification.Definitivo)
                    {
                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase("X", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase(item.v_Cie10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }

                    else if (item.i_FinalQualificationId == (int)Sigesoft.Common.FinalQualification.Descartado)
                    {
                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase("X", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase(item.v_Cie10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_LEFT };
                        cells.Add(cell);
                    }
                }
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 50f, 3f, 3f, 3f, 20f };
            }
            columnWidths = new float[] { 50f, 3f, 3f, 3f, 20f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "XIII. DIAGNÓSTICOS MÉDICO OCUPACIONALES", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            document.NewPage();
            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));
            #region Title

            CellLogo = null;
            cells = new List<PdfPCell>();
            cellPhoto1 = null;

            //if (filiationData.b_Photo != null)
            //    cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
            //else
            cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            columnWidths = new float[] { 100f };

            var cellsTit2 = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

            table = HandlingItextSharp.GenerateTableFromCells(cellsTit2, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(table));
            cells.Add(cellPhoto1);

            columnWidths = new float[] { 20f, 60f, 20f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion
            #region Otros Dx

            var DxOtros = ListDiagnosticRepository.FindAll(p => p.i_DiagnosticTypeId != (int)Sigesoft.Common.TipoDx.Accidente_Ocupacional &&
                                                            p.i_DiagnosticTypeId != (int)Sigesoft.Common.TipoDx.Enfermedad_Ocupacional &&
                //p.i_DiagnosticTypeId != null &&
                                                            p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado &&
                                                            p.v_DiseasesId != "N009-DD000000029"
                                                            );

            cells = new List<PdfPCell>()
            {

                new PdfPCell(new Phrase("OTROS DIAGNÓSTICOS", fontTitleTable)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                new PdfPCell(new Phrase("P", fontColumnValue)), 
                new PdfPCell(new Phrase("D", fontColumnValue)), 
                new PdfPCell(new Phrase("R", fontColumnValue)), 
                new PdfPCell(new Phrase("CIE-10", fontTitleTable)) {  HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 

            };

            columnWidths = new float[] { 50f, 3f, 3f, 3f, 20f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);
            document.Add(table);

            cells = new List<PdfPCell>();

            if (DxOtros != null && DxOtros.Count > 0)
            {
                //columnWidths = new float[] { 5f, 7f };
                columnWidths = new float[] { 50f, 3f, 3f, 3f, 20f };

                // Alejandro
                // arreglo de Dx normales
                string[] dxExclude = {  Sigesoft.Common.Constants.NORMOACUSIA_OIDO_DERECHO,
                                        Sigesoft.Common.Constants.NORMOACUSIA_OIDO_IZQUIERDO,
                                        Sigesoft.Common.Constants.EMETROPE,
                                        Sigesoft.Common.Constants.NORMOPESO,
                                        Sigesoft.Common.Constants.NORMOACUSIA,
                                        //Sigesoft.Common.Constants.NORMOACUSIA_UC_AUDIOMETRIA_OIDO_IZQUIERDO,
                                        //Sigesoft.Common.Constants.NORMOACUSIA_UC_AUDIOMETRIA_OIDO_DERECHO
                                     };

                var rowCountDxOtros = DxOtros.Count;

                foreach (var item in DxOtros)
                {
                    var indDxOtros = DxOtros.IndexOf(item) + 1;

                    if (indDxOtros == rowCountDxOtros) // Ultimo registro de la lista pintar una linea debajo
                    {
                        cell = new PdfPCell(new Phrase(indDxOtros + "  " + item.v_DiseasesName, fontColumnValue)) { Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_LEFT };
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(indDxOtros + "  " + item.v_DiseasesName, fontColumnValue)) { Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_LEFT };
                    }

                    cells.Add(cell);

                    // Alejandro
                    // Verificar si es un dx de normalidad
                    string newDx = string.Empty;

                    if (!(dxExclude.Contains(item.v_DiseasesId)))
                    {

                        newDx = item.i_DiagnosticTypeId == (int)Sigesoft.Common.TipoDx.Normal ? "---" : item.v_Cie10;
                    }

                    if (item.i_FinalQualificationId == (int)Sigesoft.Common.FinalQualification.Presuntivo)
                    {
                        cell = new PdfPCell(new Phrase("X", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase(newDx, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                    else if (item.i_FinalQualificationId == (int)Sigesoft.Common.FinalQualification.Definitivo)
                    {
                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase("X", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase(newDx, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);

                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);


                        cell = new PdfPCell(new Phrase(newDx, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                }
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                columnWidths = new float[] { 100f };
            }


            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable, null);
            document.Add(table);

            #endregion



            #region Apto

            string Apto = "", AptoRestricciones = "", NoApto = "", Obs = "";

            var esoType = (Sigesoft.Common.TypeESO)DataService.i_EsoTypeId;

            if (esoType != Sigesoft.Common.TypeESO.Retiro)
            {

                if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.Apto)
                {
                    Apto = "X";
                }
                else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.AptRestriccion)
                {
                    AptoRestricciones = "X";
                }
                else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.NoApto)
                {
                    NoApto = "X";
                }
                else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.AptoObs)
                {
                    Obs = "X";
                }

                cells = new List<PdfPCell>()
                  {
                       //fila
                        new PdfPCell(new Phrase("APTO", fontAptitud)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                        new PdfPCell(new Phrase(Apto, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                        new PdfPCell(new Phrase("APTO CON RESTRICCIONES", fontAptitud)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                        new PdfPCell(new Phrase(AptoRestricciones, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                        new PdfPCell(new Phrase("NO APTO", fontAptitud)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                        new PdfPCell(new Phrase(NoApto, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                         new PdfPCell(new Phrase("OBSERVADO", fontAptitud)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                        new PdfPCell(new Phrase(Obs, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                  };

                columnWidths = new float[] { 20f, 8f, 20f, 8f, 20f, 8f, 20f, 8f };
            }
            else   // es de retiro
            {
                cells = new List<PdfPCell>()
                {
                    //fila
                    new PdfPCell(new Phrase("PACIENTE EVALUADO", fontAptitud)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("X", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                      
                };

                columnWidths = new float[] { 20f, 20f };
            }

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "XIV. APTITUD", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region RECOMENDACIONES

            columnHeaders = null;

            if (ListRecomendation.Count == 0)
            {
                ListRecomendation.Add(new RecomendationList { v_RecommendationName = "No se han registrado datos." });
                columnWidths = new float[] { 100f };
                include = "v_RecommendationName";
            }
            else
            {
                columnWidths = new float[] { 3f, 97f };
                include = "i_Item,v_RecommendationName";
                columnHeaders = new string[] { "RECOMENDACIONES" };
            }

            ListRecomendation = ListRecomendation
                               .GroupBy(x => x.v_RecommendationName)
                               .Select(group => group.First())
                               .ToList();

            var Recomendations = HandlingItextSharp.GenerateTableFromList(ListRecomendation, columnWidths, include, fontColumnValue, "XV. RECOMENDACIONES", fontTitleTable);

            //var habitoNocivo = HandlingItextSharp.GenerateTableFromList(noxiousHabit, columnWidths, include, fontColumnValue, "III. HÁBITOS NOCIVOS", fontTitleTable);

            document.Add(Recomendations);

            #endregion

            #region RESTRICCIONES
            columnHeaders = null;
            var ent = ListDiagnosticRepository.SelectMany(p => p.Restrictions).ToList();
            RestrictionList oRestrictionList = null;
            List<RestrictionList> ListaRestricciones = new List<RestrictionList>();
            int Contador = 1;
            foreach (var item in ent)
            {
                oRestrictionList = new RestrictionList();
                oRestrictionList.i_Item = Contador;
                oRestrictionList.v_RestrictionName = item.v_RestrictionName;
                ListaRestricciones.Add(oRestrictionList);
                Contador++;
            }

            //List<DiagnosticRepositoryList> objData = query.ToList();


            if (ListaRestricciones.Count == 0)
            {
                ListaRestricciones.Add(new RestrictionList { v_RestrictionName = "NINGUNO" });
                columnWidths = new float[] { 100f };
                include = "v_RestrictionName";
            }
            else
            {
                columnWidths = new float[] { 3f, 97f };
                include = "i_Item,v_RestrictionName";
                columnHeaders = new string[] { "RESTRICCIONES" };
            }

            var Restricciones = HandlingItextSharp.GenerateTableFromList(ListaRestricciones, columnWidths, include, fontColumnValue, "XVI. RESTRICCIONES", fontTitleTable);

            //var habitoNocivo = HandlingItextSharp.GenerateTableFromList(noxiousHabit, columnWidths, include, fontColumnValue, "III. HÁBITOS NOCIVOS", fontTitleTable);

            document.Add(Restricciones);
            #endregion

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
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2 });

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
                        Colspan = 2,
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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case "N005-ME000000046":

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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case "N005-ME000000027":

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
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case "N005-ME000000028":

                    #region OFTALMOLOGÍA

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
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case "N005-ME000000005":

                    #region AUDIOMETRÍA

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
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case "N005-ME000000116":

                    #region DERMATOLOGICO

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
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case "N005-ME000000117":

                    #region ALTURA

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
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

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
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                default:
                    break;
            }

            return table;

        }

    }
}
