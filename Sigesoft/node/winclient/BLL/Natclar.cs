using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class Natclar
    {
        public XmlNatclar DatosXmlNatclar(string serviceId)
        {
            var groupUbigeo = 113;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.calendar
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            // Ubigeo de la persona *******************************************************
                            join dep in dbContext.datahierarchy on new { a = B.i_DepartmentId.Value, b = groupUbigeo }
                                                 equals new { a = dep.i_ItemId, b = dep.i_GroupId } into dep_join
                            from dep in dep_join.DefaultIfEmpty()

                            join prov in dbContext.datahierarchy on new { a = B.i_ProvinceId.Value, b = groupUbigeo }
                                                  equals new { a = prov.i_ItemId, b = prov.i_GroupId } into prov_join
                            from prov in prov_join.DefaultIfEmpty()

                            join distri in dbContext.datahierarchy on new { a = B.i_DistrictId.Value, b = groupUbigeo }
                                                  equals new { a = distri.i_ItemId, b = distri.i_GroupId } into distri_join
                            from distri in distri_join.DefaultIfEmpty()
                            //*********************************************************************************************
                            where A.i_IsDeleted == 0 && A.v_ServiceId == serviceId
                            select new XmlNatclar
                            {
                                Hc = B.v_DocNumber,
                                TipoDocumento = B.i_DocTypeId.Value,
                                Dni = B.v_DocNumber,
                                Sexo = B.i_SexTypeId.Value,
                                PrimerApellido = B.v_FirstName,
                                SegundoApellido = B.v_FirstLastName,
                                Nombre =B.v_SecondLastName,
                                EstadoCivil = B.i_MaritalStatusId.Value,
                                FechaNacimientoSigesoft = B.d_Birthdate.Value,
                                ProvinciaNacimiento = prov.v_Value1,
                                DistritoNacimiento = distri.v_Value1,
                                DepartamentoNacimiento = dep.v_Value1,
                                Email = B.v_Mail,
                                ResidenciaActual =B.v_AdressLocation,
                                Direccion =B.v_AdressLocation
                            };


                var objData = query.FirstOrDefault();

                return objData;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        public Ubigeo DevolverUbigue(string departamento, string provincia, string distrito)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                Ubigeo obj = new Ubigeo();
                var query = (from A in dbContext.datahierarchy
                    where A.i_GroupId == 125
                    select new
                    {
                        departamento = A.v_Value1,
                        provincia =A.v_Value2,
                        distrito = A.v_Field
                    }).ToList();

                var x = query.FindAll(p => p.departamento.ToLower().Contains(departamento.ToLower()) && p.provincia.ToLower().Contains(provincia.ToLower()) && p.distrito.ToLower().Contains(distrito.ToLower())).ToList();

                if (x.Count > 0)
                {
                    var lengthDepartamento = x[0].departamento.Length;
                    var lengthProvincia = x[0].provincia.Length;
                    var lengthDistrito = x[0].distrito.Length;
                    var ubigeoDepartamento = x[0].departamento.Substring(lengthDepartamento - 2, 2);
                    var ubigeoProvincia = x[0].provincia.Substring(lengthProvincia - 2, 2);
                    var ubigeoDistrito = x[0].distrito.Substring(lengthDistrito - 2, 2);

                 
                    obj.depar = ubigeoDepartamento;
                    obj.prov = ubigeoProvincia;
                    obj.distr = ubigeoDistrito;

                    return obj;
                }
                else
                {
                    obj.depar = "";
                    obj.prov = "";
                    obj.distr = "";

                    return obj;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}
