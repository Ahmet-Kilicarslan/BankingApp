import Account from '../models/account';


import {apiClient} from "./handleResponse";

const baseUrl = process.env.NEXT_PUBLIC_ACCOUNT_API_URL;



export async function getAccountById(accountId:number):Promise<Account>{
 
 const url = `${baseUrl}/api/account/${accountId}`;
    
 return apiClient<Account>(url);
 
}

export async function getAccountsByCustomerId(customerId:number):Promise<Account[]>{
    
    const url = `${baseUrl}/api/account/customer/${customerId}`;
    
    return apiClient<Account[]>(url);

}

export async function getAllAccounts():Promise<Account[]>{
 
const url = `${baseUrl}/api/account`;


return apiClient<Account[]>(url);

}


export async function createAccount(data:Omit<Account, 'id'>):Promise<Account> {
    
 const url = `${baseUrl}/api/account`;
 
 return apiClient<Account>(url,{
     method: 'POST',
     body: JSON.stringify(data),
 });
 
 
}