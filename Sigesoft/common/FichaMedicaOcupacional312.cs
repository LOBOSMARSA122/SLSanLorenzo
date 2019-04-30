
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
using System.Web.Configuration;

    

namespace NetPdf
{
    public class FichaMedicaOcupacional312
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
            //List<ServiceComponentFieldValuesList> Antropometria,
            //List<ServiceComponentFieldValuesList> FuncionesVitales,
            //List<ServiceComponentFieldValuesList> ExamenFisco,
            //List<ServiceComponentFieldValuesList> Oftalmologia,
            //List<ServiceComponentFieldValuesList> Psicologia,
            //List<ServiceComponentFieldValuesList> OIT,
            //List<ServiceComponentFieldValuesList> RX,
            //List<ServiceComponentFieldValuesList> Laboratorio,
            string Audiometria,
            //List<ServiceComponentFieldValuesList> Espirometria,
            List<DiagnosticRepositoryList> ListDiagnosticRepository,
            List<RecomendationList> ListRecomendation,
            List<ServiceComponentList> ExamenesServicio,
            List<ServiceComponentFieldValuesList> ValoresDxLaboratorio,
            organizationDto infoEmpresaPropietaria,
            //List<ServiceComponentFieldValuesList> TestIshihara,
            //List<ServiceComponentFieldValuesList> TestEstereopsis,
            List<ServiceComponentList> serviceComponent,
            string filePDF)
        {

            //Components---------->>>>>>>>>

            List<ServiceComponentFieldsList> Antropometria = ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000002") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000002").ServiceComponentFields;
            List<ServiceComponentFieldsList> FuncionesVitales = ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000001") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000001").ServiceComponentFields;
            List<ServiceComponentFieldsList> ExamenFisco = ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000022") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000022").ServiceComponentFields;
            List<ServiceComponentFieldsList> Oftalmologia = ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000028") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000028").ServiceComponentFields;
            List<ServiceComponentFieldsList> Laboratorio = ExamenesServicio.Find(a => a.v_ComponentId == "N001-ME000000000") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N001-ME000000000").ServiceComponentFields;

            List<ServiceComponentFieldsList> Psicologia = ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000033") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000033").ServiceComponentFields;
            List<ServiceComponentFieldsList> OIT = ExamenesServicio.Find(a => a.v_ComponentId == "N009-ME000000062") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N009-ME000000062").ServiceComponentFields;
            List<ServiceComponentFieldsList> RX = ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000032") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000032").ServiceComponentFields;
            List<ServiceComponentFieldsList> Espirometria = ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000031") == null ? new List<ServiceComponentFieldsList>() : ExamenesServicio.Find(a => a.v_ComponentId == "N002-ME000000031").ServiceComponentFields;


            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //try
            //{NO_BORDER
            // step 2: we create aPA writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
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

            Font fontTitle1 = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL,
                new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL,
                new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL,
                new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));


            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD,
                new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL,
                new BaseColor(System.Drawing.Color.Black));

            //Font fontTitleTableNegro = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion

            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));
            //document.Add(new Paragraph("\r\n"));

            #region Title     

            PdfPCell CellLogo = null;
            cells = new List<PdfPCell>();
            PdfPCell cellPhoto1 = null;

            if (filiationData.b_Photo != null)
                cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F))
                    {HorizontalAlignment = PdfPCell.ALIGN_RIGHT};
            else
                cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_RIGHT};

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F))
                    {HorizontalAlignment = PdfPCell.ALIGN_LEFT};
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_LEFT};
            }

            columnWidths = new float[] {100f};

            var cellsTit = new List<PdfPCell>()
            {

                new PdfPCell(new Phrase("FICHA MÉDICO OCUPACIONAL", fontTitle1))
                    {HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("(ANEXO N° 02 - RM. N° 312-2011/MINSA)", fontSubTitleNegroNegrita))
                    {HorizontalAlignment = PdfPCell.ALIGN_CENTER},
            };

            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null,
                fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(table));
            cells.Add(cellPhoto1);

            columnWidths = new float[] {20f, 60f, 20f};

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null,
                fontTitleTable);
            document.Add(table);

            #endregion


            #region Cabecera del Reporte

            string PreOcupacional = "", Periodica = "", Retiro = "", Otros = "";

            if (DataService != null)
            {

                if (DataService.i_EsoTypeId == (int) Sigesoft.Common.TypeESO.PreOcupacional)
                {
                    PreOcupacional = "X";
                }
                else if (DataService.i_EsoTypeId == (int) Sigesoft.Common.TypeESO.PeriodicoAnual)
                {
                    Periodica = "X";
                }
                else if (DataService.i_EsoTypeId == (int) Sigesoft.Common.TypeESO.Retiro)
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
                    new PdfPCell(new Phrase(DataService.v_ServiceId, fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("FECHA DE ATENCIÓN", fontColumnValue))
                        {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("DÍA", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.i_DiaV.ToString(), fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("MES", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Mes.ToUpper(), fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("AÑO", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(DataService.i_AnioV.ToString(), fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila
                    new PdfPCell(new Phrase("TIPO DE EVALUACIÓN: ", fontColumnValue)),
                    new PdfPCell(new Phrase("PRE EMPLEO", fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(PreOcupacional, fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("ANUAL", fontColumnValue))
                        {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Periodica, fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("RETIRO", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Retiro, fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("OTROS", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(Otros, fontColumnValue))
                        {HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila
                    new PdfPCell(new Phrase("LUGAR DE EXAMEN:", fontColumnValue)),
                    new PdfPCell(new Phrase(DataService.v_OwnerOrganizationName, fontColumnValue)) {Colspan = 9},


                };
                columnWidths = new float[] {15f, 15f, 7f, 5f, 5f, 5f, 5f, 10f, 7f, 5f};

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

                document.Add(filiationWorker);
            







            #endregion

            #region Datos de la Empresa

            //string empresageneral = DataService.v_CustomerOrganizationName;
            //string empresacontrata = DataService.EmpresaEmpleadora;
            //string empresasubcontrata = DataService.EmpresaTrabajo;

            //string empr_Conct = "";
            //if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            //else empr_Conct = empresacontrata;


            String PuestoPostula = "";
            if (DataService.i_EsoTypeId == (int) Sigesoft.Common.TypeESO.PreOcupacional)
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
                    {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("R.U.C. NRO: ", fontColumnValue)),
                new PdfPCell(new Phrase(DataService.RUC, fontColumnValue)),

                //fila
                new PdfPCell(new Phrase("ACTIVIDAD ECONÓMICA:", fontColumnValue)),
                new PdfPCell(new Phrase(DataService.RubroEmpresaTrabajo, fontColumnValue))
                    {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                //fila
                new PdfPCell(new Phrase("LUGAR DE TRABAJO:", fontColumnValue)),
                new PdfPCell(new Phrase(DataService.DireccionEmpresaTrabajo, fontColumnValue))
                    {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                //fila
                new PdfPCell(new Phrase("PUESTO DE TRABAJO:", fontColumnValue)),
                new PdfPCell(new Phrase(DataService.v_CurrentOccupation, fontColumnValue))
                    {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                //fila
                new PdfPCell(new Phrase("PUESTO AL QUE POSTULA (SOLO PRE EMPLEO): ", fontColumnValue))
                    {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase(PuestoPostula, fontColumnValue))
                    {Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            };
            columnWidths = new float[] {15f, 10f, 7f, 5f, 5f, 5f, 5f, 5f, 8f, 9f};

            filiationWorker =
                HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "I. DATOS DE LA EMPRESA",
                    fontTitleTable);

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

            if (DataService.i_ResidenceInWorkplaceId == (int) Sigesoft.Common.SiNo.SI)
            {
                ResidenciaLugarTrabajoSI = "X";
            }
            else
            {
                ResidenciaLugarTrabajoNO = "X";
            }


            string ESSALUD = "", EPS = "", SCTR = "", OTRO = "";

            if (DataService.i_TypeOfInsuranceId == (int) Sigesoft.Common.TypeOfInsurance.ESSALUD)
            {
                ESSALUD = "X";
            }
            else if (DataService.i_TypeOfInsuranceId == (int) Sigesoft.Common.TypeOfInsurance.EPS)
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
                    {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                //new PdfPCell(new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 15F)) )
                //                                                   { Rowspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, 
                //                                                     VerticalAlignment = PdfPCell.ALIGN_MIDDLE 
                //                                                   },
                //new PdfPCell(cellPhoto),
                //fila
                new PdfPCell(new Phrase("FECHA DE NACIMIENTO: ", fontColumnValue)),
                new PdfPCell(new Phrase("DÍA: ", fontColumnValue))
                    {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.i_DiaN.ToString(), fontColumnValue)),
                new PdfPCell(new Phrase("MES: ", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(Mes1.ToUpper(), fontColumnValue))
                    {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("AÑO: ", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.i_AnioN.ToString(), fontColumnValue)),

                //fila
                new PdfPCell(new Phrase("EDAD: ", fontColumnValue)),
                new PdfPCell(new Phrase(DataService.i_Edad.ToString(), fontColumnValue)),
                new PdfPCell(new Phrase("años", fontColumnValue))
                    {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                //fila
                new PdfPCell(new Phrase(
                        "[DOCUMENTO DE IDENTIDAD CARNET DE EXTRANJERÍA (  " + Carnet + "  ), DNI (  " + DNI +
                        "  ), PASAPORTE (  " + Pass + "  )]: ", fontColumnValue))
                    {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase(DataService.v_DocNumber, fontColumnValue))
                    {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                //fila
                new PdfPCell(new Phrase("DIRECCIÓN FISCAL: ", fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.v_AdressLocation, fontColumnValue))
                    {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                //fila
                new PdfPCell(new Phrase("DISTRITO: ", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.DistritoPaciente, fontColumnValue))
                    {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase("PROVINCIA: ", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.ProvinciaPaciente, fontColumnValue)),
                new PdfPCell(new Phrase("DEPARTAMENTO: ", fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.DepartamentoPaciente, fontColumnValue))
                    {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                //fila
                new PdfPCell(new Phrase("RESIDENCIA EN LUGAR DE TRABAJO: ", fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase("SI: ", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(ResidenciaLugarTrabajoSI, fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("NO: ", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(ResidenciaLugarTrabajoNO, fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA EN LUGAR DE TRABAJO: ", fontColumnValue))
                    {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase(DataService.v_ResidenceTimeInWorkplace, fontColumnValue)),
                new PdfPCell(new Phrase("AÑOS ", fontColumnValue)),


                //fila
                new PdfPCell(new Phrase("ESSALUD", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(ESSALUD, fontColumnValue)),
                new PdfPCell(new Phrase("EPS", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(EPS, fontColumnValue)),
                new PdfPCell(new Phrase("OTRO", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase("", fontColumnValue)),
                new PdfPCell(new Phrase("SCTR", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(" ", fontColumnValue)),
                new PdfPCell(new Phrase("OTRO", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(OTRO, fontColumnValue)),

                //fila
                new PdfPCell(new Phrase("CORREO ELECTRÓNICO: ", fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.Email == "" ? "NO REFIERE" : DataService.Email, fontColumnValue))
                    {Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("TELÉFONO: ", fontColumnValue))
                    {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.Telefono, fontColumnValue))
                    {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                //fila
                new PdfPCell(new Phrase("ESTADO CIVIL: ", fontColumnValue))
                    {HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.EstadoCivil, fontColumnValue))
                    {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("GRADO DE INSTRUCCIÓN: ", fontColumnValue))
                    {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(DataService.GradoInstruccion, fontColumnValue))
                    {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                //fila
                new PdfPCell(new Phrase("N° TOTAL DE HIJOS VIVOS: ", fontColumnValue))
                    {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                new PdfPCell(new Phrase(filiationData.i_NumberLivingChildren.ToString(), fontColumnValue))
                    {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("N° FALLECIDOS: ", fontColumnValue))
                    {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase(filiationData.i_NumberDependentChildren.ToString(), fontColumnValue))
                    {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

            };

            columnWidths = new float[] {15f, 13f, 7f, 10f, 14f, 14f, 5f, 5f, 8f, 9f};

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,
                "II. FILIACIÓN DEL TRABAJADOR", fontTitleTable);

            document.Add(filiationWorker);
        }

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
                    cells1.Add(new PdfPCell(new Phrase(item.d_StartDate.Value.ToString("MM/yyyy"), fontColumnValueAntecedentesOcupacionales)));

                    cells1.Add(new PdfPCell(new Phrase("FECHA FIN: ", fontColumnValueAntecedentesOcupacionales)));
                    cells1.Add(new PdfPCell(new Phrase(item.d_EndDate.Value.ToString("MM/yyyy"), fontColumnValueAntecedentesOcupacionales)));

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
                    CirugiasX = "(X) "+ Cirugias[0].v_DiagnosticDetail;
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
                table = HandlingItextSharp.GenerateTableFromList(MadreDx.Count == 0 ? ListaVacia :MadreDx, columnWidths, include, fontColumnValue);
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
                table = HandlingItextSharp.GenerateTableFromList(EspososDx.Count == 0 ? ListaVacia :EspososDx, columnWidths, include, fontColumnValue);
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
            if (DataService != null)
            {
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
            }
            

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

            #region Old
            //Antropometria
            //string Talla = (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID)) == null ? string.Empty : (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID)).v_Value1;
            //string Peso = (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID)) == null ? string.Empty : (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID)).v_Value1;
            //string IMC = (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID)) == null ? string.Empty : (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID)).v_Value1;
            //string ICC = (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID)) == null ? string.Empty : (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID)).v_Value1;
            //string PerimetroCadera = (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID)) == null ? string.Empty : (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID)).v_Value1;
            //string PerimetroAbdominal = (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID)) == null ? string.Empty : (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID)).v_Value1;
            //string PorcentajeGrasaCorporal = (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PORCENTAJE_GRASA_CORPORAL_ID)) == null ? string.Empty : (Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PORCENTAJE_GRASA_CORPORAL_ID)).v_Value1;

            ////Funciones Vitales
            //string FrecResp = (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID)) == null ? string.Empty : (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID)).v_Value1;
            //string frecCard = (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)) == null ? string.Empty : (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)).v_Value1;
            //string PAD = (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID)) == null ? string.Empty : (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID)).v_Value1;
            //string PAS = (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID)) == null ? string.Empty : (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID)).v_Value1;
            //string Temperatura = (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID)) == null ? string.Empty : (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID)).v_Value1;
            //string SaturacionOxigeno = (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID)) == null ? string.Empty : (FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID)).v_Value1;

            #endregion

            //Antropometria
            string Talla = ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID)).v_Value1;
            string Peso = ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID)).v_Value1;
            string IMC = ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID)).v_Value1;
            string ICC = ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID)).v_Value1;
            string PerimetroCadera = ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID)).v_Value1;
            string PerimetroAbdominal = ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID)).v_Value1;
            string PorcentajeGrasaCorporal = ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PORCENTAJE_GRASA_CORPORAL_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)Antropometria.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PORCENTAJE_GRASA_CORPORAL_ID)).v_Value1;

            //Funciones Vitales
            string FrecResp = ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID)).v_Value1;
            string frecCard = ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)).v_Value1;
            string PAD = ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID)).v_Value1;
            string PAS = ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID)).v_Value1;
            string Temperatura = ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID)).v_Value1;
            string SaturacionOxigeno = ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID)) == null ? string.Empty : ((ServiceComponentFieldsList)FuncionesVitales.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID)).v_Value1;

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
            //if ((ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)).v_Value1 != null)
            //{
            string Estoscopia = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)).v_Value1;
            //}


            //     Estoscopia = (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)).v_Value1 == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)).v_Value1;

            string Estado_Mental = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ESTADO_METAL_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ESTADO_METAL_DESCRIPCION_ID)).v_Value1;

            string PielX = "";
            string Piel = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID)).v_Value1;
            string PielHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_ID)).v_Value1;
            if (PielHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) PielX = "X";

            string CabelloX = "";
            string Cabello = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID)).v_Value1;
            string CabelloHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_ID)).v_Value1;
            if (CabelloHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) CabelloX = "X";


            string OidoX = "";
            string Oido = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID)).v_Value1;
            string OidoHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_ID)).v_Value1;
            if (OidoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) OidoX = "X";


            string NarizX = "";
            string Nariz = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID)).v_Value1;
            string NarizHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_ID)).v_Value1;
            if (NarizHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) NarizX = "X";


            string BocaX = "";
            string Boca = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID)).v_Value1;
            string BocaHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_ID)).v_Value1;
            if (BocaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) BocaX = "X";


            string FaringeX = "";
            string Faringe = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID)).v_Value1;
            string FaringeHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_ID)).v_Value1;
            if (FaringeHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) FaringeX = "X";


            string CuelloX = "";
            string Cuello = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID)).v_Value1;
            string CuelloHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_ID)).v_Value1;
            if (CuelloHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) CuelloX = "X";


            string ApaRespiratorioX = "";
            string ApaRespiratorio = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID)).v_Value1;
            string ApaRespiratorioHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATORESPIRATORIO_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATORESPIRATORIO_ID)).v_Value1;
            if (ApaRespiratorioHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaRespiratorioX = "X";


            string ApaCardioVascularX = "";
            string ApaCardioVascular = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID)).v_Value1;
            string ApaCardioVascularHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_ID)).v_Value1;
            if (ApaCardioVascularHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaCardioVascularX = "X";


            string ApaDigestivoX = "";
            string ApaDigestivo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID)).v_Value1;
            string ApaDigestivoHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_ID)).v_Value1;
            if (ApaDigestivoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaDigestivoX = "X";


            string ApaGenitoUrinarioX = "";
            string ApaGenitoUrinario = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID)).v_Value1;
            string ApaGenitoUrinarioHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_GENITOURINARIO_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_GENITOURINARIO_ID)).v_Value1;
            if (ApaGenitoUrinarioHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaGenitoUrinarioX = "X";

            // Alejandro
            // si es mujer el trabajador mostrar sus antecedentes
            if (DataService != null)
            {
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
            }
            

            string ApaLocomotorX = "";
            string ApaLocomotor = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID)).v_Value1;
            string ApaLocomotorHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_ID)).v_Value1;
            if (ApaLocomotorHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaLocomotorX = "X";


            string MarchaX = "";
            string Marcha = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID)).v_Value1;
            string MarchaHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_ID)).v_Value1;
            if (MarchaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) MarchaX = "X";


            string ColumnaX = "";
            string Columna = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID)).v_Value1;
            string ColumnaHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLMNA_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLMNA_ID)).v_Value1;
            if (ColumnaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ColumnaX = "X";


            string SuperioresX = "";
            string Superiores = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID)).v_Value1;
            string SuperioresHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADE_SUPERIORES_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADE_SUPERIORES_ID)).v_Value1;
            if (SuperioresHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SuperioresX = "X";


            string InferioresX = "";
            string Inferiores = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID)).v_Value1;
            string InferioresHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_ID)).v_Value1;
            if (InferioresHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) InferioresX = "X";


            string SistemaLinfaticoX = "";
            string SistemaLinfatico = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID)).v_Value1;
            string SistemaLinfaticoHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_ID)).v_Value1;
            if (SistemaLinfaticoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SistemaLinfaticoX = "X";


            string SistemaNerviosoX = "";
            string SistemaNervioso = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID)).v_Value1;
            string SistemaNerviosoHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_ID)).v_Value1;
            if (SistemaNerviosoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SistemaNerviosoX = "X";



            string Hallazgos = Oftalmologia.Count() == 0 || (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_HALLAZGOS_ID)) == null ? string.Empty : (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_HALLAZGOS_ID)).v_Value1;
            //string AgudezaVisualOjoDerechoSC = Oftalmologia.Count() == 0 || (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_DERECHO)) == null ? string.Empty : (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_DERECHO)).v_Value1;
            //string AgudezaVisualOjoIzquierdoSC = Oftalmologia.Count() == 0 || (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO)) == null ? string.Empty : (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO)).v_Value1;


            ////var ff = Oftalmologia.Find(p => p.v_Value1 == "20 / 30");
            //string AgudezaVisualOjoDerechoCC = Oftalmologia.Count() == 0 || (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO)) == null ? string.Empty : (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO)).v_Value1;
            //string AgudezaVisualOjoIzquierdoCC = Oftalmologia.Count() == 0 || (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_IZQUIERDO)) == null ? string.Empty : (Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_IZQUIERDO)).v_Value1;

            //var ss = Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_NORMAL_ID);
            ////var oTestIshihara = Oftalmologia.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ID);

            ////TEST DE ESTEREOPSIS:Frec. 10 seg/arc, Normal.
            //string TestEstereopsisNormal = TestEstereopsis.Count() == 0 || (TestEstereopsis.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_NORMAL)) == null ? string.Empty : (TestEstereopsis.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_NORMAL)).v_Value1;
            //string TestEstereopsisAnormal = TestEstereopsis.Count() == 0 || (TestEstereopsis.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_ANORMAL)) == null ? string.Empty : (TestEstereopsis.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_ANORMAL)).v_Value1;
            //string TiempoEstereopsis = TestEstereopsis.Count() == 0 || (TestEstereopsis.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_TIEMPO)) == null ? string.Empty : (TestEstereopsis.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_TIEMPO)).v_Value1;

            //string VisonProfundidad = "";
            //if (TestEstereopsisNormal == "1")
            //{
            //    VisonProfundidad = "NORMAL";
            //}
            //else if (TestEstereopsisAnormal == "1")
            //{
            //    VisonProfundidad = "ANORMAL";
            //}

            ////TEST DE ISHIHARA: Anormal, Discromatopsia: No definida.
            //string TestIshiharaNormal = TestIshihara.Count() == 0 || (TestIshihara.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL)) == null ? string.Empty : (TestIshihara.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL)).v_Value1;
            //string TestIshiharaAnormal = TestIshihara.Count() == 0 || (TestIshihara.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL)) == null ? string.Empty : (TestIshihara.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL)).v_Value1;
            //string Dicromatopsia = TestIshihara.Count() == 0 || (TestIshihara.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC)) == null ? string.Empty : (TestIshihara.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC)).v_Value1Name;

            //string VisonColores = "";
            //if (TestIshiharaNormal == "1")
            //{
            //    VisonColores = "NORMAL";
            //}
            //else if (TestIshiharaAnormal == "1")
            //{
                
            //    VisonColores = " ANORMAL" + " DISCROMATOPSIA: " + Dicromatopsia;
            //}


            //string OjoAnexoX = "";
            //string OjoAnexo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID)).v_Value1;
            //string OjoAnexoHallazgo = ExamenFisco.Count() == 0 || (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_ID)) == null ? string.Empty : (ExamenFisco.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_ID)).v_Value1;

            //ServiceComponentList findOftalmologia = ExamenesServicio.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
           
            //string ValorOD_VC_SC = "", ValorOI_VC_SC = "", ValorOD_VC_CC = "", ValorOI_VC_CC = "";
            //string ValorOD_VL_SC = "", ValorOI_VL_SC = "", ValorOD_VL_CC = "", ValorOI_VL_CC = "";

            //if (findOftalmologia != null)
            //{
            //    var OD_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_CERCA_OJO_DERECHO_ID);
            //    if (OD_VC_SC != null)
            //    {
            //        if (OD_VC_SC.v_Value1 != null) ValorOD_VC_SC = OD_VC_SC.v_Value1Name;
            //    }

            //    var OI_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO);
            //    if (OI_VC_SC != null)
            //    {
            //        if (OI_VC_SC.v_Value1 != null) ValorOI_VC_SC = OI_VC_SC.v_Value1Name;
            //    }

            //    var OD_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO);
            //    if (OD_VC_CC != null)
            //    {
            //        if (OD_VC_CC.v_Value1 != null) ValorOD_VC_CC = OD_VC_CC.v_Value1Name;
            //    }

            //    var OI_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID);
            //    if (OI_VC_CC != null)
            //    {
            //        if (OI_VC_CC.v_Value1 != null) ValorOI_VC_CC = OI_VC_CC.v_Value1Name;
            //    }

            //    var OD_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID);
            //    if (OD_VL_SC != null)
            //    {
            //        if (OD_VL_SC.v_Value1 != null) ValorOD_VL_SC = OD_VL_SC.v_Value1Name;
            //    }

            //    var OI_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID);
            //    if (OI_VL_SC != null)
            //    {
            //        if (OI_VL_SC.v_Value1 != null) ValorOI_VL_SC = OI_VL_SC.v_Value1Name;
            //    }

            //    var OD_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID);
            //    if (OD_VL_CC != null)
            //    {
            //        if (OD_VL_CC.v_Value1 != null) ValorOD_VL_CC = OD_VL_CC.v_Value1Name;
            //    }

            //    var OI_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID);
            //    if (OI_VL_CC != null)
            //    {
            //        if (OI_VL_CC.v_Value1 != null) ValorOI_VL_CC = OI_VL_CC.v_Value1Name;
            //    }


            //}
            //if (OjoAnexoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) OjoAnexoX = "X";

            cells = new List<PdfPCell>()
               {
                     //fila
                    new PdfPCell(new Phrase("ANAMNESIS", fontColumnValue)),
                    new PdfPCell(new Phrase(DataService.v_Story == null ?" - - - ":DataService.v_Story, fontColumnValue))
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
            #region OJOS
            ServiceComponentList apendice2Yanacocha = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);
            ServiceComponentList informeOftalmoSimple = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID);
            ServiceComponentList informeOftalmoCompleto = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID);
            ServiceComponentList informeOftalmoHudbay = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID);
            ServiceComponentList findOftalmologia = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);

            string vcscod = "", vcscoi = "", vcccod = "", vcccoi = "", vlscod = "", vlscoi = "", vlccod = "", vlccoi = "", enfermedadesOculares = "", testIshihara = "", reflejosPupilares = "", maculaOD = "", maculaOI = "",
                nervioOpticoOD = "", nervioOpticoOI = "", retinaOD = "", retinaOI = "", presionIntraOcOD = "", presionIntraOcOI = "", flyTest = "";
            string ValorReflejosPupilares = "", ValorEnfermedadesOculares = "", ValorFondoDeOjo = "", ValorTonometria = "";
            #region OFTAL YANACOCHA
            if (apendice2Yanacocha != null)
            {
                #region EXAMENES

                vcscod = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCSCOD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCSCOD).v_Value1Name;
                vcscoi = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCSCOI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCSCOI).v_Value1Name;
                vcccod = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCCCOD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCCCOD).v_Value1Name;
                vcccoi = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCCCOI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VCCCOI).v_Value1Name;

                vlscod = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLSCOD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLSCOD).v_Value1Name;
                vlscoi = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLSCOI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLSCOI).v_Value1Name;
                vlccod = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLCCOD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLCCOD).v_Value1Name;
                vlccoi = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLCCOI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_VLCCOI).v_Value1Name;

                enfermedadesOculares = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_ENFERMEDADES_OCULARES) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_ENFERMEDADES_OCULARES).v_Value1;

                testIshihara = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_TEST_ISHIHARA) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_TEST_ISHIHARA).v_Value1Name;
                reflejosPupilares = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_REFLEJOS_PUPILARES) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_REFLEJOS_PUPILARES).v_Value1Name;

                maculaOD = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_MACULA_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_MACULA_OD).v_Value1Name;
                maculaOI = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_MACULA_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_MACULA_OI).v_Value1Name;
                nervioOpticoOD = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_NERVIO_OPTICO_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_NERVIO_OPTICO_OD).v_Value1Name;
                nervioOpticoOI = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_NERVIO_OPTICO_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_NERVIO_OPTICO_OI).v_Value1Name;
                retinaOD = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_RETINA_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_RETINA_OD).v_Value1Name;
                retinaOI = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_RETINA_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_RETINA_OI).v_Value1Name;

                presionIntraOcOD = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OD).v_Value1;
                presionIntraOcOI = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OI).v_Value1;
                string npresionIntraOcODMed = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OD) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OD).v_MeasurementUnitName;
                string presionIntraOcOIMed = apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OI) == null ? "FALTA LLENAR" : apendice2Yanacocha.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_OFTALMOLOGICA_APENDICE_N_2_YANACOCHA_PRESION_INTRAOCULAR_OI).v_MeasurementUnitName;


                #endregion

                cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("OJOS", fontColumnValue)){Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("ENFERMEDADES OCULARES", fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(enfermedadesOculares, fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea 
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(reflejosPupilares, fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)){Rowspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcscod, fontColumnValue)){Rowspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcscoi, fontColumnValue)){Rowspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcccod, fontColumnValue)){Rowspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcccoi, fontColumnValue)){Rowspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("FONDO DE OJO", fontColumnValue1)){Rowspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("Mácula", fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("OD: " + maculaOD, fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("OI: " + maculaOI, fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

                    new PdfPCell(new Phrase("Nevio Opt", fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("OD: " + nervioOpticoOD, fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("OI: " + nervioOpticoOI, fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

                    new PdfPCell(new Phrase("Retina", fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("OD: " + retinaOD, fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("OI: " + retinaOI, fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

                    //new PdfPCell(new Phrase("Mácula Od: "+ maculaOD + "Mácula Oi: "+ maculaOI + "\n" + "Nervio Opt Od: "+ nervioOpticoOD + "Nervio Opt Oi: "+ nervioOpticoOI + "\n" + "Retina Od: "+ retinaOD + "     Retina Oi: "+ retinaOI, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlscod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlscoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlccod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlccoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("TONOMETRÍA (PIO)", fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("OD: " + presionIntraOcOD + " "+npresionIntraOcODMed, fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("OI: " + presionIntraOcOI + " "+presionIntraOcOIMed, fontColumnValue1)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},  
                    new PdfPCell(new Phrase(testIshihara, fontColumnValue)){Colspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                 };
                columnWidths = new float[] { 15f, 10f, 10f, 10f, 10f, 15F, 8f, 16f, 16f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);
            }
            #endregion
            #region OFT SIMPLE
            else if (informeOftalmoSimple != null)
            {
                vcscod = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOD) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOD).v_Value1Name;
                vcscoi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOI) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOI).v_Value1Name;
                vcccod = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOD) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOD).v_Value1Name;
                vcccoi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOI) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOI).v_Value1Name;

                vlscod = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOD) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOD).v_Value1Name;
                vlscoi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOI) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOI).v_Value1Name;
                vlccod = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOD) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOD).v_Value1Name;
                vlccoi = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOI) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOI).v_Value1Name;

                testIshihara = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VISION_COLORES) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VISION_COLORES).v_Value1Name;
                enfermedadesOculares = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ENFERMEDADES_OCULARES) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ENFERMEDADES_OCULARES).v_Value1;
                reflejosPupilares = informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_REFLEJOS_PUPILARES) == null ? "FALTA LLENAR" : informeOftalmoSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_REFLEJOS_PUPILARES).v_Value1Name;

                cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("OJOS", fontColumnValue)){Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("ENFERMEDADES OCULARES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(enfermedadesOculares, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea 
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(reflejosPupilares, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcscod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcscoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcccod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcccoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlscod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlscoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlccod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlccoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                      //Linea
                    new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},  
                    new PdfPCell(new Phrase(testIshihara, fontColumnValue)){Colspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                 };
                columnWidths = new float[] { 15f, 10f, 10f, 10f, 10f, 30f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);
            }
            #endregion
            #region OFT COMPLETO
            else if (informeOftalmoCompleto != null)
            {
                vlscod = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOD).v_Value1Name;
                vlscoi = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOI).v_Value1Name;
                vlccod = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOD).v_Value1Name;
                vlccoi = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOI).v_Value1Name;

                vcscod = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOD).v_Value1Name;
                vcscoi = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOI).v_Value1Name;
                vcccod = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOD).v_Value1Name;
                vcccoi = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOI).v_Value1Name;

                flyTest = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FLY_TEST) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FLY_TEST).v_Value1;

                var meo = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_MEO) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_MEO).v_Value1;
                var sa = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_SA) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_SA).v_Value1;
                var anexos = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ANEXOS) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ANEXOS).v_Value1;
                var fondoOjo = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FONDO_OJO) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FONDO_OJO).v_Value1;

                var tonometriaOD = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OD) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OD).v_Value1;
                var tonometriaODUnidad = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OD) == null ? "" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OD).v_MeasurementUnitName;
                var tonometriaOI = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OI) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OI).v_Value1;
                var tonometriaOIUnidad = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OI) == null ? "" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TONOMETRIA_OI).v_MeasurementUnitName;

                var refraccion = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFRACCION) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFRACCION).v_Value1;
                reflejosPupilares = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFLEJOS_PUPILARES) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFLEJOS_PUPILARES).v_Value1Name;

                testIshihara = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_ISHIHARA) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_ISHIHARA).v_Value1Name;
                var testWaggoner = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_WAGGONER) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_WAGGONER).v_Value1;
                var testLegrand = informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_LEGRAND) == null ? "FALTA LLENAR" : informeOftalmoCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_LEGRAND).v_Value1;

                cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("OJOS", fontColumnValue)){Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea 
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(reflejosPupilares, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcscod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcscoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcccod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcccoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("VISIÓN DE PROFUNDIDAD", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(flyTest, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlscod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlscoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlccod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlccoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                      //Linea
                    new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},  
                    new PdfPCell(new Phrase(testIshihara, fontColumnValue)){Colspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                 };
                columnWidths = new float[] { 15f, 10f, 10f, 10f, 10f, 30f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);
            }

            #endregion
            #region OFT HUDBAY
            else if (informeOftalmoHudbay != null)
            {
                var examenCliniciExterno = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_EXAM_CLIN_EXTERNO) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_EXAM_CLIN_EXTERNO).v_Value1;
                var correctoresOcularesSi = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CORRECTORES_OCULARES_SI) == null ? "" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CORRECTORES_OCULARES_SI).v_Value1;
                var correctoresOcularesNo = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CORRECTORES_OCULARES_NO) == null ? "" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CORRECTORES_OCULARES_NO).v_Value1;

                vcscod = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VCSCOD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VCSCOD).v_Value1Name;
                vcscoi = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VCSCOI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VCSCOI).v_Value1Name;
                vcccod = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VCCCOD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VCCCOD).v_Value1Name;
                vcccoi = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VCCCOI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VCCCOI).v_Value1Name;

                vlscod = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VLSCOD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VLSCOD).v_Value1Name;
                vlscoi = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VLSCOI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VLSCOI).v_Value1Name;
                vlccod = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VLCCOD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VLCCOD).v_Value1Name;
                vlccoi = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VLCCOI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VLCCOI).v_Value1Name;

                var movOcOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_MOVIMIENTOS_OCULARES_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_MOVIMIENTOS_OCULARES_OD).v_Value1;
                var movOcOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_MOVIMIENTOS_OCULARES_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_MOVIMIENTOS_OCULARES_OI).v_Value1;

                var fonOjoOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_FONDO_OJO_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_FONDO_OJO_OD).v_Value1;
                var fonOjoOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_FONDO_OJO_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_FONDO_OJO_OI).v_Value1;

                var poloAntOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_POLO_ANTERIOR_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_POLO_ANTERIOR_OD).v_Value1;
                var poloAntOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_POLO_ANTERIOR_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_POLO_ANTERIOR_OI).v_Value1;

                var camaraAntOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CAMARA_ANTERIOR_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CAMARA_ANTERIOR_OD).v_Value1;
                var camaraAntOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CAMARA_ANTERIOR_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CAMARA_ANTERIOR_OI).v_Value1;

                var cristalinoOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CRISTALINO_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CRISTALINO_OD).v_Value1;
                var cristalinoOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CRISTALINO_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_CRISTALINO_OI).v_Value1;

                var vitreoOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VITREO_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VITREO_OD).v_Value1;
                var vitreoOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VITREO_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VITREO_OI).v_Value1;

                var nervioOptOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_NERVIO_OPTICO_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_NERVIO_OPTICO_OD).v_Value1Name;
                var nervioOptOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_NERVIO_OPTICO_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_NERVIO_OPTICO_OI).v_Value1Name;

                var vasosRetinalesOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VASOS_RETINALES_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VASOS_RETINALES_OI).v_Value1;
                var vasosRetinalesOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VASOS_RETINALES_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VASOS_RETINALES_OI).v_Value1;

                retinaOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_RETINA_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_RETINA_OD).v_Value1Name;
                retinaOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_RETINA_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_RETINA_OI).v_Value1Name;

                var tonometriaOD = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_TONOMETRIA_OD) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_TONOMETRIA_OD).v_Value1;
                var tonometriaOI = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_TONOMETRIA_OI) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_TONOMETRIA_OI).v_Value1;

                testIshihara = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_TEST_ISHIHARA) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_TEST_ISHIHARA).v_Value1Name;
                var visionEstereoscopica = informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VISION_ESTEREOSCOPICA) == null ? "FALTA LLENAR" : informeOftalmoHudbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_VISION_ESTEREOSCOPICA).v_Value1;

                cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("OJOS", fontColumnValue)){Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea 
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(reflejosPupilares, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcscod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcscoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcccod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vcccoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlscod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlscoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlccod, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(vlccoi, fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                      //Linea
                    new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},  
                    new PdfPCell(new Phrase(testIshihara, fontColumnValue)){Colspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                 };
                columnWidths = new float[] { 15f, 10f, 10f, 10f, 10f, 30f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);
            }
            #endregion
            #region AGUDEZA VISUAL
            else if (findOftalmologia != null)
            {
                var TestIshiharaa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
                var Oftalmo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);


                var DxCateodiaOftalmologia = ListDiagnosticRepository.FindAll(p => p.i_CategoryId == 14);
                string ValorDxOftalmologia = "";
                string ValorOD_VC_SC = "", ValorOI_VC_SC = "", ValorOD_VC_CC = "", ValorOI_VC_CC = "";
                string ValorOD_VL_SC = "", ValorOI_VL_SC = "", ValorOD_VL_CC = "", ValorOI_VL_CC = "";
                string ValorDiscromatopsia = "";
                if (findOftalmologia != null)
                {


                    var OD_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_CERCA_OJO_DERECHO_ID);
                    if (OD_VC_SC != null)
                    {
                        if (OD_VC_SC.v_Value1Name != null) ValorOD_VC_SC = OD_VC_SC.v_Value1Name;
                    }

                    var OI_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO);
                    if (OI_VC_SC != null)
                    {
                        if (OI_VC_SC.v_Value1Name != null) ValorOI_VC_SC = OI_VC_SC.v_Value1Name;
                    }

                    var OD_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO);
                    if (OD_VC_CC != null)
                    {
                        if (OD_VC_CC.v_Value1Name != null) ValorOD_VC_CC = OD_VC_CC.v_Value1Name;
                    }

                    var OI_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID);
                    if (OI_VC_CC != null)
                    {
                        if (OI_VC_CC.v_Value1Name != null) ValorOI_VC_CC = OI_VC_CC.v_Value1Name;
                    }

                    var OD_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID);
                    if (OD_VL_SC != null)
                    {
                        if (OD_VL_SC.v_Value1Name != null) ValorOD_VL_SC = OD_VL_SC.v_Value1Name;
                    }

                    var OI_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID);
                    if (OI_VL_SC != null)
                    {
                        if (OI_VL_SC.v_Value1Name != null) ValorOI_VL_SC = OI_VL_SC.v_Value1Name;
                    }

                    var OD_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID);
                    if (OD_VL_CC != null)
                    {
                        if (OD_VL_CC.v_Value1Name != null) ValorOD_VL_CC = OD_VL_CC.v_Value1Name;
                    }

                    var OI_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID);
                    if (OI_VL_CC != null)
                    {
                        if (OI_VL_CC.v_Value1Name != null) ValorOI_VL_CC = OI_VL_CC.v_Value1Name;
                    }

                    if (DxCateodiaOftalmologia != null)
                    {
                        ValorDxOftalmologia = string.Join(", ", DxCateodiaOftalmologia.Select(p => p.v_DiseasesName));

                    }

                    //if (findOftalmologia.DiagnosticRepository != null)
                    //{
                    //    ValorDxOftalmologia = string.Join(", ", findOftalmologia.DiagnosticRepository.Select(p => p.v_DiseasesName));

                    //}

                    //var Discromatopsia = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DICROMATOPSIA_ID);
                    //var NormalAnormal = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL);

                    //if (Discromatopsia != null || NormalAnormal != null)
                    //{
                    //    var a = NormalAnormal.v_Value1.ToString() == "1" ? "NORMAL" : "ANORMAL";
                    //    var b = Discromatopsia.v_Value1Name;

                    //    ValorDiscromatopsia = a + " / " + b;
                    //}

                    //TEST DE ISHIHARA: Anormal, Discromatopsia: No definida.


                    if (TestIshiharaa != null)
                    {
                        string TestIshiharaNormal = TestIshiharaa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL) == null ? "" : TestIshiharaa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL).v_Value1;// TestIshihara.Count() == 0 || (TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL)) == null ? string.Empty : (TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL)).v_Value1;
                        string TestIshiharaAnormal = TestIshiharaa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL) == null ? "" : TestIshiharaa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL).v_Value1;// TestIshihara.Count() == 0 || (TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL)) == null ? string.Empty : (TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL)).v_Value1;
                        string Dicromatopsia = TestIshiharaa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPIMETRIA_OD) == null ? "" : TestIshiharaa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPIMETRIA_OD).v_Value1Name;// TestIshihara.Count() == 0 || (TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC)) == null ? string.Empty : (TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC)).v_Value1Name;


                        if (TestIshiharaNormal == "1")
                        {
                            ValorDiscromatopsia = "NORMAL" + " DISCRIMINACIÓN: " + Dicromatopsia;
                        }
                        else if (TestIshiharaAnormal == "1")
                        {

                            ValorDiscromatopsia = " ANORMAL" + " DISCRIMINACIÓN: " + Dicromatopsia;
                        }
                    }
                    else
                    {
                        ValorDiscromatopsia = "NO APLICA";
                    }

                    if (Oftalmo != null)
                    {
                        ValorFondoDeOjo =
                                Oftalmo.ServiceComponentFields.Find(
                                    p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPIMETRIA_OI) == null
                                    ? ""
                                    : Oftalmo.ServiceComponentFields.Find(
                                        p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPIMETRIA_OI).v_Value1;

                        string od =
                                 Oftalmo.ServiceComponentFields.Find(
                                     p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TONOMETRIA_O_D) == null
                                     ? ""
                                     : Oftalmo.ServiceComponentFields.Find(
                                         p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TONOMETRIA_O_D).v_Value1;

                        string oi =
                                  Oftalmo.ServiceComponentFields.Find(
                                      p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TONOMETRIA_O_I) == null
                                      ? ""
                                      : Oftalmo.ServiceComponentFields.Find(
                                          p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TONOMETRIA_O_I).v_Value1;
                        ValorTonometria = "OD: " + od + " / OI: " + oi + " (mmHg)";
                    }


                    else
                    {
                        ValorFondoDeOjo = "NO APLICA";
                        ValorTonometria = "---";
                    }



                }

                cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("OJOS", fontColumnValue)){Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("ENFERMEDADES OCULARES", fontColumnValue)),
                    new PdfPCell(new Phrase(ValorEnfermedadesOculares, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    //Linea 
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(ValorReflejosPupilares, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(ValorOD_VC_SC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VC_SC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOD_VC_CC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VC_CC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("FONDO DE OJO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(ValorFondoDeOjo, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(ValorOD_VL_SC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VL_SC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOD_VL_CC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VL_CC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("TONOMETRÍA (PIO)", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(ValorTonometria, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                      //Linea
                    new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(new Phrase(ValorDiscromatopsia, fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                 };
                columnWidths = new float[] { 15f, 10f, 10f, 10f, 10f, 30f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);


            }
            #endregion
            else
            {
                cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("OJOS", fontColumnValue)){Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("ENFERMEDADES OCULARES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea 
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("FONDO DE OJO", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("TONOMETRÍA (PIO)", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                      //Linea
                    new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValueBold)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},  
                    new PdfPCell(new Phrase("N/A", fontColumnValue)){Colspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                 };
                columnWidths = new float[] { 15f, 10f, 10f, 10f, 10f, 30f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);
            }

            #endregion
            #region Evaluación Psicológicas

            //PSICOLOGIA
            //string AreaCognitiva = Psicologia.Count() == 0 || (Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_COGNITIVA_ID)) == null ? string.Empty : (Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_COGNITIVA_ID)).v_Value1Name;
            //string AreaEmocional = Psicologia.Count() == 0 || (Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_EMOCIONAL_ID)) == null ? string.Empty : (Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_EMOCIONAL_ID)).v_Value1Name;
            string PsicologiaConclusiones = Psicologia.Count() == 0 || (Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID)) == null ? string.Empty : (Psicologia.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID)).v_Value1;

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
            //string ConclusionesOITDescripcion = OIT.Count() == 0 || (OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID)) == null ? string.Empty : (OIT.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID)).v_Value1;
            
            //string ConclusionesRadiografica = RX.Count() == 0 || (RX.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_ID)) == null ? string.Empty : (RX.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_ID)).v_Value1Name;


            var Lista = ListDiagnosticRepository.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.RX_TORAX_ID || p.v_ComponentId == Sigesoft.Common.Constants.OIT_ID);
            var ListaConcatenada = string.Join(", ", Lista.Select(p => p.v_DiseasesName));

            //string ConclusionesRadiograficaDescripcion = RX.Count() == 0 || (RX.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID)) == null ? string.Empty : (RX.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID)).v_Value1;
           
            
            
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

        
                foreach (var item1 in ValoresDxLaboratorio.FindAll(p => p.v_ComponentFieldId !="N009-MF000002132"))
                {
                    
                        ValoresLaboratorio += item1.v_ComponentFielName + ": " + item1.v_Value1 + " " + item1.v_UnidadMedida+ " ;";
                   
                }
          

            ValoresLaboratorio = ValoresLaboratorio == "" ? string.Empty : ValoresLaboratorio.Substring(0, ValoresLaboratorio.Length - 1);
            cells = new List<PdfPCell>();

        
            var DxLabo= ListDiagnosticRepository.FindAll(p => p.i_CategoryId == 1);
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
                //string ConclusionesAudiometria = Audiometria.Count() == 0 || (Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.AUDIOMETRIA_CONCLUSIONES_ID)) == null ? string.Empty : (Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.AUDIOMETRIA_CONCLUSIONES_ID)).v_Value1;
                string ConclusionesAudiometria = Audiometria;
                //&& p.v_DiseasesId != Sigesoft.Common.Constants.NORMOACUSIA_OIDO_IZQUIERDO
                var ListaAudioMetriaDx = ListDiagnosticRepository.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIOMETRIA_ID || p.v_ComponentId == "N005-ME000000005");
                string DiagnosticoAudiometria = "";

                foreach (var item in ListaAudioMetriaDx)
                {
                    DiagnosticoAudiometria +=  item.v_DiseasesName + ";";
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
                //string ResultadoEspirometria = Espirometria.Count() == 0 || (Espirometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ESPIROMETRIA_RESULTADO_ID)) == null ? string.Empty : (Espirometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ESPIROMETRIA_RESULTADO_ID)).v_Value1Name;
                //string ObservacionEspirometria = Espirometria.Count() == 0 || (Espirometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ESPIROMETRIA_OBSERVACION_ID)) == null ? string.Empty : (Espirometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.ESPIROMETRIA_OBSERVACION_ID)).v_Value1;

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
                                                 Sigesoft.Common.Constants.OIT_ID,
                                                 "N005-ME000000117",
                                                 "N005-ME000000116",
                                                 "N005-ME000000046"

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

                        newDx = item.i_DiagnosticTypeId== (int)Sigesoft.Common.TipoDx.Normal ? "---" : item.v_Cie10;
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


            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths,"", fontTitleTable,null);
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

                columnWidths = new float[] { 20f, 20f};
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
                columnWidths = new float[] { 3f,97f };
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
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2});

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
