import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import type { Observable } from 'rxjs';
import APIEndpoints from '../../../../../core/configs/APIEndpoints.enum';
import { IOrderPost } from './../../../../../core/models/IOrder.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private _HttpClient: HttpClient) {}

  addOrder(order: IOrderPost): Observable<any> {
    return this._HttpClient.post(`${APIEndpoints.addOrder}`, order);
  }
}
