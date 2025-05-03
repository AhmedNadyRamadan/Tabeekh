import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { Observable} from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { UnitsARArr } from '../../../core/global';
import { IMealWithArr } from '../../../core/models';
import IMeal from '../../../core/models/IMeal.model';
import { CartBtnComponent } from '../../../shared/components/cart-btn/cart-btn.component';
import { ChieftitlePipe } from '../../../shared/pipes/chieftitle/chieftitle.pipe';
import { MinutesToTimePipe } from '../../../shared/pipes/minutes-to-time/minutes-to-time.pipe';
import { RatePipe } from '../../../shared/pipes/rate/rate.pipe';
import { MealService } from '../../../core/services/meal/meal.service';

@Component({
  selector: 'app-meal',
  imports: [CartBtnComponent, ChieftitlePipe, AsyncPipe, CurrencyPipe, RatePipe, MinutesToTimePipe],
  templateUrl: './meal.component.html',
  styleUrl: './meal.component.scss'
})
export class MealComponent implements OnInit {
  id: WritableSignal<IMeal['id']> = signal("GGSDSADSA-SDADSAD")
  meal!: Observable<IMealWithArr>;


  constructor(private _MealService: MealService, private _ActivatedRoute: ActivatedRoute) {
    this.id.set(this._ActivatedRoute.snapshot.params['id']);
  }

  ngOnInit(): void {
    this.meal = this._MealService.getMealById(this.id());
  }

  get dummyImage() {
    return "logo.jpeg";
  }

  get UnitsAR() {
    return UnitsARArr;
  }
}
