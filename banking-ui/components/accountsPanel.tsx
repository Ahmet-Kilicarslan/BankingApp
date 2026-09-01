'use client';

import { Loader2, Inbox } from 'lucide-react';
import  Account  from '@/models/account';

export default function AccountsPanel({ accounts }: { accounts: Account[] | undefined }) {
    if (!accounts) {
        return (
            <div className="flex items-center gap-2 text-text-muted text-sm">
                <Loader2 size={16} className="animate-spin" />
                Loading accounts...
            </div>
        );
    }

    if (accounts.length === 0) {
        return (
            <div className="flex flex-col items-center gap-2 text-text-muted text-sm py-4">
                <Inbox size={24} />
                No accounts found.
            </div>
        );
    }

    return (
        <table className="w-full text-sm">
            <thead>
            <tr className="text-text-muted text-left">
                <th className="pb-2">Account No</th>
                <th className="pb-2">Balance</th>
                <th className="pb-2">Customer Id</th>
                <th className="pb-2">Opened At</th>
            </tr>
            </thead>
            <tbody>
            {accounts.map((account) => (
                <tr key={account.id} className="border-t border-border">
                    <td className="py-2 text-text-primary">{account.accountNo}</td>
                    <td className="py-2 text-text-primary">{account.balance}</td>
                    <td className="py-2 text-text-muted">{account.customerId}</td>
                    <td className="py-2 text-text-muted">{new Date(account.openedAt).toLocaleString()}</td>
                </tr>
            ))}
            </tbody>
        </table>
    );
}