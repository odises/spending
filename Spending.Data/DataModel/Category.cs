using System.Collections.Generic;

namespace Spending.Data.DataModel
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Transaction> Transactions { get; set; } 
    }
}
