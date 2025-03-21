import { Component, computed, signal } from '@angular/core';
import { CurrencyFormatPipe } from '../../pipes/currency-format.pipe';

@Component({
  selector: 'app-signals',
  imports: [CurrencyFormatPipe],
  templateUrl: './signals.component.html',
  styleUrl: './signals.component.scss',
})
export class SignalsComponent {
  creaditAmount = 100000;
  creditPercent = 0.01;
  years = signal(1);

  totalPayment = computed(() => {
    const totalAmount =
      this.creaditAmount * Math.pow(1 + this.creditPercent, this.years());
    return totalAmount;
  });

  overpayment = computed(() => {
    return this.totalPayment() - this.creaditAmount;
  });

  update(event: Event) {
    const input = event.target as HTMLInputElement;
    this.years.set(parseInt(input.value, 10));
  }
}
