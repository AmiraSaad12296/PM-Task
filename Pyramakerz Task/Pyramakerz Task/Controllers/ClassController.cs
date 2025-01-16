using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pyramakerz_Task.BL.DTO;
using Pyramakerz_Task.BL.UOW;
using Pyramakerz_Task.Models;
using System.ComponentModel.DataAnnotations;

namespace Pyramakerz_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        UnitOfWork unitOfWork;

        public ClassController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        public ActionResult Getclasses()
        {
            List<Classroom> classes = unitOfWork.classRepository.selectall();
            List<ClassDTO> classessDTO = new List<ClassDTO>();
            foreach (var c in classes)
            {
                ClassDTO cldto = new ClassDTO()
                {
                    Cl__Id=c.Cl__Id,
                    Name=c.Name,
                    Number =c.Number,
                };

                classessDTO.Add(cldto);

            }
            return Ok(classessDTO);
        }

        [HttpGet("Studentss/{className}")]
        public IActionResult GetStudentsByclassName(string className)
        {
            var classes = unitOfWork.classRepository
                .selectall()
                .FirstOrDefault(c => c.Name == className);

            if (classes == null)
            {
                return NotFound($"School with name '{className}' not found.");
            }

            var students = unitOfWork.StudentsAcademicYearRepository
            .selectall()
            .Where(cl => cl.Class_Id == classes.Cl__Id)
            .Select(cl => new StudentSchool()
            {
                Std_id = cl.Student.Std_id,
                Std_Name = cl.Student.Std_Name,
                Gender = cl.Student.Gender,
                Mobile = cl.Student.Mobile,
                Nationality = cl.Student.Nationality,
            })
            .ToList();


            return Ok(new { Students = students });
        }


        [HttpGet("{id}")]
        public ActionResult getbyid(int id)
        {
            Classroom c = unitOfWork.classRepository.selectbyid(id);
            if (c == null) return NotFound();
            else
            {
                ClassDTO clDTO = new ClassDTO()
                {
                    Name=c.Name

                };
                return Ok(clDTO);
            }

        }

    }
}
