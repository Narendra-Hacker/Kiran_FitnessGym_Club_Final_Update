using Kiran_FitnessGym_Club.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kiran_FitnessGym_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerRegtController : ControllerBase
    {
        private readonly Kiran_FitnessGym_clubContext dbContext;
        private readonly ILogger<TrainerRegtController> logger;
        private readonly IConfiguration configuration;

        // public TrainerRegtController(ILogger<TrainerRegtController> logger)
        // {
        //     this.logger = logger;
        //  }


        public TrainerRegtController(Kiran_FitnessGym_clubContext dbContext, ILogger<TrainerRegtController> logger, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.configuration = configuration;
        }

        //[Authorize]
        [HttpGet("GetTrainer")]
        public IActionResult GetTrainer()
        {
            List<TrainerRegt> trainers = dbContext.TrainerRegt.ToList();

            // throw new Exception("Some unknown error occured");
            //throw new UnauthorizedAccessException();
            if (trainers.Count == 0)
            {
                return NoContent();
            }
            else
            {
                logger.LogInformation("This is Trainer Controller");
                return StatusCode(200, trainers);

            }


        }

        [HttpGet("GetTrainer/{trainerId}")]
        public IActionResult GetTrainerId(int trainerId)
        {
            TrainerRegt trainer = dbContext.TrainerRegt.Find(trainerId);
            return StatusCode(200, trainer);
        }


        [HttpPost("Register")]
        public IActionResult Register([FromBody] TrainerRegt trainer)
        {
            // Check for null values in required properties
            if (trainer == null ||
                string.IsNullOrWhiteSpace(trainer.FirstName) ||
                string.IsNullOrWhiteSpace(trainer.LastName) ||
                trainer.MobileNo==0 ||
                trainer.Experience == null ||
                string.IsNullOrWhiteSpace(trainer.Email) ||
                string.IsNullOrWhiteSpace(trainer.Password)

                // Add additional checks for other required properties
                // For example: trainer.Name, trainer.Phone, etc.
                // Ensure that all required properties are checked for null or empty values
                )
            {
                // Return a bad request response if any required property is null or empty
                return BadRequest("All required properties must have non-null and non-empty values");
            }

            // Check if the email already exists
            if (dbContext.TrainerRegt.Any(t => t.Email == trainer.Email))
            {
                // Return a conflict response if the email already exists
                return Conflict("Email already exists");
            }

            // The email is unique, proceed with registration
            //Console.WriteLine(trainer.City);

            //Add the trainer to the database
            dbContext.TrainerRegt.Add(trainer);

            // Save changes to the database
            dbContext.SaveChanges();

            // Return a success response
            return Created("Trainer Added", trainer);
        }




        [HttpPut("Update")]
        public IActionResult Update([FromBody] TrainerRegt trainer)
        {

            dbContext.Entry(trainer).State = EntityState.Modified;
            dbContext.SaveChanges();
            return Created("Trainer Updated", trainer);
        }


        [HttpPut("Update/{trainerId}")]
        public IActionResult Updateid(int trainerId,[FromBody] TrainerRegt trainer)
        {
            TrainerRegt trainers = dbContext.TrainerRegt.Find(trainerId);

            if(trainer.FirstName != null)
            {
                trainers.FirstName = trainer.FirstName;
            }

            if(trainer.LastName !=null)
            {
                trainers.LastName = trainer.LastName;
            }

            if(trainer.MobileNo != 0)
            {
               trainers.MobileNo = trainer.MobileNo;
            }

           
           
            if(trainer.Experience !=null)
            {
                trainers.Experience = trainer.Experience;
            }


            if(trainer.Salary !=null)
            {
                trainers.Salary = trainer.Salary;
            }

            if (trainer.TrainingFees != null)
            {
                trainers.TrainingFees = trainer.TrainingFees;
            }

            if(trainer.Email !=null)
            {
                trainers.Email = trainer.Email;
            }

            if(trainer.Password !=null)
            {
                trainers.Password = trainer.Password;
            }

            if(trainer.ScheduleId !=null)
            {
                trainers.ScheduleId= trainer.ScheduleId;
            }

            dbContext.SaveChanges();
            return Ok();
        }


        [HttpDelete("Delete/{trainerId}")]
        public IActionResult Delete(int trainerId)
        {
            TrainerRegt trainer = dbContext.TrainerRegt.Find(trainerId);
            if (trainer == null)
            {
                return StatusCode(404, "Course Id not available");
            }
            else
            {
                dbContext.TrainerRegt.Remove(trainer);
                dbContext.SaveChanges();
                return Ok();
            }
        }


        [Authorize]
        [HttpGet("GetData")]

        public IActionResult GetName(string email)
        {
            var user = dbContext.TrainerRegt.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] TrainerRegt trainer)
        {
            List<TrainerRegt> ul = dbContext.TrainerRegt.ToList();

            var user = dbContext.TrainerRegt.FirstOrDefault(u => u.Email == trainer.Email && u.Password == trainer.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha512Signature
                                );
            var claims = new List<Claim>
{
      new Claim(JwtRegisteredClaimNames.Email, trainer.Email),

};
            //string userRole = null;


            var expires = DateTime.UtcNow.AddMinutes(10);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return Ok(jwtToken);
        }

        [HttpPost("GetName")]


        public IActionResult GetName(string email, string password)
        {
            var user = dbContext.TrainerRegt.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user.FirstName);

        }

        [HttpPost("CheckEmailExist/{email}")]

        public IActionResult CheckEmailExist(string email)
        {
            //List<TrainerRegt> ul = dbContext.TrainerRegt.ToList();
            var user = dbContext.TrainerRegt.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return StatusCode(200, true);
            }

            else
            {
                return StatusCode(200, false);
            }
        }

        [HttpGet("Schedule/{id}")]

        public IActionResult Schedule(int id)
        {
            var schedule = dbContext.Schedule.FirstOrDefault(u => u.Id == id);
            return Ok(schedule);
        }


        [HttpGet("GetSchedule")]
        public IActionResult GetSchedule()
        {
            List<Schedule> schedules = dbContext.Schedule.ToList();

            if (schedules.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(200, schedules);
            }


        }





    }
}
