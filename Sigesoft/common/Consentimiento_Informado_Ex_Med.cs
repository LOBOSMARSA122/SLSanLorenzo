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
    public class Consentimiento_Informado_Ex_Med
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateInformeResultadosAutorizacionCoimolache(PacientList filiationData, string filePDF,
            PacientList datosPac,
            organizationDto infoEmpresaPropietaria)
        {
            Document document = new Document(PageSize.A4, 40f, 40f, 80f, 50f);


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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

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
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split('/', ' ');
            string mes = "";
            var tamaño_celda = 18f;
            if (fechaServicio[1] == "01") mes = "Enero";
            else if (fechaServicio[1] == "02") mes = "Febrero";
            else if (fechaServicio[1] == "03") mes = "Marzo";
            else if (fechaServicio[1] == "04") mes = "Abril";
            else if (fechaServicio[1] == "05") mes = "Mayo";
            else if (fechaServicio[1] == "06") mes = "Junio";
            else if (fechaServicio[1] == "07") mes = "Julio";
            else if (fechaServicio[1] == "08") mes = "Agosto";
            else if (fechaServicio[1] == "09") mes = "Setiembre";
            else if (fechaServicio[1] == "10") mes = "Octubre";
            else if (fechaServicio[1] == "11") mes = "Noviembre";
            else if (fechaServicio[1] == "12") mes = "Diciembre";

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("CONSENTIMIENTO INFORMADO DE EXAMEN MEDICO Y AUTORIZACIÓN DE ENTREGA DE RESULTADOS", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor=BaseColor.WHITE },
                    
                    new PdfPCell(new Phrase("\n \n  "+infoEmpresaPropietaria.v_SectorName+", "+ fechaServicio[0] + " de " + mes + " del " + fechaServicio[2], fontColumnValue))
                        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE },    

                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            
            string tipodoc = "";
            if (datosPac.i_DocTypeId == 1) { tipodoc = "DNI"; }
            else if (datosPac.i_DocTypeId == 2) { tipodoc = "Pasaporte"; }
            else if (datosPac.i_DocTypeId == 3) { tipodoc = "Licencia de Conducir"; }
            else if (datosPac.i_DocTypeId == 4) { tipodoc = "Carnet de Extranjeria"; }

            #region Contenido
            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("\n \nYO,  " , fontColumnValue))
                { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
               
                new PdfPCell(new Phrase("\n \n" + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue))
                    { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("\nIdentificado (a) con " +  tipodoc  + " N° " + datosPac.v_DocNumber + ", con ocupación laboral de", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation + ", certifico que he sido inforamdo acerca de la naturaleza y propósito", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("de los exámenes ocupacionales y pruebas complementarias de la empresa", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("\n"+empresacontrata, fontColumnValue1)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda ,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 


                new PdfPCell(new Phrase("Unidad :", fontColumnValue)) 
                    { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase(empresageneral, fontColumnValue1)) 
                    { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase(" solicita, :", fontColumnValue)) 
                    { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 


                new PdfPCell(new Phrase("y que todas mis dudas y preguntas al respecto han sido absueltas; así mismo, autorizo que los", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("resultados sean entregados al departamento médico de la empresa la cual soy trabajador.", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 


                //new PdfPCell(new Phrase("\nTrabajador de la empresa :    ", fontColumnValue)) 
                //{ Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
                //new PdfPCell(new Phrase("\n"+empr_Conct, fontColumnValue)) 
                //{ Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
                

                new PdfPCell(new Phrase("\nPor tanto, en forma consciente y voluntaria doy mi consentimiento expreso, de acuerdo a lo establecido en el", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
                
                new PdfPCell(new Phrase("item 6.6.2 el Documento Técnico: Protocolos de Exámenes Médicos Ocupacionales y Guías de diagnóstico de", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("los Exámenes Médicos Obligatorios por Actividad aprobado por la Resolución Ministerial N° 312-2011/MINSA,", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("para que el Centro Médico que " + empresacontrata + " designe, proceda a efectuar" , fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("los exámenes que correspondan en relación a los riesgos laborales propios del puesto de trabajo de", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("desempeño en la empresa; asimismo, autorizo la utilización de los resultados conforme al artículo 102° del ", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("Decreto Supremo N° 002-2012-TR, Reglamento de la ley de Seguridad y Salud en el Trabajo.", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                
                new PdfPCell(new Phrase("\nDe igual manera, autorizo que el Médico Ocupacional de " + empresacontrata, fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
                
                new PdfPCell(new Phrase("obtenga la información de mi historia clínica presente o anterior, aurotizando a que la pueda solicitar", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("directamente a la institución que corresponda, conforme al inciso a) del Artículo 25° de la Ley N° 26842,", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

                new PdfPCell(new Phrase("Ley General de Salud.", fontColumnValue)) 
                    { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 

              
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            
            

            PdfPCell cellFirmaTrabajador = null;

            PdfPCell cellHuellaTrabajador = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 125, 50));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 55, 75));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;


            #region Fecha / Firmmal
            cells = new List<PdfPCell>()
            {          
                
                
                new PdfPCell(cellFirmaTrabajador ) {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=120, BorderColor=BaseColor.WHITE},
                new PdfPCell(cellHuellaTrabajador ) {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=120, BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("_____________________________________", fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("Firma del Paciente", fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("N° DNI/ Carne de Extranjería y Pasaporte: " + datosPac.v_DocNumber, fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("...................", fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("Ley N° 26842 Art. 4 y Art.25", fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},

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
