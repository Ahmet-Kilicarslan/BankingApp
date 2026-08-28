namespace TransactionApi.Models.DTOs;

public class AccountBalanceOperationDto
{

    public required int AccountId { get; set; }
    public required decimal Amount { get; set; }
    public required int TransactionTypeId { get; set; }


}