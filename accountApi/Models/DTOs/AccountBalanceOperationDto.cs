namespace AccountApi.Models.DTOs;

public enum BalanceOperationRole
{
    Sender = 1,
    Receiver = 2
}
public class AccountBalanceOperationDto
{

    public required int AccountNo { get; set; }
    
    public required decimal Amount { get; set; }
    
    public required int TransactionTypeId { get; set; }

    public BalanceOperationRole Role { get; set; }

} 