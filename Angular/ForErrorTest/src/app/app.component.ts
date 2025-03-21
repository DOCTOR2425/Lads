import { Component } from '@angular/core';
import { ErrorPageComponent } from './components/error-page/error-page.component';

@Component({
  selector: 'app-root',
  imports: [ErrorPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'ForErrorTest';
}
