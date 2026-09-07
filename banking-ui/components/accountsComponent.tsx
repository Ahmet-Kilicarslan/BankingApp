"use client";
import Image from "next/image";
import Account from "../models/account"

import {getAllAccounts} from "../services/accountService"


export default function accountsComponent({accounts}: { accounts: Account[] }) {


    function getBankLogo(bankName: string) {
        switch (bankName) {
            case "AkBank":
                return "./logos/Akbank_logo_2025.svg"
            case "Garanti":
                return "./logos/Garanti_Bankasi_Logo.svg"
            case "Vakıfbank":
                return "./logos/Vakifbank-logo.svg"
            case "Ziraat":
                return "./logos/Ziraat_Bankasi_logo.svg"
            default:
                return "./logos/TCMB_Logo.svg"

        }

    }


    return (
        <div className="flex flex-row flex-wrap gap-4 m-5">
            {accounts.map(account => (
                <div
                    key={account.id}
                    className="w-72 bg-surface border border-border rounded-lg
                        p-4 cursor-pointer transition-colors card-hover
                        hover:border-text-muted flex flex-col gap-4"
                >
                    <div className="bg-text-primary p-2 rounded-md flex items-center gap-3">
                        <Image
                            src={getBankLogo(account.bankName)}
                            alt={`${account.bankName} logo`}
                            width={120}
                            height={24}
                            className="object-contain h-6 w-auto"
                        />
                    </div>

                    <p className="text-2xl font-bold text-text-primary">
                        {account.balance.toLocaleString()} {account.currency}
                    </p>

                    <div className="flex flex-col gap-1 text-l text-text-primary">
                        <p>{account.accountNo}</p>
                        <p>{account.customerName}</p>
                    </div>
                    <div className="flex flex-col gap-1 text-sm text-text-muted">
                        <p>{new Date(account.openedAt).toLocaleDateString()}</p>
                    </div>
                </div>
            ))}
        </div>
    );


}