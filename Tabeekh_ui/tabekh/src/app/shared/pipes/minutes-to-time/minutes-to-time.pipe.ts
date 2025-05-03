import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'minutesToTime'
})
export class MinutesToTimePipe implements PipeTransform {

  transform(value: number, mode: 'display' | 'datetime' = 'display'): string {
    if (value == null || isNaN(value)) {
      return '00:00';
    }

    const hours = Math.floor(value / 60);
    const minutes = value % 60;

    const hoursStr = hours.toString().padStart(2, '0');
    const minutesStr = minutes.toString().padStart(2, '0');

    if (mode === 'datetime') {
      return `PT${hoursStr}H${minutesStr}M`;
    } else {
      // Normal display
      return `${hoursStr} ساعة  ${minutesStr} دقيقة `;
    }
  }

}
