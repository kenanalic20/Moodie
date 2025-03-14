import { Component } from '@angular/core';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
})
export class RegisterComponent {
    constructor(
        private authService: AuthService,
        private router: Router,
        private toastrService: ToastrService,
        private translateService: TranslateService
    ) {}
    faGoogle = faGoogle;
    isDevelopment = isDev;
    codeIcon = faCode;

    username: string = '';
    email: string = '';
    password: string = '';
    confirmPassword: string = '';
    emailRegex = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;

    onUsernameChange(username: any) {
        this.username = username;
    }

    onEmailChange(email: any) {
        this.email = email;
    }

    onPasswordChange(password: any) {
        this.password = password;
    }

    onConfirmPasswordChange(confirmPassword: any) {
        this.confirmPassword = confirmPassword;
    }

    register() {
        if (
            this.password === '' ||
            this.confirmPassword === '' ||
            this.username === '' ||
            this.email === '' ||
            !this.email.match(this.emailRegex) ||
            this.password.length < 8
        ) {
            this.toastrService.error(
                this.translateService.instant('Please enter valid credentials'),
                this.translateService.instant('Error')
            );

            return;
        }
        if (this.password !== this.confirmPassword) {
            this.toastrService.success(
                this.translateService.instant('Passwords do not match'),
                this.translateService.instant('Error')
            );
            return;
        }
        console.log(
            this.username,
            this.email,
            this.password,
            this.confirmPassword
        );
        this.authService
            .register(this.username, this.email, this.password)
            .subscribe(response => {
                console.log(response);
                this.toastrService.success(
                    this.translateService.instant('Registered successfully'),
                    this.translateService.instant('Success')
                );
                this.router.navigate(['/login']);
            });
    }
}
