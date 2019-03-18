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
    public class Historia_Clinica
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateHistoria_Clinica(string filePDF,organizationDto infoEmpresaPropietaria,
            PacientList datosPac, MedicoTratanteAtenciones medico, List<ServiceComponentList> exams) // List<ServiceComponentList> serviceComponent
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
            List<PdfPCell> cells2 = null;
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
            PdfPCell cell2 = null;
            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Fonts
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle1_1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
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
            string[] servicio = datosPac.FechaServicio.ToString().Split(' ');

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase(servicio[0] + "               ", fontTitle1_1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},

                    new PdfPCell(new Phrase("ATENCIÓN MÉDICA - HISTORIA CLÍNICA", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 20f},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Valores
            var tamaño_celda = 15f;

            #endregion

            #region Primera página
            #region Datos del Servicio

            string fechaInforme = DateTime.Now.ToString().Split(' ')[0];
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
            string med = "";
            if (medico != null)
            {
                med = medico.Nombre;
            }
            else
            {
                med = "CLINICA SAN LORENZO";
            }

            //Antropometria
            ServiceComponentList antro = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            string peso = "", peso_unidad = "", talla = "", talla_unidad = "", imc = "", imc_unidad = "";
            if (antro != null)
            {
                peso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
                peso_unidad = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_MeasurementUnitName;
                talla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_Value1;
                talla_unidad = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_MeasurementUnitName;
                imc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_Value1;
                imc_unidad = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_MeasurementUnitName;

            }
            else
            {
                peso = "";
                peso_unidad = "";
                talla = "";
                talla_unidad = "";
                imc = "";
                imc_unidad = "";
            }

            //Funciones Vitales
            ServiceComponentList funcVit = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
            string temp = "", temp_unidad = "", pres_Sist = "", pres_Diast = "", pres_Diast_unidad = "", frecCard = "", frecCard_unidad = "", frecResp = "", frecResp_unidad = "", spo2 = "", spo2_unidad = "";
            if (funcVit != null)
            {
                temp = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID).v_Value1;
                temp_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID).v_MeasurementUnitName;
                pres_Sist = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
                pres_Diast = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;
                pres_Diast_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_MeasurementUnitName;
                frecCard = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_Value1;
                frecCard_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_MeasurementUnitName;
                frecResp = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_Value1;
                frecResp_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_MeasurementUnitName;
                spo2 = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID).v_Value1;
                spo2_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID).v_MeasurementUnitName;

            }
            else
            {
                temp = "";
                temp_unidad = "";
                pres_Diast = "";
                pres_Diast_unidad = "";
                pres_Sist = "";
                frecCard = "";
                frecCard_unidad = "";
                frecResp = "";
                frecResp_unidad = "";
                spo2 = "";
                spo2_unidad = "";
            }

            ServiceComponentList atenInte = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ID);

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("CODIGO DE ATENCIÓN:", fontColumnValueBold)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_IdService, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("MÉDICO TRATANTE:  ", fontColumnValueBold)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(med, fontColumnValue)) { Colspan = 6,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                    new PdfPCell(new Phrase("PACIENTE:", fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f,UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase("FECHA IMPRESIÓN:", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(fechaInforme, fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    

                    new PdfPCell(new Phrase("EDAD:", fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(datosPac.Edad.ToString() + " Años", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },    
                    new PdfPCell(new Phrase("SEXO:", fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(datosPac.Genero, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase("EST. CIVIL:", fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(datosPac.v_MaritalStatus, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase("N° Tel.:", fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},
                    new PdfPCell(new Phrase(datosPac.v_TelephoneNumber, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                    new PdfPCell(new Phrase("Fecha Nacimiento:", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(fechaNac[0], fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("DNI:", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("N° HIST C:", fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("NACIONALIDAD:", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_Nacionalidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    

                    new PdfPCell(new Phrase("RESIDENCIA ACTUAL:", fontColumnValueBold)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_AdressLocation, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("RESIDENCIA ANTERIOR:", fontColumnValueBold)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_ResidenciaAnterior==""?"- - -":datosPac.v_ResidenciaAnterior==null?"- - -":datosPac.v_ResidenciaAnterior, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    new PdfPCell(new Phrase("G. DE INSTRUCCIÓN:", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.GradoInstruccion, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("RELIGIÓN:", fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_Religion==""?"- - -":datosPac.v_Religion==null?"- - -":datosPac.v_Religion, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                    new PdfPCell(new Phrase("OCUPACIÓN:", fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase(datosPac.v_CurrentOccupation==""?"- - -":datosPac.v_CurrentOccupation==null?"- - -":datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                   
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK, MinimumHeight=4f },

                     new PdfPCell(new Phrase("Talla:", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f},
                    new PdfPCell(new Phrase(talla + " "+ talla_unidad, fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f},
                    new PdfPCell(new Phrase("Peso:", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f},
                    new PdfPCell(new Phrase(peso + " " + peso_unidad, fontColumnValue)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=18f},
                    new PdfPCell(new Phrase("FR:", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=18f},
                    new PdfPCell(new Phrase(frecResp + " " + frecResp_unidad, fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f},
                    new PdfPCell(new Phrase("IMC", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },
                    new PdfPCell(new Phrase(imc + " " + imc_unidad, fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },

                    new PdfPCell(new Phrase("T°:", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },
                    new PdfPCell(new Phrase(temp + " " + temp_unidad, fontColumnValue)) {Colspan=3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },
                    new PdfPCell(new Phrase("PA:", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },
                    new PdfPCell(new Phrase(pres_Sist + " / " + pres_Diast + " " + pres_Diast_unidad, fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },
                    new PdfPCell(new Phrase("FC", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },
                    new PdfPCell(new Phrase(frecCard + " " + frecCard, fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f},
                    new PdfPCell(new Phrase("SpO2", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },
                    new PdfPCell(new Phrase(spo2 + " " + spo2_unidad, fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },

                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion


            #region MOTIVO DE CONSULTA
            
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("MOTIVO DE CONSULTA:", fontColumnValueBold)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("SIGNOS Y SÍNTOMAS:", fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=40f  },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=40f  },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("ENFERMEDAD ACTUAL:", fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("TIEMPO DE ENFERMEDAD:", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=15 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                };

            columnWidths = new float[] { 2f, 8f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 3f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER ,null, fontTitleTable);
            document.Add(table);
            #endregion

            #region RELATO CRONOLÓGICO

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("RELATO CRONOLÓGICO", fontColumnValueBold)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=30f  },
                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER ,null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ANTECEDENTES
            #region ANTECEDENTES PATOLOGICOS Y FAMILIARES
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValueBold)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=35f },
                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EXAMEN FÍSICO

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("EXAMEN FÍSICO", fontColumnValueBold)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("PIEL, FANERAS Y TEJIDO CELULAR SUBCUTÁNEO: ", fontColumnValue)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("APARATO RESPIRATORIO: ", fontColumnValue)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("APARATO CARDIOVASCULAR: ", fontColumnValue)) {Colspan=7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=12 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("ABDOMEN: ", fontColumnValue)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("APARATO GENITOURINARIO: ", fontColumnValue)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("SISTEMA NERVIOSO: ", fontColumnValue)) {Colspan=7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=12 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                   
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
                    new PdfPCell(new Phrase("OSTEOMUSCULAR: ", fontColumnValue)) {Colspan=7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=12 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f  },
            
                };

            columnWidths = new float[] { 2f, 8f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTable);
            document.Add(table);

            #endregion

            #region EXAMENES AUXILIARES

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("EXAMENES AUXILIARES", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=30f },
                };

            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DIAGNOSTICOS
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("DIAGNOSTICOS", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},       

                    new PdfPCell(new Phrase("1. ", fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    new PdfPCell(new Phrase("2. ", fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                
                };
            columnWidths = new float[] { 20.6f, 40.6f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER,null, fontTitleTableNegro, null);
            document.Add(filiationWorker);

            #endregion

            #region PERFIL TERAPEUTICO
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("PLAN TERAPEUTICO", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase("1. ", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    new PdfPCell(new Phrase("2. ", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                    new PdfPCell(new Phrase("3. ", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },
                
                    new PdfPCell(new Phrase("SELLO Y FIRMA DEL MEDICO", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=20f },

                };

            columnWidths = new float[] { 100F };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Firma

            //#region Creando celdas de tipo Imagen y validando nulls

            //// Firma del trabajador ***************************************************
            //PdfPCell cellFirmaTrabajador = null;

            //if (datosPac.FirmaTrabajador != null)
            //    cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(datosPac.FirmaTrabajador, null, null, 70, 30));
            //else

            //    cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            //// Huella del trabajador **************************************************
            //PdfPCell cellHuellaTrabajador = null;

            //if (datosPac.HuellaTrabajador != null)
            //    cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(datosPac.HuellaTrabajador, null, null, 30, 30));
            //else
            //    cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            //// Firma del doctor Auditor **************************************************

            //PdfPCell cellFirma = null;

            ////if (medico.FirmaMedicoMedicina != null)
            ////    cellFirma = new PdfPCell(HandlingItextSharp.GetImage(medico.FirmaMedicoMedicina, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            ////else
            ////    cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

            //#endregion

            //#region Crear tablas en duro (para la Firma y huella del trabajador)

            //cells = new List<PdfPCell>();

            //cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            //cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            //cellFirmaTrabajador.Border = PdfPCell.NO_BORDER;
            //cellFirmaTrabajador.FixedHeight = 40F;
            //cells.Add(cellFirmaTrabajador);
            //cells.Add(new PdfPCell(new Phrase("FIRMA DEL EXAMINADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            //columnWidths = new float[] { 100f };

            //var tableFirmaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            ////***********************************************

            //cells = new List<PdfPCell>();

            //cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            //cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            //cellFirmaTrabajador.Border = PdfPCell.NO_BORDER;
            //cellHuellaTrabajador.FixedHeight = 40F;
            //cells.Add(cellHuellaTrabajador);
            //cells.Add(new PdfPCell(new Phrase("HUELLA DEL EXAMINADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            //columnWidths = new float[] { 100f };

            //var tableHuellaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            //#endregion

            //cells = new List<PdfPCell>();

            //// 1 celda vacia              
            //cells.Add(new PdfPCell(tableFirmaTrabajador));

            //// 1 celda vacia
            //cells.Add(new PdfPCell(tableHuellaTrabajador));

            //// 2 celda
            //cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue)) { Rowspan = 2 };
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //cells.Add(cell);

            //// 3 celda (Imagen)
            ////cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            ////cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            ////cellFirma.FixedHeight = 40F;
            ////cells.Add(cellFirma);

            //cells.Add(new PdfPCell(new Phrase("CON LA CUAL DECLARA QUE LA INFORMACIÓN DECLARADA ES VERAZ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
            //cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2 });

            //columnWidths = new float[] { 35f, 35f, 30f, 40F };
            //table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            //document.Add(table);

            #endregion
            #endregion
            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);
        }
    }
}
