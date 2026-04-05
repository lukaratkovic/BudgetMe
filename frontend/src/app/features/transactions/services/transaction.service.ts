import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BankTransaction, CreateBankTransactionDto} from "../models/bank-transaction.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private http = inject(HttpClient);

  public getAll(): Observable<BankTransaction[]> {
    return this.http.get<BankTransaction[]>('/api/transaction');
  }

  public save(data: CreateBankTransactionDto): Observable<CreateBankTransactionDto> {
    return this.http.post<CreateBankTransactionDto>('/api/transaction', data);
  }

  public delete(id: string): Observable<void> {
    return this.http.delete<void>(`/api/transaction/${id}`);
  }
}
