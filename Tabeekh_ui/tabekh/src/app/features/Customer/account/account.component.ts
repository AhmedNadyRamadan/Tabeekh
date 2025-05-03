import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IUserDisplay } from '../../../core/models';
import IOrder from '../../../core/models/IOrder.model';
import { TokenService } from '../../../core/services/token/token.service';
import { OrderCardComponent } from '../../../shared/components/order-card/order-card.component';
import { AccountService } from './services/account/account.service';

@Component({
  selector: 'app-account',
  imports: [AsyncPipe, OrderCardComponent],
  templateUrl: './account.component.html',
  styleUrl: './account.component.scss',
})
export class AccountComponent implements OnInit {
  private _TokenService: TokenService = inject(TokenService);
  user: Partial<IUserDisplay> = {
    ...this._TokenService.getPayload(),
    photo: '',
    phone: '',
    address: '',
  };
  orders!: Observable<IOrder[]>;

  get userImage() {
    return this.user.photo
      ? `data:image/*;base64, ${this.user.photo}`
      : 'logo.jpeg';
  }

  constructor(
    private _AccountService: AccountService,
    private _Router: Router
  ) {}

  ngOnInit(): void {
    this._AccountService
      .getAccountDetails(this.user.id as string)
      .subscribe({ next: (res) => (this.user = res), complete: () => {} });
    this.orders = this._AccountService.getOrdersHistory(this.user.id!);
  }

  updateAccount() {
    this._Router.navigate(['account', 'update'], {
      queryParams: {
        id: this.user.id,
        name: this.user.name,
        email: this.user.email,
        phone: this.user.phone,
        address: this.user.address,
        // photo: this.user.photo,
      },
    });
  }
}
