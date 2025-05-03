import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import APIEndpoints from '../../configs/APIEndpoints.enum';
import { IMealMini } from '../../models';
import ICategory from '../../models/ICategory.model';
import IChiefDefault from '../../models/IChief.model';

@Injectable({
  providedIn: 'root',
})
export class LookupsService {
  constructor(private _HttpClient: HttpClient) {}

  getCategories(): Observable<ICategory[]> {
    return this._HttpClient.get<ICategory[]>(APIEndpoints.Category);
  }

  getTopMeals(): Observable<IMealMini[]> {
    return this._HttpClient.get<IMealMini[]>(APIEndpoints.Mtop10);
    // To Comment
    // return of<IMealMini[]>([
    //   {
    //     id: '1',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    //   {
    //     id: '2',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    //   {
    //     id: '3',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    //   {
    //     id: '4',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    //   {
    //     id: '5',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    // ]);
    // END
  }

  getOffers(): Observable<IMealMini[]> {
    return this._HttpClient.get<IMealMini[]>(APIEndpoints.MOffers);
    // To Comment
    // return of<IMealMini[]>([
    //   {
    //     id: '1',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    //   {
    //     id: '2',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    //   {
    //     id: '3',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    //   {
    //     id: '4',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    //   {
    //     id: '5',
    //     name: 'محشي كرنب',
    //     chief_name: 'محمد علي',
    //     photo: '',
    //     totalRate: 3,
    //     price: 50,
    //     category: 'عربي',
    //     available: false,
    //     measure_unit: EUnit.Kilogrm,
    //   },
    // ]);
    //END
  }

  getTopchiefs(): Observable<IChiefDefault[]> {
    return this._HttpClient.get<IChiefDefault[]>(APIEndpoints.Ctop10);
    // To Comment
    // return of<IChiefDefault[]>([
    //   {
    //     id: '1',
    //     name: 'علي محمد',
    //     photo: 'logo.jpeg',
    //     totalRate: 0,
    //   },
    //   {
    //     id: '1',
    //     name: 'محمد ابراهيم',
    //     photo: 'logo.jpeg',
    //     totalRate: 0,
    //   },
    //   {
    //     id: '1',
    //     name: 'سعد سعيد',
    //     photo: 'logo.jpeg',
    //     totalRate: 0,
    //   },
    //   {
    //     id: '1',
    //     name: 'فارس ابراهيم',
    //     photo: 'logo.jpeg',
    //     totalRate: 0,
    //   },
    // ]);
    // END
  }
}
