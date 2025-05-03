import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'chieftitle'
})
export class ChieftitlePipe implements PipeTransform {

  transform(value: string | undefined): string {
    return "<sup> الشيف </sup>" + value;
  }

}
