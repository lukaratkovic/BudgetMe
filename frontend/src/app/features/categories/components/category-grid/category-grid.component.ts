import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {CategoryService} from "../../services/category.service";
import {Category} from "../models/category.model";
import {TableModule} from "primeng/table";

@Component({
  selector: 'app-category-grid',
  standalone: true,
  imports: [CommonModule, TableModule],
  templateUrl: './category-grid.component.html',
  styleUrls: ['./category-grid.component.sass']
})
export class CategoryGridComponent implements OnInit {
  private categoryService = inject(CategoryService);

  public categories: Category[] = [];

  ngOnInit() {
    this.categoryService.getAll()
      .subscribe(categories => this.categories = categories);
  }

}
