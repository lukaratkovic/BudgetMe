import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BankTransaction, CreateBankTransactionDto, UpdateBankTransactionDto} from "../models/bank-transaction.model";
import {map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private http = inject(HttpClient);

  public getAll(): Observable<BankTransaction[]> {
    return this.http.get<BankTransaction[]>('/api/transaction').pipe(
      map(res => res.map((transaction) => ({
          ...transaction,
          transactionTime: new Date(transaction.transactionTime),
        }))
      )
    );
  }

  public get(id: string): Observable<BankTransaction> {
    return this.http.get<BankTransaction>(`/api/transaction/${id}`).pipe(
      map(transaction => ({
        ...transaction,
        transactionTime: new Date(transaction.transactionTime),
      }))
    );
  }

  public save(data: CreateBankTransactionDto): Observable<CreateBankTransactionDto> {
    return this.http.post<CreateBankTransactionDto>('/api/transaction', data);
  }

  public update(data: UpdateBankTransactionDto): Observable<void> {
    return this.http.put<void>(`/api/transaction/${data.id}`, data);
  }

  public delete(id: string): Observable<void> {
    return this.http.delete<void>(`/api/transaction/${id}`);
  }
}
