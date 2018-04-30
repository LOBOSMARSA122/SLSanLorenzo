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
    public class ConsultaMedica
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateConsultaMedica(PacientList filiationData, organizationDto infoEmpresaPropietaria, List<PersonMedicalHistoryList> personMedicalHistory,List<FamilyMedicalAntecedentsList> familyMedicalAntecedent, List<NoxiousHabitsList> noxiousHabit,List<ServiceComponentList> serviceComponent,List<DiagnosticRepositoryList> diagnosticRepository,string filePDF)
        {
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
            PdfPTable tableTitulo = null;
            PdfPTable tableNroHoja = null;
            PdfPCell cell = null;

            #endregion

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion

            #region Triaje
            var findFuncionesVitales = serviceComponent.Find(p => p.v_ComponentId == "N002-ME000000001");
            var findAntropometria = serviceComponent.Find(p => p.v_ComponentId == "N002-ME000000002");
            string Temperatura = "", PA = "", FC = "", FR = "", Peso = "", Talla = "", IMC="";
            if (findFuncionesVitales != null)
            {
                Temperatura = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-MF000000004").v_Value1;
                PA = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-MF000000001").v_Value1  +" " + findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-MF000000002").v_Value1;
                FC = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-MF000000003").v_Value1;
                FR = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-MF000000005").v_Value1;
                Peso = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-MF000000008").v_Value1;
                Talla = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-MF000000007").v_Value1;
                IMC = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-MF000000009").v_Value1;

            }
            #endregion

            #region Primera Hoja
            //PROBLEMAS CRÓNICOS
            var findProblemasCronico = serviceComponent.Find(p => p.v_ComponentId == "N009-ME000000400");
            string ProblemasCronicosDescripcion = "", ProblemasCronicosFecha = "", ProblemasCronicosInactivo = "", ProblemasCronicosObs="";
            string ProblemasAgudosDescripcion = "", ProblemasAgudosFecha = "", ProblemasAgudosInactivo = "", ProblemasAgudosObs = "";
            if (findProblemasCronico != null)
            {
                ProblemasCronicosDescripcion = findProblemasCronico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002824").v_Value1;
                ProblemasCronicosFecha = findProblemasCronico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002825").v_Value1;
                ProblemasCronicosInactivo = findProblemasCronico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002826").v_Value1;
                ProblemasCronicosObs = findProblemasCronico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002827").v_Value1;

                ProblemasAgudosDescripcion = findProblemasCronico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002828").v_Value1;
                ProblemasAgudosFecha = findProblemasCronico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002829").v_Value1;
                ProblemasAgudosInactivo = findProblemasCronico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002830").v_Value1;
                ProblemasAgudosObs = findProblemasCronico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002831").v_Value1;
            }

            //PLAN DE ATENCIÓN INTEGRAL
            var findAtencionIntegral = serviceComponent.Find(p => p.v_ComponentId == "N009-ME000000401");
            string EvGeneralCrecDesaDescrip = "", EvGeneralCrecDesaFecha1 = "", EvGeneralCrecDesaFecha2 = "", EvGeneralCrecDesaLugar = "";
            string InmunizacionesDescrip = "", InmunizacionesFecha1 = "", InmunizacionesFecha2 = "", InmunizacionesLugar = "";
            string EvBucalDescrip = "", EvBucalFecha1 = "", EvBucalFecha2 = "", EvBucalLugar = "";
            string AdmMicronutriDescrip = "", AdmMicronutriFecha1 = "", AdmMicronutriFecha2 = "", AdmMicronutriLugar = "";
            string ConsejeInteDescrip = "", ConsejeInteFecha1 = "", ConsejeInteFecha2 = "", ConsejeInteLugar = "";
            string VisitaDomiDescrip = "", VisitaDomiFecha1 = "", VisitaDomiFecha2 = "", VisitaDomiLugar = "";
            string TemasEduDescrip = "", TemasEduFecha1 = "", TemasEduFecha2 = "", TemasEduLugar = "";
            if (findAtencionIntegral != null)
            {
                EvGeneralCrecDesaDescrip = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002832").v_Value1;
                EvGeneralCrecDesaFecha1 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002833").v_Value1;
                EvGeneralCrecDesaFecha2 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002834").v_Value1;
                EvGeneralCrecDesaLugar = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002835").v_Value1;

                InmunizacionesDescrip = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002836").v_Value1;
                InmunizacionesFecha1 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002837").v_Value1;
                InmunizacionesFecha2 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002838").v_Value1;
                InmunizacionesLugar = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002839").v_Value1;

                EvBucalDescrip = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002840").v_Value1;
                EvBucalFecha1 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002841").v_Value1;
                EvBucalFecha2 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002842").v_Value1;
                EvBucalLugar = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002843").v_Value1;

                AdmMicronutriDescrip = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002844").v_Value1;
                AdmMicronutriFecha1 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002845").v_Value1;
                AdmMicronutriFecha2 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002846").v_Value1;
                AdmMicronutriLugar = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002847").v_Value1;

                ConsejeInteDescrip = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002848").v_Value1;
                ConsejeInteFecha1 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002849").v_Value1;
                ConsejeInteFecha2 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002850").v_Value1;
                ConsejeInteLugar = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002851").v_Value1;

                VisitaDomiDescrip = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002852").v_Value1;
                VisitaDomiFecha1 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002853").v_Value1;
                VisitaDomiFecha2 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002854").v_Value1;
                VisitaDomiLugar = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002855").v_Value1;

                TemasEduDescrip = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002856").v_Value1;
                TemasEduFecha1 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002857").v_Value1;
                TemasEduFecha2 = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002858").v_Value1;
                TemasEduLugar = findAtencionIntegral.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002859").v_Value1;
            }


            #region Title

            PdfPCell CellLogo = null;
            cells = new List<PdfPCell>();

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

                new PdfPCell(new Phrase("FORMATO DE ATENCIÓN", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INTEGRAL DEL (NIÑO/ ADOLESCENTE /ADULTO /ADULTOMAYOR)", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

            tableTitulo = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            var cellsTitNro = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("1", fontTitle1)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_CENTER }
               
            };

            tableNroHoja = HandlingItextSharp.GenerateTableFromCells(cellsTitNro, columnWidths, null, fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(tableTitulo));
            cells.Add(new PdfPCell(tableNroHoja));

            columnWidths = new float[] { 20f, 75f, 5f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion

            document.Add(new Paragraph("\r\n"));
            document.Add(new Paragraph("\r\n"));
            cells = new List<PdfPCell>();
            cells.Add(new PdfPCell(new Phrase("Problemas Crónicos", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Fecha", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Inactivo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Observaciones", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase(ProblemasCronicosDescripcion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ProblemasCronicosFecha, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ProblemasCronicosInactivo, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ProblemasCronicosObs, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 25f, 25f, 25f, 25f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
            document.Add(table);


            document.Add(new Paragraph("\r\n"));
            document.Add(new Paragraph("\r\n"));
            cells = new List<PdfPCell>();
            cells.Add(new PdfPCell(new Phrase("Problemas Agudos", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Fecha", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Inactivo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Observaciones", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase(ProblemasAgudosDescripcion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ProblemasAgudosFecha, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ProblemasAgudosInactivo, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ProblemasAgudosObs, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


            columnWidths = new float[] { 25f, 25f, 25f, 25f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
            document.Add(table);


            document.Add(new Paragraph("\r\n"));
            document.Add(new Paragraph("\r\n"));
            cells = new List<PdfPCell>();
            cells.Add(new PdfPCell(new Phrase("PLAN ATENCIÓN INTEGRAL", fontColumnValue)) {Colspan=6, HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase("Item", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Evaluación", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Descripción", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Fecha", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Fecha", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Lugar", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            //1
            cells.Add(new PdfPCell(new Phrase("1", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Evaluación General, Crecimiento y Desarrollo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvGeneralCrecDesaDescrip, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvGeneralCrecDesaFecha1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvGeneralCrecDesaFecha2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvGeneralCrecDesaLugar, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 
            //2
            cells.Add(new PdfPCell(new Phrase("2", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Inmunizaciones", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvBucalDescrip, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvBucalFecha1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvBucalFecha2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvBucalLugar, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //3
            cells.Add(new PdfPCell(new Phrase("3", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Evaluación Bucal", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvBucalDescrip, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvBucalFecha1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvBucalFecha2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(EvBucalLugar, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //4
            cells.Add(new PdfPCell(new Phrase("4", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Administración de Micronutrientes", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(AdmMicronutriDescrip, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(AdmMicronutriFecha1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(AdmMicronutriFecha2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(AdmMicronutriLugar, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //5
            cells.Add(new PdfPCell(new Phrase("5", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Visita Domiciliaria", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ConsejeInteDescrip, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ConsejeInteFecha1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ConsejeInteFecha2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(ConsejeInteLugar, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //6
            cells.Add(new PdfPCell(new Phrase("6", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Temas Educativos", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(VisitaDomiDescrip, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(VisitaDomiFecha1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(VisitaDomiFecha2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(VisitaDomiLugar, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //7
            cells.Add(new PdfPCell(new Phrase("6", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Temas Educativos", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(TemasEduDescrip, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(TemasEduFecha1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(TemasEduFecha2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(TemasEduLugar, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            columnWidths = new float[] { 5f,25f, 25f, 15f, 15f, 15f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
            document.Add(table);


            #endregion

            document.NewPage();

            #region Segunda Hoja

            #region Title

            PdfPCell CellLogo2 = null;
            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo2 = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo2 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            columnWidths = new float[] { 100f };

            var cellsTit2 = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("FORMATO DE ATENCIÓN", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INTEGRAL DEL (NIÑO/ ADOLESCENTE /ADULTO /ADULTOMAYOR)", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

            tableTitulo = HandlingItextSharp.GenerateTableFromCells(cellsTit2, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            var cellsTitNro2 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("2", fontTitle1)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_CENTER }
               
            };

            tableNroHoja = HandlingItextSharp.GenerateTableFromCells(cellsTitNro2, columnWidths, null, fontTitleTable);

            cells.Add(CellLogo2);
            cells.Add(new PdfPCell(tableTitulo));
            cells.Add(new PdfPCell(tableNroHoja));

            columnWidths = new float[] { 20f, 75f, 5f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion


            #region Datos        
   
            string Dia = "", Mes = "", Anio = "", Hora="", NroServicio="", Masc="", Feme="";

            Dia = filiationData.d_ServiceDate.Value.Day.ToString();
            Mes = filiationData.d_ServiceDate.Value.Month.ToString();
            Anio = filiationData.d_ServiceDate.Value.Year.ToString();

            Hora = filiationData.d_ServiceDate.Value.Hour.ToString();
            NroServicio = serviceComponent[0].v_ServiceId;
            if (filiationData.v_SexTypeName.ToLower() == "masculino")
            {
                Masc = "X";
            }
            else
            {
                Feme = "X";
            }


            document.Add(new Paragraph("\r\n"));
            document.Add(new Paragraph("\r\n"));
            cells = new List<PdfPCell>();
            cells.Add(new PdfPCell(new Phrase("Fecha", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Día", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });        
            cells.Add(new PdfPCell(new Phrase("Mes", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });           
            cells.Add(new PdfPCell(new Phrase("Año", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.NO_BORDER });
            cells.Add(new PdfPCell(new Phrase("Hora", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(Hora, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.NO_BORDER });
            cells.Add(new PdfPCell(new Phrase("N°", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(NroServicio, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.NO_BORDER });
            cells.Add(new PdfPCell(new Phrase(Dia, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(Mes, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(Anio, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) {  Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.NO_BORDER});           

            columnWidths = new float[] { 12.5f, 10f, 10f, 10f, 5f, 10f, 10f, 5f, 5f, 20f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
            document.Add(table);

            document.Add(new Paragraph("\r\n"));

            cells = new List<PdfPCell>();
            cells.Add(new PdfPCell(new Phrase("Datos Generales", fontColumnValue)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase("Apellidos", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Nombres", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Sexo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("M " + Masc, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("F "+ Feme, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Edad", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(filiationData.Edad.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase(filiationData.v_FirstLastName + " " +filiationData.v_SecondLastName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(filiationData.v_FirstName, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("F.Nac", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(filiationData.d_Birthdate.ToString(), fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase("Lugar de Nacimiento", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Domicilio/Referencia", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Grupo Sanguineo", fontColumnValue)) { Colspan=2, Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(filiationData.v_BloodGroupName, fontColumnValue)) { Colspan = 2, Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Rh", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase(filiationData.v_AdressLocation, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Preguntar", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });           
            cells.Add(new PdfPCell(new Phrase("--", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase("G. de Instrucción", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Centro Educativo", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Teléfono", fontColumnValue)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase(filiationData.GradoInstruccion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(filiationData.v_TelephoneNumber, fontColumnValue)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase("Estado Civil", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Ocupación", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase(filiationData.v_MaritalStatus, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase(filiationData.v_CurrentOccupation, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase("Madre o Padre, acompañante o cuidador", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Edad", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Identificación DNI", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("Afiliación SIS u otro Seguro", fontColumnValue)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER });

            cells.Add(new PdfPCell(new Phrase(filiationData.v_RelationshipName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("-", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("-", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER });

            columnWidths = new float[] { 30f, 15f, 15f, 10f, 5f, 5f, 10f, 10f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
            document.Add(table);

            #endregion

            #region Antecedentes Personales


            if (personMedicalHistory.Count == 0)
            {
                personMedicalHistory.Add(new PersonMedicalHistoryList { v_DiseasesName = "NO REFIERE ANTECEDENTES PERSONALES." });
                columnWidths = new float[] { 100f };
                include = "v_DiseasesName";
            }
            else
            {
                columnWidths = new float[] { 3f, 32f, 65f };
                include = "i_Item,v_DiseasesName,v_DiagnosticDetail";
            }

            var antMedicoPer = HandlingItextSharp.GenerateTableFromList(personMedicalHistory, columnWidths, include, fontColumnValue, "ANTECEDENTES PERSONALES", fontTitleTableNegro);

            document.Add(antMedicoPer);

            #endregion

            #region Antecedentes Familiares

            if (familyMedicalAntecedent.Count == 0)
            {
                familyMedicalAntecedent.Add(new FamilyMedicalAntecedentsList { v_FullAntecedentName = "NO REFIERE ANTECEDENTES FAMILIARES." });
                columnWidths = new float[] { 100f };
                include = "v_FullAntecedentName";
            }
            else
            {
                columnWidths = new float[] { 3f, 32f, 65f };
                include = "i_Item,v_FullAntecedentName,v_Comment";
            }

            var antMedicoFam = HandlingItextSharp.GenerateTableFromList(familyMedicalAntecedent, columnWidths, include, fontColumnValue, "ANTECEDENTES FAMILIARES", fontTitleTableNegro);

            document.Add(antMedicoFam);


            #endregion

            #region Habitos Nocivos

            if (noxiousHabit.Count == 0)
            {
                noxiousHabit.Add(new NoxiousHabitsList { v_NoxiousHabitsName = "No Aplica Hábitos Nocivos a la Atención." });
                columnWidths = new float[] { 100f };
                include = "v_NoxiousHabitsName";
            }
            else
            {
                columnWidths = new float[] { 3f, 32f, 65f };
                include = "i_Item,v_NoxiousHabitsName,v_Frequency";
            }

            var habitoNocivo = HandlingItextSharp.GenerateTableFromList(noxiousHabit, columnWidths, include, fontColumnValue, "III. HÁBITOS NOCIVOS", fontTitleTableNegro);

            document.Add(habitoNocivo);

            #endregion
            #endregion

           

            var findFormatoNinio = serviceComponent.Find(p => p.v_ComponentId == "N009-ME000000406");
            if (findFormatoNinio != null)
            {
                document.NewPage();
                #region Niño

                #region Title

                PdfPCell CellLogo3 = null;
                cells = new List<PdfPCell>();

                if (infoEmpresaPropietaria.b_Image != null)
                {
                    CellLogo3 = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                }
                else
                {
                    CellLogo3 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                }

                columnWidths = new float[] { 100f };

                var cellstit3 = new List<PdfPCell>()
                { 

                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN", fontTitle1))
                                    { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase("INTEGRAL DEL (NIÑO)", fontSubTitleNegroNegrita)) 
                                    { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                };

                tableTitulo = HandlingItextSharp.GenerateTableFromCells(cellstit3, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                var cellstitNro3 = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("3", fontTitle1)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_CENTER }
               
                };

                tableNroHoja = HandlingItextSharp.GenerateTableFromCells(cellstitNro3, columnWidths, null, fontTitleTable);

                cells.Add(CellLogo3);
                cells.Add(new PdfPCell(tableTitulo));
                cells.Add(new PdfPCell(tableNroHoja));

                columnWidths = new float[] { 20f, 75f, 5f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                document.Add(table);

                #endregion

                #region Examenes

                string NoQuiereMamar = "", NoPuedeBeber = "", Emaciacion = "", Convulsiones2Meses = "", Convulsiones4Años = "", PielLenta = "", Fontanela = "", Letargico = "",
                        Traumatismo = "", Enrojecimiento = "", Vomito = "", Envenenamiento = "", Fiebre = "", Estridor = "", Palidez = "", Rigidez = "", Pustulas = "", Comatoso = "";

                string RespRapida = "", ObsTriaje = "", ObsEscuch = "", EscucharSibilancias = "", ObsTriajeGrave = "", Sibilancias1 = "", Sibilancias2 = "", SangreHeces = "", NinioLetargico = "", IntranquiloIrri = "",
                    OjosHundidos = "", OfreceLiq = "", BebeAvida = "", PlieguePiel = "", SignoPliegueCuta = "";

                NoQuiereMamar = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003060").v_Value1 == "1" ? "X":"";
                NoPuedeBeber = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003068").v_Value1 == "1" ? "X":"";
                Emaciacion = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003073").v_Value1 == "1" ? "X":"";
                Convulsiones2Meses = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003061").v_Value1 == "1" ? "X":"";
                Convulsiones4Años = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003069").v_Value1 == "1" ? "X":"";
                PielLenta = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003074").v_Value1 == "1" ? "X":"";
                Fontanela = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003062").v_Value1 == "1" ? "X":"";
                Letargico = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003070").v_Value1 == "1" ? "X":"";
                Traumatismo = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003075").v_Value1 == "1" ? "X":"";
                Enrojecimiento = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003063").v_Value1 == "1" ? "X":"";
                Vomito = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003071").v_Value1 == "1" ? "X":"";
                Envenenamiento = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003076").v_Value1 == "1" ? "X":"";
                Fiebre = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003064").v_Value1 == "1" ? "X":"";
                Estridor = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003072").v_Value1 == "1" ? "X":"";
                Palidez = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003077").v_Value1 == "1" ? "X":"";
                Rigidez = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003065").v_Value1 == "1" ? "X":"";
                Pustulas = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003066").v_Value1 == "1" ? "X":"";
                Comatoso = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003067").v_Value1 == "1" ? "X":"";

                RespRapida = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003078").v_Value1 == "1" ? "X":"";
                ObsTriaje = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003079").v_Value1 == "1" ? "X":"";
                ObsEscuch = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003080").v_Value1 == "1" ? "X":"";
                EscucharSibilancias = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003081").v_Value1 == "1" ? "X":"";
                ObsTriajeGrave = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003082").v_Value1 == "1" ? "X":"";

                Sibilancias1 = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003083").v_Value1 == "1" ? "X":"";
                Sibilancias2 = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003084").v_Value1 == "1" ? "X":"";
                SangreHeces = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003085").v_Value1 == "1" ? "X":"";
                NinioLetargico = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003086").v_Value1 == "1" ? "X":"";
                IntranquiloIrri = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003087").v_Value1 == "1" ? "X" : "";
                OjosHundidos = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003088").v_Value1 == "1" ? "X":"";
                OfreceLiq = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003089").v_Value1 == "1" ? "X":"";              
                BebeAvida = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003090").v_Value1 == "1" ? "X":"";
                PlieguePiel = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003091").v_Value1 == "1" ? "X":"";
                SignoPliegueCuta = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003092").v_Value1 == "1" ? "X":"";

                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                cells = new List<PdfPCell>();

                cells.Add(new PdfPCell(new Phrase("TRIAJE", fontColumnValue)) {Colspan=10, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Signos Vitales", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("T°", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("P.A.", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("F.C.", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("F.R.", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase("Peso", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Talla", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Temperatura, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(PA, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(FC, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(FR, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(Peso, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Talla, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Descarte de signos de peligro: (marcar los hallazgos)", fontColumnValue)) { Colspan = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("MENOR DE 2 MESES :", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("DE 2 MESES A 4 AÑOS :", fontColumnValue)) { Colspan=4, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("PARA TODAS LAS EDADES :", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("No quiere mamar ni succionar: ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(NoQuiereMamar, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("No puede beber o tomar pecho:  ", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(NoPuedeBeber, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Emaciación visible grave:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(Emaciacion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Convulsiones:", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Convulsiones2Meses, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Convulsiones:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Convulsiones4Años, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Piel vuelve muy lentamente.", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(PielLenta, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Fontanela Abombada", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Fontanela, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Letárgico o Comatoso", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Letargico, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Traumatisrno I Quemaduras:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(Traumatismo, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Enrojecimiento del ombligo se extiende a la piel", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Enrojecimiento, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Vomita todo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Vomito, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Envenenamiento:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(Envenenamiento, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Fiebre o temperatura baja ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Fiebre, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Estridor en reposo / tiraje subcostal", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Estridor, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Palidez palmar intenso:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(Palidez, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Rigidez de nuca:", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Rigidez, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Pústulas muchas y extensas:", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Pustulas, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                cells.Add(new PdfPCell(new Phrase("Letárgico o Comatoso", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Comatoso, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                cells.Add(new PdfPCell(new Phrase("Si tiene Tos /o Dificultad Respiratoria: (si es NO pasa al siguiente)", fontColumnValue)) { Colspan=10, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Respiración rápida", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RespRapida, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Observar tiraje subcostal:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(ObsTriaje, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Observar y escuchar estridor en reposo:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(ObsEscuch, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                cells.Add(new PdfPCell(new Phrase("Escuchar sibilancias", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(EscucharSibilancias, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Observar tiraje subcostal grave (<2m):", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(ObsTriajeGrave, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Sibilancias:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase("1° "+Sibilancias1 + "  Rec." + Sibilancias2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Si tiene Diarrea: (si es NO pasa al siguiente)", fontColumnValue)) { Colspan = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Hay sangre en heces", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(SangreHeces, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("El niño esta letárgico o comatoso:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(NinioLetargico, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Intranquilo o irritable:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(IntranquiloIrri, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Tiene los ojos hundidos", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(OjosHundidos, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Ofrecer líquido al niño: Puede beber :", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(OfreceLiq, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Bebe ávidamente con sed:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase(BebeAvida, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Sig.pliegue piel", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(PlieguePiel, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Signo del pliegue cutáneo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(SignoPliegueCuta, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER,   });
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });




                columnWidths = new float[] { 25f, 5f, 10f, 10f, 5f, 5f, 10f, 10f, 10f, 5f};

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                document.Add(table);
            #endregion

            #endregion
            }

             var findFormatoAdoles = serviceComponent.Find(p => p.v_ComponentId == "N009-ME000000402");
             if (findFormatoAdoles != null)
             {
                 document.NewPage();
                 #region Adolescente
                 #region Title

                 PdfPCell CellLogo4 = null;
                 cells = new List<PdfPCell>();

                 if (infoEmpresaPropietaria.b_Image != null)
                 {
                     CellLogo4 = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                 }
                 else
                 {
                     CellLogo4 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                 }

                 columnWidths = new float[] { 100f };

                 var cellstit4 = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("FORMATO DE ATENCIÓN", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INTEGRAL DEL (ADOLESCENTE)", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

                 tableTitulo = HandlingItextSharp.GenerateTableFromCells(cellstit4, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                 var cellstitNro4 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("3", fontTitle1)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_CENTER }
               
            };

                 tableNroHoja = HandlingItextSharp.GenerateTableFromCells(cellstitNro4, columnWidths, null, fontTitleTable);

                 cells.Add(CellLogo4);
                 cells.Add(new PdfPCell(tableTitulo));
                 cells.Add(new PdfPCell(tableNroHoja));

                 columnWidths = new float[] { 20f, 75f, 5f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                 document.Add(table);

                 #endregion
                 string FiebreUltimosDias = "", Tos15Dias = "",FiebreComentario="";
                 FiebreUltimosDias = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002860").v_Value1 == "1" ? "X" : "";
                 Tos15Dias = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002861").v_Value1 == "1" ? "X" : "";
                 FiebreComentario = findFormatoNinio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003093").v_Value1;
                 #region Datos
                 document.Add(new Paragraph("\r\n"));
                 document.Add(new Paragraph("\r\n"));

                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("CUIDADOS PREVENTIVOS – SEGUIMIENTO DE RIESGO - ADOLESCENTES", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("CADA CONSULTA", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("COMENTARIO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Fiebre en los últimos 15 días", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase(FiebreUltimosDias, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase(FiebreComentario, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Tos más de 15 días", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase(Tos15Dias, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Secreción o lesión en genitales", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("F.U.M.", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 45f, 5f, 45f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);

                 //Periodicamente
                 document.Add(new Paragraph("\r\n"));
                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("PERIODICAMENTE", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("IMC", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Desarrollo Sexual Mamas", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Desarrollo Sexual Vello púbico", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Desarrollo Sexual Genitales", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("--", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("--", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("--", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("--", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 25f, 25f, 25f, 25f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);

                 //Vacunas
                 document.Add(new Paragraph("\r\n"));
                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("Vacunas", fontColumnValue)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Vacuna Antitetánica", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Vacuna Antiamarilica", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Vacuna Hepatitis B", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Vacuna Sarampión", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 25f, 5f, 25f, 5f, 25f, 5f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);

                 //PERIÓDICAMENTE - ASPECTOS PSICOSOCIALES
                 document.Add(new Paragraph("\r\n"));
                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("PERIÓDICAMENTE - ASPECTOS PSICOSOCIALES", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Autoestima", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Comunicación", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Asertividad", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Toma de decisiones", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Ansiedad-depresión", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Violencia familiar", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Violencia política", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Violencia sexual", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Pandillas", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Sedentarismo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Uso de alcohol", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Uso de tabaco", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Uso de drogas", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("PERIÓDICAMENTE - ASPECTOS DE SEXUALIDAD", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Método anticonceptivo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Dos o más parejas", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Sexo sin protección", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("RS del mismo sexo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("RS del otro sexo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 25f, 25f, 25f, 25f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);


                 #endregion

                 #endregion
             }
            
             var findFormatoAdulto = serviceComponent.Find(p => p.v_ComponentId == "N009-ME000000403");
             if (findFormatoAdulto != null)
             {
                 document.NewPage();

                 #region Adulto

                 #region Title

                 PdfPCell CellLogo5 = null;
                 cells = new List<PdfPCell>();

                 if (infoEmpresaPropietaria.b_Image != null)
                 {
                     CellLogo5 = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                 }
                 else
                 {
                     CellLogo5 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                 }

                 columnWidths = new float[] { 100f };

                 var cellstit5 = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("FORMATO DE ATENCIÓN", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INTEGRAL DEL (ADULTO)", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

                 tableTitulo = HandlingItextSharp.GenerateTableFromCells(cellstit5, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                 var cellstitNro5 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("3", fontTitle1)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_CENTER }
               
            };

                 tableNroHoja = HandlingItextSharp.GenerateTableFromCells(cellstitNro5, columnWidths, null, fontTitleTable);

                 cells.Add(CellLogo5);
                 cells.Add(new PdfPCell(tableTitulo));
                 cells.Add(new PdfPCell(tableNroHoja));

                 columnWidths = new float[] { 20f, 75f, 5f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                 document.Add(table);

                 #endregion

                 #region Datos
                 document.Add(new Paragraph("\r\n"));
                 document.Add(new Paragraph("\r\n"));

                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("CUIDADOS PREVENTIVOS – SEGUIMIENTO DE RIESGO - ADULTO", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("CADA CONSULTA", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("COMENTARIO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Fiebre en los últimos 15 días", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Tos más de 15 días", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Secreción o lesión en genitales", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("F.U.M.", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 45f, 5f, 45f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);

                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("PERIODICAMENTE", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("IMC", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("PRESIÓN ARTERIAL", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 25f, 25f, 25f, 25f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);

                 //VACUNAS
                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("VACUNAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Vacuna Antitetánica", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Vacuna Antiamarilica", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Vacuna Hepatitis B", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 30f, 5f, 30f, 5f, 30f, 5f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);


                 //PERIODICAMENTE BUCAL
                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("PERIODICAMENTE - EXAMEN BUCAL", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Encías", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Caries dental", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Edentulismo parcial o total", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Portador de prótesis dental", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Estado de higiene dental", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Urgencia de tratamiento", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 //PERIÓDICAMENTE - EXAMEN
                 cells.Add(new PdfPCell(new Phrase("visual ( > 40 años)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("de colesterol ( > 45 años)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("de glucosa", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("de mamas", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("pélvico y PAP (C/año, C/3 a)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("mamografia (> 50 años, c/2 a)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("próstata (> 50 años)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 columnWidths = new float[] { 25f, 25f, 25f, 25f };

                 //PERIÓDICAMENTE - PSICOSOCIAL
                 cells.Add(new PdfPCell(new Phrase("Ansiedad -depresión", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Violencia familiar", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Violencia política", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 //PERIÓDICAMENTE - PSICOSOCIAL
                 cells.Add(new PdfPCell(new Phrase("Actividad sexual", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Planificación familiar", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);
                 #endregion
                 #endregion
             }

          
             var findFormatoAdultoMayor = serviceComponent.Find(p => p.v_ComponentId == "N009-ME000000404");
             if (findFormatoAdulto != null)
             {
                 document.NewPage();
                 #region Adulto Mayor
                 #region Title

                 PdfPCell CellLogo6 = null;
                 cells = new List<PdfPCell>();

                 if (infoEmpresaPropietaria.b_Image != null)
                 {
                     CellLogo6 = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                 }
                 else
                 {
                     CellLogo6 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                 }

                 columnWidths = new float[] { 100f };

                 var cellstit6 = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("FORMATO DE ATENCIÓN", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INTEGRAL DEL (ADULTO MAYOR)", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

                 tableTitulo = HandlingItextSharp.GenerateTableFromCells(cellstit6, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                 var cellstitNro6 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("3", fontTitle1)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_CENTER }
               
            };

                 tableNroHoja = HandlingItextSharp.GenerateTableFromCells(cellstitNro6, columnWidths, null, fontTitleTable);

                 cells.Add(CellLogo6);
                 cells.Add(new PdfPCell(tableTitulo));
                 cells.Add(new PdfPCell(tableNroHoja));

                 columnWidths = new float[] { 20f, 75f, 5f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                 document.Add(table);

                 #endregion

                 #region Datos
                 document.Add(new Paragraph("\r\n"));
                 document.Add(new Paragraph("\r\n"));

                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("CUIDADOS PREVENTIVOS – SEGUIMIENTO DE RIESGO - ADULTO", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("CADA CONSULTA", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("COMENTARIO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Fiebre en los últimos 15 días", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Tos más de 15 días", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Secreción o lesión en genitales", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("F.U.M.", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("x", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 45f, 5f, 45f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);

                 //PERIODICAMENTE
                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("PERIODICAMENTE", fontColumnValue)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("IMC", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("PRESIÓN ARTERIAL", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 25f, 25f, 25f, 25f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);

                 //VACUNAS
                 cells = new List<PdfPCell>();

                 cells.Add(new PdfPCell(new Phrase("VACUNAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Vacuna Antitetánica", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Vacuna Antiamarilica", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Vacuna Hepatitis B", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Vacuna Influenza", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Vacuna Antianeumocócica", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                 //PERIÓDICAMENTE - EXAMEN BUCAL Control en el último año
                 cells.Add(new PdfPCell(new Phrase("Vacuna Influenza", fontColumnValue)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("--", fontColumnValue)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 30f, 5f, 30f, 5f, 30f, 5f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);


                 //PERIODICAMNENTE - EXAMEN
                 cells = new List<PdfPCell>();
                 cells.Add(new PdfPCell(new Phrase("PERIÓDICAMENTE - EXAMEN", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("de glucosa", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("de mamas", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("pélvico y PAP (C/año, C/3 a)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("mamografia (> 50 años, c/2 a)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("próstata (> 50 años)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 //SÍNDROMES Y PROBLEMAS GERTÁTRICOS
                 cells.Add(new PdfPCell(new Phrase("SÍNDROMES Y PROBLEMAS GERTÁTRICOS", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Vértigo-mareo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Síncope", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Dolor crónico", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Deprivacíón Auditiva", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Deprivación Visual", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Insomnio", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Incontinencia urinaria", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Prostatismo (síntomas prostéticos)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 cells.Add(new PdfPCell(new Phrase("Estreñimiento", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("Ulceras de presión", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                 columnWidths = new float[] { 25f, 25f, 25f, 25f };

                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                 document.Add(table);
                 #endregion

                 #endregion
             }

            var findFormatoAttenMedica = serviceComponent.Find(p => p.v_ComponentId == "N009-ME000000405");
            if (findFormatoAttenMedica != null)
            {
                document.NewPage();

                string MotivoConsulta = "", TiempoEnf = "", Apetito = "", Sed = "", Suenio = "", EstadoAnimo = "", Orina = "", Deposiciones = "", ExamenFisico="";

                MotivoConsulta = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002938").v_Value1;
                TiempoEnf = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002939").v_Value1;
                Apetito = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002940").v_Value1;
                Sed = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002941").v_Value1;
                Suenio = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002942").v_Value1;
                EstadoAnimo = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002943").v_Value1;
                Orina = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002944").v_Value1;
                Deposiciones = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002945").v_Value1;
                ExamenFisico = findFormatoAttenMedica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002946").v_Value1;
                #region Séptima Hoja

                #region Title

                PdfPCell CellLogo7 = null;
                cells = new List<PdfPCell>();

                if (infoEmpresaPropietaria.b_Image != null)
                {
                    CellLogo7 = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                }
                else
                {
                    CellLogo7 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                }

                columnWidths = new float[] { 100f };

                var cellstit7 = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("FORMATO DE ATENCIÓN", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INTEGRAL DEL (NIÑO/ ADOLESCENTE /ADULTO /ADULTOMAYOR)", fontSubTitleNegroNegrita)) 
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

                tableTitulo = HandlingItextSharp.GenerateTableFromCells(cellstit7, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                var cellstitNro7 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("4", fontTitle1)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_CENTER }
               
            };

                tableNroHoja = HandlingItextSharp.GenerateTableFromCells(cellstitNro7, columnWidths, null, fontTitleTable);

                cells.Add(CellLogo7);
                cells.Add(new PdfPCell(tableTitulo));
                cells.Add(new PdfPCell(tableNroHoja));

                columnWidths = new float[] { 20f, 75f, 5f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                document.Add(table);

                #endregion

                #region Datos
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                cells = new List<PdfPCell>();

                cells.Add(new PdfPCell(new Phrase("CONSULTA", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Fecha: " + filiationData.d_ServiceDate.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Hora: " + filiationData.d_ServiceDate.Value.Hour.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Edad", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(filiationData.Edad.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Motivo de la Consulta", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Tiempo de la Enfermedad", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase(MotivoConsulta, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(TiempoEnf, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Apetito", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Sed", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Sueño", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Estado de Ánimo", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase(Apetito, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Sed, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Suenio, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(EstadoAnimo, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Orina: " + Orina, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Deposiciones " + Deposiciones, fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Temperatura", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Presión Arterial", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Frec. Cardíaca", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Frec. Resp.", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase(Temperatura, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(PA, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(FC, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(FR, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("Peso", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("Talla", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("IMC", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("-", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase(Peso, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(Talla, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(IMC, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("-", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                cells.Add(new PdfPCell(new Phrase("Ex. Físico", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(ExamenFisico, fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                document.Add(table);

                #region Hallazgos y recomendaciones

                cells = new List<PdfPCell>();

                var filterDiagnosticRepository = diagnosticRepository.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);

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
                            oListaComun.Valor1 = "--------RECOMENDACIONES------";
                            oListaComun.i_Item = "";
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
                            oListaComun.Valor1 = "--------RESTRICCIONES------";
                            oListaComun.i_Item = "";
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

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "IV. CONCLUSIONES", fontTitleTableNegro);

                document.Add(table);

                #endregion

                #region Firma y sello Médico

                table = new PdfPTable(2);
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.WidthPercentage = 40;

                columnWidths = new float[] { 15f, 25f };
                table.SetWidths(columnWidths);

                PdfPCell cellFirma = null;

                if (filiationData.FirmaDoctorAuditor != null)
                    cellFirma = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaDoctorAuditor, null, null, 120, 45));
                else
                    cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

                cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirma.FixedHeight = 50F;

                cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                table.AddCell(cell);
                table.AddCell(cellFirma);

                document.Add(table);

                #endregion
                #endregion

                #endregion

            }

            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
