import Transaction from "../models/transaction";

import {apiClient} from "./handleResponse";


const baseUrl = process.env.NEXT_PUBLIC_TRANSACTION_API_URL;



export async function getTransactionById(id:number):Promise<Transaction>  {
    
    const Url = `${baseUrl}/api/transaction/${id}`;
    
    return apiClient<Transaction>(Url);
    
}

export async function GetAllTransactions():Promise<Transaction[]> {
    
    const Url = `${baseUrl}/api/transaction`;
    
    return apiClient<Transaction[]>(Url);
    
    
}

export async function createTransaction(data:Omit<Transaction,"id">):Promise<Transaction>  {
    
    const Url = `×${baseUrl}/api/transaction`;
    return apiClient<Transaction>(Url,{
        method:'POST',
        body: JSON.stringify(data),
    })
    
    
}