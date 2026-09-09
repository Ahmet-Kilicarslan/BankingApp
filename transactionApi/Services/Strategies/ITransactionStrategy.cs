using TransactionApi.Models;
using TransactionApi.Models.DTOs;
namespace TransactionApi.Services.Strategies;

public interface ITransactionStrategy
{
    Task<Transaction> Execute(TransactionInitiationDto transDto, AccountDetailsDto sourceAccount, AccountDetailsDto? destinationAccount);
}