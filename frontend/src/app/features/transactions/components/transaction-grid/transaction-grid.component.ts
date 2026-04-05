import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TransactionService} from "../../services/transaction.service";
import {BankTransaction} from "../../models/bank-transaction.model";
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {DialogService} from "primeng/dynamicdialog";
import {TransactionDetailsComponent} from "../transaction-details/transaction-details.component";
import {NotificationService} from "../../../../core/services/notification.service";
import {ConfirmationService} from "primeng/api";
import {ConfirmDialogModule} from "primeng/confirmdialog";

@Component({
  selector: 'app-transaction-grid',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, ConfirmDialogModule],
  providers: [DialogService, ConfirmationService],
  templateUrl: './transaction-grid.component.html',
  styleUrls: ['./transaction-grid.component.sass']
})
export class TransactionGridComponent implements OnInit {
  private transactionService = inject(TransactionService);
  private dialog = inject(DialogService);
  private notificationService = inject(NotificationService);
  private confirmationService = inject(ConfirmationService);

  public transactions: BankTransaction[] = [];

  ngOnInit() {
    this.getTransactions();
  }

  private getTransactions(): void {
    this.transactionService.getAll()
      .subscribe(transactions => this.transactions = transactions);
  }

  public addNew(): void {
    this.dialog.open(TransactionDetailsComponent, {
      header: 'New transaction',
      width: '30%'
    }).onClose.subscribe((refresh) => {
      if (refresh)
        this.getTransactions();
    });
  }

  public onEdit(transaction: BankTransaction): void {
    this.dialog.open(TransactionDetailsComponent, {
      header: 'Edit transaction',
      width: '30%',
      data: {
        transactionId: transaction.id
      }
    }).onClose.subscribe((refresh) => {
      if (refresh)
        this.getTransactions();
    });
  }

  public onDelete(transaction: BankTransaction): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this transaction?',
      header: 'Delete transaction',
      icon: 'pi pi-exclamation-triangle',
      rejectButtonStyleClass: 'p-button-text',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.confirmDelete(transaction.id)
    })
  }

  public confirmDelete(id: string): void {
    this.transactionService
      .delete(id)
      .subscribe({
        next: () => this.getTransactions(),
        error: (err) => this.notificationService.displayError(err),
      });
  }

  public get balance(): number {
    return this.income - this.expense;
  }

  private get income(): number {
    return this.transactions
      .filter(x => x.type == "Income")
      .reduce((sum, item) => sum + item.amount, 0);
  }

  private get expense(): number {
    return this.transactions
      .filter(x => x.type == "Expense")
      .reduce((sum, item) => sum + item.amount, 0);
  }
}
