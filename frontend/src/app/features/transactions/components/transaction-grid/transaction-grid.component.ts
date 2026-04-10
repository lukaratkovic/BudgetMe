import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {TransactionService} from "../../services/transaction.service";
import {BankTransaction} from "../../models/bank-transaction.model";
import {Table, TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {DialogService} from "primeng/dynamicdialog";
import {TransactionDetailsComponent} from "../transaction-details/transaction-details.component";
import {NotificationService} from "../../../../core/services/notification.service";
import {ConfirmationService, FilterMatchMode, FilterService} from "primeng/api";
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {TagModule} from "primeng/tag";
import {TransactionImportInputComponent} from "../transaction-import-input/transaction-import-input.component";
import {DropdownModule} from "primeng/dropdown";
import {TransactionTypeService} from "../../services/transaction-type.service";
import {TransactionType} from "../../models/transaction-type.model";
import {CategoryService} from "../../../categories/services/category.service";
import {Category} from "../../../categories/models/category.model";
import {MultiSelectModule} from "primeng/multiselect";
import {InputNumberModule} from "primeng/inputnumber";
import {TooltipModule} from "primeng/tooltip";
import {InputTextModule} from "primeng/inputtext";
import {CalendarModule} from "primeng/calendar";
import {FormsModule} from "@angular/forms";
import {DateHelper} from "../../../../core/helpers/date-helper";
import {SelectButtonModule} from "primeng/selectbutton";

@Component({
  selector: 'app-transaction-grid',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, ConfirmDialogModule, TagModule, DropdownModule, MultiSelectModule, InputNumberModule, TooltipModule, InputTextModule, CalendarModule, FormsModule, SelectButtonModule],
  providers: [DialogService, ConfirmationService, DatePipe],
  templateUrl: './transaction-grid.component.html',
  styleUrls: ['./transaction-grid.component.sass']
})
export class TransactionGridComponent implements OnInit {
  private transactionService = inject(TransactionService);
  private transactionTypeService = inject(TransactionTypeService);
  private categoryService = inject(CategoryService);

  private notificationService = inject(NotificationService);
  private confirmationService = inject(ConfirmationService);
  private filterService = inject(FilterService);
  private dialog = inject(DialogService);

  private datePipe = inject(DatePipe);

  @ViewChild('transactionGrid') table!: Table;

  public transactions: BankTransaction[] = [];
  public transactionTypes: TransactionType[] = [];
  public categories: Category[] = [];

  ngOnInit() {
    this.registerCategoryFilter();
    this.getTransactions();
    this.getTransactionTypes();
    this.getCategories();
  }

  private registerCategoryFilter(): void {
    this.filterService.register('categoryIdFilter', (value: Category[], filter: string[]) => {
      if (!filter || filter.length === 0) return true;
      if (!value || value.length === 0) return false;
      return value.some(category => filter.includes(category.id));
    });
  }

  private getTransactions(): void {
    this.transactionService.getAll()
      .subscribe(transactions => this.transactions = transactions);
  }

  private getTransactionTypes(): void {
    this.transactionTypeService.getAll()
      .subscribe(transactionTypes => this.transactionTypes = transactionTypes);
  }

  private getCategories(): void {
    this.categoryService.getAll()
      .subscribe(categories => this.categories = categories);
  }

  // TODO: Combine addNew and onEdit into one method
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
    });
  }

  public confirmDelete(id: string): void {
    this.transactionService
      .delete(id)
      .subscribe({
        next: () => this.getTransactions(),
        error: (err) => this.notificationService.displayError(err),
      });
  }

  public import(): void {
    this.dialog.open(TransactionImportInputComponent, {
      header: 'Import transactions',
      width: '30%',
    }).onClose.subscribe((refresh) => {
      if (refresh)
        this.getTransactions();
    });
  }

  public getSeverity(transaction: BankTransaction): string {
    switch (transaction.type) {
      case 'Income':
        return 'success';
      case 'Expense':
        return 'warning';
      default: return 'danger';
    }
  }

  public getFullDate(date: Date): string {
    return this.datePipe.transform(date, 'EEEE, MMMM dd, y, h:mm a') ?? '';
  }

  public get balance(): number {
    return this.income - this.expense;
  }

  private get income(): number {
    return (this.table?.filteredValue ?? this.transactions)
      .filter(x => x.type == "Income")
      .reduce((sum, item) => sum + item.amount, 0);
  }

  private get expense(): number {
    return (this.table?.filteredValue ?? this.transactions)
      .filter(x => x.type == "Expense")
      .reduce((sum, item) => sum + item.amount, 0);
  }

  public get amountMatchModeOptions(): any[] {
    return [
      { label: '=', value: 'equals' },
      { label: '>', value: 'gt' },
      { label: '<', value: 'lt' },
      { label: '≥', value: 'gte' },
      { label: '≤', value: 'lte' }
    ]
  }
}
