using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class HomeViewModel
    {
        internal static HomeIndexViewModel GetIndex(DatabaseEntities db)
        {
            return new HomeIndexViewModel()
            {
                Products = Product.GetList(db)
            };
        }
    }
    public class HomeIndexViewModel
    {
        public List<Product> Products { get; set; }
    }
}