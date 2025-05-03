import { Component } from '@angular/core';
import { OffersComponent } from './components/offers/offers.component';
import { TopchiefsComponent } from './components/top-chiefs/top-chiefs.component';
import { TopMealsComponent } from './components/top-meals/top-meals.component';

@Component({
  selector: 'app-home',
  imports: [
    OffersComponent,
    TopchiefsComponent,
    TopMealsComponent,
    // BannerComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
