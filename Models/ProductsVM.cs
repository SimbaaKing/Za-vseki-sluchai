using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Models
{
    public class ProductsVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Manifaction { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Images { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime DateRegister { get; set; }
      

    }
}
