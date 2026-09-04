"use client";

import Account from "../models/account"

import {getAllAccounts} from "../services/accountService"


export default function accountsComponent({accounts}: { accounts: Account[] }) {

    return (

        <div className="flex flex-row gap-4 m-5">
            {accounts.map(account => (
                <div
                    key={account.id}
                    className="w-full bg-surface border border-border rounded-lg
                p-4 cursor-pointer 
                hover:border-text-muted"
                >
                    <div className="grid grid-rows-4 ">
                        <p className="text-text-primary">{account.accountNo}</p>
                        <p className="text-text-primary">{account.customerName}</p>
                        <p className="text-text-primary">{account.balance}</p>
                        <p className="text-text-primary">{new Date(account.openedAt).toLocaleString()}</p>

                    </div>

                </div>

            ))}


        </div>


    )


}