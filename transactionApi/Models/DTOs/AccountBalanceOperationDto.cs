namespace TransactionApi.Models.DTOs;
public enum BalanceOperationRole
{
    Sender = 1,
    Receiver = 2
}
public class AccountBalanceOperationDto
{
    

    public  int AccountNo { get; set; }
    
    public  decimal Amount { get; set; }
    
    public  int TransactionTypeId { get; set; }

    public BalanceOperationRole Role { get; set; }

    public AccountBalanceOperationDto(int accountNo, decimal amount, int transactionTypeId, BalanceOperationRole role)
    {
        AccountNo = accountNo;
        Amount = amount;
        TransactionTypeId = transactionTypeId;
        Role = role;
    }
}