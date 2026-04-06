import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputNumberModule} from "primeng/inputnumber";
import {TransactionType} from "../../models/transaction-type.model";
import {TransactionTypeService} from "../../services/transaction-type.service";
import {DropdownModule} from "primeng/dropdown";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {ButtonModule} from "primeng/button";
import {TransactionService} from "../../services/transaction.service";
import {CreateBankTransactionDto, UpdateBankTransactionDto} from "../../models/bank-transaction.model";
import {CalendarModule} from "primeng/calendar";
import {InputTextareaModule} from "primeng/inputtextarea";
import {CategoryService} from "../../../categories/services/category.service";
import {Category} from "../../../categories/models/category.model";
import {NotificationService} from "../../../../core/services/notification.service";
import {take} from "rxjs";
import {MultiSelectModule} from "primeng/multiselect";

@Component({
  selector: 'app-transaction-details',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputNumberModule, DropdownModule, ButtonModule, CalendarModule, InputTextareaModule, MultiSelectModule],
  templateUrl: './transaction-details.component.html',
  styleUrls: ['./transaction-details.component.sass']
})
export class TransactionDetailsComponent implements OnInit {
  private formBuilder = inject(FormBuilder);
  public transactionTypeService = inject(TransactionTypeService);
  private dialogRef = inject(DynamicDialogRef);
  private transactionService = inject(TransactionService);
  private categoryService = inject(CategoryService);
  private notificationService = inject(NotificationService);
  private dialogConfig = inject(DynamicDialogConfig);

  public form: FormGroup;

  private id?: string;

  public transactionTypes: TransactionType[] = [];
  public categories: Category[] = [];

  constructor() {
    const now = new Date();
    this.form = this.formBuilder.group({
      id: null,
      transactionTypeId: [null, Validators.required],
      amount: [null, [Validators.required, Validators.min(0.01)]],
      transactionTime: [now, [Validators.required]],
      description: [null],
      categoryIds: [[], [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.transactionTypeService.getAll().subscribe(data => this.transactionTypes = data);
    this.loadEntity();

    this.form.controls["transactionTypeId"].valueChanges.subscribe((id) => this.getCategoriesByTransactionType(id));
  }

  private loadEntity(): void {
    const id = this.dialogConfig.data?.transactionId;
    if (!id) return;

    this.transactionService
      .get(id)
      .pipe(take(1))
      .subscribe({
        next: (data) => {
          this.form.patchValue(data);
          this.id = data.id;
        },
        error: (err) => this.notificationService.displayError(err),
      });
  }

  private getCategoriesByTransactionType(transactionTypeId: string): void {
    this.categoryService
      .getByTransactionType(transactionTypeId)
      .subscribe(data => {
        this.categories = data;
        // TODO: Currently clears categories on load, need to fix
        this.form.controls["categoryIds"].setValue([]);
        this.form.controls["categoryIds"].markAsPristine();
      });
  }

  public submit(): void {
    if (this.form.invalid) {
      Object.values(this.form.controls).forEach(control => {
        control.markAsTouched();
        control.markAsDirty();
      });
      return;
    }

    const dto = this.form.getRawValue();

    if (this.id) {
      this.update(dto as UpdateBankTransactionDto);
    } else {
      this.insert(dto as CreateBankTransactionDto);
    }
  }

  public insert(dto: CreateBankTransactionDto): void {
    this.transactionService
      .save(dto)
      .subscribe({
        next: () => this.dialogRef.close({refresh: true}),
        error: (err) => this.notificationService.displayError(err)
      });
  }

  public update(dto: UpdateBankTransactionDto): void {
    this.transactionService
      .update(this.id as string, dto)
      .subscribe({
        next: () => this.dialogRef.close({refresh: true}),
        error: (err) => this.notificationService.displayError(err)
      });
  }

  public close(): void {
    this.dialogRef.close();
  }
}
