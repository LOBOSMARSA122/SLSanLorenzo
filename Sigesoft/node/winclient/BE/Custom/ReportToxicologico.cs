using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportToxicologico
    {
        public string ServiceId { get; set; }//
        public DateTime? Fecha { get; set; }
        public string UBIGEO { get; set; }
        public String v_DepartamentName { get; set; }
        public string v_ProvinceName { get; set; }
        public string v_DistrictName { get; set; }
      
        public string Trabajador { get; set; }
        public int Edad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string EmpresaTrabajador { get; set; }
        public byte[] FirmaMedico { get; set; }
        public byte[] FirmaMedicina { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public string Puesto { get; set; }
        public string Empresa { get; set; }
        public string COCAINA { get; set; }
        public string MARIHUANA { get; set; }
        public byte[] LogoEmpresa { get; set; }
        public string Dni { get; set; }
        public string NombreUsuarioGraba { get; set; }
        public string ANFETAMINA { get; set; }
        public string ALCOHOLEMIA { get; set; }
        public string ALCOHOLEMIA_DESEABLE { get; set; }
        public string BENZODIACEPINA { get; set; }
        public string BENZODIACEPINA_DESEABLE { get; set; }
        public string COLINESTERASA { get; set; }
        public string COLINESTERASA_DESEABLE { get; set; }

        public string CARBOXIHEMOGLOBINA { get; set; }
        public string CARBOXIHEMOGLOBINA_DESEABLE { get; set; }


        public string MUESTRA{ get; set; }
        public string METODO{ get; set; }
        public string _1SUFRE_ENFREMEDAD{ get; set; }
        public string _1DIGA{ get; set; }
        public string _2CONSUME_MEDICAMENTO{ get; set; }
        public string _2DIGA{ get; set; }
        public string _3TOMA_MATE{ get; set; }
        public string _3CUANTAS_VECES{ get; set; }
        public string _3CUANDO_ULTIMA_VEZ{ get; set; }
        public string _4CONSUME_PRODUCTOS{ get; set; }
        public string _4CUANTAS_VECES{ get; set; }
        public string _4CUANDO_ULTIMA_VEZ{ get; set; }
        public string _5ANESTESIA{ get; set; }
        public string DIAGNOSTICO_EXAMEN{ get; set; }
        public string NOMBRE_EMPRESA_CLIENTE{ get; set; }

        public string Lote { get; set; }
        public string Lote_marihuana { get; set; }
        public string Motivo_Prueba { get; set; }

        public string Aliento { get; set; }

        public string Extasis { get; set; }
        public string Opiaceos { get; set; }
        public string Oxicodona { get; set; }
        public string Metadano { get; set; }
        public string Cocaina { get; set; }

        public string Antetaminas { get; set; }
        public string Metanfetamina { get; set; }
        public string Marihuana { get; set; }
        public string Benzodiacepinas { get; set; }
        public string Barbituricos { get; set; }


    }

}
