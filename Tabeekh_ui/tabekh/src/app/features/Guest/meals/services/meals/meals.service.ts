import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { IMealMini, IMealWithArr } from '../../../../../core/models';
import EUnit from '../../../../../core/enums/EUnit.enum';
import IMeal from '../../../../../core/models/IMeal.model';
import EDay from '../../../../../core/enums/EDay.enum';
import IMealsQuery from '../../models/IMealsQuery.model';
import APIEndpoints from '../../../../../core/configs/APIEndpoints.enum';
import { IPaginationData } from '../../../../../core/models/IPagination.model';

@Injectable({
  providedIn: 'root'
})
export class MealsService {

  constructor(private _httpClient: HttpClient) { }

  getAll(queryParams: IMealsQuery): Observable<IPaginationData<IMealMini>> {
    return this._httpClient.get<IPaginationData<IMealMini>>(APIEndpoints.Meal, {params: {...queryParams}})
  }
  
}
