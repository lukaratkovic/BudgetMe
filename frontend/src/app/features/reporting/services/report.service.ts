import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {TransactionService} from "../../transactions/services/transaction.service";
import {PerDayReportData, PerDayReportDto} from "../models/per-day.model";
import {DateHelper} from "../../../core/helpers/date-helper";
import {DayData, GroupedTransactionsTreeNode, MonthData, YearData} from "../models/grouped-transactions-report.model";
import {TreeNode} from "primeng/api";
import {BankTransactionDto} from "../../transactions/models/bank-transaction.model";
import {TransactionTypeHelper} from "../../../core/helpers/transaction-type-helper";
import {DatePipe} from "@angular/common";

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  private http = inject(HttpClient);
  private transactionService = inject(TransactionService);
  private datePipe = new DatePipe('en-US');

  public getPerDayReportData(): Observable<PerDayReportData[]> /*TODO: Put right return type*/ {
    return this.http.get<PerDayReportDto[]>('/api/reports/per-day').pipe(
      map(dtos => dtos.map(dto => ({
        ...dto,
        date: new Date(DateHelper.parseLocalDate(dto.date)),
        transactions: dto.transactions.map(transaction => this.transactionService.mapToModel(transaction)),
      })))
    );
  }

  public getGroupedTransactionsData(): Observable<GroupedTransactionsTreeNode[]> {
    return this.http.get<YearData[]>('/api/reports/grouped-transactions').pipe(
      map(d => d.map(d => this.yearDataToTreeNode(d)))
    )
  }

  private yearDataToTreeNode(yearData: YearData): GroupedTransactionsTreeNode {
    return {
      data: {
        label: yearData.year.toString(),
        income: yearData.income,
        expense: yearData.expense,
        balance: yearData.balance,
      },
      children: yearData.monthData.map(m => this.monthDataToTreeNode(m))
    }
  }

  private monthDataToTreeNode(monthData: MonthData): TreeNode {
    return {
      data: {
        label: monthData.month.toString(),
        income: monthData.income,
        expense: monthData.expense,
        balance: monthData.balance,
      },
      children: monthData.dayData.map(d => this.dayDataToTreeNode(d))
    }
  }

  private dayDataToTreeNode(dayData: DayData): TreeNode {
    return {
      data: {
        label: this.datePipe.transform(dayData.date, 'longDate'),
        income: dayData.income,
        expense: dayData.expense,
        balance: dayData.balance,
      },
      children: dayData.transactions.map(transaction => this.transactionToTreeNode(transaction)),
    }
  }

  private transactionToTreeNode(transaction: BankTransactionDto): TreeNode {
    const amounts = TransactionTypeHelper.getIncomeExpenseBalance(transaction);
    return {
      data: {
        label: transaction.description,
        income: amounts.income,
        expense: amounts.expense,
        balance: amounts.balance,
      }
    }
  }
}
