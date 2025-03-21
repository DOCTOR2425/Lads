import { Component } from '@angular/core';
import { FolderPageComponent } from './pages/folder-page/folder-page.component';

@Component({
  selector: 'app-root',
  imports: [FolderPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'FolderViewer';
}
