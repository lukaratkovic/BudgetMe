import {Component, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReportService} from "../../services/report.service";
import {GroupedTransactionsTreeNode, YearData} from "../../models/grouped-transactions-report.model";
import {TreeTableModule} from "primeng/treetable";
import {ButtonModule} from "primeng/button";
import {TreeNode} from "primeng/api";
import {TooltipModule} from "primeng/tooltip";

@Component({
  selector: 'app-grouped-transactions-report',
  standalone: true,
  imports: [CommonModule, TreeTableModule, ButtonModule, TooltipModule],
  templateUrl: './grouped-transactions-report.component.html',
  styleUrls: ['./grouped-transactions-report.component.sass']
})
export class GroupedTransactionsReportComponent {
  private reportService = inject(ReportService);

  public data: TreeNode[] = [];

  private readonly colors = ['Silver', 'LightGray', 'Gainsboro', 'GhostWhite'];

  constructor() {
    this.reportService.getGroupedTransactionsData()
      .subscribe(data => this.data = data);
  }

  public toggleExpand(expand: boolean) {
    this.data.forEach(node => this.toggleRecursive(node, expand));
    this.data = [...this.data];
  }

  private toggleRecursive(node: TreeNode, expand: boolean): void {
    node.expanded = expand;
    if (node.children) {
      node.children.forEach(child => this.toggleRecursive(child, expand));
    }
  }

  public getRowColor(level: number): string {
    return this.colors[level];
  }

  public getFontWeight(level: number, amount: number): string {
    return level == 3 && amount != 0 ? 'bold' : 'normal';
  }
}
