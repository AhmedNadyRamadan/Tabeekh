import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { SelectModule } from 'primeng/select';
import { ToasterService } from '../../../core/services/Toaster/toaster.service';
import { TokenService } from '../../../core/services/token/token.service';
import { AuthService } from '../shared/services/auth/auth.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, SelectModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _AuthService: AuthService,
    private _TokenService: TokenService,
    private _ToasterService: ToasterService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this._AuthService.login(this.loginForm.value).subscribe({
        next: (res: any) => {
          this._TokenService.setToken(res.token);
          this._ToasterService.onSuccessToaster('تم تسجيل الدخول بنجاح');
          // this._router.navigate(['/']);
          window.location.pathname = '/';
        },
        error: (err) => {
          this._ToasterService.onDangerToaster(
            'فشل في الدخول. يرجي المحاولة لاحقا'
          );
        },
      });
    }
  }
}
