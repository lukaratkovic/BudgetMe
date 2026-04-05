import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TransactionService} from "../../services/transaction.service";
import {BankTransaction} from "../../models/bank-transaction.model";
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {DialogService} from "primeng/dynamicdialog";
import {TransactionDetailsComponent} from "../transaction-details/transaction-details.component";
import {NotificationService} from "../../../../core/services/notification.service";

@Component({
  selector: 'app-transaction-grid',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule],
  providers: [DialogService],
  templateUrl: './transaction-grid.component.html',
  styleUrls: ['./transaction-grid.component.sass']
})
export class TransactionGridComponent implements OnInit {
  private transactionService = inject(TransactionService);
  private dialog = inject(DialogService);
  private notificationService = inject(NotificationService);

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

  public onDelete(transaction: BankTransaction): void {
    //TODO: Confirm
    this.transactionService
      .delete(transaction.id)
      .subscribe({
        next: () => this.getTransactions(),
        error: (err) => this.notificationService.displayError(err.error),
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
