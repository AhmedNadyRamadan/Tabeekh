import { Component, input } from '@angular/core';
import IReview from '../../../core/models/IReview.model';
import { ReviewService } from '../../../core/services/review/review.service';
import { TokenService } from '../../../core/services/token/token.service';
import { AddReviewsComponent } from './components/add-reviews/add-reviews.component';
import { UserCommentsComponent } from './components/user-comments/user-comments.component';

@Component({
  selector: 'app-reviews',
  imports: [AddReviewsComponent, UserCommentsComponent],
  templateUrl: './reviews.component.html',
  styleUrl: './reviews.component.scss',
})
export class ReviewsComponent {
  reviews = input.required<IReview[]>();
  reviewFor = input.required<'chief' | 'meal'>();
  chief_id = input<IReview['chief_Id']>();
  meal_id = input<IReview['meal_id']>();

  constructor(
    private _ReviewService: ReviewService,
    private _tokenService: TokenService
  ) {}

  onNewReview(value: Pick<IReview, 'totalRate' | 'comment'>) {
    // console.log(value);

    this._ReviewService
      .addReview(this.chief_id() as IReview['chief_Id'], {
        ...value,
        customer_Id: this._tokenService.getUserId()!,
      })
      .subscribe({
        next: (res) => {
          console.log(res);
          this.reviews().unshift({
            ...res,
            customer_Name: this._tokenService.getPayload()?.name,
          });
        },
        error(err) {
          console.log(err);
        },
        complete: () => {
          // window.location.reload();
        },
      });

    // console.log(value)
  }
}
