import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TransactionService} from "../../services/transaction.service";
import {Transaction} from "../../models/transaction.model";
import {TableModule} from "primeng/table";

@Component({
  selector: 'app-transaction-grid',
  standalone: true,
  imports: [CommonModule, TableModule],
  templateUrl: './transaction-grid.component.html',
  styleUrls: ['./transaction-grid.component.sass']
})
export class TransactionGridComponent implements OnInit {
  private transactionService = inject(TransactionService);
  public transactions: Transaction[] = [];

  ngOnInit() {
    this.getTransactions();
  }

  private getTransactions(): void {
    this.transactionService.getAll()
      .subscribe(transactions => this.transactions = transactions);
  }
}
