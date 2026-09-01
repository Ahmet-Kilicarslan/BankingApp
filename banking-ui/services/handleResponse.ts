
import ApiError from "./apiError";


  async function handleResponse<T>(response:Response):Promise<T> {
    
    if(response.status===204){
        
        return {} as T;
    }
    
    let data:any;
    
    try{
        
        data = await response.json();
    }catch{
        
        data=null;
        
    }
      if (!response.ok) {
          throw new ApiError(
              response.status,
              response.statusText,
              data,
              data?.message 
          );
      }

      
      return data as T;
    
    
}


interface RequestOptions extends RequestInit{
      token?: string;
}

async function apiClient<T>(Url:string,options:RequestOptions={}): Promise<T> {
      
      const {token,headers,method,...customConfig}=options;
      
      const config:RequestInit = {
          
          method:method ||'GET',
          ...customConfig,
          headers:{
              'Content-Type': 'application/json',
              ...(token ? { Authorization: `Bearer ${token}` } : {}),
              ...headers,
          }
      };
      
      
      
      const response = await fetch(Url,config);
      
      
      return handleResponse<T>(response);
      
      
}


export {apiClient,handleResponse};