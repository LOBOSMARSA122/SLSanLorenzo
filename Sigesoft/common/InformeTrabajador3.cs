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
    public class InformeTrabajador3
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }


        public static void CreateFichaMedicaTrabajador3(PacientList filiationData, ServiceList doctoPhisicalExam, List<DiagnosticRepositoryList> diagnosticRepository, organizationDto infoEmpresaPropietaria, string pstrExamenesConcatenados, string pstrExamenesLabConcatenados, List<ServiceComponentList> serviceComponent,string pstrRestriciones, string filePDF)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //try
            //{NO_BORDER
            // step 2: we create aPA writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
            page.Dato = "FMT3/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
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

            Font fontTitle1 = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            //Font fontTitleTableNegro = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion


            //#region Logo
            PdfPCell CellLogo = null;
     

            #region Title

            CellLogo = null;
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

                new PdfPCell(new Phrase("RESUMEN DE EXAMEN MÉDICO", fontTitle1))
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

            #region Datos del examen

            cells = new List<PdfPCell>()
                {
                     //fila
                    new PdfPCell(new Phrase("NRO DE HCL:", fontColumnValue)),  
                    new PdfPCell(new Phrase(filiationData.v_IdService, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("TIPO DE EXAMEN: ", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.v_TipoExamen, fontColumnValue)),
                    new PdfPCell(new Phrase("FECHA EXAMEN:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  


                    //fila
                    new PdfPCell(new Phrase("PROVEEDOR:", fontColumnValue)),
                    new PdfPCell(new Phrase(infoEmpresaPropietaria.v_Name, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                     new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.v_OrganitationName, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                     new PdfPCell(new Phrase("GRUPO DE RIESGO:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.GESO, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                     //fila
                    new PdfPCell(new Phrase("EXÁMENES REALIZADOS:", fontColumnValue)),
                    new PdfPCell(new Phrase(pstrExamenesConcatenados, fontColumnValue)){ Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                   //fila
                    new PdfPCell(new Phrase("LABORATORIO: ", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(pstrExamenesLabConcatenados, fontColumnValue)){ Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
            columnWidths = new float[] { 25f, 20f, 20f, 20f, 20f, 20f};

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "DATOS DEL EXAMEN", fontTitleTable);

            document.Add(filiationWorker);



            #endregion

            #region Datos del trabajador

            cells = new List<PdfPCell>()
                {
                     //fila
                    new PdfPCell(new Phrase("APELLIDOS Y NOMBRES:", fontColumnValue)),  
                    new PdfPCell(new Phrase(filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName + " " + filiationData.v_FirstName, fontColumnValue)) {Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                   

                    //fila
                    new PdfPCell(new Phrase("DNI:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.v_DocNumber, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                     new PdfPCell(new Phrase("EDAD:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.i_Age.Value.ToString(), fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                     new PdfPCell(new Phrase("PUESTO:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.v_CurrentOccupation, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                };
            columnWidths = new float[] { 25f, 20f, 10f, 10f, 10f, 50f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "DATOS DEL TRABAJADOR", fontTitleTable);

            document.Add(filiationWorker);



            #endregion

            #region Resultados del examen médico
            cells = new List<PdfPCell>();

            if (diagnosticRepository != null && diagnosticRepository.Count > 0)
            {
                //PdfPCell cellDx = null;

                columnWidths = new float[] { 50f };
                include = "v_RecommendationName";

                var ListaFinal = diagnosticRepository.FindAll(p => p.i_CategoryId != 1 && p.i_CategoryId != 10);
                foreach (var item in ListaFinal)
                {
                    if (item.v_DiseasesId == "N009-DD000000029")
                    {
                        cell = new PdfPCell(new Phrase("")) {  HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(item.Categoria + " - " +item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }

                    // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                    var tableDx = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue);
                    cell = new PdfPCell(tableDx);
                    cells.Add(cell);
                }
                columnWidths = new float[] { 50f, 54f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                columnWidths = new float[] { 100f };
            }

            var GrillaDx = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "RESULTADOS DEL EXAMEN MÉDICO", fontTitleTableNegro);
            document.Add(GrillaDx);
            #endregion
            
            #region Nutrición
            cells = new List<PdfPCell>();
            var xNutricion = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            if (xNutricion != null)
            {

                // Subtitulo  ******************
                cell = new PdfPCell(new Phrase("NUTRICIÓN", fontSubTitleNegroNegrita))
                {
                    Colspan = 4,
                    BackgroundColor = subTitleBackGroundColor,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                };

                cells.Add(cell);
                //*****************************************
                // titulo
                var valorPeso = xNutricion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID);
                var valorTalla = xNutricion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID);
                cells.Add(new PdfPCell(new Phrase("PESO(Kg):", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(valorPeso == null ? string.Empty : valorPeso.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("TALLA(m):", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(valorTalla == null ? string.Empty : valorTalla.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                // titulo
                cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("VALOR HALLADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("DIAGNÓSTICO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("RECOMENDACIONES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                var valorIMC = xNutricion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID);

               var dxIMC = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID);
               var RecoIMC = dxIMC.Recomendations.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
                // 1era fila
                cells.Add(new PdfPCell(new Phrase("IMC", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(valorIMC == null ? string.Empty : valorIMC.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxIMC == null ? "NORMAL" : dxIMC.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(string.Join(", ", RecoIMC.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                document.Add(table);
            }
            
            #endregion

            #region Presión Arterial
            cells = new List<PdfPCell>();
            var xPresionArterial = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
            if (xPresionArterial != null)
            {

                // Subtitulo  ******************
                cell = new PdfPCell(new Phrase("PRESIÓN ARTERIAL", fontSubTitleNegroNegrita))
                {
                    Colspan = 4,
                    BackgroundColor = subTitleBackGroundColor,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                };

                cells.Add(cell);
                //*****************************************

                // titulo
                cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("VALOR HALLADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("DIAGNÓSTICO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("RECOMENDACIONES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                var valorPA1 = xPresionArterial.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : xPresionArterial.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
                var valorPA2 = xPresionArterial.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : xPresionArterial.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;

                var dxPA = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID);
                var RecoPA =dxPA == null?null: dxPA.Recomendations;
                // 1era fila
                cells.Add(new PdfPCell(new Phrase("PRESIÓN ARTERIAL", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(valorPA1 + " / " + valorPA2, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxPA == null ? "NORMAL" : dxPA.v_Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoPA == null ?"CONTROL ANUAL":string.Join(", ", RecoPA.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                columnWidths = new float[] { 25f, 25f, 25f, 25f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                document.Add(table);
            }
            #endregion

            #region Laboratorio
            cells = new List<PdfPCell>();
            // Subtitulo  ******************
            cell = new PdfPCell(new Phrase("LABORATORIO", fontSubTitleNegroNegrita))
            {
                Colspan = 4,
                BackgroundColor = subTitleBackGroundColor,
                HorizontalAlignment = Element.ALIGN_CENTER,
            };

            cells.Add(cell);
            //*****************************************

            // titulo
            cells.Add(new PdfPCell(new Phrase("EXAMEN", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("VALOR HALLADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("DIAGNÓSTICO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            cells.Add(new PdfPCell(new Phrase("RECOMENDACIONES", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            var xHB = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_ID);

            var xHTO = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_ID);
            var xColesterol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_ID);
            var xTrigli = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TRIGLICERIDOS_ID);
            var xGlucosa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GLUCOSA_ID);
            var xUrea = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.UREA_ID);
            var xCreatina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CREATININA_ID);
            var xColesterol_1 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_LIPIDICO);
            var xTrigli_1 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_LIPIDICO);
            var xOrina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);
            var xGrupoSanguineo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID);
            if (xHB != null)
            {
                var hbValor = xHB.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA);
                var dxHB = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA);
                var RecoHB = dxHB == null ? null : dxHB.Recomendations;
              
                // 1era fila
                cells.Add(new PdfPCell(new Phrase("HB", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxHB == null ? "NORMAL" : dxHB.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
          
            }

            if (xHTO != null)
            {
                var hbValor = xHTO.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO);
                var dxHTO = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO);
                var RecoHB = dxHTO == null ? null : dxHTO.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("HTO", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxHTO == null ? "NORMAL" : dxHTO.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }
            if (xColesterol != null)
            {
                var hbValor = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_ID);
                var dxColesterol = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_ID);
                var RecoHB = dxColesterol == null ? null : dxColesterol.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("COLESTEROL", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxColesterol == null ? "NORMAL" : dxColesterol.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }
            if (xTrigli != null)
            {
                var hbValor = xTrigli.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS);
                var dxTrigli = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS);
                var RecoHB = dxTrigli == null ? null : dxTrigli.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxTrigli == null ? "NORMAL" : dxTrigli.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }
            if (xGlucosa != null)
            {
                var hbValor = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DESCRIPCION);
                var dxGlucosa = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DESCRIPCION);
                var RecoHB = dxGlucosa == null ? null : dxGlucosa.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("GLUCOSA", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxGlucosa == null ? "NORMAL" : dxGlucosa.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }
            if (xUrea != null)
            {
                var hbValor = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA);
                var dxUrea = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA);
                var RecoHB = dxUrea == null ? null : dxUrea.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("ÚREA", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxUrea == null ? "NORMAL" : dxUrea.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }
            if (xCreatina != null)
            {
                var hbValor = xCreatina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA);
                var dxCreatina = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA);
                var RecoHB = dxCreatina == null ? null : dxCreatina.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("CREATINA", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxCreatina == null ? "NORMAL" : dxCreatina.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }
            if (xColesterol_1 != null)
            {
                var hbValor = xColesterol_1.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL);
                var dxColesterol_1 = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL);
                var RecoHB = dxColesterol_1 == null ? null : dxColesterol_1.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("COLESTEROL", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxColesterol_1 == null ? "NORMAL" : dxColesterol_1.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }
            if (xTrigli_1 != null)
            {
                var hbValor = xTrigli_1.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS);
                var dxTrigli_1 = diagnosticRepository.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS);
                var RecoHB = dxTrigli_1 == null ? null : dxTrigli_1.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase(hbValor == null ? string.Empty : hbValor.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxTrigli_1 == null ? "NORMAL" : dxTrigli_1.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }

            if (xOrina != null)
            {
                var dxOrina = diagnosticRepository.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);
                var RecoHB = dxOrina == null ? null : dxOrina.Recomendations;

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("EXAMEN ORINA", fontColumnValue)));
                cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(dxOrina == null ? "NORMAL" : dxOrina.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase(RecoHB == null ? "CONTROL ANUAL" : string.Join(", ", RecoHB.Select(p => p.v_RecommendationName)), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

            }

            if (xGrupoSanguineo != null)
            {
                var GSValor = xGrupoSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);
                var FSValor = xGrupoSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID);

                // 1era fila
                cells.Add(new PdfPCell(new Phrase("GRUPO SANGUÍNEO", fontSubTitleNegroNegrita)));
                cells.Add(new PdfPCell(new Phrase(GSValor == null ? string.Empty : GSValor.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                cells.Add(new PdfPCell(new Phrase("FACTOR RH", fontSubTitleNegroNegrita)));
                cells.Add(new PdfPCell(new Phrase(FSValor == null ? string.Empty : FSValor.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
            }

            columnWidths = new float[] { 25f, 25f, 25f, 25f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
            document.Add(table);





            #endregion

            #region GrupoSanguineo

            #endregion

            #region Aptitud
            cells = new List<PdfPCell>()
                {
                    //fila
                    new PdfPCell(new Phrase("APTO:", fontColumnValue)),  
                    new PdfPCell(new Phrase(filiationData.i_AptitudeStatusId ==(int)Sigesoft.Common.AptitudeStatus.Apto ? "X" :"", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("APTO CON RESTRICCIÓN: ", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.i_AptitudeStatusId ==(int)Sigesoft.Common.AptitudeStatus.AptRestriccion ? "X" :"", fontColumnValue)),
                    new PdfPCell(new Phrase("NO APTO:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.i_AptitudeStatusId ==(int)Sigesoft.Common.AptitudeStatus.NoApto ? "X" :"", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(new Phrase("OBSERVADO:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.i_AptitudeStatusId ==(int)Sigesoft.Common.AptitudeStatus.AptoObs ? "X" :"", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila
                    new PdfPCell(new Phrase("COMENTARIOS:", fontColumnValue)),  
                    new PdfPCell(new Phrase(filiationData.v_ObsStatusService == "" ? "NINGUNO":filiationData.v_ObsStatusService, fontColumnValue)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //fila
                    new PdfPCell(new Phrase("RESTRICCIONES:", fontColumnValue)),  
                    new PdfPCell(new Phrase(pstrRestriciones== "" ? "NINGUNA" : pstrRestriciones, fontColumnValue)) {Colspan=7,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                };
            columnWidths = new float[] { 20f, 10f, 20f, 10f, 20f, 10f, 20f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "APTITUD", fontTitleTable);

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

       
    }
}
