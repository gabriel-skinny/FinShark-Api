using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Model;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel) {
            return new CommentDto {
                Id = commentModel.Id,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                Title = commentModel.Title,
                StockId = commentModel.StockId
            };
        }

         public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId) {
            return new Comment {
                Content = commentDto.Content,
                Title = commentDto.Title,
                StockId = stockId
            };
        }
    }
}