import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { SignalRService } from '../../services/signlaR/signal-r.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-about-page',
  imports: [RouterLink, RouterOutlet, CommonModule],
  templateUrl: './about-page.component.html',
  styleUrl: './about-page.component.scss',
})
export class AboutPageComponent {
  constructor(public signalRService: SignalRService) {}
}
