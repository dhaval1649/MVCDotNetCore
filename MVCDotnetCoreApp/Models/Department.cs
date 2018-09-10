using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDotnetCoreApp.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<DepartmentMaster> DepartmentMaster { get; set; }
        public DbSet<EmployeeMaster> EmployeeMaster { get; set; }
    }

    public class DepartmentMaster
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public List<EmployeeMaster> EmployeeMaster { get; set; }
    }

    public class EmployeeMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Qualification { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string Contact_Number { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public DepartmentMaster DepartmentMaster { get; set; }
        [NotMapped]
        public string DepartmentName { get; set; }

    }
}
