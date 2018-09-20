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
    public class OrdenServicio
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();

        }
        public static void CrearOrdenServicio(Boolean MostrarPrecio,
            List<ServiceOrderPdf> ListaServiceOrder, 
            OrganizationList infoEmpresaPropietaria,
            string EmpresaCliente,
            string ServiceOrderId,
            string Fecha,
            string Usuario, 
            string filePDF)
        {

            Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);
            document.SetPageSize(iTextSharp.text.PageSize.A4);


            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            pdfPage page = new pdfPage();
            writer.PageEvent = page;
            document.Open();

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

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
              #endregion
            var tamaño_celda = 20f;

            #region PRIMERA PÁGINA
            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            iTextSharp.text.Image imagenfirma = iTextSharp.text.Image.GetInstance("C:/Banner/GetImageText.jpg");
            imagenfirma.ScalePercent(100);
            imagenfirma.SetAbsolutePosition(200, 200);
            document.Add(imagenfirma);
            #endregion
            #region Contenido
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
               
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            string[] fech = Fecha.ToString().Split(' ');
            string mes = null;
            if (fech[2] == "enero") mes = "01";
            else if (fech[2] == "febrero") mes = "02";
            else if (fech[2] == "marzo") mes = "03";
            else if (fech[2] == "abril") mes = "04";
            else if (fech[2] == "mayo") mes = "05";
            else if (fech[2] == "junio") mes = "06";
            else if (fech[2] == "julio") mes = "07";
            else if (fech[2] == "agosto") mes = "08";
            else if (fech[2] == "setiembre") mes = "09";
            else if (fech[2] == "octubre") mes = "10";
            else if (fech[2] == "noviembre") mes = "11";
            else if (fech[2] == "diciembre") mes = "12";

            string anio = fech[4];

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("\n"+infoEmpresaPropietaria.v_Sede + ", " + Fecha, fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, BorderColor = BaseColor.WHITE, MinimumHeight = tamaño_celda},
               
                new PdfPCell(new Phrase("Carta N° "+ServiceOrderId.Substring(7,9)+"-"+ mes +"-"+anio.Substring(2,2)+"-CSLS.R.L./CAL.", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("Estimado (s) Sr (s). " + EmpresaCliente, fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("ASUNTO: PROPUESTA ECONÓMICA PARA EXAMENES MÉDICOS OCUPACIONALES", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("Estimado (s) Sr (s).", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               

                new PdfPCell(new Phrase("\n\n\t    Es grato dirigirnos a Ud (s). Para saludarlo (s), y a la vez agradecer su preferencia en nuestra ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("institución.", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                
                
                new PdfPCell(new Phrase("\nEn esta oportunidad nos complace hacerles llegar nuestra propuesta técnico económico de nuestros", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\nservicios de Exámenes Médicos Ocupacionales (pre ocupacionales, periódicos, retiro, visita, transcripciones, ", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("\nexámenes de altura, psicosensométrico, psicológico, otros). Además cabe señalar que nuestra propuesta", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("está sujeta a evaluación", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                

                new PdfPCell(new Phrase("\n\t    "+infoEmpresaPropietaria.v_Name+" agradece su rpeferencia y está a vuestra disposición para iniciar un vínculo", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("laboral duradero.", fontColumnValue)) 
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda , BorderColor=BaseColor.WHITE}, 
                

                new PdfPCell(new Phrase("\nNos despedimos quedansdo a sus gratas órdenes.", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("\nAtentamente.", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("\nCc. Archivo", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 50, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(filiationWorker);
            #endregion
            #region Fecha / Firmma
            cells = new List<PdfPCell>()
            {          
          
                new PdfPCell(new Phrase("", fontColumnValue)){Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=180, BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase(infoEmpresaPropietaria.v_Name, fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15 , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("Tel. 076 340201 anexo 29", fontColumnValueBold)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15 , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("Cel. 976220538", fontColumnValueBold)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15 , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("cesar.medina@clinicasanlorenzo.com.pe", fontColumnValueBold)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15 , BorderColor=BaseColor.WHITE},

              };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion       
            #endregion
            document.NewPage();
           

            #region SEGUNDA PAGINA
            #region TÍTULO
            
            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            #endregion
            #region empresa y servicio
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("\n\n", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
               
                new PdfPCell(new Phrase("PROPUESTA ECONOMICA " + EmpresaCliente, fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("EVALUACION MEDICO OCUPACIONAL", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("EXAMENES MÉDICOS PRE OCUPACIONAL", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
            
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(filiationWorker);
            cells = new List<PdfPCell>();
            #region
            #region Parte Dinámica
            cells = new List<PdfPCell>();
            foreach (var Cabecera in ListaServiceOrder)
            {           

               cell = new PdfPCell(new Phrase(Cabecera.EmpresaCliente, fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT };
              cells.Add(cell);
              var ListaFiltrada = Cabecera.DetalleServiceOrder.FindAll(p => p.v_Precio != 0.00);
              foreach (var Detalle in ListaFiltrada)
              {
                  cell = new PdfPCell(new Phrase(Detalle.Componente, fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                  cells.Add(cell);
                  string Valor = MostrarPrecio == true ? Detalle.v_Precio.ToString() == "0.01" ? "COSTO CERO" :  Detalle.v_Precio.ToString() : Detalle.v_Precio.ToString() == "0.01" ? "COSTO CERO" : "";
              
                  if (Valor != "")
                  {
                      Valor =  "S/." + Valor;
                  }

                  cell = new PdfPCell(new Phrase(Valor, fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
                  cells.Add(cell);
              }
              cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
              cells.Add(cell);
            
              cell = new PdfPCell(new Phrase("TOTAL: S/. ", fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER };
              cells.Add(cell);
            

              cell = new PdfPCell(new Phrase(Cabecera.TotalProtocolo.ToString(), fontColumnValueNegrita)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER};
              cells.Add(cell);

              cell = new PdfPCell(new Phrase("Los precios no incluyen I.G.V.", fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
              cells.Add(cell);

              cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
              cells.Add(cell);
            }

           
            columnWidths = new float[] { 50f,50f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion
           
            #endregion
            #endregion
            #endregion

            document.NewPage();
            #region TERCERA HOJA
            #region TÍTULO

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            #endregion
            #region Texto en duro

            cells = new List<PdfPCell>()
            {
             new PdfPCell(new Phrase("\n\n", fontColumnValue))
             { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
              
                new PdfPCell(new Phrase("INTERCONSULTA CON ESPECIALISTAS", fontColumnValueBold ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("   -  OFTALMOLOGIA", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               new PdfPCell(new Phrase("   -  OTORRINOLARINGOLOGIA", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               new PdfPCell(new Phrase("   -  NUTRICIONISTA", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               new PdfPCell(new Phrase("   -  MEDICINA INTERNA", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               new PdfPCell(new Phrase("   -  CARDIOLOGIA", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               new PdfPCell(new Phrase("   -  TRAUMATOLOGIA", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               new PdfPCell(new Phrase("   -  NEUMOLOGIA", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               new PdfPCell(new Phrase("   -  OTROS SEGÚN REQUERIMIENTO", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("NOTA:", fontColumnValueBold ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("TODOS LOS COSTOS INCLUYEN IGV", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                 new PdfPCell(new Phrase("\nNUESTROS HORARIOS DE ATENCIÓN", fontColumnValueBold ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("Nuestro horario de atención son las 24 horas del día, los 365 días del año en nuestra área asistencial.", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("De Lunes a Sábado de 7:30 am a 12 mm, y de 3:00 pm a 7:00 pm en nuestra ára Ocupacional. ", fontColumnValue ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("NÚMERO DE CUENTA:", fontColumnValueBold ))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                
                new PdfPCell(new Phrase("", fontColumnValue))
             { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
             

            //new PdfPCell(new Phrase("FRACCIONAMIENTO DE PAGO(solo aplica para tipo de examen PERIÓDICO)", fontColumnValueNegrita)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

            //new PdfPCell(new Phrase("(Modalidad flexible)", fontColumnValueNegrita)){Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
           
            //new PdfPCell(new Phrase("MONTO GLOBAL A CANCELAR", fontColumnValueNegrita)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            //new PdfPCell(new Phrase("FRACCIONAMIENTO DE PAGO", fontColumnValueNegrita)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
            //new PdfPCell(new Phrase("PORCENTAJE", fontColumnValueNegrita)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},

            //new PdfPCell(new Phrase("100% del total a pagar (incluyendo I.G.V)", fontColumnValueNegrita)){ Rowspan= 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER , VerticalAlignment = PdfPCell.ALIGN_MIDDLE},
            //new PdfPCell(new Phrase("Ira cuota\n (3 días previos a la evaluación)", fontColumnValueNegrita)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
            // new PdfPCell(new Phrase("50%", fontColumnValueNegrita)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},

            // new PdfPCell(new Phrase("2da cuota \n(30 días posteriores a la Ira. cuota contra entrega a los resiltados)", fontColumnValueNegrita)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
            // new PdfPCell(new Phrase("50%", fontColumnValueNegrita)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE},

            // new PdfPCell(new Phrase("PAGO TOTAL DE CANCELACIÓN (INCLUIDO I.G.V.)", fontColumnValueNegrita)){ Colspan= 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
            //new PdfPCell(new Phrase("100% ", fontColumnValueNegrita)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},   
 
            // new PdfPCell(new Phrase(" ", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },

            //new PdfPCell(new Phrase("Esperamos poder atender a su empresa recordándole que esta opción de brindar un servicio de salud de alta calidad está disponible para", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },
            //new PdfPCell(new Phrase("vuestra empresa; nos despedimos quedando a sus gratas órdenes.", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },

            // new PdfPCell(new Phrase(" ", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },

            // new PdfPCell(new Phrase("Atentamente.", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },

            //  new PdfPCell(new Phrase(" ", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },
            // new PdfPCell(new Phrase(" ", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },


            // new PdfPCell(new Phrase(Usuario, fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },
            // new PdfPCell(new Phrase("Coordinadora Médico Ocupacional", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },
            // new PdfPCell(new Phrase(infoEmpresaPropietaria.v_Name, fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER },
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };

            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(filiationWorker);

            cells = new List<PdfPCell>()
                {          
                    new PdfPCell(new Phrase("ENTIDAD BANCARIA", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("NÚMERO DE CUENTA", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("CUENTA INTERBANCARIA SOLES", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("NÚMERO DE CUENTA DOLARES", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },

                    new PdfPCell(new Phrase("BANCO CONTINENTAL\n(Cuenta Corriente)", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("0011-0277-0100054539-10", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("011-277-000100054539-10", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("0011-0277-0100054547-13", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },

                    new PdfPCell(new Phrase("BANCO CONTINENTAL\n(Cuenta de Ahorro)", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("0011-0277-11-0200278043", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("0011-0277-11-0200278043 11", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("-", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },

                    new PdfPCell(new Phrase("BANCO DE CREDITO\n(Cuenta Corriente)", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("245-1810834-0-98", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("002 245 0018 1083 409897", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("-", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },

                    new PdfPCell(new Phrase("BANCO INTERBANK\n(Cuenta Corriente)", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("702-3000888410", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("-", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("-", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },

                    new PdfPCell(new Phrase("", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },

                    new PdfPCell(new Phrase("CUENTA DE DETRACCIONES", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },

                    new PdfPCell(new Phrase("BANCO DE LA NACION", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("00-774-002413", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },

                };

            columnWidths = new float[] { 25f, 25f, 25f, 25f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion
            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);
        }

    }
}
