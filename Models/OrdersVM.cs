using ManicureAndPedicureSalon.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Models
{
    public class OrdersVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public List<SelectListItem> Products { get; set; }

        public int ClientId { get; set; }
        public List<SelectListItem> Client { get; set; }

        public int Quantity { get; set; }
        public DateTime OrderedOn { get; set; }
    }
}
