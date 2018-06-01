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

        public static void CreateAtencionIntegralAdulto(PacientList filiationData, ServiceList DataService, string filePDF, byte[] CuadroVacio, byte[] CuadroCheck)
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
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region Title
            cells = new List<PdfPCell>();

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("FORMATO DE ATENCIÓN INTEGRAL DEL ADULTO ", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },           
                };
            
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            document.Add(new Paragraph("\r\n"));
            #endregion

            #region datos

            PdfPCell cellConCheck = null;
            cellConCheck = new PdfPCell(HandlingItextSharp.GetImage(CuadroCheck));

            PdfPCell cellSinCheck = null;
            cellSinCheck = new PdfPCell(HandlingItextSharp.GetImage(CuadroVacio));


            PdfPCell Masculino = cellSinCheck, Femenino = cellSinCheck;

            //Genero
            if (DataService.i_SexTypeId == (int)Sigesoft.Common.Gender.MASCULINO)
            {
                Masculino = cellConCheck;
            }
            else if (DataService.i_SexTypeId == (int)Sigesoft.Common.Gender.FEMENINO)
            {
                Femenino = cellConCheck;
            }

            cells = new List<PdfPCell>()
                  {
                    new PdfPCell(new Phrase("FECHA", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},              
                    new PdfPCell(new Phrase(DataService==null ? "" :DataService.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("N°", fontColumnValue)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                    new PdfPCell(new Phrase("DATOS GENERALES", fontColumnValue)){ Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_CENTER},   


                    new PdfPCell(new Phrase("NOMBRES: ", fontColumnValue)),
                    new PdfPCell(new Phrase(filiationData.v_FirstName, fontColumnValue)), 
                    new PdfPCell(new Phrase("GÉNERO", fontColumnValue)){ Border = PdfPCell.LEFT_BORDER, Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
                    new PdfPCell(new Phrase("M", fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(Masculino){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("F", fontColumnValue)){Rowspan=2, Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
                    new PdfPCell(Femenino){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    

                    new PdfPCell(new Phrase("APELLIDOS: ", fontColumnValue)), 
                    new PdfPCell(new Phrase(filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName , fontColumnValue)),
                    new PdfPCell(new Phrase("FECHA NACIMIENTO", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase( DataService.d_BirthDate.Value.ToShortDateString(), fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("EDAD", fontColumnValue)){ Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(new Phrase(DataService.i_Edad.ToString() + " Años", fontColumnValue)){Colspan=2, Border = PdfPCell.LEFT_BORDER,  HorizontalAlignment = PdfPCell.ALIGN_LEFT},    

                    new PdfPCell(new Phrase("LUGAR DE LABOR", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},            
                    
                    new PdfPCell(new Phrase("DNI", fontColumnValue)){ Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},                                            
                    new PdfPCell(new Phrase("ESTADO CIVIL", fontColumnValue)){  Border = PdfPCell.LEFT_BORDER,Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("GRADO DE INSTRUCCIÓN", fontColumnValue)){Border = PdfPCell.LEFT_BORDER, Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("", fontColumnValue)){ Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},              
                     new PdfPCell(new Phrase(DataService.v_DocNumber, fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},                                                                     
                    
                  };

            columnWidths = new float[] { 20f, 30f, 10f, 10f, 10f, 10f, 10f }; 

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            document.NewPage();
            document.Close();
            writer.Close();
            writer.Dispose();
        }

    }
}
