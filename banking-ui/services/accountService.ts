import Account from '../models/account';


import apiClient from "./handleResponse";

const baseUrl = process.env.NEXT_PUBLIC_ACCOUNT_API_URL;



export async function getAccountById(accountId:number):Promise<Account>{
 
 const url = `${baseUrl}/api/accounts/${accountId}`;
    
 return apiClient<Account>(url);
 
}


export async function getAllAccounts():Promise<Account[]>{
 
const url = `${baseUrl}/api/account`;


return apiclient<Account[]>(url);

}


export async function createAccount(data:Omit<Account, 'id'>):Promise<Account> {
    
 const url = `${baseUrl}/api/account`;
 
 return apiClient<Account>(url,{
     method: 'POST',
     body: JSON.stringify(data),
 });
 
 
}