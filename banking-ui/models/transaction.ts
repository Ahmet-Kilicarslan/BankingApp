

export  interface Transaction {
    id:number;
    customerName: string;
    accountNo: number;
    destinationAccountNo?: number;
    transactionType: TransactionType;
    amount: number;
    transactionDate: Date;
    
    
}

export interface TransactionType{
    id:number;
    name: string;
    isInterBank: boolean;
    requiresDestinationAccount: boolean;
    
    
}

export interface TransactionInitiationDto {

    accountNo: number;
    destinationAccountNo?: number;
    amount: number;
    transactionTypeId: number;
    
    
    
}