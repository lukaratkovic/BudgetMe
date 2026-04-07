import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Binding, BindingDto} from "../models/binding.model";
import {map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BindingService {
  private http = inject(HttpClient);

  public getAll(): Observable<Binding[]> {
    return this.http.get<BindingDto[]>("/api/binding").pipe(
      map(bindings => bindings.map(this.mapToModel))
    )
  }

  private mapToModel(dto: BindingDto): Binding {
    return {
      ...dto
    }
  }
}
