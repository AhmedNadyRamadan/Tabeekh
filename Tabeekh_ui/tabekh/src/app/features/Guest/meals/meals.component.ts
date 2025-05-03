import { AsyncPipe } from '@angular/common';
import { Component, signal, WritableSignal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PaginatorModule, PaginatorState } from 'primeng/paginator';
import { SelectModule } from 'primeng/select';
import { Observable, of } from 'rxjs';
import { IMealMini } from '../../../core/models';
import ICategory from '../../../core/models/ICategory.model';
import { IPaginationData } from '../../../core/models/IPagination.model';
import { LookupsService } from '../../../core/services/lookups/lookups.service';
import { MealCardComponent } from '../../../shared/components/meal-card/meal-card.component';
import IMealsQuery from './models/IMealsQuery.model';
import { MealsService } from './services/meals/meals.service';

@Component({
  selector: 'app-meals',
  imports: [
    AsyncPipe,
    MealCardComponent,
    PaginatorModule,
    FormsModule,
    SelectModule,
  ],
  templateUrl: './meals.component.html',
  styleUrl: './meals.component.scss',
})
export class MealsComponent {
  filterQuery() {
    throw new Error('Method not implemented.');
  }
  data: Observable<IPaginationData<IMealMini>> = of({
    items: [],
    totalCount: 0,
  });
  categories!: Observable<ICategory[]>;

  queryParams: WritableSignal<IMealsQuery> = signal<IMealsQuery>(
    this.defaultQuery
  );

  constructor(
    private _mealsService: MealsService,
    private _LookupsService: LookupsService
  ) {}

  ngOnInit(): void {
    this.categories = this._LookupsService.getCategories();
    this.getMeals();
  }

  onPageChange(event: PaginatorState) {
    this.queryParams.update((value) => {
      return { ...value, limit: event.rows!, pageNumber: event.page! + 1 };
    });
    this.getMeals();
  }

  getMeals() {
    this.data = this._mealsService.getAll(this.queryParams());
  }

  reset() {
    this.queryParams.set(this.defaultQuery);
    document.location.reload();
  }

  get isDisabled() {
    return (
      this.queryParams().name.length === 0 &&
      this.queryParams().category.length === 0
    );
  }

  get first() {
    return 0;
  }

  // get meals(): Observable<IMealMini[]>  {
  //   return this.data.pipe(first(), map(value => value.items)) //this.data ?  :
  // }

  // get totalCount(): Observable<number> {
  //   return this.data.pipe(first(), map(value => value.totalCount));//this.data ?  :
  // }

  get defaultQuery() {
    return {
      name: '',
      category: '',
      pageNumber: 1,
      limit: 10,
    };
  }
}
