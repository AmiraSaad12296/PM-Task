﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pyramakerz_Task.Models;

[Table("Acadimic_Year")]
public partial class Acadimic_Year
{
    [Key]
    public int Ac_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    
    public int Datefrom { get; set; }

    public int DateTo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string IsActive { get; set; }

    public int? School_Id { get; set; }

    [InverseProperty("Academic_Year")]
    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();

    [ForeignKey("School_Id")]
    [InverseProperty("Acadimic_Years")]
    public virtual School School { get; set; }

    [InverseProperty("Academic_Year")]
    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();
}