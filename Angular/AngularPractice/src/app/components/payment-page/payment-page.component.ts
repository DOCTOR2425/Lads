import { Component, OnInit, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentService } from '../../services/payment/payment.service';
import { HttpClient } from '@angular/common/http';
import { SignalRService } from '../../services/signlaR/signal-r.service';

@Component({
  standalone: true,
  imports: [CommonModule],
  providers: [PaymentService],
  selector: 'app-payment-page',
  templateUrl: './payment-page.component.html',
  styleUrl: './payment-page.component.scss',
})
export class PaymentPageComponent implements OnInit {
  public countData: string = '';
  public timeData: string = '';
  public allMessages: string[] = [];
  public displayedMessages: string[] = [];

  constructor(
    private paymentService: PaymentService,
    private signalRService: SignalRService,
    private http: HttpClient
  ) {
    effect(() => {
      this.allMessages = this.signalRService.signalMessages();
      this.updateDisplayedMessages();
    });
  }

  public ngOnInit(): void {
    this.http
      .get<{ text: string }>('https://localhost:7295/api/PrestigeBank/get-data')
      .subscribe((val) => {
        console.log(val.text);
        this.timeData = val.text;
      });
  }

  public pay(): void {
    this.paymentService.createPayment().subscribe((response) => {
      console.log('Response:', response);
    });
  }

  public sendSignal(): void {
    this.signalRService.sendMessage('Работает!!!!');
  }

  public serviceWorker(): void {
    this.http
      .get<{ date: string }>(
        'https://localhost:7295/api/PrestigeBank/service-worker'
      )
      .subscribe((val) => {
        this.countData = val.date.toString();
      });
    this.http
      .get<{ text: string }>('https://localhost:7295/api/PrestigeBank/get-data')
      .subscribe((val) => {
        console.log(val.text);
        this.timeData = val.text;
      });
  }

  private updateDisplayedMessages(): void {
    if (this.allMessages.length % 3 === 0) {
      this.displayedMessages = [];
      for (let i = 0; i < this.allMessages.length; i++)
        this.displayedMessages.push(i + 1 + ': ' + this.allMessages[i]);
    }
  }
}
