import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserAchievement, Achievement } from '../models/achievement.model';

@Injectable({
    providedIn: 'root',
})
export class AchievementsService {
    private apiUrl = 'https://localhost:8001/api/achievement';

    constructor(private http: HttpClient) {}

    getUserAchievements(): Observable<UserAchievement[]> {
        return this.http.get<UserAchievement[]>(this.apiUrl, {
            withCredentials: true,
        });
    }

    // This method could be used in the future when the endpoint is implemented
    getAllAchievements(): Observable<Achievement[]> {
        return this.http.get<Achievement[]>(`${this.apiUrl}/all`, {
            withCredentials: true,
        });
    }
}
