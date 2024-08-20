using BlogManagementApi.Models;
using BlogManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService _service;

        public BlogPostsController(IBlogPostService service)
        {
            _service = service;
        }

        [HttpGet("GetBlogs")]
        public async Task<ActionResult<List<BlogPost>>> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var post = await _service.GetAsync(id.Value);
                    if (post == null)
                    {
                        return NotFound();
                    }
                    return Ok(post);
                }
                var posts = await _service.GetAllAsync();
                return Ok(posts);
            }
            catch (ApplicationException ex)
            {
                // Log the exception (logging setup not shown here)
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddBlogs")]
        public async Task<ActionResult<BlogPost>> Post([FromBody] BlogPost post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddAsync(post);
                return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
            }
            catch (ApplicationException ex)
            {
                // Log the exception (logging setup not shown here)
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateBlog/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BlogPost post)
        {
            if (id != post.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (await _service.GetAsync(id) == null)
                {
                    return NotFound();
                }

                await _service.UpdateAsync(post);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                // Log the exception (logging setup not shown here)
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteBlog/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingPost = await _service.GetAsync(id);
                if (existingPost == null)
                {
                    return NotFound();
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                // Log the exception (logging setup not shown here)
                return StatusCode(500, ex.Message);
            }
        }
    }
}