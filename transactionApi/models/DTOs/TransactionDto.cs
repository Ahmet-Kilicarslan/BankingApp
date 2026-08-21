namespace TransactionApi.Models.DTOs;

public class TransactionDto {

    public required int AccountId { get; set; }
    public required decimal Amount { get; set; }

    public required int TransactionTypeId { get; set; }



}