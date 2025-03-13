import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private apiUrl = 'https://localhost:8001/api';

  constructor(private http:HttpClient) { }
  getLanguage() {
    const url = `${this.apiUrl}/languages`;
    return this.http.get(url);
  }
  getLanguageById(id:number) {
    const url = `${this.apiUrl}/languages/${id}`;
    return this.http.get(url,{withCredentials:true});
  }
}
