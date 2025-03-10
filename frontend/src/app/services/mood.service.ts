import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Mood } from "../types";

@Injectable({
	providedIn: "root",
})
export class MoodService {
	private apiUrl = "https://localhost:8001/api";

	constructor(private http: HttpClient) {}

	getMoods(): Observable<any> {
		const url = `${this.apiUrl}/mood`;
		return this.http.get(url, { withCredentials: true });
	}

	addMood(mood: Mood): Observable<any> {
		const url = `${this.apiUrl}/mood`;
		// get date from query params
		const date = new URLSearchParams(window.location.search).get("date");
		const moodDate = date ? new Date(new Date(date).getTime() + 24 * 60 * 60 * 1000) : mood.date;

		return this.http.post(
			url,
			{
				moodValue: mood.moodValue,
				date: moodDate,
				notes: mood.notes,
			},
			{ withCredentials: true },
		);
	}
}
