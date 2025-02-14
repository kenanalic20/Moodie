import { Component } from '@angular/core';
import { Language, TranslateService } from '@ngx-translate/core';
import { LanguageService } from '../services/language.service';
import { AuthService } from '../services/auth.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
@Component({
  selector: 'app-language-switcher',
  templateUrl: './language-switcher.component.html',
})
export class LanguageSwitcherComponent {
  languages: Array<{id:number, code: string, name: string, region:string}> = [];

  constructor(private translate: TranslateService,private languageService:LanguageService,private authService:AuthService) {}

  ngOnInit() {
    this.languageService.getLanguage().subscribe((data: any) => {
      for (const lang of data) {
        this.languages.push(lang);
      }
    });
  }

  switchLanguage(language: string) {
    this.authService.isAuthenticated().pipe(
      catchError((error) => {
        console.log('Error checking authentication:', error);
        return of(false);
      })
    ).subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        console.log('User is authenticated');
        
      } else {
        console.log('User is not authenticated');
      }
    });

    localStorage.setItem('Language', language);
    this.translate.use(language);
    // pipe(
    //       map((isAuthenticated) => {
    //         if(!isAuthenticated) {
    //           console.log('User is not authenticated');
    //         }
            
    //         console.log('User is authenticated');
            
    //       }),
    //       catchError((error) => {
    //         localStorage.setItem('Language', language);
    //         this.translate.use(language);
    //         return of(false);
    //       })
    //     );
  }
}
