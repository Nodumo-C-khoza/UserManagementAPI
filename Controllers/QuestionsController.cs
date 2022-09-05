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
    public class QuestionsController : ControllerBase
    {
        //DI
        private readonly UsersDbContext _context;

        public QuestionsController(UsersDbContext context)
        {
            _context = context;
        }
        // GET: api/QuestionsDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionsModel>>> GetQuestions()
        {
            //Random 5 questions
            var Qns = await (_context.QuestionsDetails
                 .Select(x => new
                 {
                     QnId = x.QnId,
                   QnInWords   = x.QnInWords,
                     ImageName = x.ImageName,
                     Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                 })
                 .OrderBy(y => Guid.NewGuid())
                 .Take(5)
                 ).ToListAsync();

            return Ok(Qns);
        }

        // POST: api/QuestionsDetails/GetAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("GetAnswers")]
        public async Task<ActionResult<QuestionsModel>> RetrieveAnswers(int[] qnIds)
        {
            var answers = await (_context.QuestionsDetails
                .Where(x => qnIds.Contains(x.QnId))
                .Select(y => new
                {
                    QnId = y.QnId,
                    QnInWords = y.QnInWords,
                    ImageName = y.ImageName,
                    Options = new string[] { y.Option1, y.Option2, y.Option3, y.Option4 },
                    Answer = y.Answer
                })).ToListAsync();
            return Ok(answers);
        }
         
        // DELETE: api/QuestionsDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.QuestionsDetails.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.QuestionsDetails.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/QuestionsDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionsModel>> GetQuestion(int id)
        {
            var question = await _context.QuestionsDetails.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/QuestionsDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, QuestionsModel question)
        {
            if (id != question.QnId)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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
        private bool QuestionExists(int id)
        {
            return _context.QuestionsDetails.Any(e => e.QnId == id);
        }
    }
}
