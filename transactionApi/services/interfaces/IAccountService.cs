using TransactionApi.Models;

namespace TransactionApi.Services.Interfaces;


public interface IAccountService{

Task<Account?> GetAccountById(int Id);

task<Account> CreateAccount(Account account);

}