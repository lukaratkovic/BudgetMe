import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Transaction} from "../models/transaction.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private http = inject(HttpClient);

  public getAll(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>('/api/transactions');
  }
}
