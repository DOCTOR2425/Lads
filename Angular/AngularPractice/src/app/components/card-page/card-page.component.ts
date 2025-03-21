import {
  Component,
  ViewChild,
  ViewContainerRef,
  ViewChildren,
  QueryList,
  AfterViewInit,
} from '@angular/core';
import { FormComponent } from './form/form.component';
import { CardComponent } from './card/card.component';
import { CanComponentDeactivate } from '../../guards/can-diactivate/can-deactivate.guard';
import { Observable } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-card-page',
  imports: [FormComponent],
  templateUrl: './card-page.component.html',
  styleUrls: ['./card-page.component.scss'],
})
export class CardPageComponent
  implements CanComponentDeactivate, AfterViewInit
{
  @ViewChild('myCard', { read: ViewContainerRef }) container!: ViewContainerRef;
  @ViewChildren(CardComponent) cards!: QueryList<CardComponent>;

  public createdCards: CardComponent[] = [];

  public handleFormSubmit(data: any) {
    const newCard = this.container.createComponent(CardComponent);
    newCard.setInput('name', data.cardName);
    newCard.setInput('number', data.cardBalance);
    newCard.setInput('format', data.currency);
    this.createdCards.push(newCard.instance);
  }

  ngAfterViewInit() {
    this.cards.changes.subscribe(() => {
      this.updateCards();
    });
  }

  private updateCards() {
    this.createdCards = this.cards.toArray();
  }

  canDeactivate(): Observable<boolean> | boolean {
    if (this.createdCards.length == 0) {
      return confirm('Уверенны, что хотите выйти не создав ни одной карты?');
    }
    return true;
  }
}
