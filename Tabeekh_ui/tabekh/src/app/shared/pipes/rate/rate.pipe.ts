import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'rate'
})
export class RatePipe implements PipeTransform {

  transform(value: number): string {
    let fullStars = Math.floor(value);
    let halfStar = value % 1 >= 0.5;
    let emptyStars = 5 - fullStars - (halfStar ? 1 : 0);

    let starsHtml = '';

    for (let i = 0; i < fullStars; i++) {
      starsHtml += '<i class="fa fa-star"></i>';
    }

    if (halfStar) {
      starsHtml += '<i class="fa fa-star-half-o flip-X"></i>';
    }

    for (let i = 0; i < emptyStars; i++) {
      starsHtml += '<i class="fa fa-star-o"></i>';
    }

    return starsHtml;
  }

}
