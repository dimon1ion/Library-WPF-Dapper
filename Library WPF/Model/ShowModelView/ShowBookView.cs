using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Model.ShowModelView
{
    public class ShowBookView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public Genre Genre { get; set; }
        public int PublisherName { get; set; }
        public int Number_of_pages { get; set; }
        public int Year_of_publishing { get; set; }
        public decimal Cost_price { get; set; }
        public decimal Selling_price { get; set; }
        public bool Continuation { get; set; }
        public int Quantity { get; set; }
    }
}
