namespace AccountApi.Models.DTOs;


public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Phone { get; set; }
    public DateTime JoinedAt {get;set;} = DateTime.UtcNow;

    
    
}