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
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Sigesoft.Node.WinClient.UI.Configuration;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmExamenesRealizados : Form
    {    
        private UltraGridBand _banda;
        ServiceBL _serviceBL = new ServiceBL();
        public frmExamenesRealizados()
        {   
            InitializeComponent();
        }

        private void frmExamenesRealizados_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.Select);
          
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

            if (uvPacient.Validate(true, false).IsValid)
            {
                var id1 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

                this.BindGrid(id1[0], id1[1], ddlProtocolId.SelectedValue.ToString());
            }
            
        }

        private void BindGrid(string pstrEmpresaId, string pstrSedeId, string pstrProtocolId)
        {
              using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
            var objData = GetData(pstrEmpresaId, pstrSedeId,pstrProtocolId).ToList();
          
            grdDataService.DataSource = objData;
            if (grdDataService.Rows.Count > 0)
            {
                grdDataService.Rows[0].Selected = true;
                btnExportExcel.Enabled = true;
                btnEditarESO.Enabled = true;
                btnEditar.Enabled = true;
            } 
            if (!grdDataService.Rows.Any()) return;

            _banda = grdDataService.DisplayLayout.Bands[0];


            //this.grdDataService.DisplayLayout.Bands[0].RowLayoutStyle = RowLayoutStyle.GroupLayout;
            //this.grdDataService.DisplayLayout.Override.AllowRowLayoutColMoving = Infragistics.Win.Layout.GridBagLayoutAllowMoving.AllowAll;
            //UltraGridGroup GrupoLaboratorio = _banda.Groups.Add("Grupo Laboratorio");


            _banda.Columns["ServiceId"].Width = 150;
            _banda.Columns["ServiceId"].Hidden = false;
          


            _banda.Columns["Paciente"].Width = 250;      
            _banda.Columns["Paciente"].Hidden = false;
            _banda.Columns["Paciente"].Header.Caption = "Colaborador";

            _banda.Columns["Documento"].Width = 100;
            _banda.Columns["Documento"].Hidden = false;

            _banda.Columns["Cargo"].Width = 100;
            _banda.Columns["Cargo"].Hidden = false;

            _banda.Columns["Empresa"].Width = 150;
            _banda.Columns["Empresa"].Hidden = false;

            _banda.Columns["Perfil"].Width = 100;
            _banda.Columns["Perfil"].Hidden = false;


            _banda.Columns["TipoExamen"].Width = 100;
            _banda.Columns["TipoExamen"].Hidden = false;

            _banda.Columns["Edad"].Width = 50;
            _banda.Columns["Edad"].Hidden = false;

            _banda.Columns["FechaServicio"].Width = 150;
            _banda.Columns["FechaServicio"].Header.Caption = "Fecha: Hora Examen";
            _banda.Columns["FechaServicio"].Hidden = false;

            

          

            _banda.Columns["AcidoUrico_N009_ME000000086"].Width = 150;
            _banda.Columns["AcidoUrico_N009_ME000000086"].Header.Caption = "AcidoUrico";
            //_banda.Columns["AcidoUrico_N009_ME000000086"].RowLayoutColumnInfo.ParentGroup = GrupoLaboratorio;
            if (objData[0].AcidoUrico_N009_ME000000086 != "Borrar")_banda.Columns["AcidoUrico_N009_ME000000086"].Hidden = false;           
            

            _banda.Columns["AglutinacionesLamina_N009_ME000000025"].Width = 150;
            _banda.Columns["AglutinacionesLamina_N009_ME000000025"].Header.Caption = "Aglutinaciones Lamina";
            //_banda.Columns["AglutinacionesLamina_N009_ME000000025"].RowLayoutColumnInfo.ParentGroup = GrupoLaboratorio;
            if (objData[0].AglutinacionesLamina_N009_ME000000025 != "Borrar") _banda.Columns["AglutinacionesLamina_N009_ME000000025"].Hidden = false;     

            _banda.Columns["AntigenoProstatico_N009_ME000000009"].Width = 150;
            _banda.Columns["AntigenoProstatico_N009_ME000000009"].Header.Caption = "AntigenoProstatico";
            //_banda.Columns["AntigenoProstatico_N009_ME000000009"].RowLayoutColumnInfo.ParentGroup = GrupoLaboratorio;
            if (objData[0].AntigenoProstatico_N009_ME000000009 != "Borrar") _banda.Columns["AntigenoProstatico_N009_ME000000009"].Hidden = false;     

           
            _banda.Columns["Bioquimica2_N009_ME000000087"].Width = 150;
            _banda.Columns["Bioquimica2_N009_ME000000087"].Header.Caption = "Bioquimica2";
            if (objData[0].Bioquimica2_N009_ME000000087 != "Borrar") _banda.Columns["Bioquimica2_N009_ME000000087"].Hidden = false;  

            _banda.Columns["BkDirecto_N009_ME000000081"].Width = 150;
            _banda.Columns["BkDirecto_N009_ME000000081"].Header.Caption = "Bk Directo";
            if (objData[0].BkDirecto_N009_ME000000081 != "Borrar") _banda.Columns["BkDirecto_N009_ME000000081"].Hidden = false;  
                 
            
            _banda.Columns["ColesterolTotal_N009_ME000000016"].Width = 150;
            _banda.Columns["ColesterolTotal_N009_ME000000016"].Header.Caption = "Colesterol Total";
            if (objData[0].ColesterolTotal_N009_ME000000016 != "Borrar") _banda.Columns["ColesterolTotal_N009_ME000000016"].Hidden = false;  

          
            _banda.Columns["Creatina_N009_ME000000028"].Width = 150;
            _banda.Columns["Creatina_N009_ME000000028"].Header.Caption = "Creatina";
            if (objData[0].Creatina_N009_ME000000028 != "Borrar") _banda.Columns["Creatina_N009_ME000000028"].Hidden = false;  

        
            _banda.Columns["ExamenCompletoOrina_N009_ME000000046"].Width = 150;
            _banda.Columns["ExamenCompletoOrina_N009_ME000000046"].Header.Caption = "Examen Completo Orina";
            if (objData[0].ExamenCompletoOrina_N009_ME000000046 != "Borrar") _banda.Columns["ExamenCompletoOrina_N009_ME000000046"].Hidden = false;  

            _banda.Columns["VHI_N009_ME000000030"].Width = 150;
            _banda.Columns["VHI_N009_ME000000030"].Header.Caption = "VHI";
            if (objData[0].VHI_N009_ME000000030 != "Borrar") _banda.Columns["VHI_N009_ME000000030"].Hidden = false;  

            _banda.Columns["Fecatest_N009_ME000000097"].Width = 150;
            _banda.Columns["Fecatest_N009_ME000000097"].Header.Caption = "Fecatest";
            if (objData[0].Fecatest_N009_ME000000097 != "Borrar") _banda.Columns["Fecatest_N009_ME000000097"].Hidden = false; 
 
            _banda.Columns["Glucosa_N009_ME000000008"].Width = 150;
            _banda.Columns["Glucosa_N009_ME000000008"].Header.Caption = "Glucosa";
            if (objData[0].Glucosa_N009_ME000000008 != "Borrar") _banda.Columns["Glucosa_N009_ME000000008"].Hidden = false; 
            
            _banda.Columns["GrupoFactorSanguineo_N009_ME000000000"].Width = 150;
            _banda.Columns["GrupoFactorSanguineo_N009_ME000000000"].Header.Caption = "Grupo Factor Sanguineo";
            if (objData[0].GrupoFactorSanguineo_N009_ME000000000 != "Borrar") _banda.Columns["GrupoFactorSanguineo_N009_ME000000000"].Hidden = false; 

            _banda.Columns["HAVIGM_N009_ME000000004"].Width = 150;
            _banda.Columns["HAVIGM_N009_ME000000004"].Header.Caption = "HAVIGM";
            if (objData[0].HAVIGM_N009_ME000000004 != "Borrar") _banda.Columns["HAVIGM_N009_ME000000004"].Hidden = false; 

            _banda.Columns["Hematocrito_N009_ME000000001"].Width = 150;
            _banda.Columns["Hematocrito_N009_ME000000001"].Header.Caption = "Hematocrito";
            if (objData[0].Hematocrito_N009_ME000000001 != "Borrar") _banda.Columns["Hematocrito_N009_ME000000001"].Hidden = false; 

            _banda.Columns["Hemoglobina_N009_ME000000006"].Width = 150;
            _banda.Columns["Hemoglobina_N009_ME000000006"].Header.Caption = "Hemoglobina";
            if (objData[0].Hemoglobina_N009_ME000000006 != "Borrar") _banda.Columns["Hemoglobina_N009_ME000000006"].Hidden = false; 

            _banda.Columns["HemogramaCompleto_N009_ME000000045"].Width = 150;
            _banda.Columns["HemogramaCompleto_N009_ME000000045"].Header.Caption = "Hemograma Completo";
            if (objData[0].HemogramaCompleto_N009_ME000000045 != "Borrar") _banda.Columns["HemogramaCompleto_N009_ME000000045"].Hidden = false;

            _banda.Columns["HEMOGRAMACONSTANTESCORP_N009_ME000000113"].Width = 150;
            _banda.Columns["HEMOGRAMACONSTANTESCORP_N009_ME000000113"].Header.Caption = "HEMOGRAMACONSTANTESCORP";
            if (objData[0].HEMOGRAMACONSTANTESCORP_N009_ME000000113 != "Borrar") _banda.Columns["HEMOGRAMACONSTANTESCORP_N009_ME000000113"].Hidden = false;

            _banda.Columns["HisopadoFaringeo_N009_ME000000095"].Width = 150;
            _banda.Columns["HisopadoFaringeo_N009_ME000000095"].Header.Caption = "Hisopado Faringeo";
            if (objData[0].HisopadoFaringeo_N009_ME000000095 != "Borrar") _banda.Columns["HisopadoFaringeo_N009_ME000000095"].Hidden = false; 

            _banda.Columns["InmunoEnzima_N009_ME000000005"].Width = 150;
            _banda.Columns["InmunoEnzima_N009_ME000000005"].Header.Caption = "Inmuno Enzima";
            if (objData[0].InmunoEnzima_N009_ME000000005 != "Borrar") _banda.Columns["InmunoEnzima_N009_ME000000005"].Hidden = false; 

            _banda.Columns["ParasitologicoSeriado_N009_ME000000049"].Width = 150;
            _banda.Columns["ParasitologicoSeriado_N009_ME000000049"].Header.Caption = "Parasitologico Seriado";
            if (objData[0].ParasitologicoSeriado_N009_ME000000049 != "Borrar") _banda.Columns["ParasitologicoSeriado_N009_ME000000049"].Hidden = false; 

            _banda.Columns["ParasitologicoSimple_N009_ME000000010"].Width = 150;
            _banda.Columns["ParasitologicoSimple_N009_ME000000010"].Header.Caption = "Parasitologico Simple";
            if (objData[0].ParasitologicoSimple_N009_ME000000010 != "Borrar") _banda.Columns["ParasitologicoSimple_N009_ME000000010"].Hidden = false;

            _banda.Columns["PERFILLIPIDICO_N009_ME000000114"].Width = 150;
            _banda.Columns["PERFILLIPIDICO_N009_ME000000114"].Header.Caption = "PERFILLIPIDICO";
            if (objData[0].PERFILLIPIDICO_N009_ME000000114 != "Borrar") _banda.Columns["PERFILLIPIDICO_N009_ME000000114"].Hidden = false;

            _banda.Columns["PerfilHepatico_N009_ME000000096"].Width = 150;
            _banda.Columns["PerfilHepatico_N009_ME000000096"].Header.Caption = "Perfil Hepatico";
            if (objData[0].PerfilHepatico_N009_ME000000096 != "Borrar") _banda.Columns["PerfilHepatico_N009_ME000000096"].Hidden = false; 

            _banda.Columns["PlomoSangre_N009_ME000000060"].Width = 150;
            _banda.Columns["PlomoSangre_N009_ME000000060"].Header.Caption = "Plomo en Sangre";
            if (objData[0].PlomoSangre_N009_ME000000060 != "Borrar") _banda.Columns["PlomoSangre_N009_ME000000060"].Hidden = false; 

            _banda.Columns["SeriologiaLues_N002_ME000000013"].Width = 150;
            _banda.Columns["SeriologiaLues_N002_ME000000013"].Header.Caption = "Seriologia Lues";
            if (objData[0].SeriologiaLues_N002_ME000000013 != "Borrar") _banda.Columns["SeriologiaLues_N002_ME000000013"].Hidden = false; 

            _banda.Columns["SubUniBetaCualitativo_N009_ME000000027"].Width = 150;
            _banda.Columns["SubUniBetaCualitativo_N009_ME000000027"].Header.Caption = "SubUniBetaCualitativo";
            if (objData[0].SubUniBetaCualitativo_N009_ME000000027 != "Borrar") _banda.Columns["SubUniBetaCualitativo_N009_ME000000027"].Hidden = false; 

            _banda.Columns["TGO_N009_ME000000054"].Width = 150;
            _banda.Columns["TGO_N009_ME000000054"].Header.Caption = "TGO";
            if (objData[0].TGO_N009_ME000000054 != "Borrar") _banda.Columns["TGO_N009_ME000000054"].Hidden = false; 

            _banda.Columns["TGP_N009_ME000000055"].Width = 150;
            _banda.Columns["TGP_N009_ME000000055"].Header.Caption = "TGP";
            if (objData[0].TGP_N009_ME000000055 != "Borrar") _banda.Columns["TGP_N009_ME000000055"].Hidden = false; 

            _banda.Columns["TIFICOH_N009_ME000000080"].Width = 150;
            _banda.Columns["TIFICOH_N009_ME000000080"].Header.Caption = "TIFICO H";
            if (objData[0].TIFICOH_N009_ME000000080 != "Borrar") _banda.Columns["TIFICOH_N009_ME000000080"].Hidden = false; 

            _banda.Columns["TIFICOO_N009_ME000000079"].Width = 150;
            _banda.Columns["TIFICOO_N009_ME000000079"].Header.Caption = "TIFICO O";
            if (objData[0].TIFICOO_N009_ME000000079 != "Borrar") _banda.Columns["TIFICOO_N009_ME000000079"].Hidden = false; 

            _banda.Columns["ToxicologicoAlcoholemia_N009_ME000000041"].Width = 150;
            _banda.Columns["ToxicologicoAlcoholemia_N009_ME000000041"].Header.Caption = "Toxicologico Alcoholemia";
            if (objData[0].ToxicologicoAlcoholemia_N009_ME000000041 != "Borrar") _banda.Columns["ToxicologicoAlcoholemia_N009_ME000000041"].Hidden = false; 

            _banda.Columns["ToxicologicoAnfetaminas_N009_ME000000043"].Width = 150;
            _banda.Columns["ToxicologicoAnfetaminas_N009_ME000000043"].Header.Caption = "Toxicologico Anfetaminas";
            if (objData[0].ToxicologicoAnfetaminas_N009_ME000000043 != "Borrar") _banda.Columns["ToxicologicoAnfetaminas_N009_ME000000043"].Hidden = false; 

            _banda.Columns["ToxicologicoBenzodiazepinas_N009_ME000000040"].Width = 150;
            _banda.Columns["ToxicologicoBenzodiazepinas_N009_ME000000040"].Header.Caption = "Toxicologico Benzodiazepinas";
            if (objData[0].ToxicologicoBenzodiazepinas_N009_ME000000040 != "Borrar") _banda.Columns["ToxicologicoBenzodiazepinas_N009_ME000000040"].Hidden = false; 

            _banda.Columns["ToxicologicoCarboxihemoglobina_N002_ME000000042"].Width = 150;
            _banda.Columns["ToxicologicoCarboxihemoglobina_N002_ME000000042"].Header.Caption = "Toxicologico Carboxihemoglobina";
            if (objData[0].ToxicologicoCarboxihemoglobina_N002_ME000000042 != "Borrar") _banda.Columns["ToxicologicoCarboxihemoglobina_N002_ME000000042"].Hidden = false; 

            _banda.Columns["ToxicologicoColinesterasa_N009_ME000000042"].Width = 150;
            _banda.Columns["ToxicologicoColinesterasa_N009_ME000000042"].Header.Caption = "Toxicologico Colinesterasa";
            if (objData[0].ToxicologicoColinesterasa_N009_ME000000042 != "Borrar") _banda.Columns["ToxicologicoColinesterasa_N009_ME000000042"].Hidden = false; 

            _banda.Columns["ToxicologicoCocainaMarihuana_N009_ME000000053"].Width = 150;
            _banda.Columns["ToxicologicoCocainaMarihuana_N009_ME000000053"].Header.Caption = "Toxicologico Cocaina Marihuana";
            if (objData[0].ToxicologicoCocainaMarihuana_N009_ME000000053 != "Borrar") _banda.Columns["ToxicologicoCocainaMarihuana_N009_ME000000053"].Hidden = false; 

            _banda.Columns["Trigliceridos_N002_ME000000012"].Width = 150;
            _banda.Columns["Trigliceridos_N002_ME000000012"].Header.Caption = "Trigliceridos";
            if (objData[0].Trigliceridos_N002_ME000000012 != "Borrar") _banda.Columns["Trigliceridos_N002_ME000000012"].Hidden = false; 

            _banda.Columns["Trigliceridos_N009_ME000000017"].Width = 150;
            _banda.Columns["Trigliceridos_N009_ME000000017"].Header.Caption = "Trigliceridos";
            if (objData[0].Trigliceridos_N009_ME000000017 != "Borrar") _banda.Columns["Trigliceridos_N009_ME000000017"].Hidden = false; 

            _banda.Columns["Urea_N009_ME000000073"].Width = 150;
            _banda.Columns["Urea_N009_ME000000073"].Header.Caption = "Urea";
            if (objData[0].Urea_N009_ME000000073 != "Borrar") _banda.Columns["Urea_N009_ME000000073"].Hidden = false; 

            _banda.Columns["VDRL_N009_ME000000003"].Width = 150;
            _banda.Columns["VDRL_N009_ME000000003"].Header.Caption = "VDRL";
            if (objData[0].VDRL_N009_ME000000003 != "Borrar") _banda.Columns["VDRL_N009_ME000000003"].Hidden = false;

            _banda.Columns["MICROALBUMINURIA_N009_ME000000117"].Width = 150;
            _banda.Columns["MICROALBUMINURIA_N009_ME000000117"].Header.Caption = "MICROALBUMINURIA";
            if (objData[0].MICROALBUMINURIA_N009_ME000000117 != "Borrar") _banda.Columns["MICROALBUMINURIA_N009_ME000000117"].Hidden = false;

            _banda.Columns["HISOPADONASOFARINGEO_N009_ME000000118"].Width = 150;
            _banda.Columns["HISOPADONASOFARINGEO_N009_ME000000118"].Header.Caption = "HISOPADONASOFARINGEO";
            if (objData[0].HISOPADONASOFARINGEO_N009_ME000000118 != "Borrar") _banda.Columns["HISOPADONASOFARINGEO_N009_ME000000118"].Hidden = false;

            _banda.Columns["REACCIONINFLAMATORIA_N009_ME000000119"].Width = 150;
            _banda.Columns["REACCIONINFLAMATORIA_N009_ME000000119"].Header.Caption = "REACCIONINFLAMATORIA";
            if (objData[0].REACCIONINFLAMATORIA_N009_ME000000119 != "Borrar") _banda.Columns["REACCIONINFLAMATORIA_N009_ME000000119"].Hidden = false;

            _banda.Columns["HCV_N009_ME000000120"].Width = 150;
            _banda.Columns["HCV_N009_ME000000120"].Header.Caption = "HCV";
            if (objData[0].HCV_N009_ME000000120 != "Borrar") _banda.Columns["HCV_N009_ME000000120"].Hidden = false;

            _banda.Columns["HBsAg_N009_ME000000121"].Width = 150;
            _banda.Columns["HBsAg_N009_ME000000121"].Header.Caption = "HBsAg";
            if (objData[0].HBsAg_N009_ME000000121 != "Borrar") _banda.Columns["HBsAg_N009_ME000000121"].Hidden = false;

            _banda.Columns["EXAMENKOH_N009_ME000000122"].Width = 150;
            _banda.Columns["EXAMENKOH_N009_ME000000122"].Header.Caption = "EXAMENKOH";
            if (objData[0].EXAMENKOH_N009_ME000000122 != "Borrar") _banda.Columns["EXAMENKOH_N009_ME000000122"].Hidden = false;

            _banda.Columns["TIEMPODESANGRIA_N009_ME000000123"].Width = 150;
            _banda.Columns["TIEMPODESANGRIA_N009_ME000000123"].Header.Caption = "TIEMPODESANGRIA";
            if (objData[0].TIEMPODESANGRIA_N009_ME000000123 != "Borrar") _banda.Columns["TIEMPODESANGRIA_N009_ME000000123"].Hidden = false;

            _banda.Columns["TIEMPODECOAGULACION_N009_ME000000124"].Width = 150;
            _banda.Columns["TIEMPODECOAGULACION_N009_ME000000124"].Header.Caption = "TIEMPODECOAGULACION";
            if (objData[0].TIEMPODECOAGULACION_N009_ME000000124 != "Borrar") _banda.Columns["TIEMPODECOAGULACION_N009_ME000000124"].Hidden = false;

            _banda.Columns["INSULINABASAL_N009_ME000000125"].Width = 150;
            _banda.Columns["INSULINABASAL_N009_ME000000125"].Header.Caption = "INSULINABASAL";
            if (objData[0].INSULINABASAL_N009_ME000000125 != "Borrar") _banda.Columns["INSULINABASAL_N009_ME000000125"].Hidden = false;

            _banda.Columns["INFORMEDELABORATORIO_N009_ME000000002"].Width = 150;
            _banda.Columns["INFORMEDELABORATORIO_N009_ME000000002"].Header.Caption = "INFORMEDELABORATORIO";
            if (objData[0].INFORMEDELABORATORIO_N009_ME000000002 != "Borrar") _banda.Columns["INFORMEDELABORATORIO_N009_ME000000002"].Hidden = false;

            _banda.Columns["Odontograma_N002_ME000000027"].Width = 150;
            _banda.Columns["Odontograma_N002_ME000000027"].Header.Caption = "Odontograma";
            if (objData[0].Odontograma_N002_ME000000027 != "Borrar") _banda.Columns["Odontograma_N002_ME000000027"].Hidden = false; 

            _banda.Columns["Electrocardiograma_N002_ME000000025"].Width = 150;
            _banda.Columns["Electrocardiograma_N002_ME000000025"].Header.Caption = "Electrocardiograma";
            if (objData[0].Electrocardiograma_N002_ME000000025 != "Borrar") _banda.Columns["Electrocardiograma_N002_ME000000025"].Hidden = false; 

            _banda.Columns["PruebaEsfuerzo_N002_ME000000029"].Width = 150;
            _banda.Columns["PruebaEsfuerzo_N002_ME000000029"].Header.Caption = "Prueba Esfuerzo";
            if (objData[0].PruebaEsfuerzo_N002_ME000000029 != "Borrar") _banda.Columns["PruebaEsfuerzo_N002_ME000000029"].Hidden = false; 

            _banda.Columns["EvaCardiologica_N009_ME000000092"].Width = 150;
            _banda.Columns["EvaCardiologica_N009_ME000000092"].Header.Caption = "Eva Cardiologica";
            if (objData[0].EvaCardiologica_N009_ME000000092 != "Borrar") _banda.Columns["EvaCardiologica_N009_ME000000092"].Hidden = false; 
            
            _banda.Columns["RadiografiaTorax_N002_ME000000032"].Width = 150;
            _banda.Columns["RadiografiaTorax_N002_ME000000032"].Header.Caption = "Radiografia Torax";
            if (objData[0].RadiografiaTorax_N002_ME000000032 != "Borrar") _banda.Columns["RadiografiaTorax_N002_ME000000032"].Hidden = false; 

            _banda.Columns["EcografiaRenal_N009_ME000000019"].Width = 150;
            _banda.Columns["EcografiaRenal_N009_ME000000019"].Header.Caption = "Ecografia Renal";
            if (objData[0].EcografiaRenal_N009_ME000000019 != "Borrar") _banda.Columns["EcografiaRenal_N009_ME000000019"].Hidden = false; 

            _banda.Columns["EcografiaProstata_N009_ME000000020"].Width = 150;
            _banda.Columns["EcografiaProstata_N009_ME000000020"].Header.Caption = "Ecografia Prostata";
            if (objData[0].EcografiaProstata_N009_ME000000020 != "Borrar") _banda.Columns["EcografiaProstata_N009_ME000000020"].Hidden = false; 

            _banda.Columns["EcografiaAbdominal_N009_ME000000051"].Width = 150;
            _banda.Columns["EcografiaAbdominal_N009_ME000000051"].Header.Caption = "Ecografia Abdominal";
            if (objData[0].EcografiaAbdominal_N009_ME000000051 != "Borrar") _banda.Columns["EcografiaAbdominal_N009_ME000000051"].Hidden = false; 

            _banda.Columns["RadiografiaOIT_N009_ME000000062"].Width = 150;
            _banda.Columns["RadiografiaOIT_N009_ME000000062"].Header.Caption = "Radiografia OIT";
            if (objData[0].RadiografiaOIT_N009_ME000000062 != "Borrar") _banda.Columns["RadiografiaOIT_N009_ME000000062"].Hidden = false; 

           

            _banda.Columns["FuncionesVitales_N002_ME000000001"].Width = 150;
            _banda.Columns["FuncionesVitales_N002_ME000000001"].Header.Caption = "Funciones Vitales";
            if (objData[0].FuncionesVitales_N002_ME000000001 != "Borrar") _banda.Columns["FuncionesVitales_N002_ME000000001"].Hidden = false; 

            _banda.Columns["Antropometria_N002_ME000000002"].Width = 150;
            _banda.Columns["Antropometria_N002_ME000000002"].Header.Caption = "Antropometria";
            if (objData[0].Antropometria_N002_ME000000002 != "Borrar") _banda.Columns["Antropometria_N002_ME000000002"].Hidden = false;     

            _banda.Columns["ExamenFisico_N002_ME000000022"].Width = 150;
            _banda.Columns["ExamenFisico_N002_ME000000022"].Header.Caption = "Examen Fisico";
            if (objData[0].ExamenFisico_N002_ME000000022 != "Borrar") _banda.Columns["ExamenFisico_N002_ME000000022"].Hidden = false;

            _banda.Columns["ExamenFisico7C_N009_ME000000052"].Width = 150;
            _banda.Columns["ExamenFisico7C_N009_ME000000052"].Header.Caption = "Examen Fisico 7C";
            if (objData[0].ExamenFisico7C_N009_ME000000052 != "Borrar") _banda.Columns["ExamenFisico7C_N009_ME000000052"].Hidden = false; 

            _banda.Columns["ExamenAlturaGeografica_N002_ME000000045"].Width = 150;
            _banda.Columns["ExamenAlturaGeografica_N002_ME000000045"].Header.Caption = "Examen Altura Geografica";
            if (objData[0].ExamenAlturaGeografica_N002_ME000000045 != "Borrar") _banda.Columns["ExamenAlturaGeografica_N002_ME000000045"].Hidden = false; 
            
            _banda.Columns["ExamenOsteomuscular_N002_ME000000046"].Width = 150;
            _banda.Columns["ExamenOsteomuscular_N002_ME000000046"].Header.Caption = "Examen Osteomuscular";
            if (objData[0].ExamenOsteomuscular_N002_ME000000046 != "Borrar") _banda.Columns["ExamenOsteomuscular_N002_ME000000046"].Hidden = false; 

            _banda.Columns["ExamenAlturaEstructural_N009_ME000000015"].Width = 150;
            _banda.Columns["ExamenAlturaEstructural_N009_ME000000015"].Header.Caption = "Examen Altura Estructural";
            if (objData[0].ExamenAlturaEstructural_N009_ME000000015 != "Borrar") _banda.Columns["ExamenAlturaEstructural_N009_ME000000015"].Hidden = false; 

            _banda.Columns["CuestionarioActividadFisica_N009_ME000000018"].Width = 150;
            _banda.Columns["CuestionarioActividadFisica_N009_ME000000018"].Header.Caption = "Cuestionario Actividad Fisica";
            if (objData[0].CuestionarioActividadFisica_N009_ME000000018 != "Borrar") _banda.Columns["CuestionarioActividadFisica_N009_ME000000018"].Hidden = false; 

            _banda.Columns["TamizajeDermatologico_N009_ME000000044"].Width = 150;
            _banda.Columns["TamizajeDermatologico_N009_ME000000044"].Header.Caption = "Tamizaje Dermatologico";
            if (objData[0].TamizajeDermatologico_N009_ME000000044 != "Borrar") _banda.Columns["TamizajeDermatologico_N009_ME000000044"].Hidden = false; 

           

            _banda.Columns["ExamenOsteomuscular2_N009_ME000000084"].Width = 150;
            _banda.Columns["ExamenOsteomuscular2_N009_ME000000084"].Header.Caption = "Examen Osteomuscular 2";
            if (objData[0].ExamenOsteomuscular2_N009_ME000000084 != "Borrar") _banda.Columns["ExamenOsteomuscular2_N009_ME000000084"].Hidden = false; 

            _banda.Columns["EvaluacionNeurologica_N009_ME000000085"].Width = 150;
            _banda.Columns["EvaluacionNeurologica_N009_ME000000085"].Header.Caption = "Evaluacion Neurologica";
            if (objData[0].EvaluacionNeurologica_N009_ME000000085 != "Borrar") _banda.Columns["EvaluacionNeurologica_N009_ME000000085"].Hidden = false; 

            _banda.Columns["CuestionarioNordicoOsteomuscular_N009_ME000000089"].Width = 150;
            _banda.Columns["CuestionarioNordicoOsteomuscular_N009_ME000000089"].Header.Caption = "Cuestionario Nordico Osteomuscular";
            if (objData[0].CuestionarioNordicoOsteomuscular_N009_ME000000089 != "Borrar") _banda.Columns["CuestionarioNordicoOsteomuscular_N009_ME000000089"].Hidden = false; 

            _banda.Columns["TestVertigo_N009_ME000000090"].Width = 150;
            _banda.Columns["TestVertigo_N009_ME000000090"].Header.Caption = "Test Vertigo";
            if (objData[0].TestVertigo_N009_ME000000090 != "Borrar") _banda.Columns["TestVertigo_N009_ME000000090"].Hidden = false; 

            _banda.Columns["OsteoMuscular_N009_ME000000091"].Width = 150;
            _banda.Columns["OsteoMuscular_N009_ME000000091"].Header.Caption = "OsteoMuscular";
            if (objData[0].OsteoMuscular_N009_ME000000091 != "Borrar") _banda.Columns["OsteoMuscular_N009_ME000000091"].Hidden = false; 

            _banda.Columns["VacunaFiebreAmarilla_N009_ME000000063"].Width = 150;
            _banda.Columns["VacunaFiebreAmarilla_N009_ME000000063"].Header.Caption = "Vacuna Fiebre Amarilla";
            if (objData[0].VacunaFiebreAmarilla_N009_ME000000063 != "Borrar") _banda.Columns["VacunaFiebreAmarilla_N009_ME000000063"].Hidden = false; 

            _banda.Columns["VacunaInfluencia_N009_ME000000064"].Width = 150;
            _banda.Columns["VacunaInfluencia_N009_ME000000064"].Header.Caption = "Vacuna Influencia";
            if (objData[0].VacunaInfluencia_N009_ME000000064 != "Borrar") _banda.Columns["VacunaInfluencia_N009_ME000000064"].Hidden = false; 

            _banda.Columns["VacunaDifteria_N009_ME000000065"].Width = 150;
            _banda.Columns["VacunaDifteria_N009_ME000000065"].Header.Caption = "Vacuna Difteria";
            if (objData[0].VacunaDifteria_N009_ME000000065 != "Borrar") _banda.Columns["VacunaDifteria_N009_ME000000065"].Hidden = false; 

            _banda.Columns["VacunaHepatitisA_N009_ME000000066"].Width = 150;
            _banda.Columns["VacunaHepatitisA_N009_ME000000066"].Header.Caption = "Vacuna Hepatitis A";
            if (objData[0].VacunaHepatitisA_N009_ME000000066 != "Borrar") _banda.Columns["VacunaHepatitisA_N009_ME000000066"].Hidden = false; 

            _banda.Columns["VacunaHepatitisB_N009_ME000000067"].Width = 150;
            _banda.Columns["VacunaHepatitisB_N009_ME000000067"].Header.Caption = "Vacuna Hepatitis B";
            if (objData[0].VacunaHepatitisB_N009_ME000000067 != "Borrar") _banda.Columns["VacunaHepatitisB_N009_ME000000067"].Hidden = false; 

            _banda.Columns["VacunaAntirrabica_N009_ME000000068"].Width = 150;
            _banda.Columns["VacunaAntirrabica_N009_ME000000068"].Header.Caption = "Vacuna Antirrabica";
            if (objData[0].VacunaAntirrabica_N009_ME000000068 != "Borrar") _banda.Columns["VacunaAntirrabica_N009_ME000000068"].Hidden = false; 

            _banda.Columns["InfluenzaA1H1N1_N009_ME000000069"].Width = 150;
            _banda.Columns["InfluenzaA1H1N1_N009_ME000000069"].Header.Caption = "Influenza A1H1N1";
            if (objData[0].InfluenzaA1H1N1_N009_ME000000069 != "Borrar") _banda.Columns["InfluenzaA1H1N1_N009_ME000000069"].Hidden = false; 

            _banda.Columns["VacunaTriple_N009_ME000000070"].Width = 150;
            _banda.Columns["VacunaTriple_N009_ME000000070"].Header.Caption = "Vacuna Triple";
            if (objData[0].VacunaTriple_N009_ME000000070 != "Borrar") _banda.Columns["VacunaTriple_N009_ME000000070"].Hidden = false; 

            _banda.Columns["VacunaVaricela_N009_ME000000071"].Width = 150;
            _banda.Columns["VacunaVaricela_N009_ME000000071"].Header.Caption = "Vacuna Varicela";
            if (objData[0].VacunaVaricela_N009_ME000000071 != "Borrar") _banda.Columns["VacunaVaricela_N009_ME000000071"].Hidden = false; 

            _banda.Columns["Oftalmolgia_N002_ME000000028"].Width = 150;
            _banda.Columns["Oftalmolgia_N002_ME000000028"].Header.Caption = "Oftalmolgia";
            if (objData[0].Oftalmolgia_N002_ME000000028 != "Borrar") _banda.Columns["Oftalmolgia_N002_ME000000028"].Hidden = false;

            _banda.Columns["TESTISHIHARA_N009_ME000000093"].Width = 150;
            _banda.Columns["TESTISHIHARA_N009_ME000000093"].Header.Caption = "TESTISHIHARA";
            if (objData[0].TESTISHIHARA_N009_ME000000093 != "Borrar") _banda.Columns["TESTISHIHARA_N009_ME000000093"].Hidden = false;

            _banda.Columns["TESTESTEREOPSIS_N009_ME000000011"].Width = 150;
            _banda.Columns["TESTESTEREOPSIS_N009_ME000000011"].Header.Caption = "Test Estereopsis";
            if (objData[0].TESTESTEREOPSIS_N009_ME000000011 != "Borrar") _banda.Columns["TESTESTEREOPSIS_N009_ME000000011"].Hidden = false; 
                  
            _banda.Columns["TestOjoSeco_N009_ME000000083"].Width = 150;
            _banda.Columns["TestOjoSeco_N009_ME000000083"].Header.Caption = "Test Ojo Seco";
            if (objData[0].TestOjoSeco_N009_ME000000083 != "Borrar") _banda.Columns["TestOjoSeco_N009_ME000000083"].Hidden = false; 

            _banda.Columns["Petrinovic_N009_ME000000098"].Width = 150;
            _banda.Columns["Petrinovic_N009_ME000000098"].Header.Caption = "Petrinovic";
            if (objData[0].Petrinovic_N009_ME000000098 != "Borrar") _banda.Columns["Petrinovic_N009_ME000000098"].Hidden = false; 

            _banda.Columns["Audiometria_N002_ME000000005"].Width = 150;
            _banda.Columns["Audiometria_N002_ME000000005"].Header.Caption = "Audiometria";
            if (objData[0].Audiometria_N002_ME000000005 != "Borrar") _banda.Columns["Audiometria_N002_ME000000005"].Hidden = false; 

            _banda.Columns["Espirometria_N002_ME000000031"].Width = 150;
            _banda.Columns["Espirometria_N002_ME000000031"].Header.Caption = "Espirometria";
            if (objData[0].Espirometria_N002_ME000000031 != "Borrar") _banda.Columns["Espirometria_N002_ME000000031"].Hidden = false; 

            _banda.Columns["Electroencefalograma_N009_ME000000099"].Width = 150;
            _banda.Columns["Electroencefalograma_N009_ME000000099"].Header.Caption = "Electroencefalograma";
            if (objData[0].Electroencefalograma_N009_ME000000099 != "Borrar") _banda.Columns["Electroencefalograma_N009_ME000000099"].Hidden = false;




            

            _banda.Columns["SUBUNIDADBETACUALITATIVO_N009_ME000000027"].Width = 150;
            _banda.Columns["SUBUNIDADBETACUALITATIVO_N009_ME000000027"].Header.Caption = "SUBUNIDADBETACUALITATIVO";
            if (objData[0].SUBUNIDADBETACUALITATIVO_N009_ME000000027 != "Borrar") _banda.Columns["SUBUNIDADBETACUALITATIVO_N009_ME000000027"].Hidden = false;

            _banda.Columns["EVALUACIONPSICOLABORALNOVALE_N009_ME000000072"].Width = 150;
            _banda.Columns["EVALUACIONPSICOLABORALNOVALE_N009_ME000000072"].Header.Caption = "EVALUACIONPSICOLABORALNOVALE";
            if (objData[0].EVALUACIONPSICOLABORALNOVALE_N009_ME000000072 != "Borrar") _banda.Columns["EVALUACIONPSICOLABORALNOVALE_N009_ME000000072"].Hidden = false;

           

            _banda.Columns["HISTORIACLINICAPSICOLOGICA_N009_ME000000112"].Width = 150;
            _banda.Columns["HISTORIACLINICAPSICOLOGICA_N009_ME000000112"].Header.Caption = "HISTORIACLINICAPSICOLOGICA";
            if (objData[0].HISTORIACLINICAPSICOLOGICA_N009_ME000000112 != "Borrar") _banda.Columns["HISTORIACLINICAPSICOLOGICA_N009_ME000000112"].Hidden = false;

            _banda.Columns["InformePsicologico_N002_ME000000033"].Width = 150;
            _banda.Columns["InformePsicologico_N002_ME000000033"].Header.Caption = "Informe Psicologico";
            if (objData[0].InformePsicologico_N002_ME000000033 != "Borrar") _banda.Columns["InformePsicologico_N002_ME000000033"].Hidden = false; 

            _banda.Columns["FATIGA_N002_ME000000034"].Width = 150;
            _banda.Columns["FATIGA_N002_ME000000034"].Header.Caption = "Fatiga";
            if (objData[0].FATIGA_N002_ME000000034 != "Borrar") _banda.Columns["FATIGA_N002_ME000000034"].Hidden = false;

            _banda.Columns["MASLACHBURNOUTESTRES_N002_ME000000036"].Width = 150;
            _banda.Columns["MASLACHBURNOUTESTRES_N002_ME000000036"].Header.Caption = "MASLACHBURNOUTESTRES";
            if (objData[0].MASLACHBURNOUTESTRES_N002_ME000000036 != "Borrar") _banda.Columns["MASLACHBURNOUTESTRES_N002_ME000000036"].Hidden = false;

            _banda.Columns["DEPRESION_N002_ME000000037"].Width = 150;
            _banda.Columns["DEPRESION_N002_ME000000037"].Header.Caption = "DEPRESION";
            if (objData[0].DEPRESION_N002_ME000000037 != "Borrar") _banda.Columns["DEPRESION_N002_ME000000037"].Hidden = false;

            _banda.Columns["FOBIAALASALTURAS_N002_ME000000038"].Width = 150;
            _banda.Columns["FOBIAALASALTURAS_N002_ME000000038"].Header.Caption = "FOBIAALASALTURAS";
            if (objData[0].FOBIAALASALTURAS_N002_ME000000038 != "Borrar") _banda.Columns["FOBIAALASALTURAS_N002_ME000000038"].Hidden = false;

            _banda.Columns["ANSIEDAD_N002_ME000000039"].Width = 150;
            _banda.Columns["ANSIEDAD_N002_ME000000039"].Header.Caption = "ANSIEDAD";
            if (objData[0].ANSIEDAD_N002_ME000000039 != "Borrar") _banda.Columns["ANSIEDAD_N002_ME000000039"].Hidden = false;

            _banda.Columns["ESPACIOSCONFINADOS_N002_ME000000041"].Width = 150;
            _banda.Columns["ESPACIOSCONFINADOS_N002_ME000000041"].Header.Caption = "ESPACIOSCONFINADOS";
            if (objData[0].ESPACIOSCONFINADOS_N002_ME000000041 != "Borrar") _banda.Columns["ESPACIOSCONFINADOS_N002_ME000000041"].Hidden = false;

            
            

            

            _banda.Columns["SOMNOLENCIA_N009_ME000000126"].Width = 150;
            _banda.Columns["SOMNOLENCIA_N009_ME000000126"].Header.Caption = "SOMNOLENCIA";
            if (objData[0].SOMNOLENCIA_N009_ME000000126 != "Borrar") _banda.Columns["SOMNOLENCIA_N009_ME000000126"].Hidden = false;

            _banda.Columns["ACUAMETRIA_N009_ME000000127"].Width = 150;
            _banda.Columns["ACUAMETRIA_N009_ME000000127"].Header.Caption = "ACUAMETRIA";
            if (objData[0].ACUAMETRIA_N009_ME000000127 != "Borrar") _banda.Columns["ACUAMETRIA_N009_ME000000127"].Hidden = false;

            _banda.Columns["EVALUACIONERGONOMICA_N009_ME000000128"].Width = 150;
            _banda.Columns["EVALUACIONERGONOMICA_N009_ME000000128"].Header.Caption = "EVALUACIONERGONOMICA";
            if (objData[0].EVALUACIONERGONOMICA_N009_ME000000128 != "Borrar") _banda.Columns["EVALUACIONERGONOMICA_N009_ME000000128"].Hidden = false;

            _banda.Columns["OTOSCOPIA_N009_ME000000129"].Width = 150;
            _banda.Columns["OTOSCOPIA_N009_ME000000129"].Header.Caption = "OTOSCOPIA";
            if (objData[0].OTOSCOPIA_N009_ME000000129 != "Borrar") _banda.Columns["OTOSCOPIA_N009_ME000000129"].Hidden = false;

            _banda.Columns["RADIOGRAFIALUMBOSACRA_N009_ME000000130"].Width = 150;
            _banda.Columns["RADIOGRAFIALUMBOSACRA_N009_ME000000130"].Header.Caption = "RADIOGRAFIALUMBOSACRA";
            if (objData[0].RADIOGRAFIALUMBOSACRA_N009_ME000000130 != "Borrar") _banda.Columns["RADIOGRAFIALUMBOSACRA_N009_ME000000130"].Hidden = false;

            _banda.Columns["SINTOMATICORESPIRATORIO_N009_ME000000131"].Width = 150;
            _banda.Columns["SINTOMATICORESPIRATORIO_N009_ME000000131"].Header.Caption = "SINTOMATICORESPIRATORIO";
            if (objData[0].SINTOMATICORESPIRATORIO_N009_ME000000131 != "Borrar") _banda.Columns["SINTOMATICORESPIRATORIO_N009_ME000000131"].Hidden = false;

            _banda.Columns["FONDODEOJO_N009_ME000000132"].Width = 150;
            _banda.Columns["FONDODEOJO_N009_ME000000132"].Header.Caption = "FONDODEOJO";
            if (objData[0].FONDODEOJO_N009_ME000000132 != "Borrar") _banda.Columns["FONDODEOJO_N009_ME000000132"].Hidden = false;

            _banda.Columns["CAMPIMETRIA_N009_ME000000133"].Width = 150;
            _banda.Columns["CAMPIMETRIA_N009_ME000000133"].Header.Caption = "CAMPIMETRIA";
            if (objData[0].CAMPIMETRIA_N009_ME000000133 != "Borrar") _banda.Columns["CAMPIMETRIA_N009_ME000000133"].Hidden = false;


            _banda.Columns["TONOMETRIA_N009_ME000000134"].Width = 150;
            _banda.Columns["TONOMETRIA_N009_ME000000134"].Header.Caption = "TONOMETRIA";
            if (objData[0].TONOMETRIA_N009_ME000000134 != "Borrar") _banda.Columns["TONOMETRIA_N009_ME000000134"].Hidden = false;

            _banda.Columns["RADIOGRAFIADECOLUMNACERVICODORSOLUMBAR_N009_ME000000302"].Width = 150;
            _banda.Columns["RADIOGRAFIADECOLUMNACERVICODORSOLUMBAR_N009_ME000000302"].Header.Caption = "RADIOGRAFIADECOLUMNACERVICODORSOLUMBAR";
            if (objData[0].RADIOGRAFIADECOLUMNACERVICODORSOLUMBAR_N009_ME000000302 != "Borrar") _banda.Columns["RADIOGRAFIADECOLUMNACERVICODORSOLUMBAR_N009_ME000000302"].Hidden = false;

            _banda.Columns["TOXICOLOGICO_N009_ME000000303"].Width = 150;
            _banda.Columns["TOXICOLOGICO_N009_ME000000303"].Header.Caption = "TOXICOLOGICO_";
            if (objData[0].TOXICOLOGICO_N009_ME000000303 != "Borrar") _banda.Columns["TOXICOLOGICO_N009_ME000000303"].Hidden = false;

            _banda.Columns["AFECTIVIDAD_N009_ME000000304"].Width = 150;
            _banda.Columns["AFECTIVIDAD_N009_ME000000304"].Header.Caption = "AFECTIVIDAD";
            if (objData[0].AFECTIVIDAD_N009_ME000000304 != "Borrar") _banda.Columns["AFECTIVIDAD_N009_ME000000304"].Hidden = false;

            _banda.Columns["AUTOESTIMA_N009_ME000000305"].Width = 150;
            _banda.Columns["AUTOESTIMA_N009_ME000000305"].Header.Caption = "AUTOESTIMA";
            if (objData[0].AUTOESTIMA_N009_ME000000305 != "Borrar") _banda.Columns["AUTOESTIMA_N009_ME000000305"].Hidden = false;

            _banda.Columns["FOBIASOCIAL02_N009_ME000000306"].Width = 150;
            _banda.Columns["FOBIASOCIAL02_N009_ME000000306"].Header.Caption = "FOBIASOCIAL02";
            if (objData[0].FOBIASOCIAL02_N009_ME000000306 != "Borrar") _banda.Columns["FOBIASOCIAL02_N009_ME000000306"].Hidden = false;

            _banda.Columns["PERSONALIDAD_N009_ME000000307"].Width = 150;
            _banda.Columns["PERSONALIDAD_N009_ME000000307"].Header.Caption = "PERSONALIDAD";
            if (objData[0].PERSONALIDAD_N009_ME000000307 != "Borrar") _banda.Columns["PERSONALIDAD_N009_ME000000307"].Hidden = false;

            _banda.Columns["FOBIASOCIALSOCIALPHOBIAINVENTORYSPIN_N009_ME000000308"].Width = 150;
            _banda.Columns["FOBIASOCIALSOCIALPHOBIAINVENTORYSPIN_N009_ME000000308"].Header.Caption = "FOBIASOCIALSOCIALPHOBIAINVENTORYSPIN";
            if (objData[0].FOBIASOCIALSOCIALPHOBIAINVENTORYSPIN_N009_ME000000308 != "Borrar") _banda.Columns["FOBIASOCIALSOCIALPHOBIAINVENTORYSPIN_N009_ME000000308"].Hidden = false;

            _banda.Columns["TRABAJOENEQUIPO_N009_ME000000309"].Width = 150;
            _banda.Columns["TRABAJOENEQUIPO_N009_ME000000309"].Header.Caption = "TRABAJOENEQUIPO";
            if (objData[0].TRABAJOENEQUIPO_N009_ME000000309 != "Borrar") _banda.Columns["TRABAJOENEQUIPO_N009_ME000000309"].Hidden = false;

            _banda.Columns["AUTOEVALUACION_N009_ME000000310"].Width = 150;
            _banda.Columns["AUTOEVALUACION_N009_ME000000310"].Header.Caption = "AUTOEVALUACION";
            if (objData[0].AUTOEVALUACION_N009_ME000000310 != "Borrar") _banda.Columns["AUTOEVALUACION_N009_ME000000310"].Hidden = false;



            _banda.Columns["ISTASRIESGOPSICOSOCIAL_N009_ME000000311"].Width = 150;
            _banda.Columns["ISTASRIESGOPSICOSOCIAL_N009_ME000000311"].Header.Caption = "ISTASRIESGOPSICOSOCIAL";
            if (objData[0].ISTASRIESGOPSICOSOCIAL_N009_ME000000311 != "Borrar") _banda.Columns["ISTASRIESGOPSICOSOCIAL_N009_ME000000311"].Hidden = false;

            _banda.Columns["WONDERLICIMPRIMIR_N009_ME000000312"].Width = 150;
            _banda.Columns["WONDERLICIMPRIMIR_N009_ME000000312"].Header.Caption = "WONDERLICIMPRIMIR";
            if (objData[0].WONDERLICIMPRIMIR_N009_ME000000312 != "Borrar") _banda.Columns["WONDERLICIMPRIMIR_N009_ME000000312"].Hidden = false;

            _banda.Columns["COMPETENCIASLOGROPIMPRIMIR_N009_ME000000312"].Width = 150;
            _banda.Columns["COMPETENCIASLOGROPIMPRIMIR_N009_ME000000312"].Header.Caption = "COMPETENCIASLOGROPIMPRIMIR";
            if (objData[0].COMPETENCIASLOGROPIMPRIMIR_N009_ME000000312 != "Borrar") _banda.Columns["COMPETENCIASLOGROPIMPRIMIR_N009_ME000000312"].Hidden = false;

            _banda.Columns["CARGAVOCALIMPRIMIR_N009_ME000000314"].Width = 150;
            _banda.Columns["CARGAVOCALIMPRIMIR_N009_ME000000314"].Header.Caption = "CARGAVOCALIMPRIMIR";
            if (objData[0].CARGAVOCALIMPRIMIR_N009_ME000000314 != "Borrar") _banda.Columns["CARGAVOCALIMPRIMIR_N009_ME000000314"].Hidden = false;

            _banda.Columns["SENSIBILIDADMUCOSAEDITARHCL_N009_ME000000315"].Width = 150;
            _banda.Columns["SENSIBILIDADMUCOSAEDITARHCL_N009_ME000000315"].Header.Caption = "SENSIBILIDADMUCOSAEDITARHCL";
            if (objData[0].SENSIBILIDADMUCOSAEDITARHCL_N009_ME000000315 != "Borrar") _banda.Columns["SENSIBILIDADMUCOSAEDITARHCL_N009_ME000000315"].Hidden = false;

            _banda.Columns["VSGEDITARHCL_N009_ME000000316"].Width = 150;
            _banda.Columns["VSGEDITARHCL_N009_ME000000316"].Header.Caption = "VSGEDITARHCL";
            if (objData[0].VSGEDITARHCL_N009_ME000000316 != "Borrar") _banda.Columns["VSGEDITARHCL_N009_ME000000316"].Hidden = false;

            _banda.Columns["TESTTRABAJOENEQUIPONOVALE_N009_ME000000317"].Width = 150;
            _banda.Columns["TESTTRABAJOENEQUIPONOVALE_N009_ME000000317"].Header.Caption = "TESTTRABAJOENEQUIPONOVALE";
            if (objData[0].TESTTRABAJOENEQUIPONOVALE_N009_ME000000317 != "Borrar") _banda.Columns["TESTTRABAJOENEQUIPONOVALE_N009_ME000000317"].Hidden = false;


            _banda.Columns["COMUNICACIONIMPRIMIR_N009_ME000000318"].Width = 150;
            _banda.Columns["COMUNICACIONIMPRIMIR_N009_ME000000318"].Header.Caption = "COMUNICACIONIMPRIMIR";
            if (objData[0].COMUNICACIONIMPRIMIR_N009_ME000000318 != "Borrar") _banda.Columns["COMUNICACIONIMPRIMIR_N009_ME000000318"].Hidden = false;

            _banda.Columns["DESCARTEDEADICCIONESIMPRIMIR_N009_ME000000319"].Width = 150;
            _banda.Columns["DESCARTEDEADICCIONESIMPRIMIR_N009_ME000000319"].Header.Caption = "DESCARTEDEADICCIONESIMPRIMIR";
            if (objData[0].DESCARTEDEADICCIONESIMPRIMIR_N009_ME000000319 != "Borrar") _banda.Columns["DESCARTEDEADICCIONESIMPRIMIR_N009_ME000000319"].Hidden = false;

            _banda.Columns["INTELIGENCIAIMPRIMIR_N009_ME000000320"].Width = 150;
            _banda.Columns["INTELIGENCIAIMPRIMIR_N009_ME000000320"].Header.Caption = "INTELIGENCIAIMPRIMIR";
            if (objData[0].INTELIGENCIAIMPRIMIR_N009_ME000000320 != "Borrar") _banda.Columns["INTELIGENCIAIMPRIMIR_N009_ME000000320"].Hidden = false;

            _banda.Columns["ATENCIONCONCENTRACIONIMPRIMIR_N009_ME000000321"].Width = 150;
            _banda.Columns["ATENCIONCONCENTRACIONIMPRIMIR_N009_ME000000321"].Header.Caption = "ATENCIONCONCENTRACIONIMPRIMIR";
            if (objData[0].ATENCIONCONCENTRACIONIMPRIMIR_N009_ME000000321 != "Borrar") _banda.Columns["ATENCIONCONCENTRACIONIMPRIMIR_N009_ME000000321"].Hidden = false;

            _banda.Columns["MONOTONIAIMPRIMIR_N009_ME000000322"].Width = 150;
            _banda.Columns["MONOTONIAIMPRIMIR_N009_ME000000322"].Header.Caption = "MONOTONIAIMPRIMIR";
            if (objData[0].MONOTONIAIMPRIMIR_N009_ME000000322 != "Borrar") _banda.Columns["MONOTONIAIMPRIMIR_N009_ME000000322"].Hidden = false;

            _banda.Columns["DECLARNODROGASEDITARHCL_N009_ME000000323"].Width = 150;
            _banda.Columns["DECLARNODROGASEDITARHCL_N009_ME000000323"].Header.Caption = "DECLARNODROGASEDITARHCL";
            if (objData[0].DECLARNODROGASEDITARHCL_N009_ME000000323 != "Borrar") _banda.Columns["DECLARNODROGASEDITARHCL_N009_ME000000323"].Hidden = false;

            _banda.Columns["MINIMENTALIMPRIMIR_N009_ME000000324"].Width = 150;
            _banda.Columns["MINIMENTALIMPRIMIR_N009_ME000000324"].Header.Caption = "MINIMENTALIMPRIMIR";
            if (objData[0].MINIMENTALIMPRIMIR_N009_ME000000324 != "Borrar") _banda.Columns["MINIMENTALIMPRIMIR_N009_ME000000324"].Hidden = false;

            _banda.Columns["GINECOLOGIAPAP_N009_ME000000325"].Width = 150;
            _banda.Columns["GINECOLOGIAPAP_N009_ME000000325"].Header.Caption = "GINECOLOGIAPAP";
            if (objData[0].GINECOLOGIAPAP_N009_ME000000325 != "Borrar") _banda.Columns["GINECOLOGIAPAP_N009_ME000000325"].Hidden = false;

            _banda.Columns["LAKELOUISIMPRIMIR_N009_ME000000326"].Width = 150;
            _banda.Columns["LAKELOUISIMPRIMIR_N009_ME000000326"].Header.Caption = "LAKELOUISIMPRIMIR";
            if (objData[0].LAKELOUISIMPRIMIR_N009_ME000000326 != "Borrar") _banda.Columns["LAKELOUISIMPRIMIR_N009_ME000000326"].Hidden = false;

            _banda.Columns["COPROPORFORINAEDITAR_N009_ME000000329"].Width = 150;
            _banda.Columns["COPROPORFORINAEDITAR_N009_ME000000329"].Header.Caption = "COPROPORFORINAEDITAR";
            if (objData[0].COPROPORFORINAEDITAR_N009_ME000000329 != "Borrar") _banda.Columns["COPROPORFORINAEDITAR_N009_ME000000329"].Hidden = false;



            _banda.Columns["COSTO"].Width = 100;
            _banda.Columns["COSTO"].Hidden = false;

            _banda.Columns["IGV"].Width = 100;
            _banda.Columns["IGV"].Hidden = false;

            _banda.Columns["TOTALSERVICIO"].Width = 100;
            _banda.Columns["TOTALSERVICIO"].Hidden = false;
            };

            //lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            //if (grdDataService.Rows.Count > 0)
            //    grdDataService.Rows[0].Selected = true;
        }

        private IEnumerable<ListaExamenes> GetData(string pstrEmpresaId, string pstrSedeId, string pstrProtocolId)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var  _objData = _serviceBL.DevolverExamenesPorProtocolo(pstrEmpresaId, pstrSedeId, pstrProtocolId, pdatBeginDate, pdatEndDate);

            //if (objOperationResult.Success != 1)
            //{
            //    MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            return _objData;
        }

        private void ddlCustomerOrganization_SelectedIndexChanged(object sender, EventArgs e)
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

             Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.Select);          
            
        }

        private void grdDataService_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void grdDataService_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //var x = e.Row.Cells["AcidoUrico_N009_ME000000086"].Value.ToString();
            //if (x == "NO REALIZADO")
            //{
            //    e.Row.Cells["AcidoUrico_N009_ME000000086"].Hidden = true;
            //}
            //else 
            //{
            //    e.Row.Cells["AcidoUrico_N009_ME000000086"].Hidden = false;
            //}

            //var y = e.Row.Cells["Antropometria_N002_ME000000002"].Value.ToString();
            //if (y == "NO REALIZADO")
            //{
            //    e.Row.Cells["Antropometria_N002_ME000000002"].Hidden = true;
            //}
            //else
            //{
            //    e.Row.Cells["Antropometria_N002_ME000000002"].Hidden = false;
            //}
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Detallado del " + dtpDateTimeStar.Value.ToString("dd MM yyyy") + " al " + dptDateTimeEnd.Value.ToString("dd MM yyyy") + " " + ddlCustomerOrganization.Text;

            }
            else
            {
                NombreArchivo = "Consolidado de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos";
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataService, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditarESO_Click(object sender, EventArgs e)
        {
            Form frm;
            string TserviceId = grdDataService.Selected.Rows[0].Cells["ServiceId"].Value.ToString();
            //if (TserviceId == (int)MasterService.AtxMedicaParticular|| TserviceId == (int)MasterService.AtxMedicaSeguros)
            //{
            //    frm = new Operations.frmMedicalConsult(_serviceId, null, null);
            //    frm.ShowDialog();
            //}
            //else
            //{
                this.Enabled = false;
                frm = new Operations.frmEso(TserviceId, null, "View", (int)MasterService.Eso);
                frm.ShowDialog();
                this.Enabled = true;
            //}
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            var frm = new frmProtocolEdit(ddlProtocolId.SelectedValue.ToString(), "Edit");
            frm.ShowDialog();
        }

    }
}
