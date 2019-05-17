using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;


namespace NetPdf
{
    public class pdfPage : PdfPageEventHelper
    {
        public string Dato { get; set; }
        public byte[] FirmaTrabajador{ get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public string Dni { get; set; }
        public string EmpresaId { get; set; }
        /** The template with the total number of pages. */
        PdfTemplate templateNumPage;

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        //I create a font object to use within my footer
        protected Font footer
        {
            get
            {
                int color1 = int.Parse(Sigesoft.Common.Utils.GetApplicationConfigValue("color1").ToString());
                int color2 = int.Parse(Sigesoft.Common.Utils.GetApplicationConfigValue("color2").ToString());
                int color3 = int.Parse(Sigesoft.Common.Utils.GetApplicationConfigValue("color3").ToString());
                // create a basecolor to use for the footer font, if needed.
                BaseColor grey = new BaseColor(color1, color2, color3);
                //HOLO 0, 98, 145
                //SAN MARTIN 98, 24, 99
                //PREVIMEDIC 28, 170, 192
                //RVMEDIC 16, 64, 40
                Font font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, grey);
                return font;
            }
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            templateNumPage = writer.DirectContent.CreateTemplate(50, 50);
            cb = writer.DirectContent;
        }

        //override the OnStartPage event handler to add our header
        public override void OnStartPage(PdfWriter writer, Document doc)
        {
            ////I use a PdfPtable with 1 column to position my header where I want it
            //PdfPTable headerTbl = new PdfPTable(1);

            ////set the width of the table to be the same as the document
            //headerTbl.TotalWidth = doc.PageSize.Width;

         

          
            ////I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
            //Image logo = Image.GetInstance(@"Resources\Logo-Laboral-Medical1.jpg");
            ////Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));

            ////I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
            //logo.ScalePercent(5f);

            ////create instance of a table cell to contain the logo
            //PdfPCell cell = new PdfPCell(logo);

            //cell.VerticalAlignment = Element.ALIGN_TOP;

            ////align the logo to the right of the cell
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;

            ////add a bit of padding to bring it away from the right edge
            //cell.PaddingLeft = 20;
            //cell.PaddingTop = -10;
           
            ////remove the border
            //cell.Border = PdfPCell.NO_BORDER;

            ////Add the cell to the table
            //headerTbl.AddCell(cell);

            ////write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
            ////headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 10), writer.DirectContent);
            //headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 0), writer.DirectContent);
        }

        //override the OnPageEnd event handler to add our footer

        #region antiguo Pie Pagina...

        //public override void OnEndPage(PdfWriter writer, Document doc)
        //{

        //    if (EmpresaId == "N009-OO000000582")
        //    {
        //        var fontColour = new BaseColor(35, 31, 32);
        //        var calibri6 = FontFactory.GetFont("Calibri", 6, fontColour);
        //        #region Firma Trabajador
        //        var cellFirmaTrabajador = FirmaTrabajador != null ? new PdfPCell(HandlingItextSharp.GetImage(FirmaTrabajador, null, null, 120, 45)) : new PdfPCell(new Phrase(" ", calibri6));
        //        //cellFirmaTrabajador.Colspan = 2;
        //        //cellFirmaTrabajador.Rowspan = 8;
        //        cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //        cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //        #endregion

        //        #region  Huella Trabajador
        //        var cellHuellaTrabajador = HuellaTrabajador != null ? new PdfPCell(HandlingItextSharp.GetImage(HuellaTrabajador, null, null, 20, 40)) : new PdfPCell(new Phrase(" ", calibri6));
        //        //cellHuellaTrabajador.Colspan = 2;
        //        //cellHuellaTrabajador.Rowspan = 4;
        //        cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //        cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

        //        #endregion
        //        var cells = new List<PdfPCell>()
        //        {
                   
        //            //Linea
        //            new PdfPCell(new Phrase("Declaro que las respuestas son ciertas según mi leal saber y entender. En caso de ser requeridos, los resultados del examen médico ocupacional podrán ser revelados conforme al an¿rtículo 25 de la ley Genral de Salud  N°26842. ", calibri6)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
        //            new PdfPCell(cellFirmaTrabajador), 
        //            new PdfPCell(cellHuellaTrabajador){ FixedHeight = 55F}, 

        //            new PdfPCell(new Phrase("HUELLA DIGITAL ÍNDICE DERECHO", calibri6)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                   

        //        };
        //        var columnWidths = new float[] { 50f, 30f, 20f };

        //        var filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", calibri6);
        //        //filiationWorker.SetTotalWidth(columnWidths);
        //        filiationWorker.TotalWidth = doc.PageSize.Width;
        //        filiationWorker.WriteSelectedRows(0, -1, 0, (doc.BottomMargin + 10), writer.DirectContent);
        //    }
        //    else
        //    {
        //        //I use a PdfPtable with 2 columns to position my footer where I want it
        //        PdfPTable footerTbl = new PdfPTable(2);

        //        //set the width of the table to be the same as the document
        //        footerTbl.TotalWidth = doc.PageSize.Width;

        //        float[] widths = new float[] { 30f, 70f };
        //        footerTbl.SetWidths(widths);

        //        //Center the table on the page
        //        footerTbl.HorizontalAlignment = Element.ALIGN_LEFT;

        //        var FontColour = new BaseColor(35, 31, 32);
        //        var Calibri6 = FontFactory.GetFont("Calibri", 9, FontColour);

        //        //create new instance of Paragraph for 2nd cell text
        //        Paragraph para = new Paragraph("", Calibri6);

        //        //create new instance of cell to hold the text
        //        PdfPCell cell = new PdfPCell(para);

        //        //align the text to the right of the cell
        //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        cell.PaddingLeft = 0;
        //        //set border to 0
        //        cell.Border = PdfPCell.NO_BORDER;

        //        // add some padding to take away from the edge of the page


        //        //add the cell to the table
        //        footerTbl.AddCell(cell);

        //        // Page Datos Empresa         
        //        string linea1 = Sigesoft.Common.Utils.GetApplicationConfigValue("linea1").ToString();
        //        String Direccion = linea1;

        //        //Create a paragraph that contains the footer text
        //        para = new Paragraph(Direccion, footer);

        //        //add a carriage return
        //        para.Add(Environment.NewLine);
        //        string WebSite = Sigesoft.Common.Utils.GetApplicationConfigValue("linea2").ToString();
        //        para.Add(WebSite);

        //        //create a cell instance to hold the text
        //        cell = new PdfPCell(para);
        //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        cell.PaddingRight = 0;
        //        //set cell border to 0           
        //        cell.BorderWidth = 0;
        //        //add some padding to bring away from the edge           

        //        //add cell to table
        //        footerTbl.AddCell(cell);

        //        //write the rows out to the PDF output stream.
        //        footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin + 10), writer.DirectContent);

        //    }


        //}

        #endregion

 
        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            var rutaImg = Sigesoft.Common.Utils.GetApplicationConfigValue("imgFooter2");
            var footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = doc.PageSize.Width;

            var type = doc.PageSize.GetType();
            if (doc.PageSize.Width == PageSize.A5.Width)
            {
                var imageCell = new PdfPCell(HandlingItextSharp.GetImage(rutaImg, null, null, 370, 20)) { Border = PdfPCell.NO_BORDER };
                footerTbl.AddCell(imageCell);
                footerTbl.WriteSelectedRows(0, -1, doc.LeftMargin, (doc.BottomMargin + 0), writer.DirectContent);
            }
            else
            {
                var imageCell = new PdfPCell(HandlingItextSharp.GetImage(rutaImg, null, null, 520, 41)) { Border = PdfPCell.NO_BORDER };
                footerTbl.AddCell(imageCell);
                footerTbl.WriteSelectedRows(0, -1, doc.LeftMargin, (doc.BottomMargin + 0), writer.DirectContent);
            }
   
        }
       
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {

            //ColumnText.ShowTextAligned(templateNumPage, Element.ALIGN_LEFT, new Phrase((writer.PageNumber - 1).ToString()), 0, (document.BottomMargin + 10), 0);
            templateNumPage.BeginText();
            templateNumPage.SetFontAndSize(bf, 8);
            templateNumPage.SetTextMatrix(0, 0);
            templateNumPage.ShowText("" + (writer.PageNumber - 1));
            templateNumPage.EndText();
        } 

    }
}
