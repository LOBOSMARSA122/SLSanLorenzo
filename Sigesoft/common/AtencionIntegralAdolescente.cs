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
    public class SAtencionIntegralAdolescente
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateAtencionIntegral(string filePDF, 
            List<ProblemasList> problemasList,
             List<TipoAtencionList> planIntegralList,
             DatosAtencion datosAtencion,
             PacientList datosPac,
             List<frmEsoAntecedentesPadre> Antecedentes,
             List<frmEsoCuidadosPreventivosFechas> FechasCP,
             organizationDto infoEmpresaPropietaria,
             Adolescente datosAdoles,
             List<frmEsoCuidadosPreventivosComentarios> ComentariosCP)
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);
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

            var estatico_1 = 15f; 
            var alto_Celda_1 = 15f;
            var alto_Celda_3 = 45f;
            var alto_Celda_4 = 60f;
            var alto_Celda_6 = 90f; 
            var alto_Celda_8 = 120f;
            var alto_Celda_13 = 195f;
            #region PRIMERA PÁGINA
            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            imagenMinsa.ScalePercent(10);
            imagenMinsa.SetAbsolutePosition(400, 785);
            document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL DE SALUD - ADOLESCENTE", fontTitle1)) { BackgroundColor= BaseColor.ORANGE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},
                    new PdfPCell(new Phrase("LISTA DE PROBLEMAS", fontColumnValue)) { BackgroundColor= BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
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
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);
                    if (item.i_EsControlado == 1)
                    {
                        cell = new PdfPCell(new Phrase("SI", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                        cells.Add(cell);
                    }
                    else {
                        cell = new PdfPCell(new Phrase("NO", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                        cells.Add(cell);
                    }

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    count += 1;
                }
                cell = new PdfPCell(new Phrase(null, fontColumnValue)) {Colspan=5, BackgroundColor=BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2 };
                cells.Add(cell);
                columnWidths = new float[] { 5F, 8f, 42f, 8f, 37f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "FECHA", "PROBLEMA CRÓNICOS", "INACTIVO", "OBSERVACIÓN" };
            columnWidths = new float[] { 5F, 8f, 42f, 8f, 37f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable, columnHeaders);
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
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5f, 45f, 8f, 42f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = alto_Celda_1 });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "PROBLEMAS AGUDOS", "FECHA", "OBSERVACIÓN" };
            columnWidths = new float[] { 5f, 45f, 8f, 42f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable, columnHeaders);
            document.Add(table);

            #endregion

            #region PLAN DE ATENCIÓN INTEGRAL

            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL", fontColumnValue)){Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1, BackgroundColor= BaseColor.GRAY  },    
                    
                    new PdfPCell(new Phrase("ÍTEM", fontSubTitleNegroNegrita)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },    
                    new PdfPCell(new Phrase("TIPO", fontSubTitleNegroNegrita)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },
                    new PdfPCell(new Phrase("DESCRIPCIÓN", fontSubTitleNegroNegrita)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1}, 
                    new PdfPCell(new Phrase("FECHA", fontSubTitleNegroNegrita)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 }, 
                    new PdfPCell(new Phrase("LUGAR", fontSubTitleNegroNegrita)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1}, 
                };

            columnWidths = new float[] { 5f, 25f, 30f, 8f, 32f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);

            document.Add(filiationWorker);


            cells = new List<PdfPCell>();
            int nro = 1;
            foreach (var plan in planIntegralList)
            {
                columnWidths = new float[] { 30f, 8f, 32f };
                include = "v_Descripcion,v_Fecha,v_Lugar";

                cell = new PdfPCell(new Phrase(nro.ToString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(plan.Value, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
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
            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            imagenMinsa.ScalePercent(10);
            imagenMinsa.SetAbsolutePosition(400, 785);
            document.Add(imagenMinsa);
            document.Add(new Paragraph("\n"));

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL DEL ADOLESCENTE", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.ORANGE },  
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Fecha
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split('/', ' ');

            cells = new List<PdfPCell>()
                {          
                    new PdfPCell(new Phrase("FECHA", fontColumnValue)) { Rowspan=2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase("Día", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("Mes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("Año", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("", fontColumnValue)) { Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("N° N009-SR00002542", fontColumnValueBold)) { Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment = PdfPCell.ALIGN_MIDDLE }, 

                    new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase(fechaServicio[1], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase(fechaServicio[2], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                
                    
                };

            columnWidths = new float[] { 10f, 5f, 5f, 10f, 50f, 20f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS GENERALES
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
            string sexM = " ";
            string sexF = " ";
            if (datosPac.i_SexTypeId == 1)
            {
                sexM = "X";
            }
            if (datosPac.i_SexTypeId == 2)
            {
                sexF = "X";
            }


            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("DATOS GENEREALES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.ORANGE},       

                new PdfPCell(new Phrase("Apellidos", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Nombres", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Sexo:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("M", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(sexM, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("F", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(sexF, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Edad:", fontColumnValue)) { Colspan = 1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString(), fontColumnValue)) { Colspan = 1,Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
                
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName,fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_FirstName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Fecha Nacimiento", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(fechaNac[0], fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                
                new PdfPCell(new Phrase("Lugar de Nacimiento", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Procedencia", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Grupo Sanguineo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
                new PdfPCell(new Phrase("Rh", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase(datosPac.v_BirthPlace, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_DistrictName+" - "+datosPac.v_ProvinceName+" - "+ datosPac.v_DepartamentName, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_BloodGroupName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
                new PdfPCell(new Phrase(datosPac.v_BloodFactorName, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("G° de Instrucción", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Centro Educativo", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Estado Civil", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Ocupación", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase(datosPac.GradoInstruccion, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_MaritalStatus, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Madre o Padre, acompañante o cuidador", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Identificación (DNI)", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, Rowspan = 2, BackgroundColor=BaseColor.BLACK,HorizontalAlignment= PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase(datosAdoles.v_NombreCuidador, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosAdoles.v_EdadCuidador, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosAdoles.v_DniCuidador, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region ANTECEDENTES PERSONALES Y FAMILIARES

            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValueBold)) { Colspan = 23, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.ORANGE},   
    
                new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { Colspan = 12,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},   
                new PdfPCell(new Phrase("Familiares", fontColumnValueBold)) { Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},  
                
                new PdfPCell(new Phrase("NORMALES", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("Criterio", fontColumnValueBold)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},   
                //new PdfPCell(new Phrase("no se", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("Criterio", fontColumnValueBold)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },   
                //new PdfPCell(new Phrase("no se", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("VIVE CON", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },


                new PdfPCell(new Phrase("Criterio", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },   
                //new PdfPCell(new Phrase("no se", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                 
                new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },           
                new PdfPCell(new Phrase("MADRE", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },           

                new PdfPCell(new Phrase("PERINATALES", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PERINATALES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PERINATALES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PERINATALES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PERINATALES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },           
                new PdfPCell(new Phrase("SOBA/ASMA", fontColumnValue)) { Colspan =4 ,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
                new PdfPCell(new Phrase("OBESIDAD", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OBESIDAD").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OBESIDAD").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OBESIDAD").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OBESIDAD").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("PADRE", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("CRECIMIENTO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CRECIMIENTO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CRECIMIENTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CRECIMIENTO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CRECIMIENTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },        
                new PdfPCell(new Phrase("TRANSF. SANGUINEAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
               new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSF. SANGUINEAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSF. SANGUINEAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSF. SANGUINEAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSF. SANGUINEAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("VIH/SIDA", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("HERMANOS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HERMANOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HERMANOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HERMANOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HERMANOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                
                new PdfPCell(new Phrase("DESARROLLO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("USO DE MEDICINAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE MEDICINAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE MEDICINAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE MEDICINAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE MEDICINAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("HIPERTENSION ARTERIAL", fontColumnValue)) { Colspan =4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERTENSION ARTERIAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERTENSION ARTERIAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERTENSION ARTERIAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERTENSION ARTERIAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("HIJOS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("-", fontColumnValue)) {BackgroundColor= BaseColor.GRAY ,Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("CONSUMO DE DROGAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("DIABETES", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("PAREJAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PAREJAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PAREJAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PAREJAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PAREJAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("VACUNAS", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("INTERVEN. QRIRURGICAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INTERV. QUIRURGICAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INTERV. QUIRURGICAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INTERV. QUIRURGICAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INTERV. QUIRURGICAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("HIPERLIPIDEMIA", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERLIPIDEMIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERLIPIDEMIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERLIPIDEMIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERLIPIDEMIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("OTROS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },


                new PdfPCell(new Phrase("Nombre", fontColumnValueBold)) {Colspan = 3, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment = PdfPCell.ALIGN_MIDDLE,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("DOSIS / FECHA", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("ALERGIAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INFARTO", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFARTO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFARTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFARTO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFARTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("REFERENTE ADULTO: ", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("1º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("2º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("3º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("ACCIDENTES", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("TRANSTORNO PSICOLOGICO", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNO PSICOLOGICO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNO PSICOLOGICO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNO PSICOLOGICO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNO PSICOLOGICO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(datosAdoles.v_ViveCon, fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("DT", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("TRANSTORNOS PSICOLOGICOS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNOS PSICOLOGICOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNOS PSICOLOGICOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNOS PSICOLOGICOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNOS PSICOLOGICOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("DROGAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("INSTRUCCION", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT ,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("P", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("M", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT ,BackgroundColor= BaseColor.GRAY},

                new PdfPCell(new Phrase("S R", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("HOSPITALIZACIONES", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("VIOLENCIA FAMILIAR", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA INTRAFAMILIAR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA INTRAFAMILIAR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA INTRAFAMILIAR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA INTRAFAMILIAR").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("ANALFABETO", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE ANALFABETO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE ANALFABETO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ANALFABETA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ANALFABETA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("H B", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("MADRE ADOLESCENTE", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ADOLESCENTE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ADOLESCENTE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ADOLESCENTE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ADOLESCENTE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("PRIMARIA", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE PRIMARIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE PRIMARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE PRIMARIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE PRIMARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("F A", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("MALTRATO", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MALTRATO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MALTRATO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MALTRATO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MALTRATO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("SECUNDARIA", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE SECUNDARIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE SECUNDARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE SECUNDARIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE SECUNDARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.BLACK,Colspan =6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor= BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor= BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("OTROS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
               new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("SUPERIOR", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
               new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE SUPERIOR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE SUPERIOR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE SUPERIOR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE SUPERIOR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
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
                
                new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY}, 
                new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
                new PdfPCell(new Phrase("CRITERIO", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },

                new PdfPCell(new Phrase("¿ESTUDIA?", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTUDIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTUDIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTUDIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTUDIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("¿TRABAJAS?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRABAJA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRABAJA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRABAJA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRABAJA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("¿ERES ACEPTADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES ACEPTADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES ACEPTADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES ACEPTADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES ACEPTADO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("EJERCICIOS", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "EJERCICIOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "EJERCICIOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "EJERCICIOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "EJERCICIOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("¿DE ACUERDO A LA EDAD?", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DE ACUERDO A LA EDAD").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DE ACUERDO A LA EDAD").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DE ACUERDO A LA EDAD").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DE ACUERDO A LA EDAD").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("¿REMUNERADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REMUNERADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REMUNERADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REMUNERADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REMUNERADO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("¿ERES IGNORADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES IGNORADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES IGNORADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES IGNORADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES IGNORADO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("TABACO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TABACO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TABACO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TABACO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TABACO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("NIVEL", fontColumnValueBold)) {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("¿ESTABLE?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTABLE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTABLE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTABLE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTABLE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("¿ERES RECHAZADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES RECHAZADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES RECHAZADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES RECHAZADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES RECHAZADO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("ALCOHOL", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("NO ESCOLARIZADO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "NO ESCOLARIZADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "NO ESCOLARIZADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase("PRIMARIA", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRIMARIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRIMARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("¿TIEMPO COMPLETO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIEMPO COMPLETO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIEMPO COMPLETO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIEMPO COMPLETO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIEMPO COMPLETO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("¿TIENES AMIGOS?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE AMIGOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE AMIGOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE AMIGOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE AMIGOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("DROGAS", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

                new PdfPCell(new Phrase("SECUNDARIA", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECUNDARIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECUNDARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },         
                new PdfPCell(new Phrase("SUPERIOR", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SUPERIOR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SUPERIOR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
                new PdfPCell(new Phrase("EDAD INICIO DE TRABAJO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosAdoles.v_EdadInicioTrabajo, fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("¿TIENES PAREJA?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE PAREJA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE PAREJA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE PAREJA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE PAREJA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("CONDUCE VEHIC.", fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCE VEHICULO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCE VEHICULO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCE VEHICULO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCE VEHICULO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase("BAJO RENDIMIENTO", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "BAJO RENDIMIENTO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "BAJO RENDIMIENTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "BAJO RENDIMIENTO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "BAJO RENDIMIENTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("TIPO DE TRABAJO: "+"\n" + datosAdoles.v_TipoTrabajo, fontColumnValue)) {Colspan = 6,Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("¿PRACTICAS DEPORTE?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRACTICA DEPORTES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRACTICA DEPORTES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRACTICA DEPORTES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRACTICA DEPORTES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("TELEVISION (Horas/día)", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosAdoles.v_NroHorasTv, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase("DESERCION", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESERCION").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESERCION").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESERCION").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESERCION").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("¿ORGANIZAC. JUVENILES?", fontColumnValue)) {Colspan = 4,Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ORGANIZAC. JUVENILES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ORGANIZAC. JUVENILES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ORGANIZAC. JUVENILES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ORGANIZAC. JUVENILES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("VIDEO JUEGOS (Horas/día)", fontColumnValue)) {Colspan = 4,Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosAdoles.v_NroHorasJuegos, fontColumnValue)) {Colspan = 2, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase("REPITENCIA", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REPITENCIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REPITENCIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REPITENCIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REPITENCIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

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
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("AÑOS", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY,Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
                new PdfPCell(new Phrase("Labios", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LABIOS_SANOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LABIOS_SANOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LABIOS_SANOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LABIOS_SANOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 

                new PdfPCell(new Phrase("MENARQUIA / ESPERMARQUIA", fontColumnValue)) {Colspan = 3, Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(datosAdoles.v_MenarquiaEspermarquia, fontColumnValue)) { Colspan = 1,Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("ABUSO SEXUAL", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABUSO SEXUAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABUSO SEXUAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                   
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABUSO SEXUAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABUSO SEXUAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Carrillos", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARRILLOS_SANOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARRILLOS_SANOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARRILLOS_SANOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARRILLOS_SANOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 

                new PdfPCell(new Phrase("Paladar", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PALADAR_SANO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PALADAR_SANO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PALADAR_SANO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PALADAR_SANO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },


                new PdfPCell(new Phrase("EMBARAZOS", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMBARAZOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMBARAZOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMBARAZOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMBARAZOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
                new PdfPCell(new Phrase("Encía", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENCÍA_SANO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENCÍA_SANO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENCÍA_SANO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENCÍA_SANO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Lengua", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LENGUA_SANO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LENGUA_SANO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LENGUA_SANO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LENGUA_SANO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("EDAD INICIO RELACION SEXUAL", fontColumnValue)) {Colspan = 3, Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(datosAdoles.v_EdadInicioRS, fontColumnValue)) { Colspan = 1,Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("HIJOS", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
                new PdfPCell(new Phrase("Estado clìnico de higiene dental", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Buena", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_BUENA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_BUENA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Regular", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_REGULAR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_REGULAR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Mala", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_MALA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_MALA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                
                new PdfPCell(new Phrase("Caries Dental", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARIES_DENTAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARIES_DENTAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARIES_DENTAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARIES_DENTAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("ABORTOS", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABORTOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABORTOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABORTOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABORTOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Ugencia de Tratamiento", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "URGENCIA_DE_TRATAMIENTO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "URGENCIA_DE_TRATAMIENTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "URGENCIA_DE_TRATAMIENTO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "URGENCIA_DE_TRATAMIENTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("OBSERVACIONES", fontColumnValue)) {Colspan = 4, Rowspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(datosAdoles.v_Observaciones, fontColumnValue)) {Colspan = 17, Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f },

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #endregion
            document.NewPage();

            #region TERCERA PÁGINA

            #region TÍTULO
            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            imagenMinsa.ScalePercent(10);
            imagenMinsa.SetAbsolutePosition(400, 785);
            document.Add(imagenMinsa);
            document.Add(new Paragraph("\n"));

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL ADOLESCENTE", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.ORANGE, FixedHeight=18f  },  
                    new PdfPCell(new Phrase("CUIDADOS PREVENTIVOS - SEGUIMIENTO DE RIESGO - ADOLESCENTES", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = 15f },  
              
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region FECHAS CONSULTAS

            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("CADA CONSULTA", fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT , FixedHeight = estatico_1 },   
                new PdfPCell(new Phrase("FECHA", fontColumnValue)) {Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = estatico_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER , FixedHeight = estatico_1}, 
                    new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER , FixedHeight = estatico_1},               
                    new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER , FixedHeight = estatico_1},                
                    new PdfPCell(new Phrase("COMENTARIOS", fontColumnValue)) {Colspan=5,HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 }, 

                new PdfPCell(new Phrase("Fiebre en los últimos 15 días", fontColumnValue)) {Colspan = 7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                 new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(y => y.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(y => y.Nombre == "FIEBRE EN LOS ÚLTIMOS 15 DIAS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 

    
                new PdfPCell(new Phrase("Tos más de 15 días", fontColumnValue)) {Colspan = 7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 
                    new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(y => y.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(y => y.Nombre == "TOS MÁS DE 15 DIAS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Secreción o lesión en genitales", fontColumnValue)) {Colspan = 7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                    new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(y => y.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(y => y.Nombre == "SECRECIÓN O LESIÓN EN GENITALES").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
          
            };

            columnWidths = new float[] { 2f, 2f, 3f, 3f, 3f, 3f, 3f, 5f, 5f, 5f, 5f, 5f, 5f, 3f, 3f, 3f, 3f, 2f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            if (datosAtencion.Genero.ToUpper() == "FEMENINO")
            {
                cells = new List<PdfPCell>()
            {   
               new PdfPCell(new Phrase("Fem. Fecha de última regla", fontColumnValue)) {Colspan = 7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},
               new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 
                    new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CADA CONSULTA").FirstOrDefault().Hijos.Where(x => x.Nombre == "FECHA DE ULTIMA REGLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                    new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

            };

                columnWidths = new float[] { 2f, 2f, 3f, 3f, 3f, 3f, 3f, 5f, 5f, 5f, 5f, 5f, 5f, 3f, 3f, 3f, 3f, 2f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
            }

            #endregion
            #region PERIODOS
            cells = new List<PdfPCell>()
            {   
                new PdfPCell(new Phrase("LISTA DE PERIODOS", fontColumnValue)) { Colspan = 18, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY,FixedHeight = estatico_1},  

                new PdfPCell(new Phrase("PERIODICAMENTE", fontColumnValueBold)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = estatico_1 },   
                new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = estatico_1 },
                    new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1},               
                    new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].FechaServicio.ToShortDateString(), fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].FechaServicio.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1},             
                    new PdfPCell(new Phrase("COMENTARIOS", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = estatico_1 },
 
                    new PdfPCell(new Phrase("ASPECTOS FÍSICOS Y NUTRICIONALES", fontColumnValue)) {Colspan = 3,Rowspan = 8, Rotation=90, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_8},
                    new PdfPCell(new Phrase("Indice de masa corporal", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                    new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                    new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                    new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                    new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(y => y.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(y => y.Nombre == "INDICE DE MASA CORPORAL").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

               

                new PdfPCell(new Phrase("Desarrollo sexual", fontColumnValue)) {Colspan = 2,Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_3 },
                new PdfPCell(new Phrase("Mamas", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "MAMAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},    
                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "MAMAS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "MAMAS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Vello púbico", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
               new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "VELLO PÚBICO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "VELLO PÚBICO").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "VELLO PÚBICO").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
               

                new PdfPCell(new Phrase("Genitales", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 
                    
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "GENITALES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "GENITALES").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "DESARROLLO SEXUAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "GENITALES").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 


                new PdfPCell(new Phrase("Vacuna", fontColumnValue)) {Colspan = 2,Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_4 },
                new PdfPCell(new Phrase("Antitetánica", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTITETANICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "ANTITETANICA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "ANTITETANICA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Antiamarilica", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANTIAMARILICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "ANTIAMARILICA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "ANTIAMARILICA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 



                new PdfPCell(new Phrase("Contra la hepatitis B", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "CONTRA LA HEPATITIS B").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 


                new PdfPCell(new Phrase("Contra rubeola", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS FISICOS Y NUTRICIONALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "CONTRA LA RUBEOLA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("ASPECTOS PSICOSOCIALES", fontColumnValue)) {Colspan = 3,Rowspan = 13, Rotation=90, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_13},
                new PdfPCell(new Phrase("Habilidades para la vida", fontColumnValue)) {Colspan = 2,Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase("Autoestima", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "AUTOESTIMA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(y => y.Nombre == "AUTOESTIMA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(y => y.Nombre == "AUTOESTIMA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 

                new PdfPCell(new Phrase("Comunicación", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "COMUNICACIÓN").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(y => y.Nombre == "COMUNICACIÓN").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(y => y.Nombre == "COMUNICACIÓN").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Asertividad", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASERTIVIDAD").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(y => y.Nombre == "ASERTIVIDAD").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(y => y.Nombre == "ASERTIVIDAD").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                
                new PdfPCell(new Phrase("Toma de Decisiones", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(x => x.Nombre == "TOMA DE DESICIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(y => y.Nombre == "TOMA DE DESICIONES").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABILIDADES PARA LA VIDA").FirstOrDefault().Hijos.Where(y => y.Nombre == "TOMA DE DESICIONES").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Ansiedad - depresión", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "ANSIEDAD - DEPRESION").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Violencia familiar", fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Violencia política", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) 
                {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Violencia sexual", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "VIOLENCIA SEXUAL").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) 
                {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Pandillas", fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PANDILLAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "PANDILLAS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "PANDILLAS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue))
                {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase("Habitos", fontColumnValue)) {Colspan = 2,Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_4},
                new PdfPCell(new Phrase("Actividad fìsica", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACTIVIDAD FISICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(y => y.Nombre == "ACTIVIDAD FISICA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(y => y.Nombre == "ACTIVIDAD FISICA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 


                new PdfPCell(new Phrase("Uso de alcohol", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE ALCOHOL").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(y => y.Nombre == "USO DE ALCOHOL").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(y => y.Nombre == "USO DE ALCOHOL").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 

                new PdfPCell(new Phrase("Uso de tabaco", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE TABACO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(y => y.Nombre == "USO DE TABACO").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(y => y.Nombre == "USO DE TABACO").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Uso de drogas", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE DROGAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},    

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(y => y.Nombre == "USO DE DROGAS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS PSICOSOCIALES").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "HABITOS").FirstOrDefault().Hijos.Where(y => y.Nombre == "USO DE DROGAS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("ASPECTOS DE SEXUALIDAD", fontColumnValue)) {Colspan = 3,Rowspan = 6, Rotation=90, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_6 },
                new PdfPCell(new Phrase("Uso de método anticonceptivo", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                				new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "USO DE METODOS ANTICONCEPTIVOS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) 
                {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },


                new PdfPCell(new Phrase("Conducta sexual de riesgo", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "CONDUCTA SEXUAL DE RIESGO").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) 
                {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

               new PdfPCell(new Phrase("Dos o más parejas", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
               				new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "DOS O MÁS PAREJAS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue))
                {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase("Sexo sin protección", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},
                				new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "SEXO SIN PROTECCION").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "SEXO SIN PROTECCION").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "SEXO SIN PROTECCION").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) 
                {Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase("RS con personas del mismo sexo", fontColumnValue)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                				new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue))
                {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase("RS con personas de ambos sexos", fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
               				new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               

                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },  

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ASPECTOS DE SEXUALIDAD").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "RS CON PERSONAS DE AMBOS SEXOS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue))
                {Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

            };

            columnWidths = new float[] { 2f, 2f, 3f, 3f, 3f, 3f, 3f, 5f, 5f, 5f, 5f, 5f, 5f, 3f, 3f, 3f, 3f, 2f };
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
           