import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { Observable, catchError } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { map } from "rxjs/operators";
import { of } from "rxjs";
@Injectable({
	providedIn: "root",
})
export class AuthService {
	private apiUrl = "https://localhost:8001/api";
	private jwtCookieName = "jwt";
	constructor(
		private http: HttpClient,
		private toastrService: ToastrService,
	) {}
	login(email: string, password: string, twoStepCode?: string) {
		const url = `${this.apiUrl}/login`;
		const body = { email, password, twoStepCode };
		return this.http.post(url, body, { withCredentials: true });
	}
	register(username: string, email: string, password: string) {
		const url = `${this.apiUrl}/register`;
		const body = { username, email, password };
		return this.http.post(url, body);
	}
	logout(): Observable<any> {
		const url = `${this.apiUrl}/logout`;
		return this.http.post(url, null, { withCredentials: true });
	}
	user() {
		const url = `${this.apiUrl}/user`;
		return this.http.get(url, { withCredentials: true });
	}
	requestResetPassword(email: string) {
		const url = `${this.apiUrl}/request-reset-password`;
		const body = { email };
		return this.http.post(url, body);
	}

	resetPassword(token: string, newPassword: string) {
		const url = `${this.apiUrl}/reset-password`;
		const body = { token, newPassword };
		return this.http.post(url, body);
	}

	resendVerificationEmail(email: string) {
		return this.http.post(`${this.apiUrl}/resend-verification-email`, { email });
	}

	clearUserCookie() {
		// Clear user data from local storage or any other storage mechanism
		document.cookie = `${this.jwtCookieName}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
	}
	isAuthenticated(): Observable<boolean> {
		return this.user().pipe(
			map((data) => !!data),
			catchError(() => of(false)),
		);
	}
}
