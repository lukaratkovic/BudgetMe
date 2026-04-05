import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputTextModule} from "primeng/inputtext";
import {InputTextareaModule} from "primeng/inputtextarea";
import {TransactionTypeService} from "../../../transactions/services/transaction-type.service";
import {TransactionType} from "../../../transactions/models/transaction-type.model";
import {DropdownModule} from "primeng/dropdown";
import {ButtonModule} from "primeng/button";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {CreateCategoryDto, UpdateCategoryDto} from "../../models/category.model";
import {CategoryService} from "../../services/category.service";
import {NotificationService} from "../../../../core/services/notification.service";

@Component({
  selector: 'app-category-details',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, InputTextareaModule, DropdownModule, ButtonModule],
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.sass']
})
export class CategoryDetailsComponent implements OnInit {
  private formBuilder = inject(FormBuilder);
  private transactionTypeService = inject(TransactionTypeService);
  private categoryService = inject(CategoryService);
  private dialogRef = inject(DynamicDialogRef);
  private notificationService = inject(NotificationService);
  private dialogConfig = inject(DynamicDialogConfig);

  public form: FormGroup;

  private id?: string;

  public transactionTypes: TransactionType[] = [];

  constructor() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      description: '',
      transactionTypeId: [null, Validators.required],
    });
  }

  ngOnInit() {
    this.transactionTypeService.getAll()
      .subscribe(data => this.transactionTypes = data);
    this.loadEntity();
  }

  private loadEntity(): void {
    const id = this.dialogConfig.data?.categoryId;
    if (!id) return;

    this.categoryService.get(id)
      .subscribe(data => {
        this.form.patchValue(data);
        this.id = data.id;
      });
  }

  public submit(): void {
    if (this.form.invalid)
      return;

    const dto = this.form.getRawValue();

    if (this.id) {
      this.update(dto as UpdateCategoryDto);
    } else {
      this.insert(dto as CreateCategoryDto);
    }
  }

  private update(dto: UpdateCategoryDto): void {
    this.categoryService
      .update(this.id as string, dto)
      .subscribe({
        next: () => this.dialogRef.close(true),
        error: (err) => this.notificationService.displayError(err),
      });
  }

  private insert(dto: CreateCategoryDto): void {
    this.categoryService
      .save(dto)
      .subscribe({
        next: () => this.dialogRef.close(true),
        error: (err) => this.notificationService.displayError(err),
      });
  }

  public close(): void {
    this.dialogRef.close();
  }
}
