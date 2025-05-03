import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import EUserMode from '../../enums/EUserMode.enum';
import { TokenService } from '../../services/token/token.service';

export const AdminGuard: CanActivateFn = (route, state) => {
  const tokenService = inject(TokenService);
  const router = inject(Router);

  if (tokenService.getUserRole() === EUserMode.Admin) {
    return true;
  }

  router.navigate(['/']);
  return false;
};
