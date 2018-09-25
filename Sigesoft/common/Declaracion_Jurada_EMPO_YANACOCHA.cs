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
    public class Declaracion_Jurada_EMPO_YANACOCHA
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateDeclaracion_Jurada_EMPO_YANACOCHA(ServiceList DataService, string filePDF,
          PacientList datosPac,
          organizationDto infoEmpresaPropietaria, PacientList filiationData,
          List<ServiceComponentList> serviceComponent,
            byte[] CuadroVacio,
            byte[] CuadroCheck)
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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
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


            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("DECLARACÓNN JURADA PARA EXAMENES MÉDICOS PRE OCUPACIONALES", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                };
            columnWidths = new float[] { 20f, 60f, 20f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            var tamaño_celda = 25f;
            string tipodoc1 = "", tipodoc2 = "", tipodoc3 = "", tipodoc4 = "";
            if (datosPac.i_DocTypeId == 1) { tipodoc1 = ""; }
            else if (datosPac.i_DocTypeId == 2) { tipodoc2 = ""; }
            else if (datosPac.i_DocTypeId == 3) { tipodoc3 = ""; }
            else if (datosPac.i_DocTypeId == 4) { tipodoc4 = ""; }

            #region Contenido
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("\n \n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n \n      YO, ", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n \n" , fontColumnValue)){ Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n \n identificado con", fontColumnValue)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("\n \n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                //+ datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("DNI ( "+ tipodoc1  + ") N°  / Pasaporte ( "+ tipodoc2+" ) /cARNET DE eXTRANJERÍA ( " + tipodoc4 + " )  Nro ", fontColumnValue)){ Colspan = 11, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("" + datosPac.v_DocNumber, fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("representante legal", fontColumnValue)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("de la empresa ", fontColumnValue)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("" , fontColumnValue)){ Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("mediante la presente declaro que la información presentada ante la Oficina de", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Proceso de Admisión de Minera Yanacocha SRL, respecto a los exámenes médicos", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("para la visita de mis trabajadores , es fidedigna, veraz y autorizo a comprobarla, ", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("entendiendo que la omisión o distorsión de la misma es motivo suficiente para la ", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase(" ", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("invalidez de mi tramite.", fontColumnValue)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            string[] fechaServicio = datosPac.FechaServicio.ToString().Split('/', ' ');
            string mes = "";
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

            PdfPCell cellFirmaTrabajador = null;

            PdfPCell cellHuellaTrabajador = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 100, 35));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 60, 80));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            #region Fecha / Firmma
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("\n \n \n \n ........................, "+ "....... de .....................del 20.....", fontColumnValue))
                { Colspan = 19, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BorderColor=BaseColor.WHITE },    
                new PdfPCell(new Phrase("\n", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell() {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=100, BorderColor=BaseColor.WHITE},
                new PdfPCell() {Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=100, BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("_____________________________________", fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("Firma", fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},


              };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Title

            PdfPCell CellLogo = null;
            PdfPCell cellPhoto1 = null;

            if (filiationData.b_Photo != null)
                cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, null, null, 80, 60)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
            else
                cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 20F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            PdfPCell cellConCheck = null;
            cellConCheck = new PdfPCell(HandlingItextSharp.GetImage(CuadroCheck));

            PdfPCell cellSinCheck = null;
            cellSinCheck = new PdfPCell(HandlingItextSharp.GetImage(CuadroVacio));


            PdfPCell PreOcupacional = cellSinCheck, Periodica = cellSinCheck, Retiro = cellSinCheck, Otros = cellSinCheck, Empresa = cellSinCheck, Contratista = cellSinCheck;

            if (DataService != null)
            {
                if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.PreOcupacional)
                {
                    PreOcupacional = cellConCheck;
                }
                else if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.PeriodicoAnual)
                {
                    Periodica = cellConCheck;
                }
                else if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.Retiro)
                {
                    Retiro = cellConCheck;
                }
                else
                {
                    Otros = cellConCheck;
                }

                if (DataService.i_ServiceTypeId == (int)Sigesoft.Common.ServiceType.Empresarial)
                {
                    Empresa = cellConCheck;

                }
                else
                {

                    Contratista = cellConCheck;
                }
            }

            cells = new List<PdfPCell>()
                   {      
                    //fila 
                    new PdfPCell(CellLogo){Rowspan =2, Colspan = 2, Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("ANEXO N° 16", fontTitle1)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("EXAMEN MÉDICO", fontColumnValueBold)){ Border = PdfPCell.NO_BORDER ,HorizontalAlignment=PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER},  
                    //fila
                    new PdfPCell(new Phrase("FICHA MÉDICA OCUPACIONAL", fontTitle1)){Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("PRE-OCUPACIONAL", fontColumnValue)){ Border = PdfPCell.NO_BORDER ,HorizontalAlignment=PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(PreOcupacional){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    //fila
                    new PdfPCell(new Phrase(" ", fontColumnValue)) {Border = PdfPCell.LEFT_BORDER, Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(cellPhoto1){Rowspan = 4, Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("ANUAL", fontColumnValue)){ Border = PdfPCell.NO_BORDER ,HorizontalAlignment=PdfPCell.ALIGN_LEFT},   
                    new PdfPCell(Periodica){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_CENTER },
                    //fila
                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)){Border = PdfPCell.LEFT_BORDER}, 
                    new PdfPCell(Empresa){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },                                   
                    new PdfPCell(new Phrase( DataService.v_CustomerOrganizationName,fontColumnValue)){Border = PdfPCell.NO_BORDER}, 
                    new PdfPCell(new Phrase("RETIRO", fontColumnValue)){ Border = PdfPCell.NO_BORDER ,HorizontalAlignment=PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(Retiro){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_CENTER },
                    //fila
                    new PdfPCell(new Phrase("CONTRATISTA:", fontColumnValue)){Border = PdfPCell.LEFT_BORDER},     
                    new PdfPCell(Contratista){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },                               
                    new PdfPCell(new Phrase(DataService.EmpresaEmpleadora, fontColumnValue)){Border = PdfPCell.NO_BORDER},    
                    new PdfPCell(new Phrase("REUBICACIÓN", fontColumnValue)){ Border = PdfPCell.NO_BORDER ,HorizontalAlignment=PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(Otros){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_CENTER },
                    //fila
                    new PdfPCell(new Phrase("APELLIDOS Y NOMBRES: ", fontColumnValue)){Colspan=1, Border = PdfPCell.LEFT_BORDER},                
                    new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan=2, Border = PdfPCell.NO_BORDER},                                        
                    new PdfPCell(new Phrase("N° DE FICHA: " + datosPac.v_IdService , fontColumnValue)){Colspan=2,Border = PdfPCell.NO_BORDER},    
                  
                    new PdfPCell(new Phrase("FECHA DEL EXAMEN", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},              
                    new PdfPCell(new Phrase(datosPac.FechaServicio.ToString().Split(' ')[0], fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    // fila                   
                    new PdfPCell(new Phrase("MINERALES EXPLOTADOS O PROCESADOS", fontColumnValue)){Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                           
                    new PdfPCell(new Phrase(datosPac.v_ExploitedMineral, fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                   
                   };

            columnWidths = new float[] { 15f, 5f, 30f, 25f, 20f, 5f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
