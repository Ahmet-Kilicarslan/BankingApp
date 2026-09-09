'use client';
import {useState, useEffect} from 'react'
import {X, Check,LoaderPinwheel} from 'lucide-react'
import Customer from "../models/customer"
import Account from "../models/account"
import {TransactionInitiationDto, TransactionType} from "../models/transaction"
import {getAccountsByCustomerId, getAccountByAccountNoFast} from "../services/accountService"
import {createTransaction, getAllTransactionTypes} from '../services/transactionService'
import {getCustomerById} from "../services/customerService"
import {getErrorMessage} from "../services/handleResponse"
import Image from "next/image";
import { useRouter } from "next/navigation";
export default function CreateTransactionPanel(
    {customer, onClose}: { customer: Customer; onClose: () => void }
) {

    const [accounts, setAccounts] = useState<Account[]>([])
    const [selectedAccount, setSelectedAccount] = useState<Account | null>(null)

    const [destinationAccountNo, setDestinationAccountNo] = useState<string>("")
    const [destinationAccount, setDestinationAccount] = useState<Account>()
    const [destinationCustomer, setDestinationCustomer] = useState<Customer>()

    const [amount, setAmount] = useState<string>("");

    const [transactionTypes, setTransactionTypes] = useState<TransactionType[]>([])
    const [selectedTransactionType, setSelectedTransactionType] = useState<TransactionType | null>(null)

    const [error, setError] = useState<string | null>(null)
    const [isSubmitting, setIsSubmitting] = useState<boolean>(false)

    const isReady = !!selectedAccount && !!selectedTransactionType && amount > 0 &&
        (!selectedTransactionType.requiresDestinationAccount || !!destinationAccount);


    type submitState = "idle" | "submitting" | "success";

    const [submitState, setSubmitState] = useState<submitState>("idle")
    
    const router = useRouter();
    
    
    useEffect(() => {
        async function fetchTransactionTypes() {
            try {
                const result = await getAllTransactionTypes();
                setTransactionTypes(result);
            } catch (err) {
                console.error("Failed to fetch transaction types:", err);
            }
        }

        fetchTransactionTypes();
    }, [])

    useEffect(() => {
        async function fetchAccounts() {
            try {
                const result = await getAccountsByCustomerId(customer.id);
                setAccounts(result);
            } catch (err) {
                console.error("Failed to fetch accounts:", err);
            }
        }

        fetchAccounts();
    }, [customer.id]);

    useEffect(() => {
        async function fetchDestinationAccount() {
            if (destinationAccountNo.length < 6) {
                setDestinationAccount(undefined);
                return;
            }
            const destinationNo: number = Number(destinationAccountNo);
            try {
                const result = await getAccountByAccountNoFast(destinationNo);
                setDestinationAccount(result);
            } catch (err) {
                console.error("Failed to fetch destination account:", err);
                setDestinationAccount(undefined);
            }
        }

        fetchDestinationAccount();
    }, [destinationAccountNo]);

    /* useEffect(() => {
         async function fetchDestinationCustomer() {
             if (!destinationAccount) {
                 setDestinationCustomer(undefined);
                 return;
             }
             try {
                 const result = await getCustomerById(destinationAccount.customerId);
                 setDestinationCustomer(result);
             } catch (err) {
                 console.error("Failed to fetch destination customer:", err);
                 setDestinationCustomer(undefined);
             }
         }
 
         fetchDestinationCustomer();
     }, [destinationAccount])
 */
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

    async function TransferMoney(dto: TransactionInitiationDto) {
        setError(null);
        setSubmitState("submitting");
        try {
            await createTransaction(dto);
            setSubmitState("success");
            setTimeout(() => {
                onClose();
                router.push("/accounts");
            }, 3000);
        } catch (err) {
            setSubmitState("idle"); 
            console.error(err);
            setError(getErrorMessage(err));
        } 
    }

    return (
        <div
            className="relative flex flex-col w-[480px] max-w-[92vw] bg-background border border-border rounded-xl shadow-xl">

            {/* Header */}
            <div className="flex items-center justify-between px-6 py-4 border-b border-border">
                <div>
                    <p className="text-xs uppercase tracking-wide text-text-muted mb-2">New transaction</p>
                    <h2 className="text-xl font-semibold text-text-primary">{customer.name}</h2>
                </div>
                <button
                    onClick={onClose}
                    className="text-text-muted hover:text-text-primary transition-colors rounded-md p-1"
                    aria-label="Close"
                >
                    <X size={18}/>
                </button>
            </div>

            {/* Account selection */}
            <div className="px-6 py-4 flex flex-col gap-3">
                <p className="text-xs uppercase tracking-wide text-text-muted">From account</p>

                {accounts.length === 0 && (
                    <p className="text-sm text-text-muted">No accounts found for this customer.</p>
                )}

                <div className="flex gap-3 overflow-x-auto pb-1">
                    {accounts.map((account: Account) => {
                        const isSelected = selectedAccount?.accountNo === account.accountNo;

                        return (
                            <div
                                key={account.id}
                                onClick={() => setSelectedAccount(isSelected ? null : account)}
                                className={`shrink-0 w-40 bg-surface border rounded-lg
                                    p-3 cursor-pointer transition-colors flex flex-col gap-3
                                 
                                 
                                    ${isSelected
                                    ? "border-accent bg-accent/10 shadow-lg scale-[1.02]"
                                    : "border-border "
                                }`}
                            >
                                {isSelected && (
                                    <div className="absolute top-2 right-2 rounded-full bg-wordle-correct p-1">
                                        <Check size={12} className="text-white"/>
                                    </div>
                                )}

                                <div className="bg-text-primary p-2 rounded-md flex items-center gap-3 h-8">
                                    <Image
                                        src={getBankLogo(account.bankName)}
                                        alt={`${account.bankName} logo`}
                                        width={120}
                                        height={24}
                                        className="object-contain h-5 w-auto"
                                    />
                                </div>

                                <div className="flex flex-col gap-0.5">
                                    <p className="text-base font-semibold text-text-primary">
                                        {account.balance.toLocaleString()} {account.currency}
                                    </p>
                                    <p className="text-xs text-text-muted">{account.accountNo}</p>
                                </div>
                            </div>
                        );
                    })}
                </div>
            </div>

            {/* Transaction details */}
            <div className="px-6 py-4 border-t border-border flex flex-col gap-4">

                <div className="flex flex-col gap-2">
                    <label className="text-xs uppercase tracking-wide text-text-muted">
                        Transaction type
                    </label>
                    <select
                        value={selectedTransactionType?.id ?? ""}
                        onChange={(e) => {
                            const type = transactionTypes.find(t => t.id === Number(e.target.value));
                            setSelectedTransactionType(type ?? null);
                        }}
                        className="rounded-lg border border-border px-4 py-2 bg-surface text-text-primary
                            outline-none focus:border-accent focus:ring-2 focus:ring-blue-200"
                    >
                        <option value="" disabled>Select type</option>
                        {transactionTypes.map(t => (
                            <option key={t.id} value={t.id}>{t.name}</option>
                        ))}
                    </select>
                </div>

                {selectedTransactionType?.requiresDestinationAccount && (
                    <div className="flex flex-col gap-2">
                        <label className="text-xs uppercase tracking-wide text-text-muted">
                            To account
                        </label>
                        <input
                            type="text"
                            value={destinationAccountNo}
                            onChange={(e) => setDestinationAccountNo(e.target.value)}
                            placeholder="Enter account number"
                            className="rounded-lg border border-border px-4 py-2 bg-surface text-text-primary
                                outline-none focus:border-accent focus:ring-2 focus:ring-blue-200"
                        />
                        {destinationCustomer && (
                            <p className="text-sm text-text-muted">
                                To {destinationCustomer.name}
                            </p>
                        )}
                    </div>
                )}

                <div className="flex flex-col gap-2">
                    <label className="text-xs uppercase tracking-wide text-text-muted">
                        Amount
                    </label>
                    <input
                        type="number"
                        min="0"
                        value={amount}
                        onChange={(e) => setAmount(e.target.value)}
                        placeholder="0.00"
                        className="rounded-lg border border-border px-4 py-2 bg-surface text-text-primary
                            outline-none focus:border-accent focus:ring-2 focus:ring-blue-200"
                    />
                </div>

                {error && (
                    <p className="text-sm text-rose-500">{error}</p>
                )}
            </div>

            {/* Footer */}
            <div className="px-6 py-4 border-t border-border mb-2">
                <button
                    className={`
        btn-game w-full
        flex items-center justify-center
        transition-all duration-1000 ease-in-out
        disabled:cursor-not-allowed
        ${submitState === "success"
                        ? "bg-worlde-correct hover:bg-wordle-correct text-white"
                        : "disabled:opacity-50"
                    }
    `}
                    disabled={!isReady || submitState !== "idle"}
                    onClick={() => {
                        if (!isReady || !selectedAccount || !selectedTransactionType) return;

                        TransferMoney({
                            accountNo: selectedAccount.accountNo,
                            destinationAccountNo: destinationAccount?.accountNo ?? null,
                            amount: Number(amount),
                            transactionTypeId: selectedTransactionType.id
                        });
                    }}
                >
                    {submitState === "submitting" && (
                        <LoaderPinwheel className="animate-spin" />
                    )}

                    {submitState === "success" && (
                        <Check className="animate-in zoom-in duration-300" />
                    )}

                    {submitState === "idle" && "Transfer"}
                </button>
            </div>

        </div>
    );
}