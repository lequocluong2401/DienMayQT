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
        public ProductType Category { get; set; }
        public Product Product1 { get; set; }
        public CashBill Cashbill { get; set; }
        public String search { get; set; }
        public int searchNumber { get; set; }
    } 
}