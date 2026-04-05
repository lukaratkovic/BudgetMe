import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {CategoryService} from "../../services/category.service";
import {Category} from "../../models/category.model";
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {DialogService} from "primeng/dynamicdialog";
import {CategoryDetailsComponent} from "../category-details/category-details.component";
import {ConfirmationService} from "primeng/api";
import {NotificationService} from "../../../../core/services/notification.service";
import {ConfirmDialogModule} from "primeng/confirmdialog";

@Component({
  selector: 'app-category-grid',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, ConfirmDialogModule],
  providers: [DialogService, ConfirmationService],
  templateUrl: './category-grid.component.html',
  styleUrls: ['./category-grid.component.sass']
})
export class CategoryGridComponent implements OnInit {
  private categoryService = inject(CategoryService);
  private dialog = inject(DialogService);
  private confirmationService = inject(ConfirmationService);
  private notificationService = inject(NotificationService);

  public categories: Category[] = [];

  ngOnInit() {
    this.getCategories();
  }

  private getCategories(): void {
    this.categoryService.getAll()
      .subscribe(categories => this.categories = categories);
  }

  public addNew(): void {
    this.dialog.open(CategoryDetailsComponent, {
      header: 'New category',
      width: '30%',
    }).onClose.subscribe((refresh) => {
      if (refresh)
        this.getCategories();
    });
  }

  public onDelete(category: Category): void {
    this.confirmationService.confirm({
      message: `Are you sure you want to delete category '${category.name}'?`,
      header: 'Delete category',
      icon: 'pi pi-exclamation-triangle',
      rejectButtonStyleClass: 'p-button-text',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.confirmDelete(category.id)
    });
  }

  private confirmDelete(id: string): void {
    this.categoryService
      .delete(id)
      .subscribe({
        next: () => this.getCategories(),
        error: err => this.notificationService.displayError(err),
      });
  }

}
