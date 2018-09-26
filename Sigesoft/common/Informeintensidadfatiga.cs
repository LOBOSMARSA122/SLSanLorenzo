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
    public class Informeintensidadfatiga
    {
        public static void CreateInformeintensidadfatiga(ServiceList DataService, organizationDto infoEmpresaPropietaria, string filePDF)
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

            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
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
                    new PdfPCell(new Phrase("ESCALA DE INTENSIDAD DE FATIGA", fontTitle1)){Rowspan =2,Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},       
                  
                   };

            columnWidths = new float[] { 15f, 5f, 30f, 25f, 20f, 5f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion
            var tamaño_celda = 27f;
            var tamaño_celda1 = 43f;
            #region Datos Personales

            cells = new List<PdfPCell>()
                   {      
                    //fila 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("Nombre:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_Pacient, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Sexo:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_GenderName, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Edad:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.i_Edad.ToString(), fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Estado Civil:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.EstadoCivil, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Fecha:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.d_ServiceDate.Value.ToString("dd/MM/yyyy"), fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Historia Clínica:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_ServiceId, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE,},
                    new PdfPCell(new Phrase("DNI:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_DocNumber, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Dirección:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_AdressLocation, fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                   };

            columnWidths = new float[] { 15f, 35f, 15f, 35f, };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Texto01
            
            cells = new List<PdfPCell>()
                   {      
                    //fila                       
                    new PdfPCell(new Phrase("Conteste a las siguientes afirmaciones, segùn los criterios de la siguiente escala", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
                    new PdfPCell(new Phrase("TOTAL DESACUERDO 1 2 3 4 5 6 7 TOTALMENTE DE ACUERDO", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                   };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Cuadro

            cells = new List<PdfPCell>()
                   {      
                    //fila                     
                    new PdfPCell(new Phrase("", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = tamaño_celda1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, BorderColorRight=BaseColor.WHITE },   
                    new PdfPCell(new Phrase("", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, FixedHeight = tamaño_celda1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("1", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },
                    new PdfPCell(new Phrase("2", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },
                    new PdfPCell(new Phrase("3", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },   
                    new PdfPCell(new Phrase("4", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1},
                    new PdfPCell(new Phrase("5", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },    
                    new PdfPCell(new Phrase("6", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1},
                    new PdfPCell(new Phrase("7", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE, FixedHeight = tamaño_celda1 },    
  
                    new PdfPCell(new Phrase("1", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Mi motivaciòn se reduce cuando estoy fatigado.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("2", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("El ejercicion me produce fatiga.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("3", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("Me fatigo fácilmente.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("4", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga me produce con fecuencia problemas.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("5", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga me impide hacer ejercicio físico continuado.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("6", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga me impide hacer ejercicio físico continuado.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("7", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga interfiere en el desempeño de algunas obligaciones y responsabilidades.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("8", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga es uno de mis tres síntomas que más me incapacitan.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                    new PdfPCell(new Phrase("9", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, FixedHeight = tamaño_celda, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },   
                    new PdfPCell(new Phrase("La fatiga interfiere en mi trabajo, familia o vida social.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                   };

            columnWidths = new float[] { 5f, 39f, 8f, 8f, 8f, 8f, 8f, 8f, 8f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Texto02
            cells = new List<PdfPCell>()
                   {      
                    //fila    
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase("La escala de severidad Fatiga (FSS), es un método para evaluar el impacto de la fatiga en ti.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase("Lea cada declaración y marque un número del 1 al 7, basado en la precisión con la que refleja su condición durante la última semana y hasta qué punto está de acuerdo o en desacuerdo en que la declaración se aplica a usted.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase("Un valor bajo (por ejemplo, 1); indica un fuerte desacuerdo con la afirmación, mientras que un valor alto (por ejemplo, 7); indica un acuerdo fuerte.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase("Anotación los resultados", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Ahora que ha completado el cuestionario, es el momento para anotar sus resultados y evaluar su nivel de fatiga. Es muy sencillo: Sume todos los números que marcó para obtener su puntuación total.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    new PdfPCell(new Phrase(" ", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                    new PdfPCell(new Phrase("La gravedad de la fatiga clave escala", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Una puntuación total inferior a 36 sugiere que usted no puede estar sufriendo de fatiga.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Una puntuación total de 36 o más indica que es posible que necesite una evaluación más exhaustiva por un médico.", fontTitle2)){HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED },
                    
                    
                   };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion
           
            document.Close();
        }
    }
}
