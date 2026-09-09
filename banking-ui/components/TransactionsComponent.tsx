'use client';

import {ArrowDownLeft, ArrowUpRight} from 'lucide-react';
import Image from 'next/image';
import {useEffect, useState} from 'react';

import {Transaction} from '../models/transaction';
import Account from '../models/account';
import {getAccountByAccountNoFast} from '../services/accountService';

export default function TransactionsComponent({transactions}: { transactions: Transaction[]; }) {
    
    const [transactionAccounts, setTransactionAccounts] = useState<Record<number, { source: Account | null; destination: Account | null; }>>({});
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

    useEffect(() => {
        async function fetchTransactionAccounts() {
            const destinationTransactions = transactions.filter(
                transaction =>
                    transaction.transactionType.requiresDestinationAccount &&
                    transaction.destinationAccountNo != null
            );

            const results = await Promise.all(
                destinationTransactions.map(async transaction => {
                    try {
                        const [sourceAccount, destinationAccount] =
                            await Promise.all([
                                getAccountByAccountNoFast(
                                    transaction.accountNo
                                ),
                                getAccountByAccountNoFast(
                                    transaction.destinationAccountNo!
                                )
                            ]);

                        return {
                            transactionId: transaction.id,
                            source: sourceAccount,
                            destination: destinationAccount
                        };
                    } catch (err) {
                        console.error(
                            `Failed to fetch accounts for transaction ${transaction.id}:`,
                            err
                        );

                        return {
                            transactionId: transaction.id,
                            source: null,
                            destination: null
                        };
                    }
                })
            );

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

        fetchTransactionAccounts();
    }, [transactions]);

    return (
        <div className="flex flex-col gap-2 m-4">
            {transactions.map((transaction: Transaction) => {
                const isDestinationRequired =
                    transaction.transactionType.requiresDestinationAccount;

                const isDeposit =
                    transaction.transactionType.name === 'Deposit';

                const accounts = transactionAccounts[transaction.id];

                const sourceAccount = accounts?.source;
                const destinationAccount = accounts?.destination;

                return (
                    <div
                        key={transaction.id}
                        className="
                            bg-surface
                            border border-border
                            rounded-lg
                            p-4
                            transition-colors
                            card-hover
                            hover:border-text-muted
                        "
                    >
                        {/* Header */}
                        <div className="flex items-center justify-between mb-5">
                            <div className="flex items-center gap-3">
                                <div
                                    className={`rounded-full p-2 ${
                                        isDeposit
                                            ? 'bg-emerald-500/10'
                                            : 'bg-rose-500/10'
                                    }`}
                                >
                                    {isDeposit ? (
                                        <ArrowDownLeft
                                            className="text-emerald-500"
                                            size={18}
                                        />
                                    ) : (
                                        <ArrowUpRight
                                            className="text-rose-500"
                                            size={18}
                                        />
                                    )}
                                </div>

                                <div>
                                    <p className="text-text-primary font-medium">
                                        {transaction.transactionType.name}
                                    </p>

                                    <p className="text-text-muted text-xs">
                                        {new Date(
                                            transaction.transactionDate
                                        ).toLocaleString()}
                                    </p>
                                </div>
                            </div>
                        </div>

                        {/* Transfer */}
                        {isDestinationRequired ? (
                            <div className="flex items-center gap-4">

                                {/* FROM */}
                                <div className="flex items-center gap-3 min-w-0 flex-1">

                                    {/* Source bank logo */}
                                    {sourceAccount?.bankName && (
                                        <div className="
                                            shrink-0
                                            w-10
                                            h-10
                                            rounded-lg
                                            bg-text-primary
                                            p-2
                                            flex
                                            items-center
                                            justify-center
                                        ">
                                            <Image
                                                src={getBankLogo(
                                                    sourceAccount.bankName
                                                )}
                                                alt={`${sourceAccount.bankName} logo`}
                                                width={80}
                                                height={24}
                                                className="
                                                    object-contain
                                                    max-h-6
                                                    w-auto
                                                "
                                            />
                                        </div>
                                    )}

                                    {/* Source account information */}
                                    <div className="flex flex-col gap-1 min-w-0">
                                        <p className="
                                            text-xs
                                            uppercase
                                            tracking-wide
                                            text-text-muted
                                        ">
                                            From
                                        </p>

                                        <p className="
                                            text-text-primary
                                            font-medium
                                            truncate
                                        ">
                                            {transaction.customerName}
                                        </p>

                                        <p className="text-text-muted text-sm">
                                            Account {transaction.accountNo}
                                        </p>
                                    </div>
                                </div>

                                {/* CENTER */}
                                <div className="
                                    flex
                                    flex-col
                                    items-center
                                    justify-center
                                    shrink-0
                                    px-2
                                ">
                                    <p
                                        className={`font-semibold text-lg whitespace-nowrap 
                                        ${
                                             isDeposit
                                                ? 'text-emerald-500'
                                                : 'text-rose-500'
                                        }
`}
                                    >
                                        {isDeposit ? '+' : '−'}
                                        {transaction.amount.toLocaleString()}
                                    </p>

                                    <ArrowUpRight
                                        size={18}
                                        className="text-text-muted mt-1"
                                    />
                                </div>

                                {/* TO */}
                                <div className="
                                    flex
                                    items-center
                                    gap-3
                                    min-w-0
                                    flex-1
                                    justify-end
                                    text-right
                                ">

                                    {/* Destination account information */}
                                    <div className="
                                        flex
                                        flex-col
                                        gap-1
                                        min-w-0
                                    ">
                                        <p className="
                                            text-xs
                                            uppercase
                                            tracking-wide
                                            text-text-muted
                                        ">
                                            To
                                        </p>

                                        <p className="
                                            text-text-primary
                                            font-medium
                                            truncate
                                        ">
                                            {destinationAccount?.customerName ??
                                                'Unknown customer'}
                                        </p>

                                        <p className="text-text-muted text-sm">
                                            Account{' '}
                                            {transaction.destinationAccountNo}
                                        </p>
                                    </div>

                                    {/* Destination bank logo */}
                                    {destinationAccount?.bankName && (
                                        <div className="
                                            shrink-0
                                            w-10
                                            h-10
                                            rounded-lg
                                            bg-text-primary
                                            p-2
                                            flex
                                            items-center
                                            justify-center
                                        ">
                                            <Image
                                                src={getBankLogo(
                                                    destinationAccount.bankName
                                                )}
                                                alt={`${destinationAccount.bankName} logo`}
                                                width={80}
                                                height={24}
                                                className="
                                                    object-contain
                                                    max-h-6
                                                    w-auto
                                                "
                                            />
                                        </div>
                                    )}
                                </div>
                            </div>
                        ) : (
                            /* Normal transaction */
                            <div className="flex items-center justify-between">

                                {/* Account */}
                                <div className="
                                    flex
                                    items-center
                                    gap-3
                                    min-w-0
                                ">
                                    {sourceAccount?.bankName && (
                                        <div className="
                                            shrink-0
                                            w-10
                                            h-10
                                            rounded-lg
                                            bg-text-primary
                                            p-2
                                            flex
                                            items-center
                                            justify-center
                                        ">
                                            <Image
                                                src={getBankLogo(
                                                    sourceAccount.bankName
                                                )}
                                                alt={`${sourceAccount.bankName} logo`}
                                                width={80}
                                                height={24}
                                                className="
                                                    object-contain
                                                    max-h-6
                                                    w-auto
                                                "
                                            />
                                        </div>
                                    )}

                                    <div className="flex flex-col gap-1">
                                        <p className="
                                            text-text-primary
                                            font-medium
                                        ">
                                            {transaction.customerName}
                                        </p>

                                        <p className="text-text-muted text-sm">
                                            Account {transaction.accountNo}
                                        </p>
                                    </div>
                                </div>

                                {/* Amount */}
                                <p
                                    className={
                                    `font-semibold text-lg
                                    ${
                                        isDeposit
                                            ? 'text-emerald-500'
                                            : 'text-rose-500'
                                    }
`}
                                >
                                    {isDeposit ? '+' : '−'}
                                    {transaction.amount.toLocaleString()}
                                </p>
                            </div>
                        )}
                    </div>
                );
            })}
        </div>
    );
}
