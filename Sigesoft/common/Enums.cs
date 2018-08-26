using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Common
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum SystemUserTypeId
    {
        Internal = 1,
        External = 2
    }

    public enum EmpresaDx
    {
        ConDx = 1,
        SinDx = 2
    }

    public enum TipoProblema
    {
        Cronico = 1,
        Agudo = 2
    }

    public enum CargoHospitalizacion
    {
        MedicoTratante = 1,
        Paciente = 2
    }

    public enum Enfermedad
    {
        Si = 1,
        No = 0
    }


    public enum TypePrinter
    {
        Image = 1,
        Printer = 2
    }

    public enum StatusBlackListPerson
    {
        Registrado = 1,
        Detectado = 2,
        Eliminado = 3
    }

    public enum LogEventType
    {
        ACCESOSSISTEMA = 1,
        CREACION = 2,
        ACTUALIZACION = 3,
        ELIMINACION = 4,
        EXPORTACION = 5,
        GENERACIONREPORTE = 6,
        PROCESO = 7
    }


    public enum Success
    {
        Failed = 0,
        Ok = 1
    }

    public enum TypeForm
    {
        Windows = 1,
        Web = 2
    }

    public enum OperatorValue1
    {
        IGUAL = 1,
        DIFERENTE = 2,
        MENOR = 3,
        MENORIGUAL = 4,
        MAYOR = 5,
        MAYORIGUAL = 6,
        D18_1 = 4
    }

    public enum Document
    {
        DNI = 1,
        PASAPORTE = 2,
        LICENCIACONDUCIR = 3,
        CARNETEXTRANJ = 4
    }

    public enum TypeFamily
    {
        PADRE = 4,
        MADRE = 7,
        HERMANOS = 20,
        ABUELOS = 15,

        PADRE_OK = 1,
        MADRE_OK = 2,
        HERMANOS_OK = 3,
        ABUELOS_OK = 13,
        HIJOS_OK = 66
    }


    public enum ModeOperation
    {
        Total = 1,
        Parcial = 2
    }


    public enum DropDownListAction
    {
        All,
        Select
    }

    public enum Scope
    {
        Global = 1,
        Contextual = 2
    }

    public enum MasterService
    {
        Eso = 2,
        ConsultaInformatica = 4,
        ConsultaMedica = 3,
        ConsultaNutricional = 6,
        ConsultaPsicológica = 7,
        ProcEnfermería = 8,
        AtxMedica = 9,
        AtxMedicaParticular = 10
    }

    public enum MotiveType
    {
        IngresoCompra = 1,
        IngresoCargaInicial = 2,
        EgresoAtencion = 11
    }

    public enum MovementType
    {
        INGRESO = 1,
        EGRESO = 2
    }

    public enum AptitudeStatus
    {
        Apto = 2,
        NoApto = 3,
        AptoObs = 4,
        AptRestriccion = 5,
        SinAptitud = 1
    }
    public enum RecordType
    {
        /// <summary>
        /// El registro tiene un ID [GUID]
        /// </summary>
        Temporal = 1,
        /// <summary>
        /// El registro Tiene in ID de Base de Datos
        /// </summary>
        NoTemporal = 2
    }
    public enum RecordStatus
    {
        Grabado = 0,
        Modificado = 1,
        Agregado = 2,
        EliminadoLogico = 3
    }

    public enum CalendarStatus
    {
        Agendado = 1,
        Atendido = 2,
        Vencido = 3,
        Cancelado = 4,
        Ingreso = 5
    }

    public enum ServiceStatus
    {
        PorIniciar = 1,
        Iniciado = 2,
        Culminado = 3,
        Incompleto = 4,
        Cancelado = 5,
        EsperandoAptitud = 6
    }

    public enum Flag_Call
    {
        NoseLlamo = 0,
        Sellamo = 1
    }

    public enum TipoProfesional
    {
        Evaluador = 30,
        Auditor = 31
    }

    public enum ServiceComponentStatus
    {
        PorIniciar = 1,
        Iniciado = 2,
        Evaluado = 3,
        Auditado = 4,
        NoRealizado = 5,
        PorAprobacion = 6,
        Especialista =7
    }


    public enum ServiceOrderStatus
    {
        Iniciado = 1,
        Concluido = 2,
        Observado = 3,
        Conforme = 4,
    }

    public enum LineStatus
    {
        EnCircuito = 1,
        FueraCircuito = 2,
    }


    public enum QueueStatusId
    {
        LIBRE = 1,
        LLAMANDO = 2,
        OCUPADO = 3
    }

    public enum TypeHabit
    {
        Tabaco = 1,
        Alcohol = 2,
        Drogas = 3,
        ActividadFisica = 4
    }

    public enum modality
    {
        NuevoServicio = 1,
        ContinuacionServicio = 2,
    }

    public enum SiNo
    {
        NO = 0,
        SI = 1,
        NONE = 2
    }

    public enum TypeOfInsurance
    {
        ESSALUD = 1,
        EPS = 2,
        OTRO = 3
    }

    public enum ProcessType
    {
        LOCAL = 1,
        REMOTO = 2
    }


    public enum Gender
    {
        MASCULINO = 1,
        FEMENINO = 2
    }


    public enum GenderConditional
    {
        MASCULINO = 1,
        FEMENINO = 2,
        AMBOS = 3
    }



    public enum ControlType 
    {
        /// <summary>
        /// select * from systemparameter where i_GroupId = 112
        /// </summary>
        CadenaTextual = 1,
        CadenaMultilinea = 2,
        NumeroEntero = 3,
        NumeroDecimal = 4,
        SiNoCheck = 5,
        SiNoRadioButton = 6,
        SiNoCombo = 7,
        UcFileUpload = 8,
        Lista = 9,
        UcAudiometria = 10,
        UcOdontograma = 11,
        Fecha = 12,
        UcSomnolencia =13,
        UcAcumetria = 14,
        UcSintomaticoRespi= 15,
        UcRxLumboSacra = 16,
        UcOtoscopia =17,
        UcEvaluacionErgonomica =18,
        UcOjoSeco=19,
        UcOsteoMuscular = 20,
        UcBoton = 21,
        UcFototipo = 22,
        UcHistorialTriaje = 23,
        UcHistorialGrupoSanguineo = 24,
        Radiobutton = 30
    }

    public enum ComponentType
    {
        Examen = 1,
        NoExamen = 2
    }

    public enum ServiceType
    {
        Empresarial = 1,
        Particular = 9,
        Preventivo = 11
    }

    public enum AutoManual
    {
        Automático = 1,
        Manual = 2
    }

    public enum TypeESO
    {
        PreOcupacional = 1,
        PeriodicoAnual = 2,
        Retiro = 3,
        Preventivo = 4,
        Reubicacion = 5,
        Chequeo = 6
    }

    public enum PreQualification
    {
        SinPreCalificar = 1,
        Aceptado = 2,
        Rechazado = 3

    }

    public enum FinalQualification
    {
        SinCalificar = 1,
        Definitivo = 2,
        Presuntivo = 3,
        Descartado = 4

    }

    public enum LugarTrabajo
    {
        Superfice = 1,
        Concentradora = 2,
        Subsuelo = 3

    }

    public enum Operator2Values
    {
        X_esIgualque_A = 1,
        X_noesIgualque_A = 2,
        X_esMenorque_A = 3,
        X_esMenorIgualque_A = 4,
        X_esMayorque_A = 5,
        X_esMayorIgualque_A = 6,
        X_esMayorque_A_yMenorque_B = 7,
        X_esMayorque_A_yMenorIgualque_B = 8,
        X_esMayorIgualque_A_yMenorque_B = 9,
        X_esMayorIgualque_A_yMenorIgualque_B = 12,
    }

    public enum GrupoEtario
    {
        Ninio = 4,
        Adolecente = 2,
        Adulto =1,
        AdultoMayor=3
    }

    public enum ComponenteProcedencia
    {
        Interno = 1,
        Externo = 2
    }

    public enum Typifying
    {
        Recomendaciones = 1,
        Restricciones = 2
    }

    public enum ExternalUserFunctionalityType
    {
        PermisosOpcionesUsuarioExternoWeb = 1,
        NotificacionesUsuarioExternoWeb = 2
    }

    public enum FileExtension
    {
        JPG,
        GIF,
        JPEG,
        PNG,
        BMP,
        XLS,
        XLSX,
        DOC,
        DOCX,
        PDF,
        PPT,
        PPTX,
        TXT,
        AVI,
        MPG,
        MPEG,
        MOV,
        WMV,
        FLV,
        MP3,
        MP4,
        WMA,
        WAV
    }

    /// <summary>
    ///  Especifica una acción.
    /// </summary>
    public enum ActionForm
    {
        None = 0,
        Add = 1,
        Edit = 2,
        Delete = 3,
        Upload = 4,
        Browse = 5,
        Cancel = 6
    }


    public enum NormalAlterado
    {
        Normal = 1,
        Alterado = 2,
        NoSeRealizo = 3
    }

    public enum Altitud
    {
        Debajo2500 = 1,
        Entre2501a3000 = 2,
        Entre3001a3500 = 3,
        Entre3501a4000 = 4,
        Entre4001a4500 = 5,
        Mas4501 = 6
    }

    public enum EstadoCivil
    {
        Soltero = 1,
        Casado = 2,
        Viudo = 3,
        Divorciado = 4,
        Conviviente = 5
    }
    public enum Quien
    {
        P = 1,
        M = 2,
        H = 3,
        A = 4,
        O = 5
    }
    public enum NivelEducacion
    {
        Analfabeto = 1,
        PIncompleta = 2,
        PCompleta = 3,
        SIncompleta = 4,
        SCompleta = 5,
        Tecnico = 6,
        Universitario = 7
    }

    public enum TipoOcurrencia
    {
        Accidente = 1,
        Enfermedad = 2,
        NoDefine = 3
    }

    public enum TipoDx
    {
        Enfermedad_Comun = 1,
        Enfermedad_Ocupacional = 2,
        Accidente_Común = 3,
        Accidente_Ocupacional = 4,
        Otros = 5,
        Normal = 6,
        SinDx = 7
    }


    public enum OrigenOcurrencia
    {
        Comun = 1,
        Laboral = 2,
        NoDefine = 3
    }

    public enum NormalAlteradoHallazgo
    {
        SinHallazgos = 1,
        ConHallazgos = 2,
        NoseRealizo = 3
    }



    public enum AppHierarchyType
    {
        AgrupadorDeMenu = 1,
        PantallaOpcionDeMenu = 2,
        AccionDePantalla = 3,
        PantallaOpcionIndependiente = 4
    }

    public enum CategoryTypeExam
    {
        Laboratorio = 1,
        odontologia = 2,
        Neonatal = 3,
        Ginecologia = 4,
        Cardiologia = 5,
        Rx = 6,
        Psicologia = 7,
        Triaje = 10,
        ExamenFisico = 11,
        Inmunizaciones = 113,
        Oftalmología = 14,

        Audiometria = 15,
        Espiromertia = 16,
        RXLumbar = 18,

        RiesgoActividadCritica = 19,
        Labclinica = 20,
        Clinica = 21,
    }

    public enum PeligrosEnElPuesto
    {
        Fisicos = 1,
        Quimicos = 2,
        Ergonomicos = 3,
        Biologicos = 4,
        Ruido = 5,

    }

    public enum TipoEPP
    {
        TaponesAuditivosEspuma = 1,
        TaponesAuditivosSilicona = 2,
        Orejeras = 3,

    }

    public enum Consultorio
    {
        Laboratorio = 1,
        Odontologia = 2,
        Neonatal = 3,
        Ginecología = 4,
        Cardiología = 5,
        RayosX = 6,
        Psicología = 7,
        Triaje = 10,
        Medicina = 11,
        GinecologíaExAuxiliares = 12,
        Inmunizaciones = 13,

    }
    //
    //

    #region Estados de conexion a Internet

    [Flags]
    public enum ConnectionState : int
    {
        INTERNET_CONNECTION_MODEM = 0x1,
        INTERNET_CONNECTION_LAN = 0x2,
        INTERNET_CONNECTION_PROXY = 0x4,
        INTERNET_RAS_INSTALLED = 0x10,
        INTERNET_CONNECTION_OFFLINE = 0x20,
        INTERNET_CONNECTION_CONFIGURED = 0x40
    }

    public enum ColorDiente
    {
        White = 1,
        Red = 2,
        Blue = 3
    }

    public enum LeyendaOdontograma
    {
        Ausente = 1,
        Exodoncia = 2,
        Obturacion = 3,
        Corona = 4,
        Puente = 5,
        PPR = 6,
        ProtesisTotal = 7,
        Implante = 8,
        AparatoOrtodontico = 9,
        RemanenteRedicular = 10,
        CoronaTemporal = 11,
        CoronaDefinitiva = 12,
        ProtesisFijaBueno = 13,
        ProtesisFijaMalo = 14,
        ProtesisRemovible = 15
    }

    public enum ResultKlockloff
    {
        MIXTA = 1,
        NS = 2,
        COND = 3,
        ND = 4,
        NORM = 5
    }

    public enum PreLiquidationStatus
    {
        Pendiente = 1,
        Generada = 2

    }

    public enum ModoCargaImagen
    {
        DesdeArchivo = 1,
        DesdeDispositivo = 2

    }

    #endregion

    public enum VisionLejos
    {
        _20_20 = 1,
        _20_25 = 2,
        _20_30 = 3,
        _20_40 = 4,
        _20_50 = 5,
        _20_60 = 6,
        _20_80 = 7,
        _20_100 = 8,
        _20_140 = 9,
        _20_200 = 10,
        _20_400 = 11,
        _CD3M = 12,
        _CD1M = 13,
        _CD03M = 14,
        _MM = 15,
        _PL = 16,
        _NPL = 17,
        _raya = 18,
    }
    public enum VisionCerca
    {
        _20_20 = 1,
        _20_30 = 2,
        _20_40 = 3,
        _20_50 = 4,
        _20_60 = 5,
        _20_70 = 6,
        _20_80 = 7,
        _20_100 = 8,
        _20_160 = 9,
        _20_200 = 10,
    }

    public enum SystemParameterGroups
    {
        ConHallazgoSinHallazgosNoSeRealizo = 135,
        AlturaLabor = 176,
        TiempoExpsosicionRuidoSalus = 234,
        TiempoExpsosicionRuido = 235,
        NivelRuido = 236,
        OftalmologiaMedidas = 237,
        OftalmologiaMedidasCerca = 262,
        NormalAlterado = 238,
        ColoresBasicos = 239,
        EscalaDolor = 240,
        SiNoNoDefine = 241,
        HabitosNocivos = 148,
        GrupoOcupacional_EMPO_PSICOLOGIA = 242,
        GrupoOcupacional_EMOA_PSICOLOGIA = 243,
        GrupoOcupacional_EMOR_PSICOLOGIA = 244,
        DISC_Combinaciones_PSICOLOGIA = 245,
        ResultadoEvaluacion_EstabilidadEmocional = 255,
        ResultadoEvaluacion_NivelEstres = 256,
        ResultadoEvaluacion_Personalidad = 257,
        ResultadoEvaluacion_Afectividad = 258,
        ResultadoEvaluacion_Motivacion = 259,
        ResultadoEvaluacion_NIVEL_EMPO = 260,
    }
}

