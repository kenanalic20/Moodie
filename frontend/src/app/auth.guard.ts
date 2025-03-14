import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class AuthGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private router: Router
    ) {}

    canActivate(): Observable<boolean> {
        return this.authService.isAuthenticated().pipe(
            map(isAuthenticated => {
                if (isAuthenticated) {
                    return true;
                } else {
                    this.router.navigate(['auth/login']);
                    return false;
                }
            }),
            catchError(error => {
                this.router.navigate(['auth/login']);
                return of(false);
            })
        );
    }
}
