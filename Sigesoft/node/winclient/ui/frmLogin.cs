using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Common;
using System.Data.SqlClient;
using System.Data.Common;
using System.Management;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmLogin : Form
    {
        private int _intNodeId;

        public frmLogin()
        {

            //try
            //{
            //    var connectionString =
            //        "Data Source=.;Initial Catalog=MigracionPostgres;Integrated Security=False;Persist Security Info=True;User ID=sa;Password=123456";

            //    using (var connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();          
            //    }
            //}
            //catch (DbException)
            //{
            //    MessageBox.Show( "ERROR","Error al conectarse a Base de Datos !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
            InitializeComponent();
            //#region Setear Usuarios
            //     #region Obtener ip Local
            //            string localIP = "";
            //            string hostName = Dns.GetHostName();
            //            localIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            //    #endregion
            //            #region Setear ip
            //            if (localIP == "192.168.1.179")
            //            {
            //                txtUserName.Text = "sa";
            //                txtPassword.Text = "Alph@2536";
            //            } 
            //            else if (localIP == "192.168.1.71")
            //            {
            //                txtUserName.Text = "sa";
            //                txtPassword.Text = "Alph@2536";
            //            }
            //            else if (localIP == "192.168.1.106")
            //            {
            //                txtUserName.Text = "ruth.quispe";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.184")
            //            {
            //                txtUserName.Text = "cesar.medina";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.52")
            //            {
            //                txtUserName.Text = "cinthya.vasquez";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.81")
            //            {
            //                txtUserName.Text = "mmedina";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.80")
            //            {
            //                txtUserName.Text = "rocio.medina";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.117")
            //            {
            //                txtUserName.Text = "vladimir.figueroa";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.182")
            //            {
            //                txtUserName.Text = "roberto.perez";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.120")
            //            {
            //                txtUserName.Text = "roger.narro";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.73")
            //            {
            //                txtUserName.Text = "sali.palacios";
            //                txtPassword.Text = "";
            //            }
            //            else if (localIP == "192.168.1.69")
            //            {
            //                txtUserName.Text = "sa";
            //                txtPassword.Text = "Alph@2536";
            //            }
            //            else if (localIP == "25.4.135.45")
            //            {
            //                txtUserName.Text = "sa";
            //                txtPassword.Text = "Alph@2536";
            //            }
            //            else if (localIP == "192.168.0.26")
            //            {
            //                txtUserName.Text = "sa";
            //                txtPassword.Text = "Alph@2536";
            //            }
            //            else
            //            {
            //                if (bool.Parse(Common.Utils.GetApplicationConfigValue("Developer")))
            //                {
            //                    txtUserName.Text = "sa";
            //                    txtPassword.Text = "Alph@2536";
            //                }
            //                else
            //                {
            //                    txtUserName.Text = "";
            //                    txtPassword.Text = "";
            //                }
                            
            //            }

            //            #endregion

            //#endregion

           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //this.Close();
        }

        
        private string SerieDD()
        {         

            DirectoryInfo currentDir = new DirectoryInfo(Environment.CurrentDirectory);
            string path = string.Format("win32_logicaldisk.deviceid=\"{0}\"",
                currentDir.Root.Name.Replace("\\", ""));
            ManagementObject disk = new ManagementObject(path);
            disk.Get();

            string serial = disk["VolumeSerialNumber"].ToString();

            return serial;
            //Console.WriteLine("Presione una tecla para terminar... ");
            //Console.ReadKey(true);
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            //#region Actualización
            //string rutaserver = Common.Utils.GetApplicationConfigValue("RutaServer_Act").ToString();
            //string rutapc = Common.Utils.GetApplicationConfigValue("RutaPC_Act").ToString();
            //string rutaejecutable = Common.Utils.GetApplicationConfigValue("RutaAct_exe").ToString();
            //if (File.Exists(rutaserver) && File.Exists(rutapc) && File.Exists(rutaejecutable))
            //{
            //    DateTime fechatxt = File.GetLastWriteTime(rutaserver);
            //    DateTime fechatxt2 = File.GetLastWriteTime(rutapc);
            //    if (fechatxt != fechatxt2)
            //    {
            //        System.Diagnostics.Process.Start(rutaejecutable);
            //        this.Close();
            //    }
            //    else
            //    {
            //        //MessageBox.Show("NO HAY ACTUALICACIONES PENDIENTES...", "ACTUALIZACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //#endregion
            
            OperationResult objOperationResult = new OperationResult();

            ////----------------------------------------------------------------------------------------------------------------
            //bool Validador = false;
            //string[] Serial = new string[] 
            //{ 
            //    "E63B2796",
            //    "2443F7CC"
            //};

            // string MAC1 =   SerieDD();
            //foreach (var item in Serial)
            //{
            //    if (item == MAC1)
            //    {
            //        Validador = true;
            //    }
            //}


            //if (Validador != true)
            //{
            //    MessageBox.Show(objOperationResult.ExceptionMessage, "Error al conectarse a Base de Datos !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}

            ////----------------------------------------------------------------------------------------------------------------

            // Obtener el ID del nodo del archivo de configuración
            _intNodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
           

            string MAC = Common.Utils.GetApplicationConfigValue("ClientSettingsProviderx");

            // Leer el nodo
            NodeBL objNodeBL = new NodeBL();
            
            nodeDto objNodeDto = objNodeBL.GetNodeByNodeId(ref objOperationResult, _intNodeId);
            if (objOperationResult.Success == 1)
            {
                // Mostrar el nombre del nodo
                txtNode.Text = objNodeDto.v_Description;
            }
            else
            {
                MessageBox.Show(objOperationResult.ExceptionMessage, "Error al conectarse a Base de Datos !!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnOK.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;

                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.AuthenticateUser();
            ///
        }

        private void AuthenticateUser()
        {
            // Autenticación de usuario
            SecurityBL objSecurityBL = new SecurityBL();

            OperationResult objOperationResult = new OperationResult();
            var objUserDto = objSecurityBL.ValidateSystemUser(ref objOperationResult, _intNodeId, txtUserName.Text, Common.Utils.Encrypt(txtPassword.Text));

            if (objUserDto != null)
            {
                ClientSession objClientSession = new ClientSession();
                objClientSession.i_SystemUserId = objUserDto.i_SystemUserId;
                objClientSession.v_UserName = objUserDto.v_UserName;
                objClientSession.i_CurrentExecutionNodeId = _intNodeId;
                objClientSession.v_CurrentExecutionNodeName = txtNode.Text;
                //_ClientSession.i_CurrentOrganizationId = 57;
                objClientSession.v_PersonId = objUserDto.v_PersonId;
                objClientSession.i_RolVentaId = objUserDto.i_RolVentaId;
                objClientSession.i_SystemUserCopyId = objUserDto.i_SystemUserId;
                objClientSession.i_ProfesionId = objUserDto.i_ProfesionId;

                var dataOrganization = new ServiceBL().GetInfoMedicalCenter();

                objClientSession.b_LogoOwner = dataOrganization.b_Image;
                objClientSession.v_TelephoneOwner = dataOrganization.v_PhoneNumber;
                objClientSession.v_RucOwner = dataOrganization.v_IdentificationNumber;
                objClientSession.v_AddressOwner = dataOrganization.v_Address;
                objClientSession.v_OrganizationOwner = dataOrganization.v_Name;
                objClientSession.v_SectorName = dataOrganization.v_SectorName;

                // Pasar el objeto de sesión al gestor de objetos globales
                Globals.ClientSession = objClientSession;

                // Abrir el formulario principal
                this.Hide();
                frmMaster frm = new frmMaster();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show(objOperationResult.AdditionalInformation, "Advertencia-->>>>>>>>>>>>>>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            // Autenticar el usuario



        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            //btnOK.Enabled = (txtUserName.Text != string.Empty && txtPassword.Text != string.Empty);
        }



  
    }
}
