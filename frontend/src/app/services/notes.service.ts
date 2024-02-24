import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  private apiUrl = 'http://localhost:8000/api';
  constructor(private http:HttpClient) { }
  addNotes(Title: string, Image:File|null, Description: string) {
    const url = `${this.apiUrl}/notes`;

      const formData = new FormData();
      formData.append('Title', Title);
      if(Image){
        formData.append('Image', Image);
      }
      formData.append('Description', Description);
      return this.http.post(url, formData, { withCredentials: true });

  }
  getNotes(){
    const url = `${this.apiUrl}/notes`;
    return this.http.get(url,{withCredentials:true});
  }

}
