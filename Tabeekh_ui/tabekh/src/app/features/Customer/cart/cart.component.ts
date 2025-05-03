import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { map } from 'rxjs';
import { TokenService } from '../../../core/services/token/token.service';
import { calculateTotals } from '../../../core/states/cart';
import * as CartActions from '../../../core/states/cart/cart.actions';
import * as CartSelectors from '../../../core/states/cart/cart.selectors';
import { CartitemComponent } from '../../../shared/components/cartitem/cartitem.component';
import { CartManageService } from './../../../core/services/cart-manage/cart-manage.service';
import { ToasterService } from './../../../core/services/Toaster/toaster.service';
import { OrderService } from './services/order/order.service';

@Component({
  selector: 'app-cart',
  imports: [CartitemComponent, AsyncPipe, CurrencyPipe],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss',
})
export class CartComponent {
  private store = inject(Store);
  cartitems = this.store.select(CartSelectors.selectCartItems);
  totalPrice = this.store.select(CartSelectors.selectTotalPrice);

  constructor(
    private _TokenService: TokenService,
    private _OrderService: OrderService,
    private _CartManageService: CartManageService,
    private _ToasterService: ToasterService
  ) {}

  isShowOrder() {
    return `${this._TokenService?.getPayload()?.role}` === `Customer`;
  }

  onSubmitOrder() {
    var carItems = this._CartManageService.getCartState();
    let { totalPrice } = calculateTotals(carItems);

    this._OrderService
      .addOrder({
        customer_Id: this._TokenService.getPayload()?.id!,
        price: totalPrice,
        address: this._TokenService.getPayload()?.address!,
        items: carItems.map((value) => {
          return {
            mealId: value.id,
            mealName: value.name,
            quantity: value.quantity,
            measure_unit: value.measure_unit,
            price: value.price,
            photo: value.photo,
          };
        }),
      })
      .subscribe({
        next: (res) => {
          console.log(res);
        },
        error: () => {
          this._ToasterService.onDangerToaster('فشل عمل الطلب');
        },
        complete: () => {
          this.store.dispatch(CartActions.clearCart());
          this._ToasterService.onSuccessToaster('تم عمل الطلب بنجاح');
        },
      });
  }

  get orderDisabled() {
    return this.totalPrice.pipe(map((value) => value === 0));
  }
}
