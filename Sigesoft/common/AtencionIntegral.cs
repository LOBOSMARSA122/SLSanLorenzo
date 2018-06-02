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
    public class AtencionIntegral
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateAtencionIntegral(string filePDF, List<ProblemasList> problemasList, List<TipoAtencionList> planIntegralList)
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

            #region PROBLEMA CRÓNICOS

            var problemasCronicos = problemasList.FindAll(p => p.i_Tipo ==  (int)Sigesoft.Common.TipoProblema.Cronico);

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
                columnWidths = new float[] {100f };
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
                columnWidths = new float[] {100f };
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
            columnWidths = new float[] { 5f, 25f, 70f};
             
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            document.NewPage();

            #region TÍTULO
            
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL DEL ADULTO", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },  
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths,null, fontTitleTable);
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
                new PdfPCell(new Phrase("DATOS GENEREALES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_CENTER },       

                new PdfPCell(new Phrase("Apellidos", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Nombres", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Sexo:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("M", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("F", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Edad:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Merchan Cosme", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Alberto", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Fecha Nacimiento", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("26", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("08", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("1984", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Lugar de Nacimiento", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Procedencia", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Grupo Sanguineo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
                new PdfPCell(new Phrase("Rh", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Lima", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Trujillo", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("O", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
                new PdfPCell(new Phrase("+", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("G° de Instrucción", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Centro Educativo", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Estado Civil", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Ocupación", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Técnico", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Santa Rosa", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Soltero", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Programador", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Madre o Padre, acompañante o cuidador", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Identificación (DNI)", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Rosa Merchan", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("33", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("42708421", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            };

            columnWidths = new float[] { 5f,5f,5f,5f,5f,5f,5f,5f,    5f,5f,5f,5f,5f,5f,5f,5f,   5f,5f,5f,5f};
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ANTECEDENTES
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValueBold)) { Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
    
                new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                new PdfPCell(new Phrase("si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                new PdfPCell(new Phrase("si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Familiares", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                new PdfPCell(new Phrase("si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Consumo tabaco", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("tuberculosis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Inf. Transmisión Sexual", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Consumo alcohol", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Consumo otras drogas", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Hepatitis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Hepatitis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Transfusiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("DBM", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Diabetes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Hospitalización", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("HTA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("HTA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Inter. Quirúrgica", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Infarto", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Obesidad/sobrepeso", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Cancer", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Cancer", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Infarto cardiaco", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("? Cáncer de cervix / mama", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Depresión", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Dislipidemia(colesterol)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("? Patología prostática", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Prob. Psiquiatricos", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Enf. Renal", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Discapacidad", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Otros", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Visuales(glaucoma)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Prob. Laborales", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".........", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Riesgo Ocupacional", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".........", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Depresión", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Violencia política", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".........", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Esquizofrenia", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("..........", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".........", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },


                new PdfPCell(new Phrase("Descripción de antecendentes y otros: ", fontColumnValue)) { Colspan =3,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan =6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },   

            };

            columnWidths = new float[] { 22f, 5f, 5f,    22f, 5f, 5f,    22f, 5f, 5f };
             table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
             document.Add(table);

            #endregion

            #region MEDICAMENTO

             cells = new List<PdfPCell>()
                {          
                    new PdfPCell(new Phrase("Reacción Alérgica a Medicamentos", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                    new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Medicamentos de uso frecuente", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                    new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("(dosis, tiempo de uso u otra observación)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    
                    new PdfPCell(new Phrase(".", fontColumnValue)) {Colspan = 6, Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                };

             columnWidths = new float[] { 20f, 5f, 5f, 5f, 5f, 60f };
             table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
             document.Add(table);

             #endregion

            #region MEDICAMENTO

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("Sexualidad:", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase("Edad de inicio de Relación sexual", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("Número de parejas sexuales últimos 3 meses", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan=4,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase("Hijos vivos:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("RS con personas del mismo sexo: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("Si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            };

            columnWidths = new float[] { 10f, 20f, 5f, 30f, 5f, 5f, 5f, 5f};
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region MENARQUIA

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("Menarquia:", fontColumnValueBold)) { Colspan= 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan= 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("Fecha de última regla", fontColumnValue)) { Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("R/C", fontColumnValue)) {Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan=10,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Flujo vaginal patológico:", fontColumnValueBold)) { Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("Si", fontColumnValue)) {Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Dismenorrea", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Embarazo:", fontColumnValueBold)) { Colspan=4,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan=2,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("Parto:", fontColumnValue)) { Colspan=4,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("Prematuro:", fontColumnValue)) { Colspan=3,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Aborto", fontColumnValue)) { Colspan=3,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) {  Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase("N°", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("año", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("CPN", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("Complicación", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Parto", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Peso RN", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Puerpio", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 5, Rowspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Gestación", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase(".", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase(".", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase(".", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase(".", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Gestación", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("..", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("..", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Gestación", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("..", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("..", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("..", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


            #endregion

            document.NewPage();

            #region Preventivos Adulto
            
            #endregion
            document.Add(new Paragraph("\r\n"));



            document.Close();
            writer.Close();
            writer.Dispose();
        }

    }
}
