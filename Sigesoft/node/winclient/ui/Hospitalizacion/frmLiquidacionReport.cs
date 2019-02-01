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
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Sigesoft.Node.Contasol.Integration;
using NetPdf;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmLiquidacionReport : Form
    {
        private ServiceBL _serviceBL = new ServiceBL();
        private PacientBL _pacientBL = new PacientBL();
        private OperationResult _objOperationResult = new OperationResult();
        private HospitalizacionBL _hospitBL = new HospitalizacionBL();
        string hospiId = null;
        private OperationResult objOperationResult = new OperationResult();
        private hospitalizacionserviceDto hospser = new hospitalizacionserviceDto();
        public frmLiquidacionReport(string _hospiId)
        {
            hospiId = _hospiId;
            InitializeComponent();
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                var lista = _hospitBL.GetHospitalizcion(ref _objOperationResult, hospiId);

                //var serviceId = lista.SelectMany(p => p.Servicios.Select(q=>q.v_ServiceId));
                int doctor = 1;
                hospser = _hospitBL.GetHospitServ(hospiId);

                var _DataService = _serviceBL.GetServiceReport(hospser.v_ServiceId);
                var datosP = _pacientBL.DevolverDatosPaciente(hospser.v_ServiceId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();
                ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, hospser.v_ServiceId);

                var hospitalizacion = _hospitBL.GetHospitalizacion(ref objOperationResult, hospiId);
                var hospitalizacionhabitacion = _hospitBL.GetHospitalizacionHabitacion(ref objOperationResult, hospiId);
                var medicoTratante = new ServiceBL().GetMedicoTratante(hospser.v_ServiceId);
                string nombre = personData.v_DocNumber + "_" + personData.v_ProtocolName + "-LiquMédico";

                #region MANDAR PRECIO A BASE
                decimal totalFinal = 0;
                foreach (var hospitalizacion_precios in lista)
                {
                    var ListaServicios = hospitalizacion_precios.Servicios.FindAll(p => p.v_ServiceId != null);
                    decimal totalParcialMedicina = 0;
                    decimal sumaMedicina = 0;
                    decimal sumaServicio = 0;
                    foreach (var servicios in ListaServicios)
                    {
                        if (servicios.Tickets != null)
                        {
                            var ListaTickets = servicios.Tickets.FindAll(p => p.i_conCargoA == 1);
                            if (ListaTickets.Count() >= 1)
                            {
                                foreach (var tickets in ListaTickets)
                                {
                                    var detalletickets = tickets.Productos.FindAll(p => p.d_Cantidad != 0);
                                    foreach (var Detalle in detalletickets)
                                    {
                                        int cantidad = (int)Detalle.d_Cantidad;
                                        totalParcialMedicina = (decimal)(Detalle.d_PrecioVenta * cantidad);
                                        sumaMedicina += (decimal)totalParcialMedicina;
                                    }
                                }
                            }
                            else
                            {
                                totalParcialMedicina = decimal.Round(totalParcialMedicina, 2);
                            }
                        }

                        var ListaComponentes = servicios.Componentes.FindAll(p => p.Precio != 0 && p.i_conCargoA == 1);
                        foreach (var compo in ListaComponentes)
                        {
                            decimal compoPrecio = (decimal)compo.Precio;
                            sumaServicio += compoPrecio;
                        }
                    }

                    decimal totalParcialHabitacion = 0;
                    decimal sumaHabitacion = 0;
                    var ListaHabitaciones = hospitalizacion_precios.Habitaciones.FindAll(p => p.i_conCargoA == 1);
                    
                    foreach (var habitacion in ListaHabitaciones)
                    {
                        DateTime inicio = habitacion.d_StartDate.Value;
                        DateTime fin;

                        if (habitacion.d_EndDate != null || habitacion.d_EndDate.ToString() == "00/00/0000 0:0:0")
                        {
                            fin = habitacion.d_EndDate.Value;
                        }
                        else
                        {
                            fin = DateTime.Now;

                        }

                        TimeSpan nDias = fin - inicio;

                        int tSpan = nDias.Days;

                        int dias = 0;
                        if (tSpan == 0)
                        {
                            dias = tSpan + 1;
                        }
                        else
                        {
                            dias = tSpan;
                        }

                        decimal _habitacionPrecio = (decimal)habitacion.d_Precio;
                        _habitacionPrecio = decimal.Round(_habitacionPrecio, 2);

                        totalParcialHabitacion = (decimal)(habitacion.d_Precio * dias);
                        totalParcialHabitacion = decimal.Round(totalParcialHabitacion, 2);

                        sumaHabitacion += (decimal)totalParcialHabitacion;
                    }

                    totalFinal = sumaMedicina + sumaServicio + sumaHabitacion;

                    totalFinal = decimal.Round(totalFinal, 2);
                }


                var _Hospitalizacion = new HospitalizacionBL().GetHospitalizacion(ref objOperationResult, hospiId);

                _Hospitalizacion.d_PagoMedico = totalFinal;

                _hospitBL.UpdateHospitalizacion(ref objOperationResult, _Hospitalizacion, Globals.ClientSession.GetAsList());
                #endregion

                Liquidacion_Hospitalizacion.CreateLiquidacion(ruta + nombre + ".pdf", MedicalCenter, lista, _DataService, datosP, doctor, hospitalizacion, hospitalizacionhabitacion, medicoTratante);

                this.Enabled = true;
            }
            this.Close();
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                var lista = _hospitBL.GetHospitalizcion(ref _objOperationResult, hospiId);

                //var serviceId = lista.SelectMany(p => p.Servicios.Select(q=>q.v_ServiceId));
                int paciente = 2;
                hospser = _hospitBL.GetHospitServ(hospiId);

                var _DataService = _serviceBL.GetServiceReport(hospser.v_ServiceId);
                var datosP = _pacientBL.DevolverDatosPaciente(hospser.v_ServiceId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, hospser.v_ServiceId);

                var hospitalizacion = _hospitBL.GetHospitalizacion(ref objOperationResult, hospiId);
                var hospitalizacionhabitacion = _hospitBL.GetHospitalizacionHabitacion(ref objOperationResult, hospiId);
                var medicoTratante = new ServiceBL().GetMedicoTratante(hospser.v_ServiceId);
                string nombre = personData.v_DocNumber + "_" + personData.v_ProtocolName + "-LiquPac";

                #region MANDAR PRECIO A BASE
                decimal totalFinal = 0;
                foreach (var hospitalizacion_precios in lista)
                {
                    var ListaServicios = hospitalizacion_precios.Servicios.FindAll(p => p.v_ServiceId != null);
                    decimal totalParcialMedicina = 0;
                    decimal sumaMedicina = 0;
                    decimal sumaServicio = 0;
                    foreach (var servicios in ListaServicios)
                    {
                        if (servicios.Tickets != null)
                        {
                            var ListaTickets = servicios.Tickets.FindAll(p => p.i_conCargoA == 2);
                            if (ListaTickets.Count() >= 1)
                            {
                                foreach (var tickets in ListaTickets)
                                {
                                    var detalletickets = tickets.Productos.FindAll(p => p.d_Cantidad != 0);
                                    foreach (var Detalle in detalletickets)
                                    {
                                        int cantidad = (int)Detalle.d_Cantidad;
                                        totalParcialMedicina = (decimal)(Detalle.d_PrecioVenta * cantidad);
                                        sumaMedicina += (decimal)totalParcialMedicina;
                                    }
                                }
                            }
                            else
                            {
                                totalParcialMedicina = decimal.Round(totalParcialMedicina, 2);
                            }
                        }

                        var ListaComponentes = servicios.Componentes.FindAll(p => p.Precio != 0 && p.i_conCargoA == 2);
                        foreach (var compo in ListaComponentes)
                        {
                            decimal compoPrecio = (decimal)compo.Precio;
                            sumaServicio += compoPrecio;
                        }
                    }

                    decimal totalParcialHabitacion = 0;
                    decimal sumaHabitacion = 0;
                    var ListaHabitaciones = hospitalizacion_precios.Habitaciones.FindAll(p => p.i_conCargoA == 2);

                    foreach (var habitacion in ListaHabitaciones)
                    {
                        DateTime inicio = habitacion.d_StartDate.Value;
                        DateTime fin;

                        if (habitacion.d_EndDate != null || habitacion.d_EndDate.ToString() == "00/00/0000 0:0:0")
                        {
                            fin = habitacion.d_EndDate.Value;
                        }
                        else
                        {
                            fin = DateTime.Now;

                        }

                        int tSpan = fin.Day - inicio.Day;

                        int dias = 0;
                        if (tSpan == 0)
                        {
                            dias = tSpan + 1;
                        }
                        else
                        {
                            dias = tSpan;
                        }

                        decimal _habitacionPrecio = (decimal)habitacion.d_Precio;
                        _habitacionPrecio = decimal.Round(_habitacionPrecio, 2);

                        totalParcialHabitacion = (decimal)(habitacion.d_Precio * dias);
                        totalParcialHabitacion = decimal.Round(totalParcialHabitacion, 2);

                        sumaHabitacion += (decimal)totalParcialHabitacion;
                    }

                    totalFinal = sumaMedicina + sumaServicio + sumaHabitacion;

                    totalFinal = decimal.Round(totalFinal, 2);
                }


                var _Hospitalizacion = new HospitalizacionBL().GetHospitalizacion(ref objOperationResult, hospiId);

                _Hospitalizacion.d_PagoPaciente = totalFinal;

                _hospitBL.UpdateHospitalizacion(ref objOperationResult, _Hospitalizacion, Globals.ClientSession.GetAsList());
                #endregion
                
                
                
                
                Liquidacion_Hospitalizacion.CreateLiquidacion(ruta + nombre + ".pdf", MedicalCenter, lista, _DataService, datosP, paciente, hospitalizacion, hospitalizacionhabitacion, medicoTratante);
                this.Enabled = true;
            }
            this.Close();
        }
    }
}
