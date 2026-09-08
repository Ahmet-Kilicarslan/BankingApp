

namespace TransactionApi.Models.DTOs;

public class TransactionInitiationDto
{
    
    
    
    public int AccountNo { get; set; }
    public int? DestinationAccountNo { get; set; }
    public decimal Amount { get; set; }
    public int TransactionTypeId { get; set; }

    public TransactionInitiationDto(int accountNo, int? destinationAccountNo, decimal amount, int transactionTypeId)
    {
        AccountNo = accountNo;
        DestinationAccountNo = destinationAccountNo;
        Amount = amount;
        TransactionTypeId = transactionTypeId;
    }
}
