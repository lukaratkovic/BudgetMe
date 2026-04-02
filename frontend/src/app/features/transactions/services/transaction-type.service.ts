import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TransactionType} from "../models/transaction-type.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TransactionTypeService {
  private http = inject(HttpClient);

  public getAll(): Observable<TransactionType[]> {
    return this.http.get<TransactionType[]>("api/transactionType");
  }
}
