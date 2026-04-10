import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {
  BankTransaction,
  BankTransactionDto,
  CreateBankTransactionDto,
  UpdateBankTransactionDto
} from "../models/bank-transaction.model";
import {map, Observable} from "rxjs";
import {DateHelper} from "../../../core/helpers/date-helper";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private http = inject(HttpClient);

  public getAll(): Observable<BankTransaction[]> {
    return this.http.get<BankTransactionDto[]>('/api/transaction').pipe(
      map(transactions => transactions.map(this.mapToModel))
    );
  }

  public get(id: string): Observable<BankTransaction> {
    return this.http.get<BankTransactionDto>(`/api/transaction/${id}`).pipe(
      map(transaction => this.mapToModel(transaction))
    );
  }

  public save(data: CreateBankTransactionDto): Observable<CreateBankTransactionDto> {
    data.transactionTime = DateHelper.toLocalDateTimeString(data.transactionTime as Date);
    return this.http.post<CreateBankTransactionDto>('/api/transaction', data);
  }

  public update(id: string, data: UpdateBankTransactionDto): Observable<void> {
    data.transactionTime = DateHelper.toLocalDateTimeString(data.transactionTime as Date);
    return this.http.put<void>(`/api/transaction/${id}`, data);
  }

  public delete(id: string): Observable<void> {
    return this.http.delete<void>(`/api/transaction/${id}`);
  }

  public mapToModel(dto: BankTransactionDto): BankTransaction {
    return {
      ...dto,
      transactionTime: new Date(DateHelper.parseLocalDate(dto.transactionTime)),
    }
  }
}
