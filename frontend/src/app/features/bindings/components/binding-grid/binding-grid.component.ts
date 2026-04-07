import {Component, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {BindingService} from "../../services/binding.service";
import {Binding} from "../../models/binding.model";
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {ConfirmationService} from "primeng/api";
import {take} from "rxjs";
import {DynamicDialogRef} from "primeng/dynamicdialog";
import {NotificationService} from "../../../../core/services/notification.service";

@Component({
  selector: 'app-binding-grid',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, ConfirmDialogModule],
  providers: [ConfirmationService],
  templateUrl: './binding-grid.component.html',
  styleUrls: ['./binding-grid.component.sass']
})
export class BindingGridComponent {
  private bindingService = inject(BindingService);
  private confirmationService = inject(ConfirmationService);
  private notificationService = inject(NotificationService);

  public bindings: Binding[] = [];

  constructor() {
    this.getBindings();
  }

  private getBindings(): void {
    this.bindingService.getAll()
      .subscribe(bindings => this.bindings = bindings);
  }

  public onDelete(binding: Binding): void {
    this.confirmationService.confirm({
      message: `Are you sure you want to delete binding ${binding.keyword} - ${binding.category}?`,
      header: 'Delete binding',
      icon: 'pi pi-exclamation-triangle',
      rejectButtonStyleClass: 'p-button-text',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.confirmDelete(binding.id)
    });
  }

  private confirmDelete(id: string): void {
    this.bindingService
      .delete(id)
      .pipe(take(1))
      .subscribe({
        next: () => this.getBindings(),
        error: (err) => this.notificationService.displayError(err)
      });
  }
}
