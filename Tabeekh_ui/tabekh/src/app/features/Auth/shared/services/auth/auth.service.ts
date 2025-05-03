import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import IUser, { ILoginCredentials } from '../../../../../core/models/IUser.model';
import APIEndpoints from '../../../../../core/configs/APIEndpoints.enum';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _HttpClient: HttpClient) { }

  login(_loginCredentials: ILoginCredentials){
    return this._HttpClient.post(APIEndpoints.Login, _loginCredentials);
  }

  register(_user: IUser){
    return this._HttpClient.post(APIEndpoints.Register, _user);
  }
}
