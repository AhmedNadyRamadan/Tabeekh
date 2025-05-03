import { createFeatureSelector, createSelector } from '@ngrx/store';
import ICartState from '../../models/ICartState.model';

const selectCartState = createFeatureSelector<ICartState>('cart');

export const selectCartItems = createSelector(selectCartState, state => state.items);
export const selectTotalQuantity = createSelector(selectCartState, state => state.totalQuantity);
export const selectTotalPrice = createSelector(selectCartState, state => state.totalPrice);


export const selectCartItemById = (itemId: string) => createSelector(
    selectCartItems,
    (items) => items.find(item => item.id === itemId)
  );