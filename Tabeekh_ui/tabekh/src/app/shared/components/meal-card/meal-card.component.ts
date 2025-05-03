import { CurrencyPipe } from '@angular/common';
import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { IMealMini } from '../../../core/models';
import { ChieftitlePipe } from '../../pipes/chieftitle/chieftitle.pipe';
import { RatePipe } from '../../pipes/rate/rate.pipe';
import { CartBtnComponent } from '../cart-btn/cart-btn.component';

@Component({
  selector: 'app-meal-card',
  imports: [
    RatePipe,
    ChieftitlePipe,
    CurrencyPipe,
    CartBtnComponent,
    RouterLink,
  ],
  templateUrl: './meal-card.component.html',
  styleUrl: './meal-card.component.scss',
})
export class MealCardComponent {
  meal = input.required<IMealMini>();
  viewChiefName = input(true);
  isPreview = input(false);

  get mealImage(): string {
    return this.displayMeal()!.photo.length === 0
      ? 'logo.jpeg'
      : `data:image/*;base64, ${this.displayMeal()!.photo}`;
  }

  get showCartBtn() {
    return !this.isPreview();
  }

  get displayMeal() {
    return this.meal;
  }

  get showChiefName() {
    return !this.isPreview() && this.viewChiefName();
  }
}
