using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Data
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public TypeCategory Category { get; set; }
        public string Description { get; set; }
        public Employer Employer { get; set; }
        public int EmployerId { get; set; }
        
        public string Images { get; set;}
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public DateTime DateRegister { get; set; }
        public virtual ICollection<Appoitment> Appoitments { get; set; }
    }
}
