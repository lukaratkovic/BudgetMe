import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {TransactionService} from "../../transactions/services/transaction.service";
import {PerDayReportData, PerDayReportDto} from "../models/per-day.model";
import {DateHelper} from "../../../core/helpers/date-helper";

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private http = inject(HttpClient);
  private transactionService = inject(TransactionService);

  public getPerDayReportData(): Observable<PerDayReportData[]> /*TODO: Put right return type*/ {
    return this.http.get<PerDayReportDto[]>('/api/reports/per-day').pipe(
      map(dtos => dtos.map(dto => ({
        ...dto,
        date: new Date(DateHelper.parseLocalDate(dto.date)),
        transactions: dto.transactions.map(transaction => this.transactionService.mapToModel(transaction)),
      })))
    );
  }
}
