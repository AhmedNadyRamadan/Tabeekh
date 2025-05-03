import { NgClass } from '@angular/common';
import {
  Component,
  effect,
  inject,
  input,
  output,
  signal,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import IReview from '../../../../../core/models/IReview.model';

@Component({
  selector: 'app-add-reviews',
  imports: [ReactiveFormsModule, NgClass],
  templateUrl: './add-reviews.component.html',
  styleUrl: './add-reviews.component.scss',
})
export class AddReviewsComponent {
  private fb = inject(FormBuilder);

  review = input<IReview>();
  submitReview = output<Pick<IReview, 'comment' | 'totalRate'>>();

  form: FormGroup = this.fb.group({
    totalRate: [0, Validators.required],
    comment: ['', Validators.required],
  });

  rating = signal(0);
  starsArray = Array(5).fill(0);

  constructor() {
    effect(() => {
      this.form.patchValue({ totalRate: this.rating() });
    });
  }

  ngOnInit() {
    if (this.review()) {
      this.form.patchValue(this.review()!);
      this.rating.set(this.review()!.totalRate);
    }
  }

  selectRating(stars: number) {
    this.rating.set(stars);
  }

  onSubmit() {
    if (this.form.valid && this.rating() > 0) {
      const value = { ...this.form.value, totalRate: this.rating() } as IReview;
      this.submitReview.emit(value);
      this.form.reset();
      this.rating.set(0);
      window.location.reload();
    } else {
      this.form.markAllAsTouched();
    }
  }
}
