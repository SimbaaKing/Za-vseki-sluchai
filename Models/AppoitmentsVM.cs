using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Models
{
    public class AppoitmentsVM
    {
        public int Id { get; set; }

        public List<SelectListItem> Client { get; set; }
        public int ClientId { get; set; }

        public List<SelectListItem> Services { get; set; }
        public int ServiceId { get; set; }
        

        public DateTime DateVisit { get; set; }
        public DateTime TimeVisit { get; set; }
        public DateTime Date { get; set; }
    }
}
