import CustomersComponent from "../../components/customersComponent";
import {getAllCustomers} from "../../services/customerService";

import { Customer } from '../../models/customer';
export default async function CustomersPage() {
  
  const customers:Customer[] = await getAllCustomers();
  
  
    return (
        <div className="p-6">
            <h1 className="text-text-primary text-xl justify-self-center mb-5">Customers</h1>
 
                <CustomersComponent  customers={customers} />  
            
        </div>
    );
}