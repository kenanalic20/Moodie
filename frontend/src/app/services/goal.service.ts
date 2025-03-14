import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Goal } from '../models/goal';

@Injectable({
    providedIn: 'root',
})
export class GoalService {
    private apiUrl = 'https://localhost:8001/api/goal';
    private headers = new HttpHeaders().set('Content-Type', 'application/json');

    constructor(private http: HttpClient) {}

    getGoals(): Observable<Goal[]> {
        return this.http.get<Goal[]>(this.apiUrl, {
            headers: this.headers,
            withCredentials: true,
        });
    }

    createGoal(goal: Goal): Observable<Goal> {
        return this.http.post<Goal>(this.apiUrl, goal, {
            headers: this.headers,
            withCredentials: true,
        });
    }

    updateGoal(goal: Goal): Observable<Goal> {
        // Make sure the ID is included in both the URL and the payload
        const url = `${this.apiUrl}/${goal.id}`;
        return this.http.put<Goal>(url, goal, {
            headers: this.headers,
            withCredentials: true,
        });
    }

    deleteGoal(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`, {
            headers: this.headers,
            withCredentials: true,
        });
    }
}
