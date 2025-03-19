import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class UserLocationService {
  private apiUrl = 'https://localhost:8001/api';
  constructor(private http:HttpClient) { }

  addUserLocation(locationInfo: any) {
    return this.http.post(`${this.apiUrl}/user-location`, locationInfo,{  withCredentials:true  });
  }
  getUserLocation() {
    return this.http.get(`${this.apiUrl}/user-location`,{  withCredentials:true  });
  }
  deleteUserLocation() {
    return this.http.delete(`${this.apiUrl}/user-location`,{  withCredentials:true  });
  }
}
