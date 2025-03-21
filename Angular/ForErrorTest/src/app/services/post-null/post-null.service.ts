import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PostNullService {
  constructor(private http: HttpClient) {}

  public postNull(): Observable<Object> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post('https://localhost:7295/api/Main/post-null', null, {
      headers,
    });
  }
}
