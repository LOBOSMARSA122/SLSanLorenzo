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
    public partial class frmEntryToMedical : Form
    {

        #region Declarations
            
        string strFilterExpression;
        string _CalendarId;
        string _DocNumber;
        string _PersonId;
        string _AuthorizedPersonId;
        CalendarBL _objCalendarBL = new CalendarBL();
        AuthorizedPersonBL _objAuthorizedPersonBL = new AuthorizedPersonBL();
        List<CalendarList> _objLista = new List<CalendarList>();
        List<AuthorizedPersonList> _objListaAuthorizedPerson = new List<AuthorizedPersonList>();

        #endregion

        public frmEntryToMedical()
        {
            InitializeComponent();
        }

        private void frmEntryToMedical_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlDocTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null), DropDownListAction.Select);
            ddlDocTypeId.SelectedValue = "1";
           
            _objLista = _objCalendarBL.GetCalendarsPagedAndFiltered1(ref objOperationResult, 0, null, "i_CalendarStatusId ASC , d_EntryTimeCM ASC", null, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            grdData.DataSource = _objLista;
            lblRecordCountTotal.Text = "Total : " + _objLista.Count.ToString();
            lblRecordCountPendiente.Text = "Pendientes : " + _objLista.FindAll(p => p.i_CalendarStatusId == (int)CalendarStatus.Agendado).Count();
            txtDocNumber.Select();


            //Cargar Grilla de Persona Autorizadas
            _objListaAuthorizedPerson = _objAuthorizedPersonBL.GetAuthorizedPersonPagedAndFiltered(ref objOperationResult, 0, null, "", "");
            grdDataPeopleAuthoritation.DataSource = _objListaAuthorizedPerson;


        }

        private void btnFilter_Click(object sender, EventArgs e)
        {            
            if (uvCM.Validate(true, false).IsValid)
            {
            List<string> Filters = new List<string>();
            if (ddlDocTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_DocTypeId==" + ddlDocTypeId.SelectedValue);
            if (!string.IsNullOrEmpty(txtDocNumber.Text)) Filters.Add("v_DocNumber==" + "\"" + txtDocNumber.Text.Trim() + "\"");
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }
            this.BindGrid();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtDocNumber.Text = "";
        }

        private void BindGrid()
        {
            CalendarBL objCalendarBL = new CalendarBL();
            calendarDto objcalendarDto = new calendarDto();
            PacientBL objPacientBL = new PacientBL();            
            PacientList objPacientDto = new PacientList();

            OperationResult objOperationResult = new OperationResult();

            var objData = GetData(0, null, "", strFilterExpression);
            //Validar que el trabajador exista en el sistema
            objPacientDto = objPacientBL.GetPacient(ref objOperationResult, null, txtDocNumber.Text.Trim());
            //_CalendarId = objData[0].v_CalendarId;
            if (objPacientDto == null)
            {
                MessageBox.Show("Este Trabajador no está ingresado en el sistema SIGESOFT.", "NO SE ENCONTRÓ TRABAJADOR!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                string Pacient = objPacientDto.v_FirstName + " " + objPacientDto.v_FirstLastName + " " + objPacientDto.v_SecondLastName;
          
            if (objData.Count == 0)
            {
                MessageBox.Show("El trabajado " + Pacient + " no está agendado. Comuníquese con el área de Recepción.", "NO SE ENCONTRÓ CITA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (objData[0].i_CalendarStatusId == (int)CalendarStatus.Cancelado)
                {
                    MessageBox.Show("La cita del trabajor " + Pacient + " ha sido cancelada. Comuníquese con el área de recepción.", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }   
                if (objData[0].i_CalendarStatusId != (int)CalendarStatus.Agendado)
                {
                    MessageBox.Show("El trabajor " + Pacient + " ya está dentro del Centro Médico.", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                        

                DialogResult Result = MessageBox.Show("¿Desea registar el ingreso de " + Pacient + " al centro médico?", "TRABAJADOR AGENDADO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {

                    foreach (var item in objData)
                    {
                        objcalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, item.v_CalendarId);
                        objcalendarDto.d_EntryTimeCM = DateTime.Now;

                        objcalendarDto.i_CalendarStatusId = (int)CalendarStatus.Ingreso;
                        objCalendarBL.UpdateCalendar(ref objOperationResult, objcalendarDto, Globals.ClientSession.GetAsList());

                    }
                    
                   

                }
                _objLista = _objCalendarBL.GetCalendarsPagedAndFiltered1(ref objOperationResult, 0, null, "i_CalendarStatusId ASC , d_EntryTimeCM ASC", null, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
                grdData.DataSource = _objLista;
                lblRecordCountTotal.Text = "Total : " + _objLista.Count.ToString();
                lblRecordCountPendiente.Text = "Pendientes : " + _objLista.FindAll(p => p.i_CalendarStatusId == (int)CalendarStatus.Agendado).Count();
            
            }
        }

        private List<CalendarList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = DateTime.Now.Date;
            DateTime? pdatEndDate = DateTime.Now.Date.AddDays(1);

            var _objData = _objCalendarBL.GetCalendarsPagedAndFiltered1(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (_objData.Count !=0)
            {
                _CalendarId = _objData[0].v_CalendarId;
            }
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _objData;
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

        private void ddlDocTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocTypeId.Text == "--Seleccionar--") return;

            OperationResult objOperationResult = new OperationResult();
            DataHierarchyBL objDataHierarchyBL = new DataHierarchyBL();
            datahierarchyDto objDataHierarchyDto = new datahierarchyDto();

            int value = Int32.Parse(ddlDocTypeId.SelectedValue.ToString());
            objDataHierarchyDto = objDataHierarchyBL.GetDataHierarchy(ref objOperationResult, 106, value);
            txtDocNumber.MaxLength = Int32.Parse(objDataHierarchyDto.v_Value2);
        }

        private void mnuGridCancelar_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            calendarDto objcalendarDto = new calendarDto();
            OperationResult objOperationResult = new OperationResult();

           
            for (int i = 0; i < _objLista.Count; i++)
            {
                if (_DocNumber == _objLista[i].v_DocNumber)
                {
                    objcalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, _objLista[i].v_CalendarId);


                    objcalendarDto.d_EntryTimeCM = (DateTime?)null;
                    objcalendarDto.i_CalendarStatusId = (int)CalendarStatus.Agendado;
                    objCalendarBL.UpdateCalendar(ref objOperationResult, objcalendarDto, Globals.ClientSession.GetAsList());

                }
            }
            _objLista = _objCalendarBL.GetCalendarsPagedAndFiltered1(ref objOperationResult, 0, null, "i_CalendarStatusId ASC , d_EntryTimeCM ASC", null, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            grdData.DataSource = _objLista;
            lblRecordCountTotal.Text = "Total : " + _objLista.Count.ToString();
            lblRecordCountPendiente.Text = "Pendientes : " + _objLista.FindAll(p => p.i_CalendarStatusId == (int)CalendarStatus.Agendado).Count();
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdData.Rows[row.Index].Selected = true;
                    _CalendarId = grdData.Rows[row.Index].Cells["v_CalendarId"].Value.ToString();
                    _DocNumber = grdData.Rows[row.Index].Cells["v_DocNumber"].Value.ToString();
                    _PersonId = grdData.Rows[row.Index].Cells["v_PersonId"].Value.ToString();
                    if (grdData.Rows[row.Index].Cells["i_CalendarStatusId"].Value.ToString() == ((int)CalendarStatus.Ingreso).ToString())
                    {
                        contextMenuStrip1.Items["mnuGridCancelar"].Enabled = true;
                    }
                    else
                    {
                        contextMenuStrip1.Items["mnuGridCancelar"].Enabled = false;
                    }
                   
                }
                else
                {
                    contextMenuStrip1.Items["mnuGridCancelar"].Enabled = false;
                }

            } 
        }

        private void lblRecordCountPendiente_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateandSelect_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            calendarDto objcalendarDto = new calendarDto();
            OperationResult objOperationResult = new OperationResult();


            for (int i = 0; i < _objLista.Count; i++)
            {
                if (_DocNumber == _objLista[i].v_DocNumber)
                {
                    objcalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, _objLista[i].v_CalendarId);


                    objcalendarDto.d_EntryTimeCM = (DateTime?)null;
                    objcalendarDto.i_CalendarStatusId = (int)CalendarStatus.Agendado;
                    objCalendarBL.UpdateCalendar(ref objOperationResult, objcalendarDto, Globals.ClientSession.GetAsList());

                }
            }
            _objLista = _objCalendarBL.GetCalendarsPagedAndFiltered1(ref objOperationResult, 0, null, "i_CalendarStatusId ASC , d_EntryTimeCM ASC", null, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            grdData.DataSource = _objLista;
            lblRecordCountTotal.Text = "Total : " + _objLista.Count.ToString();
            lblRecordCountPendiente.Text = "Pendientes : " + _objLista.FindAll(p => p.i_CalendarStatusId == (int)CalendarStatus.Agendado).Count();
        }

        private void btnSearchAuthoritation_Click(object sender, EventArgs e)
        {
            if (uvPersonAuthoritation.Validate(true, false).IsValid)
            {
                this.BindGridPersonAuthoritation();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtDocNumber.Text = "";
        }

        private void BindGridPersonAuthoritation()
        {
            AuthorizedPersonBL objAuthorizedPersonBL = new AuthorizedPersonBL();
            authorizedpersonDto objAuthorizedPersonDto = new authorizedpersonDto();
            OperationResult objOperationResult = new OperationResult();

            string pstrFilterExpression = "(v_Pacient.Contains(\"" + txtNameOrOrganization.Text.Trim() + "\")" + ")" + "||" + "(v_OrganitationName.Contains(\"" + txtNameOrOrganization.Text.Trim() + "\")" + ")";
            _objListaAuthorizedPerson = objAuthorizedPersonBL.GetAuthorizedPersonPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression);
            grdDataPeopleAuthoritation.DataSource = _objListaAuthorizedPerson;

        }

        private List<AuthorizedPersonList> GetDataPersonAuthoritation(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();

            var _objData = _objAuthorizedPersonBL.GetAuthorizedPersonPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);
                        
            return _objData;
        }

        private void grdDataPeopleAuthoritation_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            //Si el contenido de la columna Vip es igual a SI
            if (e.Row.Cells["d_EntryToMedicalCenter"].Value != null)
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.LightGreen;
                //e.Row.Appearance.BackColor2 = Color.White;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;

            }
        }

        private void grdData_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            //Si el contenido de la columna Vip es igual a SI
            if (e.Row.Cells["i_CalendarStatusId"].Value.ToString() == ((int)CalendarStatus.Ingreso).ToString())
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.LightGreen;
                //e.Row.Appearance.BackColor2 = Color.White;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;

            }
            if (e.Row.Cells["i_CalendarStatusId"].Value.ToString() == ((int)CalendarStatus.Atendido).ToString())
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.LightSkyBlue;
                //e.Row.Appearance.BackColor2 = Color.White;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;

            }
        }

        private void btnCancelPersonAuthoritation_Click(object sender, EventArgs e)
        {
            authorizedpersonDto oauthorizedpersonDto = new authorizedpersonDto();
            AuthorizedPersonBL oAuthorizedPersonBL = new AuthorizedPersonBL();
           
            OperationResult objOperationResult = new OperationResult();

            oauthorizedpersonDto = oAuthorizedPersonBL.GetAuthorizedPerson(ref objOperationResult, _AuthorizedPersonId);

            oauthorizedpersonDto.d_EntryToMedicalCenter = (DateTime?)null;
            oAuthorizedPersonBL.UpdateAuthorizedPerson(ref objOperationResult, oauthorizedpersonDto, Globals.ClientSession.GetAsList());


            _objListaAuthorizedPerson = oAuthorizedPersonBL.GetAuthorizedPersonPagedAndFiltered(ref objOperationResult, 0, null, "", "");
            grdDataPeopleAuthoritation.DataSource = _objListaAuthorizedPerson;
            //lblRecordCountTotal.Text = "Total : " + _objLista.Count.ToString();
            //lblRecordCountPendiente.Text = "Pendientes : " + _objLista.FindAll(p => p.i_CalendarStatusId == (int)CalendarStatus.Agendado).Count();

            MessageBox.Show("Se cancelo el ingreso al trabajar " + _objListaAuthorizedPerson[0].v_FirstName + " " + _objListaAuthorizedPerson[0].v_FirstLastName + " " + _objListaAuthorizedPerson[0].v_SecondLastName , "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void grdDataPeopleAuthoritation_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdDataPeopleAuthoritation.Rows[row.Index].Selected = true;
                    _AuthorizedPersonId = grdDataPeopleAuthoritation.Rows[row.Index].Cells["v_AuthorizedPersonId"].Value.ToString();

                    if (grdDataPeopleAuthoritation.Rows[row.Index].Cells["d_EntryToMedicalCenter"].Value != null)
                    {
                        //contextMenuStrip1.Items["mnuGridCancelar"].Enabled = true;
                        btnCancelPersonAuthoritation.Enabled = true;
                    }
                    else
                    {
                        //contextMenuStrip1.Items["mnuGridCancelar"].Enabled = false;
                        btnCancelPersonAuthoritation.Enabled = false;
                    }

                }
                else
                {
                    //contextMenuStrip1.Items["mnuGridCancelar"].Enabled = false;
                    btnCancelPersonAuthoritation.Enabled = false;
                }

            } 
        }

        private void grdDataPeopleAuthoritation_DoubleClick(object sender, EventArgs e)
        {
              OperationResult objOperationResult = new OperationResult();
              authorizedpersonDto objAuthorizedPersonDto = new authorizedpersonDto();
             objAuthorizedPersonDto = _objAuthorizedPersonBL.GetAuthorizedPerson(ref objOperationResult, _AuthorizedPersonId);
            if (objAuthorizedPersonDto == null)
            {
                MessageBox.Show("Este Trabajador no está ingresado en la LISTA DE AUTORIZADOS.", "NO SE ENCONTRÓ TRABAJADOR!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string Pacient = objAuthorizedPersonDto.v_FirstName + " " + objAuthorizedPersonDto.v_FirstLastName + " " + objAuthorizedPersonDto.v_SecondLastName;

                DialogResult Result = MessageBox.Show("¿Desea registar el ingreso de " + Pacient + " al centro médico?", "TRABAJADOR AGENDADO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    objAuthorizedPersonDto.d_EntryToMedicalCenter = DateTime.Now;
                    _objAuthorizedPersonBL.UpdateAuthorizedPerson(ref objOperationResult, objAuthorizedPersonDto, Globals.ClientSession.GetAsList());
                    _objListaAuthorizedPerson = _objAuthorizedPersonBL.GetAuthorizedPersonPagedAndFiltered(ref objOperationResult, 0, null, "", "");
                    grdDataPeopleAuthoritation.DataSource = _objListaAuthorizedPerson;
                }
            }
        }

        private void txtNameOrOrganization_TextChanged(object sender, EventArgs e)
        {
            this.BindGridPersonAuthoritation();
        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnUpdateandSelect.Enabled = (grdData.Selected.Rows.Count > 0);

            if (grdData.Selected.Rows.Count == 0)
                return;


        }


    }
}
