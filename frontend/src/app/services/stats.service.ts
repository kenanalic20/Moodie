import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class StatsService {
  private apiUrl = 'https://localhost:8001/api';

  constructor(private http:HttpClient) { }

  getStats() {
    const url = `${this.apiUrl}/stats`;
    return this.http.get(url, { withCredentials: true });
  }

}
