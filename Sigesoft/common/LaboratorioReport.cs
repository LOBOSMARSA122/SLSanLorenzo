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
    public class LaboratorioReport
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
                page.Dato = "ILAB_CLINICO/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
                //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
                writer.PageEvent = page;

                // step 3: we open the document
                document.Open();
                // step 4: we Add content to the document
                // we define some fonts

                #region Fonts

                Font fontTitle1 = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
               

                #endregion

                //#region Logo

                //Image logo = HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F);
                //PdfPTable headerTbl = new PdfPTable(1);
                //headerTbl.TotalWidth = writer.PageSize.Width;
                //PdfPCell cellLogo = new PdfPCell(logo);

                //cellLogo.VerticalAlignment = Element.ALIGN_TOP;
                //cellLogo.HorizontalAlignment = Element.ALIGN_CENTER;

                //cellLogo.Border = PdfPCell.NO_BORDER;
                //headerTbl.AddCell(cellLogo);
                //document.Add(headerTbl);

                //#endregion

                //#region Title

                //Paragraph cTitle = new Paragraph("LABORATORIO CLÍNICO", fontTitle2);
                ////Paragraph cTitle2 = new Paragraph(customerOrganizationName, fontTitle2);
                //cTitle.Alignment = Element.ALIGN_CENTER;
                ////cTitle2.Alignment = Element.ALIGN_CENTER;

                //document.Add(cTitle);
                ////document.Add(cTitle2);

                //#endregion

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
                document.Add(new Paragraph("\r\n"));
                //document.Add(new Paragraph("\r\n"));
               #region Title
                //List<PdfPCell> cells = null;
                PdfPCell CellLogo = null;
                //float[] columnWidths = null;
                cells = new List<PdfPCell>();
                PdfPCell cellPhoto1 = null;

                //if (filiationData.b_Photo != null)
                //    cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                //else
                //    cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

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
                new PdfPCell(new Phrase("LABORATORIO CLÍNICO", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
              
            };

                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                cells.Add(CellLogo);
                cells.Add(new PdfPCell(table));
                //cells.Add(cellPhoto1);

                columnWidths = new float[] { 20f, 80f};

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                document.Add(table);

                #endregion

                // Salto de linea
                //document.Add(new Paragraph("\r\n"));

                #region Datos personales del trabajador

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("PACIENTE:", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)),     
                    new PdfPCell(new Phrase("PUESTO:", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_CurrentOccupation, fontColumnValue)),     
                    new PdfPCell(new Phrase("FECHA ATENCIÓN:", fontColumnValue)), new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)),                
                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue ) ){ Colspan = 3 }

                         //cells.Add(new PdfPCell(new Phrase("FORMULA LEUCOCITARIA", fontColumnValueNegrita)) { Colspan = 4 });
                };

                columnWidths = new float[] { 15f, 35f, 15f, 35f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "I. DATOS PERSONALES", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                #region BIOQUIMICA

                cells = new List<PdfPCell>();

                var xAntigenoProstatico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ID);
                if (xAntigenoProstatico != null)
                {

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("INMUNOLÓGICO", fontSubTitleNegroNegrita))
                    {
                        Colspan = 4,
                        BackgroundColor = subTitleBackGroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    var antigenoprostatico = xAntigenoProstatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ANTIGENO_PROSTATICO_VALOR);
                    var antigenoprostaticoValord = xAntigenoProstatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_VALOR_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("ANTÍGENO PROSTÁTICO", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(antigenoprostatico == null ? string.Empty : antigenoprostatico.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(antigenoprostaticoValord == null ? string.Empty : antigenoprostaticoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("mg/dl", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }
                //else
                //{
                //    cells.Add(new PdfPCell(new Phrase("SIN EXAMEN", fontColumnValue)) { Colspan=4});
                //}


                // Subtitulo  ******************
                cell = new PdfPCell(new Phrase("BIOQUÍMICA", fontSubTitleNegroNegrita))
                {
                    Colspan = 4,
                    BackgroundColor = subTitleBackGroundColor,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                };

                cells.Add(cell);
                //*****************************************

                // titulo
                cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("UNIDADES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                
                var xColesterol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_ID);
                var xTrigliceridos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TRIGLICERIDOS_ID);
                var xColesterolHdl = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_HDL_ID);
                var xColesterolLdl = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_LDL_ID);
                var xColesterolVldl = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_VLDL_ID);
                var xUrea = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.UREA_ID);
                var xCreatinina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CREATININA_ID);
                var xTgo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TGO_ID);
                var xTgp = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TGP_ID);
                var xAcidoUrico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA01_ID);
              
                var xPlomoSangre = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_SANGRE_ID);


                var xBioquimico01 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA01_ID);
                var xBioquimico02 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA02_ID);
                var xBioquimico03 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA03_ID);


                var xMICROALBUMINURIA_ID = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.MICROALBUMINURIA_ID);
                var xEXAMEN_ELISA_ID = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_ELISA_ID);
                var xHBsAg_ID = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HBsAg_ID);
                var xHCV_ID = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HCV_ID);
                var xGlucosa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GLUCOSA_ID);
                var xINSULINA_BASAL_ID = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INSULINA_BASAL_ID);





                if (xMICROALBUMINURIA_ID != null)
                {
                    var CAMPO_MICROALBUMINURIA = xMICROALBUMINURIA_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_MICROALBUMINURIA);
                    var CAMPO_MICROALBUMINURIA_DESABLE = xMICROALBUMINURIA_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_MICROALBUMINURIA_DESABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("MICROALBUMINURIA", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(CAMPO_MICROALBUMINURIA == null ? string.Empty : CAMPO_MICROALBUMINURIA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_MICROALBUMINURIA_DESABLE == null ? string.Empty : CAMPO_MICROALBUMINURIA_DESABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_MICROALBUMINURIA == null ? string.Empty : CAMPO_MICROALBUMINURIA.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }


                if (xEXAMEN_ELISA_ID != null)
                {
                    var CAMPO_HIV = xEXAMEN_ELISA_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HIV);
                    var EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE = xEXAMEN_ELISA_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("H I V", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(CAMPO_HIV == null ? string.Empty : CAMPO_HIV.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE == null ? string.Empty : EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_HIV == null ? string.Empty : CAMPO_HIV.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xHBsAg_ID != null)
                {
                    var CAMPO_HBsAg = xHBsAg_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HBsAg);
                    var CAMPO_HBsAg_DESEABLE = xHBsAg_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HBsAg_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("HBsAg", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(CAMPO_HBsAg == null ? string.Empty : CAMPO_HBsAg.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_HBsAg_DESEABLE == null ? string.Empty : CAMPO_HBsAg_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_HBsAg == null ? string.Empty : CAMPO_HBsAg.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }


                if (xHCV_ID != null)
                {
                    var CAMPO_HCV = xHCV_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HCV);
                    var CAMPO_HCV_DESEABLE = xHCV_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HCV_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("H C V", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(CAMPO_HCV == null ? string.Empty : CAMPO_HCV.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_HCV_DESEABLE == null ? string.Empty : CAMPO_HCV_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_HCV == null ? string.Empty : CAMPO_HCV.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }


                if (xINSULINA_BASAL_ID != null)
                {
                    var CAMPO_INSULINA_BASAL = xINSULINA_BASAL_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_INSULINA_BASAL);
                    var CAMPO_INSULINA_BASAL_DESEABLE = xINSULINA_BASAL_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_INSULINA_BASAL_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("INSULINA BASAL", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(CAMPO_INSULINA_BASAL == null ? string.Empty : CAMPO_INSULINA_BASAL.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_INSULINA_BASAL_DESEABLE == null ? string.Empty : CAMPO_INSULINA_BASAL_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(CAMPO_INSULINA_BASAL == null ? string.Empty : CAMPO_INSULINA_BASAL.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }


                if (xGlucosa != null)
                {                   
                    var glucosa = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DESCRIPCION);
                    var glucosaValord = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_DESEABLE_ID);

                    var GLUCOSA_POST_PANDRIAL = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_POST_PANDRIAL);
                    var GLUCOSA_POST_DESEABLE = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_POST_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("GLUCOSA", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(glucosaValord == null ? string.Empty : glucosaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    // 2DA fila
                    cells.Add(new PdfPCell(new Phrase("GLUCOSA POST-PANDRIAL", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(GLUCOSA_POST_PANDRIAL == null ? string.Empty : GLUCOSA_POST_PANDRIAL.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(GLUCOSA_POST_DESEABLE == null ? string.Empty : GLUCOSA_POST_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(GLUCOSA_POST_PANDRIAL == null ? string.Empty : GLUCOSA_POST_PANDRIAL.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                   
                }

                if (xColesterol != null)
                {
                    var colesterol = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_ID);
                    var colesterolValord = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_DESEABLE_ID);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("COLESTEROL", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(colesterolValord == null ? string.Empty : colesterolValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }


                if (xTrigliceridos != null)
                {
                    var Triglicerido = xTrigliceridos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS);
                    var TrigliceridoValord = xTrigliceridos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(Triglicerido == null ? string.Empty : Triglicerido.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(TrigliceridoValord == null ? string.Empty : TrigliceridoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(Triglicerido == null ? string.Empty : Triglicerido.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }

                if (xColesterolHdl != null)
                {
                    var colesterolHdl = xColesterolHdl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_HDL);
                    var colesterolHdlValord = xColesterolHdl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_HDL_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("COLESTEROL HDL", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(colesterolHdl == null ? string.Empty : colesterolHdl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(colesterolHdlValord == null ? string.Empty : colesterolHdlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                   
                    cells.Add(new PdfPCell(new Phrase(colesterolHdl == null ? string.Empty : colesterolHdl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }

                if (xColesterolHdl != null)
                {
                    var colesteroTotallHdl = xColesterolHdl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_TOTAL_HDL);
                    var colesterolTotalHdlValord = xColesterolHdl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_TOTAL_HDL_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("COLESTEROL TOTAL / HDL", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(colesteroTotallHdl == null ? string.Empty : colesteroTotallHdl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(colesterolTotalHdlValord == null ? string.Empty : colesterolTotalHdlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                  
                    cells.Add(new PdfPCell(new Phrase(colesteroTotallHdl == null ? string.Empty : colesteroTotallHdl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }
                
                if (xColesterolLdl != null)
                {
                    var colesterolLdl = xColesterolLdl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL);
                    var colesterolLdlValord = xColesterolLdl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("LDL", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(colesterolLdl == null ? string.Empty : colesterolLdl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(colesterolLdlValord == null ? string.Empty : colesterolLdlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                  
                    cells.Add(new PdfPCell(new Phrase(colesterolLdl == null ? string.Empty : colesterolLdl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xColesterolLdl != null)
                {
                    var colesterolVLdl = xColesterolLdl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL_HDL);
                    var colesterolVLdlValord = xColesterolLdl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL_HDL_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("LDL", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(colesterolVLdl == null ? string.Empty : colesterolVLdl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(colesterolVLdlValord == null ? string.Empty : colesterolVLdlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                  
                    cells.Add(new PdfPCell(new Phrase(colesterolVLdl == null ? string.Empty : colesterolVLdl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xColesterolVldl != null)
                {
                    var colesterolVldl = xColesterolVldl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL);
                    var colesterolVldlValord = xColesterolVldl.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("VLDL", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(colesterolVldl == null ? string.Empty : colesterolVldl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(colesterolVldlValord == null ? string.Empty : colesterolVldlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                  
                    cells.Add(new PdfPCell(new Phrase(colesterolVldl == null ? string.Empty : colesterolVldl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xUrea != null)
                {
                    var urea = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA);
                    var ureaValord = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("ÚREA", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(urea == null ? string.Empty : urea.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(ureaValord == null ? string.Empty : ureaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                   
                    cells.Add(new PdfPCell(new Phrase(urea == null ? string.Empty : urea.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xCreatinina != null)
                {
                    var creatinina = xCreatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA);
                    var creatininaValord = xCreatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("CREATININA", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(creatinina == null ? string.Empty : creatinina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(creatininaValord == null ? string.Empty : creatininaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                   
                    cells.Add(new PdfPCell(new Phrase(creatinina == null ? string.Empty : creatinina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }
                if (xTgo != null)
                {
                    var tgo = xTgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGO_BIOQUIMICA_TGO);
                    var tgoValord = xTgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGO_BIOQUIMICA_TGO_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("TGO", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(tgo == null ? string.Empty : tgo.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(tgoValord == null ? string.Empty : tgoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    
                    cells.Add(new PdfPCell(new Phrase(tgo == null ? string.Empty : tgo.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xTgp != null)
                {
                    var tgp = xTgp.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGP_BIOQUIMICA_TGP);
                    var tgpValord = xTgp.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGP_BIOQUIMICA_TGP_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("TGP", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(tgp == null ? string.Empty : tgp.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(tgpValord == null ? string.Empty : tgpValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                   
                    cells.Add(new PdfPCell(new Phrase(tgp == null ? string.Empty : tgp.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xAcidoUrico != null)
                {
                    var acidourico = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO);
                    var acidouricoValord = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("ÁCIDO ÚRICO", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(acidourico == null ? string.Empty : acidourico.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(acidouricoValord == null ? string.Empty : acidouricoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    
                    cells.Add(new PdfPCell(new Phrase(acidourico == null ? string.Empty : acidourico.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                //if (xAntigenoProstatico != null)
                //{
                //    var antigenoprostatico = xAntigenoProstatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ANTIGENO_PROSTATICO_VALOR);
                //    var antigenoprostaticoValord = xAntigenoProstatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_VALOR_DESEABLE);

                //    // 1era fila
                //    cells.Add(new PdfPCell(new Phrase("ANTÍGENO PROSTÁTICO", fontColumnValue)));
                //    cells.Add(new PdfPCell(new Phrase(antigenoprostatico == null ? string.Empty : antigenoprostatico.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //    cells.Add(new PdfPCell(new Phrase(antigenoprostaticoValord == null ? string.Empty : antigenoprostaticoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //    cells.Add(new PdfPCell(new Phrase("mg/dl", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                //}

                if (xPlomoSangre != null)
                {
                    var plomoensangre = xPlomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE);
                    var plomoensangreValord = xPlomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("PLOMO EN SANGRE", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(plomoensangre == null ? string.Empty : plomoensangre.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(plomoensangreValord == null ? string.Empty : plomoensangreValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                
                    cells.Add(new PdfPCell(new Phrase(plomoensangre == null ? string.Empty : plomoensangre.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                //if (xBioquimico01 != null)
                //{
                //    var Bioquimico01 = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA01_VALOR);
                //    var BioquimicoValord01 = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA01_VALOR_DESEABLE);

                //    // 1era fila
                //    cells.Add(new PdfPCell(new Phrase("BIOQUÍMICA 01", fontColumnValue)));
                //    cells.Add(new PdfPCell(new Phrase(Bioquimico01 == null ? string.Empty : Bioquimico01.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //    cells.Add(new PdfPCell(new Phrase(BioquimicoValord01 == null ? string.Empty : BioquimicoValord01.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 
                //    cells.Add(new PdfPCell(new Phrase(Bioquimico01 == null ? string.Empty : Bioquimico01.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //}


                if (xBioquimico02 != null)
                {
                    var Bioquimico02 = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA02_VALOR);
                    var BioquimicoValord02 = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA02_VALOR_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("BIOQUÍMICA 02", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(Bioquimico02 == null ? string.Empty : Bioquimico02.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(BioquimicoValord02 == null ? string.Empty : BioquimicoValord02.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                  
                    cells.Add(new PdfPCell(new Phrase(Bioquimico02 == null ? string.Empty : Bioquimico02.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xBioquimico03 != null)
                {
                    var Bioquimico03 = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA03_VALOR);
                    var BioquimicoValord03 = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA03_VALOR_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("BIOQUÍMICA 02", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(Bioquimico03 == null ? string.Empty : Bioquimico03.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(BioquimicoValord03 == null ? string.Empty : BioquimicoValord03.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                  
                    cells.Add(new PdfPCell(new Phrase(Bioquimico03 == null ? string.Empty : Bioquimico03.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }


                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                document.Add(table);

                #endregion

                //string[] HemoglobinaHematocrito = new string[]
                //{ 
                //    Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_ID, 
                //    Sigesoft.Common.Constants.LABORATORIO_HEMATOCRITO_ID             
                    
                //};
                var xHemoglobina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_ID);
                var xHematocrito = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_HEMATOCRITO_ID);

                if (xHemoglobina != null || xHematocrito!= null)
                {

                    cells = new List<PdfPCell>();
                #region Hemoglobina - Hematocrito
                // Subtitulo  ******************
                cell = new PdfPCell(new Phrase("HEMOGLOBINA - HEMATOCRITO", fontSubTitleNegroNegrita))
                {
                    Colspan = 4,
                    BackgroundColor = subTitleBackGroundColor,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                };

                cells.Add(cell);
                //*****************************************

                // titulo
                cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("UNIDADES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

               
                if (xHemoglobina != null)
                {
                    var Hemoglobina = xHemoglobina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_ID);
                    var HemoglobinaValord = xHemoglobina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_VALOR_DESEABLE_ID);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(Hemoglobina == null ? string.Empty : Hemoglobina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(HemoglobinaValord == null ? string.Empty : HemoglobinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase(Hemoglobina == null ? string.Empty : Hemoglobina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                if (xHematocrito != null)
                {
                    var Hematocrito = xHematocrito.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_ID);
                    var HematocritoValord = xHematocrito.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_HEMOGRAMA_HEMATOCRITO_DESEABLE);

                    // 1era fila
                    cells.Add(new PdfPCell(new Phrase("HEMATOCRITO", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(Hematocrito == null ? string.Empty : Hematocrito.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(HematocritoValord == null ? string.Empty : HematocritoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    cells.Add(new PdfPCell(new Phrase(Hematocrito == null ? string.Empty : Hematocrito.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                document.Add(table);
                #endregion
                }

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
                    Sigesoft.Common.Constants.BIOQUIMICA01_ID,
                    Sigesoft.Common.Constants.BIOQUIMICA02_ID,
                    Sigesoft.Common.Constants.BIOQUIMICA03_ID,
                    Sigesoft.Common.Constants.PERFIL_LIPIDICO,
                    Sigesoft.Common.Constants.PERFIL_HEPATICO_ID,
                    Sigesoft.Common.Constants.HEMOGRAMA
                };
           
                #endregion

                //Capturar la firma del medico que grabo en laboratorio
                var lab = serviceComponent.Find(p => p.i_CategoryId == (int)Sigesoft.Common.Consultorio.Laboratorio);

                ReportBuilderReportForLaboratorioReport(serviceComponent, orderPrint, fontTitleTable, fontSubTitleNegroNegrita, fontColumnValue, subTitleBackGroundColor, document);

              
                //Obs
                var xobs = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_LABORATORIO_ID);
                if (xobs != null)
                {
            
                var Obs = xobs.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_HALLAZGO_PATOLOGICO_LABORATORIO_ID);
               
                cells = new List<PdfPCell>()
                {

                   new PdfPCell(new Phrase("OBSERVACIONES:", fontColumnValue)), new PdfPCell(new Phrase(Obs == null ?"": Obs.v_Value1, fontColumnValue)),        
                };

                columnWidths = new float[] {100f};

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTableNegro, null);

                document.Add(filiationWorker);
                }

                // Salto de linea
                
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
                        cellFirma = new PdfPCell(HandlingItextSharp.GetImage(lab.FirmaMedico, null, null, 120, 45));
                    else
                        cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));
                }
                else
                {
                    cellFirma = new PdfPCell();
                }
                
                cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirma.FixedHeight = 60F;

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
                            //if (ent.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID)
                            //{
                            //    document.NewPage();
                            //}

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
            Font fontColumnValueNegrita = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));


            switch (serviceComponent.v_ComponentId)
            {
                case Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_ID:

                    #region HEMOGRAMA_COMPLETO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var hematocritos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO);
                        var hematocritosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO_DESEABLE);
                        var hemoglobina = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA);
                        var hemoglobinaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA_DESEABLE);
                        var globulosRojos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATIES);
                        var globulosRojosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATIES_DESEABLE);
                        var leucocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS);
                        var leucocitosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS_DESEABLE);
                        var plaquetas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_PLAQUETAS);
                        var plaquetasValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_PLAQUETAS_DESEABLE);

                        var abastonados = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_ABASTONADOS);
                        var abastonadosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_ABASTONADOS_DESEABLE);
                        var segmentados = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_SEGMENTADOS);
                        var segmentadosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_SEGMENTADOS_DESEABLE);
                        var eosinofilos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS);
                        var eosinofilosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS_DESEABLE);
                        var basofilos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_BASOFILOS);
                        var basofilosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_BASOFILOS_DESEABLE);
                        var monocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_MONOCITOS);
                        var monocitosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_MONOCITOS_DESEABLE);
                        var linfocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_LINFOCITOS);
                        var linfocitosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_LINFOCITOS_DESEABLE);
                        var observaciones = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_OBSERVACIONES);
                        
                        // titulo
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("UNIDADES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("HEMATOCRITO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hematocritos == null ? string.Empty : hematocritos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hematocritosValord == null ? string.Empty : hematocritosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("%", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hemoglobina == null ? string.Empty : hemoglobina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hemoglobinaValord == null ? string.Empty : hemoglobinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("g/di", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("GLÓBULOS ROJOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(globulosRojos == null ? string.Empty : globulosRojos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(globulosRojosValord == null ? string.Empty : globulosRojosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("x106/mm3", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(leucocitosValord == null ? string.Empty : leucocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("x103/mm3", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("RECUENTO PLAQUETAS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(plaquetas == null ? string.Empty : plaquetas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(plaquetasValord == null ? string.Empty : plaquetasValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("x103/mm3", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("FÓRMULA LEUCOCITARIA", fontColumnValueNegrita)) { Colspan = 4 });
                      
                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("ABASTONADOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(abastonados == null ? string.Empty : abastonados.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(abastonadosValord == null ? string.Empty : abastonadosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("%", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("SEGMENTADOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(segmentados == null ? string.Empty : segmentados.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(segmentadosValord == null ? string.Empty : segmentadosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("%", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("EOSINÓFILOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(eosinofilos == null ? string.Empty : eosinofilos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(eosinofilosValord == null ? string.Empty : eosinofilosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("%", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("BASÓFILOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(basofilos == null ? string.Empty : basofilos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(basofilosValord == null ? string.Empty : basofilosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("%", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("MONOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(monocitos == null ? string.Empty : monocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(monocitosValord == null ? string.Empty : monocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("%", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("LINFOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(linfocitos == null ? string.Empty : linfocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(linfocitosValord == null ? string.Empty : linfocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("%", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                 
                        // 12va fila
                        cells.Add(new PdfPCell(new Phrase("OBSERVACIONES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(observaciones == null ? string.Empty : observaciones.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                  table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.PERFIL_LIPIDICO:
                    #region PERFIL_LIPIDICO
                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var colesteroltotal = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL);
                        var colesteroltotalValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL_DESEABLE);

                        var trigliceridos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS);
                        var trigliceridosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_DESEABLE);

                        var colesterolldl = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL);
                        var colesterolldlValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_DESEABLE);

                        var colesterolhdl = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL);
                        var colesterolhdlValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_DESEABLE);


                        var colesterolvldl = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL);
                        var colesterolvldlValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL_DESEABLE);

                        // titulo
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("UNIDADES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL TOTAL", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(colesteroltotal == null ? string.Empty : colesteroltotal.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesteroltotalValord == null ? string.Empty : colesteroltotalValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesteroltotal == null ? string.Empty : colesteroltotal.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(trigliceridos == null ? string.Empty : trigliceridos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(trigliceridosValord == null ? string.Empty : trigliceridosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(trigliceridos == null ? string.Empty : trigliceridos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL LDL", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(colesterolldl == null ? string.Empty : colesterolldl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterolldlValord == null ? string.Empty : colesterolldlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterolldl == null ? string.Empty : colesterolldl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL HDL", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(colesterolhdl == null ? string.Empty : colesterolhdl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterolhdlValord == null ? string.Empty : colesterolhdlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterolhdl == null ? string.Empty : colesterolhdl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL VLDL", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(colesterolvldl == null ? string.Empty : colesterolvldl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterolvldlValord == null ? string.Empty : colesterolvldlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterolvldl == null ? string.Empty : colesterolvldl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);


                    #endregion
                    break;


                case Sigesoft.Common.Constants.PERFIL_HEPATICO_ID:
                    #region PERFIL_HEPATICO_ID
                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {

                        var proteinastotales = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_PROTEINAS_TOTALES_ID);
                        var proteinastotalesValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_PROTEINA_TOTALES_DESEABLE_ID);

                        var albumina = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ALBUMINA_ID);
                        var albuminaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ALBUMINA_DESEABLE_ID);

                        var globulina = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GLOBULINA_ID);
                        var globulinaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GLOBULINA_DESEABLE_ID);

                        var tgo = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGO_ID);
                        var tgoValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGO_DESEABLE_ID);

                        var tgp = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_ID);
                        var tgpValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_DESEABLE_ID);

                        var fosfataalcalina = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_FOSFATASA_ALCALINA_ID);
                        var fosfataalcalinaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_FOSFATASA_ALCALINA_DESEABLE_ID);

                        var gamma = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GAMMA_GLUTAMIL_TRANSPEPTIDASA_ID);
                        var gammaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GGTP_DESEABLE_ID);

                        var btotal = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_TOTAL_ID);
                        var btotalValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_TOTAL_DESEABLE_ID);

                        var bdirecta = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_DIRECTA_ID);
                        var bdirectaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_DIRECTA_DESEABLE_ID);

                        var bindirecta = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_INDIRECTA_ID);
                        var bindirectaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_INDIRECTA_DESEABLE_ID);

                        // titulo
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("UNIDADES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("PROTEÍNAS TOTALES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(proteinastotales == null ? string.Empty : proteinastotales.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(proteinastotalesValord == null ? string.Empty : proteinastotalesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(proteinastotales == null ? string.Empty : proteinastotales.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("ALBÚMINA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(albumina == null ? string.Empty : albumina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(albuminaValord == null ? string.Empty : albuminaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(albumina == null ? string.Empty : albumina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("GLOBULINA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(globulina == null ? string.Empty : globulina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(globulinaValord == null ? string.Empty : globulinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(globulina == null ? string.Empty : globulina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TGO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(tgo == null ? string.Empty : tgo.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(tgoValord == null ? string.Empty : tgoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(tgo == null ? string.Empty : tgo.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TGP", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(tgp == null ? string.Empty : tgp.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(tgpValord == null ? string.Empty : tgpValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(tgp == null ? string.Empty : tgp.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("FOSFATASA ALCALINA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(fosfataalcalina == null ? string.Empty : fosfataalcalina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(fosfataalcalinaValord == null ? string.Empty : fosfataalcalinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(fosfataalcalina == null ? string.Empty : fosfataalcalina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("GAMMA GLUTAMIL TRANSPEPTIDASA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(gamma == null ? string.Empty : gamma.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(gammaValord == null ? string.Empty : gammaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(gamma == null ? string.Empty : gamma.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("BILIRRUBINA TOTAL", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(btotal == null ? string.Empty : btotal.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(btotalValord == null ? string.Empty : btotalValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(btotal == null ? string.Empty : btotal.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("BILIRRUBINA DIRECTA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(bdirecta == null ? string.Empty : bdirecta.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(bdirectaValord == null ? string.Empty : bdirectaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(bdirecta == null ? string.Empty : bdirecta.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("BILIRRUBINA INDIRECTA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(bindirecta == null ? string.Empty : bindirecta.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(bindirectaValord == null ? string.Empty : bindirectaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(bindirecta == null ? string.Empty : bindirecta.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion
                    break;


                case Sigesoft.Common.Constants.HEMOGRAMA:
                    #region HEMOGRAMA
                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var hemoglobina = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA);
                        var hemoglobinaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_DESEABLE);

                        var hematocrito = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO);
                        var hematocritoValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_DESEABLE);

                        var hematies = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATIES);
                        var hematiesValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATIES_DESEABLE);

                        var volcorpmedio = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_CORP_MEDIO);
                        var volcorpmedioValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_CORP_MEDIO_DESEABLE);

                        var hbcorpmedio = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HB_CORP_MEDIO);
                        var hbcorpmedioValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HB_CORP_MEDIO_DESEABLE);

                        var cehbmedio = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CE_HB_MEDIO);
                        var cehbmedioValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CE_HB_MEDIO_DESEABLE);

                        var plaquetas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLAQUETAS);
                        var plaquetasValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLAQUETAS_DESEABLE);

                        var volplaquetariomedio = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_PLAQUETARIO_MEDIO);
                        var volplaquetariomedioValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_PLAQUETARIO_MEDIO_DESEABLE);

                        var leucocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEUCOCITOS_TOTALES);
                        var leucocitosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEUCOCITOS_TOTALES_DESEABLE);

                        var linfocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEUCOCITOS_TOTALES);
                        var linfocitosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEUCOCITOS_TOTALES_DESEABLE);

                        var mid = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID);
                        var midValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_DESEABLE);

                        var neutrofilos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_SEG);
                        var neutrofilosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_SEG_DESEABLE);

                        var linfocitos_10_9 = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS_10_9);
                        var linfocitos_10_9Valord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS_10_9_DESEABLE);

                        var mid_10_9 = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_10_9);
                        var mid_10_9Valord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_10_9_DESEABLE);

                        var neutrofilos_10_9 = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_10_9);
                        var neutrofilos_10_9Valord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_10_9_DESEABLE);



                        // titulo
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("UNIDADES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hemoglobina == null ? string.Empty : hemoglobina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hemoglobinaValord == null ? string.Empty : hemoglobinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hemoglobina == null ? string.Empty : hemoglobina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("HEMATOCRITO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hematocrito == null ? string.Empty : hematocrito.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hematocritoValord == null ? string.Empty : hematocritoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hematocrito == null ? string.Empty : hematocrito.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("HEMATÍES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hematiesValord == null ? string.Empty : hematiesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("VOLUMEN CORPUSCULAR MEDIO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(volcorpmedio == null ? string.Empty : volcorpmedio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(volcorpmedioValord == null ? string.Empty : volcorpmedioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(volcorpmedio == null ? string.Empty : volcorpmedio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("HB CORPUSC. MEDIA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hbcorpmedio == null ? string.Empty : hbcorpmedio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hbcorpmedioValord == null ? string.Empty : hbcorpmedioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hbcorpmedio == null ? string.Empty : hbcorpmedio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("CE. HB COPUSC.", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(cehbmedio == null ? string.Empty : cehbmedio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(cehbmedioValord == null ? string.Empty : cehbmedioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(cehbmedio == null ? string.Empty : cehbmedio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("PLAQUETAS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(plaquetas == null ? string.Empty : plaquetas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(plaquetasValord == null ? string.Empty : plaquetasValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(plaquetas == null ? string.Empty : plaquetas.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("VOL. PLAQUETARIO MEDIO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(volplaquetariomedio == null ? string.Empty : volplaquetariomedio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(volplaquetariomedioValord == null ? string.Empty : volplaquetariomedioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(volplaquetariomedio == null ? string.Empty : volplaquetariomedio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS TOTALES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(leucocitosValord == null ? string.Empty : leucocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("LINFOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(linfocitos == null ? string.Empty : linfocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(linfocitosValord == null ? string.Empty : linfocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(linfocitos == null ? string.Empty : linfocitos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("MID(BAS. EOS. MON)", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(mid == null ? string.Empty : mid.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(midValord == null ? string.Empty : midValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(mid == null ? string.Empty : mid.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("NEUTRÓFILS SEGMENTADOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(neutrofilos == null ? string.Empty : neutrofilos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(neutrofilosValord == null ? string.Empty : neutrofilosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(neutrofilos == null ? string.Empty : neutrofilos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("LINFOCITOS(10*9)", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(linfocitos_10_9 == null ? string.Empty : linfocitos_10_9.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(linfocitos_10_9Valord == null ? string.Empty : linfocitos_10_9Valord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(linfocitos_10_9 == null ? string.Empty : linfocitos_10_9.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("MID (BAS. EOS. MON)(10*9)", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(mid_10_9 == null ? string.Empty : mid_10_9.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(mid_10_9Valord == null ? string.Empty : mid_10_9Valord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(mid_10_9 == null ? string.Empty : mid_10_9.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("NEUTRÓFILOS(10*9)", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(neutrofilos_10_9 == null ? string.Empty : neutrofilos_10_9.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(neutrofilos_10_9Valord == null ? string.Empty : neutrofilos_10_9Valord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(neutrofilos_10_9 == null ? string.Empty : neutrofilos_10_9.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);


                    #endregion
                    break;
                    

                case Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID:

                    #region EXAMEN_COMPLETO_DE_ORINA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var color = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_COLOR);
                        var colorValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_COLOR_DESEABLE);

                        //var Leucocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS);
                        //var LeucocitosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS_DESEABLE);


                        var aspecto = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_ASPECTO);
                        var aspectoValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_ASPECTO_DESEABLE);

                        var densidad = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_DENSIDAD);
                        var densidadValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_DENSIDAD_DESEABLE);

                        var ph = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_PH);
                        var phValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_PH_DESEABLE);

                        var celulasEpiteleales = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CELULAS_EPITELIALES);
                        var celulasEpitelealesValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CELULAS_EPITELIALES_DESEABLE);

                        var leucocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS);
                        var leucocitosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS_DESEABLE);

                        var hematies = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES);
                        var hematiesValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES_DESEABLE);

                        var germenes = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_GERMENES);
                        var germenesValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_GERMENES_DESEABLE);

                        var cilindros = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CILINDROS);
                        var cilindrosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CILINDROS_DESEABLE);

                        var cristales = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CRISTALES);
                        var cristalesValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CRISTALES_DESEABLE);

                        var filamentoMucoide = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_FILAMENTO_MUCOIDE);
                        var filamentoMucoideValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_FILAMENTO_MUCOIDE_DESEABLE);

                        var nitrittos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_NITRITOS);
                        var nitrittosValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_NITRITOS_DESEABLE);

                        var proteinas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_PROTEINAS);
                        var proteinasValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_PROTEINAS_DESEABLE);

                        var glucosa = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_GLUCOSA);
                        var glucosaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_GLUCOSA_DESEABLE);

                        var cetonas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_CETONAS);
                        var cetonasValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_CETONAS_DESEABLE);

                        var urobilinogeno = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_UROBILINOGENO);
                        var urobilinogenoValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_UROBILINOGENO_DESEABLE);

                        var bilirrubinas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_BILIRRUBINA);
                        var bilirrubinasValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_BILIRRUBINA_DESEABLE);

                        var sangre = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_SANGRE);
                        var sangreValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_SANGRE_DESEABLE);

                        var hemoglobina = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA);
                        var hemoglobinaValord = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA_DESEABLE);
              

                        // titulo
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("EXAMEN FÍSICO", fontColumnValueNegrita)) { Colspan = 4 });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(color == null ? string.Empty : color.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colorValord == null ? string.Empty : colorValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("ASPECTO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(aspecto == null ? string.Empty : aspecto.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(aspectoValord == null ? string.Empty : aspectoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("DENSIDAD", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(densidad == null ? string.Empty : densidad.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(densidadValord == null ? string.Empty : densidadValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("PH", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(ph == null ? string.Empty : ph.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(phValord == null ? string.Empty : phValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("SEDIMENTO URINARIO", fontColumnValueNegrita)) { Colspan = 4 });
                        
                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("CÉLULAS EPITELIALES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(celulasEpiteleales == null ? string.Empty : celulasEpiteleales.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(celulasEpitelealesValord == null ? string.Empty : celulasEpitelealesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(celulasEpiteleales == null ? string.Empty : celulasEpiteleales.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                       
                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(leucocitosValord == null ? string.Empty : leucocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(celulasEpiteleales == null ? string.Empty : celulasEpiteleales.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("HEMATÍES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hematiesValord == null ? string.Empty : hematiesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("GÉRMENES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(germenes == null ? string.Empty : germenes.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(germenesValord == null ? string.Empty : germenesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(germenes == null ? string.Empty : germenes.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("CILINDROS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(cilindros == null ? string.Empty : cilindros.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(cilindrosValord == null ? string.Empty : cilindrosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(cilindros == null ? string.Empty : cilindros.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("CRISTALES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(cristales == null ? string.Empty : cristales.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(cristalesValord == null ? string.Empty : cristalesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(cristales == null ? string.Empty : cristales.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("FILAMENTO MUCOIDE", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(filamentoMucoide == null ? string.Empty : filamentoMucoide.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(filamentoMucoideValord == null ? string.Empty : filamentoMucoideValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(filamentoMucoide == null ? string.Empty : filamentoMucoide.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("EXAMEN BIOQUÍMICO", fontColumnValueNegrita)) { Colspan = 4 });
                        
                        // 12va fila
                        cells.Add(new PdfPCell(new Phrase("NITRITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(nitrittos == null ? string.Empty : nitrittos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(nitrittosValord == null ? string.Empty : nitrittosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("PROTEÍNAS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(proteinas == null ? string.Empty : proteinas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(proteinasValord == null ? string.Empty : proteinasValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("GLUCOSA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(glucosaValord == null ? string.Empty : glucosaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("CETONAS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(cetonas == null ? string.Empty : cetonas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(cetonasValord == null ? string.Empty : cetonasValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("UROBILINÓGENO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(urobilinogeno == null ? string.Empty : urobilinogeno.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(urobilinogenoValord == null ? string.Empty : urobilinogenoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("BILIRRUBINAS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(bilirrubinas == null ? string.Empty : bilirrubinas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(bilirrubinasValord == null ? string.Empty : bilirrubinasValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("SANGRE", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(sangre == null ? string.Empty : sangre.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(sangreValord == null ? string.Empty : sangreValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hemoglobina == null ? string.Empty : hemoglobina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hemoglobinaValord == null ? string.Empty : hemoglobinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID:

                    #region GRUPO_Y_FACTOR_SANGUINEO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var grupoSanguineo = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);                  
                        var factorSanguineo = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID);
                                          
                        // titulo
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("GRUPO SANGUÍNEO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(grupoSanguineo == null ? string.Empty : grupoSanguineo.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("FACTOR RH", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(factorSanguineo == null ? string.Empty : factorSanguineo.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    
                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_ID:

                    #region PARASITOLOGICO_SIMPLE

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var color = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_COLOR);
                        var consistencia = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_CONSISTENCIA);
                        var restosAlimenticios = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESTOS_ALIMENTICIOS);
                        var sangre = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_SANGRE);
                        var moco = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_MOCO);
                        var quistes = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_QUISTES);
                        var huevos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_HUEVOS);
                        var trofozoitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_TROFOZOITOS);
                        var hematies = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_HEMATIES);
                        var leucocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_LEUCOCITOS);
                        var resultado = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESULTADOS);

                        // titulo
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(color == null ? string.Empty : color.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(consistencia == null ? string.Empty : consistencia.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("RESTOS ALIMENTICIOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(restosAlimenticios == null ? string.Empty : restosAlimenticios.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("SANGRE", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(sangre == null ? string.Empty : sangre.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(moco == null ? string.Empty : moco.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //cells.Add(new PdfPCell(new Phrase("FORMULA LEUCOCITARIA", fontColumnValueNegrita)) { Colspan = 4 });

                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("QUISTES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(quistes == null ? string.Empty : quistes.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("HUEVOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(huevos == null ? string.Empty : huevos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("TROFOZOITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(trofozoitos == null ? string.Empty : trofozoitos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("HEMATÍES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 11va fila
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(resultado == null ? string.Empty : resultado.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });                   

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_ID:

                    #region PARASITOLOGICO_SERIADO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        #region PRIMERA MUESTRA
                        
                      
                        var color = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_COLOR);
                        var consistencia = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_CONSISTENCIA);
                        var restosAlimenticios = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_RESTOS_ALIMENTICIOS);
                        var sangre = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_SANGRE);
                        var moco = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_MOCO);
                        var quistes = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_QUISTES);
                        var huevos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_HUEVOS);
                        var trofozoitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_TROFOZOITOS);
                        var hematies = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_HEMATIES);
                        var leucocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_LEUCOCITOS);
                      

                        // titulo
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("PRIMERA MUESTRA", fontColumnValueNegrita)) { Colspan = 4 });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(color == null ? string.Empty : color.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(consistencia == null ? string.Empty : consistencia.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("RESTOS ALIMENTICIOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(restosAlimenticios == null ? string.Empty : restosAlimenticios.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("SANGRE", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(sangre == null ? string.Empty : sangre.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(moco == null ? string.Empty : moco.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                      
                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("QUISTES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(quistes == null ? string.Empty : quistes.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("HUEVOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(huevos == null ? string.Empty : huevos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("TROFOZOITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(trofozoitos == null ? string.Empty : trofozoitos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("HEMATÍES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        #endregion

                        #region SEGUNDA MUESTRA

                        // SEGUNDA MUESTRA                    
                        var colorSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_COLOR);
                        var consistenciaSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_CONSISTENCIA);
                        var restosAlimenticiosSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_RESTOS_ALIMENTICIOS);
                        var sangreSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_SANGRE);
                        var mocoSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_MOCO);
                        var quistesSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_QUISTES);
                        var huevosSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_HUEVOS);
                        var trofozoitosSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_TROFOZOITOS);
                        var hematiesSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_HEMATIES);
                        var leucocitosSegundaMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_SEGUNDA_LEUCOCITOS);

                        cells.Add(new PdfPCell(new Phrase("SEGUNDA MUESTRA", fontColumnValueNegrita)) { Colspan = 4 });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(colorSegundaMuestra == null ? string.Empty : colorSegundaMuestra.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
    

                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(consistenciaSegundaMuestra == null ? string.Empty : consistenciaSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
               
                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("RESTOS ALIMENTICIOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(restosAlimenticiosSegundaMuestra == null ? string.Empty : restosAlimenticiosSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
    

                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("SANGRE", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(sangreSegundaMuestra == null ? string.Empty : sangreSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(mocoSegundaMuestra == null ? string.Empty : mocoSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });



                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("QUISTES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(quistesSegundaMuestra == null ? string.Empty : quistesSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
 

                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("HUEVOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(huevosSegundaMuestra == null ? string.Empty : huevosSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
  
                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("TROFOZOITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(trofozoitosSegundaMuestra == null ? string.Empty : trofozoitosSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
 

                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("HEMATÍES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hematiesSegundaMuestra == null ? string.Empty : hematiesSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(leucocitosSegundaMuestra == null ? string.Empty : leucocitosSegundaMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
          

                        #endregion

                        #region TERCERA MUESTRA


                        // TERCERA MUESTRA                    
                        var colorTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_COLOR);
                        var consistenciaTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_CONSISTENCIA);
                        var restosAlimenticiosTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_RESTOS_ALIMENTICIOS);
                        var sangreTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_SANGRE);
                        var mocoTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_MOCO);
                        var quistesTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_QUISTES);
                        var huevosTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_HUEVOS);
                        var trofozoitosTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_TROFOZOITOS);
                        var hematiesTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_HEMATIES);
                        var leucocitosTerceraMuestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_LEUCOCITOS);

                        cells.Add(new PdfPCell(new Phrase("TERCERA MUESTRA", fontColumnValueNegrita)) { Colspan = 4 });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(colorTerceraMuestra == null ? string.Empty : colorTerceraMuestra.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(consistenciaTerceraMuestra == null ? string.Empty : consistenciaTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("RESTOS ALIMENTICIOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(restosAlimenticiosTerceraMuestra == null ? string.Empty : restosAlimenticiosTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
       

                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("SANGRE", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(sangreTerceraMuestra == null ? string.Empty : sangreTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
         

                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(mocoTerceraMuestra == null ? string.Empty : mocoTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
   


                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("QUISTES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(quistesTerceraMuestra == null ? string.Empty : quistesTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
   

                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("HUEVOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(huevosTerceraMuestra == null ? string.Empty : huevosTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("TROFOZOITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(trofozoitosTerceraMuestra == null ? string.Empty : trofozoitosTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("HEMATÍES", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(hematiesTerceraMuestra == null ? string.Empty : hematiesTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(leucocitosTerceraMuestra == null ? string.Empty : leucocitosTerceraMuestra.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
   

                        #endregion

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case "N009-ME000000410":

                    #region MAGNESIO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003109");
                        var DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003108");

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("MAGNESIO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(RESULTADO == null ? string.Empty : RESULTADO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(DESEABLE == null ? string.Empty : DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case "N009-ME000000409":

                    #region CADMIO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003107");
                        var DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003106");

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("CADMIO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(RESULTADO == null ? string.Empty : RESULTADO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(DESEABLE == null ? string.Empty : DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;



                case "N009-ME000000408":

                    #region CADMIO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003105");
                        var DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000003104");

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("PLOMO EN ORINA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(RESULTADO == null ? string.Empty : RESULTADO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(DESEABLE == null ? string.Empty : DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


































                //case Sigesoft.Common.Constants.BK_DIRECTO_ID:

                //    #region BK_DIRECTO

                //    cells = new List<PdfPCell>();

                //    // Subtitulo  ******************
                //    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                //    {
                //        Colspan = 4,
                //        BackgroundColor = SubtitleBackgroundColor,
                //        HorizontalAlignment = Element.ALIGN_CENTER,
                //    };

                //    cells.Add(cell);
                //    //*****************************************

                //    if (serviceComponent.ServiceComponentFields.Count > 0)
                //    {
                //        var muestra = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_MUESTRA);
                //        var colaboracion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_COLORACION);
                //        var resultados = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_RESULTADOS);

                //        // titulo
                //        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("VALORES DESEABLES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("UNIDADES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                //        // 1era fila
                //        cells.Add(new PdfPCell(new Phrase("MUESTRA", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(muestra == null ? string.Empty : muestra.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                //        // 2da fila
                //        cells.Add(new PdfPCell(new Phrase("COLABORACION", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(colaboracion == null ? string.Empty : colaboracion.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                //        // 2da fila
                //        cells.Add(new PdfPCell(new Phrase("RESULTADOS", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(resultados == null ? string.Empty : resultados.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                //        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                //    }
                //    else
                //    {
                //        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                //        columnWidths = new float[] { 100f };
                //    }

                //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                //    #endregion

                    //break;

                //case Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID:

                //    #region TOXICOLOGICO_COCAINA_MARIHUANA

                //    cells = new List<PdfPCell>();

                //    // Subtitulo  ******************
                //    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                //    {
                //        Colspan = 4,
                //        BackgroundColor = SubtitleBackgroundColor,
                //        HorizontalAlignment = Element.ALIGN_CENTER,
                //    };

                //    cells.Add(cell);
                //    //*****************************************

                //    if (serviceComponent.ServiceComponentFields.Count > 0)
                //    {
                //         var resultadosCocaina = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA);
                //        var resultadosMarihuana = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA);

                //        // titulo
                //        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                //        // 1era fila
                //        cells.Add(new PdfPCell(new Phrase("MARIHUANA", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(resultadosMarihuana == null ? string.Empty : resultadosMarihuana.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("COCAINA", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(resultadosCocaina == null ? string.Empty : resultadosCocaina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                //        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                //    }
                //    else
                //    {
                //        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                //        columnWidths = new float[] { 100f };
                //    }

                //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                //    #endregion

                //    break;


                //case Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA:

                //    #region TEST_ALCOHOLEMIA

                //    cells = new List<PdfPCell>();

                //    // Subtitulo  ******************
                //    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                //    {
                //        Colspan = 4,
                //        BackgroundColor = SubtitleBackgroundColor,
                //        HorizontalAlignment = Element.ALIGN_CENTER,
                //    };

                //    cells.Add(cell);
                //    //*****************************************

                //    if (serviceComponent.ServiceComponentFields.Count > 0)
                //    {
                //        var TOXICOLOGICO_ALCOHOLEMIA_RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA_RESULTADO);
                //        var TOXICOLOGICO_ALCOHOLEMIA_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA_DESEABLE);
                                                 
                //        // 1era fila
                //        cells.Add(new PdfPCell(new Phrase("ALCOHOLEMIA", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(TOXICOLOGICO_ALCOHOLEMIA_RESULTADO == null ? string.Empty : TOXICOLOGICO_ALCOHOLEMIA_RESULTADO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(TOXICOLOGICO_ALCOHOLEMIA_DESEABLE == null ? string.Empty : TOXICOLOGICO_ALCOHOLEMIA_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

   
                //        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                //    }
                //    else
                //    {
                //        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                //        columnWidths = new float[] { 100f };
                //    }

                //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                //    #endregion

                //    break;


                //case Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS:

                //    #region TOXICOLOGICO_ANFETAMINAS

                //    cells = new List<PdfPCell>();

                //    // Subtitulo  ******************
                //    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                //    {
                //        Colspan = 4,
                //        BackgroundColor = SubtitleBackgroundColor,
                //        HorizontalAlignment = Element.ALIGN_CENTER,
                //    };

                //    cells.Add(cell);
                //    //*****************************************

                //    if (serviceComponent.ServiceComponentFields.Count > 0)
                //    {
                //        var resultadosAnfetaminas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS_RESULTADO);
                //        var resultadosAnfetaminasDeseable = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS_DESEABLE);
             
                //        // 1era fila
                //        cells.Add(new PdfPCell(new Phrase("ANFETAMINAS", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(resultadosAnfetaminas == null ? string.Empty : resultadosAnfetaminas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(resultadosAnfetaminasDeseable == null ? string.Empty : resultadosAnfetaminasDeseable.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                //        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                //    }
                //    else
                //    {
                //        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                //        columnWidths = new float[] { 100f };
                //    }

                //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                //    #endregion

                //    break;


                //case Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS:

                //    #region TOXICOLOGICO_BENZODIAZEPINAS

                //    cells = new List<PdfPCell>();

                //    // Subtitulo  ******************
                //    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                //    {
                //        Colspan = 4,
                //        BackgroundColor = SubtitleBackgroundColor,
                //        HorizontalAlignment = Element.ALIGN_CENTER,
                //    };

                //    cells.Add(cell);
                //    //*****************************************

                //    if (serviceComponent.ServiceComponentFields.Count > 0)
                //    {
                //        var resultados = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS_RESULTADO);
                //        var Deseable = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS_DESEABLE);

                //        // 1era fila
                //        cells.Add(new PdfPCell(new Phrase("BENZODIAZEPINAS", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(resultados == null ? string.Empty : resultados.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(Deseable == null ? string.Empty : Deseable.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                //        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                //    }
                //    else
                //    {
                //        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                //        columnWidths = new float[] { 100f };
                //    }

                //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                //    #endregion

                //    break;


                //case Sigesoft.Common.Constants.TOXICOLOGICO_COLINESTERASA:

                //    #region TOXICOLOGICO_COLINESTERASA

                //    cells = new List<PdfPCell>();

                //    // Subtitulo  ******************
                //    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                //    {
                //        Colspan = 4,
                //        BackgroundColor = SubtitleBackgroundColor,
                //        HorizontalAlignment = Element.ALIGN_CENTER,
                //    };

                //    cells.Add(cell);
                //    //*****************************************

                //    if (serviceComponent.ServiceComponentFields.Count > 0)
                //    {
                //        var RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COLINESTERASA_RESULTADO);
                //        var DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COLINESTERASA_DESEABLE);

                //        // 1era fila
                //        cells.Add(new PdfPCell(new Phrase("COLINESTERASA", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(RESULTADO == null ? string.Empty : RESULTADO.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(DESEABLE == null ? string.Empty : DESEABLE.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                //        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                //    }
                //    else
                //    {
                //        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                //        columnWidths = new float[] { 100f };
                //    }

                //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                //    #endregion

                //    break;

                //case Sigesoft.Common.Constants.TOXICOLOGICO_CARBOXIHEMOGLOBINA:

                //    #region TOXICOLOGICO_CARBOXIHEMOGLOBINA

                //    cells = new List<PdfPCell>();

                //    // Subtitulo  ******************
                //    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                //    {
                //        Colspan = 4,
                //        BackgroundColor = SubtitleBackgroundColor,
                //        HorizontalAlignment = Element.ALIGN_CENTER,
                //    };

                //    cells.Add(cell);
                //    //*****************************************

                //    if (serviceComponent.ServiceComponentFields.Count > 0)
                //    {
                //        var RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_CARBOXIHEMOGLOBINA_RESULTADO);
                //        var DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_CARBOXIHEMOGLOBINA_DESEABLE);

                //        // 1era fila
                //        cells.Add(new PdfPCell(new Phrase("CARBOXIHEMOGLOBINA", fontColumnValue)));
                //        cells.Add(new PdfPCell(new Phrase(RESULTADO == null ? string.Empty : RESULTADO.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                //        cells.Add(new PdfPCell(new Phrase(DESEABLE == null ? string.Empty : DESEABLE.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                //        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                //    }
                //    else
                //    {
                //        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                //        columnWidths = new float[] { 100f };
                //    }

                //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                //    #endregion

                //    break;

                case Sigesoft.Common.Constants.HISOPADO_NASOFARINGEO_ID:

                    #region HISOPADO_NASOFARINGEO_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var TIPO_MUESTRA = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIPO_MUESTRA);
                        var FROTIS_GRAM = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FROTIS_GRAM);

                        var LEVADURAS = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEVADURAS);
                        var ANTIBIOGRAMA = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIBIOGRAMA);


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TIPO DE MUESTRA ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(TIPO_MUESTRA == null ? string.Empty : TIPO_MUESTRA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("LEVADURAS", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(LEVADURAS == null ? string.Empty : LEVADURAS.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //2 da fila
                        cells.Add(new PdfPCell(new Phrase("FROTIS GRAM", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(FROTIS_GRAM == null ? string.Empty : FROTIS_GRAM.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("ANTIBIOGRAMA", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ANTIBIOGRAMA == null ? string.Empty : ANTIBIOGRAMA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;



                case Sigesoft.Common.Constants.REACCION_INFLAMATORIA_ID:

                    #region REACCION_INFLAMATORIA_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var REACCION_INFLAMATORIA_COLOR = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_COLOR);
                        var REACCION_INFLAMATORIA_ph = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_ph);

                        var REACCION_INFLAMATORIA_Consistencia = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Consistencia);
                        var REACCION_INFLAMATORIA_sangre = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_sangre);

                        var REACCION_INFLAMATORIA_Celulas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Celulas);
                       
                        var REACCION_INFLAMATORIA_Fibras_musculares = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Fibras_musculares);
                        var REACCION_INFLAMATORIA_Leucocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Leucocitos);

                        var REACCION_INFLAMATORIA_Levaduras_de_hongos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Levaduras_de_hongos);
                        var REACCION_INFLAMATORIA_Piocitos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Piocitos);
                        
                        var REACCION_INFLAMATORIA_Fibras_vegetales = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Fibras_vegetales);
                        var REACCION_INFLAMATORIA_Hematies = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Hematies);

                        var REACCION_INFLAMATORIA_Cristales = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Cristales);
                        var REACCION_INFLAMATORIA_Gotas_de_grasa = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Gotas_de_grasa);

                        var REACCION_INFLAMATORIA_Fibras_Algodon = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Fibras_Algodon);
                        var REACCION_INFLAMATORIA_Granulos_de_almidon = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_Granulos_de_almidon);
                    

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("Color ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_COLOR == null ? string.Empty : REACCION_INFLAMATORIA_COLOR.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("pH", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_ph == null ? string.Empty : REACCION_INFLAMATORIA_ph.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //2 da fila
                        cells.Add(new PdfPCell(new Phrase("Consistencia", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Consistencia == null ? string.Empty : REACCION_INFLAMATORIA_Consistencia.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Sangre", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_sangre == null ? string.Empty : REACCION_INFLAMATORIA_sangre.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        
                        //3 da fila
                        cells.Add(new PdfPCell(new Phrase("Células", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Celulas == null ? string.Empty : REACCION_INFLAMATORIA_Celulas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //4 da fila
                        cells.Add(new PdfPCell(new Phrase("Fibras musculares", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Fibras_musculares == null ? string.Empty : REACCION_INFLAMATORIA_Fibras_musculares.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Leucocitos", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Leucocitos == null ? string.Empty : REACCION_INFLAMATORIA_Leucocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //5 da fila
                        cells.Add(new PdfPCell(new Phrase("Levaduras de hongos", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Levaduras_de_hongos == null ? string.Empty : REACCION_INFLAMATORIA_Levaduras_de_hongos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Piocitos", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Piocitos == null ? string.Empty : REACCION_INFLAMATORIA_Piocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //6 da fila
                        cells.Add(new PdfPCell(new Phrase("Fibras vegetales", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Fibras_vegetales == null ? string.Empty : REACCION_INFLAMATORIA_Fibras_vegetales.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Hematíes", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Hematies == null ? string.Empty : REACCION_INFLAMATORIA_Hematies.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //7 da fila
                        cells.Add(new PdfPCell(new Phrase("Cristales", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Cristales == null ? string.Empty : REACCION_INFLAMATORIA_Cristales.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Gotas de grasa", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Gotas_de_grasa == null ? string.Empty : REACCION_INFLAMATORIA_Gotas_de_grasa.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //8 da fila
                        cells.Add(new PdfPCell(new Phrase("Fibras de algodón", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Fibras_Algodon == null ? string.Empty : REACCION_INFLAMATORIA_Fibras_Algodon.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Gránulos de almidón", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(REACCION_INFLAMATORIA_Granulos_de_almidon == null ? string.Empty : REACCION_INFLAMATORIA_Granulos_de_almidon.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.Hisopado_Faringeo_ID:

                    #region Hisopado_Faringeo_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var Hisopado_Faringeo_COLOR = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Hisopado_Faringeo_COLOR);
                        var Hisopado_Faringeo_ASPECTO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Hisopado_Faringeo_ASPECTO);

                        var Hisopado_Faringeo_LEUCOCITOS = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Hisopado_Faringeo_LEUCOCITOS);
                        var Hisopado_Faringeo_HEMATIES = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Hisopado_Faringeo_HEMATIES);

                        var Hisopado_Faringeo_CELULAS_EPITELIALES = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Hisopado_Faringeo_CELULAS_EPITELIALES);
                        var Hisopado_BACTERIAS_GRAM = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Hisopado_BACTERIAS_GRAM);

                     

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("Color ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(Hisopado_Faringeo_COLOR == null ? string.Empty : Hisopado_Faringeo_COLOR.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Aspecto", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Hisopado_Faringeo_ASPECTO == null ? string.Empty : Hisopado_Faringeo_ASPECTO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //2 da fila
                        cells.Add(new PdfPCell(new Phrase("Leucocitos", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(Hisopado_Faringeo_LEUCOCITOS == null ? string.Empty : Hisopado_Faringeo_LEUCOCITOS.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Hematíes", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Hisopado_Faringeo_HEMATIES == null ? string.Empty : Hisopado_Faringeo_HEMATIES.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        //3 da fila
                        cells.Add(new PdfPCell(new Phrase("Células Epiteliales", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(Hisopado_Faringeo_CELULAS_EPITELIALES == null ? string.Empty : Hisopado_Faringeo_CELULAS_EPITELIALES.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("Bacterias GRAM", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Hisopado_BACTERIAS_GRAM == null ? string.Empty : Hisopado_BACTERIAS_GRAM.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                       
                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.RASPADO_SUPERFICIE_UNIA_ID:

                    #region RASPADO_SUPERFICIE_UNIA_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var RASPADO_SUPERFICIE_TIPO_MUESTRA = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RASPADO_SUPERFICIE_TIPO_MUESTRA);
                        var RASPADO_SUPERFICIE_RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RASPADO_SUPERFICIE_RESULTADO);

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TIPO DE MUESTRA  ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(RASPADO_SUPERFICIE_TIPO_MUESTRA == null ? string.Empty : RASPADO_SUPERFICIE_TIPO_MUESTRA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(RASPADO_SUPERFICIE_RESULTADO == null ? string.Empty : RASPADO_SUPERFICIE_RESULTADO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                     

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.BK_DIRECTO_ID:

                    #region RASPADO_SUPERFICIE_UNIA_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var BK_DIRECTO_MICROBIOLOGICO_MUESTRA = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_MUESTRA);
                        var BK_DIRECTO_MICROBIOLOGICO_RESULTADOS = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_RESULTADOS);

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TIPO DE MUESTRA  ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(BK_DIRECTO_MICROBIOLOGICO_MUESTRA == null ? string.Empty : BK_DIRECTO_MICROBIOLOGICO_MUESTRA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(BK_DIRECTO_MICROBIOLOGICO_RESULTADOS == null ? string.Empty : BK_DIRECTO_MICROBIOLOGICO_RESULTADOS.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });



                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;



                case Sigesoft.Common.Constants.TIPO_DE_SANGRIA_ID:

                    #region RASPADO_SUPERFICIE_UNIA_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var TIPO_DE_SANGRIA_TIEMPO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_TIEMPO);
                        var TIPO_DE_SANGRIA_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_DESEABLE);

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TIEMPO ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(TIPO_DE_SANGRIA_TIEMPO == null ? string.Empty : TIPO_DE_SANGRIA_TIEMPO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(TIPO_DE_SANGRIA_DESEABLE == null ? string.Empty : TIPO_DE_SANGRIA_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });



                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;



                case Sigesoft.Common.Constants.TIEMPO_COAGULACION_ID:

                    #region RASPADO_SUPERFICIE_UNIA_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var TIEMPO_COAGULACION_TIEMPO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_TIEMPO);
                        var TIEMPO_COAGULACION_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_DESEABLE);

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TIEMPO ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(TIEMPO_COAGULACION_TIEMPO == null ? string.Empty : TIEMPO_COAGULACION_TIEMPO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(TIEMPO_COAGULACION_DESEABLE == null ? string.Empty : TIEMPO_COAGULACION_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });



                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_ID:

                    #region AGLUTINACIONES_LAMINA_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O);
                        var AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O_DESEABLE);

                        var AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H);
                        var AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H_DESEABLE);

                        var AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A);
                        var AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A_DESEABLE);

                        var AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B);
                        var AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B_DESEABLE);

                        var AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA);
                        var AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA_DESEABLE);


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TÍFICO “O” ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("TÍFICO “O” DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O_DESEABLE == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("TÍFICO “H” ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("TÍFICO “H” DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H_DESEABLE == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("PARATÍFICO “A”", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("PARATÍFICO “A”  DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A_DESEABLE == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("PARATÍFICO “B”", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("PARATÍFICO “B” DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B_DESEABLE == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("BRUCELLA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("BRUCELLA  DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA_DESEABLE == null ? string.Empty : AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });





                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;



                case Sigesoft.Common.Constants.VDRL_ID:

                    #region VDRL_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var LABORATORIO_VDRL_ID = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_VDRL_ID);
                        var VDRL_REACTIVOS_VDRL_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VDRL_REACTIVOS_VDRL_DESEABLE);

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("RESULTADO ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(LABORATORIO_VDRL_ID == null ? string.Empty : LABORATORIO_VDRL_ID.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("V D R L DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(VDRL_REACTIVOS_VDRL_DESEABLE == null ? string.Empty : VDRL_REACTIVOS_VDRL_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });



                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.HEPATITIS_C_ID:

                    #region HEPATITIS_C_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var HEPATITIS_C_REACTIVOS_HEPATITIS_C = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_C_REACTIVOS_HEPATITIS_C);
                        var VDRL_REACTIVOS_VDRL_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VDRL_REACTIVOS_VDRL_DESEABLE);
                        var HEPATITIS_C_REACTIVOS_HEPATITIS_C_OBSERVACION = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_C_REACTIVOS_HEPATITIS_C_OBSERVACION);

                        
                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("RESULTADO ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(HEPATITIS_C_REACTIVOS_HEPATITIS_C == null ? string.Empty : HEPATITIS_C_REACTIVOS_HEPATITIS_C.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("HEPATITIS A DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(VDRL_REACTIVOS_VDRL_DESEABLE == null ? string.Empty : VDRL_REACTIVOS_VDRL_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("OBSERVACION ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(HEPATITIS_C_REACTIVOS_HEPATITIS_C_OBSERVACION == null ? string.Empty : HEPATITIS_C_REACTIVOS_HEPATITIS_C_OBSERVACION.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;



                case Sigesoft.Common.Constants.HEPATITIS_A_ID:

                    #region HEPATITIS_C_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var HEPATITIS_A_REACTIVOS_HEPATITIS_A = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A);
                        var HEPATITIS_A_REACTIVOS_HEPATITIS_A_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A_DESEABLE);
                        var HEPATITIS_A_REACTIVOS_HEPATITIS_A_OBSERVACION = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A_OBSERVACION);


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("RESULTADO ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(HEPATITIS_A_REACTIVOS_HEPATITIS_A == null ? string.Empty : HEPATITIS_A_REACTIVOS_HEPATITIS_A.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("HEPATITIS A DESEABLE", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(HEPATITIS_A_REACTIVOS_HEPATITIS_A_DESEABLE == null ? string.Empty : HEPATITIS_A_REACTIVOS_HEPATITIS_A_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("OBSERVACION ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(HEPATITIS_A_REACTIVOS_HEPATITIS_A_OBSERVACION == null ? string.Empty : HEPATITIS_A_REACTIVOS_HEPATITIS_A_OBSERVACION.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.BIOQUIMICA01_ID:

                    #region HEPATITIS_C_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var BIOQUIMICA01_VALOR = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA01_VALOR);
                        var BIOQUIMICA01_VALOR_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA01_VALOR_DESEABLE);
                      
                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("EXAMEN ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("VALOR DESEABLE ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("UNIDAD DE MEDIDA", fontColumnValue)));


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("ÁCIDO ÚRICO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(BIOQUIMICA01_VALOR == null ? string.Empty : BIOQUIMICA01_VALOR.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(BIOQUIMICA01_VALOR_DESEABLE == null ? string.Empty : BIOQUIMICA01_VALOR_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                      
               
                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.EXAMEN_ELISA_ID:

                    #region EXAMEN_ELISA_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var CAMPO_HIV = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HIV);
                        var EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE);

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("EXAMEN ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("VALOR DESEABLE ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("UNIDAD DE MEDIDA", fontColumnValue)));


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("EXAMEN DE  ELISA (H I V)", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(CAMPO_HIV == null ? string.Empty : CAMPO_HIV.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE == null ? string.Empty : EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID:

                    #region SUB_UNIDAD_BETA_CUALITATIVO_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var SUB_UNIDAD_BETA_CUALITATIVO_RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_RESULTADO);
                        var SUB_UNIDAD_BETA_CUALITATIVO_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_DESEABLE);
                        var SUB_UNIDAD_BETA_CUALITATIVO_OBSERVACION = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_OBSERVACION);


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(SUB_UNIDAD_BETA_CUALITATIVO_RESULTADO == null ? string.Empty : SUB_UNIDAD_BETA_CUALITATIVO_RESULTADO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(SUB_UNIDAD_BETA_CUALITATIVO_DESEABLE == null ? string.Empty : SUB_UNIDAD_BETA_CUALITATIVO_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("OBSERVACÓN", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(SUB_UNIDAD_BETA_CUALITATIVO_OBSERVACION == null ? string.Empty : SUB_UNIDAD_BETA_CUALITATIVO_OBSERVACION.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                       


                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.FECATEST_ID:

                    #region FECATEST_ID

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName.ToUpper(), fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var FECATEST_RESULTADO = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_RESULTADO);
                        var FECATEST_DESEABLE = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_DESEABLE);
                        var FECATEST_OBSERVACION = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_OBSERVACION);


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(FECATEST_RESULTADO == null ? string.Empty : FECATEST_RESULTADO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("DESEABLE", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(FECATEST_DESEABLE == null ? string.Empty : FECATEST_DESEABLE.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("OBSERVACÓN", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(FECATEST_OBSERVACION == null ? string.Empty : FECATEST_OBSERVACION.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });



                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                    

                default:

                    break;
            }

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
