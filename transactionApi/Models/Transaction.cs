namespace TransactionApi.Models;

public class Transaction
{
    public int Id { get; set; }
    public int AccountNo { get; set; }

    public int DestinationAccountNo { get; set; }
    public decimal Amount { get; set; }

    public Status Status { get; set; } = Status.Initiated;
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    public int TransactionTypeId { get; set; }

    public TransactionType? TransactionType { get; set; }

    public Transaction(int accountNo, int destinationAccountNo, decimal amount, int transactionTypeId)
    {
        AccountNo = accountNo;
        DestinationAccountNo = destinationAccountNo;
        Amount = amount;
        TransactionTypeId = transactionTypeId;
    }

   
}