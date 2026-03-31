import {Component, inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RouterOutlet} from "@angular/router";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
})
export class AppComponent implements OnInit {
  title = 'budget-me-client';
  private http = inject(HttpClient);

  ngOnInit(): void {
    this.http.get('/api/transactions').subscribe(console.log)
  }

}
