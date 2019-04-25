
using iTextSharp.text;
using iTextSharp.text.pdf;
using NetPdf;
using Sigesoft.Node.WinClient.BE;
using System.Collections.Generic;
using System.IO;
namespace Sigesoft
{
    public class Certificado_AptitudMedicoOcupacional
    {

        public static void Create_Certificado_AptitudMedicoOcupacional(organizationDto infoEmpresa, string filePDF)
        {
            Document document = new Document(PageSize.A4, 50f, 30f, 45f, 41f);
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

            #region Cabecera

            if (infoEmpresa != null )
            {
                if (infoEmpresa.b_Image != null)
                {
                    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                    imagenEmpresa.ScalePercent(25);
                    imagenEmpresa.SetAbsolutePosition(40, 790);
                    document.Add(imagenEmpresa);
                }
                
            }
            
         
            var cellsTit = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("CERTIFICADO DE APTITUD DE EVALUACIÓN PSICOSENSOMETRICO", fontTitle1)) {BorderColor = BaseColor.BLACK, BackgroundColor= BaseColor.WHITE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f},

                new PdfPCell(new Phrase("AQUÍ LA RAZÓN SOCIAL", fontTitle1)) {BorderColor = BaseColor.BLACK, BackgroundColor= BaseColor.WHITE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f},
            };


            columnWidths = new float[] { 50f,50f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            var tamaño_celda = 20f;

            //organizationDto infoEmpresa, PacientList datosPac, string filePDF
            //datosPac.v_FirstLastName + datosPac.v_SecondLastName + ", " + datosPac.v_FirstName
            //datosPac.v_DocNumber
            //datosPac.Edad.ToString()
            //datosPac.Genero
            //datosPac.Empresa

            cells = new List<PdfPCell>
            {
                new PdfPCell(new Phrase()){BorderColor = BaseColor.WHITE, Colspan = 1, Rowspan = 12},

                new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("JASON JOSEPH GUTIERREZ CUADROS", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},

                
                new PdfPCell(new Phrase("DOC. DE IDENTIDAD", fontColumnValue)){ BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("74390363",fontColumnValue )){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("EDAD", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("23",fontColumnValue )){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("GÉNERO", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("MASCULINO",fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},

                new PdfPCell(new Phrase("PUESTO AL QUE POSTULA (Solo pre-ocupacional)", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("PUESTO DE PRUEBA", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("EMPRESA", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("EMPRESA DE PRUEBA", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                
                new PdfPCell(new Phrase("OCUPACIÓN ACTUAL O ÚLTIMA OCUPACIÓN", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("DESARROLLADOR", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("TIPO DE SERVICIO", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("MEDICO OCUPACIONAL", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},

                new PdfPCell(new Phrase("CONCLUSIÓN DE LA APTITUD MÉDICA:", fontTitle1)){BorderColor = BaseColor.BLACK, Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30f},

                new PdfPCell(new Phrase("APTO  (Para el puesto en que trabaja o postula)", fontTitle1)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30},
                new PdfPCell(new Phrase("X", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30},
                new PdfPCell(new Phrase("RESTRICCIONES", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30},
                
                new PdfPCell(new Phrase("APTO CON RESTRICCIÓN  (Para el puesto en que trabaja o postula)", fontTitle1)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30},
                new PdfPCell(new Phrase("", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30},
                new PdfPCell(new Phrase("por verificar", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

                new PdfPCell(new Phrase("NO APTO  (Para el puesto en que trabaja o postula)", fontTitle1)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30},
                new PdfPCell(new Phrase("", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 30},

                new PdfPCell(new Phrase("", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 60},
                                    
                new PdfPCell(new Phrase("RECOMENDACIOENS", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                new PdfPCell(new Phrase("SELLO Y FIRMA DEL MÉDICO QUE CERTIFICA", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},
                     
                new PdfPCell(new Phrase("1.- ESTA ES UNA RECOMENDACIÓN DE PRUEBA", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 100},
                new PdfPCell(new Phrase("ESTA ES UNA FIRMA DE PRUEBA", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 100},
                
                new PdfPCell(new Phrase("FECHA DE E.M.O", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 100},
                new PdfPCell(new Phrase("15/04/2019", fontColumnValue)){BorderColor = BaseColor.BLACK, Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 100},

                new PdfPCell(new Phrase()){BorderColor = BaseColor.WHITE, Colspan = 1, Rowspan = 12},
            };
            columnWidths = new float[] { 5f, 20f,  15f, 10f, 15f, 10f, 20f, 5f};
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
