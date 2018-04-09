using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
  public  class OstioCoimolache
    {
        //Datos Generales del Servicio
        public string ServiceId{ get; set; }
        public string ServiceComponentId { get; set; }
        public DateTime? FechaServicio{ get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad{ get; set; }
        public int TipoDocumentoId { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string EmpresaCliente { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string EmpresaEmpleadora { get; set; }
        public string Puesto { get; set; }
        public int GeneroId{ get; set; }
        public string Genero { get; set; }
        public byte[] FirmaTrabajador{ get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] FirmaUsuarioGraba { get; set; }
        
      //Datos del Examen Componente
        public string Escoliosis{ get; set; }
        public string Cifosis{ get; set; }
        public string Lordosis{ get; set; }
        public string EscoliosisComen{ get; set; }
        public string CifosisComen{ get; set; }
        public string LordosisComen{ get; set; }
        public string FlexionAdelante{ get; set; }
        public string Hiperextesion{ get; set; }
        public string FlexionIzquierdo{ get; set; }
        public string FlexionDerecho{ get; set; }

        public string RotacionIzquierdo{ get; set; }
        public string RotacionDerecho{ get; set; } 
        public string FuerzaLevantase{ get; set; }
        public string FlexionAdelanteComen{ get; set; }
        public string HiperextesionComen{ get; set; }
        public string FlexionIzquierdoComen{ get; set; }
        public string FlexionDerechoComen{ get; set; }
        public string RotacionIzquierdoComen{ get; set; }
        public string RotacionDerechoComen{ get; set; } 
        public string FuerzaLevantaseComen{ get; set; }

        public string Corriendo{ get; set; }  
        public string Caminando{ get; set; }
        public string PonerseCuclilla{ get; set; }
        public string CorriendoComen{ get; set; }  
        public string CaminandoComen{ get; set; }
        public string PonerseCuclillaComen{ get; set; }
        public string CodoIzquierdo{ get; set; }
        public string CodoDerecho{ get; set; }
        public string RodillaIzquierdo{ get; set; }
        public string RodillaDerecho{ get; set; }

        public string Abdomen{ get; set; }
        public string Cadera{ get; set; }
        public string Muslo{ get; set; }
        public string Lateral{ get; set; }
        public string AbdomenComen{ get; set; }
        public string CaderaComen{ get; set; }
        public string MusloComen{ get; set; }
        public string LateralComen{ get; set; }

    }
}
