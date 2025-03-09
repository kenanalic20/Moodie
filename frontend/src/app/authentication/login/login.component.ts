import { Component } from '@angular/core';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) { }
  faGoogle = faGoogle;
  isDevelopment = isDev;
  codeIcon = faCode;
  emailRegex = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;
  email: string = '';
  password: string = '';
  verificationCode: string = '';
  hide = true;
  hideLogin = false;
  hideVerification = true;



  onEmailChange(email: any) {
    this.email = email;
  }

  onPasswordChange(password: any) {
    this.password = password;
  }
  onVerificationCodeChange(verificationCode: any) {
    this.verificationCode = verificationCode;
  }

  login() {
    if (this.password === '' || this.email === '' || !this.email.match(this.emailRegex) || this.password.length < 8) {
      this.toastr.error('Please enter valid credentials', 'Error')
      console.log(this.email, this.password, this.verificationCode);
      return;
    }

    this.authService.login(this.email, this.password).subscribe((response) => {
      this.toastr.success('Check your email', 'Success')
      this.hide = false;
      this.hideLogin = true;
      this.hideVerification = false;
    },
      (error) => {
        this.toastr.error(error.error, 'Error');
      });
  }
  submitVerificationCode() {
    if (this.verificationCode === '') {
      this.toastr.error('Please enter valid verification code', 'Error')
      return;
    }
    this.authService.login(this.email, this.password, this.verificationCode).subscribe((response) => {
      this.toastr.success('Logged in', 'Success')
      this.router.navigate(['/dashboard']);
    });
  }
}
