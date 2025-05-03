import { Injectable } from '@angular/core';
import EUserMode from '../../enums/EUserMode.enum';
import ITokenPayload from '../../models/IToken.model';

const TOKEN_KEY = 'auth_token';
@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {
    //http://schemas.xmlsoap.org/ws/2005/05/identity/claims/
  }

  setToken(token: string): void {
    localStorage.setItem(TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  removeToken(): void {
    localStorage.removeItem(TOKEN_KEY);
  }

  getPayload(): ITokenPayload | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const base64Payload = token.split('.')[1];
      const jsonPayload = atob(base64Payload);
      const rawPayload = JSON.parse(jsonPayload);

      const mappedPayload: ITokenPayload = {
        email:
          rawPayload[
            'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
          ],
        id: rawPayload[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ],
        name: rawPayload[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
        ],
        role: rawPayload[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ],
        address:
          rawPayload[
            'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress'
          ],
        exp: rawPayload.exp,
        iss: rawPayload.iss,
        aud: rawPayload.aud,
      };

      return mappedPayload;
    } catch (error) {
      console.error('Invalid token format', error);
      return null;
    }
  }

  isTokenExpired(): boolean {
    const payload = this.getPayload();
    if (!payload) return true;

    const now = Math.floor(Date.now() / 1000);
    return payload.exp < now;
  }

  getUserRole(): EUserMode | null {
    const roleStr = this.getPayload()?.role as EUserMode;
    if (!roleStr) return null;

    return roleStr;
    // const role: EUserMode = EUserMode[roleStr];
    // return role !== undefined ? role : null;
  }

  getUserId(): ITokenPayload['id'] | null {
    const id = this.getPayload()?.id;
    if (!id) return null;

    return id;
  }

  isLoggedIn(): boolean {
    return !!this.getToken() && !this.isTokenExpired();
  }
}
