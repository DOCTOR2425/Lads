import { Component } from '@angular/core';
import { PostNullTestComponent } from './post-null-test/post-null-test.component';

@Component({
  selector: 'app-root',
  imports: [PostNullTestComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'ForTest';
}
