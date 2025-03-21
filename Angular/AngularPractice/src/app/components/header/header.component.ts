import { Component, NgZone } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  time: string = '';
  private timer: any;

  constructor(private ngZone: NgZone) {}

  ngOnInit() {
    this.startClock();
  }

  ngOnDestroy() {
    this.stopClock();
  }

  startClock() {
    this.ngZone.runOutsideAngular(() => {
      this.timer = setInterval(() => {
        this.ngZone.run(() => {
          this.updateTime();
        });
      }, 1000);
    });
  }

  stopClock() {
    if (this.timer) {
      clearInterval(this.timer);
    }
  }

  updateTime() {
    const now = new Date();
    this.time = now.toLocaleTimeString();
  }
}
