import { Injectable } from '@angular/core';
import type { ICartItem } from '../../models/ICartState.model';
// import { ICartItem, ICartItems } from '../../models/ICartState.model';

@Injectable({
  providedIn: 'root',
})
export class CartManageService {
  constructor() {}

  getCartState(): ICartItem[] {
    return JSON.parse(localStorage.getItem('cart')!) || [];
  }

  // private setCartItems(items: ICartItem) {
  //   localStorage.setItem("cartItems", JSON.stringify(items))
  // }

  // isItemAdded(id: ICartItem["id"]): boolean {
  //   return this.getCartItems().findIndex((value) => value.id === id) !== -1;
  // }

  // getCartItem(id: ICartItem["id"]): ICartItem{
  //   return this.getCartItems().filter((value) => value.id === id);
  // }

  // addCartItem(item: ICartItem): void {
  //   const items = this.getCartItems();
  //   items.push(item);
  //   this.setCartItems(items);
  // }

  // removeCartItem(id: ICartItem["id"]){
  //   this.setCartItems(this.getCartItems().filter((value)=> value.id !== id));
  // }

  // changeitemQuantity(id: ICartItem["id"], by: number) {
  //   const items = this.getCartItems();
  //   this.setCartItems(items.map((value => { return value.id === id ? { ...value, quantity: value.quantity + by } : value })));
  // }
}
