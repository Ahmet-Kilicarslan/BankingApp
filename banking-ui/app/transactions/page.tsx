
import TransactionComponent from "../../components/transactionsComponent"
import {GetAllTransactions} from "../../services/transactionService"
export default  async function TransactionsPage() {

    const transactions = await GetAllTransactions();
    
    
    
    return (
        <div className="p-6">
            <h1 className="text-text-primary text-xl justify-self-center">Transactions</h1>
        
        <TransactionComponent transactions={transactions} />
            
        </div>
    );
}
    
    
