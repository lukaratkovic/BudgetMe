import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FileUpload, FileUploadModule} from "primeng/fileupload";

@Component({
  selector: 'app-transaction-import-input',
  standalone: true,
  imports: [CommonModule, FileUploadModule],
  templateUrl: './transaction-import-input.component.html',
  styleUrls: ['./transaction-import-input.component.sass']
})
export class TransactionImportInputComponent {

}
