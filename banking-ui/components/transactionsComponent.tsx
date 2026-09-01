'use client';

import {useState} from 'react';
import Transaction from "../models/transaction";


export default function transactionsComponent({transactions}: { transactions: Transtion[] }) {


    function transactionTypeName(id: number) {


        switch (id) {
            case  1:
                return "Deposit"
                break;
            case 2:
                return "Withdraw"
               break;
    
        }

    }


    return (
        <div className="flex flex-row gap-3 m-4">
            {transactions.map((transaction: Transaction) => (
                <div
                    key={transaction.id}
                    className="w-full bg-surface border border-border rounded-lg
                p-4 cursor-pointer 
                hover:border-text-muted"
                >
                    <div className="flex flex-col gap-2">
                        <p className="text-text-primary">Customer Id: {transaction.accountId}</p>
                        <p className="text-text-primary">Amount: {transaction.amount}</p>
                        <p className="text-text-primary">Transaction Type: {transactionTypeName(transaction.transactionTypeId)}</p>
                        <p className="text-text-primary">Date: {new Date(transaction.transactionDate).toLocaleString()}</p>
                    </div>


                </div>
            ))}
        </div>
    )


}




