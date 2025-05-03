import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenService } from '../../services/token/token.service';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const _TokenService = inject(TokenService);

  if (_TokenService.isLoggedIn()) return next(req.clone({
    setHeaders: {
      Authorization: `Bearer ${_TokenService.getToken()}`
    }
  }))

  return next(req);
};
