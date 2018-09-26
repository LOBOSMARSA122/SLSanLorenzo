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
    public class InformeMaslach
    {
        public static void CreateInformeMaslach(ServiceList DataService, organizationDto infoEmpresaPropietaria, string filePDF)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            //holaaaaaaa
            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
            page.FirmaTrabajador = DataService.FirmaTrabajador;
            page.HuellaTrabajador = DataService.HuellaTrabajador;
            page.Dni = DataService.v_DocNumber;
            page.EmpresaId = DataService.EmpresaClienteId;
            //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
            writer.PageEvent = page;

            // step 3: we open the document
            document.Open();
            // step 4: we Add content to the document
            // we define some fonts

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
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
            //string[] columnValues = null;
            string[] columnHeaders = null;


            PdfPTable filiationWorker = new PdfPTable(8);

            PdfPTable table = null;

            PdfPCell cell = null;

            #endregion

            #region Title

            PdfPCell CellLogo = null;

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 20F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }


            cells = new List<PdfPCell>()
                   {      
                    //fila 
                    new PdfPCell(CellLogo){Rowspan =2, Colspan = 2, Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("MBI (INVENTARIO DE BURNOUT DE MASLACH) ", fontTitle1)){Rowspan =2,Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},       
                  
                   };

            columnWidths = new float[] { 15f, 5f, 30f, 25f, 20f, 5f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion
            var tamaño_celda = 24f;            

            #region Cabecera
            cells = new List<PdfPCell>()
                   {      
                    //fila                      
                    new PdfPCell(new Phrase("A continuación encontrará una serie de enunciados acerca de su trabajo y de sus sentimientos en él. Le pedimos su colaboración respondiendo a ellos como lo siente. No existen respuestas mejores o peores, la respuesta correcta es aquella que expresa verídicamente su propia existencia. Los resultados de este cuestionario son estrictamente confidenciales y en ningún caso accesibles a otras personas. Su objeto es contribuir al conocimiento de las condiciones de su trabajo y mejorar su nivel de satisfacción.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase("A cada una de las frases debe responder expresando la frecuencia con que tiene ese sentimiento de la siguiente forma: ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase("(1)  Nunca - (2) Algunas veces al año - (3)  Algunas veces al mes - (4)  Algunas veces a la semana - (5) Diariamente", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },                    
                   };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Datos Personales

            cells = new List<PdfPCell>()
                   {      
                    //fila                     
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("Nombre:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_Pacient, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("DNI:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_DocNumber, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                     new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                   };

            columnWidths = new float[] { 15f, 35f, 15f, 35f, };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Cuadro

            cells = new List<PdfPCell>()
                   {      
                    //fila                     
                    new PdfPCell(new Phrase("", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = tamaño_celda },   
                    new PdfPCell(new Phrase("ITEMS", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda },
                    new PdfPCell(new Phrase("1", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda },
                    new PdfPCell(new Phrase("2", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda },
                    new PdfPCell(new Phrase("3", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda },   
                    new PdfPCell(new Phrase("4", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                    new PdfPCell(new Phrase("5", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                      
                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("1. Me siento emocionalmente defraudado en mi trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                   
                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("2. Cuando termino mi jornada de trabajo me siento agotado ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("3. Cuando me levanto por la mañana y me enfrento a otra jornada de trabajo me siento agotado ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("4. Siento que puedo entender fácilmente a las personas que tengo que atender", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("5. Siento que estoy tratando a algunos beneficiados de mí, como si fuesen objetos impersonales ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("6. Siento que trabajar todo el día con la gente me cansa ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("7. Siento que trato con mucha efectividad los problemas de las personas a las que tengo que atender  ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("8. Siento que mi trabajo me está desgastando ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("9. Siento que estoy influyendo positivamente en las vidas de otras personas a través de mi trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("10. Siento que me he hecho más duro con la gente ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    
                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("11. Me preocupa que este trabajo me está endureciendo emocionalmente ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("12. Me siento muy enérgico en mi trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("13. Me siento frustrado por el trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("14. Siento que estoy demasiado tiempo en mi trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("15. Siento que realmente no me importa lo que les ocurra a las personas a las que tengo que atender profesionalmente ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("16. Siento que trabajar en contacto directo con la gente me cansa  ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("17. Siento que puedo crear con facilidad un clima agradable en mi trabajo  ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("18.  Me siento estimulado después de haber trabajado íntimamente con quienes tengo que atender ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("19. Creo que consigo muchas cosas valiosas en este trabajo ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("A.E", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("20. Me siento como si estuviera al límite de mis posibilidades ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("R.P.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("21. Siento que en mi trabajo los problemas emocionales son tratados de forma adecuada ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("D.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("22. Me parece que los beneficiarios de mi trabajo me culpan de algunos problemas ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                   };

            columnWidths = new float[] { 5f, 55f, 8f, 8f, 8f, 8f, 8f};

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Texto02
            cells = new List<PdfPCell>()
                   {      
                    //fila    
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("A. E.  AGOTAMIENTO EMOCIONAL", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase("D.      DESPERSONALIZAICÓN", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase("R. P.  REALIZACIÓN PERSONAL", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    
                    
                   };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            document.Close();
        }
    }
}
