using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomerApi.Models;





public class Customer
{
    public int Id {get;set;}
    public required string Name {get;set;}

    [EmailAddress(ErrorMessage = "Invalid mail format")]
    public  required string Mail {get;set;}

    [RegularExpression( @"^\+?[0-9\s().-]{7,20}$", ErrorMessage = "Invalid phone number format")]
    public  required string Phone {get; set;}
    
    public DateTime JoinedAt {get;set;} = DateTime.UtcNow;


}