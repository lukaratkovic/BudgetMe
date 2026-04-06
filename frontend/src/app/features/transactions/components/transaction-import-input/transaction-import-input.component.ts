import {Component, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FileUpload, FileUploadModule} from "primeng/fileupload";
import {DynamicDialogRef} from "primeng/dynamicdialog";
import {NotificationService} from "../../../../core/services/notification.service";

@Component({
  selector: 'app-transaction-import-input',
  standalone: true,
  imports: [CommonModule, FileUploadModule],
  templateUrl: './transaction-import-input.component.html',
  styleUrls: ['./transaction-import-input.component.sass']
})
export class TransactionImportInputComponent {
  private dialogRef = inject(DynamicDialogRef);
  private notificationService = inject(NotificationService);

  onUpload(event: any): void {
    this.dialogRef.close(true);
  }

  onError(event: any): void {
    let message = event.error.error.join('\n');
    this.notificationService.displayError(message);
  }
}
