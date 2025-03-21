import { Component, Input, OnInit } from '@angular/core';
import { CurrencyFormatPipe } from '../../../pipes/currency-format.pipe';

@Component({
  selector: 'app-card',
  standalone: true,
  imports: [CurrencyFormatPipe],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss',
})
export class CardComponent {
  @Input() name: string = 'Карта Престиж';
  @Input() number: number = 0;
  @Input() format: string = 'BYN';
  @Input() cardNumber: string = 'BYN';
  @Input() expiryDate: string = 'BYN';
}
