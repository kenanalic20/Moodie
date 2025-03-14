import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class AlreadyLoggedInGuard implements CanActivate {
    constructor(
        private router: Router,
        private authService: AuthService
    ) {}

    canActivate(): Observable<boolean> {
        return this.authService.isAuthenticated().pipe(
            map(isAuthenticated => {
                if (isAuthenticated) {
                    this.router.navigate(['/dashboard']);
                    return false;
                }
                return true;
            })
        );
    }
}
