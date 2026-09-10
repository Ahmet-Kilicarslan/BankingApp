export function getBankLogo(bankName: string) {
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