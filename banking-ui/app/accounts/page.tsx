import AccountsComponent from "../../components/accountsComponent"
import {getAllAccounts} from "../../services/accountService"
import {Account} from "../../models/account"

export default async function AccountsPage() {

    var accounts: Account[] = await getAllAccounts();

    return (
        <div className="p-6">
            <h1 className="text-text-primary  justify-self-center text-xl">Accounts</h1>
            <AccountsComponent accounts={accounts}/>
        </div>
    );

}