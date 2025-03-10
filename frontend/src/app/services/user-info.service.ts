import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

export interface UserInfo {
	firstName?: string;
	lastName?: string;
	gender?: string;
	birthday?: Date;
}

@Injectable({
	providedIn: "root",
})
export class UserInfoService {
	private apiUrl = "https://localhost:8001/api";

	constructor(private http: HttpClient) {}

	getUserInfo(): Observable<UserInfo> {
		return this.http.get<UserInfo>(`${this.apiUrl}/user-info`, { withCredentials: true });
	}

	updateUserInfo(userInfo: UserInfo): Observable<any> {
		return this.http.put(`${this.apiUrl}/user-info`, userInfo, { withCredentials: true });
	}

	deleteUserInfo(): Observable<any> {
		return this.http.delete(`${this.apiUrl}/user-info`, { withCredentials: true });
	}
}
