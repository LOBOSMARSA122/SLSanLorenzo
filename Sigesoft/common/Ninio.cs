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
             organizationDto infoEmpresaPropietaria,
             Ninioo datosNinio,
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
            float[] columnHeight = null;
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

            #region Primera Hoja

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
                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL DE SALUD - NIÑO", fontTitle1)) { BackgroundColor= BaseColor.ORANGE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},  
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

            var estatico_1 = 15f;
            var estatico_2 = 30f;
            var alto_Celda_1 = 15f;
            var alto_Celda_2 = 30f;
            var alto_Celda_3 = 60f;
            var alto_Celda_4 = 75f;
            #region DATOS GENERALES
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("DATOS GENEREALES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = estatico_1},       

                new PdfPCell(new Phrase("Nº de Historia Clínica", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},    
                new PdfPCell(new Phrase(datosPac.v_IdService, fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1}, 
                new PdfPCell(new Phrase("Código Afiliación SIS u otro Seguro:", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = estatico_1}, 
                new PdfPCell(new Phrase("N° " + datosPac.v_PersonId, fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},
                
                new PdfPCell(new Phrase("Apellidos", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 }, 
                new PdfPCell(new Phrase("CUI / DNI:", fontColumnValue)) { Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1}, 
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 6,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},

                new PdfPCell(new Phrase("Nombres", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},    
                new PdfPCell(new Phrase(datosPac.v_FirstName, fontColumnValue)) { Colspan = 7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },
                new PdfPCell(new Phrase("Sexo", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },
                new PdfPCell(new Phrase("M", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },
                new PdfPCell(new Phrase(sexM, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},
                new PdfPCell(new Phrase("F", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },
                new PdfPCell(new Phrase(sexF, fontColumnValue)) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},
                new PdfPCell(new Phrase("F. de Nac.", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 }, 
                new PdfPCell(new Phrase(fechaNac[0], fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},

                new PdfPCell(new Phrase("Dirección / Referencia", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},    
                new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 }, 

                new PdfPCell(new Phrase("Madre, Padre o adulto responsable del cuidado del niño", fontColumnValue)) { Colspan = 14,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },
                new PdfPCell(new Phrase("DNI", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},

                new PdfPCell(new Phrase(datosNinio.v_NombreCuidador, fontColumnValue)) { Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },    
                new PdfPCell(new Phrase(datosNinio.v_EdadCuidador, fontColumnValue)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },
                new PdfPCell(new Phrase(datosNinio.v_DniCuidador, fontColumnValue)) { Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1 },

                new PdfPCell(new Phrase("Problemas y Necesidades", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor= BaseColor.ORANGE, FixedHeight = estatico_1},    
                
               
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
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    //cell = new PdfPCell(new Phrase(item.v_PersonId, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    //cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5F, 10f, 30f,25f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "FECHA", "PROBLEMA CRÓNICOS","OBSERVACIÓN" };
            columnWidths = new float[] { 5f, 10f, 40f, 30f };
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
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.d_Fecha.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Descripcion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(item.v_Observacion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 };
                    cells.Add(cell);

                    count += 1;
                }
                columnWidths = new float[] { 5F, 10f, 30f, 25f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PROBLEMAS CRÓNICOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "FECHA", "PROBLEMAS AGUDOS", "OBSERVACIÓN" };
            columnWidths = new float[] { 5f, 10f, 40f, 30f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable, columnHeaders);
            document.Add(table);

            #endregion
            #region PRESTACIONES DE SALUD

            cells = new List<PdfPCell>()
                {
                new PdfPCell(new Phrase("ATENCIONES", fontColumnValue)) { Colspan = 28, BackgroundColor=BaseColor.ORANGE , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},    

                new PdfPCell(new Phrase("N°", fontColumnValue)) { Colspan = 1, Rowspan=2, BackgroundColor=BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_2 },    
                new PdfPCell(new Phrase("PRESTACIONES DE SALUD", fontColumnValue)) { Colspan = 7,Rowspan=2, BackgroundColor=BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_2},    
                new PdfPCell(new Phrase("FECHAS", fontColumnValue)) { Colspan = 16, BackgroundColor=BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_1},
                new PdfPCell(new Phrase("COMENTARIOS", fontColumnValue)) { Colspan = 4,Rowspan=2, BackgroundColor=BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = estatico_2},
                
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].FechaServicio.ToShortDateString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].FechaServicio.ToShortDateString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].FechaServicio.ToShortDateString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].FechaServicio.ToShortDateString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].FechaServicio.ToShortDateString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].FechaServicio.ToShortDateString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },     
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].FechaServicio.ToShortDateString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].FechaServicio.ToShortDateString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },

                new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},
                new PdfPCell(new Phrase("Atenciones del recién nacido", fontColumnValue)) { Colspan = 7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(y => y.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(y => y.Nombre == "ATENCIÓN DEL RECIÉN NACIDO").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase("Control de Crecimiento y Desarrollo del Niño", fontColumnValue)) { Colspan = 7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = alto_Celda_1},                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(y => y.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "CONTROL").FirstOrDefault().Hijos.Where(y => y.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 1, Rowspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight = alto_Celda_3},
                new PdfPCell(new Phrase("Administración de Micro nutrientes", fontColumnValue)) { Colspan = 4, Rowspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight = alto_Celda_3},
                new PdfPCell(new Phrase("Hierro", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight = alto_Celda_1},
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },
   
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : 
                FechasCP[6].Listado == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault() == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },    

                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : 
                FechasCP[7].Listado == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault() == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIERRO").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(y => y.Nombre == "HIERRO").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(y => y.Nombre == "HIERRO").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },

                new PdfPCell(new Phrase("Vitamina A", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },
   
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : 
                FechasCP[6].Listado == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault() == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : 
                FechasCP[7].Listado == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault() == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VITAMINA A").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(y => y.Nombre == "VITAMINA A").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(y => y.Nombre == "VITAMINA A").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },

                new PdfPCell(new Phrase("Otros", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : 
                FechasCP[0].Listado == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },               


                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : 
                FechasCP[1].Listado == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  }, 
                    

                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : 
                FechasCP[2].Listado == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : 
                FechasCP[3].Listado == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : 
                FechasCP[4].Listado == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : 
                FechasCP[5].Listado == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },
   
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : 
                FechasCP[6].Listado == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },    

                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : 
                FechasCP[7].Listado == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault() == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
                FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().Valor ? "X" : "", fontColumnValue
                )) { Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },

                new PdfPCell(new Phrase(ComentariosCP.
                Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(y => y.Nombre == "OTROS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.
                Where(z => z.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.
                Where(y => y.Nombre == "ADMINISTRACIÓN DE MICRO NUTRIENTES").FirstOrDefault().Hijos.Where(y => y.Nombre == "OTROS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue
                )) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },

                new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },
                new PdfPCell(new Phrase("Sesión de estimulación temprana", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1}, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(y => y.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(y => y.Nombre == "SESIÓN DE ESTIMULACIÓN TEMPRANA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  }, 

                new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1},
                new PdfPCell(new Phrase("Consejería Nutricional", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(y => y.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "ALIMENTACIÓN").FirstOrDefault().Hijos.Where(y => y.Nombre == "CONSEJERÍA NUTRICIONAL").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase("Administración de vacuna", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "VACUNA").FirstOrDefault().Hijos.Where(y => y.Nombre == "ADMINISTRACIÓN DE VACUNA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("7", fontColumnValue)) { Colspan = 1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_2 },
                new PdfPCell(new Phrase("Detección, Dx y Tto de:", fontColumnValue)) { Colspan = 4, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_2 },
                new PdfPCell(new Phrase("Anemia", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "ANEMIA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(y => y.Nombre == "ANEMIA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(y => y.Nombre == "ANEMIA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Parasitosis", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARASITOSIS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(y => y.Nombre == "PARASITOSIS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "DETECCIÓN, DX Y TTO").FirstOrDefault().Hijos.Where(y => y.Nombre == "PARASITOSIS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("8", fontColumnValue)) { Colspan = 1, Rowspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_3 },
                new PdfPCell(new Phrase("Salud Bucal", fontColumnValue)) { Colspan = 3, Rowspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_3 },
                new PdfPCell(new Phrase("Atención Odontológica", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "ATENCIÓN ODONTOLÓGICA").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Aplicación de barnices y/o sellantes", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "APLICACIÓN DE BARNICES Y/O SELLANTES").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("Tto. Recuperativo (obturac. y/o exodonc.)", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(y => y.Nombre == "TTO. RECUPERATIVO (OBTURAC. Y/O EXODONC.").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("9", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  },
                new PdfPCell(new Phrase("Visita Familiar Integral", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "VISITA").FirstOrDefault().Hijos.Where(x => x.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "VISITA").FirstOrDefault().Hijos.Where(y => y.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "VISITA").FirstOrDefault().Hijos.Where(y => y.Nombre == "VISITA FAMILIAR INTEGRAL").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("10", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase("Atención de patologías prevalentes", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(y => y.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "PATOLOGÍAS").FirstOrDefault().Hijos.Where(y => y.Nombre == "ATENCIÓN DE PATOLOGÍAS PREVALENTES").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  }, 

                new PdfPCell(new Phrase("11", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase("Sesiones educativas", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(y => y.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(y => y.Nombre == "SESIONES EDUCATIVAS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1  }, 

                new PdfPCell(new Phrase("12", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase("Sesiones demostrativas", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(y => y.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "SESIONES").FirstOrDefault().Hijos.Where(y => y.Nombre == "SESIONES DEMOSTRATIVAS").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                new PdfPCell(new Phrase("13", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase("Otras prestaciones", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },
                new PdfPCell(new Phrase(FechasCP.Count < 1 ? "" : FechasCP[0].Listado == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault() == null ? "" : FechasCP[0].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 2 ? "" : FechasCP[1].Listado == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault() == null ? "" : FechasCP[1].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 3 ? "" : FechasCP[2].Listado == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault() == null ? "" : FechasCP[2].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 4 ? "" : FechasCP[3].Listado == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault() == null ? "" : FechasCP[3].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 
                new PdfPCell(new Phrase(FechasCP.Count < 5 ? "" : FechasCP[4].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault() == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 6 ? "" : FechasCP[5].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault() == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(FechasCP.Count < 7 ? "" : FechasCP[6].Listado == null ? "" : FechasCP[4].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault() == null ? "" : FechasCP[6].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },               
                new PdfPCell(new Phrase(FechasCP.Count < 8 ? "" : FechasCP[7].Listado == null ? "" : FechasCP[5].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault() == null ? "" : FechasCP[7].Listado.Where(x => x.Nombre == "OTROS").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().Valor ? "X" : "", fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 },                
                new PdfPCell(new Phrase(ComentariosCP.Where(x => x.GrupoId == FechasCP[0].Listado.Where(z => z.Nombre == "OTROS").FirstOrDefault().Hijos.Where(y => y.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().GrupoId && x.ParametroId == FechasCP[0].Listado.Where(z => z.Nombre == "OTROS").FirstOrDefault().Hijos.Where(y => y.Nombre == "OTRAS_PRESTACIONES").FirstOrDefault().ParameterId).FirstOrDefault().Comentario, fontColumnValue)) {Colspan =4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_1 }, 

                };

            columnWidths = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 3f, 5f, 4f, 3f };

            columnHeight = new float[] { 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontColumnValueApendice);
            document.Add(table);

            #endregion

            #endregion

            document.NewPage();
            #region Segunda Hoja
            #region TÍTULO
            
            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            imagenMinsa.ScalePercent(10);
            imagenMinsa.SetAbsolutePosition(400, 785);
            document.Add(imagenMinsa);

            //cellsTit = new List<PdfPCell>()
            //    { 
            //        new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL DE SALUD", fontTitle1)) { BackgroundColor= BaseColor.ORANGE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},  
            //    };
            //columnWidths = new float[] { 100f };
            //table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            //document.Add(table);

            #endregion

            #region Seguimiento Hospitalario
            document.Add(new Paragraph("\n"));
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("PLAN DE ATENCIÓN INTEGRAL DE SALUD", fontTitle1)) { Colspan = 12,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor= BaseColor.ORANGE },  
                    
                    new PdfPCell(new Phrase("Establecimiento de Salud: "  + datosPac.v_OrganitationName, fontColumnValue)) { Colspan = 7,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },    
                    new PdfPCell(new Phrase("Nº de Historia Clínica", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase(datosPac.v_IdService, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("Cod. Afiliación SIS u otro seguro", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase(datosPac.v_PersonId, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                };

            columnWidths = new float[] { 10f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #region DATOS GENERALES
            string[] fechaNac2 = datosPac.d_Birthdate.ToString().Split('/',' ');
            string gInstr_M = "";
            string gInstr_P = "";
            if (datosNinio.i_GradoInstruccionMadre == 1)
                gInstr_M = "ANALFABETO";
            else if (datosNinio.i_GradoInstruccionMadre == 2)
                gInstr_M = "PRIMARIA INCOMPLETA";
            else if (datosNinio.i_GradoInstruccionMadre == 3)
                gInstr_M = "PRIMARIA COMPLETA";
            else if (datosNinio.i_GradoInstruccionMadre == 4)
                gInstr_M = "SECUNDARIA INCOMPLETA";
            else if (datosNinio.i_GradoInstruccionMadre == 5)
                gInstr_M = "SECUNDARIA COMPLETA";
            else if (datosNinio.i_GradoInstruccionMadre == 6)
                gInstr_M = "TECNICO";
            else if (datosNinio.i_GradoInstruccionMadre == 7)
                gInstr_M = "UNIVERSITARIO";
            else
                gInstr_M = "-";

            if (datosNinio.i_GradoInstruccionPadre == 1)
                gInstr_P = "ANALFABETO";
            else if (datosNinio.i_GradoInstruccionPadre == 2)
                gInstr_P = "PRIMARIA INCOMPLETA";
            else if (datosNinio.i_GradoInstruccionPadre == 3)
                gInstr_P = "PRIMARIA COMPLETA";
            else if (datosNinio.i_GradoInstruccionPadre == 4)
                gInstr_P = "SECUNDARIA INCOMPLETA";
            else if (datosNinio.i_GradoInstruccionPadre == 5)
                gInstr_P = "SECUNDARIA COMPLETA";
            else if (datosNinio.i_GradoInstruccionPadre == 6)
                gInstr_P = "TECNICO";
            else if (datosNinio.i_GradoInstruccionPadre == 7)
                gInstr_P = "UNIVERSITARIO";
            else
                gInstr_P = "-";

            string eCivil_M = "";
            string eCivil_P = "";

            if (datosNinio.i_EstadoCivilIdMadre1 == 1)
                eCivil_M = "SOLTERA";
            else if (datosNinio.i_EstadoCivilIdMadre1 == 2)
                eCivil_M = "CASADA";
            else if (datosNinio.i_EstadoCivilIdMadre1 == 3)
                eCivil_M = "VIUDA";
            else if (datosNinio.i_EstadoCivilIdMadre1 == 4)
                eCivil_M = "DIVORCIADA";
            else if (datosNinio.i_EstadoCivilIdMadre1 == 5)
                eCivil_M = "CONVIVIENTE";
            else
                eCivil_M = "-";

            if (datosNinio.i_EstadoCivilIdPadre == 1)
                eCivil_P = "SOLTERO";
            else if (datosNinio.i_EstadoCivilIdPadre == 2)
                eCivil_P = "CASADO";
            else if (datosNinio.i_EstadoCivilIdPadre == 3)
                eCivil_P = "VIUDO";
            else if (datosNinio.i_EstadoCivilIdPadre == 4)
                eCivil_P = "DIVORCIADO";
            else if (datosNinio.i_EstadoCivilIdPadre == 5)
                eCivil_P = "CONVIVIENTE";
            else
                eCivil_P = "-";

            string seguro_M1="" , seguro_M2 = "";
            string seguro_P1 ="", seguro_P2 = "";
            if (datosNinio.i_TipoAfiliacionMadre == 1)
                seguro_M1 = "X";
            else if (datosNinio.i_TipoAfiliacionMadre == 2)
                seguro_M2 = "X";

            if (datosNinio.i_TipoAfiliacionPadre == 1)
                seguro_P1 = "X";
            else if (datosNinio.i_TipoAfiliacionPadre == 2)
                seguro_P2 = "X";

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("DATOS GENEREALES", fontColumnValueBold)) { Colspan = 21, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.ORANGE},       

                new PdfPCell(new Phrase("Apellidos", fontColumnValueBold)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Nombres", fontColumnValueBold)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Sexo:", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("M", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(sexM, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("F", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(sexF, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Edad:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosPac.Edad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_FirstName, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Fecha de Nacimiento:", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(fechaNac2[0], fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(fechaNac2[1], fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(fechaNac2[2], fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Lugar de Nacimiento", fontColumnValueBold)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Domicilio / Referencia", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("CUI / DNI", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("G.S.", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Rh", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase(datosPac.v_BirthPlace, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_BloodGroupName, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosPac.v_BloodFactorName, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Grado de Instrucción", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Centro Educativo", fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Teléfono Domicilio", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                
                new PdfPCell(new Phrase(datosPac.GradoInstruccion, fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_CentroEducativo, fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_TelephoneNumber, fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Nombres y Apellidos de la Madre o Tutora", fontColumnValueBold)) { Colspan = 11, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },    
                new PdfPCell(new Phrase("Edad", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
                new PdfPCell(new Phrase("Identificación (DNI)", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Cod. Afiliación:", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("SIS", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(seguro_M1, fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Otro", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(seguro_M2, fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(datosNinio.v_NombreMadre, fontColumnValue)) { Colspan = 11, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosNinio.v_EdadMadre, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosNinio.v_DniMadre, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_CodigoAfiliacionMadre, fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Grado de Instrucción", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Ocupación", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Estado Civil", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Religión", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase(gInstr_M, fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosNinio.v_OcupacionMadre, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(eCivil_M, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_ReligionMadre, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Nombres y Apellidos del Padre o Tutor", fontColumnValueBold)) { Colspan = 11, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },    
                new PdfPCell(new Phrase("Edad", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
                new PdfPCell(new Phrase("Identificación (DNI)", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Cod. Afiliación:", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("SIS", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(seguro_P1, fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Otro", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(seguro_P2, fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(datosNinio.v_NombrePadre, fontColumnValue)) { Colspan = 11, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosNinio.v_EdadPadre, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosNinio.v_DniPadre, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_CodigoAfiliacionMadre, fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Grado de Instrucción", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Ocupación", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Estado Civil", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Religión", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase(gInstr_P, fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosNinio.v_OcupacionPadre, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(eCivil_P, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_ReligionPadre, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f,5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion
            #region ANTECEDENTES
            string tuberculosis1 = "", tuberculosis2="";
            if (datosNinio.i_QuienTuberculosis == 0) tuberculosis2 = "X";
            else if (datosNinio.i_QuienTuberculosis == 1) tuberculosis1 = "X";

            string asma1 = "", asma2 = "";
            if (datosNinio.i_QuienAsma == 0) asma2 = "X";
            else if (datosNinio.i_QuienAsma == 1) asma1 = "X";

            string vih1 = "", vih2 = "";
            if (datosNinio.i_QuienVIH == 0) vih2 = "X";
            else if (datosNinio.i_QuienVIH == 1) vih1 = "X";

            string diabetes1 = "", diabetes2 = "";
            if (datosNinio.i_QuienDiabetes == 0) diabetes2 = "X";
            else if (datosNinio.i_QuienDiabetes == 1) diabetes1 = "X";

            string epilepsia1 = "", epilepsia2 = "";
            if (datosNinio.i_QuienEpilepsia == 0) epilepsia2 = "X";
            else if (datosNinio.i_QuienEpilepsia == 1) epilepsia1 = "X";

            string alergias1 = "", alergias2 = "";
            if (datosNinio.i_QuienAlergias == 0) alergias2 = "X";
            else if (datosNinio.i_QuienAlergias == 1) alergias1 = "X";

            string violencia1 = "", violencia2 = "";
            if (datosNinio.i_QuienViolenciaFamiliar == 0) violencia2 = "X";
            else if (datosNinio.i_QuienViolenciaFamiliar == 1) violencia1 = "X";

            string alcoholismo1 = "", alcoholismo2 = "";
            if (datosNinio.i_QuienAlcoholismo == 0) alcoholismo2 = "X";
            else if (datosNinio.i_QuienAlcoholismo == 1) alcoholismo1 = "X";

            string drogadiccion1 = "", drogadiccion2 = "";
            if (datosNinio.i_QuienDrogadiccion == 0) drogadiccion2 = "X";
            else if (datosNinio.i_QuienDrogadiccion== 1) drogadiccion1 = "X";

            string hepatitis1 = "", hepatitis2 = "";
            if (datosNinio.i_QuienHeptitisB == 0) hepatitis2 = "X";
            else if (datosNinio.i_QuienHeptitisB == 1) hepatitis1 = "X";

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
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMB_NORMAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMB_NORMAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
                new PdfPCell(new Phrase("Complicado", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMB_COMPLICADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMB_COMPLICADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
                new PdfPCell(new Phrase("Edad Gest. al nacer (sem)", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_EdadGestacion, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TBC").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TBC").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TBC").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TBC").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Patología(s) durante la gestación", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Peso al nacer (gr)", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_Peso, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("SOBA / Asma", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase(datosNinio.v_PatologiasGestacion, fontColumnValue)) {Colspan =10, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = alto_Celda_2  },
                new PdfPCell(new Phrase("Talla al nacer (cm)", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase(datosNinio.v_Talla, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Epilepsia", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Perímetro cefálico", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_PerimetroCefalico, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },         
                new PdfPCell(new Phrase("Infecciones", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFECCIONES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFECCIONES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFECCIONES").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFECCIONES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Nº de embarazo", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosNinio.v_nEmbarazos, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Perímetro Torácico", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_PerimetroToracico, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Hospitalizaciones", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES ").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES ").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES ").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES ").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Atención Prenatal", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCION PRENATAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCION PRENATAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCION PRENATAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ATENCION PRENATAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Nº APN", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(datosNinio.v_nAPN, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Respiración y llanto al nacer:", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Transfusiones sang.", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES SANG.").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES SANG.").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES SANG.").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES SANG.").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase("Lugar de APN", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_LugarAPN, fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("Inmediato", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "RESP_INMEDIATA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "RESP_INMEDIATA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "RESP_INMEDIATA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "RESP_INMEDIATA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                new PdfPCell(new Phrase("Cirugia", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CIRUGIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CIRUGIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CIRUGIA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CIRUGIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("1.2 Parto", fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("APGAR", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("1 min", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APGAR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APGAR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER},                       
                new PdfPCell(new Phrase("5 min", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APGAR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APGAR").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
                new PdfPCell(new Phrase("Alergia a medicamentos", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICAMENTOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICAMENTOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICAMENTOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICAMENTOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Parto Eutócico", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARTO EUTOCICO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARTO EUTOCICO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },              
                new PdfPCell(new Phrase("Complicado", fontColumnValue)) { Colspan = 4,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARTO COMPLICADO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PARTO COMPLICADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1},   
                new PdfPCell(new Phrase("Reanimación", fontColumnValue)) { Colspan = 4,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1},
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REANIMACION").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REANIMACION").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },   
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },
                 new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REANIMACION").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REANIMACION").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },                
                new PdfPCell(new Phrase(datosNinio.v_AlergiasMedicamentos, fontColumnValue)) { Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },
                
                new PdfPCell(new Phrase("Complicaciones del parto: ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Patología Neonatal", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PATOLOGIA NEONATAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PATOLOGIA NEONATAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PATOLOGIA NEONATAL").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PATOLOGIA NEONATAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Otros antecedentes", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase(datosNinio.v_ComplicacionesParto, fontColumnValue)) { Colspan = 10,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },
                new PdfPCell(new Phrase("Especifique  :  " + datosNinio.v_EspecificacionesNac, fontColumnValue)) { Colspan = 8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },
                new PdfPCell(new Phrase("Especifique  :  " + datosNinio.v_OtrosAntecedentes, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=alto_Celda_1 },

                new PdfPCell(new Phrase("Lugar del parto", fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Hospitalización", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACION_RESP").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACION_RESP").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACION_RESP").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACION_RESP").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("II. Antecedentes Familiares", fontColumnValueBold)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},

                new PdfPCell(new Phrase("EESS", fontColumnValue)) { Colspan = 2, Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "LUGAR_EESS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "LUGAR_EESS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Domicilio", fontColumnValue)) { Colspan = 2, Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "LUGAR_DOMICILIO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "LUGAR_DOMICILIO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Consult. Partic.X", fontColumnValue)) { Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "LUGAR_CONS_PART").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "LUGAR_CONS_PART").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Tiempo de Hospitalización", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_TiempoHospitalizacion, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },              
                new PdfPCell(new Phrase("Enfermedad", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Quién", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Si", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("No", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("2. Alimentación", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienTuberculosis == "" ? "-":datosNinio.v_QuienTuberculosis, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase(datosNinio.i_QuienTuberculosis.ToString() == "1"?"X":datosNinio.i_QuienTuberculosis.ToString()=="0"?"":datosNinio.i_QuienTuberculosis.ToString(), fontColumnValue)) { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienTuberculosis.ToString() == "1"?"":datosNinio.i_QuienTuberculosis.ToString()=="0"?"X":datosNinio.i_QuienTuberculosis.ToString(), fontColumnValue)) { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Atendido por:", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                
                new PdfPCell(new Phrase("Primeros 6 meses", fontColumnValueBold)) { Colspan = 4,Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("LME", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_LME, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("ASMA", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienAsma == "" ?"-":datosNinio.v_QuienAsma, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(datosNinio.i_QuienAsma.ToString() == "1"?"X":datosNinio.i_QuienAsma.ToString()=="0"?"":datosNinio.i_QuienAsma.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienAsma.ToString() == "1"?"":datosNinio.i_QuienAsma.ToString()=="0"?"X":datosNinio.i_QuienAsma.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Profesional de Salud:", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PROFESIONAL DE SALUD").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PROFESIONAL DE SALUD").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                
                new PdfPCell(new Phrase("Técnico", fontColumnValue)) { Colspan = 4, Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TECNICO").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TECNICO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Mixta:", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_Mixta, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienVIH==""?"-":datosNinio.v_QuienVIH, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase(datosNinio.i_QuienVIH.ToString() == "1"?"X":datosNinio.i_QuienVIH.ToString()=="0"?"":datosNinio.i_QuienVIH.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienVIH.ToString() == "1"?"":datosNinio.i_QuienVIH.ToString()=="0"?"X":datosNinio.i_QuienVIH.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Artificial:", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_Artificial, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Diabetes", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienDiabetes==""?"-":datosNinio.v_QuienDiabetes, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(datosNinio.i_QuienDiabetes.ToString() == "1"?"X":datosNinio.i_QuienDiabetes.ToString()=="0"?"":datosNinio.i_QuienDiabetes.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienDiabetes.ToString() == "1"?"":datosNinio.i_QuienDiabetes.ToString()=="0"?"X":datosNinio.i_QuienDiabetes.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("ACS", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Familiar", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FAMILIAR").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PERINATALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FAMILIAR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Inicio de Alimentación complementaria", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase(datosNinio.v_InicioAlimentacionComp, fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Epilepsia", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienEpilepsia==""?"-":datosNinio.v_QuienEpilepsia, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(datosNinio.i_QuienEpilepsia.ToString() == "1"?"X":datosNinio.i_QuienEpilepsia.ToString()=="0"?"":datosNinio.i_QuienEpilepsia.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienEpilepsia.ToString() == "1"?"":datosNinio.i_QuienEpilepsia.ToString()=="0"?"X":datosNinio.i_QuienEpilepsia.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Alergia a medicinas", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienAlergias==""?"-":datosNinio.v_QuienAlergias, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(datosNinio.i_QuienAlergias.ToString() == "1"?"X":datosNinio.i_QuienAlergias.ToString()=="0"?"":datosNinio.i_QuienAlergias.ToString(), fontColumnValue)){ Colspan =1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienAlergias.ToString() == "1"?"":datosNinio.i_QuienAlergias.ToString()=="0"?"X":datosNinio.i_QuienAlergias.ToString(), fontColumnValue)){ Colspan =1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

                new PdfPCell(new Phrase("Otro (especificar)", fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(datosNinio.v_Atencion, fontColumnValueBold)) {  Colspan =6, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Suplemento de Fe <2 años", fontColumnValue)) { Colspan = 6, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES ALIMENTACION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES ALIMENTACION").FirstOrDefault().Hijos.Where(x => x.Nombre == "SUPLEMENTO FE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES ALIMENTACION").FirstOrDefault().Hijos.Where(x => x.Nombre == "SUPLEMENTO FE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Violencia familiar", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienViolenciaFamiliar == ""?"-":datosNinio.v_QuienViolenciaFamiliar, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(datosNinio.i_QuienViolenciaFamiliar.ToString() == "1"?"X":datosNinio.i_QuienViolenciaFamiliar.ToString()=="0"?"":datosNinio.i_QuienViolenciaFamiliar.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienViolenciaFamiliar.ToString() == "1"?"":datosNinio.i_QuienViolenciaFamiliar.ToString()=="0"?"X":datosNinio.i_QuienViolenciaFamiliar.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES ALIMENTACION").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES ALIMENTACION").FirstOrDefault().Hijos.Where(x => x.Nombre == "SUPLEMENTO FE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES ALIMENTACION").FirstOrDefault().Hijos.Where(x => x.Nombre == "SUPLEMENTO FE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("Alcoholismo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienAlcoholismo==""?"-":datosNinio.v_QuienAlcoholismo, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(datosNinio.i_QuienAlcoholismo.ToString() == "1"?"X":datosNinio.i_QuienAlcoholismo.ToString()=="0"?"":datosNinio.i_QuienAlcoholismo.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienAlcoholismo.ToString() == "1"?"":datosNinio.i_QuienAlcoholismo.ToString()=="0"?"X":datosNinio.i_QuienAlcoholismo.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("III. Vivienda / Saneamiento Básico", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Drogadicción", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(datosNinio.v_QuienDrogadiccion == "" ? "-":datosNinio.v_QuienDrogadiccion, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase(datosNinio.i_QuienDrogadiccion.ToString() == "1"?"X":datosNinio.i_QuienDrogadiccion.ToString()=="0"?"":datosNinio.i_QuienDrogadiccion.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
                new PdfPCell(new Phrase(datosNinio.i_QuienDrogadiccion.ToString() == "1"?"":datosNinio.i_QuienDrogadiccion.ToString()=="0"?"X":datosNinio.i_QuienDrogadiccion.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

                new PdfPCell(new Phrase("Agua Potable", fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AGUA POTABLE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AGUA POTABLE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AGUA POTABLE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AGUA POTABLE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Especificar", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase(datosNinio.v_EspecificacionesAgua, fontColumnValue)) { Colspan = 6,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Hepat. B", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(datosNinio.v_QuienHeptitisB=="" ?"-":datosNinio.v_QuienHeptitisB, fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase(datosNinio.i_QuienHeptitisB.ToString() == "1"?"X":datosNinio.i_QuienHeptitisB.ToString()=="0"?"":datosNinio.i_QuienHeptitisB.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                   
                new PdfPCell(new Phrase(datosNinio.i_QuienHeptitisB.ToString() == "1"?"":datosNinio.i_QuienHeptitisB.ToString()=="0"?"X":datosNinio.i_QuienHeptitisB.ToString(), fontColumnValue)){ Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase("Desague", fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESAGUE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESAGUE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESAGUE").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "ANTECEDENTES VIVIENDA/SANEAMIENTO").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESAGUE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
                { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("Especificar", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase(datosNinio.v_EspecificacionesDesague, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("*** Padre(P), Madre(M), Hno(H), Abuelo/a(A), Otro(O)", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region Inmunizaciones && Control de crecimiento y desarrollo

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("Inmunizaciones", fontColumnValue)){Colspan = 5, Rowspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.ORANGE },
                new PdfPCell(new Phrase("RCG", fontColumnValue)){Colspan = 2,Rowspan =2 ,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("HVB", fontColumnValue)){Colspan = 2, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,BackgroundColor = BaseColor.ORANGE},
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

                

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "RCG").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "RCG").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HBV").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HBV").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APO1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APO1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APO2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APO2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APO3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APO3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PENTAVALENTE1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PENTAVALENTE1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PENTAVALENTE2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PENTAVALENTE2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PENTAVALENTE3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PENTAVALENTE3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ROTAVIRUS1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ROTAVIRUS1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ROTAVIRUS2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ROTAVIRUS2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "NEUMOCOCO1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "NEUMOCOCO1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "NEUMOCOCO2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "NEUMOCOCO2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "NEUMOCOCO3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "NEUMOCOCO3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFLUENZA1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFLUENZA1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFLUENZA2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFLUENZA2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SPR1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SPR1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SPR2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SPR2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "AMA").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "AMA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DTP-1R").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DTP-1R").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DTP-2R").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "INMUNIZACIONES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DTP-2R").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },


                new PdfPCell(new Phrase("RN", fontColumnValue)){Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("Menor de 01 año", fontColumnValue)){Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("1 año", fontColumnValue)){Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},
                new PdfPCell(new Phrase("2 años", fontColumnValue)){Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.ORANGE},

                //

                new PdfPCell(new Phrase("Control de crecimiento y desarrollo", fontColumnValue)){Colspan = 5, Rowspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.ORANGE},
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


                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "RN-1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "RN-1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "RN-2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "RN-2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_4").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_4").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_5").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_5").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_6").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_6").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_7").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_7").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_8").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_8").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_9").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_9").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_10").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_10").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_11").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "MENOR_AÑO_11").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_4").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_4").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_5").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_5").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_6").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "AÑO_6").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "2_AÑOS_1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "2_AÑOS_1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "2_AÑOS_2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "2_AÑOS_2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

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

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "2_AÑOS_3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "2_AÑOS_3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "2_AÑOS_4").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "2_AÑOS_4").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "3_AÑOS_1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "3_AÑOS_1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "3_AÑOS_2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "3_AÑOS_2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "3_AÑOS_3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "3_AÑOS_3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "3_AÑOS_4").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "3_AÑOS_4").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "4_AÑOS_1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "4_AÑOS_1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "4_AÑOS_2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "4_AÑOS_2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "4_AÑOS_3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "4_AÑOS_3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "4_AÑOS_4").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "4_AÑOS_4").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "5_AÑOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "5_AÑOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "6_AÑOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "6_AÑOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "7_AÑOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "7_AÑOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "8_AÑOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "8_AÑOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "9_AÑOS").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "CONTROL DE CRECIMIENTO Y DESARROLLO").FirstOrDefault().Hijos.Where(x => x.Nombre == "9_AÑOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
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
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "NEONATAL_<1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "NEONATAL_<1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 18,HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                
                new PdfPCell(new Phrase("Descarte de anemia", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("Descarte de Hb o Hto", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_<1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_<1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_4").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_4").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_5").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_5").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_6").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_6").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_7").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_7").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_8").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_8").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_9").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB/HTO_9").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                

                new PdfPCell(new Phrase("Descarte de parasitosis", fontColumnValue)){Colspan = 3, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("Examen seriado", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("-", fontColumnValue)){Colspan = 2, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.BLACK},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_4").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_4").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_5").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_5").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_6").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_6").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_7").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_7").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_8").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_8").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_9").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "SERIADO_9").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                
                new PdfPCell(new Phrase("Test de Graham", fontColumnValue)){Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_1").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_2").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_3").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_4").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_4").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_5").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_5").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_6").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_6").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_7").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_7").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_8").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_8").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault() == null ? null:
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_9").FirstOrDefault() == null ? "" : 
                Antecedentes.Where(x => x.Nombre == "TAMIZAJE").FirstOrDefault().Hijos.Where(x => x.Nombre == "GRAHAM_9").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
                { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
               
                new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", fontColumnValue)){BackgroundColor=BaseColor.GRAY, Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase(datosPac.v_FirstLastName  + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName , fontColumnValue)){BackgroundColor=BaseColor.GRAY,Colspan = 10,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("Nº HCL", fontColumnValue)){BackgroundColor=BaseColor.GRAY,Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase(datosPac.v_IdService, fontColumnValue)){BackgroundColor=BaseColor.GRAY,Colspan = 7,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                

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
