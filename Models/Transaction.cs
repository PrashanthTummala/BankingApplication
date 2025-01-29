namespace BankingApplication.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public DateTime TransactionTime { get; set; }
        public decimal Amount { get; set; }
        public decimal FromAccountBalance { get; set; }
        public decimal ToAccountBalance { get; set; }
    }
}
