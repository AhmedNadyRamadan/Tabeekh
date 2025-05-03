import { Component, input, OnInit } from '@angular/core';
// import { CartManageService } from '../../../core/services/cart-manage/cart-manage.service';
import * as CartSelectors from '../../../core/states/cart/cart.selectors';
import * as CartActions from '../../../core/states/cart/cart.actions';
import { IMealMini } from '../../../core/models';
import { Store } from '@ngrx/store';
import { FormsModule } from '@angular/forms';
import { ToasterService } from '../../../core/services/Toaster/toaster.service';

type TChangeStates = "increase" | "decrease" | "set";

@Component({
  selector: 'app-cart-btn',
  templateUrl: './cart-btn.component.html',
  styleUrl: './cart-btn.component.scss',
  imports: [FormsModule]
})
export class CartBtnComponent implements OnInit{
  meal = input.required<IMealMini>();
  isLarger = input<boolean>(false);
  increaseBy = input<number>(1);
  isAdded: boolean = false;
  quantity: number = 1;

  constructor(private store: Store, private _ToasterService: ToasterService){}

  ngOnInit(): void {
    this.store.select(CartSelectors.selectCartItemById(this.meal().id)).subscribe((item) => {
      if (item) {
        this.isAdded = true;
        this.quantity = item.quantity;
      } else {
        this.isAdded = false;
        this.quantity = 1;
      }
    });
  }

  onQuantityChange(states: TChangeStates){
    if(states === "increase") this.quantity += this.increaseBy();

    if(states === "decrease" && this.quantity > 0) this.quantity -= this.increaseBy();

    if(this.quantity <= 0){ 
      this.store.dispatch(CartActions.removeItem({ itemId: this.meal().id }));
      this.isAdded = false;
      return;
    }

    this.store.dispatch(CartActions.updateQuantity({ itemId: this.meal().id, quantity: this.quantity }));
  }

  add(){
    this.store.dispatch(CartActions.addItem({ item: {...this.meal(), quantity: this.quantity} }));
    this.isAdded = true;
    this.quantity = this.quantity || 1;
    this._ToasterService.onSuccessToaster('تم إضافة الوجبة');
  }

  get large(){
    return this.isLarger() == true ? "larger" : null;
  }
}
