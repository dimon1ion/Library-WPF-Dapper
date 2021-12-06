using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Model
{
    public class Sale
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
