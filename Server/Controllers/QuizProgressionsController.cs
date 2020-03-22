using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorSignalRApp.Shared;

namespace BlazorSignalRApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizProgressionsController : ControllerBase
    {
        private readonly QuizDBContext _context;

        public QuizProgressionsController(QuizDBContext context)
        {
            _context = context;
        }

        // GET: api/QuizProgressions
        /*
                [HttpGet]
                public async Task<ActionResult<IEnumerable<QuizProgression>>> GetQuizProgressions()
                {
                    return await _context.QuizProgressions.ToListAsync();
                }
        */

        // GET: api/QuizProgressions
        [HttpGet]
        public async Task<ActionResult<QuizProgression>> GetQuizProgression()
        {
            var quizProgression = await _context.QuizProgressions.FirstAsync();

            if (quizProgression == null)
            {
                return NotFound();
            }

            return quizProgression;
        }

        // PUT: api/QuizProgressions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizProgression(int id, QuizProgression quizProgression)
        {
            if (id != quizProgression.QuizProgressionID)
            {
                return BadRequest();
            }

            _context.Entry(quizProgression).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizProgressionExists(id))
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

        // POST: api/QuizProgressions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<QuizProgression>> PostQuizProgression(QuizProgression quizProgression)
        {
            _context.QuizProgressions.Add(quizProgression);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuizProgression", new { id = quizProgression.QuizProgressionID }, quizProgression);
        }

        // DELETE: api/QuizProgressions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuizProgression>> DeleteQuizProgression(int id)
        {
            var quizProgression = await _context.QuizProgressions.FindAsync(id);
            if (quizProgression == null)
            {
                return NotFound();
            }

            _context.QuizProgressions.Remove(quizProgression);
            await _context.SaveChangesAsync();

            return quizProgression;
        }

        private bool QuizProgressionExists(int id)
        {
            return _context.QuizProgressions.Any(e => e.QuizProgressionID == id);
        }
    }
}
