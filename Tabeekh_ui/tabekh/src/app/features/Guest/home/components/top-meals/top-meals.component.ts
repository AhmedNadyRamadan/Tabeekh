import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { CarouselModule } from 'primeng/carousel';
import { responsiveOptions } from '../../../../../core/configs';
import { IMealMini } from '../../../../../core/models';
import { MealCardComponent } from '../../../../../shared/components/meal-card/meal-card.component';
import { LookupsService } from '../../../../../core/services/lookups/lookups.service';

@Component({
  selector: 'app-top-meals',
  imports: [AsyncPipe, MealCardComponent, CarouselModule],
  templateUrl: './top-meals.component.html',
  styleUrl: './top-meals.component.scss'
})
export class TopMealsComponent {
  topMeals!: Observable<IMealMini[]>;

  responsiveOptions!: any[];

  constructor(private _lookupsService: LookupsService) { }

  ngOnInit(): void {
    this.topMeals = this._lookupsService.getTopMeals();
    this.responsiveOptions = responsiveOptions;
  }
}
