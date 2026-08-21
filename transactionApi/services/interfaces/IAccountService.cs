using TransactionApi.Models;

namespace TransactionApi.Services.Interfaces;


public interface IAccountService{

Task<Account?> GetAccountById(int Id);

Task<Account> CreateAccount(Account account);

}