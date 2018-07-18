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

        public static void CreateLaboratorioReport(PacientList filiationData, 
            List<ServiceComponentList> serviceComponent, 
            organizationDto infoEmpresaPropietaria, 
            string filePDF)
        {
            //Document document = new Document();
           // document.SetPageSize(iTextSharp.text.PageSize.A4);
            Document document = new Document(PageSize.A4, 30f, 30f, 0f, 41f);
            try
            {

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
                pdfPage page = new pdfPage();
                page.Dato = "ILAB_CLINICO/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
                writer.PageEvent = page;
                document.Open();

                #region Fonts
                Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                #endregion

                #region Declaration Tables
                var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
                string include = string.Empty;
                List<PdfPCell> cells = null;
                float[] columnWidths = null;
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
                PdfPTable cabecera = null;
                PdfPCell cell = null;
                document.Add(new Paragraph("\r\n"));
                #endregion

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
                    new PdfPCell(new Phrase("LABORATORIO CLÍNICO", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },              
                };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                cells.Add(CellLogo);
                cells.Add(new PdfPCell(table));
                columnWidths = new float[] { 20f, 80f };
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
                    Sigesoft.Common.Constants.GLUCOSA_ID, 
                    Sigesoft.Common.Constants.PERFIL_LIPIDICO, 
                    Sigesoft.Common.Constants.PERFIL_HEPATICO_ID, 
                    Sigesoft.Common.Constants.BIOQUIMICA01_ID,                     
                    Sigesoft.Common.Constants.UREA_ID, 
                    Sigesoft.Common.Constants.CREATININA_ID,
                 };

                var examenesLab = serviceComponent.FindAll(p => p.i_CategoryId == 1);

                var examenesBioquimica = examenesLab.FindAll(p => groupBioquimica.Contains(p.v_ComponentId));
                if (examenesBioquimica.Count > 0)
                {
                    cells = new List<PdfPCell>();

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    var xTrigliceridos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TRIGLICERIDOS_ID);
                    var xColesterol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_ID);
                    var xGlucosa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GLUCOSA_ID);
                    var xPerfilLipidico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_LIPIDICO);
                    var xPerfilHepatico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ID);
                    var xAcidoUrico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA01_ID);
                    
                    var xUrea = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.UREA_ID);
                    var xCreatinina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CREATININA_ID);


                    if (xTrigliceridos != null)
                    {
                        var Triglicerido = xTrigliceridos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS);
                        var TrigliceridoValord = xTrigliceridos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Triglicerido == null ? string.Empty : Triglicerido.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(TrigliceridoValord == null ? string.Empty : TrigliceridoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Triglicerido == null ? string.Empty : Triglicerido.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                    if (xColesterol != null)
                    {
                        var colesterol = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_ID);
                        var colesterolValord = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("COLESTEROL", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterolValord == null ? string.Empty : colesterolValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                    if (xGlucosa != null)
                    {
                        var glucosa = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_RESULTADO_ID);
                        var glucosaValord = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("GLUCOSA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(glucosaValord == null ? string.Empty : glucosaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    if (xAcidoUrico != null)
                    {
                        var acidourico = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO);
                        var acidouricoValord = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("ÁCIDO ÚRICO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(acidourico == null ? string.Empty : acidourico.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(acidouricoValord == null ? string.Empty : acidouricoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(acidourico == null ? string.Empty : acidourico.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    if (xUrea != null)
                    {
                        var urea = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA);
                        var ureaDeseable = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("UREA SERICA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(urea == null ? string.Empty : urea.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ureaDeseable == null ? string.Empty : ureaDeseable.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(urea == null ? string.Empty : urea.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    if (xCreatinina != null)
                    {
                        var creatinina = xCreatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA);
                        var creatininaDeseable = xCreatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("CREATININA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(creatinina == null ? string.Empty : creatinina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(creatininaDeseable == null ? string.Empty : creatininaDeseable.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(creatinina == null ? string.Empty : creatinina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

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
                            cells = new List<PdfPCell>();
                            cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

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
                            cells.Add(new PdfPCell(new Phrase("COLESTEROL TOTAL", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(colesteroltotal == null ? string.Empty : colesteroltotal.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(colesteroltotalValord == null ? string.Empty : colesteroltotalValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(colesteroltotal == null ? string.Empty : colesteroltotal.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(trigliceridos == null ? string.Empty : trigliceridos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(trigliceridosValord == null ? string.Empty : trigliceridosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(trigliceridos == null ? string.Empty : trigliceridos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("COLESTEROL LDL", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(colesterolldl == null ? string.Empty : colesterolldl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(colesterolldlValord == null ? string.Empty : colesterolldlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(colesterolldl == null ? string.Empty : colesterolldl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("COLESTEROL HDL", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(colesterolhdl == null ? string.Empty : colesterolhdl.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(colesterolhdlValord == null ? string.Empty : colesterolhdlValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(colesterolhdl == null ? string.Empty : colesterolhdl.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("COLESTEROL VLDL", fontColumnValueBold)));
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
                            cells = new List<PdfPCell>();
                            cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

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
                            cells.Add(new PdfPCell(new Phrase("EXMENES AUXILIARES", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(proteinastotales == null ? string.Empty : proteinastotales.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(proteinastotalesValord == null ? string.Empty : proteinastotalesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(proteinastotales == null ? string.Empty : proteinastotales.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("ALBÚMINA", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(albumina == null ? string.Empty : albumina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(albuminaValord == null ? string.Empty : albuminaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(albumina == null ? string.Empty : albumina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("GLOBULINA", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(globulina == null ? string.Empty : globulina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(globulinaValord == null ? string.Empty : globulinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(globulina == null ? string.Empty : globulina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("TGO", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(tgo == null ? string.Empty : tgo.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(tgoValord == null ? string.Empty : tgoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(tgo == null ? string.Empty : tgo.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("TGP", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(tgp == null ? string.Empty : tgp.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(tgpValord == null ? string.Empty : tgpValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(tgp == null ? string.Empty : tgp.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("FOSFATASA ALCALINA", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(fosfataalcalina == null ? string.Empty : fosfataalcalina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(fosfataalcalinaValord == null ? string.Empty : fosfataalcalinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(fosfataalcalina == null ? string.Empty : fosfataalcalina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("GAMMA GLUTAMIL TRANSPEPTIDASA", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(gamma == null ? string.Empty : gamma.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(gammaValord == null ? string.Empty : gammaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(gamma == null ? string.Empty : gamma.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("BILIRRUBINA TOTAL", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(btotal == null ? string.Empty : btotal.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(btotalValord == null ? string.Empty : btotalValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(btotal == null ? string.Empty : btotal.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("BILIRRUBINA DIRECTA", fontColumnValueBold)));
                            cells.Add(new PdfPCell(new Phrase(bdirecta == null ? string.Empty : bdirecta.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(bdirectaValord == null ? string.Empty : bdirectaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            cells.Add(new PdfPCell(new Phrase(bdirecta == null ? string.Empty : bdirecta.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("BILIRRUBINA INDIRECTA", fontColumnValueBold)));
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
                   

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "BIOQUÍMICA ENZIMATICO/COLORIMETRICO AUTOMATIZADO", fontTitleTableNegro, null);
                    document.Add(table);

                }


                #endregion

                #region GRUPO SANGUINEO

                string[] groupSanguineo = new string[]
                 {
                    Sigesoft.Common.Constants.TIPO_DE_SANGRIA_ID,
                    Sigesoft .Common.Constants .TIEMPO_COAGULACION_ID,
                    Sigesoft .Common.Constants .PROTOMBINA_ID,
                    Sigesoft .Common.Constants .INR_ID,
                    Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID, 
                 };
                cells = new List<PdfPCell>();

                cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                var examenesSanguineo = examenesLab.FindAll(p => groupSanguineo.Contains(p.v_ComponentId));
                var xTiempoSangria = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_ID);
                var xTiempoCoagulacion = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_ID);
                var xTiempoProtombina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PROTOMBINA_ID);
                var xINR = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INR_ID);
                
                if (xTiempoSangria != null)
                {
                    var TiempoSangria = xTiempoSangria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_TIEMPO);
                    var TiempoSangriaValord = xTiempoSangria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_DESEABLE);

                    cells.Add(new PdfPCell(new Phrase("TIEMPO DE SANGRIA", fontColumnValueBold)));
                    cells.Add(new PdfPCell(new Phrase(TiempoSangria == null ? string.Empty : TiempoSangria.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(TiempoSangriaValord == null ? string.Empty : TiempoSangriaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(TiempoSangria == null ? string.Empty : TiempoSangria.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }
                if (xTiempoCoagulacion != null)
                {
                    var TiempoCoagulacion = xTiempoCoagulacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_TIEMPO);
                    var TiempoCoagulacionValord = xTiempoCoagulacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_DESEABLE);

                    cells.Add(new PdfPCell(new Phrase("TIEMPO DE COAGULACION", fontColumnValueBold)));
                    cells.Add(new PdfPCell(new Phrase(TiempoCoagulacion == null ? string.Empty : TiempoCoagulacion.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(TiempoCoagulacionValord == null ? string.Empty : TiempoCoagulacionValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(TiempoCoagulacion == null ? string.Empty : TiempoCoagulacion.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }
                if (xTiempoProtombina != null)
                {
                    var TiempoProtombina = xTiempoProtombina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTOMBINA_VALOR);
                    var TiempoProtombinaDeseable = xTiempoProtombina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTOMBINA_VALOR_DESEABLE);

                    cells.Add(new PdfPCell(new Phrase("TIEMPO DE PROTOMBINA", fontColumnValueBold)));
                    cells.Add(new PdfPCell(new Phrase(TiempoProtombina == null ? string.Empty : TiempoProtombina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(TiempoProtombinaDeseable == null ? string.Empty : TiempoProtombinaDeseable.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(TiempoProtombina == null ? string.Empty : TiempoProtombina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }
                if (xINR != null)
                {
                    var INR = xINR.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INR_VALOR);
                    var INRDeseable = xINR.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INR_VALOR_DESEABLE);

                    cells.Add(new PdfPCell(new Phrase("INR", fontColumnValueBold)));
                    cells.Add(new PdfPCell(new Phrase(INR == null ? string.Empty : INR.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(INRDeseable == null ? string.Empty : INRDeseable.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase(INR == null ? string.Empty : INR.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                }

                if (examenesSanguineo.Count > 0)
                {
                    var xSanguineo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID);

                    if (xSanguineo != null)
                    {
                        cells.Add(new PdfPCell(new Phrase("GRUPO Y FACTOR SANGUÍNEO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        var Sanguineo = xSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);
                        var SanguineoValord = xSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID);

                        cells.Add(new PdfPCell(new Phrase("GRUPO SANGUÍNEO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Sanguineo == null ? string.Empty : Sanguineo.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("FACTOR RH", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(SanguineoValord == null ? string.Empty : SanguineoValord.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "HEMATOLOGÍA", fontTitleTableNegro, null);
                    document.Add(table);
                }



                #endregion

                #region Hemograma

                string[] groupHemograma = new string[]
                 {
                    Sigesoft.Common.Constants.HEMOGRAMA,
                 };

                var examenesHemograma = examenesLab.FindAll(p => groupHemograma.Contains(p.v_ComponentId));
                if (examenesHemograma.Count > 0)
                {
                    cells = new List<PdfPCell>();

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    var xHemograma = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA);
                    if (xHemograma != null)
                    {
                        cells.Add(new PdfPCell(new Phrase("HEMOGRAMA", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                        var Hemoglobina = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA);
                        var HemoglobinaValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_DESEABLE);

                        var Hematocrito = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO);
                        var HematocritoValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_DESEABLE);

                        var Hematies = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATIES);
                        var HematiesValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATIES_DESEABLE);

                        var LeucocitosTotales = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEUCOCITOS_TOTALES);
                        var LeucocitosTotalesValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEUCOCITOS_TOTALES_DESEABLE);

                        var Paquetas = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLAQUETAS);
                        var PaquetasValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLAQUETAS_DESEABLE);

                        var Basofilos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_10_9);
                        var BasofilosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_10_9_DESEABLE);

                        var Eosinofilos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID);
                        var EosinofilosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_DESEABLE);

                        var Monocitos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_10_9);
                        var MonocitosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_10_9_DESEABLE);

                        var Linfocitos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS);
                        var LinfocitosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS_DESEABLE);

                        var NeutrofilosSegmentos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_SEG);
                        var NeutrofilosSegmentosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_SEG_DESEABLE);

                        var Mielocitos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_PLAQUETARIO_MEDIO);
                        var MielocitosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_PLAQUETARIO_MEDIO_DESEABLE);

                        var Juveniles = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.JUVENILES);
                        var JuvenilesValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.JUVENILES_DESEABLE);

                        var Abastonados = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ABASTONADOS);
                        var AbastonadosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ABASTONADOS_DESEABLE);

                        var Segmentados = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS_10_9);
                        var SegmentadosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS_10_9_DESEABLE);

                        var VolCorpusMedio = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_CORP_MEDIO);
                        var VolCorpusMedioValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_CORP_MEDIO_DESEABLE);

                        var HemoglobCorpMedia = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HB_CORP_MEDIO);
                        var HemoglobCorpMediaValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HB_CORP_MEDIO_DESEABLE);

                        var ConcHBCorp = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CE_HB_MEDIO);
                        var ConcHBCorpValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CE_HB_MEDIO_DESEABLE);



                        cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Hemoglobina == null ? string.Empty : Hemoglobina.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(HemoglobinaValord == null ? string.Empty : HemoglobinaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Hemoglobina == null ? string.Empty : Hemoglobina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("HEMATOCRITO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Hematocrito == null ? string.Empty : Hematocrito.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(HematocritoValord == null ? string.Empty : HematocritoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Hematocrito == null ? string.Empty : Hematocrito.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("HEMATIES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(HematiesValord == null ? string.Empty : HematiesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS TOTALES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(LeucocitosTotales == null ? string.Empty : LeucocitosTotales.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(LeucocitosTotalesValord == null ? string.Empty : LeucocitosTotalesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(LeucocitosTotales == null ? string.Empty : LeucocitosTotales.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("PLAQUETAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Paquetas == null ? string.Empty : Paquetas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(PaquetasValord == null ? string.Empty : PaquetasValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Paquetas == null ? string.Empty : Paquetas.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("BASOFILOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Basofilos == null ? string.Empty : Basofilos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(BasofilosValord == null ? string.Empty : BasofilosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Basofilos == null ? string.Empty : Basofilos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("EOSINOFILOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Eosinofilos == null ? string.Empty : Eosinofilos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(EosinofilosValord == null ? string.Empty : EosinofilosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Eosinofilos == null ? string.Empty : Eosinofilos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("MONOCITOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Monocitos == null ? string.Empty : Monocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(MonocitosValord == null ? string.Empty : MonocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Monocitos == null ? string.Empty : Monocitos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("LINFOCITOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Linfocitos == null ? string.Empty : Linfocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(LinfocitosValord == null ? string.Empty : LinfocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Linfocitos == null ? string.Empty : Linfocitos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("NEUTROFILOS SEGMENTADOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(NeutrofilosSegmentos == null ? string.Empty : NeutrofilosSegmentos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(NeutrofilosSegmentosValord == null ? string.Empty : NeutrofilosSegmentosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(NeutrofilosSegmentos == null ? string.Empty : NeutrofilosSegmentos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("MIELOCITOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Mielocitos == null ? string.Empty : Mielocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(MielocitosValord == null ? string.Empty : MielocitosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Mielocitos == null ? string.Empty : Mielocitos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("JUVENILES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Juveniles == null ? string.Empty : Juveniles.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(JuvenilesValord == null ? string.Empty : JuvenilesValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Juveniles == null ? string.Empty : Juveniles.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("ABASTONADOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Abastonados == null ? string.Empty : Abastonados.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AbastonadosValord == null ? string.Empty : AbastonadosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Abastonados == null ? string.Empty : Abastonados.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("SEGMENTADOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Segmentados == null ? string.Empty : Segmentados.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(SegmentadosValord == null ? string.Empty : SegmentadosValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Segmentados == null ? string.Empty : Segmentados.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("VOLUMEN CORPUSCULAR MEDIO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(VolCorpusMedio == null ? string.Empty : VolCorpusMedio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(VolCorpusMedioValord == null ? string.Empty : VolCorpusMedioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(VolCorpusMedio == null ? string.Empty : VolCorpusMedio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA CORPUSCIAAR MEDIA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(HemoglobCorpMedia == null ? string.Empty : HemoglobCorpMedia.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(HemoglobCorpMediaValord == null ? string.Empty : HemoglobCorpMediaValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(HemoglobCorpMedia == null ? string.Empty : HemoglobCorpMedia.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("CONCENTRACION HB CORPUSCULAR", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ConcHBCorp == null ? string.Empty : ConcHBCorp.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ConcHBCorpValord == null ? string.Empty : ConcHBCorpValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ConcHBCorp == null ? string.Empty : ConcHBCorp.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });


                    }
                   

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "HEMOGRAMA IMPERANCIA DE FLIJO", fontTitleTableNegro, null);
                    document.Add(table);
                }
                #endregion

                #region VSG
                string[] groupVSG = new string[]
                 {
                   Sigesoft.Common.Constants.VSG_ID,
                 };
                var examenesVSG = examenesLab.FindAll(p => groupVSG.Contains(p.v_ComponentId));
                if (examenesVSG.Count > 0)
                {
                    cells = new List<PdfPCell>();

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                   var xVSG = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.VSG_ID);
                   if (xVSG != null)
                   {
                       var vsg = xVSG.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VSG_RESUL);
                       var vsgValord = xVSG.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VSG_DESEABLE);

                       cells.Add(new PdfPCell(new Phrase("VSG", fontColumnValueBold)));
                       cells.Add(new PdfPCell(new Phrase(vsg == null ? string.Empty : vsg.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                       cells.Add(new PdfPCell(new Phrase(vsgValord == null ? string.Empty : vsgValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                       cells.Add(new PdfPCell(new Phrase(vsg == null ? string.Empty : vsg.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                   }

                   columnWidths = new float[] { 25f, 25f, 25f, 25f };
                   table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "HEMOGRAMA", fontTitleTableNegro, null);
                   document.Add(table);

                }

                #endregion

                #region INMUNOLOGIA INMUNOCROMATOGRAFIA

                string[] groupInmunologia = new string[]
                 {
                    Sigesoft.Common.Constants.R_P_R_ID,
                    Sigesoft.Common.Constants.EXAMEN_ELISA_ID,
                    Sigesoft.Common.Constants.HEPATITIS_A_ID,
                    Sigesoft.Common.Constants.HBSAG_ID,
                    Sigesoft.Common.Constants.VDRL_ID,
                    Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID,                    
                 };

                var examenesInmunologia = examenesLab.FindAll(p => groupInmunologia.Contains(p.v_ComponentId));

                if (examenesInmunologia.Count > 0)
                {
                    cells = new List<PdfPCell>();

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    var xRPR = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.R_P_R_ID);

                    if (xRPR != null)
                    {
                        var rpr = xRPR.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INMUNOLOGIA_R_P_R_ID);

                        cells.Add(new PdfPCell(new Phrase("R.P.R.", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(rpr == null ? string.Empty : rpr.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xExamenElisa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_ELISA_ID);

                    if (xExamenElisa != null)
                    {
                        var ExamenElisa = xExamenElisa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HIV);
                        var ExamenElisaValord = xExamenElisa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("HIV (ANTIGENO - ANTICUERPO)", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ExamenElisa == null ? string.Empty : ExamenElisa.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    }
                    var xHepatitisA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEPATITIS_A_ID);

                    if (xHepatitisA != null)
                    {
                        var HepatitisA = xHepatitisA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A);

                        cells.Add(new PdfPCell(new Phrase("HBSAG (HEPATITS A ANTIGENO DE SUPERFICIE)", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(HepatitisA == null ? string.Empty : HepatitisA.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    }

                    var xReaccSerol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.VDRL_ID);

                    if (xReaccSerol != null)
                    {
                        var ReaccSerol = xReaccSerol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_VDRL_ID);

                        cells.Add(new PdfPCell(new Phrase("REACCIONES SEROLOGICAS (LUES - VDRL)", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ReaccSerol == null ? string.Empty : ReaccSerol.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    }

                   var xB_HCG = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID);

                    if (xB_HCG != null)
                    {
                        var b_hcg = xB_HCG.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_RESULTADO);
                        var b_hcgValord = xB_HCG.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("B-HCG", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(b_hcg == null ? string.Empty : b_hcg.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    }
                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "INMUNOLOGIA INMUNOCROMATOGRAFIA", fontTitleTableNegro, null);
                    document.Add(table);
                }
                #endregion

                #region INMUNOLOGIA
                 string[] groupInmuno = new string[]
                 {
                   
                    Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_ID,
                    Sigesoft.Common.Constants.FACTOR_REMATOIDEO_ID,
                    Sigesoft.Common.Constants.PCR_ID,
                 };

                var examenesInmuno = examenesLab.FindAll(p => groupInmuno.Contains(p.v_ComponentId));

                if (examenesInmuno.Count > 0)
                {
                    var xAglutinaciones = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_ID);
                    var xFReuma = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FACTOR_REMATOIDEO_ID);
                    var xPCR = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PCR_ID);

                    if (xFReuma != null)
                    {
                        var FR = xFReuma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_REMATOIDEO_VALOR);

                        cells.Add(new PdfPCell(new Phrase("FACTOR REMATOIDEO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(FR == null ? string.Empty : FR.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValueBold)));
                    }

                    if (xPCR != null)
                    {
                        var PCR = xPCR.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PCR_VALOR);

                        cells.Add(new PdfPCell(new Phrase("PROTEINA C REACTIVA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(PCR == null ? string.Empty : PCR.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValueBold)));
                    }

                    if (xAglutinaciones != null)
                    {
                        cells = new List<PdfPCell>();
                        cells.Add(new PdfPCell(new Phrase("AGLUTINACIONES", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        
                        var ParatificoA = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A);
                        var ParatificoB = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B);
                        var ParatificoO = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O);
                        var ParatificoH = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H);
                        var Brucella = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA);

                        cells.Add(new PdfPCell(new Phrase("FLAGELAR PARATYFICO A", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ParatificoA == null ? string.Empty : ParatificoA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("FLAGELAR PARATYFICO B", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ParatificoB == null ? string.Empty : ParatificoB.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("FLAGELAR PARATYFICO O", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ParatificoO == null ? string.Empty : ParatificoO.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("FLAGELAR PARATYFICO H", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ParatificoH == null ? string.Empty : ParatificoH.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("BRUCELLA ABORTUS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Brucella == null ? string.Empty : Brucella.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValueBold)));
                    }

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "INMUNOLOGIA", fontTitleTableNegro, null);
                    document.Add(table);
                }
                #endregion

                #region INMUNO ELISA
                string[] groupInmunoElisa = new string[]
                 {                   
                    Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ID,
                 };

                var examenesInmunoElisa = examenesLab.FindAll(p => groupInmunoElisa.Contains(p.v_ComponentId));

                if (examenesInmunoElisa.Count > 0)
                {
                    cells = new List<PdfPCell>();

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    var xPSA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ID);
                    if (xPSA != null)
                    {
                        var PSA = xPSA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ANTIGENO_PROSTATICO_VALOR);
                        var PSADeseable = xPSA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_VALOR_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("ANTIGENO PROSTATICO ESPECIFICO ( PSA )", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(PSA == null ? string.Empty : PSA.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(PSADeseable == null ? string.Empty : PSADeseable.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(PSA == null ? string.Empty : PSA.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    }
                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "INMUNOLOGIA ELISA", fontTitleTableNegro, null);
                    document.Add(table);
                }

                #endregion

                #region MICROBIOLOGIA - BACILOSCOPIA
                string[] groupMicrobiologiaBaciolscopia = new string[]
                 {
                    Sigesoft.Common.Constants.BK_DIRECTO_ID, 
                    Sigesoft.Common.Constants.HISOPADO_NASOFARINGEO_ID,                     
                 };

                var examenesMicrobiologiaBacilo = examenesLab.FindAll(p => groupMicrobiologiaBaciolscopia.Contains(p.v_ComponentId));
                if (examenesMicrobiologiaBacilo.Count > 0)
                {
                    cells = new List<PdfPCell>();

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    var xBK = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BK_DIRECTO_ID);

                    if (xBK != null)
                    {
                        var bk = xBK.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_RESULTADOS);

                        cells.Add(new PdfPCell(new Phrase("BK (ESPUTO)", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(bk == null ? string.Empty : bk.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                                        
                    var xHisopadoNasof = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HISOPADO_NASOFARINGEO_ID);

                    if (xHisopadoNasof != null)
                    {
                        var HisopadoNasof = xHisopadoNasof.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FROTIS_GRAM);

                        cells.Add(new PdfPCell(new Phrase("HISOPADO NASOFARINGEO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(HisopadoNasof == null ? string.Empty : HisopadoNasof.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xReaaccionInfHeces = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_ID);
                    
                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "MICROBIOLOGIA - BACILOSCOPIA", fontTitleTableNegro, null);
                    document.Add(table);
                }

               

                #endregion

                #region MICROBIOLOGIA  - COPROCULTIVO
                string[] groupMicrobiologiaCoprocultivo = new string[]
                 {                     
                    Sigesoft.Common.Constants.SALMONELLA_ID, 
                    Sigesoft.Common.Constants.EXAMEN_SEREADO_EN_HECES_ID, 
                    Sigesoft.Common.Constants.REACCION_INFLAMATORIA_ID,
                    Sigesoft.Common.Constants.FECATEST_ID,
                 };

                var examenesMicrobiologiaCopro = examenesLab.FindAll(p => groupMicrobiologiaCoprocultivo.Contains(p.v_ComponentId));
                if (examenesMicrobiologiaCopro.Count > 0)
                {
                    cells = new List<PdfPCell>();

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    
                    var xSalmonella = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SALMONELLA_ID);

                    if (xSalmonella != null)
                    {
                        var Salmonella = xSalmonella.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SALMONELLA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("CULTIVO DE HECES PARA SALMONELLA TYPHI", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Salmonella == null ? string.Empty : Salmonella.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Salmonella == null ? string.Empty : Salmonella.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                   
                    var xExamenSereadoEnHeces = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_SEREADO_EN_HECES_ID);

                    if (xExamenSereadoEnHeces != null)
                    {
                        var ExamenSereadoEnHeces = xExamenSereadoEnHeces.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SEREADO_EN_HECES_REACTIVOS);

                        cells.Add(new PdfPCell(new Phrase("EXAMEN SEREADO EN HECES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ExamenSereadoEnHeces == null ? string.Empty : ExamenSereadoEnHeces.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ExamenSereadoEnHeces == null ? string.Empty : ExamenSereadoEnHeces.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                   
                    var xReaaccionInfHeces = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_ID);

                    if (xReaaccionInfHeces != null)
                    {
                        var ReaaccionInfHeces = xReaaccionInfHeces.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_RESULTADO_ID);
                        
                        cells.Add(new PdfPCell(new Phrase("REACCION INFLAMATORIA EN HECES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ReaaccionInfHeces == null ? string.Empty : ReaaccionInfHeces.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    #region COprocultivo
                    var xCoprocultivo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FECATEST_ID);

                    if (xCoprocultivo != null)
                    {
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("EXAMEN MACROSCÓPICO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Color = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_COLOR);
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Color == null ? string.Empty : Color.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Color == null ? string.Empty : Color.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Consistencia = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_CONSISTENCIA);
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Consistencia == null ? string.Empty : Consistencia.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Consistencia == null ? string.Empty : Consistencia.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var moco = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_MOCO);
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(moco == null ? string.Empty : moco.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(moco == null ? string.Empty : moco.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("EXAMEN MICROSCOPICO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                        var hematies = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_HEMATIES);
                        cells.Add(new PdfPCell(new Phrase("HEMATÍES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Levaduras = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_LEVADURAS);
                        cells.Add(new PdfPCell(new Phrase("LEVADURAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Levaduras == null ? string.Empty : Levaduras.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Levaduras == null ? string.Empty : Levaduras.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Grasas = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_GRASAS);
                        cells.Add(new PdfPCell(new Phrase("GRASAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Grasas == null ? string.Empty : Grasas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Grasas == null ? string.Empty : Grasas.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Leucocitos = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_LEUCOCITOS);
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Leucocitos == null ? string.Empty : Leucocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Leucocitos == null ? string.Empty : Leucocitos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Polimorfos = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_POLIMORFO_NUCLEARES);
                        cells.Add(new PdfPCell(new Phrase("POLIMORFO NUCLEARES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Polimorfos == null ? string.Empty : Polimorfos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Polimorfos == null ? string.Empty : Polimorfos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Mononucleares = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_MONONUCLEARES);
                        cells.Add(new PdfPCell(new Phrase("MONONUCLEARES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Mononucleares == null ? string.Empty : Mononucleares.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Mononucleares == null ? string.Empty : Mononucleares.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var ReaccInflam = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FECATEST_REACCION_INFLAMATORIA);
                        cells.Add(new PdfPCell(new Phrase("REACCION INFLAMATORIA ", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ReaccInflam == null ? string.Empty : ReaccInflam.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ReaccInflam == null ? string.Empty : ReaccInflam.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        
                    }
                    #endregion
                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "MICROBIOLOGIA - COPROCULTIVO", fontTitleTableNegro, null);
                    document.Add(table);
                }



                #endregion

                #region MICROBIOLOGIA  - EXAMEN COMPLETO DE ORINA
                string[] groupMicrobiologia = new string[]
                 {                     
                    Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID,                    
                 };

                var examenesMicrobiologia = examenesLab.FindAll(p => groupMicrobiologia.Contains(p.v_ComponentId));
                if (examenesMicrobiologia.Count > 0)
                {
                    #region Examen de Orina
                    var xExamenCompletoDeOrina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);

                    if (xExamenCompletoDeOrina != null)
                    {
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("EXAMEN FÍSICO - QUÍMICO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Color = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_COLOR);
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Color == null ? string.Empty : Color.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Color == null ? string.Empty : Color.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Aspecto = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_ASPECTO);
                        cells.Add(new PdfPCell(new Phrase("ASPECTO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Aspecto == null ? string.Empty : Aspecto.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Aspecto == null ? string.Empty : Aspecto.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Densidad = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_DENSIDAD);
                        var DensidadValord = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMP_ORINA_MACROSCOPICO_DENSIDAD_DESEABLE);
                        cells.Add(new PdfPCell(new Phrase("DENSIDAD", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Densidad == null ? string.Empty : Densidad.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(DensidadValord == null ? string.Empty : DensidadValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Densidad == null ? string.Empty : Densidad.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var ReaccionPH = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_PH);
                        var ReaccionPHValord = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMP_ORINA_MACROSCOPICO_PH_DESEABLE);
                        cells.Add(new PdfPCell(new Phrase("REACCION PH", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ReaccionPH == null ? string.Empty : ReaccionPH.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ReaccionPHValord == null ? string.Empty : ReaccionPHValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ReaccionPH == null ? string.Empty : ReaccionPH.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var SangreOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA);
                        cells.Add(new PdfPCell(new Phrase("SANGRE EN ORINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(SangreOrina == null ? string.Empty : SangreOrina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(SangreOrina == null ? string.Empty : SangreOrina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var NitritosOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_NITRITOS);
                        cells.Add(new PdfPCell(new Phrase("NITRITOS EN ORINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(NitritosOrina == null ? string.Empty : NitritosOrina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(NitritosOrina == null ? string.Empty : NitritosOrina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var ProteinasOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_PROTEINAS);
                        cells.Add(new PdfPCell(new Phrase("PROTEINAS EN ORINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(ProteinasOrina == null ? string.Empty : ProteinasOrina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(ProteinasOrina == null ? string.Empty : ProteinasOrina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var GlucosaOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_GLUCOSA);
                        cells.Add(new PdfPCell(new Phrase("GLUCOSA EN ORINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(GlucosaOrina == null ? string.Empty : GlucosaOrina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(GlucosaOrina == null ? string.Empty : GlucosaOrina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var PicmentosBiliares = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_BILIRRUBINA);
                        cells.Add(new PdfPCell(new Phrase("PIGMENTOS BILIARES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(PicmentosBiliares == null ? string.Empty : PicmentosBiliares.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(PicmentosBiliares == null ? string.Empty : PicmentosBiliares.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var UrobinogenoOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_UROBILINOGENO);
                        cells.Add(new PdfPCell(new Phrase("UROBIUNOGENO EN ORINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(UrobinogenoOrina == null ? string.Empty : UrobinogenoOrina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(UrobinogenoOrina == null ? string.Empty : UrobinogenoOrina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Cetonas = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_CETONAS);
                        cells.Add(new PdfPCell(new Phrase("CETONAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Cetonas == null ? string.Empty : Cetonas.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Cetonas == null ? string.Empty : Cetonas.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var AcidoAscorbico = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_FILAMENTO_MUCOIDE);
                        cells.Add(new PdfPCell(new Phrase("ACIDO ASCORBICO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(AcidoAscorbico == null ? string.Empty : AcidoAscorbico.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AcidoAscorbico == null ? string.Empty : AcidoAscorbico.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("EXAMEN MICROSCOPICO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                        var Leucocitos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS);
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS EN ORINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Leucocitos == null ? string.Empty : Leucocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("1 - 2 POR CAMPO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Leucocitos == null ? string.Empty : Leucocitos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Hematies = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES);
                        cells.Add(new PdfPCell(new Phrase("HEMATIES EN ORINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("NEGATIVO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Germenes = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_GERMENES);
                        cells.Add(new PdfPCell(new Phrase("GERMENES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Germenes == null ? string.Empty : Germenes.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Germenes == null ? string.Empty : Germenes.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Cristales = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CRISTALES);
                        cells.Add(new PdfPCell(new Phrase("CRISTALES DEOXALATOS DE CALCIO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Cristales == null ? string.Empty : Cristales.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Cristales == null ? string.Empty : Cristales.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var CristalesDeuratos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CRISTALES_DEURATOS_AMORFOS);
                        cells.Add(new PdfPCell(new Phrase("CRISTALES DEURATOS AMORFOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(CristalesDeuratos == null ? string.Empty : CristalesDeuratos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(CristalesDeuratos == null ? string.Empty : CristalesDeuratos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var FosfatosAmorfos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CRISTALES_FOSFATOS_AMORFOS);
                        cells.Add(new PdfPCell(new Phrase("CRISTALES FOSFATOS AMORFOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(FosfatosAmorfos == null ? string.Empty : FosfatosAmorfos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(FosfatosAmorfos == null ? string.Empty : FosfatosAmorfos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var FosfatosTriples = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CRISTALES_FOSFATOS_TRIPLES);
                        cells.Add(new PdfPCell(new Phrase("CRISTALES FOSFATOS TRIPLES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(FosfatosTriples == null ? string.Empty : FosfatosTriples.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(FosfatosTriples == null ? string.Empty : FosfatosTriples.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Celulas = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CELULAS_EPITELIALES);
                        cells.Add(new PdfPCell(new Phrase("CELULAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Celulas == null ? string.Empty : Celulas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Celulas == null ? string.Empty : Celulas.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Cilindos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CILINDROS);
                        cells.Add(new PdfPCell(new Phrase("CILINDROS HIALINOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Cilindos == null ? string.Empty : Cilindos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Cilindos == null ? string.Empty : Cilindos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var CilindosGranulosos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CILINDROS_GRANULOSOS);
                        cells.Add(new PdfPCell(new Phrase("CILINDROS GRANULOSOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(CilindosGranulosos == null ? string.Empty : CilindosGranulosos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(CilindosGranulosos == null ? string.Empty : CilindosGranulosos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var CilindosLeuco = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CILINDROS_LEUCOCITARIOS);
                        cells.Add(new PdfPCell(new Phrase("CILINDROS LEUCOCITARIOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(CilindosLeuco == null ? string.Empty : CilindosLeuco.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(CilindosLeuco == null ? string.Empty : CilindosLeuco.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var CilindosHema = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CILINDROS_HEMATICOS);
                        cells.Add(new PdfPCell(new Phrase("CILINDROS HEMATICOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(CilindosHema == null ? string.Empty : CilindosHema.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(CilindosHema == null ? string.Empty : CilindosHema.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var Pus = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_SANGRE);
                        cells.Add(new PdfPCell(new Phrase("PUS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Pus == null ? string.Empty : Pus.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Pus == null ? string.Empty : Pus.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        var resultados = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_RESULTADOS_ID);
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)));
                        if (resultados == null)
                        {
                            cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        }
                        else if (resultados.v_Value1 == "0")
                        {
                            cells.Add(new PdfPCell(new Phrase("No Patológico", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("Patológico", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        }
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));

                    }
                    #endregion
                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "MICROBIOLOGIA - EXAMEN COMPLETO DE ORINA", fontTitleTableNegro, null);
                    document.Add(table);
                }



                #endregion

                #region PARASITOLOGIA SIMPLE

                string[] groupParasitologia = new string[]
                 {
                    Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_ID, 
                 };

                var examenesParasitologia = examenesLab.FindAll(p => groupParasitologia.Contains(p.v_ComponentId));

                cells = new List<PdfPCell>();

                if (examenesParasitologia.Count > 0)
                {
                    var xParasitologiaSimple = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_ID);

                    if (xParasitologiaSimple != null)
                    {
                        #region ParasitologiaSimple
                        var color = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_COLOR);
                        var consistencia = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_CONSISTENCIA);
                        var restosAlimenticios = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESTOS_ALIMENTICIOS);
                        var moco = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_MOCO);
                        var hematies = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_HEMATIES);
                        var leucocitos = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_LEUCOCITOS);
                        var levaduras = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_LEVADURAS);
                        var grasas = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_GRASAS);
                        var resultado_Sedim = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESULTADOS_SEDIM);
                        var resultado = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESULTADOS);

                        cells.Add(new PdfPCell(new Phrase("EXAMEN MACROSCÓPICO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(color == null ? string.Empty : color.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(consistencia == null ? string.Empty : consistencia.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(moco == null ? string.Empty : moco.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("RESTOS ALIMENTICIOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(restosAlimenticios == null ? string.Empty : restosAlimenticios.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("EXAMEN MICROSCÓPICO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("HEMATIES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("LEVADURAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(levaduras == null ? string.Empty : levaduras.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("GRASAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(grasas == null ? string.Empty : grasas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("RESULTADOS", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("METODO DIRECTO /(SOLUC.SALINA/LUGOL)", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(resultado == null ? string.Empty : resultado.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("METODO DE SEDIMENTACION", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(resultado_Sedim == null ? string.Empty : resultado_Sedim.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        #endregion
                    }
                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "PARASITOLÓGICO SIMPLE", fontTitleTableNegro, null);
                    document.Add(table);
                }



                #endregion

                #region PARASITOLOGIA SERIADO

                string[] groupParasitologiaSeriado = new string[]
                 {
                   Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_ID,
                 };

                var examenesParasitologiaSeriado = examenesLab.FindAll(p => groupParasitologiaSeriado.Contains(p.v_ComponentId));

                cells = new List<PdfPCell>();

                if (examenesParasitologia.Count > 0)
                {
                    var xParasitologiaSeriado = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_ID);

                    if (xParasitologiaSeriado != null)
                    {
                        #region PRIMERA MUESTRA


                        var color = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_COLOR);
                        var consistencia = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_CONSISTENCIA);
                        var restosAlimenticios = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_RESTOS_ALIMENTICIOS);
                        var hematies = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_HEMATIES);
                        var leucocitos = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_LEUCOCITOS);
                        var moco = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_MOCO);
                        var levaduras = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_LEVADURAS);
                        var directo = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_DIRECTO);
                        var grasas = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_GRASAS);
                        var sedimentacion = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_SEDIMENTACION);

                        cells.Add(new PdfPCell(new Phrase("EXAMEN MACROSCÓPICO", fontColumnValueBold)) { Colspan = 4 });
                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(color == null ? string.Empty : color.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(consistencia == null ? string.Empty : consistencia.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(moco == null ? string.Empty : moco.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("RESTOS ALIMENTICIOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(restosAlimenticios == null ? string.Empty : restosAlimenticios.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("EXAMEN MICROSCÓPICO", fontColumnValueBold)) { Colspan = 4 });
                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("HEMATIES", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(hematies == null ? string.Empty : hematies.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(leucocitos == null ? string.Empty : leucocitos.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("LEVADURAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(levaduras == null ? string.Empty : levaduras.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("GRASAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(grasas == null ? string.Empty : grasas.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                        cells.Add(new PdfPCell(new Phrase("RESULTADOS", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("METODO DIRECTO /(SOLUC.SALINA/LUGOL)", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(directo == null ? string.Empty : directo.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("METODO DE SEDIMENTACION", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(sedimentacion == null ? string.Empty : sedimentacion.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        #endregion

                    }
                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "PARASITOLÓGICO SERIADO", fontTitleTableNegro, null);
                    document.Add(table);
                }



                #endregion

                #region TOXICOLOGIA

                string[] groupToxicologia = new string[]
                 {
                    Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA, 
                    Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID, 
                    Sigesoft.Common.Constants.PLOMO_ID, 
                    Sigesoft.Common.Constants.CADMIO_EN_ORINA_ID, 
                    Sigesoft.Common.Constants.MAGNESIO_ID, 
                    Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS, 
                    Sigesoft.Common.Constants.BARBITURICOS_ID, 
                    Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS, 
                    Sigesoft.Common.Constants.METANFETAMINAS_ID, 
                    Sigesoft.Common.Constants.MORFINA_ID, 
                    Sigesoft.Common.Constants.OPIACEOS_ID, 
                    Sigesoft.Common.Constants.METADONA_ID, 
                    Sigesoft.Common.Constants.FENCICLIDINA_ID, 
                    Sigesoft.Common.Constants.ALCOHOL_EN_SALIVA_ID, 
                    Sigesoft.Common.Constants.EXTASIS_ID, 
                 };

                var examenesToxicologia = examenesLab.FindAll(p => groupToxicologia.Contains(p.v_ComponentId));
                cells = new List<PdfPCell>();

                if (examenesToxicologia.Count > 0)
                {
                 

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    var xDosajeAlcohol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA);

                    if (xDosajeAlcohol != null)
                    {
                        var DosajeAlcohol = xDosajeAlcohol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA_RESULTADO);
                        var DosajeAlcoholValord = xDosajeAlcohol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("DOSAJE DE ALCOHOL", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(DosajeAlcohol == null ? string.Empty : DosajeAlcohol.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(DosajeAlcohol == null ? string.Empty : DosajeAlcohol.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(DosajeAlcoholValord == null ? string.Empty : DosajeAlcoholValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                    var xCocaina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                    if (xCocaina != null)
                    {
                        var Cocaina = xCocaina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA);

                        cells.Add(new PdfPCell(new Phrase("COCAINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Cocaina == null ? string.Empty : Cocaina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Cocaina == null ? string.Empty : Cocaina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xMarihuana = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                    if (xMarihuana != null)
                    {
                        var Marihuana = xMarihuana.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA);

                        cells.Add(new PdfPCell(new Phrase("MARIHUANA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Marihuana == null ? string.Empty : Marihuana.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Marihuana == null ? string.Empty : Marihuana.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                   
                    var xAnfetaminas = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS);

                    if (xAnfetaminas != null)
                    {
                        var Anfetaminas = xAnfetaminas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS_RESULTADO);
                        var AnfetaminasValord = xAnfetaminas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("ANFETAMINAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Anfetaminas == null ? string.Empty : Anfetaminas.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Anfetaminas == null ? string.Empty : Anfetaminas.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AnfetaminasValord == null ? string.Empty : AnfetaminasValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xBarbituricos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BARBITURICOS_ID);

                    if (xBarbituricos != null)
                    {
                        var Barbituricos = xBarbituricos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BARBITURICOS_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("BARBITURICOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Barbituricos == null ? string.Empty : Barbituricos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Barbituricos == null ? string.Empty : Barbituricos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xBenzodiazepinas = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS);

                    if (xBenzodiazepinas != null)
                    {
                        var Benzodiazepinas = xBenzodiazepinas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS_RESULTADO);
                        var BenzodiazepinasValord = xBenzodiazepinas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("BENZODIAZEPINAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Benzodiazepinas == null ? string.Empty : Benzodiazepinas.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Benzodiazepinas == null ? string.Empty : Benzodiazepinas.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(BenzodiazepinasValord == null ? string.Empty : BenzodiazepinasValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xMetanfetaminas = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.METANFETAMINAS_ID);

                    if (xMetanfetaminas != null)
                    {
                        var Metanfetaminas = xMetanfetaminas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.METANFETAMINAS_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("METANFETAMINAS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Metanfetaminas == null ? string.Empty : Metanfetaminas.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Metanfetaminas == null ? string.Empty : Metanfetaminas.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xMorfina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.MORFINA_ID);

                    if (xMorfina != null)
                    {
                        var Morfina = xMorfina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MORFINA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("MORFINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Morfina == null ? string.Empty : Morfina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Morfina == null ? string.Empty : Morfina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xOpiaceos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OPIACEOS_ID);

                    if (xOpiaceos != null)
                    {
                        var Opiaceos = xOpiaceos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OPIACEOS_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("OPIACEOS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Opiaceos == null ? string.Empty : Opiaceos.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Opiaceos == null ? string.Empty : Opiaceos.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xMetadona = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.METADONA_ID);

                    if (xMetadona != null)
                    {
                        var Metadona = xMetadona.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.METADONA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("METADONA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Metadona == null ? string.Empty : Metadona.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Metadona == null ? string.Empty : Metadona.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xFenciclidina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FENCICLIDINA_ID);

                    if (xFenciclidina != null)
                    {
                        var Fenciclidina = xFenciclidina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FENCICLIDINA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("FENCICLIDINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Fenciclidina == null ? string.Empty : Fenciclidina.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Fenciclidina == null ? string.Empty : Fenciclidina.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xAlcoholSaliva = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALCOHOL_EN_SALIVA_ID);

                    if (xAlcoholSaliva != null)
                    {
                        var AlcoholSaliva = xAlcoholSaliva.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALCOHOL_EN_SALIVA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("ALCOHOL EN SALIVA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(AlcoholSaliva == null ? string.Empty : AlcoholSaliva.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(AlcoholSaliva == null ? string.Empty : AlcoholSaliva.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xExtasis = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXTASIS_ID);

                    if (xExtasis != null)
                    {
                        var Extasis = xExtasis.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXTASIS_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("EXTASIS", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Extasis == null ? string.Empty : Extasis.v_Value1Name, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Extasis == null ? string.Empty : Extasis.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xPlomo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_ID);

                    if (xPlomo != null)
                    {
                        var Plomo = xPlomo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_RESULTADO);
                        var PlomoValord = xPlomo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("PLOMO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Plomo == null ? string.Empty : Plomo.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Plomo == null ? string.Empty : Plomo.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(PlomoValord == null ? string.Empty : PlomoValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                    var xCadmio = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CADMIO_EN_ORINA_ID);

                    if (xCadmio != null)
                    {
                        var Cadmio = xCadmio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CADMIO_EN_ORINA_RESULTADO_ID);
                        var CadmioValord = xCadmio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CADMIO_EN_ORINA_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("CADMIO EN ORINA", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Cadmio == null ? string.Empty : Cadmio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Cadmio == null ? string.Empty : Cadmio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(CadmioValord == null ? string.Empty : CadmioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                    var xMagnesio = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.MAGNESIO_ID);

                    if (xMagnesio != null)
                    {
                        var Magnesio = xMagnesio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MAGNESIO_RESULTADO_ID);
                        var MagnesioValord = xMagnesio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MAGNESIO_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("MAGNESIO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Magnesio == null ? string.Empty : Magnesio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Magnesio == null ? string.Empty : Magnesio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(MagnesioValord == null ? string.Empty : MagnesioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "TOXICOLOGIA", fontTitleTableNegro, null);
                    document.Add(table);
                }

              

                #endregion

                #region METALES PESADOS

                string[] groupMetalesPesados = new string[]
                 {
                    Sigesoft.Common.Constants.PLOMO_SANGRE_ID, 
                    Sigesoft.Common.Constants.CADMIO_ID, 
                    Sigesoft.Common.Constants.PLOMO_SANGRE_MAGNESIO_ID, 
                    Sigesoft.Common.Constants.COBRE_ID, 
                 };

                var examenesMetalesPesados = examenesLab.FindAll(p => groupMetalesPesados.Contains(p.v_ComponentId));
                cells = new List<PdfPCell>();

                if (examenesMetalesPesados.Count > 0)
                {
                  

                    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    var xPlomoSangre = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_SANGRE_ID);

                    if (xPlomoSangre != null)
                    {
                        var PlomoSangre = xPlomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE);
                        var PlomoSangreValord = xPlomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("PLOMO EN SANGRE", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(PlomoSangre == null ? string.Empty : PlomoSangre.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(PlomoSangre == null ? string.Empty : PlomoSangre.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(PlomoSangreValord == null ? string.Empty : PlomoSangreValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xCadmio = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CADMIO_ID);

                    if (xCadmio != null)
                    {
                        var Cadmio = xCadmio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CADMIO_RESULTADO);
                        var CadmioValord = xCadmio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CADMIO_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("CADMIO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Cadmio == null ? string.Empty : Cadmio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Cadmio == null ? string.Empty : Cadmio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(CadmioValord == null ? string.Empty : CadmioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xMagnesio = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_SANGRE_MAGNESIO_ID);

                    if (xMagnesio != null)
                    {
                        var Magnesio = xMagnesio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_MAGNESIO_RESULTADO_ID);
                        var MagnesioValord = xMagnesio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_MAGNESIO_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("MAGNESIO", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Magnesio == null ? string.Empty : Magnesio.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Magnesio == null ? string.Empty : Magnesio.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(MagnesioValord == null ? string.Empty : MagnesioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                    var xCobre = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COBRE_ID);

                    if (xCobre != null)
                    {
                        var Cobre = xCobre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COBRE_RESULTADO_ID);
                        var CobreValord = xCobre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COBRE_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("COBRE", fontColumnValueBold)));
                        cells.Add(new PdfPCell(new Phrase(Cobre == null ? string.Empty : Cobre.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(Cobre == null ? string.Empty : Cobre.v_MeasurementUnitName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        cells.Add(new PdfPCell(new Phrase(CobreValord == null ? string.Empty : CobreValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }

                    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "METALES PESADOS", fontTitleTableNegro, null);
                    document.Add(table);
                }

                

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
