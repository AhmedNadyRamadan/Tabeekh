import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import type { Observable } from 'rxjs';
import APIEndpoints from '../../configs/APIEndpoints.enum';
import type IReview from '../../models/IReview.model';
import type { IReviewPost } from '../../models/IReview.model';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  constructor(private _HttpClient: HttpClient) {}

  addReview(id: IReview['chief_Id'], data: IReviewPost): Observable<any> {
    console.log(data);

    return this._HttpClient.post(
      `${APIEndpoints.AddChiefReview}/${id}${APIEndpoints.Reviews}`,
      data
    );
  }
}
