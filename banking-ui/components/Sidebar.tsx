'use client';

import Link from 'next/link';
import {usePathname} from 'next/navigation';
import {useState} from 'react';
import {House,Users,ArrowLeftRight,ChevronLeft} from "lucide-react";

const navItems = [
    {label:'Accounts',href:'/accounts',icon:House},
    {label:'Transactions',href:'/transactions',icon:ArrowLeftRight},
    {label: "Customers", href: "/customers",icon:Users},
];


export default function Sidebar() {
    
    const pathname = usePathname();
    const [collapsed, setCollapsed] = useState(false);
    
    return (
        <aside  className={`h-screen bg-surface border-r border-border flex flex-col transition-all duration-200 
        ${collapsed ? 'w-16' : 'w-56'}`}>
          
          <button
          onClick={() => setCollapsed(!collapsed)}
          className="p-3 flex justify-end text-text-muted hover:text-text-primary"
          >
              <ChevronLeft className={`trasnition-transfrom ${collapsed ? 'rotate-180' : ''}`} size={18}/>
          </button>
           

        </aside>
        
        
        
    )
    
    
    
}