using Kiran_FitnessGym_Club.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kiran_FitnessGym_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeDetailsController : ControllerBase
    {
        private readonly Kiran_FitnessGym_clubContext dbContext;
        public FeeDetailsController(Kiran_FitnessGym_clubContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("GetFeeDetails")]
        public IActionResult GetFeeDetails()
        {
            List<FeeDetails> fees = dbContext.FeeDetails.ToList();

            if (fees.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(200, fees);
            }

        }

        [HttpGet("GetFeeDetails/{feeId}")]
        public IActionResult GetFeeDetailsId(int feeId)
        {
            FeeDetails fees = dbContext.FeeDetails.Find(feeId);
            return StatusCode(200, fees);

        }



        [HttpPost("Register")]
        public IActionResult Post([FromBody] FeeDetails feeDetail)
        {
            System.Console.WriteLine(feeDetail.MemberId + " " + feeDetail.Subscription + " " + feeDetail.AmountPaid);
            int subsc = (int)feeDetail.Subscription;
            int trainerId = (int)dbContext.MemberRegt.Find(feeDetail.MemberId).TrainerId;


            int trainerFee = (int)dbContext.TrainerRegt.Find(trainerId).TrainingFees;

            System.Console.WriteLine("trainer Id:" + trainerId + " trainer fee:" + trainerFee);

            feeDetail.TotalFees = subsc * 1000 + subsc * trainerFee;

            feeDetail.FeeDue = feeDetail.TotalFees - feeDetail.AmountPaid;
            System.Console.WriteLine(feeDetail.FeeDue + "   " + feeDetail.TotalFees);

            if (feeDetail.FeeDue != 0)
            {
                feeDetail.Status = "Pending";
            }
            else
            {
                feeDetail.Status = "Completed";
            }
            dbContext.FeeDetails.Add(feeDetail);


            System.Console.WriteLine("hell");
            dbContext.SaveChanges();
            System.Console.WriteLine("byee");
            return Ok("Fee Added" + feeDetail);
        }




        [HttpPut("Update")]
        public IActionResult Update([FromBody] FeeDetails feeDetail)
        {
            int subsc = (int)feeDetail.Subscription;

            int trainerId = (int)dbContext.MemberRegt.Find(feeDetail.MemberId).TrainerId;


            int trainerFee = (int)dbContext.TrainerRegt.Find(trainerId).TrainingFees;

            System.Console.WriteLine("trainer Id:" + trainerId + " trainer fee:" + trainerFee);

            feeDetail.TotalFees = subsc * 1000 + subsc * trainerFee;

            feeDetail.FeeDue = feeDetail.TotalFees - feeDetail.AmountPaid;

            System.Console.WriteLine(feeDetail.FeeDue + "   " + feeDetail.TotalFees);

            if (feeDetail.FeeDue != 0)
            {
                feeDetail.Status = "Pending";
            }
            else
            {
                feeDetail.Status = "Completed";
            }

            dbContext.Entry(feeDetail).State = EntityState.Modified;
            System.Console.WriteLine("hell");
            dbContext.SaveChanges();
            System.Console.WriteLine("byee");
            return Ok("Fee Added" + feeDetail);
        }

        [HttpPut("Update/{feeId}")]
        public IActionResult Updateid(int feeId, [FromBody] FeeDetails feeDetail)
        {
            // Find the existing FeeDetails object by feeId
            FeeDetails fees = dbContext.FeeDetails.Find(feeId);

            if (fees == null)
            {
                return NotFound("Fee details not found.");
            }

            try
            {
                // Update properties only if they are not null in feeDetail
                if (feeDetail.MemberId != null)
                {
                    fees.MemberId = feeDetail.MemberId;
                }

                if (feeDetail.Subscription != null)
                {
                    fees.Subscription = feeDetail.Subscription;
                }

                if (feeDetail.AmountPaid != null)
                {
                    fees.AmountPaid = feeDetail.AmountPaid;
                }

                // Calculate subsc, trainerId, trainerFee as before
                int subsc = (int)(fees.Subscription ?? 0);
                int trainerId = (int)dbContext.MemberRegt.Find(fees.MemberId).TrainerId;
                int trainerFee = (int)dbContext.TrainerRegt.Find(trainerId).TrainingFees;

                System.Console.WriteLine("trainer Id:" + trainerId + " trainer fee:" + trainerFee);

                fees.TotalFees = subsc * 1000 + subsc * trainerFee;
                fees.FeeDue = fees.TotalFees - (feeDetail.AmountPaid ?? fees.AmountPaid);

                System.Console.WriteLine(fees.FeeDue + "   " + fees.TotalFees);

                // Determine the Status
                fees.Status = fees.FeeDue != 0 ? "Pending" : "Completed";

                // Save changes to the database
                dbContext.SaveChanges();

                return Ok("Fee details updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete("Delete/{feeId}")]
        public IActionResult Delete(int feeId)
        {
            FeeDetails fees = dbContext.FeeDetails.Find(feeId);
            if (fees == null)
            {
                return StatusCode(404, "Course Id not available");
            }
            else
            {
                dbContext.FeeDetails.Remove(fees);
                dbContext.SaveChanges();
                return Ok();
            }
        }


        [HttpGet("getReceipt/{id}")]
        public IActionResult GetReceipt(int id)
        {
            Console.WriteLine("GetReceipt" + id);
            FeeDetails fees = dbContext.FeeDetails.Find(id);
            if (fees == null)
            {
                return StatusCode(404, "FeeDetails not available");

            }
            else
            {
                return StatusCode(200, fees);
            }

        }

        [HttpGet("getfeeid/{id}")]

        public IActionResult feedata(int id)
        {
            var data = dbContext.FeeDetails.FirstOrDefault(u => u.MemberId == id);
            return Ok(data);

        }
    }
}
