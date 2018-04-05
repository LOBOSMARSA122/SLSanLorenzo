using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MatrizAbreviada
    {
        
            
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string IdProtocolId { get; set; }
        
        public string ServiceId { get; set; }
        public string Nombre_Completo { get; set; }
        public string DNI { get; set; }
        public string Examen_Solicitado_Por { get; set; }
        public string Tipo_Examen { get; set; }
        public string Grupo_Riesgo { get; set; }
        public string Puesto_Postula { get; set; }
        public DateTime? Fecha_Examen { get; set; }
        public string Grupo_Sanguineo { get; set; }
        public string Resumen_Medico { get; set; }
        public string Recomendaciones { get; set; }
        public string Restricciones { get; set; }
        public string Aptitud_Medica { get; set; }
        public string Medico_Ocupacional { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }

        public DateTime? d_HoraInicio { get; set; }
        public DateTime? d_Horafin { get; set; }

        public string HoraInicio { get; set; }
        public string Horafin { get; set; }
        public string IdTrabajador { get; set; }
        
    }
}
