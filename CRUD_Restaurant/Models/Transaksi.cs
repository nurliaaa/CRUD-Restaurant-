using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Restaurant.Models
{
    public class Transaksi
    {
   
        public int TransaksiID { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public int FoodID { get; set; }
        public int CustomerID { get; set; }
        public int JumlahItem { get; set; }
        public Decimal TotalHarga { get; set; }

        [ForeignKey("FoodID")]
        public Food Food { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
    }
}
