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
    public class HistoriaClinica
    {
        public static void CreateHistoriaClinica(PacientList filiationData, List<ServiceComponentList> serviceComponent, 
            organizationDto infoEmpresaPropietaria, List<PersonMedicalHistoryList> listMedicoPersonales, 
            List<FamilyMedicalAntecedentsList> listaPatologicosFamiliares, List<NoxiousHabitsList> listaHabitoNocivos, List<DiagnosticRepositoryList> ListDiagnosticRepository, List<MedicationList> ListMedicamentos, string Recomendaciones, string filePDF)
        {
            //      
            // step 1: creation of a document-object
            Document document = new Document();
            //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
            //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            //Document document = new Document(PageSize.A4, 0, 0, 20, 20);
            //
            try
            {
                 // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
                //
                //create an instance of your PDFpage class. This is the class we generated above.
                pdfPage page = new pdfPage();
                //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
                writer.PageEvent = page;

                // step 3: we open the document
                document.Open();
                // step 4: we Add content to the document
                // we define some fonts

                #region Fonts

                Font fontTitle1 = FontFactory.GetFont(FontFactory.HELVETICA, 18, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));


                #endregion

                #region Logo

                Image logo = HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F);
                PdfPTable headerTbl = new PdfPTable(1);
                headerTbl.TotalWidth = writer.PageSize.Width;
                PdfPCell cellLogo = new PdfPCell(logo);

                cellLogo.VerticalAlignment = Element.ALIGN_TOP;
                cellLogo.HorizontalAlignment = Element.ALIGN_CENTER;

                cellLogo.Border = PdfPCell.NO_BORDER;
                headerTbl.AddCell(cellLogo);
                document.Add(headerTbl);

                #endregion

                #region Title

                Paragraph cTitle = new Paragraph("HISTORIA CLÍNICA", fontTitle2);
                //Paragraph cTitle2 = new Paragraph(customerOrganizationName, fontTitle2);
                cTitle.Alignment = Element.ALIGN_CENTER;
                //cTitle2.Alignment = Element.ALIGN_CENTER;

                document.Add(cTitle);
               //document.Add(cTitle2);

                #endregion
               
                #region Declaration Tables
                var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
                string include = string.Empty;
                List<PdfPCell> cells = null;
                float[] columnWidths = null;
                string[] columnValues = null;
                string[] columnHeaders = null;

                //PdfPTable header1 = new PdfPTable(2);
                //header1.HorizontalAlignment = Element.ALIGN_CENTER;
                //header1.WidthPercentage = 100;
                ////header1.TotalWidth = 500;
                ////header1.LockedWidth = true;    // Esto funciona con TotalWidth
                //float[] widths = new float[] { 150f, 200f};
                //header1.SetWidths(widths);


                //Rectangle rec = document.PageSize;
                PdfPTable header2 = new PdfPTable(6);
                header2.HorizontalAlignment = Element.ALIGN_CENTER;
                header2.WidthPercentage = 100;
                //header1.TotalWidth = 500;
                //header1.LockedWidth = true;    // Esto funciona con TotalWidth
                float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
                header2.SetWidths(widths1);
                //header2.SetWidthPercentage(widths1, rec);

                PdfPTable companyData = new PdfPTable(6);
                companyData.HorizontalAlignment = Element.ALIGN_CENTER;
                companyData.WidthPercentage = 100;
                //header1.TotalWidth = 500;
                //header1.LockedWidth = true;    // Esto funciona con TotalWidth
                float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
                companyData.SetWidths(widthscolumnsCompanyData);

                PdfPTable filiationWorker = new PdfPTable(4);

                PdfPTable table = null;

                PdfPCell cell = null;

                #endregion

                // Salto de linea
                document.Add(new Paragraph("\r\n"));


                #region Datos Empresa Propietaria

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Dirección: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(infoEmpresaPropietaria.v_Address, fontColumnValue)),                   
                    new PdfPCell(new Phrase("Teléfono: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(infoEmpresaPropietaria.v_PhoneNumber, fontColumnValue)),     
                    new PdfPCell(new Phrase("Email: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(infoEmpresaPropietaria.v_Mail, fontColumnValue)),     
                         };

                columnWidths = new float[] { 17f, 17f, 17f, 17f, 17f, 17f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                // Salto de linea
                document.Add(new Paragraph("\r\n"));
                #region Cabecera del Reporte

                 var xFuncionesVitales = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
                 var FrecCar = "";
                 var FrecResp = "";
                 var PResionArterial = "";
                 var PResionDias = "";
                 var Temperatura = "";
                 if (xFuncionesVitales != null || xFuncionesVitales.ServiceComponentFields.Count == 0)
                 {

                     PResionArterial = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
                     PResionDias = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;

                     FrecCar = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_Value1;
                     FrecResp = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_Value1;

                     Temperatura = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID) == null ? "" : xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID).v_Value1;


                 }
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Código: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_IdService, fontColumnValue)),                   
                    new PdfPCell(new Phrase("Fecha: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)),     
                    new PdfPCell(new Phrase("Emp. Seguro: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_DocNumber, fontColumnValue)){ Colspan=3},     
                    

                    new PdfPCell(new Phrase("Paciente: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName, fontColumnValue)){Colspan=7},                   
                    new PdfPCell(new Phrase("Dirección: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_AdressLocation, fontColumnValue)){Colspan=7},                   
                   
                    new PdfPCell(new Phrase("Sexo: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_SexTypeName, fontColumnValue)),     
                    new PdfPCell(new Phrase("Teléfono: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_TelephoneNumber, fontColumnValue)),     
                    new PdfPCell(new Phrase("DNI: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_DocNumber, fontColumnValue)),     
                    new PdfPCell(new Phrase("Edad: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.i_Age.ToString() + "años", fontColumnValue)),     
                   

                    new PdfPCell(new Phrase("F.C: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(FrecCar + " (Lat/min)", fontColumnValue)),     
                    new PdfPCell(new Phrase("F.R: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(FrecResp + " (Resp/min)", fontColumnValue)),     
                    new PdfPCell(new Phrase("P.A: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(PResionArterial + " (mmHg)", fontColumnValue)),     
                    new PdfPCell(new Phrase("T.: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(Temperatura + " C°", fontColumnValue)),     
                   

                    new PdfPCell(new Phrase("T. Emfermedad: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.TiempoEnfermedad, fontColumnValue)),     
                    new PdfPCell(new Phrase("Inicio: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.InicioEnfermedad, fontColumnValue)),     
                    new PdfPCell(new Phrase("curso: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.CursoEnfermedad, fontColumnValue)){Colspan=3},     
                   
                    new PdfPCell(new Phrase("Signos y Síntomas: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_MainSymptom, fontColumnValue)){Colspan=7},                   
                    new PdfPCell(new Phrase("Enfermedad Actual(Motivo): ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_Story, fontColumnValue)){Colspan=7},                   
                

                           };

                columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion


                #region  Antecedentes Personales
                 cells = new List<PdfPCell>();

            if (listMedicoPersonales != null && listMedicoPersonales.Count > 0)
            {                
                foreach (var item in listMedicoPersonales)
                {
                    //Columna DX
                    cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna FECHA INICIO
                    cell = new PdfPCell(new Phrase(item.d_StartDate.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna DETALLE
                    cell = new PdfPCell(new Phrase(item.v_TreatmentSite, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                  
                }

                columnWidths = new float[] { 33f, 33f, 33f};

            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 33f, 33f, 33f };
            }

            columnHeaders = new string[] { "Diagnóstico", "Fecha Inicio", "Detalle"};

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "ANTECEDENTES PERSONALES", fontTitleTableNegro, columnHeaders);

            document.Add(table);

                #endregion

            #region  Antecedentes Familiares
            cells = new List<PdfPCell>();

            if (listaPatologicosFamiliares != null && listaPatologicosFamiliares.Count > 0)
            {
                foreach (var item in listaPatologicosFamiliares)
                {

                    //Columna Grupo Familiar
                    cell = new PdfPCell(new Phrase(item.v_TypeFamilyName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna DX
                    cell = new PdfPCell(new Phrase(item.v_DiseaseName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);


                    //Columna DETALLE
                    cell = new PdfPCell(new Phrase(item.v_Comment, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                }
                columnWidths = new float[] { 33f, 33f, 33f };

            }

            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 33f, 33f, 33f };
            }

            columnHeaders = new string[] { "Grupo Familiar", "Diagnostico", "Detalle" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "ANTECEDENTES FAMILIARES", fontTitleTableNegro, columnHeaders);

            document.Add(table);

            #endregion

            #region  Antecedentes Habitos Noscivos
            cells = new List<PdfPCell>();

            if (listaHabitoNocivos != null && listaHabitoNocivos.Count > 0)
            {
                foreach (var item in listaHabitoNocivos)
                {

                    //Columna Hábito
                    cell = new PdfPCell(new Phrase(item.v_NoxiousHabitsName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna Frecuencia
                    cell = new PdfPCell(new Phrase(item.v_FrecuenciaHabito, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);


                    //Columna Comentario
                    cell = new PdfPCell(new Phrase(item.v_Comment, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                }
                columnWidths = new float[] { 33f, 33f, 33f };

            }

            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 33f, 33f, 33f };
            }

            columnHeaders = new string[] { "Hábito", "Frecuencia", "Comentartio" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "HÁBITOS NOSCIVOS", fontTitleTableNegro, columnHeaders);

            document.Add(table);

            #endregion


            var xExamenFisico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID);
            string ValorExamenFisico = "";
            if (xExamenFisico != null)
            {
                ValorExamenFisico = xExamenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_HALLAZGOS_ID).v_Value1;
            }
            else
            {
                ValorExamenFisico = "";
            }

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Examen Físico: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, 
                    new PdfPCell(new Phrase(ValorExamenFisico, fontColumnValue)),                   
                };

            columnWidths = new float[] { 12.5f,88.5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTableNegro, null);

            document.Add(filiationWorker);

            #region  Diagnósticos
            cells = new List<PdfPCell>();

            if (ListDiagnosticRepository != null && ListDiagnosticRepository.Count > 0)
            {
                foreach (var item in ListDiagnosticRepository)
                {

                    //Columna DX
                    cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna XIE10
                    cell = new PdfPCell(new Phrase(item.v_Cie10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                }
                columnWidths = new float[] { 66f, 33f};

            }

            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 66f, 33f };
            }

            columnHeaders = new string[] { "Diagnósticos", "CIE10"};

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "DIAGNÓSTICOS", fontTitleTableNegro, columnHeaders);

            document.Add(table);

            #endregion

            #region Resultados

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Resultados: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(filiationData.v_ExaAuxResult, fontColumnValue)),                   
                };

            columnWidths = new float[] { 12.5f, 88.5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTableNegro, null);

            document.Add(filiationWorker);

            #endregion


            #region  Medicación
            cells = new List<PdfPCell>();

            if (ListMedicamentos != null && ListMedicamentos.Count > 0)
            {
                foreach (var item in ListMedicamentos)
                {
                  
                    cell = new PdfPCell(new Phrase(item.v_GenericName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

             
                    cell = new PdfPCell(new Phrase(item.v_PresentationName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                  
                    cell = new PdfPCell(new Phrase(item.v_Doses, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                  
                    cell = new PdfPCell(new Phrase(item.v_ViaName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                }

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 25f, 25f, 25f, 25f };
            }

            columnHeaders = new string[] { "Fármaco", "Presentación", "Dosis","Vía" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "MEDICACIÓN Y RECETA", fontTitleTableNegro, columnHeaders);

            document.Add(table);

            #endregion

            #region Reomendaciones

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Recomendaciones: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT }, new PdfPCell(new Phrase(Recomendaciones, fontColumnValue)),                   
                };

            columnWidths = new float[] { 20f, 80f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTableNegro, null);

            document.Add(filiationWorker);

            #endregion


            #region Firma
        

            #region Creando celdas de tipo Imagen y validando nulls

            // Firma del trabajador ***************************************************
            PdfPCell cellFirmaTrabajador = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador,40F));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase("Sin Firma Trabajador", fontColumnValue));

            // Huella del trabajador **************************************************
            PdfPCell cellHuellaTrabajador = null;

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, 40F));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase("Sin Huella", fontColumnValue));

            // Firma del doctor Auditor **************************************************

            PdfPCell cellFirma = null;

            if (filiationData.FirmaMedico != null)
                cellFirma = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaMedico, 50F));
            else
                cellFirma = new PdfPCell(new Phrase("Sin Firma", fontColumnValue));

            #endregion

            #region Crear tablas en duro (para la Firma y huella del trabajador)

            cells = new List<PdfPCell>();

            cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaTrabajador.FixedHeight = 80F;
            cells.Add(cellFirmaTrabajador);
            cells.Add(new PdfPCell(new Phrase("Firma del Examinado", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            columnWidths = new float[] { 100f };

            var tableFirmaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            //***********************************************

            cells = new List<PdfPCell>();

            cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellHuellaTrabajador.FixedHeight = 80F;
            cells.Add(cellHuellaTrabajador);
            cells.Add(new PdfPCell(new Phrase("Huella del Examinado", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            columnWidths = new float[] { 100f };

            var tableHuellaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            #endregion

            cells = new List<PdfPCell>();

            // 1 celda vacia              
            cells.Add(new PdfPCell(tableFirmaTrabajador));

            // 1 celda vacia
            cells.Add(new PdfPCell(tableHuellaTrabajador));

            // 2 celda
            cell = new PdfPCell(new Phrase("Firma y Sello Médico", fontColumnValue)) { Rowspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cells.Add(cell);

            // 3 celda (Imagen)
            cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirma.FixedHeight = 80F;
            cells.Add(cellFirma);

            cells.Add(new PdfPCell(new Phrase("Con la cual declara que la información declarada es veraz", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
            cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2 });

            columnWidths = new float[] { 25f, 25f, 20f, 30F };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, " ", fontTitleTable);

            document.Add(table);

            #endregion

                // step 5: we close the document
                document.Close();
                writer.Close();
                writer.Dispose();
                //RunFile(filePDF);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
