using System;
using Spending.Data.DataModel;

namespace Spending.Web.Controllers
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int DocumentId { get; set; }
        public string Description { get; set; }
        public long DepositValue { get; set; }
        public long WithdrawValue { get; set; }
        public long Balance { get; set; }
        public string Comment { get; set; }
        public string TerminalId { get; set; }
        public string TerminalTitle { get; set; }
        public string CardNumber { get; set; }
        public bool IsTransfer { get; set; }
        public virtual Category Category { get; set; }
    }
}