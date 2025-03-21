import { Component } from '@angular/core';
import { ApiService } from '../../services/api-service/api.service';
import { CommonModule } from '@angular/common';
import { PostNullService } from '../../services/post-null/post-null.service';

@Component({
  selector: 'app-error-page',
  imports: [CommonModule],
  templateUrl: './error-page.component.html',
  styleUrl: './error-page.component.scss',
})
export class ErrorPageComponent {
  mathResult: number | null = null;
  mathError: string | null = null;

  weatherForecast: any = null;
  weatherError: string | null = null;

  userInfo: any = null;
  userError: string | null = null;

  nullPostInfo: any = null;
  nullPostError: string | null = null;

  constructor(
    private apiService: ApiService,
    private postNullService: PostNullService
  ) {}

  public addNumbers(): void {
    this.mathError = null;
    this.apiService.addNumbers(2, 3).subscribe({
      next: (res) => {
        this.mathResult = res;
      },
      error: (err) =>
        (this.mathError = `Code: ${err.status}, Message: ${err.message}`),
    });
  }

  public getWeatherForecast(): void {
    this.weatherError = null;
    this.apiService.getWeatherForecast().subscribe({
      next: (res) => {
        this.weatherForecast = res;
      },
      error: (err) =>
        (this.weatherError = `Code: ${err.status}, Message: ${err.message}`),
    });
  }

  public getUserInfo(): void {
    this.userError = null;
    this.apiService.getUserInfo().subscribe({
      next: (res) => {
        this.userInfo = res;
      },
      error: (err) =>
        (this.userError = `Code: ${err.status}, Message: ${err.message}`),
    });
  }

  public postNull(): void {
    this.postNullService.postNull().subscribe({
      next: (res) => {
        this.nullPostInfo = res;
      },
      error: (err) =>
        (this.nullPostError = `Code: ${err.status}, Message: ${err.message}`),
    });
  }
}
