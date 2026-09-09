
namespace TransactionApi.Models.DTOs;

public class AccountDetailsDto(
    int id,
    int accountNo,
    int  customerId,
    string customerName,
    decimal balance,
    string currency,
    string bankName,
    DateTime openedAt)
{
    public int Id { get; } = id;
    public int AccountNo { get; } = accountNo;
    public int CustomerId { get; } = customerId;
    public string? CustomerName { get; set; } = customerName;
    public string Currency { get; } = currency;
    public decimal Balance { get; } = balance;
    public string BankName { get; } = bankName;
    public DateTime OpenedAt { get; } = openedAt;
}

