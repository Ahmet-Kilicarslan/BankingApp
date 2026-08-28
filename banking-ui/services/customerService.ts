import Customer from './models/customer';


const baseUrl = process.env.NEXT_PUBLIC_CUSTOMER_API_URL;



export async function getCustomerById(id:number):Promise<Customer> {
    
    const response = await fetch(`${baseUrl}/api/customer/${id}`);
    
    if(!response.ok){
        throw new Error("Failed to fetch customer: ",response.statusText);
    }
    
    
    return response.json();
    
    
    
    
}



export async function getAllCustomers():Promise<Customer[]> {
    
    const response = await fetch(`${baseUrl}/api/customer`);
    if(!response.ok){
        
        throw new Error("Failed to fetch all customers: ",response.statusText);
    }
    
    return response.json();
    
}


export async function createCustomer(  data: Omit<Customer, 'id'>):Promise<Customer> {
    
    const response = await fetch(`${baseUrl}/api/customer`, { 
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data),});
    
    if(!response.ok){
        throw new Error("Failed to create customer : " ,response.statusText);
    }
    
    return response.json();
    
}


