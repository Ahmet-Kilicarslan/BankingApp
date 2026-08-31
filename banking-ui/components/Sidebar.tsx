'use client';

import Link from 'next/link';
import {usePathname} from 'next/navigation';
import {useState} from 'react';
import {House, Users, ArrowLeftRight, ChevronLeft,Wallet} from "lucide-react";

const navItems = [
    {label: 'Accounts', href: '/accounts', icon: Wallet},
    {label: 'Transactions', href: '/transactions', icon: ArrowLeftRight},
    {label: "Customers", href: "/customers", icon: Users},
];


export default function Sidebar() {

    const pathname = usePathname();
    const [collapsed, setCollapsed] = useState(false);

    return (
        <aside className={`h-screen bg-surface border-r border-border flex flex-col transition-all duration-200 
        ${collapsed ? 'w-16' : 'w-56'}`}>

            <button
                onClick={() => setCollapsed(!collapsed)}
                className="p-3 flex justify-end text-text-muted hover:text-text-primary"
            >
                <ChevronLeft className={`trasnition-transfrom ${collapsed ? 'rotate-180' : ''}`} size={20}/>
            </button>

            <nav className="flex flex-col gap-1 px-2">
                {navItems.map((item) => {
                    const isActive = pathname === item.href;
                    const Icon = item.icon;

                    return (
                        <Link
                            key={item.href}
                            href={item.href}
                            className={`flex items-center gap-3 px-3 py-2 rounded-md text-sm transition-colors ${
                                isActive
                                    ? 'bg-accent/10 text-accent'
                                    : 'text-text-muted hover:bg-surface hover:text-text-primary'
                            }`}
                        >
                            <Icon size={20}/>
                            {!collapsed && <span>{item.label}</span>}

                        </Link>
                    );
                })}
            </nav>
        </aside>


    )


}