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

    public class ConsentimientoInformadoAccesoClinica
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateConsentimientoInformadoAccesoHistoriClinica(ServiceList DataService, string filePDF,
            PacientList datosPac,
            List<DiagnosticRepositoryList> Diagnosticos,
            List<ServiceComponentList> serviceComponent)
        {
            Document document = new Document(PageSize.A4, 40f, 40f, 10f, 0f);

            //
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

            Font fontColumnValue = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            #region TÍTULO

            cells = new List<PdfPCell>();

            iTextSharp.text.Image hudbay = iTextSharp.text.Image.GetInstance("C:/Banner/HUDBAY.png");
            hudbay.ScalePercent(50);
            hudbay.SetAbsolutePosition(45, 760);
            document.Add(hudbay);

            iTextSharp.text.Image banner4 = iTextSharp.text.Image.GetInstance("C:/Banner/Imagen2.png");
            banner4.ScalePercent(50);
            banner4.SetAbsolutePosition(-10, 0);
            document.Add(banner4);


            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f },
                    new PdfPCell(new Phrase("CONSENTIMIENTO INFORMADO PARA \n ACCESO A LA HISTORIA CLÍNICA", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f},
                    new PdfPCell(new Phrase("Formato \n\nDocumento ID   : \nVersión     : \nFecha       : ", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f },

                };
            columnWidths = new float[] { 20f, 50f, 30f  };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            var tamaño_celda = 20f;
            

            ServiceComponentList declaracion_jurada = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXCEPCIONES_RX_ID);
            var fecha_regla = declaracion_jurada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXCEPCIONES_RX_FECHA_ULTIMA_REGLA) == null ? "" : declaracion_jurada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXCEPCIONES_RX_FECHA_ULTIMA_REGLA).v_Value1;

            #region Contenido
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("La historia clínica es el único documento válido desde el punto de vista clínico y legal.", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("\nLa Ley General de Salud del Perú menciona que toda persona usuaria de los servicios de salud tiene ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("derecho a exigir la reserva de la información relacionada con el acto médico y su historia clínica, con las ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("excepciones que la ley establece.", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                
                new PdfPCell(new Phrase("\nEl artículo 77° del Código de Ética y Deontología del Colegio Médico del Perú señala que la elaboración, ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("diagnóstico, terapéutica y pronóstico contenidos en la historia clínica pueden ser utilizados por el médico ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("tratante para fines de investigación, docencia, mejora continua de la calidad, médico legal, gestión y ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("administración de los servicios médicos. El médico está obligado, en este caso, a mantener en reserva", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("aquellos datos que permitan la identificación del paciente. La mencionada norma establece que la ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("violación de esta disposición constituye falta ética.", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
               
                new PdfPCell(new Phrase("\nLa Gerencia de Seguridad y Salud Ocupacional de Hudbay y los Centros Médicos Autorizados de HBP,", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("mantendrán confidencial su información médica privada obtenida al proporcionarle servicios a usted a ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("menos que se autorice por ley, su información médica protegida no será utilizada ni revelada sin su ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("consentimiento. ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                
                new PdfPCell(new Phrase("\nEl informe médico que se entregará a su Empresa contiene información relacionada únicamente a la ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("aptitud laboral y enfermedad (es) relacionada (s) al trabajo diagnosticado(s), u otras condiciones", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("médicas que puedan afectar la seguridad en el trabajo y/o aquellas condiciones médicas que puedan", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("confundir la  exposición a riesgos ocupacionales (Embarazo y/o Riesgos de Salud Reproductiva),", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("asimismo se indicará las recomendaciones y restricciones del caso. Por otro lado Usted recibirá un", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("Informe Médico Ocupacional resumido de lo más importante de su evaluación realizada.", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                
                new PdfPCell(new Phrase("\nSu información médica será protegida y no será revelada sin su consentimiento por escrito. Usted puede ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("invalidar cualquier consentimiento oral y/o escrito, enviando un documento a la Gerencia de Seguridad y ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("Salud Ocupacional.", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                
                new PdfPCell(new Phrase("\nHabiendo leído y comprendido lo anteriormente descrito, acepto el acceso a la historia clínica para los ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("fines detallados en este documento al Área de Salud Ocupacional de Hudbay.", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            
            string tipodoc = "";
            if (datosPac.i_DocTypeId == 1) { tipodoc = "DNI"; }
            else if (datosPac.i_DocTypeId == 2) { tipodoc = "Pasaporte"; }
            else if (datosPac.i_DocTypeId == 3) { tipodoc = "Licencia de Conducir"; }
            else if (datosPac.i_DocTypeId == 4) { tipodoc = "Carnet de Extranjeria"; }
            PdfPCell cellFirmaTrabajador = null;

            PdfPCell cellHuellaTrabajador = null;

            if (DataService.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(DataService.FirmaTrabajador, null, null, 100, 35));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (DataService.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(DataService.HuellaTrabajador, null, null, 50, 70));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            #region Fecha / Firmma
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("APELLIDOS Y NOMBRES: ", fontColumnValue)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80, BorderColor=BaseColor.WHITE },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)){ Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80, BorderColor=BaseColor.WHITE},    
                new PdfPCell(new Phrase("FIRMA: ", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80, BorderColor=BaseColor.WHITE },    
                new PdfPCell(cellFirmaTrabajador){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80, BorderColor=BaseColor.WHITE },    
                new PdfPCell(cellHuellaTrabajador){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80, BorderColor=BaseColor.WHITE },    


                new PdfPCell(new Phrase(tipodoc + " :", fontColumnValue)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20, BorderColor=BaseColor.WHITE },    
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)){ Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20, BorderColor=BaseColor.WHITE },    
                new PdfPCell(new Phrase("FECHA: ", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20, BorderColor=BaseColor.WHITE },    
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)){ Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20, BorderColor=BaseColor.WHITE },    

                new PdfPCell(new Phrase("EMPRESA: ", fontColumnValue)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20, BorderColor=BaseColor.WHITE },    
                new PdfPCell(new Phrase(DataService.v_CustomerOrganizationName, fontColumnValue)) 
                { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20 , BorderColor=BaseColor.WHITE, ExtraParagraphSpace = 5.0f}, 
              
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
