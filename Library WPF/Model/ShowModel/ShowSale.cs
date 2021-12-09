using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Model.ShowModel
{
    public class ShowSale
    {
        public int Id { get; set; }
        public string Book { get; set; }
        public string Customer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
