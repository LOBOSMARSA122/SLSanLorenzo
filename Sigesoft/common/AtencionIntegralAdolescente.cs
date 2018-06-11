using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.PropertyGridInternal;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;
using Font = iTextSharp.text.Font;

namespace NetPdf
{
   public class AtencionIntegralAdolescente
    {
       private static void RunFile(string filePDF)
       {
           Process proceso = Process.Start(filePDF);
           proceso.WaitForExit();
           proceso.Close();
       }

        public static void CreateAtencionIntegral(string filePDF, List<ProblemasList> problemasList,
            List<TipoAtencionList> planIntegralList)
        {
            Document document = new Document();
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

            #region Title
            cells = new List<PdfPCell>();

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },           
                    new PdfPCell(new Phrase("LISTA DE PROBLEMAS", fontTitle2)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },             
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            document.Add(new Paragraph("\r\n"));
            #endregion

            #region PRIMERA PÁGINA
            #region PROBLEMA CRÓNICOS

            var problemasCronicos = problemasList.FindAll(p => p.i_Tipo == (int)Sigesoft.Common.TipoProblema.Cronico);

            cells = new List<PdfPCell>();

            if (problemasCronicos != null && problemasCronicos.Count > 0)
            {
                var count = 1;
                foreach (var item in problemasCronicos)
                {
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_EsControlao, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5F, 20f, 30f, 20f, 25f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "FECHA", "PROBLEMA CRÓNICOS", "INACTIVO", "OBSERVACIÓN" };
            columnWidths = new float[] { 5F, 20f, 30f, 20f, 25f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "LISTA DE PROBLEMAS CRÓNICOS", fontTitleTable, columnHeaders);
            document.Add(table);
            #endregion

            #region PROBLEMA AGUDOS

            var problemasAgudos = problemasList.FindAll(p => p.i_Tipo == (int)Sigesoft.Common.TipoProblema.Agudo);

            cells = new List<PdfPCell>();

            if (problemasAgudos != null && problemasAgudos.Count > 0)
            {
                var count = 1;
                foreach (var item in problemasAgudos)
                {
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5f, 45f, 20f, 30f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "PROBLEMAS AGUDOS", "FECHA", "OBSERVACIÓN" };
            columnWidths = new float[] { 5f, 45f, 20f, 30f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "LISTA DE PROBLEMAS AGUDOS", fontTitleTable, columnHeaders);
            document.Add(table);

            #endregion

            #region PLAN DE ATENCIÓN INTEGRAL

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL", fontTitleTable)){Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, BackgroundColor= BaseColor.GRAY  },    
                    
                    new PdfPCell(new Phrase("ÍTEM", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },    
                    new PdfPCell(new Phrase("", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },
                    new PdfPCell(new Phrase("DESCRIPCIÓN", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER }, 
                    new PdfPCell(new Phrase("FECHA", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER }, 
                    new PdfPCell(new Phrase("LUGAR", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER }, 
                };

            columnWidths = new float[] { 5f, 25f, 30f, 20f, 20f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

            document.Add(filiationWorker);


            cells = new List<PdfPCell>();
            int nro = 1;
            foreach (var plan in planIntegralList)
            {
                columnWidths = new float[] { 30f, 20f, 20f };
                include = "v_Descripcion,v_Fecha,v_Lugar";

                cell = new PdfPCell(new Phrase(nro.ToString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(plan.Value, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                cells.Add(cell);

                table = HandlingItextSharp.GenerateTableFromList(plan.List, columnWidths, include, fontColumnValue);
                cell = new PdfPCell(table);
                cells.Add(cell);

                nro += 1;
            }
            columnWidths = new float[] { 5f, 25f, 70f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion
            #endregion

            document.NewPage();

            #region SEGUNDA PÁGINA

            #region TÍTULO

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL DEL ADOLESCENTE", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.ORANGE },  
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Fecha
            cells = new List<PdfPCell>()
                {          
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                    new PdfPCell(new Phrase("día", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("mes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("año", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("N° N009-SR00002542", fontColumnValueBold)) { Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment = PdfPCell.ALIGN_MIDDLE }, 

                    new PdfPCell(new Phrase("FECHA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                    new PdfPCell(new Phrase("01", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("06", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("2018", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    
                };

            columnWidths = new float[] { 10f, 5f, 5f, 10f, 50f, 20f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS GENERALES
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("DATOS GENEREALES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.ORANGE},       

                new PdfPCell(new Phrase("Apellidos", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Nombres", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Sexo:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("M", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("F", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Edad:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Odar Jauregui", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Arnold Mijail", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Fecha Nacimiento", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("02", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("19", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("1995", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Lugar de Nacimiento", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Procedencia", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Grupo Sanguineo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
                new PdfPCell(new Phrase("Rh", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("San Pedro de Lloc", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Tembladera", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("O", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
                new PdfPCell(new Phrase("+", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("G° de Instrucción", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Centro Educativo", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Estado Civil", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Ocupación", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Superior", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("San Isidro", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Soltero", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Programador", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Madre o Padre, acompañante o cuidador", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Identificación (DNI)", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Cecilia Jauregui", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("33", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("12345678", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            
            #endregion

            #region ANTECEDENTES PERSONALES Y FAMILIARES

            //var AntecedentesPersonales = Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "Personales").FirstOrDefault() == null ? null : Antecedentes.Where(x => x.Nombre == "Personales").FirstOrDefault().Hijos;
            //var AntecedentesFamiliares = Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "Familiares").FirstOrDefault() == null ? null : Antecedentes.Where(x => x.Nombre == "Familiares").FirstOrDefault().Hijos;

            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValueBold)) { Colspan = 23, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.ORANGE},   
    
                new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { Colspan = 12,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},   
                new PdfPCell(new Phrase("Familiares", fontColumnValueBold)) { Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},  
                
                new PdfPCell(new Phrase("NORMALES", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Criterio", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("no se", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Criterio", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("no se", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("VIVE CON", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },


                new PdfPCell(new Phrase("Criterio", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("no se", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("MADRE", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("PARENTALES", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SOBA/ASMA", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("OBECIDAD", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("PADRE", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("CRECIMIENTO", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TRANSF. SANGUINEAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("VIH/SIDA", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("PADRE", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("DESARROLLO", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("USO DE MEDICINAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("HIPERTENSION ARTERIAL", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("HIJOS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("---------------------------------------", fontColumnValue)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("CONSUMO DE DROGAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("DIABETES", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("PAREJAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("VACUNAS", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INTERVEN. QRIRURGICAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("HIPERLIPIDEMIA", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("OTROS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Nombre", fontColumnValueBold)) {Colspan = 3, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment = PdfPCell.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("DOSIS / FECHA", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("ALERGIAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("INFARTO", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("REFERENTE ADULTO", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("1º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("2º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("3º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("ACCIDENTES", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TRANSTORNO PSICOLOGICO", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("---------------------------------------", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("DT", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TRANSTORNOS PSICOLOGICOS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("DROGAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("INSTRUCCION", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("P", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("M", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("S R", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("HOSPITALIZACIONES", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("VIOLENCIA FAMILIAR", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("ANALFABETO", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("H B", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("_______________", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("MADRE ADOLESCENTE", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("PRIMARIA", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("F A", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("_______________", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("MALTRATO", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SECUNDARIA", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("---------------------------------------", fontColumnValueBold)) {Colspan =6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("_______________", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("OTROS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SUPERIOR", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region ANTECEDENTES PSICOSOCIALES
            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("ANTECEDENTES PSICOSOCIALES", fontColumnValueBold)) { Colspan = 27, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},   
    
                new PdfPCell(new Phrase("EDUCATIVOS", fontColumnValueBold)) { Colspan = 9,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},   
                new PdfPCell(new Phrase("LABORALES", fontColumnValueBold)) { Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("VIDA SOCIAL", fontColumnValueBold)) { Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("HABITOS", fontColumnValueBold)) { Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},
                
                new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("CRITERIO", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("¿ESTUDIA?", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("¿TRABAJAS?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿ERES ACEPTADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("EJERCICIOS", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("¿DE ACUERDO A LA EDAD?", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("¿REMUNERADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿ERES IGNORADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TABACO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("NIVEL", fontColumnValueBold)) {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿ESTABLE?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿ERES RECHAZADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("ALCOHOL", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("NO ESCOLARIZADO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("PRIMARIA", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿TIEMPO COMPLETO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿TIENES AMIGOS?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("DROGAS", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("SECUNDARIA", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SUPERIOR", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("EDAD INICIO DE TRABAJO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿TIENES PAREJA?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("CONDUCE VEHIC.", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("BAJO RENDIMIENTO", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TIPO DE TRABAJO: _______________________________________________" +
                                        "_________________________________________________________________" +
                                        "___________", fontColumnValue)) {Colspan = 6,Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿PRACTICAS DEPORTE?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TELEVISION (Horas/día)", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("DESERCION", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿ORGANIZAC. JUVENILES?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("VIDEO JUEGOS (Horas/día)", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("REPITENCIA", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-------------------", fontColumnValue)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("-------------------", fontColumnValue)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region SALUD SEXUAL Y REPRODUCTIVA / SALUD BUCAL
            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("SALUD SEXUAL Y REPRODUCTIVA", fontColumnValueBold)) { Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},   
                new PdfPCell(new Phrase("SALUD BUCAL", fontColumnValueBold)) { Colspan = 10,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},
                
                new PdfPCell(new Phrase("-----------------------------------------", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("AÑOS", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("---------------------------------------------------------", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
                new PdfPCell(new Phrase("Labios", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("MENARQUIA / ESPERMARQUIA", fontColumnValue)) {Colspan = 4, Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,Rowspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("ABUSO SEXUAL", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Carrillos", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("Paladar", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("EMBARAZOS", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Encía", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Lengua", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("EDAD INICIO RELACION SEXUAL", fontColumnValue)) {Colspan = 4, Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,Rowspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("HIJOS", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("Estado clìnico de higiene dental", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Buena", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Regular", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Mala", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                 new PdfPCell(new Phrase("Caries Dental", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("ABORTOS", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,Rowspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Uugencia de Tratamiento", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("OBSERVACIONES", fontColumnValue)) {Colspan = 4, Rowspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("-", fontColumnValue)) {Colspan = 17, Rowspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #endregion
            document.NewPage();

            #region TERCERA PÁGINA

            #region TÍTULO


            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL ADOLESCENTE", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.ORANGE },  
                    new PdfPCell(new Phrase("CUIDADOS PREVENTIVOS - SEGUIMIENTO DE RIESGO - ADOLESCENTES", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
              
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region FECHAS CONSULTAS
            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("CADA CONSULTA", fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("COMENTARIO", fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                
                new PdfPCell(new Phrase("Fiebre en los últimos 15 días", fontColumnValue)) {Colspan = 7,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Tos más de 15 días", fontColumnValue)) {Colspan = 7,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Secreción o lesión en genitales", fontColumnValue)) {Colspan = 7,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Fem. Fecha de última regla", fontColumnValue)) {Colspan = 7,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region PERIODOS
            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("LISTA DE PERIODOS", fontTitle1)) { Colspan = 18, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.ORANGE},  

                new PdfPCell(new Phrase("PERIODICAMENTE", fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("COMENTARIO", fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("ASPECTOS FÍSICOS Y NUTRICIONALES", fontColumnValue)) {Colspan = 3,Rowspan = 8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Indice de masa corporal", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Desarrollo sexual", fontColumnValue)) {Colspan = 2,Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Mamas", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Vello púbico", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("genitales", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Vacuna", fontColumnValue)) {Colspan = 2,Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("antitetánica", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("antiamarilica", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("contra la hepatitis B", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("contra rubeola", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
               
                new PdfPCell(new Phrase("ASPECTOS PSICOSOCIALES", fontColumnValue)) {Colspan = 3,Rowspan = 13,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Habilidades para la vida", fontColumnValue)) {Colspan = 2,Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Autoestima", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Comunicación", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Asertividad", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Toma de Decisiones", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Ansiedad - depresión", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Violencia familiar", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Violencia política", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Violencia sexual", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Pandillas", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Habitos", fontColumnValue)) {Colspan = 2,Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Actividad fìsica", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Uso de alcohol", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Uso de tabaco", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Uso de drogas", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("ASPECTOS DE SEXUALIDAD", fontColumnValue)) {Colspan = 3,Rowspan = 6,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Uso de método anticonceptivo", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Conducta sexual de riesgo", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Dos o más parejas", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Sexo sin protección", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("RS con personas del mismo sexo", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("RS con personas de ambos sexos", fontColumnValue)) {Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #endregion


            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
