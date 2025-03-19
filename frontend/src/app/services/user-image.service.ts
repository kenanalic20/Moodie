import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserImageService {
  private apiUrl = 'https://localhost:8001/api';
  private headers = new HttpHeaders({
    'Content-Type': 'multipart/form-data'
  });


  constructor(private http:HttpClient) { }

  addUserImage(formData: FormData) {
    const url = `${this.apiUrl}/user-image`;
    return this.http.post(url, formData, { withCredentials: true });
  }
  getUserImage() {
    const url = `${this.apiUrl}/user-image`;
    return this.http.get(url, { withCredentials: true });
  }
  deleteUserImage() {
    const url = `${this.apiUrl}/user-image`;
    return this.http.delete(url, { withCredentials: true })
  }

}
