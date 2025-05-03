import { Component, input, InputSignal } from '@angular/core';
import IReview from '../../../../../core/models/IReview.model';
import { RatePipe } from '../../../../pipes/rate/rate.pipe';

@Component({
  selector: 'app-user-comments',
  imports: [RatePipe],
  templateUrl: './user-comments.component.html',
  styleUrl: './user-comments.component.scss'
})
export class UserCommentsComponent {
  review: InputSignal<IReview> = input.required<IReview>()
}
