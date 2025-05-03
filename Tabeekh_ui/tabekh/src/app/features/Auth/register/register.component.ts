import { Component, OnInit, signal } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { SelectModule } from 'primeng/select';
import { ToasterService } from '../../../core/services/Toaster/toaster.service';
import UserRoles from '../shared/data/UserRoles.data';
import { AuthService } from '../shared/services/auth/auth.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, SelectModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  roles = signal<typeof UserRoles>(UserRoles);

  constructor(
    private fb: FormBuilder,
    private _AuthService: AuthService,
    private _ToasterService: ToasterService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group(
      {
        username: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required],
        photo: [''],
        phone: ['', [Validators.required, Validators.pattern(/^01[0-9]{9}$/)]],
        address: ['', Validators.required],
        role: [null, Validators.required],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(
    group: AbstractControl
  ): { [key: string]: boolean } | null {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      this._AuthService.register(this.registerForm.value).subscribe({
        next: () => {
          this._ToasterService.onSuccessToaster('تم تسجيل الحساب بنجاح');
          this._router.navigate(['/login']);
        },
        error: (err) => {
          this._ToasterService.onDangerToaster(
            'فشل في التسجيل. يرجي المحاولة لاحقا'
          );
        },
      });
    } else {
      this._ToasterService.onDangerToaster('رجاءاّ تأكد من البيانات المسجلة');
    }
  }

  onPhotoChange(event: any): void {
    const file = event.target.files?.[0];
    if (!file) return;
    const reader = new FileReader();
    reader.onload = () => {
      this.registerForm.patchValue({
        photo: (reader.result as string).split(',')[1],
      });
      // const base64 = reader.result as string;

      // // Properly update the form control
      // this.registerForm.get('photo')?.setValue(base64);
      // this.registerForm.get('photo')?.markAsDirty();
      // this.registerForm.get('photo')?.updateValueAndValidity();
    };
    reader.readAsDataURL(file);
  }

  get usernameControl() {
    return this.registerForm.get('username');
  }

  get emailControl() {
    return this.registerForm.get('email');
  }

  get passwordControl() {
    return this.registerForm.get('password');
  }

  get confirmPasswordControl() {
    return this.registerForm.get('confirmPassword');
  }
  get phoneControl() {
    return this.registerForm.get('phone');
  }
}
