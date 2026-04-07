import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputTextModule} from "primeng/inputtext";
import {TransactionTypeService} from "../../../transactions/services/transaction-type.service";
import {TransactionType} from "../../../transactions/models/transaction-type.model";
import {Category} from "../../../categories/models/category.model";
import {CategoryService} from "../../../categories/services/category.service";
import {DropdownModule} from "primeng/dropdown";
import {FormHelper} from "../../../../core/helpers/form-helpers";
import {BindingService} from "../../services/binding.service";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {NotificationService} from "../../../../core/services/notification.service";
import {ButtonModule} from "primeng/button";
import {CreateBindingDto, UpdateBindingDto} from "../../models/binding.model";

@Component({
  selector: 'app-binding-details',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, DropdownModule, ButtonModule],
  templateUrl: './binding-details.component.html',
  styleUrls: ['./binding-details.component.sass']
})
export class BindingDetailsComponent implements OnInit {
  private formBuilder = inject(FormBuilder);
  private transactionTypeService = inject(TransactionTypeService);
  private categoryService = inject(CategoryService);
  private bindingService = inject(BindingService);
  private dialogRef = inject(DynamicDialogRef);
  private dialogConfig = inject(DynamicDialogConfig);
  private notificationService = inject(NotificationService);

  public transactionTypes: TransactionType[] = [];
  public categories: Category[] = [];

  public form: FormGroup;

  private id?: string;

  private isInitialLoad = false;

  constructor() {
    this.form = this.formBuilder.group({
      keyword: ['', Validators.required],
      transactionTypeId: [null, Validators.required],
      categoryId: [null, Validators.required]
    });
  }

  ngOnInit() {
    this.transactionTypeService.getAll()
      .subscribe(data => this.transactionTypes = data);
    this.loadEntity();

    this.form.controls["transactionTypeId"].valueChanges.subscribe(() => this.onTransactionTypeChange())
  }

  private loadEntity(): void {
    const id = this.dialogConfig.data?.bindingId;
    if (!id) return;

    this.isInitialLoad = true;
    this.bindingService.get(id)
      .subscribe({
        next: data => {
          this.id = data.id;
          this.form.patchValue(data);
          this.isInitialLoad = false;
        },
        error: err => this.notificationService.displayError(err),
      });
  }

  private onTransactionTypeChange(): void {
    const transactionTypeId = this.form.controls["transactionTypeId"].value;
    const categoryControl = this.form.controls["categoryId"];
    // Saving the state of initialLoad before service call because it might change by the time subscribe returns value
    const clear = !this.isInitialLoad;
    this.categoryService
      .getByTransactionType(transactionTypeId)
      .subscribe(data => {
        this.categories = data;
        if (!clear) return;
        categoryControl.setValue(null);
        categoryControl.updateValueAndValidity();
      });
  }

  public submit(): void {
    if (this.form.invalid) {
      FormHelper.markAsDirty(this.form);
      return;
    }

    const dto = this.form.getRawValue();

    if (this.id) {
      this.update(dto as UpdateBindingDto);
    } else {
      this.insert(dto as CreateBindingDto);
    }
  }

  private update(dto: UpdateBindingDto): void {
    this.bindingService.update(this.id as string, dto)
      .subscribe({
        next: data => this.dialogRef.close(true),
        error: err => this.notificationService.displayError(err),
      });
  }

  private insert(dto: CreateBindingDto): void {
    this.bindingService.save(dto)
      .subscribe({
        next: () => this.dialogRef.close(true),
        error: err => this.notificationService.displayError(err),
      });
  }

  public close(): void {
    this.dialogRef.close();
  }
}
