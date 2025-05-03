import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import APIEndpoints from '../../../../../core/configs/APIEndpoints.enum';
import { IUserDisplay, type IUserUpdate } from '../../../../../core/models';
import IOrder from '../../../../../core/models/IOrder.model';
import { TokenService } from './../../../../../core/services/token/token.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(
    private _HttpClient: HttpClient,
    private _TokenService: TokenService
  ) {}

  getAccountDetails(id: IUserDisplay['id']): Observable<IUserDisplay> {
    return this._HttpClient.get<IUserDisplay>(`${APIEndpoints.User}/${id}`);
  }

  getAccountPhoto(id: IUserDisplay['id']): Observable<IUserDisplay['photo']> {
    return this._HttpClient.get<IUserDisplay['photo']>(
      `${APIEndpoints.User}/${id}${APIEndpoints.Photo}`
    );
  }

  updateAccount(id: IUserUpdate['id'], user: IUserUpdate): Observable<any> {
    return this._HttpClient.put<any>(`${APIEndpoints.UpdateUser}/${id}`, user);
  }

  getOrdersHistory(id: IUserDisplay['id']): Observable<IOrder[]> {
    const role = `${this._TokenService.getUserRole()}`;

    return this._HttpClient
      .get<IOrder[]>(
        `${
          role === 'Customer' ? APIEndpoints.Customer : APIEndpoints.Chief
        }/${id}${APIEndpoints.Order}`
      )
      .pipe(
        map((value) => {
          console.log(value);
          return value.map((order: any) => {
            return {
              ...order,
              items: order.items.map((value: any) => {
                return { id: value.mealId, name: value.mealName, ...value };
              }),
            };
          });
        })
      );
  }
}
