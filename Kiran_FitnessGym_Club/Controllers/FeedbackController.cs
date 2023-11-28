
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kiran_FitnessGym_Club.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly Kiran_FitnessGym_clubContext _context;

    public FeedbackController(Kiran_FitnessGym_clubContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback()
    {
        return await _context.Feedback.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Feedback>> GetFeedbackById(int id)
    {
        var feedback = await _context.Feedback.FindAsync(id);

        if (feedback == null)
        {
            return NotFound("Feedback not found");
        }

        return feedback;
    }

    [HttpGet("trainer/{trainerId}")]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbackForTrainer(int trainerId)
    {
        var feedbackForTrainer = await _context.Feedback
            .Where(f => f.TrainerId == trainerId)
            .ToListAsync();

        if (feedbackForTrainer == null || feedbackForTrainer.Count == 0)
        {
            return NotFound("No feedback found for the specified trainer");
        }

        return feedbackForTrainer;
    }

    [HttpGet("member/{memberId}")]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbackForMember(int memberId)
    {
        var feedbackForMember = await _context.Feedback
            .Where(f => f.MemberId == memberId)
            .ToListAsync();

        if (feedbackForMember == null || feedbackForMember.Count == 0)
        {
            return NotFound("No feedback found for the specified member");
        }

        return feedbackForMember;
    }

    [HttpPost]
    public async Task<ActionResult> PostFeedback([FromBody] Feedback feedback)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid feedback data");
        }

        // Check if the trainer and member with the given ids exist
        if (!_context.TrainerRegt.Any(t => t.TrainerId == feedback.TrainerId) ||
            !_context.MemberRegt.Any(m => m.MemberId == feedback.MemberId))
        {
            return NotFound("Trainer or Member not found");
        }

        // Additional validation for the rating
        if (feedback.Rating < 1 || feedback.Rating > 5)
        {
            return BadRequest("Invalid rating. Rating should be between 1 and 5.");
        }

        _context.Feedback.Add(feedback);
        await _context.SaveChangesAsync();

        return Ok("Feedback submitted successfully");
    }
}
