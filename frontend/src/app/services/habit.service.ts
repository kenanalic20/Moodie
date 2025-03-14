import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Habit } from '../models/habit';
import { map, catchError, switchMap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class HabitService {
    private apiUrl = 'https://localhost:8001/api';
    private headers = new HttpHeaders().set('Content-Type', 'application/json');
    private bosnianTimeZone = 'Europe/Sarajevo';

    constructor(private http: HttpClient) {}

    getHabits(): Observable<Habit[]> {
        return this.http.get<Habit[]>(`${this.apiUrl}/habits`, {
            headers: this.headers,
            withCredentials: true,
        });
    }

    createHabit(name: string, description: string): Observable<any> {
        return this.http.post(
            `${this.apiUrl}/habits`,
            { name, description },
            {
                headers: this.headers,
                withCredentials: true,
            }
        );
    }

    updateHabit(
        id: number,
        name: string,
        description: string
    ): Observable<Habit> {
        return this.http.put<Habit>(
            `${this.apiUrl}/habits/${id}`,
            { name, description },
            {
                headers: this.headers,
                withCredentials: true,
            }
        );
    }

    deleteHabit(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/habits/${id}`, {
            headers: this.headers,
            withCredentials: true,
        });
    }

    checkIn(id: number): Observable<Habit> {
        return this.http.post<Habit>(
            `${this.apiUrl}/habits/${id}/check-in`,
            {},
            {
                headers: this.headers,
                withCredentials: true,
            }
        );
    }

    canCheckInToday(habit: Habit): boolean {
        if (!habit.lastCheckIn) return true; // No check-in yet

        const lastCheckIn = new Date(habit.lastCheckIn);

        // Get current date in Bosnian time zone
        const now = new Date();

        // Convert both dates to Bosnian timezone string format (YYYY-MM-DD)
        const lastCheckInBosnian = this.convertToBosnianDate(lastCheckIn);
        const todayBosnian = this.convertToBosnianDate(now);

        // Compare dates in Bosnian timezone
        return lastCheckInBosnian !== todayBosnian;
    }

    private convertToBosnianDate(date: Date): string {
        // Convert the date to Bosnian timezone string (YYYY-MM-DD)
        return date.toLocaleDateString('en-US', {
            timeZone: this.bosnianTimeZone,
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
        });
    }
}
