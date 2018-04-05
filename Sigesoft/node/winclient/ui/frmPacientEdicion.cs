using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using System.IO;
using System.Drawing.Imaging;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPacientEdicion : Form
    {
        PacientBL _objPacientBL = new PacientBL();
        personDto objpersonDto;        

        private string _fileName;
        private string _filePath;

        string PacientId;
        string Mode;
        string NumberDocument;

        #region Properties

        public byte[] FingerPrintTemplate { get; set; }

        public byte[] FingerPrintImage { get; set; }

        public byte[] RubricImage { get; set; }

        public string RubricImageText { get; set; }

        #endregion

        public frmPacientEdicion( string strPacientId, string pstrMode)
        {
            InitializeComponent();
            Mode = pstrMode;
            PacientId = strPacientId;
        }

        private void frmPacientEdiccion_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            dtpBirthdate.CustomFormat = "dd/MM/yyyy";
            //Llenado de combos
            Utils.LoadDropDownList(ddlMaritalStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDocTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLevelOfId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBloodGroupId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 154, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBloodFactorId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 155, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRelationshipId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 207, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltitudeWorkId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 208, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPlaceWorkId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 204, null), DropDownListAction.Select);
            
                if (Mode == "New")
                {
                    // Additional logic here.
                    dtpBirthdate.Checked = false;

                }
               else if (Mode == "Edit")
               {
                    PacientList objpacientDto = new PacientList();
                    objpacientDto = _objPacientBL.GetPacient(ref objOperationResult, PacientId,null);


                   ddlRelationshipId.SelectedValue = objpacientDto.i_Relationship== null ? "-1" : objpacientDto.i_Relationship.ToString() ;
                   ddlAltitudeWorkId.SelectedValue = objpacientDto.i_AltitudeWorkId == null ? "-1" : objpacientDto.i_AltitudeWorkId.ToString();
                    ddlPlaceWorkId.SelectedValue =  objpacientDto.i_PlaceWorkId ==null ? "-1" : objpacientDto.i_PlaceWorkId.ToString();
                    txtExploitedMineral.Text = objpacientDto.v_ExploitedMineral;

                    txtName.Text = objpacientDto.v_FirstName;
                    txtFirstLastName.Text = objpacientDto.v_FirstLastName;
                    txtSecondLastName.Text = objpacientDto.v_SecondLastName;
                    ddlDocTypeId.SelectedValue = objpacientDto.i_DocTypeId.ToString();
                    ddlSexTypeId.SelectedValue = objpacientDto.i_SexTypeId.ToString();
                    ddlMaritalStatusId.SelectedValue = objpacientDto.i_MaritalStatusId.ToString();
                    ddlLevelOfId.SelectedValue = objpacientDto.i_LevelOfId.ToString();
                    txtDocNumber.Text = objpacientDto.v_DocNumber;
                    NumberDocument = txtDocNumber.Text;
                    dtpBirthdate.Value = (DateTime)objpacientDto.d_Birthdate;
                    txtBirthPlace.Text = objpacientDto.v_BirthPlace;
                    txtTelephoneNumber.Text = objpacientDto.v_TelephoneNumber;
                    txtAdressLocation.Text = objpacientDto.v_AdressLocation;
                    txtMail.Text = objpacientDto.v_Mail;
                    ddlBloodGroupId.SelectedValue = objpacientDto.i_BloodGroupId ==  0 ? "-1" : objpacientDto.i_BloodGroupId.ToString();
                    ddlBloodFactorId.SelectedValue =objpacientDto.i_BloodFactorId ==  0 ? "-1" : objpacientDto.i_BloodFactorId.ToString();
                    txtCurrentOccupation.Text = objpacientDto.v_CurrentOccupation;

                    FingerPrintTemplate = objpacientDto.b_FingerPrintTemplate;
                    FingerPrintImage = objpacientDto.b_FingerPrintImage;
                    RubricImage = objpacientDto.b_RubricImage;
                    RubricImageText = objpacientDto.t_RubricImageText;

                    pbPersonImage.Image = Common.Utils.BytesArrayToImage(objpacientDto.b_Photo, pbPersonImage);

               }
        }
  
        private void btnOK_Click(object sender, EventArgs e)
        {

            OperationResult objOperationResult = new OperationResult();
            string Result ="";
            if (uvPacient.Validate(true, false).IsValid)
            {
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Nombres.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtFirstLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Paterno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtSecondLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Materno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtDocNumber.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Número Documento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtMail.Text.Trim() != "")
                {

                    if (!Sigesoft.Common.Utils.email_bien_escrito(txtMail.Text.Trim()))
                    {
                        MessageBox.Show("Por favor ingrese un Email con formato correcto.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }                    
                }

                if (dtpBirthdate.Checked ==  false)
                {
                      MessageBox.Show("Por favor ingrese una fecha de nacimiento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int caracteres = txtDocNumber.TextLength;
                if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.DNI)
                {
                    if (caracteres != 8)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.PASAPORTE)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.LICENCIACONDUCIR)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.CARNETEXTRANJ)
                {
                    if (caracteres < 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (Mode == "New")
                {
                    // Populate the entity
                    objpersonDto = new personDto();
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
                    objpersonDto.d_Birthdate = dtpBirthdate.Value;
                    objpersonDto.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objpersonDto.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    objpersonDto.v_CurrentOccupation = txtCurrentOccupation.Text.Trim();

                    objpersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
                    objpersonDto.b_FingerPrintImage = FingerPrintImage;
                    objpersonDto.b_RubricImage = RubricImage;
                    objpersonDto.t_RubricImageText = RubricImageText;

                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;

                    if (pbPersonImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        Bitmap bm = new Bitmap(pbPersonImage.Image);
                        bm.Save(ms, ImageFormat.Bmp);

                        objpersonDto.b_PersonImage = Common.Utils.ResizeUploadedImage(ms);
                        pbPersonImage.Image.Dispose();
                    }
                    else
                    {
                        objpersonDto.b_PersonImage = null;
                    }
                   
                    // Save the data
                    Result =  _objPacientBL.AddPacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList());
                    
                }
                else if (Mode == "Edit")
                {
                    // Populate the entity
                    objpersonDto = new personDto();

                    objpersonDto = _objPacientBL.GetPerson(ref objOperationResult, PacientId);
                    objpersonDto.v_PersonId = PacientId;
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
                    objpersonDto.d_Birthdate = dtpBirthdate.Value;
                    objpersonDto.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objpersonDto.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    objpersonDto.v_CurrentOccupation = txtCurrentOccupation.Text.Trim();
                    objpersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
                    objpersonDto.b_FingerPrintImage = FingerPrintImage;
                    objpersonDto.b_RubricImage = RubricImage;
                    objpersonDto.t_RubricImageText = RubricImageText;

                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;
                   
                    if (pbPersonImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        Bitmap bm = new Bitmap(pbPersonImage.Image);
                        bm.Save(ms, ImageFormat.Bmp); 
                        objpersonDto.b_PersonImage = Common.Utils.ResizeUploadedImage(ms);
                        pbPersonImage.Image.Dispose();
                    }
                    else
                    {
                        objpersonDto.b_PersonImage = null;
                    }

                    // Save the data
                    Result = _objPacientBL.UpdatePacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList(), NumberDocument, txtDocNumber.Text);
                 
                }
                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }                
                else  // Operación con error
                {
                    if (Result == "-1")
                    {
                        MessageBox.Show("El Número de documento ya existe.", "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Se queda en el formulario.
                    }
                
                }

            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            if (pbPersonImage.Image != null)
            {
                pbPersonImage.Image.Dispose();
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Image Files (*.jpg;*.gif;*.jpeg;*.png)|*.jpg;*.gif;*.jpeg;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!IsValidImageSize(openFileDialog1.FileName)) 
                    return;

                // Seteaar propiedades del control PictutreBox
                LoadFile(openFileDialog1.FileName);
                //pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
                txtFileName.Text = Path.GetFileName(openFileDialog1.FileName);
                // Setear propiedades de usuario
                _fileName = Path.GetFileName(openFileDialog1.FileName);
                _filePath = openFileDialog1.FileName; 

                var Ext = Path.GetExtension(txtFileName.Text);
              
                if (Ext == ".JPG" || Ext == ".GIF" || Ext == ".JPEG" || Ext == ".PNG" || Ext == "")
                {

                    //System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(pbPersonImage.Image);

                    //Decimal Hv = 280;
                    //Decimal Wv = 383;

                    //Decimal k = -1;

                    //Decimal Hi = bmp1.Height;
                    //Decimal Wi = bmp1.Width;

                    //Decimal Dh = -1;
                    //Decimal Dw = -1;

                    //Dh = Hi - Hv;
                    //Dw = Wi - Wv;

                    //if (Dh > Dw)
                    //{
                    //    k = Hv / Hi;
                    //}
                    //else
                    //{
                    //    k = Wv / Wi;
                    //}

                    //pbPersonImage.Height = (int)(k * Hi);
                    //pbPersonImage.Width = (int)(k * Wi);
                }
            }
            else
            {
                return;
            }
        }

        private void LoadFile(string pfilePath)
        {
            Image img = pbPersonImage.Image;

            // Destruyo la posible imagen existente en el control
            //
            if (img != null)
            {
                img.Dispose();
            }

            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);
                pbPersonImage.Image = original;

            }
        }

        private bool IsValidImageSize(string pfilePath)
        {
            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);

                if (original.Width > Constants.WIDTH_MAX_SIZE_IMAGE || original.Height > Constants.HEIGHT_MAX_SIZE_IMAGE)
                {
                    MessageBox.Show("La imagen que está tratando de subir es damasiado grande.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false; 
                }     
            }
            return true;
        }

        private void ddlDocTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocTypeId.Text == "--Seleccionar--") return;

             OperationResult objOperationResult = new OperationResult();
             DataHierarchyBL objDataHierarchyBL = new DataHierarchyBL();
             datahierarchyDto objDataHierarchyDto = new datahierarchyDto();

            int value =Int32.Parse(ddlDocTypeId.SelectedValue.ToString());
            objDataHierarchyDto=  objDataHierarchyBL.GetDataHierarchy(ref objOperationResult, 106, value);
            txtDocNumber.MaxLength = Int32.Parse(objDataHierarchyDto.v_Value2);

        }

        private void txtDocNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == 1)
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                    if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        //el resto de teclas pulsadas se desactivan
                        e.Handled = true;
                    }           
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //pbPersonImage.Dispose();
            pbPersonImage.Image = null;
        }

        private void btnWebCam_Click(object sender, EventArgs e)
        {
            try
            {
                frmCamera frm = new frmCamera();
                frm.ShowDialog();

                if (System.Windows.Forms.DialogResult.Cancel != frm.DialogResult)
                {
                    pbPersonImage.Image = frm._Image;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("ddd");
            }
        }

        private void btnCapturedFingerPrintAndRubric_Click(object sender, EventArgs e)
        {
            var frm = new frmCapturedFingerPrint();
            frm.Mode = Mode;

            if (Mode == "Edit")
            {
                frm.FingerPrintTemplate = FingerPrintTemplate;
                frm.FingerPrintImage = FingerPrintImage;
                frm.RubricImage = RubricImage;
                frm.RubricImageText = RubricImageText;
            }

            frm.ShowDialog();

            FingerPrintTemplate = frm.FingerPrintTemplate;
            FingerPrintImage = frm.FingerPrintImage;
            RubricImage = frm.RubricImage;
            RubricImageText = frm.RubricImageText;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
        
    }
}
