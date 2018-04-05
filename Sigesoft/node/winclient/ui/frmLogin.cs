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
