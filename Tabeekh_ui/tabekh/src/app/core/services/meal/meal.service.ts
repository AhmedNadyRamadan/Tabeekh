import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import APIEndpoints from '../../configs/APIEndpoints.enum';
import { IMealWithArr } from '../../models';
import IMeal from '../../models/IMeal.model';
import IReview from '../../models/IReview.model';

@Injectable({
  providedIn: 'root',
})
export class MealService {
  constructor(private _HttpClient: HttpClient) {}

  getMealById(id: IMeal['id']): Observable<IMealWithArr> {
    return this._HttpClient
      .get<IMealWithArr>(`${APIEndpoints.Meal}/${id}`)
      .pipe(
        map((value) => {
          return {
            ...value,
            ingredients: (value.ingredients as unknown as string)
              .split('^')
              .filter((value) => value.length > 0),
            recipe: (value.recipe as unknown as string)
              .split('^')
              .filter((value) => value.length > 0),
          };
        })
      );
  }

  getChiefReviews(id: IMeal['id']): Observable<IReview[]> {
    return this._HttpClient.get<IReview[]>(
      `${APIEndpoints.Meal}/${id}${APIEndpoints.Reviews}`
    );
  }
  recommendMeal(category: IMeal['category']): Observable<any> {
    return this._HttpClient.get<any>(`${APIEndpoints.suggestMeal}`, {
      params: {
        category: category,
      },
    });
  }
}
