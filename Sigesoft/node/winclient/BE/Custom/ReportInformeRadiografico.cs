using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportInformeRadiografico
    {
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public string v_ServiceId { get; set; }
        public string v_ServiceComponentId { get; set; }
        public string MedicoEvaluador { get; set; }
        public string FechaLectura { get; set; }
        public string FechaRadiografia { get; set; }
          public string Placa{get;set;}
                        public string Hcl{get;set;} 
                        public string Lector{get;set;} 
                        public string Nombre{get;set;} 
                        public int Edad{get;set;} 
                        public int FLDia{get;set;} 
                        public int FLMes{get;set;} 
                        public int FLAnio{get;set;} 
                        public int FRDia{get;set;} 
                        public int FRMes{get;set;} 
                        public int FRAnio{get;set;} 
                        public string CalidaRadio{get;set;} 
                        public string Causas{get;set;} 
                        public string SuperiorDerecho{get;set;}
                        public string SuperiorIzquierda { get; set; } 
                        public string MedioDerecho{get;set;}
                        public string MedioIzquierda { get; set; } 
                        public string InferiorDerecho{get;set;}
                        public string InferiorIzquierdo { get; set; }


                        public string CeroNada { get; set; }
                        public string CeroCero { get; set; }
                        public string CeroUno { get; set; } 

                        public string UnoCero{get;set;} 
                        public string UnoUno{get;set;} 
                        public string UnoDos{get;set;}

                        public string DosUno { get; set; }
                        public string DosDos { get; set; }
                        public string DosTres { get; set; } 
                                        
                        public string TresDos{get;set;} 
                        public string TresTres{get;set;} 
                        public string TresMas{get;set;} 


                        public string p{get;set;} 
                        public string q{get;set;} 
                        public string r{get;set;} 
                        public string s{get;set;} 
                        public string t{get;set;} 
                        public string u{get;set;} 
                        public string p1{get;set;} 
                        public string q1{get;set;} 
                        public string r1{get;set;} 
                        public string s1{get;set;} 
                        public string t1{get;set;} 
                        public string u1{get;set;} 
                        public string O{get;set;} 
                        public string A{get;set;} 
                        public string B{get;set;} 
                        public string C{get;set;} 
                        public string SimboloSi{get;set;}
                        public string SimboloNo { get; set; } 
                        public string aa{get;set;} 
                        public string at{get;set;} 
                        public string ax{get;set;} 
                        public string bu{get;set;} 
                        public string ca{get;set;} 
                        public string cg{get;set;} 
                        public string cn{get;set;} 
                        public string co{get;set;} 
                        public string cp{get;set;} 
                        public string cv{get;set;}

                        public string di { get; set; }
                        public string ef { get; set; }
                        public string em { get; set; }
                        public string es { get; set; }
                        public string fr { get; set; }
                        public string hi { get; set; }
                        public string ho { get; set; }
                        public string id { get; set; }
                        public string ih { get; set; }
                        public string kl { get; set; }
                        public string me { get; set; }
                        public string od { get; set; }
                        public string pa { get; set; }
                        public string pb { get; set; }
                        public string pi { get; set; }
                        public string px { get; set; }
                        public string ra { get; set; }
                        public string rp { get; set; }
                        public string tb { get; set; }
                        public string Comentario_Od { get; set; } 




                        public string Comentario{get;set;} 
                        public string Conclusiones{get;set;}
                        public byte[] FirmaMedico { get; set; }
                        public byte[] FirmaTecnologo { get; set; }
                        public string Dx { get; set; }



                        public byte[] b_Logo { get; set; }
                        public string EmpresaPropietaria { get; set; }
                        public string EmpresaPropietariaDireccion { get; set; }
                        public string EmpresaPropietariaTelefono { get; set; }
                        public string EmpresaPropietariaEmail { get; set; }

                        public string DNI { get; set; }
                        public string NombreUsuarioGraba { get; set; }


                        public string LOCALIZACION_PERFIL { get; set; }
                        public string LOCALIZACION_FRENTE { get; set; }
                        public string CLACIFICACION_PERFIL { get; set; }
                        public string CLACIFICACION_FRENTE { get; set; }
                        public string EXTENSION_DER_ENGROSAMIENTO { get; set; }
                        public string EXTENSION_IZQ_ENGROSAMIENTO { get; set; }
                        public string ANCHURA_DER_ENGROSAMIENTO { get; set; }
                        public string ANCHURA_IZQ_ENGROSAMIENTO { get; set; }
                        public string DE_PERFIL { get; set; }
                        public string DE_FRENTE { get; set; }
                        public string DIAFRAGMA { get; set; }
                        public string OTROS_SITIOS { get; set; }
                        public string DE_PERFIL_CLASF { get; set; }
                        public string DE_FRENTE_CLASF { get; set; }
                        public string DIAFRAGMA_CLASF { get; set; }
                        public string OTROS_SITIOS_CLASF { get; set; }
                        public string EXTENSION_DER_PLACA { get; set; }
                        public string EXTENSION_IZQ_PLACA { get; set; }
                        public string ANCHURA_DER_PLACA { get; set; }
                        public string ANCHURA_IZQ_PLACA { get; set; }
                        public string OBLITANG_DER_PLACA { get; set; }
                        public string OBLITANG_IZQ_PLACA { get; set; }
                        public string  CAMPOS_PULMONARES  { get; set; }
                        public string  HILOS { get; set; }
                        public string  MEDIASTINOS { get; set; }
                        public string  SENOS { get; set; }
                        public string SILUETA_CARDIOVASCULAR { get; set; }
                        public string VERTICES { get; set; }
                        public string ARTEFACTOS { get; set; }
                        public string BAJA_INSPRACION { get; set; }
                        public string ESCAPULAS { get; set; }
                        public string NINGUNA { get; set; }
                        public string OTROS { get; set; }
                        public string POSICION_CENTRADO { get; set; }
                        public string SOBRE_EXPOSICION { get; set; }
                        public string SUB_EXPOSICION { get; set; }


    }
}
