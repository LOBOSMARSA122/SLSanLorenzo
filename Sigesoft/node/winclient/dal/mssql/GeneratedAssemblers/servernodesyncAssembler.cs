//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/12 - 15:07:02
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BE
{

    /// <summary>
    /// Assembler for <see cref="servernodesync"/> and <see cref="servernodesyncDto"/>.
    /// </summary>
    public static partial class servernodesyncAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="servernodesyncDto"/> converted from <see cref="servernodesync"/>.</param>
        static partial void OnDTO(this servernodesync entity, servernodesyncDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="servernodesync"/> converted from <see cref="servernodesyncDto"/>.</param>
        static partial void OnEntity(this servernodesyncDto dto, servernodesync entity);

        /// <summary>
        /// Converts this instance of <see cref="servernodesyncDto"/> to an instance of <see cref="servernodesync"/>.
        /// </summary>
        /// <param name="dto"><see cref="servernodesyncDto"/> to convert.</param>
        public static servernodesync ToEntity(this servernodesyncDto dto)
        {
            if (dto == null) return null;

            var entity = new servernodesync();

            entity.i_NodeId = dto.i_NodeId;
            entity.v_DataSyncVersion = dto.v_DataSyncVersion;
            entity.i_DataSyncFrecuency = dto.i_DataSyncFrecuency;
            entity.i_Enabled = dto.i_Enabled;
            entity.d_LastSuccessfulDataSync = dto.d_LastSuccessfulDataSync;
            entity.i_LastServerProcessStatus = dto.i_LastServerProcessStatus;
            entity.i_LastNodeProcessStatus = dto.i_LastNodeProcessStatus;
            entity.d_NextDataSync = dto.d_NextDataSync;
            entity.v_LastServerPackageFileName = dto.v_LastServerPackageFileName;
            entity.v_LastServerProcessErrorMessage = dto.v_LastServerProcessErrorMessage;
            entity.v_LastNodePackageFileName = dto.v_LastNodePackageFileName;
            entity.v_LastNodeProcessErrorMessage = dto.v_LastNodeProcessErrorMessage;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="servernodesync"/> to an instance of <see cref="servernodesyncDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="servernodesync"/> to convert.</param>
        public static servernodesyncDto ToDTO(this servernodesync entity)
        {
            if (entity == null) return null;

            var dto = new servernodesyncDto();

            dto.i_NodeId = entity.i_NodeId;
            dto.v_DataSyncVersion = entity.v_DataSyncVersion;
            dto.i_DataSyncFrecuency = entity.i_DataSyncFrecuency;
            dto.i_Enabled = entity.i_Enabled;
            dto.d_LastSuccessfulDataSync = entity.d_LastSuccessfulDataSync;
            dto.i_LastServerProcessStatus = entity.i_LastServerProcessStatus;
            dto.i_LastNodeProcessStatus = entity.i_LastNodeProcessStatus;
            dto.d_NextDataSync = entity.d_NextDataSync;
            dto.v_LastServerPackageFileName = entity.v_LastServerPackageFileName;
            dto.v_LastServerProcessErrorMessage = entity.v_LastServerProcessErrorMessage;
            dto.v_LastNodePackageFileName = entity.v_LastNodePackageFileName;
            dto.v_LastNodeProcessErrorMessage = entity.v_LastNodeProcessErrorMessage;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="servernodesyncDto"/> to an instance of <see cref="servernodesync"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<servernodesync> ToEntities(this IEnumerable<servernodesyncDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="servernodesync"/> to an instance of <see cref="servernodesyncDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<servernodesyncDto> ToDTOs(this IEnumerable<servernodesync> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
