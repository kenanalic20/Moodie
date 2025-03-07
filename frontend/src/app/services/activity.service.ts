import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {
  private apiUrl = 'https://localhost:8001/api';

  constructor( private http:HttpClient) {
  }

  addActivity(Name? :string, Description?: string){
    const url = `${this.apiUrl}/mood/activities`;
    const body = {
      Name,
      Description
    }
    console.log(body);
    return this.http.post(url, body,{withCredentials:true});
  }

  updateActivity(MoodId?:number,ActivityId?:number,Name?:string,Description?:string){ 
    const url = `${this.apiUrl}/mood/activities`;
    const body = {
      id: ActivityId,
      moodId: MoodId,
      name: Name,
      description: Description
    }
    return this.http.post(url,body, { withCredentials: true });
  }

  getActivitiesByUserId(){
    const url = `${this.apiUrl}/mood/activities`;
    return this.http.get(url,{withCredentials:true});
  }

  getBestMoodActivities(){
    const url = `${this.apiUrl}/mood/activities/best`;
    return this.http.get(url,{withCredentials:true});
  }

  getWorstMoodActivities(){
    const url = `${this.apiUrl}/mood/activities/worst`;
    return this.http.get(url,{withCredentials:true});  
  }

  deleteActivity(id:number){
    const url = `${this.apiUrl}/mood/activities/${id}`;
    return this.http.delete(url,{withCredentials:true});
  }
  
}
