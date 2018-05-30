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
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
                pdfPage page = new pdfPage();
                page.Dato = "ILAB_CLINICO/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
                writer.PageEvent = page;
                document.Open();

                #region Fonts
                Font fontTitle1 = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValue = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));               
                #endregion

                #region Declaration Tables
                var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
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
                         
                #region Title
                PdfPCell CellLogo = null;
                cells = new List<PdfPCell>();
                PdfPCell cellPhoto1 = null;
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
                    new PdfPCell(new Phrase("LABORATORIO CLÍNICO", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },              
                };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                cells.Add(CellLogo);
                cells.Add(new PdfPCell(table));
                columnWidths = new float[] { 20f, 80f};
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                document.Add(table);
                #endregion             
           
                #region Datos personales del trabajador

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("PACIENTE:", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)), 
                    new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)), 
    
                    new PdfPCell(new Phrase("PUESTO:", fontColumnValue)), 
                    new PdfPCell(new Phrase(filiationData.v_CurrentOccupation, fontColumnValue)),     
                    new PdfPCell(new Phrase("FECHA ATENCIÓN:", fontColumnValue)), 
                    new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)), 

                    new PdfPCell(new Phrase("EDAD:", fontColumnValue)), 
                    new PdfPCell(new Phrase(filiationData.i_Age.Value.ToString(), fontColumnValue)),
                    new PdfPCell(new Phrase("SEXO:", fontColumnValue)), 
                    new PdfPCell(new Phrase(filiationData.v_SexTypeName, fontColumnValue)), 
                    

                         //cells.Add(new PdfPCell(new Phrase("FORMULA LEUCOCITARIA", fontColumnValueNegrita)) { Colspan = 4 });
                };

                columnWidths = new float[] { 15f, 35f, 15f, 35f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "I. DATOS PERSONALES", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion
                
                #region Bioquimica
                string[] groupBioquimica = new string[]
                 {
                    Sigesoft.Common.Constants.TRIGLICERIDOS_ID, 
                    Sigesoft.Common.Constants.COLESTEROL_ID, 
                    Sigesoft.Common.Constants.GLUCOSA_SL_ID, 
                    Sigesoft.Common.Constants.PERFIL_LIPIDICO, 
                    Sigesoft.Common.Constants.PERFIL_HEPATICO_ID, 
                    Sigesoft.Common.Constants.ACIDO_URICO_SL, 
                    Sigesoft.Common.Constants.TIPO_DE_SANGRIA_ID, 
                    Sigesoft.Common.Constants.TIEMPO_COAGULACION_ID, 
                 };

                var examenesLab = serviceComponent.FindAll(p => p.i_CategoryId == 1);

                var examenesBioquimica = examenesLab.FindAll(p => groupBioquimica.Contains(p.v_ComponentId));
                if (examenesBioquimica.Count > 0)
                {
                    cells = new List<PdfPCell>();
                    //cell = new PdfPCell(new Phrase("BIOQUÍMICA", fontSubTitleNegroNegrita))
                    //{
                    //    Colspan = 4,
                    //    BackgroundColor = subTitleBackGroundColor,
                    //    HorizontalAlignment = Element.ALIGN_CENTER,
                    //};
                    //cells.Add(cell);

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    var xTrigliceridos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TRIGLICERIDOS_ID);
                    var xColesterol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_ID);
                    var xGlucosa = serviceComponent.Find(p => p.v_ComponentId ==  Sigesoft.Common.Constants.GLUCOSA_SL_ID);
                    var xPerfilLipidico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_LIPIDICO);
                    var xPerfilHepatico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ID);
                    var xAcidoUrico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ACIDO_URICO_SL);
                    var xTiempoSangria = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_ID);
                    var xTiempoCoagulacion = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_ID);

           
                    if (xTrigliceridos != null)
                    {
                        var Triglicerido = xTrigliceridos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS);
                        var TrigliceridoValord = xTrigliceridos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(Triglicerido == null ? string.Empty : Triglicerido.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Triglicerido == null ? string.Empty : Triglicerido.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(TrigliceridoValord == null ? string.Empty : TrigliceridoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                  
                    if (xColesterol != null)
                    {
                        var colesterol = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_ID);
                        var colesterolValord = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("COLESTEROL", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterolValord == null ? string.Empty : colesterolValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                       
                    }
                  
                    if (xGlucosa != null)
                    {
                        var glucosa = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DESCRIPCION);
                        var glucosaValord = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("GLUCOSA", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(glucosaValord == null ? string.Empty : glucosaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                       
                    }

                    if (xPerfilLipidico != null)
                    {
                        #region PERFIL_LIPIDICO
                        //cells = new List<PdfPCell>();
                        // Subtitulo  ******************
                        cell = new PdfPCell(new Phrase(xPerfilLipidico.v_ComponentName.ToUpper(), fontSubTitleNegroNegrita))
                        {
                            Colspan = 4,
                            BackgroundColor = subTitleBackGroundColor,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                        };

                        cells.Add(cell);
                        //*****************************************

                        if (xPerfilLipidico.ServiceComponentFields.Count > 0)
                        {
                            var colesteroltotal = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL);
                            var colesteroltotalValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL_DESEABLE);

                            var trigliceridos = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS);
                            var trigliceridosValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_DESEABLE);

                            var colesterolldl = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL);
                            var colesterolldlValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_DESEABLE);

                            var colesterolhdl = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL);
                            var colesterolhdlValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_DESEABLE);

                            var colesterolvldl = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL);
                            var colesterolvldlValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL_DESEABLE);

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

                           
                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                            columnWidths = new float[] { 100f };
                        }                       

                        #endregion
                    }

                    if (xPerfilHepatico != null)
                    {
                        #region PERFIL_HEPATICO_ID
                        // Subtitulo  ******************
                        cell = new PdfPCell(new Phrase(xPerfilHepatico.v_ComponentName.ToUpper(), fontSubTitleNegroNegrita))
                        {
                            Colspan = 4,
                            BackgroundColor = subTitleBackGroundColor,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                        };
                        cells.Add(cell);
                        //*****************************************

                        if (xPerfilHepatico.ServiceComponentFields.Count > 0)
                        {

                            var proteinastotales = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_PROTEINAS_TOTALES_ID);
                            var proteinastotalesValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_PROTEINA_TOTALES_DESEABLE_ID);

                            var albumina = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ALBUMINA_ID);
                            var albuminaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ALBUMINA_DESEABLE_ID);

                            var globulina = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GLOBULINA_ID);
                            var globulinaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GLOBULINA_DESEABLE_ID);

                            var tgo = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGO_ID);
                            var tgoValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGO_DESEABLE_ID);

                            var tgp = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_ID);
                            var tgpValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_DESEABLE_ID);

                            var fosfataalcalina = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_FOSFATASA_ALCALINA_ID);
                            var fosfataalcalinaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_FOSFATASA_ALCALINA_DESEABLE_ID);

                            var gamma = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GAMMA_GLUTAMIL_TRANSPEPTIDASA_ID);
                            var gammaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GGTP_DESEABLE_ID);

                            var btotal = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_TOTAL_ID);
                            var btotalValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_TOTAL_DESEABLE_ID);

                            var bdirecta = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_DIRECTA_ID);
                            var bdirectaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_DIRECTA_DESEABLE_ID);

                            var bindirecta = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_INDIRECTA_ID);
                            var bindirectaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_INDIRECTA_DESEABLE_ID);

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

                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                            columnWidths = new float[] { 100f };
                        }
                        #endregion
                    }
                    if (xAcidoUrico != null)
                    {
                        var acidourico = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO);
                        var acidouricoValord = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("ÁCIDO ÚRICO", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(acidourico == null ? string.Empty : acidourico.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(acidouricoValord == null ? string.Empty : acidouricoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(acidourico == null ? string.Empty : acidourico.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        
                    }

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    //table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "BIOQUÍMICA", fontTitleTableNegro, null);
                    document.Add(table);
                
                }   


                #endregion

                #region Hemograma
                #endregion  



                #region Firma y sello Médico
                var lab = serviceComponent.Find(p => p.i_CategoryId == (int)Sigesoft.Common.Consultorio.Laboratorio);
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
    
                document.Close();
                writer.Close();
                writer.Dispose();

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

          #endregion

        #region Utils

        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();

        }

        #endregion   
    }
}
