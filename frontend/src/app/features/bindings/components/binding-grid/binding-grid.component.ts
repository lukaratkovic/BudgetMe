import {Component, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {BindingService} from "../../services/binding.service";
import {Binding} from "../../models/binding.model";
import {TableModule} from "primeng/table";

@Component({
  selector: 'app-binding-grid',
  standalone: true,
  imports: [CommonModule, TableModule],
  templateUrl: './binding-grid.component.html',
  styleUrls: ['./binding-grid.component.sass']
})
export class BindingGridComponent {
  private bindingService = inject(BindingService);

  public bindings: Binding[] = [];

  constructor() {
    this.bindingService.getAll()
      .subscribe(bindings => this.bindings = bindings);
  }


}
