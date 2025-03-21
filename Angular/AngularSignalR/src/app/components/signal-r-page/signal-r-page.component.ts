import { Component, effect } from '@angular/core';
import { SignalRService } from '../../services/signlaR/signal-r.service';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'app-signal-r-page',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './signal-r-page.component.html',
  styleUrl: './signal-r-page.component.scss',
})
export class SignalRPageComponent {
  public allMessages: string[] = [];
  public selectedFile!: File;

  public signalForm: FormGroup = new FormBuilder().group({
    receiverName: ['', Validators.required],
    message: ['', Validators.required],
  });

  public connectForm: FormGroup = new FormBuilder().group({
    sender: ['', Validators.required],
  });

  public fileForm: FormGroup = new FormBuilder().group({});

  constructor(
    private signalRService: SignalRService,
    private http: HttpClient
  ) {
    effect(() => {
      this.allMessages = this.signalRService.signalMessages();
    });
  }

  public connectTo(): void {
    this.signalRService.connectTo(this.connectForm.get('sender')?.value);
  }

  public sendSignal(): void {
    const receiver = this.signalForm.get('receiverName')?.value;
    const message = this.signalForm.get('message')?.value;

    this.signalRService.sendMessage(receiver, message);
  }

  public onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0] as File;
  }

  public uploadFile(): void {
    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.http
      .post<{ text: string }>(
        'https://localhost:7295/api/Main/upload',
        formData
      )
      .subscribe({
        next: (response) => {
          console.log('Файл успешно загружен:', response);
          this.allMessages.push('Файл успешно загружен:' + response.text);
        },
        error: (err) => {
          console.error('Ошибка при загрузке файла:', err);
          this.allMessages.push(`Ошибка при загрузке файла.
            Проверьте расширение файла`);
        },
      });
  }

  public sendNullPost(): void {
    console.log('qwerty');
    let a: { text: string };

    this.http
      .post<{ text: string }>(
        'https://localhost:7295/api/Main/post-null',
        'string'
      )
      .subscribe({
        next: (response) => {
          console.log('text:', response);
        },
      });
  }
}
