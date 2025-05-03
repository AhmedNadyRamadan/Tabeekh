import { AsyncPipe } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DialogModule } from 'primeng/dialog';
import { Table } from 'primeng/table';
import { map, Observable } from 'rxjs';
import { MealService } from './../../../core/services/meal/meal.service';

import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { SelectModule } from 'primeng/select';
import { EditableRow, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';

import { CardModule } from 'primeng/card';
import DaysAR, { DaysARArr } from '../../../core/global/Days.data';
import ICategory from '../../../core/models/ICategory.model';
import IChiefMeals from '../../../core/models/IChiefMeal.model';
import { ChiefService } from '../../../core/services/chief/chief.service';
import { LookupsService } from '../../../core/services/lookups/lookups.service';
import { ToasterService } from '../../../core/services/Toaster/toaster.service';
import { TokenService } from '../../../core/services/token/token.service';
@Component({
  selector: 'app-chief-dashboard',
  templateUrl: './chief-dashboard.component.html',
  styleUrl: './chief-dashboard.component.scss',
  imports: [
    TableModule,
    ToastModule,
    RouterLink,
    MultiSelectModule,
    InputNumberModule,
    InputTextModule,
    SelectModule,
    ButtonModule,
    TagModule,
    FormsModule,
    AsyncPipe,
    DialogModule,
    CardModule,
  ],
  providers: [EditableRow],
})
export class ChiefDashboardComponent {
  @ViewChild('dt') dt!: Table;
  chief!: Observable<IChiefMeals>;
  initialMealsValue!: Observable<IChiefMeals['meals']>;
  categories!: Observable<ICategory[]>;
  statuses!: { label: string; value: IChiefMeals['meals'][0]['available'] }[];

  isSorted: boolean = false;
  clonedMeals: { [id: string]: IChiefMeals['meals'][0] } = {};

  selectedCategory: any = null;
  displayDialog = false;
  displayResult = false;
  recipes: any[] = [];

  constructor(
    private _tokenService: TokenService,
    private _chiefService: ChiefService,
    private _LookupsService: LookupsService,
    private _ToasterService: ToasterService,
    private _MealService: MealService
  ) {}

  ngOnInit() {
    this.chief = this._chiefService.getChiefById(
      this._tokenService.getPayload()?.id!
    );
    this.initialMealsValue = this.chief.pipe(map((value) => value.meals));
    this.categories = this._LookupsService.getCategories();
    this.statuses = [
      { label: 'متواجد', value: true },
      { label: 'غير متواجد', value: false },
    ];
  }

  onRowEditInit(meal: IChiefMeals['meals'][0]) {
    this.clonedMeals[meal.id as string] = { ...meal };
  }

  onRowEditSave(meal: IChiefMeals['meals'][0]) {
    if (meal.price > 0) {
      delete this.clonedMeals[meal.id as string];
      this._chiefService
        .updateMeal(meal.id, {
          name: meal.name,
          category: meal.category,
          day: meal.day,
          available: meal.available,
          price: meal.price,
        })
        .subscribe({
          next: () => {},
          error: () => {
            this._ToasterService.onDangerToaster('فشل تعديل الوجبة');
          },
          complete: () => {
            this._ToasterService.onSuccessToaster('تم تحديث الوجبة');
          },
        });
    } else {
      this._ToasterService.onDangerToaster('المبلغ غير صحيح');
    }
  }

  onRowEditCancel(meal: IChiefMeals['meals'][0], index: number) {
    // this.initialMealsValue[index as string] = this.clonedMeals[meal.id as string];
    window.location.reload();
    delete this.clonedMeals[meal.id as string];
  }

  getSeverity(status: IChiefMeals['meals'][0]['available']) {
    switch (status) {
      case true:
        return 'success';
      case false:
        return 'danger';
    }
  }
  showDialog() {
    this.displayDialog = true;
    this.selectedCategory = null;
    this.recipes = [];
  }
  async submitCategory() {
    // Example: send category to backend
    const category = this.selectedCategory;
    this._ToasterService.onInfoToaster('الرجاء الانتظار يتم بناء الاقتراح...', {
      summary: 'انتظر',
    });
    // Simulate backend response
    this.displayDialog = false;
    this._MealService.recommendMeal(category).subscribe({
      next: (res: any) => {
        this.recipes = res;
      },
      error: () => {},
      complete: () => {
        this.displayResult = true;
        this._ToasterService.clear();
      },
    });
  }

  get meals() {
    return this.initialMealsValue;
  }

  get DaysARArr() {
    return DaysARArr;
  }

  get DaysAR() {
    return DaysAR;
  }

  get status() {
    return ['غير متواجد', 'متواجد'];
  }
}
