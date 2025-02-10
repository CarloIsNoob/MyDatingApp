using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController(DataContext context) : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await context.Users.ToListAsync();

            return users;
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task< ActionResult<AppUser>> GetUsers(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null) return NotFound();

            return user;
        }
    }
}
