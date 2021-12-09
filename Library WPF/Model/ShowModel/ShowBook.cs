using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Model.ShowModel
{
    public class ShowBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int Number_of_pages { get; set; }
        public int Year_of_publishing { get; set; }
        public decimal Cost_price { get; set; }
        public decimal Selling_price { get; set; }
        public bool Continuation { get; set; }
        public int Quantity { get; set; }
    }
}
