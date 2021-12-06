using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Model.ShowModelView
{
    public class ShowSalesView
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
