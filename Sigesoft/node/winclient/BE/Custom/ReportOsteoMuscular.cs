using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportOsteoMuscular
    {
        public DateTime? d_BirthDate { get; set; }
        public string IdServicio { get; set; }
        public string IdSericioComponente { get; set; }
        public string Paciente { get; set; }
        public string Puesto { get; set; }
        public int Edad { get; set; }
        public string Protocolo { get; set; }
        public string TipoExamen { get; set; }
        public string Empresa { get; set; }
        public string AntecedentesSiNo { get; set; }
        public string AntecedentesDescripcion { get; set; }
        public string PosturaSentado { get; set; }
        public string PosturaPie { get; set; }
        public string PosturaForzadaSiNo { get; set; }
        public string MovCargaManualSiNo { get; set; }
        public string PesoCarga { get; set; }
        public string MovRepetitivosSiNo { get; set; }
        public string UsuPantallaPVDSiNo { get; set; }
        public string HorasDia { get; set; }
        public string LordisisCervical { get; set; }
        public string CifosisDorsal { get; set; }
        public string LordosisLumbar { get; set; }
        public string EscoliosisLumbar { get; set; }        
        public string EscofiosisDorsal { get; set; }
        public string Observaciones { get; set; }
        public string DolorEspalda { get; set; }
        public string ContracturaMuscular { get; set; }
        public string RodillaDerechaVaroSiNo { get; set; }
        public string RodillaDerechaValgoSiNo { get; set; }
        public string RodillaIzquierdaVaroSiNo { get; set; }
        public string RodillaIzquierdaValgoSiNo { get; set; }
        public string PieDerechoCavoSiNo { get; set; }
        public string PieDerechoPlanoSiNo { get; set; }
        public string PieIzquierdoCavoSiNo { get; set; }
        public string PieIzquierdoPlanoSiNo { get; set; }
        public string ReflejoTotulianoDerechoSiNo { get; set; }
        public string ReflejoTotulianoIzquierdoSiNo { get; set; }
        public string ReflejoAquileoDerechoSiNo { get; set; }
        public string ReflejoAquileoIzquierdoSiNo { get; set; }
        public string TestPhalenDerechoSiNo { get; set; }
        public string TestPhalenIzquierdoSiNo { get; set; }
        public string TestTinelDerechoSiNo { get; set; }
        public string TestTinelIzquierdoSiNo { get; set; }
        public string SignoLasagueIzquierdoSiNo { get; set; }
        public string SignoLasagueDerechoSiNo { get; set; }
        public string SignoBragardIzquierdoSiNo { get; set; }
        public string SignoBragardDerechoSiNo { get; set; }

        public byte[] FirmaTrabajador { get; set; }
        public byte[] FirmaMedico { get; set; }


        public string TemporoMadibularNID { get; set; }
        public string TemporoMadibularObs { get; set; }
        public string HombroNID { get; set; }
        public string HombroObs { get; set; }
        public string CodoNID { get; set; }
        public string CodoObs { get; set; }
        public string MunecaNID { get; set; }
        public string MunecaObs { get; set; }
        public string InterfalangicaNID { get; set; }
        public string InterfalangicaObs { get; set; }
        public string CoxoFermoralNID { get; set; }
        public string CoxoFermoralObs { get; set; }
        public string RodillaNID { get; set; }
        public string RodillaObs { get; set; }
        public string TobilloPieNID { get; set; }
        public string TobilloPieObs { get; set; }

        public string ColumnaCervicalSiNo { get; set; }
        public string ColumnaCervicalObs { get; set; }
        public string ColumnaDorsalSiNo { get; set; }
        public string ColumnaDorsalObs { get; set; }
        public string CostoEsternalesSiNo { get; set; }
        public string CostoEsternalesObs { get; set; }
        public string CondralesSiNo { get; set; }
        public string CondralesObs { get; set; }
        public string DorsoLumbarSiNo { get; set; }
        public string DorsoLumbarObs { get; set; }
        public string LumbroSacraSiNo { get; set; }
        public string LumbroSacraObs { get; set; }


        public string Descripcion { get; set; }
        public string Hallazgos { get; set; }
        public string Recomendacion { get; set; }
        public string Aptitud { get; set; }

        public string MetodoCarga { get; set; }

        public byte[] HuellaTrabajador { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        public string NombreUsuarioGraba { get; set; }

    }
}
