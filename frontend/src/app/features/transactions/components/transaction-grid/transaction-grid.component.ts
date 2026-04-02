import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TransactionService} from "../../services/transaction.service";
import {BankTransaction} from "../../models/bank-transaction.model";
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {DialogService} from "primeng/dynamicdialog";
import {TransactionDetailsComponent} from "../transaction-details/transaction-details.component";

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
}
