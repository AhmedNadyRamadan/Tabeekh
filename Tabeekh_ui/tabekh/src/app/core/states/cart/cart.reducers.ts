import { createReducer, on } from '@ngrx/store';
import ICartState from '../../models/ICartState.model';
import * as CartActions from './cart.actions';
const initialState: ICartState = { items: [], totalQuantity: 0, totalPrice: 0 };

/**
 * Calc total price and total quantity of cart items
 * @param items Cart items
 * @returns [Object] consists of total quantity and total price of cart items
 */
export function calculateTotals(items: ICartState['items']) {
  const totalQuantity = items.reduce((sum, item) => sum + item.quantity, 0);
  const totalPrice = items.reduce(
    (sum, item) => sum + item.quantity * item.price,
    0
  );
  return { totalQuantity, totalPrice };
}

export const cartReducer = createReducer(
  initialState,

  on(CartActions.addItem, (state, { item }) => {
    const existingItem = state.items.find((i) => i.id === item.id);
    let updatedItems;
    if (existingItem) {
      updatedItems = state.items.map((i) =>
        i.id === item.id ? { ...i, quantity: i.quantity + item.quantity } : i
      );
    } else {
      updatedItems = [...state.items, item];
    }
    const { totalQuantity, totalPrice } = calculateTotals(updatedItems);
    return { items: updatedItems, totalQuantity, totalPrice };
  }),

  on(CartActions.removeItem, (state, { itemId }) => {
    const updatedItems = state.items.filter((i) => i.id !== itemId);
    const { totalQuantity, totalPrice } = calculateTotals(updatedItems);
    return { items: updatedItems, totalQuantity, totalPrice };
  }),

  on(CartActions.updateQuantity, (state, { itemId, quantity }) => {
    const updatedItems = state.items.map((i) =>
      i.id === itemId ? { ...i, quantity } : i
    );
    const { totalQuantity, totalPrice } = calculateTotals(updatedItems);
    return { items: updatedItems, totalQuantity, totalPrice };
  }),

  on(CartActions.clearCart, () => initialState),

  on(CartActions.loadCartFromStorage, (state, { items }) => {
    const { totalQuantity, totalPrice } = calculateTotals(items);
    return { items, totalQuantity, totalPrice };
  })
);
