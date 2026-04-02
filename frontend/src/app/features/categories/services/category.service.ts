import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Category} from "../components/models/category.model";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private http = inject(HttpClient);

  public getAll(): Observable<Category[]> {
    return this.http.get<Category[]>(`/api/categories`);
  }

  public getByTransactionType(transactionTypeId?: string): Observable<Category[]> {
    let params = new HttpParams();
    if (transactionTypeId) {
      params = params.set('transactionTypeId', transactionTypeId);
    }
    return this.http.get<Category[]>(`/api/categories`, { params });
  }
}
