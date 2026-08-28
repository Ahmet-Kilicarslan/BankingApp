using System.ComponentModel.DataAnnotations;


namespace CustomerApi.Models;


public class Customer
{
    public int Id {get;set;}
    public required string Name {get;set;}

    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",ErrorMessage ="Invalid mail format")] 
    public  required string Mail {get;set;}

    [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Invalid phone number format")]
    public  required string Phone {get;set;}
    
    public DateTime JoinedAt {get;set;} = DateTime.UtcNow;


}