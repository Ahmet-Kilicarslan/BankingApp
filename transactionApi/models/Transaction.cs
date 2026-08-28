namespace TransactionApi.Models;

    public class Transaction{

    public int Id {get;set;}
    public  required int AccountId {get;set;}
    public required decimal Amount {get;set;}
    public DateTime TransactionDate {get;set;} = DateTime.UtcNow;
    public required  int TransactionTypeId { get; set; }
    public  TransactionType? TransactionType { get; set; }

    }