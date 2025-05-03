import { createAction, props } from '@ngrx/store';
import { ICartItem } from '../../models';

/**
 * Add Items to Cart
 */
export const addItem = createAction('[Cart] Add Item', props<{ item: ICartItem }>());

/**
 * Remove Items from Cart
 */
export const removeItem = createAction('[Cart] Remove Item', props<{ itemId: string }>());

/**
 * Remove Items from Cart
 */
export const updateQuantity = createAction('[Cart] Update Quantity', props<{
    itemId: string; quantity: number
}>());

/**
 * Clear Cart
 */
export const clearCart = createAction('[Cart] Clear Cart');

/**
 * Load Cart to localstorage
 */
export const loadCartFromStorage = createAction('[Cart] Load Cart From Storage', props<{
    items: ICartItem[]
}>());