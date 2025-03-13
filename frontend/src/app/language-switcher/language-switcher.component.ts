import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from '../services/language.service';
import { AuthService } from '../services/auth.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { SettingsService } from '../services/settings.service';

@Component({
  selector: 'app-language-switcher',
  templateUrl: './language-switcher.component.html',
})
export class LanguageSwitcherComponent {
  languages: Array<{ id: number, code: string, name: string, region: string }> = [];
  isAuthenticated: boolean = false;
  settings?: any;

  constructor(
    private translateService: TranslateService,
    private languageService: LanguageService,
    private authService: AuthService,
    private settingsService: SettingsService
  ) {}

  ngOnInit() {
    this.languageService.getLanguage().subscribe((data: any) => {
      this.languages = data;
    });

    this.authService.isAuthenticated().pipe(
      catchError((error) => {
        console.log('Error checking authentication:', error);
        return of(false);
      })
    ).subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        this.isAuthenticated = true;

        this.settingsService.getSettings().subscribe((res) => {
          this.settings = res;

          const preferredLanguage = this.settings?.preferredLanguage;

          if (preferredLanguage) {
            this.switchLanguage(preferredLanguage);
          }
        });
      } else {
        this.isAuthenticated = false;
      }
    });
  }

  switchLanguage(language: string) {
    localStorage.setItem('Language', language);

    this.translateService.use(language);
  }
}