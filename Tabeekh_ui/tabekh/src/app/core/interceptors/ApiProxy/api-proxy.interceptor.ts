import { HttpInterceptorFn } from '@angular/common/http';

export const apiProxyInterceptor: HttpInterceptorFn = (req, next) => {
  const backendBaseUrl = 'http://localhost:5082'; 

  if (req.url.startsWith('/api')) {
    const updatedRequest = req.clone({
      url: backendBaseUrl + req.url
    });
    return next(updatedRequest);
  }

  return next(req);
};
