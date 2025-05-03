import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TokenService } from '../../../../core/services/token/token.service';
import { AccountService } from '../services/account/account.service';
import { ToasterService } from './../../../../core/services/Toaster/toaster.service';

@Component({
  selector: 'app-update-account',
  imports: [ReactiveFormsModule],
  templateUrl: './update-account.component.html',
  styleUrl: './update-account.component.scss',
})
export class UpdateAccountComponent {
  updateForm!: FormGroup;
  previewPhoto: string = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private tokenService: TokenService,
    private ToasterService: ToasterService,
    private Router: Router
  ) {}

  ngOnInit(): void {
    this.updateForm = this.fb.group({
      id: [''],
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^01[0-9]{9}$/)]],
      address: ['', Validators.required],
      photo: [''],
    });

    this.route.queryParams.subscribe((params) => {
      const id = params['id'];
      if (!id) {
        this.router.navigate(['/account']);
        return;
      }

      const currentUserId = this.tokenService.getUserId();
      if (id !== currentUserId) {
        this.router.navigate(['/account']);
        return;
      }

      if (Object.keys(params).length === 5) {
        this.updateForm.patchValue({
          id: params['id'],
          username: params['name'],
          email: params['email'],
          phone: params['phone'],
          address: params['address'],
          // photo: params['photo'],
        });
        this.previewPhoto = params['photo'];
      } else {
        this.accountService.getAccountDetails(id).subscribe({
          next: (user) => {
            this.updateForm.patchValue({
              username: params['name'],
              email: params['email'],
              phone: params['phone'],
              address: params['address'],
            });
            this.previewPhoto = user.photo;
          },
          error: () => {
            this.router.navigate(['/account']);
          },
        });
      }
    });
  }

  onPhotoChange(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.previewPhoto = (reader.result as string).split(',')[1];
        this.updateForm.patchValue({ photo: this.previewPhoto });
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit(): void {
    if (this.updateForm.valid) {
      this.accountService
        .updateAccount(this.tokenService.getUserId()!, this.updateForm.value)
        .subscribe({
          next: (res) => {
            console.log(res);
          },
          error: (err) => {
            this.ToasterService.onDangerToaster('فشلت عملية التحديث');
          },
          complete: () => {
            this.ToasterService.onSuccessToaster('تم التحديث بنجاح');
            this.Router.navigate(['/account']);
          },
        });
      // console.log('Updated user:', this.updateForm.value);
      // submit the form data
    }
  }
}
