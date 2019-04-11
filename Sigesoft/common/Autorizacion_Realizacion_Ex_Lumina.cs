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
    public class Autorizacion_Realizacion_Ex_Lumina
    {

        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateAutorizacion_Realizacion_Ex_Lumina(string filePDF,
            PacientList datosPac,
            organizationDto infoEmpresa, PacientList filiationData)
        {


        Document document = new Document(PageSize.A4, 40f, 40f, 40f, 50f);



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
                document.Add(new Paragraph("\r\n"));
                #endregion

                #region Fonts
                Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));


               Font fontColumnValue = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
               Font fontColumnValue1 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
               Font fontColumnValueBold = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
               Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                        
               Font fontColumnValue2 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
               Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

               #endregion

               #region TITULO

               cells = new List<PdfPCell>();

             if (infoEmpresa.b_Image != null)
             {

               iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
               imagenEmpresa.ScalePercent(25);
               imagenEmpresa.SetAbsolutePosition(40, 790);
               document.Add(imagenEmpresa);
             }




            var cellsTit = new List<PdfPCell>()

           {

             new PdfPCell(new Phrase("AUTORIZACIÓN DE REALIZACIÓN DE EXAMEN \nMÉDICO Y LIBERACIÓN DE INFORMACIÓN MÉDICO", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},


             };
            columnWidths = new float[] {100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            var tamaño_celda = 15;
            string tipodoc = "";
            if (datosPac.i_DocTypeId == 1) { tipodoc = "DNI"; }
            else if (datosPac.i_DocTypeId == 2) { tipodoc = "Pasaporte"; }
            else if (datosPac.i_DocTypeId == 3) { tipodoc = "Licencia de Conducir"; }
            else if (datosPac.i_DocTypeId == 4) { tipodoc = "Carnet de Extranjeria"; }

            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;


            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');

            #region Contenido
            cells = new List<PdfPCell>()
            {          

                new PdfPCell(new Phrase("\n \n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n \nYO, ", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n \n" + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName , fontColumnValue)){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n \n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("             (Apellidos y Nombres como aparece en su documento de identidad)", fontColumnValue2)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase(" ", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\nTrabajador / Postulante / Contratista de la Empresa", fontColumnValue)){ Colspan = 10 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\n"+ empresageneral + ", ", fontColumnValue1)){ Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\nidentificado", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("con DNI /Pasaporte /Carné de Extranjería N° " + datosPac.v_DocNumber + ", con domicilio legal en: ", fontColumnValue)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue1)){ Colspan =18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    


                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\nDeclaro que forma libre  y espontanea, autorizo a:", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    


                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase(infoEmpresa.v_Name, fontColumnValue1)){ Colspan =18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                //new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue)){ Colspan =11, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE}, 

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("      (Razon Social del Centro de Salud Ocupacional)", fontColumnValue2)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\na realizar las evaluaiones médicas ocupacionales requeridas por ley, así como a proporcionar ", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("información con respecto a mi estado de salud, resultados de exámenes auciliares y/o resultados de", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("evaluaciones médicas ocupacionales contenidas en mi examen ocupacional, al médico responsable del", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Área de Salud Ocupacional de la empresa en la cual laboro o laboraré, así como al médico ocupacional", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("de la empresa destino donde se desarrollara el trabajo. Debiendo primar en estos procesos el principio", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("de CONFIDENCIALIDAD.", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\nLa presente autorización se ampara en lo dispuesto en los Articulos 5°, segundo párrafo, Art 13°, 25°,", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("27° Y 29° tercer  párrafo de la LEY GENERAL DE SALUD  N° 26842.", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\nDe acuerdo com los dispuesto en la Ley 29733 (Ley de Protección de Datos Personales), declaro haber", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("tomado conocimiento que los exámenes médicos efectuados por el Centtro Médico y la información", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("contenida en los mismos, a fin de evaluar mi condición médica para laborar o postular a un puesto de", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("trabajo en la campamento minero es registrada por la Compañía Minera y/o la Companía Aseguradora", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("que tenga a su cargo  la Cobertura del Seguro Complementario de Trabajo  de Riesgo o la que esta", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("designe para efectos de control de dicho seguro.En ese sentido, mediante la suscripcion del,", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("presente documento, otorga consentimiento expreso e inequívoco para que la Compañía Minera efectúe", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("el tratamiento de los datos personales facilitado y los trasfiera la Compañía  Aseguradora a fin de la", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("evaluación y otorgamiento de la Cobertura del Seguro Complementario de Trabajo de Riesgo, pudiendo", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("esta última informar  a la Compañía Minera, Contratista o Corredor de Seguros de ambos el estado de", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("la cobertura del Seguro Complementario de Trabajo de Riesgos. Esta declaración autoriza la transferencia", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("de la información de la hitoria clinica y exámenes médicos confidenciles de conformidad con la ley", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("26842 (Ley General de Salud), y de la ley 29783 (Ley de Seguridad y Salud en el Trabajo y su", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Reglamento aprobado por Decreto Supremo 005-2012-TR).", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Fecha: ", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue1)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\nFirno y dejo huella mi huella digital voluntariamente en señal de conformidad.", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

            };

            columnWidths = new float [] { 5f, 4f, 5f, 5f, 5f, 4f, 5f, 4f, 5f, 5f, 4f, 5f, 5f, 4f, 5f, 5f, 5f, 5f, 10f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Firma

            PdfPCell cellFirmaTrabajador = null;

            PdfPCell cellHuellaTrabajador = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 125, 50));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 50, 70));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 100f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(cellFirmaTrabajador){HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = 100f},
                new PdfPCell(cellHuellaTrabajador){HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight = 100f},
                new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 100f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("FIRMA", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f},    
                new PdfPCell(new Phrase("HUELLA DIGITAL", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f},    
                new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

            };
            columnWidths = new float[] { 5F, 45f, 45f, 5F };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

        document.Close();
        writer.Close();
        writer.Dispose();
        }
        
    }
}






