import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { CarouselModule } from 'primeng/carousel';
import { responsiveOptions } from '../../../../../core/configs';
import { IMealMini } from '../../../../../core/models';
import { MealCardComponent } from '../../../../../shared/components/meal-card/meal-card.component';
import { LookupsService } from '../../../../../core/services/lookups/lookups.service';

@Component({
  selector: 'app-offers',
  imports: [AsyncPipe, MealCardComponent, CarouselModule],
  templateUrl: './offers.component.html',
  styleUrl: './offers.component.scss'
})
export class OffersComponent implements OnInit {
  offers!: Observable<IMealMini[]>;

  responsiveOptions!: any[];

  constructor(private _LookupsService: LookupsService) { }

  ngOnInit(): void {
    this.offers = this._LookupsService.getOffers();
    this.responsiveOptions = responsiveOptions;
  }



}
