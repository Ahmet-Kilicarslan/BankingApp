

export default class ApiError<T=unknown> extends Error {
    status: number;
    statusText: string;
    data:T;

    
    
    constructor(status: number, statusText: string, data: T, message?: string) {
        super(message || `HTTP ${status}: ${statusText}`);
        this.name = 'ApiError';
        this.status = status;
        this.statusText = statusText;
        this.data = data;
    }
    
    
    
}