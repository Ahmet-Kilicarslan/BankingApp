using TransactionApi.Models;
using TransactionApi.Repositories.Interfaces;
using TransactionApi.Services.Interfaces;
using TransactionApi.Models.DTOs;
using TransactionApi.Services.ApiClients;
using TransactionApi.Services.Strategies;

namespace TransactionApi.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    
    private readonly AccountApiClient _accountApiClient;

    private readonly CustomerApiClient _customerApiClient;


    private readonly TransactionStrategyResolver _transactionStrategyResolver;

    public TransactionService(
        ITransactionRepository transactionRepository,
        AccountApiClient accountApiClient,
        CustomerApiClient customerApiClient,
        TransactionStrategyResolver transactionStrategyResolver)
    {
        _transactionRepository = transactionRepository;
        _accountApiClient = accountApiClient;
        _customerApiClient = customerApiClient;
        _transactionStrategyResolver = transactionStrategyResolver;
    }


    public async Task<Transaction> CreateTransaction(TransactionInitiationDto transactionDto)
    {
        var sourceAccount = await _accountApiClient.GetAccountDetailsByAccountNo(transactionDto.AccountNo);


        AccountDetailsDto? destinationAccount = transactionDto.DestinationAccountNo.HasValue
            ? await _accountApiClient.GetAccountDetailsByAccountNo(transactionDto.DestinationAccountNo.Value)
            : null;

        var strategy = _transactionStrategyResolver.Resolve(transactionDto.TransactionTypeId);

        var newTransaction = await strategy.Execute(transactionDto, sourceAccount, destinationAccount);

        await _transactionRepository.CreateTransaction(newTransaction);
        await _transactionRepository.SaveChangesAsync();

        return newTransaction;
    }

    public async Task<Transaction?> GetTransactionbyId(int Id)
    {
        return await _transactionRepository.GetTransactionById(Id);
    }


    public async Task<List<TransactionDetailsDto>> GetAllTransactionDetails()
    {
        var transList = await _transactionRepository.GetAllTransactions();

        List<TransactionDetailsDto> transDetailsList = new List<TransactionDetailsDto>();

        foreach (var item in transList)
        {
            var accountDetails = await _accountApiClient.GetAccountDetailsByAccountNo(item.AccountNo);
            var customerDetails = await _customerApiClient.GetCustomerDetailsByCustomerId(accountDetails.CustomerId);
            var trans = await GetTransactionType(item.TransactionTypeId);


            var transDetail = new TransactionDetailsDto
            {
                Id = item.Id,
                CustomerName = customerDetails.Name,
                AccountNo = accountDetails.AccountNo,
                DestinationAccountNo = item.DestinationAccountNo,
                TransactionType = trans,
                Amount = item.Amount,
                TransactionDate = item.TransactionDate
            };

            transDetailsList.Add(transDetail);
        }


        return transDetailsList;
    }


    public async Task<List<TransactionType>> GetAllTransactionTypes()
    {
        return await _transactionRepository.GetAllTransactionTypes();
    }

    private async Task<TransactionType> GetTransactionType(int transactionTypeId)
    {
        return await _transactionRepository.GetTransactionType(transactionTypeId);
    }

    
}