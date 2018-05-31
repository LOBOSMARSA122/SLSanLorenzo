//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/05/28 - 11:58:43
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
    /// Assembler for <see cref="multimediafile"/> and <see cref="multimediafileDto"/>.
    /// </summary>
    public static partial class multimediafileAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="multimediafileDto"/> converted from <see cref="multimediafile"/>.</param>
        static partial void OnDTO(this multimediafile entity, multimediafileDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="multimediafile"/> converted from <see cref="multimediafileDto"/>.</param>
        static partial void OnEntity(this multimediafileDto dto, multimediafile entity);

        /// <summary>
        /// Converts this instance of <see cref="multimediafileDto"/> to an instance of <see cref="multimediafile"/>.
        /// </summary>
        /// <param name="dto"><see cref="multimediafileDto"/> to convert.</param>
        public static multimediafile ToEntity(this multimediafileDto dto)
        {
            if (dto == null) return null;

            var entity = new multimediafile();

            entity.v_MultimediaFileId = dto.v_MultimediaFileId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_FileName = dto.v_FileName;
            entity.b_File = dto.b_File;
            entity.b_ThumbnailFile = dto.b_ThumbnailFile;
            entity.v_Comment = dto.v_Comment;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="multimediafile"/> to an instance of <see cref="multimediafileDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="multimediafile"/> to convert.</param>
        public static multimediafileDto ToDTO(this multimediafile entity)
        {
            if (entity == null) return null;

            var dto = new multimediafileDto();

            dto.v_MultimediaFileId = entity.v_MultimediaFileId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_FileName = entity.v_FileName;
            dto.b_File = entity.b_File;
            dto.b_ThumbnailFile = entity.b_ThumbnailFile;
            dto.v_Comment = entity.v_Comment;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="multimediafileDto"/> to an instance of <see cref="multimediafile"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<multimediafile> ToEntities(this IEnumerable<multimediafileDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="multimediafile"/> to an instance of <see cref="multimediafileDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<multimediafileDto> ToDTOs(this IEnumerable<multimediafile> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
