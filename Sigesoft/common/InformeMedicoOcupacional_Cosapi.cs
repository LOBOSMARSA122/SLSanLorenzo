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
    public class InformeMedicoOcupacional_Cosapi
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }

        #region Reporte SAS
        public static void CreateInformeMedicoOcupacional_Cosapi(ServiceList DataService,
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
            string Aptitud
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
                    new PdfPCell(new Phrase("", fontTitle1)) {Colspan=1, BackgroundColor= BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                    new PdfPCell(new Phrase("N° H.C. " + datosPac.v_PersonId, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                    new PdfPCell(new Phrase("INFORME MÉDICO OCUPACIONAL", fontTitle1)) { Colspan=2, BackgroundColor= BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f},
                };
            columnWidths = new float[] { 80f, 20f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS GENERALES

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("Apellidos y Nombres", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("DNI", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(datosPac.v_DocNumber , fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Empresa", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda}, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString() , fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                new PdfPCell(new Phrase("Sede/Proyecto", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(filiationData.v_NombreProtocolo, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Puesto de trabajo", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                
                new PdfPCell(new Phrase("Antecedentes o Preexistencia", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            
                new PdfPCell(new Phrase("Alergias", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


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
                    new PdfPCell(new Phrase("Tipo de examen", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("Preocupacional", fontColumnValue)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(PreOcupacional, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                    new PdfPCell(new Phrase("Periódico", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(Periodica, fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    

                    new PdfPCell(new Phrase("Retiro", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(Retiro, fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                    new PdfPCell(new Phrase("Otro(Especifique)", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(Otros, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            };


            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EVALUACIÓN MÉDICA

            //Antropometria
            ServiceComponentList antro = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            var peso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
            var talla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_Value1;
            var imc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_Value1;
            var per_abdomen = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).v_Value1;

            //Funciones Vitales
            ServiceComponentList funcVit = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
            var pres_Sist = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
            var pres_Diast = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;

            // Orina Completo
            var xOrinaCompleto = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);
            var Hemo = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA);
            //var HemoValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA_DESEABLE);


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 2, BackgroundColor = BaseColor.GRAY },       
                
                new PdfPCell(new Phrase("Evaluación Médica", fontColumnValue)) { Colspan = 4, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Hemoglobina \n (Hb)", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Peso (Kg)", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Talla (m)", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("IMC (Kg/m2)", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("PA (mmHg)", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Per. Abd. (cm)", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Grupo Sanguineo y Factor Rh", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                
                
                new PdfPCell(new Phrase(Hemo == null ? string.Empty : Hemo.v_Value1Name, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(peso + " Kg", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(talla + " m.", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(imc + " kg/m2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(pres_Sist +" / " + pres_Diast + " mmHg" , fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(per_abdomen + " cm", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(datosPac.v_BloodGroupName + "\n" + datosPac.v_BloodFactorName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region CLÍNICO OC. / MUSCULOESQUELETICO / PSICOLOGÍA / ODONTOLÓGICO /
            #region EXAMEN CLÍNICO OCUPACIONAL

            var dxExamenFisico = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_ID);
            var dxExamenFisicoConcatenado = string.Join(", ", dxExamenFisico.Select(p => p.v_DiseasesName));
            if (dxExamenFisicoConcatenado != null)
            {
                cells = new List<PdfPCell>()
                    {
                     new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 2, BackgroundColor = BaseColor.GRAY },       
                     new PdfPCell(new Phrase("Clínico Ocupacional", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                     new PdfPCell(new Phrase(dxExamenFisicoConcatenado, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                     };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            }
            else 
            {
                cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Clínico Ocupacional", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase("NO TIENE OBSERVACIONES", fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            }
            #endregion

                #region MUSOESQUELÉTICO
            var dxArticuOsteo = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.OSTEO_MUSCULAR_ID_1);
            string ArticuOsteo = string.Join(", ", dxArticuOsteo.Select(p => p.v_DiseasesName));
                 cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Musculoesquelético", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                        new PdfPCell(new Phrase(ArticuOsteo== "" ? "SIN RESULTADOS": ArticuOsteo, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            #endregion
                #region PSICOLOGÍA
            var xPsico = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.PSICOLOGIA_ID);
            var ValorDxPsico = string.Join(", ", xPsico.Select(p => p.v_DiseasesName)); ;
       
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Psicología", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(ValorDxPsico == "" ?"SIN RESULTADOS" : ValorDxPsico, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);

            #endregion
                #region ODONTOLOGÍA
            var xOdo = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.ODONTOGRAMA_ID);
            string ValorDxOdo = string.Join(", ", xOdo.Select(p => p.v_DiseasesName));
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Odontología", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(ValorDxOdo == "" ? "SIN RESULTADOS": ValorDxOdo, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            #endregion
                #endregion
                #region OFTALMOLOGÍA
            ServiceComponentList oftalmologia = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
            var av_cerca_sc_od = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_CERCA_OJO_DERECHO_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_CERCA_OJO_DERECHO_ID).v_Value1;
            string av_cerca_sc_od_1 = "", av_cerca_sc_oi_1 = "", av_cerca_cc_od_1 = "", av_cerca_cc_oi_1 = "";
            if (av_cerca_sc_od == "1") av_cerca_sc_od_1 = "J1 0.50";
            else if (av_cerca_sc_od == "2") av_cerca_sc_od_1 = "J2 0.75";
            else if (av_cerca_sc_od == "3") av_cerca_sc_od_1 = "J3 1.00";
            else if (av_cerca_sc_od == "4") av_cerca_sc_od_1 = "J4 1.25";
            else if (av_cerca_sc_od == "5") av_cerca_sc_od_1 = "J5 1.50";
            else if (av_cerca_sc_od == "6") av_cerca_sc_od_1 = "J6 1.75";
            else if (av_cerca_sc_od == "7") av_cerca_sc_od_1 = "J7 2.00";
            else if (av_cerca_sc_od == "8") av_cerca_sc_od_1 = "J8 >2.00";

            var av_cerca_sc_oi = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_CERCA_OJO_IZQUIERDO_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_CERCA_OJO_IZQUIERDO_ID).v_Value1;

            if (av_cerca_sc_oi == "1") av_cerca_sc_oi_1 = "J1 0.50";
            else if (av_cerca_sc_oi == "2") av_cerca_sc_oi_1 = "J2 0.75";
            else if (av_cerca_sc_oi == "3") av_cerca_sc_oi_1 = "J3 1.00";
            else if (av_cerca_sc_oi == "4") av_cerca_sc_oi_1 = "J4 1.25";
            else if (av_cerca_sc_oi == "5") av_cerca_sc_oi_1 = "J5 1.50";
            else if (av_cerca_sc_oi == "6") av_cerca_sc_oi_1 = "J6 1.75";
            else if (av_cerca_sc_oi == "7") av_cerca_sc_oi_1 = "J7 2.00";
            else if (av_cerca_sc_oi == "8") av_cerca_sc_oi_1 = "J8 >2.00";

            var av_cerca_cc_od = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_CERCA_OJO_DERECHO_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_CERCA_OJO_DERECHO_ID).v_Value1;

            if (av_cerca_cc_od == "1") av_cerca_cc_od_1 = "J1 0.50";
            else if (av_cerca_cc_od == "2") av_cerca_cc_od_1 = "J2 0.75";
            else if (av_cerca_cc_od == "3") av_cerca_cc_od_1 = "J3 1.00";
            else if (av_cerca_cc_od == "4") av_cerca_cc_od_1 = "J4 1.25";
            else if (av_cerca_cc_od == "5") av_cerca_cc_od_1 = "J5 1.50";
            else if (av_cerca_cc_od == "6") av_cerca_cc_od_1 = "J6 1.75";
            else if (av_cerca_cc_od == "7") av_cerca_cc_od_1 = "J7 2.00";
            else if (av_cerca_cc_od == "8") av_cerca_cc_od_1 = "J8 >2.00";

            var av_cerca_cc_oi = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID).v_Value1;

            if (av_cerca_cc_oi == "1") av_cerca_cc_oi_1 = "J1 0.50";
            else if (av_cerca_cc_oi == "2") av_cerca_cc_oi_1 = "J2 0.75";
            else if (av_cerca_cc_oi == "3") av_cerca_cc_oi_1 = "J3 1.00";
            else if (av_cerca_cc_oi == "4") av_cerca_cc_oi_1 = "J4 1.25";
            else if (av_cerca_cc_oi == "5") av_cerca_cc_oi_1 = "J5 1.50";
            else if (av_cerca_cc_oi == "6") av_cerca_cc_oi_1 = "J6 1.75";
            else if (av_cerca_cc_oi == "7") av_cerca_cc_oi_1 = "J7 2.00";
            else if (av_cerca_cc_oi == "8") av_cerca_cc_oi_1 = "J8 >2.00";

            var av_lejos_sc_od = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID).v_Value1;
            string av_lejos_sc_od_1 = "", av_lejos_sc_oi_1 = "", av_lejos_cc_od_1 = "", av_lejos_cc_oi_1 = "";
            if (av_lejos_sc_od == "1") av_lejos_sc_od_1 = "20/10";
            else if (av_lejos_sc_od == "2") av_lejos_sc_od_1 = "20/100";
            else if (av_lejos_sc_od == "3") av_lejos_sc_od_1 = "20/13";
            else if (av_lejos_sc_od == "4") av_lejos_sc_od_1 = "20/15";
            else if (av_lejos_sc_od == "5") av_lejos_sc_od_1 = "20/20";
            else if (av_lejos_sc_od == "6") av_lejos_sc_od_1 = "20/200";
            else if (av_lejos_sc_od == "7") av_lejos_sc_od_1 = "20/25";
            else if (av_lejos_sc_od == "8") av_lejos_sc_od_1 = "20/30";
            else if (av_lejos_sc_od == "9") av_lejos_sc_od_1 = "20/40";
            else if (av_lejos_sc_od == "10") av_lejos_sc_od_1 = "20/50";
            else if (av_lejos_sc_od == "11") av_lejos_sc_od_1 = "20/70";
            else if (av_lejos_sc_od == "12") av_lejos_sc_od_1 = "NO APLICA";

            var av_lejos_sc_oi = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).v_Value1;

            if (av_lejos_sc_oi == "1") av_lejos_sc_oi_1 = "20/10";
            else if (av_lejos_sc_oi == "2") av_lejos_sc_oi_1 = "20/100";
            else if (av_lejos_sc_oi == "3") av_lejos_sc_oi_1 = "20/13";
            else if (av_lejos_sc_oi == "4") av_lejos_sc_oi_1 = "20/15";
            else if (av_lejos_sc_oi == "5") av_lejos_sc_oi_1 = "20/20";
            else if (av_lejos_sc_oi == "6") av_lejos_sc_oi_1 = "20/200";
            else if (av_lejos_sc_oi == "7") av_lejos_sc_oi_1 = "20/25";
            else if (av_lejos_sc_oi == "8") av_lejos_sc_oi_1 = "20/30";
            else if (av_lejos_sc_oi == "9") av_lejos_sc_oi_1 = "20/40";
            else if (av_lejos_sc_oi == "10") av_lejos_sc_oi_1 = "20/50";
            else if (av_lejos_sc_oi == "11") av_lejos_sc_oi_1 = "20/70";
            else if (av_lejos_sc_oi == "12") av_lejos_sc_oi_1 = "NO APLICA";

            var av_lejos_cc_od = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID).v_Value1;

            if (av_lejos_cc_od == "1") av_lejos_cc_od_1 = "20/10";
            else if (av_lejos_cc_od == "2") av_lejos_cc_od_1 = "20/100";
            else if (av_lejos_cc_od == "3") av_lejos_cc_od_1 = "20/13";
            else if (av_lejos_cc_od == "4") av_lejos_cc_od_1 = "20/15";
            else if (av_lejos_cc_od == "5") av_lejos_cc_od_1 = "20/20";
            else if (av_lejos_cc_od == "6") av_lejos_cc_od_1 = "20/200";
            else if (av_lejos_cc_od == "7") av_lejos_cc_od_1 = "20/25";
            else if (av_lejos_cc_od == "8") av_lejos_cc_od_1 = "20/30";
            else if (av_lejos_cc_od == "9") av_lejos_cc_od_1 = "20/40";
            else if (av_lejos_cc_od == "10") av_lejos_cc_od_1 = "20/50";
            else if (av_lejos_cc_od == "11") av_lejos_cc_od_1 = "20/70";
            else if (av_lejos_cc_od == "12") av_lejos_cc_od_1 = "NO APLICA";

            var av_lejos_cc_oi = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID).v_Value1;

            if (av_lejos_cc_oi == "1") av_lejos_cc_oi_1 = "20/10";
            else if (av_lejos_cc_oi == "2") av_lejos_cc_oi_1 = "20/100";
            else if (av_lejos_cc_oi == "3") av_lejos_cc_oi_1 = "20/13";
            else if (av_lejos_cc_oi == "4") av_lejos_cc_oi_1 = "20/15";
            else if (av_lejos_cc_oi == "5") av_lejos_cc_oi_1 = "20/20";
            else if (av_lejos_cc_oi == "6") av_lejos_cc_oi_1 = "20/200";
            else if (av_lejos_cc_oi == "7") av_lejos_cc_oi_1 = "20/25";
            else if (av_lejos_cc_oi == "8") av_lejos_cc_oi_1 = "20/30";
            else if (av_lejos_cc_oi == "9") av_lejos_cc_oi_1 = "20/40";
            else if (av_lejos_cc_oi == "10") av_lejos_cc_oi_1 = "20/50";
            else if (av_lejos_cc_oi == "11") av_lejos_cc_oi_1 = "20/70";
            else if (av_lejos_cc_oi == "12") av_lejos_cc_oi_1 = "NO APLICA";

            var estereopsis = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_A) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_A).v_Value1;
            var TestIshiharaNormal = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL).v_Value1;
            var TestIshiharaAnormal = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL).v_Value1;
            //var Dicromatopsia = oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DICROMATOPSIA_ID) == null ? "" : oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DICROMATOPSIA_ID).v_Value1;
            string VisonColores = "";
            if (TestIshiharaNormal == "1")
            {
                VisonColores = "Normal";
            }
            else if (TestIshiharaAnormal == "1")
            {
                VisonColores = " Anormal";
            }

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 2, BackgroundColor = BaseColor.GRAY },       

                    new PdfPCell(new Phrase("Oftalmología", fontColumnValue)) { Colspan = 2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("Agudeza Visual", fontColumnValue)) { Colspan = 2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("V. de cerca", fontColumnValue)) { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OD S/C", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(av_cerca_sc_od_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OD C/C", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(av_cerca_cc_od_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OI S/C", fontColumnValue)) { Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(av_cerca_sc_oi_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OI C/C", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(av_cerca_cc_oi_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("Esteropsis", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(estereopsis, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 

                    new PdfPCell(new Phrase("V. de lejos", fontColumnValue)) { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OD S/C", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(av_lejos_sc_od_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OD C/C", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(av_lejos_cc_od_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OI S/C", fontColumnValue)) { Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(av_lejos_sc_oi_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OI C/C", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(av_lejos_cc_oi_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("Ishihara", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase(VisonColores, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            #endregion

                #region ESPIROMETRÍA
            var xEspiro = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
            string ValorDxEspiro = string.Join(", ", xEspiro.Select(p => p.v_DiseasesName));
            cells = new List<PdfPCell>()
                {
                   new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 2, BackgroundColor = BaseColor.GRAY },       

                   new PdfPCell(new Phrase("Espirometría", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                   new PdfPCell(new Phrase(ValorDxEspiro== "" ? "SIN RESULTADOS": ValorDxEspiro , fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            #endregion 

                #region RX TÓRAX / SEGÚN OIT
            var xRx = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.RX_TORAX_ID);
            string ValorDxRx = string.Join(", ", xRx.Select(p => p.v_DiseasesName));

            var xRxOIT = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.OIT_ID);
            string ValorDxRxOIT = string.Join(", ", xRxOIT.Select(p => p.v_DiseasesName));
              cells = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("Rx Tórax", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(ValorDxRx== "" ? "SIN RESULTADOS": ValorDxRx, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("Según OIT", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(ValorDxRxOIT== "" ? "SIN RESULTADOS": ValorDxRxOIT, fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            #endregion 
                #region AUDIOMETRÍA
            var dxAudiometria = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
            var dxAudio = string.Join(", ", dxAudiometria.Select(p => p.v_DiseasesName));
         
                cells = new List<PdfPCell>()
                    {
                     new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 2, BackgroundColor = BaseColor.GRAY },       

                     new PdfPCell(new Phrase("Audiometría", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                     new PdfPCell(new Phrase(dxAudio== "" ? "SIN RESULTADOS" :dxAudio, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                     };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            #endregion

                #region ELECTROCARDIOGRAMA
            var xElectroCardiograma = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID);
            string ValorDxElectroCardiograma = string.Join(", ", xElectroCardiograma.Select(p => p.v_DiseasesName));

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Electrocardiograma", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(ValorDxElectroCardiograma== "" ? "SIN RESULTADOS": ValorDxElectroCardiograma, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
            #endregion

                #region LABORATORIO
                ServiceComponentList glucosa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GLUCOSA_ID);

                var Valor = glucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_RESULTADO_ID) == null ? "" : glucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_RESULTADO_ID).v_Value1;
                var Uni = glucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_RESULTADO_ID) == null ? "" : glucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_RESULTADO_ID).v_MeasurementUnitName;

                var dxHemograma = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_ID);
                var dxHemogramaConcatenado = string.Join(", ", dxHemograma.Select(p => p.v_DiseasesName)) == "" ? "Hemograma sin alteración" : string.Join(", ", dxHemograma.Select(p => p.v_DiseasesName));
                
                var dxOrina = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);
                var dxOrinaConcatenado = string.Join(", ", dxOrina.Select(p => p.v_DiseasesName)) == "" ? "Orina sin alteración" : string.Join(", ", dxOrina.Select(p => p.v_DiseasesName));

                ServiceComponentList perfillipidico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_LIPIDICO);

                var valor_colesterol = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL).v_Value1;
                var uni_colesterol = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL).v_MeasurementUnitName;

                var valor_hdl = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL).v_Value1;
                var uni_hdl = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL).v_MeasurementUnitName;

                var valor_ldl = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL).v_Value1;
                var uni_ldl = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL).v_MeasurementUnitName;

                var valor_vldl = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL).v_Value1;
                var uni_vldl = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL).v_MeasurementUnitName;

                var valor_trigliceridos = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS).v_Value1;
                var uni_trigliceridos = perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS) == null ? "" : perfillipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS).v_MeasurementUnitName;


                ServiceComponentList perfilhepatico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ID);

                var valor_tgp = perfilhepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_ID) == null ? "" : perfilhepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_ID).v_Value1;
                var uni_tgp = perfilhepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_ID) == null ? "" : perfilhepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_ID).v_MeasurementUnitName;

                var valor_ggtp = perfilhepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GAMMA_GLUTAMIL_TRANSPEPTIDASA_ID) == null ? "" : perfilhepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GAMMA_GLUTAMIL_TRANSPEPTIDASA_ID).v_Value1;
                var uni_ggtp = perfilhepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GAMMA_GLUTAMIL_TRANSPEPTIDASA_ID) == null ? "" : perfilhepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GAMMA_GLUTAMIL_TRANSPEPTIDASA_ID).v_MeasurementUnitName;


                ServiceComponentList coca_marih = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                string valor_coca = coca_marih.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000705") == null ? "" : coca_marih.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000705").v_Value1;
                string valor_coca_1 = "";
                if (valor_coca == "1") valor_coca_1 = "POSITIVO";
                else if (valor_coca =="2") valor_coca_1 ="NEGATIVO";
                else if(valor_coca=="3") valor_coca_1 = "CONFIDENCIAL";
                string valor_marih = coca_marih.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294") == null ? "" : coca_marih.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294").v_Value1;
                string valor_marih_1 = "";
                if (valor_marih == "1") valor_marih_1 = "POSITIVO";
                else if (valor_marih == "2") valor_marih_1 = "NEGATIVO";
                else if (valor_marih == "3") valor_marih_1 = "CONFIDENCIAL";
                
                ServiceComponentList creatinina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CREATININA_ID);

                var valor_creatinina = creatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA) == null ? "" : creatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA).v_Value1;
                var uni_creatinina = creatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA) == null ? "" : creatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA).v_MeasurementUnitName;

               
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("LABORATORIO:", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       

                    new PdfPCell(new Phrase("Glucosa", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("Hemograma", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(dxHemogramaConcatenado, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                    new PdfPCell(new Phrase("Colesterol Total", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_colesterol + " " + uni_colesterol, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("Examen de Orina", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(dxOrinaConcatenado, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    
                    new PdfPCell(new Phrase("Colesterol HDL", fontColumnValue)) { Colspan = 4, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_hdl + " " + uni_hdl, fontColumnValue)) { Colspan = 3, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("TGP", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_tgp + " " + uni_tgp, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                    new PdfPCell(new Phrase("GGTP", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_ggtp + " " + uni_ggtp, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                
                    new PdfPCell(new Phrase("Colesterol LDL", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_ldl + " " + uni_ldl, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("Toxicologico", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("Cocaina", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_coca_1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("Marihuana", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_marih_1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    
                    new PdfPCell(new Phrase("Colesterol VLDL", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_vldl +" "+uni_vldl, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("Creatinina", fontColumnValue)) { Colspan = 3, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_creatinina+ " " + uni_creatinina, fontColumnValue)) { Colspan = 10, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    
                    new PdfPCell(new Phrase("Trigliceridos", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(valor_trigliceridos + " " +uni_trigliceridos , fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
                #endregion

                #region APTITUDES ESPECÍFICAS
                ServiceComponentList geografico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_ID);
                var alt_geograf = geografico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_APTO_ID) == null ? "" : geografico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_APTO_ID).v_Value1;
                string alt_geograf_1 = "", alt_geograf_2 = "", alt_geograf_3 = "", alt_geograf_4="";
                if (alt_geograf == "1") alt_geograf_1 = "X";
                else if (alt_geograf == "2") alt_geograf_2 = "X";
                else if (alt_geograf == "3") alt_geograf_3 = "X";
                else if (alt_geograf == "4") alt_geograf_4 = "X";

                ServiceComponentList estructural = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID);
                var alt_estruc = estructural.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_APTO_ID) == null ? "" : estructural.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_APTO_ID).v_Value1;
                string alt_estruc_1 = "", alt_estruc_2 = "", alt_estruc_3 = "", alt_estruc_4="";
                if (alt_estruc == "1") alt_estruc_1 = "X";
                else if (alt_estruc == "2") alt_estruc_2 = "X";
                else if (alt_estruc == "3") alt_estruc_3 = "X";
                else if (alt_estruc == "4") alt_estruc_4 = "X";
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("APTITUDES ESPECÍFICAS:", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       

                    new PdfPCell(new Phrase("Trabajador en altura geográfica mayor a 2500 m.s.n.m.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase("APTO", fontColumnValue)) { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(alt_geograf_1, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("NO APTO", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(alt_geograf_2, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("APTO CON RESTRIC.", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(alt_geograf_3, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    new PdfPCell(new Phrase("OBSERVADO", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                    new PdfPCell(new Phrase(alt_geograf_4, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                    
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
                #endregion

                #region Hallazgos y recomendaciones
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("DIAGNOSTICOS MÉDICOS", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 13, BackgroundColor = BaseColor.GRAY },       

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
                string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
                string[] fechacaducidad = datosPac.FechaCaducidad.ToString().Split(' ');
                cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("Resultado para Trabajar", fontColumnValue)){ Colspan=20,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor=BaseColor.GRAY, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase("APTO", fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(Apto,fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                    new PdfPCell(new Phrase("APTO CON RESTRICCIONES", fontColumnValue)){Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(AptoConRestricciones,fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                    new PdfPCell(new Phrase("NO APTO", fontColumnValue)){ Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(NoApto,fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},
                    
                    new PdfPCell(new Phrase("FECHA DE EXAMEN", fontColumnValue)){Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(fechaServicio[0],fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                    new PdfPCell(new Phrase("FECHA DE CADUCIDAD", fontColumnValue)){ Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                    new PdfPCell(new Phrase(fechacaducidad[0],fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE , MinimumHeight= tamaño_celda},

                 };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);


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
                document.Close();
            writer.Close();
            writer.Dispose();
        }
        #endregion
    }
}
