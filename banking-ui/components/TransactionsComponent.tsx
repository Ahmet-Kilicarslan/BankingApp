'use client';

import { ArrowDownLeft, ArrowUpRight } from 'lucide-react';
import Transaction from "../models/transaction";

export default function TransactionsComponent({ transactions }: { transactions: Transaction[] }) {
    return (
        <div className="flex flex-col gap-2 m-4">
            {transactions.map((transaction: Transaction) => {
                const isDeposit = transaction.transactionType === "Deposit";

                return (
                    <div
                        key={transaction.id}
                        className="flex items-center justify-between bg-surface border border-border rounded-lg
                        p-4 cursor-pointer transition-colors
                        hover:border-text-muted"
                    >
                        <div className="flex items-center gap-3">
                            <div className={`rounded-full p-2 ${isDeposit ? "bg-emerald-500/10" : "bg-rose-500/10"}`}>
                                {isDeposit
                                    ? <ArrowDownLeft className="text-emerald-500" size={18} />
                                    : <ArrowUpRight className="text-rose-500" size={18} />}
                            </div>
                            <div className="flex flex-col gap-1">
                                <p className="text-text-primary">{transaction.customerName}</p>
                                <p className="text-text-muted text-sm">
                                    Account {transaction.accountNo} · {transaction.transactionType}
                                </p>
                            </div>
                        </div>

                        <div className="flex flex-col items-end gap-1">
                            <p className={`font-semibold ${isDeposit ? "text-emerald-500" : "text-rose-500"}`}>
                                {isDeposit ? "+" : "−"}{transaction.amount}
                            </p>
                            <p className="text-text-muted text-sm">
                                {new Date(transaction.transactionDate).toLocaleString()}
                            </p>
                        </div>
                    </div>
                );
            })}
        </div>
    );
}