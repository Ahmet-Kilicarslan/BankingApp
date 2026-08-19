namespace TransactionApi.Models;

    public class Transaction{

    public int Id {get;set;}
    public int AccountId {get;set;}
    public required Account Account {get;set;}
    public decimal Amount {get;set;}
    public DateTime TransactionDate {get;set;} = DateTime.UtcNow;
    public   int TransactionTypeId { get; set; }
    public required TransactionType TransactionType { get; set; }

    }