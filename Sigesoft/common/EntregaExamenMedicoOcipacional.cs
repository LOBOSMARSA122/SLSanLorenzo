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
    public class EntregaExamenMedicoOcipacional
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateEntregaExamenMedicoOcipacional(PacientList filiationData, ServiceList DataService,
           List<ServiceComponentList> serviceComponent,
           organizationDto infoEmpresa,
           PacientList datosPac,
           string filePDF)
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 45f, 38f);

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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresa.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(45, 718);
                document.Add(imagenEmpresa);
            }

            //if (filiationData.logoCliente != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(filiationData.logoCliente));
            //    imagenEmpresa.ScalePercent(20);
            //    imagenEmpresa.SetAbsolutePosition(40, 795);
            //    document.Add(imagenEmpresa);
            //}
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 60f},
                    new PdfPCell(new Phrase("ENTREGA DE EXAMEN MEDICO OCUPACIONAL", fontTitle2)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 60f},
                    new PdfPCell(new Phrase("", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 60f},
                    
                    new PdfPCell(new Phrase("", fontTitle1)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f},
                    new PdfPCell(new Phrase("", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f},
                    new PdfPCell(new Phrase("", fontTitle2)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f},
                    new PdfPCell(new Phrase("Página 1 de 1", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f},

                };
            columnWidths = new float[] { 25f, 15f, 30f, 15f, 25f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            var tamaño_cuerpo = 25f;

            #region Contenido
            string preOcp = "", Ocup = "", postOcup = "";
            if (filiationData.i_EsoTypeId == 1)
            {
                preOcp = "X";
            }
            else if (filiationData.i_EsoTypeId == 2)
            {
                Ocup = "X";
            }
            else if (filiationData.i_EsoTypeId == 3)
            {
                postOcup = "X";
            }

            cells = new List<PdfPCell>()
            {         

                new PdfPCell(new Phrase("", fontTitle1)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor=BaseColor.WHITE, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK},


                new PdfPCell(new Phrase("ENTREGA E INFORME DE EXAMEN MEDICO OCUPACIONAL", fontTitle1)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor=BaseColor.WHITE},

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\nLA EMPRESA," , fontColumnValueBold)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n" + filiationData.v_FullWorkingOrganizationName, fontColumnValue)){ Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n; por medio del presente documento,", fontColumnValue)){ Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("deja constancia que el área  de Salud Ocupacional, ha hecho entrega del informe del Examen Médico" , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Ocupacional en Físico, Pre ocupacional ( "+preOcp+" ), Periódico ( "+Ocup+" ) Retiro (" + postOcup +" ) " , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
 
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Al Señor (a):   " , fontColumnValue)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase( datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName ,fontColumnValue)) { Colspan =15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Identificado con DNI N°    " , fontColumnValue)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase( datosPac.v_DocNumber,fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase(", Puesto de Trabajo  " , fontColumnValue)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase( datosPac.v_CurrentOccupation,fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("destinado al Proyecto: " , fontColumnValue)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase( filiationData.v_NombreProtocolo,fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("  CR:" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("",fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("; así mismo se ha informado detalladamente los resultados obtenidos en el examen médico ocupacional" , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("realizado el día: " , fontColumnValue)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase( datosPac.FechaServicio.ToString().Split(' ')[0],fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo,UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE},    
                new PdfPCell(new Phrase("; indicando al trabajador que cumpla con las recomendaciones" , fontColumnValue)){ Colspan = 11, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("médicas respectivas y se compromete a cumplirlas de acuerdo a las indicaciones respectivas." , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    

                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("El trabajador firma en señal de conformidad de todo lo escrito previamente." , fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE},    

            };

            columnWidths = new float[] { 2f, 6f, 5f, 5f, 6f, 5f, 6f, 5f, 5f, 5f, 5f, 5f, 6f, 5f, 6f, 5f, 6f, 5f, 5f, 2f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            string[] fechaImpresion = DateTime.Now.ToString().Split('/', ' ');
            string mes = "";
            if (fechaImpresion[1] == "01") mes = "Enero";
            else if (fechaImpresion[1] == "02") mes = "Febrero";
            else if (fechaImpresion[1] == "03") mes = "Marzo";
            else if (fechaImpresion[1] == "04") mes = "Abril";
            else if (fechaImpresion[1] == "05") mes = "Mayo";
            else if (fechaImpresion[1] == "06") mes = "Junio";
            else if (fechaImpresion[1] == "07") mes = "Julio";
            else if (fechaImpresion[1] == "08") mes = "Agosto";
            else if (fechaImpresion[1] == "09") mes = "Setiembre";
            else if (fechaImpresion[1] == "10") mes = "Octubre";
            else if (fechaImpresion[1] == "11") mes = "Noviembre";
            else if (fechaImpresion[1] == "12") mes = "Diciembre";

            PdfPCell cellFirmaTrabajador = null;

            PdfPCell cellHuellaTrabajador = null;
            PdfPCell cellFirmaMedico = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 100, 40));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 40, 60));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (DataService.FirmaMedicoMedicina != null)
                cellFirmaMedico = new PdfPCell(HandlingItextSharp.GetImage(DataService.FirmaMedicoMedicina, null, null, 100, 40));
            else
                cellFirmaMedico = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaMedico.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaMedico.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            #region Fecha / Firmmal
            cells = new List<PdfPCell>()
            {          
        
                new PdfPCell(new Phrase("\n \n \n \n  "+infoEmpresa.v_SectorName+", "+ fechaImpresion[0] + " de " + mes + " del " + fechaImpresion[2], fontColumnValue2))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cuerpo, BorderColor=BaseColor.WHITE },    
                
                new PdfPCell(new Phrase("", fontColumnValue2)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 50f , BorderColor=BaseColor.WHITE},

                new PdfPCell(cellFirmaMedico ) {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=80, BorderColor=BaseColor.WHITE},
                new PdfPCell(cellFirmaTrabajador ) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=80, BorderColor=BaseColor.WHITE},
                new PdfPCell(cellHuellaTrabajador ) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=80, BorderColor=BaseColor.WHITE},
                
                new PdfPCell(new Phrase("___________________________________", fontColumnValue2)){ Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_cuerpo , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("___________________________________", fontColumnValue2)){ Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_cuerpo , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("PERSONAL MEDICO RESPONSABLE ", fontColumnValue2)){ Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_cuerpo , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("FIRMA DEL TRABAJADOR", fontColumnValue2)){ Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_cuerpo , BorderColor=BaseColor.WHITE},

              };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
