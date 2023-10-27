using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Restaurant.Models
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
    }
}
