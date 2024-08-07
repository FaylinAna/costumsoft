export interface Package {
    id: number;
    trackingNumber: string;
    customerName: string;
    deliveryAddress: string;
    stateDate: string;
    weight: number;
    StateDescription : string;
    states: State[];
  }
  
  export interface State {
    id: number;
    package_id: number;
    state: string;
    state_Date_Packages: string;
  }
  export interface ApplicationModel {
    ClientSecret: string;
    ApiKey: string;
    Nombre: string;


  }

  interface ErrorDetails {
    type: string;
    title: string;
    status: number;
    traceId: string;
    errors: Record<string, string[]>;
  }
  
  export interface TokenModel {
    access_token: number;
    expires_in: string;
    token_type: string;


  }

export interface File {
  FileName: string;
  FileType: string; 
  FilePath: string;
  PackageId: Number;
}
