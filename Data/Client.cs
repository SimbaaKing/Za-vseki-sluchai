using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Data
{
    public class Client : IdentityUser
    {
        public string FullName { get; set; }
        public Roles Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Appoitment> Appoitments { get; set; }

    }
}
