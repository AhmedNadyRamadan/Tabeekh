import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import IChiefDefault from '../../../../../core/models/IChief.model';
import IChiefsQuery from '../../models/IChiefsQuery.model';
import { IPaginationData } from '../../../../../core/models/IPagination.model';
import APIEndpoints from '../../../../../core/configs/APIEndpoints.enum';

@Injectable({
  providedIn: 'root'
})
export class chiefsService {

  constructor(private _httpClient: HttpClient) { }

  getAll(queryParams: IChiefsQuery): Observable<IPaginationData<IChiefDefault>>{
    return this._httpClient.get<IPaginationData<IChiefDefault>>(APIEndpoints.Chief, {params: {...queryParams}});
  }
  
}
