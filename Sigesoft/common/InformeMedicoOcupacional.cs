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
   public class InformeMedicoOcupacional
    {
       public static void CreateInformeMedicoOcupacional(PacientList filiationData, List<ServiceComponentList> serviceComponent, organizationDto infoEmpresaPropietaria, string filePDF, string RecoAudio, string RecoElectro, string RecoEspiro, string RecoNeuro,
           string RecoAltEst, string RecoActFis, string RecoCustNor, string RecoAlt7D, string RecoExaFis, string RecoExaFis7C,
           string RecoOsteoMus1, string RecoTamDer, string RecoOdon, string RecoPsico, string RecoRx, string RecoOit, string RecoOft, string Restricciton, string Aptitud)
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
                page.Dato = "IMR/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
                //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
                writer.PageEvent = page;

                // step 3: we open the document
                document.Open();
                // step 4: we Add content to the document
                // we define some fonts
                
                #region Fonts

                Font fontTitle1 = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));


                #endregion
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                PdfPTable table = null;

                PdfPCell cell = null;

                #region Title
                List<PdfPCell> cells = null;
                PdfPCell CellLogo = null;
                float[] columnWidths = null;
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
                new PdfPCell(new Phrase("INFORME MÉDICO OCUPACIONAL", fontTitle1))
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

               
                #region Declaration Tables
                var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
                string include = string.Empty;
                //List<PdfPCell> cells = null;
                //float[] columnWidths = null;
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

                //PdfPTable table = null;

                //PdfPCell cell = null;

                #endregion

                // Salto de linea
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));

                #region Cabecera del Reporte

                cells = new List<PdfPCell>()
                {
                    //Linea1
                    new PdfPCell(new Phrase("PACIENTE:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName, fontColumnValue)){HorizontalAlignment= Element.ALIGN_LEFT,Colspan=4},                   
                    new PdfPCell(new Phrase("DNI:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData.v_DocNumber, fontColumnValue)){HorizontalAlignment= Element.ALIGN_LEFT},     
                    new PdfPCell(new Phrase("EDAD:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData.i_Age.ToString(), fontColumnValue)){HorizontalAlignment= Element.ALIGN_LEFT},     
                    new PdfPCell(new Phrase("SEXO:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData.v_SexTypeName, fontColumnValue)){HorizontalAlignment= Element.ALIGN_LEFT},     
                   

                      //Linea2
                    new PdfPCell(new Phrase("EMPRESA: ", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)){HorizontalAlignment= Element.ALIGN_LEFT,Colspan=3},                       
                    new PdfPCell(new Phrase("TIPO DE EXAMEN:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT }, new PdfPCell(new Phrase(filiationData.v_TipoExamen, fontColumnValue)){HorizontalAlignment= Element.ALIGN_LEFT,Colspan=6},                                    
                   
                         
                  
                new PdfPCell(new Phrase("PUESTO DE TRABAJO:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT}, new PdfPCell(new Phrase(filiationData.v_CurrentOccupation, fontColumnValue)){HorizontalAlignment= Element.ALIGN_LEFT,Colspan=4 },                                    
                 new PdfPCell(new Phrase("FECHA DE EXAMEN:", fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT,Colspan=3 }, new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)){HorizontalAlignment= Element.ALIGN_LEFT,Colspan=3},   
                   
                };

                columnWidths = new float[] { 11f, 15f,15,15f,15f, 5f, 8f, 6f, 6f, 6f, 8f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "I. DATOS PERSONALES", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                // Salto de linea
                //document.Add(new Paragraph("\r\n"));

                #region CABECERA DETALLE 1
                   cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EXÁMENES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("DIAGNÓSTICOS", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("CONCLUSIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("RECOMENDACIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion



                var xAudiometria = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
                string ValorDxAudiometria = "";

                if (xAudiometria != null)
                {
                    //DX
                    if (xAudiometria.DiagnosticRepository != null)
                    {
                        ValorDxAudiometria = string.Join(", ", xAudiometria.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxAudiometria = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xAudiometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AUDIOMETRIA_CONCLUSIONES_ID);

                   //Recomendaciones


                    #region AUDIOMETRÍA
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("AUDIOMETRÍA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxAudiometria=="" ? "NORMAL" :ValorDxAudiometria, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null? string.Empty :Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoAudio =="" ? "CONTROL ANUAL" : RecoAudio , fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }



                var xOftalmologia = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
                string ValorDxOftalmologia = "";


                if (xOftalmologia != null)
                {
                    //DX
                    if (xOftalmologia.DiagnosticRepository != null)
                    {
                        ValorDxOftalmologia = string.Join(", ", xOftalmologia.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxOftalmologia = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_HALLAZGOS_ID);


                    #region OFTALMOLOGÍA
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("OFTALMOLOGÍA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxOftalmologia=="" ? "NORMAL" :ValorDxOftalmologia , fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones ==  null ? "" :Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoOft == "" ? "CONTROL ANUAL": RecoOft, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion
                }
               
               
                var xElectroCardiograma = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID);
                string ValorDxElectroCardiograma = "";

                if (xElectroCardiograma != null)
                {
                    //DX
                    if (xElectroCardiograma.DiagnosticRepository != null)
                    {
                        ValorDxElectroCardiograma = string.Join(", ", xElectroCardiograma.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxElectroCardiograma = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xElectroCardiograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID);


                    #region ELECTOCARDIOGRAMA
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ELECTROCARDIOGRAMA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxElectroCardiograma=="" ? "NORMAL" :ValorDxElectroCardiograma , fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones ==  null ? "" :Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoElectro == "" ? "CONTROL ANUAL": RecoElectro, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion
                }


                var xEspiro = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
                string ValorDxEspiro = "";

                if (xEspiro != null)
                {
                    //DX
                    if (xEspiro.DiagnosticRepository != null)
                    {
                        ValorDxEspiro = string.Join(", ", xEspiro.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxEspiro = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xEspiro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_FUNCIÓN_RESPIRATORIA_ABS_OBSERVACION);

                    #region ESPIROMETRÍA
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ESPIROMETRÍA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxEspiro == "" ? "NORMAL":ValorDxEspiro, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones ==  null ? "" : Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoEspiro == "" ? "CONTROL ANUAL":RecoEspiro, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xEvaNeuro = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EVAL_NEUROLOGICA_ID);
                string ValorDxEvaNeuro = "";

                if (xEvaNeuro != null)
                {
                    //DX
                    if (xEvaNeuro.DiagnosticRepository != null)
                    {
                        ValorDxEvaNeuro = string.Join(", ", xEvaNeuro.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxEvaNeuro = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xEvaNeuro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVAL_NEUROLOGICA_DESCRIPCION_ID);

                    #region EVALUACIÓN NEUROLÓGICA
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EVALUACIÓN NEURLÓGICA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxEvaNeuro == "" ? "NORMAL" : ValorDxEvaNeuro, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ? "" :Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoNeuro == "" ? "CONTROL ANUAL":RecoNeuro, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xAltEstr = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID);
                string ValorDxAltEstr = "";

                if (xAltEstr != null)
                {
                    //DX
                    if (xAltEstr.DiagnosticRepository != null)
                    {
                        ValorDxAltEstr = string.Join(", ", xAltEstr.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxAltEstr = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xAltEstr.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID);

                    #region EXAMEN ALTURA ESTRUCTURAL
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EXAMEN ALTURA ESTRUCTURAL", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxAltEstr==""?"NORMAL" : ValorDxAltEstr, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ? "" :Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoAltEst == "" ? "CONTROL ANUAL" : RecoAltEst, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xActFis = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CUESTIONARIO_ACTIVIDAD_FISICA);
                string ValorDxActFis = "";

                if (xActFis != null)
                {
                    //DX
                    if (xActFis.DiagnosticRepository != null)
                    {
                        ValorDxActFis = string.Join(", ", xActFis.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxActFis = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xActFis.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID);

                    #region CUESTIONARIO ACTIVIDAD FÍSICA
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("CUESTIONARIO ACTIVIDAD FÍSICA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxActFis == "" ? "NORMAL": ValorDxActFis, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoActFis == "" ? "CONTROL ANUAL" : RecoActFis, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xCustNord = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.C_N_ID);
                string ValorDxCustNord = "";
                if (xCustNord != null)
                {
                    //DX
                    if (xCustNord.DiagnosticRepository != null)
                    {
                        ValorDxCustNord = string.Join(", ", xCustNord.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxCustNord = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xCustNord.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID);

                    #region CUESTIONARIO NÓRDICO OSTEOMUSCULAR
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("CUESTIONARIO NÓRDICO OSTEOMUSCULAR", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxCustNord== "" ? "NORMAL" :ValorDxCustNord, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoCustNor == "" ? "CONTROL ANUAL" :RecoCustNor, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var x7D = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_7D_ID);
                string ValorDx7D = "";
                if (x7D != null)
                {
                    //DX
                    if (x7D.DiagnosticRepository != null)
                    {
                        ValorDx7D = string.Join(", ", x7D.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDx7D = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = x7D.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_7D_DESCRIPCION_ID);

                    #region EXAMEN ALTURA GEOGRÁFICA 7D
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EXAMEN ALTURA GEOGRÁFICA 7D", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDx7D =="" ?"NORMAL" :ValorDx7D , fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ? "" : Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoAlt7D == "" ? "CONTROL ANUAL" :RecoAlt7D, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xExaFis = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID);
                string ValorDxExaFis = "";
                if (xExaFis != null)
                {
                    //DX
                    if (xExaFis.DiagnosticRepository != null)
                    {
                        ValorDxExaFis = string.Join(", ", xExaFis.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxExaFis = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xExaFis.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_HALLAZGOS_ID);


                    #region EXAMEN FÍSICO
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EXAMEN FÍSICO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxExaFis == "" ?"NORMAL" : ValorDxExaFis, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ?"" : Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoExaFis== "" ? "CONTROL ANUAL" :RecoExaFis, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xExaFis7C = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_ID);
                string ValorDxExaFis7C = "";
                if (xExaFis7C != null)
                {
                    //DX
                    if (xExaFis7C.DiagnosticRepository != null)
                    {
                        ValorDxExaFis7C = string.Join(", ", xExaFis7C.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxExaFis7C = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xExaFis7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_HALLAZGOS_ID);
                 
                    #region EXAMEN FÍSICO 7C
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EXAMEN FÍSICO 7C", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxExaFis7C == "" ?"NORMAL" : ValorDxExaFis7C, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("")){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoExaFis7C== "" ? "CONTROL ANUAL" :RecoExaFis7C, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
             
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion
                }



                var xOsteo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OSTEO_MUSCULAR_ID_1);
                string ValorDxOsteo = "";
                if (xOsteo != null)
                {
                    //DX
                    if (xOsteo.DiagnosticRepository != null)
                    {
                        ValorDxOsteo = string.Join(", ", xOsteo.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxOsteo = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xOsteo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DESCRIPCION);

                    #region EXAMEN OSTEOMUSCULAR 1
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EXAMEN OSTEOMUSCULAR 1", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxOsteo == "" ?"NORMAL" : ValorDxOsteo, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ?"" : Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoOsteoMus1== "" ? "CONTROL ANUAL" :RecoOsteoMus1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                          
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xTami = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_ID);
                string ValorDxTami = "";
                if (xTami != null)
                {
                    //DX
                    if (xTami.DiagnosticRepository != null)
                    {
                        ValorDxTami = string.Join(", ", xTami.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxTami = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xTami.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID);

                    #region TAMIZAJE DERMATOLOGICO
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("TAMIZAJE DERMATOLÓGICO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxTami == "" ?"NORMAL" : ValorDxTami, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ?"" : Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoTamDer== "" ? "CONTROL ANUAL" :RecoTamDer, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                                               
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xOdo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ODONTOGRAMA_ID);
                string ValorDxOdo = "";
                if (xOdo != null)
                {
                    //DX
                    if (xOdo.DiagnosticRepository != null)
                    {
                        ValorDxOdo = string.Join(", ", xOdo.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxOdo = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xOdo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);

                    #region ODONTOGRAMA
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ODONTOGRAMA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxOdo == "" ?"NORMAL" : ValorDxOdo, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ?"" : Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoOdon== "" ? "CONTROL ANUAL" :RecoOdon, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                     
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xPsico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PSICOLOGIA_ID);
                string ValorDxPsico = "";
                if (xPsico != null)
                {
                    //DX
                    if (xPsico.DiagnosticRepository != null)
                    {
                        ValorDxPsico = string.Join(", ", xPsico.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxPsico = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones1 = xPsico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_COGNITIVA_ID);
                    var Conclusiones2 = xPsico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_EMOCIONAL_ID);
                    var Conclusiones3 = xPsico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_CONCLUSIONES_ID);//cambiar

                    //string ConclusionesConcatenadas = Conclusiones1.v_Value1Name + "/" + Conclusiones2.v_Value1Name + "/" + Conclusiones3.v_Value1;
                   
                    #region INFORME PSICOLÓGICO OCUPACIONAL
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("INFORME PSICOLÓGICO OCUPACIONAL", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxPsico == "" ?"NORMAL" : ValorDxPsico, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones1 == null? "": Conclusiones1.v_Value1Name +"/ "+ Conclusiones2 == null? "":Conclusiones2.v_Value1Name+"/ "+ Conclusiones3 == null? "":Conclusiones3.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoPsico== "" ? "CONTROL ANUAL" :RecoPsico, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                                          
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion

                }

                var xRx = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.RX_TORAX_ID);
                string ValorDxRx = "";
                if (xRx != null)
                {
                    //DX
                    if (xRx.DiagnosticRepository != null)
                    {
                        ValorDxRx = string.Join(", ", xRx.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxRx = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xRx.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID);

                    #region RADIOGRAFÍA DE TORAX
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("RADIOGRAFÍA DE TÓRAX", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxRx == "" ?"NORMAL" : ValorDxRx, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ?"" : Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoRx== "" ? "CONTROL ANUAL" :RecoRx, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER }, 
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion
                }

                var xRxOIT = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OIT_ID);
                string ValorDxRxOIT = "";
                if (xRxOIT != null)
                {
                    //DX
                    if (xRxOIT.DiagnosticRepository != null)
                    {
                        ValorDxRxOIT = string.Join(", ", xRxOIT.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxRxOIT = "NORMAL";
                    }
                    //Conclusiones
                    var Conclusiones = xRxOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID);

                    #region RADIOGRAFÍA OIT
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("RADIOGRAFÍA OIT", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                   new PdfPCell(new Phrase(ValorDxRxOIT == "" ?"NORMAL" : ValorDxRxOIT, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Conclusiones == null ?"" : Conclusiones.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(RecoOit== "" ? "CONTROL ANUAL" :RecoOit, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER }, 
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion
                }





                var xTriaje = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
                string ValorDxTriaje = "";
                if (xTriaje != null)
                {
                    //DX
                    if (xTriaje.DiagnosticRepository != null)
                    {
                        ValorDxTriaje = string.Join(", ", xTriaje.DiagnosticRepository.Select(p => p.v_DiseasesName));
                    }
                    else
                    {
                        ValorDxTriaje = "NORMAL";
                    }
                 
                    #region TRIAJE
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("TRIAJE", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase(ValorDxTriaje == "" ?"NORMAL" : ValorDxTriaje, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                    #endregion
                }

                #region ANTROPOMETRÍA
                var xAntropometria = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);

                if (xAntropometria != null || xAntropometria.ServiceComponentFields.Count==0)
                {
                    var Talla = xAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID);
                    var Peso = xAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID);
                    var IMC = xAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID);

                    
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ANTROPOMETRÍA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("TALLA:" + Talla == null ? "" : "TALLA(m): " +Talla.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("PESO:"+ Peso == null ? "" :"PESO(Kg): "+ Peso.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("I.M.C: "+ IMC == null ? "" :"I.M.C: "+ IMC.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                document.Add(filiationWorker);
                }
               


                #endregion

                #region FUNCIONES VITALES

                var xFuncionesVitales = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);

                if (xFuncionesVitales != null || xFuncionesVitales.ServiceComponentFields.Count==0)
                {

                    var PResionArterial = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID);
                    var PResionDias = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID);

                    var FrecCar = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID);
                    var FrecResp = xFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID);



                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("FUNCIONES VITALES", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("PRESIÓN ARTERIAL: " + PResionArterial == null || PResionDias ==  null  ? "" :"PRESIÓN ARTERIAL(mm Hg): " + PResionArterial.v_Value1 + " / " + PResionDias.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("FREC. CARD.: "+ FrecCar == null ? "" :"FREC. CARD.(Lat/min): "+ FrecCar.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("FREC. RESP.: "+ FrecResp == null ? "" :"FREC. RESP.(Resp/min): "+ FrecResp.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);

                }
               
                #endregion

                #region TÍTULO
                cells = new List<PdfPCell>()
                {                                    
                   
                };

                columnWidths = new float[] { 100F };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "LABORATORIO - RESUMEN", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

          #region CABECERA DETALLE 2
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("VALOR REFERENCIA", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("UNIDAD DE MEDIDA", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion


             
                var xAcidoUrico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TEST_ESTEREOPSIS_ID);
                var xAntigenoPro = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ID);
                var xBKDirecto = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BK_DIRECTO_ID);
                var xHDL = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_HDL_ID);
                var xLDL = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_LDL_ID);
                var xColesterolTotal = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_ID);
                var xColesterolVLDL = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_VLDL_ID);
                var xCreatina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CREATININA_ID);
                var xVIH = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_ELISA_ID);
                var xGlucosa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GLUCOSA_ID);
                var xFactorSan = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID);
                var xHepaA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEPATITIS_A_ID);
                var xHepaC = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEPATITIS_C_ID);
                var xParaSeri = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_ID);
                var xParaSimp = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_ID);
                var xPlomoSangre = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_SANGRE_ID);
                var xUniBeta = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID);
                var xTGO = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TGO_ID);
                var xTGP = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TGP_ID);
                var xCocaina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                var xCARBOXIHEMOGLOBINA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_CARBOXIHEMOGLOBINA);
                var xBENZODIAZEPINAS = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS);
                var xALCOHOLEMIA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA);
                var xCOLINESTERASA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COLINESTERASA);
                var xANFETAMINAS = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS);




                var xTrigli = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TRIGLICERIDOS_ID);
                var xVDRL = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.VDRL_ID);
                var xUrea = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.UREA_ID);
                var xBio1 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA01_ID);
                var xBio2 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA02_ID);
                var xBio3 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA03_ID);






                if (xAcidoUrico != null)
                {
                    var AcidoUrico = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO);
                    var AcidoUricoValord = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO_DESEABLE);
                  
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("ÁCIDO ÚRICO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(AcidoUrico == null ? string.Empty : AcidoUrico.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(AcidoUricoValord == null ? string.Empty : AcidoUricoValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(AcidoUrico == null ?string.Empty: AcidoUrico.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xAntigenoPro != null)
                {
                    var AntigenoPro = xAntigenoPro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ANTIGENO_PROSTATICO_VALOR);
                    var AntigenoProValord = xAntigenoPro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_VALOR_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("ANTÍGENO PROSTÁTICO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(AntigenoPro == null ? string.Empty : AntigenoPro.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(AntigenoProValord == null ? string.Empty : AntigenoProValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(AntigenoPro == null ?string.Empty: AntigenoPro.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }



                if (xBKDirecto != null)
                {
                    var BKDirecto = xBKDirecto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_RESULTADOS);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("BK-DIRECTO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(BKDirecto == null ? string.Empty : BKDirecto.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(BKDirecto == null ?string.Empty:BKDirecto.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xHDL != null)
                {
                    var HDL = xHDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HIPIDICO_COLESTEROL_HDL_ID);
                    var HDLProValord = xHDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HIPIDICO_COLESTEROL_HDL_DESEABLE_ID);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("COLESTEROL HDL", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(HDL == null ? string.Empty : HDL.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(HDLProValord == null ? string.Empty : HDLProValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(HDL == null ?string.Empty:HDL.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xLDL != null)
                {
                    var LDL = xLDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL);
                    var LDLProValord = xLDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("COLESTEROL LDL", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(LDL == null ? string.Empty : LDL.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(LDLProValord == null ? string.Empty : LDLProValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(LDL == null ?string.Empty:LDL.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }

                if (xColesterolTotal != null)
                {
                    var ColesterolTotal = xColesterolTotal.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_ID);
                    var ColesterolTotalValord = xColesterolTotal.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_DESEABLE_ID);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("COLESTEROL Total", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(ColesterolTotal == null ? string.Empty : ColesterolTotal.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ColesterolTotalValord == null ? string.Empty : ColesterolTotalValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ColesterolTotal == null ?string.Empty:ColesterolTotal == null ?string.Empty:ColesterolTotal.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xColesterolVLDL != null)
                {
                    var ColesterolVLDL = xColesterolVLDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL);
                    var ColesterolVLDLValord = xColesterolVLDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("COLESTEROL VLDL", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(ColesterolVLDL == null ? string.Empty : ColesterolVLDL.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ColesterolVLDLValord == null ? string.Empty : ColesterolVLDLValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ColesterolVLDL == null ?string.Empty:ColesterolVLDL.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xCreatina != null)
                {
                    var Creatina = xCreatina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA);
                    var CreatinaValord = xCreatina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("CREATININA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(Creatina == null ? string.Empty : Creatina.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(CreatinaValord == null ? string.Empty : CreatinaValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Creatina == null ?string.Empty:Creatina.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xVIH != null)
                {
                    var VIH = xVIH.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HIV);
                   
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("EXAMEN DE ELISA (HIV)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(VIH == null ? string.Empty : VIH.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(VIH == null ?string.Empty:VIH.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }

                if (xGlucosa != null)
                {
                    var Glucosa = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DESCRIPCION);
                    var GlucosaValord = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_DESEABLE_ID);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("GLUCOSA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(Glucosa == null ? string.Empty : Glucosa.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(GlucosaValord == null ? string.Empty : GlucosaValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Glucosa == null ?string.Empty:Glucosa.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xFactorSan != null)
                {
                    var Factor = xFactorSan.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID);
                    var Grupo = xFactorSan.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("GRUPO SANGUÍNEO Y FACTOR RH", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT, Rowspan=2 }, 
                        new PdfPCell(new Phrase("GRUPO: "+ Grupo == null ? string.Empty : "GRUPO: "+ Grupo.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },       
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   

                        new PdfPCell(new Phrase("FACTOR RH: "+ Factor == null ? string.Empty :"FACTOR RH: "+  Factor.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },       
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },       

                    
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xHepaA != null)
                {
                    var HepaA = xHepaA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A);
                    var HepaAValord = xHepaA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("HAV IgM (HEPATITIS A)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(HepaA == null ? string.Empty : HepaA.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(HepaAValord == null ? string.Empty : HepaAValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(HepaA.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }

                if (xHepaC != null)
                {
                    var HepaC = xHepaC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_C_REACTIVOS_HEPATITIS_C);
                    var HepaCValord = xHepaC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_C_REACTIVOS_HEPATITIS_C_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("INMUNO ENZIMA (HEPATITIS C)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(HepaC == null ? string.Empty : HepaC.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(HepaCValord == null ? string.Empty : HepaCValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(HepaC.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }



                if (xParaSeri != null)
                {
                    var ParaSeri = xParaSeri.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_TERCERA_RESULTADOS);


                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("PARASITOLÓGICO SERIADO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(ParaSeri == null ? string.Empty : ParaSeri.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ParaSeri.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xParaSimp!= null)
                {
                    var ParaSimp = xParaSimp.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESULTADOS);


                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("PARASITOLÓGICO SIMPLE", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(ParaSimp == null ? string.Empty : ParaSimp.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ParaSimp.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xPlomoSangre != null)
                {
                    var PlomoSangre = xPlomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE);
                    var PlomoSangreValord = xPlomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("PLOMO EN SANGRE", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(PlomoSangre == null ? string.Empty : PlomoSangre.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(PlomoSangreValord == null ? string.Empty : PlomoSangreValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(PlomoSangre.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }

                if (xUniBeta != null)
                {
                    var UniBeta = xUniBeta.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_RESULTADO);
                    
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("SUB UNIDAD BETA CUALITATIVO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(UniBeta == null ? string.Empty : UniBeta.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(UniBeta.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xTGO != null)
                {
                    var TGO = xTGO.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGO_BIOQUIMICA_TGO);
                    var TGOValord = xTGO.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGO_BIOQUIMICA_TGO_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("TGO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(TGO == null ? string.Empty : TGO.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(TGOValord == null ? string.Empty : TGOValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(TGO.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }

                if (xTGP != null)
                {
                    var TGP = xTGP.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGP_BIOQUIMICA_TGP);
                    var TGPValord = xTGP.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGP_BIOQUIMICA_TGP_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("TGP", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(TGP == null ? string.Empty : TGP.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(TGPValord == null ? string.Empty : TGPValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(TGP.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }



                if (xCocaina != null)
                {
                    var Cocaina = xCocaina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA);
                    var Marihuana = xCocaina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("TOXICOLÓGICO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT, Rowspan=2 }, 
                        new PdfPCell(new Phrase("MARIHUANA: "+ Marihuana == null ? string.Empty : "MARIHUANA: "+ Marihuana.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },       
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   

                        new PdfPCell(new Phrase("COCAINA: "+ Cocaina == null ? string.Empty :"COCAINA: "+  Cocaina.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_LEFT },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },       
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },       

                    
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }



                if (xCARBOXIHEMOGLOBINA != null)
                {
                    var CARBOXIHEMOGLOBINA = xCARBOXIHEMOGLOBINA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_CARBOXIHEMOGLOBINA_RESULTADO);
                    //var TrigliValord = xCARBOXIHEMOGLOBINA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("T. CARBOXIHEMOGLOBINA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(CARBOXIHEMOGLOBINA == null ? string.Empty : CARBOXIHEMOGLOBINA.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(CARBOXIHEMOGLOBINA.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xBENZODIAZEPINAS != null)
                {
                    var BENZODIAZEPINAS = xBENZODIAZEPINAS.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS_RESULTADO);
                    //var TrigliValord = xBENZODIAZEPINAS.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("T. BENZODIAZEPINAS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(BENZODIAZEPINAS == null ? string.Empty : BENZODIAZEPINAS.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(BENZODIAZEPINAS.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xALCOHOLEMIA != null)
                {
                    var ALCOHOLEMIA = xALCOHOLEMIA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA_RESULTADO);
                    //var TrigliValord = xALCOHOLEMIA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("T. ALCOHOLEMIA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(ALCOHOLEMIA == null ? string.Empty : ALCOHOLEMIA.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ALCOHOLEMIA.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xCOLINESTERASA != null)
                {
                    var ALCOHOLEMIA = xCOLINESTERASA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COLINESTERASA_RESULTADO);
                    //var TrigliValord = xCOLINESTERASA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("T. COLINESTERASA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(ALCOHOLEMIA == null ? string.Empty : ALCOHOLEMIA.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ALCOHOLEMIA.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }



                if (xANFETAMINAS != null)
                {
                    var ANFETAMINAS = xANFETAMINAS.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS_RESULTADO);
                    //var TrigliValord = xANFETAMINAS.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("T. ANFETAMINAS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(ANFETAMINAS == null ? string.Empty : ANFETAMINAS.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(ANFETAMINAS.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }



                if (xTrigli != null)
                {
                    var Trigli = xTrigli.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS);
                    var TrigliValord = xTrigli.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(Trigli == null ? string.Empty : Trigli.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(TrigliValord == null ? string.Empty : TrigliValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Trigli.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xVDRL != null)
                {
                    var VDRL = xVDRL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_VDRL_ID);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("VDRL", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(VDRL == null ? string.Empty : VDRL.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(VDRL.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }

                if (xUrea != null)
                {
                    var Urea = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA);
                    var UreaValord = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("ÚREA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(Urea == null ? string.Empty : Urea.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(UreaValord == null ? string.Empty : UreaValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Urea.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xBio1 != null)
                {
                    var Bio1 = xBio1.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA01_VALOR);
                    var Bio1Valord = xBio1.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA01_VALOR_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Bioquímica 01", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(Bio1 == null ? string.Empty : Bio1.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Bio1Valord == null ? string.Empty : Bio1Valord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Bio1.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }


                if (xBio2 != null)
                {
                    var Bio2 = xBio2.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA02_VALOR);
                    var Bio2Valord = xBio2.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA02_VALOR_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Bioquímica 02", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(Bio2 == null ? string.Empty : Bio2.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Bio2Valord == null ? string.Empty : Bio2Valord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Bio2.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }

                if (xBio3 != null)
                {
                    var Bio3 = xBio3.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA03_VALOR);
                    var Bio3Valord = xBio3.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BIOQUIMICA03_VALOR_DESEABLE);

                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("Bioquímica 03", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                        new PdfPCell(new Phrase(Bio3 == null ? string.Empty : Bio3.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Bio3Valord == null ? string.Empty : Bio3Valord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                        new PdfPCell(new Phrase(Bio3.v_MeasurementUnitName, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                    };

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                    document.Add(filiationWorker);
                }




                #region AGLUTACUIONES EN LAMINA

                var xAglutacionesLamina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_ID);

                if (xAglutacionesLamina != null)
                {

                    var Brucela = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA);
                    var BrucelaValord = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA_DESEABLE);

                    var ParafiticoA = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A);
                    var ParafiticoAValord = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A_DESEABLE);

                    var ParafiticoB = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B);
                    var ParafiticoBValord = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B_DESEABLE);

                    var TificoH = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H);
                    var TificoHValord = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H_DESEABLE);

                    var TificoO = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O);
                    var TificoOValord = xAglutacionesLamina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O_DESEABLE);

                          cells = new List<PdfPCell>()
                {

                    new PdfPCell(new Phrase("BRUCELLA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(Brucela == null ? string.Empty : Brucela.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(BrucelaValord == null ? string.Empty : BrucelaValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },      
 
                    new PdfPCell(new Phrase("PARAFÍTICO A", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(ParafiticoA == null ? string.Empty : ParafiticoA.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(ParafiticoAValord == null ? string.Empty : ParafiticoAValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   

                    new PdfPCell(new Phrase("PARAFÍTICO B", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(ParafiticoB == null ? string.Empty : ParafiticoB.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(ParafiticoBValord == null ? string.Empty : ParafiticoBValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER }, 

                    new PdfPCell(new Phrase("TÍFICO H", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(TificoH == null ? string.Empty : TificoH.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(TificoHValord == null ? string.Empty : TificoHValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER }, 

                    new PdfPCell(new Phrase("TÍFICO O", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(TificoO == null ? string.Empty : TificoO.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(TificoOValord == null ? string.Empty : TificoOValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER }, 
                     
                };

                          columnWidths = new float[] { 25f, 25f, 25f, 25f };

                          filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "AGLUTACIONES EN LÁMINA", fontTitleTableNegro, null);

                          document.Add(filiationWorker);


                }
                #endregion


                #region Orina Completo

                //cells = new List<PdfPCell>()
                //{
                //    new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                //    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                //            new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                //    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                //            new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                //    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                //            new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                //    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                                             
                   
                //};

                //columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };

                //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                //document.Add(filiationWorker);


                var xOrinaCompleto = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);



                if (xOrinaCompleto != null)
                {
                    var Color = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_COLOR);
                    var Aspecto = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_ASPECTO);

                    var Densidad = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_DENSIDAD);
                    var DensidadValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_DENSIDAD_DESEABLE);

                    var PH = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_PH);
                    var PHValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_PH_DESEABLE);

                    var CelEpi = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CELULAS_EPITELIALES);
                    var CelEpiValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CELULAS_EPITELIALES_DESEABLE);

                    var Leucocitos = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS);
                    var LeucocitosValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS_DESEABLE);

                    var Hematies = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES);
                    var HematiesValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES_DESEABLE);

                    var Cristales = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CRISTALES);
                    var CristalesValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CRISTALES_DESEABLE);

                    var Germenes = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_GERMENES);
                    var GermenesValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_GERMENES_DESEABLE);
                    
                    var Cilindros = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CILINDROS);
                    var CilindrosValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CILINDROS_DESEABLE);

                    var Filamento = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_FILAMENTO_MUCOIDE);
                    var FilamentoValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_FILAMENTO_MUCOIDE_DESEABLE);
                    
                    var Sangre = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_SANGRE);
                    var SangreValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_SANGRE_DESEABLE);

                    var Uro = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_UROBILINOGENO);
                    var UroValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_UROBILINOGENO_DESEABLE);

                    var Bili = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_BILIRRUBINA);
                    var BiliValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_BILIRRUBINA_DESEABLE);

                    var Proteinas = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_PROTEINAS);
                    var ProteinasValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_PROTEINAS_DESEABLE);

                    var Nitritos = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_NITRITOS);
                    var NitritosValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_NITRITOS_DESEABLE);

                    var Ceto = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_CETONAS);
                    var CetoValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_CETONAS_DESEABLE);

                    var Glucosa = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_GLUCOSA);
                    var GlucosaValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_GLUCOSA_DESEABLE);

                    var Hemo = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA);
                    var HemoValord = xOrinaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA_DESEABLE);




                            cells = new List<PdfPCell>()
                        {

                            new PdfPCell(new Phrase("COLOR", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Color == null ? string.Empty : Color.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },      

                            new PdfPCell(new Phrase("ASPECTO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Aspecto == null ? string.Empty : Aspecto.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },      


                            new PdfPCell(new Phrase("DENSIDAD", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Densidad == null ? string.Empty : Densidad.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(DensidadValord == null ? string.Empty : DensidadValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },      

                            new PdfPCell(new Phrase("PH", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(PH == null ? string.Empty : PH.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(PHValord == null ? string.Empty : PHValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   
   
                            new PdfPCell(new Phrase("CÉLULAS EPITELIALES", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(CelEpi == null ? string.Empty : CelEpi.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(CelEpiValord == null ? string.Empty : CelEpiValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("%", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   
                            
                            new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Leucocitos == null ? string.Empty : Leucocitos.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(LeucocitosValord == null ? string.Empty : LeucocitosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   

                            new PdfPCell(new Phrase("HEMATÍES", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(HematiesValord == null ? string.Empty : HematiesValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   

                            new PdfPCell(new Phrase("CRISTALES", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Cristales == null ? string.Empty : Cristales.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(CristalesValord == null ? string.Empty : CristalesValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   

                            new PdfPCell(new Phrase("GÉRMENES", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Germenes == null ? string.Empty : Germenes.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(GermenesValord == null ? string.Empty : GermenesValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   

                            new PdfPCell(new Phrase("CILINDROS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Cilindros == null ? string.Empty : Cilindros.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(CilindrosValord == null ? string.Empty : CilindrosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   

                            new PdfPCell(new Phrase("FILAMENTO MUCOIDE", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Filamento == null ? string.Empty : Filamento.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(FilamentoValord == null ? string.Empty : FilamentoValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  

                            new PdfPCell(new Phrase("SANGRE", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Sangre == null ? string.Empty : Sangre.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(SangreValord == null ? string.Empty : SangreValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  

                            new PdfPCell(new Phrase("UROBILINÓGENO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Uro == null ? string.Empty : Uro.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(UroValord == null ? string.Empty : UroValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  

                            new PdfPCell(new Phrase("BILIRRUBINA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Bili == null ? string.Empty : Bili.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(BiliValord == null ? string.Empty : BiliValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  

                            new PdfPCell(new Phrase("PROTEINAS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Proteinas == null ? string.Empty : Proteinas.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(ProteinasValord == null ? string.Empty : ProteinasValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  

                            new PdfPCell(new Phrase("NITRITOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Nitritos == null ? string.Empty : Nitritos.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(NitritosValord == null ? string.Empty : NitritosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  

                            new PdfPCell(new Phrase("CUERPOS CETÓNICOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Ceto == null ? string.Empty : Ceto.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(CetoValord == null ? string.Empty : CetoValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  
                            
                             new PdfPCell(new Phrase("GLUCOSA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Glucosa == null ? string.Empty : Glucosa.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(GlucosaValord == null ? string.Empty : GlucosaValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  


                            new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                            new PdfPCell(new Phrase(Hemo == null ? string.Empty : Hemo.v_Value1Name, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase(HemoValord == null ? string.Empty : HemoValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                            //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  

 
                        };

                            columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
                            columnHeaders = new string[] { "EXÁMEN", "RESULTADO", "EXÁMEN", "RESULTADO", "EXÁMEN", "RESULTADO", "EXÁMEN", "RESULTADO" };
                            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "EXAMEN COMPLETO DE ORINA", fontSubTitleNegroNegrita, columnHeaders);

                            document.Add(filiationWorker);


                }
                #endregion


                #region HEMOGRAMA COMPLETO
                //cells = new List<PdfPCell>()
                //{
                //    new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                //    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                //            new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                //    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                //            new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                //    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                //            new PdfPCell(new Phrase("EXÁMEN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                //    new PdfPCell(new Phrase("RESULTADO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                                             
                   
                //};

                //columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };

                //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

                //document.Add(filiationWorker);

                var xHemogramaCompleto = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_ID);

                if (xHemogramaCompleto != null)
                {

                    var hematocritos = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO);
                    var hematocritosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO_DESEABLE);

                    var hemoglobina = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA);
                    var hemoglobinaValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA_DESEABLE);

                    var globulosRojos = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATIES);
                    var globulosRojosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATIES_DESEABLE);

                    var leucocitos = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS);
                    var leucocitosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS);

                    var abastonados = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_ABASTONADOS);
                    var abastonadosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_ABASTONADOS_DESEABLE);

                    var segmentados = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_SEGMENTADOS);
                    var segmentadosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_SEGMENTADOS_DESEABLE);

                    var eosinofilos = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS);
                    var eosinofilosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS_DESEABLE);

                    var basofilos = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_BASOFILOS);
                    var basofilosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_BASOFILOS_DESEABLE);

                    var monocitos = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_MONOCITOS);
                    var monocitosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_MONOCITOS_DESEABLE);

                    var linfocitos = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_LINFOCITOS);
                    var linfocitosValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_LINFOCITOS_DESEABLE);

                    var Hemtaíes = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATIES);
                    var HemtaíesValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATIES_VALOR_REF);

                    var Plaquetas = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_PLAQUETAS);
                    var PlaquetasValord = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_PLAQUETAS_DESEABLE);

                    var Morfologia = xHemogramaCompleto.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_CONCLUSIONES_DE_HEMOGRAMA);

             
                    cells = new List<PdfPCell>()
                {

                    new PdfPCell(new Phrase("HEMATOCRITO", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(hematocritos == null ? string.Empty : hematocritos.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(hematocritosValord == null ? string.Empty : hematocritosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("%", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },      
 
                    new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(hemoglobina == null ? string.Empty : hemoglobina.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(hemoglobinaValord == null ? string.Empty : hemoglobinaValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("g/di", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },           

                    new PdfPCell(new Phrase("HEMATÍES", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(Hemtaíes == null ? string.Empty : Hemtaíes.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(HemtaíesValord == null ? string.Empty : HemtaíesValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },             

                    new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(leucocitosValord == null ? string.Empty : leucocitosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("x103/mm3", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     

                    
                    new PdfPCell(new Phrase("RECUENTO DE PLAQUETAS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(Plaquetas == null ? string.Empty : Plaquetas.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(PlaquetasValord == null ? string.Empty : PlaquetasValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("x106/mm3", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },              

                    new PdfPCell(new Phrase("ABASTONADOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(abastonados == null ? string.Empty : abastonados.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(abastonadosValord == null ? string.Empty : abastonadosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("%", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },   
        
                    new PdfPCell(new Phrase("SEGMENTADOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(segmentados == null ? string.Empty : segmentados.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(segmentadosValord == null ? string.Empty : segmentadosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("%", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },        
   
                    new PdfPCell(new Phrase("EOSINÓFILOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(eosinofilos == null ? string.Empty : eosinofilos.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(eosinofilosValord == null ? string.Empty : eosinofilosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("%", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },      
            
                    new PdfPCell(new Phrase("BASÓFILOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(basofilos == null ? string.Empty : basofilos.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(basofilosValord == null ? string.Empty : basofilosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("%", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },  
                    
                    new PdfPCell(new Phrase("MONOCITOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(monocitos == null ? string.Empty : monocitos.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(monocitosValord == null ? string.Empty : monocitosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("%", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },      

                    new PdfPCell(new Phrase("LINFOCITOS", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(linfocitos == null ? string.Empty : linfocitos.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(linfocitosValord == null ? string.Empty : linfocitosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("%", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },        

                    new PdfPCell(new Phrase("CONCLUSIONES", fontColumnValue)){HorizontalAlignment = Element.ALIGN_RIGHT },                   
                    new PdfPCell(new Phrase(Morfologia == null ? string.Empty : Morfologia.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },         
                    
                    //new PdfPCell(new Phrase("Glóbulos Rojos", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    //new PdfPCell(new Phrase(globulosRojos == null ? string.Empty : globulosRojos.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase(globulosRojosValord == null ? string.Empty : globulosRojosValord.v_Value1, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    //new PdfPCell(new Phrase("x106/mm3", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                  
                };

                    columnWidths = new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };

                    columnHeaders = new string[] { "EXÁMEN", "RESULTADO", "EXÁMEN", "RESULTADO", "EXÁMEN", "RESULTADO", "EXÁMEN", "RESULTADO" };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "HEMOGRAMA COMPLETO", fontSubTitleNegroNegrita, columnHeaders);

                    document.Add(filiationWorker);

                
                }

                #endregion


               // #region   Grupo Sanguineo
               // cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("Grupo Sanguineo", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region  Factor RH
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("Factor RH", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region   TGO (UI/L)
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase(" TGO (UI/L)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region   TGP (UI/L)
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("TGP (UI/L)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region   Colesterol total (mg/dl)
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("Colesterol total (mg/dl)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region   Trigliceridos (mg/dl)
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("Trigliceridos (mg/dl)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region   Glucosa (mg/dl)
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("Glucosa (mg/dl)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region    Dosaje de cocaina
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("Dosaje de cocaina", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region  Dosaje de marihuana
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("Dosaje de marihuana", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               //#region  B-HCG (Cualitativo)
               //cells = new List<PdfPCell>()
               // {
               //     new PdfPCell(new Phrase("B-HCG (Cualitativo)", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                   
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
               //     new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
               // };

               //columnWidths = new float[] { 25f, 25f, 25f, 25f };

               //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               //document.Add(filiationWorker);

               //#endregion

               #region  Aptitud Restricciones
               cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("APTITUD", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("RESTRICCIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Aptitud, fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },     
                    new PdfPCell(new Phrase(Restricciton == "" ? "NINGUNA":Restricciton , fontColumnValue)){HorizontalAlignment = Element.ALIGN_CENTER },                                    
                   
                };

               columnWidths = new float[] { 25f, 75f};

               filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

               document.Add(filiationWorker);

               #endregion


                // Salto de linea
                document.Add(new Paragraph("\r\n"));


                ServiceComponentList lab = null;
                #region Firma y sello Médico
 
                table = new PdfPTable(2);
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.WidthPercentage = 30;

                columnWidths = new float[] { 15f, 25f };
                table.SetWidths(columnWidths);

                PdfPCell cellFirma = null;

                if (filiationData != null)
                {
                    if (filiationData.FirmaDoctor != null)
                        cellFirma = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaDoctor, null, null, 120,45));
                    else
                        cellFirma = new PdfPCell(new Phrase("Sin Foto", fontColumnValue));
                }
                else
                {
                    cellFirma = new PdfPCell();
                }

                cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirma.FixedHeight = 30F;

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

    }
}
