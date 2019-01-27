using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;
using System.Configuration;
using Sigesoft.Common;
using Document = iTextSharp.text.Document;

namespace NetPdf
{
    public class Pago_Especialsta_Report
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreatePago_Especialsta_Report(string monto1, string fechaInicio1, string fechaFin1, string usuarioMedico1, int usuarioPaga1, List<string> ids1, string ruta, string filePDF,organizationDto infoEmpresa)
        {

            Document document = new Document(PageSize.A5, 15f, 15f, 15f, 40f);

            document.SetPageSize(iTextSharp.text.PageSize.A5);

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

            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            #endregion

            var tamaño_celda = 18f;
            var tamaño_celda2 = 60f;

            #region Conexion Sigesoft
            ConexionSigesoft conectaSigesoft = new ConexionSigesoft();
            conectaSigesoft.opensigesoft();
            #endregion
            
            #region Fecha
            string anioinicio = fechaInicio1.Substring(6, 4);
            string mesinicio = fechaInicio1.Substring(3, 2);
            string diainicio = fechaInicio1.Substring(0, 2);
            DateTime localDate = DateTime.Now;
            #endregion

            #region Title

            var fechain = fechaInicio1.Split(' ');
            var fechafin = fechaFin1.Split(' ');
            var rutaImg = GetApplicationConfigValue("Logo");
            var imageLogo = new PdfPCell(GetImageLogo(rutaImg.ToString(), null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };

            #region Obtener usuarios

            var cadena1 = "select PP.v_FirstName+' '+PP.v_FirstLastName+' '+PP.v_SecondLastName, v_UserName from systemuser SU " +
                          "Inner join person PP " +
                          "On SU.v_PersonId=PP.v_PersonId " +
                          "where i_SystemUserId="+usuarioPaga1;
            SqlCommand comando1 = new SqlCommand(cadena1, connection: conectaSigesoft.conectarsigesoft);
            SqlDataReader lector1 = comando1.ExecuteReader();
            String cajanombre = "", cajauser="";
            while (lector1.Read())
            {
                cajanombre = lector1.GetValue(0).ToString();
                cajauser = lector1.GetValue(1).ToString();
            }
            lector1.Close();
                #endregion
            cells = new List<PdfPCell>()
            {
                new PdfPCell(imageLogo){Rowspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  MinimumHeight = tamaño_celda2,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("PAGO MÉDICO ESPECIALISTA ", fontTitle1)) {Rowspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase(" usuario: "+cajauser+"\r\n Desde: "+fechain[0]+"\r\n Hasta: "+fechafin[0], fontColumnValueBold)) {Rowspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
            };
            columnWidths = new float[] { 35f, 35f, 30f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
           
            #endregion

            #region Cabecera
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("Nombre del Paciente", fontColumnValueBold)) {BackgroundColor=BaseColor.LIGHT_GRAY, Rowspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  MinimumHeight = tamaño_celda,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                new PdfPCell(new Phrase("Protocolo de Atención", fontColumnValueBold)) {BackgroundColor=BaseColor.LIGHT_GRAY, Rowspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(new Phrase("Fecha", fontColumnValueBold)) {BackgroundColor=BaseColor.LIGHT_GRAY, Rowspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
            };
            columnWidths = new float[] { 42f, 42f, 15f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            #endregion

            #region Detalle
            foreach (var serviceId in ids1)
            {
                var ids = serviceId.Split('|');
                foreach (var id in ids)
                {
                    var r = id;
                    #region Query
                    var cadena = "Select PP.v_FirstName+', '+PP.v_FirstLastName+' '+PP.v_SecondLastName as Persona, " +
                                 "PR.v_Name as Protocolo, d_ServiceDate as Fecha " +
                                 "from service SR " +
                                 "Inner Join person PP " +
                                 " On SR.v_PersonId = PP.v_PersonId " +
                                 "Inner Join protocol PR " +
                                 "On SR.v_ProtocolId = PR.v_ProtocolId " +
                                 "where SR.v_ServiceId='" + id + "'";
                    #endregion

                    #region Lector Open
                    SqlCommand comando = new SqlCommand(cadena, connection: conectaSigesoft.conectarsigesoft);
                    SqlDataReader lector = comando.ExecuteReader();
                    #endregion

                    #region Llenar Detalle
                    while (lector.Read())
                    {
                        var fecha = lector.GetValue(2).ToString().Split(' ');
                        cells = new List<PdfPCell>()
                        { 
                        new PdfPCell(new Phrase(lector.GetValue(0).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        new PdfPCell(new Phrase(lector.GetValue(1).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        new PdfPCell(new Phrase(fecha[0].ToString(), fontColumnValue)){  Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        };
                        columnWidths = new float[] {  42f, 42f, 15f };
                        filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                        document.Add(filiationWorker);
                    }
                    lector.Close();
                    #endregion

                   
                }
            }
            #endregion

            #region Recibido
            cells = new List<PdfPCell>()
                    { 
                        new PdfPCell(new Phrase("Documento emitido por:", fontColumnValueBold)){BackgroundColor=BaseColor.LIGHT_GRAY,  Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        new PdfPCell(new Phrase("Dr(a) Especialista", fontColumnValueBold)){BackgroundColor=BaseColor.LIGHT_GRAY,  Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        new PdfPCell(new Phrase("Monto a pagar:", fontColumnValueBold)){BackgroundColor=BaseColor.LIGHT_GRAY,  Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 

                        new PdfPCell(new Phrase(cajanombre, fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        new PdfPCell(new Phrase(usuarioMedico1, fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        new PdfPCell(new Phrase(monto1, fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        
                        new PdfPCell(new Phrase("Firma Emisor:", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        new PdfPCell(new Phrase("Recibi conforme:", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 
                        new PdfPCell(new Phrase("Fecha y hora"+"\rde Impresión: \r"+localDate, fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK}, 

                    };
            columnWidths = new float[] {  42f, 42f, 15f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);


            #endregion
          
            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);
        }

        private static object GetApplicationConfigValue(string nombre)
        {
            return Convert.ToString(ConfigurationManager.AppSettings[nombre]);
        }

        public static Image GetImageLogo(string imgFileName, float? scalePercent, int? alignment, int? width, int? height)
        {
            //Insertar Imagen
            Image gif = Image.GetInstance(imgFileName);

            if (alignment != null)
                gif.Alignment = alignment.Value;
            else
                gif.Alignment = Image.ALIGN_LEFT;

            if (scalePercent != null)
            {
                gif.ScalePercent(scalePercent.Value);
            }
            else
            {
                gif.ScaleAbsolute(width.Value, height.Value);
            }

            return gif;
        }
    }
}
