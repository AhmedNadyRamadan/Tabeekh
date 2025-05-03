import { Component, input } from '@angular/core';
import { ICartItem } from '../../../core/models';
import { CartBtnComponent } from '../cart-btn/cart-btn.component';
import { UnitsARArr } from '../../../core/global';
import { CurrencyPipe } from '@angular/common';
import IOrder from '../../../core/models/IOrder.model';

@Component({
  selector: 'app-cartitem',
  imports: [CartBtnComponent, CurrencyPipe],
  templateUrl: './cartitem.component.html',
  styleUrl: './cartitem.component.scss'
})
export class CartitemComponent {
  cartitem = input.required<ICartItem>();
  doneWith = input<boolean>(false)

  get image(): string {
    return this.cartitem()!.photo.length === 0 ? "logo.jpeg" : `data:image/*;base64, ${this.cartitem()!.photo}`;
  }
  get UnitsAR() {
    return UnitsARArr;
  }
}
