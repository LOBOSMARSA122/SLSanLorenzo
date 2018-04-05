using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.BLL
{
    public class ProductBL
    {
        public List<ProductList> GetProductsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.product
                            join B in dbContext.datahierarchy on new { a = A.i_CategoryId.Value, b = 103 } equals new { a = B.i_ItemId, b = B.i_GroupId }
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                            orderby B.v_Value1, A.v_Name ascending
                            select new ProductList
                            {
                                v_ProductId = A.v_ProductId,
                                i_CategoryId = A.i_CategoryId.Value,
                                v_CategoryName = B.v_Value1,
                                v_Name = A.v_Name,
                                v_GenericName = A.v_GenericName,
                                v_BarCode = A.v_BarCode,
                                v_ProductCode = A.v_ProductCode,
                                v_Brand = A.v_Brand,
                                v_Model = A.v_Model,
                                v_SerialNumber = A.v_SerialNumber,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<ProductList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<ProductList> GetLimitedProductsForSearch(ref OperationResult pobjOperationResult, int pintMaxResults, string pstrProductNameOrId)
        {
            //mon.IsActive = true;
            try
            {
                string intId = "-1";
                //bool FindById = int.TryParse(pstrProductNameOrId, out intId);

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.product
                             join B in dbContext.datahierarchy on new { a = A.i_CategoryId.Value, b = 103 } equals new { a = B.i_ItemId, b = B.i_GroupId }
                             where A.v_Name.Contains(pstrProductNameOrId) || A.v_Brand.Contains(pstrProductNameOrId)
                                    || A.v_Model.Contains(pstrProductNameOrId) || A.v_SerialNumber.Contains(pstrProductNameOrId)
                             select new ProductList
                             {
                                 v_ProductId = A.v_ProductId,
                                 v_Name = A.v_Name,
                                 v_CategoryName = B.v_Value1,
                                 v_Brand = A.v_Brand == null ? "" : A.v_Brand,
                                 v_Model = A.v_Model == null ? "" : A.v_Model,
                                 v_SerialNumber = A.v_SerialNumber == null ? "" : A.v_SerialNumber
                             }).Concat
                            (from A in dbContext.product
                             join B in dbContext.datahierarchy on new { a = A.i_CategoryId.Value, b = 103 } equals new { a = B.i_ItemId, b = B.i_GroupId }
                             where A.v_ProductId == intId
                             select new ProductList
                             {
                                 v_ProductId = A.v_ProductId,
                                 v_Name = A.v_Name,
                                 v_CategoryName = B.v_Value1,
                                 v_Brand = A.v_Brand == null ? "" : A.v_Brand,
                                 v_Model = A.v_Model == null ? "" : A.v_Model,
                                 v_SerialNumber = A.v_SerialNumber == null ? "" : A.v_SerialNumber
                             }).OrderBy("v_Name").Take(pintMaxResults);

                var query2 = from A in query.ToList()
                             select new ProductList
                             {
                                 v_ProductId = A.v_ProductId,
                                 v_Name = A.v_Name,
                                 v_CategoryName = A.v_CategoryName,
                                 v_Brand = A.v_Brand,
                                 v_Model = A.v_Model,
                                 v_SerialNumber = A.v_SerialNumber,
                                 v_ConcatenatedData = A.v_Name + " (Id:" + A.v_ProductId.ToString() + ") /Ma: " + A.v_Brand + " /Mod: " + A.v_Model + "/#SN: " + A.v_SerialNumber + "/Cat: " + A.v_CategoryName
                             };

                List<ProductList> objData = query2.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public productDto GetProduct(ref OperationResult pobjOperationResult, string v_ProductId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                productDto objDtoEntity = null;

                var objEntity = (from a in dbContext.product
                                 where a.v_ProductId == v_ProductId 
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = productAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void AddProduct(ref OperationResult pobjOperationResult, productDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                product objEntity = productAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 6), "PI"); ;
                objEntity.v_ProductId = NewId;

                dbContext.AddToproduct(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PRODUCTO", "v_ProductId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PRODUCTO", "v_ProductId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateProduct(ref OperationResult pobjOperationResult, productDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.product
                                       where a.v_ProductId == pobjDtoEntity.v_ProductId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               
                product objEntity = productAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.product.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PRODUCTO", "v_ProductId=" + objEntity.v_ProductId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PRODUCTO", "v_ProductId=" + pobjDtoEntity.v_ProductId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteProduct(ref OperationResult pobjOperationResult, string v_ProductId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.product
                                       where a.v_ProductId == v_ProductId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PRODUCTO", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PRODUCTO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }
    

    
    }
}
