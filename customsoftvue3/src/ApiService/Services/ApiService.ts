import axios from 'axios';
import { ApplicationModel, Package, TokenModel } from '../Interfaces/types';
import { API_URL } from '../config'; 
import {jwtDecode} from 'jwt-decode';

//const API_URL = process.env.VUE_APP_API_URL;
const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Access-Control-Allow-Origin': '*',
    'Content-Type': 'application/json',
  },
});

//Packages
export async function getPackages(): Promise<Package[]> {
  try {
    const response = await api.get('Package/GetAllPackages');
    console.log('paquetes:', response);
    return response.data as Package[];
  } catch (error) {
    console.error('Error al obtener los paquetes:', error);
    return [];
  }
}
export async function getPackageByTrackingNumber(trackingNumber: string): Promise<Package | null> {
  try {
    const response = await api.get(`Package/GetPackagesByTrackingNumber?trackingNumber=`+trackingNumber);
    return response.data as Package;
  } catch (error) {
    console.error('Error al obtener el paquete:', error);
    return null;
  }
}
export async function addPackage(newPackage: Package): Promise<Package> {
  try {
    const response = await api.post('Package/AddPackage', newPackage);
    const addedPackage: Package = response.data; 

    return addedPackage; 
  } catch (error) {
    console.error('Error al agregar el paquete:', error);
    throw new Error('Error al agregar el paquete');
  }
}
//Aut
export async function getToken(application: ApplicationModel): Promise<TokenModel> {
  try {
    const response = await api.post('Auth/GetToken', application);
    const token = response.data;
  
    localStorage.setItem('accessToken', token.accessToken);
    localStorage.setItem('key', application.ApiKey);
    localStorage.setItem('nombre', application.Nombre);
    
    return token;
  } catch (error) {
    console.error('Error al obtener el token:', error);
    throw new Error('Error al obtener el token');
  }
}
export async function RefreshToken(application: ApplicationModel): Promise<TokenModel> {
  try {
    const accessToken = localStorage.getItem('accessToken');
    const key = localStorage.getItem('key');
    const nombre = localStorage.getItem('nombre');

    if (accessToken) {
      const tokenExpiry = jwtDecode<TokenModel>(accessToken).expires_in;
      const currentTime = Date.now() / 1000; 
      if (tokenExpiry && 50 > currentTime) {
        
      }
    }


    const response = await api.post('Auth/GetToken', application);
    const token = response.data;

    localStorage.setItem('accessToken', token.accessToken);
    localStorage.setItem('key', application.ApiKey);
    localStorage.setItem('nombre', application.Nombre);

    return token;
  } catch (error) {
    console.error('Error al obtener o refrescar el token:', error);
    throw new Error('Error al obtener o refrescar el token');
  }
}
//Files

export async function addFile(formData, idPackage, type) {
  try {
      const addResponseFile = await api.post('File/UploadFile', formData, {
          headers: {
              'X-Package-ID': idPackage,
              'Content-Type': type
          }
      });

      const addedPackage: File = addResponseFile.data; 
      return addedPackage;
      console.log(addResponseFile, 'Archivo enviado con éxito');
  } catch (error) {
      console.error('Error al enviar el archivo:', error);
  }
}