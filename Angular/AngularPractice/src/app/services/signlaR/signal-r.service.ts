import { Injectable, signal, NgZone } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  public signalMessages = signal<string[]>([]);
  private messageCount = 0;

  constructor(private ngZone: NgZone) {
    this.startConnection();
    this.addReceiveMessageListener();
  }

  private startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7295/chatHub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();

    this.hubConnection
      .start()
      .catch((err) => console.error('Error while starting connection: ' + err));
  }

  private addReceiveMessageListener() {
    this.hubConnection.on('ReceiveMessage', (message: string) => {
      // this.ngZone.runOutsideAngular(() => {
      console.log('Message received outside Angular zone:', message);
      this.processMessage(message);
      // });
    });
  }

  private processMessage(message: string) {
    this.messageCount++;
    this.signalMessages.update((messages) => [...messages, message]);
  }

  public sendMessage(message: string): void {
    this.hubConnection
      .invoke('SendMessage', message)
      .catch((err) => console.error(err));
  }
}
