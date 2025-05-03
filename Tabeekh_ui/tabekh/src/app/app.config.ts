import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter } from '@angular/router';
import { providePrimeNG } from 'primeng/config';

import {
  provideHttpClient,
  withFetch,
  withInterceptors,
} from '@angular/common/http';
import { provideEffects } from '@ngrx/effects';
import { provideStore } from '@ngrx/store';
import { MessageService } from 'primeng/api';
import { MyPreset } from '../style/_colorTheme';
import { routes } from './app.routes';
import { apiProxyInterceptor } from './core/interceptors/ApiProxy/api-proxy.interceptor';
import { tokenInterceptor } from './core/interceptors/Token/token.interceptor';
import { CartEffects } from './core/states/cart/cart.effects';
import { cartReducer } from './core/states/cart/cart.reducers';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(
      withFetch(),
      withInterceptors([
        apiProxyInterceptor,
        tokenInterceptor,
        // timeoutInterceptor
      ])
    ),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: MyPreset,
        options: {
          darkModeSelector: false || 'none',
        },
      },
    }),
    provideStore({ cart: cartReducer }),
    provideEffects(CartEffects),
    MessageService,
  ],
};
