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
                rutaapp = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
                CrearCarpeta(NombreCarpeta, rutaapp);
            #endregion
            
            #region  Ambulatorio (Tablas AB1-AB2)
                #region Conexion SAM
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                #endregion
                #region Select
                var cadena1 = "select i_Genero, i_GrupoEtario, v_CIE10Id from Tramas where MONTH(d_FechaIngreso)="+mes+" and YEAR(d_FechaIngreso)="+anio+" and v_TipoRegistro='Ambulatorio'";
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
            cadena1 = "select i_Genero, i_GrupoEtario, v_CIE10Id from Tramas where MONTH(d_FechaIngreso)=" + mes + " and YEAR(d_FechaIngreso)=" + anio + " and v_TipoRegistro='Emergencia'";
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
            cadena1 = "select i_Genero, i_GrupoEtario, v_CIE10Id, d_FechaIngreso, d_FechaAlta, i_UPS, i_Procedimiento  from Tramas where MONTH(d_FechaIngreso)=" + mes + " and YEAR(d_FechaIngreso)=" + anio + " and v_TipoRegistro='Hospitalización'";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                if (lector.GetValue(3).ToString() != null) { HospiIngreso = "|SI|"; }
                else{HospiIngreso = "|NO|"+"";}
                if (lector.GetValue(4).ToString() != null) { HospiEgreso = "|SI|" ; }
                else { HospiEgreso = "|NO|"+""; }
                UPSIngresoList.Add(lector.GetValue(5).ToString() + HospiIngreso);
                UPSEgresoList.Add(lector.GetValue(5).ToString() + HospiEgreso);
                FechasLis.Add(lector.GetValue(5).ToString() + "|" +lector.GetValue(3).ToString() + "|" + lector.GetValue(4).ToString());
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
                    FechasCount.Add(itemsplit[0] + "|" + tiempo.ToString());
                }
                else
                {
                    FechasCount.Add(itemsplit[0]);
                }
            }
            FechasCount = FechasCount.Distinct().ToList();
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

            conectasam.closesigesoft();
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
                                if (item1split[0] == item2split[0] && item1split[0] == item3split[0] && item1split[0] == item4split[0])
                                {
                                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item1split[0] + "|" + item1split[1] + "|" + item2split[1] + "|" + item3split[1];
                                    file.WriteLine(texto);
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
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item + "|0|" + countitem[2];
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
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|" + item ;
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
                    string texto = anio + mes + "|" + ugi + "|" + ugi + "|"+ item + "|0|" + countitem[2];
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
                    return;
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
