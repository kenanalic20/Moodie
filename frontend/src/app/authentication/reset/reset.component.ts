import { Component } from '@angular/core';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-login',
    templateUrl: './reset.component.html',
})
export class ResetComponent {
    faGoogle = faGoogle;
    isDevelopment = isDev;
    codeIcon = faCode;
    email: any = '';
    password: any = '';
    response?: any;
    token?: string;
    constructor(
        private route: ActivatedRoute,
        private authService: AuthService,
        private toastrService: ToastrService,
        private translateService: TranslateService
    ) {}

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            this.token = params['token'];
        });
    }

    onEmailChange(email: any) {
        this.email = email;
    }

    onPasswordChange(password: any) {
        this.password = password;
    }

    requestReset() {
        if (this.email == '') {
            this.toastrService.error(
                this.translateService.instant('Please enter valid credentials'),
                this.translateService.instant('Error')
            );

            return;
        }
        if (!this.token) {
            this.authService
                .requestResetPassword(this.email)
                .subscribe((res: any) => {
                    this.toastrService.success(
                        this.translateService.instant(res.message),
                        this.translateService.instant('Success')
                    );
                });
        }
    }

    resetPassword() {
        if (!this.token) return;
        console.log(this.token, this.password);
        this.authService.resetPassword(this.token, this.password).subscribe(
            (res: any) => {
                this.toastrService.success(
                    this.translateService.instant(res.message),
                    this.translateService.instant('Success')
                );
            },
            error => {
                this.toastrService.error(
                    this.translateService.instant(error.error),
                    this.translateService.instant('Error')
                );
            }
        );
    }
}
