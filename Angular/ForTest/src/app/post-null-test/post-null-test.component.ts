import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-post-null-test',
  imports: [],
  templateUrl: './post-null-test.component.html',
  styleUrl: './post-null-test.component.scss',
})
export class PostNullTestComponent {
  constructor(private http: HttpClient) {}

  public SendNull() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', // Указываем правильный Content-Type
    });

    this.http
      .post('https://localhost:7295/api/Main/post-null', null, { headers })
      .subscribe();
  }
}
