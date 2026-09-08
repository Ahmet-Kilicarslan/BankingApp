'use client';

import {useState} from 'react';
import Customer from '../models/customer';
import Account from '../models/account';
import {getAccountsByCustomerId} from "../services/accountService"
import {BanknoteArrrowUp} from "lucide-react";
import CreateTransactionPanel from "./CreateTransactionPanel"


export default function CustomersComponent({customers}: { customers: Customer[] }) {


    const [isTransactionPanelOpen, setIsTransactionPanelOpen] = useState<boolean>(false);
    const [selectedCustomer, setSelectedCustomer] = useState<Customer>();


    function openCreateTransactionPanel(customer: Customer) {
        setSelectedCustomer(customer);
        setIsTransactionPanelOpen(true);
    }

    return (
        <div className="relative">
            <div className={isTransactionPanelOpen ? "blur-sm pointer-events-none" : ""}>

                <div className="flex flex-col gap-3 -mb-5 ">


                    {customers.map((customer) => (
                        <div
                            key={customer.id}
                            className="w-full bg-surface border border-border rounded-lg
                p-4 cursor-pointer card-hover
                hover:border-text-muted"
                        >
                            <div className="grid grid-cols-5 items-center gap-4">
                                <p className="text-text-primary">{customer.name}</p>
                                <p className="text-text-primary">{customer.mail}</p>
                                <p className="text-text-primary">{customer.phone}</p>
                                <p className="text-text-primary">{new Date(customer.joinedAt).toLocaleDateString()}</p>
                                <button className="btn-game"
                                        onClick={() => openCreateTransactionPanel(customer)}>Transfer </button>


                            </div>

                        </div>

                    ))}

                </div>
            </div>

            {isTransactionPanelOpen && (
                <div className="fixed inset-0 flex items-center justify-center   z-50">
                    <CreateTransactionPanel
                        customer={selectedCustomer}
                        onClose={() => setIsTransactionPanelOpen(false)}
                    />
                </div>
            )}
        </div>
    )

}