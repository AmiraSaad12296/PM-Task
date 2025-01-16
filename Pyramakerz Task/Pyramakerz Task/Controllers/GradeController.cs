using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pyramakerz_Task.BL.DTO;
using Pyramakerz_Task.BL.UOW;
using Pyramakerz_Task.Models;

namespace Pyramakerz_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        UnitOfWork unitOfWork;

        public GradeController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        public ActionResult Getgrades()
        {
            List<Grade> grades = unitOfWork.GradeRepository.selectall();
            List<GradeDto> gradesDTO = new List<GradeDto>();
            foreach (var g in grades)
            {
                GradeDto gdto = new GradeDto()
                {
                    Gr_Id=g.Gr_Id,
                    Name = g.Name,
                    DateFrom= g.DateFrom,
                    DateTo= g.DateTo,
                };

                gradesDTO.Add(gdto);

            }
            return Ok(gradesDTO);
        }

        [HttpGet("Students/{gradeName}")]
        public IActionResult GetStudentsByGradeName(string gradeName)
        {
            var grade = unitOfWork.GradeRepository
                .selectall()
                .FirstOrDefault(s => s.Name == gradeName);

            if (grade == null)
            {
                return NotFound($"Grade with name '{gradeName}' not found.");
            }

            var grades = unitOfWork.StudentsAcademicYearRepository
            .selectall()
            .Where(gr => gr.Grade_Id == grade.Gr_Id)
            .Select(gr => new StudentSchool()
            {
                Std_id = gr.Student.Std_id,
                Std_Name = gr.Student.Std_Name,
                Gender = gr.Student.Gender,
                Mobile = gr.Student.Mobile,
                Nationality = gr.Student.Nationality,
            })
            .ToList();

            return Ok(new { Grades = grades });
        }
    }
}
