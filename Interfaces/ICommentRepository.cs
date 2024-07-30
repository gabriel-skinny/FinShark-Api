using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Model;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);

        Task<Comment> CreateAsync(Comment commentModel);

        Task<bool> CommentExists(int id);

        Task<Comment?> UpdateAsnyc(int commentId, UpdateCommentDto commentUpdateDto);
        Task<Comment?> Delete (int id);
    }
}