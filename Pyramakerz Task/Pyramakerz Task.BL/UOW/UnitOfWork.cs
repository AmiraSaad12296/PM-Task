using Pyramakerz_Task.BL.Repo;
using Pyramakerz_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyramakerz_Task.BL.UOW
{
    public class UnitOfWork
    {
        PyramakerzTaskContext db;
        GenericRepo<School> school;
        GenericRepo<Student> student;
        GenericRepo<StudentAcademicYear> studentAcademicYear;
        GenericRepo<Grade> grade;
        GenericRepo<Classroom> classroom;
        GenericRepo<Semester> semster;
        GenericRepo<Acadimic_Year> academicYear;






        public UnitOfWork(PyramakerzTaskContext db)
        {
            this.db = db;
        }

        public GenericRepo<School> schoolRepository
        {
            get
            {
                if (school == null)
                {
                    school = new GenericRepo<School>(db);

                }
                return school;
            }
        }

        public GenericRepo<Student> StudentsRepository
        {
            get
            {
                if (student == null)
                {
                    student = new GenericRepo<Student>(db);

                }
                return student;
            }
        }

        public GenericRepo<StudentAcademicYear> StudentsAcademicYearRepository
        {
            get
            {
                if (studentAcademicYear == null)
                {
                    studentAcademicYear = new GenericRepo<StudentAcademicYear>(db);

                }
                return studentAcademicYear;
            }
        }


        public GenericRepo<Grade> GradeRepository
        {
            get
            {
                if (grade == null)
                {
                    grade = new GenericRepo<Grade>(db);

                }
                return grade;
            }
        }

        public GenericRepo<Classroom> classRepository
        {
            get
            {
                if (classroom == null)
                {
                    classroom = new GenericRepo<Classroom>(db);

                }
                return classroom;
            }
        }


        public GenericRepo<Semester> semsterRepository
        {
            get
            {
                if (semster == null)
                {
                    semster = new GenericRepo<Semester>(db);

                }
                return semster;
            }
        }

        public GenericRepo<Acadimic_Year> academicYearRepository
        {
            get
            {
                if (academicYear == null)
                {
                    academicYear = new GenericRepo<Acadimic_Year>(db);

                }
                return academicYear;
            }
        }
        public void savechanges()
        {
            db.SaveChanges();
        }
    }
}
