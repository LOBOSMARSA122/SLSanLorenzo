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

   public class InformeTrabajador
   {
       //public override void OnEndPage(PdfWriter writer, Document doc)
       //{
       //    Paragraph footer = new Paragraph("THANK YOU", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));
       //    footer.Alignment = Element.ALIGN_RIGHT;
       //    PdfPTable footerTbl = new PdfPTable(1);
       //    footerTbl.TotalWidth = 300;
       //    footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

       //    PdfPCell cell = new PdfPCell(footer);
       //    cell.Border = 0;
       //    cell.PaddingLeft = 10;

       //    footerTbl.AddCell(cell);
       //    footerTbl.WriteSelectedRows(0, -1, 415, 30, writer.DirectContent);
       //}
       private static void RunFile(string filePDF)
       {
           Process proceso = Process.Start(filePDF);
           proceso.WaitForExit();
           proceso.Close();
       }

       public static void CreateFichaMedicaTrabajador2(PacientList filiationData,ServiceList doctoPhisicalExam, List<DiagnosticRepositoryList> diagnosticRepository, organizationDto infoEmpresaPropietaria, string filePDF)
       {
           Document document = new Document();
           document.SetPageSize(iTextSharp.text.PageSize.A4);

           //try
           //{NO_BORDER
           // step 2: we create aPA writer that listens to the document
           PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

           //create an instance of your PDFpage class. This is the class we generated above.
           pdfPage page = new pdfPage();
           page.Dato = "FMT2/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
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

           Font fontAptitud = FontFactory.GetFont("Arial",8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

           //Font fontTitleTableNegro = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

           #endregion


           //#region Logo
           PdfPCell CellLogo = null;
           //if (infoEmpresaPropietaria.b_Image != null)
           //{
           //    CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F));
           //}
           //else
           //{
           //    CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue));
           //}
           //Image logo = HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 20F);
           //PdfPTable headerTbl = new PdfPTable(1);
           //headerTbl.TotalWidth = writer.PageSize.Width;
           //PdfPCell cellLogo = new PdfPCell(CellLogo);

           //cellLogo.VerticalAlignment = Element.ALIGN_TOP;
           //cellLogo.HorizontalAlignment = Element.ALIGN_CENTER;

           //cellLogo.Border = PdfPCell.NO_BORDER;
           //headerTbl.AddCell(cellLogo);
           //document.Add(headerTbl);

           //#endregion

           //document.Add(new Paragraph("\r\n"));

           //#region Title
           //Paragraph cTitle = new Paragraph("CARTA DE COMPROMISO", fontTitle1);
           //cTitle.Alignment = Element.ALIGN_CENTER;
           //document.Add(cTitle);
           //#endregion

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

                new PdfPCell(new Phrase("CARTA DE COMPROMISO", fontTitle1))
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
           #region Cabecera
           string XPreocupacional = "( )";
           string PeriodicoAnual = "( )";
           string Retiro = "( )";
           if (filiationData.i_EsoTypeId ==  (int)Sigesoft.Common.TypeESO.PreOcupacional)
           {
               XPreocupacional = "( X )";
           }
           else if (filiationData.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.PeriodicoAnual)
           {
               PeriodicoAnual = "( X )";
           }
           else if (filiationData.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.Retiro)
           {
               Retiro = "( X )";
           }
           string Linea1 = "YO,        " + filiationData.v_OwnerName.ToUpper();
          
           string Linea2 = "IDENTIFICADO CON DNI/CE/PASAPORTE       " + filiationData.v_DocNumber + ",         CON EL CARGO / PUESTO " + filiationData.v_CurrentOccupation == null ? "" : filiationData.v_CurrentOccupation;
          
           string Linea3 = "PERTENECIENTE A LA EMPRESA        " + filiationData.v_FullWorkingOrganizationName.ToUpper();
      
           string Linea4 = "EN EL CENTRO MÉDICO:        " + infoEmpresaPropietaria.v_Name.ToUpper() + ",         EN LA FECHA:" + filiationData.d_ServiceDate.Value.ToShortDateString();
         
           cells = new List<PdfPCell>()
              {
                   //fila
                    new PdfPCell(new Phrase(Linea1, fontColumnValue)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED},     
                    
                    //fila
                    new PdfPCell(new Phrase(Linea2, fontColumnValue)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED},         

                    //fila
                    new PdfPCell(new Phrase(Linea3, fontColumnValue)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED},      
   
                    //fila
                    new PdfPCell(new Phrase("HABIENDO PASADO LA EVALUACIÓN MÉDICA:", fontTitleTable)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
      
                    //fila
                    new PdfPCell(new Phrase(XPreocupacional, fontColumnValue)){ Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},     
                    new PdfPCell(new Phrase("PRE OCUPACIONAL", fontColumnValue)){ Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
                    new PdfPCell(new Phrase(PeriodicoAnual, fontColumnValue)){ Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},     
                    new PdfPCell(new Phrase("ANUAL", fontColumnValue)){ Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
                    new PdfPCell(new Phrase(Retiro, fontColumnValue)){ Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},     
                    new PdfPCell(new Phrase("DE RETIRO", fontColumnValue)){ Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},   

                    //fila
                    new PdfPCell(new Phrase(Linea4, fontColumnValue)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},     
                    
                     //fila
                    new PdfPCell(new Phrase("HE SIDO DEBIDAMENTE INFORMADO DE LOS RESULTADOS OBTENIDOS EN EL EXAMEN MÉDICO, SIENDO LOS DIAGNÓSTICOS ENCONTRADOS:", fontColumnValue)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},     

                   
              };
         
           columnWidths = new float[] { 5f,20f,5f,20f,5f,20f };

           filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

           document.Add(filiationWorker);

           #endregion

           
           #region DETALLE CONCLUSIONES
           cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ÍTEM", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },
                    new PdfPCell(new Phrase("DIAGNÓSTICOS", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                   
                    new PdfPCell(new Phrase("CIE10", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                         
                };

           columnWidths = new float[] { 5f,80f, 15f };

           filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

           document.Add(filiationWorker);

           #endregion
       
           #region Hallazgos y recomendaciones

           cells = new List<PdfPCell>();

           int nro = 1;
           var dx = diagnosticRepository.FindAll(p => p.i_DiagnosticTypeId != (int)Sigesoft.Common.TipoDx.Normal);
           if (dx != null && dx.Count > 0)
           {
               //columnWidths = new float[] { 0.7f, 23.6f };
               //include = "i_Item,v_RecommendationName";

               foreach (var item in dx)
               {
                   cell = new PdfPCell(new Phrase(nro.ToString() + ".- ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                   cells.Add(cell);

                   cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                   cells.Add(cell);
                   cell = new PdfPCell(new Phrase(item.i_DiagnosticTypeId == (int)Sigesoft.Common.TipoDx.Normal ? "---" : item.v_Dx_CIE10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                   cells.Add(cell);
                   //// Crear tabla de recomendaciones para insertarla en la celda que corresponde
                   //table = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue);
                   //cell = new PdfPCell(table);
                   //cells.Add(cell);
                   nro++;
               }

               columnWidths = new float[] { 5f, 80f, 15f };
           }
           else
           {
               cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
               columnWidths = new float[] { 100 };
           }

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro);

           document.Add(table);

           #endregion
          
           #region Texto
           cells = new List<PdfPCell>()
            {
                 //fila
                new PdfPCell(new Phrase("LOS MISMOS QUE ACEPTO Y POR LO TANTO ME COMPROMETO A SEGUIR LAS SIGUIENTES RECOMENDACIONES CLÍNICAS Y DE SALUD OCUPACIONAL QUE EL MÉDICO CONSIDERE.", fontColumnValue)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},     
            };
          
           columnWidths = new float[] { 100f };

           filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

           document.Add(filiationWorker);
           #endregion
           
           #region Recomendaciones

           cells = new List<PdfPCell>()
                {
                     new PdfPCell(new Phrase("ÍTEM", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },    
                    new PdfPCell(new Phrase("RECOMENDACIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                           
                };

           columnWidths = new float[] { 5f,95f};

           filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

           document.Add(filiationWorker);


            cells = new List<PdfPCell>();

            int nroreco = 1;
            if (dx != null && dx.Count > 0)
           {
               foreach (var item in dx)
               {
                   columnWidths = new float[] {95f};
                   include = "v_RecommendationName";

                   cell = new PdfPCell(new Phrase(nroreco.ToString() + ".- ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                   cells.Add(cell);

                   table = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue);
                   cell = new PdfPCell(table);
                   cells.Add(cell);
                   nroreco++;
               }

               columnWidths = new float[] {5f,95f};
                 }
           else
           {
               cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
               columnWidths = new float[] { 100 };
           }

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro);

           document.Add(table);
 

           #endregion

           #region Restricciones

           cells = new List<PdfPCell>()
                {
                     new PdfPCell(new Phrase("ÍTEM", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },    
                    new PdfPCell(new Phrase("RESTRICCIONES", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },                           
                };

           columnWidths = new float[] { 5f, 95f };

           filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

           document.Add(filiationWorker);


           cells = new List<PdfPCell>();

           int nroreco1 = 1;
           if (dx != null && dx.Count > 0)
           {
               foreach (var item in dx)
               {
                   if (item.Restrictions.Count > 0)
                   {
                   columnWidths = new float[] { 95f };
                   include = "v_RestrictionName";

                   cell = new PdfPCell(new Phrase(nroreco1.ToString() + ".- ", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                   cells.Add(cell);

                   //cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                   //cells.Add(cell);
                   //cell = new PdfPCell(new Phrase(item.i_DiagnosticTypeId == (int)Sigesoft.Common.TipoDx.Normal ? "---" : item.v_Dx_CIE10, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                   //cells.Add(cell);


                   // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                 
                       table = HandlingItextSharp.GenerateTableFromList(item.Restrictions, columnWidths, include, fontColumnValue);
                       cell = new PdfPCell(table);
                       cells.Add(cell);
                       nroreco1++;
                   }
                  
               }

               columnWidths = new float[] { 5f, 95f };
           }
           else
           {
               cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
               columnWidths = new float[] { 100 };
           }

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro);

           document.Add(table);


           #endregion
        
           #region Texto
           cells = new List<PdfPCell>()
            {
                //fila
                new PdfPCell(new Phrase("LAS QUE CUMPLIRÉ ESTRICTAMENTE CON LA FINALIDAD DE PREVENIR LA SEVERIDAD O EMPEORAMIENTO DE LOS PRESENTES CUADROS DIAGNOSTICADOS,  ASÍ MISMO  ME COMPROMETO A CUMPLIR CON LOS CHEQUEOS Y CONTROLES MÉDICOS OCUPACIONALES QUE EL  MÉDICO DE LA CLÍNICA MENCIONADA  INDIQUE.", fontColumnValue)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},     
                //fila
                new PdfPCell(new Phrase("FIRMO EN CONFORMIDAD CON LO ANTES EXPUESTO.", fontColumnValue)){Colspan=6, Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT},     
         
            };

           columnWidths = new float[] { 100f };

           filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

           document.Add(filiationWorker);
           #endregion
           
           document.Add(new Paragraph("\r\n"));
           #region Firma

           #region Creando celdas de tipo Imagen y validando nulls

           // Firma del trabajador ***************************************************
           PdfPCell cellFirmaTrabajador = null;

           if (filiationData.FirmaTrabajador != null)
               cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, 20, 10));
           else
               cellFirmaTrabajador = new PdfPCell(new Phrase("Sin Firma Trabajador", fontColumnValue));

           // Huella del trabajador **************************************************
           PdfPCell cellHuellaTrabajador = null;

           if (filiationData.HuellaTrabajador != null)
               cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, 20, 30));
           else
               cellHuellaTrabajador = new PdfPCell(new Phrase("Sin Huella", fontColumnValue));

           // Firma del doctor EXAMEN FISICO **************************************************

           PdfPCell cellFirma = null;

           if (filiationData == null)
           {
               cellFirma = new PdfPCell(new Phrase("Sin Firma", fontColumnValue));
           }
           else if (filiationData.FirmaDoctorAuditor != null)
           {
               cellFirma = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaDoctorAuditor, null, null, 120, 45));
           }
           else
           {
               cellFirma = new PdfPCell(new Phrase("Sin Firma", fontColumnValue));
           }
           #endregion

           #region Crear tablas en duro (para la Firma y huella del trabajador)

           cells = new List<PdfPCell>();

           cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
           cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
           cellFirmaTrabajador.FixedHeight = 80F;
           cells.Add(cellFirmaTrabajador);
           cells.Add(new PdfPCell(new Phrase("NOMBRE: " + filiationData.v_OwnerName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
           cells.Add(new PdfPCell(new Phrase("D.N.I: " + filiationData.v_DocNumber, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
           cells.Add(new PdfPCell(new Phrase("PUESTO: " + filiationData.v_CurrentOccupation, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
           columnWidths = new float[] { 100f };

           var tableFirmaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

           //***********************************************

           cells = new List<PdfPCell>();

           cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
           cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
           cellHuellaTrabajador.FixedHeight = 80F;
           cells.Add(cellHuellaTrabajador);
           cells.Add(new PdfPCell(new Phrase("HUELLA DEL EXAMINADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

           columnWidths = new float[] { 100f };

           var tableHuellaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

           #endregion

           cells = new List<PdfPCell>();

           // 1 celda vacia              
           cells.Add(new PdfPCell(tableFirmaTrabajador));

           // 1 celda vacia
           cells.Add(new PdfPCell(tableHuellaTrabajador));

           // 2 celda
           cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue));
           cell.HorizontalAlignment = Element.ALIGN_CENTER;
           cell.VerticalAlignment = Element.ALIGN_MIDDLE;
           cells.Add(cell);

           // 3 celda (Imagen)
           cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
           cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
           cellFirma.FixedHeight = 30F;
           cells.Add(cellFirma);

           columnWidths = new float[] { 25f, 25f, 20f, 30F };
           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, " ", fontTitleTable);

           document.Add(table);

           #endregion


           document.Close();
           writer.Close();
           writer.Dispose();

       }
    }
}
