using System;
using System.ComponentModel.DataAnnotations;

namespace Spending.Data.DataModel
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int DocumentId { get; set; }
        public string Description { get; set; }
        public long DepositValue { get; set; }
        public long WithdrawValue { get; set; }
        public long Balance { get; set; }
        public string Comment { get; set; }
        public int? CategoryId { get; set; }

        public string TerminalId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
