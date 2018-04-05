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
    public class CertificadoAptitudCompleto
    {

        public static void CreateCertificadoAptitudCompleto(List<DiagnosticRepositoryList> filiationData, organizationDto infoEmpresaPropietaria, List<DiagnosticRepositoryList> diagnosticRepository, string filePDF, string PathNegro, string PathBlanco)
        {
            //
            // step 1: creation of a document-object
            Document document = new Document();
            //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
            //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            //Document document = new Document(PageSize.A4, 0, 0, 20, 20);
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

                //Paragraph cTitle = new Paragraph("CERTIFICADO DE APTITUD MÉDICO OCUPACIONAL", fontTitle2);
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
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                #region Title
                //List<PdfPCell> cells = null;
                PdfPCell CellLogo = null;
                //float[] columnWidths = null;
                cells = new List<PdfPCell>();
                PdfPCell cellPhoto1 = null;

                if (filiationData[0].b_Photo != null)
                    cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData[0].b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
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
                new PdfPCell(new Phrase("CERTIFICADO DE APTITUD MÉDICO OCUPACIONAL", fontTitle1))
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


       

                // Salto de linea
               

                string PuestoPostula = "-----";
                string PuestoActual = "-----";


                if (filiationData[0].i_EsoTypeId == ((int)Sigesoft.Common.TypeESO.PreOcupacional).ToString())
                {
                    PuestoPostula = filiationData[0].v_OccupationName;
                }
                else
                {
                    PuestoActual = filiationData[0].v_OccupationName;
                }

                #region Cabecera del Reporte

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("APELLIDOS Y NOMBRES:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].v_PersonName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("N° HS:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].v_ServiceId, fontColumnValue)),     
                    new PdfPCell(new Phrase("DOC. DE IDENTIDAD:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].v_DocNumber, fontColumnValue)),     
                    new PdfPCell(new Phrase("EDAD:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].i_Age.ToString(), fontColumnValue)),     
                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].v_OrganizationName, fontColumnValue)),     
                    new PdfPCell(new Phrase("FECHA DE E.M.O:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].v_ServiceDate, fontColumnValue)),   
                    new PdfPCell(new Phrase("PUESTO AL QUE POSTULA:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase( PuestoPostula, fontColumnValue)),   
                    new PdfPCell(new Phrase("FECHA DE NAC.:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].d_BirthDate.Value.ToShortDateString(), fontColumnValue)),   
                    new PdfPCell(new Phrase("OCUPACIÓN ACTUAL:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase( PuestoActual, fontColumnValue)),   
                    new PdfPCell(new Phrase("GÉNERO:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].v_GenderName, fontColumnValue)),     
                    new PdfPCell(new Phrase("EXAMEN MÉDICO:", fontColumnValue)){  HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].v_EsoTypeName, fontColumnValue)){Colspan=3},    
                    new PdfPCell(new Phrase("GRUPO SANGUÍNEO Y RH:", fontColumnValue)){ HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData[0].GrupoFactorSanguineo , fontColumnValue)){Colspan=3},                                    
                  
                      
                    };

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "DATOS GENEREALES", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion
                

                // Salto de linea
                document.Add(new Paragraph("\r\n"));


                #region DETALLE EXAMENES
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EXAMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("RESTRICCIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER }                                  
                   
                };

                columnWidths = new float[] { 25f, 15f, 60f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "EXÁMENES REALIZADOS", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                #region Categorias y Restricciones

                cells = new List<PdfPCell>();

                if (filiationData != null && filiationData.Count > 0)
                {
                    columnWidths = new float[] { 30f };
                    include = "RESULTADO";

                    foreach (var item in filiationData)
                    {
                        cell = new PdfPCell(new Phrase(item.Categoria, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(item.Resultado, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(item.v_RestrictionsName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }

                    columnWidths = new float[] { 25f, 15f, 60f };
                }
                else
                {
                    cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    columnWidths = new float[] { 100 };
                }

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro);

                document.Add(table);

                #endregion


                #region DETALLE CONCLUSIONES
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("DIAGNÓSTICOS", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("CIE10", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("RECOMENDACIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER }                                  
                   
                };

                columnWidths = new float[] { 20f, 10f, 70f};

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "CONCLUSIONES", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                #region Hallazgos y recomendaciones

                cells = new List<PdfPCell>();

                var DxNODescartados = diagnosticRepository.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);
                if (DxNODescartados != null && DxNODescartados.Count > 0)
                {
                    columnWidths = new float[] { 0.7f, 23.6f };
                    include = "i_Item,v_RecommendationName";

                    foreach (var item in DxNODescartados)
                    {
                        cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(item.i_DiagnosticTypeId == (int)Sigesoft.Common.TipoDx.Normal?"---": item.v_Dx_CIE10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                        // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                        table = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue);
                        cell = new PdfPCell(table);
                        cells.Add(cell);
                    }

                    columnWidths = new float[] { 20f,10f, 70f };
                }
                else
                {
                    cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    columnWidths = new float[] { 100 };
                }

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro);

                document.Add(table);

                #endregion

                #region APTITUD

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("APTO", fontSubTitleNegroNegrita)){ Colspan=2, HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("OBSERVADO", fontSubTitleNegroNegrita)){  Rowspan = 2,HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("NO APTO", fontSubTitleNegroNegrita)){Rowspan=2, HorizontalAlignment = Element.ALIGN_CENTER },     
                    
                    new PdfPCell(new Phrase("SIN RESTRICCIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },
                    new PdfPCell(new Phrase("CON RESTRICCIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },
 
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.LEFT_BORDER},
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.LEFT_BORDER},
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.LEFT_BORDER},
                     new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER},                   


                    new PdfPCell(HandlingItextSharp.GetImage(filiationData[0].i_AptitudeStatusId==2 ?PathNegro :PathBlanco, 30, 7)){ HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.LEFT_BORDER }, 
                    new PdfPCell(HandlingItextSharp.GetImage(filiationData[0].i_AptitudeStatusId==5 ?PathNegro :PathBlanco, 30, 7)){ HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.LEFT_BORDER }, 
                    new PdfPCell(HandlingItextSharp.GetImage(filiationData[0].i_AptitudeStatusId==4 ?PathNegro :PathBlanco, 30, 7)){ HorizontalAlignment = Element.ALIGN_CENTER, Border = PdfPCell.LEFT_BORDER }, 
                    new PdfPCell(HandlingItextSharp.GetImage(filiationData[0].i_AptitudeStatusId==3 ?PathNegro :PathBlanco, 30, 7)){ Rowspan=2,HorizontalAlignment = Element.ALIGN_CENTER},
                    

                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.LEFT_BORDER},
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.LEFT_BORDER},
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.LEFT_BORDER},

                    
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.TOP_BORDER},
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.TOP_BORDER},
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.TOP_BORDER},
                    new PdfPCell(new Phrase(" ", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER , Border = PdfPCell.TOP_BORDER}
                   
                };

                columnWidths = new float[] {25f, 25f, 25f, 25f};

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "FUE CONSIDERADO EN ÉSTA OPORTUNIDAD", fontTitleTableNegro, null);

                document.Add(filiationWorker);



                #endregion


                //ServiceComponentList lab = null;
                #region Firma y sello Médico

                table = new PdfPTable(2);
   
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.WidthPercentage = 40;
               
                columnWidths = new float[] { 15f, 25f };
                table.SetWidths(columnWidths);

                PdfPCell cellFirma = null;

                if (filiationData != null)
                {
                    if (filiationData[0].g_Image != null)
                        cellFirma = new PdfPCell(HandlingItextSharp.GetImage(filiationData[0].g_Image, null, null, 70, 40));
                    else
                        cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));
                }
                else
                {
                    cellFirma = new PdfPCell();
                }
           
                cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirma.FixedHeight = 70F;

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
            catch (Exception)
            {
                
                throw;
            }
        }
           
    }
}
