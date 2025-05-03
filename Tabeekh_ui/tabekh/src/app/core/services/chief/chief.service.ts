import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import APIEndpoints from '../../configs/APIEndpoints.enum';
import IChiefDefault from '../../models/IChief.model';
import IChiefMeals from '../../models/IChiefMeal.model';
import type IMeal from '../../models/IMeal.model';
import type { IMealDefault } from '../../models/IMeal.model';
import IReview from '../../models/IReview.model';

@Injectable({
  providedIn: 'root',
})
export class ChiefService {
  constructor(private _HttpClient: HttpClient) {}

  getChiefById(id: IChiefDefault['id']): Observable<IChiefMeals> {
    return this._HttpClient.get<IChiefMeals>(`${APIEndpoints.Chief}/${id}`);
  }

  updateMeal(
    id: IMeal['id'],
    data: Pick<
      IMealDefault,
      'name' | 'category' | 'day' | 'available' | 'price'
    >
  ): Observable<any> {
    return this._HttpClient.put(`${APIEndpoints.Meal}/${id}`, data);
  }

  addMeal(id: IChiefDefault['id'], data: IMeal): Observable<any> {
    return this._HttpClient.post(`${APIEndpoints.AddMeal}/${id}`, data);
  }

  getChiefReviews(id: IChiefDefault['id']): Observable<IReview[]> {
    return this._HttpClient.get<IReview[]>(
      `${APIEndpoints.Chief}/${id}${APIEndpoints.Reviews}`
    );
  }
}
