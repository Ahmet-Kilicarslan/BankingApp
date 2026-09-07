namespace TransactionApi.Models.DTOs;

public class TransactionDetailsDto()
{
    

    public int Id { get; set; }
    public string CustomerName { get; set; }
    public int AccountNo { get; set; }
    
    public int DestinationAccountNo { get; set; }
    
    public string TransactionTypeName { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; } =  DateTime.UtcNow;
    
}


