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

    public class OrdenServicioPromocion
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();

        }
        public static void CrearOrdenServicio(Boolean MostrarPrecio, List<ServiceOrderPdf> ListaServiceOrder, OrganizationList infoEmpresaPropietaria, string EmpresaCliente, string Fecha, string Usuario, string filePDF)
        {

            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //try
            //{NO_BORDER
            // step 2: we create aPA writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
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

            Font fontTitle1 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region Logo Empresa Propietaria

            PdfPCell CellLogo = null;
            cells = new List<PdfPCell>();
            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase("sin logo ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            cells.Add(CellLogo);
            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            cells = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase(EmpresaCliente, fontSubTitleNegroNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                new PdfPCell(new Phrase(infoEmpresaPropietaria.v_Sede + ", " + Fecha, fontSubTitleNegroNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
              
            };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region Texto en duro
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)),
                new PdfPCell(new Phrase("Atte.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("Es muy grato dirigirme a usted, saludarlos y contestar sus requerimientos. Además, ofrecerle en condiciones especiales los servicios de nuestra empresa y así iniciar el programa de salud ocupacional que brindamos.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                //new PdfPCell(new Phrase("nuestra empresa y así iniciar el programa de salud ocupacional que brindamos.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("Prevenciones Ocupacionales de Salud SAC, es una empresa constituida por un staff de profesionales de salud altamente calificados, nuestra primordial misión es brindarles LA EVALUACIÓN MÉDICA OCUPACIONAL de acuerdo a ley 29783 de SEGURIDAD Y SALUD EN EL TRABAJO, dentro de la infraestructura de cada empresa a nivel nacional.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                //new PdfPCell(new Phrase("primordial misión es brindarles LA EVALUACIÓN MÉDICA OCUPACIONAL de acuerdo a ley 29783 de SEGURIDAD Y SALUD EN", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                //new PdfPCell(new Phrase("EL TRABAJO, dentro de la infraestructura de cada empresa a nivel nacional.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("Así mismo nuestras Evaluaciones Médicas Ocupacionales serán detalladas a continuación según sus referencias y a solicitud:", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase(" ", fontColumnValue)),    
                new PdfPCell(new Phrase(" ", fontColumnValue)),     
            };
            columnWidths = new float[] { 100f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Parte Dinámica
            cells = new List<PdfPCell>();

        
            foreach (var Cabecera in ListaServiceOrder)
            {

                cell = new PdfPCell(new Phrase(Cabecera.EmpresaCliente, fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                cells.Add(cell);

                var ListaFiltrada = Cabecera.DetalleServiceOrder.FindAll(p => p.v_Precio != 0.00);
                foreach (var Detalle in ListaFiltrada)
                {
                    cell = new PdfPCell(new Phrase(Detalle.Componente, fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                    cells.Add(cell);
                    string Valor = MostrarPrecio == true ? Detalle.v_Precio.ToString() == "0.01" ? "COSTO CERO" : Detalle.v_Precio.ToString() : Detalle.v_Precio.ToString() == "0.01" ? "COSTO CERO" : "";
                    if (Valor != "")
                    {
                        Valor = "S/." + Valor;
                    }

                    cell = new PdfPCell(new Phrase(Valor, fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
                    cells.Add(cell);
                }
                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase("TOTAL: S/. ", fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);


                cell = new PdfPCell(new Phrase(Cabecera.TotalProtocolo.ToString(), fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase("Los precios no incluyen I.G.V.", fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);
            }


            columnWidths = new float[] { 50f, 50f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Texto en duro

            cells = new List<PdfPCell>()
            {
            new PdfPCell(new Phrase(" ", fontColumnValue)),
            new PdfPCell(new Phrase("Además, Prevenciones Ocupacionales de Salud se compromete realizar a COSTOCERO parte delas evaluaciones del protocolo exigidas según la 'Ley de seguridad y salud en el trabajo 29783' y el Documento Técnico según 'Resolución Ministerial 312' para la totalidad de sus colaboradores, bajo la modalidad SEMIPAGADA.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            //new PdfPCell(new Phrase("según la 'Ley de seguridad y salud en el trabajo 29783' y el Documento Técnico según 'Resolución Ministerial 312' para la totalidad de", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            //new PdfPCell(new Phrase("sus colaboradores, bajo la modalidad SEMIPAGADA.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            //new PdfPCell(new Phrase(" ", fontColumnValue)),    
            new PdfPCell(new Phrase("PARA QUE TAL MODALIDAD SE HAGA EFECTIVA, queremos hacer mención que si durante las evaluaciones sus trabajadores equieran o deseen algún servicio brindado por nuestra institución en el área de Optometría y Laboratorio se les otorgará un crédito inmediato en cuotas (2 a 3 meses dependiendo del monto), al cual podrán acceder en forma voluntaria, bajo la modalidad: Descuento por planilla.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            //new PdfPCell(new Phrase("requieran o deseen algún servicio brindado por nuestra institución en el área de Optometría y Laboratorio se les otorgará un crédito", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            //new PdfPCell(new Phrase("inmediato en cuotas (2 a 3 meses dependiendo del monto), al cual podrán acceder en forma voluntaria, bajo la modalidad: Descuento por", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            //new PdfPCell(new Phrase("planilla.", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            new PdfPCell(new Phrase("Esperamos poder atender a su empresa recordándole que esta opción de brindar un servicio de salud de alta calidad está disponible vuestra empresa; nos despedimos quedando a sus gratas órdenes.", fontColumnValue)),    
            //new PdfPCell(new Phrase("vuestra empresa; nos despedimos quedando a sus gratas órdenes.", fontColumnValue)),     
            //new PdfPCell(new Phrase(" ", fontColumnValue)),    
            //new PdfPCell(new Phrase("Esperamos poder atender a su empresa recordándole que esta opción de brindar un servicio de salud de alta calidad está disponible para", fontColumnValue)),   
            //new PdfPCell(new Phrase("vuestra empresa; nos despedimos quedando a sus gratas órdenes.", fontColumnValue)),  
              new PdfPCell(new Phrase(" ", fontColumnValue)), 
            new PdfPCell(new Phrase("Atentamente. ", fontColumnValue)),  
 
             new PdfPCell(new Phrase(" ", fontColumnValue)), 
             new PdfPCell(new Phrase(Usuario, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },
             new PdfPCell(new Phrase("Coordinadora Médico Ocupacional", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },
             new PdfPCell(new Phrase(infoEmpresaPropietaria.v_Name, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },
          
            };
            columnWidths = new float[] { 100f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
            //RunFile(filePDF);
        }

    }
}
