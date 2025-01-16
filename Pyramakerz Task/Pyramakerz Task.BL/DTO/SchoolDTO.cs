using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyramakerz_Task.BL.DTO
{
    public class SchoolDTO
    {
        public int Sc_Id { get; set; }

  
        public string Name { get; set; }

        public string Address { get; set; }

        public string ReportHeaderOneEn { get; set; }

        public string ReportHeaderOneAr { get; set; }

      
        public string ReportHeaderTwoEn { get; set; }

        public string ReportHeaderTwoAr { get; set; }

        public string ReportImage { get; set; }
    }
}
