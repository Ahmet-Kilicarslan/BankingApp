

using TransactionApi.Models;
using TransactionApi.Models.DTOs;
using TransactionApi.Services.ApiClients;


namespace TransactionApi.Services.Strategies;




public class EftStrategy : ITransactionStrategy
{
    private readonly AccountApiClient _accountApiClient;
    
    private readonly CustomerApiClient _customerApiClient;
    
    
    public EftStrategy(AccountApiClient accountApiClient, CustomerApiClient customerApiClient){
        _accountApiClient = accountApiClient;
        _customerApiClient = customerApiClient;
        
    }

    public async Task<Transaction> Execute(TransactionInitiationDto transDto, AccountDetailsDto sourceAccount, AccountDetailsDto? destinationAccount)
    {

        if (sourceAccount.BankName == destinationAccount?.BankName &&
            sourceAccount.Currency != destinationAccount.Currency)
        {
            throw new InvalidOperationException($"Invalid Destination Account  " ) ;

        }

        var sourceOperationDto =  new AccountBalanceOperationDto(
            sourceAccount.AccountNo,
            transDto.Amount,  
            transDto.TransactionTypeId,
            BalanceOperationRole.Sender
        );

        var destinationOperationDto = new AccountBalanceOperationDto(
            destinationAccount.AccountNo,
            transDto.Amount,
            transDto.TransactionTypeId,
            BalanceOperationRole.Receiver
            
            );

        await _accountApiClient.UpdateAccountBalance(sourceOperationDto);

        await _accountApiClient.UpdateAccountBalance(destinationOperationDto);
        
        
        return new Transaction(sourceAccount.AccountNo, destinationAccount.AccountNo, transDto.Amount, transDto.TransactionTypeId);



    }
}