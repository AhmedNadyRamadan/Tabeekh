import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  constructor(private _MessageService: MessageService) {}

  onSuccessToaster(msg: string) {
    this._MessageService.add({
      severity: 'success',
      summary: 'Success',
      detail: msg,
    });
  }

  onDangerToaster(msg: string) {
    this._MessageService.add({
      severity: 'error',
      summary: 'Error',
      detail: msg,
    });
  }
  onInfoToaster(msg: string, options?: any) {
    this._MessageService.add({
      ...options,
      severity: 'info',
      detail: msg,
      sticky: true,
    });
  }
  clear() {
    this._MessageService.clear();
  }
}
