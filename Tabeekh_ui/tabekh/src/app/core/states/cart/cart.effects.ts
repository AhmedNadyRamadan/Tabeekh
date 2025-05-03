import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as CartActions from './cart.actions';
import { tap, withLatestFrom } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { selectCartItems } from './cart.selectors';

@Injectable()
export class CartEffects {
    private actions$: Actions = inject(Actions);
    private store: Store = inject(Store)
    saveCart$ = createEffect(
        () =>
            this.actions$.pipe(
                ofType(
                    CartActions.addItem,
                    CartActions.removeItem,
                    CartActions.updateQuantity,
                    CartActions.clearCart
                ),
                withLatestFrom(this.store.select(selectCartItems)),
                tap(([action, items]) => {
                    localStorage.setItem('cart', JSON.stringify(items));
                })
            ),
        { dispatch: false }
    );

}