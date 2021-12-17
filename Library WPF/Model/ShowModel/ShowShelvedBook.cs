using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Model.ShowModel
{
    public class ShowShelvedBook
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string CustomerName { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
    }
}
