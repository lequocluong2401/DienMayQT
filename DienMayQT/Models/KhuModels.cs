using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DienMayQT.Models
{
    public class KhuModels
    {
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<ProductType> ProductType { get; set; }
        
    }
}