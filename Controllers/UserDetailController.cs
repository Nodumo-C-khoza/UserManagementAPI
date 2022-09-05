using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public UserDetailController(UsersDbContext context)
        {
            _context = context;
        }
        // GET: api/UserDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailsModel>>> GetUserDetails()
        {
            return await _context.UserDetails.ToListAsync();
        }

        // GET: api/UserDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsModel>> GetUser(int id)
        {
            var user = await _context.UserDetails.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //POST: api/UserDetail
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "PostUserModel")]
        public async Task<ActionResult<UserDetailsModel>> PostUserModel(UserDetailsModel UserDetail)
        {
            var temp = _context.UserDetails
                .Where(x => x.FullName == UserDetail.FullName
                && x.Email == UserDetail.Email)
                .FirstOrDefault();

            if (temp == null)
            {
                _context.UserDetails.Add(UserDetail);
                await _context.SaveChangesAsync();
            }
            else
                UserDetail = temp;

            return Ok(UserDetail);

        }

        // PUT: api/Participant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(int id, UserRestult _userResult)
        {
            if (id != _userResult.UserId)
            {
                return BadRequest();
            }

            // get all current details of the record, then update with quiz results
            UserDetailsModel user = _context.UserDetails.Find(id);
            user.Score = _userResult.Score;
            user.TimeTaken = _userResult.TimeTaken;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/UserDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            var user = await _context.UserDetails.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.UserDetails.Any(e => e.UserId == id);
        }
    }

}
