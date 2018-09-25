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
   public class InformeAnsiedadZung
    {
       public static void CreateInformeAnsiedadZung(ServiceList DataService, organizationDto infoEmpresaPropietaria, string filePDF)
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
           Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
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
                    new PdfPCell(new Phrase("ESCALA DE AUTOVALORACIÓN DE LA ANSIEDAD DE ZUNG", fontTitle1)){Rowspan =2,Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},       
                  
                   };

           columnWidths = new float[] { 15f, 5f, 30f, 25f, 20f, 5f };

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
           document.Add(table);

           #endregion

           #region Datos Personales

           cells = new List<PdfPCell>()
                   {      
                    //fila 
                    new PdfPCell(new Phrase(" ", fontTitle1)){Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("Nombre:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_Pacient, fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Sexo:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_GenderName, fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Edad:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.i_Edad.ToString(), fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Estado Civil:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.EstadoCivil, fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Fecha:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.d_ServiceDate.Value.ToString("dd/MM/yyyy"), fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Historia Clínica:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_ServiceId, fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("DNI:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(DataService.v_DocNumber, fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Dirección:", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(DataService.v_AdressLocation, fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },                  
                   };

           columnWidths = new float[] { 15f, 35f, 15f, 35f, };

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
           document.Add(table);
           #endregion

           #region Cuadro
           
           cells = new List<PdfPCell>()
                   {      
                    //fila                     
                    new PdfPCell(new Phrase("", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase("", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Nunca o casi nunca", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("A veces", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Con bastante frecuencia", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase("Siempre o caso siempre", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("Puntos", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
  
                    new PdfPCell(new Phrase("1", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase("Me siento más intranquilo y nervioso que de costumbre", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
                    new PdfPCell(new Phrase(" ", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase(" ", fontTitle1)){HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                   };

           columnWidths = new float[] { 5f, 35f, 12f, 12f, 12f, 12f, 12f };

           table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
           document.Add(table);
           #endregion

           document.Close();
       }
    }
}
