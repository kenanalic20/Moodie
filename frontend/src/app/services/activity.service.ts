import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {
  private apiUrl = 'http://localhost:8000/api';

  constructor( private http:HttpClient) {
  }
  addActivity(Name :string,Description:string){
    const url = `${this.apiUrl}/mood/activities`;
    const body = {
      Name,
      Description
    }
    console.log(body);
    return this.http.post(url, body,{withCredentials:true});
  }
  getActivitiesByUserId(){
    const url = `${this.apiUrl}/mood/activities`;
    return this.http.get(url,{withCredentials:true});
  }
}
