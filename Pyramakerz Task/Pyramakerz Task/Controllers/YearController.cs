using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pyramakerz_Task.BL.DTO;
using Pyramakerz_Task.BL.UOW;
using Pyramakerz_Task.Models;

namespace Pyramakerz_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearController : ControllerBase
    {
        UnitOfWork unitOfWork;

        public YearController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult GetYears()
        {
            List<Acadimic_Year> years = unitOfWork.academicYearRepository.selectall();
            List<AcademicYearDTO> yearsDTO = new List<AcademicYearDTO>();
            foreach (var y in years)
            {
                AcademicYearDTO acdto = new AcademicYearDTO()
                {
                   Ac_id=y.Ac_id,
                   Name=y.Name, 
                   Datefrom=y.Datefrom, 
                   DateTo=y.DateTo,
                   IsActive=y.IsActive, 
                };

                yearsDTO.Add(acdto);

            }
            return Ok(yearsDTO);
        }


        [HttpGet("Students/{year}")]
        public IActionResult GetStudentsByYear(int year)
        {
            var semesters = unitOfWork.semsterRepository
            .selectall()
         .Where(s => s.Academic_Year.Datefrom == year)
         .ToList();
            if (!semesters.Any())
            {
                return NotFound($"No semesters found for academic year {year}.");
            }

            var students = unitOfWork.StudentsAcademicYearRepository
            .selectall()
            .Where(sa => semesters.Any(s => s.Se_Id == sa.Semester_Id))
            .Select(sa => new StudentSchool()
            {
                Std_id = sa.Student.Std_id,
                Std_Name = sa.Student.Std_Name,
                Gender = sa.Student.Gender,
                Mobile = sa.Student.Mobile,
                Nationality = sa.Student.Nationality,
            })
            .ToList();

            return Ok(new { Students = students });
        }
    }


}
