import GUID from '../types/GUID.type';
import { ICartItem } from './ICartState.model';

interface IOrderMeal
  extends Pick<
    ICartItem,
    'id' | 'name' | 'photo' | 'price' | 'quantity' | 'measure_unit'
  > {}

export default interface IOrder {
  id: GUID;
  customer_Id: GUID;
  price: number;
  address: string;
  items: IOrderMeal[];
  date: Date;
}

export interface IOrderPost extends Omit<IOrder, 'id' | 'date' | 'items'> {
  items: IOrderMealPost[];
}
interface IOrderMealPost extends Omit<IOrderMeal, 'id' | 'name'> {
  mealName: IOrderMeal['name'];
  mealId: IOrderMeal['id'];
}
