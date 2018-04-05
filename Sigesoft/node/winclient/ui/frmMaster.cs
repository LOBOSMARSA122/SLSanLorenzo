using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using System.Reflection;
using Sigesoft.Node.WinClient.UI.Operations;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmMaster : Form
    {
        List<string> _lstBackGroundImageFiles;
        Random _objRandom = new Random();
        //private System.Reflection.Assembly Ensamblado;
        SecurityBL _objSecurityBL = new SecurityBL();
        List<AutorizationList> objAuthorizationList;
       
        public frmMaster()
        {
            InitializeComponent();
        }
       
        private void frmMaster_Load(object sender, EventArgs e)
        {
            //Ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
            this.MenuPpal.Items.Clear();
            CargarMenus();
            MenuPpal_1.Items[0].Text = "NodeClient Versión:  2.01";
            MenuPpal_1.Items[1].Text = "NODO:  " + Globals.ClientSession.v_CurrentExecutionNodeName + " (Id: " + Globals.ClientSession.i_CurrentExecutionNodeId + ")";
            MenuPpal_1.Items[2].Text = "Usuario: " + Globals.ClientSession.v_UserName + " (Id: " + Globals.ClientSession.v_PersonId + ")";
            MenuPpal_1.Items[3].Text = string.Format("Rol: {0}", Globals.ClientSession.v_RoleName);

            GetBackGroundImageFiles();

            //timer1.Start();
            //timer1.Interval = 2000;
   
        }

        private void CargarMenus()
        {
            OperationResult objOperationResult2 = new OperationResult();
            objAuthorizationList = _objSecurityBL.GetAuthorization(ref objOperationResult2, int.Parse(Globals.ClientSession.i_CurrentExecutionNodeId.ToString()), int.Parse(Globals.ClientSession.i_SystemUserId.ToString()));
            objAuthorizationList = objAuthorizationList.FindAll(p => p.I_ParentId != -1);
            
            if (objAuthorizationList != null)
            {
                var roleInfo = objAuthorizationList.Find(p => p.i_RoleId != null);

                Globals.ClientSession.v_RoleName = roleInfo.V_RoleName;
                Globals.ClientSession.i_RoleId = roleInfo.i_RoleId;

                var zzz = objAuthorizationList.FindAll(p => p.I_ApplicationHierarchyTypeId == 1);

                foreach (var MenuPadre in zzz)
                {
                    ToolStripItem[] Menu = new ToolStripItem[1];
                    Menu[0] = new ToolStripMenuItem();
                    Menu[0].Name = MenuPadre.I_ApplicationHierarchyId.ToString();
                    Menu[0].Text = MenuPadre.V_Description;
                    Menu[0].Tag = MenuPadre.V_Form;
                    Menu[0].ForeColor = Color.MediumBlue;

                    var FindResult = objAuthorizationList.FindAll(p => p.I_ParentId == MenuPadre.I_ApplicationHierarchyId);

                    if (FindResult.Count == 0)
                    {
                        Menu[0].Click += new EventHandler(MenuItemClicked);
                        MenuPpal.Items.Add(Menu[0]);
                    }
                    else
                    {
                        MenuPpal.Items.Add(Menu[0]);
                        AgregarMenuHijo(Menu[0]);                     
                    }

                }
            }
        }

        private void AgregarMenuHijo(ToolStripItem MenuItemPadre)
        {
            ToolStripMenuItem MenuPadre = (ToolStripMenuItem)MenuItemPadre;

            //Obtengo el ID del menu Enviado para saber si tiene hijos o no
            int Id = int.Parse(MenuPadre.Name.ToString());


            //Averiguando si tiene Hijos o no           
            var yyy = objAuthorizationList.FindAll(p => p.I_ParentId == Id);
            if (yyy.Count == 0)
            {
                //Si No tiene Hijos Solo Agrego el Evento
                MenuPadre.Click += new EventHandler(MenuItemClicked);
            }
            else
            {
                //Si Aun tiene Hijos
                foreach (var Menu in objAuthorizationList.FindAll(p => p.I_ParentId == Id))
                {
                    ToolStripItem[] NuevoMenu = new ToolStripItem[1];
                    NuevoMenu[0] = new ToolStripMenuItem();
                    NuevoMenu[0].Name = Menu.I_ApplicationHierarchyId.ToString();
                    NuevoMenu[0].Text = Menu.V_Description;
                    NuevoMenu[0].Tag = Menu.V_Form;
                    NuevoMenu[0].ForeColor = Color.MediumBlue;

                  
                        //Obtengo el ID del Menu del foreach                        
                        //Averiguando si tiene Hijos o no                        
                    var xxx = objAuthorizationList.FindAll(p => p.I_ParentId == Menu.I_ApplicationHierarchyId);
                    if (xxx.Count ==0)
                        {
                            //Sino tiene hijos lo agrego al Menu Padre                          
                            NuevoMenu[0].Click += new EventHandler(MenuItemClicked);
                            MenuPadre.DropDownItems.Add(NuevoMenu[0]);
                        }
                        else
                        {
                            //Si tiene hijos llamo a la funcion recursiva y Agrego el Item sin Evento                        
                            MenuPadre.DropDownItems.Add(NuevoMenu[0]);
                            AgregarMenuHijo(NuevoMenu[0]);
                        }
                }
            }
        }

        private Boolean FormularioEstaAbierto(String NombreDelFrm)
        {
            if (this.MdiChildren.Length > 0)
            {
                for (int i = 0; i < this.MdiChildren.Length; i++)
                {
                    //MessageBox.Show(NombreDelFrm.Substring(NombreDelFrm.IndexOf("Frm_"), NombreDelFrm.Length - NombreDelFrm.IndexOf("Frm_")));
                    if (this.MdiChildren[i].Name == NombreDelFrm.Substring(NombreDelFrm.IndexOf("Frm_"), NombreDelFrm.Length - NombreDelFrm.IndexOf("Frm_")))
                    {
                        MessageBox.Show("El formulario solicitado ya se encuentra abierto");
                        return true;
                    }
                }
                return false;
            }
            else
                return false;
        }

        private void MenuItemClicked(object sender, EventArgs e)
        {
            // if the sender is a ToolStripMenuItem
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {

                //try
                //{
                    string NombreFormulario = ((ToolStripItem)sender).Tag.ToString();
                    Assembly asm = Assembly.GetEntryAssembly();
                    Type formtype = asm.GetType(NombreFormulario);

                    if (asm == null)
                        throw new Exception("estoy null ayudame  (asm)");

                    if (formtype == null)
                        throw new Exception("estoy null ayudame (formtype)");

                   

                    if (formtype != null)
                    {
                        Form f = (Form)Activator.CreateInstance(formtype);
                        f.ShowDialog();
                    }           
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                //}                               
            }
        }

        private void GetBackGroundImageFiles()
        {
            // Obtener las imágenes de la carpeta \Backgrounds
            string strFolder = Common.Utils.GetApplicationExecutingFolder() + @"\Backgrounds";
            string strSupportedExtensions = "*.jpg,*.png,*.bmp,*.jpeg"; // Solo imágenes
            _lstBackGroundImageFiles = Common.Utils.GetFolderFiles(strFolder, strSupportedExtensions);

            // Menú de imágenes
            toolStripDropDownButton1.DropDownItems.Clear();
            for (int i = 0; i < _lstBackGroundImageFiles.Count; i++)
            {
                toolStripDropDownButton1.DropDownItems.Add(_lstBackGroundImageFiles[i]);
                toolStripDropDownButton1.DropDownItems[i].Click += new EventHandler(mnuImages_Click);
            }

            SetBackGroundImage(-1, true);
        }

        private void mnuImages_Click(object sender, EventArgs e)
        {
            ToolStripItem mnu = (ToolStripItem)sender;
            int intIndex = toolStripDropDownButton1.DropDownItems.IndexOf(mnu);
            SetBackGroundImage(intIndex, false);
        }

        private void SetBackGroundImage(int pintIndex, bool pbooRandom)
        {
            if (_lstBackGroundImageFiles.Count > 0)
            {
                int intSelectedIndex = pintIndex;       

                if (pbooRandom)
                {
                    // Elegir aleatoriamente un archivo
                    intSelectedIndex = _objRandom.Next(0, _lstBackGroundImageFiles.Count - 1);
                }

                // Establecer la imagen de fondo
                string strSelectedFile = _lstBackGroundImageFiles[intSelectedIndex];
                this.BackgroundImage = new System.Drawing.Bitmap(strSelectedFile);

                // Mostrar en el status bar, el archivo seleccionado
                MenuPpal_1.Items[4].Text = Path.GetFileName(strSelectedFile) + " (" + (intSelectedIndex + 1) + "/" + _lstBackGroundImageFiles.Count + ")";
            }
            else
            {
                MenuPpal_1.Items[4].Text = "Imagen DEFAULT";
            }
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            SetBackGroundImage(-1, true);
        }

        private void frmMaster_Click(object sender, EventArgs e)
        {
            SetBackGroundImage(-1, true);
        }

        private void administraciónDeMovimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdministracion frm = new frmAdministracion();
            frm.ShowDialog();
        }

        private void consultaDeStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmLogEdicion frm = new frmLogEdicion();
            //frm.ShowDialog();
        }

        private void tEST3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void administraciónDeParámetrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmParametersManagement frm = new frmParametersManagement();
            frm.ShowDialog();
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLog frm = new frmLog();
            frm.ShowDialog();
        }

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpresa frm = new frmEmpresa();
            frm.ShowDialog();
        }

        private void administraciónDeExamenesMédicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMedicalExam frm = new frmMedicalExam();
            frm.ShowDialog();
        }

        private void administraciónDePacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPacient frm = new frmPacient();
            frm.ShowDialog();
        }

        private void agendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCalendar frm = new frmCalendar();
            frm.ShowDialog();
        }

        private void atencionesEnConsultorioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreOffice frm = new frmPreOffice();
            frm.ShowDialog();
        }

        private void administraciónProtocolosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.frmProtocolManagement frm = new Configuration.frmProtocolManagement();
            frm.ShowInTaskbar = false;
            frm.ShowDialog(this);
        }

        private void administradorProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSupplier frm = new frmSupplier();
            frm.ShowDialog();
        }

        private void administradorDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct();
            frm.ShowDialog();
        }

        private void consultaDeStocksToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmStock frm = new frmStock();
            frm.ShowDialog();
        }

        private void consultaDeMoviminetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMovement frm = new frmMovement();
            frm.ShowDialog();
        }

        private void administradorDeServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmService frm = new frmService();
            frm.ShowDialog();
        }

        private void MenuPpal_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MenuPpal_1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        bool _ran = false; //initial setting at start up
        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void btnDespacho_Click(object sender, EventArgs e)
        {
         
        }

    }
}
