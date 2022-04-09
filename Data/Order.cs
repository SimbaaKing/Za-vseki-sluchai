using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManicureAndPedicureSalon.Data
{
    public class Order
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public  Product Product { get; set; } 
       
        public string ClientId { get; set; }
        public Client Client { get; set; }   
        
        public int Quantity { get; set; }
        public DateTime OrderedOn { get; set; }
    }
}
