using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
   public  class ReporteAltura18_CI
    {
        public string EmpresaCliente { get; set; }
        public string Ficha { get; set; }
        public string Hc { get; set; }
        public string NombreTrabajador { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaNacimiento { get; set; }
       
        public byte[] FirmaMedico { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public string NombreUsuarioGraba { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }


        public string ACTIVIDAD_A_REALIZAR{ get; set; }
        public string TRABAJOS_PREVIOS_SOBRE_NIVEL_USO_DE_ARNS{ get; set; }
        public string CARDIOVASCULARES_Y_RESPIRATORIOS{ get; set; }
        public string QUIRRGICOS_{ get; set; }
        public string FOBIAS_ACROFOBIA_AGAROFOBIA{ get; set; }
        public string ANTECEDENTES_DE_USO_O_ABUSO_DE_ALCOHOL_Y_DROGAS{ get; set; }
        public string _FRMACOS_DE_CONSUMO_ACTUAL{ get; set; }
        public string FAMILIARES__PSIQUITRICOS_{ get; set; }
        public string APARATO_CARDIOVASCULAR{ get; set; }
        public string APARATO_RESPIRATORIO{ get; set; }
        public string SISTEMA_NERVIOSO{ get; set; }
        public string NISTAGMUS_ESPONTNEO_{ get; set; }
        public string MANIFESTACIONES_O_ESTIGMAS_SUGESTIVOS_DE_ALCOHOLISMO{ get; set; }
        public string RECIBI_ENTRENAMIENTO_EN_PRIMEROS_AUXILIOS{ get; set; }
        public string TIMPANOS_NORMALES_{ get; set; }
        public string EQUILIBRIO_NORMAL_{ get; set; }
        public string SUSTENTACIN_EN_PIE_POR_20{ get; set; }
        public string CAMINAR_LIBRE_SOBRE_RECTA_SIN_DESVO_{ get; set; }
        public string _ADIADOCOCINESIA_DIRECTA_{ get; set; }
        public string _INDICE_NARIZ{ get; set; }
        public string RECIBIO_CURSO_DE_SEGURIDAD_PARA_TRABAJO_EN_ALTURA_MAYOR_18M_{ get; set; }
        public string APTITUD__{ get; set; }
public string OBSERVACIONES{ get; set; }


public string PuestoTrabajo { get; set; }
public int? Edad { get; set; }
public string Talla { get; set; }
public string Peso { get; set; }
public string PresionArterialSistolica { get; set; }
public string PresionArterialDiastolica { get; set; }
public string FrecCard { get; set; }
public string FrecResp { get; set; }
public string DxEkg { get; set; }
public string ColesterolTotal { get; set; }
public string Trigliceridos { get; set; }
    }
}
