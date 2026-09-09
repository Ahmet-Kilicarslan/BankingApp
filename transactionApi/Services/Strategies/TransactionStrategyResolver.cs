namespace TransactionApi.Services.Strategies;


public class TransactionStrategyResolver
{
    
    
    private  readonly IServiceProvider _serviceProvider;
    
    public TransactionStrategyResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITransactionStrategy Resolve(int  transactionTypeId)
    {

        return (TransactionTypeId)transactionTypeId switch
        {
            TransactionTypeId.Deposit => _serviceProvider.GetRequiredService<DepositStrategy>(),
            TransactionTypeId.Withdraw => _serviceProvider.GetRequiredService<WithdrawStrategy>(),
            TransactionTypeId.Virman => _serviceProvider.GetRequiredService<VirmanStrategy>(),
            TransactionTypeId.Havale => _serviceProvider.GetRequiredService<HavaleStrategy>(),
            TransactionTypeId.Fast => _serviceProvider.GetRequiredService<FastStrategy>(),
            TransactionTypeId.Eft => _serviceProvider.GetRequiredService<EftStrategy>(),
            _ => throw new InvalidOperationException(
                $"No strategy registered for transaction type id {transactionTypeId}.")



        };


    }
    
}