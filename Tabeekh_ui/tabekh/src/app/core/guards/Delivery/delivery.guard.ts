import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import EUserMode from '../../enums/EUserMode.enum';
import { TokenService } from '../../services/token/token.service';

export const deliveryGuard: CanActivateFn = (route, state) => {
  const tokenService = inject(TokenService);
  const router = inject(Router);

  if (tokenService.getUserRole() === EUserMode.Delivery) {
    return true;
  }

  router.navigate(['/unauthorized']);
  return false;

};
