import {inject, Injectable} from '@angular/core';
import {MessageService} from "primeng/api";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private messageService = inject(MessageService);

  public displayError(error: any): void {
    const message = error?.error?.message ?? error?.error ?? error;
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: message
    });
  }
}
