namespace TransactionApi.Models;


public class TransactionType
{
    public int Id { get; set; }
    public  string Name { get; set; }
    
    public bool? IsInterBank { get; set; }
    
    public bool? RequiresDestinationAccount { get; set; }
    
}