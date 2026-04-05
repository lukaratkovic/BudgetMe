import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {CategoryService} from "../../services/category.service";
import {Category} from "../../models/category.model";
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {DialogService} from "primeng/dynamicdialog";
import {CategoryDetailsComponent} from "../category-details/category-details.component";

@Component({
  selector: 'app-category-grid',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule],
  providers: [DialogService],
  templateUrl: './category-grid.component.html',
  styleUrls: ['./category-grid.component.sass']
})
export class CategoryGridComponent implements OnInit {
  private categoryService = inject(CategoryService);
  private dialog = inject(DialogService);

  public categories: Category[] = [];

  ngOnInit() {
    this.getCategories();
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

  private getCategories(): void {
    this.categoryService.getAll()
      .subscribe(categories => this.categories = categories);
  }

}
