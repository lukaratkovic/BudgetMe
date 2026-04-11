import { Routes} from "@angular/router";
import {
  GroupedTransactionsReportComponent
} from "./features/reporting/components/grouped-transactions-report/grouped-transactions-report.component";

export const routes: Routes = [
  {
    path: 'transactions',
    loadComponent: () =>
      import('./features/transactions/components/transaction-grid/transaction-grid.component')
        .then(m => m.TransactionGridComponent),
  },
  {
    path: 'categories',
    loadComponent: () =>
      import('./features/categories/components/category-grid/category-grid.component')
        .then(m => m.CategoryGridComponent),
  },
  {
    path: 'bindings',
    loadComponent: () =>
      import('./features/bindings/components/binding-grid/binding-grid.component')
        .then(m => m.BindingGridComponent),
  },
  {
    path: 'reports',
    children: [
      { path: 'grouped-transactions', component: GroupedTransactionsReportComponent }
    ]
  },
  {
    path: '',
    redirectTo: 'transactions',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: 'transactions',
  }
]
