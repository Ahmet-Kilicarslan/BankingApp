'use client';

import {ArrowDownLeft, ArrowUpRight} from 'lucide-react';
import Image from 'next/image';
import {useEffect, useState} from 'react';
import {BanknoteArrowUp,BanknoteArrowDown} from "lucide-react";
import {Transaction} from '../models/transaction';
import Account from '../models/account';
import {getAccountByAccountNoFast} from '../services/accountService';
import {getBankLogo} from "../services/utils"


export default function TransactionsComponent({transactions}: { transactions: Transaction[] }) {

    const [transactionAccounts, setTransactionAccounts] = useState<Record<number, {
        source: Account;
        destination: Account | null;
    }>>({});

    useEffect(() => {

        async function loadDestinationAccounts() {


            const results = await Promise.all(
                transactions.map(async transaction => {

                    const [sourceAccount, destinationAccount] = await Promise.all(
                        [await getAccountByAccountNoFast(transaction.accountNo), await getAccountByAccountNoFast(transaction.destinationAccountNo)]
                    )

                    return {
                        transactionId: transaction.id,
                        source: sourceAccount,
                        destination: destinationAccount,

                    }

                })
            )
            const accountMap: Record<
                number,
                {
                    source: Account | null;
                    destination: Account | null;
                }
            > = {};

            results.forEach(
                ({transactionId, source, destination}) => {
                    accountMap[transactionId] = {
                        source,
                        destination
                    };
                }
            );

            setTransactionAccounts(accountMap);


        }

        loadDestinationAccounts();
    }, [transactions]);

    return (
        <div className="flex flex-col gap-4 m-5">

            {transactions.map(transaction => {

             

                return (
                    <div
                        key={transaction.id}
                        className="w-full bg-surface border border-border rounded-lg
                     p-4 cursor-pointer transition-colors card-hover
                     hover:border-text-muted flex flex-col gap-4">



                        {!transaction.transactionType.requiresDestinationAccount
                            ? <div className="flex flex-row justify-between items-center">
                                <div className="text-xl font-bold text-text-primary">
                                {transaction.transactionType.name == "Deposit"
                                    ? <div className="bg-wordle-correct"><BanknoteArrowUp/> {transaction.amount}</div>
                                    : <div className="bg-danger"><BanknoteArrowDown/>{transaction.amount}</div>

                                }
                            </div>
                                <div className="flex flex-row">
                                    <Image
                                        src={getBankLogo(accounts.source.bankName)}
                                        alt={`${accounts.source.bankName} logo`}
                                        width={32}
                                        height={16}
                                        className="object-contain h-6 w-auto bg-text-primary"
                                    />
                                    <p className="text-2xl font-bold text-text-primary">
                                        {accounts.source.customerName}</p>

                                    <p className="text-sm text-text-muted">
                                        {accounts.source.accountNo}</p>


                                </div>
                            </div>
                            : <div className="flex flex-col">
                                <div className="flex flex-row">
                                    <Image
                                        src={getBankLogo(accounts.source.bankName)}
                                        alt={`${accounts.source.bankName} logo`}
                                        width={32}
                                        height={16}
                                        className="object-contain h-6 w-auto bg-text-primary"
                                    />
                                    <p className="text-2xl font-bold text-text-primary">
                                        {accounts.source.customerName}</p>

                                    <p className="text-sm text-text-muted">
                                        {accounts.source.accountNo}</p>
                                    <div className="text-xl font-bold text-text-primary bg-wordle-danger"><BanknoteArrowDown/>{transaction.amount}</div>


                                </div>
                                <div className="flex flex-row">
                                    <Image
                                        src={getBankLogo(accounts.destination.bankName)}
                                        alt={`${accounts.destination.bankName} logo`}
                                        width={60}
                                        height={12}
                                        className="object-contain h-6 w-auto"
                                    />
                                    <p className="text-2xl font-bold text-text-primary">
                                        {accounts.destination.customerName}</p>

                                    <p className="text-sm text-text-muted">
                                        {accounts.destination.accountNo}</p>
                                    <div className=" text-xl font-bold text-text-primary bg-wordle-correct"><BanknoteArrowUp/> {transaction.amount}</div>
                                </div>
                            </div>
                        }

                    </div>
                );
            })}


        </div>


    );


}