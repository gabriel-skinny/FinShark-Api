using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository: ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async  Task<Comment?> GetByIdAsync(int id)
        {
            
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment> CreateAsync (Comment commentModel) {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<bool> CommentExists(int id) {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsnyc(int commentId, UpdateCommentDto commentUpdateDto) {
            var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (commentToUpdate == null) return null;

            commentToUpdate.Content = commentUpdateDto.Content;
            commentToUpdate.Title = commentUpdateDto.Title;

            await _context.SaveChangesAsync();

            return commentToUpdate;
        }

        public async Task<Comment?> Delete(int commentId) {
            var commentToDelete = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (commentToDelete == null) return null;

            _context.Comments.Remove(commentToDelete);
            await _context.SaveChangesAsync();

            return commentToDelete;
        }
    }
}