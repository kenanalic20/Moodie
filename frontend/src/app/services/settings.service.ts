import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  private headers = new HttpHeaders().set("Content-Type", "application/json");

  constructor(private http: HttpClient) { }

  getSettings() {
    const url = `https://localhost:8001/api/settings`;
    return this.http.get(url, { withCredentials: true });
  }

  updateSettings(settings: any) {
    const url = `https://localhost:8001/api/settings`;
    const body = settings;
    return this.http.put(url, body, { headers: this.headers, withCredentials: true, responseType: 'text' });
  }
}
