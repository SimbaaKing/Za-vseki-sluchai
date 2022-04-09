using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Data
{
    public class Employer
    { 
        public int EmployerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Activity { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
