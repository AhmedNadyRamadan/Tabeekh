import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import IChiefDefault from '../../../core/models/IChief.model';
import { ChieftitlePipe } from '../../pipes/chieftitle/chieftitle.pipe';
import { RatePipe } from '../../pipes/rate/rate.pipe';

@Component({
  selector: 'app-chief-card',
  imports: [RatePipe, ChieftitlePipe, RouterLink],
  templateUrl: './chief-card.component.html',
  styleUrl: './chief-card.component.scss',
})
export class ChiefCardComponent {
  chief = input.required<IChiefDefault>();

  get chiefImage(): string {
    return !this.chief()!.photo
      ? 'logo.jpeg'
      : `data:image/*;base64, ${this.chief()!.photo}`;
  }
}
