import { Component, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from '../not-found/not-found.component';
import { FormsModule } from '@angular/forms';
import { TextBlockComponent } from '../text-block/text-block.component';
import { SignalsComponent } from '../signals/signals.component';

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [CommonModule, FormsModule],
  // encapsulation: ViewEncapsulation.Emulated;
})
export class HomeComponent {
  @ViewChild('dynamicContainer', { read: ViewContainerRef, static: true })
  dynamicContainer!: ViewContainerRef;

  ViewCard(component: string): void {
    this.dynamicContainer.clear();

    let componentType: Type<any>;
    let componentInput: string;

    if (component === 'news') {
      componentType = TextBlockComponent;
      componentInput = 'Новости';
    } else if (component === 'discount') {
      componentType = TextBlockComponent;
      componentInput = 'Скидки!';
    } else if (component === 'signal') {
      componentType = SignalsComponent;
      componentInput = '';
    } else {
      componentType = NotFoundComponent;
      componentInput = '';
    }

    const componentRef = this.dynamicContainer.createComponent(componentType);
    if (componentRef.instance instanceof TextBlockComponent) {
      componentRef.instance.text = componentInput;
    }
  }

  images = [
    'https://visme.co/blog/wp-content/uploads/2016/09/website-1024x512.jpg',
    'https://visme.co/blog/wp-content/uploads/2016/09/website2-1024x512.jpg',
    'https://visme.co/blog/wp-content/uploads/2016/09/website3-1024x512.png',
    'https://visme.co/blog/wp-content/uploads/2016/09/website4-1024x512.jpg',
    'https://visme.co/blog/wp-content/uploads/2016/09/website5-1024x512.jpg',
    'https://visme.co/blog/wp-content/uploads/2016/09/website6-1024x512.jpg',
    'https://visme.co/blog/wp-content/uploads/2016/09/website7-1024x512.jpg',
    'https://visme.co/blog/wp-content/uploads/2016/09/website8-1024x512.jpg',
    'https://visme.co/blog/wp-content/uploads/2016/09/website9-1024x512.jpg',
    'https://visme.co/blog/wp-content/uploads/2016/09/website10-1024x512.jpg',
  ];
}
