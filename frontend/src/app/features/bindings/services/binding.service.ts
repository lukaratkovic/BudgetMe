import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Binding, BindingDto, CreateBindingDto, UpdateBindingDto} from "../models/binding.model";
import {map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BindingService {
  private http = inject(HttpClient);

  public getAll(): Observable<Binding[]> {
    return this.http.get<BindingDto[]>('/api/binding').pipe(
      map(bindings => bindings.map(this.mapToModel))
    )
  }

  public get(id: string): Observable<Binding> {
    return this.http.get<Binding>(`/api/binding/${id}`);
  }

  public delete(id: string): Observable<void> {
    return this.http.delete<void>(`/api/binding/${id}`);
  }

  public save(data: CreateBindingDto): Observable<CreateBindingDto> {
    return this.http.post<CreateBindingDto>(`/api/binding`, data);
  }

  public update(id: string, data: UpdateBindingDto): Observable<void> {
    return this.http.put<void>(`/api/binding/${id}`, data);
  }

  private mapToModel(dto: BindingDto): Binding {
    return {
      ...dto
    }
  }
}
