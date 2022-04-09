using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manifaction { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public DateTime DateRegister { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
