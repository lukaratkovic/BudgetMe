import {Component, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReportService} from "../../services/report.service";
import {GroupedTransactionsTreeNode, YearData} from "../../models/grouped-transactions-report.model";
import {TreeTableModule} from "primeng/treetable";

@Component({
  selector: 'app-grouped-transactions-report',
  standalone: true,
  imports: [CommonModule, TreeTableModule],
  templateUrl: './grouped-transactions-report.component.html',
  styleUrls: ['./grouped-transactions-report.component.sass']
})
export class GroupedTransactionsReportComponent {
  private reportService = inject(ReportService);

  public data: GroupedTransactionsTreeNode[] = [];

  constructor() {
    this.reportService.getGroupedTransactionsData()
      .subscribe(data => this.data = data);
  }
}
