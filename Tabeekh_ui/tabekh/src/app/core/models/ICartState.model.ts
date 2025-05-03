import { IMealMini } from './IMeal.model';

export interface ICartItem extends IMealMini {
  quantity: number;
}

export default interface ICartState {
  items: ICartItem[];
  totalQuantity: number;
  totalPrice: number;
}
