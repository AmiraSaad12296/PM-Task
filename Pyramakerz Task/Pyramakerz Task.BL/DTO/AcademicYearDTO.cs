using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyramakerz_Task.BL.DTO
{
    public class AcademicYearDTO
    {
        public int Ac_id { get; set; }

      
        public string Name { get; set; }


        public int Datefrom { get; set; }

        public int DateTo { get; set; }
        public string IsActive { get; set; }

    }
}
