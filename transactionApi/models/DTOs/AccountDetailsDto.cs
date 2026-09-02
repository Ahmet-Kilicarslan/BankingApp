
namespace TransactionApi.Models.DTOs;

public class AccountDetailsDto
{
    public int Id { get; set; }
    public required int AccountNo { get; set; }
    public required int CustomerId { get; set; }
}

