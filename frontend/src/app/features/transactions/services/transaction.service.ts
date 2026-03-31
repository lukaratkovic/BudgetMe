import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BankTransaction, SaveTransactionDto} from "../models/bank-transaction.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private http = inject(HttpClient);

  public getAll(): Observable<BankTransaction[]> {
    return this.http.get<BankTransaction[]>('/api/transactions');
  }

  // TODO: Change this from any to the correct type
  public save(data: SaveTransactionDto): Observable<any> {
    return this.http.post<any>('/api/transaction', data);
  }
}
