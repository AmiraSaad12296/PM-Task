using Microsoft.AspNetCore.Mvc;
using Pyramakerz_Task.BL.UOW;
using Pyramakerz_Task.Models;
using Pyramakerz_Task.BL.DTO;

namespace Pyramakerz_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        UnitOfWork unit;

        public SchoolController(UnitOfWork unit)
        {
            this.unit = unit;
        }




        [HttpGet]
        public ActionResult GetSchools()
        {
            List<School> schools = unit.schoolRepository.selectall();
            List<SchoolDTO> schoolsDTO = new List<SchoolDTO>();
            foreach (var s in schools)
            {
                SchoolDTO scdto = new SchoolDTO()
                {
                    Sc_Id = s.Sc_Id,
                    Address = s.Address,
                    Name = s.Name,
                    ReportHeaderOneAr = s.ReportHeaderOneAr,
                    ReportHeaderTwoAr = s.ReportHeaderTwoAr,
                    ReportHeaderOneEn = s.ReportHeaderOneEn,
                    ReportHeaderTwoEn = s.ReportHeaderTwoEn,
                    ReportImage = s.ReportImage,
                };

                schoolsDTO.Add(scdto);

            }
            return Ok(schoolsDTO);
        }


        //Get student data by school name

        [HttpGet("{id}")]
        public ActionResult getbyid(int id)
        {
            School s = unit.schoolRepository.selectbyid(id);
            if (s == null) return NotFound();
            else
            {
                SchoolHeaders shdto = new SchoolHeaders()
                {
                    ReportHeaderOneAr=s.ReportHeaderOneAr,
                    ReportHeaderOneEn=s.ReportHeaderOneEn,
                    ReportHeaderTwoAr=s.ReportHeaderTwoAr,  
                    ReportHeaderTwoEn=s.ReportHeaderTwoEn,
                    ReportImage= s.ReportImage, 
                
                };
                return Ok(shdto);
            }
            
        }



        [HttpGet("years")]
        public ActionResult GetYears(int schoolId)
        {
            var years = unit.semsterRepository.selectall()
                .Where(s => s.Academic_Year.School_Id == schoolId)
                .Select(s => new
                {
                    s.Academic_Year.Ac_id,
                   s.Academic_Year.Datefrom,
                   s.Academic_Year.DateTo,
                })
                .Distinct()
                .ToList();

            return Ok(years);
        }



        [HttpGet("grades")]
        public ActionResult GetGrades(int schoolID, int year)
        {
            var grades = unit.StudentsAcademicYearRepository.selectall()
                .Where(sa => sa.School.Sc_Id == schoolID && sa.Semester.Academic_Year.Ac_id == year)
                .Select(sa => sa.Grade)
                .Distinct()
                .Select(g => new { g.Gr_Id,g.Name })
                .ToList();

            return Ok(grades);
        }




        [HttpGet("classes")]
        public IActionResult GetClasses(int schoolid, int year, int gradeId)
        {
            var classes = unit.StudentsAcademicYearRepository.selectall()
                .Where(sa => sa.School.Sc_Id == schoolid &&
                             sa.Semester.Academic_Year.Ac_id == year &&
                             sa.Grade.Gr_Id == gradeId)
                .Select(sa => sa.Class)
                .Distinct()
                .Select(c => new { c.Cl__Id,c.Name })
                .ToList();

            return Ok(classes);
        }




        [HttpGet("detailed-reports")]
        public IActionResult GetReport(int schoolID, int year, int gradeID, int classID)
        {
            var students = unit.StudentsAcademicYearRepository.selectall()
                .Where(sa => sa.School.Sc_Id == schoolID &&
                             sa.Semester.Academic_Year.Ac_id == year &&
                             sa.Grade.Gr_Id == gradeID &&
                             sa.Class.Cl__Id == classID)
                .Select(sa => sa.Student)
                .Select(st => new
                {
                    st.Std_id,
                    st.Std_Name,
                    st.Nationality,
                    st.Mobile,
                    st.Gender
                })
                .ToList();

          

            return Ok(students);
        }



        [HttpGet("images/{fileName}")]
        public ActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            if (System.IO.File.Exists(filePath))
            {
                var bytes = System.IO.File.ReadAllBytes(filePath);
                return File(bytes, "image/png");
            }
            else
            {
                return NotFound();
            }
        }

    }
}
