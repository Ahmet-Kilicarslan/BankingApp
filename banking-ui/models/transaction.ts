

export  interface Transaction {
    id:number;
    customerName: string;
    accountNo: number;
    destinationAccountNo: number;
    transactionType: string;
    amount: number;
    transactionDate: Date;
    
    
}

export interface TransactionInitiationDto {

    accountNo: number;
    destinationAccountNo: number;
    amount: number;
    transactionTypeId: number;
    
    
    
}