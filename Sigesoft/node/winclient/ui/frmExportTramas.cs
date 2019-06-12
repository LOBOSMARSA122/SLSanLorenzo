using Sigesoft.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmExportTramas : Form
    {
        private List<string> DiagnosticList = new List<string>();
        private List<string> DiagnosticCount = new List<string>();
        private List<string> AgeGenderList = new List<string>();
        private List<string> AgeGenderCount = new List<string>();
        private List<string> AgeGenderListEmer = new List<string>();
        private List<string> AgeGenderCountEmer = new List<string>();
        private List<string> DiagnosticListEmer = new List<string>();
        private List<string> DiagnosticCountEmer = new List<string>();
        private List<string> UPSIngresoList = new List<string>();
        private List<string> UPSIngresoCount = new List<string>();
        private List<string> FechasLis = new List<string>();
        private List<string> FechasCount = new List<string>();
        private List<string> UPSEgresoList = new List<string>();
        private List<string> UPSEgresoCount = new List<string>();
        private string ugi;
        private string anio;
        private string mes;
        private string NombreCarpeta;
        private string rutaapp;
        private string HospiIngreso;
        private string HospiEgreso;
        private List<string> UPSList = new List<string>();
        private List<string> UPSCount = new List<string>();
        private List<string> HospiList = new List<string>();
        private List<string> HospiCount = new List<string>();
        private List<string> HospiFallecidoList = new List<string>();
        private List<string> HospiFallecidoCount = new List<string>();
        private List<string> Estancia = new List<string>();
        private List<string> PartoComplicacionList = new List<string>();
        private List<string> PartoComplicacionCount = new List<string>();
        private List<string> PartoComplicacionVivosList = new List<string>();
        private List<string> PartoComplicacionVivosCount = new List<string>();
        private List<string> ProcedimientoList = new List<string>();
        private List<string> ProcedimientoCount = new List<string>();
        private List<string> ProgMayorList = new List<string>();
        private int ProgMayorHorasProg = 0;
        private int ProgMayorHorasEfect = 0;
        private int ProgMayorHorasAct = 0;
        private List<string> ProgMayorCount = new List<string>();
        private List<string> ProgMenorList = new List<string>();
        private int ProgMenorHorasProg = 0;
        private int ProgMenorHorasEfect = 0;
        private int ProgMenorHorasAct = 0;
        private List<string> ProgMenorCount = new List<string>();
        private List<string> EmergMayorList = new List<string>();
        private int EmerMayorHorasProg = 0;
        private int EmerMayorHorasEfect = 0;
        private int EmerMayorHorasAct = 0;
        private List<string> EmergMayorCount = new List<string>();
        private List<string> EmergMenorList = new List<string>();
        private int EmerMenorHorasProg = 0;
        private int EmerMenorHorasEfect = 0;
        private int EmerMenorHorasAct = 0;
        private List<string> EmergMenorCount = new List<string>();
        private List<string> SuspMayorList = new List<string>();
        private int SusMayorHorasProg = 0;
        private int SusMayorHorasEfect = 0;
        private int SusMayorHorasAct = 0;
        private List<string> SuspMayorCount = new List<string>();
        private List<string> SuspMenorList = new List<string>();
        private int SusMenorHorasProg = 0;
        private int SusMenorHorasEfect = 0;
        private int SusMenorHorasAct = 0;
        private List<string> SuspMenorCount = new List<string>();


        public frmExportTramas()
        {
            InitializeComponent();
        }

        private void frmExportTramas_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbAnio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 355, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbmes, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 356, "i_ParameterId"), DropDownListAction.Select);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            #region Crear Carpeta
            anio = cbAnio.Text; mes = ""; ugi = txtUgi.Text;
            if (cbmes.SelectedValue.ToString().Length == 1) { mes = "0" + cbmes.SelectedValue.ToString(); }
            else { mes = cbmes.SelectedValue.ToString(); }
            NombreCarpeta = anio + mes + "_SUSALUD";
            rutaapp = Common.Utils.GetApplicationConfigValue("rutaTramas").ToString();
            CrearCarpeta(NombreCarpeta, rutaapp);
            #endregion

            #region  Ambulatorio (Tablas AB1-AB2)
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion
            #region Select
            var cadena1 = "select i_Genero, i_GrupoEtario, v_CIE10Id from Tramas where MONTH(d_FechaIngreso)=" + mes + " and YEAR(d_FechaIngreso)=" + anio + " and v_TipoRegistro='Ambulatorio' and i_IsDeleted=0";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                AgeGenderList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
                DiagnosticList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString() + "|" + lector.GetValue(2).ToString());
            }
            lector.Close();
            #endregion
            #region Contar DiagnosticCount
            foreach (var item in DiagnosticList)
            {
                var count = DiagnosticList.FindAll(p => p == item).ToList().Count;
                DiagnosticCount.Add(item + "|" + count.ToString());
            }
            DiagnosticCount = DiagnosticCount.Distinct().ToList();
            foreach (var item in AgeGenderList)
            {
                var count = AgeGenderList.FindAll(p => p == item).ToList().Count;
                AgeGenderCount.Add(item + "|" + count.ToString());
            }
            AgeGenderCount = AgeGenderCount.Distinct().ToList();
            #endregion
            #region Generar TXT
            GenerartxtAB1();
            GenerartxtAB2();
            #endregion
            #endregion

            #region  Emergencia (Tablas C1-C2)
            #region Select
            cadena1 = "select i_Genero, i_GrupoEtario, v_CIE10Id from Tramas where MONTH(d_FechaIngreso)=" + mes + " and YEAR(d_FechaIngreso)=" + anio + " and v_TipoRegistro='Emergencia' and i_IsDeleted=0";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                AgeGenderListEmer.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
                DiagnosticListEmer.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString() + "|" + lector.GetValue(2).ToString());
            }
            lector.Close();
            #endregion
            #region Contar DiagnosticCountEmer
            foreach (var item in DiagnosticListEmer)
            {
                var count = DiagnosticListEmer.FindAll(p => p == item).ToList().Count;
                DiagnosticCountEmer.Add(item + "|" + count.ToString());
            }
            DiagnosticCountEmer = DiagnosticCountEmer.Distinct().ToList();
            foreach (var item in AgeGenderListEmer)
            {
                var count = AgeGenderListEmer.FindAll(p => p == item).ToList().Count;
                AgeGenderCountEmer.Add(item + "|" + count.ToString());
            }
            AgeGenderCountEmer = AgeGenderCountEmer.Distinct().ToList();
            #endregion
            #region Generar TXT
            GenerartxtC1();
            GenerartxtC2();
            #endregion

            #endregion

            #region  Hospi (Tablas D1-D2)
            #region Select
            cadena1 = "select i_Genero, i_GrupoEtario, v_CIE10Id, d_FechaIngreso, d_FechaAlta, i_UPS, i_Procedimiento, v_TramaId  from Tramas where MONTH(d_FechaIngreso)=" + mes + " and YEAR(d_FechaIngreso)=" + anio + " and v_TipoRegistro='Hospitalización' and i_IsDeleted=0";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                if (lector.GetValue(3).ToString() != null) { HospiIngreso = "|SI|"; }
                else { HospiIngreso = "|NO|" + ""; }
                if (lector.GetValue(4).ToString() != null) { HospiEgreso = "|SI|"; }
                else { HospiEgreso = "|NO|" + ""; }
                UPSIngresoList.Add(lector.GetValue(5).ToString() + HospiIngreso);
                UPSEgresoList.Add(lector.GetValue(5).ToString() + HospiEgreso);
                FechasLis.Add(lector.GetValue(5).ToString() + "|" + lector.GetValue(3).ToString() + "|" + lector.GetValue(4).ToString() + "|" + lector.GetValue(7).ToString());
                UPSList.Add(lector.GetValue(5).ToString());
                HospiList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString() + "|" + lector.GetValue(2).ToString());
                HospiFallecidoList.Add(lector.GetValue(5).ToString() + "|" + lector.GetValue(6).ToString());
            }
            lector.Close();
            #endregion
            #region Contar UPSIngresoList
            foreach (var item in UPSIngresoList)
            {
                var count = UPSIngresoList.FindAll(p => p == item).ToList().Count;
                UPSIngresoCount.Add(item + count.ToString());
            }
            UPSIngresoCount = UPSIngresoCount.Distinct().ToList();
            foreach (var item in UPSEgresoList)
            {
                var count = UPSEgresoList.FindAll(p => p == item).ToList().Count;
                UPSEgresoCount.Add(item + count.ToString());
            }
            UPSEgresoCount = UPSEgresoCount.Distinct().ToList();
            foreach (var item in FechasLis)
            {
                string[] itemsplit = item.Split('|');
                DateTime DateInicial = DateTime.Parse(itemsplit[1]);
                if (itemsplit[2] != null)
                {
                    DateTime DateFinal = DateTime.Parse(itemsplit[2]);
                    var tiempoestancia = DateFinal - DateInicial;
                    string[] tiemposplit = tiempoestancia.ToString().Split(':');
                    double tiempo = double.Parse(tiemposplit[0]);
                    FechasCount.Add(itemsplit[0] + "|" + tiempo.ToString() + "|" + itemsplit[3]);
                }
                else
                {
                    FechasCount.Add(itemsplit[0] + itemsplit[3]);
                }
            }
            FechasCount = FechasCount.Distinct().ToList();
            foreach (var item in FechasCount)
            {
                double acumulador = 0;
                string[] itemsplit = item.Split('|');
                foreach (var item1 in FechasCount)
                {
                    string[] itemsplit1 = item1.Split('|');
                    if ((itemsplit[0] == itemsplit1[0]) && (itemsplit[2] != itemsplit1[2]))
                    {
                        acumulador = acumulador + double.Parse(itemsplit1[1]);
                    }
                }
                acumulador = acumulador + double.Parse(itemsplit[1]);
                acumulador = Math.Round(acumulador, 0);
                Estancia.Add(itemsplit[0] + "|" + acumulador.ToString());
            }
            Estancia = Estancia.Distinct().ToList();
            foreach (var item in UPSList)
            {
                var count = UPSList.FindAll(p => p == item).ToList().Count;
                UPSCount.Add(item + "|" + count.ToString());
            }
            UPSCount = UPSCount.Distinct().ToList();
            foreach (var item in HospiList)
            {
                var count = HospiList.FindAll(p => p == item).ToList().Count;
                HospiCount.Add(item + "|" + count.ToString());
            }
            HospiCount = HospiCount.Distinct().ToList();
            foreach (var item in HospiFallecidoList)
            {
                var count = HospiFallecidoList.FindAll(p => p == item).ToList().Count;
                HospiFallecidoCount.Add(item + "|" + count.ToString());
            }
            HospiFallecidoCount = HospiFallecidoCount.Distinct().ToList();
            #endregion
            #region Generar TXT
            GenerartxtD1();
            GenerartxtD2();
            #endregion

            #endregion

            #region  Partos (Tabla E0)
            #region Select
            cadena1 = "select i_TipoParto, i_TipoNacimiento, i_TipoComplicacion from Tramas where MONTH(d_FechaIngreso)=" + mes + " and YEAR(d_FechaIngreso)=" + anio + " and v_TipoRegistro='Partos' and i_IsDeleted=0";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                string nacimientovivo = "";
                if (lector.GetValue(1).ToString() == "1")
                {
                    nacimientovivo = "VIVO";
                }
                else
                {
                    nacimientovivo = "MUERTO";
                }
                PartoComplicacionList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(2).ToString());
                PartoComplicacionVivosList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(2).ToString() + "|" + nacimientovivo);
            }
            lector.Close();
            #endregion
            #region Contar PartoComplicacionList
            foreach (var item in PartoComplicacionList)
            {
                var count = PartoComplicacionList.FindAll(p => p == item).ToList().Count;
                PartoComplicacionCount.Add(item + "|" + count.ToString());
            }
            PartoComplicacionCount = PartoComplicacionCount.Distinct().ToList();
            foreach (var item in PartoComplicacionVivosList)
            {
                var count = PartoComplicacionVivosList.FindAll(p => p == item).ToList().Count;
                PartoComplicacionVivosCount.Add(item + "|" + count.ToString());
            }
            PartoComplicacionVivosCount = PartoComplicacionVivosCount.Distinct().ToList();
            #endregion
            #region Generar TXT
            GenerartxtE0();
            #endregion

            #endregion

            #region  Procedimientos (Tabla G0)
            #region Select
            cadena1 = "select i_Procedimiento, i_UPS  from Tramas where MONTH(d_FechaIngreso)=" + mes + " and YEAR(d_FechaIngreso)=" + anio + " and v_TipoRegistro='Procedimientos / Cirugía' and i_IsDeleted=0";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                ProcedimientoList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
            }
            lector.Close();
            #endregion
            #region Contar DiagnosticCount
            foreach (var item in ProcedimientoList)
            {
                var count = ProcedimientoList.FindAll(p => p == item).ToList().Count;
                ProcedimientoCount.Add(item + "|" + count.ToString());
            }
            ProcedimientoCount = ProcedimientoCount.Distinct().ToList();
            #endregion
            #region Generar TXT
            GenerartxtG0();
            #endregion
            #endregion

            #region  Cirugia (Tabla H0)
            #region Select
            cadena1 = "select i_Programacion, i_TipoCirugia, i_HorasProg, i_HorasEfect, i_HorasActo, v_TramaId   from Tramas where MONTH(d_FechaIngreso)=" + mes + " and YEAR(d_FechaIngreso)=" + anio + " and v_TipoRegistro='Procedimientos / Cirugía' and i_IsDeleted=0";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                if (lector.GetValue(0).ToString() == "1" && lector.GetValue(1).ToString() == "1")
                {
                    ProgMayorList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
                    ProgMayorHorasProg = ProgMayorHorasProg + int.Parse(lector.GetValue(2).ToString());
                    ProgMayorHorasEfect = ProgMayorHorasEfect + int.Parse(lector.GetValue(3).ToString());
                    ProgMayorHorasAct = ProgMayorHorasAct + int.Parse(lector.GetValue(4).ToString());
                }
                else if (lector.GetValue(0).ToString() == "1" && lector.GetValue(1).ToString() == "2")
                {
                    ProgMenorList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
                    ProgMenorHorasProg = ProgMenorHorasProg + int.Parse(lector.GetValue(2).ToString());
                    ProgMenorHorasEfect = ProgMayorHorasEfect + int.Parse(lector.GetValue(3).ToString());
                    ProgMenorHorasAct = ProgMayorHorasAct + int.Parse(lector.GetValue(4).ToString());
                }
                else if (lector.GetValue(0).ToString() == "2" && lector.GetValue(1).ToString() == "1")
                {
                    EmergMayorList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
                    EmerMayorHorasProg = EmerMayorHorasProg + int.Parse(lector.GetValue(2).ToString());
                    EmerMayorHorasEfect = EmerMayorHorasEfect + int.Parse(lector.GetValue(3).ToString());
                    EmerMayorHorasAct = EmerMayorHorasAct + int.Parse(lector.GetValue(4).ToString());
                }
                else if (lector.GetValue(0).ToString() == "2" && lector.GetValue(1).ToString() == "2")
                {
                    EmergMenorList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
                    EmerMenorHorasProg = EmerMenorHorasProg + int.Parse(lector.GetValue(2).ToString());
                    EmerMenorHorasEfect = EmerMenorHorasEfect + int.Parse(lector.GetValue(3).ToString());
                    EmerMenorHorasAct = EmerMenorHorasAct + int.Parse(lector.GetValue(4).ToString());
                }
                else if (lector.GetValue(0).ToString() == "3" && lector.GetValue(1).ToString() == "1")
                {
                    SuspMayorList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
                    SusMayorHorasProg = EmerMayorHorasProg + int.Parse(lector.GetValue(2).ToString());
                    SusMayorHorasEfect = EmerMayorHorasEfect + int.Parse(lector.GetValue(3).ToString());
                    SusMayorHorasAct = EmerMayorHorasAct + int.Parse(lector.GetValue(4).ToString());
                }
                else if (lector.GetValue(0).ToString() == "3" && lector.GetValue(1).ToString() == "2")
                {
                    SuspMenorList.Add(lector.GetValue(0).ToString() + "|" + lector.GetValue(1).ToString());
                    SusMenorHorasProg = EmerMenorHorasProg + int.Parse(lector.GetValue(2).ToString());
                    SusMenorHorasEfect = EmerMenorHorasEfect + int.Parse(lector.GetValue(3).ToString());
                    SusMenorHorasAct = EmerMenorHorasAct + int.Parse(lector.GetValue(4).ToString());
                }

            }
            lector.Close();
            #endregion
            #region Contar ProgMayorList
            foreach (var item in ProgMayorList)
            {
                var count = ProgMayorList.FindAll(p => p == item).ToList().Count;
                ProgMayorCount.Add(item + "|" + count.ToString());
            }
            ProgMayorCount = ProgMayorCount.Distinct().ToList();
            foreach (var item in ProgMenorList)
            {
                var count = ProgMenorList.FindAll(p => p == item).ToList().Count;
                ProgMenorCount.Add(item + "|" + count.ToString());
            }
            ProgMenorCount = ProgMenorCount.Distinct().ToList();

            foreach (var item in EmergMayorList)
            {
                var count = EmergMayorList.FindAll(p => p == item).ToList().Count;
                EmergMayorCount.Add(item + "|" + count.ToString());
            }
            EmergMayorCount = EmergMayorCount.Distinct().ToList();
            foreach (var item in EmergMenorList)
            {
                var count = EmergMenorList.FindAll(p => p == item).ToList().Count;
                EmergMenorCount.Add(item + "|" + count.ToString());
            }
            EmergMenorCount = EmergMenorCount.Distinct().ToList();

            foreach (var item in SuspMayorList)
            {
                var count = SuspMayorList.FindAll(p => p == item).ToList().Count;
                SuspMayorCount.Add(item + "|" + count.ToString());
            }
            SuspMayorCount = SuspMayorCount.Distinct().ToList();
            foreach (var item in SuspMenorList)
            {
                var count = SuspMenorList.FindAll(p => p == item).ToList().Count;
                SuspMenorCount.Add(item + "|" + count.ToString());
            }
            SuspMenorCount = SuspMenorCount.Distinct().ToList();

            #endregion
            #region Generar TXT
            GenerartxtH0();
            #endregion
            #endregion

            conectasam.closesigesoft();
            this.Close();
            MessageBox.Show("Generación Exitosa", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerartxtH0()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAH0.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                #region Programada

                if (ProgMayorCount.Count > 0)
                {
                    foreach (var item in ProgMayorCount)
                    {
                        string[] itemspilt = item.Split('|');
                        if (ProgMenorCount.Count > 0)
                        {
                            foreach (var item1 in ProgMenorCount)
                            {
                                string[] itemspilt1 = item1.Split('|');
                                if (itemspilt[0] + itemspilt[1] == "11")
                                {
                                    if (itemspilt1[0] + itemspilt1[1] == "12")
                                    {
                                        string cantdcirmayor = itemspilt[2];
                                        string cantdcirmenor = itemspilt1[2];
                                        string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (ProgMayorHorasProg + ProgMenorHorasProg).ToString() + "|" + (ProgMayorHorasEfect + ProgMenorHorasEfect).ToString() + "|" + (ProgMayorHorasAct + ProgMenorHorasAct).ToString() + ",00|0";
                                        file.WriteLine(texto);
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (itemspilt[0] + itemspilt[1] == "11")
                            {
                                string cantdcirmayor = itemspilt[2];
                                string cantdcirmenor = "0";
                                string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (ProgMayorHorasProg + ProgMenorHorasProg).ToString() + "|" + (ProgMayorHorasEfect + ProgMenorHorasEfect).ToString() + "|" + (ProgMayorHorasAct + ProgMenorHorasAct).ToString() + ",00|0";
                                file.WriteLine(texto);
                            }
                        }

                    }
                }
                else
                {
                    foreach (var item1 in ProgMenorCount)
                    {
                        string[] itemspilt1 = item1.Split('|');

                        if (itemspilt1[0] + itemspilt1[1] == "12")
                        {
                            string cantdcirmayor = "0";
                            string cantdcirmenor = itemspilt1[2];
                            string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt1[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (ProgMayorHorasProg + ProgMenorHorasProg).ToString() + "|" + (ProgMayorHorasEfect + ProgMenorHorasEfect).ToString() + "|" + (ProgMayorHorasAct + ProgMenorHorasAct).ToString() + ",00|0";
                            file.WriteLine(texto);
                        }
                    }
                }
                #endregion

                #region Emergencia

                if (EmergMayorCount.Count > 0)
                {
                    foreach (var item in EmergMayorCount)
                    {
                        string[] itemspilt = item.Split('|');
                        if (EmergMenorCount.Count > 0)
                        {
                            foreach (var item1 in EmergMenorCount)
                            {
                                string[] itemspilt1 = item1.Split('|');
                                if (itemspilt[0] + itemspilt[1] == "21")
                                {
                                    if (itemspilt1[0] + itemspilt1[1] == "22")
                                    {
                                        string cantdcirmayor = itemspilt[2];
                                        string cantdcirmenor = itemspilt1[2];
                                        string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (EmerMayorHorasProg + EmerMenorHorasProg).ToString() + "|" + (EmerMayorHorasEfect + EmerMenorHorasEfect).ToString() + "|" + (EmerMayorHorasAct + EmerMenorHorasAct).ToString() + "|0";
                                        file.WriteLine(texto);
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (itemspilt[0] + itemspilt[1] == "21")
                            {
                                string cantdcirmayor = itemspilt[2];
                                string cantdcirmenor = "0";
                                string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (EmerMayorHorasProg + EmerMenorHorasProg).ToString() + "|" + (EmerMayorHorasEfect + EmerMenorHorasEfect).ToString() + "|" + (EmerMayorHorasAct + EmerMenorHorasAct).ToString() + "|0";
                                file.WriteLine(texto);


                            }
                        }


                    }
                }
                else
                {
                    foreach (var item1 in EmergMenorCount)
                    {
                        string[] itemspilt = item1.Split('|');
                        if (itemspilt[0] + itemspilt[1] == "22")
                        {
                            string cantdcirmayor = "0";
                            string cantdcirmenor = itemspilt[2];
                            string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (EmerMayorHorasProg + EmerMenorHorasProg).ToString() + "|" + (EmerMayorHorasEfect + EmerMenorHorasEfect).ToString() + "|" + (EmerMayorHorasAct + EmerMenorHorasAct).ToString() + "|0";
                            file.WriteLine(texto);
                        }
                    }
                }

                #endregion

                #region Suspendida
                if (SuspMayorCount.Count > 0)
                {
                    foreach (var item in SuspMayorCount)
                    {
                        string[] itemspilt = item.Split('|');
                        if (SuspMenorCount.Count > 0)
                        {
                            foreach (var item1 in SuspMenorCount)
                            {
                                string[] itemspilt1 = item1.Split('|');
                                if (itemspilt[0] + itemspilt[1] == "31")
                                {
                                    if (itemspilt1[0] + itemspilt1[1] == "32")
                                    {
                                        string cantdcirmayor = itemspilt[2];
                                        string cantdcirmenor = itemspilt1[2];
                                        int cant = int.Parse(cantdcirmayor) + int.Parse(cantdcirmenor);
                                        string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (SusMayorHorasProg + SusMenorHorasProg).ToString() + "|" + (SusMayorHorasEfect + SusMenorHorasEfect).ToString() + "|" + (SusMayorHorasAct + SusMenorHorasAct).ToString() + "|" + cant.ToString();
                                        file.WriteLine(texto);
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (itemspilt[0] + itemspilt[1] == "31")
                            {

                                string cantdcirmayor = itemspilt[2];
                                string cantdcirmenor = "0";
                                int cant = int.Parse(cantdcirmayor) + int.Parse(cantdcirmenor);
                                string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (SusMayorHorasProg + SusMenorHorasProg).ToString() + "|" + (SusMayorHorasEfect + SusMenorHorasEfect).ToString() + "|" + (SusMayorHorasAct + SusMenorHorasAct).ToString() + "|" + cant.ToString();
                                file.WriteLine(texto);

                            }
                        }

                    }
                }
                else
                {
                    foreach (var item in SuspMenorCount)
                    {
                        string[] itemspilt = item.Split('|');

                        if (itemspilt[0] + itemspilt[1] == "32")
                        {
                            string cantdcirmayor = "0";
                            string cantdcirmenor = itemspilt[2];
                            int cant = int.Parse(cantdcirmayor) + int.Parse(cantdcirmenor);
                            string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + "0" + itemspilt[0] + "|" + cantdcirmayor + "|" + cantdcirmenor + "|" + (SusMayorHorasProg + SusMenorHorasProg).ToString() + "|" + (SusMayorHorasEfect + SusMenorHorasEfect).ToString() + "|" + (SusMayorHorasAct + SusMenorHorasAct).ToString() + "|" + cant.ToString();
                            file.WriteLine(texto);
                        }
                    }
                }
                #endregion


                file.Close();
            }
        }

        private void GenerartxtG0()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAG0.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                foreach (var item in ProcedimientoCount)
                {
                    string[] itemspilt = item.Split('|');
                    int proced = itemspilt[0].Length;
                    if (proced == 3)
                    {
                        itemspilt[0] = "00" + itemspilt[0];
                    }
                    else if (proced == 4)
                    {
                        itemspilt[0] = "0" + itemspilt[0];
                    }
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + itemspilt[0] + "|" + itemspilt[2] + "|" + itemspilt[1];
                    file.WriteLine(texto);
                }
                file.Close();
            }
        }

        private void GenerartxtE0()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAE0.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                foreach (var item in PartoComplicacionCount)
                {
                    string[] itemsplit = item.Split('|');
                    foreach (var item2 in PartoComplicacionVivosCount)
                    {
                        string[] itemsplit2 = item2.Split('|');
                        string vivos = "0";
                        string tipoparto = "";
                        string complicacionparto = "";
                        string muertos = "0";
                        string nropartos = "";
                        if (itemsplit[0] + itemsplit[1] == itemsplit2[0] + itemsplit2[1])
                        {
                            tipoparto = "0" + itemsplit[0];
                            complicacionparto = "0" + itemsplit[1];
                            nropartos = itemsplit[2];

                            if (itemsplit2[2] == "VIVO")
                            {
                                vivos = itemsplit2[3];
                            }
                            else if (itemsplit2[2] == "MUERTO")
                            {
                                muertos = itemsplit2[3];
                            }
                            string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + tipoparto + "|" + complicacionparto + "|" + nropartos + "|" + nropartos + "|" + vivos + "|" + muertos;
                            file.WriteLine(texto);
                        }

                    }

                }
                file.Close();
            }
        }

        private void GenerartxtD2()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAD2.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                foreach (var item in HospiCount)
                {
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item;
                    file.WriteLine(texto);
                }
                file.Close();
            }
        }

        private void GenerartxtD1()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAD1.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                foreach (var item1 in UPSIngresoCount)
                {
                    string[] item1split = item1.ToString().Split('|');
                    foreach (var item2 in UPSEgresoCount)
                    {
                        string[] item2split = item2.ToString().Split('|');
                        foreach (var item3 in UPSCount)
                        {
                            string[] item3split = item3.ToString().Split('|');
                            foreach (var item4 in HospiFallecidoCount)
                            {
                                string[] item4split = item4.ToString().Split('|');
                                foreach (var item5 in Estancia)
                                {
                                    string[] item5split = item5.ToString().Split('|');
                                    if (item1split[0] == item2split[0] && item1split[0] == item3split[0] && item1split[0] == item4split[0] && item1split[0] == item5split[0])
                                    {
                                        int pacientesdias = int.Parse(item1split[2]) * int.Parse(item5split[1]);
                                        int diascama = 480;
                                        int camas = diascama - pacientesdias;

                                        string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item1split[0] + "|" + item1split[2] + "|" + item2split[2] + "|" + item5split[1] + "|" + pacientesdias.ToString() + "|" + camas.ToString() + "|" + diascama.ToString() + "|" + item4split[1];
                                        file.WriteLine(texto);
                                    }
                                }

                            }
                        }
                    }
                }
                file.Close();
            }
        }

        private void GenerartxtC2()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAC2.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                foreach (var item in DiagnosticCountEmer)
                {
                    string[] countitem = item.Split('|');
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item;
                    file.WriteLine(texto);
                }
                file.Close();
            }
        }

        private void GenerartxtC1()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAC1.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                foreach (var item in AgeGenderCountEmer)
                {
                    string[] countitem = item.Split('|');
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item + "|" + countitem[2];
                    file.WriteLine(texto);
                }
                file.Close();
            }
        }

        private void GenerartxtAB2()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAB2.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                foreach (var item in DiagnosticCount)
                {
                    string[] countitem = item.Split('|');
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item;
                    file.WriteLine(texto);
                }
                file.Close();
            }
        }

        private void GenerartxtAB1()
        {
            string ruta = rutaapp + @"\" + NombreCarpeta + @"\" + ugi + "_" + anio + "_" + mes + "_TAB1.txt";
            using (StreamWriter mylogs = File.AppendText(ruta))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");
                mylogs.Close();
            }
            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                foreach (var item in AgeGenderCount)
                {
                    string[] countitem = item.Split('|');
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item + "|0|" + countitem[2];
                    file.WriteLine(texto);
                }
                file.Close();
            }
        }

        private void CrearCarpeta(string Name, string ruta)
        {
            string path = ruta + @"\" + Name;

            try
            {
                if (Directory.Exists(path))
                {
                    MessageBox.Show("La Carpeta ya existe, verifique e intente nuevamente", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                MessageBox.Show("El proceso ha fallado: " + e.ToString(), "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { }
        }

    }
}
