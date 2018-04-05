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
    public class ExamenesEspecialesReport
    {
        #region Report de Laboratorio

        public static void CreateLaboratorioReport(PacientList filiationData, List<ServiceComponentList> serviceComponent, organizationDto infoEmpresaPropietaria, string filePDF)
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

                Font fontTitle1 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));


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

                Paragraph cTitle = new Paragraph("EXAMENES ESPECIALES", fontTitle2);
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

                #region Datos personales del trabajador

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("PACIENTE:", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)),     
                    new PdfPCell(new Phrase("PUESTO:", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_CurrentOccupation, fontColumnValue)),     
                    new PdfPCell(new Phrase("FECHA ATENCIÓN:", fontColumnValue)), new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)),                                    
                };

                columnWidths = new float[] { 20f, 70f};

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, "I. DATOS PERSONALES", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                #region EXAMENES ESPECIALES

                cells = new List<PdfPCell>();

                // Subtitulo  ******************
                cell = new PdfPCell(new Phrase("EXAMENES ESPECIALES", fontSubTitleNegroNegrita))
                {
                    Colspan = 5,
                    BackgroundColor = subTitleBackGroundColor,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                };

                cells.Add(cell);
                //*****************************************

                var xBKDirecto = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BK_DIRECTO_ID);
                var xExamenElisa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_ELISA_ID);
                var xHepatitisA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEPATITIS_A_ID);
                var xHepatitisC = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEPATITIS_C_ID);
                var xVdrl = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.VDRL_ID);
                var xSubUnidad = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID);


                if (xBKDirecto != null)
                {
                    var MuestraBKDirecto = xBKDirecto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_MUESTRA);
                    var ColoracionBKDirecto = xBKDirecto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_COLORACION);
                    var ResultadosBKDirecto = xBKDirecto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_RESULTADOS);

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("BK - DIRECTO", fontTitleTableNegro)));
                cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("MUSTRA", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(MuestraBKDirecto.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("COLORACIÓN", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(ColoracionBKDirecto.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("RESULTADOS", fontTitleTableNegro)));
                cells.Add(new PdfPCell(new Phrase(ResultadosBKDirecto.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT });

                }


                if (xExamenElisa != null)
                {
                    var ResultadoExamenElisa = xExamenElisa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HIV);
                    var DeseableExamenElisa = xExamenElisa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE);
                    var ObsExamenElisa = xExamenElisa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_OBSERVACION);

                    // 1era fila
                cells.Add(new PdfPCell(new Phrase("EXAMEN DE ELISA", fontTitleTableNegro)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan=4, HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(ResultadoExamenElisa.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("DESEABLE", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(DeseableExamenElisa.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase("OBSERVACIÓN", fontTitleTableNegro)));
                    cells.Add(new PdfPCell(new Phrase(ObsExamenElisa.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT });

                }

                if (xHepatitisA != null)
                {
                    var ResultadoHepatitisA = xHepatitisA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A);
                    var DeseableHepatitisA = xHepatitisA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A_DESEABLE);
                    var ObsHepatitisA = xHepatitisA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A_OBSERVACION);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("HEPATITIS A", fontTitleTableNegro)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(ResultadoHepatitisA.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("DESEABLE", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(DeseableHepatitisA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase("OBSERVACIÓN", fontTitleTableNegro)));
                    cells.Add(new PdfPCell(new Phrase(ObsHepatitisA.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT });


                }

                if (xHepatitisC != null)
                {
                    var ResultadoHepatitisC = xHepatitisC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_C_REACTIVOS_HEPATITIS_C);
                    var DesableHepatitisC = xHepatitisC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_C_REACTIVOS_HEPATITIS_C_DESEABLE);
                    var ObsHepatitisC = xHepatitisC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_C_REACTIVOS_HEPATITIS_C_OBSERVACION);


                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("HEPATITIS C", fontTitleTableNegro)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(ResultadoHepatitisC.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("DESEABLE", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(DesableHepatitisC.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase("OBSERVACIÓN", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(ObsHepatitisC.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT });

                }

                if (xSubUnidad != null)
                    {
                        var ResultadoSubUnidad = xHepatitisC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_RESULTADO);
                        var DesableSubUnidad = xHepatitisC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_DESEABLE);
                        var ObsSubUnidad = xHepatitisC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_OBSERVACION);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("Sub Unidad Beta Culaitativo", fontTitleTableNegro)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(ResultadoSubUnidad==null ?"":ResultadoSubUnidad.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("DESEABLE", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(ResultadoSubUnidad == null ? "" : ResultadoSubUnidad.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase("OBSERVACIÓN", fontTitleTableNegro)));
                    cells.Add(new PdfPCell(new Phrase(ResultadoSubUnidad == null ? "" : ResultadoSubUnidad.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT });

                    }
                
                if (xVdrl != null)
                {
                    var ResultadoVdrl = xVdrl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VDRL_REACTIVOS_VDRL);
                    var DeseableVdrl = xVdrl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VDRL_REACTIVOS_VDRL_DESEABLE);
                    var ObservacionVdrl = xVdrl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VDRL_REACTIVOS_VDRL);


                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("VDRL", fontTitleTableNegro)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(ResultadoVdrl ==null?"":ResultadoVdrl.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("DESEABLE", fontTitleTableNegro)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(ResultadoVdrl == null ? "" : DeseableVdrl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                   
                }
                columnWidths = new float[] { 20f, 20f, 20f, 20f, 20f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                document.Add(table);

                #endregion

                #region Imprimir todos los examenes de laboratorio

                string[] orderPrint = new string[]
                { 
                    Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_ID, 
                    Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID,                  
                    Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID,
                    Sigesoft.Common.Constants.GLUCOSA_ID,                
                    Sigesoft.Common.Constants.COLESTEROL_ID,
                    Sigesoft.Common.Constants.COLESTEROL_HDL_ID,
                    Sigesoft.Common.Constants.COLESTEROL_LDL_ID,
                    Sigesoft.Common.Constants.COLESTEROL_VLDL_ID,
                    Sigesoft.Common.Constants.CREATININA_ID,
                    Sigesoft.Common.Constants.TEST_ESTEREOPSIS_ID,
                    Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ID,
                    Sigesoft.Common.Constants.PLOMO_SANGRE_ID,
                    Sigesoft.Common.Constants.TGO_ID,
                    Sigesoft.Common.Constants.TGP_ID,
                    Sigesoft.Common.Constants.UREA_ID,
                    Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_ID,
                    Sigesoft.Common.Constants.EXAMEN_ELISA_ID,
                    Sigesoft.Common.Constants.HEPATITIS_A_ID,
                    Sigesoft.Common.Constants.HEPATITIS_C_ID,
                    Sigesoft.Common.Constants.VDRL_ID,
                    Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_ID,
                    Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_ID,
                    Sigesoft.Common.Constants.BK_DIRECTO_ID,
                    Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID,

                };

                #endregion

                //Capturar la firma del medico que grabo en laboratorio
                var lab = serviceComponent.Find(p => p.i_CategoryId == (int)Sigesoft.Common.Consultorio.Laboratorio);

                ReportBuilderReportForLaboratorioReport(serviceComponent, orderPrint, fontTitleTable, fontSubTitleNegroNegrita, fontColumnValue, subTitleBackGroundColor, document);

                // Salto de linea
                document.Add(new Paragraph("\r\n"));

                #region Firma y sello Médico

                table = new PdfPTable(2);
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.WidthPercentage = 40;

                columnWidths = new float[] { 15f, 25f };
                table.SetWidths(columnWidths);

                PdfPCell cellFirma = null;

                if (lab != null)
                {
                    if (lab.FirmaMedico != null)
                        cellFirma = new PdfPCell(HandlingItextSharp.GetImage(lab.FirmaMedico, 40F));
                    else
                        cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));
                }
                else
                {
                    cellFirma = new PdfPCell();
                }

                cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirma.FixedHeight = 80F;

                cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                table.AddCell(cell);
                table.AddCell(cellFirma);

                document.Add(table);

                #endregion

                // step 5: we close the document
                document.Close();
                writer.Close();
                writer.Dispose();
                //RunFile(filePDF);

            }
            catch (DocumentException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }

        }

        private static void ReportBuilderReportForLaboratorioReport(List<ServiceComponentList> serviceComponent, string[] order, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor, Document document)
        {
            if (order != null)
            {
                var sortEntity = GetSortEntity(order, serviceComponent);

                if (sortEntity != null)
                {
                    foreach (var ent in sortEntity)
                    {
                        var table = TableBuilderReportForLaboratorioReport(ent, fontTitle, fontSubTitle, fontColumnValue, SubtitleBackgroundColor);

                        if (table != null)
                        {
                            if (ent.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID)
                            {
                                document.NewPage();
                            }

                            document.Add(table);
                        }
                    }
                }
            }
        }

        private static PdfPTable TableBuilderReportForLaboratorioReport(ServiceComponentList serviceComponent, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor)
        {
            PdfPTable table = null;
            List<PdfPCell> cells = null;
            PdfPCell cell = null;
            float[] columnWidths = null;
            Font fontColumnValueNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));


            //switch (serviceComponent.v_ComponentId)
            //{
            //    case Sigesoft.Common.Constants.BK_DIRECTO_ID:

            //        #region BK_DIRECTO

            //        cells = new List<PdfPCell>();

            //        // Subtitulo  ******************
            //        cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
            //        {
            //            Colspan = 4,
            //            BackgroundColor = SubtitleBackgroundColor,
            //            HorizontalAlignment = Element.ALIGN_CENTER,
            //        };

            //        cells.Add(cell);
            //        //*****************************************

            //        if (serviceComponent.ServiceComponentFields.Count > 0)
            //        {
            //            var muestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_MUESTRA);
            //            var colaboracion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_COLORACION);
            //            var resultados = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_RESULTADOS);

            //            // titulo
            //            cells.Add(new PdfPCell(new Phrase("Examen", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase("Resultado", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase("Valores Deseables", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase("Unidades", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            //            // 1era fila
            //            cells.Add(new PdfPCell(new Phrase("MUESTRA", fontColumnValue)));
            //            cells.Add(new PdfPCell(new Phrase(muestra == null ? string.Empty : muestra.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            //            // 2da fila
            //            cells.Add(new PdfPCell(new Phrase("COLABORACION", fontColumnValue)));
            //            cells.Add(new PdfPCell(new Phrase(colaboracion == null ? string.Empty : colaboracion.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            //            // 2da fila
            //            cells.Add(new PdfPCell(new Phrase("RESULTADOS", fontColumnValue)));
            //            cells.Add(new PdfPCell(new Phrase(resultados == null ? string.Empty : resultados.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            //            cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


            //            columnWidths = new float[] { 25f, 25f, 25f, 25f };
            //        }
            //        else
            //        {
            //            cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
            //            columnWidths = new float[] { 100f };
            //        }

            //        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

            //        #endregion

            //        break;
            //    default:

            //        break;
            //}

            return table;
        }

        #endregion

        #region Utils

        private static List<ServiceComponentList> GetSortEntity(string[] order, List<ServiceComponentList> serviceComponent)
        {
            List<ServiceComponentList> orderEntities = new List<ServiceComponentList>();

            foreach (var op in order)
            {
                var find = serviceComponent.Find(P => P.v_ComponentId == op);

                if (find != null)
                {
                    orderEntities.Add(find);

                    // Eliminar 
                    serviceComponent.Remove(find);
                }
            }

            // Unir la entidades

            orderEntities.AddRange(serviceComponent);

            return orderEntities;
        }

        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();

        }

        #endregion   
    }
}
