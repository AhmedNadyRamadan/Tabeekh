import { AsyncPipe } from '@angular/common';
import {
  Component,
  computed,
  OnInit,
  Signal,
  signal,
  WritableSignal,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginatorModule, PaginatorState } from 'primeng/paginator';
import { map, Observable } from 'rxjs';
import IChiefMeals from '../../../core/models/IChiefMeal.model';
import IReview from '../../../core/models/IReview.model';
import { ChiefService } from '../../../core/services/chief/chief.service';
import { MealCardComponent } from '../../../shared/components/meal-card/meal-card.component';
import { ReviewsComponent } from '../../../shared/components/reviews/reviews.component';
import { RatePipe } from '../../../shared/pipes/rate/rate.pipe';

@Component({
  selector: 'app-chief',
  imports: [
    MealCardComponent,
    ReviewsComponent,
    PaginatorModule,
    AsyncPipe,
    RatePipe,
  ],
  templateUrl: './chief.component.html',
  styleUrl: './chief.component.scss',
})
export class ChiefComponent implements OnInit {
  id: WritableSignal<IChiefMeals['id']> = signal('0');
  chief!: Observable<IChiefMeals>;
  displayedMeals: Signal<Observable<IChiefMeals['meals']>> = computed(() => {
    if (this.tabIndex() === 1)
      return this.chief.pipe(
        map((value) =>
          value.meals.filter((value) => value.day === new Date().getDay())
        )
      );
    if (this.tabIndex() === 2)
      return this.chief.pipe(
        map((value) =>
          value.meals.filter(
            (value) => value.day === (new Date().getDay() + 1) % 7
          )
        )
      );

    return this.chief.pipe(map((value) => value.meals));
  });
  reviews!: Observable<IReview[]>;

  tabIndex: WritableSignal<number> = signal(0);
  first: number = 0;
  rows: number = 10;

  constructor(
    private _chiefService: ChiefService,
    private _ActivatedRoute: ActivatedRoute
  ) {
    this.id.set(this._ActivatedRoute.snapshot.params['id'] || 0);
  }

  ngOnInit(): void {
    this.chief = this._chiefService.getChiefById(this.id());

    this.reviews = this._chiefService.getChiefReviews(this.id());
  }

  toggleTabs(tabIndex: number) {
    this.tabIndex.set(tabIndex);
  }

  onPageChange(event: PaginatorState) {
    this.first = event.first ?? 0;
    this.rows = event.rows ?? 10;
  }

  get dummyImage() {
    return 'logo.jpeg';
  }
  get isActiveTab0() {
    return this.tabIndex() == 0 ? 'active' : null;
  }
  get isActiveTab1() {
    return this.tabIndex() == 1 ? 'active' : null;
  }
  get isActiveTab2() {
    return this.tabIndex() == 2 ? 'active' : null;
  }
  get isActiveTab3() {
    return this.tabIndex() == 3 ? 'active' : null;
  }
}
