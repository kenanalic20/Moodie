import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class NotesService {
	private apiUrl = "https://localhost:8001/api";
	constructor(private http: HttpClient) {}
	addNotes(formData: FormData) {
		const url = `${this.apiUrl}/notes`;
		return this.http.post(url, formData, { withCredentials: true });
	}
	getNotes() {
		const url = `${this.apiUrl}/notes`;
		return this.http.get(url, { withCredentials: true });
	}
	
	deleteNotes(id:number){
		const url = `${this.apiUrl}/notes/${id}`;
		return this.http.delete(url,{withCredentials:true});
	}

	getNoteById(id:number) {
		const url = `${this.apiUrl}/notes/${id}`;
		return this.http.get(url,{withCredentials:true});
	}

	updateNotes(formData: FormData) {
		const url = `${this.apiUrl}/notes`;
		return this.http.put(url, formData, { withCredentials: true });
	}
}
