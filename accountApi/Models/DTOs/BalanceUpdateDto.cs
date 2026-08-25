namespace AccountApi.Models.DTOs;

public class BalanceUpdateDto()
{


    public required int AccountId { get; set; }
    public required decimal Amount { get; set; }
    public required int TransactionTypeId { get; set; }



}