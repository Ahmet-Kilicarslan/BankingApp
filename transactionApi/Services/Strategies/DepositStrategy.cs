using TransactionApi.Models;
using TransactionApi.Models.DTOs;
using TransactionApi.Services.ApiClients;

namespace TransactionApi.Services.Strategies;


public class DepositStrategy : ITransactionStrategy
{

    private readonly AccountApiClient _accountApiClient;
    private readonly CustomerApiClient _customerApiClient;


    public DepositStrategy(AccountApiClient accountApiClient, CustomerApiClient customerApiClient)
    {
        _accountApiClient = accountApiClient;
        _customerApiClient = customerApiClient;
    }
    
    public async Task<Transaction> Execute(TransactionInitiationDto transDto, AccountDetailsDto sourceAccount, AccountDetailsDto? destinationAccount)
    {

        var operationDto = new AccountBalanceOperationDto(
            sourceAccount.AccountNo,
            transDto.Amount,
            transDto.TransactionTypeId,
            BalanceOperationRole.Receiver
            );
        
        await _accountApiClient.UpdateAccountBalance(operationDto);
        
        return new Transaction(sourceAccount.AccountNo, null, transDto.Amount, transDto.TransactionTypeId);



    }
    
    
    
}
