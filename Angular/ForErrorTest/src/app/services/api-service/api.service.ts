import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = 'https://localhost:7295/api';

  constructor(private http: HttpClient) {}

  public addNumbers(a: number, b: number): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/math/add?a=${a}&b=${b}`);
  }

  public getWeatherForecast(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/weather/forecast`);
  }

  public getUserInfo(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/user/info`);
  }
}
