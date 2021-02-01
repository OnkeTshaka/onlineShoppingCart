using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReturnGym.Models.OnlineShopping
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public  string Refcode { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

      
    }
}