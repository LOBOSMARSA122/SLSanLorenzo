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
    public class AtencionIntegralAdulto
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateAtencionIntegral(string filePDF, List<ProblemasList> problemasList, List<TipoAtencionList> planIntegralList, DatosAtencion datosAtencion, List<frmEsoAntecedentesPadre> Antecedentes, List<frmEsoCuidadosPreventivosFechas> FechasCP, List<frmEsoCuidadosPreventivosComentarios> ComentariosCP)
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

            var AntecedentesPersonales = Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "Personales").FirstOrDefault() == null ? null : Antecedentes.Where(x => x.Nombre == "Personales").FirstOrDefault().Hijos;
            var AntecedentesFamiliares = Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "Familiares").FirstOrDefault() == null ? null : Antecedentes.Where(x => x.Nombre == "Familiares").FirstOrDefault().Hijos;

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
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Tuberculosis").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Tuberculosis").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Tuberculosis").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Tuberculosis").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Consumo de tabaco", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo de tabaco").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo de tabaco").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo de tabaco").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo de tabaco").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Tuberculosis").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Tuberculosis").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Tuberculosis").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Tuberculosis").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Inf. Transmisión Sexual", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Inf. Transmisión Sexual").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Inf. Transmisión Sexual").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Inf. Transmisión Sexual").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Inf. Transmisión Sexual").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Consumo de alcohol", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo de alcohol").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo de alcohol").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo de alcohol").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo de alcohol").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "VIH - SIDA").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "VIH - SIDA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "VIH - SIDA").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "VIH - SIDA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIH - SIDA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIH - SIDA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIH - SIDA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIH - SIDA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Consumo otras drogas", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo otras drogas").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo otras drogas").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo otras drogas").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Consumo otras drogas").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Hepatitis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Hepatitis").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Hepatitis").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Hepatitis").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Hepatitis").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Hepatitis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Hepatitis").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Hepatitis").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Hepatitis").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Hepatitis").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Transfusiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Transfusiones").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Transfusiones").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Transfusiones").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Transfusiones").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("DBM", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DBM").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DBM").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DBM").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DBM").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Diabetes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Diabetes").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Diabetes").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Diabetes").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Diabetes").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Hospitalización", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Hospitalización").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Hospitalización").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Hospitalización").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Hospitalización").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("HTA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HTA").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HTA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HTA").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HTA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("HTA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HTA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HTA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HTA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HTA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Interv. Quirúrgica", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Interv. Quirúrgica").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Interv. Quirúrgica").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Interv. Quirúrgica").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Interv. Quirúrgica").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Infarto", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Infarto").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Infarto").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Infarto").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Infarto").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Obesidad / Sobrepeso", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Obesidad / Sobrepeso").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Obesidad / Sobrepeso").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Obesidad / Sobrepeso").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Obesidad / Sobrepeso").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Cáncer", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Cáncer").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Cáncer").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Cáncer").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Cáncer").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Cáncer", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Cáncer").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Cáncer").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Cáncer").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Cáncer").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Infarto cardiaco", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Infarto cardiaco").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Infarto cardiaco").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Infarto cardiaco").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Infarto cardiaco").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("? Cáncer de cervix / mama", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? Cáncer de cervix / mama").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? Cáncer de cervix / mama").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? Cáncer de cervix / mama").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? Cáncer de cervix / mama").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Depresión", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Depresión").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Depresión").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Depresión").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Depresión").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Dislipidemia (Colesterol)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Dislipidemia (Colesterol)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Dislipidemia (Colesterol)").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Dislipidemia (Colesterol)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Dislipidemia (Colesterol)").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("? Patología prostática", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? Patología prostática").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? Patología prostática").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? Patología prostática").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? Patología prostática").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Prob. Psiquiátricos", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Prob. Psiquiátricos").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Prob. Psiquiátricos").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Prob. Psiquiátricos").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "Prob. Psiquiátricos").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Enf. Renal", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Enf. Renal").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Enf. Renal").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Enf. Renal").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Enf. Renal").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Discapacidad", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Discapacidad").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Discapacidad").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Discapacidad").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Discapacidad").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Otros", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Visuales (glaucoma)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Visuales (glaucoma)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Visuales (glaucoma)").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Visuales (glaucoma)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Visuales (glaucoma)").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Prob. Laborales", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Prob. Laborales").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Prob. Laborales").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Prob. Laborales").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Prob. Laborales").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".........", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Convulsiones").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Convulsiones").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Convulsiones").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Convulsiones").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Riesgo ocupacional", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Riesgo ocupacional").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Riesgo ocupacional").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Riesgo ocupacional").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Riesgo ocupacional").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".........", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Depresión", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Depresión").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Depresión").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Depresión").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Depresión").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Violencia política", fontColumnValueApendice)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Violencia política").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Violencia política").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Violencia política").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Violencia política").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(".........", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Esquizofrenia", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Esquizofrenia").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Esquizofrenia").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Esquizofrenia").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "Esquizofrenia").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
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
            #endregion

            document.NewPage();

            #region TERCERA PÁGINA

            #region Preventivos Adulto
            #region TÍTULO


            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL DEL ADULTO", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },  
                    new PdfPCell(new Phrase("CUIDADOS PREVENTIVOS - SEGUIMIENTO DE RIESGO - " +  datosAtencion.Genero.ToUpper() , fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
              
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region CADA CONSULTA

            //if (datosAtencion.Genero.ToUpper() == "MUJER")
            //{   
            //    cells = new List<PdfPCell>() 
            // { 
            //        new PdfPCell(new Phrase("CADA CONSULTA", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("FECHA", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase("COMENTARIOS", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Fiebre en los últimos 15 días", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(y => y.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(y => y.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Tos más de 15 días", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(y => y.Nombre == "Tos más de 15 días").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(y => y.Nombre == "Tos más de 15 días").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Secreción o lesión en genitales", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(y => y.Nombre == "Secreción o lesión en genitales").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "Cada Consulta").FirstOrDefault().Hijos.Where(y => y.Nombre == "Secreción o lesión en genitales").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Fecha de última regla", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Secreción o lesión en genitales").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Secreción o lesión en genitales").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 


            //        new PdfPCell(new Phrase("PERIODICAMENTE ", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("FECHA", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },            
            //        new PdfPCell(new Phrase("COMENTARIOS", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Físico:", fontColumnValue)) {Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Indice de masa corporal", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Físico").FirstOrDefault().Hijos.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "Físico").FirstOrDefault().Hijos.Where(y => y.Nombre == "Indice de masa corporal").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "Físico").FirstOrDefault().Hijos.Where(y => y.Nombre == "Indice de masa corporal").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        /////////////// POR AQUI ME QUEDE ---- (JESUS)
            //        new PdfPCell(new Phrase("Presión arterial", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Presión arterial").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Presión arterial").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 


            //        new PdfPCell(new Phrase("Vacunas:", fontColumnValue)) {Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Antitetánica (3 dosis)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Antitetánica (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Antitetánica (3 dosis)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Antitetánica (3 dosis)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Antiamarílica (zona de riesgo)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Antiamarílica (zona de riesgo)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Contra la hepatitis B (3 dosis)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Examen bucal:", fontColumnValue)) {Rowspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Encias", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Encias").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Encias").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Caries dental", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Caries dental").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Caries dental").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Edentulismo parcial o total", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Edentulismo parcial o total").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Edentulismo parcial o total").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
 
            //        new PdfPCell(new Phrase("Portador de prótesis dental", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Portador de prótesis dental").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Portador de prótesis dental").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Estado de higiene dental", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Estado de higiene dental").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Estado de higiene dental").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Urgencia de tratamiento", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Urgencia de tratamiento").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Urgencia de tratamiento").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Examen:", fontColumnValue)) {Rowspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Visual (> 40 años)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Visual (> 40 años)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Visual (> 40 años)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("De colesterol (> 45 años)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De colesterol (> 45 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "De colesterol (> 45 años)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "De colesterol (> 45 años)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("De glucosa", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "De glucosa").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "De glucosa").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
 
            //        new PdfPCell(new Phrase("De mamas", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De mamas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "De mamas").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "De mamas").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Pélvico y PAP (C/año, C/3 a)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Pélvico y PAP (C/año, C/3 a)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("mamografia (> 50 años, c/2 a)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "mamografia (> 50 años, c/2 a)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Psicosocial:", fontColumnValue)) {Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Ansiedad - Depresión", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Ansiedad - Depresión").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Ansiedad - Depresión").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Ansiedad - Depresión").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Violencia familiar", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Violencia familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Violencia familiar").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Violencia familiar").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Violencia política", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Violencia política").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Violencia política").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Violencia política").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Habitos:", fontColumnValue)) {Rowspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Actividad física", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Actividad física").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Actividad física").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Actividad física").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Uso de alcohol", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Uso de alcohol").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Uso de alcohol").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Uso de alcohol").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Uso de tabaco", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Uso de tabaco").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Uso de tabaco").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Uso de tabaco").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
 
            //        new PdfPCell(new Phrase("Uso de otras drogas", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Uso de otras drogas").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Uso de otras drogas").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Uso de otras drogas").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Sexualidad:", fontColumnValue)) {Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Actividad sexual", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Actividad sexual").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Actividad sexual").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Actividad sexual").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Planificación familiar", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Planificación familiar").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Planificación familiar").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Planificación familiar").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            // };

            //    columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 20f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);
            //}
            //else
            //{
            //    cells = new List<PdfPCell>()
            //{
            //        new PdfPCell(new Phrase("CADA CONSULTA", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("FECHA", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },              
            //        new PdfPCell(new Phrase("COMENTARIOS", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Fiebre en los últimos 15 días", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Fiebre en los últimos 15 días").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Tos más de 15 días", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Tos más de 15 días").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Tos más de 15 días").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Tos más de 15 días").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Secreción o lesión en genitales", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Secreción o lesión en genitales").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Secreción o lesión en genitales").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Secreción o lesión en genitales").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 


            //        new PdfPCell(new Phrase("PERIODICAMENTE ", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("FECHA", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },             
            //        new PdfPCell(new Phrase("COMENTARIOS", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Físico:", fontColumnValue)) {Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Indice de masa corporal", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Indice de masa corporal").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Indice de masa corporal").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Indice de masa corporal").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Presión arterial", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Presión arterial").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Presión arterial").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Presión arterial").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 


            //        new PdfPCell(new Phrase("Vacunas:", fontColumnValue)) {Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Antitetánica (zonas de riesgo)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Antitetánica (zonas de riesgo)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Antiamarílica (zonas de riesgo)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Antiamarílica (zonas de riesgo)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Contra la hepatitis B (3 dosis)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Contra la hepatitis B (3 dosis)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Examen bucal:", fontColumnValue)) {Rowspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("Encias", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Encias").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Encias").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Encias").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Caries dental", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Caries dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Caries dental").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Caries dental").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Edentulismo parcial o total", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Edentulismo parcial o total").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Edentulismo parcial o total").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Edentulismo parcial o total").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
 
            //        new PdfPCell(new Phrase("Portador de prótesis dental", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Portador de prótesis dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Portador de prótesis dental").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Portador de prótesis dental").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Estado de higiene dental", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Estado de higiene dental").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Estado de higiene dental").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Estado de higiene dental").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Urgencia de tratamiento", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Urgencia de tratamiento").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Urgencia de tratamiento").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Urgencia de tratamiento").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("Examen:", fontColumnValue)) {Rowspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
            //        new PdfPCell(new Phrase("visual (> 40 años)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "visual (> 40 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "visual (> 40 años)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "visual (> 40 años)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("De colesterol (> 35 años)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De colesterol (> 35 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "De colesterol (> 35 años)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "De colesterol (> 35 años)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            //        new PdfPCell(new Phrase("De glucosa", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "De glucosa").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "De glucosa").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "De glucosa").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
 
            //        new PdfPCell(new Phrase("Próstata (> de 50 años)", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //        new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
            //        new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "Próstata (> de 50 años)").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },                
            //        new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(y => y.Nombre == "Próstata (> de 50 años)").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(y => y.Nombre == "Próstata (> de 50 años)").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //};
            //    columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 20f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);
            //}
            
            #endregion
            #endregion

            document.Add(new Paragraph("\r\n"));

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }

    }
}
