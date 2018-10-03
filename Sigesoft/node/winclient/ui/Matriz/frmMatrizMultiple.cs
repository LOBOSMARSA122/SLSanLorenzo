﻿using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Matriz
{
    
    public partial class frmMatrizMultiple : Form
    {
        string strFilterExpression = "";
        public frmMatrizMultiple()
        {
            InitializeComponent();
        }

        private void frmMatrizMultiple_Load(object sender, EventArgs e)
        {
            DateTime fechatemp = DateTime.Today;
            DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

            OperationResult objOperationResult = new OperationResult();
            //var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, 9);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);

            dtpDateTimeStar.Value = fecha1;
            dptDateTimeEnd.Value = DateTime.Today;
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
        }


        private void ddlCustomerOrganization_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                return;
            }
            ddlProtocolId.Enabled = true;
            OperationResult objOperationResult = new OperationResult();
            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                List<string> Filters = new List<string>();
                DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
                DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

                if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
                }

                if (ddlProtocolId.SelectedValue.ToString() != "-1")
                    Filters.Add("ProtocolId==" + "\"" + ddlProtocolId.SelectedValue + "\"");

                // Create the Filter Expression
                strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " && ";
                    }
                    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
                }

                if (tabControl1.SelectedTab.Name == "tpShauindo")
                {
                    var objData = new PacientBL().ReporteMatrizShauindo(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdShauindo.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }
                else if (tabControl1.SelectedTab.Name == "tpLaZanja")
                {
                    var objData = new PacientBL().ReporteMatrizLaZanja(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdLaZanja.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }
                else if (tabControl1.SelectedTab.Name == "tpGoldFields")
                {
                    var objData = new PacientBL().ReporteMatrizGoldFields(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdGolFields.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

                }
                else if (tabControl1.SelectedTab.Name == "toSolucManteIntegra")
                {
                    var objData = new PacientBL().ReporteMatrizSolucManteIntegra(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdSoluc.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }
                else if (tabControl1.SelectedTab.Name == "tpMiBanco")
                {
                    var objData = new PacientBL().ReporteMatrizMiBanco(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdMiBanco.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }

            }
        }

        private void btnExportLaZanja_Click(object sender, EventArgs e)
        {

        }

        private void btnExportGolFields_Click(object sender, EventArgs e)
        {

        }

        private void btnExportSoluc_Click(object sender, EventArgs e)
        {

        }

        private void btnExportMiBanco_Click(object sender, EventArgs e)
        {

        }

        private void grdLaZanja_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }
    }
}
