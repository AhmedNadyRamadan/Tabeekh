import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PageFooterComponent } from './shared/layout/page-footer/page-footer.component';
import { PageHeadComponent } from './shared/layout/page-head/page-head.component';
import { Store } from '@ngrx/store';
import { ICartItem } from './core/models';
import * as CartActions from './core/states/cart';
import { ToastModule } from 'primeng/toast';


@Component({
  selector: 'app-root',
  imports: [PageHeadComponent, ToastModule, PageFooterComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  constructor(private store: Store) { }

  ngOnInit() {
    const cartFromStorage = localStorage.getItem('cart');
    if (cartFromStorage) {
      const items: ICartItem[] = JSON.parse(cartFromStorage);
      this.store.dispatch(CartActions.loadCartFromStorage({ items }));
    }
  }
}
