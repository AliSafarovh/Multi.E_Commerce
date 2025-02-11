using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [AllowAnonymous]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _context;

        public CommentsController(CommentContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var values = await _context.UserComments.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var value = await _context.UserComments.FindAsync(id);
            if (value == null)
            {
                return NotFound("Rəy tapılmadı");
            }
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(UserComment userComment)
        {
            await _context.UserComments.AddAsync(userComment);
            await _context.SaveChangesAsync();
            return Ok("Rəy uğurla əlavə olundu");
        }
        [HttpDelete] 
        public async Task<IActionResult> DeleteComment(int id)
        {
            var value=_context.UserComments.Find(id);
            _context.UserComments.Remove(value);
            _context.SaveChanges();
            return Ok("Rəy uğurla silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment(UserComment userComment)
        {
            _context.UserComments.Update(userComment);
            _context.SaveChanges();
            return Ok("Rəy dəyisdirildi");
        }
        [HttpGet("CommentListByProductId/{id}")]
        public async Task<IActionResult> CommentListByProductId(string id)
        {
            var values = await _context.UserComments.Where(x => x.ProductId == id).ToListAsync();
            return Ok(values);
        }
    }
}
