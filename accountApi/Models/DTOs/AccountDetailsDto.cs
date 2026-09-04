namespace AccountApi.Models.DTOs;

public class AccountDetailsDto(int id, int accountNo, string customerName, decimal balance, DateTime openedAt)
{
    public int Id { get; } = id;
    public int AccountNo { get; } = accountNo;
    public string CustomerName { get; } = customerName;
    public decimal Balance { get; } = balance;
    public DateTime OpenedAt { get; } = openedAt;
}