import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

export interface Settings {
	darkMode?: boolean;
	languageId?: number;
	twoFactorEnabled?: boolean;
}

@Injectable({
	providedIn: "root",
})
export class SettingsService {
	private apiUrl = "https://localhost:8001/api";
	private headers = new HttpHeaders().set("Content-Type", "application/json");

	constructor(private http: HttpClient) {}

	getSettings(): Observable<Settings> {
		return this.http.get<Settings>(`${this.apiUrl}/settings`, {
			headers: this.headers,
			withCredentials: true,
		});
	}

	updateSettings(settings: Settings): Observable<any> {
		return this.http.put(`${this.apiUrl}/settings`, settings, {
			headers: this.headers,
			withCredentials: true,
		});
	}
}
