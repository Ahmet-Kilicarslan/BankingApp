using TransactionApi.Models;
using TransactionApi.Models.DTOs;

namespace TransactionApi.Services.Interfaces;



public interface ITransactionService{

Task<Transaction?> GetTransactionbyId(int Id);

Task<Transaction> CreateTransaction(AccountBalanceOperationDto dto);


}