namespace AccountApi.Models;


public class Account
{

    public int Id { get; set; }
    public required int AccountNo { get; set; }
    public required int CustomerId { get; set; }
    public required decimal Balance { get; set; }
    public DateTime OpenedAt { get; set; } = DateTime.UtcNow;

}