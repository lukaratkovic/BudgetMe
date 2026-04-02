import {Component} from '@angular/core';
import {RouterOutlet} from "@angular/router";
import {NavbarComponent} from "./core/layout/navbar/navbar.component";
import {ToastModule} from "primeng/toast";
import {MessageService} from "primeng/api";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, ToastModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
})
export class AppComponent {

}
