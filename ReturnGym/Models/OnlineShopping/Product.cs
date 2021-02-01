using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReturnGym.Models.OnlineShopping
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }

        public double Price { get; set; }
        public string Time { get; set; }
        public int Quantity { get; set; }
        public string CatName { get; set; }
        [ForeignKey("Categories")]
        public int CategoryID { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public string getCategory()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var gtName = (from m in db.Categories
                          where m.CategoryID == CategoryID
                          select m.CategoryName
                          ).FirstOrDefault();

            return gtName;
        }
    }
}