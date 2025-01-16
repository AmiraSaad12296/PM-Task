using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pyramakerz_Task.BL.DTO;
using Pyramakerz_Task.BL.UOW;
using Pyramakerz_Task.Models;

namespace Pyramakerz_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        UnitOfWork unitOfWork;

        public StudentController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public ActionResult GetstudentByid(int id)
        {
            Student s = unitOfWork.StudentsRepository.selectbyid(id);
            if (s == null) return NotFound();
            else
            {
                StudentSchool sdto = new StudentSchool()
                {
                    Std_id = s.Std_id,
                    Std_Name = s.Std_Name,
                    Nationality = s.Nationality,
                    Gender = s.Gender,
                    Mobile = s.Mobile,
                };

                return Ok(sdto);
            }
        }
    }
}
