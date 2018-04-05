using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Common;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using Sigesoft.Node.WinClient.UI.ReportsCutom;

namespace Sigesoft.Node.WinClient.UI.ReportCustom
{
    public class FichaOcupacional
    {
        MultiPrintDocument MultiDoc;
        #region Declaración de Controles
        PrintDocument printDocument1 = new PrintDocument();
        PrintDocument printDocument2 = new PrintDocument();
        PrintDocument printDocument3 = new PrintDocument();
        PrintDocument printDocument4 = new PrintDocument();
        PrintDocument printDocument5 = new PrintDocument();
        PictureBox pictureBoxLogo = new PictureBox();
        PictureBox pictureBoxFoto = new PictureBox();
        PictureBox pictureBoxFirmaTrabajador = new PictureBox();
        PictureBox pictureBoxHuellaTrabajador = new PictureBox();
        PictureBox pictureBoxFirmaMedico = new PictureBox();
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        PrintDialog PrintDialog1 = new PrintDialog(); 
        PageSetupDialog psp = new PageSetupDialog();
        #endregion

        #region Variables Globales

        bool Flag = false;
        int NroLineasPaginaVerticalPD4_ = 28;
        int CurrentRowVerticalPD4_ = 0;

        int TotalLineasHorizontalPD2_ = 18;
        int LineaActualHorizontalPD2_ = 0;

        int FilaImpresaMedicoOcupacionales_ = 0;
        int FilaImpresaMedicoPersonales_ = 0;
        int FilaImpresaHabitosNocivos_ = 0;
        int FilaImpresaMedicoFamiliares_ = 0;

        int FilaImpresaDiagnosticos_ = 0;

        string _serviceId, _pacientId, _protocolId;
        int _TypePrinter;
        int pagewidthPD1, pageheightPD1;
        int pagewidthPD2, pageheightPD2;
        int pagewidthPD3, pageheightPD3;
        int pagewidthPD4, pageheightPD4;
        int pagewidthPD5, pageheightPD5;
        //Título y Subtítulos   
        bool _WasPrintAntecedentesOcupacionalesSubTitle = false;
        bool _WasPrintAntecedentesMedicoPersonalesSubTitle = false;
        bool _WasPrintAntecedentesHabitosNocivosSubTitle = false;
        bool _WasPrintAntecedentesPatologicosFamiliaresSubTitle = false;

        bool _WasPrintTactoRectal = false;
        bool _WasPrintOftalmologiaSubTitle = false;
        bool _WasPrintAudiometriaSubTitle = false;
        bool _WasPrintRxSubTitle = false;
        bool _WasPrintElectroCardiogramaSubTitle = false;
        bool _WasPrintOseoMuscularSubTitle = false;
        bool _WasPrintAlturaGeograficaSubTitle = false;
        bool _WasPrintAlturaEstructuralSubTitle = false;
        bool _WasPrintPruebaEsfuerzoSubTitle = false;
        bool _WasPrintErgonomicoSubTitle = false;
        bool _WasPrintNeurologicaSubTitle = false;
        bool _WasPrintTestRombergSubTitle = false;
        bool _WasPrintLaboratorioSubTitle = false;
        bool _WasPrintOdontologiaSubTitle = false;
        bool _WasPrintDermatologicoSubTitle = false;

        bool _WasPrintPsicologiaSubTitle = false;
        bool _WasPrintTestClimaLaboralSubTitle = false;
        bool _WasPrintTestAnsiedadSubTitle = false;
        bool _WasPrintTestDepresionSubTitle = false;
        bool _WasPrintTestEspacioConfinadoSubTitle = false;
        bool _WasPrintTestStressSubTitle = false;
        bool _WasPrintTestFatigaSubTitle = false;
        bool _WasPrintTestSomnolenciaSubTitle = false;
        bool _WasPrintTestEstiloPersonalidadSubTitle = false;
        bool _WasPrintTestFobiasAlturasSubTitle = false;
        bool _WasPrintTestLuriaDNASubTitle = false;
        bool _WasPrintTestMotivacionesPsicologicasSubTitle = false;
        bool _WasPrintTestdibujoFiguraHumanaSubTitle = false;
        bool _WasPrintTestPersonaBajoLluviaSubTitle = false;
        bool _WasPrintTestdibujoWarttergSubTitle = false;
        bool _WasPrintTestCasaArbolSubTitle = false;
        bool _WasPrintTestConclusionesSubTitle = false;

        //Campos


        //Imagenes



        //Entidades
        PacientBL _objPacientBL = new PacientBL();
        ServiceBL _objServiceBL = new ServiceBL();
        ProtocolBL objProtocolBL = new ProtocolBL();
        NodeBL _objNodeBL = new NodeBL();

        PacientList _pacientList = new PacientList();
        ProtocolList _protocolList = new ProtocolList();
        ServiceComponentList _ServiceComponentList = new ServiceComponentList();
        serviceDto _serviceDto = new serviceDto();
        nodeDto _nodeDto = new nodeDto();


        //Listas
        HistoryBL _objHistoryBL = new HistoryBL();
        ServiceList _objServiceList = new ServiceList();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListAntropometria = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListFuncionesVitales = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListExamenFisico = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListOftalmologia = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListAudiometria = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListRx = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListElectro = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTactoRectal = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListOseoMuscular = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListAlturaEstructural = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListPruebaEsfuerzo = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListEvaluacionErgonomica = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListEvaluacionNeurologica = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestRomberg = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListLaboratorio = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListOdontologia = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListDermatologico = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestClimaLaboral = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestAnsiedad = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestDepresion = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestEspacioConfinados = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestEstress = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestFatiga = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestSomnolencia = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestPersonalidad = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestFobiasAlturas = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestLuriaDNA = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListTestMotivacionesPsicologicas = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListDibujoFiguraHumana = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListDibujoPersonaLluvia = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListDibujoWartteg = new List<ServiceComponentFieldValuesList>();
        List<ServiceComponentFieldValuesList> _objServiceComponentFieldValuesListDibujoCasaArbol = new List<ServiceComponentFieldValuesList>();



        List<HistoryList> _listAtecedentesOcupacionales = new List<HistoryList>();
        List<PersonMedicalHistoryList> _listMedicoPersonales = new List<PersonMedicalHistoryList>();
        List<NoxiousHabitsList> _listaHabitoNocivos = new List<NoxiousHabitsList>();
        List<FamilyMedicalAntecedentsList> _listaPatologicosFamiliares = new List<FamilyMedicalAntecedentsList>();

        List<DiagnosticRepositoryList> _DiagnosticRepositoryList = new List<DiagnosticRepositoryList>();


        //Cabecera de Grillas
        bool WasPrintAntecedentesOcupacionalesHeadGrid_ = false;
        bool WasPrintAntecedentesPersonalesHeadGrid_ = false;
        bool WasPrintAntecedentesHabitosNocivosHeadGrid_ = false;
        bool WasPrintAntecedentesFamiliaresHeadGrid_ = false;
        #endregion

        public FichaOcupacional(string pstrServiceId, string pstrPacientId, string pstrProtocolId, int pintTypePrinter)
        {
           
            #region Eventos de Controles
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
            printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument2_PrintPage);
            printDocument3.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument3_PrintPage);
            printDocument4.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument4_PrintPage);
            printDocument5.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument5_PrintPage);
            #endregion

            _serviceId = pstrServiceId;
            _pacientId = pstrPacientId;
            _protocolId = pstrProtocolId;
            _TypePrinter = pintTypePrinter;

            #region Llenado de Entidades

            //Datos del trabajador
            _pacientList = _objPacientBL.GetPacientReport(_pacientId);
            _protocolList = objProtocolBL.GetProtocolByIdReport(_protocolId);
            _ServiceComponentList = _objServiceBL.ComponenteExamenFisico(_serviceId, "N002-ME000000022");
            _objServiceList = _objServiceBL.GetServicePersonDataReport(_serviceId);
            //_serviceDto = _objServiceBL.GetServiceReport(_serviceId);
            _serviceDto = null;
            _nodeDto = _objNodeBL.GetNodeByNodeIdReport(Globals.ClientSession.i_CurrentExecutionNodeId);
            _objServiceComponentFieldValuesListFuncionesVitales = _objServiceBL.ValoresComponente(_serviceId, "N002-ME000000002");  //Funciones Vitales
            _objServiceComponentFieldValuesListAntropometria = _objServiceBL.ValoresComponente(_serviceId, "N002-ME000000002");  //Antropometria
            _objServiceComponentFieldValuesListExamenFisico = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000022",135);  //Examen físico

            _objServiceComponentFieldValuesListOftalmologia = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000028", 182);  //Oftalmologia
            _objServiceComponentFieldValuesListAudiometria = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000005", 182);  //audiometria
            _objServiceComponentFieldValuesListRx = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000032",182);  //Rx
            _objServiceComponentFieldValuesListElectro = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000025",182);  //electrocardio
            _objServiceComponentFieldValuesListTactoRectal = _objServiceBL.ValoresExamenComponete(_serviceId, "N009-ME000000031",135);  //Tacto Rectal
            _objServiceComponentFieldValuesListOseoMuscular = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000046",182);  //Oseo Muscular
            _objServiceComponentFieldValuesListAlturaEstructural = _objServiceBL.ValoresExamenComponete(_serviceId, "N009-ME000000015",182);  //Altura Estructural
            _objServiceComponentFieldValuesListPruebaEsfuerzo = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000029",182);  //Prueba de Esfuerzo


            _objServiceComponentFieldValuesListEvaluacionErgonomica = _objServiceBL.ValoresExamenComponete(_serviceId, "N009-ME000000036",182);  //Evaluacion Ergonomica
            _objServiceComponentFieldValuesListEvaluacionNeurologica = _objServiceBL.ValoresExamenComponete(_serviceId, "N009-ME000000037",182);  //Evaluacion Neurologica
            _objServiceComponentFieldValuesListTestRomberg = _objServiceBL.ValoresExamenComponete(_serviceId, "N009-ME000000038",182);  //Test Romberg
            _objServiceComponentFieldValuesListLaboratorio = _objServiceBL.ValoresExamenComponete(_serviceId, "N009-ME000000002",182);  //Laboratorio
            _objServiceComponentFieldValuesListOdontologia = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000027",182);  //Odontologia
            _objServiceComponentFieldValuesListDermatologico = _objServiceBL.ValoresExamenComponete(_serviceId, "N009-ME000000044",182);  //Dermatologico

            _objServiceComponentFieldValuesListTestClimaLaboral = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033",182);  //Clima Laboral
            _objServiceComponentFieldValuesListTestAnsiedad = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Ansiedad
            _objServiceComponentFieldValuesListTestDepresion = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Depresion
            _objServiceComponentFieldValuesListTestEspacioConfinados = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Espacion Confinados
            _objServiceComponentFieldValuesListTestEstress = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Test de Estress
            _objServiceComponentFieldValuesListTestFatiga = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Test Fatiga
            _objServiceComponentFieldValuesListTestSomnolencia = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Test Somnolencia
            _objServiceComponentFieldValuesListTestPersonalidad = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Test de Personalidad
            _objServiceComponentFieldValuesListTestFobiasAlturas = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Test Fobias Alturas
            _objServiceComponentFieldValuesListTestLuriaDNA = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Luria DNA
            _objServiceComponentFieldValuesListTestMotivacionesPsicologicas = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Test Motivacions Psicologicas
            _objServiceComponentFieldValuesListDibujoFiguraHumana = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Dibujo figura Humana
            _objServiceComponentFieldValuesListDibujoPersonaLluvia = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Dibujo Persona Lluvia
            _objServiceComponentFieldValuesListDibujoWartteg = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Dibujo Wartteg
            _objServiceComponentFieldValuesListDibujoCasaArbol = _objServiceBL.ValoresExamenComponete(_serviceId, "N002-ME000000033", 182);  //Dibujo Casa Arbol

            
            #endregion

            #region Llenado de Listas

            //Lista Antecendetes Ocupacionales
            _listAtecedentesOcupacionales = _objHistoryBL.GetHistoryReport(_pacientId);

            //Lista Médico Personales
            _listMedicoPersonales = _objHistoryBL.GetPersonMedicalHistoryReport(_pacientId);

            //Lista de Hábitos Nocivos
            _listaHabitoNocivos = _objHistoryBL.GetNoxiousHabitsReport(_pacientId);

            //Lista Patológicos Familiares
            _listaPatologicosFamiliares = _objHistoryBL.GetFamilyMedicalAntecedentsReport(_pacientId);

            //Lista de Diagnóticos
            _DiagnosticRepositoryList = _objServiceBL.GetServiceDisgnosticsReports(_serviceId);
            #endregion

            #region Instanciación de Controles



            #endregion

            print_Click();
            //printHorizontal_Click();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region VARIABLES LOCALES
            int y = 40; // posición con el Títutlo y el primer Subtítulo
            //int TotalRow = CalcularTotalFilasImprimirHojaVertical();

            #endregion

            #region CONF. PÁGINA

            int top, bottom, left, right;
            top = psp.PageSettings.Margins.Top;
            bottom = psp.PageSettings.Margins.Bottom;
            left = psp.PageSettings.Margins.Left;
            right = psp.PageSettings.Margins.Right;

            //formato de la fuente
            Font font = new Font("verdana", 7, FontStyle.Regular);
            Font fontGridHear = new Font("verdana", 8, FontStyle.Bold);
            Font fontGridRow = new Font("verdana", 7, FontStyle.Regular);

            //Agregar marco
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(50, 50, pagewidthPD1, pageheightPD1));

            //Cpaturar el tamaño de la columna mas larga
            SizeF sf = e.Graphics.MeasureString("12345678:", font);

            #endregion

            #region LOGO

            Bitmap myBitmap1 = new Bitmap(320, 110);
            //Logo Salus Laboris
            pictureBoxLogo.Image = Resources.logosalus;
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new System.Drawing.Size(285, 30);
            pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.DrawToBitmap(myBitmap1, new Rectangle(20, 20, 320, 110));
            e.Graphics.DrawImage(myBitmap1, 0, 0);
            myBitmap1.Dispose();
            #endregion

            #region FOTO
            Bitmap myBitmap2 = new Bitmap(600, 600);
            //Foto Paciente            
            pictureBoxFoto.Image = Common.Utils.BytesArrayToImage(_pacientList.b_Photo, pictureBoxFoto);
            pictureBoxFoto.Name = "pictureBoxFoto";
            pictureBoxFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxFoto.Size = new System.Drawing.Size(200, 220);
            pictureBoxFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxFoto.DrawToBitmap(myBitmap2, new Rectangle(395, 220, 200, 200));

            e.Graphics.DrawImage(myBitmap2, 100, 200);
            myBitmap2.Dispose();

            #endregion

            #region PARTE ESTÀTICA
            // Linea 1 : Título del Reporte        
            e.Graphics.DrawString("Ficha Médica Ocupacional", new Font("arial", 18, FontStyle.Bold), Brushes.Black, new PointF((pagewidthPD1 / 2) - 50, top + y));

            //Línea 2  : Anexo         
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("Anexo N° 02", new Font("arial", 14, FontStyle.Regular), Brushes.Black, new PointF((pagewidthPD1 / 2) + 50, top + y));

            //Línea 3 y 4 :                
            y = y + 30; //Agregar Línea  
            y = y + 30; //Agregar Línea 
            e.Graphics.DrawString("   Historia Clínica: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_serviceDto.v_ServiceId, font, Brushes.Blue, new PointF(left + sf.Width + 60, top + y));

            e.Graphics.DrawString("Fecha de Evaluación:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_serviceDto.d_ServiceDate.ToString().Substring(0, 10), font, Brushes.Blue, new PointF(left + sf.Width + 360, top + y));

            //Línea 5 :        
            y = y + 30; //Agregar Línea  
            e.Graphics.DrawString("   Tipo de Evaluación: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_protocolList.v_EsoType, font, Brushes.Blue, new PointF(left + sf.Width + 80, top + y));

            e.Graphics.DrawString("Lugar de Examen:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_nodeDto.v_GeografyLocationId, font, Brushes.Blue, new PointF(left + sf.Width + 345, top + y));

            //Línea 6 :       
            y = y + 30; //Agregar Línea
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("I. DATOS DE LA EMPRESA: ", new Font("verdana", 9, FontStyle.Underline), Brushes.Black, new PointF(left + 15, top + y));


            //Línea 7 :          
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Razón Social: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_protocolList.v_Organization, font, Brushes.Blue, new PointF(left + sf.Width + 45, top + y));

            e.Graphics.DrawString("Actividad Económica:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_protocolList.v_SectorTypeName, font, Brushes.Blue, new PointF(left + sf.Width + 365, top + y));

            //Línea 8 :        
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Lugar de Trabajo: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_protocolList.v_Location, font, Brushes.Blue, new PointF(left + sf.Width + 70, top + y));

            e.Graphics.DrawString("Ubicación:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_protocolList.v_OrganizationAddress, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));

            //Línea 9 :       
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Puesto de Trabajo: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_CurrentOccupation, font, Brushes.Blue, new PointF(left + sf.Width + 75, top + y));

            //Línea 10 :         
            y = y + 30;//Agregar Línea
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("II. FILICACIÓN DEL TRABAJADOR: ", new Font("verdana", 9, FontStyle.Underline), Brushes.Black, new PointF(left + 15, top + y));

            //Línea 11 :     
            y = y + 30;//Agregar Línea
            e.Graphics.DrawString("   Nombres: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_FirstName, font, Brushes.Blue, new PointF(left + sf.Width + 25, top + y));
            e.Graphics.DrawString("Foto Trabajador:", font, Brushes.Black, new PointF(left + 300, top + y));

            //Línea 12 :          
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Apellidos: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_FirstLastName + " " + _pacientList.v_SecondLastName, font, Brushes.Blue, new PointF(left + sf.Width + 25, top + y));

            //Línea 13 :       
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Edad:", font, Brushes.Black, new PointF(left + 15, top + y));
            DateTime FechaNacimiento = (DateTime)_pacientList.d_Birthdate;
            e.Graphics.DrawString((DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1).ToString(), font, Brushes.Blue, new PointF(left + sf.Width, top + y));

            //Línea 14 :        
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Tipo Documento: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_DocTypeName, font, Brushes.Blue, new PointF(left + sf.Width + 65, top + y));

            //Línea 15 :       
            y = y + 30;//Agregar Línea
            e.Graphics.DrawString("   N° Documento:", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_DocNumber, font, Brushes.Blue, new PointF(left + sf.Width + 55, top + y));

            //Línea 16 :       
            y = y + 30;//Agregar Línea
            e.Graphics.DrawString("   Domicilio Fiscal: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_AdressLocation, font, Brushes.Blue, new PointF(left + sf.Width + 65, top + y));

            //Línea 17 :   
            y = y + 30;//Agregar Línea
            e.Graphics.DrawString("   Ubicación: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_DepartamentName + " / " + _pacientList.v_ProvinceName + " / " + _pacientList.v_DistrictName, font, Brushes.Blue, new PointF(left + sf.Width + 30, top + y));

            //Línea 18:          
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Residencia en lugar de Trabajo:", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_ResidenceTimeInWorkplace, font, Brushes.Blue, new PointF(left + sf.Width + 150, top + y));

            //Línea 19 :         
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Tiempo de Residencia: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.i_ResidenceInWorkplaceId.ToString(), font, Brushes.Blue, new PointF(left + sf.Width + 100, top + y));

            e.Graphics.DrawString("Tiempo de Seguro:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_pacientList.i_TypeOfInsuranceId.ToString(), font, Brushes.Blue, new PointF(left + sf.Width + 350, top + y));

            //Línea 20 :       
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   Email: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_Mail, font, Brushes.Blue, new PointF(left + sf.Width + 5, top + y));

            e.Graphics.DrawString("Teléfono:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_pacientList.v_TelephoneNumber, font, Brushes.Blue, new PointF(left + sf.Width + 295, top + y));

            //Línea 21 :        
            y = y + 30;//Agregar Línea
            e.Graphics.DrawString("   Estado Civil: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.v_MaritalStatus, font, Brushes.Blue, new PointF(left + sf.Width + 43, top + y));

            e.Graphics.DrawString("Grado de Instrucción:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_pacientList.v_CurrentOccupation, font, Brushes.Blue, new PointF(left + sf.Width + 368, top + y));

            //Línea 22 :         
            y = y + 30; //Agregar Línea
            e.Graphics.DrawString("   N° hijos vivos: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_pacientList.i_NumberLivingChildren.ToString(), font, Brushes.Blue, new PointF(left + sf.Width + 52, top + y));

            e.Graphics.DrawString("N° Dependientes:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_pacientList.i_NumberDependentChildren.ToString(), font, Brushes.Blue, new PointF(left + sf.Width + 342, top + y));


            #endregion

        }

        private int CalcularTotalFilasImprimirPD2()
        {
            int CountRowPrintH = 0;
            CountRowPrintH += 1; //III. ANTECEDENTES OCUPACIONALES:
            CountRowPrintH += 3; // Cabecera grilla Antecendentes Ocupacionales
            CountRowPrintH += _listAtecedentesOcupacionales.Count * 2; // Cantidad de filas de los antecedentes Ocupacionales
            CountRowPrintH += 1; //Cabecera grilla  ANTECEDENTES MÉDICOS PERSONALES: 
            CountRowPrintH += _listMedicoPersonales.Count + 2; // Cantidad de filas de los antecedentes médicos personales
            CountRowPrintH += _listaHabitoNocivos.Count + 1; // Cantidad de filas de los hábitos nocivos más uno que es el título de la grilla; más una línea en blanco que es para el espacio después de la grilla
            CountRowPrintH += _listaPatologicosFamiliares.Count + 1;// Cantidad de filas de los antecedentes médicos familiares más uno que es el título de la grilla; más una línea en blanco que es para el espacio después de la grilla

            return CountRowPrintH;
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region VARIABLES LOCALES
            int y = 10; // posición con el Títutlo y el primer Subtítulo
            int TotalFilasCalculadas = CalcularTotalFilasImprimirPD2();

            #endregion

            #region CONF. PÁGINA

            int top, bottom, left, right;
            top = psp.PageSettings.Margins.Top;
            bottom = psp.PageSettings.Margins.Bottom;
            left = psp.PageSettings.Margins.Left;
            right = psp.PageSettings.Margins.Right;

            //formato de la fuente
            Font font = new Font("verdana", 7, FontStyle.Regular);
            Font fontGridHear = new Font("verdana", 8, FontStyle.Bold);
            Font fontGridRow = new Font("verdana", 7, FontStyle.Regular);

            //Agregar marco
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(50, 50, pageheightPD2, pagewidthPD2));

            //Cpaturar el tamaño de la columna mas larga
            SizeF sf = e.Graphics.MeasureString("12345678:", font);

            #endregion

            #region LOGO

            Bitmap myBitmap1 = new Bitmap(320, 110);
            //Logo Salus Laboris
            pictureBoxLogo.Image = Resources.logosalus;
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new System.Drawing.Size(285, 30);
            pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.DrawToBitmap(myBitmap1, new Rectangle(20, 20, 320, 110));
            e.Graphics.DrawImage(myBitmap1, 0, 0);
            myBitmap1.Dispose();
            #endregion

            #region ANTECEDENTES OCUPACIONALES

            #region CONF. GRID MÈDICOS OCUPACIONALES
            SizeF sHeaderOrganization = e.Graphics.MeasureString("xxxxxxxxxxxxxxx", fontGridHear);
            SizeF sHeaderTypeActivity = e.Graphics.MeasureString("ÁREA123456", fontGridHear);
            SizeF sHeaderWorkstation = e.Graphics.MeasureString("OCUPACIÓN123456", fontGridHear);
            SizeF sHeaderFecha = e.Graphics.MeasureString("FECHA123456", fontGridHear);
            SizeF sHeaderExposicion = e.Graphics.MeasureString("EXPOSICIÓN12345678901234567890", fontGridHear);
            SizeF sHeaderAltitud = e.Graphics.MeasureString("ALTITUD123", fontGridHear);
            SizeF sHeaderTipoOperacion = e.Graphics.MeasureString("TIPO OPERACIÓN123", fontGridHear);
            SizeF sHeaderEpps = e.Graphics.MeasureString("EPPS1234567890", fontGridHear);

            int i = 0, r = 0;
            SizeF sm = e.Graphics.MeasureString("yyyyyyyyyyy", font);
            bool AcaboImpresionGridMedicoOcupacionales = false;
            #endregion
            //Línea 22 : Comienza la construcción de la grilla Antecdentes Ocupacionales
            if (_WasPrintAntecedentesOcupacionalesSubTitle == false)
            {
                e.Graphics.DrawString("III. ANTECEDENTES OCUPACIONALES: ", new Font("verdana", 9, FontStyle.Underline), Brushes.Black, new PointF(left + 15, top + y));

                _WasPrintAntecedentesOcupacionalesSubTitle = true;
            }

            //Se imprime la cabecera de la Grilla Antecedentes Ocupacionales
            if (WasPrintAntecedentesOcupacionalesHeadGrid_ == false)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)(left + sHeaderOrganization.Width * i + 445), (int)(top + y), 473, (int)(sm.Height + 16)));
                e.Graphics.DrawString("                                           ANEXO 7C", fontGridHear, Brushes.Black, new PointF(left + sHeaderOrganization.Width * i + 466, top + y + 7));
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea

                //Se imprime la cabecera de la Grilla Antecedentes Ocupacionales
                PropertyInfo[] Props = typeof(HistoryList).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    if (prop.Name == "v_Organization")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderOrganization.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("EMPRESA", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_TypeActivity")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width, (int)(top + y), (int)sHeaderTypeActivity.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("ÁREA", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_workstation")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width, (int)(top + y), (int)sHeaderWorkstation.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("OCUPACIÓN", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "Fecha")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width, (int)(top + y), (int)sHeaderFecha.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("FECHA", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "Exposicion")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width + (int)sHeaderFecha.Width, (int)(top + y), (int)sHeaderExposicion.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("EXPOSICIÓN", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "i_GeografixcaHeight")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width + (int)sHeaderFecha.Width + (int)sHeaderExposicion.Width, (int)(top + y), (int)sHeaderAltitud.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("ALTITUD", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_TypeOperationName")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width + (int)sHeaderFecha.Width + (int)sHeaderExposicion.Width + (int)sHeaderAltitud.Width, (int)(top + y), (int)sHeaderTipoOperacion.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("TIPO OPERACIÓN", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "Epps")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width + (int)sHeaderFecha.Width + (int)sHeaderExposicion.Width + (int)sHeaderAltitud.Width + (int)sHeaderTipoOperacion.Width, (int)(top + y), (int)sHeaderEpps.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("EPPS", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                }
                WasPrintAntecedentesOcupacionalesHeadGrid_ = true;
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
            }

            for (r = FilaImpresaMedicoOcupacionales_; r < _listAtecedentesOcupacionales.Count; r++)
            {
                PropertyInfo[] Props = typeof(HistoryList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {

                    var item = _listAtecedentesOcupacionales[r];

                    if (prop.Name == "v_Organization")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderOrganization.Width, 60);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_TypeActivity")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width, (int)(top + y), (int)sHeaderTypeActivity.Width, 60);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_workstation")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width, (int)(top + y), (int)sHeaderWorkstation.Width, 60);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "Fecha")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width, (int)(top + y), (int)sHeaderFecha.Width, 60);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "Exposicion")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width + (int)sHeaderFecha.Width, (int)(top + y), (int)sHeaderExposicion.Width, 60);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "i_GeografixcaHeight")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width + (int)sHeaderFecha.Width + (int)sHeaderExposicion.Width, (int)(top + y), (int)sHeaderAltitud.Width, 60);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);

                    }
                    else if (prop.Name == "v_TypeOperationName")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width + (int)sHeaderFecha.Width + (int)sHeaderExposicion.Width + (int)sHeaderAltitud.Width, (int)(top + y), (int)sHeaderTipoOperacion.Width, 60);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "Epps")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderOrganization.Width + (int)sHeaderTypeActivity.Width + (int)sHeaderWorkstation.Width + (int)sHeaderFecha.Width + (int)sHeaderExposicion.Width + (int)sHeaderAltitud.Width + (int)sHeaderTipoOperacion.Width, (int)(top + y), (int)sHeaderEpps.Width, 60);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                }
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                FilaImpresaMedicoOcupacionales_ += 1;


                if (LineaActualHorizontalPD2_ > TotalLineasHorizontalPD2_)
                {
                    NewPageHorizontal(e, TotalFilasCalculadas, LineaActualHorizontalPD2_);
                    return;
                }

                if (FilaImpresaMedicoOcupacionales_ == _listAtecedentesOcupacionales.Count)
                {
                    AcaboImpresionGridMedicoOcupacionales = true;

                }
            }

            #endregion

            #region ANTECEDENTES MÉDICOS PERSONALES

            #region CONF. GRID MÈDICOS PERSONALES
            SizeF sfAPersonales = e.Graphics.MeasureString("xxxxxxxxxxxxxxxxxxxxxxxxxxx", font);
            int imp = 0, rmp = 0;
            SizeF smmp = e.Graphics.MeasureString("yyyyyyyyyyy", font);
            bool AcaboImpresionGridMedicoPersonales = false;

            SizeF sHeaderCategoria = e.Graphics.MeasureString("CATEGORIA1234567890", fontGridHear);
            SizeF sHeaderDiagnostico = e.Graphics.MeasureString("DIAGNOSTICO1234567890", fontGridHear);
            SizeF sHeaderTipoDiagnostico = e.Graphics.MeasureString("TIPO DIAGNOSTICO1234567890", fontGridHear);
            SizeF sHeaderSiNo = e.Graphics.MeasureString("SINO123", fontGridHear);
           

            #endregion

            if (_WasPrintAntecedentesMedicoPersonalesSubTitle == false)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                //y = y + 30; CurrentRowHorizontal_ += 1;

                e.Graphics.DrawString("IV. ANTECEDENTES MÉDICOS PERSONALES: ", new Font("verdana", 9, FontStyle.Underline), Brushes.Black, new PointF(left + 15, top + y));
                //y = y + 30;
                //CurrentRowAMC_ += 1;
                _WasPrintAntecedentesMedicoPersonalesSubTitle = true;
            }
            //Se imprime la cabecera de la Grilla Antecedentes Médicos Personales
            if (WasPrintAntecedentesPersonalesHeadGrid_ == false)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                PropertyInfo[] Props = typeof(PersonMedicalHistoryList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    if (prop.Name == "v_GroupName")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderCategoria.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("CATEGORÍA", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_DiseasesName")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderCategoria.Width, (int)(top + y), (int)sHeaderDiagnostico.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("DIAGNÓSTICO", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_TypeDiagnosticName")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderCategoria.Width + (int)sHeaderDiagnostico.Width, (int)(top + y), (int)sHeaderTipoDiagnostico.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("TIPO DIAGNÓSTICO", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "i_Answer")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderCategoria.Width + (int)sHeaderDiagnostico.Width + (int)sHeaderTipoDiagnostico.Width, (int)(top + y), (int)sHeaderSiNo.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("SI / NO", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                }

                //y = y + 30;
                //CurrentRowAMC_ += 1;
                WasPrintAntecedentesPersonalesHeadGrid_ = true;
            }
            for (rmp = FilaImpresaMedicoPersonales_; rmp < _listMedicoPersonales.Count; rmp++)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                PropertyInfo[] Props = typeof(PersonMedicalHistoryList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                int c = 0;
                foreach (PropertyInfo prop in Props)
                {

                    var item = _listMedicoPersonales[rmp];
                    //Setting column names as Property names
                    if (prop.Name == "v_GroupName")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderCategoria.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_DiseasesName")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderCategoria.Width, (int)(top + y), (int)sHeaderDiagnostico.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_TypeDiagnosticName")
                    {
                       
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderCategoria.Width + (int)sHeaderDiagnostico.Width, (int)(top + y), (int)sHeaderTipoDiagnostico.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "i_Answer")
                    {
                        var value = prop.GetValue(item, null);
                        if (value.ToString() == "1")
                        {
                         
                            Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderCategoria.Width + (int)sHeaderDiagnostico.Width + (int)sHeaderTipoDiagnostico.Width, (int)(top + y), (int)sHeaderSiNo.Width, 30);
                            StringFormat stringFormat = new StringFormat();
                            stringFormat.Alignment = StringAlignment.Center;
                            stringFormat.LineAlignment = StringAlignment.Center;

                            e.Graphics.DrawString("SI", fontGridRow, Brushes.Black, rect1, stringFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect1);
                        }
                        else if (value.ToString() == "2")
                        {
                            Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderCategoria.Width + (int)sHeaderDiagnostico.Width + (int)sHeaderTipoDiagnostico.Width, (int)(top + y), (int)sHeaderSiNo.Width, 30);
                            StringFormat stringFormat = new StringFormat();
                            stringFormat.Alignment = StringAlignment.Center;
                            stringFormat.LineAlignment = StringAlignment.Center;

                            e.Graphics.DrawString("ND", fontGridRow, Brushes.Black, rect1, stringFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect1);
                        }
                    }
                }

                FilaImpresaMedicoPersonales_ += 1;

                if (LineaActualHorizontalPD2_ > TotalLineasHorizontalPD2_)
                {
                    NewPageHorizontal(e, TotalFilasCalculadas, LineaActualHorizontalPD2_);
                    return;
                }

                if (FilaImpresaMedicoPersonales_ == _listMedicoPersonales.Count)
                {
                    AcaboImpresionGridMedicoPersonales = true;
                }
            }
            #endregion

            #region HÁBITOS NOCIVOS
            #region CONF. GRID HÁBITOS NOCIVOS
            SizeF sfHabitosNocivos = e.Graphics.MeasureString("xxxxxxxxxxxxxxxxxxxxxxxxxxx", font);
            int iHabitosNocivos = 0, rHabitosNocivos = 0;
            SizeF smHabitosNocivos = e.Graphics.MeasureString("yyyyyyyyyyy", font);
            bool AcaboImpresionGridHabitosNocivos = false;

            SizeF sHeaderHabito = e.Graphics.MeasureString("HABITO1234567890", fontGridHear);
            SizeF sHeaderFrecuencia = e.Graphics.MeasureString("FRECUENCIA1234567890", fontGridHear);
            SizeF sHeaderDescripcion = e.Graphics.MeasureString("DESCRIPCION1234567890", fontGridHear);
            #endregion

            if (_WasPrintAntecedentesHabitosNocivosSubTitle == false)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea              
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea

                e.Graphics.DrawString("IV. HÁBITOS NOCIVOS: ", new Font("verdana", 9, FontStyle.Underline), Brushes.Black, new PointF(left + 15, top + y));

                _WasPrintAntecedentesHabitosNocivosSubTitle = true;
            }

            //Se imprime la cabecera de la Grilla Antecedentes Hábitos Nocivos
            if (WasPrintAntecedentesHabitosNocivosHeadGrid_ == false)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                PropertyInfo[] Props = typeof(NoxiousHabitsList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    if (prop.Name == "v_NoxiousHabitsName")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderHabito.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("HÁBITO", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_Frequency")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderHabito.Width, (int)(top + y), (int)sHeaderFrecuencia.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("FRECUENCIA", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_Comment")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderHabito.Width + (int)sHeaderFrecuencia.Width, (int)(top + y), (int)sHeaderDescripcion.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("DESCRIPCIÓN", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                }

                WasPrintAntecedentesHabitosNocivosHeadGrid_ = true;
            }

            for (rHabitosNocivos = FilaImpresaHabitosNocivos_; rHabitosNocivos < _listaHabitoNocivos.Count; rHabitosNocivos++)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                PropertyInfo[] Props = typeof(NoxiousHabitsList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                int c = 0;
                foreach (PropertyInfo prop in Props)
                {

                    var item = _listaHabitoNocivos[rHabitosNocivos];
                    //Setting column names as Property names
                    if (prop.Name == "v_NoxiousHabitsName")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderHabito.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_Frequency")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderHabito.Width, (int)(top + y), (int)sHeaderFrecuencia.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_Comment")
                    {
                        
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderHabito.Width + (int)sHeaderFrecuencia.Width, (int)(top + y), (int)sHeaderDescripcion.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                }

                FilaImpresaHabitosNocivos_ += 1;

                if (LineaActualHorizontalPD2_ > TotalLineasHorizontalPD2_)
                {
                    NewPageHorizontal(e, TotalFilasCalculadas, LineaActualHorizontalPD2_);
                    return;
                }

                if (FilaImpresaHabitosNocivos_ == _listMedicoPersonales.Count)
                {
                    AcaboImpresionGridHabitosNocivos = true;
                }
            }


            #endregion

            #region ANTECEDENTES PATOLÓGICOS FAMILIARES

            #region CONF. GRID PATOLÓGICOS FAMILIARES
            SizeF sfFamiliares = e.Graphics.MeasureString("xxxxxxxxxxxxxxxxxxxxxxxxxxx", font);
            int iFamiliares = 0, rFamiliares = 0;
            SizeF smFamiliares = e.Graphics.MeasureString("yyyyyyyyyyy", font);
            bool AcaboImpresionGridFamiliares = false;

            SizeF sHeaderFamiliar = e.Graphics.MeasureString("HABITO1234567890", fontGridHear);
            SizeF sHeaderDiagnostico1 = e.Graphics.MeasureString("FRECUENCIA1234567890", fontGridHear);
            SizeF sHeaderDescripcion1 = e.Graphics.MeasureString("DESCRIPCION1234567890", fontGridHear);
            #endregion

            if (_WasPrintAntecedentesPatologicosFamiliaresSubTitle == false)
            {
          
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea              
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea

                e.Graphics.DrawString("II. ANTECEDENTES PATOLÓGICO FAMILIARES: ", new Font("verdana", 9, FontStyle.Underline), Brushes.Black, new PointF(left + 15, top + y));

                _WasPrintAntecedentesPatologicosFamiliaresSubTitle = true;
            }
            //Se imprime la cabecera de la Grilla Antecedentes Familiares

            if (WasPrintAntecedentesFamiliaresHeadGrid_ == false)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                PropertyInfo[] Props = typeof(FamilyMedicalAntecedentsList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {

                    //Setting column names as Property names
                    if (prop.Name == "v_TypeFamilyName")
                    {
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderFamiliar.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("FAMILIAR", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_DiseaseName")
                    {
                       
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderFamiliar.Width, (int)(top + y), (int)sHeaderDiagnostico1.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("DIAGNÓSTICO", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_Comment")
                    {
                    
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderFamiliar.Width + (int)sHeaderDiagnostico1.Width, (int)(top + y), (int)sHeaderDescripcion1.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString("DESCRIPCIÓN", fontGridHear, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                }
                WasPrintAntecedentesFamiliaresHeadGrid_ = true;
            }


            for (rFamiliares = FilaImpresaMedicoFamiliares_; rFamiliares < _listaHabitoNocivos.Count; rFamiliares++)
            {
                y = y + 30; LineaActualHorizontalPD2_ += 1; //Agregar Línea
                PropertyInfo[] Props = typeof(FamilyMedicalAntecedentsList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                int c = 0;
                foreach (PropertyInfo prop in Props)
                {

                    var item = _listaPatologicosFamiliares[rFamiliares];
                    //Setting column names as Property names
                    if (prop.Name == "v_TypeFamilyName")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderFamiliar.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_DiseaseName")
                    {
                        
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderFamiliar.Width, (int)(top + y), (int)sHeaderDiagnostico1.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_Comment")
                    {
                       
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderFamiliar.Width + (int)sHeaderDiagnostico1.Width, (int)(top + y), (int)sHeaderDescripcion1.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                }

                FilaImpresaMedicoFamiliares_ += 1;
                if (LineaActualHorizontalPD2_ > TotalLineasHorizontalPD2_)
                {
                    NewPageHorizontal(e, TotalFilasCalculadas, LineaActualHorizontalPD2_);
                    return;
                }

                if (FilaImpresaMedicoFamiliares_ == _listMedicoPersonales.Count)
                {
                    AcaboImpresionGridFamiliares = true;
                }
            }


            #endregion
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region VARIABLES LOCALES
            int y = 10; // posición con el Títutlo y el primer Subtítulo         

            #endregion

            #region CONF. PÁGINA

            int top, bottom, left, right;
            top = psp.PageSettings.Margins.Top;
            bottom = psp.PageSettings.Margins.Bottom;
            left = psp.PageSettings.Margins.Left;
            right = psp.PageSettings.Margins.Right;

            //formato de la fuente
            Font font = new Font("verdana", 7, FontStyle.Regular);
            Font fontGridHear = new Font("verdana", 8, FontStyle.Bold);
            Font fontGridRow = new Font("verdana", 7, FontStyle.Regular);

            //Agregar marco
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(50, 50, pagewidthPD3, pageheightPD3));

            //Cpaturar el tamaño de la columna mas larga
            SizeF sf = e.Graphics.MeasureString("12345678:", font);

            #endregion

            #region LOGO

            Bitmap myBitmap1 = new Bitmap(320, 110);
            //Logo Salus Laboris
            pictureBoxLogo.Image = Resources.logosalus;
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new System.Drawing.Size(285, 30);
            pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.DrawToBitmap(myBitmap1, new Rectangle(20, 20, 320, 110));
            e.Graphics.DrawImage(myBitmap1, 0, 0);
            myBitmap1.Dispose();
            #endregion

            #region EVALUACIÓN MÉDICA - ESTÁTICA

            //línea 1 ********************************************************************************************************************************************
            e.Graphics.DrawString("VII. EVALUACIÓN MÉDICA: ", new Font("verdana", 9, FontStyle.Regular), Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));

            //línea 2 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Anamnesis: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

            //línea 3 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea              
            e.Graphics.DrawString("   ¿Presenta Síntomas?: ", font, Brushes.Black, new PointF(left + 15, top + y));
            if (_objServiceList.i_HasSymptomId == 1)
            {
                e.Graphics.DrawString("Si", font, Brushes.Blue, new PointF(left + sf.Width + 90, top + y));
            }
            else
            {
                e.Graphics.DrawString("No", font, Brushes.Blue, new PointF(left + sf.Width + 90, top + y));
            }

            e.Graphics.DrawString("   Tiempo de Enfermedad:", font, Brushes.Black, new PointF(left + 300, top + y));
            e.Graphics.DrawString(_objServiceList.i_TimeOfDisease.ToString() + " días", font, Brushes.Blue, new PointF(left + sf.Width + 385, top + y));

            //línea 4 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea

            e.Graphics.DrawString("   Síntomas Principales: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceList.v_MainSymptom, font, Brushes.Blue, new PointF(left + sf.Width + 90, top + y));

            //línea 4 , 5 , 6 , 7 ********************************************************************************************************************************************             
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Relato: ", font, Brushes.Black, new PointF(left + 15, top + y));

            Rectangle rect1 = new Rectangle(left + 80, top + y - 5, 605, 90);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;

            e.Graphics.DrawString(_objServiceList.v_Story, font, Brushes.Blue, rect1, stringFormat);
            e.Graphics.DrawRectangle(Pens.Black, rect1);

            y = y + 30;  //Agregar Línea
            y = y + 30;  //Agregar Línea
            y = y + 30;  //Agregar Línea

            //línea 8 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Funciones Biológicas: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

            //línea 9 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Sueño: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceList.v_Dream, font, Brushes.Blue, new PointF(left + sf.Width + 50, top + y));

            e.Graphics.DrawString("Apetito:", font, Brushes.Black, new PointF(left + 280, top + y));
            e.Graphics.DrawString(_objServiceList.v_Appetite, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));

            e.Graphics.DrawString("Orina:", font, Brushes.Black, new PointF(left + 500, top + y));
            e.Graphics.DrawString(_objServiceList.v_Urine, font, Brushes.Blue, new PointF(left + sf.Width + 500, top + y));

            //línea 10 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Deposiciones: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceList.v_Deposition, font, Brushes.Blue, new PointF(left + sf.Width + 50, top + y));

            e.Graphics.DrawString("Sed:", font, Brushes.Black, new PointF(left + 280, top + y));
            e.Graphics.DrawString(_objServiceList.v_Thirst, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));

            e.Graphics.DrawString("FUR:", font, Brushes.Black, new PointF(left + 500, top + y));
            if (_objServiceList.d_Fur == null)
            {
                e.Graphics.DrawString("", font, Brushes.Blue, new PointF(left + sf.Width + 500, top + y));
            }
            else
            {
                DateTime fecha = (DateTime)_objServiceList.d_Fur;
                e.Graphics.DrawString(fecha.Date.ToString().Substring(0, 10), font, Brushes.Blue, new PointF(left + sf.Width + 500, top + y));
            }

            //línea 11 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   MAC: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceList.v_Mac, font, Brushes.Blue, new PointF(left + sf.Width + 50, top + y));

            e.Graphics.DrawString("Régimen Catemenial:", font, Brushes.Black, new PointF(left + 280, top + y));
            e.Graphics.DrawString(_objServiceList.v_CatemenialRegime, font, Brushes.Blue, new PointF(left + sf.Width + 340, top + y));

            //línea 12 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));

            //línea 13 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Antropometría: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

            //línea 14 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Talla: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListAntropometria.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAntropometria.Find(p => p.v_ComponentFieldId == "N002-MF000000007")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 80, top + y));

            e.Graphics.DrawString("Peso:", font, Brushes.Black, new PointF(left + 280, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListAntropometria.Count() == 0 ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAntropometria.Find(p => p.v_ComponentFieldId == "N002-MF000000008")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 360, top + y));

            e.Graphics.DrawString("IMC:", font, Brushes.Black, new PointF(left + 500, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListAntropometria.Count() == 0 ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAntropometria.Find(p => p.v_ComponentFieldId == "N002-MF000000009")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 570, top + y));

            //línea 15 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Perímetro Cintura: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListAntropometria.Count() == 0 ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAntropometria.Find(p => p.v_ComponentFieldId == "N002-MF000000010")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 80, top + y));

            e.Graphics.DrawString("Perímetro Cadera:", font, Brushes.Black, new PointF(left + 280, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListAntropometria.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAntropometria.Find(p => p.v_ComponentFieldId == "N002-MF000000011")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 360, top + y));

            e.Graphics.DrawString("Índice Cintura Cadera:", font, Brushes.Black, new PointF(left + 500, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListAntropometria.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAntropometria.Find(p => p.v_ComponentFieldId == "N002-MF000000012")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 570, top + y));

            //línea 16 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   % grasa Corporal: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListAntropometria.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAntropometria.Find(p => p.v_ComponentFieldId == "N002-MF000000013")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 80, top + y));

            //línea 17 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Funciones Vitales: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

            //línea 18 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Presión A. Sistólica: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListFuncionesVitales.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListFuncionesVitales.Find(p => p.v_ComponentFieldId == "N002-MF000000001")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 80, top + y));

            e.Graphics.DrawString("Presión A. Diastólica:", font, Brushes.Black, new PointF(left + 280, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListFuncionesVitales.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListFuncionesVitales.Find(p => p.v_ComponentFieldId == "N002-MF000000002")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 360, top + y));

            e.Graphics.DrawString("Frecuencia Cardiaca:", font, Brushes.Black, new PointF(left + 500, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListFuncionesVitales.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListFuncionesVitales.Find(p => p.v_ComponentFieldId == "N002-MF000000003")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 570, top + y));


            //línea 19 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Temperatura: ", font, Brushes.Black, new PointF(left + 15, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListFuncionesVitales.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListFuncionesVitales.Find(p => p.v_ComponentFieldId == "N002-MF000000004")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 80, top + y));

            e.Graphics.DrawString("Frecuencia Respiratoria:", font, Brushes.Black, new PointF(left + 280, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListFuncionesVitales.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListFuncionesVitales.Find(p => p.v_ComponentFieldId == "N002-MF000000005")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 360, top + y));

            e.Graphics.DrawString("Saturación Oxígeno:", font, Brushes.Black, new PointF(left + 500, top + y));
            e.Graphics.DrawString(_objServiceComponentFieldValuesListFuncionesVitales.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListFuncionesVitales.Find(p => p.v_ComponentFieldId == "N002-MF000000006")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 570, top + y));

            //línea 20 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));

            //línea 21 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Examen Físico: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

            //línea 22 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Piel y Mucosa: ", font, Brushes.Black, new PointF(left + 15, top + y));
            string Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 55, top + y));

            e.Graphics.DrawString("Cabello:", font, Brushes.Black, new PointF(left + 200, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 210, top + y));

            e.Graphics.DrawString("Ojos y Anexos:", font, Brushes.Black, new PointF(left + 350, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 375, top + y));

            e.Graphics.DrawString("Oídos:", font, Brushes.Black, new PointF(left + 530, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 560, top + y));

            //línea 23 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Naríz: ", font, Brushes.Black, new PointF(left + 15, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 55, top + y));

            e.Graphics.DrawString("Boca:", font, Brushes.Black, new PointF(left + 200, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 210, top + y));

            e.Graphics.DrawString("Faringe:", font, Brushes.Black, new PointF(left + 350, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 375, top + y));

            e.Graphics.DrawString("Cuello:", font, Brushes.Black, new PointF(left + 530, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 560, top + y));

            // //línea 24 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Naríz: ", font, Brushes.Black, new PointF(left + 15, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 55, top + y));

            e.Graphics.DrawString("Respiratorio:", font, Brushes.Black, new PointF(left + 200, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 210, top + y));

            e.Graphics.DrawString("Cardiovascular:", font, Brushes.Black, new PointF(left + 350, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 375, top + y));

            e.Graphics.DrawString("Genitourinario:", font, Brushes.Black, new PointF(left + 530, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 560, top + y));

            //línea 25 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Locomotor: ", font, Brushes.Black, new PointF(left + 15, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 55, top + y));

            e.Graphics.DrawString("Marcha:", font, Brushes.Black, new PointF(left + 200, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 210, top + y));

            e.Graphics.DrawString("Columna:", font, Brushes.Black, new PointF(left + 350, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 375, top + y));

            e.Graphics.DrawString("Ext. Inferiores:", font, Brushes.Black, new PointF(left + 530, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 560, top + y));

            //línea 26 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Ext. Superiores: ", font, Brushes.Black, new PointF(left + 15, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 55, top + y));

            e.Graphics.DrawString("Linfáticos:", font, Brushes.Black, new PointF(left + 200, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 210, top + y));

            e.Graphics.DrawString("Neurológico:", font, Brushes.Black, new PointF(left + 350, top + y));
            Value1 = _objServiceComponentFieldValuesListExamenFisico.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000131")).v_Value1Name;
            e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 375, top + y));

            //línea 27 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Aspecto General:", font, Brushes.Black, new PointF(left + 15, top + y));
            if (_objServiceComponentFieldValuesListExamenFisico.Count == 0)
            {
                e.Graphics.DrawString(" ", font, Brushes.Blue, new PointF(left + sf.Width + 55, top + y));
            }
            else
            {
                e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N009-MF000000003")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 55, top + y));

            }

            //línea 28 , 29 ********************************************************************************************************************************************
            y = y + 30;  //Agregar Línea
            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Hallazgo: ", font, Brushes.Black, new PointF(left + 15, top + y));

            //línea 30 , 31 ********************************************************************************************************************************************

            Rectangle rectHallazgo = new Rectangle(left + 100, top + y - 5, 585, 60);
            StringFormat stringFormatHallazgo = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            string ValueHallazgo = _objServiceComponentFieldValuesListExamenFisico.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListExamenFisico.Find(p => p.v_ComponentFieldId == "N002-MF000000138")).v_Value1.ToString();
            e.Graphics.DrawString(ValueHallazgo, font, Brushes.Blue, rectHallazgo, stringFormatHallazgo);
            e.Graphics.DrawRectangle(Pens.Black, rectHallazgo);
            y = y + 30;  //Agregar Línea
            y = y + 30;  //Agregar Línea    

            //línea 32 ********************************************************************************************************************************************

            y = y + 30;  //Agregar Línea
            e.Graphics.DrawString("   Comentario: ", font, Brushes.Black, new PointF(left + 15, top + y));

            Rectangle rectComentario = new Rectangle(left + 100, top + y - 5, 585, 60);
            StringFormat stringFormatComentario = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            string ValueComentario =_ServiceComponentList == null ? string.Empty : _ServiceComponentList.v_Comment;
            e.Graphics.DrawString(ValueComentario, font, Brushes.Blue, rectComentario, stringFormatHallazgo);
            e.Graphics.DrawRectangle(Pens.Black, rectComentario);

            #endregion
        }

        private int CalcularTotalFilasImprimirPD4()
        {
            int CountRowPrintH = 0;
            CountRowPrintH += 11; //_WasPrintTactoRectalSubTitle
            CountRowPrintH += 4; //_WasPrintOftalmologiaSubTitle
            CountRowPrintH += 5; //_WasPrintAudiometriaSubTitle
            CountRowPrintH += 5; //_WasPrintRxSubTitle
            CountRowPrintH += 5; //_WasPrintElectroCardiogramaSubTitle
            CountRowPrintH += 5; //_WasPrintOseoMuscularSubTitle
            CountRowPrintH += 5; //_WasPrintAlturaGeograficaSubTitle
            CountRowPrintH += 5; //_WasPrintAlturaEstructuralSubTitle
            CountRowPrintH += 5; //_WasPrintPruebaEsfuerzoSubTitle
            CountRowPrintH += 5; //_WasPrintErgonomicoSubTitle
            CountRowPrintH += 5; //_WasPrintNeurologicaSubTitle
            CountRowPrintH += 5; //_WasPrintTestRombergSubTitle
            CountRowPrintH += 5; //_WasPrintLaboratorioSubTitle
            CountRowPrintH += 5; //_WasPrintOdontologiaSubTitle
            CountRowPrintH += 5; //_WasPrintDermatologicoSubTitle
            //CountRowPrintH += 1; //_WasPrintPsicologiaSubTitle

            CountRowPrintH += 6; // _WasPrintTestClimaLaboralSubTitle;
            CountRowPrintH += 6; //_WasPrintTestAnsiedadSubTitle ;
            CountRowPrintH += 6; //_WasPrintTestDepresionSubTitle ;
            CountRowPrintH += 6; //_WasPrintTestEspacioConfinadoSubTitle ;
            CountRowPrintH += 6; //_WasPrintTestStressSubTitle;
            CountRowPrintH += 6; //_WasPrintTestFatigaSubTitle ;
            CountRowPrintH += 6; //_WasPrintTestSomnolenciaSubTitle;
            CountRowPrintH += 6; //_WasPrintTestEstiloPersonalidadSubTitle;
            CountRowPrintH += 6; //_WasPrintTestFobiasAlturasSubTitle;
            CountRowPrintH += 6; //_WasPrintTestLuriaDNASubTitle;
            CountRowPrintH += 6; //_WasPrintTestMotivacionesPsicologicasSubTitle;
            CountRowPrintH += 6; //_WasPrintTestdibujoFiguraHumanaSubTitle;
            CountRowPrintH += 6; //_WasPrintTestPersonaBajoLluviaSubTitle;
            CountRowPrintH += 6; //_WasPrintTestdibujoWarttergSubTitle;
            CountRowPrintH += 6; //_WasPrintTestCasaArbolSubTitle;
            CountRowPrintH += 6; //Conclusiones
            return CountRowPrintH;
        }

        private void printDocument4_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region VARIABLES LOCALES
            int y = 10; // posición con el Títutlo y el primer Subtítulo
            int TotalRow = CalcularTotalFilasImprimirPD4();

            #endregion

            #region CONF. PÁGINA

            int top, bottom, left, right;
            top = psp.PageSettings.Margins.Top;
            bottom = psp.PageSettings.Margins.Bottom;
            left = psp.PageSettings.Margins.Left;
            right = psp.PageSettings.Margins.Right;

            //formato de la fuente
            Font font = new Font("verdana", 7, FontStyle.Regular);
            Font fontGridHear = new Font("verdana", 8, FontStyle.Bold);
            Font fontGridRow = new Font("verdana", 7, FontStyle.Regular);

            //Agregar marco
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(50, 50, pagewidthPD4, pageheightPD4));

            //Cpaturar el tamaño de la columna mas larga
            SizeF sf = e.Graphics.MeasureString("12345678:", font);

            #endregion

            #region LOGO

            Bitmap myBitmap1 = new Bitmap(320, 110);
            //Logo Salus Laboris
            pictureBoxLogo.Image = Resources.logosalus;
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new System.Drawing.Size(285, 30);
            pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.DrawToBitmap(myBitmap1, new Rectangle(30, 10, 320, 110));
            e.Graphics.DrawImage(myBitmap1, 0, 0);
            myBitmap1.Dispose();
            #endregion

            #region EXAMENES

            //TACTO RECTAl
            if (_WasPrintTactoRectal == false)
            {
                if (_objServiceComponentFieldValuesListTactoRectal.Count != 0)
                {
                    string Value = string.Empty;
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Tacto Rectal: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));
                    //línea 
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Exploración: ", new Font("verdana", 7, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));
                    
                    //línea 25 ********************************************************************************************************************************************
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Región Perianal: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000234")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 95, top + y));

                    e.Graphics.DrawString("Esfinter Anal:", font, Brushes.Black, new PointF(left + 250, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000235")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));

                    e.Graphics.DrawString("Canal Anal:", font, Brushes.Black, new PointF(left + 470, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000236")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 525, top + y));

                    //línea 25 ********************************************************************************************************************************************
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Cara Posterior Próstata: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000237")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 95, top + y));

                    e.Graphics.DrawString("Base Vejiga:", font, Brushes.Black, new PointF(left + 250, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000238")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));

                    e.Graphics.DrawString("Vesículas Seminales:", font, Brushes.Black, new PointF(left + 470, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000239")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 525, top + y));


                    //línea 
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Evaluación: ", new Font("verdana", 7, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 25 ********************************************************************************************************************************************
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Ampolla Rectal: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000240")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 95, top + y));

                    e.Graphics.DrawString("Temperatura:", font, Brushes.Black, new PointF(left + 250, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000241")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));

                    e.Graphics.DrawString("fondo de Saco:", font, Brushes.Black, new PointF(left + 470, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000242")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 525, top + y));

                    //línea 25 ********************************************************************************************************************************************
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Mucosa Rectal: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000243")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 95, top + y));

                    e.Graphics.DrawString("Cara Anterior Recto:", font, Brushes.Black, new PointF(left + 250, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000244")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));


                    //línea 
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Próstata: ", new Font("verdana", 7, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 25 ********************************************************************************************************************************************
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Tamaño: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000245")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 95, top + y));

                    e.Graphics.DrawString("Superficie:", font, Brushes.Black, new PointF(left + 250, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000246")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));

                    e.Graphics.DrawString("Consistencia:", font, Brushes.Black, new PointF(left + 470, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000247")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 525, top + y));

                    //línea 25 ********************************************************************************************************************************************
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Surco Medio: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000248")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 95, top + y));

                    e.Graphics.DrawString("Dolor:", font, Brushes.Black, new PointF(left + 250, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000249")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 300, top + y));

                    e.Graphics.DrawString("Movilidad:", font, Brushes.Black, new PointF(left + 470, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTactoRectal.Find(p => p.v_ComponentFieldId == "N009-MF000000250")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 525, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Hallazgo: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    Value = _objServiceComponentFieldValuesListTactoRectal.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOftalmologia.Find(p => p.v_ComponentFieldId == "N009-MF000000206")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintTactoRectal = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            //OFTALMOLOGIA
            if (_WasPrintOftalmologiaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListOftalmologia.Count != 0)
                {
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Oftalmología: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOftalmologia.Find(p => p.v_ComponentFieldId == "N009-MF000000206") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOftalmologia.Find(p => p.v_ComponentFieldId == "N009-MF000000206")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOftalmologia.Find(p => p.v_ComponentFieldId == "N009-MF000000231") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOftalmologia.Find(p => p.v_ComponentFieldId == "N009-MF000000231")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea           
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea    
                }
                _WasPrintOftalmologiaSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            //AUDIOMETRIA
            if (_WasPrintAudiometriaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListAudiometria.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    //y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Audiometría: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAudiometria.Find(p => p.v_ComponentFieldId == "N009-MF000000111") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAudiometria.Find(p => p.v_ComponentFieldId == "N009-MF000000111")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAudiometria.Find(p => p.v_ComponentFieldId == "N009-MF000000226") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAudiometria.Find(p => p.v_ComponentFieldId == "N009-MF000000226")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea           
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea    

                }
                _WasPrintAudiometriaSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // RX
            if (_WasPrintRxSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListRx.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Radiografia de Torax: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListRx.Find(p => p.v_ComponentFieldId == "N009-MF000000224") == null ? string.Empty :  ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListRx.Find(p => p.v_ComponentFieldId == "N009-MF000000224")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
    
                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListRx.Find(p => p.v_ComponentFieldId == "N009-MF000000233") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListRx.Find(p => p.v_ComponentFieldId == "N009-MF000000233")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea           
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea    
                }
                _WasPrintRxSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            //ELECTROCARDIOGRAMA
            if (_WasPrintElectroCardiogramaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListElectro.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Electrocardiograma: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000225") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000225")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 80, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintElectroCardiogramaSubTitle = true;
            }

            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            //OSEO MUSCULAR
            if (_WasPrintOseoMuscularSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListOseoMuscular.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Oseo Múscular: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOseoMuscular.Find(p => p.v_ComponentFieldId == "N009-MF000000207") == null ? string.Empty:   ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOseoMuscular.Find(p => p.v_ComponentFieldId == "N009-MF000000207")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
           
                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOseoMuscular.Find(p => p.v_ComponentFieldId == "N009-MF000000232") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOseoMuscular.Find(p => p.v_ComponentFieldId == "N009-MF000000232")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintOseoMuscularSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            //ALTURA GEOGRAFICA
            if (_WasPrintAlturaGeograficaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListElectro.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Altura Geográfica: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000225") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000225")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227") == null ? string.Empty: ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintAlturaGeograficaSubTitle = true;
            }

            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            //ALTURA ESTRUCTURAL
            if (_WasPrintAlturaEstructuralSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListAlturaEstructural.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Altura Estructural: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //string Value = _objServiceComponentFieldValuesListAlturaEstructural.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAlturaEstructural.Find(p => p.v_ComponentFieldId == "N009-MF000000225")).v_Value1Name;
                    e.Graphics.DrawString("No hay campo Conclusión", font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    //((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListAlturaEstructural.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1
                    e.Graphics.DrawString("NADAAAA", font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintAlturaEstructuralSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            //ESFUERZO
            if (_WasPrintPruebaEsfuerzoSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListPruebaEsfuerzo.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Prueba de Esfuerzo: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListPruebaEsfuerzo.Find(p => p.v_ComponentFieldId == "N002-MF000000369") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListPruebaEsfuerzo.Find(p => p.v_ComponentFieldId == "N002-MF000000369")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString( (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListPruebaEsfuerzo.Find(p => p.v_ComponentFieldId == "N002-MF000000370") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListPruebaEsfuerzo.Find(p => p.v_ComponentFieldId == "N002-MF000000370")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintPruebaEsfuerzoSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            //ERGONIMICA
            if (_WasPrintErgonomicoSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListEvaluacionErgonomica.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Ergonómica: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value =(ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListEvaluacionErgonomica.Find(p => p.v_ComponentFieldId == "N009-MF000000317") == null ? string.Empty :  ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListEvaluacionErgonomica.Find(p => p.v_ComponentFieldId == "N009-MF000000317")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString( (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListEvaluacionErgonomica.Find(p => p.v_ComponentFieldId == "N009-MF000000316") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListEvaluacionErgonomica.Find(p => p.v_ComponentFieldId == "N009-MF000000316")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintErgonomicoSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            // NEUROLOGICA
            if (_WasPrintNeurologicaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListEvaluacionNeurologica.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Neurología: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListEvaluacionNeurologica.Find(p => p.v_ComponentFieldId == "N009-MF000000353") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListEvaluacionNeurologica.Find(p => p.v_ComponentFieldId == "N009-MF000000353")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListEvaluacionNeurologica.Find(p => p.v_ComponentFieldId == "N009-MF000000354") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListEvaluacionNeurologica.Find(p => p.v_ComponentFieldId == "N009-MF000000354")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintNeurologicaSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            //ROMBERG
            if (_WasPrintTestRombergSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestRomberg.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test de Romberg: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestRomberg.Find(p => p.v_ComponentFieldId == "N009-MF000000374") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestRomberg.Find(p => p.v_ComponentFieldId == "N009-MF000000374")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestRomberg.Find(p => p.v_ComponentFieldId == "N009-MF000000375") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestRomberg.Find(p => p.v_ComponentFieldId == "N009-MF000000375")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintTestRombergSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            // LABORATORIO
            if (_WasPrintLaboratorioSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListLaboratorio.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Laboratorio: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListLaboratorio.Find(p => p.v_ComponentFieldId == "N009-MF000000273") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListLaboratorio.Find(p => p.v_ComponentFieldId == "N009-MF000000273")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListLaboratorio.Find(p => p.v_ComponentFieldId == "N009-MF000000272") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListLaboratorio.Find(p => p.v_ComponentFieldId == "N009-MF000000272")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintLaboratorioSubTitle = true;
            }

            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }


            //ODONTOLOGIA
            if (_WasPrintOdontologiaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListOdontologia.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Odontología: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value =   (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOdontologia.Find(p => p.v_ComponentFieldId == "N009-MF000000376") == null ? string.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOdontologia.Find(p => p.v_ComponentFieldId == "N009-MF000000376")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOdontologia.Find(p => p.v_ComponentFieldId == "N009-MF000000377") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListOdontologia.Find(p => p.v_ComponentFieldId == "N009-MF000000377")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintOdontologiaSubTitle = true;
            }

            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }
            //DERMATOLOGICO
            if (_WasPrintDermatologicoSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListDermatologico.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Dermatológico: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDermatologico.Find(p => p.v_ComponentFieldId == "N009-MF000000406") == null ? string.Empty  :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDermatologico.Find(p => p.v_ComponentFieldId == "N009-MF000000406")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDermatologico.Find(p => p.v_ComponentFieldId == "N009-MF000000407") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDermatologico.Find(p => p.v_ComponentFieldId == "N009-MF000000407")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                _WasPrintDermatologicoSubTitle = true;
            }

            ////PSICOLOGÍA
            //if (_WasPrintPsicologiaSubTitle == false)
            //{
            //    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
            //    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
            //    e.Graphics.DrawString("   EXAMENES PSICOLÓGICOS: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));
                
            //    _WasPrintPsicologiaSubTitle = true;
            //}
            //if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            //{
            //    NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
            //    return;
            //}

            // TEST CLIMA LABORAL
            if (_WasPrintTestClimaLaboralSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestClimaLaboral.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Clima Laboral: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestClimaLaboral.Find(p => p.v_ComponentFieldId == "N009-MF000000191") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestClimaLaboral.Find(p => p.v_ComponentFieldId == "N009-MF000000191")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestClimaLaboral.Find(p => p.v_ComponentFieldId == "N009-MF000000192") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestClimaLaboral.Find(p => p.v_ComponentFieldId == "N009-MF000000192")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestClimaLaboral.Find(p => p.v_ComponentFieldId == "N009-MF000000378") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestClimaLaboral.Find(p => p.v_ComponentFieldId == "N009-MF000000378")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }

                _WasPrintTestClimaLaboralSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // TEST DE ANSIEDAD
            if (_WasPrintTestAnsiedadSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestAnsiedad.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Ansiedad: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestAnsiedad.Find(p => p.v_ComponentFieldId == "N009-MF000000193") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestAnsiedad.Find(p => p.v_ComponentFieldId == "N009-MF000000193")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestAnsiedad.Find(p => p.v_ComponentFieldId == "N009-MF000000194") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestAnsiedad.Find(p => p.v_ComponentFieldId == "N009-MF000000194")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestAnsiedad.Find(p => p.v_ComponentFieldId == "N009-MF000000379") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestAnsiedad.Find(p => p.v_ComponentFieldId == "N009-MF000000379")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
               

                _WasPrintTestAnsiedadSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // TEST DE DEPRESIÓN
            if (_WasPrintTestDepresionSubTitle == false)
            {

                if (_objServiceComponentFieldValuesListTestDepresion.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Depresión: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestDepresion.Find(p => p.v_ComponentFieldId == "N009-MF000000195") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestDepresion.Find(p => p.v_ComponentFieldId == "N009-MF000000195")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestDepresion.Find(p => p.v_ComponentFieldId == "N009-MF000000196") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestDepresion.Find(p => p.v_ComponentFieldId == "N009-MF000000196")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestDepresion.Find(p => p.v_ComponentFieldId == "N009-MF000000380") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestDepresion.Find(p => p.v_ComponentFieldId == "N009-MF000000380")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea

                }
               
                _WasPrintTestDepresionSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // TEST ESPACIOS CONFINADOS
            if (_WasPrintTestEspacioConfinadoSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestEspacioConfinados.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Espacios Confinados: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEspacioConfinados.Find(p => p.v_ComponentFieldId == "N009-MF000000388") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEspacioConfinados.Find(p => p.v_ComponentFieldId == "N009-MF000000388")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEspacioConfinados.Find(p => p.v_ComponentFieldId == "N009-MF000000389") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEspacioConfinados.Find(p => p.v_ComponentFieldId == "N009-MF000000389")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEspacioConfinados.Find(p => p.v_ComponentFieldId == "N009-MF000000390") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEspacioConfinados.Find(p => p.v_ComponentFieldId == "N009-MF000000390")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
               

                _WasPrintTestEspacioConfinadoSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // TEST DE ESTRESS
            if (_WasPrintTestStressSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestEstress.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Estress: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEstress.Find(p => p.v_ComponentFieldId == "N002-MF000000340") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEstress.Find(p => p.v_ComponentFieldId == "N002-MF000000340")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEstress.Find(p => p.v_ComponentFieldId == "N009-MF000000188") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEstress.Find(p => p.v_ComponentFieldId == "N009-MF000000188")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEstress.Find(p => p.v_ComponentFieldId == "N009-MF000000381") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestEstress.Find(p => p.v_ComponentFieldId == "N009-MF000000381")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);
                }
              
                y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea

                _WasPrintTestStressSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // TEST DE FATIGA
            if (_WasPrintTestFatigaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestFatiga.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Fatiga: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFatiga.Find(p => p.v_ComponentFieldId == "N002-MF000000271") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFatiga.Find(p => p.v_ComponentFieldId == "N002-MF000000271")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFatiga.Find(p => p.v_ComponentFieldId == "N009-MF000000189") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFatiga.Find(p => p.v_ComponentFieldId == "N009-MF000000189")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFatiga.Find(p => p.v_ComponentFieldId == "N009-MF000000382") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFatiga.Find(p => p.v_ComponentFieldId == "N009-MF000000382")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea

                }

               
                _WasPrintTestFatigaSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            //TEST SOMNOLENCIA
            if (_WasPrintTestSomnolenciaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestSomnolencia.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Somnolencia: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestSomnolencia.Find(p => p.v_ComponentFieldId == "N002-MF000000339") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestSomnolencia.Find(p => p.v_ComponentFieldId == "N002-MF000000339")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestSomnolencia.Find(p => p.v_ComponentFieldId == "N009-MF000000190") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestSomnolencia.Find(p => p.v_ComponentFieldId == "N009-MF000000190")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestSomnolencia.Find(p => p.v_ComponentFieldId == "N009-MF000000383") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestSomnolencia.Find(p => p.v_ComponentFieldId == "N009-MF000000383")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
               

                _WasPrintTestSomnolenciaSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // ESTILO DE PERSONALIDAD
            if (_WasPrintTestEstiloPersonalidadSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestPersonalidad.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Estilo de Personalidad: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestPersonalidad.Find(p => p.v_ComponentFieldId == "N009-MF000000197") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestPersonalidad.Find(p => p.v_ComponentFieldId == "N009-MF000000197")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestPersonalidad.Find(p => p.v_ComponentFieldId == "N009-MF000000198") == null  ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestPersonalidad.Find(p => p.v_ComponentFieldId == "N009-MF000000198")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestPersonalidad.Find(p => p.v_ComponentFieldId == "N009-MF000000384") ==null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestPersonalidad.Find(p => p.v_ComponentFieldId == "N009-MF000000384")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea

                }
               
                _WasPrintTestEstiloPersonalidadSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // FOBIA A LAS ALTURAS
            if (_WasPrintTestFobiasAlturasSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestFobiasAlturas.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Fobia a las Alturas: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFobiasAlturas.Find(p => p.v_ComponentFieldId == "N009-MF000000199") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFobiasAlturas.Find(p => p.v_ComponentFieldId == "N009-MF000000199")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFobiasAlturas.Find(p => p.v_ComponentFieldId == "N009-MF000000200") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFobiasAlturas.Find(p => p.v_ComponentFieldId == "N009-MF000000200")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFobiasAlturas.Find(p => p.v_ComponentFieldId == "N009-MF000000385") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestFobiasAlturas.Find(p => p.v_ComponentFieldId == "N009-MF000000385")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
               

                _WasPrintTestFobiasAlturasSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // LURIA DNA
            if (_WasPrintTestLuriaDNASubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestLuriaDNA.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Luria DNA: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestLuriaDNA.Find(p => p.v_ComponentFieldId == "N009-MF000000201") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestLuriaDNA.Find(p => p.v_ComponentFieldId == "N009-MF000000201")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestLuriaDNA.Find(p => p.v_ComponentFieldId == "N009-MF000000202") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestLuriaDNA.Find(p => p.v_ComponentFieldId == "N009-MF000000202")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestLuriaDNA.Find(p => p.v_ComponentFieldId == "N009-MF000000386") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestLuriaDNA.Find(p => p.v_ComponentFieldId == "N009-MF000000386")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
               

                _WasPrintTestLuriaDNASubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // MOTIVACIONES PSICOLÓGICAS
            if (_WasPrintTestMotivacionesPsicologicasSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListTestMotivacionesPsicologicas.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Motivaciones Psicosociales: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestMotivacionesPsicologicas.Find(p => p.v_ComponentFieldId == "N009-MF000000203") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestMotivacionesPsicologicas.Find(p => p.v_ComponentFieldId == "N009-MF000000203")).v_Value1;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestMotivacionesPsicologicas.Find(p => p.v_ComponentFieldId == "N009-MF000000204") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestMotivacionesPsicologicas.Find(p => p.v_ComponentFieldId == "N009-MF000000204")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestMotivacionesPsicologicas.Find(p => p.v_ComponentFieldId == "N009-MF000000387") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListTestMotivacionesPsicologicas.Find(p => p.v_ComponentFieldId == "N009-MF000000387")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea

                }
               
                _WasPrintTestMotivacionesPsicologicasSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // FIGURA HUMANA
            if (_WasPrintTestdibujoFiguraHumanaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListDibujoFiguraHumana.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Dibujo Figura Humana: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //string Value = _objServiceComponentFieldValuesListDibujoFiguraHumana.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoFiguraHumana.Find(p => p.v_ComponentFieldId == "N009-MF000000225")).v_Value1;
                    e.Graphics.DrawString("No hay Campo", font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoFiguraHumana.Find(p => p.v_ComponentFieldId == "N009-MF000000395") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoFiguraHumana.Find(p => p.v_ComponentFieldId == "N009-MF000000395")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoFiguraHumana.Find(p => p.v_ComponentFieldId == "N009-MF000000396") == null  ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoFiguraHumana.Find(p => p.v_ComponentFieldId == "N009-MF000000396")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
               

                _WasPrintTestdibujoFiguraHumanaSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // PERSONA BAJO LA LLUVIA
            if (_WasPrintTestPersonaBajoLluviaSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListDibujoPersonaLluvia.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Dibujo Persona Bajo la Lluvia: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //string Value = _objServiceComponentFieldValuesListDibujoPersonaLluvia.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoPersonaLluvia.Find(p => p.v_ComponentFieldId == "N009-MF000000225")).v_Value1;
                    e.Graphics.DrawString("No hay campo", font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoPersonaLluvia.Find(p => p.v_ComponentFieldId == "N009-MF000000397") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoPersonaLluvia.Find(p => p.v_ComponentFieldId == "N009-MF000000397")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoPersonaLluvia.Find(p => p.v_ComponentFieldId == "N009-MF000000398") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoPersonaLluvia.Find(p => p.v_ComponentFieldId == "N009-MF000000398")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea

                }
               
                _WasPrintTestPersonaBajoLluviaSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            // DIBUJO DE WARTTEG
            if (_WasPrintTestdibujoWarttergSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListDibujoWartteg.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Dibujo Wartteg: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //string Value = _objServiceComponentFieldValuesListDibujoWartteg.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoWartteg.Find(p => p.v_ComponentFieldId == "N009-MF000000225")).v_Value1;
                    e.Graphics.DrawString("No hay campo", font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoWartteg.Find(p => p.v_ComponentFieldId == "N009-MF000000393") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoWartteg.Find(p => p.v_ComponentFieldId == "N009-MF000000393")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //e.Graphics.DrawString(((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListElectro.Find(p => p.v_ComponentFieldId == "N009-MF000000227")).v_Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoWartteg.Find(p => p.v_ComponentFieldId == "N009-MF000000394") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoWartteg.Find(p => p.v_ComponentFieldId == "N009-MF000000394")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                

                _WasPrintTestdibujoWarttergSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }

            //DIBUJO CASA Y ÁRBOL
            if (_WasPrintTestCasaArbolSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListDibujoCasaArbol.Count !=0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Test Dibujo Casa y Árbol: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Puntaje: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    //string Value = _objServiceComponentFieldValuesListDibujoCasaArbol.Count == 0 ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N009-MF000000225")).v_Value1;
                    e.Graphics.DrawString("No hay Valor", font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N009-MF000000391") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N009-MF000000391")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 40, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N009-MF000000392") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N009-MF000000392")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }
                

                _WasPrintTestCasaArbolSubTitle = true;
            }
            if (CurrentRowVerticalPD4_ > NroLineasPaginaVerticalPD4_)
            {
                NewPageVertical(e, TotalRow, CurrentRowVerticalPD4_);
                return;
            }


            //DIBUJO CASA Y ÁRBOL
            if (_WasPrintTestConclusionesSubTitle == false)
            {
                if (_objServiceComponentFieldValuesListDibujoCasaArbol.Count != 0)
                {
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
                    e.Graphics.DrawString("   Conclusiones: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones Área Cognitiva: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N002-MF000000336") == null ? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N002-MF000000336")).v_Value1Name;
                    e.Graphics.DrawString(Value, font, Brushes.Blue, new PointF(left + sf.Width + 120, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Conclusiones Área Emocional: ", font, Brushes.Black, new PointF(left + 15, top + y));
                    string Value1 = (ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N002-MF000000337") == null? String.Empty : ((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N002-MF000000337")).v_Value1Name;
                    e.Graphics.DrawString(Value1, font, Brushes.Blue, new PointF(left + sf.Width + 120, top + y));

                    //línea 16
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    e.Graphics.DrawString("   Descripción: ", font, Brushes.Black, new PointF(left + 15, top + y));

                    Rectangle rect1 = new Rectangle(left + (int)sf.Width + 40, (int)(top + y), 585, 60);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N002-MF000000338") == null ? string.Empty :((ServiceComponentFieldValuesList)_objServiceComponentFieldValuesListDibujoCasaArbol.Find(p => p.v_ComponentFieldId == "N002-MF000000338")).v_Value1, font, Brushes.Blue, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);

                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                    y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
                }


                _WasPrintTestConclusionesSubTitle = true;
            }
            //línea 14
            //y = y + 30; CurrentRowVertical_ += 1; //Agregar Línea
            //e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, top + y + 15), new PointF(750, top + y + 15));
            #endregion

          
        }

        private void printDocument5_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region VARIABLES LOCALES
            int y = 10; // posición con el Títutlo y el primer Subtítulo

            #endregion

            #region CONF. PÁGINA

            int top, bottom, left, right;
            top = psp.PageSettings.Margins.Top;
            bottom = psp.PageSettings.Margins.Bottom;
            left = psp.PageSettings.Margins.Left;
            right = psp.PageSettings.Margins.Right;

            //formato de la fuente
            Font font = new Font("verdana", 7, FontStyle.Regular);
            Font fontGridHear = new Font("verdana", 8, FontStyle.Bold);
            Font fontGridRow = new Font("verdana", 7, FontStyle.Regular);

            //Agregar marco
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(50, 50, pageheightPD5,pagewidthPD5 ));

            //Cpaturar el tamaño de la columna mas larga
            SizeF sf = e.Graphics.MeasureString("12345678:", font);

            #endregion

            #region LOGO

            Bitmap myBitmap1 = new Bitmap(320, 110);
            //Logo Salus Laboris
            pictureBoxLogo.Image = Resources.logosalus;
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new System.Drawing.Size(285, 30);
            pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.DrawToBitmap(myBitmap1, new Rectangle(30, 10, 320, 110));
            e.Graphics.DrawImage(myBitmap1, 0, 0);
            myBitmap1.Dispose();
            #endregion

            #region DIAGNÓSTICOS
            #region CONF. DIAGNÓSTICOS
            SizeF sHeaderExamen = e.Graphics.MeasureString("EXAMEN123456", fontGridHear);
            SizeF sHeaderDiagnostico = e.Graphics.MeasureString("DIAGNÓSTICO123456789012", fontGridHear);
            SizeF sHeaderCie10 = e.Graphics.MeasureString("CIE10123456", fontGridHear);
            SizeF sHeaderRecomendaciones = e.Graphics.MeasureString("RECOMENDACIONES12345678901234567890", fontGridHear);
            SizeF sHeaderRestricciones = e.Graphics.MeasureString("RESTRICCIONES12345612345678901234567890", fontGridHear);

            int iDiagnostico = 0, rDiagnostico = 0;
            SizeF smDiagnostico = e.Graphics.MeasureString("yyyyyyyyyyy", font);
            //bool AcaboImpresionGridHabitosNocivos = false;
            #endregion

                
            y = y + 30;
           
            e.Graphics.DrawString("   VIII. Diagnósticos: ", new Font("verdana", 9, FontStyle.Bold), Brushes.Black, new PointF(left + 15, top + y));

            
            y = y + 30;
          
            PropertyInfo[] Props = typeof(DiagnosticRepositoryList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                if (prop.Name == "v_ComponentName")
                {
                    Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderExamen.Width, 30);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    e.Graphics.DrawString("EXAMEN", fontGridHear, Brushes.Black, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);
                }
                else if (prop.Name == "v_DiseasesName")
                {
                    Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderExamen.Width, (int)(top + y), (int)sHeaderDiagnostico.Width, 30);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    e.Graphics.DrawString("DIAGNÓSTICO", fontGridHear, Brushes.Black, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);
                }
                else if (prop.Name == "v_Cie10")
                {                    
                    Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderExamen.Width + (int)sHeaderDiagnostico.Width, (int)(top + y), (int)sHeaderCie10.Width, 30);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    e.Graphics.DrawString("CIE10", fontGridHear, Brushes.Black, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);
                }
                else if (prop.Name == "v_RecomendationsName")
                {                  
                    Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderExamen.Width + (int)sHeaderDiagnostico.Width + (int)sHeaderCie10.Width, (int)(top + y), (int)sHeaderRecomendaciones.Width, 30);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    e.Graphics.DrawString("RECOMENDACIONES", fontGridHear, Brushes.Black, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);
                }
                else if (prop.Name == "v_RestrictionsName")
                {                   
                    Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderExamen.Width + (int)sHeaderDiagnostico.Width + (int)sHeaderCie10.Width + (int)sHeaderRecomendaciones.Width, (int)(top + y), (int)sHeaderRestricciones.Width, 30);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    e.Graphics.DrawString("RESTRICCIONES", fontGridHear, Brushes.Black, rect1, stringFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect1);
                }
            }

            for (rDiagnostico = FilaImpresaDiagnosticos_; rDiagnostico < _DiagnosticRepositoryList.Count; rDiagnostico++)
            {
                y = y + 30;
                foreach (PropertyInfo prop in Props)
                {

                    var item = _DiagnosticRepositoryList[rDiagnostico];
                    //Setting column names as Property names
                    if (prop.Name == "v_ComponentName")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15), (int)(top + y), (int)sHeaderExamen.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_DiseasesName")
                    {
                         var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderExamen.Width, (int)(top + y), (int)sHeaderDiagnostico.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_Cie10")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderExamen.Width + (int)sHeaderDiagnostico.Width, (int)(top + y), (int)sHeaderCie10.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_RecomendationsName")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderExamen.Width + (int)sHeaderDiagnostico.Width + (int)sHeaderCie10.Width, (int)(top + y), (int)sHeaderRecomendaciones.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                    else if (prop.Name == "v_RestrictionsName")
                    {
                        var value = prop.GetValue(item, null);
                        Rectangle rect1 = new Rectangle((int)(left + 15) + (int)sHeaderExamen.Width + (int)sHeaderDiagnostico.Width + (int)sHeaderCie10.Width + (int)sHeaderRecomendaciones.Width, (int)(top + y), (int)sHeaderRestricciones.Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(value.ToString(), fontGridRow, Brushes.Black, rect1, stringFormat);
                        e.Graphics.DrawRectangle(Pens.Black, rect1);
                    }
                }
                
            }


            #endregion

            #region FIRMA TRABAJADOR

            Bitmap myBitmap2 = new Bitmap(700, 700);
            //Foto Paciente            
            pictureBoxFirmaTrabajador.Image = Common.Utils.BytesArrayToImage(_pacientList.b_RubricImage, pictureBoxFirmaTrabajador);
            //pictureBoxFirmaTrabajador.Image = Resources.firma;
            pictureBoxFirmaTrabajador.Name = "pictureBoxFirmaTrabajador";
            pictureBoxFirmaTrabajador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxFirmaTrabajador.Size = new System.Drawing.Size(200, 55);
            pictureBoxFirmaTrabajador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxFirmaTrabajador.DrawToBitmap(myBitmap2, new Rectangle(350, 440, 200, 100));

            e.Graphics.DrawImage(myBitmap2, 100, 200);
            myBitmap2.Dispose();

            #endregion

            #region HUELLA TRABAJADOR

            Bitmap myBitmap3 = new Bitmap(900, 700);
            //Foto Paciente            
            pictureBoxHuellaTrabajador.Image = Common.Utils.BytesArrayToImage(_pacientList.b_FingerPrintImage, pictureBoxHuellaTrabajador);
            //pictureBoxHuellaTrabajador.Image = Resources.huella;
            pictureBoxHuellaTrabajador.Name = "pictureBoxHuellaTrabajador";
            pictureBoxHuellaTrabajador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxHuellaTrabajador.Size = new System.Drawing.Size(35, 35);
            pictureBoxHuellaTrabajador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxHuellaTrabajador.DrawToBitmap(myBitmap3, new Rectangle(750, 440, 55, 55));


            e.Graphics.DrawImage(myBitmap3, 100, 200);
            myBitmap3.Dispose();

            #endregion

            #region FIRMA MÉDICO

            Bitmap myBitmap4 = new Bitmap(900, 700);
            //Foto Paciente            
            //pictureBoxFirmaMedico.Image = Common.Utils.BytesArrayToImage(_pacientList.b_Photo, pictureBoxFoto);
            pictureBoxFirmaMedico.Image = Resources.firma_doctor;
            pictureBoxFirmaMedico.Name = "pictureBoxFirmaMédico";
            pictureBoxFirmaMedico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxFirmaMedico.Size = new System.Drawing.Size(200, 40);
            pictureBoxFirmaMedico.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxFirmaMedico.DrawToBitmap(myBitmap4, new Rectangle(750, 480, 200, 40));

            e.Graphics.DrawImage(myBitmap4, 100, 200);
            myBitmap4.Dispose();

            #endregion

            #region ÚLTIMOS DATOS
            
            e.Graphics.DrawLine(Pens.Black, new PointF(left + 15, 660), new PointF(1070, 660));
            e.Graphics.DrawString("   Nombres y Apellidos ", font, Brushes.Black, new PointF(left + 15, 670));
            e.Graphics.DrawString(_pacientList.v_FirstName + " " + _pacientList.v_FirstLastName + " " + _pacientList.v_SecondLastName, font, Brushes.Blue, new PointF(left + sf.Width + 85, 670));
            e.Graphics.DrawString("Firma ", font, Brushes.Black, new PointF(left + sf.Width + 270, 670));
            e.Graphics.DrawString("Huella Dactilar", font, Brushes.Black, new PointF(left + sf.Width + 575, 670));

            //línea 16
            y = y + 30; CurrentRowVerticalPD4_ += 1; //Agregar Línea
            e.Graphics.DrawString("Trabajador", font, Brushes.Black, new PointF(left + sf.Width + 270, 700));
            e.Graphics.DrawString("   Nro. Dni ", font, Brushes.Black, new PointF(left + 15, 700));
            e.Graphics.DrawString("Indice Derecho", font, Brushes.Black, new PointF(left + sf.Width + 575, 700));
            e.Graphics.DrawString(_pacientList.v_DocNumber, font, Brushes.Blue, new PointF(left + sf.Width + 20, 700));

            //línea 16
            //y = y + 30; CurrentRowVertical_ += 1; //Agregar Línea
            e.Graphics.DrawString("   Aptitud ", font, Brushes.Black, new PointF(left + 15, 730));
            e.Graphics.DrawString(_objServiceList.v_AptitudeStatusName.ToString(), font, Brushes.Blue, new PointF(left + sf.Width + 20, 730));

            e.Graphics.DrawString("Firma y Sello Médico", font, Brushes.Black, new PointF(left + sf.Width + 575, 730));

            #endregion
        }

        private void print_Click()
        {
            psp.PageSettings = printDocument1.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Landscape = false;
            printDocument1.DefaultPageSettings.Margins.Left = 60;
            printDocument1.DefaultPageSettings.Margins.Right = 60;
            printDocument1.DefaultPageSettings.Margins.Top = 60;
            printDocument1.DefaultPageSettings.Margins.Bottom = 60;
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            pagewidthPD1 = printDocument1.DefaultPageSettings.PaperSize.Width - printDocument1.DefaultPageSettings.Margins.Left - printDocument1.DefaultPageSettings.Margins.Right;
            pageheightPD1 = printDocument1.DefaultPageSettings.PaperSize.Height - printDocument1.DefaultPageSettings.Margins.Top - printDocument1.DefaultPageSettings.Margins.Bottom;


            psp.PageSettings = printDocument2.DefaultPageSettings;
            printDocument2.DefaultPageSettings.Landscape = true;
            printDocument2.DefaultPageSettings.Margins.Left = 60;
            printDocument2.DefaultPageSettings.Margins.Right = 60;
            printDocument2.DefaultPageSettings.Margins.Top = 60;
            printDocument2.DefaultPageSettings.Margins.Bottom = 60;
            printDocument2.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            pagewidthPD2 = printDocument2.DefaultPageSettings.PaperSize.Width - printDocument2.DefaultPageSettings.Margins.Left - printDocument2.DefaultPageSettings.Margins.Right;
            pageheightPD2 = printDocument2.DefaultPageSettings.PaperSize.Height - printDocument2.DefaultPageSettings.Margins.Top - printDocument2.DefaultPageSettings.Margins.Bottom;


            psp.PageSettings = printDocument3.DefaultPageSettings;
            printDocument3.DefaultPageSettings.Landscape = false;
            printDocument3.DefaultPageSettings.Margins.Left = 60;
            printDocument3.DefaultPageSettings.Margins.Right = 60;
            printDocument3.DefaultPageSettings.Margins.Top = 60;
            printDocument3.DefaultPageSettings.Margins.Bottom = 60;
            printDocument3.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            pagewidthPD3 = printDocument3.DefaultPageSettings.PaperSize.Width - printDocument3.DefaultPageSettings.Margins.Left - printDocument3.DefaultPageSettings.Margins.Right;
            pageheightPD3 = printDocument3.DefaultPageSettings.PaperSize.Height - printDocument3.DefaultPageSettings.Margins.Top - printDocument3.DefaultPageSettings.Margins.Bottom;

            psp.PageSettings = printDocument4.DefaultPageSettings;
            printDocument4.DefaultPageSettings.Landscape = false;
            printDocument4.DefaultPageSettings.Margins.Left = 60;
            printDocument4.DefaultPageSettings.Margins.Right = 60;
            printDocument4.DefaultPageSettings.Margins.Top = 60;
            printDocument4.DefaultPageSettings.Margins.Bottom = 60;
            printDocument4.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            pagewidthPD4 = printDocument4.DefaultPageSettings.PaperSize.Width - printDocument4.DefaultPageSettings.Margins.Left - printDocument4.DefaultPageSettings.Margins.Right;
            pageheightPD4 = printDocument4.DefaultPageSettings.PaperSize.Height - printDocument4.DefaultPageSettings.Margins.Top - printDocument4.DefaultPageSettings.Margins.Bottom;


            psp.PageSettings = printDocument5.DefaultPageSettings;
            printDocument5.DefaultPageSettings.Landscape = true;
            printDocument5.DefaultPageSettings.Margins.Left = 60;
            printDocument5.DefaultPageSettings.Margins.Right = 60;
            printDocument5.DefaultPageSettings.Margins.Top = 60;
            printDocument5.DefaultPageSettings.Margins.Bottom = 60;
            printDocument5.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            pagewidthPD5 = printDocument5.DefaultPageSettings.PaperSize.Width - printDocument5.DefaultPageSettings.Margins.Left - printDocument5.DefaultPageSettings.Margins.Right;
            pageheightPD5 = printDocument5.DefaultPageSettings.PaperSize.Height - printDocument5.DefaultPageSettings.Margins.Top - printDocument5.DefaultPageSettings.Margins.Bottom;


            
            MultiDoc = new MultiPrintDocument(new PrintDocument[] { printDocument1, printDocument2, printDocument3, printDocument4, printDocument5 });
            //MultiDoc = new MultiPrintDocument(new PrintDocument[] { printDocument5 });


            //printPreviewDialog1.Document = MultiDoc;
            //printPreviewDialog1.ShowDialog();

            //foreach (var item in PrinterSettings.InstalledPrinters)
            //{
            //    //this.listBox1.Items.Add(item.ToString());
            //}

            if (_TypePrinter == (int)TypePrinter.Image)
            {
                MultiDoc.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                MultiDoc.Print();
            }
            else if (_TypePrinter == (int)TypePrinter.Printer)
            {
                //printPreviewDialog1.Document = MultiDoc;
                ////printPreviewDialog1.ShowDialog();
                //MultiDoc.Print();

                PrintDialog1.Document = MultiDoc;
                PrintDialog1.ShowDialog();

            }
           

           
            //MultiDoc.Print();

        }

        private void NewPageVertical(System.Drawing.Printing.PrintPageEventArgs e, int TotalRow, int Currentrow)
        {
            int TotalRow_ = TotalRow;
            int CurrentRow_ = Currentrow;

            if (CurrentRow_ >= TotalRow_)
            {
                e.HasMorePages = false;
                CurrentRow_ = 0;
            }
            else
            {
                e.HasMorePages = true;
                NroLineasPaginaVerticalPD4_ += 28;
            }
        }

        private void NewPageHorizontal(System.Drawing.Printing.PrintPageEventArgs e, int TotalFilasCalculadas, int LineaActualHorizontal)
        {
            int TotalFilasCalculadas_ = TotalFilasCalculadas;
            int LineaActualHorizontalLocal_ = LineaActualHorizontal;

            if (LineaActualHorizontalLocal_ >= TotalFilasCalculadas_)
            {
                e.HasMorePages = false;
                LineaActualHorizontalLocal_ = 0;
            }
            else
            {
                e.HasMorePages = true;
                TotalLineasHorizontalPD2_ += 18;
            }
        }

    }
}
