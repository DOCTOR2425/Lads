import { Injectable, signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  public signalMessages = signal<string[]>([]);

  public connectTo(userName: string): void {
    if (
      this.hubConnection &&
      this.hubConnection.state === signalR.HubConnectionState.Connected
    ) {
      console.warn('Connection is already established.');
      this.signalMessages.update((messages) => [
        ...messages,
        'Вы уже зарегистированны',
      ]);
      return;
    }
    this.startConnection(userName);
    this.addReceiveMessageListener();
  }

  private startConnection(userName: string) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`https://localhost:7295/chatHub?userName=${userName}`, {
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
      this.signalMessages.update((messages) => [...messages, message]);
    });
  }

  public sendMessage(recipient: string, message: string): void {
    console.log(this.hubConnection.state);
    this.hubConnection
      .invoke('SendMessage', recipient, message)
      .catch((err) => console.error(err));
  }
}
