import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:8000/api';
  private jwtCookieName = 'jwt';
  constructor(private http:HttpClient) {}
  login(email: string, password: string) {
    const url = `${this.apiUrl}/login`;
    const body = { email, password };
    return  this.http.post(url, body,{withCredentials:true}).pipe(catchError((error:HttpResponse<any>)=>{
      alert("Login Failed");
      throw error;
    }));
  }
  register(username: string,email:string ,password: string) {
    const url = `${this.apiUrl}/register`;
    const body = { username,email,password };
    return this.http.post(url, body);
  }
  logout():Observable<any> {
    const url = `${this.apiUrl}/logout`;
    return this.http.post(url, null, { withCredentials: true });
  }
  user() {
    const url = `${this.apiUrl}/user`;
    return this.http.get(url, { withCredentials: true });
  }
  clearUserCookie() {
    // Clear user data from local storage or any other storage mechanism
    document.cookie = `${this.jwtCookieName}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`
  }
}
