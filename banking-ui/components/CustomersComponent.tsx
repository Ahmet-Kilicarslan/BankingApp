'use client';

import {useState} from 'react';
import Customer from '../models/customer';
import Account from '../models/account';
import {getAccountsByCustomerId} from "../services/accountService"
import {Skull} from "lucide-react"; 
import AccountsPanel from "./AccountsPanel";


export default  function CustomersComponent({customers}: { customers: Customer[] }) {

    const [expandedId, setExpandedId] = useState<number | null>(null);
    const [cachedAccounts, setCachedAccounts] = useState<Record<number, Account[]>>({});


   async function  toggleCustomer(id: number) {
        setExpandedId((current) => (current === id ? null : id))

        if (!cachedAccounts[id]) {
            const accounts = await getAccountsByCustomerId(id);
            setCachedAccounts((prev) => ({...prev, [id]: accounts}));
        }

    }

    return (
        <div className="flex flex-col gap-3 -mb-5">
            <div className="flex justify-between items-center px-4 text-text-muted text-xs uppercase">
                <span className="w-1/4">Name</span>
                <span className="w-1/4">Email</span>
                <span className="w-1/4">Phone</span>
                <span className="w-1/4">Joined</span>
            </div>
            {customers.map((customer) => (
                <div
                    key={customer.id}
                    onClick={() => toggleCustomer(customer.id)}
                    className="w-full bg-surface border border-border rounded-lg
                p-4 cursor-pointer 
                hover:border-text-muted"
                >
                    <div className="grid grid-cols-4 items-center gap-4">
                        <p className="text-text-primary">{customer.name}</p>
                        <p className="text-text-primary">{customer.mail}</p>
                        <p className="text-text-primary">{customer.phone}</p>
                        <p className="text-text-primary">{new Date(customer.joinedAt).toLocaleString()}</p>
                    

                    </div>

                    {/*
                    {expandedId === customer.id && (
                        <div className="bg-surface border-x border-b border-border rounded-b-lg p-4">
                            <AccountsPanel accounts={cachedAccounts[customer.id]} />
                        </div>
                        
                    )}
                    */}
                    
                    
                </div>

            ))}

        </div>
    )

}