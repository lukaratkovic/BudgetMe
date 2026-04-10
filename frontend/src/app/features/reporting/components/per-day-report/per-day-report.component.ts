import {Component, inject, signal} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReportService} from "../../services/report.service";
import {HttpClient} from "@angular/common/http";
import {PerDayReportData} from "../../models/per-day.model";
import {TableModule} from "primeng/table";

@Component({
  selector: 'app-per-day-report',
  standalone: true,
  imports: [CommonModule, TableModule],
  templateUrl: './per-day-report.component.html',
  styleUrls: ['./per-day-report.component.sass']
})
export class PerDayReportComponent {
  private reportService = inject(ReportService);

  public rows = signal<PerDayReportData[]>([]);

  constructor() {
    this.reportService.getPerDayReportData()
      .subscribe(data => this.rows.set(data));
  }
}
