using Kiran_FitnessGym_Club.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Kiran_FitnessGym_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRegtController : ControllerBase
    {
        private readonly Kiran_FitnessGym_clubContext dbContext;
        public MemberRegtController(Kiran_FitnessGym_clubContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("GetMember")]
        public IActionResult GetMember()
        {
            List<MemberRegt> members = dbContext.MemberRegt.ToList();

            if (members.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(200, members);
            }


        }


        

        [HttpGet("Alltrainers/{id}")]

        public IActionResult Gettrainers(int id)
        {
            var kiran = new ArrayList();
            List<TrainerRegt> t1 = dbContext.TrainerRegt.ToList();
            foreach (var item in t1)
            {

                if (item.ScheduleId == id)
                {
                    kiran.Add(item);
                }

            }

            return StatusCode(200, kiran);
        }





        [HttpPost("Register")]
        public IActionResult Register([FromBody] MemberRegt member)
        {
            if (member == null ||
            string.IsNullOrWhiteSpace(member.FirstName) ||
            string.IsNullOrWhiteSpace(member.LastName) ||
            member.MobileNo == 0 ||
            string.IsNullOrWhiteSpace(member.City) ||
            member.DateOfJoin == null ||
            //member.TrainerId == null ||
            string.IsNullOrWhiteSpace(member.Email) ||
            string.IsNullOrWhiteSpace(member.Password)
                )
            {
                // Return a bad request response if any required property is null or empty
                return BadRequest("All required properties must have non-null and non-empty values");
            }

            if (dbContext.MemberRegt.Any(t => t.Email == member.Email))
            {
                return Conflict("Email Already Exists");
            }

            dbContext.MemberRegt.Add(member);
            dbContext.SaveChanges();

            return Created("Member Added", member);

        }
         


        [HttpPut("Update")]
        public IActionResult Update([FromBody] MemberRegt member)
        {

            dbContext.Entry(member).State = EntityState.Modified;
            dbContext.SaveChanges();
            return Created("Member updated", member);
        }


        [HttpPut("Update/{memberId}")]
        public IActionResult Updateid(int memberId, [FromBody] MemberRegt member)
        {

            MemberRegt members = dbContext.MemberRegt.Find(memberId);


            if (member.FirstName != null)
            {
                members.FirstName = member.FirstName;
            }

            if (member.LastName != null)
            {
                members.LastName = member.LastName;
            }

            if (member.MobileNo != 0)
            {
                members.MobileNo = member.MobileNo;
            }

            if (member.City != null)
            {
                members.City = member.City;
            }

            if (member.DateOfJoin != null)
            {
                members.DateOfJoin = member.DateOfJoin;
            }

            if (member.TrainerId != null)
            {
                members.TrainerId = member.TrainerId;
            }

            if (member.Email != null)
            {
                members.Email = member.Email;
            }

            if (member.Password != null)
            {
                members.Password = member.Password;

                
            }
            dbContext.SaveChanges();
            return Ok();



        }



        [HttpDelete("Delete/{memberId}")]
        public IActionResult Delete(int memberId)
        {
            MemberRegt member = dbContext.MemberRegt.Find(memberId);
            if (member == null)
            {
                return StatusCode(404, "Course Id not available");
            }
            else
            {
                dbContext.MemberRegt.Remove(member);
                dbContext.SaveChanges();
                return Ok();
            }
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] MemberRegt member)
        {
            List<MemberRegt> ul = dbContext.MemberRegt.ToList();

            var user = dbContext.MemberRegt.FirstOrDefault(u => u.Email == member.Email && u.Password == member.Password);
            if (user == null)
            {
                return Unauthorized();
            }


            var obj = new
            {
                kiran = "Authorized",
                user = user,
            };


            return StatusCode(200, obj);
        }

        [HttpPost("GetName")]


        public IActionResult GetName(string email, string password)
        {
            var user = dbContext.MemberRegt.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user.FirstName);

        }

        [HttpGet("GetData")]

        public IActionResult GetName(string email)
        {
            var user = dbContext.MemberRegt.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [HttpGet("GetMember/{id}")]
        public IActionResult GetTrainerId(int id)
        {
            MemberRegt member = dbContext.MemberRegt.Find(id);
            if (member == null)
            {
                return StatusCode(404, "Member not available");
            }

            else
            {

                return StatusCode(200, member);
            }
        }

        // MemberRegtController.cs

        [HttpGet("GetMembersByTrainer/{trainerId}")]
        public IActionResult GetMembersByTrainer(int trainerId)
        {
            List<MemberRegt> members = dbContext.MemberRegt
                .Where(member => member.TrainerId == trainerId)
                .ToList();

            if (members.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(200, members);
            }
        }


        [HttpPost("CheckEmailExist/{email}")]

        public IActionResult CheckEmailExist(string email)
        {
            //List<TrainerRegt> ul = dbContext.TrainerRegt.ToList();
            var user = dbContext.MemberRegt.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return StatusCode(200, true);
            }

            else
            {
                return StatusCode(200, false);
            }
        }

       
    }
}
