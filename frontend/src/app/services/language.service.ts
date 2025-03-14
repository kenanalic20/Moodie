import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
    providedIn: 'root',
})
export class LanguageService {
    private apiUrl = 'https://localhost:8001/api';

    constructor(
        private http: HttpClient,
        private translateService: TranslateService
    ) {}

    getLanguage(): Observable<any> {
        return this.http.get('/assets/i18n/languages.json');
    }

    getLanguageById(id: number) {
        const url = `${this.apiUrl}/languages/${id}`;
        return this.http.get(url, { withCredentials: true });
    }

    // New helper method to ensure consistent language handling
    setCurrentLanguage(language: string): void {
        const lang = language.toUpperCase();
        localStorage.setItem('language', lang);
        this.translateService.use(lang.toLowerCase());
        console.log('Language service set language to:', lang);
    }

    // Get current language with fallback
    getCurrentLanguage(): string {
        return localStorage.getItem('language') || 'EN';
    }
}
