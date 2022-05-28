using ManicureAndPedicureSalon.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Models
{
    public class ServicesVM
    {
        public int ServiceId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public TypeCategory Category { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }
        public List<SelectListItem> Employers { get; set; }
        public int EmployerId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Images { get; set; }
        [Required(ErrorMessage = "This field is required")]

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
      
        public DateTime DateRegister { get; set; }
        
    }
}
