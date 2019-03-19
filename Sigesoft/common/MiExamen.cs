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
   
    public class MiExamen
    { 
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        #region Report MiExamen
            public static void  CreateMiExamen(
                List<ServiceComponentList> serviceComponent,
                organizationDto  infoEmpresa,
                PacientList datosPac,
                string filePDF)
            {
                Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);

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

                #region TÍTULO

                cells = new List<PdfPCell>();

                if (infoEmpresa.b_Image != null)
                {
                    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                    imagenEmpresa.ScalePercent(25);
                    imagenEmpresa.SetAbsolutePosition(40, 790);
                    document.Add(imagenEmpresa);
                }
                iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
                imagenMinsa.ScalePercent(10);
                imagenMinsa.SetAbsolutePosition(400, 785);
                document.Add(imagenMinsa);

                var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("PROBANDO EXAMEN", fontTitle1)) { BackgroundColor= BaseColor.ORANGE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},
                };
                columnWidths = new float[] { 100f };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion


                #region VALORES
                //ExamenValores
                ServiceComponentList miexamen = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.MI_EXAMEN);
                var campo1 = miexamen.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Campo_1) == null ? "" : miexamen.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Campo_1).v_Value1;
                var campo2 = miexamen.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Campo_2) == null ? "" : miexamen.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Campo_2).v_Value1;
     
                #endregion 

                #region Datos del Servicio

                //string[] servicio = filiationData.FechaServicio.ToString().Split(' ');
                //cellsTit = new List<PdfPCell>()
                //{ 
                //    new PdfPCell(new Phrase("CONSULTA MÉDICA", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, Colspan = 3, BackgroundColor=BaseColor.ORANGE, MinimumHeight=15f},  

                //    new PdfPCell(new Phrase("Fecha: " + servicio[0], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f},
                //    new PdfPCell(new Phrase("Hora: " + servicio[1], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },
                //    new PdfPCell(new Phrase("Edad: " + filiationData.Edad + " años.", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },
                //};

                columnWidths = new float[] { 40f, 40f, 20f };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                string[] fechaServicio = datosPac.FechaServicio.ToString().Split('/', ' ');
                #region Fecha
                cells = new List<PdfPCell>()
                {          
                    new PdfPCell(new Phrase("FECHA", fontColumnValue)) { Rowspan=2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                    new PdfPCell(new Phrase("día", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("mes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("año", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase("", fontColumnValue)) {Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
                    new PdfPCell(new Phrase("N° " + datosPac.v_PersonId, fontColumnValueBold)) { Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment = PdfPCell.ALIGN_MIDDLE }, 

                    new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase(fechaServicio[1], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                    new PdfPCell(new Phrase(fechaServicio[2], fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
                };

                columnWidths = new float[] { 10f, 5f, 5f, 10f, 50f, 20f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                #region DATOS GENERALES

                string[] fechaNac = datosPac.d_Birthdate.ToString().Split('/', ' ');
                string sexM = " ";
                string sexF = " ";
                if (datosPac.i_SexTypeId == 1)
                {
                    sexM = "X";
                }
                if (datosPac.i_SexTypeId == 2)
                {
                    sexF = "X";
                }
                cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("DATOS GENEREALES", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor = BaseColor.ORANGE },       

                new PdfPCell(new Phrase("Apellidos", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Nombres", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Sexo:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("M", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(sexM, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("F", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(sexF, fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Edad:", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_FirstName, fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Fecha Nacimiento", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(fechaNac[0], fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(fechaNac[1], fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(fechaNac[2], fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },

                new PdfPCell(new Phrase("Lugar de Nacimiento", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Procedencia", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Grupo Sanguineo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
                new PdfPCell(new Phrase("Rh", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase(datosPac.v_BirthPlace, fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_DistrictName + " - " + datosPac.v_ProvinceName + " - " + datosPac.v_DepartamentName, fontColumnValue)) { Colspan =8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_BloodGroupName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },               
                new PdfPCell(new Phrase(datosPac.v_BloodFactorName, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("G° de Instrucción", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Centro Educativo", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Estado Civil", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Ocupación", fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase(datosPac.GradoInstruccion, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase(datosPac.v_CentroEducativo, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_MaritalStatus, fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("Madre o Padre, acompañante o cuidador", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("Edad", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("Identificación (DNI)", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 4, Rowspan=2, BackgroundColor=BaseColor.GRAY, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT },    
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 
                new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT }, 

            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion




                #region MI EXAMEN 1

               cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("Datos de llenada...", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, Colspan = 4, BackgroundColor=BaseColor.ORANGE, MinimumHeight=15f},  

                    new PdfPCell(new Phrase("Datos 1:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f},
                    new PdfPCell(new Phrase(campo1, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f},
                    new PdfPCell(new Phrase("Datos 2:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f},
                    new PdfPCell(new Phrase(campo2, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },
                };

                columnWidths = new float[] { 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion


                document.Close();
                writer.Close();
                writer.Dispose();
            }
        #endregion
    }
    
}
