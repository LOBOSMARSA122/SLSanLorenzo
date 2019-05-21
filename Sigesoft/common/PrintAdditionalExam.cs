using iTextSharp.text;
using iTextSharp.text.pdf;
using NetPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft
{
    public class PrintAdditionalExam
    {
        public void GenerateAdditionalexam(string filePDF, organizationDto MedicalCenter, PacientList DatosPaciente, UsuarioGrabo usuarioGraba, string MotivoComentario, List<Categoria> DataSource, List<AdditionalExamCustom> ListAdditional)
        {
            Document document = new Document(PageSize.A5, 20f, 20f, 20f, 20f);

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
            Font fontTitle2_ = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            #endregion


            #region CABECERA
            cells = new List<PdfPCell>();
            var pdfCell = new PdfPCell();

            if (MedicalCenter.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(MedicalCenter.b_Image));
                imagenEmpresa.ScalePercent(25);
                
                //imagenEmpresa.SetAbsolutePosition(40, 790);
                //document.Add(imagenEmpresa);
                pdfCell = new PdfPCell(imagenEmpresa) {Colspan = 1, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders=true, BorderColor= BaseColor.WHITE};
            }
            else
            {
                pdfCell = new PdfPCell(new Phrase("SIN LOGOTIPO", fontTitle2_)) { Colspan = 1, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE };
            }

            var names = new List<PdfPCell>()
            { 
                
                pdfCell,
                new PdfPCell(new Phrase(MedicalCenter.v_Name, fontTitle2_)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders=true, BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase(MedicalCenter.v_Address, fontTitle2_)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders=true, BorderColor= BaseColor.WHITE},
            };

            columnWidths = new float[] { 30f, 70f };
            table = HandlingItextSharp.GenerateTableFromCells(names, columnWidths, null, fontTitleTable);
            document.Add(table);

            var cellsTit = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("\nORDEN DE EXÁMENES MÉDICOS \nADICIONALES", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f,BorderColor= BaseColor.WHITE},
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            string PacientFullName = "", Edad = "", FechaNac = "", TipoDoc = "", DocNumber = "", Direccion = "", Cargo = "", nHistoria = "---", Celular = "";

            if (DatosPaciente != null)
	        {
                PacientFullName = DatosPaciente.v_FirstLastName + " " + DatosPaciente.v_SecondLastName + " " + DatosPaciente.v_FirstName;
                Edad = DatosPaciente.Edad.ToString();
                FechaNac = DatosPaciente.d_Birthdate.Value.ToShortDateString();
                DocNumber = DatosPaciente.v_DocNumber == null ? "---" : DatosPaciente.v_DocNumber;
                Direccion = string.IsNullOrEmpty(DatosPaciente.v_AdressLocation) ? "---" : DatosPaciente.v_AdressLocation;
                Cargo = string.IsNullOrEmpty(DatosPaciente.v_CurrentOccupation) ? "---" : DatosPaciente.v_CurrentOccupation;
                Celular = string.IsNullOrEmpty(DatosPaciente.v_TelephoneNumber) ? "---" : DatosPaciente.v_TelephoneNumber;
                if (DatosPaciente.i_DocTypeId == 1) { TipoDoc = "DNI"; }
                else if (DatosPaciente.i_DocTypeId == 2) { TipoDoc = "Pasaporte"; }
                else if (DatosPaciente.i_DocTypeId == 3) { TipoDoc = "Licencia de Conducir"; }
                else if (DatosPaciente.i_DocTypeId == 4) { TipoDoc = "Carnet de Extranjeria"; }
            }

            //Datos del paciente
            cells = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("NOMBRES:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase(PacientFullName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase("EDAD:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase(Edad, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase("F. NAC:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase(FechaNac, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},

                new PdfPCell(new Phrase(TipoDoc + ":", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase(DocNumber, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase("DIRECCIÓN:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase(Direccion, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                
                new PdfPCell(new Phrase("CARGO:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase(Cargo, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase("N° HISTORIA:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase("CSL-" + DocNumber, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase("CELULAR:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                new PdfPCell(new Phrase(Celular, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},

            };
            columnWidths = new float[] { 12f, 27f, 15f, 23f, 12f, 11f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


            #endregion

            #region CUERPO
            //resumen

            var pdfCellComentario = new PdfPCell();
            if (MotivoComentario.Length > 0)
            {
                pdfCellComentario = new PdfPCell(new Phrase(MotivoComentario, fontColumnValue))
                {
                    HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,
                    VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f,
                    BorderColor = BaseColor.WHITE
                };
            }
            else
            {
                pdfCellComentario = new PdfPCell(new Phrase("........................................................................................................................................................................ \n........................................................................................................................................................................", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f,BorderColor= BaseColor.WHITE};
            }
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("RESUMEN", fontTitleTableNegro)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                pdfCellComentario,               
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


            //exámenes
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("EXÁMENES", fontTitleTableNegro)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            foreach (var category in DataSource)
            {
                //exámenes
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("* ", fontTitleTable)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                    new PdfPCell(new Phrase(category.v_CategoryName, fontTitleTable)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                };
                columnWidths = new float[] { 10f, 90f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);

                foreach (var component in category.Componentes)
                {
                    string estado = "";
                    var obj = ListAdditional.Find(x => x.ComponentId == component.v_ComponentId);
                    if (obj != null)
                    {

                        if (obj.IsProcessed == 1)
                        {
                            estado = " ( AGENDADO)";
                        }
                        else
                        {
                            estado = " ( POR AGENDAR )";
                        }
                    }
                    
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("- ", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                        new PdfPCell(new Phrase(component.v_ComponentName + estado, fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.WHITE},
                    };
                    columnWidths = new float[] { 15f, 85f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                }

            }

            


            //datos medicos
            var cellFirma = new PdfPCell();
            string NombreMedico = "---", CMP = "---";
            if (usuarioGraba != null)
            {
                
                NombreMedico = usuarioGraba.Nombre == null ? "---" : usuarioGraba.Nombre;
                CMP = usuarioGraba == null ? "---" : usuarioGraba.CMP;

                if (usuarioGraba.Firma != null)
                {
                    cellFirma = new PdfPCell(HandlingItextSharp.GetImage(usuarioGraba.Firma, null, null, 110, 40)) { Colspan = 1, Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, UseVariableBorders = true, BorderColor = BaseColor.BLACK };
                }
                
            }
            else
            {
                cellFirma = new PdfPCell(new Phrase("FIRMA", fontTitle2_)) { Colspan = 1, Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK };
            }


            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("DATOS MÉDICOS", fontTitle2)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.BLACK},
                cellFirma,                
                new PdfPCell(new Phrase("NOMBRE:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.BLACK},
                new PdfPCell(new Phrase(NombreMedico, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.BLACK},             
                
                new PdfPCell(new Phrase("CMP:", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.BLACK},
                new PdfPCell(new Phrase(CMP, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f,BorderColor= BaseColor.BLACK},
            };
            columnWidths = new float[] { 15f, 35f, 50f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion


            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
