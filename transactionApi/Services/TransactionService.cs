using TransactionApi.Models;
using TransactionApi.Repositories.Interfaces;
using TransactionApi.Services.Interfaces;
using TransactionApi.Models.DTOs;

namespace TransactionApi.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    private readonly IHttpClientFactory _httpClientFactory;

    private readonly TokenService _tokenService;


    public TransactionService(ITransactionRepository transactionRepository, IHttpClientFactory httpClientFactory,
        TokenService tokenService)
    {
        _transactionRepository = transactionRepository;
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }


    public async Task<Transaction?> GetTransactionbyId(int Id)
    {
        return await _transactionRepository.GetTransactionById(Id);
    }


    public async Task<Transaction> CreateTransaction(TransactionInitiationDto transactionDto)
    {
        var transactionType = await _transactionRepository.GetTransactionType(transactionDto.TransactionTypeId);

        var sourceAccount = await GetAccountDetailsByAccountNo(transactionDto.AccountNo);

        
        
        var destinationAccount = await GetAccountDetailsByAccountNo(transactionDto.DestinationAccountNo ?? 0);


        if (transactionType.RequiresDestinationAccount == false)
        {
            if (transactionType.Name == "Withdraw" && sourceAccount.Balance < transactionDto.Amount)
            {
                throw new InvalidOperationException("Not enough balance.");
            }


            var accountBalanceOperationDto = new AccountBalanceOperationDto(
                sourceAccount.AccountNo,
                transactionDto.Amount,
                transactionDto.TransactionTypeId,
                BalanceOperationRole.Receiver
            );


            await UpdateAccountBalance(accountBalanceOperationDto);
        }

        if (transactionType.RequiresDestinationAccount == true)
        {
            if (transactionType.IsInterBank == true)
            {
                if (sourceAccount.Balance < transactionDto.Amount)
                {
                    throw new InvalidOperationException("Not enough balance.");
                }


                var senderAccountBalanceOperationDto = new AccountBalanceOperationDto(
                    sourceAccount.AccountNo,
                    transactionDto.Amount,
                    transactionDto.TransactionTypeId,
                    BalanceOperationRole.Sender
                );

                await UpdateAccountBalance(senderAccountBalanceOperationDto);
                var receiverAccountBalanceOperationDto = new AccountBalanceOperationDto(
                    destinationAccount.AccountNo,
                    transactionDto.Amount,
                    transactionDto.TransactionTypeId,
                    BalanceOperationRole.Receiver
                );

                await UpdateAccountBalance(receiverAccountBalanceOperationDto);
            }
            else if (transactionType.IsInterBank == false)
            {
                if (sourceAccount.Balance < transactionDto.Amount)
                {
                    throw new InvalidOperationException("Not enough balance.");
                }

                var senderAccountBalanceOperationDto = new AccountBalanceOperationDto(
                    sourceAccount.AccountNo,
                    transactionDto.Amount,
                    transactionDto.TransactionTypeId,
                    BalanceOperationRole.Sender
                );


                await UpdateAccountBalance(senderAccountBalanceOperationDto);
                var receiverAccountBalanceOperationDto = new AccountBalanceOperationDto(
                    destinationAccount.AccountNo,
                    transactionDto.Amount,
                    transactionDto.TransactionTypeId,
                    BalanceOperationRole.Receiver
                );

                await UpdateAccountBalance(receiverAccountBalanceOperationDto);
            }
        }

        var newTransaction = new Transaction(
            sourceAccount.AccountNo,
            destinationAccount.AccountNo,
            transactionDto.Amount,
            transactionDto.TransactionTypeId
        );

        await _transactionRepository.CreateTransaction(newTransaction);
        await _transactionRepository.SaveChangesAsync();

        return newTransaction;
    }


    public async Task<List<TransactionDetailsDto>> GetAllTransactionDetails()
    {
        var transList = await _transactionRepository.GetAllTransactions();

        List<TransactionDetailsDto> transDetailsList = new List<TransactionDetailsDto>();

        foreach (var item in transList)
        {
            var accountDetails = await GetAccountDetailsByAccountNo(item.AccountNo);
            var customerDetails = await GetCustomerDetailsByCustomerId(accountDetails.CustomerId);
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

    private async Task<bool> UpdateAccountBalance(AccountBalanceOperationDto accountBalanceOperationDto)
    {
        var httpClient = _httpClientFactory.CreateClient("AccountApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


        var response = await httpClient.PostAsJsonAsync("/api/Account/Update-Balance", accountBalanceOperationDto);

        return response.IsSuccessStatusCode;
    }


    private async Task<AccountDetailsDto> GetAccountDetailsByAccountNo(int accountNo)
    {
        var httpClient = _httpClientFactory.CreateClient("AccountApi");
        var token = await _tokenService.GetTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"/api/account/by-account-no/{accountNo}");

        return await response.Content.ReadFromJsonAsync<AccountDetailsDto>()
               ?? throw new InvalidOperationException($"Account {accountNo} returned an empty response.");
    }


    private async Task<CustomerDetailsDto> GetCustomerDetailsByCustomerId(int customerId)
    {
        var httpClient = _httpClientFactory.CreateClient("CustomerApi");
        var token = await _tokenService.GetTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"/api/customer/{customerId}");

        return await response.Content.ReadFromJsonAsync<CustomerDetailsDto>()
               ?? throw new InvalidOperationException($"Account {customerId} returned an empty response.");
    }
}