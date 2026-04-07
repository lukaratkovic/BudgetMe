import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MenubarModule} from "primeng/menubar";
import {MenuItem} from "primeng/api";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, MenubarModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass']
})
export class NavbarComponent implements OnInit {
  items!: MenuItem[];

  ngOnInit() {
    this.items = [
      {
        label: 'My Transactions',
        icon: 'pi pi-credit-card',
        routerLink: '/transactions'
      },
      {
        label: 'Transaction Categories',
        icon: 'pi pi-folder-open',
        routerLink: '/categories'
      },
      {
        label: 'Bindings',
        icon: 'pi pi-sliders-h',
        routerLink: '/bindings'
      }
    ]
  }
}
