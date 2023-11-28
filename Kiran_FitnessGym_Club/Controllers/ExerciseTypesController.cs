using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kiran_FitnessGym_Club.Models;

namespace Kiran_FitnessGym_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseTypesController : ControllerBase
    {
        private readonly Kiran_FitnessGym_clubContext _context;

        public ExerciseTypesController(Kiran_FitnessGym_clubContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseType>>> GetExerciseType()
        {
            return await _context.ExerciseType.ToListAsync();
        }

        // GET: api/ExerciseTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseType>> GetExerciseType(int id)
        {
            var exerciseType = await _context.ExerciseType.FindAsync(id);

            if (exerciseType == null)
            {
                return NotFound();
            }

            return exerciseType;
        }

        // PUT: api/ExerciseTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseType(int id, ExerciseType exerciseType)
        {
            if (id != exerciseType.ExercciseId)
            {
                return BadRequest();
            }

            _context.Entry(exerciseType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseTypeExists(id))
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

        // POST: api/ExerciseTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseType>> PostExerciseType(ExerciseType exerciseType)
        {
            _context.ExerciseType.Add(exerciseType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseType", new { id = exerciseType.ExercciseId }, exerciseType);
        }

        // DELETE: api/ExerciseTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseType(int id)
        {
            var exerciseType = await _context.ExerciseType.FindAsync(id);
            if (exerciseType == null)
            {
                return NotFound();
            }

            _context.ExerciseType.Remove(exerciseType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseTypeExists(int id)
        {
            return _context.ExerciseType.Any(e => e.ExercciseId == id);
        }
    }
}
