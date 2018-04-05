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
    public class InformeMedicoTrabajadorInternacional
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }


        public static void CreateInformeMedicoTrabajadorInternacional(string filePDF, PacientList pCabecera, List<NoxiousHabitsList> listaHabitoNocivos, List<FamilyMedicalAntecedentsList> pAnteFami, List<Sigesoft.Node.WinClient.BE.ServiceComponentList> ServicesComponents, List<DiagnosticRepositoryList> Diagnosticos, List<Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList> AntePersonales, List<Sigesoft.Node.WinClient.BE.HistoryList> AnteOcupacionales, organizationDto infoEmpresaPropietaria, string pstrRecomendaciones)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //try
            //{NO_BORDER
            // step 2: we create aPA writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
            page.Dato = "FMT_CI/" + pCabecera.Trabajador;
            //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
            writer.PageEvent = page;

            // step 3: we open the document
            document.Open();
            // step 4: we Add content to the document
            // we define some fonts
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
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
           
            Font fontColumnValue = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            //Font fontTitleTableNegro = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion

            #region Title

            PdfPCell CellLogo = null;
            cells = new List<PdfPCell>();
            PdfPCell cellPhoto1 = null;

            if (pCabecera.b_Photo != null)
                cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(pCabecera.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
            else
                cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            columnWidths = new float[] { 100f };

            var cellsTit = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("INFORME MÉDICO", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER }               
            };

            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(table));
            cells.Add(cellPhoto1);

            columnWidths = new float[] { 20f, 60f, 20f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region Cabecera

            string ApaGenitoUrinario = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).v_Value1;

            string Piel = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" :ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID)==null?"": ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).v_Value1;
            string Linfatico = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" :ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID)== null?"": ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID).v_Value1;
            string Ojos = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID)== null?"":ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).v_Value1;
            string Oidos = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" :ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID)== null?"": ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).v_Value1;
            string Nariz = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" :ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID)== null?"": ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).v_Value1;
            string Boca = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID) == null?"": ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).v_Value1;
            string Cuello = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID)== null?"": ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).v_Value1;
            string Torax = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" :ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID)== null?"": ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).v_Value1;
            string Cardio = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID)== null?"":ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).v_Value1;
            string Abdomen = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID)== null?"":ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).v_Value1;
            string Genitourinario = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID)==null?"":ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).v_Value1;
            string Neurologico = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" :ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID)== null?"": ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).v_Value1;

            var dxArticuOsteo = Diagnosticos.FindAll(p => p.v_ComponentId == "N005-ME000000046");
            string ArticuOsteo = string.Join(", ", dxArticuOsteo.Select(p => p.v_DiseasesName));


            string OtrasOsteo = ServicesComponents.Find(p => p.v_ComponentId == "N005-ME000000046") == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == "N005-ME000000046").ServiceComponentFields.Find(o => o.v_ComponentFieldsId == "N005-MF000001980") == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == "N005-ME000000046").ServiceComponentFields.Find(o => o.v_ComponentFieldsId == "N005-MF000001980").v_Value1;

            string Anamnesis = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).v_Value1;
            string Ectoscopia = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_HALLAZGOS_ID) == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID).ServiceComponentFields.Find(o => o.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_HALLAZGOS_ID).v_Value1;

            var dxExamenFisico = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID);
            var dxExamenFisicoConcatenado = string.Join(", ", dxExamenFisico.Select(p => p.v_DiseasesName));

            var dxHemograma = Diagnosticos.FindAll(p => p.v_ComponentId == "N009-ME000000045");
            var dxHemogramaConcatenado = string.Join(", ", dxHemograma.Select(p => p.v_DiseasesName)) == "" ? "Hemograma sin alteración" : string.Join(", ", dxHemograma.Select(p => p.v_DiseasesName));

            var dxOrina = Diagnosticos.FindAll(p => p.v_ComponentId == "N009-ME000000046");
            var dxOrinaConcatenado = string.Join(", ", dxOrina.Select(p => p.v_DiseasesName)) == "" ? "Orina sin alteración" : string.Join(", ", dxOrina.Select(p => p.v_DiseasesName));


            int? sex = pCabecera.i_SexTypeId;

            if (sex == (int?)Sigesoft.Common.Gender.FEMENINO)
            {
                ApaGenitoUrinario = string.Format("MENARQUIA: {0} ," +
                                                   "FUM: {1} ," +
                                                   "RÉGIMEN CATAMENIAL: {2} ," +
                                                   "GESTACIÓN Y PARIDAD: {3} ," +
                                                   "MAC: {4} ," +
                                                   "CIRUGÍA GINECOLÓGICA: {5}", string.IsNullOrEmpty(pCabecera.v_Menarquia) ? "NO REFIERE" : pCabecera.v_Menarquia,
                                                                                pCabecera.d_Fur == null ? "NO REFIERE" : pCabecera.d_Fur.Value.ToShortDateString(),
                                                                                string.IsNullOrEmpty(pCabecera.v_CatemenialRegime) ? "NO REFIERE" : pCabecera.v_CatemenialRegime,
                                                                                pCabecera.v_Gestapara,
                                                                                pCabecera.v_Mac,
                                                                                string.IsNullOrEmpty(pCabecera.v_CiruGine) ? "NO REFIERE" : pCabecera.v_CiruGine);

            }

            //int? FrecAlcohol = pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 2)[0].i_FrequencyId;
            //int? FrecDrogas = pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 3)[0].i_FrequencyId;
            //string Alcohol = FrecAlcohol == 1 ? "Nunca " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 2)[0].v_NoxiousHabitsName : FrecAlcohol == 2 ? "Poco " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 2)[0].v_NoxiousHabitsName : FrecAlcohol == 3 ? "Habitual " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 2)[0].v_NoxiousHabitsName : FrecAlcohol == 4 ? "Frecuente " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 2)[0].v_NoxiousHabitsName : "";
            //string Drogas = FrecDrogas == 1 ? "Nunca " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 3)[0].v_NoxiousHabitsName : FrecDrogas == 2 ? "Poco " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 3)[0].v_NoxiousHabitsName : FrecDrogas == 3 ? "Habitual " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 3)[0].v_NoxiousHabitsName : FrecDrogas == 4 ? "Frecuente " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 3)[0].v_NoxiousHabitsName : "";


            //int? FrecTabaco = pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 1)[0].i_FrequencyId;
            //int? FrecFisico = pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 4)[0].i_FrequencyId;
            //string Tabaco = FrecTabaco == 4 ? "Nada " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 1)[0].v_NoxiousHabitsName : FrecTabaco == 6 ? "Consumo de Tabaco " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 1)[0].v_NoxiousHabitsName : FrecTabaco == 7 ? "Tabaquismo " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 1)[0].v_NoxiousHabitsName : "";
            //string Fisico = FrecFisico == 1 ? "Menos de 3 veces a la semana " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 4)[0].v_NoxiousHabitsName : FrecFisico == 2 ? "3 veces a la semana a más " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 4)[0].v_NoxiousHabitsName : FrecFisico == 3 ? "No realiza actividad física " + pHabitosNoscivos.FindAll(p => p.i_TypeHabitsId == 4)[0].v_NoxiousHabitsName : "";
            List<NoxiousHabitsList> Alcohol = null;
            List<NoxiousHabitsList> Tabaco = null;
            List<NoxiousHabitsList> Drogas = null;
            List<NoxiousHabitsList> Fisica = null;
            if (listaHabitoNocivos != null)
            {
                Alcohol = listaHabitoNocivos.FindAll(p => p.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.Alcohol);
                Tabaco = listaHabitoNocivos.FindAll(p => p.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.Tabaco);
                Drogas = listaHabitoNocivos.FindAll(p => p.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.Drogas);
                Fisica = listaHabitoNocivos.FindAll(p => p.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.ActividadFisica);
            }     
            cells = new List<PdfPCell>()
              {
                new PdfPCell(new Phrase("NOMBRE:", fontColumnValue)),                                  
                new PdfPCell(new Phrase(pCabecera.Trabajador, fontColumnValue)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},      
                   
                new PdfPCell(new Phrase("EDAD:", fontColumnValue)),                                   
                new PdfPCell(new Phrase(pCabecera.Edad+" años", fontColumnValue)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},    

                new PdfPCell(new Phrase("SEXO:", fontColumnValue)),                                   
                new PdfPCell(new Phrase(pCabecera.Genero, fontColumnValue)){  HorizontalAlignment = PdfPCell.ALIGN_LEFT},    



                new PdfPCell(new Phrase("DNI:", fontColumnValue)),                                   
                new PdfPCell(new Phrase(pCabecera.v_DocNumber, fontColumnValue)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},    

                new PdfPCell(new Phrase("TELÉFONO:", fontColumnValue)),                                   
                new PdfPCell(new Phrase(pCabecera.v_TelephoneNumber, fontColumnValue)){ Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},    



                new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)),                                   
                new PdfPCell(new Phrase(pCabecera.Empresa, fontColumnValue)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},    
                new PdfPCell(new Phrase("SEDE:", fontColumnValue)),                                   
                new PdfPCell(new Phrase(pCabecera.Sede, fontColumnValue)){ Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
 


                   
                new PdfPCell(new Phrase("M. EVALUADOR:", fontColumnValue)),                                    
                new PdfPCell(new Phrase(pCabecera.MedicoGrabaMedicina, fontColumnValue)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                new PdfPCell(new Phrase("OCUPACIÓN:", fontColumnValue)),                                  
                new PdfPCell(new Phrase(pCabecera.v_CurrentOccupation, fontColumnValue)){ Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                new PdfPCell(new Phrase("FECHA:", fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(pCabecera.FechaServicio.Value.ToString("MMMM dd, yyyy"), fontColumnValue)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT},  



                //new PdfPCell(new Phrase("INFORME MÉDICO", fontTitle1)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},    

                //Línea blanca
                new PdfPCell(new Phrase(" ", fontTitle1)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},    

                new PdfPCell(new Phrase("A continuación le informamos los resultados de su evaluación médica:", fontColumnValue)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT},   

                  //Línea blanca
                new PdfPCell(new Phrase(" ", fontTitle1)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},    

                new PdfPCell(new Phrase("Antecedentes Peronales:", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                new PdfPCell(new Phrase("Actividad física:", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(  Fisica ==  null || Fisica.Count == 0 ? string.Empty :Fisica[0].v_Frequency, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
               
                new PdfPCell(new Phrase("Consumo de Tabaco:", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Tabaco ==  null || Tabaco.Count == 0 ? string.Empty :Tabaco[0].v_Frequency, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                new PdfPCell(new Phrase("Consumo de Licor:", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Alcohol ==  null || Alcohol.Count == 0 ? string.Empty :Alcohol[0].v_Frequency, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Consumo de Drogas:", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Drogas ==  null || Drogas.Count == 0 ? string.Empty :Drogas[0].v_Frequency, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                
                new PdfPCell(new Phrase("Antecedentes Ginecológicos: ", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(ApaGenitoUrinario, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                new PdfPCell(new Phrase("Antecedentes Patológicos: ", fontTitleTable)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                };
            columnWidths = new float[] { 13f, 8f, 10f, 10f, 12f, 10f, 10f, 10f, 7f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);


            cells = new List<PdfPCell>()
             {
                   new PdfPCell(new Phrase("ENFERMEDAD", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                   new PdfPCell(new Phrase("FECHA INICIO", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                   new PdfPCell(new Phrase("DETALLE", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER},
             };
            foreach (var item in AntePersonales)
            {
                cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_LEFT };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(item.d_StartDate.Value.ToString("MMMM dd, yyyy"), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_LEFT };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(item.v_DiagnosticDetail, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_LEFT };
                cells.Add(cell);
            }
            columnWidths = new float[] { 20f, 20f, 20f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

           
            cells = new List<PdfPCell>()
              {
                new PdfPCell(new Phrase("Sintomatología Actual: ", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Anamnesis, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                   };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);


            #region Antecedentes Patológicos Familiares


            cells = new List<PdfPCell>()
              {              
                new PdfPCell(new Phrase("Antecedentes Familiares: ", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                   };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            if (pAnteFami.Count == 0)
            {
                pAnteFami.Add(new FamilyMedicalAntecedentsList { v_FullAntecedentName = "No Refiere Antecedentes Patológicos Familiares." });
                columnWidths = new float[] { 100f };
                include = "v_FullAntecedentName";
            }
            else
            {
                columnWidths = new float[] { 0.7f, 23.6f };
                include = "i_Item,v_FullAntecedentName";
            }

            var pathologicalFamilyHistory = HandlingItextSharp.GenerateTableFromList(pAnteFami, columnWidths, include, fontColumnValue,null, fontTitleTableNegro);

            document.Add(pathologicalFamilyHistory);

            #endregion



            cells = new List<PdfPCell>()
              {              
                new PdfPCell(new Phrase("Antecedentes Ocupacionales: ", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                   };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            #region Antecedentes Ocupacionales

            cells = new List<PdfPCell>();

            if (AnteOcupacionales != null && AnteOcupacionales.Count > 0)
            {
                columnWidths = new float[] { 5f, 7f };

                foreach (var item in AnteOcupacionales)
                {
                    //Columna EMPRESA
                    cell = new PdfPCell(new Phrase(item.v_Organization, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna ÁREA
                    cell = new PdfPCell(new Phrase(item.v_TypeActivity, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna OCUPACIÓN
                    cell = new PdfPCell(new Phrase(item.i_Trabajo_Actual == 1 ? item.v_workstation + " Puesto Actual " : item.v_workstation, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Crear Tabla en duro FECHA
                    List<PdfPCell> cells1 = new List<PdfPCell>();
                    cells1.Add(new PdfPCell(new Phrase("FECHA INICIO: ", fontColumnValueAntecedentesOcupacionales)));
                    cells1.Add(new PdfPCell(new Phrase(item.d_StartDate.Value.ToString("MM/yyyy"), fontColumnValueAntecedentesOcupacionales)));

                    cells1.Add(new PdfPCell(new Phrase("FECHA FIN: ", fontColumnValueAntecedentesOcupacionales)));
                    cells1.Add(new PdfPCell(new Phrase(item.d_EndDate.Value.ToString("MM/yyyy"), fontColumnValueAntecedentesOcupacionales)));

                    table = HandlingItextSharp.GenerateTableFromCells(cells1, columnWidths, "", fontTitleTable);

                    cell = new PdfPCell(table);
                    cells.Add(cell);

                    //Columna EXPOSICIÓN
                    cell = new PdfPCell(new Phrase(item.Exposicion == "" ? "NO REFIERE PELIGROS EN EL PUESTO" : item.Exposicion, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna TIPO OPERACIÓN
                    cell = new PdfPCell(new Phrase(item.TiempoLabor, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna EPPS
                    cell = new PdfPCell(new Phrase(item.Epps == "" ? "NO USÓ EPP" : item.Epps, fontColumnValueAntecedentesOcupacionales)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                }

                columnWidths = new float[] { 15f, 15f, 15f, 15f, 15f, 15f, 15f };

            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 15f, 15f, 15f, 15f, 15f, 15f, 15f };
            }

            columnHeaders = new string[] { "EMPRESA", "ÁREA", "OCUPACIÓN", "FECHA", "EXPOSICIÓN", "TIEMPO DE TRABAJO", "EPP" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableAntecedentesOcupacionales, columnHeaders);

            document.Add(table);

            #endregion





            cells = new List<PdfPCell>()
              {
                new PdfPCell(new Phrase("Examen Clínico: ", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                               
                new PdfPCell(new Phrase(Ectoscopia, fontColumnValue)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                //Línea blanca
                new PdfPCell(new Phrase(" ", fontTitle1)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},    

               

               
              };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            #region Examen Físico
            //Examen Físico
            if (true)
            {
                cells = new List<PdfPCell>(){
                new PdfPCell(new Phrase("Examen Físico: ", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                               
                new PdfPCell(new Phrase("Piel y Anexos: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Piel, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                new PdfPCell(new Phrase("Sistema Linfático: ", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Linfatico, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                new PdfPCell(new Phrase("Ojos: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Ojos, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                new PdfPCell(new Phrase("Oidos: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Oidos, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                new PdfPCell(new Phrase("Nariz: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Nariz, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Boca: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Boca, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Cuello: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Cuello, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Tórax y Pulmones: ", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Torax, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Cardiovascular: ", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Cardio, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Abdomen: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Abdomen, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Ap Genitourinario: ", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Genitourinario, fontColumnValue)){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                new PdfPCell(new Phrase("Neurológico: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(Neurologico, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
               
                new PdfPCell(new Phrase("Examen Físico: ", fontColumnValue)){ Colspan=1, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                new PdfPCell(new Phrase(dxExamenFisicoConcatenado == "" ?"EXAMEN FÍSICO SIN ALTERACIÓN":dxExamenFisicoConcatenado, fontColumnValue)){ Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                 //Línea blanca
                new PdfPCell(new Phrase(" ", fontTitle1)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},    

                };

                columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }
            #endregion

            #region Examen OsteoMuscular
            //if (true)
            //{
            cells = new List<PdfPCell>(){
                new PdfPCell(new Phrase("Articulaciones Osteomusculares: ", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                new PdfPCell(new Phrase(ArticuOsteo, fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                new PdfPCell(new Phrase("otras descripciones Osteomusculares: ", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                new PdfPCell(new Phrase(OtrasOsteo, fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                         
                };
            //}
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);



            #endregion

            #region Examenes Auxiliares

            //Grupo Sanguineo
            if (ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) != null)
            {
                string Grupo = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID).ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID)==null?"":ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID).ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID).v_Value1Name;
                string Factor = ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID).ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID)==null?"":ServicesComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID).ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID).v_Value1Name;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Grupo sanguineo: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Grupo + " " +Factor , fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //Hemograma
            if (true)
            {
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Hemograma: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(dxHemogramaConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
            }
            columnWidths = new float[] { 20f, 80f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);


            //Hemoglobina
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000006") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000006").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000265")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000006").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000265").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000006").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000265")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000006").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000265").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Hemoglobina: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //glucosa
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000008") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000008").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000261")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000008").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000261").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000008").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000261")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000008").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000261").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Glucosa: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Colesterol Total
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000016") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000016").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001086")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000016").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001086").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000016").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001086")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000016").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001086").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Colesterol Total: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //HDL
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000074") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000074").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000254")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000074").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000254").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000074").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000254")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000074").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000254").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("HDL: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //LDL
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000075") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000075").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001073")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000075").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001073").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000075").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001073")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000075").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001073").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("LDL: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //VLDL
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000076") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000076").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001075")== null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000076").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001075").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000076").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001075")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000076").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001075").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("VLDL: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //TGC
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000017") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000017").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001296")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000017").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001296").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000017").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001296")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000017").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001296").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("TGC: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //Lípidos Totales
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000129") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000129").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002142")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000129").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002142").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000129").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002142")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000129").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002142").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Lípidos Totales: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Ácido Úrico
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000086") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000086").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001425")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000086").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001425").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000086").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001425")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000086").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001425").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Ácido Úrico: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Úrea
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000073") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000073").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000253")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000073").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000253").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000073").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000253")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000073").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000253").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Úrea: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Creatina
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000028") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000028").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000518")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000028").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000518").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000028").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000518")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000028").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000518").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Creatina: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //TGO
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000054") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000054").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000706")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000054").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000706").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000054").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000706")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000054").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000706").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("TGO: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //TGP
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000055") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000055").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000707")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000055").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000707").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000055").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000707")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000055").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000707").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("TGP: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //GGTP
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000164") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000164").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001804")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000164").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001804").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000164").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001804")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000164").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001804").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("GGTP: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //VDRL
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000003") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000003").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000269")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000003").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000269").v_Value1Name;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000003").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000269")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000003").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000269").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("VDRL: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //PSA
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000009") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000009").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000443")==null?"": ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000009").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000443").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000009").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000443")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000009").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000443").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("PSA: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Plomo  en orina
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000139") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000139").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002162")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000139").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002162").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000139").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002162")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000139").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002162").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Plomo en Orina: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Plomo  en Sangre
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000060") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000060").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001158")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000060").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001158").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000060").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001158")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000060").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001158").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Plomo en Sangre: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //Marihuana
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294") == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294") == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Marihuana: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //Cocaína
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000705")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000705").v_Value1Name;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000705")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000053").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000705").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Cocaína: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Metanfetamina
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000128") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000128").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000128").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294").v_Value1Name;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000128").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000128").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001294").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Metanfetamina: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Anfetamina
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000043") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000043").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000391")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000043").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000391").v_Value1Name;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000043").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000391")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000043").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000391").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Anfetamina: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //Benodiacepinas
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000040") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000040").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000395")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000040").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000395").v_Value1Name;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000040").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000395")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000040").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000000395").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Benodiacepinas: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Extasis
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000127") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000127").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002136")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000127").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002136").v_Value1Name;
                string Uni =ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000127").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002136")==null?"": ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000127").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002136").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Extasis: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Orina
            if (true)
            {
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Orina: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(dxOrinaConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
            }
            columnWidths = new float[] { 20f, 80f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            //Parasitológico
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000010") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000010").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001339") == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000010").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001339").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000010").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001339") == null ? "" : ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000010").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001339").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Parasitológico: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //Thevenon en heces
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000158") != null)
            {
                string Valor =  ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000158").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002202")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000158").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002202").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000158").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002202")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000158").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002202").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Thevenon en heces: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Coprocultivo
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000159") != null)
            {
                string Valor = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000159").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002205")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000159").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002205").v_Value1;
                string Uni = ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000159").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002205")==null?"":ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000159").ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000002205").v_MeasurementUnitName;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Coprocultivo: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(Valor + " " + Uni, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            ////RPR
            //if (true)
            //{
            //    cells = new List<PdfPCell>()
            //    {
            //        new PdfPCell(new Phrase("RPR: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //        new PdfPCell(new Phrase(" ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //    };
            //}
            //columnWidths = new float[] { 20f,80f };

            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            //document.Add(filiationWorker);

            //Audiometría
            if (ServicesComponents.Find(p => p.v_ComponentId == "N005-ME000000005") != null)
            {
                var Dx = Diagnosticos.FindAll(p => p.v_ComponentId == "N005-ME000000005");
                var DxConcatenado = string.Join(", ", Dx.Select(p => p.v_DiseasesName));
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Audiometría: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DxConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Espirometría
            if (ServicesComponents.Find(p => p.v_ComponentId == "N002-ME000000031") != null)
            {
                var Dx = Diagnosticos.FindAll(p => p.v_ComponentId == "N002-ME000000031");
                var DxConcatenado = string.Join(", ", Dx.Select(p => p.v_DiseasesName));
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Espirometría: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DxConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //EKG
            if (ServicesComponents.Find(p => p.v_ComponentId == "N002-ME000000025") != null)
            {
                var Dx = Diagnosticos.FindAll(p => p.v_ComponentId == "N002-ME000000025");
                var DxConcatenado = string.Join(", ", Dx.Select(p => p.v_DiseasesName));
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("EKG: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DxConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Rx Torax
            if (ServicesComponents.Find(p => p.v_ComponentId == "N002-ME000000032") != null)
            {
                var Dx = Diagnosticos.FindAll(p => p.v_ComponentId == "N002-ME000000032");
                var DxConcatenado = string.Join(", ", Dx.Select(p => p.v_DiseasesName));
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Rx Torax: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DxConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }



            //Rx Torax OIT
            if (ServicesComponents.Find(p => p.v_ComponentId == "N009-ME000000062") != null)
            {
                var Dx = Diagnosticos.FindAll(p => p.v_ComponentId == "N002-ME000000062");
                var DxConcatenado = string.Join(", ", Dx.Select(p => p.v_DiseasesName));
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Rx Torax OIT: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DxConcatenado == "" ? "SIN NEUMOCONIOSIS":DxConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            ////Rx Lumbar
            //if (true)
            //{
            //    cells = new List<PdfPCell>()
            //    {
            //        new PdfPCell(new Phrase("Rx Lumbar: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //        new PdfPCell(new Phrase(" ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //    };
            //}
            //columnWidths = new float[] { 20f,80f };

            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            //document.Add(filiationWorker);


            ////Prueba de Esfuerzo
            //if (true)
            //{
            //    cells = new List<PdfPCell>()
            //    {
            //        new PdfPCell(new Phrase("Prueba de Esfuerzo: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //        new PdfPCell(new Phrase(" ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //    };
            //}
            //columnWidths = new float[] { 20f,80f };

            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            //document.Add(filiationWorker);

            ////Ecografía Abdominal
            //if (true)
            //{
            //    cells = new List<PdfPCell>()
            //    {
            //        new PdfPCell(new Phrase("Ecografía Abdominal: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //        new PdfPCell(new Phrase(" ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //    };
            //}
            //columnWidths = new float[] { 20f,80f };

            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            //document.Add(filiationWorker);

            //Examen Oftalmológico
            if (ServicesComponents.Find(p => p.v_ComponentId == "N005-ME000000028") != null)
            {
                var Dx = Diagnosticos.FindAll(p => p.v_ComponentId == "N005-ME000000028");
                var DxConcatenado = string.Join(", ", Dx.Select(p => p.v_DiseasesName));
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Examen Oftalmológico: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DxConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            //Examen Odontológico
            if (ServicesComponents.Find(p => p.v_ComponentId == "N005-ME000000027") != null)
            {
                var Dx = Diagnosticos.FindAll(p => p.v_ComponentId == "N005-ME000000027");
                var DxConcatenado = string.Join(", ", Dx.Select(p => p.v_DiseasesName));
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Examen Odontológico: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(DxConcatenado, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                };
                columnWidths = new float[] { 20f, 80f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                document.Add(filiationWorker);
            }


            ////Evaluación Cardiológica
            //if (true)
            //{
            //    cells = new List<PdfPCell>()
            //    {
            //        new PdfPCell(new Phrase("Evaluación Cardiológica: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //        new PdfPCell(new Phrase(" ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //    };
            //}
            //columnWidths = new float[] { 20f,80f };

            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            //document.Add(filiationWorker);

            ////Evaluación Espirometría
            //if (true)
            //{
            //    cells = new List<PdfPCell>()
            //    {
            //        new PdfPCell(new Phrase("Evaluación Espirometría: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //        new PdfPCell(new Phrase(" ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
            //    };
            //}
            //columnWidths = new float[] { 20f,80f };

            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            //document.Add(filiationWorker);

            #endregion

            #region Hallazgos Importantes

            var Dxs = Diagnosticos.FindAll(p => p.v_Cie10 != "Z000" && p.i_FinalQualificationId == 2);
            var DxsConca = string.Join(", ", Dxs.Select(p => p.v_DiseasesName));
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("Hallazgos Importantes: En la presente evaluación médica se encontró lo siguiente: ", fontTitleTable)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                //Línea blanca
                new PdfPCell(new Phrase(DxsConca, fontTitleTable)){Colspan=2,  HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
            };
            columnWidths = new float[] { 20f, 80f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Aptitud

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("Aptitud: ", fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                new PdfPCell(new Phrase(pCabecera.Aptitud, fontTitleTable)){ HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                //Línea blanca
                new PdfPCell(new Phrase(" ", fontTitle1)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
            };
            columnWidths = new float[] { 10f, 90f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Recomendaciones
         
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("Recomendaciones: ", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                new PdfPCell(new Phrase(pstrRecomendaciones, fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                //Línea blanca
                new PdfPCell(new Phrase(" ", fontTitle1)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
                //Línea blanca
                new PdfPCell(new Phrase(" ", fontTitle1)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
            };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion


            #region Final

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("Ha sido muy garto ofrecerkes nuestors servicios y des ya ponemos a disposición nuestro staff médico y las instalaciones de nuestra institución. No dude en llamarnos para cualquier aclaración o consulta complementaria al 6196161 anexo 5134: ", fontTitleTable)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                //Línea blanca
                new PdfPCell(new Phrase(" ", fontColumnValue)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
            };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion



            #endregion
            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls

            

            // Firma del doctor EXAMEN FISICO **************************************************

            PdfPCell cellFirmaAuditor = null;
            PdfPCell cellFirmaEvaluador = null;

            if (pCabecera == null)
            {
                cellFirmaAuditor = new PdfPCell(new Phrase(" ", fontColumnValue));
            }
            else if (pCabecera.b_FirmaAuditor != null)
            {
                cellFirmaAuditor = new PdfPCell(HandlingItextSharp.GetImage(pCabecera.b_FirmaAuditor, null, null, 120, 45));
            }
            else
            {
                cellFirmaAuditor = new PdfPCell(new Phrase(" ", fontColumnValue));
            }
            #endregion

             if (pCabecera == null)
            {
                cellFirmaEvaluador = new PdfPCell(new Phrase("Sin Firma", fontColumnValue));
            }
             else if (pCabecera.b_FirmaEvaluador != null)
            {
                cellFirmaEvaluador = new PdfPCell(HandlingItextSharp.GetImage(pCabecera.b_FirmaEvaluador, null, null, 120, 45));
            }
            else
            {
                cellFirmaEvaluador = new PdfPCell(new Phrase("Sin Firma", fontColumnValue));
            }
            #endregion

            cells = new List<PdfPCell>();
                     

            // 2 celda
            cellFirmaEvaluador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirmaEvaluador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaEvaluador.FixedHeight = 50F;
            cells.Add(cellFirmaEvaluador);

            // 3 celda (Imagen)
            cellFirmaAuditor.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirmaAuditor.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaAuditor.FixedHeight = 50F;
            cells.Add(cellFirmaAuditor);

            columnWidths = new float[] { 50f, 50f};
            columnHeaders = new string[] { "FIRMA EVALUADOR", " "};
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, " ", fontTitleTable, columnHeaders);

            document.Add(table);


            document.Close();
            writer.Close();
            writer.Dispose();
        }

    }
}
