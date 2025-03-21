import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencyFormat',
})
export class CurrencyFormatPipe implements PipeTransform {
  transform(
    value: number | null | undefined,
    currency: string = 'BYN'
  ): string {
    if (value == null || isNaN(value)) {
      return '';
    }
    const roundedValue = Math.round(value * 100) / 100;

    let locale: string = 'be-BY';
    if (currency === 'BYN') {
      locale = 'be-BY';
    } else if (currency === 'USD') {
      locale = 'en-US';
    } else {
      return `${roundedValue} ${currency}`;
    }

    return new Intl.NumberFormat(locale, {
      style: 'currency',
      currency: currency,
    }).format(roundedValue);
  }
}
