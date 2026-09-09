
namespace TransactionApi.Models.DTOs;

public class AccountDetailsDto
{
    public int Id { get; set; }
    public  int AccountNo { get; set; }
    public  int CustomerId { get; set; }
    
    
    public decimal Balance { get; set; }
}

