import { HttpInterceptorFn } from '@angular/common/http';
import { timeout, catchError, TimeoutError, throwError } from 'rxjs';

export const timeoutInterceptor: HttpInterceptorFn = (req, next) => {
  const timeoutValue = Number(req.headers.get('X-Timeout')) || 5000;

  console.log(timeoutValue);
  return next(req).pipe(
    timeout(timeoutValue),
    catchError((error) => {
      if (error instanceof TimeoutError) {
        // console.error(`Request to ${req.url} timed out`);
        return throwError(() => new Error('Request timed out'));
      }
      return throwError(() => error);
    })
  );
};
