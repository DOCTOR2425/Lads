import { Component } from '@angular/core';
import { SignalRPageComponent } from './components/signal-r-page/signal-r-page.component';

@Component({
  selector: 'app-root',
  imports: [SignalRPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'AngularPractice';
}
