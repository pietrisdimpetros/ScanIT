using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScanIT.ViewModels
{
    public class ProductsDetailsVM
    {
        public IEnumerable<Product> Products { get; set; }


        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Dietary> Dietaries { get; set; }

        public IEnumerable<ProductDietary> ProductDietaries { get; set; }
    }
}