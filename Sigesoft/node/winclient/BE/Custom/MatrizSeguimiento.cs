using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MatrizSeguimiento
    {
        public string IdServicio { get; set; }
        public string IdTrabajador { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        /////////// DATOS PERSONA /////////////////
        public int Nro { get; set; }
        public string Tipo_Documento { get; set; }
        public int Tipo_Documento_ID { get; set; }
        public string Nro_Documento { get; set; }
        public string Nombres { get; set; }
        public string AP_Paterno { get; set; }
        public string AP_Materno { get; set; }
        public DateTime Fecha_Nac { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public int Genero_ID { get; set; }
        public string Grado_Inst { get; set; }
        public int Grado_Inst_ID { get; set; }
        public string Puesto_Laboral { get; set; }
        public string Area { get; set; }
        public string Zona { get; set; }
        public int Zona_ID { get; set; }
        public string Lugar_de_Trabajo { get; set; }
        public string Discapacitado { get; set; }
        public int Discapacitado_ID { get; set; }
        public string Proveedor_Clinica { get; set; }
        public string RUC { get; set; }
        public DateTime Fecha_Examen { get; set; }
        public string Tipo_Examen { get; set; }
        public int Tipo_Examen_ID { get; set; }
        public string Aptitud { get; set; }
        public int Aptitud_ID { get; set; }
        ///////////// HABITOS NOSCIVOS ////////////////
        public string Fumar { get; set; }
        public int Fumar_ID { get; set; }
        public string Licor { get; set; }
        public int Licor_ID { get; set; }
        public string Drogas { get; set; }
        public int Drogas_ID { get; set; }
        /////////////////// TRIAJE //////////////////////////
        public decimal Peso { get; set; }
        public decimal Talla { get; set; }
        public decimal IMC { get; set; }
        public string IMC_CIE10 { get; set; }
        public string IMC_Obs { get; set; }
        public decimal Cintura { get; set; }
        public decimal Cadera { get; set; }
        public decimal ICC { get; set; }
        public string Sistolica { get; set; }
        public string Sistolica_CIE10 { get; set; }
        public string Sistolica_Obs { get; set; }
        public string Diastolica { get; set; }
        public string Diastolica_CIE10 { get; set; }
        public string Diastolica_Obs { get; set; }
        public decimal FC { get; set; }
        public decimal FR { get; set; }
        /////////////////// OFTALMO //////////////////////////
        public string Sin_Corr_Cerca_OD { get; set; }
        public string Sin_Corr_Cerca_OI { get; set; }
        public string Sin_Corr_Lejos_OD { get; set; }
        public string Sin_Corr_Lejos_OI { get; set; }
        public string Corr_Cerca_OD { get; set; }
        public string Corr_Cerca_OI { get; set; }
        public string Corr_Lejos_OD { get; set; }
        public string Corr_Lejos_OI { get; set; }
        public string OD_CIE10 { get; set; }
        public string OD_Obs { get; set; }
        public string OI_CIE10 { get; set; }
        public string OI_Obs { get; set; }
        public string Discro { get; set; }
        public string Discro_CIE10 { get; set; }
        public string Discro_Obs { get; set; }
        ///////////////// AUDIOMETRIA ////////////////////////
        public string Otoscopia_OD { get; set; }
        public string Otoscopia_OI { get; set; }

        public string Oido_Der_125 { get; set; }
        public string Oido_Der_250 { get; set; }
        public string Oido_Der_500 { get; set; }
        public string Oido_Der_750 { get; set; }
        public string Oido_Der_1000 { get; set; }
        public string Oido_Der_1500 { get; set; }
        public string Oido_Der_2000 { get; set; }
        public string Oido_Der_3000 { get; set; }
        public string Oido_Der_4000 { get; set; }
        public string Oido_Der_6000 { get; set; }
        public string Oido_Der_8000 { get; set; }

        public string Oido_Izq_125 { get; set; }
        public string Oido_Izq_250 { get; set; }
        public string Oido_Izq_500 { get; set; }
        public string Oido_Izq_750 { get; set; }
        public string Oido_Izq_1000 { get; set; }
        public string Oido_Izq_1500 { get; set; }
        public string Oido_Izq_2000 { get; set; }
        public string Oido_Izq_3000 { get; set; }
        public string Oido_Izq_4000 { get; set; }
        public string Oido_Izq_6000 { get; set; }
        public string Oido_Izq_8000 { get; set; }

        public string Audiometria_D_CIE10 { get; set; }
        public string Audiometria_D_Obs { get; set; }
        public string Audiometria_I_CIE10 { get; set; }
        public string Audiometria_I_Obs { get; set; }
        ///////////////// LABORATORIO //////////////////////////
        public string Grupo_Sanguineo { get; set; }
        public int Grupo_Sanguineo_ID { get; set; }
        public string Factor_RH { get; set; }
        public int Factor_RH_ID { get; set; }
        public decimal Hemoglobina { get; set; }
        public string Hemoglobina_CIE10 { get; set; }
        public string Hemoglobina_Obs { get; set; }
        public decimal Colesterol { get; set; }
        public string Colesterol_CIE10 { get; set; }
        public string Colesterol_Obs { get; set; }
        public decimal Trigliceridos { get; set; }
        public string Trigliceridos_CIE10 { get; set; }
        public string Trigliceridos_Obs { get; set; }
        public decimal Glucosa { get; set; }
        public string Glucosa_CIE10 { get; set; }
        public string Glucosa_Obs { get; set; }
        /////////////////// ESPIRO /////////////////////////////
        public string FEV1_Teorico { get; set; }
        public string FVC_Teorico { get; set; }
        public string Espiro_CIE10 { get; set; }
        public string Espiro_Obs { get; set; }
        //////////////// MEDICINA ////////////////////////////
        public string Osteomuscular_CIE10 { get; set; }
        public string Osteomuscular_Obs { get; set; }
        public string Clinico_CIE10 { get; set; }
        public string Clinico_Obs { get; set; }
        /////////////////// ODONTO /////////////////////////
        public string Odonto_CIE10 { get; set; }
        public string Odonto_Obs { get; set; }
        /////////////////////// EKG ////////////////////////////
        public string EKG_CIE10 { get; set; }
        public string EKG_Obs { get; set; }
        ///////////////////// RAYOS X /////////////////////////
        public string Rayos_X_CIE10 { get; set; }
        public string Rayos_X_Obs { get; set; }
        ///////////////////// PSICOLOGIA //////////////////////
        public string Psico_CIE10 { get; set; }
        public string Psico_Obs { get; set; }
    }
}
