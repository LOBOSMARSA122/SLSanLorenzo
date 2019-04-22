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
    public class AtencionIntegral
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateAtencionIntegral(string filePDF,
            DatosDoctorMedicina medico,
            PacientList datosPac,
            List<frmEsoAntecedentesPadre> Antecedentes,
            organizationDto infoEmpresaPropietaria,
            List<ServiceComponentList> exams,
            Ninioo datosNinio,
            Adolescente datosAdoles,
            Adulto datosAdult,
            List<Embarazo> listEmbarazos,
            AdultoMayor datosAdultMay,
            List<DiagnosticRepositoryList> Diagnosticos, List<recetadespachoDto> medicina, 
            List<ServiceComponentList> ExamenesServicio, MedicoTratanteAtenciones medicoo,
            UsuarioGrabo DatosGrabo)
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
            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));

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
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Valores
            var tamaño_celda = 13f;
            #endregion

            #region Datos del Servicio

            string fechaInforme = DateTime.Now.ToString().Split(' ')[0];
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
            string med = "";
            if (medicoo != null)
            {
                med = medicoo.Nombre;
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
                peso =  antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
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
                    new PdfPCell(new Phrase(frecCard + " " + frecCard_unidad, fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f},
                    new PdfPCell(new Phrase("SpO2", fontColumnValueBold)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },
                    new PdfPCell(new Phrase(spo2 + " " + spo2_unidad, fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=18f },

                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Descarte de Signos de Peligro
            //if (datosPac.Edad <= 12) //verificar que sea niño
            //{
            //    //niño
            //    ServiceComponentList niño = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ID);
            //    ///2
            //    var no_mama_2m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_NO_MAMAR_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_NO_MAMAR_2M).v_Value1;
            //    string no_mama_2m_1 = "", no_mama_2m_2 = "";
            //    if (no_mama_2m == "1") no_mama_2m_1 = "X";
            //    else if (no_mama_2m == "0") no_mama_2m_2 = "X";

            //    var convulsiones = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_CONVULSIONES_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_CONVULSIONES_2M).v_Value1;
            //    string convulsiones_1 = "", convulsiones_2 = "";
            //    if (convulsiones == "1") convulsiones_1 = "X";
            //    else if (convulsiones == "0") convulsiones_2 = "X";

            //    var fontanela = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_FONTANELA_ABOMB_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_FONTANELA_ABOMB_2M).v_Value1;
            //    string fontanela_1 = "", fontanela_2 = "";
            //    if (fontanela == "1") fontanela_1 = "X";
            //    else if (fontanela == "0") fontanela_2 = "X";

            //    var enroj_omb = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ENROJ_OMBLIGO_EXT_PIEL_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ENROJ_OMBLIGO_EXT_PIEL_2M).v_Value1;
            //    string enroj_omb_1 = "", enroj_omb_2 = "";
            //    if (enroj_omb == "1") enroj_omb_1 = "X";
            //    else if (enroj_omb == "0") enroj_omb_2 = "X";

            //    var fieb_tem_ba = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_FIEBRE_TEMP_BAJA_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_FIEBRE_TEMP_BAJA_2M).v_Value1;
            //    string fieb_tem_ba_1 = "", fieb_tem_ba_2 = "";
            //    if (fieb_tem_ba == "1") fieb_tem_ba_1 = "X";
            //    else if (fieb_tem_ba == "0") fieb_tem_ba_2 = "X";

            //    var rigi_nunca = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_RIGIDEZ_NUNCA_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_RIGIDEZ_NUNCA_2M).v_Value1;
            //    string rigi_nunca_1 = "", rigi_nunca_2 = "";
            //    if (rigi_nunca == "1") rigi_nunca_1 = "X";
            //    else if (rigi_nunca == "0") rigi_nunca_2 = "X";

            //    var postulas_much = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_POSTULAS_MUCHAS_EXT_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_POSTULAS_MUCHAS_EXT_2M).v_Value1;
            //    string postulas_much_1 = "", postulas_much_2 = "";
            //    if (postulas_much == "1") postulas_much_1 = "X";
            //    else if (postulas_much == "0") postulas_much_2 = "X";

            //    var letar_comatoso = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETAR_COMATOSO_2M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETAR_COMATOSO_2M).v_Value1;
            //    string letar_comatoso_1 = "", letar_comatoso_2 = "";
            //    if (letar_comatoso == "1") letar_comatoso_1 = "X";
            //    else if (letar_comatoso == "0") letar_comatoso_2 = "X";
            //    ///4
            //    var no_toma_pecho = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_NO_BEBER_TOMAR_PECHO_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_NO_BEBER_TOMAR_PECHO_2_4M).v_Value1;
            //    string no_toma_pecho_1 = "", no_toma_pecho_2 = "";
            //    if (no_toma_pecho == "1") no_toma_pecho_1 = "X";
            //    else if (no_toma_pecho == "0") no_toma_pecho_2 = "X";

            //    var convulsiones_4m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_CONVULSIONES_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_CONVULSIONES_2_4M).v_Value1;
            //    string convulsiones_4m_1 = "", convulsiones_4m_2 = "";
            //    if (convulsiones_4m == "1") convulsiones_4m_1 = "X";
            //    else if (convulsiones_4m == "0") convulsiones_4m_2 = "X";

            //    var letar_comat_4m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETAR_COMAT_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETAR_COMAT_2_4M).v_Value1;
            //    string letar_comat_4m_1 = "", letar_comat_4m_2 = "";
            //    if (letar_comat_4m == "1") letar_comat_4m_1 = "X";
            //    else if (letar_comat_4m == "0") letar_comat_4m_2 = "X";

            //    var vomito_4m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_VOMITO_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_VOMITO_2_4M).v_Value1;
            //    string vomito_4m_1 = "", vomito_4m_2 = "";
            //    if (vomito_4m == "1") vomito_4m_1 = "X";
            //    else if (vomito_4m == "0") vomito_4m_2 = "X";

            //    var estridor_4m = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESTRIDOR_REPOSO_2_4M) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESTRIDOR_REPOSO_2_4M).v_Value1;
            //    string estridor_4m_1 = "", estridor_4m_2 = "";
            //    if (estridor_4m == "1") estridor_4m_1 = "X";
            //    else if (estridor_4m == "0") estridor_4m_2 = "X";
            //    ///todos
            //    var emaciaciacion_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_EMACIACION_VISIBLE_GRAVE_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_EMACIACION_VISIBLE_GRAVE_T).v_Value1;
            //    string emaciaciacion_t_1 = "", emaciaciacion_t_2 = "";
            //    if (emaciaciacion_t == "1") emaciaciacion_t_1 = "X";
            //    else if (emaciaciacion_t == "0") emaciaciacion_t_2 = "X";

            //    var piel_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PIEL_VUELVE_LENTO_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PIEL_VUELVE_LENTO_T).v_Value1;
            //    string piel_t_1 = "", piel_t_2 = "";
            //    if (piel_t == "1") piel_t_1 = "X";
            //    else if (piel_t == "0") piel_t_2 = "X";

            //    var traumatismo_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_TRAUMATISMO_QUEMADURAS_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_TRAUMATISMO_QUEMADURAS_T).v_Value1;
            //    string traumatismo_t_1 = "", traumatismo_t_2 = "";
            //    if (traumatismo_t == "1") traumatismo_t_1 = "X";
            //    else if (traumatismo_t == "0") traumatismo_t_2 = "X";

            //    var envenenamiento_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ENVENENAMIENTO_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ENVENENAMIENTO_T).v_Value1;
            //    string envenenamiento_t_1 = "", envenenamiento_t_2 = "";
            //    if (envenenamiento_t == "1") envenenamiento_t_1 = "X";
            //    else if (envenenamiento_t == "0") envenenamiento_t_2 = "X";

            //    var palidez_t = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PALIDEZ_PALMAR_T) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PALIDEZ_PALMAR_T).v_Value1;
            //    string palidez_t_1 = "", palidez_t_2 = "";
            //    if (palidez_t == "1") palidez_t_1 = "X";
            //    else if (palidez_t == "0") palidez_t_2 = "X";
            //    ////tos
            //    var respiracion_rapida = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_RESPIRACION_RAPIDA_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_RESPIRACION_RAPIDA_TOS).v_Value1;
            //    string respiracion_rapida_1 = "", respiracion_rapida_2 = "";
            //    if (respiracion_rapida == "1") respiracion_rapida_1 = "X";
            //    else if (respiracion_rapida == "0") respiracion_rapida_2 = "X";

            //    var observar_tiraje_subcostal = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OBSERVAR_TIRAJE_SUBCOSTAL_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OBSERVAR_TIRAJE_SUBCOSTAL_TOS).v_Value1;
            //    string observar_tiraje_subcostal_1 = "", observar_tiraje_subcostal_2 = "";
            //    if (observar_tiraje_subcostal == "1") observar_tiraje_subcostal_1 = "X";
            //    else if (observar_tiraje_subcostal == "0") observar_tiraje_subcostal_2 = "X";

            //    var escuchar_estridor = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESCUCHAR_ESTRIDOR_REPOSO_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESCUCHAR_ESTRIDOR_REPOSO_TOS).v_Value1;
            //    string escuchar_estridor_1 = "", escuchar_estridor_2 = "";
            //    if (escuchar_estridor == "1") escuchar_estridor_1 = "X";
            //    else if (escuchar_estridor == "0") escuchar_estridor_2 = "X";

            //    var escuchar_sibilancias = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESCUCHAR_SIBILANCIAS_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_ESCUCHAR_SIBILANCIAS_TOS).v_Value1;
            //    string escuchar_sibilancias_1 = "", escuchar_sibilancias_2 = "";
            //    if (escuchar_sibilancias == "1") escuchar_sibilancias_1 = "X";
            //    else if (escuchar_sibilancias == "0") escuchar_sibilancias_2 = "X";

            //    var observar_tiraje_subcostal_grave = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OBSERVAR_TRIAJE_SUBCOSTAL_GRAVE_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OBSERVAR_TRIAJE_SUBCOSTAL_GRAVE_TOS).v_Value1;
            //    string observar_tiraje_subcostal_grave_1 = "", observar_tiraje_subcostal_grave_2 = "";
            //    if (observar_tiraje_subcostal_grave == "1") observar_tiraje_subcostal_grave_1 = "X";
            //    else if (observar_tiraje_subcostal_grave == "0") observar_tiraje_subcostal_grave_2 = "X";

            //    var sibilancias_1ra = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIBILANCIAS_1RA_VEZ_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIBILANCIAS_1RA_VEZ_TOS).v_Value1;
            //    string sibilancias_1ra_1 = "", sibilancias_1ra_2 = "";
            //    if (sibilancias_1ra == "1") sibilancias_1ra_1 = "X";
            //    else if (sibilancias_1ra == "0") sibilancias_1ra_2 = "X";

            //    var sibilancias_recurrente = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIBILANCIAS_RECURRENTE_TOS) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIBILANCIAS_RECURRENTE_TOS).v_Value1;
            //    string sibilancias_recurrente_1 = "", sibilancias_recurrente_2 = "";
            //    if (sibilancias_recurrente == "1") sibilancias_recurrente_1 = "X";
            //    else if (sibilancias_recurrente == "0") sibilancias_recurrente_2 = "X";
            //    ///diarrea
            //    var sangre_heces = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SANGRE_HECES_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SANGRE_HECES_DIARREA).v_Value1;
            //    string sangre_heces_1 = "", sangre_heces_2 = "";
            //    if (sangre_heces == "1") sangre_heces_1 = "X";
            //    else if (sangre_heces == "0") sangre_heces_2 = "X";

            //    var letargio_comatoso = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETARGICO_COMATOSO_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_LETARGICO_COMATOSO_DIARREA).v_Value1;
            //    string letargio_comatoso_1 = "", letargio_comatoso_2 = "";
            //    if (letargio_comatoso == "1") letargio_comatoso_1 = "X";
            //    else if (letargio_comatoso == "0") letargio_comatoso_2 = "X";

            //    var intranquilo = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_INTRANQUILO_IRRITABLE_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_INTRANQUILO_IRRITABLE_DIARREA).v_Value1;
            //    string intranquilo_1 = "", intranquilo_2 = "";
            //    if (intranquilo == "1") intranquilo_1 = "X";
            //    else if (intranquilo == "0") intranquilo_2 = "X";

            //    var ojos_hundidos = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OJOS_HUNDIDOS_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_OJOS_HUNDIDOS_DIARREA).v_Value1;
            //    string ojos_hundidos_1 = "", ojos_hundidos_2 = "";
            //    if (ojos_hundidos == "1") ojos_hundidos_1 = "X";
            //    else if (ojos_hundidos == "0") ojos_hundidos_2 = "X";

            //    var puede_beber = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PUEDE_BEBER_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_PUEDE_BEBER_DIARREA).v_Value1;
            //    string puede_beber_1 = "", puede_beber_2 = "";
            //    if (puede_beber == "1") puede_beber_1 = "X";
            //    else if (puede_beber == "0") puede_beber_2 = "X";

            //    var bebe_Avidamente = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_BEBE_AVIDAMENTE_SED_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_BEBE_AVIDAMENTE_SED_DIARREA).v_Value1;
            //    string bebe_Avidamente_1 = "", bebe_Avidamente_2 = "";
            //    if (bebe_Avidamente == "1") bebe_Avidamente_1 = "X";
            //    else if (bebe_Avidamente == "0") bebe_Avidamente_2 = "X";

            //    var sig_pliegue_piel = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIG_PLIEGUE_PIEL_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIG_PLIEGUE_PIEL_DIARREA).v_Value1;
            //    string sig_pliegue_piel_1 = "", sig_pliegue_piel_2 = "";
            //    if (sig_pliegue_piel == "1") sig_pliegue_piel_1 = "X";
            //    else if (sig_pliegue_piel == "0") sig_pliegue_piel_2 = "X";

            //    var sig_pliegue_piel_cutaneo = niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIGNO_PLIEGUE_CUTANEO_DIARREA) == null ? "" : niño.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_NIÑO_SIGNO_PLIEGUE_CUTANEO_DIARREA).v_Value1;
            //    string sig_pliegue_piel_cutaneo_1 = "", sig_pliegue_piel_cutaneo_2 = "";
            //    if (sig_pliegue_piel_cutaneo == "1") sig_pliegue_piel_cutaneo_1 = "X";
            //    else if (sig_pliegue_piel_cutaneo == "0") sig_pliegue_piel_cutaneo_2 = "X";
            //    cellsTit = new List<PdfPCell>()
            //    { 
            //        new PdfPCell(new Phrase("(NIÑO) - Descarte de signos de peligro:", fontTitle2)) { Colspan=18, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.ORANGE },
  
            //        new PdfPCell(new Phrase("MENOS DE 2 MESES:", fontTitle2)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F},
            //        new PdfPCell(new Phrase("NO", fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("DE 2 MESES A 4 AÑOS:", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F},
            //        new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F},
            //        new PdfPCell(new Phrase("NO", fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("PARA TODAS LAS EDADES:", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("NO", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        //////////////////////////////////////////////////////////////////////////////////////////////////////
            //        new PdfPCell(new Phrase("No quiere mamar, no succiona", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
            //        new PdfPCell(new Phrase(no_mama_2m_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(no_mama_2m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("No puede beber o tomar el pecho", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(no_toma_pecho_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(no_toma_pecho_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Emaciación visible grave", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(emaciaciacion_t_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(emaciaciacion_t_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        //////////////////////////////////////////////////////////////////////////////////////////////////////
            //        new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(convulsiones_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(convulsiones_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(convulsiones_4m_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(convulsiones_4m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Piel vuelve muy lentamente", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(piel_t_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(piel_t_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        //////////////////////////////////////////////////////////////////////////////////////////////////////
            //        new PdfPCell(new Phrase("Fontanela abombada", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(fontanela_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(fontanela_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Letárgico o comatoso", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(letar_comat_4m_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(letar_comat_4m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Traumatismo - Quemaduras", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(traumatismo_t_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(traumatismo_t_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        //////////////////////////////////////////////////////////////////////////////////////////////////////
            //        new PdfPCell(new Phrase("Enrojecimiento del ombligo se extiende a la piel", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(enroj_omb_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(enroj_omb_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Vomita todo", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(vomito_4m_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(vomito_4m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Envenenamiento", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(envenenamiento_t_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(envenenamiento_t_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        //////////////////////////////////////////////////////////////////////////////////////////////////////
            //        new PdfPCell(new Phrase("Fiebre o temperatura baja", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
            //        new PdfPCell(new Phrase(fieb_tem_ba_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(fieb_tem_ba_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Estridor en reposo - Tiraje subcostal", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(estridor_4m_1, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(estridor_4m_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Palidez palmar intenso", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(palidez_t_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(palidez_t_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        //////////////////////////////////////////////////////////////////////////////////////////////////////
            //        new PdfPCell(new Phrase("Rigidez de nuca", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(rigi_nunca_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(rigi_nunca_2, fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},

            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
            //        new PdfPCell(new Phrase("", fontColumnValue)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        //////////////////////////////////////////////////////////////////////////////////////////////////////
            //        new PdfPCell(new Phrase("Pústulas muchas y extensas", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(postulas_much_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(postulas_much_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("", fontColumnValue)) { Colspan=4,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},

            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Letárquico o comatoso", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(letar_comatoso_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(letar_comatoso_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("TOS O DIFICULTADES RESPIRATORIAS", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("NO", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("SINTOMAS DE DIARREA", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("SI", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("NO", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor = BaseColor.GRAY, MinimumHeight=15F },

            //        new PdfPCell(new Phrase("Respiración Rápida", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(respiracion_rapida_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(respiracion_rapida_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("HAY SANGRE EN LAS HECES", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(sangre_heces_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(sangre_heces_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

            //        new PdfPCell(new Phrase("Observar tiraje subcostal", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(observar_tiraje_subcostal_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(observar_tiraje_subcostal_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("Letargico o Comatoso", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(letargio_comatoso_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(letargio_comatoso_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

            //        new PdfPCell(new Phrase("Observar escuchar estridor", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(escuchar_estridor_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(escuchar_estridor_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("Intranquilo o Irritable", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(intranquilo_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(intranquilo_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

            //        new PdfPCell(new Phrase("Escuchar Sibilancias / Tos", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(escuchar_sibilancias_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(escuchar_sibilancias_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("Tiene los ojos hundidos", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(ojos_hundidos_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(ojos_hundidos_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

            //        new PdfPCell(new Phrase("Observar tiraje subcostal - Tos grave", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(observar_tiraje_subcostal_grave_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(observar_tiraje_subcostal_grave_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("Ofrecer líquido al niño", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(puede_beber_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(puede_beber_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

            //        new PdfPCell(new Phrase("Sibilancias 1ra vez", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(sibilancias_1ra_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(sibilancias_1ra_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("Bebe avidamente con sed", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(bebe_Avidamente_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(bebe_Avidamente_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

            //        new PdfPCell(new Phrase("Sibilancias recurrente", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(sibilancias_recurrente_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase(sibilancias_recurrente_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("SIG. Pliegue Piel", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(sig_pliegue_piel_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(sig_pliegue_piel_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},

            //        new PdfPCell(new Phrase("", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F },
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15F},
            //        new PdfPCell(new Phrase("Signo de pliegue cutáneo", fontTitle2)) {Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(sig_pliegue_piel_cutaneo_1, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //        new PdfPCell(new Phrase(sig_pliegue_piel_cutaneo_2, fontColumnValue)) {Colspan=1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15F},
            //    };

            //    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, };
            //    table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            //    document.Add(table);
            //}
            #endregion

            #region Categoria del Adulto Mayor
            //if (datosPac.Edad > 64)//Validar que sea adulto mayor
            //{
            //    // Adulto Mayor
            //    ServiceComponentList adulto_mayor = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_ID);

            //    var saludable = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SALUDABLE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SALUDABLE).v_Value1;
            //    string saludable_1 = "";
            //    if (saludable == "1") saludable_1 = "X";

            //    var fragil = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_FRAGIL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_FRAGIL).v_Value1;
            //    string fragil_1 = "";
            //    if (fragil == "1") fragil_1 = "X";

            //    var enfermo = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_ENFERMO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_ENFERMO).v_Value1;
            //    string enfermo_1 = "";
            //    if (enfermo == "1") enfermo_1 = "X";

            //    var geriatrico = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_GERIATRICO_COMPLEJO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_GERIATRICO_COMPLEJO).v_Value1;
            //    string geriatrico_1 = "";
            //    if (geriatrico == "1") geriatrico_1 = "X";

            //    var independiente = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DEPENDIENTE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DEPENDIENTE).v_Value1;
            //    string independiente_1 = "";
            //    if (independiente == "1") independiente_1 = "X";

            //    var dependiente = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_INDEPENDIENTE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_INDEPENDIENTE).v_Value1;
            //    string dependiente_1 = "";
            //    if (dependiente == "1") dependiente_1 = "X";

            //    var total = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_TOTAL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_TOTAL).v_Value1;

            //    var ec_normal = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_NORMAL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_NORMAL).v_Value1;
            //    string ec_normal_1 = "";
            //    if (ec_normal == "1") ec_normal_1 = "X";

            //    var ec_leve = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_LEVE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_LEVE).v_Value1;
            //    string ec_leve_1 = "";
            //    if (ec_leve == "1") ec_leve_1 = "X";

            //    var ec_moderado = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_MODERADO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_C_MODERADO).v_Value1;
            //    string ec_moderado_1 = "";
            //    if (ec_moderado == "1") ec_moderado_1 = "X";

            //    var severo = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SEVERO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SEVERO).v_Value1;
            //    string severo_1 = "";
            //    if (severo == "1") severo_1 = "X";

            //    var fecha_hoy = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_FECHA_DE_HOY) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_FECHA_DE_HOY).v_Value1;
            //    string fecha_hoy_1 = "";
            //    if (fecha_hoy == "1") fecha_hoy_1 = "X";

            //    var dia_semana = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DIA_DE_SEMANA) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DIA_DE_SEMANA).v_Value1;
            //    string dia_semana_1 = "";
            //    if (dia_semana == "1") dia_semana_1 = "X";

            //    var lugar_estamos = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_LUGAR_ESTAMOS) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_LUGAR_ESTAMOS).v_Value1;
            //    string lugar_estamos_1 = "";
            //    if (lugar_estamos == "1") lugar_estamos_1 = "X";

            //    var numero_telefono_direccion = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR__NUMERO_TELEFONO_DIRECCION) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR__NUMERO_TELEFONO_DIRECCION).v_Value1;
            //    string numero_telefono_direccion_1 = "";
            //    if (numero_telefono_direccion == "1") numero_telefono_direccion_1 = "X";

            //    var años_tiene = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_AÑOS_TIENE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_AÑOS_TIENE).v_Value1;
            //    string años_tiene_1 = "";
            //    if (años_tiene == "1") años_tiene_1 = "X";

            //    var donde_nacio = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DONDE_NACIO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DONDE_NACIO).v_Value1;
            //    string donde_nacio_1 = "";
            //    if (donde_nacio == "1") donde_nacio_1 = "X";

            //    var nombre_presidente = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_NOMBRE_PRESIDENTE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_NOMBRE_PRESIDENTE).v_Value1;
            //    string nombre_presidente_1 = "";
            //    if (nombre_presidente == "1") nombre_presidente_1 = "X";

            //    var nombre_past_president = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_NOMBRE_ANTERIOR_PRESIDENTE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_NOMBRE_ANTERIOR_PRESIDENTE).v_Value1;
            //    string nombre_past_president_1 = "";
            //    if (nombre_past_president == "1") nombre_past_president_1 = "X";

            //    var primer_apellido_madre = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PRIMER_APELLIDO_MADRE) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PRIMER_APELLIDO_MADRE).v_Value1;
            //    string primer_apellido_madre_1 = "";
            //    if (primer_apellido_madre == "1") primer_apellido_madre_1 = "X";

            //    var restar_3 = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_RESTAR_3) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_RESTAR_3).v_Value1;

            //    var ea_sin_md = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_A_SIN_MD) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_A_SIN_MD).v_Value1;
            //    string ea_sin_md_1 = "";
            //    if (ea_sin_md == "1") ea_sin_md_1 = "X";

            //    var ea_con_md = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_A_CON_MD) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_E_A_CON_MD).v_Value1;
            //    string ea_con_md_1 = "";
            //    if (ea_con_md == "1") ea_con_md_1 = "X";

            //    var buena = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_BUENA) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_BUENA).v_Value1;
            //    string buena_1 = "";
            //    if (buena == "1") buena_1 = "X";

            //    var riesgo_social = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_RIESGO_SOCIAL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_RIESGO_SOCIAL).v_Value1;
            //    string riesgo_social_1 = "";
            //    if (riesgo_social == "1") riesgo_social_1 = "X";

            //    var problema_social = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PROBLEMA_SOCIAL) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PROBLEMA_SOCIAL).v_Value1;
            //    string problema_social_1 = "";
            //    if (problema_social == "1") problema_social_1 = "X";

            //    var vida_satisfecho = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SATISFECHO_VIDA) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_SATISFECHO_VIDA).v_Value1;
            //    string vida_satisfecho_1 = "", vida_satisfecho_2 = "";
            //    if (vida_satisfecho == "1") vida_satisfecho_1 = "X";
            //    else if (vida_satisfecho == "0") vida_satisfecho_2 = "X";

            //    var impotente_indefenso = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_IMPOTENTE_INDEFENSO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_IMPOTENTE_INDEFENSO).v_Value1;
            //    string impotente_indefenso_1 = "", impotente_indefenso_2 = "";
            //    if (impotente_indefenso == "1") impotente_indefenso_1 = "X";
            //    else if (impotente_indefenso == "0") impotente_indefenso_2 = "X";

            //    var problemas_memoria = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PROBLEMAS_MEMORIA) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_PROBLEMAS_MEMORIA).v_Value1;
            //    string problemas_memoria_1 = "", problemas_memoria_2 = "";
            //    if (problemas_memoria == "1") problemas_memoria_1 = "X";
            //    else if (problemas_memoria == "0") problemas_memoria_2 = "X";

            //    var desgano_imposibilitado = adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DESGANO_IMPOSIBILITADO) == null ? "" : adulto_mayor.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ADULTO_MAYOR_DESGANO_IMPOSIBILITADO).v_Value1;
            //    string desgano_imposibilitado_1 = "", desgano_imposibilitado_2 = "";
            //    if (desgano_imposibilitado == "1") desgano_imposibilitado_1 = "X";
            //    else if (desgano_imposibilitado == "0") desgano_imposibilitado_2 = "X";


            //    cellsTit = new List<PdfPCell>()
            //    { 
            //        new PdfPCell(new Phrase("(A.M.)ESTADO DE ENFERMEDAD: ", fontTitle2)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Colspan = 20, BackgroundColor = BaseColor.ORANGE, MinimumHeight=15f },

            //        new PdfPCell(new Phrase("SALUDABLE", fontTitle2)) { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(saludable_1, fontTitle2)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("FRAGIL", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(fragil_1, fontTitle2)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("ENFERMO", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15f },
            //        new PdfPCell(new Phrase(enfermo_1, fontTitle2)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase("GERIATRICO COMPLEJO", fontTitle2)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(geriatrico_1, fontTitle2)) { Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },

            //        new PdfPCell(new Phrase("I. FUNCIONAL", fontColumnValueBold)) {Colspan=20, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },

            //        new PdfPCell(new Phrase("Independiente", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT , MinimumHeight=15f },
            //        new PdfPCell(new Phrase(independiente_1, fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase("Dependiente", fontColumnValue)) {Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_RIGHT , MinimumHeight=15f },
            //        new PdfPCell(new Phrase(dependiente_1, fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("Total", fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(total, fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
      
            //        new PdfPCell(new Phrase("II. Mental", fontColumnValueBold)) {Colspan=20,HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },

            //        new PdfPCell(new Phrase("2.1 Valoración Cognitiva (DC: Deterioro Cognitivo)", fontColumnValue)) {Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("Normal", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(ec_normal_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("DC Leve", fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(ec_leve_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("DC Moderado", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(ec_moderado_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase("DC Severo", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(severo_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },

            //        new PdfPCell(new Phrase("2.2 Estado Cognitivo", fontColumnValue)) {Colspan=5, Rowspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=45f },

            //        new PdfPCell(new Phrase("¿Cuál es la fecha de hoy?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(fecha_hoy_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("¿Qué día de la semana?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(dia_semana_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("¿En qué lugar estamos? (cualquier descripción correcta del lugar)", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(lugar_estamos_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    
            //        new PdfPCell(new Phrase("¿Cuál es su número de teléfono? ó ¿Cuál es su dirección completa?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(numero_telefono_direccion_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("¿Cuántos años tiene", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(años_tiene_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("¿Dónde nació?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(donde_nacio_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },

            //        new PdfPCell(new Phrase("¿Cuál es el nombre del presidente del Perú?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(nombre_presidente_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("¿Cuál es el nombre del anterior presidente del Perú?", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(nombre_past_president_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("Dígame el primer apellido de su madre", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(primer_apellido_madre_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
                    
            //        new PdfPCell(new Phrase("Restar de 3 en 3 desde 30 (cualquier error hace errónea la respuesta)", fontColumnValue)) {Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(restar_3, fontColumnValue)) {Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
                   
 
            //        new PdfPCell(new Phrase("2.3 Estado Afectivo", fontColumnValue)) {Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase("Sin manifestaciones depresivas", fontColumnValue)) {Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(ea_sin_md_1, fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("Con manifestaciones depresivas", fontTitle2)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(ea_con_md_1, fontColumnValue)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  },

            //        new PdfPCell(new Phrase("Preguntas", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("Sí", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("Preguntas", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase("Sí", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },


            //        new PdfPCell(new Phrase("¿Está satisfecho con su vida?", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(vida_satisfecho_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase(vida_satisfecho_2, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("¿Se siente impotente o indefenso?", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(impotente_indefenso_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase(impotente_indefenso_2, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },

            //        new PdfPCell(new Phrase("¿Tiene problemas de memoria?", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(problemas_memoria_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase(problemas_memoria_2, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("¿Siente desgano o se siente imposibilitado respecto a actividades e intereses?", fontColumnValue)) {Colspan=8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(desgano_imposibilitado_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },
            //        new PdfPCell(new Phrase(desgano_imposibilitado_2, fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , MinimumHeight=15f },

            //        new PdfPCell(new Phrase("III. SOCIO-FAMILIAR", fontColumnValueBold)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f  }, 
                    
            //        new PdfPCell(new Phrase("Buena", fontColumnValue)) {Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f },
            //        new PdfPCell(new Phrase(buena_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase("Riesgo social", fontColumnValue)) {Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT , MinimumHeight=15f },
            //        new PdfPCell(new Phrase(riesgo_social_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER , MinimumHeight=15f },
            //        new PdfPCell(new Phrase("Problema Social", fontTitle2)) {Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight=15f  },
            //        new PdfPCell(new Phrase(problema_social_1, fontColumnValue)) {Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, MinimumHeight=15f  },
            //    };

            //    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            //    document.Add(table);
            //}
            #endregion

            #region MOTIVO DE CONSULTA
            var signos_sintomas = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SIGNOS_SINTOMAS) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SIGNOS_SINTOMAS).v_Value1;
            var enfer_actual = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ENFERMEDAD_ACTUAL) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ENFERMEDAD_ACTUAL).v_Value1;
            var tiempoEnf = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_TIEMPO_EMF) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_TIEMPO_EMF).v_Value1;
            var apetito = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APETITO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APETITO).v_Value1;
            var sed = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SED) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SED).v_Value1;
            var sueño = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SUEÑO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SUEÑO).v_Value1;
            var estAnimo = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_EST_ANIMO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_EST_ANIMO).v_Value1;
            var orina = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ORINA) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ORINA).v_Value1;
            var depos = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_DEPOSICIONES) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_DEPOSICIONES).v_Value1;

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("MOTIVO DE CONSULTA", fontColumnValueBold1)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=15F },
                    
                    new PdfPCell(new Phrase("SIGNOS Y SÍNTOMAS :", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    new PdfPCell(new Phrase(signos_sintomas, fontColumnValue)) {Colspan=16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    new PdfPCell(new Phrase("ENFERMEDAD ACTUAL :", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    new PdfPCell(new Phrase(enfer_actual, fontColumnValue)) {Colspan=16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    new PdfPCell(new Phrase("Tiempo de enfermedad :", fontColumnValueBold)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
                    new PdfPCell(new Phrase(tiempoEnf, fontColumnValue)) {Colspan=16 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },

                    new PdfPCell(new Phrase("Apetito :", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(apetito, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase("Sed :", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(sed, fontColumnValue)) { Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },

                    new PdfPCell(new Phrase("Sueño :", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(sueño, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase("Estado de ánimo :", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},
                    new PdfPCell(new Phrase(estAnimo, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },

                    new PdfPCell(new Phrase("Orina :", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(orina, fontColumnValue)) {Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },
                    new PdfPCell(new Phrase("Deposiciones :", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
                    new PdfPCell(new Phrase(depos, fontColumnValue)) { Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F  },

                  
                };

            columnWidths = new float[] { 3f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 3f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region RELATO CRONOLÓGICO
            var relato_cronologico = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_RELATO_PATOLOGICO_DESC) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_RELATO_PATOLOGICO_DESC).v_Value1;

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("RELATO CRONOLÓGICO", fontColumnValueBold1)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=15f },
                    new PdfPCell(new Phrase(relato_cronologico==""?"-":relato_cronologico, fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=30f  },
                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ANTECEDENTES
            //if (datosPac.Edad <= 12) //verificar que sea niño
            //{
            //    #region ANTECEDENTES PATOLOGICOS Y FAMILIARES
            //    cellsTit = new List<PdfPCell>()
            //    { 
            //        new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValue)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=tamaño_celda },

            //        new PdfPCell(new Phrase("ANTECEDENTES PATOLÓGICOS", fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("ANTECEDENTES FAMILIARES", fontColumnValueBold)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
                
            //        new PdfPCell(new Phrase("ENFERMEDAD", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("NO", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("ENFERMEDAD", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("QUIEN", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("NO", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
                    
            //        new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 6,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TBC").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TBC").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TBC").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TBC").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 

            //        new PdfPCell(new Phrase("SOBA / Asma", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase("ASMA", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASMA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASMA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASMA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ASMA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //        new PdfPCell(new Phrase("Epilepsia", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //        new PdfPCell(new Phrase("Infecciones", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFECCIONES").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFECCIONES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFECCIONES").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFECCIONES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase("Diabetes", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //        new PdfPCell(new Phrase("Hospitalizaciones", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES ").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES ").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES ").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES ").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase("Epilepsia", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "APILEPSIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //        new PdfPCell(new Phrase("Transfusiones sanguíneas", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES SANG.").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES SANG.").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES SANG.").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSFUSIONES SANG.").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase("Alergia a medicinas", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICINAS").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICINAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICINAS").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICINAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

            //         new PdfPCell(new Phrase("Cirugia", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CIRUGIA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CIRUGIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CIRUGIA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CIRUGIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase("Violencia familiar", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA FAMILIAR").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER},

            //        new PdfPCell(new Phrase("Alergia a medicamentos", fontColumnValue)) { Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICAMENTOS").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICAMENTOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICAMENTOS").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES PATOLOGICOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIA MEDICAMENTOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("Alcoholismo", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOLISMO").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOLISMO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOLISMO").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOLISMO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //        new PdfPCell(new Phrase(datosNinio.v_AlergiasMedicamentos, fontColumnValue)) { Colspan = 10,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("Drogadicción", fontColumnValue)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGADICCION").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGADICCION").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGADICCION").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGADICCION").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //        new PdfPCell(new Phrase("Otros antecedentes", fontColumnValue)) { Colspan = 10, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //        new PdfPCell(new Phrase("Hepat. B", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase("-", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HEPATITIS B").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HEPATITIS B").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                   
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HEPATITIS B").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "ANTECEDENTES FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HEPATITIS B").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //        new PdfPCell(new Phrase("Especifique  :  " + datosNinio.v_OtrosAntecedentes, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
            //        new PdfPCell(new Phrase("*** Padre(P), Madre(M), Hno(H), Abuelo/a(A), Otro(O)", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //    };

            //    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            //    document.Add(table);
            //    #endregion
            //}
            //else if (13 <= datosPac.Edad && datosPac.Edad <= 17)
            //{
            //    #region ANTECEDENTES PERSONALES Y FAMILIARES

            //    cells = new List<PdfPCell>()
            //{   
            //    new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValue)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=tamaño_celda },
    
            //    new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { Colspan = 12,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},   
            //    new PdfPCell(new Phrase("Familiares", fontColumnValueBold)) { Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},  
                
            //    new PdfPCell(new Phrase("NORMALES", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_LEFT ,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("Criterio", fontColumnValueBold)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},   
            //    //new PdfPCell(new Phrase("no se", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("Criterio", fontColumnValueBold)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },   
            //    //new PdfPCell(new Phrase("no se", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("VIVE CON", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },


            //    new PdfPCell(new Phrase("Criterio", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("si", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },   
            //    //new PdfPCell(new Phrase("no se", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("no", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                 
            //    new PdfPCell(new Phrase("TBC", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },           
            //    new PdfPCell(new Phrase("MADRE", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },           

            //    new PdfPCell(new Phrase("PERINATALES", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PERINATALES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PERINATALES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PERINATALES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "PERINATALES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },           
            //    new PdfPCell(new Phrase("SOBA/ASMA", fontColumnValue)) { Colspan =4 ,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SOBA/ASMA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
            //    new PdfPCell(new Phrase("OBESIDAD", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OBESIDAD").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OBESIDAD").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OBESIDAD").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OBESIDAD").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("PADRE", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("CRECIMIENTO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CRECIMIENTO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CRECIMIENTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CRECIMIENTO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CRECIMIENTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },        
            //    new PdfPCell(new Phrase("TRANSF. SANGUINEAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //   new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSF. SANGUINEAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSF. SANGUINEAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSF. SANGUINEAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRANSF. SANGUINEAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("VIH/SIDA", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH/SIDA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("HERMANOS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HERMANOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HERMANOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HERMANOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HERMANOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                
            //    new PdfPCell(new Phrase("DESARROLLO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESARROLLO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("USO DE MEDICINAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE MEDICINAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE MEDICINAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE MEDICINAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "USO DE MEDICINAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("HIPERTENSION ARTERIAL", fontColumnValue)) { Colspan =4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERTENSION ARTERIAL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERTENSION ARTERIAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERTENSION ARTERIAL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERTENSION ARTERIAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("HIJOS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("-", fontColumnValue)) {BackgroundColor= BaseColor.GRAY ,Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("CONSUMO DE DROGAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("DIABETES", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DIABETES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("PAREJAS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PAREJAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PAREJAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PAREJAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "PAREJAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("VACUNAS", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("INTERVEN. QRIRURGICAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INTERV. QUIRURGICAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INTERV. QUIRURGICAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INTERV. QUIRURGICAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INTERV. QUIRURGICAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("HIPERLIPIDEMIA", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERLIPIDEMIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERLIPIDEMIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERLIPIDEMIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIPERLIPIDEMIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("OTROS", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "VIVE CON").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },


            //    new PdfPCell(new Phrase("Nombre", fontColumnValueBold)) {Colspan = 3, Rowspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment = PdfPCell.ALIGN_MIDDLE,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("DOSIS / FECHA", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("ALERGIAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALERGIAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("INFARTO", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFARTO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFARTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFARTO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "INFARTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("REFERENTE ADULTO: ", fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("1º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("2º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("3º", fontColumnValueBold)) {Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("ACCIDENTES", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("TRANSTORNO PSICOLOGICO", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNO PSICOLOGICO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNO PSICOLOGICO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNO PSICOLOGICO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNO PSICOLOGICO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(datosAdoles.v_ViveCon, fontColumnValue)) { Colspan = 5,HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("DT", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT1").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT2").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT3").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DT3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("TRANSTORNOS PSICOLOGICOS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNOS PSICOLOGICOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNOS PSICOLOGICOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNOS PSICOLOGICOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRASTORNOS PSICOLOGICOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("DROGAS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("INSTRUCCION", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT ,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("P", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("M", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT ,BackgroundColor= BaseColor.GRAY},

            //    new PdfPCell(new Phrase("S R", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR1").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR2").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR3").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "SR3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("HOSPITALIZACIONES", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HOSPITALIZACIONES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("VIOLENCIA FAMILIAR", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA INTRAFAMILIAR").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA INTRAFAMILIAR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA INTRAFAMILIAR").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIOLENCIA INTRAFAMILIAR").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("ANALFABETO", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE ANALFABETO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE ANALFABETO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ANALFABETA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ANALFABETA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("H B", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB1").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB2").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB3").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "HB3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("MADRE ADOLESCENTE", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ADOLESCENTE").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ADOLESCENTE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ADOLESCENTE").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE ADOLESCENTE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("PRIMARIA", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE PRIMARIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE PRIMARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE PRIMARIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE PRIMARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("F A", fontColumnValueBold)) {Colspan =3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA1").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA1").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA2").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA2").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA3").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "FA3").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("MALTRATO", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MALTRATO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MALTRATO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MALTRATO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "MALTRATO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("SECUNDARIA", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE SECUNDARIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE SECUNDARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE SECUNDARIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE SECUNDARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.BLACK,Colspan =6, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor=BaseColor.GRAY, Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor= BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {BackgroundColor= BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("OTROS", fontColumnValue)) { Colspan = 4,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //   new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "OTROS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("SUPERIOR", fontColumnValueBold)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //   new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE SUPERIOR").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "PADRE SUPERIOR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE SUPERIOR").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "GRADO INSTRUCCION").FirstOrDefault().Hijos.Where(x => x.Nombre == "MADRE SUPERIOR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //};

            //    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);

            //    #endregion

            //    #region ANTECEDENTES PSICOSOCIALES
            //    cells = new List<PdfPCell>()
            //{   
            //    new PdfPCell(new Phrase("ANTECEDENTES PSICOSOCIALES", fontColumnValueBold)) { Colspan = 27, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},   
    
            //    new PdfPCell(new Phrase("EDUCATIVOS", fontColumnValueBold)) { Colspan = 9,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},   
            //    new PdfPCell(new Phrase("LABORALES", fontColumnValueBold)) { Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},
            //    new PdfPCell(new Phrase("VIDA SOCIAL", fontColumnValueBold)) { Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},
            //    new PdfPCell(new Phrase("HABITOS", fontColumnValueBold)) { Colspan = 6,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},
                
            //    new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY}, 
            //    new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("PREGUNTAS", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor= BaseColor.GRAY},
            //    new PdfPCell(new Phrase("CRITERIO", fontColumnValueBold)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },
            //    new PdfPCell(new Phrase("NO", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor= BaseColor.GRAY },

            //    new PdfPCell(new Phrase("¿ESTUDIA?", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTUDIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTUDIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTUDIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTUDIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("¿TRABAJAS?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRABAJA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRABAJA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRABAJA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TRABAJA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("¿ERES ACEPTADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES ACEPTADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES ACEPTADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES ACEPTADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES ACEPTADO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("EJERCICIOS", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "EJERCICIOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "EJERCICIOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "EJERCICIOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "EJERCICIOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("¿DE ACUERDO A LA EDAD?", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DE ACUERDO A LA EDAD").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DE ACUERDO A LA EDAD").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DE ACUERDO A LA EDAD").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DE ACUERDO A LA EDAD").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("¿REMUNERADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REMUNERADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REMUNERADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REMUNERADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "REMUNERADO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("¿ERES IGNORADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES IGNORADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES IGNORADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES IGNORADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES IGNORADO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("TABACO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TABACO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TABACO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TABACO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "TABACO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("NIVEL", fontColumnValueBold)) {Colspan = 9, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("¿ESTABLE?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTABLE").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTABLE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTABLE").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ESTABLE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("¿ERES RECHAZADO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES RECHAZADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES RECHAZADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES RECHAZADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ES RECHAZADO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("ALCOHOL", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "ALCOHOL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("NO ESCOLARIZADO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "NO ESCOLARIZADO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "NO ESCOLARIZADO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase("PRIMARIA", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRIMARIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRIMARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("¿TIEMPO COMPLETO?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIEMPO COMPLETO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIEMPO COMPLETO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIEMPO COMPLETO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES LABORALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIEMPO COMPLETO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("¿TIENES AMIGOS?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE AMIGOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE AMIGOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE AMIGOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE AMIGOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("DROGAS", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DROGAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

            //    new PdfPCell(new Phrase("SECUNDARIA", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECUNDARIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SECUNDARIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },         
            //    new PdfPCell(new Phrase("SUPERIOR", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SUPERIOR").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "SUPERIOR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("EDAD INICIO DE TRABAJO", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(datosAdoles.v_EdadInicioTrabajo, fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase("¿TIENES PAREJA?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE PAREJA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE PAREJA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE PAREJA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "TIENE PAREJA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("CONDUCE VEHIC.", fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCE VEHICULO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCE VEHICULO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCE VEHICULO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES HABITOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONDUCE VEHICULO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //    new PdfPCell(new Phrase("BAJO RENDIMIENTO", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "BAJO RENDIMIENTO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "BAJO RENDIMIENTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "BAJO RENDIMIENTO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "BAJO RENDIMIENTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("TIPO DE TRABAJO: "+"\n" + datosAdoles.v_TipoTrabajo, fontColumnValue)) {Colspan = 6,Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("¿PRACTICAS DEPORTE?", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRACTICA DEPORTES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRACTICA DEPORTES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRACTICA DEPORTES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PRACTICA DEPORTES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("TELEVISION (Horas/día)", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(datosAdoles.v_NroHorasTv, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //    new PdfPCell(new Phrase("DESERCION", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESERCION").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESERCION").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESERCION").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "DESERCION").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("¿ORGANIZAC. JUVENILES?", fontColumnValue)) {Colspan = 4,Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ORGANIZAC. JUVENILES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ORGANIZAC. JUVENILES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ORGANIZAC. JUVENILES").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES VIDA SOCIAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ORGANIZAC. JUVENILES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, Rowspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("VIDEO JUEGOS (Horas/día)", fontColumnValue)) {Colspan = 4,Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(datosAdoles.v_NroHorasJuegos, fontColumnValue)) {Colspan = 2, Rowspan=2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //    new PdfPCell(new Phrase("REPITENCIA", fontColumnValue)) {Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REPITENCIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REPITENCIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //     new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REPITENCIA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PSICOSOCIALES EDUCATIVOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REPITENCIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //};

            //    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);
            //    #endregion

            //    #region SALUD SEXUAL Y REPRODUCTIVA / SALUD BUCAL
            //    cells = new List<PdfPCell>()
            //{   
            //    new PdfPCell(new Phrase("SALUD SEXUAL Y REPRODUCTIVA", fontColumnValueBold)) { Colspan = 11,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},   
            //    new PdfPCell(new Phrase("SALUD BUCAL", fontColumnValueBold)) { Colspan = 10,HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor = BaseColor.GRAY},
                
            //    new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("AÑOS", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY, Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY,Colspan = 7, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
            //    new PdfPCell(new Phrase("Labios", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LABIOS_SANOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LABIOS_SANOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
            //    new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LABIOS_SANOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LABIOS_SANOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 

            //    new PdfPCell(new Phrase("MENARQUIA / ESPERMARQUIA", fontColumnValue)) {Colspan = 3, Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(datosAdoles.v_MenarquiaEspermarquia, fontColumnValue)) { Colspan = 1,Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase("ABUSO SEXUAL", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABUSO SEXUAL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABUSO SEXUAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                   
            //    new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABUSO SEXUAL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABUSO SEXUAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("Carrillos", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARRILLOS_SANOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARRILLOS_SANOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
            //    new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARRILLOS_SANOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARRILLOS_SANOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 

            //    new PdfPCell(new Phrase("Paladar", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PALADAR_SANO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PALADAR_SANO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER }, 
            //    new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PALADAR_SANO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "PALADAR_SANO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },


            //    new PdfPCell(new Phrase("EMBARAZOS", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMBARAZOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMBARAZOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMBARAZOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "EMBARAZOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //    new PdfPCell(new Phrase("Encía", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENCÍA_SANO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENCÍA_SANO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENCÍA_SANO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENCÍA_SANO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Lengua", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("Sano", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LENGUA_SANO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LENGUA_SANO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Enfermo", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LENGUA_SANO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "LENGUA_SANO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("EDAD INICIO RELACION SEXUAL", fontColumnValue)) {Colspan = 3, Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(datosAdoles.v_EdadInicioRS, fontColumnValue)) { Colspan = 1,Rowspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase("HIJOS", fontColumnValue)) {Colspan = 3,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1,Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIJOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1,Rowspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE }, 
            //    new PdfPCell(new Phrase("Estado clìnico de higiene dental", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("Buena", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_BUENA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_BUENA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Regular", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_REGULAR").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_REGULAR").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Mala", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_MALA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "HIGIENE_DENTAL_MALA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                
            //    new PdfPCell(new Phrase("Caries Dental", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARIES_DENTAL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARIES_DENTAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARIES_DENTAL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "CARIES_DENTAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("ABORTOS", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABORTOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABORTOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) { Colspan = 1,Rowspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABORTOS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD SEXUAL Y REPRODUCTIVA").FirstOrDefault().Hijos.Where(x => x.Nombre == "ABORTOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Ugencia de Tratamiento", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "URGENCIA_DE_TRATAMIENTO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "URGENCIA_DE_TRATAMIENTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "URGENCIA_DE_TRATAMIENTO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SALUD BUCAL").FirstOrDefault().Hijos.Where(x => x.Nombre == "URGENCIA_DE_TRATAMIENTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("OBSERVACIONES", fontColumnValue)) {Colspan = 4, Rowspan = 4,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(datosAdoles.v_Observaciones, fontColumnValue)) {Colspan = 17, Rowspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f },

            //};

            //    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);
            //    #endregion
            //}
            //else if (18 <= datosPac.Edad && datosPac.Edad <= 64)
            //{
            //    #region ANTECEDENTES

            //    var AntecedentesPersonales = Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos;
            //    var AntecedentesFamiliares = Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos;

            //    cells = new List<PdfPCell>()
            //{          
            //    new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValue)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=tamaño_celda },
    
            //    new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor=BaseColor.GRAY},   
            //    new PdfPCell(new Phrase("si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor=BaseColor.GRAY },
            //    new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor=BaseColor.GRAY},   
            //    new PdfPCell(new Phrase("si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor=BaseColor.GRAY},
            //    new PdfPCell(new Phrase("Familiares", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER,BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("no", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER ,BackgroundColor=BaseColor.GRAY},

            //    new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Consumo de tabaco", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE TABACO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE TABACO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE TABACO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE TABACO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },    

            //    new PdfPCell(new Phrase("Enf. Transmisión Sexual", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENF. TRANSMISION SEXUAL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENF. TRANSMISION SEXUAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENF. TRANSMISION SEXUAL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "ENF. TRANSMISION SEXUAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Consumo de alcohol", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE ALCOHOL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE ALCOHOL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE ALCOHOL").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos.Where(x => x.Nombre == "CONSUMO DE ALCOHOL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },                   
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("VIH - SIDA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIH-SIDA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Consumo otras drogas", fontColumnValue)) 
            //    { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CONSUMO DE DROGAS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Hepatitis", fontColumnValue))
            //    { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Hepatitis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Transfusiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("DBM", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DBM").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DBM").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DBM").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DBM").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Diabetes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DIABETES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DIABETES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Hospitalización", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HOSPITALIZACION").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HOSPITALIZACION").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HOSPITALIZACION").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HOSPITALIZACION").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("HTA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HTA").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HTA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HTA").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HTA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("HTA", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HTA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HTA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HTA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HTA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Interv. Quirúrgica", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INTERV. QUIRURGICA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INTERV. QUIRURGICA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INTERV. QUIRURGICA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INTERV. QUIRURGICA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Infarto", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "INFARTO").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "INFARTO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "INFARTO").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "INFARTO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Obesidad / Sobrepeso", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "OBESIDAD/SOBREPESO").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "OBESIDAD/SOBREPESO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "OBESIDAD/SOBREPESO").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "OBESIDAD/SOBREPESO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Cáncer", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CANCER").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CANCER").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CANCER").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CANCER").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Cáncer", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "CANCER").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "CANCER").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "CANCER").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "CANCER").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Infarto cardiaco", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INFARTO CARDIACO").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INFARTO CARDIACO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INFARTO CARDIACO").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INFARTO CARDIACO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Cáncer de cervix / mama", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CANCER DE CERVIX / MAMA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CANCER DE CERVIX / MAMA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CANCER DE CERVIX / MAMA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CANCER DE CERVIX / MAMA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Depresión", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DEPRESION").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DEPRESION").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DEPRESION").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DEPRESION").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Dislipidemia (Colesterol)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISLIPIDEMIA (COLESTEROL)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISLIPIDEMIA (COLESTEROL)").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISLIPIDEMIA (COLESTEROL)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISLIPIDEMIA (COLESTEROL)").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Patología prostática", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "PATOLOGIA PROSTATICA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "PATOLOGIA PROSTATICA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "PATOLOGIA PROSTATICA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "PATOLOGIA PROSTATICA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Prob. Psiquiátricos", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "PROB. PSIQUIATRICOS").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "PROB. PSIQUIATRICOS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "PROB. PSIQUIATRICOS").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "PROB. PSIQUIATRICOS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Enf. Renal", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ENF. RENAL").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ENF. RENAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ENF. RENAL").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ENF. RENAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Discapacidad", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISCAPACIDAD").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISCAPACIDAD").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISCAPACIDAD").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISCAPACIDAD").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Otros", fontColumnValue)) {Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT },   

            //    new PdfPCell(new Phrase("Visuales (glaucoma)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VISUALES (GLAUCOMA)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VISUALES (GLAUCOMA)").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VISUALES (GLAUCOMA)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VISUALES (GLAUCOMA)").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Prob. Laborales", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "PROB. LABORALES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "PROB. LABORALES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "PROB. LABORALES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "PROB. LABORALES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(datosAdult.v_OtrosAntecedentes, fontColumnValue)) {Colspan=3, Rowspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },   

            //    new PdfPCell(new Phrase("Convulsiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CONVULSIONES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CONVULSIONES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CONVULSIONES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CONVULSIONES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Riesgo ocupacional", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "RIESGO OCUPACIONAL").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "RIESGO OCUPACIONAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "RIESGO OCUPACIONAL").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "RIESGO OCUPACIONAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
           
            //    new PdfPCell(new Phrase("Depresión", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DEPRESION").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DEPRESION").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DEPRESION").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DEPRESION").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Violencia Política", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "VIOLENCIA POLITICA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
        
            //    new PdfPCell(new Phrase("Esquizofrenia", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ESQUIZOFRENIA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ESQUIZOFRENIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ESQUIZOFRENIA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ESQUIZOFRENIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) {  Colspan=3,BackgroundColor=BaseColor.GRAY, HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    //new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    //new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    ////new PdfPCell(new Phrase(".........", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    //new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    //new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                
            //    new PdfPCell(new Phrase("Descripción de antecendentes y otros: ", fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },   
            //    new PdfPCell(new Phrase(datosAdult.v_DescripcionAntecedentes==null?"-":datosAdult.v_DescripcionAntecedentes, fontColumnValue)) { Colspan =6,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15f },   

            //};

            //    columnWidths = new float[] { 22f, 5f, 5f, 22f, 5f, 5f, 22f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);

            //    #endregion

            //    #region MEDICAMENTO

            //    cells = new List<PdfPCell>()
            //    {          
            //        new PdfPCell(new Phrase("Reacción Alérgica a Medicamentos", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},       
            //        new PdfPCell(new Phrase("Si", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REACCIÓN ALÉRGICA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REACCIÓN ALÉRGICA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //        new PdfPCell(new Phrase("No", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REACCIÓN ALÉRGICA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REACCIÓN ALÉRGICA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase(datosAdult.v_ReaccionAlergica, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

            //        new PdfPCell(new Phrase("Medicamentos de uso frecuente", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},       
            //        new PdfPCell(new Phrase("Si", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "MEDICAMENTO DE USO FRECUENTE").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "MEDICAMENTO DE USO FRECUENTE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                
            //        new PdfPCell(new Phrase("No", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "MEDICAMENTO DE USO FRECUENTE").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "MEDICAMENTO DE USO FRECUENTE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase("(dosis, tiempo de uso u otra observación):    " + datosAdult.v_MedicamentoFrecuente, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                    
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 6, Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor=BaseColor.GRAY, FixedHeight = 3f },
            //    };

            //    columnWidths = new float[] { 20f, 5f, 5f, 5f, 5f, 60f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);

            //    #endregion

            //    #region SEXUALIDAD - 1

            //    cells = new List<PdfPCell>()
            //{          
            //    new PdfPCell(new Phrase("Sexualidad", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
            //    new PdfPCell(new Phrase("Edad de inicio de Relación sexual", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //    new PdfPCell(new Phrase(datosAdult.v_InicioRS, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("Número de parejas sexuales últimos 3 meses", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //    new PdfPCell(new Phrase(datosAdult.v_NroPs, fontColumnValue)) { Colspan=4,  HorizontalAlignment = PdfPCell.ALIGN_CENTER },
      
            //    new PdfPCell(new Phrase("Hijos vivos:", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //    new PdfPCell(new Phrase(datosPac.i_NumberLivingChildren.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("RS con personas del mismo sexo: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    {HorizontalAlignment = PdfPCell.ALIGN_CENTER },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    {HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //};

            //    columnWidths = new float[] { 10f, 20f, 5f, 30f, 5f, 5f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);

            //    #endregion

            //    #region SEXUALIDAD - 2
            //    if (datosPac.Genero == "FEMENINO")
            //    {
            //        cells = new List<PdfPCell>()
            //    {          
            //    new PdfPCell(new Phrase("Menarquia:", fontColumnValueBold)) { Colspan= 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},       
            //    new PdfPCell(new Phrase("FECHA DE ÚLTIMA REGLA", fontColumnValue)) { Colspan= 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase(datosAdult.v_FechaUR, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("R/C", fontColumnValueBold)) {Colspan= 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },  
            //    new PdfPCell(new Phrase(datosAdult.v_RC, fontColumnValue)) { Colspan=8,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //    new PdfPCell(new Phrase("Flujo vaginal patológico:", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },       
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    {Colspan=1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan=1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    {Colspan=1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
 
            //    new PdfPCell(new Phrase(datosAdult.v_FlujoVaginal, fontColumnValue)) { Colspan=5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("Dismenorrea", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DISMENORREA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DISMENORREA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DISMENORREA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DISMENORREA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //    new PdfPCell(new Phrase("Embarazo", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },       
            //    new PdfPCell(new Phrase("Parto:", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},  
            //    new PdfPCell(new Phrase(datosAdult.v_Parto, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},  
            //    new PdfPCell(new Phrase("Prematuro:", fontColumnValue)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},
            //    new PdfPCell(new Phrase(datosAdult.v_Prematuro, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },  
            //    new PdfPCell(new Phrase("Aborto", fontColumnValue)) { Colspan=2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
            //    new PdfPCell(new Phrase(datosAdult.v_Aborto, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},  
            //    new PdfPCell(new Phrase(datosAdult.v_ObservacionesEmbarazo, fontColumnValue)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},
                
            //    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor=BaseColor.GRAY, FixedHeight = 3f },

            //    new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY,  Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
            //    new PdfPCell(new Phrase("N°", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("año", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("CPN", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("Complicación", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Parto", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Peso RN", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Puerpio", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Observaciones", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //};

            //        var embarazos = listEmbarazos.FindAll(p => p.v_PersonId == datosPac.v_PersonId);
            //        if (embarazos != null && embarazos.Count > 0)
            //        {
            //            var count = 1;
            //            foreach (var emb in embarazos)
            //            {
            //                cell = new PdfPCell(new Phrase("Gestación", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Anio, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Cpn, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Complicacion, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Parto, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_PesoRn, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Puerpio, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_ObservacionesGestacion, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                count += 1;

            //            }
            //            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //        }
            //        else
            //        {
            //            cells.Add(new PdfPCell(new Phrase("LISTA VACÍA", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda });
            //            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //        }
            //        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //        document.Add(table);
            //    }
            //    #endregion
            //}
            //else 
            //{
            //    #region ANTECEDENTES

            //    var AntecedentesPersonales = Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault() == null ? null : Antecedentes.Where(x => x.Nombre == "PERSONALES").FirstOrDefault().Hijos;
            //    var AntecedentesFamiliares = Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault() == null ? null : Antecedentes.Where(x => x.Nombre == "FAMILIARES").FirstOrDefault().Hijos;

            //    cells = new List<PdfPCell>()
            //{          
            //    new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValue)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=tamaño_celda },
    
            //    new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("No", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },
            //    new PdfPCell(new Phrase("Personales", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("No", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },
            //    new PdfPCell(new Phrase("Familiares", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },   
            //    new PdfPCell(new Phrase("No", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, BackgroundColor=BaseColor.GRAY },

            //    new PdfPCell(new Phrase("Hipertensión arterial", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HIPERTENSIÓN ARTERIAL").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HIPERTENSIÓN ARTERIAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HIPERTENSIÓN ARTERIAL").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HIPERTENSIÓN ARTERIAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Hepatitis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HEPATITIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Diabetes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DIABETES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DIABETES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Tuberculosis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TUBERCULOSIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Hipertensión arterial", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HIPERTENSIÓN ARTERIAL").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HIPERTENSIÓN ARTERIAL").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HIPERTENSIÓN ARTERIAL").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "HIPERTENSIÓN ARTERIAL").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Dislipidemia (Colesterol)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISLIPIDEMIA (COLESTEROL)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISLIPIDEMIA (COLESTEROL)").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISLIPIDEMIA (COLESTEROL)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "DISLIPIDEMIA (COLESTEROL)").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Hospitalizado el último año", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HOSPITALIZADO EL ULTIMO AÑO").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HOSPITALIZADO EL ULTIMO AÑO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HOSPITALIZADO EL ULTIMO AÑO").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "HOSPITALIZADO EL ULTIMO AÑO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Diabetes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DIABETES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DIABETES").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DIABETES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Osteoartritis", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "OSTEOARTRITIS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "OSTEOARTRITIS").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "OSTEOARTRITIS").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "OSTEOARTRITIS").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Transfusiones", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "TRANSFUSIONES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Infarto de miocardio", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "INFARTO DE MIOCARDIO").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "INFARTO DE MIOCARDIO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "INFARTO DE MIOCARDIO").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "INFARTO DE MIOCARDIO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("acv (derrame cerebral)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ACV (DERRAME CEREBRAL)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ACV (DERRAME CEREBRAL)").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ACV (DERRAME CEREBRAL)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ACV (DERRAME CEREBRAL)").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Intervención quirúrgica", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INTERVENCIÓN QUIRÚRGICA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INTERVENCIÓN QUIRÚRGICA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INTERVENCIÓN QUIRÚRGICA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "INTERVENCIÓN QUIRÚRGICA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Demencia", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DEMENCIA").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DEMENCIA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DEMENCIA").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "DEMENCIA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Enfermedad cardiovascular (Infarto, arritmia, icc)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ENFERMEDAD CARDIOVASCULAR (INFARTO, ARRITMIA, ICC)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ENFERMEDAD CARDIOVASCULAR (INFARTO, ARRITMIA, ICC)").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ENFERMEDAD CARDIOVASCULAR (INFARTO, ARRITMIA, ICC)").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ENFERMEDAD CARDIOVASCULAR (INFARTO, ARRITMIA, ICC)").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Accidentes", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "ACCIDENTES").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Cáncer (Mama, estómago, colon)", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "CÁNCER (MAMA, ESTÓMAGO, COLON)").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "CÁNCER (MAMA, ESTÓMAGO, COLON)").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesFamiliares == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "CÁNCER (MAMA, ESTÓMAGO, COLON)").FirstOrDefault() == null ? "" : AntecedentesFamiliares.Where(x => x.Nombre == "CÁNCER (MAMA, ESTÓMAGO, COLON)").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },

            //    new PdfPCell(new Phrase("Cáncer", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CÁNCER").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CÁNCER").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CÁNCER").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "CÁNCER").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("Cáncer de cervix / mama", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? CÁNCER DE CERVIX / MAMA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? CÁNCER DE CERVIX / MAMA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? CÁNCER DE CERVIX / MAMA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? CÁNCER DE CERVIX / MAMA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("Cáncer de próstata", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? CÁNCER DE PRÓSTATA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? CÁNCER DE PRÓSTATA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },   
            //    new PdfPCell(new Phrase(AntecedentesPersonales == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? CÁNCER DE PRÓSTATA").FirstOrDefault() == null ? "" : AntecedentesPersonales.Where(x => x.Nombre == "? CÁNCER DE PRÓSTATA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },   
            //    new PdfPCell(new Phrase("", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },

            //    new PdfPCell(new Phrase("Descripción de antecendentes y otros: ", fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},   
            //    new PdfPCell(new Phrase(datosAdultMay.v_DescripciónAntecedentes, fontColumnValue)) { Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},   

            //};

            //    columnWidths = new float[] { 22f, 5f, 5f, 22f, 5f, 5f, 22f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);

            //    #endregion

            //    #region MEDICAMENTO

            //    cells = new List<PdfPCell>()
            //    {          
            //        new PdfPCell(new Phrase("Reacción Alérgica a Medicamentos", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},       
            //        new PdfPCell(new Phrase("Si", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REACCIÓN ALÉRGICA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REACCIÓN ALÉRGICA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //        new PdfPCell(new Phrase("No", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REACCIÓN ALÉRGICA").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "REACCIÓN ALÉRGICA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase(datosAdultMay.v_ReaccionAlergica, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},

            //        new PdfPCell(new Phrase("Medicamentos de uso frecuente", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},       
            //        new PdfPCell(new Phrase("Si", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "MEDICAMENTO DE USO FRECUENTE").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "MEDICAMENTO DE USO FRECUENTE").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},                
            //        new PdfPCell(new Phrase("No", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //        new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault() == null ? null:
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "MEDICAMENTO DE USO FRECUENTE").FirstOrDefault() == null ? "" : 
            //        Antecedentes.Where(x => x.Nombre == "MEDICAMENTOS").FirstOrDefault().Hijos.Where(x => x.Nombre == "MEDICAMENTO DE USO FRECUENTE").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //        {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //        new PdfPCell(new Phrase("(dosis, tiempo de uso u otra observación):    " + datosAdultMay.v_MedicamentoFrecuente, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
                    
            //        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 6, Rowspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor=BaseColor.GRAY, FixedHeight = 3f },
            //    };

            //    columnWidths = new float[] { 20f, 5f, 5f, 5f, 5f, 60f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);

            //    #endregion

            //    #region SEXUALIDAD - 1

            //    cells = new List<PdfPCell>()
            //{          
            //    new PdfPCell(new Phrase("Sexualidad", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
            //    new PdfPCell(new Phrase("Edad de inicio de Relación sexual", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //    new PdfPCell(new Phrase(datosAdultMay.v_InicioRS, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("Número de parejas sexuales últimos 3 meses", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //    new PdfPCell(new Phrase(datosAdultMay.v_NroPs, fontColumnValue)) { Colspan=4,  HorizontalAlignment = PdfPCell.ALIGN_CENTER },
      
            //    new PdfPCell(new Phrase("Hijos vivos:", fontColumnValue)) { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //    new PdfPCell(new Phrase(datosPac.i_NumberLivingChildren.ToString(), fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("RS con personas del mismo sexo: ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT },  
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    {HorizontalAlignment = PdfPCell.ALIGN_CENTER },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    {HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //};

            //    columnWidths = new float[] { 10f, 20f, 5f, 30f, 5f, 5f, 5f, 5f };
            //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //    document.Add(table);

            //    #endregion

            //    #region SEXUALIDAD - 2
            //    if (datosPac.Genero == "FEMENINO")
            //    {
            //        cells = new List<PdfPCell>()
            //    {          
            //    new PdfPCell(new Phrase("Menarquia:", fontColumnValueBold)) { Colspan= 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},       
            //    new PdfPCell(new Phrase("FECHA DE ÚLTIMA REGLA", fontColumnValue)) { Colspan= 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase(datosAdultMay.v_FechaUR, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("R/C", fontColumnValueBold)) {Colspan= 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },  
            //    new PdfPCell(new Phrase(datosAdultMay.v_RC, fontColumnValue)) { Colspan=8,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //    new PdfPCell(new Phrase("Flujo vaginal patológico:", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },       
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    {Colspan=1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) {Colspan=1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "RS CON PERSONAS DEL MISMO SEXO").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    {Colspan=1,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
 
            //    new PdfPCell(new Phrase(datosAdultMay.v_FlujoVaginal, fontColumnValue)) { Colspan=5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("Dismenorrea", fontColumnValue)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase("Si", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DISMENORREA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DISMENORREA").FirstOrDefault().SI ? "X" : "", fontColumnValue)) 
            //    {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },                
            //    new PdfPCell(new Phrase("No", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
            //    new PdfPCell(new Phrase(Antecedentes == null ? null : Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault() == null ? null:
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DISMENORREA").FirstOrDefault() == null ? "" : 
            //    Antecedentes.Where(x => x.Nombre == "SEXUALIDAD").FirstOrDefault().Hijos.Where(x => x.Nombre == "DISMENORREA").FirstOrDefault().NO ? "X" : "", fontColumnValue)) 
            //    {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE },

            //    new PdfPCell(new Phrase("Embarazo", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },       
            //    new PdfPCell(new Phrase("Parto:", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},  
            //    new PdfPCell(new Phrase(datosAdultMay.v_Parto, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},  
            //    new PdfPCell(new Phrase("Prematuro:", fontColumnValue)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},
            //    new PdfPCell(new Phrase(datosAdultMay.v_Prematuro, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },  
            //    new PdfPCell(new Phrase("Aborto", fontColumnValue)) { Colspan=2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F },
            //    new PdfPCell(new Phrase(datosAdultMay.v_Aborto, fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},  
            //    new PdfPCell(new Phrase(datosAdultMay.v_ObservacionesEmbarazo, fontColumnValue)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=15F},
                
            //    new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_LEFT, BackgroundColor=BaseColor.GRAY, FixedHeight = 3f },

            //    new PdfPCell(new Phrase("", fontColumnValueBold)) {BackgroundColor=BaseColor.GRAY,  Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_LEFT },       
            //    new PdfPCell(new Phrase("N°", fontColumnValue)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("año", fontColumnValue)) { Colspan = 1,HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("CPN", fontColumnValue)) { Colspan = 3,HorizontalAlignment = PdfPCell.ALIGN_CENTER },  
            //    new PdfPCell(new Phrase("Complicación", fontColumnValue)) { Colspan = 2,HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Parto", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Peso RN", fontColumnValue)) {Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Puerpio", fontColumnValue)) {Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //    new PdfPCell(new Phrase("Observaciones", fontColumnValue)) {Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            //};

            //        var embarazos = listEmbarazos.FindAll(p => p.v_PersonId == datosPac.v_PersonId);
            //        if (embarazos != null && embarazos.Count > 0)
            //        {
            //            var count = 1;
            //            foreach (var emb in embarazos)
            //            {
            //                cell = new PdfPCell(new Phrase("Gestación", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Anio, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Cpn, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Complicacion, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Parto, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_PesoRn, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_Puerpio, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                cell = new PdfPCell(new Phrase(emb.v_ObservacionesGestacion, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
            //                cells.Add(cell);

            //                count += 1;

            //            }
            //            //cell = new PdfPCell(new Phrase("OBSERVACIONES: ", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2 };
            //            //cells.Add(cell);
            //            //cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2 };
            //            //cells.Add(cell);
            //            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //        }
            //        else
            //        {
            //            cells.Add(new PdfPCell(new Phrase("LISTA VACÍA", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda });
            //            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //        }
            //        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            //        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //        document.Add(table);
            //    }
            //    #endregion
            //}
            #endregion

            #region ANTECEDENTES
            #region ANTECEDENTES
            var antecedentes = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ANTECEDENTES) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ANTECEDENTES).v_Value1;

            cellsTit = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValueBold1)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=tamaño_celda },

                new PdfPCell(new Phrase(antecedentes, fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=35f },
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EXAMEN FÍSICO

            var piel_faneras= atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_PIEL_FANERAS_TEJIDO_SUBCUTANEO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_PIEL_FANERAS_TEJIDO_SUBCUTANEO).v_Value1;
            var aparato_respiratorio = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APARATO_RESPIRATORIO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APARATO_RESPIRATORIO).v_Value1;
            var aparato_cardiovascular = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APARATO_CARDIOVASCULAR) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APARATO_CARDIOVASCULAR).v_Value1;
            var abdomen = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ABDOMEN) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ABDOMEN).v_Value1;
            var aparato_genitourinario = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APARATO_GENITOURINARIO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_APARATO_GENITOURINARIO).v_Value1;
            var sistema_nervioso = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SISTEMA_NERVIOSO) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_SISTEMA_NERVIOSO).v_Value1;
            var osteomuscular = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_OSTEMUSCULAR) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_OSTEMUSCULAR).v_Value1;

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("EXAMEN FISICO", fontColumnValueBold1)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=15F },
                    
                    new PdfPCell(new Phrase("PIEL, FANERAS Y TEJIDO CELULAR SUBCUTÁNEO: ", fontColumnValueBold)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    new PdfPCell(new Phrase(piel_faneras, fontColumnValue)) {Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    
                    new PdfPCell(new Phrase("APARATO RESPIRATORIO: ", fontColumnValueBold)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    new PdfPCell(new Phrase(aparato_respiratorio, fontColumnValue)) {Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    
                    new PdfPCell(new Phrase("APARATO CARDIOVASCULAR: ", fontColumnValueBold)) {Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
                    new PdfPCell(new Phrase(aparato_cardiovascular, fontColumnValue)) {Colspan=15 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    
                    new PdfPCell(new Phrase("ABDOMEN: ", fontColumnValueBold)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    new PdfPCell(new Phrase(abdomen, fontColumnValue)) {Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    
                    new PdfPCell(new Phrase("APARATO GENITOURINARIO: ", fontColumnValueBold)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    new PdfPCell(new Phrase(aparato_genitourinario, fontColumnValue)) {Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    
                    new PdfPCell(new Phrase("SISTEMA NERVIOSO: ", fontColumnValueBold)) {Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
                    new PdfPCell(new Phrase(sistema_nervioso, fontColumnValue)) {Colspan=15 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
                    
                    new PdfPCell(new Phrase("OSTEOMUSCULAR: ", fontColumnValueBold)) {Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda },
                    new PdfPCell(new Phrase(osteomuscular, fontColumnValue)) {Colspan=15 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda  },
            
                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
    
            #endregion

            #region EXAMENES AUXILIARES
            var a = atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_RELATO_PATOLOGICO_DESC) == null ? "" : atenInte.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_RELATO_PATOLOGICO_DESC).v_Value1;

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("EXAMENES AUXILIARES", fontColumnValueBold1)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=15F },
                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            string[] excludeComponents = { Sigesoft.Common.Constants.ANTROPOMETRIA_ID,
                                                 Sigesoft.Common.Constants.FUNCIONES_VITALES_ID,
                                                 Sigesoft.Common.Constants.EXAMEN_FISICO_ID,
                                                 "N005-ME000000117",
                                                 "N005-ME000000116",
                                                 "N005-ME000000046"

                                             };

            //int[] excludeCategoryTypeExam = {  (int)Sigesoft.Common.CategoryTypeExam.Laboratorio,
            //                                       (int)Sigesoft.Common.CategoryTypeExam.Psicologia,
                                                
            //                                    };
            ////// &&
            //                                               !excludeCategoryTypeExam.Contains(p.i_CategoryId.Value)

            var otherExams = ExamenesServicio.FindAll(p => !excludeComponents.Contains(p.v_ComponentId));

            // Utilizado Solo para mostrar titulo <OTROS>
            cells = new List<PdfPCell>()
            {

            };

            columnWidths = new float[] { 100f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            // Otros Examenes

            foreach (var oe in otherExams)
            {
                table = TableBuilderReportFor312(oe, fontTitleTable, fontSubTitleNegroNegrita, fontColumnValue, subTitleBackGroundColor);

                if (table != null)
                    document.Add(table);
            }

            #endregion

            #region DIAGNOSTICOS
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("DIAGNOSTICOS", fontColumnValueBold1)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                    new PdfPCell(new Phrase("CIE 10", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                    new PdfPCell(new Phrase("ESPECIFICACIONES", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       

                };
            columnWidths = new float[] { 20.6f, 40.6f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            cells = new List<PdfPCell>();

            var filterDiagnosticRepository = Diagnosticos.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);

            if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
            {
                columnWidths = new float[] { 0.7f, 23.6f };
                include = "i_Item,Valor1";

                foreach (var item in filterDiagnosticRepository)
                {
                    if (item.v_DiseasesId == "N009-DD000000029")
                    {
                        cell = new PdfPCell(new Phrase("")) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }

                    ListaComun oListaComun = null;
                    List<ListaComun> Listacomun = new List<ListaComun>();

                    if (item.Recomendations.Count > 0)
                    {
                        oListaComun = new ListaComun();
                        oListaComun.Valor1 = "RECOMENDACIONES";
                        oListaComun.i_Item = "#";
                        Listacomun.Add(oListaComun);
                    }


                    int Contador = 1;
                    foreach (var Reco in item.Recomendations)
                    {
                        oListaComun = new ListaComun();

                        oListaComun.Valor1 = Reco.v_RecommendationName;
                        oListaComun.i_Item = Contador.ToString();
                        Listacomun.Add(oListaComun);
                        Contador++;
                    }

                    if (item.Restrictions.Count > 0)
                    {
                        oListaComun = new ListaComun();
                        oListaComun.Valor1 = "RESTRICCIONES";
                        oListaComun.i_Item = "#";
                        Listacomun.Add(oListaComun);

                    }
                    int Contador1 = 1;
                    foreach (var Rest in item.Restrictions)
                    {
                        oListaComun = new ListaComun();
                        oListaComun.Valor1 = Rest.v_RestrictionName;
                        oListaComun.i_Item = Contador1.ToString();
                        Listacomun.Add(oListaComun);
                        Contador1++;
                    }

                    // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                    table = HandlingItextSharp.GenerateTableFromList(Listacomun, columnWidths, include, fontColumnValue);
                    cell = new PdfPCell(table);

                    cells.Add(cell);
                }

                columnWidths = new float[] { 20.6f, 40.6f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("", fontColumnValue)));
                columnWidths = new float[] { 100 };
            }

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null);
            document.Add(table);
            #endregion

            #region PERFIL TERAPEUTICO
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("PLAN TERAPEUTICO", fontColumnValueBold1)) {Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor=BaseColor.GRAY, MinimumHeight=15F },
                };

            columnWidths = new float[] { 100F };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            cells2 = new List<PdfPCell>();
            if (medicina != null && medicina.Count > 0)
            {
                var count = 1;
                foreach (var item in medicina)
                {
                    cell = new PdfPCell(new Phrase(count.ToString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
                    cells2.Add(cell);

                    cell = new PdfPCell(new Phrase(item.Medicamento, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
                    cells2.Add(cell);

                    cell = new PdfPCell(new Phrase(item.CantidadRecetada.ToString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
                    cells2.Add(cell);

                    cell = new PdfPCell(new Phrase(item.Dosis, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
                    cells2.Add(cell);

                    cell = new PdfPCell(new Phrase(item.Duracion, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
                    cells2.Add(cell);

                    cell = new PdfPCell(new Phrase(item.FechaFin.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda };
                    cells2.Add(cell);

                    count += 1;
                }
                cell = new PdfPCell(new Phrase(null, fontColumnValue)) { Colspan = 5, BackgroundColor = BaseColor.GRAY, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 2 };
                cells2.Add(cell);
                columnWidths = new float[] { 5F, 25f, 10f, 30f, 20f, 10f };
            }
            else
            {
                cells2.Add(new PdfPCell(new Phrase("NO SE HAN  REGISTRADO PLAN TERAPEUTICO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda });
                columnWidths = new float[] { 100f };
            }
            columnHeaders = new string[] { "N°", "MEDICAMENTO", "CANT", "DOSIS Y TIEMPO", "DURACIÓN TTO", "FECHA FIN DEL TTO" };
            columnWidths = new float[] { 5F, 25f, 10f, 30f, 20f, 10f };
            table = HandlingItextSharp.GenerateTableFromCells(cells2, columnWidths, null, fontColumnValueBold, columnHeaders);
            document.Add(table);
            #endregion

            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls
            PdfPCell cellFirmaTrabajador = null;
            PdfPCell cellHuellaTrabajador = null;
            PdfPCell cellFirma = null;

            // Firma del trabajador ***************************************************

            if (datosPac.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(datosPac.FirmaTrabajador, null, null, 80, 40));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaTrabajador.FixedHeight = 50F;
            // Huella del trabajador **************************************************

            if (datosPac.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(datosPac.HuellaTrabajador, null, null, 30, 45));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellHuellaTrabajador.FixedHeight = 50F;
            // Firma del doctor Auditor **************************************************
            if (DatosGrabo != null)
            {
                if (DatosGrabo.Firma != null)
                    cellFirma = new PdfPCell(HandlingItextSharp.GetImage(DatosGrabo.Firma, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            }
            else
                cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirma.FixedHeight = 50F;
            #endregion

            cells = new List<PdfPCell>()
            {
                new PdfPCell(cellFirmaTrabajador){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(cellHuellaTrabajador){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("FIRMA Y SELLO DEL MÉDICO", fontColumnValueBold)) {Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(cellFirma){Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
 
                new PdfPCell(new Phrase("FIRMA DEL EXAMINADO", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},    
                new PdfPCell(new Phrase("HUELLA DEL EXAMINADO", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},    

                new PdfPCell(new Phrase("¡GRACIAS POR ELEGIR CLINICA SAN LORENZO!", fontColumnValueBold)) {Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    

            };
            columnWidths = new float[] { 25f, 25f, 25f, 25f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
        }
        private static PdfPTable TableBuilderReportFor312(ServiceComponentList serviceComponent, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor)
        {
            PdfPTable table = null;
            List<PdfPCell> cells = null;
            PdfPCell cell = null;
            float[] columnWidths = null;

            switch (serviceComponent.v_ComponentId)
            {

                case Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID:

                    #region ELECTROCARDIOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 1,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_ID:

                    #region EVALUACION_ERGONOMICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_CONCLUSION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID:

                    #region ALTURA_ESTRUCTURAL

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_ID:

                    #region ALTURA_GEOGRAFICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.OSTEO_MUSCULAR_ID_1:

                    #region OSTEO_MUSCULAR

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DESCRIPCION);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID:

                    #region PRUEBA_ESFUERZO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_ID:

                    #region TAMIZAJE_DERMATOLOGICO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.ODONTOGRAMA_ID:

                    #region ODONTOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.GINECOLOGIA_ID:

                    #region EVALUACION_GINECOLOGICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "No se han registrado datos." : descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EXAMEN_MAMA_ID:

                    #region EXAMEN_MAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_MAMA_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "No se han registrado datos." : descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.AUDIOMETRIA_ID:

                    #region AUIDIOMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.ESPIROMETRIA_ID:

                    #region ESPIROMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.OFTALMOLOGIA_ID:

                    #region OFTALMOLOGIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.INMUNIZACIONES_ID:

                    #region INMUNIZACIONES

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INMUNIZACIONES_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case "N002-ME000000033":

                    #region PSICOLOGIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N002-ME000000033");
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.OIT_ID:

                    #region RX OIT 

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OIT_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.RX_TORAX_ID:

                    #region RX TORAX

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_TORAX_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.LUMBOSACRA_ID:

                    #region RX LUMBAR

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LUMBOSACRA_ID);
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion.v_Value1) ? "No se han registrado datos." : "Conclusiones: " + conclusion.v_Value1, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case "N001-ME000000000":

                    #region INFORME LABORATORIO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N001-ME000000000");
                        var hallazgos = serviceComponent.DiagnosticRepository;
                        var join = string.Join(",", hallazgos.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "HALLAZGOS: -----" : "HALLAZGOS: " + join, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                default:
                    break;
            }
            
            return table;

        }
    }
}
