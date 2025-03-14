import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class MoodActivityService {
    private apiUrl = 'https://localhost:8001/api';

    constructor(private http: HttpClient) {}
    addMoodActivity(moodId: number, activityId: number) {
        const url = `${this.apiUrl}/moodactivity`;
        const body = {
            moodId,
            activityId,
        };
        console.log(body);
        return this.http.post(url, body, { withCredentials: true });
    }

    deleteMoodActivity(moodID: number, activityId: number) {
        const url = `${this.apiUrl}/delete/${moodID}/${activityId}`;
        return this.http.delete(url, { withCredentials: true });
    }
}
