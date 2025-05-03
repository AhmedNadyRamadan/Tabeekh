import { Component, inject, signal, WritableSignal } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { MenuItem, MenuItemCommandEvent } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { Menu } from 'primeng/menu';
import { TokenService } from '../../../core/services/token/token.service';
@Component({
  selector: 'app-page-head',
  imports: [RouterLink, RouterLinkActive, Menu, ButtonModule],
  templateUrl: './page-head.component.html',
  styleUrl: './page-head.component.scss',
})
export class PageHeadComponent {
  private _TokenService: TokenService = inject(TokenService);
  private _Router: Router = inject(Router);

  routes: WritableSignal<
    { route: string; titleEn: string; titleAr: string }[]
  > = signal([
    {
      route: '/',
      titleEn: 'Home',
      titleAr: 'الصفحة الرئيسية',
    },
    {
      route: '/chiefs',
      titleEn: 'chiefs',
      titleAr: 'الشيفات',
    },
    {
      route: '/meals',
      titleEn: 'Meals',
      titleAr: 'الوجبات',
    },
  ]);

  iconLinks: WritableSignal<{ icon: string; route: string; show: boolean }[]> =
    signal([
      {
        icon: 'pi pi-sign-in', //'fa fa-solid fa-user',
        route: '/login',
        show: !this.isLoggedIn,
      },
      {
        icon: 'pi pi-user-plus', //'fa fa-solid fa-user-plus',
        route: '/register',
        show: !this.isLoggedIn,
      },
      {
        icon: 'pi pi-shopping-cart', //'fa fa-solid fa-shopping-cart',
        route: '/cart',
        show: true,
      },
    ]);

  items: MenuItem[] | undefined;

  get isLoggedIn() {
    return this._TokenService.isLoggedIn();
  }

  get user() {
    return this._TokenService.getPayload();
  }

  constructor() {
    // console.log(this._TokenService.getPayload())
  }

  ngOnInit() {
    this.items = [
      {
        label: 'الحساب',
        items: [
          {
            label: 'بيانات الحساب',
            icon: 'pi pi-user',
            routerLink: '/account',
          },
          {
            label: 'تسجيل الخروج',
            icon: 'pi pi-sign-out',
            command: this.logout(),
          },
        ],
      },
    ];
    if (`${this.user?.role}` === `Chief`) {
      this.items.unshift({
        label: 'لوحة التحكم',
        items: [
          {
            label: 'الوجبات',
            icon: 'pi pi-receipt',
            routerLink: '/chief/dashboard',
          },
        ],
      });
    }
  }

  routeTo(route: string) {
    this._Router.navigate([route]);
  }

  logout() {
    return (event: MenuItemCommandEvent) => {
      this._TokenService.removeToken();
      window.location.pathname = '/login';
    };
  }
}
