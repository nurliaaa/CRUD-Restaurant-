using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace CRUD_Restaurant.Models
{
    public class Food
    {
        public int FoodID { get; set; }
        public String Name { get; set; }
        public String Category { get; set; }
        public Decimal Price { get; set; }

        public List<Transaksi> Transaksi { get; set; }
    }
}
