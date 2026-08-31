import Customer from './models/customer';
import apiClient from './handleResponse';

const baseUrl = process.env.NEXT_PUBLIC_CUSTOMER_API_URL;



export async function getCustomerById(id:number):Promise<Customer> {
    
    
    const Url = `${baseUrl}/api/customer/${id}`;
    
    return apiClient<Customer>(Url);
   
    
    
    
}



export async function getAllCustomers():Promise<Customer[]> {
    
const Url = `${baseUrl}/api/customer`;

return apiClient<Customer[]>(Url);
    
}


export async function createCustomer(  data: Omit<Customer, 'id'>):Promise<Customer> {
    
  const Url= `${baseUrl}/api/customer`;
    
  return apiClient<Customer>(Url,{
      method: 'POST',
      body: JSON.stringify(data),
  });
  
}


