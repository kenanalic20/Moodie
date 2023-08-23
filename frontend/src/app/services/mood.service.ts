import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";


@Injectable({
  providedIn: 'root'
})
export class MoodService {
  private apiUrl = 'http://localhost:8000/api';

  constructor( private http:HttpClient) {
  }
  addMood(moodValue :number){
    const url = `${this.apiUrl}/add-mood`;
    const body = {moodValue }
    return this.http.post(url, body,{withCredentials:true});
  }
  getMoods(){
    const url = `${this.apiUrl}/get-mood`;
    return this.http.get(url,{withCredentials:true});
  }

}
