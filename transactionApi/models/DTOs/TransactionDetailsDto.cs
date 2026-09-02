namespace TransactionApi.Models.DTOs;

public class TransactionDetailsDto()
{
    

    public string CustomerName { get; set; }
    public int AccountNo { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; } =  DateTime.UtcNow;
    
}


