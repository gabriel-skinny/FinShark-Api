using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{   
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(
            ICommentRepository commentRepository,
            IStockRepository stockRepository
        )
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var comments = await _commentRepository.GetAllAsync();
        
            var commentDto = comments.Select(x => x.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDto());
        } 

        [HttpPost]
        [Route("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto) {
            if (!await _stockRepository.StockExists(stockId)) return BadRequest("Stock doest not exists");

            var commentModel = commentDto.ToCommentFromCreate(stockId);

            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{commentId}")]
        public async Task<ActionResult> Update([FromRoute] int commentId, UpdateCommentDto updateDto) {
            if (!await _commentRepository.CommentExists(commentId)) return BadRequest("Could not update comment that doest not exists");

            var commentUpdated = await _commentRepository.UpdateAsnyc(commentId, updateDto);

            if (commentUpdated == null) return NotFound();

            return Ok(commentUpdated.ToCommentDto());
        }

        [HttpDelete]
        [Route("{commentId}")]
        public async Task<ActionResult> DeleteById([FromRoute] int commentId) {
            if (!await _commentRepository.CommentExists(commentId)) return BadRequest("Could not update comment that doest not exists");

            var deletedComment = await _commentRepository.Delete(commentId);
    
            return Ok(deletedComment);
        }
    }
}