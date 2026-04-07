import {Component, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {BindingService} from "../../services/binding.service";
import {Binding} from "../../models/binding.model";
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {ConfirmationService} from "primeng/api";
import {take} from "rxjs";
import {NotificationService} from "../../../../core/services/notification.service";
import {DialogService} from "primeng/dynamicdialog";
import {BindingDetailsComponent} from "../binding-details/binding-details.component";

@Component({
  selector: 'app-binding-grid',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, ConfirmDialogModule],
  providers: [ConfirmationService, DialogService],
  templateUrl: './binding-grid.component.html',
  styleUrls: ['./binding-grid.component.sass']
})
export class BindingGridComponent {
  private bindingService = inject(BindingService);
  private confirmationService = inject(ConfirmationService);
  private notificationService = inject(NotificationService);
  private dialog = inject(DialogService);

  public bindings: Binding[] = [];

  constructor() {
    this.getBindings();
  }

  private getBindings(): void {
    this.bindingService.getAll()
      .subscribe(bindings => this.bindings = bindings);
  }

  public addNew(): void {
    this.dialog.open(BindingDetailsComponent, {
      header: 'New binding',
      width: '30%',
    }).onClose.subscribe(refresh => {
      if (refresh)
        this.getBindings();
    });
  }

  public onEdit(binding: Binding): void {
    this.dialog.open(BindingDetailsComponent, {
      header: 'Edit binding',
      width: '30%',
      data: {
        bindingId: binding.id
      }
    }).onClose.subscribe((refresh) => {
      if (refresh)
        this.getBindings();
    });
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
