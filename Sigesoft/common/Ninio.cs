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
    public class Ninio
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateAtencionNinio(string filePDF, List<ProblemasList> problemasList,
             List<TipoAtencionList> planIntegralList,
             DatosAtencion datosAtencion,
             PacientList datosPac,
             List<frmEsoAntecedentesPadre> Antecedentes,
             List<frmEsoCuidadosPreventivosFechas> FechasCP,
             List<frmEsoCuidadosPreventivosComentarios> ComentariosCP)
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
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL NIÑO", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },          
                 
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Primera Hoja

            #region TÍTULO

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL DE SALUD", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.ORANGE },  
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            string sexM =" ";
            string sexF = " ";
            if (datosPac.i_SexTypeId == 1)
            {
                sexM = "X";
            }
            if (datosPac.i_SexTypeId == 2)
            {
                sexF = "X";
            }
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
            #region DATOS GENERALES
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("DATOS GENEREALES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_LEFT },       

                new PdfPCell(new Phrase("Nº de Historia Clínica", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_IdService, fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Código Afiliación SIS u otro Seguro:", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_PersonId, fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("Apellidos", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("CUI / DNI:", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosPac.v_PersonId, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor=BaseColor.BLACK },

                new PdfPCell(new Phrase("Nombres", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_FirstName, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sexo", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("M", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(sexM, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("F", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(sexF, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("F. de Nac.", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(fechaNac[0], fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Dirección / Referencia", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue)) { Colspan = 16, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Madre, Padre o adulto responsable del cuidado del niño", fontColumnValue)) { Colspan = 14, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("DNI", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase(datosPac.v_ContactName, fontColumnValue)) { Colspan = 14, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Problemas y Necesidades", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor= BaseColor.ORANGE},    
                //new PdfPCell(new Phrase("A", fontColumnValue)) { Colspan = 15, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                //new PdfPCell(new Phrase("B", fontColumnValue)) { Colspan = 15, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                //new PdfPCell(new Phrase("C", fontColumnValue)) { Colspan = 15, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                //new PdfPCell(new Phrase("D", fontColumnValue)) { Colspan = 15, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                //new PdfPCell(new Phrase("E", fontColumnValue)) { Colspan = 15, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
              };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

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

                    //cell = new PdfPCell(new Phrase(item.v_PersonId, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    //cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5F, 10f, 30f,25f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "FECHA", "PROBLEMA CRÓNICOS","OBSERVACIÓN" };
            columnWidths = new float[] { 5f, 10f, 40f, 30f };
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
                    
                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                    
                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5F, 10f, 30f, 25f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "FECHA", "PROBLEMAS AGUDOS", "OBSERVACIÓN" };
            columnWidths = new float[] { 5f, 10f, 40f, 30f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "LISTA DE PROBLEMAS AGUDOS", fontTitleTable, columnHeaders);
            document.Add(table);

            #endregion
            #region PLAN DE ATENCIÓN INTEGRAL

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ATENCIONES", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},       

                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL", fontTitleTable)){Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, BackgroundColor= BaseColor.GRAY  },    
                    
                    new PdfPCell(new Phrase("ÍTEM", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },    
                    new PdfPCell(new Phrase("TIPO", fontSubTitleNegroNegrita)){HorizontalAlignment = Element.ALIGN_CENTER },
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

            
            //#region ATENCIONES
            //cells = new List<PdfPCell>()
            //{     
            //    new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 22, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
            //    new PdfPCell(new Phrase("ATENCIONES", fontColumnValueBold)) { Colspan = 22, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},       

            //    new PdfPCell(new Phrase("Nº", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.ORANGE },    
            //    new PdfPCell(new Phrase("Prestaciones de Salud", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.ORANGE }, 
            //    new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE }, 
            //    new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE},
            //    new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE },
            //    new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE },
            //    new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE },
            //    new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE },
            //    new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE },
            //    new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE},
                
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Atención del recién nacido", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 1,Rowspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Control de Crecimiento y desarrollo del niño", fontColumnValue)) { Colspan = 5,Rowspan = 4, HorizontalAlignment = PdfPCell.ALIGN_MIDDLE }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 1,Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Administración de Micro nutrientes (suplemento)", fontColumnValue)) { Colspan = 3,Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("Hierro", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("Vitamina 'A'", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("Otros", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 1,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Sesión de estimulación temprana", fontColumnValue)) { Colspan = 5,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 1,Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Consejería Nutricional", fontColumnValue)) { Colspan = 5,Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 1,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Detección, Dx y Tto de:", fontColumnValue)) { Colspan = 3,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("Anemia", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("Parasitosis", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 1,Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Salud Bucal", fontColumnValue)) { Colspan = 2,Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("Atención odontológica", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("Aplicación de barnices y/o sellantes", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("Tto. Recuperativo (obbturac. y/o exodonc.)", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("9", fontColumnValue)) { Colspan = 1,Rowspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Visita Familiar Integral", fontColumnValue)) { Colspan = 5,Rowspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("10", fontColumnValue)) { Colspan = 1,Rowspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Ateción de patologías prevalentes", fontColumnValue)) { Colspan = 5,Rowspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("11", fontColumnValue)) { Colspan = 1,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Sesiones educativas", fontColumnValue)) { Colspan = 5,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("12", fontColumnValue)) { Colspan = 1,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Sesiones demostrativas", fontColumnValue)) { Colspan = 5,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("13", fontColumnValue)) { Colspan = 1,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    
            //    new PdfPCell(new Phrase("Otros", fontColumnValue)) { Colspan = 5,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
            //    new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //  };

            //columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //document.Add(table);

            //#endregion

            #endregion

            document.NewPage();
            #region Segunda Hoja
            #region Seguimiento Hospitalario

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL DE SALUD", fontTitle1)) { Colspan = 12,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.ORANGE },  
                    
                    new PdfPCell(new Phrase("Establecimiento de Salud", fontColumnValue)) { Colspan = 7,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                    new PdfPCell(new Phrase("Nº de Historia Clínica", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Cod. Afiliación SIS u otro seguro", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                };

            columnWidths = new float[] { 10f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region DATOS GENERALES
            string[] fechaNac2 = datosPac.d_Birthdate.ToString().Split('/',' ');
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("DATOS GENEREALES", fontColumnValueBold)) { Colspan = 21, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.ORANGE},       

                new PdfPCell(new Phrase("Apellidos", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Nombres", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Sexo:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("M", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(sexM, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("F", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(sexF, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Edad:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosPac.Edad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_FirstName, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Fecha de Nacimiento:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(fechaNac2[0], fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(fechaNac2[1], fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(fechaNac2[2], fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Lugar de Nacimiento", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Domicilio / Referencia", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("CUI / DNI", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("G.S.", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Rh", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase(datosPac.v_BirthPlace, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_BloodGroupName, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosPac.v_BloodFactorName, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Grado de Instrucción", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Centro Educativo", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Teléfono Domicilio", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                
                new PdfPCell(new Phrase(datosPac.GradoInstruccion, fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_TelephoneNumber, fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Nombres y Apellidos de la Madre o Padre o Tutor", fontColumnValue)) { Colspan = 11, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Identificación (DNI)", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Cod. Afiliación: SIS ( ) Otro ( )", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 11, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Grado de Instrucción", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Ocupación", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Estado Civil", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Religión", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Nombres y Apellidos de la Madre o Padre o Tutor", fontColumnValue)) { Colspan = 11, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Identificación (DNI)", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Cod. Afiliación: SIS ( ) Otro ( )", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 11, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Grado de Instrucción", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Ocupación", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Estado Civil", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Religión", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f,5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion
            #region ANTECEDENTES
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValueBold)) { Colspan = 25, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.ORANGE},       

                new PdfPCell(new Phrase("I. Antecedentes Personales", fontColumnValueBold)) { Colspan = 25, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.ORANGE},
                
                new PdfPCell(new Phrase("1. Antecedentes Perinatales:", fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("1.3 Nacimiento", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("3. Patológicos", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Si", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("1.1 Embarazo", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Normal", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Complicado", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Edad Gest. al nacer (sem)", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Patología(s) durante la gestación:", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Peso al nacer (gr)", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("SOBA / Asma", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 10, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Talla al nacer (cm)", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Epilepsia", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Perímetro cefálico", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Infecciones", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Nº de embarazo", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Perímetro Torácico", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Hospitalizaciones", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACION").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACION").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACION").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACION").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Atención Prenatal", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Nº APN", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Respiración y llanto al nacer:", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Transfusiones sang.", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Lugar de APN", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Inmediato", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Cirugia", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("1.2 Parto", fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("APGAR", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("1 min", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("5 min", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Alergia a medicamentos", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Parto Eutócico", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Complicado", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Reanimación", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("Complicaciones del parto: ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Patología Neonatal", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Otros antec.", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 10, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Especifique: ", fontColumnValue)) { Colspan = 8,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Especifique: ", fontColumnValue)) { Colspan = 7,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 8,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("- ", fontColumnValue)) { Colspan = 7,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Lugar del parto", fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Hospitalización", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("II. Antecedentes Familiares", fontColumnValueBold)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("EESS", fontColumnValue)) { Colspan = 2, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan =1, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Domicilio", fontColumnValue)) { Colspan = 2, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan =1,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Consult. Partic.X", fontColumnValue)) { Colspan = 3,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan =1,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Timepo de Hospitalización", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Enfermedad", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Quién", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("No", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

               
                new PdfPCell(new Phrase("2. Alimentación", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Atendido por:", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("Primeros 6 meses", fontColumnValueBold)) { Colspan = 4,Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("LME", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("ASMA", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                new PdfPCell(new Phrase("Profesional de Salud:", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Técnico", fontColumnValue)) { Colspan = 4, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1,Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Mixta:", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                 new PdfPCell(new Phrase("Artificial:", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Diabetes", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("ACS", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Familiar", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1,Rowspan = 2,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Inicio de Alimentación complementaria", fontColumnValue)) { Colspan = 5, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 3,Rowspan = 2,  HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Epilepsia", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Alergia a medicinas", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Otro (especificar)", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 6, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Suplemento de Fe <2 años", fontColumnValue)) { Colspan = 6, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Violencia familiar", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Alcoholismo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
               
                new PdfPCell(new Phrase("III. Vivienda / Saneamiento Básico", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Drogadicción", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Agua Potable", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("Especificar", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("Hepat. B", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
               new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Desague", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("Especificar", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("*** Padre(P), Madre(M), Hno(H), Abuelo/a(A), Otro(O)", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },


            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region Inmunizaciones && Control de crecimiento y desarrollo

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("Inmunizaciones", fontColumnValue)){Colspan = 5, Rowspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE },
                new PdfPCell(new Phrase("RCG", fontColumnValue)){Colspan = 2,Rowspan =2 ,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("HVB", fontColumnValue)){Colspan = 2, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("APO", fontColumnValue)){Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Pentavalente", fontColumnValue)){Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Rotavirus", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Neumococo", fontColumnValue)){Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Influenza", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("SPR", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("AMA", fontColumnValue)){Colspan = 2,Rowspan =2 ,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("DTP", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("3º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("3º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("3º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Rº", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("1º R", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º R", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2 ,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("RN", fontColumnValue)){Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Menor de 01 año", fontColumnValue)){Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("1 año", fontColumnValue)){Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2 años", fontColumnValue)){Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                //

                new PdfPCell(new Phrase("Control de crecimiento y desarrollo", fontColumnValue)){Colspan = 5, Rowspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                
                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("3º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("4º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("5º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("6º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("7º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("8º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("9º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("10º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("11º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                
                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("3º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("4º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("5º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("6º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
               
                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},


                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("3º", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("4º", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("3 años", fontColumnValue)){Colspan = 1,Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("3º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("4º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("4 años", fontColumnValue)){Colspan = 1,Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("1º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("3º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("4º", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("5 años", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("6 años", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("7 años", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("8 años", fontColumnValue)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("9 años", fontColumnValue)){Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2, Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 4f, 4f, 4f, 4f, 5f, };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

  
            #region Tamizaje
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("Tamizaje", fontColumnValueBold)){Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("<1a", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("1a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("2a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("3a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("4a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("5a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("6a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("7a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("8a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("9a  ", fontColumnValueBold)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("Neotonal: THS y otros", fontColumnValue)){Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},

                new PdfPCell(new Phrase("Descarte de anemia", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("Descarte de Hb o Hto", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("Descarte de parasitosis", fontColumnValue)){Colspan = 3, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("Examen seriado", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("Test de Graham", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", fontColumnValue)){Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 10,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("Nº HCL", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 7,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
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
