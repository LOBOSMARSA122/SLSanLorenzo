using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MatrizLaZanja
    {
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string ProtocolId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }	

        public string ApellidosNombres { get; set; }
        public string Procedencia { get; set; }
        public string Sexo { get; set; }
        public string Empresa { get; set; }
        public string Dni { get; set; }
        public string PuestoTrabajo { get; set; }
        public int Edad { get; set; }
        public string TipoExamen { get; set; }
        public DateTime? FechaDigitacion { get; set; }
        public DateTime? FechaExamenOcupacional { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Peso { get; set; }
        public string Talla { get; set; }
        public string Imc { get; set; }
        public string DxAntropometria { get; set; }
        public string Fc { get; set; }

        public string Fr { get; set; }
        public string SatO2 { get; set; }
        public string Pas { get; set; }
        public string Pad { get; set; }
        public string DxPa { get; set; }
        public string GsFactor { get; set; }
        public string Hto { get; set; }
        public string Hb { get; set; }
        public string DxHb { get; set; }
        public string Orina { get; set; }

        public string Rpr { get; set; }
        public string Cocaina { get; set; }
        public string Marihuana { get; set; }
        public string Calidad { get; set; }
        public string Oit { get; set; }
        public string DxRx { get; set; }
        public string Audiometria { get; set; }
        public string CalidadEspirometria { get; set; }
        public string DxExpirometria { get; set; }
        public string VcScOd { get; set; }

        public string VcScOi { get; set; }
        public string VcCcOd { get; set; }
        public string VcCcOi { get; set; }
        public string VlScOd { get; set; }
        public string VlScOi { get; set; }
        public string VlCcOi { get; set; }
        public string VlCcOd { get; set; }
        public string DxAgudeza { get; set; }
        public string VisionCromatica { get; set; }
        public string Ekg { get; set; }

        public string OtrosDx { get; set; }
        public string Interconsultas { get; set; }
        public string Condicion { get; set; }
        public string Restriccion1 { get; set; }
        public string Restriccion2 { get; set; }
        public string Restriccion3 { get; set; }
        public string Restriccion4 { get; set; }
        public string Restriccion5 { get; set; }
        public string Restriccion6 { get; set; }
        public string Observacion1 { get; set; }

        public string Observacion2 { get; set; }
        public string Observacion3 { get; set; }
        public string FechaLevantamientoObs { get; set; }
        public string ValidacionSi { get; set; }
        public string ValidacionNo { get; set; }
        public string LugarExamen { get; set; }
        public string MEdicoRevisaExamen { get; set; }
        public string FechaAptitud { get; set; }
        public string LicenciadaRegistra { get; set; }
    }
}
