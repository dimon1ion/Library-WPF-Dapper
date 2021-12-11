using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
        public int Number_of_pages { get; set; }
        public int Year_of_publishing { get; set; }
        public decimal Cost_price { get; set; }
        public decimal Selling_price { get; set; }
        public bool Continuation { get; set; }
        public int Quantity { get; set; }

    }
}
