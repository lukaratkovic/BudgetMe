import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputNumberModule} from "primeng/inputnumber";
import {TransactionType} from "../../models/transaction-type.model";
import {TransactionTypeService} from "../../services/transaction-type.service";
import {DropdownModule} from "primeng/dropdown";
import {DynamicDialogRef} from "primeng/dynamicdialog";
import {ButtonModule} from "primeng/button";
import {TransactionService} from "../../services/transaction.service";
import {SaveTransactionDto} from "../../models/bank-transaction.model";
import {CalendarModule} from "primeng/calendar";
import {InputTextareaModule} from "primeng/inputtextarea";

@Component({
  selector: 'app-transaction-details',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputNumberModule, DropdownModule, ButtonModule, CalendarModule, InputTextareaModule],
  templateUrl: './transaction-details.component.html',
  styleUrls: ['./transaction-details.component.sass']
})
export class TransactionDetailsComponent implements OnInit {
  private formBuilder = inject(FormBuilder);
  public transactionTypeService = inject(TransactionTypeService);
  private dialogRef = inject(DynamicDialogRef);
  private transactionService = inject(TransactionService);

  public form: FormGroup;

  public transactionTypes: TransactionType[] = [];

  constructor() {
    const now = new Date();
    this.form = this.formBuilder.group({
      typeId: [null, Validators.required],
      amount: [0, [Validators.required, Validators.min(0)]],
      transactionTime: [now, [Validators.required]],
      description: [null],
    });
  }

  ngOnInit(): void {
    this.transactionTypeService.getAll().subscribe(data => this.transactionTypes = data);
  }

  public submit(): void {
    if (this.form.invalid)
      return;

    // TODO: See if I really have to create a whole new object here or I can cast form data somehow
    const dto: SaveTransactionDto = {
      TransactionTypeId: this.form.controls['typeId'].value,
      Amount: this.form.controls['amount'].value,
      TransactionTime: this.form.controls['transactionTime'].value,
      Description: this.form.controls['description'].value,
    };

    this.transactionService
      .save(dto)
      .subscribe({
        next: () => this.dialogRef.close({refresh: true}),
        // TODO: Handle errors, need some popup
      })
  }

  public close(): void {
    this.dialogRef.close();
  }
}
